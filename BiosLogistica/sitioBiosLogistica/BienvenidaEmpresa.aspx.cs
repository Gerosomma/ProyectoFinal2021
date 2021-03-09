using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wcfLogistica;

public partial class BienvenidaEmpresa : System.Web.UI.Page
{
    private Empresa usuarioLogueado = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            usuarioLogueado = (Empresa)Session["Usuario"];
            lblUsuario.Text = usuarioLogueado.NombreCompleto;
        }
        catch (Exception)
        {
            lblMensaje.Text = "Ocurrio un error al cargar la pagina.";
        }
    }
}