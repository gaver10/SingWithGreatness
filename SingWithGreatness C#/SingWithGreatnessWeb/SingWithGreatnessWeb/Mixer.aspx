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
            <!--<asp:Button runat="server" ID="addTrack" Text="Add Track" OnClick="addTrack_Click"/>-->
            <table>
                <tr>
                    <td>
                        <center>
                            <asp:Label runat="server" ID="track1Label" Text="Track 1" />
                            <br />
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox runat="server" ID="track1Start1" Text="0" />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="track1End1" Text="5" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox runat="server" ID="track1Start2" Text="10" />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="track1End2" Text="15" />
                                    </td>
                                </tr>
                            </table>
                        </center>
                    </td>
                    <td>
                        <center>
                            <asp:Label runat="server" ID="track2Label" Text="Track 2" />
                            <br />
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox runat="server" ID="track2Start1" Text="5" />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="track2End1" Text="10" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox runat="server" ID="track2Start2" Text="15" />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="track2End2" Text="20" />
                                    </td>
                                </tr>
                            </table>
                        </center>
                    </td>
                </tr>
            </table>
                <br />
	        <div id="mixerControl">
                <asp:Button runat="server" ID="mixButton" Text="Process Mix" OnClick="mixButton_Click" />
	        </div>
                <br />
	        <div class="mixerButtons">
		        <asp:Button runat="server" ID="playButton" Text="Play" OnClick="playButton_Click" />
                &nbsp;
                <asp:Button runat="server" ID="stopButton" Text="Stop" OnClick="stopButton_Click" />
	        </div>
        </div>
    </center>
</asp:Content>