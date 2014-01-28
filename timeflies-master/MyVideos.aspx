<%@ Page Title="My Video" Language="C#" MasterPageFile="~/TimeFlies.master"
	AutoEventWireup="true" CodeFile="MyVideos.aspx.cs" Inherits="MyVideos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
	<html>
	<head>
		<script src="js/swfobject.js" type="text/javascript"></script>
		<script type="text/javascript">
			$(document).ready(function ()
			{
				showPlayer();
				$("#divOne").slider({
					value: 15,
					range: "min",
					min: 1,
					max: 15,

					slide: function (event, ui)
					{
						switch (ui.value)
						{
							case 1: $("#speedImage").css({ backgroundImage: 'url(images/r_1.png)' }); break;
							case 2: $("#speedImage").css({ backgroundImage: 'url(images/r_2.png)' }); break;
							case 3: $("#speedImage").css({ backgroundImage: 'url(images/r_3.png)' }); break;
							case 4: $("#speedImage").css({ backgroundImage: 'url(images/r_4.png)' }); break;
							case 5: $("#speedImage").css({ backgroundImage: 'url(images/r_5.png)' }); break;
							case 6: $("#speedImage").css({ backgroundImage: 'url(images/r_6.png)' }); break;
							case 7: $("#speedImage").css({ backgroundImage: 'url(images/r_7.png)' }); break;
							case 8: $("#speedImage").css({ backgroundImage: 'url(images/r_8.png)' }); break;
							case 9: $("#speedImage").css({ backgroundImage: 'url(images/r_9.png)' }); break;
							case 10: $("#speedImage").css({ backgroundImage: 'url(images/r_10.png)' }); break;
							case 11: $("#speedImage").css({ backgroundImage: 'url(images/r_11.png)' }); break;
							case 12: $("#speedImage").css({ backgroundImage: 'url(images/r_12.png)' }); break;
							case 13: $("#speedImage").css({ backgroundImage: 'url(images/r_13.png)' }); break;
							case 14: $("#speedImage").css({ backgroundImage: 'url(images/r_14.png)' }); break;
							case 15: $("#speedImage").css({ backgroundImage: 'url(images/r_15.png)' }); break;
						}
					},
					change: function (event, ui)
					{

						switch (ui.value)
						{
							case 1: $("#speedImage").css({ backgroundImage: 'url(images/r_1.png)' }); SetPlayInterval(1000); break;
							case 2: $("#speedImage").css({ backgroundImage: 'url(images/r_2.png)' }); SetPlayInterval(910); break;
							case 3: $("#speedImage").css({ backgroundImage: 'url(images/r_3.png)' }); SetPlayInterval(840); break;
							case 4: $("#speedImage").css({ backgroundImage: 'url(images/r_4.png)' }); SetPlayInterval(770); break;
							case 5: $("#speedImage").css({ backgroundImage: 'url(images/r_5.png)' }); SetPlayInterval(700); break;
							case 6: $("#speedImage").css({ backgroundImage: 'url(images/r_6.png)' }); SetPlayInterval(630); break;
							case 7: $("#speedImage").css({ backgroundImage: 'url(images/r_7.png)' }); SetPlayInterval(560); break;
							case 8: $("#speedImage").css({ backgroundImage: 'url(images/r_8.png)' }); SetPlayInterval(490); break;
							case 9: $("#speedImage").css({ backgroundImage: 'url(images/r_9.png)' }); SetPlayInterval(420); break;
							case 10: $("#speedImage").css({ backgroundImage: 'url(images/r_10.png)' }); SetPlayInterval(350); break;
							case 11: $("#speedImage").css({ backgroundImage: 'url(images/r_11.png)' }); SetPlayInterval(280); break;
							case 12: $("#speedImage").css({ backgroundImage: 'url(images/r_12.png)' }); SetPlayInterval(210); break;
							case 13: $("#speedImage").css({ backgroundImage: 'url(images/r_13.png)' }); SetPlayInterval(140); break;
							case 14: $("#speedImage").css({ backgroundImage: 'url(images/r_14.png)' }); SetPlayInterval(70); break;
							case 15: $("#speedImage").css({ backgroundImage: 'url(images/r_15.png)' }); SetPlayInterval(0); break;
						}
					}
				});
			});

			function selectTxt()
			{
				$('#txtlink').focus();
				$('#txtlink').select();
			}
			function showText()
			{

				document.getElementById("lnktxt").style.visibility = 'visible';
				ip = $('#ctl00_ContentPlaceHolder1_ip').val();
				vid = $('#ctl00_ContentPlaceHolder1_vid').val();
				var linkVal = "http://" + ip + "/" + vid;
				$('#txtlink').val(linkVal);
			}

		</script>
		<style type="text/css">
			#container .ui-state-default, #container .ui-widget-content, #container .ui-state-default
			{
				background: url(images/handle.png) no-repeat;
				font-weight: bold;
				outline: none;
			}
			#container .ui-slider, #container .ui-slider-handle
			{
				cursor: default;
				height: 18px;
				position: absolute;
				width: 16px;
				z-index: 2;
			}
			#container .ui-slider-horizontal, #container .ui-slider-handle
			{
				margin-left: -0.3em;
				top: -4px;
			}
			#container .ui-widget-header
			{
				background: url("images/pbar_color.png") repeat-x #5DBAF6;
				border: 1px solid #5DBAF6;
				color: #FFFFFF;
				font-weight: bold;
			}
			#container .ui-slider, #container .ui-slider-range
			{
				background-position: 0 0;
				border: 0 none;
				display: block;
				font-size: 0.7em;
				position: absolute;
				z-index: 1;
			}
		</style>
	</head>
	<body>
		<asp:Label runat="server" ID="lblmsg"></asp:Label>
		<div style="margin-left: 0px; width: 520px;">
			<div style="display: block;" id="tab1">
				<div id="divImage" style="display: none; border: 0px solid #FFFFFF; width: 520px;
					height: 460px;">
				</div>
			</div>
			<div style="display: none;" id="tab2">
				<div id="divVideo" style="display: none; border: 0px solid #FFFFFF; width: 520px;">
					<table cellpadding="0" cellspacing="0">
						<tr>
							<td>
								<div id="pictureContainer" style="width: 520px;">
									<img id="picture" style="width: 520px; height: 388px;" name="picture"></div>
							</td>
						</tr>
						<tr>
							<td>
								<table width="520" border="0" align="center" cellpadding="0" cellspacing="0">
									<tr>
										<td width="5">
											<img src="images/bg_left.png" alt="" width="5" height="72" />
										</td>
										<td style="background: url(images/bg_rept.png);">
											<table width="100%" border="0" cellspacing="0" cellpadding="0" style="height: 37px;">
												<tr>
													<td width="60" align="center" valign="bottom">
														<div id="DvPlayNextPrev" style="height: 33px;">
															<a onmouseout="document.Play.src='images/play.png'" onmouseover="document.Play.src='images/play_over.png'">
																<img onclick="startPlayer('play')" alt="Play" src="images/play.png" title="Play"
																	width="33" height="33" name="Play" id="Play" /></a>
														</div>
														<div id="DvSpeedPause" style="visibility: hidden; margin-top: -33px; height: 30px;">
															<a onmouseout="document.pause.src='images/pause.png'" onmouseover="document.pause.src='images/pause_over.png'">
																<img id="pause" name="pause" title="Pause" width="33" height="33" src="images/pause.png"
																	alt="Pause" onclick="startPlayer('stop')" />
															</a>
														</div>
													</td>
													<td>
														<table width="364" border="0" cellspacing="0" cellpadding="0">
															<tr>
																<td height="7">
																	<div id="container" style="width: 435px; height: 7px; margin-top: 4px; background: url(images/pbar_bg.png);">
																	</div>
																</td>
															</tr>
														</table>
													</td>
												</tr>
											</table>
											<table width="100%" border="0" cellspacing="0" cellpadding="0">
												<tr>
													<td align="center">
														<table width="100%" border="0" cellspacing="0" cellpadding="0">
															<tr>
																<td width="228" align="left">
																	<div id="number" style="color: #464646; font-size: 14px; font-family: Arial; font-weight: bold;
																		margin-left: 18px;">
																	</div>
																</td>
																<td id="playbtm1" width="36">
																	<a onmouseout="document.prev.src='images/previous.png'" onmouseover="document.prev.src='images/previous_over.png'">
																		<img onclick="previmg()" alt="Frame-" src="images/previous.png" title="Frame-" name="prev"
																			width="26" height="25" id="prev" />
																	</a>
																</td>
																<td id="playbtm2" width="36">
																	<a onmouseout="document.next.src='images/forward.png'" onmouseover="document.next.src='images/forward_over.png'">
																		<img onclick="nextimg()" alt="Frame+" src="images/forward.png" title="Frame+" width="26"
																			height="25" name="next" id="next" />
																	</a>
																</td>
																<td id="pausebtm1" width="1">
																	<img src="images/sep_line.png" alt="" width="1" height="35" />
																</td>
																<td id="pausebtm2" width="45" align="center">
																	<a onmouseout="mouseOut();" onmouseover="mouseOver();">
																		<img src="images/rotate_over.png" alt="" onclick="loopPlayer();" width="26" height="25"
																			id="rotate" name="rotate" />
																	</a>
																</td>
																<td id="pausebtm3" width="1">
																	<img src="images/sep_line.png" alt="" width="1" height="35" />
																</td>
																<td id="pausebtm4" width="30" align="center">
																	<div id="speedImage" style="width: 25px; height: 22px; background: url(images/r_2.png) no-repeat;">
																	</div>
																</td>
																<td id="pausebtm5">
																	<table width="100%" border="0" cellpadding="0" cellspacing="0">
																		<tr>
																			<td style="background: url(images/bg_handle_v.png) no-repeat">
																				<div id="divOne" style="width: 70px; height: 10px; border: none;" />
																			</td>
																		</tr>
																	</table>
																</td>
															</tr>
														</table>
													</td>
												</tr>
											</table>
										</td>
										<td width="5">
											<img src="images/bg_right.png" alt="" width="5" height="72" />
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td>
								<table>
									<tr style="margin-top: 5px;">
										<td id="lnktxt" style="visibility: hidden;">
											<input id="txtlink" onclick="selectTxt();" class="linkTestBox" type="text" />
										</td>
										<td>
											<div id="link" runat="server" onclick="showText();" class="linkurl" style="margin-top: 5px;
												margin-left: 5px; cursor: pointer;">
											</div>
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td align="left">
								<div id="FbLike" runat="server" style="margin-top: 2px;">
								</div>
								<div id="speed">
								</div>
							</td>
						</tr>
						<tr>
							<td align="left">
								<div id="fbcomment" style="float: left;">
									<asp:PlaceHolder ID="FBCommentPlaceHolder" runat="server"></asp:PlaceHolder>
								</div>
							</td>
						</tr>
					</table>
					<script src="js/selectToUISlider/js/jquery-ui-1.7.1.custom.min.js" type="text/javascript"></script>
					<script src="js/selectToUISlider/js/selectToUISlider.jQuery.js" type="text/javascript"></script>
					<link href="js/selectToUISlider/css/ui.slider.extras.css" rel="stylesheet" type="text/css" />
					<link href="js/selectToUISlider/css/redmond/jquery-ui-1.7.1.custom.css" rel="stylesheet"
						type="text/css" />
				</div>
			</div>
			<div style="display: none;" id="tab3">
				<div id="divEdit" style="border: 0px solid #FFFFFF; width: 520px; height: 625px;">
				</div>
			</div>
			<div style="display: none;" id="tab4">
				<div id="divHelp" style="border: 0px solid #FFFFFF; width: 520px; height: 400px;">
					sorry, not yet implemented</div>
			</div>
		</div>
		<script>
			function getCameraHtml()
			{
				var ip = $("#<%=hdnIp.ClientID %>").val();
				var vid = $("#<%=hdnVid.ClientID %>").val();
				var uid = $("#<%=hdnUid.ClientID %>").val();
				return "<embed id=\"CaptureImage\" height=\"460\" align=\"middle\" width=\"520\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.adobe.com/go/getflashplayer\" flashvars=\"serverIP=" + ip + "&uid=" + uid + "&isUpdate=0&imgId=0&vid=" + vid + "\" allowfullscreen=\"true\" allowscriptaccess=\"sameDomain\" name=\"CaptureImage\" bgcolor=\"#869ca7\" quality=\"high\" src=\"Common/Camera/CamraFaceDetectorNew.swf\"/>";
			}
			
			function getEditImageHtml()
			{
				var ip = $("#<%=hdnIp.ClientID %>").val();
				var vid = $("#<%=hdnVid.ClientID %>").val();
				var uid = $("#<%=hdnUid.ClientID %>").val();

				var lastcurrentimage = getLastCurrentImage(uid);
				return "<embed id=\"EditImage\" height=\"625\" align=\"middle\" width=\"520\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.adobe.com/go/getflashplayer\" flashvars=\"serverIP=" + ip + "&uid=" + uid + "&vid=" + vid + "&latestcurrent=" + lastcurrentimage + "\" allowfullscreen=\"true\" allowscriptaccess=\"sameDomain\" name=\"EditImage\" bgcolor=\"#869ca7\" quality=\"high\" src=\"Common/ImageEdit.swf\"/>";
			}

		</script>
		<asp:HiddenField runat="server" ID="hdnUid" />
		<asp:HiddenField runat="server" ID="hdnVid" />
		<asp:HiddenField runat="server" ID="hdnIp" />
	</body>
	</html>
</asp:Content>
