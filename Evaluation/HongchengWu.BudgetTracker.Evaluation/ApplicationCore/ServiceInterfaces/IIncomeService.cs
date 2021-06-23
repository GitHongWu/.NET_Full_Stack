using ApplicationCore.Models.Request;
using ApplicationCore.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IIncomeService
    {
        Task<List<RecordDetailResponseModel>> GetAllIncomes();
        Task<RecordDetailResponseModel> GetIncomeById(int incomeId);
        Task<List<RecordDetailResponseModel>> GetIncomeByUserId(int userId);
        Task<RecordDetailResponseModel> AddIncome(RecordAddRequestModel model);
        Task<RecordDetailResponseModel> UpdateIncome(RecordAddRequestModel model, int id);
        Task<RecordDetailResponseModel> DeleteIncome(int id);
    }
}
