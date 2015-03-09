<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
  
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" 
    "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
<title>Sing With Greatness Login</title>
<link rel="stylesheet" href="./style.css" type="text/css">
</head>
<body>
	<form action="LoginServlet">
		<center>
			<div class="header">
				<h1>
					Sing With Greatness
				</h1>
			</div>
		</center>
		<div class="boxes">
			<center>
				<table class="boxTable">
					<tr>
						<td>
							<table class="loginTable">
								<tr>
									<td>
										<label>Login</label>
									</td>
								</tr>
								<tr>
									<td>
										<input type="text" name="loginUsername" placeholder="Username">
									</td>
								</tr>
								<tr>
									<td>
										<input type="password" name="loginPassword" placeholder="Password">
									</td>
								</tr>
								<tr>
									<td>
										<input type="button" name="loginBtn" value="Login">
									</td>
								</tr>
								<tr>
									<td>
										<p name="wrongInfoText" hidden="true" color="red">
											Login information incorrect.
										</p>
									</td>
								</tr>
							</table>
						</td>
						<td>
							<table class="registerTable">
								<tr>
									<td>
										<label>Register</label>
									</td>
								</tr>
								<tr>
									<td>
										<input type="text" name="registerEmail" placeholder="Email">
									</td>
								</tr>
								<tr>
									<td>
										<input type="text" name="registerUsername" placeholder="Username">
									</td>
								</tr>
								<tr>
									<td>
										<input type="password" name="registerPassword" placeholder="Password">
									</td>
								</tr>
								<tr>
									<td>
										<select name="registerUserType">
										    <option value="audienceMember">Audience Member</option>
										    <option value="bandRepresentative">Band Representative</option>
										  </select>
									</td>
								</tr>
								<tr>
									<td>
										<input type="button" name="registerBtn" value="Register">
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</center>
		</div>
	</form>
</body>
</html>