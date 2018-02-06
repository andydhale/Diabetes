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
    ///     
    /// </summary>
    [Route(Constants.ApiRoute)]
    public class InsulinController : Controller
    {
        private readonly DiabetesContext _db;
        private readonly IMapper _mapper;

        public InsulinController(DiabetesContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        /// <summary>
        ///     Get a list of all insulin injections
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await _db.Injections.ToListAsync();
            return Ok(results);
        }

        /// <summary>
        ///     Get an individual insulin injection
        /// </summary>
        /// <param name="id">The id of the injection</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var injection = await _db.Injections.FindAsync(id);
            if (injection == null)
            {
                return NotFound();
            }

            return Ok(injection);
        }

        /// <summary>
        ///     Get a list of insulin injections today
        /// </summary>
        /// <returns></returns>
        [HttpGet("today")]
        public async Task<IActionResult> GetToday()
        {
            var injections = await _db.Injections
                .Where(injection => injection.Time.Date == DateTimeOffset.UtcNow.Date)
                .ToListAsync();

            return Ok(injections);
        }

        /// <summary>
        ///     Create a new insulin injection
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateInjectionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var injection = _mapper.Map<InsulinInjection>(model);
            _db.Injections.Add(injection);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = injection.Id }, injection);
        }

        /// <summary>
        ///     Update an existing insulin injection
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(ModifyInjectionViewModel model)
        {
            var injection = await _db.Injections.FindAsync(model.Id);
            if (injection == null)
            {
                return NotFound();
            }

            injection.Type = model.Type;
            injection.Amount = model.Amount;
            injection.Time = model.Time;

            await _db.SaveChangesAsync();

            return Ok(new EntityUpdatedViewModel<InsulinInjection>
            {
                Data = injection,
                Type = Constants.Updated
            });
        }

        /// <summary>
        ///     Delete an injection
        /// </summary>
        /// <param name="id">The id of the injection to delete</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var injection = await _db.Injections.FindAsync(id);
            if (injection == null)
            {
                return NotFound();
            }

            _db.Injections.Remove(injection);
            await _db.SaveChangesAsync();

            return Ok(new EntityUpdatedViewModel<InsulinInjection>
            {
                Data = injection,
                Type = Constants.Deleted
            });
        }
    }
}
