using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestMvcDatabaseFirstApproach.ViewModels
{
    [JsonObject("Person")]
    public class PersonViewModel
    {
        [JsonProperty("PersonId")]
        public int PersonId { get; set; }

        [JsonProperty("Username")]
        public string Username { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }
    }
}