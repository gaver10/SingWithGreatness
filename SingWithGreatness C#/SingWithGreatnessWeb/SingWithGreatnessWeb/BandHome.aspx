<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="BandHome.aspx.cs" Inherits="SingWithGreatnessWeb.BandHome" %>
<html>
    <head>
        <link href="Style/Style.css" rel="stylesheet" type="text/css" />
        
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
                </ul>
            </div>
            <div style="float: left; width: 10%;">
                <!-- this div is just for space -->
            </div>
            <div style="float: left; width:70%;">
                <center>
                    <asp:Gridview runat="server" ID="bandGridview" ShowFooter="True" AutoGenerateColumns="False" EmptyDataText="No rows at this time." ShowHeaderWhenEmpty="True">
                        <Columns>
                            <asp:TemplateField HeaderText="Path">
                                <ItemTemplate>
                                    <asp:Label CssClass="lbl" ID="pathLabel" runat="server" Text='<% # Eval("path") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="pathTextbox" runat="server" Text='<%# Eval("path") %>'/>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User">
                                <ItemTemplate>
                                    <asp:Label CssClass="lbl" ID="userLabel" runat="server" Text='<%# Eval("user") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="userTextbox" runat="server" Text='<%# Eval("user") %>' />
                                </EditItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <FooterTemplate>
                                    <asp:Button CssClass="btn" ID="addPathButton" runat="server" Text="Add New Row" OnClick="addPathButton_Click" />
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:Gridview>
                </center>
            </div>
        </form>
    </body>
</html>
