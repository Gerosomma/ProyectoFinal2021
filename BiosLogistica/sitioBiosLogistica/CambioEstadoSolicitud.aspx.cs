using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wcfLogistica;

public partial class CambioEstadoSolicitud : System.Web.UI.Page
{
    Empleado usuarioLogueado;
    List<Solicitud> solicitudes;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ((Label)this.Master.FindControl("lblPagina")).Text = "Alta solicitud";
            usuarioLogueado = (Empleado)Session["Usuario"];
            
            ServiceClient wcf = new ServiceClient();
            solicitudes = wcf.listadoSolicitudes(new Empresa()).ToList<Solicitud>();
            gvSolicitudes.DataSource = solicitudes;
            gvSolicitudes.DataBind();

        }
        catch (Exception)
        {
            lblMensaje.Text = "Ocurrió un error al cargar paquetes";
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Solicitud sol = solicitudes.ElementAt(gvSolicitudes.SelectedIndex);
            ServiceClient wcf = new ServiceClient();
            wcf.ModificarEstadoSolicitud(sol, usuarioLogueado);
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }
}