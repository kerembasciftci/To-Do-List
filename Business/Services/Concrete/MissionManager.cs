using AutoMapper;
using Business.Services.Abstract;
using Core.DataAccess.Repositories;
using Core.Log;
using Core.Services;
using Core.UnitOfWorks;
using DataAccess.Repositories.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete
{
    public class MissionManager : Service<Mission>, IMissionService
    {
        private readonly IMissionDal _missionDal;
        private readonly IMapper _mapper;
        public MissionManager(IGenericRepository<Mission> repository, ILoggerService loggerService, 
            IUnitOfWork unitOfWork, IMapper mapper, IMissionDal missionDal) : base(repository, unitOfWork, loggerService)
        {
            _missionDal = missionDal;
            _mapper = mapper;
        }
    }
}
