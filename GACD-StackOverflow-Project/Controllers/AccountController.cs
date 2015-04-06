using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using GACD_StackOverflow_Project.Models;
using GACD_StackOverflow_Project.Utilities;
using MiniStackOverflow.DataDeployed;
using MiniStackOverflow.Domain.Entities;

namespace GACD_StackOverflow_Project.Controllers
{
   
    public class AccountController : Controller
    {

        public AccountController()
        {
            
        }
        readonly IMappingEngine _mappingEngine;
        public UnitOfWork UnitOfWork = new UnitOfWork();
        string subject, text;
        
        public AccountController(IMappingEngine mappingEngine)
        {
            _mappingEngine = mappingEngine;
        }

        /*----------------Login--------------------------------*/
        public ActionResult Login()
        {
            int n = 0;
            Session["Session"] = n;
            
            var model = new AccountLoginModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(AccountLoginModel modelLogin)
        {
           
            var context = new MiniStackOverflowContext();
            var account = context.Accounts.FirstOrDefault(x => x.Email == modelLogin.Email);
            if (account != null)
            {
                
                if (account.Password == modelLogin.Password)
                {
                    FormsAuthentication.SetAuthCookie(modelLogin.Email, false);
                    return RedirectToAction("Index", "Question");
                }
                int session = (int) (Session["Session"]);
                session += 1;
                Session["Session"] = session;
                if ( session>= 3)
                {
                    modelLogin.ImnotRobot = true;

                }
            }
            TempData["NotLogin"] = "Plase confirm your credentials, complete all the fields and try again.";
            return View(modelLogin);


        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Question");
        }

        /*----------------Register-----------------------------*/
        public ActionResult Register()
        {
            var model = new AccountRegisterModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(AccountRegisterModel modelRegister)
        {
            var context = new MiniStackOverflowContext();
            var getAccount = context.Accounts.FirstOrDefault(x => x.Email == modelRegister.Email);
            if (ModelState.IsValid)
            {

                if (modelRegister.Password == modelRegister.Confirm)
                {
                    if (getAccount == null)
                    {
                        //Primero Registro
                        AutoMapper.Mapper.CreateMap<AccountRegisterModel, Account>().ReverseMap();
                        Account newAccount2 = AutoMapper.Mapper.Map<AccountRegisterModel, Account>(modelRegister);

                        var account2 = Mapper.Map<AccountRegisterModel, Account>(modelRegister);

                        
                        context.Accounts.Add(account2);
                        context.SaveChanges();

                        //Enviar correo de Activación
                        var Host = HttpContext.Request.Url.Host;
                        if (Host == "localhost")
                        {
                            Host = Request.Url.GetLeftPart(UriPartial.Authority);
                        }

                        subject = "Confirm Account";
                        text = "Welcome to MiniStackOverflow activate your account "+Host+"/Account/ConfirmationAccount/?idAccount=" + account2.Id;

                        MailSender.SendSimpleMessage(modelRegister.Email, subject, text);
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        TempData["UserError"] = "An Account with email "+getAccount.Email+" does exist";
                        return View(modelRegister);
                    }
               
                    //return RedirectToAction("Login");

                 
                }
                TempData["FailRegister"] = "There is a problem with your registration, please check that the password and confrim field are the same and try again.";
                return View(modelRegister);
            }
            TempData["FailRegister2"] = "All the fields Requested.";
            return View(modelRegister);
        }

        /*----------------PassworRecovery-----------------------------*/
        public ActionResult PasswordRecovery()
        {
            var model = new AccountPasswordRecoveryModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult PasswordRecovery(AccountPasswordRecoveryModel modelPasswprdRecovery)
        {
            var context = new MiniStackOverflowContext();
            var account = context.Accounts.FirstOrDefault(x => x.Email==modelPasswprdRecovery.Email);

           
            if (account != null)
            {
                var Host = HttpContext.Request.Url.Host;
                if (Host == "localhost")
                {
                    Host = Request.Url.GetLeftPart(UriPartial.Authority);
                }
                string to = modelPasswprdRecovery.Email;
                subject = "Recover Password Email";
                text = Host + "/Account/AccountRecovery/?OwnerId=" + account.Id;
                MailSender.SendSimpleMessage(to, subject, text);
                TempData["ErrorEmail"] = "The message was send ";
                return View(modelPasswprdRecovery);
            }
            TempData["ErrorEmail"] = "There isn't an account with this email address "+modelPasswprdRecovery.Email;
            return View(modelPasswprdRecovery);
        }

        /*----------------Profile------------------------------------*/
        
        public ActionResult Profile(Guid OwnerId)
        {
            var OwnerAccount = UnitOfWork.AccountRepository.GetById(OwnerId);
            Mapper.CreateMap<Account, AccountProfileModel>();
            OwnerAccount.Views += 1;

            UnitOfWork.AccountRepository.Update(OwnerAccount);
            UnitOfWork.Save();
            var model = Mapper.Map<Account, AccountProfileModel>(OwnerAccount);
           
            return View(model);
            
            
        }
        /*----------------AccountRecovery-----------------------------*/
        public ActionResult AccountRecovery(Guid OwnerId)
        {
            return View(new AccountRecoveryAccountModel());
        }

        [HttpPost]
        public ActionResult AccountRecovery(Guid OwnerId,AccountRecoveryAccountModel AccountRecovery)
        {
            var account = UnitOfWork.AccountRepository.GetById(OwnerId);
            if (ModelState.IsValid)
            {
                if (AccountRecovery.NewPassword == AccountRecovery.ConfirmNewPassword)
                {
                    account.Password = AccountRecovery.NewPassword;
                    UnitOfWork.AccountRepository.Update(account);
                    UnitOfWork.Save();
                    TempData["Updated"] = "Your Password was updated";
                    return RedirectToAction("Login");
                }
                TempData["Fail"] = "Password must be the same, try again";
            }
            return View(AccountRecovery);
        }
        /*----------------ConfirmationAccount-----------------------------*/

        public ActionResult ConfirmationAccount()
        {
            return View(new AccountConfirmationAccountModel());
        }

        [HttpPost]
        public ActionResult ConfirmationAccount(Guid idAccount)
        {
            var account = UnitOfWork.AccountRepository.GetById(idAccount);
            account.isAuthenticated = true;
            UnitOfWork.AccountRepository.Update(account);
            UnitOfWork.Save();
            
            return RedirectToAction("Login");
        }
        //clean 2
        
    }
}