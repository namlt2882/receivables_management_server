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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IReceivableService _receivableService;

        public CustomerController(ICustomerService customerService, IReceivableService receivableService)
        {
            _customerService = customerService;
            _receivableService = receivableService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<CustomerVM> result = new List<CustomerVM>();
            var data = _customerService.GetCustomers(_ => _.IsDeleted == false);
            foreach (var item in data)
            {
                var tmp = item.Adapt<CustomerVM>();
                tmp.NumberOfReceivable = item.Receivables.Count();
                result.Add(tmp);
            }
            return Ok(result);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var customer = _customerService.GetCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer.Adapt<CustomerVM>());
        }

        [HttpGet("{id}/Receivables")]
        public IActionResult GetReceivables(int id)
        {
            var customer = _customerService.GetCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }
            List<ReceivableVM> result = new List<ReceivableVM>();
            foreach (var item in customer.Receivables)
            {
                result.Add(item.Adapt<ReceivableVM>());
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody]CustomerCM customer)
        {
            try
            {
                _customerService.CreateCustomer(customer.Adapt<Customer>());
                _customerService.SaveCustomer();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return StatusCode(201);
        }

        //[HttpPost("AddReceivables")]
        //public IActionResult AddReceivables([FromBody]ReceivablesCustomerCM data)
        //{
        //    try
        //    {
        //        foreach (var productId in data.ReceivableIds)
        //        {
        //            _receivableService.CreateReceivable(new Model.Receivable { CustomerId = data.CustomerId, ReceivableId = productId });
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
        public IActionResult Update([FromBody] CustomerUM customerUM)
        {
            try
            {
                var customer = _customerService.GetCustomer(customerUM.Id);
                if (customer == null) return NotFound();
                customer = customerUM.Adapt(customer);
                _customerService.EditCustomer(customer);
                _customerService.SaveCustomer();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return StatusCode(200);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customer = _customerService.GetCustomer(id);
            if (customer == null) return NotFound();
            _customerService.RemoveCustomer(customer);
            _customerService.SaveCustomer();
            return Ok();
        }
    }
}
