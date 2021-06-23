using ApplicationCore.Models.Request;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomesController : ControllerBase
    {
        private readonly IIncomeService _incomeService;

        public IncomesController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        // GET api/Incomes
        [HttpGet]
        public async Task<IActionResult> GetAllIncomes()
        {
            var incomes = await _incomeService.GetAllIncomes();
            return Ok(incomes);
        }

        // GET api/Incomes/{id}
        [HttpGet]
        [Route("{incomeId:int}")]
        public async Task<IActionResult> GetIncomeById(int incomeId)
        {
            var income = await _incomeService.GetIncomeById(incomeId);
            if (income == null) return NotFound("Income Id does not exist");
            return Ok(income);
        }

        // GET api/Incomes/User/{id}
        [HttpGet]
        [Route("User/{userId:int}")]
        public async Task<IActionResult> GetIncomeByUserId(int userId)
        {
            var incomes = await _incomeService.GetIncomeByUserId(userId);
            if (incomes == null) return NotFound("User Id does not exist");
            return Ok(incomes);
        }

        // POST api/Incomes/Add
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddIncome([FromBody] RecordAddRequestModel model)
        {
            var response = await _incomeService.AddIncome(model);
            return Ok(response);
        }

        // PUT api/Incomes/Update/{id}
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateIncome([FromBody] RecordAddRequestModel model, int id)
        {
            var response = await _incomeService.UpdateIncome(model, id);
            if (response == null) return NotFound("Income Id does not exist");
            return Ok(response);
        }

        // DELETE api/Incomes/Delete/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteIncome(int id)
        {
            var response = await _incomeService.DeleteIncome(id);
            if (response == null) return NotFound("Income Id does not exist");
            return Ok("Delete Successed!");
        }
    }
}
