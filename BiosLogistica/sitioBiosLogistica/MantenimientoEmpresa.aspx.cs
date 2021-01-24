using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EntidadesCompartidas;
using Logica;

public partial class MantenimientoEmpresa : System.Web.UI.Page
{
    Usuario usuarioLogueado;
    protected void Page_Load(object sender, EventArgs e)
    {
        ((Label)this.Master.FindControl("lblPagina")).Text = "ABM de Empresa";
        usuarioLogueado = (Usuario)Session["Usuario"];
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        string logueo;
        Empresa empresaEncontrada = null;

        if (!string.IsNullOrWhiteSpace(txtLogueo.Text))
        {
            try
            {
                logueo = txtLogueo.Text;
                empresaEncontrada = (Empresa)FabricaLogica.GetLogicaUsuario().BuscarUsuario(logueo, usuarioLogueado);

                btnBuscar.Enabled = false;

                txtLogueo.Enabled = false;
                txtContrasena.Enabled = true;
                txtContrasena.Focus();
                txtNombre.Enabled = true;
                txtTelefono.Enabled = true;
                txtDireccion.Enabled = true;
                txtEmail.Enabled = true;

                if (empresaEncontrada != null)
                {
                    txtContrasena.Text = empresaEncontrada.Contrasena;
                    txtNombre.Text = empresaEncontrada.NombreCompleto;
                    txtTelefono.Text = empresaEncontrada.Telefono;
                    txtDireccion.Text = empresaEncontrada.Direccion;
                    txtEmail.Text = empresaEncontrada.Email;

                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;

                    lblMensaje.Text = "Empresa encontrada";
                }
                else
                {
                    btnAgregar.Enabled = true;

                    lblMensaje.Text = "No se encontró ninguna empresa con el nombre " + logueo + ". Puede agregar una ahora.";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Ocurrió un error al buscar la empresa.";

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
            string telefono = txtTelefono.Text;
            string direccion = txtDireccion.Text;
            string email = txtEmail.Text;

            Empresa empresa = new Empresa(logueo, contrasena, nombre, telefono, direccion, email);

            FabricaLogica.GetLogicaUsuario().AltaUsuario(empresa, usuarioLogueado);

            LimpiarFormulario();
            lblMensaje.Text = "Empresa agregada con éxito";
        }
        catch(Exception ex)
        {
            lblMensaje.Text = "Ocurrió un error al agregar la empresa";

            return;
        }
    }

    protected void btnModificar_Click(object sender, EventArgs e)
    {
        try
        {
            string logueo = txtLogueo.Text;
            Empresa empresa = (Empresa)FabricaLogica.GetLogicaUsuario().BuscarUsuario(logueo, usuarioLogueado);

            empresa.Contrasena = txtContrasena.Text;
            empresa.NombreCompleto = txtNombre.Text;
            empresa.Telefono = txtTelefono.Text;
            empresa.Direccion = txtDireccion.Text;
            empresa.Email = txtEmail.Text;

            FabricaLogica.GetLogicaUsuario().ModificarUsuario(empresa, usuarioLogueado);

            LimpiarFormulario();
            lblMensaje.Text = "Empresa modificada con éxito";
        }
        catch (Exception ex)
        {
            lblMensaje.Text = "Ocurrió un error al modificar la empresa";

            return;
        }
        
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            string logueo = txtLogueo.Text;
            Empresa empresa = (Empresa)FabricaLogica.GetLogicaUsuario().BuscarUsuario(logueo, usuarioLogueado);

            FabricaLogica.GetLogicaUsuario().BajaUsuario(empresa, usuarioLogueado);

            LimpiarFormulario();
            lblMensaje.Text = "Empresa eliminada con éxito";
        }
        catch (Exception ex)
        {
            lblMensaje.Text = "Ocurrió un error al eliminar la empresa.";
        }
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        LimpiarFormulario();
    }

    private void LimpiarFormulario()
    {
        txtLogueo.Text = string.Empty;
        txtLogueo.Focus();
        txtContrasena.Text = string.Empty;
        txtContrasena.Enabled = false;
        txtNombre.Text = string.Empty;
        txtNombre.Enabled = false;
        txtTelefono.Text = string.Empty;
        txtTelefono.Enabled = false;
        txtDireccion.Text = string.Empty;
        txtDireccion.Enabled = false;
        txtEmail.Text = string.Empty;
        txtEmail.Enabled = false;

        btnBuscar.Enabled = true;
        btnAgregar.Enabled = false;
        btnModificar.Enabled = false;
        btnEliminar.Enabled = false;
    }
}