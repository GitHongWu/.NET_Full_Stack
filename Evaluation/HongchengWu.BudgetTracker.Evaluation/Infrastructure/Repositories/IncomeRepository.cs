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
    public class IncomeRepository : EfRepository<Income>, IIncomeRepository
    {
        public IncomeRepository(BudgetTrackerDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Income>> GetIncomesByUserId(int userId)
        {
            return await _dbContext.Incomes.Where(i => i.UserId == userId).Take(25).ToListAsync();
        }
    }
}
