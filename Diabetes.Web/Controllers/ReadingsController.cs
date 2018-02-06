using AutoMapper;
using Diabetes.Web.Logic;
using Diabetes.Web.Models;
using Diabetes.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diabetes.Web.Controllers
{
    /// <summary>
    ///     Readings from an insulin meter
    /// </summary>
    [Route(Constants.ApiRoute)]
    public class ReadingsController : Controller
    {
        private readonly DiabetesContext _db;
        private readonly IMapper _mapper;

        public ReadingsController(DiabetesContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        /// <summary>
        ///     Get all the readings that have been taken
        /// </summary>
        /// <returns>A list of all the readings stored</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Reading>), 200)]
        public async Task<IActionResult> Get()
        {
            var results = await _db.Readings.ToListAsync();

            return Ok(results);
        }

        /// <summary>
        ///     Get an individual reading
        /// </summary>
        /// <param name="id">The id of the reading</param>
        /// <returns>The reading if found</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Reading), 201)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> Get(int id)
        {
            var reading = await _db.Readings.FindAsync(id);
            if (reading == null)
            {
                return NotFound();
            }

            return Ok(reading);
        }

        /// <summary>
        ///     Get all of the readings that have been added for today
        /// </summary>
        /// <returns>A list of the readings today</returns>
        [HttpGet("today")]
        [ProducesResponseType(typeof(IEnumerable<Reading>), 200)]
        public async Task<IActionResult> GetToday()
        {
            var results = await _db.Readings
                .Where(reading => reading.Time.Date == DateTimeOffset.UtcNow.Date)
                .ToListAsync();

            return Ok(results);
        }

        /// <summary>
        ///     Create a new reading
        /// </summary>
        /// <param name="model">The data to post</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Reading), 201)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> Create(CreateReadingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var reading = _mapper.Map<Reading>(model);
            _db.Readings.Add(reading);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = reading.Id }, reading);
        }

        /// <summary>
        ///     Update an existing reading
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(EntityUpdatedViewModel<Reading>), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> Update(ModifyReadingViewModel model)
        {
            var reading = await _db.Readings.FindAsync(model.Id);
            if (reading == null)
            {
                return NotFound();
            }

            // manual :(
            reading.Value = model.Value;
            reading.ReadingTime = model.ReadingTime;
            reading.Time = model.Time;

            await _db.SaveChangesAsync();

            return Ok(new EntityUpdatedViewModel<Reading>
            {
                Type = Constants.Updated,
                Data = reading
            });
        }

        /// <summary>
        ///     Delete a reading
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(EntityUpdatedViewModel<Reading>), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> Delete(int id)
        {
            var reading = await _db.Readings.FindAsync(id);
            if (reading == null)
            {
                return NotFound();
            }

            _db.Readings.Remove(reading);
            await _db.SaveChangesAsync();

            return Ok(new EntityUpdatedViewModel<Reading>
            {
                Type = Constants.Deleted,
                Data = reading
            });
        }
    }
}
