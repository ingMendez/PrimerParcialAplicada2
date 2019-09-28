using BLL;
using Entities;
using PrimerParcialAplication.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrimerParcialAplication.Registros
{
    public partial class rEvaluacion : System.Web.UI.Page
    {
        Evaluacion evalu = new Evaluacion();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FechaTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                ViewState["Detalle"] = new Evaluacion().Detalle;
            }
        }

        public Evaluacion LlenarClase()
        {
            Evaluacion evaluacion = new Evaluacion();

            evaluacion.EvaluacionId = Utils.ToInt(IdTextBox.Text);
            bool resultado = DateTime.TryParse(FechaTextBox.Text, out DateTime fecha);
            evaluacion.Fecha = fecha;
            evaluacion.NombreEstudiante = EstudianteTextBox.Text;
            evaluacion.TotalLogrados = Utils.ToInt(TotalLogradosTextBox.Text);
            evaluacion.TotalPerdidos = Utils.ToInt(TotalPerdidosTextBox.Text);
            evaluacion.Estado = EstadoTextBox.Text;
            evaluacion.Detalle = (List<EvaluacionDetalle>)ViewState["Detalle"];

            return evaluacion;
        }

        public void LlenarCampos(Evaluacion evaluacion)
        {
            List<EvaluacionDetalle> detalles = Utils.ListaDetalle(Utils.ToInt(IdTextBox.Text));
            ViewState["Detalle"] = detalles;
            FechaTextBox.Text = evaluacion.Fecha.ToString("yyyy-MM-dd");
            EstudianteTextBox.Text = evaluacion.NombreEstudiante;
            detalleGridView.DataSource = ViewState["Detalle"];
            detalleGridView.DataBind();
            TotalLogradosTextBox.Text = evaluacion.TotalLogrados.ToString();
            TotalPerdidosTextBox.Text = evaluacion.TotalPerdidos.ToString();
            EstadoTextBox.Text = evaluacion.Estado;
        }

        protected void Limpiar()
        {
            IdTextBox.Text = "0";
            FechaTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
            EstudianteTextBox.Text = "";
            CategoriaTextBox.Text = "";
            ValorTextBox.Text = "";
            LogradoTextBox.Text = "";
            PerdidoTextBox.Text = "";
            detalleGridView.DataSource = null;
            detalleGridView.DataBind();
            TotalLogradosTextBox.Text = "";
            TotalPerdidosTextBox.Text = "";
            EstadoTextBox.Text = "";
        }

        private bool ValidarAgregar()
        {
            bool estato = false;

            if (String.IsNullOrWhiteSpace(EstudianteTextBox.Text))
            {
                Utils.ShowToastr(this, "Debe llenar", "Error", "error");
                estato = true;
            }
            if (String.IsNullOrWhiteSpace(CategoriaTextBox.Text))
            {
                Utils.ShowToastr(this, "Debe llenar", "Error", "error");
                estato = true;
            }
            if (String.IsNullOrWhiteSpace(ValorTextBox.Text))
            {
                Utils.ShowToastr(this, "Debe tener un Id para guardar", "Error", "error");
                estato = true;
            }
            if (String.IsNullOrWhiteSpace(LogradoTextBox.Text))
            {
                Utils.ShowToastr(this, "Debe tener un Id para guardar", "Error", "error");
                estato = true;
            }
            if (String.IsNullOrWhiteSpace(PerdidoTextBox.Text))
            {
                Utils.ShowToastr(this, "Debe tener un Id para guardar", "Error", "error");
                estato = true;
            }
            return estato;
        }

        private bool Validar()
        {
            bool estato = false;

            if (detalleGridView.Rows.Count == 0)
            {
                Utils.ShowToastr(this, "Debe agregar.", "Error", "error");
                estato = true;
            }
            if (String.IsNullOrWhiteSpace(IdTextBox.Text))
            {
                Utils.ShowToastr(this, "Debe tener un Id para guardar", "Error", "error");
                estato = true;
            }
            return estato;
        }

        private void LlenaTotal()
        {
            decimal total = 0;
            decimal perdidos = 0;
            List<EvaluacionDetalle> lista = (List<EvaluacionDetalle>)ViewState["Detalle"];
            foreach (var item in lista)
            {
                total += item.Logrado;
                perdidos += item.Perdido;
            }

            TotalLogradosTextBox.Text = total.ToString();
            TotalPerdidosTextBox.Text = perdidos.ToString();
        }

        protected void agregarLinkButton_Click(object sender, EventArgs e)
        {
            List<EvaluacionDetalle> detalles = new List<EvaluacionDetalle>();
            if (IsValid)
            {
                DateTime date = DateTime.Now.Date;

                string estudiante = EstudianteTextBox.Text;
                string categoria = CategoriaTextBox.Text;
                decimal valor = Utils.ToDecimal(ValorTextBox.Text);
                decimal logrado = Utils.ToDecimal(LogradoTextBox.Text);
                decimal perdido = Utils.ToDecimal(PerdidoTextBox.Text);
                if (ValidarAgregar())
                {
                    return;
                }
                else
                {
                    if (detalleGridView.Rows.Count != 0)
                    {
                        evalu.Detalle = (List<EvaluacionDetalle>)ViewState["Detalle"];
                    }

                    EvaluacionDetalle detalle = new EvaluacionDetalle();
                    evalu.Detalle.Add(new EvaluacionDetalle(0, detalle.EvaluacionId, categoria, valor, logrado, perdido));

                    ViewState["Detalle"] = evalu.Detalle;
                    detalleGridView.DataSource = ViewState["Detalle"];
                    detalleGridView.DataBind();
                    LlenaTotal();
                    CategoriaTextBox.Text = "";
                    ValorTextBox.Text = "";
                    LogradoTextBox.Text = "";
                    PerdidoTextBox.Text = "";
                }

                decimal total = 0;
                total = Utils.ToDecimal(TotalLogradosTextBox.Text);
                if (total > 69)
                    EstadoTextBox.Text = "Aprobado";
                else
                    EstadoTextBox.Text = "Reprobado";
            }
        }

        protected void BuscarLinkButton_Click(object sender, EventArgs e)
        {
            RepositorioEvaluacion repositorio = new RepositorioEvaluacion();

            var evaluacion = repositorio.Buscar(Utils.ToInt(IdTextBox.Text));
            if (evaluacion != null)
            {
                Utils.ShowToastr(this, "Busqueda exitosa", "Exito", "success");
                LlenarCampos(evaluacion);
            }
            else
            {
                Limpiar();
                Utils.ShowToastr(this, "No existe la Factura especificada", "Error", "error");
            }
        }

        protected void nuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void guadarButton_Click(object sender, EventArgs e)
        {
            RepositorioEvaluacion repositorio = new RepositorioEvaluacion();
            bool estado = false;
            Evaluacion evaluacion = new Evaluacion();

            if (Validar())
            {
                return;
            }
            else
            {
                evaluacion = LlenarClase();

                if (Convert.ToInt32(IdTextBox.Text) == 0)
                {
                    estado = repositorio.Guardar(evaluacion);
                    Utils.ShowToastr(this, "Guardado", "Exito", "success");
                    Limpiar();
                }
                else
                {
                    RepositorioEvaluacion repo = new RepositorioEvaluacion();
                    //Repositorio<Analisis> repo = new Repositorio<Analisis>();
                    int id = Convert.ToInt32(IdTextBox.Text);
                    Evaluacion anali = new Evaluacion();
                    anali = repo.Buscar(id);

                    if (anali != null)
                    {
                        estado = repo.Modificar(LlenarClase());
                        Utils.ShowToastr(this, "Modificado", "Exito", "success");
                    }
                    else
                        Utils.ShowToastr(this, "Id no existe", "Error", "error");
                }

                if (estado)
                {
                    Limpiar();
                }
                else
                    Utils.ShowToastr(this, "No se pudo guardar", "Error", "error");
            }
        }

        protected void eliminarButton_Click(object sender, EventArgs e)
        {
            RepositorioEvaluacion repositorio = new RepositorioEvaluacion();
            int id = Utils.ToInt(IdTextBox.Text);

            var evaluacion = repositorio.Buscar(id);

            if (evaluacion != null)
            {
                if (repositorio.Eliminar(id))
                {
                    Utils.ShowToastr(this, "Eliminado", "Exito", "success");
                    Limpiar();
                }
                else
                    Utils.ShowToastr(this, "No se pudo eliminar", "Error", "error");
            }
            else
                Utils.ShowToastr(this, "No existe", "Error", "error");
        }

        protected void TotalTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        protected void detalleGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                ((List<EvaluacionDetalle>)ViewState["Detalle"]).RemoveAt(index);
                detalleGridView.DataSource = ViewState["Detalle"];
                detalleGridView.DataBind();
                LlenaTotal();
            }
        }

        protected void detalleGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            detalleGridView.DataSource = ViewState["Detalle"];
            detalleGridView.PageIndex = e.NewPageIndex;
            detalleGridView.DataBind();
        }

        protected void LogradoTextBox_TextChanged(object sender, EventArgs e)
        {
            RepositorioEvaluacion repositorio = new RepositorioEvaluacion();
            decimal Valor = Utils.ToDecimal(ValorTextBox.Text);
            decimal Logrado = Utils.ToDecimal(LogradoTextBox.Text);

            decimal Perdido = repositorio.Perdidos(Valor, Logrado);
            PerdidoTextBox.Text = Perdido.ToString();
        }
    }
}