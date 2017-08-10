using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EntidadDeNegocio;
using System.IO;

namespace ProFinanciacion.Controllers
{
    public class EmprendimientosController : Controller
    {
        private FinanciacionConText db = new FinanciacionConText();

        

        // GET: Emprendimientos
        public ActionResult Index()
        {
            return View(db.Emprendimientos.ToList());
        }

        // GET: Emprendimientos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprendimiento emprendimiento = db.Emprendimientos.Find(id);
            if (emprendimiento == null)
            {
                return HttpNotFound();
            }
            return View(emprendimiento);
        }

        // GET: Emprendimientos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Emprendimientos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodigoEmp_id,Titulo,Costo,Duracion,Puntaje,Descripcion")] Emprendimiento emprendimiento)
        {
            if (ModelState.IsValid)
            {
                db.Emprendimientos.Add(emprendimiento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(emprendimiento);
        }

        // GET: Emprendimientos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprendimiento emprendimiento = db.Emprendimientos.Find(id);
            if (emprendimiento == null)
            {
                return HttpNotFound();
            }
            return View(emprendimiento);
        }

        // POST: Emprendimientos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodigoEmp_id,Titulo,Costo,Duracion,Puntaje,Descripcion")] Emprendimiento emprendimiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emprendimiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(emprendimiento);
        }

        // GET: Emprendimientos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprendimiento emprendimiento = db.Emprendimientos.Find(id);
            if (emprendimiento == null)
            {
                return HttpNotFound();
            }
            return View(emprendimiento);
        }

        // POST: Emprendimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Emprendimiento emprendimiento = db.Emprendimientos.Find(id);
            db.Emprendimientos.Remove(emprendimiento);
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

        public ActionResult ListadoPorcentaje()
        {          
            List<Emprendimiento> emp = Emprendimiento.buscarEmprenidmientos();
            return View(emp.ToList());
        }

        public ActionResult cargarArchivo()
        {
            string line;
            StreamReader file = new StreamReader(@"\solucionFinanciacion\EntidadDeNegocio\archivostexto\PRO3TEXTOEMPRENDIMIENTOS.txt");
            while ((line = file.ReadLine()) != null)
            {
                Char delimiter = '#';
                String[] substrings = line.Split(delimiter);
               
                    double numero = 0;
                    double.TryParse(substrings[1], out numero);

                    int numero1 = 0;
                    int.TryParse(substrings[2], out numero1);

                    int numero2 = 0;
                    int.TryParse(substrings[3], out numero2);

                    Emprendimiento empre = new Emprendimiento
                    {
                        Titulo = substrings[0],
                        Costo = numero,
                        Duracion = numero1,
                        Puntaje = numero2,
                        Descripcion = substrings[4]

                    };
                    db.Emprendimientos.Add(empre);
            }
            file.Close();
            db.SaveChanges();
            List<Emprendimiento> emp = Emprendimiento.buscarEmprenidmientos();
            return View(emp.ToList());
        }
        [HttpGet]
        public ActionResult empMontoDuracion()
        {

            return View();
        }
        
        [HttpPost]
        public ActionResult empMontoDuracion(double monto1, double monto2, int duracion)
        {
            List<Emprendimiento> emp = new List<Emprendimiento>();
            if (monto1>=0 && monto2>=0 && duracion>=0) {
                emp = Emprendimiento.buscarEmprendimientosFiltrados(monto1, monto2, duracion);
                return View("ListarVista",emp);      
            }
            return View();
        }

        
        public ActionResult ListarVista(List<Emprendimiento> emp)
        {
            List<Emprendimiento> aux = emp;
            if (emp != null) {
                
                aux = emp;
            }
            else
            {
                aux = new List<Emprendimiento>();
            }
            return View(aux.ToList());     
        }

        public ActionResult Financiar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprendimiento emprendimiento = db.Emprendimientos.Find(id);
            if (emprendimiento == null)
            {
                return HttpNotFound();
            }
            return View(emprendimiento);
        }
        [HttpPost, ActionName("Financiar")]
        [ValidateAntiForgeryToken]
        public ActionResult Financiar(int id)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Index");
            }

            string email = Session["Email"].ToString();
            List<Financiador> f = Financiador.buscarPorFinaciador(email);
            Emprendimiento empre = db.Emprendimientos.Find(id);
            if (empre != null )
            {
                foreach(Financiador j in f)
                {
                    double saldo = 0;
                    saldo=Convert.ToDouble(j.Monto);

                    if (saldo> empre.Costo)
                    {
                        j.Financiados = new List<Emprendimiento>();
                        j.Financiados.Add(empre);
                        db.SaveChanges();
                    }
                    
                }
            }
            return View();       
        }

    }
    
}
