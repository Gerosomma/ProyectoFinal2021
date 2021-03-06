using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wcfLogistica;

public partial class CambioContrasenaEmpleado : System.Web.UI.Page
{
    private Empleado usuarioLogueado = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        usuarioLogueado = (Empleado)Session["Usuario"];
    }

    protected void btnCambiar_Click(object sender, EventArgs e)
    {
        try
        {
            Empleado emp = new Empleado();
            emp.Logueo = usuarioLogueado.Logueo;
            emp.Contrasena = txtCnnNueva.Text;
            emp.NombreCompleto = usuarioLogueado.NombreCompleto;
            emp.HoraFin = usuarioLogueado.HoraFin;
            emp.HoraInicio = usuarioLogueado.HoraInicio;
            
            if (usuarioLogueado.Contrasena != txtCnnActual.Text)
                throw new Exception("Constraseña actual incorrecta.");

            if (txtCnnNueva.Text != txtCnnNuevaRep.Text)
                throw new Exception("Repeticion contraseña inválida.");

            emp.Contrasena = txtCnnNueva.Text;

            ServiceClient wcf = new ServiceClient();
            wcf.ModificarContrasenaUsuario(emp, usuarioLogueado);
            lblMensaje.Text = "Contraseña modficada correctamente.";
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }
}