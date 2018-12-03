using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TestMvcDatabaseFirstApproach.DataAccess.Repository
{
    public class LogRepository : Repository<DataAccess.Log>
    {
        public IEnumerable<Log> GetLogsWhichContainsClientCode(string clientCode)
        {
            var logs = _dbSet.Where(l => l.Message.Contains(clientCode));
            return logs;
        }
    }
}