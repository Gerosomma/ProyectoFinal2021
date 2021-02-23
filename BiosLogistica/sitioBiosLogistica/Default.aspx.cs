using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using wcfLogistica;

public partial class _Default : System.Web.UI.Page
{
    static string documento = "";
    static XmlDocument doc = new XmlDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                //documento = FabricaLogica.GetLogicaSolicitud().listadoSolicitudesEnCamino();
                ServiceClient wcf = new ServiceClient();
                documento = wcf.listadoSolicitudesEnCamino();
                wcf.Close();
                doc.LoadXml(documento);

                XElement element = XElement.Parse(doc.OuterXml);
                var res = (from node in element.Elements("solicitud")
                            select new
                            {
                                Numero = node.Elements("numero").First().Value,
                                FechaEntrega = node.Elements("fechaEntrega").First().Value,
                                NombreDestinatario = node.Elements("nombreDestinatario").First().Value,
                                direccionDestinatario = node.Elements("direccionDestinatario").First().Value,
                                Estado = node.Elements("estado").First().Value
                            }).ToList();

                gvSolicitudes.DataSource = res;
                gvSolicitudes.DataBind();

                if (doc == null)
                {
                    lblMensaje.Text = "No hay solicitudes pendientes de entrega";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Login.aspx");
    }
}