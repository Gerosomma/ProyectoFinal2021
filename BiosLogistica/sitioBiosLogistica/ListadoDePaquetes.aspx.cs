using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using wcfLogistica;

public partial class ListadoDePaquetes : System.Web.UI.Page
{
    private Empleado usuarioLogueado = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        ((Label)this.Master.FindControl("lblPagina")).Text = "Listado de paquetes";

        usuarioLogueado = (Empleado)Session["Usuario"];

        if (usuarioLogueado == null)
        {            
            Response.Redirect("~/Login.aspx");
        }

        try
        {
            ServiceClient wcf = new ServiceClient();

            gvPaquetes.DataSource = wcf.ListadoPaquetesSinSolicitud(usuarioLogueado);
            gvPaquetes.DataBind();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;

            return;
        }        
    }
}