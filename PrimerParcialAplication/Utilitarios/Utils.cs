using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace PrimerParcialAplication.Utilitarios
{
    public static class Utils
    {
        public static int ToInt(string valor)
        {
            int retorno = 0;
            int.TryParse(valor, out retorno);

            return retorno;
        }      
        
        public static int ToIntObjetos(object valor)
        {
            int retorno = 0;
            int.TryParse(valor.ToString(), out retorno);

            return retorno;
        }

        public static decimal ToDecimal(string valor)
        {
            decimal retorno = 0;
            decimal.TryParse(valor, out retorno);

            return retorno;
        }

        public static decimal ToDecimalO(object valor)
        {
            decimal retorno = 0;
            decimal.TryParse(valor.ToString(), out retorno);

            return retorno;
        }

        public static double ToDouble(string valor)
        {
            double retorno = 0;
            double.TryParse(valor, out retorno);

            return retorno;
        }

        public static double ToDoubleObjetos(object valor)
        {
            double retorno = 0;
            double.TryParse(valor.ToString(), out retorno);

            return Convert.ToDouble(retorno);
        }
        
        public static DateTime ToDateTime(string valor)
        {
            DateTime retorno = DateTime.Now;
            DateTime.TryParse(valor, out retorno);

            return retorno;
        }

        public static void ShowToastr(this Page page, string message, string title, string type = "info")
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "toastr_message",
                  String.Format("toastr.{0}('{1}', '{2}');", type.ToLower(), message, title), addScriptTags: true);
        }

        public static void MostrarMensaje(this Page page, string message, string title, string type = "info")
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "toastr_message",
                 String.Format("toastr.{0}('{1}', '{2}');", type.ToLower(), message, title), addScriptTags: true);
        }
        

        //public static List<TipoAnalisis> FiltrarTipoAnalisis(int index, string criterio, DateTime desde, DateTime hasta)
        //{
        //    Expression<Func<TipoAnalisis, bool>> filtro = p => true;
        //    Repositorio<TipoAnalisis> repositorio = new Repositorio<TipoAnalisis>();
        //    List<TipoAnalisis> list = new List<TipoAnalisis>();

        //    int id = ToInt(criterio);
        //    switch (index)
        //    {
        //        case 0://Todo
        //            break;

        //        case 1://FacturaId
        //            filtro = p => p.TipoId == id;
        //            break;

        //        case 2://ClienteId
        //            filtro = p => p.Descripcion.Contains(criterio);
        //            break;
        //    }

        //    list = repositorio.GetList(filtro);

        //    return list;
        //}

    }
}