using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniStackOverflow.Domain.Entities;

namespace MiniStackOverflow.DataDeployed
{
    public class UnitOfWork
    {

        private MiniStackOverflowContext context = new MiniStackOverflowContext();
        private GenericRepository<Account> accountRepository;
        private GenericRepository<Question> questionRepository;
        private GenericRepository<Answer> answerRepository;

        public GenericRepository<Account> AccountRepository
        {
            get
            {

                if (this.accountRepository == null)
                {
                    this.accountRepository = new GenericRepository<Account>(context);
                }
                return accountRepository;
            }
        }

        public GenericRepository<Question> QuestionRepository
        {
            get
            {

                if (this.questionRepository == null)
                {
                    this.questionRepository = new GenericRepository<Question>(context);
                }
                return questionRepository;
            }
        }
        
        public GenericRepository<Answer> AnswerRepository
        {
            get
            {

                if (this.answerRepository == null)
                {
                    this.answerRepository = new GenericRepository<Answer>(context);
                }
                return answerRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }

}
