<%@ Page Title="" Language="C#" MasterPageFile="~/TimeFlies.master" AutoEventWireup="true"
	CodeFile="Friends.aspx.cs" Inherits="Friends" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
	<script type="text/javascript">

	</script>
	<table cellspacing="0" align="center" class="tbl">
		<tr>
			<td align="left">
				<a href="./">Home</a> > <a class="breadcrumb">Friends</a>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
			</td>
		</tr>
		<tr>
			<td align="left" style="height: 18px;">
				<asp:DataList ID="dlFriends" runat="server" RepeatColumns="3" CellPadding="13"
					ItemStyle-HorizontalAlign="Center" AlternatingItemStyle-HorizontalAlign="Center"
					AlternatingItemStyle-Width="0px" OnItemDataBound="dlFriends_ItemDataBound">
					<ItemTemplate>
						<div id="imageList">
							<asp:HyperLink runat="server" ID="hl" style="font-size: 12px; text-decoration: none; border: 0;">
								<asp:Image runat="server" ID="img" CssClass="image-wrapper" Width="140" Height="105" />
								<div style="text-decoration: underline; margin-bottom: 10px; margin-left: 0px;">
									<asp:Label runat="server" ID="lblName"></asp:Label>
								</div>
							</asp:HyperLink>
						</div>
						&nbsp;&nbsp;&nbsp;
					</ItemTemplate>
				</asp:DataList>
			</td>
		</tr>
	</table>
</asp:Content>
