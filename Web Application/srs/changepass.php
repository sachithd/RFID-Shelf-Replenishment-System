<?php
require_once(dirname(__FILE__)."/ws/classes/connection.class.php");
session_start(); 
$statusMessage = '';


if (isset($_POST['btnChangePassword'])) {
    $username = mysql_real_escape_string($_POST['txtUsername']);
    $oldpassword = mysql_real_escape_string($_POST['txtOldPassword']);
    $newpassword = mysql_real_escape_string($_POST['txtNewPassword']);
    
    $database = new Connection();

    $strSQL = "UPDATE users SET password='$newpassword' WHERE username='$username' and password='$oldpassword'";
    //echo $strSQL;
    $database->runQuery($strSQL);
    $num = mysql_affected_rows();
    if ($num > 0) {
        $statusMessage = "New Password Updated";
        
    } else {
        $statusMessage = 'Username / Old Password do not match';
    }
    
}
?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Change User Password</title>
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
<h3>Change Password </h3>
<?php if($statusMessage != '') {?>
<p align="left"><strong><font color="#990000"><?php echo $statusMessage; ?></font></strong></p>
<?php }?>
<div align="center">
<form id="changePassword" name="changePassword" method="post" action="">
  <table width="400" border="0">
    <tr>
      <td>Username</td>
      <td><input type="text" name="txtUsername" id="txtUsername" readonly value="<?php echo $_SESSION['username'] ?>"/></td>
    </tr>
    <tr>
      <td>Old Password</td>
      <td><input type="password" name="txtOldPassword" id="txtOldPassword" /></td>
    </tr>
    <tr>
      <td>New Password</td>
      <td><input type="password" name="txtNewPassword" id="txtNewPassword" /></td>
    </tr>  
    <tr>
      <td colspan="2" align="center"><p>
        <br/>
              <input type="submit" name="btnChangePassword" id="btnChangePassword" value="Change Password" />
        <input type="reset" name="btnClear" id="btnClear" value="Clear" />
      </p></td>
    </tr>
  </table>
</form>
</div>
<p>&nbsp;</p>
</body>
</html>