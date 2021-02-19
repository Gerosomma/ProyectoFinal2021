using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EntidadesCompartidas;
using Logica;

public partial class MantenimientoEmpleado : System.Web.UI.Page
{
    Empleado usuarioLogueado;
    protected void Page_Load(object sender, EventArgs e)
    {
        ((Label)this.Master.FindControl("lblPagina")).Text = "ABM de Empleado";
        usuarioLogueado = (Empleado)Session["Usuario"];
        
        if (usuarioLogueado == null)
        {
            lblMensaje.Text = "Logeese para modificar ";
            btnBuscar.Enabled = false;
        }
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        string logueo;
        Empleado empleadoEncontrado = null;

        if (!String.IsNullOrWhiteSpace(txtLogueo.Text))
        {
            try
            {
                logueo = txtLogueo.Text;
                empleadoEncontrado = (Empleado)FabricaLogica.GetLogicaUsuario().BuscarUsuario(logueo, usuarioLogueado);

                btnBuscar.Enabled = false;

                txtLogueo.Enabled = false;
                txtContrasena.Enabled = true;
                txtContrasena.Focus();
                txtNombre.Enabled = true;
                txtHoraInicio.Enabled = true;
                txtHoraFin.Enabled = true;
                

                if (empleadoEncontrado != null)
                {
                    txtContrasena.Text = empleadoEncontrado.Contrasena;
                    txtNombre.Text = empleadoEncontrado.NombreCompleto;
                    txtHoraInicio.Text = empleadoEncontrado.HoraInicio;
                    txtHoraFin.Text = empleadoEncontrado.HoraFin;

                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;

                    lblMensaje.Text = "Empleado encontrado";
                }
                else
                {
                    btnAgregar.Enabled = true;

                    lblMensaje.Text = "No se encontró ningún empleado con el nombre " + logueo + ". Puede agregar uno ahora.";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Ocurrió un error al buscar el empleado";

                return;
            }

        }
        else
        {
            lblMensaje.Text = "Debe ingresar un nombre de usuario.";

            return;
        }
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        try
        {
            string logueo = txtLogueo.Text;
            string contrasena = txtContrasena.Text;
            string nombre = txtNombre.Text;
            string horaInicio = txtHoraInicio.Text;
            string horaFin = txtHoraFin.Text;         

            Empleado empleado = new Empleado(logueo, contrasena, nombre, horaInicio, horaFin);

            FabricaLogica.GetLogicaUsuario().AltaUsuario(empleado, usuarioLogueado);

            LimpiarFormulario();
            lblMensaje.Text = "Empleado agregado con éxito";
        }
        catch (Exception ex)
        {
            lblMensaje.Text = "Ocurrió un error al agregar el empleado";

            return;
        }
    }

    protected void btnModificar_Click(object sender, EventArgs e)
    {
        try
        {
            string logueo = txtLogueo.Text;
            Empleado empleado = (Empleado)FabricaLogica.GetLogicaUsuario().BuscarUsuario(logueo, usuarioLogueado);

            empleado.Contrasena = txtContrasena.Text;
            empleado.NombreCompleto = txtNombre.Text;
            empleado.HoraInicio = txtHoraInicio.Text;
            empleado.HoraFin = txtHoraFin.Text;            

            FabricaLogica.GetLogicaUsuario().ModificarUsuario(empleado, usuarioLogueado);

            LimpiarFormulario();
            lblMensaje.Text = "Empleado modificado con éxito";
        }
        catch (Exception ex)
        {
            lblMensaje.Text = "Ocurrió un error al modificar el empleado";

            return;
        }
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            string logueo = txtLogueo.Text;
            Empleado empleado = (Empleado)FabricaLogica.GetLogicaUsuario().BuscarUsuario(logueo, usuarioLogueado);

            FabricaLogica.GetLogicaUsuario().BajaUsuario(empleado, usuarioLogueado);

            LimpiarFormulario();
            lblMensaje.Text = "Empleado eliminado con éxito";
        }
        catch (Exception ex)
        {
            lblMensaje.Text = "Ocurrió un error al eliminar el empleado.";
        }
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        LimpiarFormulario();
    }

    private void LimpiarFormulario()
    {
        txtLogueo.Text = string.Empty;
        txtLogueo.Enabled = true;
        txtLogueo.Focus();
        txtContrasena.Text = string.Empty;
        txtContrasena.Enabled = false;
        txtNombre.Text = string.Empty;
        txtNombre.Enabled = false;
        txtHoraInicio.Text = string.Empty;
        txtHoraInicio.Enabled = false;
        txtHoraFin.Text = string.Empty;
        txtHoraFin.Enabled = false;

        btnBuscar.Enabled = true;
        btnAgregar.Enabled = false;
        btnModificar.Enabled = false;
        btnEliminar.Enabled = false;

        lblMensaje.Text = string.Empty;
    }
}