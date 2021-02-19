using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EntidadesCompartidas;
using Logica;

public partial class AltaPaquetes : System.Web.UI.Page
{

    Empleado usuarioLogueado;
    List<Empresa> empresas;
    protected void Page_Load(object sender, EventArgs e)
    {
        ((Label)this.Master.FindControl("lblPagina")).Text = "Alta de paquetes";
        usuarioLogueado = (Empleado)Session["Usuario"];

        lbxEmpresa.Items.Clear();

        try
        {
            empresas = FabricaLogica.GetLogicaUsuario().ListarEmpresas(usuarioLogueado);

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

    protected void btnAlta_Click(object sender, EventArgs e)
    {
        int codigo;
        string tipo;
        string descripcion;
        double peso;
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
            peso = Convert.ToDouble(txtPeso.Text);
        }
        catch (Exception)
        {
            lblMensaje.Text = "Ingrese un peso válido.";

            return;
        }

        empresa = (Empresa)FabricaLogica.GetLogicaUsuario().BuscarUsuario(lbxEmpresa.SelectedValue, usuarioLogueado);

        paquete = new Paquete(codigo, tipo, descripcion, peso, empresa);

        FabricaLogica.GetLogicaPaquete().AltaPaquete(paquete, usuarioLogueado);
    }
}