﻿<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }

    protected void Application_Error(object sender, EventArgs e)
    {
        Exception objErr = Server.GetLastError().GetBaseException();
        string err = "Error Caught in Application_Error event\n" +
                "Error in: " + Request.Url.ToString() +
                "\nError Message:" + objErr.Message.ToString() +
                "\nStack Trace:" + objErr.StackTrace.ToString();
       //  System.Diagnostics.EventLog.WriteEntry("SumiteStore", err, System.Diagnostics.EventLogEntryType.Error);
       //  Server.ClearError();
        //additional actions...
    }


    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    protected void Application_AuthenticateRequest(Object sender, EventArgs e)
    {
        //Fires upon attempting to authenticate the use
        if (!(HttpContext.Current.User == null))
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (HttpContext.Current.User.Identity.GetType() == typeof(FormsIdentity))
                {
                    FormsIdentity fi = (FormsIdentity)HttpContext.Current.User.Identity;
                    FormsAuthenticationTicket fat = fi.Ticket;

                    String[] astrRoles = fat.UserData.Split('|');
                    HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(fi, astrRoles);
                }
            }
        }
    }
       
</script>
