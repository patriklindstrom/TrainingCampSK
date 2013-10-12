using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrainingCamp.Web.Repository;


namespace TrainingCamp.Web
{
    public class AutoTranslatorController : ApiController
    {
        //todo: Write new Get for javascript only get authtoken. write javascript get lang
        public ITranslatorRepo TranslatorRepo { get; set; }

        [HttpGet]
        public string Get()
        {
                // todo make this depenency injected instead with tdt also.
            TranslatorRepo = new TranslatorRepo();
            var AuthToken = "foo";
          AuthToken=  TranslatorRepo.GetBingToken();

            return AuthToken;
        }

        // GET api/<controller>
         [HttpPost]
        public WebText Get([FromBody] WebText webText,[FromUri] string targetLang)
         {
             var fum = "foo";
            // string targetLang = "sv";
            var wText = new WebText
            {
                View = webText.View,
                Lang = targetLang,
                Name = webText.Name,
                HtmlText = "Översatt Text " + targetLang + " 赤春の花の木 " + webText.Name,
                Translator = "Bing"
            };
            return wText;
            /* 
             {
   "WebText": {
       "View": "Home",
       "Lang": "ja",
       "Name": "Welcome",
       "HtmlText": "Översatt Text 赤春の花の木",
       "Translator": "Bing"
   },
   "targetLang": "sv"
             * {
    "Id": "0",
    "View": "Home",
    "Name": "Welcome",
    "Lang": "ja",
    "HtmlText": "Översatt Text ja赤春の花の木Home",
    "Comment": null,
    "CreationTime": "2013-10-08T21:44:32.4214516Z",
    "Translator": "Bing"
}
}
              
             */
        }
        
        // GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        // [HttpGet]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}