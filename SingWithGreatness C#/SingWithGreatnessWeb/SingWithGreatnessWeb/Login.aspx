<%@ Page Title="Login" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SingWithGreatnessWeb.Login" %>

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
                                <input type="text"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Password: </label>
                            </td>
                            <td>
                                <input type="password" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <center>
                        <button>Login</button>
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
                                <input type="text"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Password: </label>
                            </td>
                            <td>
                                <input type="password" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Email: </label>
                            </td>
                            <td>
                                <input type="email" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Account Type:</label>
                            </td>
                            <td>
                                <input type="radio" name="accountType" value="Audience"/>Audience
                                <br />
                                <input type="radio" name="accountType" value="Band" />Band
                            </td>
                        </tr>
                    </table><br />
                    <br />
                    <center>
                        <button>Register</button>
                    </center>
                </div>
            </div>
        </center>
    </body>
</html>