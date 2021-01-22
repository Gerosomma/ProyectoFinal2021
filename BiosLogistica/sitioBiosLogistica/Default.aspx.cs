using EntidadesCompartidas;
using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

public partial class _Default : System.Web.UI.Page
{
    static string documento = "";
    static XmlDocument doc = new XmlDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        Usuario us = (Usuario)Session["Usuario"];
        if (us != null)
        {
            try
            {

                lblUsuario.Text = "Bienvenido! " + us.Logueo;

                documento = FabricaLogica.GetLogicaSolicitud().listadoSolicitudes(us);
                doc.LoadXml(documento);

                XElement element = XElement.Parse(doc.OuterXml);
                var res = (from node in element.Elements("Solicitud")
                           select new
                           {
                               Nombre = node.Elements("nombre").First().Value
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
        else
        {
            lblMensaje.Text = "Logueese para ver solicitudes pendientes de entrega";
        }
    }
}