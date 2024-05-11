﻿using AutoMapper;
using CORE.Models;
using DAL.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartRecycling.Dto;

namespace SmartRecycling.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TransportationController : BaseController
    {
        public TransportationController(SmartRecyclingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransportation(TransportationDto transportation)
        {
            var transportationMap = _mapper.Map<Transportation>(transportation);

            await dbContext.Transportation.AddAsync(transportationMap);
            await dbContext.SaveChangesAsync();

            return Ok(transportation);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTransportation(int id, TransportationDto updatedTransportationDto)
        {
            if (updatedTransportationDto == null || id != updatedTransportationDto.Id)
                return BadRequest(ModelState);

            var existingTransportation = await dbContext.Transportation.FindAsync(id);

            if (existingTransportation == null)
                return NotFound();

            _mapper.Map(updatedTransportationDto, existingTransportation);

            await dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
