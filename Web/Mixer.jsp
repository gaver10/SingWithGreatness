<%@ page language="java" contentType="text/html; charset=UTF-8"
    pageEncoding="UTF-8"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
<title>Sing With Greatness Mixer</title>
<script src="http://momentjs.com/downloads/moment.min.js"></script>
<script src="http://code.jquery.com/jquery.min.js"></script>
<script src="https://rawgithub.com/quarterto/Estira/master/index.js"></script>
<script src="./scripts/Elessar Slider/elessar.min.js"></script>
<link rel="stylesheet" href="./elessar.css" type="text/css">
</head>
<body>
<pre class="changing"></pre>
<script>
function addTrack (trackLength) {
	var objTo = document.getElementById('mixerControl');
    var newDiv = document.createElement("div");
    
    newDiv.innerHTML = "<br/>";
    objTo.appendChild(newDiv);
    
    var slider = document.createElement('input');
    slider.id = "volume";
    slider.type = 'range';
    slider.min = 0;
    slider.max = 1;
    slider.value = 0.5;
    slider.step = 0.1;
    
    $('#mixerControl').append($('#newTrackName').val());
    $('#mixerControl').append(slider);
    $('#mixerControl').append(RangeBar({
    		min: 0,
    		max: 500,
    	  values: [[0, 10], [20, 30]],
    	  label: function(i) { return i.map(Math.floor).toString();}} ).$el.css({width: '500px', textAlign: 'center', color: 'white'}));
}
</script>
	<form action="MixerS" method="post">
		<center>
			<div class="header">
				<h1>
					Sing With Greatness
				</h1>
			</div>
			<div>
				<div class="addTrack">
					<p>Add New Track</p>
					<input type="text" id='newTrackName' placeholder="Friend Name"/>
					<input type="text" id="newTrackID" placeholder="Track ID"/>
					<input type="button" id="addNewTrackBtn" value="Add Track" onclick="addTrack();"/>
				</div>
				<div class="yourTrack">
					<p>Your Track ID: </p>
					<p>24354642</p>
				</div>
				<div id="mixerControl">
				</div>
				<div class="mixerButtons">
					<input type="button" id="playBtn" name="playBtn" value="Play"/>
					<input type="button" id="stopBtn" name="stopBtn" value="Stop"/>
				</div>
			</div>
		</center>
	</form>
</body>
</html>