using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestMvcDatabaseFirstApproach.DataAccess.Repository;
using TestMvcDatabaseFirstApproach.Mappings;
using TestMvcDatabaseFirstApproach.ViewModels;

namespace TestMvcDatabaseFirstApproach.Controllers
{
    public class LogsController : Controller
    {
        private readonly LogRepository _logRepository;

        public LogsController()
        {
            _logRepository = new LogRepository();
        }

        public IEnumerable<LogViewModel> GetLogsByClientCode(string clientCode)
        {
            var logs = _logRepository.GetLogsWhichContainsClientCode(clientCode);
            var logViewModels = logs.Select(l => l.ToLogViewModel());
            var listOfLogViewModels = logViewModels.ToList();
            return listOfLogViewModels;
        }
    }
}