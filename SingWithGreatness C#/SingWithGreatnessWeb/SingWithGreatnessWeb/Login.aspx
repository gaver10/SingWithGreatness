<%@ Page Title="Login" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SingWithGreatnessWeb.Login" %>

<html>
    <head>
        <link href="Style/Style.css" rel="stylesheet" type="text/css" />
    </head>
    <body>
        <form runat="server">
            <h1>
                <center>
                    <img src="Images/singwithgreatnessheader.png" />
                </center>
            </h1>
            <center>
                <div style="width:600px;">
                    <div style="float:left; width:49%; height:280px; border:solid; border-color:#6699cc; border-width:thin;">
                        <center>
                            <label>Login</label>
                        </center>
                        <br />
                        <br />
                        <table>
                            <tr>
                                <td>
                                    <label>Username: </label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="loginUsernameTextbox" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Password: </label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" type="password" id="loginPasswordTextbox"/>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <center>
                            <asp:Button CssClass="btn" runat="server" ID="loginButton" Text="Login" OnClick="loginButton_Click" />
                        </center>
                    </div>
                    <div style="float: left; width:49%; height:280px; border:solid; border-color:#6699cc; border-width:thin;">
                        <center>
                            <label>Register</label>
                        </center>
                        <br />
                        <br />
                        <table>
                            <tr>
                                <td>
                                    <label>Username: </label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" id="registerUsernameTextbox"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Password: </label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" type="password" id="registerPasswordTextbox"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Email: </label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" id="registerEmailTextbox"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Account Type:</label>
                                </td>
                                <td>
                                    <asp:RadioButtonList runat="server" ID="accountTypeRadioButtonList">
                                        <asp:ListItem Text="Band" />
                                        <asp:ListItem Text="Audience" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table><br />
                        <br />
                        <center>
                           <asp:Button CssClass="btn" runat="server" ID="RegisterButton" Text="Register" OnClick="RegisterButton_Click" />
                        </center>
                    </div>
                </div>
            </center>
        </form>
    </body>
</html>