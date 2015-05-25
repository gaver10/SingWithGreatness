<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mixer.aspx.cs" Inherits="SingWithGreatnessWeb.Mixer" %>

<html>
    <head>
        <link href="Style/Style.css" rel="stylesheet" type="text/css" />
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.1/jquery.min.js"></script>
        <script src="Jquery/jquery.collapse.js"></script>
    </head>
    <body>
        <h1>
            <center>
                <img src="Images/singwithgreatnessheader.png" />
            </center>
        </h1>
        <form runat="server">
            <div style="float: left; width: 20%;">
                <ul>
                  <li><a href="test">Home</a></li>
                  <li><a href="test">Logout</a></li>
                    <li class="blank"></li>
                    <li><a href="test">Play</a></li>
                    <li><a href="test">Stop</a></li>
                    <li><a href="test">Save</a></li>
                </ul>
            </div>
            <div style="float: left; width: 10%;">
                <!-- this div is just for space -->
            </div>
            <div style="float: left; width:70%;">
                <div id="tracks" style="width:100%;" data-collapse>
                  <h2>Track 1</h2>
                    <div style="border:solid; border-color:#99ccff;">
                        <asp:CheckBox runat="server" Text="Use Track?" />
                    </div>
                  <h2>Track 2</h2>
                    <div style="border:solid; border-color:#99ccff;">
                        <asp:CheckBox runat="server" Text="Use Track?" />
                    </div>
                  <h2>Track 3</h2>
                    <div style="border:solid; border-color:#99ccff;">
                        <asp:CheckBox runat="server" Text="Use Track?" />
                    </div>
                  <h2>Track 4</h2>
                    <div style="border:solid; border-color:#99ccff;">
                        <asp:CheckBox runat="server" Text="Use Track?" />
                    </div>
                  <h2>Track 5</h2>
                    <div style="border:solid; border-color:#99ccff;">
                        <asp:CheckBox runat="server" Text="Use Track?" />
                    </div>
                </div>
            </div>
        </form>
    </body>
</html>