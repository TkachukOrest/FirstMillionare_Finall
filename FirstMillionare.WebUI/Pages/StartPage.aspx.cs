using System;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using FirstMillionare.WebUI.Code;

namespace FirstMillionare.WebUI.Pages
{
    public partial class StartPage : System.Web.UI.Page
    {
        #region Consts
        private const string DDL_THEMES_ID_KEY = "ddlThemesIDKey";
        #endregion

        #region Events
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string uniqueId = Request[DDL_THEMES_ID_KEY];

                if (uniqueId != null && Request[uniqueId] != null)
                {
                    Page.Theme = Request[uniqueId];
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClientScript.RegisterHiddenField(DDL_THEMES_ID_KEY, ddlThemes.UniqueID);
                VisualizeThemes();
            }
        }

        protected void btnStartGame_Click(object sender, EventArgs e)
        {
            if (tbPlayerName.Text.Length == 0 || tbPlayerName.Text.Length > 10)
            {
                Response.Redirect("StartPage.aspx");
            }
            else
            {
                GameManager manager = new GameManager();
                manager.NameContext = tbPlayerName.Text;
                manager.ThemeContext = ddlThemes.SelectedItem.Text;

                Response.Redirect("MainPage.aspx");
            }
        }
        #endregion

        #region Helpers
        protected void VisualizeThemes()
        {
            var directories = Directory.GetDirectories(Server.MapPath("~/App_Themes/"));
            foreach (var folder in directories)
            {
                ddlThemes.Items.Add(new ListItem(folder.Substring(folder.LastIndexOf("\\") + 1)));
            }
        }
        #endregion
    }
}