using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;

namespace funcApp1.net;

public class SendEMail
{
    private readonly ILogger<SendEMail> _logger;

    public SendEMail(ILogger<SendEMail> logger)
    {
        _logger = logger;
    }

    [Function("SendEMail")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        string smtpServer = Environment.GetEnvironmentVariable("SMTP_SERVER") ?? "";
        string smtpPort = Environment.GetEnvironmentVariable("SMTP_PORT") ?? "";
        string smtpUser = Environment.GetEnvironmentVariable("SMTP_USER") ?? "";
        string smtpPass = Environment.GetEnvironmentVariable("SMTP_PASS") ?? "";
        string toEmail = req.Form["email"].ToString();
        string subject = req.Form["subject"].ToString().Trim();
        string body = req.Form["message"].ToString().Trim();
        string sender = req.Form["contactName"].ToString().Trim();


        _logger.LogInformation("Env Variables : {0},{1},{2},{3}",smtpServer,smtpPort,
                string.IsNullOrEmpty(smtpUser)?"User:Empty":"User:****",
                string.IsNullOrEmpty(smtpPass)?"PWD:EMPTY":"PWD:****");

        try
        {
            using (var client = new SmtpClient(smtpServer, int.Parse(smtpPort)))
            {
                client.Credentials = new NetworkCredential(smtpUser, smtpPass);
                client.EnableSsl = true;

                string sub = string.Format("Email From {0} SUBJECT: {1}", 
                    string.IsNullOrEmpty(sender) ? "NoName" : sender,
                    string.IsNullOrEmpty(subject) ? "None" : subject);

                var mail = new MailMessage(smtpUser, toEmail, sub, body);
                
                client.Send(mail);
            }
            _logger.LogInformation("Email sent");
        }
        catch (SmtpException ex)
        {
            _logger.LogError("Error: {0} , {1}", ex.Message, ex.StatusCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
        return new OkObjectResult("OK");

    }
}

