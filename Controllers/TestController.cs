using System.Collections;
using System;
using Microsoft.AspNetCore.Mvc;
using WebServices.NetCore.Criostasis.AplicationServices.Produce;
using WebServices.NetCore.Criostasis.AplicationServices.Core.Requests;
using System.Collections.Generic;
using WebServices.NetCore.Criostasis.AplicationServices.Core.DTOs;

namespace WebServices.NetCore.Criostasis.Controllers
{
    [ApiController]
    [Route("api/v1/test1")]
    public class TestController : ControllerBase
    {
        private readonly IProduceAppService _produceAppService;
        public TestController(IProduceAppService produceAppService)
        {
            if (produceAppService == null) throw new ArgumentException(nameof(produceAppService));

            _produceAppService = produceAppService;

        }

        [HttpGet]
        [Route("")]
        public IActionResult GetProducts()
        {
            List<ProduceDto> response = _produceAppService.GetProducts(new ProduceRequest());

            return Ok(Response);
        }

        [HttpPost]
        [Route("")]
        public IActionResult SaveProduct()
        {
            ProduceDto response = _produceAppService.SaveProduce(new ProduceRequest());

            return Ok(Response);
        }


        [HttpDelete]
        [Route("")]
        public IActionResult DeleProduct()
        {
            var response = _produceAppService.GetProducts(new ProduceRequest());

            return Ok(response);
        }

    }
}