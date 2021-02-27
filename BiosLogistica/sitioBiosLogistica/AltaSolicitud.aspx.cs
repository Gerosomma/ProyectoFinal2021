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
    List<Paquete> paquetes;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ((Label)this.Master.FindControl("lblPagina")).Text = "Alta solicitud";
            usuarioLogueado = (Empleado)Session["Usuario"];
            
            //empresas = FabricaLogica.GetLogicaUsuario().ListarEmpresas(usuarioLogueado);
            ServiceClient wcf = new ServiceClient();
            paquetes = wcf.ListadoPaquetesSinSolicitud(usuarioLogueado).ToList<Paquete>();
            gvPaquetes.DataSource = paquetes;
            gvPaquetes.DataBind();
            
        }
        catch (Exception)
        {
            lblMensaje.Text = "Ocurrió un error al cargar paquetes";
        }
    }

    protected void btnGenerar_Click(object sender, EventArgs e)
    {
        try
        {
            int nroSolitud = 0;
            List<Paquete> paqSolicitud = new List<Paquete>();
            if (calFEntrega.SelectedDate.Date == null)
            {
                throw new Exception("Debe seleccionar una fecha de entrega.");
            }
            ServiceClient wcf2 = new ServiceClient();
            foreach (GridViewRow row in gvPaquetes.Rows)
            {
                CheckBox ck = ((CheckBox)row.FindControl("Sel"));
                if (ck.Checked)
                {
                    Paquete paq = wcf2.BuscarPaquete(Convert.ToInt32(row.Cells[1].ToString()), usuarioLogueado);
                    paqSolicitud.Add(paq);
                }
            }
            wcf2.Close();

            if (paqSolicitud.Count == 0)
            {
                throw new Exception("Debe seleccionar los paquetes a enviar.");
            }

            Solicitud solicitud = new Solicitud();
            solicitud.Empleado = usuarioLogueado;
            solicitud.NombreDestinatario = txtNombre.Text;
            solicitud.DireccionDestinatario = txtDireccion.Text;
            solicitud.FechaEntrega = calFEntrega.SelectedDate;
            solicitud.PaquetesSolicitud = paqSolicitud.ToArray();

            ServiceClient wcf = new ServiceClient();
            wcf.AltaSolicitud(solicitud, usuarioLogueado);
            wcf.Close();
            lblMensaje.Text = "Exito, el nro de solicitud generada es: " + nroSolitud;
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }
}