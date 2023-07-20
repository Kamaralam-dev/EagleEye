using System.Web;
using System.Web.Optimization;

namespace EagleEye
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/vendor/bootstrap/css/bootstrap.css", /*Vendor CSS*/
                      "~/Content/vendor/font-awesome/css/font-awesome.css",
                      "~/Content/vendor/magnific-popup/magnific-popup.css",
                      "~/Content/vendor/bootstrap-datepicker/css/datepicker3.css",
                      "~/Content/vendor/jquery-ui/css/ui-lightness/jquery-ui-1.10.4.custom.css",//Specific Page Vendor CSS*/
                      "~/Content/vendor/select2/select2.css",
                      "~/Content/vendor/bootstrap-multiselect/bootstrap-multiselect.css",
                      "~/Content/vendor/morris/morris.css",
                      "~/Content/stylesheets/theme.css",/*Theme CSS*/
                      "~/Content/stylesheets/skins/default.css",/*Skin CSS */
                      "~/Content/stylesheets/theme-custom.css", /*Theme Custom CSS*/
                      "~/Content/stylesheets/datatable1.10.9.css", /*Server side datatable CSS*/
                      "~/Content/javascripts/custom/pnotify/pnotify.css" /*notification*/
                      ));

            bundles.Add(new StyleBundle("~/Content/reportCss").Include(
                     "~/Content/vendor/bootstrap/css/bootstrap.css", /*Vendor CSS*/
                     "~/Content/vendor/font-awesome/css/font-awesome.css",
                      "~/Content/stylesheets/theme.css",/*Theme CSS*/
                      "~/Content/stylesheets/skins/default.css",/*Skin CSS */
                      "~/Content/stylesheets/theme-custom.css" /*Theme Custom CSS*/
                     ));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                     "~/Content/vendor/modernizr/modernizr.js"));

            bundles.Add(new ScriptBundle("~/bundles/VendorJs").Include(
                    "~/Content/vendor/jquery/jquery.js",
                    "~/Content/vendor/jquery-browser-mobile/jquery.browser.mobile.js",
                    "~/Content/vendor/bootstrap/js/bootstrap.js",
                    "~/Content/vendor/nanoscroller/nanoscroller.js",
                    "~/Content/vendor/bootstrap-datepicker/js/bootstrap-datepicker.js",
                    "~/Content/vendor/magnific-popup/magnific-popup.js",
                    "~/Content/vendor/jquery-placeholder/jquery.placeholder.js"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/SpecificPageJS").Include(
                     "~/Content/vendor/jquery-ui/js/jquery-ui-1.10.4.custom.js",
                     "~/Content/vendor/jquery-ui-touch-punch/jquery.ui.touch-punch.js",
                     "~/Content/vendor/jquery-appear/jquery.appear.js",
                     "~/Content/vendor/select2/select2.js",
                     "~/Content/vendor/bootstrap-multiselect/bootstrap-multiselect.js",
                     "~/Content/vendor/jquery-maskedinput/jquery.maskedinput.js",
                     "~/Content/vendor/jquery-easypiechart/jquery.easypiechart.js",
                     "~/Content/vendor/flot/jquery.flot.js",
                     "~/Content/vendor/flot-tooltip/jquery.flot.tooltip.js",
                     "~/Content/vendor/flot/jquery.flot.pie.js",
                     "~/Content/vendor/flot/jquery.flot.categories.js",
                     "~/Content/vendor/flot/jquery.flot.resize.js",
                     "~/Content/vendor/jquery-sparkline/jquery.sparkline.js",
                     "~/Content/vendor/raphael/raphael.js",
                     "~/Content/vendor/morris/morris.js",
                     "~/Content/vendor/gauge/gauge.js",
                     "~/Content/vendor/snap-svg/snap.svg.js",
                     "~/Content/vendor/liquid-meter/liquid.meter.js",
                     "~/Content/vendor/jqvmap/jquery.vmap.js",
                     "~/Content/vendor/jqvmap/data/jquery.vmap.sampledata.js",
                     "~/Content/jqvmap/jqvmap/maps/jquery.vmap.world.js",
                     "~/Content/vendor/jqvmap/maps/continents/jquery.vmap.africa.js",
                     "~/Content/vendor/jqvmap/maps/continents/jquery.vmap.asia.js",
                     "~/Content/vendor/jqvmap/maps/continents/jquery.vmap.australia.js",
                     "~/Content/vendor/jqvmap/maps/continents/jquery.vmap.europe.js",
                     "~/Content/vendor/jqvmap/maps/continents/jquery.vmap.north-america.js",
                     "~/Content/vendor/jqvmap/maps/continents/jquery.vmap.australia.js",
                     "~/Content/javascripts/theme.js",/*Theme*/
                     "~/Content/javascripts/theme.custom.js",/*Theme costom*/
                     "~/Content/javascripts/theme.init.js",/*Theme Initialization Files*/
                     "~/Content/javascripts/dashboard/examples.dashboard.js",/*Exaple*/
                     "~/Content/javascripts/custom/pnotify/pnotify.js", /*notification*/
                     "~/Content/javascripts/custom/pnotify/PNotifyStyleMaterial.js", /*notification*/
                     "~/Content/javascripts/custom/pnotify/PNotifyButtons.js", /*notification*/
                     "~/Content/javascripts/custom/pnotify/PNotifyConfirm.js", /*notification*/
                     "~/Content/javascripts/custom/pnotify/PNotifyMobile.js", /*notification*/
                     "~/Content/javascripts/custom/jquery.validate.min.js",/*to prevent postback on submit button*/
                     "~/Content/javascripts/custom/Shared.js"/*common functions*/
                     //"~/Content/javascripts/jquery.userTimeout.js"/*common functions*/                     
                 ));

            BundleTable.EnableOptimizations = false;

        }
    }
}
