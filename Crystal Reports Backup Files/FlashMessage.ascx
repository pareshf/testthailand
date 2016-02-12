<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FlashMessage.ascx.cs"
    Inherits="CRM.WebApp.Views.Shared.Controls.Message.FlashMessage" %>

<script language="javascript" type="text/javascript">

    function setOpacity(id, level) 
    {            
        var element = document.getElementById(id); 
        element.style.display = 'inline';           
        element.style.zoom = 1;
        element.style.opacity = level;
        element.style.MozOpacity = level;
        element.style.KhtmlOpacity = level;
        element.style.filter = "alpha(opacity=" + (level * 100) + ");";
    }
    
    function fadeIn(id, steps, duration, interval, fadeOutSteps, fadeOutDuration)
    {  
        var fadeInComplete;      
        for (i = 0; i <= 1; i += (1 / steps)) 
        {
            setTimeout("setOpacity('" + id + "', " + i + ")", i * duration); 
            fadeInComplete = i * duration;             
        }
        //set the timeout to start after the fade in time and the interval to display the 
        //message on the screen have both completed            
        setTimeout("fadeOut('" + id + "', " + fadeOutSteps + ", " + fadeOutDuration + ")", fadeInComplete + interval);           
    }
    
    function fadeOut(id, steps, duration) 
    {         
        var fadeOutComplete;       
        for (i = 0; i <= 1; i += (1 / steps)) 
        {
            setTimeout("setOpacity('" + id + "', "  + (1 - i) + ")", i * duration);
            fadeOutComplete = i * duration;
        }      
        //completely hide the displayed message after the fade effect is complete              
        setTimeout("hide('" + id + "')", fadeOutComplete);     
    }   

    function hide(id)
    {
        document.getElementById(id).style.display = 'none';     
    }
</script>

<div style="position: fixed; z-index: 1000; top: 200px; text-align:center; font-size:large;">
    <asp:Label ID="lblMessage" runat="server" SkinID="sknFlashLabel"  Style="display: none; font-weight:bold; font-size:large"/>
</div>
