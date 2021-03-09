using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wcfLogistica;

public partial class BienvenidaEmpleado : System.Web.UI.Page
{
    private Empleado usuarioLogueado = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            usuarioLogueado = (Empleado)Session["Usuario"];
            lblUsuario.Text = usuarioLogueado.NombreCompleto;
        }
        catch (Exception)
        {
            lblMensaje.Text = "Ocurrio un error al cargar la pagina.";
        }
    }
}