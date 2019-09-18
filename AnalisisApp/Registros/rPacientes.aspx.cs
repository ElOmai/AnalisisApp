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
    public partial class rPacientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            {
                if (!Page.IsPostBack)
                {
                    int id = Utils.ToInt(Request.QueryString["id"]);
                    if (id > 0)
                    {
                        RepositorioBase<Paciente> repositorio = new RepositorioBase<Paciente>();
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
            NombresTextBox.Text = string.Empty;
            DireccionTextBox.Text = string.Empty;
            TelefonoTextBox.Text = string.Empty;
        }

        protected void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected Paciente LlenaClase(Paciente Paciente)
        {
            Paciente.PacienteId = Utils.ToInt(IdTextBox.Text);
            Paciente.Nombres = NombresTextBox.Text;
            Paciente.Direccion = DireccionTextBox.Text;
            Paciente.Telefono = TelefonoTextBox.Text;

            return Paciente;
        }

        private void LlenaCampos(Paciente Paciente)
        {
            IdTextBox.Text = Convert.ToString(Paciente.PacienteId);
            NombresTextBox.Text = Paciente.Nombres;
            DireccionTextBox.Text = Paciente.Direccion;
            TelefonoTextBox.Text = Paciente.Telefono;
        }


        private bool ExisteEnLaBaseDeDatos()
        {
            RepositorioBase<Paciente> repositorio = new RepositorioBase<Paciente>();
            Paciente Paciente = repositorio.Buscar(Utils.ToInt(IdTextBox.Text));
            return (Paciente != null);
        }

        protected void GuardarButton_Click1(object sender, EventArgs e)
        {
            RepositorioBase<Paciente> Repositorio = new RepositorioBase<Paciente>();
            Paciente Paciente = new Paciente();
            bool paso = false;

            Paciente = LlenaClase(Paciente);

            if (Utils.ToInt(IdTextBox.Text) == 0)
            {
                paso = Repositorio.Guardar(Paciente);
                Limpiar();
            }
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {

                    Utils.ShowToastr(this, "No se pudo guardar", "Error", "error");
                    return;
                }
                paso = Repositorio.Modificar(Paciente);
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
            RepositorioBase<Paciente> repositorio = new RepositorioBase<Paciente>();
            var Paciente = repositorio.Buscar(Utils.ToInt(IdTextBox.Text));

            if (Paciente != null)
            {
                Limpiar();
                LlenaCampos(Paciente);
                Utils.ShowToastr(this, "Busqueda exitosa", "Exito", "success");
            }
            else
            {
                Utils.ShowToastr(this.Page, "El paciente que intenta buscar no existe", "Error", "error");
                Limpiar();
            }
        }

        protected void EliminarButton_Click1(object sender, EventArgs e)
        {
            if (Utils.ToInt(IdTextBox.Text) > 0)
            {
                int id = Convert.ToInt32(IdTextBox.Text);
                RepositorioBase<Paciente> repositorio = new RepositorioBase<Paciente>();
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
                Utils.ShowToastr(this.Page, "No se pudo eliminar, paciente no existe", "error", "error");
            }
        }

    }
}
