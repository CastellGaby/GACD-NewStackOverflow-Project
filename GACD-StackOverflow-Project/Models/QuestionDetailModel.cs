using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MiniStackOverflow.Domain.Entities;

namespace GACD_StackOverflow_Project.Models
{
    public class QuestionDetailModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public Guid Id { get; set; }
        public Guid OwnerUserId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime ModificationDate { get; set; }
        public int Votes { get; set; }
        public int Views { get; set; }
        public int QCounterAnswers { get; set; }
        public bool QCorrectAnswers { get; set; }

        /*
        public string Title { get; set; }
       
        public string Description { get; set; }
        public string OwnerUsername { get; set; }
       
        [DataType(DataType.DateTime)]
        public DateTime CreationDateQuestion { get; set; }
        public int Votes { get; set; }

        public IEnumerable<Question> QuestionEnum { get; set; }*/
    }
}