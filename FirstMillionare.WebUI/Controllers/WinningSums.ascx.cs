using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using FirstMillionare.Domain.Helpers;

namespace FirstMillionare.WebUI.Controllers
{
    public partial class WinningSums : UserControl
    {
        public ListItemCollection Items
        {
            get { return cblWinningSums.Items; }
        }

        public int GetWinningSum()
        {
            for (int level = 0; level < Game.COUNT_OF_LEVELS; level++)
            {
                if (cblWinningSums.Items[(level * Game.COUNT_OF_QUESTION_ON_LEVEL)].Selected == true)
                {
                    string res = cblWinningSums.Items[(level * Game.COUNT_OF_QUESTION_ON_LEVEL)].Text;
                    int from = res.IndexOf(" ");
                    return Int32.Parse(res.Remove(0, from));
                }
            }
            return 0;
        }

        public void Visualize(Game gameContext)
        {
            for (int i = 0; i < gameContext.WinningSums.Length; i++)
            {
                cblWinningSums.Items.Add(new ListItem(String.Format("{0}) {1}", gameContext.WinningSums.Length - i, gameContext.WinningSums[i].ToString())));
            }
        }

        public void DeactiveWinningSums()
        {
            for (int i = 0; i < cblWinningSums.Items.Count; i++)
            {
                cblWinningSums.Items[i].Selected = false;
            }
        }

        public void SetSavedSumColor(string color)
        {
            for (int level = 0; level < Game.COUNT_OF_LEVELS; level++)
            {
                cblWinningSums.Items[(level * Game.COUNT_OF_QUESTION_ON_LEVEL)].Attributes.CssStyle.Add(HtmlTextWriterStyle.Color, color);
            }
        }
    }
}