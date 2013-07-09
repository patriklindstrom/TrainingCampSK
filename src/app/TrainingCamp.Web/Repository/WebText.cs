using System;

namespace TrainingCamp.Web.Repository
{
    public class WebText
    {
        //ID
        public int WebTextId { get; set; }
        //Keys
        public String View { get; set; }
        public String Name { get; set; }
        public String Lang { get; set; }
        //Data
        public String HtmlText { get; set; }
        public String Comment { get; set; }
    }
}