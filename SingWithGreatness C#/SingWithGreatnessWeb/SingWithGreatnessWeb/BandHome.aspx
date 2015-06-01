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
                    <asp:Gridview runat="server" ID="bandGridview" ShowFooter="True" AutoGenerateColumns="False" EmptyDataText="No rows at this time." ShowHeaderWhenEmpty="True"
                         Width="100%" OnRowEditing="bandGridview_RowEditing" OnRowCancelingEdit="bandGridview_RowCancelingEdit"
                            OnRowUpdating="bandGridview_RowUpdating" OnRowDeleting="bandGridview_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkEdit" runat="server" Text="Edit" CommandName="Edit" CssClass="btn" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="linkUpdate" runat="server" Text="Update" CommandName="Update" CssClass="btn" />
                                    <asp:LinkButton ID="linkCancel" runat="server" Text="Cancel" CommandName="Cancel" CssClass="btn" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="ID" DataField="id" ReadOnly="true" />
                            <asp:TemplateField HeaderText="Path">
                                <ItemTemplate>
                                    <%# Eval("path") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="pathTextbox" runat="server" Text='<%# Eval("path") %>'/>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User">
                                <ItemTemplate>
                                    <%# Eval("user") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="userTextbox" runat="server" Text='<%# Eval("user") %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete?">
                                <ItemTemplate>
                                    <span onclick="return confirm('Are you sure you want to delete?')">
                                        <asp:LinkButton ID="linkDelete" runat="server" Text="Delete" CommandName="Delete" CssClass="btn" />
                                    </span>
                                </ItemTemplate>
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
