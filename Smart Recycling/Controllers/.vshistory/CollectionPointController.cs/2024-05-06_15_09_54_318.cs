﻿using AutoMapper;
using CORE.Models;
using DAL.Contexts;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartRecycling.Dto;
using System.Xml.XPath;

namespace SmartRecycling.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CollectionPointController : Controller
    {
        private readonly SmartRecyclingDbContext dbContext;
        private readonly IMapper _mapper;

        public CollectionPointController(SmartRecyclingDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCollectionPoint(CollectionPointDto collectionPoint)
        {

            var collectionPointMap = _mapper.Map<CollectionPoint>(collectionPoint);

            await dbContext.CollectionPoint.AddAsync(collectionPointMap);
            await dbContext.SaveChangesAsync();

            return Ok(collectionPoint);
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateClient(int id, CollectionPointPatchDto patchDocument)
        {
            /*            var collectionPoint = await dbContext.CollectionPoint.FindAsync(id);
                        if (collectionPoint == null)
                        {
                            return NotFound();
                        }

                        var collPointToPatch = _mapper.Map<CollectionPointPatchDto>(collectionPoint); // AutoMapper or manual mapping

                        patchDocument.ApplyTo(collPointToPatch, ModelState);

                        if (!TryValidateModel(collPointToPatch))
                        {
                            return BadRequest(ModelState);
                        }

                        _mapper.Map(collPointToPatch, collectionPoint); // Update the entity with patched properties

                        await dbContext.SaveChangesAsync();

                        return NoContent();*/
            var existingPoint = await dbContext.CollectionPoint.FindAsync(id);

            _mapper.Map(updatedOperation, existingPoint);

            dbContext.Entry(existingPoint).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            var collectionPointMap = _mapper.Map<CollectionPoint>(collectionPoint);

            await dbContext.CollectionPoint.AddAsync(collectionPointMap);
            await dbContext.SaveChangesAsync();

            return Ok(collectionPoint);
        }
    }
}