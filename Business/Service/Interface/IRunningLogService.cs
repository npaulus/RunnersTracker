﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RunnersTracker.Business.DTO;

namespace RunnersTracker.Business.Service.Interface
{
    public interface IRunningLogService
    {
        IList<ActivityTypesDTO> ActivityTypes();
        IList<ShoeDTO> GetUserShoes(UserDTO user);
        bool AddActivity(LogEntryDTO logEntryDTO, UserDTO user);
    }
}
