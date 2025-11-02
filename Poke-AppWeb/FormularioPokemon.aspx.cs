using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace Poke_AppWeb
{
    public partial class FormularioPokemon : System.Web.UI.Page
    {
        public bool ConfirmaEliminacion { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false;
            ConfirmaEliminacion = false;
            try
            {
                if (!IsPostBack)
                {
                    ElementoNegocio negocio = new ElementoNegocio();
                    List<Elemento> lista = negocio.listar();

                    ddlTipo.DataSource = lista;
                    ddlTipo.DataTextField = "Descripcion";
                    ddlTipo.DataValueField = "Id";
                    ddlTipo.DataBind();

                    ddlDebilidad.DataSource = lista;
                    ddlDebilidad.DataTextField = "Descripcion";
                    ddlDebilidad.DataValueField = "Id";
                    ddlDebilidad.DataBind();
                }

                string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
                if (id != "" && !IsPostBack)
                {
                    PokemonNegocio negocio = new PokemonNegocio();
                    List<Pokemon> lista = negocio.listar(id);
                    Pokemon seleccionado = lista[0];
                    Session["PokemonSeleccionado"] = seleccionado;

                    txtId.Text = id;
                    txtNumero.Text = seleccionado.Numero.ToString();
                    txtNombre.Text = seleccionado.Nombre;
                    txtDescripcion.Text = seleccionado.Descripcion;
                    txtUrlImagen.Text = seleccionado.UrlImagen;
                    ddlTipo.SelectedValue = seleccionado.Tipo.Id.ToString();
                    ddlDebilidad.SelectedValue = seleccionado.Debilidad.Id.ToString();
                    imgPokemon.ImageUrl = seleccionado.UrlImagen;
                    if (!seleccionado.Activo)
                        btnInactivar.Text = "Reactivar";
                }
            }
            catch (Exception ex)
            {

                Session.Add("Error", ex);
                throw;
                //rediccion pantalla
            }
        }

        protected void txtUrlImagen_TextChanged(object sender, EventArgs e)
        {
            imgPokemon.ImageUrl = txtUrlImagen.Text;

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Pokemon pokemon = new Pokemon();
                PokemonNegocio negocio = new PokemonNegocio();

                pokemon.Numero = int.Parse(txtNumero.Text);
                pokemon.Nombre = txtNombre.Text;
                pokemon.Descripcion = txtDescripcion.Text;
                pokemon.UrlImagen = txtUrlImagen.Text;
                pokemon.Tipo = new Elemento();
                pokemon.Tipo.Id = int.Parse(ddlTipo.SelectedValue);
                pokemon.Debilidad = new Elemento();
                pokemon.Debilidad.Id = int.Parse(ddlDebilidad.SelectedValue);

                if (Request.QueryString["id"] != null)
                {
                    pokemon.Id = int.Parse(txtId.Text);
                    negocio.modificarConSP(pokemon);
                }
                else
                    negocio.agregarConSP(pokemon);

                Response.Redirect("PokemonLista.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
                throw;
                //rediccion pantalla
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ConfirmaEliminacion = true;
        }

        protected void btnConfirmaEliminacion_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkConfirmarEliminacion.Checked)
                {
                    PokemonNegocio pokemonNegocio = new PokemonNegocio();
                    pokemonNegocio.eliminar(int.Parse(txtId.Text));
                    Response.Redirect("PokemonLista.aspx", false);
                }
                
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
                throw;
                //rediccion pantalla
            }
        }

        protected void btnInactivar_Click(object sender, EventArgs e)
        {
            try
            {
                PokemonNegocio pokemonNegocio = new PokemonNegocio();
                Pokemon seleccionado = (Pokemon)Session["PokemonSeleccionado"];

                pokemonNegocio.eliminarLogico(seleccionado.Id, !seleccionado.Activo);
                Response.Redirect("PokemonLista.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
                throw;
                //rediccion pantalla
            }
        }
    }
}