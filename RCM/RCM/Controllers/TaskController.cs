using Microsoft.AspNetCore.Mvc;
using RCM.Helper;
using RCM.Service;
using RCM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RCM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IProgressStageActionService _progressStageActionService;
        private readonly IReceivableService _receivableService;

        public TaskController(IProgressStageActionService progressStageActionService, IReceivableService receivableService)
        {
            _progressStageActionService = progressStageActionService;
            _receivableService = receivableService;
        }

        [HttpGet("Done")]
        public IActionResult MarkTaskIsDone(int id)
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

            action.Status = Constant.COLLECTION_STATUS_DONE_CODE;
            action.DoneAt = Int32.Parse(Utility.ConvertDatetimeToString(DateTime.Now));
            _progressStageActionService.EditProgressStageAction(action);
            _progressStageActionService.SaveProgressStageAction();

            return Ok();
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
            var rawData = _progressStageActionService.GetProgressStageActions()
                           .Where(action =>
                           action.Type == Constant.ACTION_NOTIFICATION_CODE
                           || action.Type == Constant.ACTION_REPORT_CODE);

            rawData = rawData.Where(action =>
            action.ProgressStage
               .CollectionProgress
               .Receivable
               .AssignedCollectors
                    .Select(assignedCollector =>
                     (assignedCollector.UserId == collectorId && assignedCollector.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE)
                    ).SingleOrDefault());

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
                    Type = x.Type
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
                    Type = x.Type
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

            var rawData = _progressStageActionService.GetProgressStageActions()
                .Where(action =>
                action.Type == Constant.ACTION_NOTIFICATION_CODE
                || action.Type == Constant.ACTION_REPORT_CODE);

            rawData = rawData.Where(action =>
            action.ProgressStage
               .CollectionProgress
               .Receivable
               .AssignedCollectors
                    .Select(assignedCollector =>
                     (assignedCollector.UserId == collectorId && assignedCollector.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE)
                    ).SingleOrDefault());

            rawData = rawData.Where(action =>
            action.ExcutionDay == Int32.Parse(Utility.ConvertDatetimeToString(DateTime.Now)));

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
                    Type = x.Type
                });
                return Ok(result);
            }

            return Ok(new List<TaskVM>());
        }
    }
}
