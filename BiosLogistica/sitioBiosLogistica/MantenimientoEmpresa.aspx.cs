using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wcfLogistica;

public partial class MantenimientoEmpresa : System.Web.UI.Page
{
    private Empleado usuarioLogueado = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        ((Label)this.Master.FindControl("lblPagina")).Text = "ABM de Empresa";
        usuarioLogueado = (Empleado)Session["Usuario"];
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        string logueo;

        if (!string.IsNullOrWhiteSpace(txtLogueo.Text))
        {
            try
            {
                logueo = txtLogueo.Text;
                ServiceClient wcf = new ServiceClient();
                Empresa objEmpresa = (Empresa)wcf.BuscarUsuario(logueo, usuarioLogueado);

                btnBuscar.Enabled = false;

                txtLogueo.Enabled = false;
                txtContrasena.Enabled = true;
                txtContrasena.Focus();
                txtNombre.Enabled = true;
                txtTelefono.Enabled = true;
                txtDireccion.Enabled = true;
                txtEmail.Enabled = true;

                if (objEmpresa != null)
                {
                    txtContrasena.Text = objEmpresa.Contrasena;
                    txtNombre.Text = objEmpresa.NombreCompleto;
                    txtTelefono.Text = objEmpresa.Telefono;
                    txtDireccion.Text = objEmpresa.Direccion;
                    txtEmail.Text = objEmpresa.Email;

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
            Empresa emp = new Empresa();
            emp.Logueo = txtLogueo.Text;
            emp.Contrasena = txtContrasena.Text;
            emp.NombreCompleto = txtNombre.Text;
            emp.Telefono = txtTelefono.Text;
            emp.Direccion = txtDireccion.Text;
            emp.Email = txtEmail.Text;
            
            ServiceClient wcf = new ServiceClient();
            wcf.AltaUsuario(emp, usuarioLogueado);
            //Empresa empresa = new Empresa(logueo, contrasena, nombre, telefono, direccion, email);
            //FabricaLogica.GetLogicaUsuario().AltaUsuario(empresa, usuarioLogueado);

            LimpiarFormulario();
            lblMensaje.Text = "Empresa agregada con éxito";
        }
        catch(Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    protected void btnModificar_Click(object sender, EventArgs e)
    {
        try
        {
            Empresa objEmpresa = new Empresa();
            objEmpresa.Logueo = txtLogueo.Text;
            objEmpresa.Contrasena = txtContrasena.Text;
            objEmpresa.NombreCompleto = txtNombre.Text;
            objEmpresa.Telefono = txtTelefono.Text;
            objEmpresa.Direccion = txtDireccion.Text;
            objEmpresa.Email = txtEmail.Text;

            //FabricaLogica.GetLogicaUsuario().ModificarUsuario(empresa, usuarioLogueado);
            ServiceClient wcf = new ServiceClient();
            wcf.ModificarUsuario(objEmpresa, usuarioLogueado);

            LimpiarFormulario();
            lblMensaje.Text = "Empresa modificada con éxito";
            
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
        
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            Empresa objEmpresa = new Empresa();
            objEmpresa.Logueo = txtLogueo.Text;
            objEmpresa.Contrasena = txtContrasena.Text;
            objEmpresa.NombreCompleto = txtNombre.Text;
            objEmpresa.Telefono = txtTelefono.Text;
            objEmpresa.Direccion = txtDireccion.Text;
            objEmpresa.Email = txtEmail.Text;

            ServiceClient wcf = new ServiceClient();
            wcf.BajaUsuario(objEmpresa, usuarioLogueado);

            LimpiarFormulario();
            lblMensaje.Text = "Empresa eliminada con éxito";
            

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