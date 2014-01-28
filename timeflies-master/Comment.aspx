<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Comment.aspx.cs" Inherits="Comment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<div style="float: left;">
			<b>Comments:</b>
			<br />
			<asp:PlaceHolder ID="phFBComment" runat="server"></asp:PlaceHolder>
			<br />
		</div>
	</div>
	</form>
</body>
</html>
