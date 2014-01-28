<%@ Page Title="Contact" Language="C#" MasterPageFile="~/TimeFlies.master"	AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
	<table cellspacing="13" align="center" class="tbl">
		<tr>
			<td align="left">
				<a href="Default.aspx">Home</a> > <a class="breadcrumb">Contact</a>
			</td>
		</tr>
		<tr>
			<td align="left">
				<span>Questions?
					<br />
					<br />
					Feature requests?
					<br />
					<br />
					Send us a message and we’ll try to help.</span>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblmsg" runat="server"></asp:Label>
			</td>
		</tr>
		<tr>
			<td valign="top">
				<asp:TextBox TextMode="MultiLine" runat="server" ID="txtFeature" Width="490" Height="180"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td align="right">
				<div style="margin-right: 0px;">
					<asp:Button runat="server" Text="Send" ID="btnSend" OnClick="btnSend_Click" /></div>
			</td>
		</tr>
		<tr id="trFbLogin" runat="server">
			<td align="center">
				<span>Before you can send us a message, you will need to login with Facebook.</span>
			</td>
		</tr>
		<tr id="trFbLogin1" runat="server">
			<td align="center">
				<div style="margin-right: 0px;">
					<img src="images/facebook-connect.png" id="imgfbConnect" runat="server" style="cursor: pointer;" /></div>
			</td>
		</tr>
	</table>
</asp:Content>
