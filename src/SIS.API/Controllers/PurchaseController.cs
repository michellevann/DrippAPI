using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedStarter.API.DataContract.Purchase;
using RedStarter.Business.DataContract.Purchase;
using RedStarter.Business.DataContract.Purchase.DTOs;
using RedStarter.Database.DataContract.Purchase;

namespace RedStarter.API.Controllers.Purchase
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPurchaseManager _manager;

        public PurchaseController(IMapper mapper, IPurchaseManager manager)
        {
            _mapper = mapper;
            _manager = manager;
        }

        [HttpPost]
        [Route("CreateCharge")]
        public async Task<IActionResult> CreateCharge([FromBody]PurchaseCreateChargeRequest request)
        {
            var dtoToken = _mapper.Map<PurchaseCreateChargeDTO>(request);
            dtoToken.PurchaseDate = DateTime.Now;
            var success = await _manager.CreateCharge(dtoToken);
            var success2 = await _manager.CreatePurchase(dtoToken);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetPurchases()
        {
            if (!ModelState.IsValid)
                return StatusCode(400);

            var dto = await _manager.GetPurchases();
            var response = _mapper.Map<IEnumerable<PurchaseListItemResponse>>(dto);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPurchaseById(int id)
        {
            if (!ModelState.IsValid)
                return StatusCode(400);

            var dto = await _manager.GetPurchaseById(id);
            var response = _mapper.Map<PurchaseListItemResponse>(dto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaseById(int id)
        {
            if (!ModelState.IsValid)
                return StatusCode(400);

            if (await _manager.DeletePurchaseById(id))
                return StatusCode(202);
            else
                return StatusCode(500);
        }
    }
}

