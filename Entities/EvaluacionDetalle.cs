using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Serializable]
    public class EvaluacionDetalle
    {
        [Key]
        public int Id { get; set; }
        public int EvaluacionId { get; set; }
        public string Categoria { get; set; }
        public decimal Valor { get; set; }
        public decimal Logrado { get; set; }
        public decimal Perdido { get; set; }

        public EvaluacionDetalle()
        {
            Id = 0;
            EvaluacionId = 0;
            Categoria = string.Empty;
            Valor = 0;
            Logrado = 0;
            Perdido = 0;
        }

        public EvaluacionDetalle(int id, int evaluacionId, string categoria, decimal valor, decimal logrado, decimal perdido)
        {
            Id = id;
            EvaluacionId = evaluacionId;
            Categoria = categoria;
            Valor = valor;
            Logrado = logrado;
            Perdido = perdido;
        }
    }
}
