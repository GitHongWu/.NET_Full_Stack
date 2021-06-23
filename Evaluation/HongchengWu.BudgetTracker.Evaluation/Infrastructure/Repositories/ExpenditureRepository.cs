using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ExpenditureRepository : EfRepository<Expenditure>, IExpenditureRepository
    {
        public ExpenditureRepository(BudgetTrackerDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<Expenditure>> GetExpendituresByUserId(int userId)
        {
            return await _dbContext.Expenditures.Where(e => e.UserId == userId).Take(25).ToListAsync();
        }

    }
}
