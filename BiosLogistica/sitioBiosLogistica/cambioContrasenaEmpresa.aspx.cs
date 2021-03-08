using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wcfLogistica;

public partial class cambioContraseñaEmpresa : System.Web.UI.Page
{
    private Empresa usuarioLogueado = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        usuarioLogueado = (Empresa)Session["Usuario"];
    }

    protected void btnCambiar_Click(object sender, EventArgs e)
    {
        try
        {
            Empresa emp = new Empresa();
            emp.Logueo = usuarioLogueado.Logueo;
            emp.Contrasena = txtCnnNueva.Text;
            emp.NombreCompleto = usuarioLogueado.NombreCompleto;
            emp.Telefono = usuarioLogueado.Telefono;
            emp.Direccion = usuarioLogueado.Direccion;
            emp.Email = usuarioLogueado.Email;

            if (usuarioLogueado.Contrasena != txtCnnActual.Text)
                throw new Exception("Constraseña actual incorrecta.");

            if (txtCnnNueva.Text != txtCnnNuevaRep.Text)
                throw new Exception("Repeticion contraseña inválida.");

            ServiceClient wcf = new ServiceClient();
            wcf.ModificarContrasenaUsuario(emp, usuarioLogueado);
            lblMensaje.Text = "Contraseña modficada correctamente.";
            Session["Usuario"] = emp;
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }
}