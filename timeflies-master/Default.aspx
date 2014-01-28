<%@ Page Language="C#" MasterPageFile="~/TimeFlies.master" AutoEventWireup="true" CodeFile="Default.aspx.cs"
	Inherits="_DefaultPage" Title="Time Flies By - Take a Photo Every Day and Watch Time Fly By" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
	<script>
		// initialize the library with the API key
		FB.init({ apiKey: '<%=FBAppId %>' });

		// fetch the status on load
		FB.getLoginStatus(handleSessionResponse);

		// handle a session response from any of the auth related calls
		function handleSessionResponse(response)
		{
    //debugger;
			// if we dont have a session, just hide the user info
			if (response.status != 'connected') //!response.session
			{
				// alert("Not Connected");
				return;
			}
			else
			{
				//alert(FB.getSession().uid);
				loginUser(FB.getSession().uid); // Facebook Login User Sync
			}
		}

		function loginUser(userId)
		{
			try
			{
				$.get("GetSnapshot.ashx?userid=" + userId + "&flag=LoginUser", loginUserCallback);
			}
			catch (e)
			{
				alert(e);
			}
		}

		function loginUserCallback(data)
		{
			if (data == "Success")
			{
				window.location = "MyVideos.aspx?Camera=Camera&uid=" + FB.getSession().uid;
			}
			else if (data == "Error")
			{
			}
		}
	</script>
	<script src="js/flowplayer-3.2.6.min.js" type="text/javascript"></script>
	<table cellpadding="5" align="center">
		<tr>
			<td align="center">
				<%--An easy way to make one of these:--%>
			</td>
		</tr>
		<tr>
			<td align="center">
				<!-- this A tag is where your Flowplayer will be placed. it can be anywhere -->
				<div id="video-wrapper">
					<a href="contents/home.flv" style="display: block; width: 320px; height: 240px" id="player">
					</a>
				</div>
				<!-- this will install flowplayer inside previous A- tag. -->
				<script>
					flowplayer("player", "common/flowplayer-3.2.6.swf", {
						clip: {			// Clip is an object, hence '{...}'
							autoPlay: false,
							autoBuffering: true
						}
,
						plugins: {
							controls: null
							//           controls: {
							//		url: 'common/flowplayer.controls-3.2.4.swf',
							//		
							//		// which buttons are visible and which are not?
							//		play:true,
							//		volume:false,
							//		mute:false,
							//		time:true,
							//		stop:false,
							//		playlist:false,
							//		fullscreen:false
							//		
							//		// you can also use the "all" flag to disable/enable all controls
							//	}
						}
        ,
						onLoad: function ()
						{	// called when player has finished loading
							this.setVolume(0); // set volume property
						}

					});
				</script>
			</td>
		</tr>
		<tr>
			<td align="center">
				To get started, Log in with Facebook<br />
				<br />
				<img src="images/facebook-connect.png" id="imgfbConnect" runat="server" style="cursor: pointer;" />
			</td>
		</tr>
		<tr>
			<td align="center">
				P.S You'll need a webcam
				<br />
				<br />
				<iframe src="http://www.facebook.com/plugins/facepile.php?app_id=<%=FBAppId %>"
					scrolling="no" frameborder="0" style="border: none; overflow: hidden; width: 435px;
					height: 70px;" allowtransparency="true"></iframe>
			</td>
		</tr>
	</table>
</asp:Content>
