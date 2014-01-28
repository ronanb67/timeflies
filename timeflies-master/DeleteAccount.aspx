<%@ Page Title="" Language="C#" MasterPageFile="~/TimeFlies.master" AutoEventWireup="true"
	CodeFile="DeleteAccount.aspx.cs" Inherits="DeleteAccountPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
	<br />
	<table style="width: 100%">
		<tr>
			<td align="center">
				<asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
			</td>
		</tr>
		<tr>
			<td align="center">
				<asp:LinkButton ID="lbDownload" runat="server" OnClick="lbDownload_Click">Download data</asp:LinkButton>
			</td>
		</tr>
	</table>
	<br />
</asp:Content>
