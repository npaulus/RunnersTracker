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

namespace RunnersTracker.Business.Service
{
    public class RunningLogService
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ActivityTypesDAC _activityTypesDAC = new ActivityTypesDAC();
        ShoeDAC _shoeDAC = new ShoeDAC();
        LogEntriesDAC _logEntriesDAC = new LogEntriesDAC();
        UserDAC _userDAC = new UserDAC();

        public ISet<ActivityTypesDTO> ActivityTypes()
        {
            ISet<ActivityTypes> activityTypesDB = _activityTypesDAC.ActivityTypes();
            ISet<ActivityTypesDTO> activityTypesDTO = new HashSet<ActivityTypesDTO>();
            Mapper.CreateMap<ActivityTypes, ActivityTypesDTO>();
            foreach (ActivityTypes a in activityTypesDB)
            {
                ActivityTypesDTO temp = Mapper.Map<ActivityTypes, ActivityTypesDTO>(a);
                activityTypesDTO.Add(temp);
            }

            return activityTypesDTO;
        }

        public ISet<ShoeDTO> GetUserShoes(UserDTO user)
        {
            ISet<Shoe> userShoes = new HashSet<Shoe>();
            ISet<ShoeDTO> userShoesDTO = new HashSet<ShoeDTO>();
            Mapper.CreateMap<UserDTO, User>();
            Mapper.CreateMap<Shoe, ShoeDTO>();
            User userEntity = Mapper.Map<UserDTO, User>(user);
            userShoes = _shoeDAC.GetUserShoes(userEntity);
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

            if (_logEntriesDAC.AddNewActivity(logEntryEntity))
            {
                logger.Info("DB Returned true for add");
                return true;
            }
            else
            {
                logger.Info("DB Returned false for add");
                return false;
            }
        }

    }
}
