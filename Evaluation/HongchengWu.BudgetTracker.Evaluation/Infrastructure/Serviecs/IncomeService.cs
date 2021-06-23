using ApplicationCore.Entities;
using ApplicationCore.Models.Request;
using ApplicationCore.Models.Response;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Serviecs
{
    public class IncomeService : IIncomeService
    {
        private readonly IUserRepository _userRepository;
        private readonly IIncomeRepository _incomeRepository;

        public IncomeService(IUserRepository userRepository, IIncomeRepository incomeRepository)
        {
            _userRepository = userRepository;
            _incomeRepository = incomeRepository;
        }

        public async Task<List<RecordDetailResponseModel>> GetAllIncomes()
        {
            var incomes = await _incomeRepository.ListAll();

            List<RecordDetailResponseModel> response = new List<RecordDetailResponseModel>();
            foreach (var i in incomes)
            {
                response.Add(new RecordDetailResponseModel
                {
                    Id = i.Id,
                    UserId = i.UserId,
                    Amount = i.Amount,
                    Description = i.Description,
                    Date = i.IncomeDate,
                    Remarks = i.Remarks,
                });
            }
            return response;
        }

        public async Task<RecordDetailResponseModel> GetIncomeById(int incomeId)
        {
            var income = await _incomeRepository.GetById(incomeId);
            if (income == null) return null;

            RecordDetailResponseModel response = new RecordDetailResponseModel
            {
                Id = income.Id,
                UserId = income.UserId,
                Amount = income.Amount,
                Description = income.Description,
                Date = income.IncomeDate,
                Remarks = income.Remarks,
            };
            return response;
        }

        public async Task<List<RecordDetailResponseModel>> GetIncomeByUserId(int userId)
        {
            var user = await _userRepository.GetById(userId);
            if (user == null) return null;

            var incomes = await _incomeRepository.GetIncomesByUserId(userId);
            
            List<RecordDetailResponseModel> response = new List<RecordDetailResponseModel>();
            foreach (var income in incomes)
            {
                response.Add(new RecordDetailResponseModel {
                    Id = income.Id,
                    UserId = income.UserId,
                    Amount = income.Amount,
                    Description = income.Description,
                    Date = income.IncomeDate,
                    Remarks = income.Remarks,
                });
            }
            return response;
        }

        public async Task<RecordDetailResponseModel> AddIncome(RecordAddRequestModel model)
        {
            var income = new Income
            {
                UserId = model.UserId,
                Amount = model.Amount,
                Description = model.Description,
                IncomeDate = model.Date,
                Remarks = model.Remarks,
            };
            var addedIncome = await _incomeRepository.Add(income);

            var response = new RecordDetailResponseModel
            {
                Id = addedIncome.Id,
                UserId = addedIncome.UserId,
                Amount = addedIncome.Amount,
                Description = addedIncome.Description,
                Date = addedIncome.IncomeDate,
                Remarks = addedIncome.Remarks,
            };
            return response;
        }

        public async Task<RecordDetailResponseModel> UpdateIncome(RecordAddRequestModel model, int id)
        {
            var record = await _incomeRepository.GetById(id);
            if (record == null) return null;

            record.UserId = model.UserId;
            record.Amount = model.Amount;
            record.Description = model.Description;
            record.IncomeDate = model.Date;
            record.Remarks = model.Remarks;

            var updatedIncome = await _incomeRepository.Update(record);

            var response = new RecordDetailResponseModel
            {
                Id = updatedIncome.Id,
                UserId = updatedIncome.UserId,
                Amount = updatedIncome.Amount,
                Description = updatedIncome.Description,
                Date = updatedIncome.IncomeDate,
                Remarks = updatedIncome.Remarks,
            };
            return response;
        }

        public async Task<RecordDetailResponseModel> DeleteIncome(int id)
        {
            var record = await _incomeRepository.GetById(id);
            if (record == null) return null;

            var response = new RecordDetailResponseModel
            {
                UserId = record.UserId,
                Amount = record.Amount,
                Description = record.Description,
                Date = record.IncomeDate,
                Remarks = record.Remarks,
            };

            await _incomeRepository.Delete(record);
            return response;
        }
    }
}
