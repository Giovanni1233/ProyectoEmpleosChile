using System.Web;
using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapjs").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/popper.js"));

            bundles.Add(new StyleBundle("~/bundles/bootstrapcss").Include(
                "~/Content/bootstrap.css"));

            bundles.Add(new ScriptBundle("~/bundles/libraryjs").Include(
                "~/Scripts/Owner/DatePicker.js",
                "~/Scripts/Owner/Owner.js",
                "~/Scripts/Owner/FuncionesAjax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/Scripts/jqueryui.js"));

            bundles.Add(new StyleBundle("~/bundles/jqueryuicss").Include(
                "~/Content/jqueryui.css"));

            bundles.Add(new StyleBundle("~/bundles/logincss").Include(
                "~/Content/App/estiloPublicaciones.css"));

            bundles.Add(new StyleBundle("~/bundles/appcss").Include(
                "~/Content/App/background-color.css",
                "~/Content/App/btn.css",
                "~/Content/App/estilosPlanes.css",
                "~/Content/App/estilosPlanes2.css",
                "~/Content/App/estilosError.css"));

          
        }
    }
}
