using System;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FirstMillionare.Domain.Concrete;
using FirstMillionare.Domain.Abstract;
using FirstMillionare.Domain.Entities;
using FirstMillionare.Domain.Helpers;
using FirstMillionare.WebUI.Code;

namespace FirstMillionare.WebUI
{
    public partial class MainPage : System.Web.UI.Page
    {
        #region Properties
        private IMillionareRepository RepositoryContext
        {
            get
            {
                IMillionareRepository repository = (EFMillionareRepository)HttpContext.Current.Session[SessionKeys.REPOSITORY_SESSION_KEY];
                if (repository == null)
                {
                    repository = new EFMillionareRepository();
                    HttpContext.Current.Session[SessionKeys.REPOSITORY_SESSION_KEY] = repository;
                }
                return repository;
            }
        }
        private Game GameContext
        {
            get
            {
                Game game = (Game)HttpContext.Current.Session[SessionKeys.GAME_SESSION_KEY];
                if (game == null)
                {
                    game = new Game(RepositoryContext);
                    HttpContext.Current.Session[SessionKeys.GAME_SESSION_KEY] = game;
                }
                return game;
            }
            set
            {
                HttpContext.Current.Session[SessionKeys.GAME_SESSION_KEY] = value;
            }
        }
        private QuestionItem QuestionContext
        {
            get
            {
                QuestionItem question = (QuestionItem)HttpContext.Current.Session[SessionKeys.QUESTION_SESSION_KEY];
                if (question == null)
                {
                    question = new QuestionItem();
                    HttpContext.Current.Session[SessionKeys.QUESTION_SESSION_KEY] = question;
                }
                return question;
            }
            set
            {
                HttpContext.Current.Session[SessionKeys.QUESTION_SESSION_KEY] = value;
            }
        }
        #endregion

        #region Form events
        protected void Page_PreInit(object sender, EventArgs e)
        {
            GameManager manager = new GameManager();
            Page.Theme = manager.ThemeContext;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetVisibilityOfEmailForm(false);
                VisualizeWinningSums();
                GetNewQuestion();
            }
        }

        protected void btnResponse_Click(object sender, EventArgs e)
        {
            string res = rblOptions.SelectedItem == null ? "" : rblOptions.Text;

            if (QuestionContext.Answer != res)
            {
                IncorectAnswer(res);
            }
            else
            {
                CorrectAnswer(res);
            }
        }

        protected void btnRestart_Click(object sender, EventArgs e)
        {
            GameContext = new Game(RepositoryContext);
            GetNewQuestion();

            DeactiveWinningSums();

            btnCallTip.Enabled = true;
            btnHalfByHalfTip.Enabled = true;
            rblOptions.Enabled = true;
            btnResponse.Enabled = true;
            btnGoogleTip.Enabled = true;
        }

        protected void btnEnd_Click(object sender, EventArgs e)
        {
            Response.Redirect("EndPage.aspx");
        }

        protected void btnCallTip_Click(object sender, EventArgs e)
        {
            SetVisibilityOfEmailForm(true);
        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            SetVisibilityOfEmailForm(false);

            try
            {
                EmailSettings settings = new EmailSettings(tbFriendEmail.Text, tbPlayerEmail.Text, "incorrect password");
                ICallTipProcessor processor = new EmailTipProcessor(settings);
                processor.ProcessQuestion(QuestionContext);

                lblMistake.Text = "Повідомлення надіслано! Чекайте відповіді";
                lblMistake.ForeColor = Color.DarkGreen;
                lblMistake.Visible = true;

                btnCallTip.Enabled = false;
            }
            catch (Exception ex)
            {
                lblMistake.Text = "Повідомлення не надіслано! ;(";
                lblMistake.ForeColor = Color.Red;
                lblMistake.Visible = true;
            }
        }

        protected void btnHalfTip_Click(object sender, EventArgs e)
        {
            int halfCount = rblOptions.Items.Count / 2;
            for (int i = 0; i < rblOptions.Items.Count; i++)
            {
                if ((rblOptions.Items[i].ToString() != QuestionContext.Answer) && ((halfCount - 1) >= 0))
                {
                    halfCount--;
                    QuestionContext.Options[i].OptionText = "";
                    VisualizeQuestion(QuestionContext);
                }
            }
            btnHalfByHalfTip.Enabled = false;
        }

        protected void btnGoogleTip_Click(object sender, ImageClickEventArgs e)
        {
            ClientScript.RegisterStartupScript(this.Page.GetType(), "",
                                                "window.open('https://www.google.com.ua/','Graph','height=400,width=500');",
                                                true);
            btnGoogleTip.Enabled = false;
        }
        #endregion

        #region Methods
        private void VisualizeWinningSums()
        {
            ucWinningSums.Visualize(GameContext);
        }

        private void DeactiveWinningSums()
        {
           ucWinningSums.DeactiveWinningSums();
        }

        private void GetNewQuestion()
        {
            QuestionContext = GameContext.GetQuestion();
            VisualizeQuestion(QuestionContext);
        }

        private void VisualizeQuestion(QuestionItem question)
        {
            SetVisibilityOfEmailForm(false);

            lblMistake.Visible = false;
            btnRestart.Visible = false;
            btnEnd.Visible = false;

            lblQuestion.Text = String.Format("{0}) {1}", GameContext.CurrentQuestion, question.QuestionText);

            rblOptions.Items.Clear();
            foreach (var option in question.Options)
            {
                rblOptions.Items.Add(new ListItem(option.OptionText, option.OptionText, true));
            }

            //add css to win sum list
            ucWinningSums.SetSavedSumColor("#2DCDD8");
        }

        private void IncorectAnswer(string res)
        {
            if (GameContext.Lives-- > 0)
            {
                lblMistake.ForeColor = Color.Red;
                lblMistake.Text = "Помилка! Правильна відповідь була: " + QuestionContext.Answer;

                GetNewQuestion();

                lblMistake.Visible = true;
            }
            else
            {
                EndGame();
            }
        }

        private void CorrectAnswer(string res)
        {
            if (GameContext.CurrentQuestion < Game.COUNT_OF_QUESTION_ON_LEVEL * Game.COUNT_OF_LEVELS)
            {
                ucWinningSums.Items[ucWinningSums.Items.Count - GameContext.CurrentQuestion].Selected = true;
                lblMistake.ForeColor = Color.DarkGreen;
                lblMistake.Text = "Вірно! Відповідь була: " + QuestionContext.Answer;

                GetNewQuestion();

                lblMistake.Visible = true;
            }
            else
            {
                ucWinningSums.Items[0].Selected = true;
                WinGame();
            }
        }

        private void EndGame()
        {
            rblOptions.Enabled = false;
            btnResponse.Enabled = false;

            lblMistake.Text = String.Format("{0}, Ви програли! Виграшна сума: {1}", (new GameManager().NameContext), GetWinningSum());
            lblMistake.ForeColor = Color.Red;
            lblMistake.Visible = true;

            btnRestart.Visible = true;
            btnEnd.Visible = true;
            btnHalfByHalfTip.Enabled = false;
            btnCallTip.Enabled = false;
            btnGoogleTip.Enabled = false;
        }

        private void WinGame()
        {
            rblOptions.Enabled = false;
            btnResponse.Enabled = false;

            lblMistake.Text = String.Format("{0}, Ви виграли! Виграшна сума:{1}",(new GameManager().NameContext), GetWinningSum());
            lblMistake.ForeColor = Color.DarkGreen;
            lblMistake.Visible = true;

            btnRestart.Visible = true;
            btnEnd.Visible = true;
        }

        private int GetWinningSum()
        {
            return ucWinningSums.GetWinningSum();
        }

        private void SetVisibilityOfEmailForm(bool visible)
        {
            pnlEmailPanel.Visible = visible;
            lblFriendEmail.Visible = visible;
            lblPlayerEmail.Visible = visible;
            tbFriendEmail.Visible = visible;
            tbPlayerEmail.Visible = visible;
        }
        #endregion       
    }
}