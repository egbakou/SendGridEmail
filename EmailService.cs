using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SendGridEmailClient
{
    public static class EmailService
    {
        private static string API_KEY = "KEY";


        public static async Task<Response> SendEmailAsync(string _filePath, string _fileName, string _to, string _subject, string _htmlContent)
        {

            var client = new SendGridClient(API_KEY);

            var from = new EmailAddress("email@email.com", "COMPANY NAME");
            var to = new EmailAddress(_to, _to);


            var msg = MailHelper.CreateSingleEmail(from, to, _subject, null, _htmlContent);

            //var bytes = File.ReadAllBytes(_filePath);

            var file = Convert.ToBase64String(ReadFile(_filePath));
            msg.AddAttachment(_fileName, file);
            var response = await client.SendEmailAsync(msg);
            return response;
        }



        public static byte[] ReadFile(string filePath)
        {
            byte[] buffer;
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileStream.Length;  // get file length
                buffer = new byte[length];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  // sum is a buffer offset for next reading
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
        }
        
    }
}
