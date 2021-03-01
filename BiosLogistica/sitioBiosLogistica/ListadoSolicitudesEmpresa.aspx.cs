using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wcfLogistica;

public partial class ListadoSolicitudesEmpresa : System.Web.UI.Page
{
    Empresa usuarioLogueado;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ((Label)this.Master.FindControl("lblPagina")).Text = "Alta solicitud";
            usuarioLogueado = (Empresa)Session["Usuario"];

            ServiceClient wcf = new ServiceClient();
            List<Solicitud> solicitudes = wcf.listadoSolicitudesEmpresa(usuarioLogueado).ToList<Solicitud>();
            Session["SolicitudesEmpresa"] = solicitudes;

            var res = (from sol in solicitudes
                       select new
                       {
                           Numero = sol.Numero.ToString(),
                           FechaEntrega = sol.FechaEntrega.ToShortDateString(),
                           Destinatario = sol.NombreDestinatario,
                           Direccion = sol.DireccionDestinatario,
                           Estado = sol.Estado,
                           Empleado = sol.Empleado.NombreCompleto
                       }
                ).ToList();

            gvSolicitudes.DataSource = res;
            gvSolicitudes.DataBind();
        }
        catch (Exception)
        {
            lblMensaje.Text = "Ocurrió un error al cargar solicitudes";
        }
    }

    protected void btnUltimoMes_Click(object sender, EventArgs e)
    {
        try
        {
            List<Solicitud> solicitudes = (List<Solicitud>)Session["SolicitudesEmpresa"];
            var res = (from sol in solicitudes
                       orderby sol.FechaEntrega.Date
                       where sol.FechaEntrega.Date.Month == DateTime.Today.Date.Month
                       select new
                       {
                           Numero = sol.Numero.ToString(),
                           FechaEntrega = sol.FechaEntrega.ToShortDateString(),
                           Destinatario = sol.NombreDestinatario,
                           Direccion = sol.DireccionDestinatario,
                           Estado = sol.Estado,
                           Empleado = sol.Empleado.NombreCompleto
                       }
                ).ToList();

            gvSolicitudes.DataSource = res;
            gvSolicitudes.DataBind();
        }
        catch (Exception ex)
        {
            lblMensaje.Text = "Error al filtrar ultimo mes.";
        }
    }

    protected void btnResumenMensual_Click(object sender, EventArgs e)
    {
        try
        {
            List<Solicitud> solicitudes = (List<Solicitud>)Session["SolicitudesEmpresa"];
            var res = (from sol in solicitudes
                       group sol by sol.FechaEntrega.Date.Year & sol.FechaEntrega.Date.Month  into y
                       join s in solicitudes on y.Key equals s.Numero
                       select new
                       {
                            Anio = s.FechaEntrega.Date.Year + " / " + s.FechaEntrega.Date.Month,
                            Cantidad = y.Count()
                       }
                ).ToList();

            gvSolicitudes.DataSource = res;
            gvSolicitudes.DataBind();
        }
        catch (Exception ex)
        {
            lblMensaje.Text = "Error al filtrar resumen mensual.";
        }
    }

    protected void btnFecha_Click(object sender, EventArgs e)
    {
        DateTime fechaSeleccionada = new DateTime();
        try
        {
            fechaSeleccionada = calFecha.SelectedDate;
        }
        catch (Exception)
        {
            lblMensaje.Text = "Seleccione una fecha válida";
        }

        try
        {
            List<Solicitud> solicitudes = (List<Solicitud>)Session["SolicitudesEmpresa"];
            var res = (from sol in solicitudes
                             orderby sol.FechaEntrega
                             where sol.FechaEntrega.Date == (fechaSeleccionada.Date)
                             select new
                             {
                                 Numero = sol.Numero.ToString(),
                                 Estado = sol.Estado,
                                 Fecha = sol.FechaEntrega.ToShortDateString()
                             }
                            ).ToList();
            gvSolicitudes.DataSource = res;
            gvSolicitudes.DataBind();
            lblMensaje.Text = "Solicitudes filtradas por fecha " + fechaSeleccionada.ToShortDateString() + ".";
        }
        catch (Exception ex)
        {
            lblMensaje.Text = "Error al filtrar solicitudes a la fecha.";
        }
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        try
        {
            List<Solicitud> solicitudes = (List<Solicitud>)Session["SolicitudesEmpresa"];
            var res = (from sol in solicitudes
                       select new
                       {
                           Numero = sol.Numero.ToString(),
                           FechaEntrega = sol.FechaEntrega.ToShortDateString(),
                           Destinatario = sol.NombreDestinatario,
                           Direccion = sol.DireccionDestinatario,
                           Estado = sol.Estado,
                           Empleado = sol.Empleado.NombreCompleto
                       }
                ).ToList();

            gvSolicitudes.DataSource = res;
            gvSolicitudes.DataBind();
        }
        catch (Exception ex)
        {
            lblMensaje.Text = "Error al limpiar fomulario.";
        }
    }
}