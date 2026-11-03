using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net;
using System.Net.Security;

namespace RameshInnovation.SimplyfyiExpress
{
    public static class LegacyHttpHelper
    {
        // ---------------------------
        // GET
        // ---------------------------
        public static string Get(string Url)
        {
            string responseString = "";
            WebClient webClient = new WebClient();
            RemoteCertificateValidationCallback validationCallback = (RemoteCertificateValidationCallback)((param0, param1, param2, param3) => true);
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback += validationCallback;
                responseString = webClient.DownloadString(Url);
                ServicePointManager.ServerCertificateValidationCallback -= validationCallback;
            }
            catch (Exception ex)
            {
                ServicePointManager.ServerCertificateValidationCallback -= validationCallback;
            }

            return responseString;
        }

        // ---------------------------
        // PUT (JSON or raw)
        // ---------------------------
        public static string Put(string url, string jsonPayload, string contentType = "application/json")
        {
            string responseString = "";
            WebClient webClient = new WebClient();
            RemoteCertificateValidationCallback validationCallback = (RemoteCertificateValidationCallback)((param0, param1, param2, param3) => true);
            
            try
            {
                 // Ensure TLS 1.2 is enabled
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
       
                // 3. Make the PUT request using WebClient
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    client.Headers[System.Net.HttpRequestHeader.ContentType] = contentType;
                       
                    // UploadString with "PUT" method sends the data and returns the response
                    responseString = client.UploadString(url, "PUT", jsonPayload);
                }
            }
            catch (Exception ex)
            {
                ServicePointManager.ServerCertificateValidationCallback -= validationCallback;
            }
       
            return responseString;            
        }
    }
}
