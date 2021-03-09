using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wcfLogistica;

public partial class AltaPaquetes : System.Web.UI.Page
{

    Empleado usuarioLogueado;
    List<Empresa> empresas;
    protected void Page_Load(object sender, EventArgs e)
    {
        usuarioLogueado = (Empleado)Session["Usuario"];
        
        if (!IsPostBack)
        {
            try
            {
                ServiceClient wcf = new ServiceClient();
                empresas = wcf.ListarEmpresas(usuarioLogueado).ToList<Empresa>();

                foreach (Empresa emp in empresas)
                {
                    lbxEmpresa.Items.Add(new ListItem(emp.NombreCompleto, emp.Logueo));
                }
            }
            catch (Exception)
            {
                lblMensaje.Text = "Ocurrió un error al listar las empresas";

                return;
            }
        }                        
    }

    protected void btnAlta_Click(object sender, EventArgs e)
    {
        int codigo;
        string tipo;
        string descripcion;
        decimal peso;
        Empresa empresa;
        Paquete paquete;

        try
        {
            codigo = Convert.ToInt32(txtCodigo.Text);
        }
        catch
        {
            lblMensaje.Text = "Ingrese un código válido.";

            return;
        }

        tipo = ddlTipo.SelectedValue;

        if (!string.IsNullOrWhiteSpace(txtDescripcion.Text))
        {
            descripcion = txtDescripcion.Text;
        }
        else
        {
            lblMensaje.Text = "Debe ingresar una descripción";

            return;
        }

        try
        {
            peso = Convert.ToDecimal(txtPeso.Text);
        }
        catch (FormatException)
        {
            lblMensaje.Text = "Ingrese un peso válido.";

            return;
        }

        try
        {
            ServiceClient wcf = new ServiceClient();
            empresa = (Empresa)wcf.BuscarUsuario(lbxEmpresa.SelectedValue, usuarioLogueado);

            paquete = new Paquete();
            paquete.Codigo = codigo;
            paquete.Tipo = tipo;
            paquete.Descripcion = descripcion;
            paquete.Peso = peso;
            paquete.EmpresaOrigen = empresa;
            
            wcf.AltaPaquete(paquete, usuarioLogueado);

            LimpiarFormulario();
            lblMensaje.Text = "Paquete agregado con éxito";
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;

            return;
        }
    }

    protected void LimpiarFormulario()
    {
        txtCodigo.Text = string.Empty;
        txtCodigo.Focus();
        ddlTipo.SelectedIndex = 0;
        txtPeso.Text = string.Empty;
        txtDescripcion.Text = string.Empty;
        lbxEmpresa.SelectedIndex = -1;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        LimpiarFormulario();
    }
}