﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RCM.Helper;
using RCM.Model;
using RCM.Service;
using RCM.ViewModels;
using RCM.ViewModels.Mobile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RCM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IProgressStageActionService _progressStageActionService;
        private readonly IAssignedCollectorService _assignedCollectorService;
        private readonly IReceivableService _receivableService;
        private readonly UserManager<User> _userManager;
        private readonly IHostingEnvironment _hostingEnvironment;
        public TaskController(IProgressStageActionService progressStageActionService, IAssignedCollectorService assignedCollectorService, IReceivableService receivableService, UserManager<User> userManager, IHostingEnvironment hostingEnvironment)
        {
            _assignedCollectorService = assignedCollectorService;
            _progressStageActionService = progressStageActionService;
            _receivableService = receivableService;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }

        [Authorize]
        [HttpPost("Done")]
        public async Task<IActionResult> MarkTaskIsDone([FromForm]UpdateTaskModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var action = _progressStageActionService.GetProgressStageAction(model.Id);
            if (action == null)
            {
                return NotFound();
            }
            try
            {
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, $"Task");
                var pictureName = $"{model.Id}-{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}{Path.GetExtension(model.File.FileName)}";
                using (var fileStream = new FileStream(Path.Combine(uploads, pictureName), FileMode.Create))
                {
                    await model.File.CopyToAsync(fileStream);
                    fileStream.Flush();
                    fileStream.Close();
                }
                action.Evidence = pictureName;
                action.Note = model.Note;
                action.Status = Constant.COLLECTION_STATUS_DONE_CODE;
                action.DoneAt = Int32.Parse(Utility.ConvertDatetimeToString(DateTime.Now));
                action.UserId = user.Id;
                _progressStageActionService.EditProgressStageAction(action);
                _progressStageActionService.SaveProgressStageAction();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("Cancel")]
        public IActionResult MarkTaskIsCancel(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var action = _progressStageActionService.GetProgressStageAction(id);
            if (action == null)
            {
                return NotFound();
            }
            action.Status = Constant.COLLECTION_STATUS_CANCEL_CODE;
            _progressStageActionService.EditProgressStageAction(action);
            _progressStageActionService.SaveProgressStageAction();

            return Ok();
        }

        [HttpGet("GetTaskByCollectorId")]
        public IActionResult GetTaskByCollectorId(string collectorId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var receivableIdList = _assignedCollectorService.GetAssignedCollectors()
                .Where(x =>
                x.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE
                && x.UserId == collectorId)
                .Select(x => x.ReceivableId).ToList();

            var rawData = from action in _progressStageActionService.GetProgressStageActions()
                          where receivableIdList.Contains(action.ProgressStage.CollectionProgress.ReceivableId)
                          && action.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                          && (action.Type == Constant.ACTION_NOTIFICATION_CODE || action.Type == Constant.ACTION_REPORT_CODE)
                          select action;

            if (rawData.Any())
            {
                var result = rawData.Select(x => new TaskVM()
                {
                    Id = x.Id,
                    ExecutionDay = x.ExcutionDay,
                    Name = x.Name,
                    StartTime = x.StartTime,
                    Status = x.Status,
                    Type = x.Type,
                    ReceivableId = x.ProgressStage.CollectionProgress.ReceivableId
                });
                return Ok(result);
            }

            return Ok(new List<TaskVM>());
        }

        [HttpGet("GetReceivableTodayTask")]
        public IActionResult GetReceivableTodayTask(int receivableId)
        {
            var rawData = _progressStageActionService.GetProgressStageActions()
                .Where(x =>
                x.ProgressStage.CollectionProgress.ReceivableId == receivableId
                && x.ExcutionDay == Int32.Parse(Utility.ConvertDatetimeToString(DateTime.Now)));

            rawData = rawData.Where(x => x.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE);

            if (rawData.Any())
            {
                var result = rawData.Select(x => new TaskVM()
                {
                    Id = x.Id,
                    ExecutionDay = x.ExcutionDay,
                    Name = x.Name,
                    StartTime = x.StartTime,
                    Status = x.Status,
                    Type = x.Type,
                    ReceivableId = x.ProgressStage.CollectionProgress.ReceivableId
                });
                return Ok(result);
            }
            return Ok(new List<TaskVM>());
        }

        [HttpGet("GetCollectorTodayTask")]
        public IActionResult GetCollectorTodayTask(string collectorId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var receivableIdList = _assignedCollectorService.GetAssignedCollectors()
                .Where(x =>
                x.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE
                && x.UserId == collectorId)
                .Select(x => x.ReceivableId).ToList();

            var rawData = from action in _progressStageActionService.GetProgressStageActions()
                          where receivableIdList.Contains(action.ProgressStage.CollectionProgress.ReceivableId)
                          && action.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                          && (action.Type == Constant.ACTION_NOTIFICATION_CODE || action.Type == Constant.ACTION_REPORT_CODE)
                          select action;

            rawData = rawData.Where(action =>
            action.ExcutionDay == Int32.Parse(Utility.ConvertDatetimeToString(DateTime.Now)));

            if (rawData.Any())
            {
                var result = rawData.Select(x => new TaskVM()
                {
                    Id = x.Id,
                    ExecutionDay = x.ExcutionDay,
                    Name = x.Name,
                    StartTime = x.StartTime,
                    Status = x.Status,
                    Type = x.Type,
                    ReceivableId = x.ProgressStage.CollectionProgress.ReceivableId
                });
                return Ok(result);
            }

            return Ok(new List<TaskVM>());
        }
        #region Mobile

        [Authorize]
        [HttpGet("GetCollectorAssignedTasks")]
        public async System.Threading.Tasks.Task<IActionResult> GetCollectorAssignedTasks()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var receivableIdList = _assignedCollectorService.GetAssignedCollectors()
                .Where(x =>
                x.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE
                && x.UserId == user.Id)
                .Select(x => x.ReceivableId).ToList();

            var rawData = from action in _progressStageActionService.GetProgressStageActions()
                          where receivableIdList.Contains(action.ProgressStage.CollectionProgress.ReceivableId)
                          && action.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                          && (action.Type == Constant.ACTION_NOTIFICATION_CODE || action.Type == Constant.ACTION_REPORT_CODE)
                          select action;

            rawData = rawData.Where(action =>
            action.ExcutionDay <= Int32.Parse(Utility.ConvertDatetimeToString(DateTime.Now)));

            if (rawData.Any())
            {
                var result = rawData.Select(x => new TaskMobileVM()
                {
                    Id = x.Id,
                    ExecutionDay = Helper.Utility.ConvertIntToDatetime(x.ExcutionDay),
                    Name = x.Name,
                    StartTime = Helper.Utility.ConvertIntToTimeSpan(x.StartTime),
                    Evidence = _hostingEnvironment.EnvironmentName + x.Evidence,
                    Status = x.Status,
                    Type = x.Type,
                    ReceivableId = x.ProgressStage.CollectionProgress.ReceivableId
                });
                return Ok(result);
            }
            return Ok(new List<TaskMobileVM>());
        }

        [Authorize]
        [HttpGet("GetCollectorAssignedTasks/{receivableId}")]
        public async System.Threading.Tasks.Task<IActionResult> GetCollectorTasksByReceivable(int receivableId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var progressStageActions = _progressStageActionService.GetProgressStageActions
                (psa =>
                psa.ProgressStage.CollectionProgress.ReceivableId == receivableId
                &&
                (psa.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                || psa.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_LATE_CODE)
                && (psa.Type == Constant.ACTION_NOTIFICATION_CODE || psa.Type == Constant.ACTION_REPORT_CODE)
                );
            if (progressStageActions.Any())
            {
                var result = progressStageActions.Select(x => new TaskMobileVM()
                {
                    Id = x.Id,
                    ExecutionDay = Helper.Utility.ConvertIntToDatetime(x.ExcutionDay),
                    Name = x.Name,
                    StartTime = Helper.Utility.ConvertIntToTimeSpan(x.StartTime),
                    Evidence = string.IsNullOrEmpty(x.Evidence) ? "" : "/Task/" + x.Evidence,
                    UpdateDay = x.UpdatedDate,
                    Status = x.Status,
                    Type = x.Type,
                    UserId = x.UserId,
                    ReceivableId = x.ProgressStage.CollectionProgress.ReceivableId
                });
                return Ok(result);
            }
            return Ok(new List<TaskMobileVM>());
        }

        [Authorize]
        [HttpGet("GetAssignedTaskByReceivableAndDay/{day}/{receivableId}")]
        public async System.Threading.Tasks.Task<IActionResult> GetAssignedTaskByReceivableAndDay(int day,int receivableId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var progressStageActions = _progressStageActionService.GetProgressStageActions
                (psa =>
                psa.ProgressStage.CollectionProgress.ReceivableId == receivableId
                && psa.ProgressStage.CollectionProgress.Status != Constant.COLLECTION_STATUS_DONE_CODE
                && psa.Type != Constant.ACTION_PHONECALL_CODE 
                && psa.Type != Constant.ACTION_SMS_CODE
                && psa.ExcutionDay == day
                && psa.DoneAt==null
                );
            if (progressStageActions.Any())
            {
                var result = progressStageActions.Select(x => new TaskMobileVM()
                {
                    Id = x.Id,
                    ExecutionDay = Helper.Utility.ConvertIntToDatetime(x.ExcutionDay),
                    Name = x.Name,
                    StartTime = Helper.Utility.ConvertIntToTimeSpan(x.StartTime),
                    Evidence = string.IsNullOrEmpty(x.Evidence) ? "" : "/Task/" + x.Evidence,
                    UpdateDay = x.UpdatedDate,
                    Status = x.Status,
                    Type = x.Type,
                    UserId = x.UserId,
                    ReceivableId = x.ProgressStage.CollectionProgress.ReceivableId
                });
                return Ok(result);
            }
            return Ok(new List<TaskMobileVM>());
        }

        [Authorize]
        [HttpGet("GetTaskByReceivableId/{receivableId}")]
        public async System.Threading.Tasks.Task<IActionResult> GetDoneTaskByReceivableId(int receivableId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var progressStageActions = _progressStageActionService.GetProgressStageActions
                (psa =>
                psa.ProgressStage.CollectionProgress.ReceivableId == receivableId
                && psa.Type != Constant.ACTION_PHONECALL_CODE
                && psa.Type != Constant.ACTION_SMS_CODE);
            if (progressStageActions.Any())
            {
                var result = progressStageActions.Select(x => new TaskMobileVM()
                {
                    Id = x.Id,
                    ExecutionDay = Helper.Utility.ConvertIntToDatetime(x.ExcutionDay),
                    Name = x.Name,
                    StartTime = Helper.Utility.ConvertIntToTimeSpan(x.StartTime),
                    Evidence = string.IsNullOrEmpty(x.Evidence) ? "" : "/Task/" + x.Evidence,
                    UpdateDay = x.UpdatedDate,
                    Status = x.Status,
                    Note = x.Note,
                    Type = x.Type,
                    UserId = x.UserId,
                    ReceivableId = x.ProgressStage.CollectionProgress.ReceivableId
                });
                return Ok(result);
            }
            return Ok(new List<TaskMobileVM>());
        }

        [Authorize]
        [HttpGet("GetCollectorCalendarTasks")]
        public async System.Threading.Tasks.Task<IActionResult> GetCollectorCalendarTasks()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var receivableIdList = _assignedCollectorService.GetAssignedCollectors()
                .Where(x =>
                x.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE
                && x.UserId == user.Id)
                .Select(x => x.ReceivableId);

            var progressStageActions = _progressStageActionService.GetProgressStageActions
                (psa =>
                receivableIdList.Contains(psa.ProgressStage.CollectionProgress.ReceivableId)
                &&
                (psa.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                || psa.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_LATE_CODE)
                && (psa.Type == Constant.ACTION_NOTIFICATION_CODE || psa.Type == Constant.ACTION_REPORT_CODE)
                && psa.ExcutionDay <= Int32.Parse(Utility.ConvertDatetimeToString(DateTime.Now))
                && psa.DoneAt == null
                );
            if (progressStageActions.Any())
            {
                var result = new List<DateTime>();
                progressStageActions.ToList().ForEach(psa =>
                {
                    var day = Utility.ConvertIntToDatetime(psa.ExcutionDay);
                    if (result.Count() == 0)
                    {
                        result.Add(day);
                    }
                    else if (!result.Contains(day))
                    {
                        result.Add(day);
                    }
                });
                //Add day to next Sat
                var lastDay = result.First();
                var days = DayOfWeek.Saturday - result.First().DayOfWeek;
                for (int i = 1; i < days; i++)
                {
                    var futureDay = lastDay.AddDays(i);
                    if (!result.Contains(futureDay))
                        result.Add(futureDay);
                }
                return Ok(result);
            }
            return Ok(new List<DateTime>());
        }

        [Authorize]
        [HttpGet("GetAssignedTaskByDay/{day}")]
        public async System.Threading.Tasks.Task<IActionResult> GetAssignedTaskByDay(int day)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var receivableIdList = _assignedCollectorService.GetAssignedCollectors()
                .Where(x =>
                x.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE
                && x.UserId == user.Id
                &&x.IsDeleted==false
                )
                .Select(x => x.ReceivableId).ToList();

            var progressStageActions = _progressStageActionService.GetProgressStageActions
                (psa =>
                receivableIdList.Contains(psa.ProgressStage.CollectionProgress.ReceivableId)
                &&
                (psa.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                || psa.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_LATE_CODE)
                && (psa.Type == Constant.ACTION_NOTIFICATION_CODE || psa.Type == Constant.ACTION_REPORT_CODE)
                && psa.ExcutionDay == day
                && psa.DoneAt == null);
            if (progressStageActions.Any())
            {
                var result = progressStageActions.Select(x => new TaskMobileVM()
                {
                    Id = x.Id,
                    ExecutionDay = Helper.Utility.ConvertIntToDatetime(x.ExcutionDay).Subtract(new TimeSpan(12, 0, 0)),
                    Name = x.Name,
                    StartTime = Helper.Utility.ConvertIntToTimeSpan(x.StartTime),
                    Evidence = x.Evidence,
                    Status = x.Status,
                    Type = x.Type,
                    ReceivableId = x.ProgressStage.CollectionProgress.ReceivableId
                });
                return Ok(result);
            }
            return Ok(new List<TaskVM>());
        }
        #endregion

    }
}
