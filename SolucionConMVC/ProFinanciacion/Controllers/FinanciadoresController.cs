using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EntidadDeNegocio;

namespace ProFinanciacion.Controllers
{
    public class FinanciadoresController : Controller
    {
        private FinanciacionConText db = new FinanciacionConText();

        // GET: Financiadores
        public ActionResult Index()
        {
            return View(db.Financiadores.ToList());
        }

        // GET: Financiadores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Financiador financiador = db.Financiadores.Find(id);
            if (financiador == null)
            {
                return HttpNotFound();
            }
            return View(financiador);
        }

        // GET: Financiadores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Financiadores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Fin_id,Email,Pass,NombreOrg,Monto")] Financiador financiador)
        {
            if (ModelState.IsValid)
            {
                db.Financiadores.Add(financiador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(financiador);
        }

        // GET: Financiadores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Financiador financiador = db.Financiadores.Find(id);
            if (financiador == null)
            {
                return HttpNotFound();
            }
            return View(financiador);
        }

        // POST: Financiadores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Fin_id,Email,Pass,NombreOrg,Monto")] Financiador financiador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(financiador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(financiador);
        }

        // GET: Financiadores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Financiador financiador = db.Financiadores.Find(id);
            if (financiador == null)
            {
                return HttpNotFound();
            }
            return View(financiador);
        }

        // POST: Financiadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Financiador financiador = db.Financiadores.Find(id);
            db.Financiadores.Remove(financiador);
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

        public ActionResult ListarEmprendimientos()
        {

            if (Session["Email"] == null)
            {
                return RedirectToAction("Index");
            }

            string email = Session["Email"].ToString();
            List<Financiador> f = Financiador.buscarPorFinaciador(email);
            List<Emprendimiento> e = new List<Emprendimiento>();
            foreach (Financiador j in f)
            {
                e = j.Financiados;
            }

            return View(e.ToList());

        }

        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(string email, string pass)
        {
            Session["Email"] = null;
            Financiador f = Financiador.login(email, pass);
            if (f != null)
            {
                Session["Email"] = f.Email;
                return RedirectToAction("Index");
            }
            else
                return View(f);
        }

        
    }
}
