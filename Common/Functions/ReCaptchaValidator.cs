using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Common.Functions
{
    public static class ReCaptchaValidator
    {
        public static void Validate(string recaptchaResponse)
        {
            var captchaResponse = recaptchaResponse;// Request.Form["g-recaptcha-response"];
            var secretKey = "6LeyVqAfAAAAACW6WCzEEdXHplNlRUbF_GAWWh9d";
            var apiUrl = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}";
            var requestUri = string.Format(apiUrl, secretKey, captchaResponse);
            var request = (HttpWebRequest)WebRequest.Create(requestUri);

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    var jResponse = JObject.Parse(stream.ReadToEnd());
                    var isSuccess = jResponse.Value<bool>("success");
                    if (!isSuccess)
                        throw new CustomException("ریکپچا معتبر نمی باشد.");
                }
            } 
        }
    }
}
