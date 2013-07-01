using System.Web;
using System.Web.Optimization;

namespace TrainingCamp.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/layout.css",
                "~/Content/color.css"
                            ));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                "~/Content/themes/base/jquery.ui.core.css",
                "~/Content/themes/base/jquery.ui.resizable.css",
                "~/Content/themes/base/jquery.ui.selectable.css",
                "~/Content/themes/base/jquery.ui.accordion.css",
                "~/Content/themes/base/jquery.ui.autocomplete.css",
                "~/Content/themes/base/jquery.ui.button.css",
                "~/Content/themes/base/jquery.ui.dialog.css",
                "~/Content/themes/base/jquery.ui.slider.css",
                "~/Content/themes/base/jquery.ui.tabs.css",
                "~/Content/themes/base/jquery.ui.datepicker.css",
                "~/Content/themes/base/jquery.ui.progressbar.css",
                "~/Content/themes/base/jquery.ui.theme.css"));
            //Bundles for my superweb toolkit 
            bundles.Add(new StyleBundle("~/extlibs/superweb/css").Include(
                "~/extlibs/bootstrap/css/bootstrap.css",
// plugin css
                "~/extlibs/js-plugin/pretty-photo/css/prettyPhoto.css",
                "~/extlibs/js-plugin/one_by_one/css/jquery.onebyone.css",
                "~/extlibs/js-plugin/one_by_one/css/animate.css",
// icon fonts
                "~/extlibs/font-icons/custom-icons/css/custom-icons.css"
            ))
            ;
            bundles.Add(new ScriptBundle("~/extlibs/superweb/js").Include(
            "~/extlibs/js-plugin/respond/respond.min.js",
            "~/extlibs/js-plugin/jquery-ui/jquery-ui-1.8.23.custom.min.js",
                "~/extlibs/bootstrap/js/bootstrap.js",
                "~/extlibs/js-plugin/waypoint/waypoints.min.js",
                "~/extlibs/js-plugin/waypoint/sticky/waypoints-sticky.min.js",
                // One by one Slider  
                "~/extlibs/js-plugin/one_by_one/jquery.onebyone.min.js",
                "~/extlibs/js-plugin/one_by_one/jquery.touchwipe.min.js",
                //Custom
               "~/Scripts/custom.js"
                            )
                );
        }
    }
}