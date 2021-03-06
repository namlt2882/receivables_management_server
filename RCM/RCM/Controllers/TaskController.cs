﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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
        [HttpPost("UpdateTask")]
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
                bool exists = System.IO.Directory.Exists(uploads);
                if (!exists)
                    System.IO.Directory.CreateDirectory(uploads);
                var pictureName = $"{model.Id}-{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}{Path.GetExtension(model.File.FileName)}";
                using (var fileStream = new FileStream(Path.Combine(uploads, pictureName), FileMode.Create))
                {
                    await model.File.CopyToAsync(fileStream);
                    fileStream.Flush();
                    fileStream.Close();
                }
                action.Evidence = pictureName;
                action.Note = model.Note;
                action.Status = model.Status;
                action.DoneAt = Int32.Parse(Utility.ConvertDatetimeToString(DateTime.Now));
                action.UserId = user.Id;
                _progressStageActionService.EditProgressStageAction(action);
                _progressStageActionService.SaveProgressStageAction();
                return Ok(new TaskMobileVM()
                {
                    Id = action.Id,
                    ExecutionDay = Helper.Utility.ConvertIntToDatetime(action.ExcutionDay),
                    Name = action.Name,
                    StartTime = Helper.Utility.ConvertIntToTimeSpan(action.StartTime),
                    Evidence = string.IsNullOrEmpty(action.Evidence) ? "" : "/Task/" + action.Evidence,
                    UpdateDay = action.UpdatedDate,
                    Status = action.Status,
                    CollectorName = action.User.FirstName + " " + action.User.LastName,
                    Type = action.Type,
                    UserId = action.UserId,
                    ReceivableId = action.ProgressStage.CollectionProgress.ReceivableId,
                    Note = action.Note
                });
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut("Cancel/{id}")]
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
                          && (action.Type == Constant.ACTION_VISIT_CODE || action.Type == Constant.ACTION_REPORT_CODE)
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
            var rawData = _progressStageActionService.GetProgressStageActions(
                x => x.ProgressStage.CollectionProgress.ReceivableId == receivableId
            && x.ExcutionDay == Utility.ConvertDatimeToInt(DateTime.Now));
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
                    ReceivableId = x.ProgressStage.CollectionProgress.ReceivableId,
                    Evidence = x.Evidence,
                    Note = x.Note
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
                          && (action.Type == Constant.ACTION_VISIT_CODE || action.Type == Constant.ACTION_NOTIFICATION_CODE || action.Type == Constant.ACTION_REPORT_CODE)
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
                          && (action.Type == Constant.ACTION_VISIT_CODE || action.Type == Constant.ACTION_REPORT_CODE)
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
            var collector = _assignedCollectorService.GetAssignedCollector(
                ac => ac.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE && ac.ReceivableId == receivableId && ac.UserId == user.Id);
            if (collector == null) return Ok(new List<TaskMobileVM>());

            var progressStageActions = _progressStageActionService.GetProgressStageActions
                (psa =>
                (psa.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                || psa.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_LATE_CODE)
                && psa.ProgressStage.CollectionProgress.ReceivableId == receivableId
                &&
                (psa.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                || psa.Status == Constant.COLLECTION_STATUS_LATE_CODE)
                && (psa.Type == Constant.ACTION_VISIT_CODE || psa.Type == Constant.ACTION_NOTIFICATION_CODE || psa.Type == Constant.ACTION_REPORT_CODE)
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
                    ReceivableId = receivableId
                });
                return Ok(result);
            }
            return Ok(new List<TaskMobileVM>());
        }

        [Authorize]
        [HttpGet("GetAllCollectorLateTask")]
        public IActionResult GetAllCollectorLateTask()
        {
            IEnumerable<ProgressStageAction> progressStageActions;
            progressStageActions = _progressStageActionService.GetProgressStageActions
                (psa =>
                psa.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                && psa.Status == Constant.COLLECTION_STATUS_LATE_CODE
                && (psa.Type == Constant.ACTION_VISIT_CODE || psa.Type == Constant.ACTION_NOTIFICATION_CODE || psa.Type == Constant.ACTION_REPORT_CODE)
                && psa.DoneAt == null);
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

        /// <summary>
        ///  Manager get completed Task in day
        /// </summary>
        /// <param name="day"></param>
        /// <param name="receivableId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetCollectorCompletedTaskByAndDay/{day}")]
        public IActionResult GetCollectorCompletedTaskByReceivableAndDay(int day)
        {
            IEnumerable<ProgressStageAction> progressStageActions;
            if (day == 0)
                progressStageActions = _progressStageActionService.GetProgressStageActions
                    (psa =>
                    psa.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                    && psa.Status == Constant.COLLECTION_STATUS_DONE_CODE
                    && (psa.Type == Constant.ACTION_VISIT_CODE || psa.Type == Constant.ACTION_NOTIFICATION_CODE || psa.Type == Constant.ACTION_REPORT_CODE)
                    && psa.ExcutionDay <= Utility.ConvertDatimeToInt(DateTime.Now)
                    && psa.DoneAt != null);
            else
                progressStageActions = _progressStageActionService.GetProgressStageActions
                    (psa =>
                    psa.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                    && psa.Status == Constant.COLLECTION_STATUS_DONE_CODE
                    && (psa.Type == Constant.ACTION_VISIT_CODE || psa.Type == Constant.ACTION_NOTIFICATION_CODE || psa.Type == Constant.ACTION_REPORT_CODE)
                    && psa.ExcutionDay == day
                    && psa.DoneAt != null);
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
                    CollectorName = x.User.FirstName + " " + x.User.LastName,
                    Type = x.Type,
                    UserId = x.UserId,
                    ReceivableId = x.ProgressStage.CollectionProgress.ReceivableId,
                    Note = x.Note
                });
                return Ok(result);
            }
            return Ok(new List<TaskMobileVM>());
        }

        /// <summary>
        /// Task were assign in a day classify by receivable for collector
        /// </summary>
        /// <param name="day"></param>
        /// <param name="receivableId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetAssignedTaskByReceivableAndDay/{day}/{receivableId}")]
        public async System.Threading.Tasks.Task<IActionResult> GetAssignedTaskByReceivableAndDay(int day, int receivableId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var collector = _assignedCollectorService.GetAssignedCollector(
                ac => ac.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE && ac.ReceivableId == receivableId && ac.UserId == user.Id);
            if (collector == null) return Ok(new List<TaskMobileVM>());
            IEnumerable<ProgressStageAction> progressStageActions;
            if (day == 0)
                progressStageActions = _progressStageActionService.GetProgressStageActions
                    (psa =>
                    (psa.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                || psa.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_LATE_CODE)
                    && psa.ProgressStage.CollectionProgress.ReceivableId == receivableId
                    &&
                    (psa.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                    || psa.Status == Constant.COLLECTION_STATUS_LATE_CODE)
                    && (psa.Type == Constant.ACTION_VISIT_CODE || psa.Type == Constant.ACTION_NOTIFICATION_CODE || psa.Type == Constant.ACTION_REPORT_CODE)
                    && psa.ExcutionDay <= Utility.ConvertDatimeToInt(DateTime.Now)
                    && psa.DoneAt == null);
            else
                progressStageActions = _progressStageActionService.GetProgressStageActions
                    (psa =>
                    (psa.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                    || psa.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_LATE_CODE)
                    && psa.ProgressStage.CollectionProgress.ReceivableId == receivableId
                    &&
                    (psa.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                    || psa.Status == Constant.COLLECTION_STATUS_LATE_CODE)
                    && (psa.Type == Constant.ACTION_VISIT_CODE || psa.Type == Constant.ACTION_NOTIFICATION_CODE || psa.Type == Constant.ACTION_REPORT_CODE)
                    && psa.ExcutionDay == day
                    && psa.DoneAt == null);
            if (progressStageActions.Any())
            {
                var result = progressStageActions.OrderByDescending(psa=>psa.ExcutionDay).Select(x => new TaskMobileVM()
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


        /// <summary>
        /// Task Already finish
        /// </summary>
        /// <param name="receivableId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetCompletedTaskByReceivableId/{receivableId}")]
        public async System.Threading.Tasks.Task<IActionResult> GetCompletedTaskByReceivableId(int receivableId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var progressStageActions = _progressStageActionService.GetProgressStageActions
                (psa =>
                psa.ProgressStage.CollectionProgress.ReceivableId == receivableId
                && (psa.Status == Constant.COLLECTION_STATUS_DONE_CODE
                || psa.Status == Constant.COLLECTION_STATUS_CANCEL_CODE)
                && psa.Type != Constant.ACTION_PHONECALL_CODE
                && psa.Type != Constant.ACTION_SMS_CODE);
            if (progressStageActions.Any())
            {

                var result = new List<TaskMobileVM>();
                progressStageActions.ToList().ForEach(async x =>
                {
                    var collector = await _userManager.FindByIdAsync(x.UserId);
                    var vm = new TaskMobileVM()
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
                        CollectorName = collector != null ? collector.FirstName + " " + collector.LastName : "",
                        ReceivableId = receivableId
                    };
                    result.Add(vm);
                });
                return Ok(result);
            }
            return Ok(new List<TaskMobileVM>());
        }

        /// <summary>
        /// Task Already planning
        /// </summary>
        /// <param name="receivableId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetTodoTaskByReceivableId/{receivableId}")]
        public async System.Threading.Tasks.Task<IActionResult> GetTodoTaskByReceivableId(int receivableId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var progressStageActions = _progressStageActionService.GetProgressStageActions
                (psa =>
                (psa.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                || psa.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_LATE_CODE)
                && psa.ProgressStage.CollectionProgress.ReceivableId == receivableId
                && psa.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                && psa.ExcutionDay > Utility.ConvertDatimeToInt(DateTime.Now)
                && psa.Type != Constant.ACTION_PHONECALL_CODE
                && psa.Type != Constant.ACTION_SMS_CODE);
            if (progressStageActions.Any())
            {

                var result = new List<TaskMobileVM>();
                progressStageActions.Take(5).ToList().ForEach(async x =>
                {
                    var vm = new TaskMobileVM()
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
                        ReceivableId = receivableId,
                    };
                    result.Add(vm);
                });
                return Ok(result);
            }
            return Ok(new List<TaskMobileVM>());
        }

        /// <summary>
        /// Get Calendar for collector trom day which is tasks not finish yet to days of recent week 
        /// </summary>
        /// <returns></returns>
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
                (
                (psa.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                || psa.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_LATE_CODE)
                && psa.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                || psa.Status == Constant.COLLECTION_STATUS_LATE_CODE)
                && (psa.Type == Constant.ACTION_VISIT_CODE || psa.Type == Constant.ACTION_NOTIFICATION_CODE || psa.Type == Constant.ACTION_REPORT_CODE)
                && psa.ExcutionDay <= Int32.Parse(Utility.ConvertDatetimeToString(DateTime.Now.AddDays(DayOfWeek.Saturday - DateTime.Now.DayOfWeek)))
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
                return Ok(result);
            }
            return Ok(new List<DateTime>());
        }
        /// <summary>
        /// Task in a day from calendar
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetAssignedTaskByDay/{day}")]
        public async System.Threading.Tasks.Task<IActionResult> GetAssignedTaskByDay(int day)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var receivableIdList = _assignedCollectorService.GetAssignedCollectors()
                .Where(x =>
                x.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE
                && x.UserId == user.Id
                && x.IsDeleted == false
                )
                .Select(x => x.ReceivableId).ToList();
            IEnumerable<ProgressStageAction> progressStageActions;
            if (day == 0)
                progressStageActions = _progressStageActionService.GetProgressStageActions
                    (psa =>
                    (psa.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                || psa.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_LATE_CODE)
                    && receivableIdList.Contains(psa.ProgressStage.CollectionProgress.ReceivableId)
                    &&
                    (psa.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                    || psa.Status == Constant.COLLECTION_STATUS_LATE_CODE)
                    && (psa.Type == Constant.ACTION_VISIT_CODE || psa.Type == Constant.ACTION_NOTIFICATION_CODE || psa.Type == Constant.ACTION_REPORT_CODE)
                    && psa.ExcutionDay == Utility.ConvertDatimeToInt(DateTime.Now)
                    && psa.DoneAt == null);
            else
                progressStageActions = _progressStageActionService.GetProgressStageActions
                    (psa =>
                     (psa.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                || psa.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_LATE_CODE)
                    && receivableIdList.Contains(psa.ProgressStage.CollectionProgress.ReceivableId)
                    &&
                    (psa.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                    || psa.Status == Constant.COLLECTION_STATUS_LATE_CODE)
                    && (psa.Type == Constant.ACTION_VISIT_CODE || psa.Type == Constant.ACTION_NOTIFICATION_CODE || psa.Type == Constant.ACTION_REPORT_CODE)
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
                    ReceivableId = x.ProgressStage.CollectionProgress.ReceivableId,
                    Partner = x.ProgressStage.CollectionProgress.Receivable.Customer.Name,
                    Debtor = x.ProgressStage.CollectionProgress.Receivable.Contacts.FirstOrDefault().Name
                });
                return Ok(result);
            }
            return Ok(new List<TaskVM>());
        }
        #endregion

        #region SMS/PHONE CALL ACTION

        [HttpGet("GetAllFailAutoActions")]
        public IActionResult GetAllFailAutoActions()
        {
            var progressStageActions = _progressStageActionService.GetProgressStageActions
                (psa =>
                psa.Status == Constant.COLLECTION_STATUS_CANCEL_CODE
                && (psa.Type == Constant.ACTION_PHONECALL_CODE
                || psa.Type == Constant.ACTION_SMS_CODE));
            if (progressStageActions.Any())
            {
                var result = new List<TaskMobileVM>();
                progressStageActions.ToList().ForEach(x =>
                {
                    var vm = new TaskMobileVM()
                    {
                        Id = x.Id,
                        ExecutionDay = Helper.Utility.ConvertIntToDatetime(x.ExcutionDay).Subtract(new TimeSpan(12, 0, 0)),
                        Name = x.Name,
                        StartTime = Helper.Utility.ConvertIntToTimeSpan(x.StartTime),
                        Status = x.Status,
                        Note = x.Note,
                        Type = x.Type,
                        ReceivableId = x.ProgressStage.CollectionProgress.ReceivableId,
                        UpdateDay = x.UpdatedDate,
                    };
                    result.Add(vm);
                });
                return Ok(result);
            }
            return Ok(new List<TaskMobileVM>());
        }

        [Authorize]
        [HttpGet("GetSuccessAutoActions/{receivableId}")]
        public IActionResult GetSuccessAutoActions(int receivableId)
        {
            var progressStageActions = _progressStageActionService.GetProgressStageActions
                (psa =>
                psa.Status == Constant.COLLECTION_STATUS_DONE_CODE
                && psa.UpdatedDate<=DateTime.Now
                && psa.ProgressStage.CollectionProgress.ReceivableId == receivableId
                && (psa.Type == Constant.ACTION_PHONECALL_CODE
                || psa.Type == Constant.ACTION_SMS_CODE));

            if (progressStageActions.Any())
            {
                var result = new List<TaskMobileVM>();
                progressStageActions.ToList().ForEach(x =>
                {
                    var vm = new TaskMobileVM()
                    {
                        Id = x.Id,
                        ExecutionDay = Helper.Utility.ConvertIntToDatetime(x.ExcutionDay).Subtract(new TimeSpan(12, 0, 0)),
                        Name = x.Name,
                        StartTime = Helper.Utility.ConvertIntToTimeSpan(x.StartTime),
                        Status = x.Status,
                        Note = x.Note,
                        Type = x.Type,
                        ReceivableId = x.ProgressStage.CollectionProgress.ReceivableId,
                        UpdateDay = x.UpdatedDate,
                    };
                    result.Add(vm);
                });
                return Ok(result);
            }
            return Ok(new List<TaskMobileVM>());
        }

        [Authorize]
        [HttpGet("GetFailAutoActions/{receivableId}")]
        public IActionResult GetFailAutoActions(int receivableId)
        {
            var progressStageActions = _progressStageActionService.GetProgressStageActions
                (psa =>
                psa.Status == Constant.COLLECTION_STATUS_CANCEL_CODE
                && psa.UpdatedDate <= DateTime.Now
                && psa.ProgressStage.CollectionProgress.ReceivableId == receivableId
                && (psa.Type == Constant.ACTION_PHONECALL_CODE
                || psa.Type == Constant.ACTION_SMS_CODE));

            if (progressStageActions.Any())
            {
                var result = new List<TaskMobileVM>();
                progressStageActions.ToList().ForEach(x =>
               {
                   var vm = new TaskMobileVM()
                   {
                       Id = x.Id,
                       ExecutionDay = Helper.Utility.ConvertIntToDatetime(x.ExcutionDay).Subtract(new TimeSpan(12, 0, 0)),
                       Name = x.Name,
                       StartTime = Helper.Utility.ConvertIntToTimeSpan(x.StartTime),
                       Status = x.Status,
                       Note = x.Note,
                       Type = x.Type,
                       ReceivableId = x.ProgressStage.CollectionProgress.ReceivableId,
                       UpdateDay = x.UpdatedDate,
                   };
                   result.Add(vm);
               });
                return Ok(result);
            }
            return Ok(new List<TaskMobileVM>());
        }

        [Authorize]
        [HttpPut("MakeManualAction/{actionId}")]
        public async System.Threading.Tasks.Task<IActionResult> MakeManualAction(int actionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var progressStageAction = _progressStageActionService.GetProgressStageAction(actionId);
            var phoneNo = progressStageAction.ProgressStage.CollectionProgress
                .Receivable
                .Contacts.Where(x => x.Type == Constant.CONTACT_DEBTOR_CODE).FirstOrDefault().Phone;
            var messageContent = progressStageAction.ProgressMessageForm.Content;
            if (progressStageAction != null)
            {
                switch (progressStageAction.Type)
                {
                    #region phonecall
                    case Constant.ACTION_PHONECALL_CODE:

                        if (phoneNo != Constant.DEFAULT_PHONE_NUMBER)
                        {
                            ////Make phone call
                            var stringeeMsg = await Utility.MakePhoneCallAsync(phoneNo, messageContent);
                            JObject call = JObject.Parse(stringeeMsg);
                            progressStageAction.NData = call.SelectToken("call_id").ToString();
                            progressStageAction.Evidence = "";
                        }
                        else
                        {
                            _progressStageActionService.MarkAsDone(progressStageAction);
                        }
                        break;
                    #endregion

                    #region SMS
                    case Constant.ACTION_SMS_CODE:

                        if (phoneNo != Constant.DEFAULT_PHONE_NUMBER)
                        {
                            System.Diagnostics.Debug.WriteLine("Tin nhan duoc gui di");
                            ////Make phone call
                            string response = Utility.SendSMS(phoneNo, messageContent);
                            //string response = Utility.SendSMS(phoneNo, messageContent);
                            var result = SpeedSMS.SendSms.FromJson(response);
                            if (result.Status.ToLower() != "success")
                            {
                                //var error = "";
                                //switch (result.Code)
                                //{
                                //    case SmsErrorCode.ACCOUNT_LOCKED_CODE: error = SmsErrorCode.ACCOUNT_LOCKED; break;
                                //    case SmsErrorCode.ACCOUNT_NOT_ALLOW_CODE: error = SmsErrorCode.ACCOUNT_NOT_ALLOW; break;
                                //    case SmsErrorCode.ACCOUNT_NOT_ENOUGH_BALANCE_CODE: error = SmsErrorCode.ACCOUNT_NOT_ENOUGH_BALANCE; break;
                                //    case SmsErrorCode.CONTENT_NOT_SUPPORT_CODE: error = SmsErrorCode.CONTENT_NOT_SUPPORT; break;
                                //    case SmsErrorCode.CONTENT_TOO_LONG_CODE: error = SmsErrorCode.CONTENT_TOO_LONG_CODE; break;
                                //    case SmsErrorCode.INVALID_PHONE_CODE: error = SmsErrorCode.INVALID_PHONE; break;
                                //    case SmsErrorCode.IP_LOCKED_CODE: error = SmsErrorCode.IP_LOCKED; break;
                                //    case SmsErrorCode.PROVIDER_ERROR_CODE: error = SmsErrorCode.PROVIDER_ERROR; break;
                                //}
                                progressStageAction.Note = MakeFailNote(progressStageAction.Note);
                            }
                            else
                            {
                                _progressStageActionService.MarkAsDone(progressStageAction);
                            }

                        }
                        else
                        {
                            _progressStageActionService.MarkAsDone(progressStageAction);
                        }
                        break;
                        #endregion

                }
                progressStageAction.UserId = user.Id;
                _progressStageActionService.EditProgressStageAction(progressStageAction);
                _progressStageActionService.SaveProgressStageAction();
                return Ok(new TaskMobileVM()
                {
                    Id = progressStageAction.Id,
                    ExecutionDay = Helper.Utility.ConvertIntToDatetime(progressStageAction.ExcutionDay).Subtract(new TimeSpan(12, 0, 0)),
                    Name = progressStageAction.Name,
                    StartTime = Helper.Utility.ConvertIntToTimeSpan(progressStageAction.StartTime),
                    Status = progressStageAction.Status,
                    Note = progressStageAction.Note,
                    Type = progressStageAction.Type,
                    ReceivableId = progressStageAction.ProgressStage.CollectionProgress.ReceivableId,
                    UpdateDay = progressStageAction.UpdatedDate,
                });
            }
            return BadRequest();
        }

        private string MakeFailNote(string note)
        {
            if (string.IsNullOrEmpty(note)) return "1";
            var result = int.Parse(note);
            result = result + 1;
            return result.ToString();
        }
        #endregion
    }
}
