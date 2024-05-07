using AutoMapper;
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
        public async Task<IActionResult> UpdateClient(int id, CollectionPointPatchDto collectionPoint)
        {
            var existingPoint = await dbContext.CollectionPoint.FindAsync(id);

            _mapper.Map(collectionPoint, existingPoint);

            dbContext.Entry(existingPoint).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();

            return Ok(collectionPoint);
        }
    }
}