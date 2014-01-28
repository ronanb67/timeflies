<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginWelcome.ascx.cs"
	Inherits="UserControls_LoginWelcome" %>

<asp:Panel ID="Panel2" runat="server">
	<div style="margin-top: -60px; margin-right: 4px; float: right;">
		<asp:Button runat="server" ID="btnLogout" CssClass="btnLogout" OnClick="btnLogout_Click" />
		<asp:HyperLink ID="lnkLoginName" runat="server" Style="font-size: 13px;" Font-Bold="true" ForeColor="Black"></asp:HyperLink>
		&nbsp;
		<a id="logout" onclick="fbLogout();" class="LogoutLink">Log Out</a>
	</div>
</asp:Panel>
