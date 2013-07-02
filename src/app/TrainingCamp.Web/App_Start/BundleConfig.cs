using System.Web;
using System.Web.Optimization;

namespace TrainingCamp.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Google Api map first

            // Css for customization
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/layout.css"));
            //Bundles for my superweb toolkit 
            bundles.Add(new StyleBundle("~/Extlibs/superweb/css").Include(
                "~/Extlibs/bootstrap/css/bootstrap.css",
                // plugin css
                "~/Extlibs/js-plugin/pretty-photo/css/prettyPhoto.css",
                "~/Extlibs/js-plugin/flexslider/flexslider.css",              
                "~/Extlibs/js-plugin/one_by_one/css/jquery.onebyone.css",
                "~/Extlibs/js-plugin/one_by_one/css/animate.css",
                "~/Extlibs/js-plugin/neko-contact-ajax-plugin/css/cmxformTemplate.css",
                // icon fonts
                "~/Extlibs/font-icons/custom-icons/css/custom-icons.css"));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));


 
            bundles.Add(new ScriptBundle("~/Extlibs/superweb/js").Include(
                "~/Extlibs/js-plugin/respond/respond.src.js",
                "~/Extlibs/js-plugin/jquery-ui/jquery-ui-1.8.23.custom.js",
                //Third party plubins //Third party plubins 
                "~/Extlibs/bootstrap/js/bootstrap.js",
                "~/Extlibs/js-plugin/easing/jquery.easing.1.3.js",
                "~/Scripts/jquery.validate.js",
                "~/Extlibs/js-plugin/waypoint/waypoints.js",
                "~/Extlibs/js-plugin/waypoint/sticky/waypoints-sticky.js",
                // One by one Slider  
                "~/Extlibs/js-plugin/one_by_one/jquery.onebyone.js",
                "~/Extlibs/js-plugin/one_by_one/jquery.touchwipe.js",
                //Custom
                "~/Scripts/custom.js"
                            )
                );
        }
    }
}