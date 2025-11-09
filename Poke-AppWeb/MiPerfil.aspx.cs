using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Poke_AppWeb
{
    public partial class MiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["Trainee"] != null)
                {
                    Trainee user = (Trainee)Session["Trainee"];
                    txtNombre.Text = user.Nombre;
                    txtApellido.Text = user.Apellido;
                    txtFechaNacimiento.Text = user.FechaNacimiento.ToString("yyyy-MM-dd");
                    imgNuevoPerfil.ImageUrl = "./Images/" + user.ImagenPerfil;

                }
            }



        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                TraineeNegocio negocio = new TraineeNegocio();

                string ruta = Server.MapPath("./Images/");
                Trainee user = (Trainee)Session["trainee"];
                txtImagen.PostedFile.SaveAs(ruta + "perfil-"+ user.Id + ".jpg" );

                user.ImagenPerfil = "perfil-" + user.Id + ".jpg";
                user.Nombre = txtNombre.Text;
                user.Apellido = txtApellido.Text;
                user.FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);
                negocio.Actualizar(user);

                imgNuevoPerfil.ImageUrl = "./Images/" + user.ImagenPerfil;
                Image img =(Image)Master.FindControl("imgPerfil");
                img.ImageUrl = "~/Images/" + user.ImagenPerfil;
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }
    }
}