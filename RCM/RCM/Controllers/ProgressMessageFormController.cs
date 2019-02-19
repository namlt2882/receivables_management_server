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
    public class ProgressMessageFormController : ControllerBase
    {
        private readonly IProgressMessageFormService _progressMessageFormService;

        public ProgressMessageFormController(IProgressMessageFormService progressMessageFormService)
        {
            _progressMessageFormService = progressMessageFormService;
        }

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    List<ProgressMessageFormVM> result = new List<ProgressMessageFormVM>();
        //    var data = _pProgressMessageFormService.GetProgressMessageForms(_ => _.IsDeleted == false);
        //    foreach (var item in data)
        //    {
        //        result.Add(item.Adapt<ProgressMessageFormVM>());
        //    }
        //    return Ok(result);
        //}


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var ProgressMessageForm = _progressMessageFormService.GetProgressMessageForm(id);
            if (ProgressMessageForm == null)
            {
                return NotFound();
            }
            return Ok(ProgressMessageForm.Adapt<ProgressMessageFormVM>());
        }

        [HttpPost]
        public IActionResult Create([FromBody]ProgressMessageFormCM progressMessageForm)
        {
            try
            {
                _progressMessageFormService.CreateProgressMessageForm(progressMessageForm.Adapt<ProgressMessageForm>());
                _progressMessageFormService.SaveProgressMessageForm();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return StatusCode(201);
        }

        [HttpPut]
        public IActionResult Update([FromBody] ProgressMessageFormUM progressMessageFormUM)
        {
            try
            {
                var progressMessageForm = _progressMessageFormService.GetProgressMessageForm(progressMessageFormUM.Id);
                if (progressMessageForm == null) return NotFound();
                progressMessageForm = progressMessageFormUM.Adapt(progressMessageForm);
                _progressMessageFormService.EditProgressMessageForm(progressMessageForm);
                _progressMessageFormService.SaveProgressMessageForm();
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
        //    var ProgressMessageForm = _pProgressMessageFormService.GetProgressMessageForm(id);
        //    if (ProgressMessageForm == null) return NotFound();
        //    _pProgressMessageFormService.RemoveProgressMessageForm(ProgressMessageForm);
        //    _pProgressMessageFormService.SaveProgressMessageForm();
        //    return Ok();
        //}
    }
}
