using System.Web;
using System.Web.Optimization;

namespace DataAnalysis.Web.Admin
{
    public static class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/prereq").Include(
                "~/Scripts/libraries/jquery-{version}.js",
                "~/Scripts/libraries/bootstrap.js",
                "~/Scripts/libraries/respond.js",
                "~/Scripts/libraries/radiocheck.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularjs").Include(
                "~/Scripts/libraries/ng/angular.js",
                "~/Scripts/libraries/ng/angular-resource.js",
                "~/Scripts/libraries/ng/angular-ui-router.js",
                "~/Scripts/libraries/ng/loading-bar.js",
                "~/Scripts/libraries/ng/ui-bootstrap-tpls-0.12.0.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                // App
                "~/app/app.js",
                // Controllers
                "~/app/projects/js/projectListController.js",
                "~/app/projects/js/projectUpsertController.js",
                "~/app/projects/js/projectViewController.js",
                "~/app/dataSource/js/dataSourceUpsertController.js",
                "~/app/dataSource/js/connectionUpsertController.js",
                "~/app/dataSource/js/dataSourceTablesUpsertController.js",
                // Services
                "~/app/_common/services/projectService.js",
                "~/app/_common/services/datasourceService.js",
                // Utils
                "~/app/_common/filters.js",
                "~/app/_common/directives.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/flat-ui.css",
                "~/Content/loading-bar.css",
                "~/Content/Site.css"));

#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}
