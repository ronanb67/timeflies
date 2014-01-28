<%@ Page Title="Settings" Language="C#" MasterPageFile="~/TimeFlies.master"	AutoEventWireup="true" CodeFile="Setting.aspx.cs" Inherits="SettingPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
	<script type="text/javascript" language="javascript">
		function zip()
		{
			try
			{
				$("#divProgress").html("<img src=\"images/progress-indicator.gif\" id=\"progress\"  />");
				$.get("GetSnapshot.ashx?userid=<%=UserId %>&flag=zip", zipCallback);
			}
			catch (e)
			{
				alert(e);
			}
		}

		function zipCallback(data)
		{
			$("#divProgress").html("");
			
			if (data == "Success")
			{
				$("#<%=lblmsg.ClientID %>").removeClass('error');
				$("#<%=lblmsg.ClientID %>").addClass('success');
				$("#<%=lblmsg.ClientID %>").html('Thank you, an email has been sent to your account.');
			}
			else if (data == "Error")
			{
				$("#<%=lblmsg.ClientID %>").addClass('error');
				$("#<%=lblmsg.ClientID %>").html('Error occur while processing your request');
			}
			else if (data == "NoImage")
			{
				$("#<%=lblmsg.ClientID %>").addClass('warning');
				$("#<%=lblmsg.ClientID %>").html('Ops, there is no image to zip');
			}
			else if (data == "SessionOut")
			{
				parent.location = "Default.aspx";
			}
		}

		function deleteAccount()
		{
			try
			{
				$("#DeleteProgress").html("<img src=\"images/progress-indicator.gif\" id=\"progress\"  />");
				$.get("GetSnapshot.ashx?userid=<%=UserId %>&flag=DeleteAccount", deleteCallback);
			}
			catch (e)
			{
				alert(e);
			}
		}

		function deleteCallback(data)
		{
			$("#DeleteProgress").html("");
			if (data == "Success")
			{
				$("#<%=lblmsg.ClientID %>").removeClass('error');
				$("#<%=lblmsg.ClientID %>").addClass('success');
				$("#<%=lblmsg.ClientID %>").html('Thank you, an email has been sent to your account.');
			}
			else if (data == "Error")
			{
				$("#<%=lblmsg.ClientID %>").addClass('error');
				$("#<%=lblmsg.ClientID %>").html('Error occur while processing your request');
			}
		}

	</script>
	<table cellspacing="0" cellpadding="0" align="left" class="tbl">
		<tr>
			<td>
				<asp:Label runat="server" ID="lblmsg" Visible="true"></asp:Label>
			</td>
		</tr>
		<tr>
			<td align="left">
				<asp:ImageButton runat="server" ID="imgOnOf" OnClick="imgOnOf_Click" />
				<div style="position: relative; margin-left: 109px; margin-top: -21px;">
					Daily Email Reminders</div>
			</td>
		</tr>
		<tr>
			<td align="left">
				<br />
				<b>Share Video</b>
			</td>
		</tr>
		<tr>
			<td align="left">
				<asp:RadioButtonList runat="server" ID="rdoPublish" AutoPostBack="true" OnSelectedIndexChanged="rdoPublish_SelectedIndexChanged">
					<asp:ListItem Selected="True" Value="Private">Share with nobody</asp:ListItem>
					<asp:ListItem Value="PublicFriends">Share with my friends only </asp:ListItem>
					<asp:ListItem Value="Public">Share with anybody in the world who has the link  </asp:ListItem>
				</asp:RadioButtonList>
			</td>
		</tr>
		<tr>
			<td align="left">
				<br />
				<a onclick="zip();" style="cursor: pointer;">Download my images</a> <span style="font-size: 11px;">
					(We will send you an email with further instructions)</span>
			</td>
		</tr>
		<tr>
			<td align="center">
				<div id="divProgress">
				</div>
			</td>
		</tr>
		<tr>
			<td align="left">
				<br />
				<a onclick="deleteAccount();" style="cursor: pointer;">Delete My Account</a><span
					style="font-size: 11px;"> (We will send you an email with further instructions)</span>
			</td>
		</tr>
		<tr>
			<td align="center">
				<div id="DeleteProgress">
				</div>
			</td>
		</tr>
	</table>
</asp:Content>
