using BLL;
using Entidades;
using AnalisisApp.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnalisisApp.Registros
{
    public partial class rTipoAnalisis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            {
                if (!Page.IsPostBack)
                {
                    int id = Utils.ToInt(Request.QueryString["id"]);
                    if (id > 0)
                    {
                        RepositorioBase<TipoAnalisis> repositorio = new RepositorioBase<TipoAnalisis>();
                        var registro = repositorio.Buscar(id);

                        if (registro == null)
                        {
                            Utils.ShowToastr(this.Page, "Registro no existe", "Error", "error");
                        }
                        else
                        {
                            LlenaCampos(registro);
                        }
                    }
                }
            }
        }



        protected void Limpiar()
        {
            IdTextBox.Text = "0";
            DescripcionTextBox.Text = string.Empty;
            PrecioTextBox.Text = "0";
        }

        protected void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected TipoAnalisis LlenaClase(TipoAnalisis tipoAnalisis)
        {
            tipoAnalisis.TiposId = Utils.ToInt(IdTextBox.Text);
            tipoAnalisis.Descripcion = DescripcionTextBox.Text;
            tipoAnalisis.Precio = 0;
            return tipoAnalisis;
        }

        private void LlenaCampos(TipoAnalisis tipoAnalisis)
        {
            IdTextBox.Text = Convert.ToString(tipoAnalisis.TiposId);
            DescripcionTextBox.Text = tipoAnalisis.Descripcion;
            PrecioTextBox.Text = tipoAnalisis.Precio.ToString();
        }

        private bool ExistEnLaBaseDeDatos()
        {
            RepositorioBase<TipoAnalisis> repositorio = new RepositorioBase<TipoAnalisis>();
            TipoAnalisis tipoAnalisis = repositorio.Buscar(Utils.ToInt(IdTextBox.Text));
            return (tipoAnalisis != null);
        }

        protected void GuardarButton_Click1(object sender, EventArgs e)
        {
            RepositorioBase<TipoAnalisis> Repositorio = new RepositorioBase<TipoAnalisis>();
            TipoAnalisis tipoAnalisis = new TipoAnalisis();
            bool paso = false;

            tipoAnalisis = LlenaClase(tipoAnalisis);

            if (Utils.ToInt(IdTextBox.Text) == 0)
            {
                paso = Repositorio.Guardar(tipoAnalisis);
                Limpiar();
            }
            else
            {
                if (!ExistEnLaBaseDeDatos())
                {

                    Utils.ShowToastr(this, "No se pudo guardar", "Error", "error");
                    return;
                }
                paso = Repositorio.Modificar(tipoAnalisis);
                Limpiar();
            }

            if (paso)
            {
                Utils.ShowToastr(this, "Guardado", "Exito", "success");
                return;
            }
            else

                Utils.ShowToastr(this, "No se pudo guardar", "Error", "error");
        }



        protected void BuscarButton_Click1(object sender, EventArgs e)
        {
            RepositorioBase<TipoAnalisis> repositorio = new RepositorioBase<TipoAnalisis>();
            var tipo = repositorio.Buscar(Utils.ToInt(IdTextBox.Text));

            if (tipo != null)
            {
                Limpiar();
                LlenaCampos(tipo);
                Utils.ShowToastr(this, "Busqueda exitosa", "Exito", "success");
            }
            else
            {
                Utils.ShowToastr(this.Page, "El usuario que intenta buscar no existe", "Error", "error");
                Limpiar();
            }
        }

        protected void EliminarButton_Click1(object sender, EventArgs e)
        {
            if (Utils.ToInt(IdTextBox.Text) > 0)
            {
                int id = Convert.ToInt32(IdTextBox.Text);
                RepositorioBase<TipoAnalisis> repositorio = new RepositorioBase<TipoAnalisis>();
                if (repositorio.Eliminar(id))
                {

                    Utils.ShowToastr(this.Page, "Eliminado con exito!!", "Eliminado", "info");
                }
                else
                    Utils.ShowToastr(this.Page, "Fallo al Eliminar :(", "Error", "error");
                Limpiar();
            }
            else
            {
                Utils.ShowToastr(this.Page, "No se pudo eliminar, usuario no existe", "error", "error");
            }
        }

    }
}