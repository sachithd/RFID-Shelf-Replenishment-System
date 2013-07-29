<!DOCTYPE html>
<html>
<head>
	<meta charset="utf-8" />
<?php 
foreach($css_files as $file): ?>
	<link type="text/css" rel="stylesheet" href="<?php echo $file; ?>" />
<?php endforeach; ?>
<?php foreach($js_files as $file): ?>
	<script src="<?php echo $file; ?>"></script>
<?php endforeach; ?>
<style type='text/css'>
body
{
	font-family: Verdana;
	font-size: 12px;
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


<!--	<div>
		<a href='<?php //echo site_url('main/user_management')?>'>Users</a> |
		<a href='<?php //echo site_url('main/userroles_management')?>'>User Roles</a> |
		<a href='<?php //echo site_url('main/aisle_management')?>'>Aisles</a> |
		<a href='<?php //echo site_url('main/products')?>'>Products</a> | 
		<a href='<?php //echo site_url('main/products_inventory')?>'>Products Inventory</a> |	
		<a href='<?php //echo site_url('main/rfid_tagdata')?>'>RFID Tag Data</a>	
	</div> -->
	<div style='height:20px;'></div>  
    <div>
		<?php echo $output; ?>
    </div>
</body>
</html>
