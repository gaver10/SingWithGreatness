<%@ Page Title="" Language="C#" MasterPageFile="~/SingWithGreatness.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SingWithGreatnessWeb.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <div style="width:500px; height:500px;">
            <div style="width:250px; height:500px; float:left;">
                <asp:Label runat="server" Text="Login" />
                <br />
                <br />
                <br />
            </div>
            <div style="width:250px; height:500px; float:left;">
                <asp:Label runat="server" Text="Register" />
            </div>
        </div>
    </center>
</asp:Content>

