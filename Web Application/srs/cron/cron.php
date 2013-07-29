<?php
require_once(dirname(__FILE__)."/../ws/classes/connection.class.php");

//http://stackoverflow.com/questions/1019076/how-to-search-by-key-value-in-a-multidimensional-array-in-php
function search($array, $key, $value) {
    $results = array();

    if (is_array($array)) {
        if (isset($array[$key]) && $array[$key] == $value)
            $results[] = $array;

        foreach ($array as $subarray)
            $results = array_merge($results, search($subarray, $key, $value));
    }

    return $results;
}
 
	//Define the variable
	$picklist_id = -1;
	
	
	//Database connection
	$database = new Connection();
		
	//Limit query to 24hrs as we do not need all the records
	
	$yesterday = date("Y-m-d H:i:s", (time()-(24 * 60 * 60)));
	$now = date("Y-m-d H:i:s", time());
	$strQuery = "SELECT * FROM stocktake where end_datetime BETWEEN '$yesterday' and '$now' ORDER by end_datetime Desc";
	
	$stresult = $database->runQuery($strQuery); // Go through the stocktake table 
	$snum = mysql_num_rows($stresult);
	//echo $strQuery;
	
	if ($snum == 0) {
		echo "No Results Found";
	} else {

		for ($l = 0; $l < $snum; $l++) {

			$stockid = mysql_result($stresult, $l, "stocktake_id");
			$mobile_stocktake_id = mysql_result($stresult, $l, "mobile_stocktake_id");
			$start_datetime = mysql_result($stresult, $l, "start_datetime");
			$end_datetime = mysql_result($stresult, $l, "end_datetime");
			$username = mysql_result($stresult, $l, "username");
			
			
			//Check if stock id if exists
			$strStock = "SELECT * FROM picklist WHERE stocktake_id = $stockid";
			$strStockResult = $database->runQuery($strStock); // Go through the stocktake table 
			$stocknum = mysql_num_rows($strStockResult);
			
			//echo $stocknum;
			//Do this if the stock id is not in the table
			if($stocknum==0)
			{
				
				
				//Insert a recod to the picklist table
				$strSQLInsert = "INSERT INTO picklist (picklist_datetime,username,stocktake_id) VALUES(NOW(),'$username',$stockid)";
				
				$picklist_id = $database->runQueryReturnid($strSQLInsert);

				//Run the following query for all the stock ids found
				$strSQL = "SELECT * FROM
						(
						SELECT p.products_id,p.products_name,p.products_description,
							pi.products_quantity,pi.min_order_quantity,pi.max_shelf_quantity,pi.min_shelf_quantity,pi.warehouse_location_id,
							a.aisle_id as correct_aisle_id,a.aisle_description,a.aisle_number
						FROM aisles a, products p, productsinventory pi
						WHERE
						pi.aisle_id = a.aisle_id
						AND p.products_id = pi.products_id
						AND a.aisle_status=1) as t1
						LEFT JOIN
						(SELECT count(rtd.products_id) as pcnt,sd.aisle_id as actual_aisle_id, rtd.products_id as t2pid FROM stocktakedetails sd
										LEFT JOIN rfidtagdata rtd 
										ON sd.rfid_tag_id = rtd.rfid_tag_id
										WHERE sd.stocktake_id=" . $stockid . "   
						GROUP BY sd.aisle_id,rtd.products_id               
						ORDER BY sd.aisle_id) as t2
						ON t1.products_id= t2.t2pid
						ORDER BY t1.aisle_number";
				
				//echo $strSQL;
				
				$presult = $database->runQuery($strSQL); // Get the products count

				$pnum = mysql_num_rows($presult);

				if ($pnum == 0) {
					echo "No Results Found";
				} else {

					$pList = array();
					for ($k = 0; $k < $pnum; $k++) {


						$products_id = mysql_result($presult, $k, "products_id");
						$products_name = mysql_result($presult, $k, "products_name");
						$products_description = mysql_result($presult, $k, "products_description");
						$products_quantity = mysql_result($presult, $k, "products_quantity");
						$min_order_quantity = mysql_result($presult, $k, "min_order_quantity");
						$max_shelf_quantity = mysql_result($presult, $k, "max_shelf_quantity");
						$min_shelf_quantity = mysql_result($presult, $k, "min_shelf_quantity");
						$correct_aisle_id = mysql_result($presult, $k, "correct_aisle_id");
						$aisle_description = mysql_result($presult, $k, "aisle_description");
						$aisle_number = mysql_result($presult, $k, "aisle_number");
						$pcnt = mysql_result($presult, $k, "pcnt");
						$actual_aisle_id = mysql_result($presult, $k, "actual_aisle_id");
						$warehouse_location_id = mysql_result($presult, $k, "warehouse_location_id");
						$t2pid = mysql_result($presult, $k, "t2pid");


						$tempArray = array(
							'products_id' => $products_id,
							'products_name' => $products_name,
							'products_description' => $products_description,
							'products_quantity' => $products_quantity,
							'min_order_quantity' => $min_order_quantity,
							'max_shelf_quantity' => $max_shelf_quantity,
							'min_shelf_quantity' => $min_shelf_quantity,
							'correct_aisle_id' => $correct_aisle_id,
							'aisle_description' => $aisle_description,
							'aisle_number' => $aisle_number,
							'pcnt' => $pcnt,
							'actual_aisle_id' => $actual_aisle_id,
							'warehouse_location_id'=> $warehouse_location_id,
							't2pid' => $t2pid,
						);
						array_push($pList, $tempArray);


						/*if (!isset($products_id) || empty($products_id)) {
							$row['firstname'] = 'No Info';
						}*/
					}
					// print_r($pList);
				}
				
				
				
			//Running the report for all the active aisles

			$strSQL = "SELECT * FROM aisles where aisle_status=1 ORDER BY aisle_number";
			$aresult = $database->runQuery($strSQL); // Get the products count

			$anum = mysql_num_rows($aresult);

			if ($anum == 0) {
				echo "No Aisles Found";
			} else {
			
			 for ($i = 0; $i < $anum; $i++) {
					$aid = mysql_result($aresult, $i, "aisle_id");
					$ano = mysql_result($aresult, $i, "aisle_number");
					$adesc = mysql_result($aresult, $i, "aisle_description");
					
					//Running the report for all the products in the aisle

					$strSQL = "SELECT p.products_id,pi.aisle_id FROM products p, productsinventory pi 
								WHERE aisle_id=" . $aid . "
								AND p.products_id = pi.products_id";
					
					//echo $strSQL . "<br>";
					$result2 = $database->runQuery($strSQL); // Get the products count
								
					$num2 = mysql_num_rows($result2);

							if ($num2 == 0) {
								//  echo "No Products Found";
							} else {
								for ($j = 0; $j < $num2; $j++) {

									$tmpResult = search($pList, 'products_id', mysql_result($result2, $j, "products_id"));

									foreach ($tmpResult as $value) {

										$tmpResult2 = search($tmpResult, 'products_id', $value['products_id']);
										//This means the product id is found more than one location
										if (sizeof($tmpResult2) > 1) {
											
											
											//Product is in a different aisles
											$tmpResult3 = search($tmpResult2, 'actual_aisle_id', $aid);


											//Loop but this has to be only 1 record 
											foreach ($tmpResult3 as $value3) {
												if (is_null($value3['pcnt'])) {
													$count = 0;
												} else {
													$count = $value3['pcnt'];
												}
												if ($value3['actual_aisle_id'] != $value3['correct_aisle_id']) {
													$count = 0;
													//echo "his";
												}
												if ($value3['actual_aisle_id'] == $value['correct_aisle_id']) {
													
													$qty_required = ($value3['max_shelf_quantity'] - $count);
													$v3_pid = $value3['products_id'];
													$v3_aid = $value['correct_aisle_id'];
													$v3_wid = $value3['warehouse_location_id'];
													

													//Insert a recod to the picklist details table
													$strSQLInsert = "INSERT INTO picklistdetails (picklist_id,products_id,required_qty,aisle_id,warehouse_location_id) ". 
																	"VALUES($picklist_id,$v3_pid,$qty_required,$v3_aid,$v3_wid)";
													//echo  "1 : " .	$strSQLInsert . "<br>";
													$picklistd_id = $database->runQueryReturnid($strSQLInsert);

												}
											}
											break; //Break the loop
										} else {
											//Either product is not found or found only in one location. Happy scenario

											if (is_null($value['pcnt'])) {
												$count = 0;
											} else {
												$count = $value['pcnt'];
											}
											if ($value['actual_aisle_id'] != $value['correct_aisle_id']) {
												$count = 0;
												//echo "his";
											}
											
											$qty_required = ($value['max_shelf_quantity'] - $count);
											$v_pid = $value['products_id'];
											$v_aid = $value['correct_aisle_id'];
											$v_wid = $value['warehouse_location_id'];
											
											//Insert a recod to the picklist details table
											$strSQLInsert = "INSERT INTO picklistdetails (picklist_id,products_id,required_qty,aisle_id,warehouse_location_id) ".
														  "VALUES($picklist_id,$v_pid,$qty_required,$v_aid,$v_wid)";
											//echo  "2 : " .	$strSQLInsert . "<br>";
											
											$picklistd_id = $database->runQueryReturnid($strSQLInsert);

										}
									  
									}
								}
							}

				
				
						}
					}
		
		    } // End of if for checking the stock id
		} //End of first for loop 

	} //End of else (For all the stocktake records)


?>