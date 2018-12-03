using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestMvcDatabaseFirstApproach.ViewModels
{
    public class LogViewModel
    {
        public int LogId { get; set; }
        public string Message { get; set; }
        public string CreateBy { get; set; }
    }
}