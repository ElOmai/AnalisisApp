using BLL;
using Entidades;
using AnalisisApp.Utilitarios;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnalisisApp.Registros
{
    public partial class rAnalisis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TipoAnalisis tipoAnalisis = new TipoAnalisis();

            if (!Page.IsPostBack)
            {
                RepositorioBase<TipoAnalisis> repositorioBase = new RepositorioBase<TipoAnalisis>();

                TipoAnalisisDropDownList.DataSource = repositorioBase.GetList(t => true);
                TipoAnalisisDropDownList.DataValueField = "TiposId";
                TipoAnalisisDropDownList.DataTextField = "Descripcion";
                FechaTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                TipoAnalisisDropDownList.DataBind();

                LlenarDropDownListAnalisis();
                ViewState["Analisis"] = new Analisis();
                BindGrid();
            }
        }
        protected void BindGrid()
        {
            if (ViewState["Analisis"] != null)
            {
                DetalleGridView.DataSource = ((Analisis)ViewState["Analisis"]).Detalle;
                DetalleGridView.DataBind();
            }
        }

        public Analisis LlenarClase()
        {
            Analisis analisis = new Analisis();

            analisis = (Analisis)ViewState["Analisis"];
            TipoAnalisis a = new TipoAnalisis();
            analisis.AnalisisId = Utilitarios.Utils.ToInt(AnalisisIdTextBox.Text);
            analisis.AnalisisId = AnalisisIdTextBox.Text.ToInt();
            analisis.PacienteId = PacienteDropDownList.SelectedValue.ToInt();
            analisis.FechaRegistro = Utils.ToDateTime(FechaTextBox.Text);
            analisis.Monto = a.Precio;
            analisis.Balance = 0;

            return analisis;
        }

        public void LlenarCampos(Analisis analisis)
        {
            Limpiar();

            analisis.FechaRegistro = Utils.ToDateTime(FechaTextBox.Text);
            MontoTextBox.Text = analisis.Monto.ToString();
            BalanceTextBox.Text = analisis.Balance.ToString();
            ViewState["Analisis"] = new Analisis();
            this.BindGrid();
        }
        protected void Limpiar()
        {
            PacienteDropDownList.ClearSelection();
            TipoAnalisisDropDownList.ClearSelection();
            ResultadoTextBox.Text = string.Empty;
            MontoTextBox.Text = 0.ToString();
            BalanceTextBox.Text = 0.ToString();
            this.BindGrid();
        }

        protected void LimpiarButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void AgregarButton_Click1(object sender, EventArgs e)
        {
            Analisis analisis = new Analisis();
            string desc = TipoAnalisisDropDownList.Text;

            analisis = (Analisis)ViewState["Analisis"];
            analisis.AgregarDetalle(analisis.AnalisisId, ResultadoTextBox.Text, desc);

            ViewState["Analisis"] = analisis;

            this.BindGrid();
            MontoTextBox.Text = "";
        }

        protected void Grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Analisis Analisis = new Analisis();

            Analisis = (Analisis)ViewState["Analisis"];

            ViewState["Detalle"] = Analisis.Detalle;

            int Fila = e.RowIndex;

            Analisis.Detalle.RemoveAt(Fila);

            this.BindGrid();

            ResultadoTextBox.Text = string.Empty;
        }


        private void LlenarDropDownListAnalisis()
        {

            RepositorioBase<TipoAnalisis> repositorio = new RepositorioBase<TipoAnalisis>();
            RepositorioBase<Paciente> Paciente = new RepositorioBase<Paciente>();

            var list = new List<TipoAnalisis>();
            var lista = new List<Paciente>();
            list = repositorio.GetList(p => true);
            lista = Paciente.GetList(p => true);

            TipoAnalisisDropDownList.DataSource = list;
            TipoAnalisisDropDownList.DataTextField = "Descripcion";
            TipoAnalisisDropDownList.DataBind();

            PacienteDropDownList.DataSource = lista;
            PacienteDropDownList.DataTextField = "Nombres";
            PacienteDropDownList.DataBind();

        }
        private bool ExisteEnLaBaseDeDatos()
        {
            RepositorioBase<Analisis> Repositorio = new RepositorioBase<Analisis>();
            Analisis Tipos = Repositorio.Buscar(Utils.ToInt(AnalisisIdTextBox.Text));
            return (Tipos != null);
        }
        protected void GuardarButton_Click(object sender, EventArgs e)
        {
            Analisis Analisis = new Analisis();
            RepositorioBase<Analisis> Repositorio = new RepositorioBase<Analisis>();
            bool paso = false;

            Analisis = LlenarClase();

            if (Utils.ToInt(AnalisisIdTextBox.Text) == 0)
            {
                paso = Repositorio.Guardar(Analisis);
                Limpiar();
            }
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {

                    Utils.ShowToastr(this.Page, "No se pudo guardar!!", "Error", "error");
                    return;
                }
                paso = Repositorio.Modificar(Analisis);
                Limpiar();
            }

            if (paso)
            {
                Utils.ShowToastr(this.Page, "Guardado con exito!!", "Guardado", "success");
                return;
            }
            else
                Utils.ShowToastr(this.Page, "No se pudo guardar!!", "Error", "error");

            Limpiar();
        }

        protected void BuscarButton_Click1(object sender, EventArgs e)
        {
            RepositorioBase<Analisis> repositorio = new RepositorioBase<Analisis>();
            Analisis analisis = new Analisis();
            analisis = repositorio.Buscar(Utils.ToInt(AnalisisIdTextBox.Text));

            if (analisis != null)
                LlenarCampos(analisis);

            else
            {
                Utils.ShowToastr(this.Page, "El analisis que intenta buscar no existe", "Error", "error");
                Limpiar();
            }
        }

        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Analisis> Repositorio = new RepositorioBase<Analisis>();

            var Analisis = Repositorio.Buscar(Utils.ToInt(AnalisisIdTextBox.Text));

            if (Analisis != null)
            {
                Repositorio.Eliminar(Utils.ToInt(AnalisisIdTextBox.Text));
                Utils.ShowToastr(this.Page, "Eliminado con exito!!", "Error", "success");
            }
            else
                Utils.ShowToastr(this.Page, "No se pudo eliminar!!", "Error", "error");
            Limpiar();
        }
    }
}