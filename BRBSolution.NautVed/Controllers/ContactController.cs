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

            SmtpClient client = new();
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("erlisbrito@gmail.com", "Veonc@270315");
            MailMessage mail = new();
            mail.Sender = new MailAddress("erlisbrito@gmail.com", "Erlis");
            mail.From = new MailAddress("erlisbrito@gmail.com", "Erlis");
            mail.To.Add(new MailAddress("erlisbrito@gmail.com", "Erlis"));
            mail.Subject = "Contato";
            mail.Body = $" Mensagem do site:<br/> Nome:{dispararEmailViewModel.Nome} <br/> Email :{dispararEmailViewModel.Email} <br/> Mensagem :{dispararEmailViewModel.Mensagem}";
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            SmtpClient smtp = new();
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com"; //for gmail host  
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("FromMailAddress", "password");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                // tratar excessão aqui 
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
