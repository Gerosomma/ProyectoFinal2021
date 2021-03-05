using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wcfLogistica;
public partial class AltaSolicitud : System.Web.UI.Page
{
    Empleado usuarioLogueado;

    protected void Page_Load(object sender, EventArgs e)
    {
        usuarioLogueado = (Empleado)Session["Usuario"];
        if (!IsPostBack)
        {
            try
            {
                ServiceClient wcf = new ServiceClient();
                List<Paquete> paquetes = wcf.ListadoPaquetesSinSolicitud(usuarioLogueado).ToList<Paquete>();
                Session["Paquetes"] = paquetes;
                Session["PaquetesSeleccionados"] = new List<Paquete>();
                gvPaquetes.DataSource = paquetes;
                gvPaquetes.DataBind();
                wcf.Close();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Ocurrió un error al cargar paquetes";
            }
        }
    }

    protected void CustomersGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            List<Paquete> paquetes = (List<Paquete>)Session["Paquetes"];
            List<Paquete> paquetesSeleccionados = (List<Paquete>)Session["PaquetesSeleccionados"];
            Paquete p = paquetes.ElementAt(index);
            if (paquetesSeleccionados.Contains(p))
            {
                gvPaquetes.Rows[index].BackColor = System.Drawing.Color.Empty;
                paquetesSeleccionados.Remove(p);
                Session["PaquetesSeleccionados"] = paquetesSeleccionados;
            }
            else
            {
                gvPaquetes.Rows[index].BackColor = System.Drawing.Color.Yellow;
                paquetesSeleccionados.Add(p);
                Session["PaquetesSeleccionados"] = paquetesSeleccionados;
            }
        }
    }

    protected void btnGenerar_Click(object sender, EventArgs e)
    {
        try
        {
            List<Paquete> paquetesSeleccionados = (List<Paquete>)Session["PaquetesSeleccionados"];
            if (calFEntrega.SelectedDate.Date == null)
            {
                throw new Exception("Debe seleccionar una fecha de entrega.");
            }

            if (paquetesSeleccionados == null || paquetesSeleccionados.Count == 0)
            {
                throw new Exception("Debe seleccionar los paquetes a enviar.");
            }

            Solicitud solicitud = new Solicitud();
            solicitud.Numero = 1;
            solicitud.Empleado = usuarioLogueado;
            solicitud.NombreDestinatario = txtNombre.Text;
            solicitud.DireccionDestinatario = txtDireccion.Text;
            solicitud.Estado = "en deposito";
            solicitud.FechaEntrega = calFEntrega.SelectedDate;
            solicitud.PaquetesSolicitud = paquetesSeleccionados.ToArray();

            ServiceClient wcf = new ServiceClient();
            solicitud.Numero = wcf.AltaSolicitud(solicitud, usuarioLogueado);
            wcf.Close();
            lblMensaje.Text = "Exito, el nro de solicitud generada es: " + solicitud.Numero;

            limpiarFormulario();
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        limpiarFormulario();
    }

    private void limpiarFormulario()
    {
        try
        {
            Session["Paquetes"] = null;
            Session["PaquetesSeleccionados"] = null;
            txtNombre.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            calFEntrega.SelectedDate = DateTime.Today;
            ServiceClient wcf = new ServiceClient();
            List<Paquete> paquetes = wcf.ListadoPaquetesSinSolicitud(usuarioLogueado).ToList<Paquete>();
            Session["Paquetes"] = paquetes;
            gvPaquetes.DataSource = paquetes;
            gvPaquetes.DataBind();
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }
}