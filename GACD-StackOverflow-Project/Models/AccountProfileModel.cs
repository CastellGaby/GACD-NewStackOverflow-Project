using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GACD_StackOverflow_Project.Models
{
    public class AccountProfileModel
    {
        public string Username{ get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }

        [DataType(DataType.EmailAddress)]
        public string  Email{ get; set; }
        //public int ReputationVotes { get; set; }


    }
}