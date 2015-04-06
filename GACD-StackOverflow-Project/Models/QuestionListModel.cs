using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MiniStackOverflow.Domain.Entities;

namespace GACD_StackOverflow_Project.Models
{
    public class QuestionListModel 
    {
        
        public string Title { get; set; }
        public string Description { get; set; }
        public string OwnerUsername { get; set; }
        //------------------------------------------
        public string Name { get; set; }
        public string Lastname { get; set; }
        //------------------------------------------
        public Guid Id { get; set; }
        public Guid OwnerUserId { get; set; }
        //Edite de Datetime a String
        [DataType(DataType.DateTime)]
        public string CreationDate { get; set; }
        public int Votes { get; set; }
        public int Views{ get; set; }
        public int QCounterAnswers { get; set; }

       
    }
}