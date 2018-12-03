using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestMvcDatabaseFirstApproach.ViewModels;

namespace TestMvcDatabaseFirstApproach.Mappings
{
    public static class LogMapper
    {
        public static LogViewModel ToLogViewModel(this DataAccess.Log logEntity)
        {
            return new LogViewModel
            {
                LogId = logEntity.LogId,
                Message = logEntity.Message,
                CreateBy = logEntity.CreateBy
            };
        }
    }
}