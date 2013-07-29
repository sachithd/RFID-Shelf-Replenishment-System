<?php
session_start();
//If not login redirect to the main page
if (!isset($_SESSION['username']) || !isset($_SESSION['userrole'])) {
    header('Location: login.php');
} else {
    $usertype = $_SESSION['userrole'];
}

 if (isset($_POST['btnLogOut'])) 
{
  //session_start();
  $_SESSION = array();
  session_destroy(); 
  header('Location: login.php');
}


?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>Shelf Replenishment System Web</title>
        <link href="style.css" rel="stylesheet" type="text/css" />
    </head>

    <body>

        <div class="container">
            <div class="header">
                <h1 align="center"><img src="images/icon.png" width="105" height="98" />RFID Shelf Replenishment System - Web Portal</h1>
            </div>

            <div class="sidebar1">
                <p> 

<?php
//Legacy System data. System Admin only
if ($usertype == 3) {
    ?>

                        <h3>Stock Management</h3>
                    </p>

                    <ul class="nav">
                        <li><a target="frmcontent" href="/srs/crud/index.php/main/aisle_management">Aisles</a></li>
                        <li><a target="frmcontent" href="/srs/crud/index.php/main/products">Products</a></li>
                        <li><a target="frmcontent" href="/srs/crud/index.php/main/products_inventory">Products Inventory</a></li>
                        <li><a target="frmcontent" href="/srs/crud/index.php/main/rfid_tagdata">RFID Tag data</a></li>

                    </ul>

    <?php
}
//Web Admin and System Admin and Web User
if ($usertype == 1 || $usertype == 3 || $usertype == 4) {
    ?>

                    <h3>Reports</h3>
                    <ul class="nav">
                        <li><a target="frmcontent" href="picklist.php">Pick List</a></li>
                        <li><a target="frmcontent" href="missplaced.php">Misplaced Items</a></li>
                        <li><a target="frmcontent" href="invalid.php">Invalid Tags Found</a></li>
                    </ul>
<?php } 
//Web Admin and System Admin Only
if ($usertype == 3 || $usertype == 1) {

?>


              <h3>View Stock Take</h3>
				<ul class="nav">
                    <li><a target="frmcontent" href="/srs/crud/index.php/main/stocktake_management">Stock-take</a></li>
					<li><a target="frmcontent" href="/srs/crud/index.php/main/stocktakedetails_management">Stock-take Item Details</a></li>
					</ul>
			   <h3>Auto Generated Pick List</h3>
				<ul class="nav">
                    <li><a target="frmcontent" href="/srs/crud/index.php/main/picklist_management">Pick List</a></li>
					<li><a target="frmcontent" href="/srs/crud/index.php/main/picklistdetails_management">Pick List Item Details</a></li>
					</ul>		
                        <?php
}
  ?>
                    
                <h3>User Management</h3>
                <ul class="nav">
                <!-- Allows users to change the current password-->
                    <li><a target="frmcontent" href="changepass.php">Change Password</a></li>
                <?php
				//Web Admin and System Admin Only
                if ($usertype == 3 || $usertype == 1) {
                    ?>
                        <li><a target="frmcontent" href="/srs/crud/index.php/main/user_management">User Management</a></li>
						<li><a target="frmcontent" href="/srs/crud/index.php/main/userroles_management">User Roles</a></li>
                </ul><?php } ?>
                <form id="frmLogout" name="frmLogout" method="post" action="">
                  <?php echo "<br/><h4> Logged in user:  <font color=red>" . $_SESSION['username'] . "</font></h><br/>"; ?>
				  <h3><input name="btnLogOut" type="submit" class="nav" id="btnLogOut" value="Logout" /></h3>
              </form>
                <p>&nbsp;</p>
                <!-- end .sidebar1 --></div>
            <div class="content">

                <iframe src="welcome.php" name="frmcontent" width="100%" height="595" frameborder="0"></iframe>

                <!-- end .content -->
            </div>

            <div class="footer">
                <p>Copyright © 1996-2009 SRS LTD, All Rights Reserved.</p>
                <!-- end .footer --></div>
            <!-- end .container --></div>
    </body>
</html>