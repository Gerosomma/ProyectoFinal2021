using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MPEmpresa : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Usuario us = (Usuario)Session["Usuario"];
        if (us != null)
        {
            lblMensaje.Text = us.Logueo;
        }
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {

        Session.Remove("Usuario");
        Session.Remove("Mensaje");
        lblMensaje.Text = "Usuario desconectado";
        Response.Redirect("~/Default.aspx");
    }
}
