<%@ Page Title="Перший мільйонер: 1" Language="C#" MasterPageFile="~/FirstMillionareMaster.Master" AutoEventWireup="true" CodeBehind="StartPage.aspx.cs" Inherits="FirstMillionare.WebUI.Pages.StartPage" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="cphMain" runat="server">
    <div class="header">
        <div class="title-name" style="width: 95%">Перший мільйонер: Старт</div>
    </div>

    <div class="game-field" style="width: 95%;">
        <asp:Panel ID="pnlEmailPanel" runat="server">
            <div class="footer">
                <asp:Label ID="lblPlayerName" runat="server" Text="Вашe ім'я:"></asp:Label><br />
                <asp:TextBox ID="tbPlayerName" runat="server" SkinID="TextBoxPlayerName"></asp:TextBox><br />
                <br />
                <asp:DropDownList ID="ddlThemes" runat="server" AutoPostBack="True" SkinID="DropDownThemes"/><br />

                <asp:RequiredFieldValidator ID="revPlayerNameRequired" runat="server"
                    ErrorMessage="Введіть ім'я" ControlToValidate="tbPlayerName"
                    ForeColor="Red" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="revPlayerNameLenght" runat="server"
                    ErrorMessage="Введіть коректне ім'я" ForeColor="Red"
                    ValidationExpression="\w{1,10}" ControlToValidate="tbPlayerName"
                    Display="Dynamic" /><br />

                <asp:Button ID="btnStartGame" SkinID="ProButton" runat="server" Text="Почати гру" OnClick="btnStartGame_Click" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>
