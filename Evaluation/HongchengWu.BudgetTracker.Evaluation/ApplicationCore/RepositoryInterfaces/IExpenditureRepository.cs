using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IExpenditureRepository : IAsyncRepository<Expenditure>
    {
        Task<IEnumerable<Expenditure>> GetExpendituresByUserId(int userId);
    }
}
