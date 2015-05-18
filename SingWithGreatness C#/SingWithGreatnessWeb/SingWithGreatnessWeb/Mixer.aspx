<%@ Page Title="" Language="C#" MasterPageFile="~/SingWithGreatness.Master" AutoEventWireup="true" CodeBehind="Mixer.aspx.cs" Inherits="SingWithGreatnessWeb.Mixer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="Style/elessar.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
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
           <asp:Panel runat="server" ID="trackPanel">
               <asp:Panel runat="server" ID="track1Panel">
                   <asp:Label runat="server" ID="track1Label" Text="Track 1" />
                   <br /><br />
                   <asp:Label runat="server" Text="Start: " />&nbsp;
                   <asp:TextBox runat="server" ID="track1Start1Textbox" />
                   &nbsp;&nbsp;&nbsp;
                   <asp:Label runat="server" Text="End: " />&nbsp;
                   <asp:TextBox runat="server" ID="track1End1TextBox" />
                   <br /><br />
                   <asp:Label runat="server" Text="Start: " />&nbsp;
                   <asp:TextBox runat="server" ID="track1Start2Textbox" />
                   &nbsp;&nbsp;&nbsp;
                   <asp:Label runat="server" Text="End: " />&nbsp;
                   <asp:TextBox runat="server" ID="track1End2TextBox" />
               </asp:Panel>
               <asp:Button runat="server" ID="addTrack1Button" Text="Add" OnClick="addTrack1Button_Click" />
               <br />
               <br />
               <asp:Panel runat="server" ID="track2Panel">
                   <asp:Label runat="server" ID="track2Label" Text="Track 2" />
                   <br /><br />
                   <asp:Label runat="server" Text="Start: " />&nbsp;
                   <asp:TextBox runat="server" ID="track2Start1Textbox" />
                   &nbsp;&nbsp;&nbsp;
                   <asp:Label runat="server" Text="End: " />&nbsp;
                   <asp:TextBox runat="server" ID="track2End1Textbox" />
                   <br /><br />
                   <asp:Label runat="server" Text="Start: " />&nbsp;
                   <asp:TextBox runat="server" ID="track2Start2Textbox" />
                   &nbsp;&nbsp;&nbsp;
                   <asp:Label runat="server" Text="End: " />&nbsp;
                   <asp:TextBox runat="server" ID="track2End2Textbox" />
               </asp:Panel>
               <br />
               <asp:Button runat="server" ID="addTrack2Button" Text="Add" OnClick="addTrack2Button_Click" />
           </asp:Panel>
            <br />
            <asp:Button runat="server" ID="addTrackButton" Text="AddTrack" OnClick="addTrackButton_Click" />
                <br /><br />
	        <div id="mixerControl">
                <asp:Button runat="server" ID="mixButton" Text="Process Mix" OnClick="mixButton_Click" UseSubmitBehavior="False" />
                <br />
                <asp:Label runat="server" ID="completeLabel" Text="Done processing" Visible="false" />
	        </div>
                <br />
	        <div class="mixerButtons">
		        <asp:Button runat="server" ID="playButton" Text="Play" OnClick="playButton_Click"  UseSubmitBehavior="False" />
                &nbsp;
                <asp:Button runat="server" ID="stopButton" Text="Stop" OnClick="stopButton_Click" UseSubmitBehavior="False" />
	        </div>
        </div>
    </center>
</asp:Content>