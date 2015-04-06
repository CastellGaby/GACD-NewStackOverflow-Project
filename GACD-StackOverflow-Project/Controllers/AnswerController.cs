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
    public class AnswerController : Controller
    {
        UnitOfWork unitOfWork=new UnitOfWork();
        public ActionResult AnswerQuestion()
        {
            return View(new AnswerResponseQuestionModel());
        }

        [HttpPost]
        public ActionResult AnswerQuestion(Guid questionId, AnswerResponseQuestionModel AnsQuestion)
        {
            
            var QuestionUnitEnt = unitOfWork.QuestionRepository.GetById(questionId);
            AutoMapper.Mapper.CreateMap<AnswerResponseQuestionModel,Answer>().ReverseMap();
            Answer entidad = Mapper.Map<AnswerResponseQuestionModel, Answer>(AnsQuestion);
            entidad.QuestionId = questionId;
            QuestionUnitEnt.QCounterAnswer += 1;
            unitOfWork.QuestionRepository.Update(QuestionUnitEnt);
            unitOfWork.AnswerRepository.Insert(entidad);
            unitOfWork.Save();

            //entidad.OwnerId = Guid.Parse(HttpContext.User.Identity.Name);
            
            return RedirectToAction("Index","Question");
            //return View(AnsQuestion);
            
        }

        public ActionResult AnswerList(Guid questionId)
        {
            List<AnswerListModel> Models=new List<AnswerListModel>();
            var Question = unitOfWork.QuestionRepository.GetById(questionId);
/*
            Mapper.CreateMap<Answer, AnswerListModel>();
            foreach (Answer answer in Question.A)
            {
                var ans=Mapper.Map<Answer, AnswerListModel>(answer);
                Models.Add(ans);    
            }
            */
            return RedirectToAction("Index", "Question");
            

        }
	}

    public class AnswerListModel
    {
        public string Description { get; set; }
        public int Votes { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        //public bool isCorrect{ get; set; }
    }
}