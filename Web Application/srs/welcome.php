<?php
session_start();
//If not login redirect to the main page
if (!isset($_SESSION['username']) || !isset($_SESSION['userrole'])) {
    header('Location: login.php');
} else {
    $usertype = $_SESSION['userrole'];
}
?>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Welcome Page</title>
        <style type="text/css">
            <!--
            @import url("style.css");
            -->
        </style>
                <style type='text/css'>
            body
            {
                font-family: Verdana;
                font-size: 13px;
                text-align: center;
                color:#2B1B17;
                background-color:white;
            }
            a {
                color: blue;
                text-decoration: none;
                font-size: 12px;
            }
            a:hover
            {
                text-decoration: underline;
            }
        </style>
</head>

<body>
<h2>Welcome to SRS Web Portal</h2>
<p><img src="images/mc9090.jpg" width="225" height="225" alt="mc9090" /></p>
<div align="left">
&nbsp;&nbsp;Please use the menu on the left to navigate through the various sections
<br/><br/>
&nbsp;&nbsp;The options are given according the user level.</div>
<p>&nbsp;</p>
<p>Technical queries please email: <a href="mailto:sachithd@gmail.com">sachithd@gmail.com</a></p>
</body>
</html>