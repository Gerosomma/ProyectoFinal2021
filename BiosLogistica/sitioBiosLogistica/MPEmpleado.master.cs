using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wcfLogistica;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Usuario"] is Empleado)
            {
                lblMensaje.Text = ((Empleado)Session["Usuario"]).Logueo;
                btnCambioContrasena.Visible = true;
            }
            else
            {
                btnCambioContrasena.Visible = false;
                Response.Redirect("~/Login.aspx");
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Remove("Usuario");
        Session.Remove("Mensaje");
        lblMensaje.Text = "Usuario desconectado";
        Response.Redirect("~/Default.aspx");
    }

    protected void btnCambioContrasena_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/CambioContrasenaEmpleado.aspx");
    }
}
