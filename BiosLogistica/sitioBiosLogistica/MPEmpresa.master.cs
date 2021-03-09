using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wcfLogistica;

public partial class MPEmpresa : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Usuario"] is Empresa)
            {
                lblMensaje.Text = ((Empresa)Session["Usuario"]).NombreCompleto;
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
        Session.Remove("SolicitudesEmpresa");
        Session.Remove("Paquetes");
        Session.Remove("PaquetesSeleccionados");
        lblMensaje.Text = "Usuario desconectado";
        Response.Redirect("~/Default.aspx");
    }

    protected void btnCambioContrasena_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/CambioContrasenaEmpresa.aspx");
    }
}
