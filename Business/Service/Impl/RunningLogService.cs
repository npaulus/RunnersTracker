using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RunnersTracker.DataAccess;
using RunnersTracker.Business.DTO;
using RunnersTrackerDB;
using AutoMapper;
using log4net;
using RunnersTracker.Business.Service.Interface;

namespace RunnersTracker.Business.Service.Impl
{
    public class RunningLogService : IRunningLogService
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IUnitOfWork unitOfWork;

        public RunningLogService(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }

        public IList<ActivityTypesDTO> ActivityTypes()
        {
            IList<ActivityTypes> activityTypesDB = unitOfWork.ActivityTypesRepository.Get().ToList();
            IList<ActivityTypesDTO> activityTypesDTO = new List<ActivityTypesDTO>();
            Mapper.CreateMap<ActivityTypes, ActivityTypesDTO>();
            foreach (ActivityTypes a in activityTypesDB)
            {
                ActivityTypesDTO temp = Mapper.Map<ActivityTypes, ActivityTypesDTO>(a);
                activityTypesDTO.Add(temp);
            }

            return activityTypesDTO;
        }

        public IList<ShoeDTO> GetUserShoes(UserDTO user)
        {
            IList<Shoe> userShoes = unitOfWork.ShoeRepository.Get(s => s.UserId == user.UserId).ToList();
            IList<ShoeDTO> userShoesDTO = new List<ShoeDTO>();
            Mapper.CreateMap<UserDTO, User>();
            Mapper.CreateMap<Shoe, ShoeDTO>();
            User userEntity = Mapper.Map<UserDTO, User>(user);
            
            foreach (Shoe s in userShoes)
            {
                ShoeDTO temp = Mapper.Map<Shoe, ShoeDTO>(s);
                userShoesDTO.Add(temp);
            }
            logger.Info("Number of user shoes: " + userShoesDTO.Count);
            return userShoesDTO;
        }

        public bool AddActivity(LogEntryDTO logEntryDTO, UserDTO user)
        {
            logger.Info("Inside Add Activity Method");
            LogEntry logEntryEntity = new LogEntry();
            Mapper.CreateMap<LogEntryDTO, LogEntry>();
            logEntryEntity = Mapper.Map<LogEntryDTO, LogEntry>(logEntryDTO);
            if (logEntryDTO.ShoeId.HasValue)
            {
                int _shoeId = (int)logEntryDTO.ShoeId;
                Shoe shoeEntity = unitOfWork.ShoeRepository.GetByID(_shoeId);
                shoeEntity.ShoeDistance += logEntryDTO.Distance;
                logEntryEntity.Shoe = shoeEntity;
            }

            logEntryEntity.User = unitOfWork.UserRepository.GetByID(user.UserId);
            logEntryEntity.ActivityType = unitOfWork.ActivityTypesRepository.GetByID(logEntryDTO.ActivityTypesId);

            unitOfWork.LogEntryRepository.Insert(logEntryEntity);
            unitOfWork.Save();
            return true;           
        }

    }
}
