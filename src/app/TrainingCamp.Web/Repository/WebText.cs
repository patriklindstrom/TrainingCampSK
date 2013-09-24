using System;

namespace TrainingCamp.Web.Repository
{
    public class WebText
    {
        public WebText()
        {
           
            CreationTime = DateTime.UtcNow;
        }
        //ID
        public String Id { get; set; }
        //Keys
        public String View { get; set; }
        public String Name { get; set; }
        public String Lang { get; set; }
        //Data
        public String HtmlText { get; set; }
        public String Comment { get; set; }
        //Metadata
        public DateTime CreationTime { get; set; }
        public string Translator { get; set; }
    }

    public class WebTextArchive
    {
        //ID
        public String Id { get; set; }
        //ArchiveData
        public WebText WebText { get; set; }
        //Metadata
        public DateTime ArchiveTime { get; set; }
    }

    public class WebTextCombined
    { //key
        public String View { get; set; }
        public String Name { get; set; } 
        //data
        public WebText WebTextLeft { get; set; }
        public WebText WebTextRight { get; set; }

    }
}