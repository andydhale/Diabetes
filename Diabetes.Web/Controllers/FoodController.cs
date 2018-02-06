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
    [Route(Constants.ApiRoute)]
    public class FoodController : Controller
    {
        private readonly DiabetesContext _db;
        private readonly IMapper _mapper;

        public FoodController(DiabetesContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        /// <summary>
        ///     Get a list of all the food entries added
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await _db.Foods.ToListAsync();
            return Ok(results);
        }

        /// <summary>
        ///     Get an individual food entry
        /// </summary>
        /// <param name="id">The id of the entry</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var injection = await _db.Foods.FindAsync(id);
            if (injection == null)
            {
                return NotFound();
            }

            return Ok(injection);
        }

        /// <summary>
        ///     Get a list of all the food entries today
        /// </summary>
        /// <returns></returns>
        [HttpGet("today")]
        public async Task<IActionResult> GetToday()
        {
            var injections = await _db.Foods
                .Where(food => food.Time.Date == DateTimeOffset.UtcNow.Date)
                .ToListAsync();

            return Ok(injections);
        }

        /// <summary>
        ///     Create a new food entry
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateFoodViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var food = _mapper.Map<Food>(model);
            _db.Foods.Add(food);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = food.Id }, food);
        }

        /// <summary>
        ///     Update an existing food entry
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(ModifyFoodViewModel model)
        {
            var food = await _db.Foods.FindAsync(model.Id);
            if (food == null)
            {
                return NotFound();
            }

            food.Description = model.Description;
            food.Time = model.Time;

            await _db.SaveChangesAsync();

            return Ok(new EntityUpdatedViewModel<Food>
            {
                Data = food,
                Type = Constants.Updated
            });
        }

        /// <summary>
        ///     Delete a food entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var food = await _db.Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }

            _db.Foods.Remove(food);
            await _db.SaveChangesAsync();

            return Ok(new EntityUpdatedViewModel<Food>
            {
                Data = food,
                Type = Constants.Deleted
            });
        }
    }
}
