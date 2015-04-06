using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStackOverflow.Domain.Entities
{
    public class Question : IEntity
    {
        public Question()
        {
            Id = Guid.NewGuid();
            //OwnerUserId = Guid.NewGuid();
            //CreationDate = DateTime.Now;
           //ModificationDate = DateTime.Now;
            QCorrectAnswer = false;
        }
        public Guid Id { get; set; }
        public Guid OwnerUserId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Votes { get; set; }
        public int Views { get; set; }
        public int QCounterAnswer { get; set; }
        public bool QCorrectAnswer { get; set; }
        public virtual ICollection<Answer> AnswersInQuestion { get; set; }
    }
}
