using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RCM.Model;
using RCM.Service;
using Microsoft.AspNetCore.SignalR;

namespace RCM.CenterHubs
{
    //[Authorize]
    public class CenterHub : Hub
    {
        private IHttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly IHubUserConnectionService _hubService;
        private HttpContext _context { get { return _contextAccessor.HttpContext; } }

        public CenterHub(IHttpContextAccessor contextAccessor, UserManager<User> userManager, IHubUserConnectionService hubUserConnectionService)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _hubService = hubUserConnectionService;
        }

        public override async Task OnConnectedAsync()
        {
            //var username = _context.User.Identity.Name;
            //var _user = _userManager.FindByNameAsync(username).Result;
            //if (_user != null)
            //{
            //    var connectionId = Context.ConnectionId;
            //    _hubService.CreateHubUserConnection(new HubUserConnection
            //    {
            //        UserId = _user.Id,
            //        Connection = connectionId
            //    });
            //    _hubService.SaveHubUserConnection();
            //    await base.OnConnectedAsync();
        //    }
        //await base.OnConnectedAsync();

        }

        //public override async Task OnDisconnectedAsync(Exception exception)
        //{
        //    //var user = _context.User;
        //    //_hubService.RemoveHubUserConnection(_ => _.Connection.Equals(Context.ConnectionId));
        //    //_hubService.SaveHubUserConnection();
        //    await base.OnDisconnectedAsync(exception);
        //}

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
