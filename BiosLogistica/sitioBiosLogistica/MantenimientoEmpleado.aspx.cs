using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wcfLogistica;

public partial class MantenimientoEmpleado : System.Web.UI.Page
{
    private Empleado usuarioLogueado = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        ((Label)this.Master.FindControl("lblPagina")).Text = "ABM de Empleado";
        usuarioLogueado = (Empleado)Session["Usuario"];
        
        if (usuarioLogueado == null)
        {
            lblMensaje.Text = "Logeese para modificar ";
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        string logueo;

        if (!String.IsNullOrWhiteSpace(txtLogueo.Text))
        {
            try
            {
                logueo = txtLogueo.Text;

                ServiceClient wcf = new ServiceClient();
                Empleado objEmpleado = (Empleado)wcf.BuscarUsuario(logueo, usuarioLogueado);
                //empleadoEncontrado = (Empleado)FabricaLogica.GetLogicaUsuario().BuscarUsuario(logueo, usuarioLogueado);

                btnBuscar.Enabled = false;

                txtLogueo.Enabled = false;
                txtContrasena.Enabled = true;
                txtContrasena.Focus();
                txtNombre.Enabled = true;
                txtHoraInicio.Enabled = true;
                txtHoraFin.Enabled = true;
                

                if (objEmpleado != null)
                {
                    txtContrasena.Text = objEmpleado.Contrasena;
                    txtNombre.Text = objEmpleado.NombreCompleto;
                    txtHoraInicio.Text = objEmpleado.HoraInicio;
                    txtHoraFin.Text = objEmpleado.HoraFin;

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
                lblMensaje.Text = ex.Message;

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
            //Empleado empleado = new Empleado(logueo, contrasena, nombre, horaInicio, horaFin);
            Empleado empleado = new Empleado();
            empleado.Logueo = txtLogueo.Text;
            empleado.Contrasena = txtContrasena.Text;
            empleado.NombreCompleto = txtNombre.Text;
            empleado.HoraInicio = txtHoraInicio.Text + ":00";
            empleado.HoraFin = txtHoraFin.Text + ":00";

            ServiceClient wcf = new ServiceClient();
            wcf.AltaUsuario(empleado, usuarioLogueado);
            //FabricaLogica.GetLogicaUsuario().AltaUsuario(empleado, usuarioLogueado);

            LimpiarFormulario();
            lblMensaje.Text = "Empleado agregado con éxito";
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;

            return;
        }
    }

    protected void btnModificar_Click(object sender, EventArgs e)
    {
        try
        {
            Empleado objEmpleado = new Empleado();
            objEmpleado.Logueo = txtLogueo.Text;
            objEmpleado.Contrasena = txtContrasena.Text;
            objEmpleado.NombreCompleto = txtNombre.Text;
            objEmpleado.HoraInicio = txtHoraInicio.Text + ":00";
            objEmpleado.HoraFin = txtHoraFin.Text + ":00";

            ServiceClient wcf = new ServiceClient();
            wcf.ModificarUsuario(objEmpleado, usuarioLogueado);
            
            LimpiarFormulario();
            lblMensaje.Text = "Empleado modificado con éxito";
            
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;

            return;
        }
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            Empleado objEmpleado = new Empleado();
            objEmpleado.Logueo = txtLogueo.Text;
            objEmpleado.Contrasena = txtContrasena.Text;
            objEmpleado.NombreCompleto = txtNombre.Text;
            objEmpleado.HoraInicio = txtHoraInicio.Text;
            objEmpleado.HoraFin = txtHoraFin.Text;
            ServiceClient wcf = new ServiceClient();
            wcf.BajaUsuario(objEmpleado, usuarioLogueado);

            LimpiarFormulario();
            lblMensaje.Text = "Empleado eliminado con éxito";
            

        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
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