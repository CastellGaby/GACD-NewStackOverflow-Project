using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using GACD_StackOverflow_Project.Models;
using MiniStackOverflow.DataDeployed;
using MiniStackOverflow.Domain.Entities;

namespace GACD_StackOverflow_Project.Controllers
{
    [Authorize]
    public class AnswerController : Controller
    {
        UnitOfWork unitOfWork=new UnitOfWork();
        private MiniStackOverflowContext context= new MiniStackOverflowContext();

        /*----------------AnswerQuestion------------------------------------------*/
        public ActionResult AnswerQuestion()
        {
            return View(new AnswerResponseQuestionModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AnswerQuestion(Guid questionId, AnswerResponseQuestionModel AnsQuestion)
        {
            
            var QuestionUnitEnt = unitOfWork.QuestionRepository.GetById(questionId);
            AutoMapper.Mapper.CreateMap<AnswerResponseQuestionModel,Answer>().ReverseMap();
            Answer entidad = Mapper.Map<AnswerResponseQuestionModel, Answer>(AnsQuestion);
            entidad.QuestionId = questionId;
            QuestionUnitEnt.QCounterAnswer += 1;
            unitOfWork.QuestionRepository.Update(QuestionUnitEnt);
           

            var varc = HttpContext.User.Identity.Name;
            var account = context.Accounts.FirstOrDefault(x => x.Email == varc);

            entidad.OwnerId = account.Id;
            
            unitOfWork.AnswerRepository.Insert(entidad);
            unitOfWork.Save();

            return RedirectToAction("Index","Question");
            //return View(AnsQuestion);
            
        }

        /*----------------AnswerList---------------------------------------------*/
        /*
        public ActionResult AnswerList(Guid questionId)
        {
            List<AnswerListAnswersModel> Models=new List<AnswerListAnswersModel>();
            var Question = unitOfWork.QuestionRepository.GetById(questionId);
            
            Mapper.CreateMap<Answer, AnswerListAnswersModel>();
            foreach (Answer answer in Question.AnswersInQuestion)
            {
                            var ans = Mapper.Map<Answer, AnswerListAnswersModel>(answer);
                            ans.OwnerAnswerName = unitOfWork.AccountRepository.GetById(ans.OwnerId).Name;
                            ans.LastName = unitOfWork.AccountRepository.GetById(ans.OwnerId).Lastname;
                            var account = unitOfWork.AccountRepository.GetById(answer.OwnerId);
                            ans.OwnerAnswerName = account.Name;
                            Models.Add(ans);    
            }
      
            return View(Models);
         }*/

        /*----------------VotesAnswer---------------------------------------------*/
        [HttpPost]
        public ActionResult VotePlusAnswer(Guid ansId)
        {
            var ans = unitOfWork.AnswerRepository.GetById(ansId);
            ans.Votes = ans.Votes +1;
            unitOfWork.AnswerRepository.Update(ans);
            unitOfWork.Save();

            return RedirectToAction("DetailQuestion", "Question", new { questionId = ans.QuestionId });
        }

        [HttpPost]
        public ActionResult VoteLessAnswer(Guid ansId)
        {
            var ans = unitOfWork.AnswerRepository.GetById(ansId);
            ans.Votes = ans.Votes -1;
            unitOfWork.AnswerRepository.Update(ans);
            unitOfWork.Save();

            return RedirectToAction("DetailQuestion", "Question", new { questionId = ans.QuestionId }); ;
        }
        /*limpio*/
	}

   
}