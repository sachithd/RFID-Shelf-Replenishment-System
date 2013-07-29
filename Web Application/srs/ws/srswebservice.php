<?php

require_once(dirname(__FILE__)."/nusoap/lib/nusoap.php");
require_once(dirname(__FILE__)."/classes/connection.class.php");
//$debug = 1;
$server = new nusoap_server;
 
$server->configureWSDL('server', 'urn:server');
 
$server->wsdl->schemaTargetNamespace = 'urn:server';



/******************************************************************************************************
*********************************	SOAP complex type return type (an array/struct) 	***************	
******************************************************************************************************/


// Aisles Structure
$server->wsdl->addComplexType(
    'Aisles',
    'complexType',
    'struct',
    'all',
    '',
    array(
        'aisle_id' => array('name' => 'aisle_id', 'type' => 'xsd:int'),
        'aisle_description' => array('name' => 'aisle_description', 'type' => 'xsd:string'),
        'aisle_number' => array('name' => 'aisle_number', 'type' => 'xsd:string')
    )
);		

//Aisles array
$server->wsdl->addComplexType(
'AislesArray',
'complexType',
'array',
'',
'SOAP-ENC:Array',
array(),
array(array('ref'=>'SOAP-ENC:arrayType','wsdl:arrayType'=>'tns:Aisles[]')),'tns:Aisles');	


//Stock 
$server->wsdl->addComplexType(
    'Stock',
    'complexType',
    'struct',
    'all',
    '',
    array(
        'stocktake_id' => array('name'=>'stocktake_id','type'=>'xsd:int'),
        'time_start' => array('name'=>'time_start','type'=>'xsd:string'),
        'time_finished' => array('name'=>'time_finished','type'=>'xsd:string'),
        'user_name' => array('name'=>'user_name','type'=>'xsd:string'),
        'upload_status' => array('name'=>'upload_status','type'=>'xsd:int')
    )
);


//Stock items
$server->wsdl->addComplexType(
    'StockItems',
    'complexType',
    'struct',
    'all',
    '',
    array(
        'stocktake_id' => array('name'=>'stocktake_id','type'=>'xsd:int'),
		'aisle_id' => array('name'=>'aisle_id','type'=>'xsd:int'),
        'aisle_number' => array('name'=>'aisle_number','type'=>'xsd:string'),
        'tag_id' => array('name'=>'tag_id','type'=>'xsd:string'),
    )
);


//Stock Items array
$server->wsdl->addComplexType(
'StockItemsArray',
'complexType',
'array',
'',
'SOAP-ENC:Array',
array(),
array(array('ref'=>'SOAP-ENC:arrayType','wsdl:arrayType'=>'tns:StockItems[]')),'tns:StockItems');	
/*
*/



/************************************************************************************
*********************************	webservice entry point/function 	************	
************************************************************************************/


//login webservice entry point/function 
$server->register('login',
			array('username' => 'xsd:string', 'password'=>'xsd:string'),  //parameters
			array('return' => 'xsd:int'),  //output
			'urn:server',   //namespace
			'urn:server#loginServer',  //soapaction
			'rpc', // style
			'encoded', // use
			'Check user login');  //description


//GetAllAisles webservice entry point/function 
$server->register('GetAllAisles',
			array(),  //parameters
			array('return' => 'tns:AislesArray'),  //output
			'urn:server',   //namespace
			'urn:server#productAllServer',  //soapaction
			'rpc', // style
			'encoded', // use
			'Return all products');  //description 	

			
//Insert stocktake data webservice entry point/function 
$server->register('InsertStockCount',
			array('StockItems'=>'tns:StockItemsArray', 'Stock'=>'tns:Stock'),  //parameters
			array('return' => 'xsd:int'),  //output
			'urn:server',   //namespace
			'urn:server#insertStockServer',  //soapaction
			'rpc', // style
			'encoded', // use
			'Insert stock take data');  //description 				

			
/***************************************************************************
*********************************	function implementations	************	
***************************************************************************/			
			

//Login function implementation 
function login($username, $password) {
	$loginsucess = -1;
	
	$database = new Connection();
	
	$strsql = "SELECT * FROM users u, userroles ur where username='$username' 
	AND password='$password' 
	AND ur.mobile_user=1
	AND ur.user_role_id = u.user_role";
	
	$result = $database->runQuery($strsql);
	$num = mysql_num_rows($result);
	
	if($num > 0)
	{
		//$loginsucess = 1;
		//User found with correct password and a mobile user
		$loginsucess= mysql_result($result,0,"user_role");
	}
	else
	{
		
		$loginsucess = 0;
	}	

	return $loginsucess;
}			
			

// GetAllAisles function implementation				
function GetAllAisles()
{

	$database = new Connection();
			
	$result = $database->runQuery("SELECT * FROM aisles where aisle_status=1"); // Only active aisles
	$num = mysql_num_rows($result);
	
	if($num == 0)
	{
		return "";
	}
	else
	{
		$results = array();
		
		for($i=0;$i<$num;$i++)
		{
			 $tempArray= array('aisle_id' => mysql_result($result,$i,"aisle_id"),
		 		'aisle_description' => mysql_result($result,$i,"aisle_description"),
		 		'aisle_number' => mysql_result($result,$i,"aisle_number"),
			);
			array_push($results, $tempArray);
			
		}
		
		return $results;

	}
	

}


//InsertStockCount function implementation
function InsertStockCount($stockItemsArray, $stock)
{

	$stockrecord = 0;
	$stocktakerecord = 0;
	
	
	$stocktake_id = $stock['stocktake_id'];
	$startdatetime = strtotime($stock['time_start']);
	$startdatetime = date("Y-m-d H:i:s", $startdatetime);
	$enddatetime= $stock['time_finished'];
	$enddatetime = $startdatetime;
	
	$username = $stock['user_name'];
	//$status = $stock['upload_status']; // Not required
	//$status = 1;
	
	$database = new Connection();
	//echo "sid : " . $stocktake_id;
	
	
	if(!is_null($stocktake_id))
	{
					
		$strsql = "INSERT INTO stocktake (mobile_stocktake_id,start_datetime,end_datetime,username) VALUES($stocktake_id,'$startdatetime','$enddatetime','$username')";
				
		$result = $database->runQueryReturnid($strsql);
		$num = $result;
		if($num > 0) //Recorded inserted sucessfully
		{
		
			$stockrecord = 1; //Flag 
			
			foreach($stockItemsArray as $value) 
			{
                $stocktake_ida = $value['stocktake_id'];
				$stocktake_ida = $num;
				$rfidtagid	= $value['tag_id'];
				$aisle_number	= $value['aisle_number'];
				$aisleid = $value['aisle_id'];
				
				if(!is_null($stocktake_ida))
				{
					
					$strsql = "INSERT INTO stocktakedetails (stocktake_id,rfid_tag_id,aisle_id) VALUES($stocktake_ida,'$rfidtagid','$aisleid')";
					
					$result = $database->runQuery($strsql);
					$num2 = $result;
					
					if($num2 > 0) //Stock take items inserted successfully
					{
						$stocktakerecord = 1;
					}
					else
					{
						$stocktakerecord = 2;
					}
					
				}	
				
			}	
		
		}
		else //No record stock inserted
		{
			$stockrecord = 2;
		}

		
	}
	
	if($stockrecord==1&&$stocktakerecord==1)
	{
		return 1; //Sucessfully inserted to both stock and stocktakedetails table
	}
	else if($stockrecord==1&&$stocktakerecord==2)
	{
		return 2; //Error inserting to the stocktakedetails table
	}
	else if($stockrecord==1&&$stocktakerecord==0)
	{
		return 3; //0 items scanned or empty aisle
	}
	else 
	{
		return 0;
	}
	
	
}				
			

$HTTP_RAW_POST_DATA = isset($HTTP_RAW_POST_DATA) ? $HTTP_RAW_POST_DATA : '';
 
$server->service($HTTP_RAW_POST_DATA);

exit();

?>