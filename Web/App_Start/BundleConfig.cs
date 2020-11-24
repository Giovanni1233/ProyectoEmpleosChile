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

            bundles.Add(new ScriptBundle("~/bundles/appjs").Include(
                "~/MaterialDesign/js/popper.min.js",
                "~/MaterialDesign/js/mdb.min.js"));

            bundles.Add(new StyleBundle("~/bundles/jqueryuicss").Include(
                "~/Content/jqueryui.css"));

            bundles.Add(new StyleBundle("~/bundles/logincss").Include(
                "~/Content/App/estiloPublicaciones.css",
                "~/Content/Owner/EstiloInicio.css",
                "~/Content/Owner/LoginStyle.css"));

            bundles.Add(new StyleBundle("~/bundles/appcss").Include(
                "~/Content/App/background-color.css",
                "~/Content/App/btn.css",
                "~/Content/App/estilosPlanes.css",
                "~/Content/App/estilosPlanes2.css",
                "~/Content/App/estilosError.css",
                "~/Content/Owner/bootstrap.css",
                "~/Content/App/background-size.css",
                "~/Content/App/barcodes.css",
                "~/Content/App/color.css",
                "~/Content/App/cursor.css",
                "~/Content/App/display.css",
                "~/Content/App/font-size.css",
                "~/Content/App/form-tw.css",
                "~/Content/App/generic-tw.css",
                "~/Content/App/glyphicon-tw.css",
                "~/Content/App/height.css",
                "~/Content/App/loader.css",
                "~/Content/App/margin.css",
                "~/Content/App/menu.css",
                "~/Content/App/padding.css",
                "~/Content/App/paginator.css",
                "~/Content/App/teamwork-auth.css",
                "~/Content/App/teamwork-files.css",
                "~/Content/App/text-decoration.css",
                "~/Content/App/width.css"
                ));

            bundles.Add(new StyleBundle("~/bundles/MaterialDesign").Include(
                "~/MaterialDesign/css/mdb.min.css"
                ));
        }
    }
}
