﻿using AutoMapper;
using BLL.Services;
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
        private readonly PointStateService pointService;
        private StatisticsService statisticsService;

        public CollectionPointController(SmartRecyclingDbContext dbContext, IMapper mapper, PointStateService pointService, StatisticsService statisticsService)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
            this.pointService = pointService;
            this.statisticsService = statisticsService;
        }

        [HttpGet]
        public IEnumerable<CollectionPoint> GetCollectionPoints()
        {
            return dbContext.CollectionPoint.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CollectionPoint>> GetCollectionPoint()
        {
            var point = await dbContext.CollectionPoint
                .Include(p => p.CollectionPointComposition)
                .Include(p => p.Transportations)
                .FirstOrDefaultAsync();
            return Ok(point);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCollectionPoint(CollectionPointDto collectionPoint)
        {

            var collectionPointMap = _mapper.Map<CollectionPoint>(collectionPoint);

            await dbContext.CollectionPoint.AddAsync(collectionPointMap);
            await dbContext.SaveChangesAsync();

            await pointService.CreatePointComposition(collectionPointMap.Id); 
            await dbContext.SaveChangesAsync();

            return Ok(collectionPoint);
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateCollectionPointPartially(int id, CollectionPointPatchDto collectionPoint)
        {
            var existingPoint = await dbContext.CollectionPoint.FindAsync(id);

            _mapper.Map(collectionPoint, existingPoint);

            dbContext.Entry(existingPoint).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();

            return Ok(collectionPoint);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReport(int pointID, DateOnly startDate, DateOnly endDate)
        {
            var statistics = statisticsService.CreatePointStatistics(pointID, startDate, endDate);
            await dbContext.CollectionPointStatistics.AddAsync(statistics);
            await dbContext.SaveChangesAsync();

            return (IActionResult)statisticsService.GetFile(statistics);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReport(int id)
        {
            var statistics = dbContext.CollectionPointStatistics.FirstOrDefault(p => p.Id == id);
            if (statistics == null)
            {
                return NotFound(); // Return 404 if statistics with the given id is not found
            }

            return Ok(statisticsService.GetFile(statistics));
        }
    }
}