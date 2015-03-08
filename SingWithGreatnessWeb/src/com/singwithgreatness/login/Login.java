package com.singwithgreatness.login;
 
import java.io.IOException;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import java.io.PrintWriter;
 
public class Login extends HttpServlet {
    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
    	boolean validated = false;

        if (request.getParameter("loginBtn") != null) {
        	String username = request.getParameter("loginUsername");
        	String password = request.getParameter("loginPassword");
            validated = Login(username, password);
        } else if (request.getParameter("registerBtn") != null) {
        	String email = request.getParameter("registerEmail");
        	String username = request.getParameter("registerUsername");
        	String password = request.getParameter("registerPassword");
            validated = Register(email, username, password);
        }

        if (validated)
        {
        	request.getRequestDispatcher("/WEB-INF/MixerScreen.jsp").forward(request, response);
        }
        else
        {
        	request.getRequestDispatcher("/WEB-INF/LoginScreen.jsp").forward(request, response);
        }
    }
    
    public static boolean Login(String username, String password)
    {
    	String tempUsername = "SteveBartalbee";
    	String tempPassword = "bees";
    	
    	if (username.equals(tempUsername) && password.equals(tempPassword))
    		return true;
    	else
    		return false;
    }
    
    public static boolean Register(String email, String username, String password)
    {
    	// once database actually works, do stuff
    	
    	return true;
    }
}