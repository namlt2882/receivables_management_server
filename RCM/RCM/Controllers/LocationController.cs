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
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly IReceivableService _receivableService;

        public LocationController(ILocationService locationService, IReceivableService receivableService)
        {
            _locationService = locationService;
            _receivableService = receivableService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<LocationVM> result = new List<LocationVM>();
            var data = _locationService.GetLocations(_ => _.IsDeleted == false);
            foreach (var item in data)
            {
                result.Add(item.Adapt<LocationVM>());
            }
            return Ok(result);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var location = _locationService.GetLocation(id);
            if (location == null)
            {
                return NotFound();
            }
            return Ok(location.Adapt<LocationVM>());
        }

        [HttpGet("{id}/Receivables")]
        public IActionResult GetReceivables(int id)
        {
            var location = _locationService.GetLocation(id);
            if (location == null)
            {
                return NotFound();
            }
            List<ReceivableVM> result = new List<ReceivableVM>();
            foreach (var item in location.Receivables)
            {
                result.Add(item.Adapt<ReceivableVM>());
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody]LocationCM location)
        {
            try
            {
                _locationService.CreateLocation(location.Adapt<Location>());
                _locationService.SaveLocation();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return StatusCode(201);
        }

        //[HttpPost("AddReceivables")]
        //public IActionResult AddReceivables([FromBody]ReceivablesLocationCM data)
        //{
        //    try
        //    {
        //        foreach (var productId in data.ReceivableIds)
        //        {
        //            _receivableService.CreateReceivable(new Model.Receivable { LocationId = data.LocationId, ReceivableId = productId });
        //        }
        //        _receivableService.SaveChange();
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //    return StatusCode(201);
        //}

        [HttpPut]
        public IActionResult Update([FromBody] LocationUM locationUM)
        {
            try
            {
                var location = _locationService.GetLocation(locationUM.Id);
                if (location == null) return NotFound();
                location = locationUM.Adapt(location);
                _locationService.EditLocation(location);
                _locationService.SaveLocation();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var location = _locationService.GetLocation(id);
            if (location == null) return NotFound();
            _locationService.RemoveLocation(location);
            _locationService.SaveLocation();
            return Ok();
        }
    }
}
