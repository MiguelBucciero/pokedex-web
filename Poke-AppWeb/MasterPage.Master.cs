using Microsoft.Win32;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace Poke_AppWeb
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnCerrarSession.Visible = false;
            }
            if(!(Page is Login || Page is Default || Page is WebForm1 || Page is Error))
            {
                if (!Seguridad.sessionActiva(Session["trainee"]))
                {
                    Response.Redirect("Login.aspx", false);
                }
            }
            if (Session["trainee"] != null)
            {
                btnlogin.Visible = false;
                btnRegistrarse.Visible = false;
                btnCerrarSession.Visible = true;
            }
            if (Seguridad.sessionActiva(Session["trainee"]))
            {
                imgPerfil.ImageUrl = "~/Images/" + ((Trainee)Session["trainee"]).ImagenPerfil;
            }
            else
            {
                imgPerfil.ImageUrl = "Images/ImgDefault.png";
            }



        }
        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registro.aspx");
        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void btnCerrarSession_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("/", false);
        }
    }
}