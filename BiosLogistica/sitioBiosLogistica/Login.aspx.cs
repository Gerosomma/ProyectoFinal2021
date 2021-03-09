using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wcfLogistica;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Session["Usuario"] = null;
        }
        catch (Exception ex)
        {
            lblError.Text = "Error: " + ex.Message;
        }
    }

    protected void btnLog_Click(object sender, EventArgs e)
    {
        try
        {
            string _usuario = txtUsuario.Text;
            string _contrasenia = txtContrasena.Text;

            ServiceClient wcf = new ServiceClient();
            Usuario usLog = wcf.LogueoUsuario(_usuario, _contrasenia);
            if (usLog == null)
            {
                lblError.Text = "Usuario o Pass Invalidos";
            }
            else
            {
                if (usLog is Empleado)
                {
                    Session["Usuario"] = usLog;
                    Response.Redirect("~/BienvenidaEmpleado.aspx");
                }
                else
                {
                    Session["Usuario"] = usLog;
                    Response.Redirect("~/BienvenidaEmpresa.aspx");
                }
                
            }
        }
        catch (FormatException)
        {
            lblError.Text = "Documento invalido.";
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void txtContrasena_TextChanged(object sender, EventArgs e)
    {
        btnLog_Click(sender, e);
    }
}