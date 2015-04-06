using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GACD_StackOverflow_Project.Models
{
    public class AnswerListAnswersModel
    {
        public string Description { get; set; }
        public int Votes { get; set; }
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public Guid QuestionId { get; set; }
        [DataType(DataType.DateTime)]
        public string CreationDate { get; set; }
        public string OwnerAnswerName { get; set; }
        public string LastName { get; set; }
        public bool isCorrect{ get; set; }

    }
}