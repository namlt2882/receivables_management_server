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
    public class CollectionProgressController : ControllerBase
    {
        private readonly ICollectionProgressService _collectionProgressService;

        public CollectionProgressController(ICollectionProgressService collectionProgressService)
        {
            _collectionProgressService = collectionProgressService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<CollectionProgressVM> result = new List<CollectionProgressVM>();
            var data = _collectionProgressService.GetCollectionProgresss(_ => _.IsDeleted == false);
            foreach (var item in data)
            {
                result.Add(item.Adapt<CollectionProgressVM>());
            }
            return Ok(result);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var collectionProgress = _collectionProgressService.GetCollectionProgress(id);
            if (collectionProgress == null)
            {
                return NotFound();
            }
            return Ok(collectionProgress.Adapt<CollectionProgressVM>());
        }

        [HttpPost]
        public IActionResult Create([FromBody]CollectionProgressCM collectionProgress)
        {
            try
            {
                _collectionProgressService.CreateCollectionProgress(collectionProgress.Adapt<CollectionProgress>());
                _collectionProgressService.SaveCollectionProgress();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return StatusCode(201);
        }

        [HttpPut]
        public IActionResult Update([FromBody] CollectionProgressUM collectionProgressUM)
        {
            try
            {
                var collectionProgress = _collectionProgressService.GetCollectionProgress(collectionProgressUM.Id);
                if (collectionProgress == null) return NotFound();
                collectionProgress = collectionProgressUM.Adapt(collectionProgress);
                _collectionProgressService.EditCollectionProgress(collectionProgress);
                _collectionProgressService.SaveCollectionProgress();
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
        //    var collectionProgress = _collectionProgressService.GetCollectionProgress(id);
        //    if (collectionProgress == null) return NotFound();
        //    _collectionProgressService.RemoveCollectionProgress(collectionProgress);
        //    _collectionProgressService.SaveCollectionProgress();
        //    return Ok();
        //}
    }
}
