<%@ Page Title="Перший мільйонер: 2" Language="C#" MasterPageFile="~/FirstMillionareMaster.Master" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="FirstMillionare.WebUI.MainPage" %>

<%@ Register Src="~/Controllers/WinningSums.ascx" TagPrefix="uc1" TagName="WinningSums" %>


<asp:Content ID="HeadContent" ContentPlaceHolderID="cphHead" runat="server">   
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="cphMain" runat="server">
    <div class="header">
        <div class="title-name">Перший мільйонер</div>

        <div class="buttons">
            <asp:ImageButton ID="btnCallTip" CssClass="img-button" ImageUrl="/Resources/call.png" runat="server" OnClick="btnCallTip_Click"></asp:ImageButton>
            <asp:ImageButton ID="btnHalfByHalfTip" CssClass="img-button" ImageUrl="/Resources/half.png" runat="server" OnClick="btnHalfTip_Click"></asp:ImageButton>
            <asp:ImageButton ID="btnGoogleTip" CssClass="img-button" ImageUrl="/Resources/google.png" runat="server" OnClick="btnGoogleTip_Click" OnClientClick="aspnetForm.target ='_blank';"></asp:ImageButton>
        </div>
    </div>
    <div class="game-field">
        <asp:Label ID="lblQuestion" runat="server" Text="Label"></asp:Label>

        <div class="rblOptions">
            <asp:RadioButtonList ID="rblOptions" runat="server" RepeatColumns="2">
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
            </asp:RadioButtonList>
        </div>

        <p>
            <asp:Label ID="lblMistake" runat="server" Text="Label"></asp:Label>
        </p>
        <asp:Button ID="btnRestart" CssClass="pro-button" runat="server" OnClick="btnRestart_Click" Text="Нова гра" />
         
        <asp:Button ID="btnEnd" CssClass="pro-button" runat="server" OnClick="btnEnd_Click" Text="Закінчити" />

        <asp:Button ID="btnResponse" CssClass="pro-button" runat="server" OnClick="btnResponse_Click" Text="Відповісти" />
        <br />
        <br />

        <asp:Panel ID="pnlEmailPanel" runat="server">
            <div class="footer">
                <asp:Label ID="lblPlayerEmail" runat="server" Text="Ваш e-mail:"></asp:Label><br />
                <asp:TextBox ID="tbPlayerEmail" runat="server"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="revPlayerEmail" runat="server" ErrorMessage="Введіть коректний email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="tbPlayerEmail"></asp:RegularExpressionValidator><br />
                <asp:Label ID="lblFriendEmail" runat="server" Text="Друга e-mail:"></asp:Label><br />
                <asp:TextBox ID="tbFriendEmail" runat="server"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="revFriendEmail" runat="server" ErrorMessage="Введіть коректний email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="tbFriendEmail"></asp:RegularExpressionValidator><br />
                <asp:Button ID="btnSendEmail" CssClass="pro-button" runat="server" OnClick="btnSendEmail_Click" Text="Відправити підказку" />
            </div>
        </asp:Panel>
    </div>
    <uc1:WinningSums runat="server" id="ucWinningSums" />
</asp:Content>
