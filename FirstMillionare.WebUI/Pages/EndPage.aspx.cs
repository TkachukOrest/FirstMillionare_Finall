using System;
using FirstMillionare.WebUI.Code;

namespace FirstMillionare.WebUI.Pages
{
    public partial class EndPage : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            GameManager manager = new GameManager();
            Page.Theme = manager.ThemeContext;
        }      
    }
}