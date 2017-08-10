using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EntidadDeNegocio
{
    public class Financiador
    {
        [Key]
        public int Fin_id { get; set; }

        [Required(ErrorMessage = "El Email Obligatorio")]
        [DataType(DataType.EmailAddress, ErrorMessage = "El Email es incorrecto")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "El Pass es Obligatorio")]
        [DisplayName("Password")]
        [PasswordPropertyText]
        public string Pass { get; set; }

        [Required(ErrorMessage = "El Nombre de la Organisacion es Obligatorio")]
        [DisplayName("Nombre de Organisacion")]
        public string NombreOrg { get; set; }

        [Required(ErrorMessage = "El Monto para Financiar es Obligatorio")]
        [DisplayName("Monto Disponible para Financiar")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "El Monto puede ser solo numeros.")]
        public decimal Monto { get; set; }
        public List<Emprendimiento> Financiados { get; set; }

        
        public static List<Financiador> buscarPorFinaciador(string email)
        {
            using (FinanciacionConText db = new FinanciacionConText())
            {
                var queryEmp = from h in db.Financiadores where h.Email == email select h;
                return queryEmp.ToList();
            }

        }

        public static Financiador login(string email, string pass)
        {
            using (FinanciacionConText db = new FinanciacionConText())
            {
                Financiador f = null;
                var queryEmp = from h in db.Financiadores where h.Email == email && h.Pass == pass select h;
                queryEmp.ToList();
                foreach(Financiador j in queryEmp)
                {
                    f = j;              
                }
                return f;
            }
        }
    }
}