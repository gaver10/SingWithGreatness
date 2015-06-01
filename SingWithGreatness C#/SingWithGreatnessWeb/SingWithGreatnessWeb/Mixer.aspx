<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mixer.aspx.cs" Inherits="SingWithGreatnessWeb.Mixer" %>

<html>
    <head>
        <link href="Style/Style.css" rel="stylesheet" type="text/css" />
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.1/jquery.min.js"></script>
        <script src="http://momentjs.com/downloads/moment.min.js"></script>
        <script src="http://code.jquery.com/jquery.min.js"></script>
        <script src="https://rawgithub.com/quarterto/Estira/master/index.js"></script>
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
                    <li><asp:Button CssClass="btn" runat="server" ID="logoutButton" Text="Logout" OnClick="logoutButton_Click" /></li>
                    <li class="blank"></li>
                    <li><asp:Button CssClass="btn" runat="server" ID="processButton" Text="Process" OnClick="mixButton_Click"/></li>
                    <li><asp:Button CssClass="btn" runat="server" ID="playButton" Text="Play" OnClick="playButton_Click"/></li>
                    <li><asp:Button CssClass="btn" runat="server" ID="stopButton" Text="Stop" OnClick="stopButton_Click"/></li>
                </ul>
            </div>
            <div style="float: left; width: 10%;">
                <!-- this div is just for space -->
            </div>
            <div style="float: left; width:70%;">
                <center>
                    <asp:Label CssClass="lbl" runat="server" ID="completeLabel" Text="Processing Complete" Visible="false"/>
                </center>
                <br />
                <br />
                <div id="tracks" style="width:100%;">
                  <h2 style="font-weight: bold; color: #99ccff; text-transform: uppercase;">Track 1</h2>
                    <div id="track1Div" style="border:solid; border-color:#99ccff;">
                        <asp:CheckBox runat="server" Text="Use Track?" id="track1Checkbox"/>
                        <br />
                        <asp:Label runat="server" Text="Track: " /> &nbsp;
                        <asp:DropDownList runat="server" ID="track1DropDownList" />
                        <br />
                        <center>
                            <asp:gridview ID="track1Gridview" runat="server" ShowFooter="true" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="Start">
                                        <ItemTemplate>
                                            <asp:TextBox ID="track1StartTextbox" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Left" />
                                        <FooterTemplate>
                                         <asp:Button ID="track1DeleteButton" runat="server" Text="Remove Row" OnClick="track1DeleteButton_Click" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stop">
                                        <ItemTemplate>
                                             <asp:TextBox ID="track1StopTextbox" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Button ID="track1AddButton" runat="server" Text="Add New Row" OnClick="track1AddButton_Click" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:gridview>
                        </center>
                    </div>
                  <h2 style="font-weight: bold; color: #99ccff; text-transform: uppercase;">Track 2</h2>
                    <div ID="track2Div" style="border:solid; border-color:#99ccff;">
                        <asp:CheckBox runat="server" Text="Use Track?" id="track2Checkbox"/>
                        <br />
                        <asp:Label runat="server" Text="Track: " /> &nbsp;
                        <asp:DropDownList runat="server" ID="track2DropdownList" />
                        <center>
                            <asp:gridview ID="track2Gridview" runat="server" ShowFooter="true" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="Start">
                                        <ItemTemplate>
                                            <asp:TextBox ID="track2StartTextbox" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Left" />
                                        <FooterTemplate>
                                         <asp:Button ID="track2DeleteButton" runat="server" Text="Remove Row" OnClick="track2DeleteButton_Click" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stop">
                                        <ItemTemplate>
                                             <asp:TextBox ID="track2StopTextbox" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                         <asp:Button ID="track2AddButton" runat="server" Text="Add New Row" OnClick="track2AddButton_Click" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:gridview>
                        </center>
                    </div>
                  <h2 style="font-weight: bold; color: #99ccff; text-transform: uppercase;">Track 3</h2>
                    <div ID="track3Div" style="border:solid; border-color:#99ccff;">
                        <asp:CheckBox runat="server" Text="Use Track?" id="track3Checkbox"/>
                        <br />
                        <asp:Label runat="server" Text="Track: " /> &nbsp;
                        <asp:DropDownList runat="server" ID="track3DropdownList" />
                        <center>
                            <asp:gridview ID="track3Gridview" runat="server" ShowFooter="true" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="Start">
                                        <ItemTemplate>
                                            <asp:TextBox ID="track3StartTextbox" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Left" />
                                        <FooterTemplate>
                                         <asp:Button ID="track3DeleteButton" runat="server" Text="Remove Row" OnClick="track3DeleteButton_Click" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stop">
                                        <ItemTemplate>
                                             <asp:TextBox ID="track3StopTextbox" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                         <asp:Button ID="track3AddButton" runat="server" Text="Add New Row" OnClick="track3AddButton_Click" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:gridview>
                        </center>
                    </div>
                  <h2 style="font-weight: bold; color: #99ccff; text-transform: uppercase;">Track 4</h2>
                    <div ID="track4Div" style="border:solid; border-color:#99ccff;">
                        <asp:CheckBox runat="server" Text="Use Track?" id="track4Checkbox"/>
                        <br />
                        <asp:Label runat="server" Text="Track: " /> &nbsp;
                        <asp:DropDownList runat="server" ID="track4DropdownList" />
                        <center>
                            <asp:gridview ID="track4Gridview" runat="server" ShowFooter="true" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="Start">
                                        <ItemTemplate>
                                            <asp:TextBox ID="track4StartTextbox" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Left" />
                                        <FooterTemplate>
                                         <asp:Button ID="track4DeleteButton" runat="server" Text="Remove Row" OnClick="track4DeleteButton_Click" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stop">
                                        <ItemTemplate>
                                             <asp:TextBox ID="track4StopTextbox" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                         <asp:Button ID="track4AddButton" runat="server" Text="Add New Row" OnClick="track4AddButton_Click" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:gridview>
                        </center>
                    </div>
                  <h2 style="font-weight: bold; color: #99ccff; text-transform: uppercase;">Track 5</h2>
                    <div ID="track5Div" style="border:solid; border-color:#99ccff;">
                        <asp:CheckBox runat="server" Text="Use Track?" id="track5Checkbox"/>
                        <br />
                        <asp:Label runat="server" Text="Track: " /> &nbsp;
                        <asp:DropDownList runat="server" ID="track5DropdownList" />
                        <center>
                            <asp:gridview ID="track5Gridview" runat="server" ShowFooter="true" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="Start">
                                        <ItemTemplate>
                                            <asp:TextBox ID="track5StartTextbox" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Left" />
                                        <FooterTemplate>
                                         <asp:Button ID="track5DeleteButton" runat="server" Text="Remove Row" OnClick="track5DeleteButton_Click" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stop">
                                        <ItemTemplate>
                                             <asp:TextBox ID="track5StopTextbox" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                         <asp:Button ID="track5AddButton" runat="server" Text="Add New Row" OnClick="track5AddButton_Click" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:gridview>
                        </center>
                    </div>
                    <br />
                    <br />
                </div>
            </div>
        </form>
    </body>
</html>