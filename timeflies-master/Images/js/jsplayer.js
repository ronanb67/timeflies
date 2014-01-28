var i = 0;
var finished = false;
var paused = false;
var running = false;
var userid;
var Total = 0;
var progressPercentage = 0;
var pictdate = new Array();
var pict = new Array();
var playinterval = 0;
var totalPages = 0;
var interval = 0;
var stp = "";
var imgLoaded = "";
var timeOuts = new Array();
var loop = false;

//interval = setInterval(FFSlideshow, playinterval);
function loadPics(st)
{
	if (document.getElementById("pause").title == "Pause")
	{
		document.getElementById("pause").title = "Play";
		paused = true;
		//document.getElementById('number').innerHTML = "Paused";
		document.getElementById("DvPlayNextPrev").style.visibility = 'visible';
		document.getElementById("playbtm1").style.visibility = 'visible';
		document.getElementById("playbtm2").style.visibility = 'visible';
		document.getElementById("DvSpeedPause").style.visibility = 'hidden';
	}
	stp = st;
	i = 0;
	finished = false;
	//paused = false;
	running = false;
	loop = true;
	userid;
	Total = 0;
	progressPercentage = 0;
	pictdate = new Array();
	pict = new Array();
	playinterval = 0;
	totalPages = 0;

	document.getElementById("prev").disabled = true;
	$('#container').slider('option', 'value', 0);
	userid = $("[id$='hdnUid']").val(); 
	$.get("GetSnapshot.ashx?userid=" + userid + "&direction=2&pageSize=5&flag=1&frameNumber=0&random=" + Math.random() + "", loadPicsCallback);
}

function loadPicsCallback(data)
{
	var element = data.split("|");
	//alert(element[1]);
	if (element[0] == "Success")
	{
		pictdate = element[1].split(",");
		Total = pictdate.length;
		$('#divOne').slider('option', 'value', 15);
		$("#speedImage").css({ backgroundImage: 'url(images/r_15.png)' });
		$("#container").slider("option", "max", Total - 1);
		loop = true;
		document.getElementById('rotate').src = "images/rotate_over.png";

		$("#container").slider({
			range: "min",
			min: 0,
			max: Total - 1,

			change: function (event, ui)
			{
				//if (onImg.complete != null && onImg.complete == true) {
				i = ui.value;
				pict = pictdate[i].split("#");
				//clearInterval(interval);
				clearAllTimeouts();
				var onImg = rndString(5);
				onImg = new Image();
				var onImg = document.getElementById('picture');
				pict = pictdate[i].split("#");
				if (onImg.complete)
				{
					onImg.onload = function ()
					{
						if (paused == false && running == true)
						{
							startSlide();
							//running == false;
						} else
						{
							//alert("change " + finished);
							finished = false;
							return false;
						}
					}
				}
				onImg.src = pict[0] + "?v=" + rndString(5);
				$("[id$='hdnLatestCurrentImage']").val(pict[0]);
				document.getElementById('number').innerHTML = pict[1] + " (" + 1 * (i + 1) + " of " + Total + ")  ";
				$('#container').slider('option', 'value', i);
			}
		});
		totalPages = element[2];
		progressPercentage = 100 / Total;
		if (totalPages > 0)
		{
			pict = pictdate[0].split("#");
			document.getElementById("Play").disabled = false;
			document.getElementById("next").disabled = false;
			//document.getElementById('picture').src = pict[0];
			var onImg = rndString(5);
			onImg = new Image();
			var onImg = document.getElementById('picture');

			onImg.onload = function ()
			{
				//   startSlide();
			}

			onImg.src = pict[0] + "?v=" + Math.random().toString().substr(1, 8);
			$("[id$='hdnLatestCurrentImage']").val(pict[0]);
			$('#container').slider('option', 'value', 0);
			document.getElementById('number').innerHTML = pict[1] + " (" + 1 * (i + 1) + " of " + Total + ")   ";
		}
		else
		{
			//alert("nopic");
			document.getElementById('picture').src = "../images/no_pic.jpg";
			document.getElementById('number').innerHTML = "No Frame Found";
			document.getElementById("Play").disabled = true;
			document.getElementById("next").disabled = true;
		}
	}
}

function nextimg()
{
	finished = false;

	if (i < pictdate.length - 1)
	{
		i++;
		pict = pictdate[i].split("#");

		var onImg = rndString(5);
		onImg = new Image();
		var onImg = document.getElementById('picture');
		pict = pictdate[i].split("#");

		onImg.onload = function ()
		{
			//   startSlide();
		}
		onImg.src = pict[0] + "?v=" + Math.random().toString().substr(1, 8);
		$("[id$='hdnLatestCurrentImage']").val(pict[0]);
		document.getElementById('number').innerHTML = pict[1] + " (" + 1 * (i + 1) + " of " + Total + ")   ";
		$('#container').slider('option', 'value', i);
	}
	else
	{
		document.getElementById("next").disabled = true;
	}
	document.getElementById("prev").disabled = false;
}

function previmg()
{
	finished = false;
	if (i > 0)
	{
		i--;
		pict = pictdate[i].split("#");

		var onImg = rndString(5);
		onImg = new Image();
		var onImg = document.getElementById('picture');
		pict = pictdate[i].split("#");

		onImg.onload = function ()
		{
			//   startSlide();
		}
		onImg.src = pict[0] + "?v=" + Math.random().toString().substr(1, 8);
		$("[id$='hdnLatestCurrentImage']").val(pict[0]);

		document.getElementById('number').innerHTML = pict[1] + " (" + 1 * (i + 1) + " of " + Total + ") ";
		$('#container').slider('option', 'value', i);
	}
	else
	{
		document.getElementById("prev").disabled = true;
	}
	document.getElementById("next").disabled = false;
}

function startSlide()
{
	//alert(finished);
	paused = false;
	running = true;
	if (navigator.appVersion.indexOf("MSIE") == -1 || navigator.appVersion.indexOf("MSIE") == 17)
	{
		//alert('called');
		this.interval = setInterval(FFSlideshow, playinterval);
		if (timeOuts[interval] == undefined)
		{
			timeOuts[interval] = interval;
		}
	}
	else
	{
		//alert('called');
		this.interval = setInterval(slideshow, playinterval);
	}
	var frames = 1 * (i + 1);
	if (frames > Total)
	{
		frames = 1;
	}
	if (frames != Total)
	{
		pict = pictdate[i].split("#");
		document.getElementById('number').innerHTML = pict[1] + " (" + 1 * (i + 1) + " of " + Total + ") ";
	}
}

function slideshow()
{
	if (i < pictdate.length - 1)
	{
		document.getElementById('pictureContainer').filters[0].Apply();
		//document.getElementById("slideshow").disabled=true;
		i++;
		var onImg = rndString(5);
		onImg = new Image();
		var onImg = document.getElementById('picture');
		pict = pictdate[i].split("#");

		onImg.onload = function ()
		{
			if (onImg.complete)
			{
				if (paused == false)
				{
					startSlide();
				} else
				{
					return false;
				}
			}
		}
		onImg.src = pict[0] + "?v=" + rndString(5);
		$("[id$='hdnLatestCurrentImage']").val(pict[0]);
		document.getElementById('number').innerHTML = pict[1] + " (" + 1 * (i + 1) + " of " + Total + ")     ";
		document.getElementById('pictureContainer').filters[0].Play();
		document.getElementById("next").disabled = false;
		document.getElementById("prev").disabled = false;
		$('#container').slider('option', 'value', i);
	}
	else if (i == pictdate.length - 1 && finished == false)
	{
		document.getElementById('number').innerHTML = "";
		document.getElementById("DvPlayNextPrev").style.visibility = 'visible';
		document.getElementById("playbtm1").style.visibility = 'visible';
		document.getElementById("playbtm2").style.visibility = 'visible';
		document.getElementById("DvSpeedPause").style.visibility = 'hidden';
		if (!loop)
		{
			finished = true;
			running = false;
			clearAllTimeouts();
		} else
		{
			i = 0;
		}
	}
	else
	{
		document.getElementById('pictureContainer').filters[0].Apply();
		i = 0;

		pict = pictdate[i].split("#");
		finished = false;
		document.getElementById('picture').src = pict[0] + "?v=" + Math.random().toString().substr(1, 8);
		$("[id$='hdnLatestCurrentImage']").val(pict[0]);
		document.getElementById('number').innerHTML = pict[1] + " (" + 1 * (i + 1) + " of " + Total + "    ";
		document.getElementById('pictureContainer').filters[0].Play();
		$('#container').slider('option', 'value', i);
	}
	//alert(i);
}

function rndString(slength)
{
	var chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghiklmnopqrstuvwxyz";
	if (!slength || typeof (slength) != 'number')
	{
		var slength = 8;
	}
	var rs = '';
	for (var i = 0; i < slength; i++)
	{
		var rnum = Math.floor(Math.random() * chars.length);
		rs += chars.substring(rnum, rnum + 1);
	}
	return rs;
}

function FFSlideshow()
{
	document.getElementById("next").disabled = false;
	if (stp == 'stop')
	{
		//clearInterval(interval);
		clearAllTimeouts();
		return false;
	}
	if (i < pictdate.length - 1)
	{

		i++
		clearAllTimeouts();
		var onImg = rndString(5);
		onImg = new Image();
		var onImg = document.getElementById('picture');
		pict = pictdate[i].split("#");
		if (onImg.complete != null && onImg.complete == true)
		{
			onImg.onload = function ()
			{
				imgLoaded = true;
				if (paused == false)
				{
					startSlide();
				} else
				{
					return false;
				}
			}
		} else
		{
			return false;
		}

		if (imgLoaded != true)
		{
			clearAllTimeouts();
		} else
		{
			imgLoaded = false;
		}
		onImg.src = pict[0] + "?v=" + rndString(5);
		$("[id$='hdnLatestCurrentImage']").val(pict[0]);
		document.getElementById('number').innerHTML = pict[1] + " (" + 1 * (i + 1) + " of " + Total + ")    ";
		document.getElementById("next").disabled = false;
		document.getElementById("prev").disabled = false;
		$('#container').slider('option', 'value', i);
	}
	else if (i == pictdate.length - 1 && finished == false)
	{
		if (!loop)
		{

			document.getElementById('number').innerHTML = "";
			document.getElementById("DvPlayNextPrev").style.visibility = 'visible';
			document.getElementById("playbtm1").style.visibility = 'visible';
			document.getElementById("playbtm2").style.visibility = 'visible';
			document.getElementById("DvSpeedPause").style.visibility = 'hidden';
			document.getElementById("pause").title = "Play";
			document.getElementById('speed').innerHTML = "";
			// alert('call1');
			finished = true;
			running = false;
			//clearInterval(interval);
			clearAllTimeouts();
		} else
		{
			//alert('call2');

			i = 0;
			$('#container').slider('option', 'value', 0);
			pict = pictdate[i].split("#");
			finished = false;
			clearAllTimeouts();
			var onImg = rndString(5);
			onImg = new Image();
			var onImg = document.getElementById('picture');
			pict = pictdate[i].split("#");

			onImg.onload = function ()
			{
				startSlide();
			}
			onImg.src = pict[0] + "?v=" + Math.random().toString().substr(1, 8);
			$("[id$='hdnLatestCurrentImage']").val(pict[0]);

			document.getElementById('number').innerHTML = pict[1] + " (" + 1 * (i + 1) + " of " + Total + ")   ";
			$('#container').slider('option', 'value', i);
		}

	}
	else
	{
		i = 0;
		$('#container').slider('option', 'value', i);
		pict = pictdate[i].split("#");
		finished = false;
		clearAllTimeouts();
		var onImg = rndString(5);
		onImg = new Image();
		var onImg = document.getElementById('picture');
		pict = pictdate[i].split("#");

		onImg.onload = function ()
		{
			startSlide();
		}
		onImg.src = pict[0] + "?v=" + Math.random().toString().substr(1, 8);
		$("[id$='hdnLatestCurrentImage']").val(pict[0]);

		document.getElementById('number').innerHTML = pict[1] + " (" + 1 * (i + 1) + " of " + Total + ")   ";
		$('#container').slider('option', 'value', i);
	}
}

function pauseimg()
{

	if (running == true)
	{
		if (paused == false)
		{
			paused = true;
			//alert('pause called');
			document.getElementById("pause").title = "Play";
			//clearInterval(interval);
			clearAllTimeouts();
			//  document.getElementById('number').innerHTML = "Paused";
			document.getElementById("DvPlayNextPrev").style.visibility = 'visible';
			document.getElementById("playbtm1").style.visibility = 'visible';
			document.getElementById("playbtm2").style.visibility = 'visible';
			document.getElementById("DvSpeedPause").style.visibility = 'hidden';
		}
		else
		{

			startSlide()
			// alert('play called');
			paused = false;
			document.getElementById("DvPlayNextPrev").style.visibility = 'hidden';
			document.getElementById("playbtm1").style.visibility = 'hidden';
			document.getElementById("playbtm2").style.visibility = 'hidden';
			document.getElementById("DvSpeedPause").style.visibility = 'visible';
			document.getElementById("pause").title = "Pause";
		}
	} 
	else
	{
		document.getElementById("pause").title = "Pause";
		document.getElementById("DvPlayNextPrev").style.visibility = 'hidden';
		document.getElementById("playbtm1").style.visibility = 'hidden';
		document.getElementById("playbtm2").style.visibility = 'hidden';
		document.getElementById("DvSpeedPause").style.visibility = 'visible';
		startSlide();
	}
}

function SetPlayInterval(val)
{
	playinterval = val;
	paused = true;
	//clearInterval(interval);
	clearAllTimeouts();
	//alert("paused" + document.getElementById("pause").title);
	if (document.getElementById("pause").title == "Pause")
	{
		//alert('called');
		pauseimg();
	}
}

function startPlayer(playedValue)
{
	document.getElementById('speed').innerHTML = "";
	stp = playedValue;
	pauseimg();
}

function loopPlayer()
{
	if (loop)
	{
		loop = false;
		document.getElementById('rotate').src = "images/rotate.png";
		// alert(loop);
	} else
	{
		loop = true;
		SetPlayInterval(0);
		$("#speedImage").css({ backgroundImage: 'url(images/r_15.png)' });
		$('#divOne').slider('option', 'value', 15);
		document.getElementById('rotate').src = "images/rotate_over.png";
		//alert(loop);

	}
}

function clearAllTimeouts()
{
	try
	{
		//if (oTimer != null) clearTimeout(oTimer);
		for (key in timeOuts)
		{
			clearTimeout(timeOuts[key]);
		}
	} 
	catch (e) { }
}

function mouseOver()
{
	if (loop)
	{
		document.getElementById('rotate').src = "images/rotate.png";
	} 
	else
	{
		document.getElementById('rotate').src = "images/rotate_over.png";
	}
}

function mouseOut()
{
	if (!loop)
	{
		document.getElementById('rotate').src = "images/rotate.png";
	} 
	else
	{
		document.getElementById('rotate').src = "images/rotate_over.png";
	}
}