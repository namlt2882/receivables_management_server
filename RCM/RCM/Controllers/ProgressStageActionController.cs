using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RCM.Model;
using RCM.Service;
using RCM.ViewModels;

namespace RCM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressStageActionController : ControllerBase
    {
        private readonly IProgressStageActionService _progressStageActionService;

        public ProgressStageActionController(IProgressStageActionService ProgressStageActionService)
        {
            _progressStageActionService = ProgressStageActionService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<ProgressStageActionVM> result = new List<ProgressStageActionVM>();
            var data = _progressStageActionService.GetProgressStageActions(_ => _.IsDeleted == false);
            foreach (var item in data)
            {
                result.Add(item.Adapt<ProgressStageActionVM>());
            }
            return Ok(result);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var ProgressStageAction = _progressStageActionService.GetProgressStageAction(id);
            if (ProgressStageAction == null)
            {
                return NotFound();
            }
            return Ok(ProgressStageAction.Adapt<ProgressStageActionVM>());
        }

        [HttpPost]
        public IActionResult Create([FromBody]ProgressStageActionCM progressStageAction)
        {
            try
            {
                _progressStageActionService.CreateProgressStageAction(progressStageAction.Adapt<ProgressStageAction>());
                _progressStageActionService.SaveProgressStageAction();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return StatusCode(201);
        }

        [HttpPut]
        public IActionResult Update([FromBody] ProgressStageActionUM progressStageActionUM)
        {
            try
            {
                var progressStageAction = _progressStageActionService.GetProgressStageAction(progressStageActionUM.Id);
                if (progressStageAction == null) return NotFound();
                progressStageAction = progressStageActionUM.Adapt(progressStageAction);
                _progressStageActionService.EditProgressStageAction(progressStageAction);
                _progressStageActionService.SaveProgressStageAction();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return StatusCode(200);
        }

        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var ProgressStageAction = _progressStageActionService.GetProgressStageAction(id);
        //    if (ProgressStageAction == null) return NotFound();
        //    _progressStageActionService.RemoveProgressStageAction(ProgressStageAction);
        //    _progressStageActionService.SaveProgressStageAction();
        //    return Ok();
        //}
    }
}
