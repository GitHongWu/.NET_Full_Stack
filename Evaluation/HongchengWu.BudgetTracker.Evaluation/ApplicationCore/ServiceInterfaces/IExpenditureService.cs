using ApplicationCore.Models.Request;
using ApplicationCore.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IExpenditureService
    {
        Task<List<RecordDetailResponseModel>> GetAllExpenditures();
        Task<RecordDetailResponseModel> GetExpenditureById(int expenditureId);
        Task<List<RecordDetailResponseModel>> GetExpenditureByUserId(int userId);
        Task<RecordDetailResponseModel> AddExpenditure(RecordAddRequestModel model);
        Task<RecordDetailResponseModel> UpdateExpenditure(RecordAddRequestModel model, int id);
        Task<RecordDetailResponseModel> DeleteExpenditure(int id);
    }
}
