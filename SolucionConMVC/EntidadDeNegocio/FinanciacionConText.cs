using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace EntidadDeNegocio
{
    public class FinanciacionConText:DbContext
    {
        public DbSet<Emprendimiento> Emprendimientos { get; set; }
        public DbSet<Financiador> Financiadores { get; set; }
        public FinanciacionConText():base("con")
        {

        }
    }
}
