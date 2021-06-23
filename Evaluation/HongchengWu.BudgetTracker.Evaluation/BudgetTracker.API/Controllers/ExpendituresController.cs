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
    public class ExpendituresController : ControllerBase
    {
        private readonly IExpenditureService _expenditureService;

        public ExpendituresController(IExpenditureService expenditureService)
        {
            _expenditureService = expenditureService;
        }

        // GET api/Expenditures
        [HttpGet]
        public async Task<IActionResult> GetAllExpenditures()
        {
            var expenditures = await _expenditureService.GetAllExpenditures();
            return Ok(expenditures);
        }

        // GET api/Expenditures/{id}
        [HttpGet]
        [Route("{expenditureId:int}")]
        public async Task<IActionResult> GetExpenditureById(int expenditureId)
        {
            var expenditure = await _expenditureService.GetExpenditureById(expenditureId);
            if (expenditure == null) return NotFound("Expenditure Id does not exist");
            return Ok(expenditure);
        }

        // GET api/Expenditures/User/{id}
        [HttpGet]
        [Route("User/{userId:int}")]
        public async Task<IActionResult> GetExpenditureByUserId(int userId)
        {
            var expenditures = await _expenditureService.GetExpenditureByUserId(userId);
            if (expenditures == null) return NotFound("User Id does not exist");
            return Ok(expenditures);
        }

        // POST api/Expenditures/Add
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddExpenditure([FromBody] RecordAddRequestModel model)
        {
            var response = await _expenditureService.AddExpenditure(model);
            return Ok(response);
        }

        // PUT api/Expenditures/Update/{id}
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateExpenditure([FromBody] RecordAddRequestModel model, int id)
        {
            var response = await _expenditureService.UpdateExpenditure(model, id);
            if (response == null) return NotFound("Expenditure Id does not exist");
            return Ok(response);
        }

        // DELETE api/Expenditures/Delete/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteExpenditure(int id)
        {
            var response = await _expenditureService.DeleteExpenditure(id);
            if (response == null) return NotFound("Expenditure Id does not exist");
            return Ok("Delete Successed!");
        }
    }
}
