using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JyC_Exterior.NegocioPlantilla
{
    public partial class MenuPlantillaPrincipal : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Literal cssLink = new Literal();
                cssLink.Text = "<link href= '../Styles/MenuPlantilla.css' rel='stylesheet' type='text/css' />";
                Page.Header.Controls.Add(cssLink);

            }

        }
    }
}
