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
        try
        {
            Session["Usuario"] = null;
        }
        catch (Exception ex)
        {
            lblMensaje.Text = "Error: " + ex.Message;
        }
        if (!IsPostBack)
        {
            try
            {
                ServiceClient wcf = new ServiceClient();
                documento = wcf.listadoSolicitudesEnCamino();
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

    protected void gvSolicitudes_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string codigoTramite = gvSolicitudes.SelectedRow.Cells[1].Text;
            doc.LoadXml(documento);

            XElement element = XElement.Parse(doc.OuterXml);
            var resultado = (from item in element.Elements("solicitud")
                             where (string)item.Element("numero") == codigoTramite
                             select item);

            string _resultado = "";
            foreach (var unNodo in resultado)
            {
                _resultado += unNodo.ToString();
            }

            Xml1.DocumentContent = _resultado;
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        try
        {
            doc.LoadXml(documento);

            XElement element = XElement.Parse(doc.OuterXml);
            var res = (from node in element.Elements("solicitud")
                       where (string)node.Element("fechaEntrega") == calFechaEntrega.SelectedDate.Date.ToShortDateString()
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
        catch (Exception)
        {
            lblMensaje.Text = "Ocurrio un error al aplicar filtro";
        }
    }

    protected void btnQuitar_Click(object sender, EventArgs e)
    {
        try
        {
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
        catch (Exception)
        {
            lblMensaje.Text = "Ocurrio un error al aplicar filtro";
        }
    }
}