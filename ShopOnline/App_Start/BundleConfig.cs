using System.Web;
using System.Web.Optimization;

namespace ShopOnline
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/AdminJsCore").Include(
                      "~/Assets/admin/vendor/jquery/jquery.js",
                      "~/Scripts/jquery.unobtrusive-ajax.min.js",
                      "~/Assets/admin/vendor/bootstrap/js/bootstrap.min.js",
                      "~/Assets/admin/vendor/metisMenu/metisMenu.min.js",
                      "~/Assets/admin/js/plugin/ckfinder/ckfinder.js",
                      "~/Assets/admin/js/plugin/ckeditor/ckeditor.js",
                      "~/Assets/admin/vendor/raphael/raphael.min.js",
                      "~/Assets/admin/vendor/morrisjs/morris.min.js",
                      "~/Assets/admin/dist/js/sb-admin-2.js",
                      "~/Assets/admin/dist/js/Alert-admin.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/ClientJsCore").Include(
                "~/Assets/client/js/jquery-3.3.1.min.js",
                "~/Assets/client/js/bootstrap.min.js",
                "~/Assets/client/js/popper.min.js",
                "~/Assets/client/js/jquery.auto-complete.min.js",
                "~/Assets/client/js/move-top.js",
                "~/Assets/client/js/easing.js",
                "~/Assets/client/js/startstop-slider.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/ClientJsController").Include(
                "~/Assets/client/js/controller/baseController.js"
                ));

            bundles.Add(new StyleBundle("~/bundles/AdminCssCore")
                      .Include("~/Assets/admin/vendor/bootstrap/css/bootstrap.min.css", new CssRewriteUrlTransform())
                      .Include("~/Assets/admin/vendor/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform())
                      .Include("~/Assets/admin/vendor/metisMenu/metisMenu.min.css", new CssRewriteUrlTransform())
                      .Include("~/Assets/admin/dist/css/sb-admin-2.css", new CssRewriteUrlTransform())
                      .Include("~/Assets/admin/vendor/morrisjs/morris.css", new CssRewriteUrlTransform())
                      );

            bundles.Add(new StyleBundle("~/bundles/ClientCssCore")
                .Include("~/Assets/client/css/bootstrap.css", new CssRewriteUrlTransform())
                .Include("~/Assets/client/css/bootstrap-social.css", new CssRewriteUrlTransform())
                .Include("~/Assets/client/css/all.css", new CssRewriteUrlTransform())
                .Include("~/Assets/client/css/font-awesome.min.css", new CssRewriteUrlTransform())
                .Include("~/Assets/client/css/style.css", new CssRewriteUrlTransform())
                .Include("~/Assets/client/css/slider.css", new CssRewriteUrlTransform())
                .Include("~/Assets/client/css/jquery.auto-complete.css", new CssRewriteUrlTransform())
                );

            BundleTable.EnableOptimizations = true;
        }
    }
}
