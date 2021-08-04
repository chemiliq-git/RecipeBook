using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Logging;

namespace RecipeBook.Services.Data
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> logger;
        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor, ILogger<EmailSender> logger)
        {
            Options = optionsAccessor.Value;
            this.logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await Execute(email, subject, htmlMessage);
        }

        private async Task Execute(string email, string subject, string htmlMessage)
        {
            MailjetClient client = new MailjetClient(Options.APIKey, Options.APISecret)
            {               
            };
            MailjetRequest request = new MailjetRequest
            {
                Resource = SendV31.Resource,
            }
             .Property(Send.Messages, new JArray {
     new JObject {
      {
       "From",
       new JObject {
        {"Email", Options.Email},
        {"Name", Options.Name}
       }
      }, {
       "To",
       new JArray {
        new JObject {
         {
          "Email",
          email
         }, {
          "Name",
          "User"
         }
        }
       }
      }, {
       "Subject",
       subject
      }, {
       "TextPart",
       "My first Mailjet email"
      }, {
       "HTMLPart",
       htmlMessage
      }, {
       "CustomID",
       "AppGettingStartedTest"
      }
     }
             });
            MailjetResponse response = await client.PostAsync(request);
            if (response.IsSuccessStatusCode)
            {
                //TODO
                //Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                //Console.WriteLine(response.GetData());
            }
            else
            {
                //TODOD
                //Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
                //Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                //Console.WriteLine(response.GetData());
                //Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
            }

        }
    }
}

