<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CallbackFB.aspx.cs" Inherits="CallbackFB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>Connected to Facebook</title>
</head>
<body>
	<form id="form1" runat="server">

	      <div id="pnlFBLoginError" runat="server" visible="false">
        <center>
          <table border="0" cellpadding="0" cellspacing="0"> 
            <tr>
              <td><img src="Images/facebook-button.jpg" alt="Facebook error" style="display:inherit;"  /></td>
              <td>
                <div style="padding:2px;border:solid 1px #6f87b3;">
                  <span style="color:#fff;background:#3c5a98;margin:2px;" id="pnlErrMsg" runat="server"></span>
                </div>            
               </td>
             </tr>
          </table>    
          <input type="button"  onclick="window.close();" value='Close' class="btn" />
        </center>
      </div>   
      <div id="pnlFacebookLogged" runat="server" visible="false">
        <script type="text/javascript">
        	try
        	{
        		window.opener.location = '<%=GetUserHomeUrl()%>';
        	}
        	catch (e) { }
        	// Prevent IE warn message.
        	window.open('', '_top');
        	window.close();
        </script> 
      </div>

<!--
	<table width="100%">
		<tr>
			<td align="center">
				<img src="Images/1286031789_facebook.png" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblMsg" runat="server"></asp:Label>
			</td>
		</tr>
		<tr>
			<td align="center">
				<div onclick="closeme();" style="cursor: pointer;">
					Close window
				</div>
			</td>
		</tr>
	</table>-->
	</form>
</body>
</html>
