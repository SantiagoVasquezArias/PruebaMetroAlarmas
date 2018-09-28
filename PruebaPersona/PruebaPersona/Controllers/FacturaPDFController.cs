using PruebaPersonaDao;
using System.Linq;
using System.Web.Mvc;
using Rotativa;

namespace PruebaPersona.Controllers
{
    public class FacturaPDFController : Controller
    {
        PruebaPersonaEntities context;

        public FacturaPDFController()
        {
            context = new PruebaPersonaEntities();
        }

        //public ActionResult GetAll()
        //{
        //    var recibo = context.Factura.ToList();
        //    return View(recibo);
        //}

        //public ActionResult imprimir()
        //{
        //    var pdf = new Rotativa.ActionAsPdf("GetAll");
        //    return pdf;
        //}
    }
}