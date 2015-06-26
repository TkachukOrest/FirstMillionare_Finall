using System;
using System.Web;

namespace FirstMillionare.WebUI.Code
{
    public class GameManager
    {
        #region Properties
        public string NameContext
        {
            get
            {
                string name = (string)HttpContext.Current.Session[SessionKeys.NAME_SESSION_KEY];
                if (name == null)
                {
                    name = "Unknown player";
                    HttpContext.Current.Session[SessionKeys.NAME_SESSION_KEY] = name;
                }
                return name;
            }
            set
            {
                HttpContext.Current.Session[SessionKeys.NAME_SESSION_KEY] = value;
            }
        }

        public string ThemeContext
        {
            get
            {
                string theme = (string)HttpContext.Current.Session[SessionKeys.THEME_SESSION_KEY];
                if (theme == null)
                {
                    theme = "MainTheme";
                    HttpContext.Current.Session[SessionKeys.THEME_SESSION_KEY] = theme;
                }
                return theme;
            }
            set
            {
                HttpContext.Current.Session[SessionKeys.THEME_SESSION_KEY] = value;
            }
        }
        #endregion
    }
}