<%@ Page Title="" Language="C#" MasterPageFile="~/SingWithGreatness.Master" AutoEventWireup="true" CodeBehind="Mixer.aspx.cs" Inherits="SingWithGreatnessWeb.Mixer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        function addTrack () {
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
    <center>
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
		        <input type="button" id="playBtn" value="Play"/>
		        <input type="button" id="stopBtn" value="Stop"/>
	        </div>
        </div>
    </center>
</asp:Content>
