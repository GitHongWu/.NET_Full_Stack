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
    public class ExpenditureService : IExpenditureService
    {
        private readonly IUserRepository _userRepository;
        private readonly IExpenditureRepository _expenditureRepository;

        public ExpenditureService(IUserRepository userRepository, IExpenditureRepository expenditureRepository)
        {
            _userRepository = userRepository;
            _expenditureRepository = expenditureRepository;
        }

        public async Task<List<RecordDetailResponseModel>> GetAllExpenditures()
        {
            var expenditures = await _expenditureRepository.ListAll();

            List<RecordDetailResponseModel> response = new List<RecordDetailResponseModel>();
            foreach (var i in expenditures)
            {
                response.Add(new RecordDetailResponseModel
                {
                    Id = i.Id,
                    UserId = i.UserId,
                    Amount = i.Amount,
                    Description = i.Description,
                    Date = i.ExpDate,
                    Remarks = i.Remarks,
                });
            }
            return response;
        }

        public async Task<RecordDetailResponseModel> GetExpenditureById(int expenditureId)
        {
            var expenditure = await _expenditureRepository.GetById(expenditureId);
            if (expenditure == null) return null;

            RecordDetailResponseModel response = new RecordDetailResponseModel
            {
                Id = expenditure.Id,
                UserId = expenditure.UserId,
                Amount = expenditure.Amount,
                Description = expenditure.Description,
                Date = expenditure.ExpDate,
                Remarks = expenditure.Remarks,
            };
            return response;
        }

        public async Task<List<RecordDetailResponseModel>> GetExpenditureByUserId(int userId)
        {
            var user = await _userRepository.GetById(userId);
            if (user == null) return null;

            var expenditures = await _expenditureRepository.GetExpendituresByUserId(userId);

            List<RecordDetailResponseModel> response = new List<RecordDetailResponseModel>();
            foreach (var expenditure in expenditures)
            {
                response.Add(new RecordDetailResponseModel
                {
                    Id = expenditure.Id,
                    UserId = expenditure.UserId,
                    Amount = expenditure.Amount,
                    Description = expenditure.Description,
                    Date = expenditure.ExpDate,
                    Remarks = expenditure.Remarks,
                });
            }
            return response;
        }

        public async Task<RecordDetailResponseModel> AddExpenditure(RecordAddRequestModel model)
        {
            var expenditure = new Expenditure
            {
                UserId = model.UserId,
                Amount = model.Amount,
                Description = model.Description,
                ExpDate = model.Date,
                Remarks = model.Remarks,
            };
            var addedExpenditure = await _expenditureRepository.Add(expenditure);

            var response = new RecordDetailResponseModel
            {
                Id = addedExpenditure.Id,
                UserId = addedExpenditure.UserId,
                Amount = addedExpenditure.Amount,
                Description = addedExpenditure.Description,
                Date = addedExpenditure.ExpDate,
                Remarks = addedExpenditure.Remarks,
            };
            return response;
        }

        public async Task<RecordDetailResponseModel> UpdateExpenditure(RecordAddRequestModel model, int id)
        {
            var record = await _expenditureRepository.GetById(id);
            if (record == null) return null;

            record.UserId = model.UserId;
            record.Amount = model.Amount;
            record.Description = model.Description;
            record.ExpDate = model.Date;
            record.Remarks = model.Remarks;

            var updatedIncome = await _expenditureRepository.Update(record);

            var response = new RecordDetailResponseModel
            {
                Id = updatedIncome.Id,
                UserId = updatedIncome.UserId,
                Amount = updatedIncome.Amount,
                Description = updatedIncome.Description,
                Date = updatedIncome.ExpDate,
                Remarks = updatedIncome.Remarks,
            };
            return response;
        }

        public async Task<RecordDetailResponseModel> DeleteExpenditure(int id)
        {
            var record = await _expenditureRepository.GetById(id);
            if (record == null) return null;

            var response = new RecordDetailResponseModel
            {
                UserId = record.UserId,
                Amount = record.Amount,
                Description = record.Description,
                Date = record.ExpDate,
                Remarks = record.Remarks,
            };

            await _expenditureRepository.Delete(record);
            return response;
        }
    }
}
