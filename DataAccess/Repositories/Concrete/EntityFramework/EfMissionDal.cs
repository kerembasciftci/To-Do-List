using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete.EntityFramework
{
    public class EfMissionDal : EfGenericRepository<Mission>, IMissionDal
    {
        public EfMissionDal(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
