using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStackOverflow.Domain.Entities
{
    public class Answer:IEntity
    {
        public Guid Id { get; set; }

        public Answer()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }

        public Guid OwnerId { get; set; }
        public Guid QuestionId { get; set; }
        public string Description { get; set; }
        public int Votes { get; set; }
        public bool isCorrect { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
