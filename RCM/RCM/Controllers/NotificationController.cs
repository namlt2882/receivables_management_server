using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using RCM.CenterHubs;
using RCM.Model;
using RCM.Service;
using RCM.ViewModels;

namespace RCM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IHubContext<CenterHub> _hubContext;
        private readonly UserManager<User> _userManager;
        private readonly IHubUserConnectionService _hubService;
        private readonly INotificationService _notiService;
        
        public NotificationController(IHubContext<CenterHub> hubContext, UserManager<User> userManager, IHubUserConnectionService hubService, INotificationService notiService)
        {
            _hubContext = hubContext;
            _userManager = userManager;
            _hubService = hubService;
            _notiService = notiService;
        }

        [HttpPost]
        public IActionResult Post([FromQuery]string userName, [FromBody]NotificationCM model)
        {
            var _user = _userManager.FindByNameAsync(userName).Result;
            if (_user != null)
            {
                var notification = new Notification
                {
                    Title = model.Title,
                    Type = model.Type,
                    Body = model.Body,
                    UserId = _user.Id,
                    NData = model.NData == null ? null : JsonConvert.SerializeObject(model.NData),
                    IsSeen = false,
                };
                _notiService.CreateNotification(notification);
                _notiService.SaveNotification();

                var connections = _hubService.GetHubUserConnections(_ => _.UserId.Equals(_user.Id));
                _hubContext.Clients.Clients(connections.Select(_ => _.Connection).ToList())
                            .SendAsync("Notify", JsonConvert.SerializeObject(notification.Adapt<NotificationVM>()));
            }
            return Ok();
        }

        [HttpPost("SendNotiByPermission")]
        public IActionResult SendNotiByPermission([FromQuery]int permissionId, [FromBody]NotificationCM model)
        {
            var jobId = BackgroundJob.Enqueue(
                    () => SendNotiAsync(permissionId, model));
            return Ok();
        }

        [HttpPost("SendNotiAsync")]
        public async Task SendNotiAsync(int permissionId, NotificationCM model)
        {
            List<string> connections = new List<string>();
            //try
            //{
            //    var permission = _permissionService.GetPermission(permissionId);
            //    if (permission == null)
            //    {
            //        return;
            //    }

            //    var userIds = _permissionService.GetUsersByPermission(permissionId);

            //    foreach (var userId in userIds)
            //    {
            //        try
            //        {
            //            var notification = CreateNotification(userId, model);
            //            _notiService.CreateNotification(notification);
            //            _notiService.SaveNotification();

            //            connections = connections.Union(_hubService.GetHubUserConnections(_ => _.UserId.Equals(userId))
            //                            .Select(_ => _.Connection).ToList()).ToList();
            //        }
            //        catch { continue; }
            //    }
            //}
            //catch (Exception e)
            //{
            //    throw;
            //}

            //Send notification
            var _notification = new NotificationVM
            {
                Title = model.Title,
                Type = model.Type,
                Body = model.Body,
                NData = model.NData == null ? null : JsonConvert.SerializeObject(model.NData),
                IsSeen = false,
            };

            _hubContext.Clients.Clients(connections)
                            .SendAsync("Notify", JsonConvert.SerializeObject(_notification.Adapt<NotificationVM>()));
        }
        private Notification CreateNotification(string userId, NotificationCM model)
        {
            return new Notification
            {
                Title = model.Title,
                Type = model.Type,
                Body = model.Body,
                UserId = userId,
                NData = model.NData == null ? null : JsonConvert.SerializeObject(model.NData),
                IsSeen = false,
            };
        }

        [Authorize]
        [HttpGet("GetNotifications")]
        public async Task<IActionResult> GetNotifications()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return BadRequest();
            var data = _notiService.GetNotifications(_ => _.UserId.Equals(user.Id)).OrderByDescending(_ => _.CreatedDate);
            List<NotificationVM> result = new List<NotificationVM>();
            foreach (var item in data)
            {
                result.Add(item.Adapt<NotificationVM>());
            }
            return Ok(result);
        }

        [Authorize]
        [HttpPut("ToggleSeen/{id}")]
        public ActionResult SwapIsSeen(int id)
        {
            try
            {
                var noti = _notiService.GetNotification(id);
                if (noti == null) return NotFound();
                noti.IsSeen = !noti.IsSeen;
                _notiService.EditNotification(noti);
                _notiService.SaveNotification();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }
    }
}
