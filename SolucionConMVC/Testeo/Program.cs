using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadDeNegocio;
using System.Data.Entity;


namespace Testeo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese Titulo:");
            string TituloNuevo = Console.ReadLine();
            Console.WriteLine("Ingrese Costo:");
            double CostoNuevo = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ingrese Duracion en dias:");
            int DuracionNuevo = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ingrese Puntaje del 0 al 4:");
            int PuntajeNuevo = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ingrese Descripcion (Incluir Evaluadores e Integrantes):");
            string DescripcionNuevo = Console.ReadLine();
            Console.ReadKey();

            Emprendimiento e = new Emprendimiento { Titulo = TituloNuevo, Costo = CostoNuevo, Duracion = DuracionNuevo, Puntaje = PuntajeNuevo, Descripcion = DescripcionNuevo };
           
          using(  FinanciacionConText db = new FinanciacionConText()) {
            db.Emprendimientos.Add(e);
            db.SaveChanges();
            }
            
        }
    }
}
