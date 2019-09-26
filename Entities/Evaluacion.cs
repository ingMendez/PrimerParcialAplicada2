using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Serializable]
    public class Evaluacion
    {
        [Key]
        public int EvaluacionId { get; set; }
        public DateTime Fecha { get; set; }
        public string NombreEstudiante { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
        public List<EvaluacionDetalle> Detalle { get; set; }

        public Evaluacion()
        {
            EvaluacionId = 0;
            Fecha = DateTime.Now;
            NombreEstudiante = string.Empty;
            Total = 0;
            Estado = string.Empty;
            this.Detalle = new List<EvaluacionDetalle>();
        }

        public void AgregarDetalle(int Id, int EvaluacionId, string Categoria, decimal Valor, decimal Logrado, decimal Perdido)
        {
            this.Detalle.Add(new EvaluacionDetalle(Id, EvaluacionId, Categoria, Valor, Logrado, Perdido));
        }
    }
}
