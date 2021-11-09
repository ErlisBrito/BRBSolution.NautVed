using BRBSolution.NautVed.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BRBSolution.NautVed.Controllers
{
    public class ContactController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DispararEmail(DispararEmailViewModel dispararEmailViewModel)
        {
            string enviarEmail = "erlisbrito@gmail.com";
            string senha = "Veonc@270315";

            SmtpClient client = new()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(enviarEmail, senha)
            };

            //SmtpClient client = new SmtpClient();
            //client.Host = "smtp.gmail.com";
            //client.EnableSsl = true;
            //client.Credentials = new NetworkCredential(enviarEmail, senha);
            MailMessage mail = new()
            {
                Sender = new MailAddress(enviarEmail, senha),
                From = new MailAddress(enviarEmail, "Roger")
            };
            mail.To.Add(new MailAddress(dispararEmailViewModel.Email, dispararEmailViewModel.Nome));
            mail.Subject = "Contato";
            mail.Body = $" Olá {dispararEmailViewModel.Nome} recebemos o seu contato," +
                $"e Logo um de nossos representantes entrará em contato com você!";
                //$" Email : {dispararEmailViewModel.Email} " +
                //$" Mensagem : {dispararEmailViewModel.Mensagem}";
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                //trata erro
            }
            finally
            {
                mail = null;
            }


            return View();
        }


        public bool ValidarEmail(string email)
        {
            Regex regex = new(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            return match.Success;
        }

    }
}
