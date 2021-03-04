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
            usuarioLogueado = (Empleado)Session["Usuario"];
            listarSolicitudes();
        }
        catch (Exception)
        {
            lblMensaje.Text = "Ocurrió un error al cargar solicitudes";
        }
    }

    protected void gvSolicitudes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            Solicitud sol = solicitudes.ElementAt(Convert.ToInt32(e.CommandArgument));
            ServiceClient wcf = new ServiceClient();
            wcf.ModificarEstadoSolicitud(sol, usuarioLogueado);
            wcf.Close();
            listarSolicitudes();
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    private void listarSolicitudes()
    {
        ServiceClient wcf = new ServiceClient();
        solicitudes = wcf.listadoSolicitudes(usuarioLogueado).ToList<Solicitud>();
        gvSolicitudes.DataSource = solicitudes;
        gvSolicitudes.DataBind();
        wcf.Close();
    }
}