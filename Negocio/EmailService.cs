using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class EmailService
    {
        private MailMessage email;
        private SmtpClient server;

        public EmailService()
        {
            server = new SmtpClient();
            server.Credentials = new NetworkCredential("miguelbucciero@gmail.com", "bvnf kneu ldli lwyn");
            server.EnableSsl = true;
            server.Port = 587;
            server.Host = "smtp.gmail.com";
        }

        public void armarCorreo(string emailDestino, string asunto , string cuerpo)
        {
            email = new MailMessage();
            email.From = new MailAddress("noresponder@pokedexWeb.com");
            email.To.Add(emailDestino);
            email.Subject = (asunto);
            email.IsBodyHtml = true;
            email.Body = cuerpo;
        }

        public void enviarEmail()
        {
            try
            {
                server.Send(email);
            }
            catch (SmtpException ex)
            {
                throw new Exception("Error sending email: " + ex.Message, ex);
            }
        }
    }
}

