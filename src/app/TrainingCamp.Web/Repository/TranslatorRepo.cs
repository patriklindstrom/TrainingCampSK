using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Web;

namespace TrainingCamp.Web.Repository
{
    public interface ITranslatorRepo
    {
        WebText GetTranslation(WebText webText,  string targetLang);
        List<WebText> GetTranslation(List<WebText> webTextList ,string sourceLang,string targetLang);
    }

    public class TranslatorRepo : ITranslatorRepo
    {
        public TranslatorRepo()
        {
        }

        private void OldMain()
        {
            const string toLang = "de";
            //const string toLang = "zh-CHS";
            AdmAccessToken admToken = null;
            string headerValue;
            string fromLang = "en";
            string textToTranslate = "";
            //Get Client Id and Client Secret from https://datamarket.azure.com/developer/applications/
            //Refer obtaining AccessToken (http://msdn.microsoft.com/en-us/library/hh454950.aspx) 
            //AdmAuthentication admAuth = new AdmAuthentication("HelloBingTranslator", "WEG1nbJcFpZB/64CmgJv+Zx+EZeIWbUqj23LAf2bEjh=");
            AdmAuthentication admAuth = new AdmAuthentication("TrainingCamp",
                "Vebd9/FrVsnoZrpn/8wMHRnCz9x3vw/CwoG8/dgIgQI=");
            try
            {
                admToken = admAuth.GetAccessToken();
                // Create a header with the access_token property of the returned token
                headerValue = "Bearer " + admToken.access_token;
                Console.WriteLine("Enter Text to detect language:");
                textToTranslate = Console.ReadLine();
                fromLang = DetectMethod(headerValue, textToTranslate);
            }
            catch (WebException e)
            {
                ProcessWebException(e);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }


            //string from = "en";
            string to = "de";

            string uri = "http://api.microsofttranslator.com/v2/Http.svc/Translate?text=" +
                         System.Web.HttpUtility.UrlEncode(textToTranslate) + "&from=" + fromLang + "&to=" + toLang;
            string authToken = "Bearer" + " " + admToken.access_token;

            HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(uri);
            httpWebRequest.Headers.Add("Authorization", authToken);


            WebResponse response = null;
            try
            {
                response = httpWebRequest.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    System.Runtime.Serialization.DataContractSerializer dcs =
                        new System.Runtime.Serialization.DataContractSerializer(Type.GetType("System.String"));
                    string translation = (string) dcs.ReadObject(stream);
                    Console.WriteLine("Translation for source text '{0}' from {1} to {2} is", textToTranslate, fromLang,
                        toLang);
                    Console.WriteLine(translation);
                }
            }
            catch (WebException e)
            {
                ProcessWebException(e);
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
            }
            Console.WriteLine("Enter to exit");
            Console.ReadLine();
        }

        public WebText GetTranslation(WebText webText,  string targetLang)
        {
            throw new NotImplementedException();
        }
        public List<WebText> GetTranslation(List<WebText> webTextList ,string sourceLang,string targetLang)
        {
            throw new NotImplementedException();
        }
        private static string DetectMethod(string authToken, string textToDetect)
        {
            //Keep appId parameter blank as we are sending access token in authorization header.
            string uri = "http://api.microsofttranslator.com/v2/Http.svc/Detect?text=" + textToDetect;
            HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(uri);
            httpWebRequest.Headers.Add("Authorization", authToken);
            string languageDetected = null;
            WebResponse response = null;
            try
            {
                response = httpWebRequest.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    System.Runtime.Serialization.DataContractSerializer dcs =
                        new System.Runtime.Serialization.DataContractSerializer(Type.GetType("System.String"));
                    languageDetected = (string) dcs.ReadObject(stream);
                    Console.WriteLine(string.Format("Language detected:{0}", languageDetected));
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                }
            }

            catch (WebException e)
            {
                ProcessWebException(e);
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
            }
            return languageDetected;
        }

        private static void ProcessWebException(WebException e)
        {
            Console.WriteLine("{0}", e.ToString());
            // Obtain detailed error information
            string strResponse = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse) e.Response)
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(responseStream, System.Text.Encoding.ASCII))
                    {
                        strResponse = sr.ReadToEnd();
                    }
                }
            }
            Console.WriteLine("Http status code={0}, error message={1}", e.Status, strResponse);
        }
    }

    [DataContract]
    public class AdmAccessToken
    {
        [DataMember]
        public string access_token { get; set; }

        [DataMember]
        public string token_type { get; set; }

        [DataMember]
        public string expires_in { get; set; }

        [DataMember]
        public string scope { get; set; }
    }

    public class AdmAuthentication
    {
        public static readonly string DatamarketAccessUri = "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";
        private string clientId;
        private string clientSecret;
        private string request;
        private AdmAccessToken token;
        private Timer accessTokenRenewer;

        //Access token expires every 10 minutes. Renew it every 9 minutes only.
        private const int RefreshTokenDuration = 9;

        public AdmAuthentication(string clientId, string clientSecret)
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
            //If clientid or client secret has special characters, encode before sending request
            this.request =
                string.Format(
                    "grant_type=client_credentials&client_id={0}&client_secret={1}&scope=http://api.microsofttranslator.com",
                    HttpUtility.UrlEncode(clientId), HttpUtility.UrlEncode(clientSecret));
            this.token = HttpPost(DatamarketAccessUri, this.request);
            //renew the token every specfied minutes
            accessTokenRenewer = new Timer(new TimerCallback(OnTokenExpiredCallback), this,
                TimeSpan.FromMinutes(RefreshTokenDuration), TimeSpan.FromMilliseconds(-1));
        }

        public AdmAccessToken GetAccessToken()
        {
            return this.token;
        }


        private void RenewAccessToken()
        {
            AdmAccessToken newAccessToken = HttpPost(DatamarketAccessUri, this.request);
            //swap the new token with old one
            //Note: the swap is thread unsafe
            this.token = newAccessToken;
            Console.WriteLine(string.Format("Renewed token for user: {0} is: {1}", this.clientId,
                this.token.access_token));
        }

        private void OnTokenExpiredCallback(object stateInfo)
        {
            try
            {
                RenewAccessToken();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Failed renewing access token. Details: {0}", ex.Message));
            }
            finally
            {
                try
                {
                    accessTokenRenewer.Change(TimeSpan.FromMinutes(RefreshTokenDuration), TimeSpan.FromMilliseconds(-1));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format(
                        "Failed to reschedule the timer to renew access token. Details: {0}", ex.Message));
                }
            }
        }


        private AdmAccessToken HttpPost(string DatamarketAccessUri, string requestDetails)
        {
            //Prepare OAuth request 
            WebRequest webRequest = WebRequest.Create(DatamarketAccessUri);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "POST";
            byte[] bytes = Encoding.ASCII.GetBytes(requestDetails);
            webRequest.ContentLength = bytes.Length;
            using (Stream outputStream = webRequest.GetRequestStream())
            {
                outputStream.Write(bytes, 0, bytes.Length);
            }
            using (WebResponse webResponse = webRequest.GetResponse())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof (AdmAccessToken));
                //Get deserialized object from JSON stream
                AdmAccessToken token = (AdmAccessToken) serializer.ReadObject(webResponse.GetResponseStream());
                return token;
            }
        }
    }



}