<?php

require_once(dirname(__FILE__)."/nusoap/lib/nusoap.php");
 
//This is your webservice server WSDL URL address
$wsdl = "http://localhost/srs/ws/testservice.php?wsdl";
 
//create client object
$client = new nusoap_client($wsdl, 'wsdl');
 
$err = $client->getError();
if ($err) {
	// Display the error
	echo '<h2>Constructor error</h2>' . $err;
	// At this point, you know the call that follows will fail
        exit();
}
 
//calling our first simple entry point
//$result1=$client->call('hello', array('username'=>'sach'));
//print_r($result1); 
 
//call second function which return complex type
$result2 = $client->call('login', array('username'=>'admin', 'password'=>'passwordr') );
//$result2 would be an array/struct
//print_r($result2);

//$result3 = $client->call('GetAllAisles');
//$result3 = $client->call('GetAllProducts');
//print_r($result3);
//echo "test" . "<br>";
$result4 = $client->call('GetAllAisles');
//print_r($result4);

$stocktake_id=1;
$time_start='1/31/05 11:05:01 AM';
$time_finished='1/31/05 11:05:01 AM';
$user_name='admin';
$upload_status='0';

/*$result5 = $client->call('InsertStockCount2',
    array(array(
        "stocktake_id"=>$stocktake_id, 
        "time_start"=>$time_start,
		"time_finished"=>$time_finished,
		"user_name"=>$user_name,
		"upload_status"=>$upload_status
    ))
);*/
//print_r($result5);

/*				
$stocktake_id = $result5['stocktake_id'];
$startdatetime	= $result5['time_start'];
$enddatetime	=$result5['time_finished'];
$username	=$result5['user_name'];
$status	=$result5['upload_status'];*/

//$strsql = "INSERT INTO stocktake (mobile_stocktake_id,start_datetime,end_datetime,username,upload_status) VALUES($stocktake_id,'$startdatetime','$enddatetime','$username','$status')";				
//echo "<br/><br/>" . $strsql;


      /*  'stocktake_id' => array('name'=>'stocktake_id','type'=>'xsd:int'),
        'aisle_number' => array('name'=>'aisle_number','type'=>'xsd:string'),
        'tag_id' => array('name'=>'tag_id','type'=>'xsd:string'),*/

$sitems[] = array("stocktake_id" => 1, "aisle_number" => "7", "tag_id" => "456454654564564","aisle_id" => 9); 
$sitems[] = array("stocktake_id" => 1, "aisle_number" => "8", "tag_id" => "897897897898978","aisle_id" => 9);
$sitems[] = array("stocktake_id" => 1, "aisle_number" => "9", "tag_id" => "123123142893423","aisle_id" => 9);

$result6 = $client->call('InsertStockCount3',
    array($sitems));

print_r($result6);

/*
$result7 = $client->call('InsertStockCount',
    array($sitems,array(
        "stocktake_id"=>$stocktake_id, 
        "time_start"=>$time_start,
		"time_finished"=>$time_finished,
		"user_name"=>$user_name,
		"upload_status"=>$upload_status
    ))
);
print_r($result7);*/









function array_to_objecttree($array) {
  if (is_numeric(key($array))) { // Because Filters->Filter should be an array
    foreach ($array as $key => $value) {
      $array[$key] = array_to_objecttree($value);
    }
    return $array;
  }
  $Object = new stdClass;
  foreach ($array as $key => $value) {
    if (is_array($value)) {
      $Object->$key = array_to_objecttree($value);
    }  else {
      $Object->$key = $value;
    }
  }
  return $Object;
}


/*
if($client->fault)
{
	echo "FAULT: <p>Code: (".$client->faultcode.")</p>";
	echo "String: ".$client->faultstring;
}
else
{
	$r = $result3;
	$count = count($r);
	echo $count;
	print_r($result3);
}*/

?>