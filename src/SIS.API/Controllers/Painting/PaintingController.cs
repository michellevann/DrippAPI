﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedStarter.API.DataContract.Painting;
using RedStarter.Business.DataContract.Painting;

namespace RedStarter.API.Controllers.Painting
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class PaintingController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPaintingManager _manager;

        public PaintingController(IMapper mapper, IPaintingManager manager)
        {
            _mapper = mapper;
            _manager = manager;
        }

        [HttpPost]
        public async Task<IActionResult> PostPainting(PaintingCreateRequest request)
        {    
            if (!ModelState.IsValid)
            {
                return StatusCode(400);
            }

            var identityClaimNum = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var dto = _mapper.Map<PaintingCreateDTO>(request);
            dto.DateAdded = DateTime.Now;
            dto.OwnerId = identityClaimNum;

            if (await _manager.CreatePainting(dto))
                return StatusCode(201);

            throw new Exception();
        }

        [HttpGet]
        //[Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> GetPaintings()
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400);
            }

            var identityClaimNum = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var dto = await _manager.GetPaintings();
            var response = _mapper.Map<IEnumerable<PaintingResponse>>(dto);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaintingById(int id)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400);
            }

            var identityClaimNum = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var dto = await _manager.GetPaintingById(id);
            var response = _mapper.Map<PaintingGetByIdRequest>(dto);

            return Ok(response);
        }
    }
}