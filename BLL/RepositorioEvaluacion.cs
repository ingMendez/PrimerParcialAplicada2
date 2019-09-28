using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RepositorioEvaluacion : Repositorio<Evaluacion>
    {
        public override Evaluacion Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Evaluacion evaluacion = new Evaluacion();
            try
            {
                evaluacion = contexto.Evaluacion.Find(id);

                if (evaluacion != null)
                {
                    evaluacion.Detalle.Count();

                    foreach (var item in evaluacion.Detalle)
                    {
                        string s = item.EvaluacionId.ToString();
                        string ss = item.Categoria.ToString();
                    }
                }
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return evaluacion;
        }

        public string Estado(int num)
        {
            string estado = string.Empty;
            if (num > 69)
                estado = "Aprobado";
            else
                estado = "Reprobado";

            return estado;
        }

        public decimal Perdidos(decimal valor, decimal logrado)
        {
            decimal resultado = 0;
            resultado = valor - logrado;

            return resultado;
        }

    }
}
