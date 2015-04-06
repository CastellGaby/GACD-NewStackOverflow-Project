using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using AutoMapper;
using GACD_StackOverflow_Project.Models;
using GACD_StackOverflow_Project.Utilities;
using Microsoft.AspNet.Identity;
using MiniStackOverflow.DataDeployed;
using MiniStackOverflow.Domain.Entities;

namespace GACD_StackOverflow_Project.Controllers
{
    [Authorize]
    public class QuestionController : Controller
    {
        //
        // GET: /Question/
        UnitOfWork unitOfWork = new UnitOfWork();
        /*----------------Index------------------------------------------*/
       [AllowAnonymous]
        public ActionResult Index()
        {

            List<QuestionListModel> models = new ListStack<QuestionListModel>();

            //De la entidad Question lo guardo a un Modelo QuestionList
            Mapper.CreateMap<Question, QuestionListModel>();

            var questions = unitOfWork.QuestionRepository.Get();

            foreach (var quest in questions)
            {
                var account = unitOfWork.AccountRepository.GetById(quest.OwnerUserId);
                var qmodel = Mapper.Map<Question, QuestionListModel>(quest);
                //-----------------------------------------
                qmodel.Name = account.Name;
                qmodel.Lastname = account.Lastname;
                RelativeTime Relative=new RelativeTime();
                qmodel.CreationDate = Relative.getRelativeTime(DateTime.Now);
                //-----------------------------------------
                models.Add(qmodel);
            }

            return View(models);

        }
        /*----------------AskQuestion------------------------------------------*/
        public ActionResult AskQuestion()
        {
            var model = new QuestionAskModel();
            return View(model);
        }
        
        [HttpPost]
        public ActionResult AskQuestion(QuestionAskModel modelAskQ)
        {
            AutoMapper.Mapper.CreateMap<QuestionAskModel, Question>().ReverseMap();
            var question = Mapper.Map<QuestionAskModel, Question>(modelAskQ);
            var context = new MiniStackOverflowContext();

            var varc = HttpContext.User.Identity.Name;
            var account = context.Accounts.FirstOrDefault(x => x.Email == varc);
            
            question.OwnerUserId = account.Id;
            var d = account.Id;
            var name = context.Accounts.FirstOrDefault(x => x.Name == varc);
            

            question.CreationDate = DateTime.Now;
            question.ModificationDate = DateTime.Now;
            //question.OwnerUserId = Guid.Parse(HttpContext.User.Identity.Name);
            unitOfWork.QuestionRepository.Insert(question);
            unitOfWork.Save();
            //context.Questions.Add(question);

            context.SaveChanges();
            return RedirectToAction("Index", "Question");
        }
        /*----------------DetailQuestion------------------------------------------*/
        [AllowAnonymous]
        public ActionResult DetailQuestion(Guid questionId)
        {
            //aqui toma la entidad en base al id que recibira del view
            var QuestionUnitEnt = unitOfWork.QuestionRepository.GetById(questionId);
            //crear ma´peo desde la entidad al modelo
            AutoMapper.Mapper.CreateMap<Question, QuestionDetailModel>().ReverseMap();
            QuestionDetailModel model = Mapper.Map<Question, QuestionDetailModel>(QuestionUnitEnt);
            var account = unitOfWork.AccountRepository.GetById(QuestionUnitEnt.OwnerUserId);
            model.Name = account.Name;
            model.LastName = account.Lastname;
            return View(model);
        }
        /*---------------VoteQuestion------------------------------------------*/

        [HttpPost]
        public ActionResult VotePlusQuestion(Guid questId)
        {
            var quest = unitOfWork.QuestionRepository.GetById(questId);
            quest.Votes = quest.Votes + 1;
            unitOfWork.QuestionRepository.Update(quest);
            unitOfWork.Save();

            return RedirectToAction("DetailQuestion","Question",new{questionId=questId});
        }

        [HttpPost]
        public ActionResult VoteLessQuestion(Guid questId)
        {
            var quest = unitOfWork.QuestionRepository.GetById(questId);
            quest.Votes = quest.Votes - 1;
            unitOfWork.QuestionRepository.Update(quest);
            unitOfWork.Save();

            return RedirectToAction("DetailQuestion", "Question", new { questionId = questId });
        }
    }
}