<%@ Page Language="C#" MasterPageFile="~/TimeFlies.master" AutoEventWireup="true" CodeFile="Home.aspx.cs"
	Inherits="home" Title="TimeFlies" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
	<table class="mainContent" cellpadding="15">
		<tr>
			<td align="center">
				An easy way to make one of these:
			</td>
		</tr>
		<tr>
			<td align="center">
				<iframe title="YouTube video player" class="youtube-player" type="text/html" width="400"
					height="330" src="http://www.youtube.com/embed/p_ci-keXmn4" frameborder="0" allowfullscreen>
				</iframe>
			</td>
		</tr>
		<tr>
			<td align="center">
				To get started, Sign up or Login with Facebook
			</td>
		</tr>
		<tr>
			<td align="center">
				<asp:ImageButton ID="btn" runat="server" ImageUrl="Common/Resources/fb.jpg" />
			</td>
		</tr>
	</table>
</asp:Content>
