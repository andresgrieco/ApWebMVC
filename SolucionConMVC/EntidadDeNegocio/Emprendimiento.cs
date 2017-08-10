using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace EntidadDeNegocio
{
    public class Emprendimiento
    {

        [Key]
        public int CodigoEmp_id { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public double Costo { get; set; }
        [Required]
        public int Duracion { get; set; }
        [Required]
        public int Puntaje { get; set; }
        [Required]
        public string Descripcion { get; set; }


        public static List<Emprendimiento> buscarEmprenidmientos()
        {
            using (FinanciacionConText db = new FinanciacionConText())
            {

                var queryEmpCant = db.Emprendimientos.Count();

                int cant = Convert.ToInt32(queryEmpCant * 0.80);

                var orden = db.Emprendimientos.OrderByDescending(e => e.Puntaje);

                var ListadoFin = orden.Take(cant);

                return ListadoFin.ToList();
            }

        }

        public static List<Emprendimiento> buscarEmprendimientosFiltrados(double monto1, double monto2, int duracion)
        {
            using (FinanciacionConText db = new FinanciacionConText()){

                var queryEmp = db.Emprendimientos.Where(e => e.Costo > monto1 && e.Costo < monto2 && e.Duracion < duracion).OrderBy(e => e.Costo);
                return queryEmp.ToList();
            }
        }

       



    }
}
