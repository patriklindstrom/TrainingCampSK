using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Web;
using System.Xml.Linq;
using DotNetOpenAuth.Messaging;
using Raven.Abstractions.Extensions;

namespace TrainingCamp.Web.Repository
{
    public interface ITranslatorRepo
    {
        WebText GetTranslation(WebText webText,  string targetLang);
        List<WebText> GetTranslation(List<WebText> webTextList ,string sourceLang,string targetLang);
        string GetBingToken();
        List<WebText> TranslateAll(string sourceLang, string targetLang,List<WebText>  webTexts);
       
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
            AdmAuthentication admAuth = new AdmAuthentication("HelloBingTranslator", "WEG1nbJcFpZB/64CmgJv+Zx+EZeIWbUqj23LAf2bEjg=");
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
        // http://msdn.microsoft.com/en-us/library/ff512419.aspx
        public List<WebText> GetTranslation(List<WebText> webTextList ,string sourceLang,string targetLang)
        {
            List<WebText> webTexts = null;
            var access_token = GetBingToken();
            string uri = "http://api.microsofttranslator.com/v2/Http.svc/TranslateArray";

            string body = "<TranslateArrayRequest>" +
                            "<AppId />" +
                            "<From>{0}</From>" +
                            "<Options>" +
                               " <Category xmlns=\"http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2\" />" +
                                "<ContentType xmlns=\"http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2\">{1}</ContentType>" +
                                "<ReservedFlags xmlns=\"http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2\" />" +
                                "<State xmlns=\"http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2\" />" +
                                "<Uri xmlns=\"http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2\" />" +
                                "<User xmlns=\"http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2\" />" +
                            "</Options>" +
                            "<Texts>" +
                               "<string xmlns=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\">{2}</string>" +
                               "<string xmlns=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\">{3}</string>" +
                               "<string xmlns=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\">{4}</string>" +
                            "</Texts>" +
                            "<To>{5}</To>" +
                         "</TranslateArrayRequest>";
            string reqBody = string.Format(body, sourceLang, "text/plain", webTextList[0].HtmlText, webTextList[1].HtmlText, webTextList[2].HtmlText, targetLang);

            // create the request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Headers.Add("Authorization", access_token);
            request.ContentType = "text/xml";
            request.Method = "POST";
            using (System.IO.Stream stream = request.GetRequestStream())
            {
                byte[] arrBytes = System.Text.Encoding.UTF8.GetBytes(reqBody);
                stream.Write(arrBytes, 0, arrBytes.Length);
            }

            WebResponse response = null;
            try
            {
                response = request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader rdr = new StreamReader(stream, System.Text.Encoding.UTF8))
                    {
                        // Deserialize the response
                        string strResponse = rdr.ReadToEnd();
                        
                        XDocument doc = XDocument.Parse(@strResponse);
                        XNamespace ns = "http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2";
                        int soureceTextCounter = 0;
                        foreach (XElement xe in doc.Descendants(ns + "TranslateArrayResponse"))
                        {

                            foreach (var node in xe.Elements(ns + "TranslatedText"))
                            {
                                Debug.Write(node.Value);
                            }
                            soureceTextCounter++;
                        }
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey(true);
                    }
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
            return webTexts;
        }

        public string GetBingToken()
        {
            AdmAccessToken admToken = null;
            AdmAuthentication admAuth = new AdmAuthentication("HelloBingTranslator",
                "WEG1nbJcFpZB/64CmgJv+Zx+EZeIWbUqj23LAf2bEjg=");
            try
            {
                admToken = admAuth.GetAccessToken();             
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
            return admToken.access_token;
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

        public List<WebText>  TranslateAll( string targetLang,string sourceLang,List<WebText> webTexts)
        {
            //TODO: Next day chore
           return GetTranslation(webTextList: webTexts, targetLang: targetLang, sourceLang: sourceLang);            
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