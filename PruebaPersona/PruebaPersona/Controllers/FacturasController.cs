using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PruebaPersonaDao;
using Rotativa;

namespace PruebaPersona.Controllers
{
    [Authorize]
    public class FacturasController : Controller
    {
        private PruebaPersonaEntities db = new PruebaPersonaEntities();

        // GET: Facturas
        public ActionResult Index(String busqueda)
        {
            if (busqueda != null)
            {
                return View(db.Factura.Where(f => f.Cliente.Cédula.Contains(busqueda) || f.Banco.Nombre.Contains(busqueda) ||
                f.Ciudad.Nombre.Contains(busqueda) || f.Cliente.Nombre.Contains(busqueda)).ToList());

            }
            else

                return View(db.Factura.ToList());
            
            //var factura = db.Factura.Include(f => f.Banco).Include(f => f.Ciudad).Include(f => f.Cliente);
          }

        //Imprimir Funciona pero en el index 

        //public ActionResult GetAll()
        //{
        //    var recibo = db.Factura.ToList();
        //    return View(recibo);
        //}

        //public ActionResult imprimir()
        //{          
        //    var pdf = new Rotativa.ActionAsPdf("Details");
        //    return pdf;
        //}


        public ActionResult PrintPartialViewToPdf(int? id)
        {
            //using (DbPrintPDFEntities db = new DbPrintPDFEntities())

            Factura customer = db.Factura.FirstOrDefault(c => c.Id == id);

            var report = new PartialViewAsPdf("~/Views/Facturas/Details.cshtml", customer);
            return report;

        }

              
        // GET: Facturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // GET: Facturas/Create
       
        public ActionResult Create()
        {
            ViewBag.IdBanco = new SelectList(db.Banco, "Id", "Nombre");
            ViewBag.IdCiudad = new SelectList(db.Ciudad, "Id", "Nombre");
            ViewBag.IdCliente = new SelectList(db.Cliente, "Id", "Cédula");
            return View();
        }

        // POST: Facturas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdCiudad,IdCliente,IdBanco,Valor,Fecha")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                db.Factura.Add(factura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdBanco = new SelectList(db.Banco, "Id", "Nombre", factura.IdBanco);
            ViewBag.IdCiudad = new SelectList(db.Ciudad, "Id", "Nombre", factura.IdCiudad);
            ViewBag.IdCliente = new SelectList(db.Cliente, "Id", "Cédula", factura.IdCliente);
            return View(factura);
        }


        // GET: Facturas/Edit/5

        [Authorize(Roles = "JefeFacturacion")]

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdBanco = new SelectList(db.Banco, "Id", "Nombre", factura.IdBanco);
            ViewBag.IdCiudad = new SelectList(db.Ciudad, "Id", "Nombre", factura.IdCiudad);
            ViewBag.IdCliente = new SelectList(db.Cliente, "Id", "Cédula", factura.IdCliente);
            return View(factura);
        }

        // POST: Facturas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "JefeFacturacion")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdCiudad,IdCliente,IdBanco,Valor,Fecha")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(factura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdBanco = new SelectList(db.Banco, "Id", "Nombre", factura.IdBanco);
            ViewBag.IdCiudad = new SelectList(db.Ciudad, "Id", "Nombre", factura.IdCiudad);
            ViewBag.IdCliente = new SelectList(db.Cliente, "Id", "Cédula", factura.IdCliente);
            return View(factura);
        }

        // GET: Facturas/Delete/5

        [Authorize(Roles = "JefeFacturacion")]

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // POST: Facturas/Delete/5

        [Authorize(Roles = "JefeFacturacion")]

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Factura factura = db.Factura.Find(id);
            db.Factura.Remove(factura);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
