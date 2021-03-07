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
        if (!IsPostBack)
        {
            try
            {
                calFecha.SelectedDate = DateTime.Today;
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
    }

    protected void btnUltimoMes_Click(object sender, EventArgs e)
    {
        try
        {
            List<Solicitud> solicitudes = (List<Solicitud>)Session["SolicitudesEmpresa"];
            var res = (from sol in solicitudes
                       where sol.FechaEntrega.Date.Month == DateTime.Today.Date.Month
                       orderby sol.FechaEntrega
                       group sol by new
                       {
                           sol.FechaEntrega.Date.Month,
                           sol.Estado
                       } into mes
                       select new
                       {
                           Mes = obtenerNombreMesNumero(DateTime.Today.Date.Month),
                           Estado = mes.Key.Estado,
                           Cantidad = mes.Count()
                       }
                ).ToList();

            gvSolicitudes.DataSource = res;
            gvSolicitudes.DataBind();
            lblMensaje.Text = "Solicitudes del mes actual por estado.";
        }
        catch (Exception)
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
                       orderby sol.FechaEntrega
                       group sol by new
                       {
                           sol.FechaEntrega.Date.Year,
                           sol.FechaEntrega.Date.Month
                       } into aniomes
                       select new
                       {
                            Año = aniomes.Key.Year,
                            Mes = obtenerNombreMesNumero(aniomes.Key.Month),
                            Cantidad = aniomes.Count()
                       }
                ).ToList();

            gvSolicitudes.DataSource = res;
            gvSolicitudes.DataBind();
            lblMensaje.Text = "Solicitudes por año y mes";
        }
        catch (Exception)
        {
            lblMensaje.Text = "Error al filtrar resumen mensual.";
        }
    }

    protected void btnFecha_Click(object sender, EventArgs e)
    {
        if (calFecha.SelectedDate == null)
            throw new Exception("Debe seleccionar una fecha.");

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
                                 FechaEntrega = sol.FechaEntrega.ToShortDateString(),
                                 Destinatario = sol.NombreDestinatario,
                                 Direccion = sol.DireccionDestinatario,
                                 Estado = sol.Estado,
                                 Empleado = sol.Empleado.NombreCompleto
                             }
                            ).ToList();
            gvSolicitudes.DataSource = res;
            gvSolicitudes.DataBind();
            lblMensaje.Text = "Solicitudes filtradas por fecha " + fechaSeleccionada.ToShortDateString() + ".";
        }
        catch (Exception)
        {
            lblMensaje.Text = "Error al filtrar solicitudes a la fecha.";
        }
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        try
        {
            lblMensaje.Text = "";
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

            calFecha.SelectedDate = DateTime.Today;
        }
        catch (Exception)
        {
            lblMensaje.Text = "Error al limpiar fomulario.";
        }
    }

    private string obtenerNombreMesNumero(int numeroMes)
    {
        try
        {
            System.Globalization.DateTimeFormatInfo formatoFecha = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat;
            string nombreMes = formatoFecha.GetMonthName(numeroMes);
            return nombreMes;
        }
        catch
        {
            return "Desconocido";
        }
    }
}