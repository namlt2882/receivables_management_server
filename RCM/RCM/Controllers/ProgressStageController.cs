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
    public class ProgressStageController : ControllerBase
    {
        private readonly IProgressStageService _progressStageService;

        public ProgressStageController(IProgressStageService progressStageService)
        {
            _progressStageService = progressStageService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<ProgressStageVM> result = new List<ProgressStageVM>();
            var data = _progressStageService.GetProgressStages(_ => _.IsDeleted == false);
            foreach (var item in data)
            {
                result.Add(item.Adapt<ProgressStageVM>());
            }
            return Ok(result);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var ProgressStage = _progressStageService.GetProgressStage(id);
            if (ProgressStage == null)
            {
                return NotFound();
            }
            return Ok(ProgressStage.Adapt<ProgressStageVM>());
        }

        [HttpPost]
        public IActionResult Create([FromBody]ProgressStageCM progressStage)
        {
            try
            {
                _progressStageService.CreateProgressStage(progressStage.Adapt<ProgressStage>());
                _progressStageService.SaveProgressStage();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return StatusCode(201);
        }

        [HttpPut]
        public IActionResult Update([FromBody] ProgressStageUM progressStageUM)
        {
            try
            {
                var progressStage = _progressStageService.GetProgressStage(progressStageUM.Id);
                if (progressStage == null) return NotFound();
                progressStage = progressStageUM.Adapt(progressStage);
                _progressStageService.EditProgressStage(progressStage);
                _progressStageService.SaveProgressStage();
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
        //    var ProgressStage = _ProgressStageService.GetProgressStage(id);
        //    if (ProgressStage == null) return NotFound();
        //    _ProgressStageService.RemoveProgressStage(ProgressStage);
        //    _ProgressStageService.SaveProgressStage();
        //    return Ok();
        //}
    }
}
