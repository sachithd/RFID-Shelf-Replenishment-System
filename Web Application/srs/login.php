<?php
require_once(dirname(__FILE__)."/ws/classes/connection.class.php");
session_start(); // This starts the session which is like a cookie, but it isn't saved on your hdd and is much more secure.
$errorMessage = '';

if (isset($_SESSION['username'])) {
    die("You are already logged in!");
}


if (!isset($_POST['btnSubmit'])) {
    
} else {

	$database = new Connection();
    //var_dump($_POST);
    $username = mysql_real_escape_string($_POST['txtUsername']);
    $password = mysql_real_escape_string($_POST['txtPassword']);

    

    $strSQL = "SELECT * FROM users u, userroles ur 
	WHERE username='$username' 
	AND password='$password'
	AND ur.user_role_id=u.user_role";
    //echo $strSQL;
    $result = $database->runQuery($strSQL);
    $num = mysql_num_rows($result);

    if ($num == 0) {
        $errorMessage = 'Invalid username or password. Please try again';
    } else {
        for ($i = 0; $i < $num; $i++) {
            $_SESSION['username'] = mysql_result($result, $i, "username");
            $_SESSION['userrole'] = mysql_result($result, $i, "user_role");
            header('Location: index.php');
            exit;
        }
    }
}
?>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>Login to RFID Shelf Replenishment System</title>
		
		<script>
		if (parent.frames.length > 0) {
		parent.location.href = self.document.location
		}
		</script>
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
                background-color:#FFF8C6;
                text-align:left
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
            p
            {
	text-align:center;
            }
            div{
                width: 600px ;
                margin-left: auto ;
                margin-right: auto ;
            }
            
        </style>
    </head>

    <body>
        <br/>
        <h1><img src="images/icon.png" width="50" height="50" alt="rfid" />RFID Shelf Replenishment System Online </h1>


        <div align="center"> 
                    <?php
        if ($errorMessage != '') {
            ?>
          <div align="center"><strong><font color="RED"><?php echo $errorMessage; ?></font></strong></div>
            <br/>
                <?php
        }
        ?>
            <div align="center">Please enter username and password to access the SRS Web Portal</div>
            <br/>
            <form id="frmLogin" name="frmLogin" method="post" action="">

                
                    <fieldset id="inputs"> 
                        
                            Username
                            <!--<td><input type="text" name="txtUsername" id="txtUsername" /></td>-->

                            <input id="username" name="txtUsername" type="text" placeholder="Username" autofocus required> 
                            <br/> <br/>Password
                            <!--<td><input type="password" name="txtPassword" id="txtPassword" /></td>-->
                            <input type="password" name="txtPassword" id="password" placeholder="Password" required/>
                    </fieldset>
                 
                                <fieldset id="actions">
                                    <input name="btnSubmit" value="Login" type="submit">
                                   <!-- <input type="submit" name="btnLogin" id="btnLogin" value="Login" />-->
                                        <input type="reset" name="btnClear" id="btnClear" value="Clear" /> </fieldset>
                            </p>
            </form>


        </div>
        <p>&nbsp;</p>
        <p><img src="images/mc9090.jpg" alt="mc9090" width="225" height="225" /></p>
    </body>
</html>