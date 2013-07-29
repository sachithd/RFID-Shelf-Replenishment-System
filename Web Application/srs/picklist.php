<?php
require_once(dirname(__FILE__)."/ws/classes/connection.class.php");

session_start();
//If not logged in redirect to the main page
if (!isset($_SESSION['username']) || !isset($_SESSION['userrole'])) {
    header('Location: login.php');
} else {
    $usertype = $_SESSION['userrole'];
}

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
?>        
<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <title>Pick List</title>
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



        <p>
        <form method="post" action="" name="GeneratePickList">

            <div style="text-align:center;">
                Generate Pick list from the mobile stock take completed on
                <select size="1" name="stocktake">
                    <?php
                    $database = new Connection();

                    $result = $database->runQuery("SELECT * FROM stocktake order by end_datetime Desc"); // Only active aisles
                    $num = mysql_num_rows($result);

                    if ($num == 0) {
                        echo "No Results Found";
                    } else {

                        for ($i = 0; $i < $num; $i++) {

                            $stocktake_id = mysql_result($result, $i, "stocktake_id");
                            $mobile_stocktake_id = mysql_result($result, $i, "mobile_stocktake_id");
                            $start_datetime = mysql_result($result, $i, "start_datetime");
                            $end_datetime = mysql_result($result, $i, "end_datetime");
                            $username = mysql_result($result, $i, "username");
                            //$upload_status = mysql_result($result, $i, "upload_status");
                            echo '<option value= "' . $stocktake_id . '">' . $end_datetime . '</option>';
                        }
                    }
                    ?>

                </select>
                <br>

                <br/>
                <input name="btnSubmit" value="Generate Pick List" type="submit"></div>
        </form></p>

    <?php
    if (!isset($_POST['btnSubmit']) || !isset($_POST['stocktake'])) {
        exit();
    } else {
        $stockid = $_POST['stocktake'];


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

        $result = $database->runQuery($strSQL); // Get the products count

        $num = mysql_num_rows($result);

        if ($num == 0) {
            echo "No Results Found";
        } else {

            $pList = array();
            for ($i = 0; $i < $num; $i++) {


                $products_id = mysql_result($result, $i, "products_id");
                $products_name = mysql_result($result, $i, "products_name");
                $products_description = mysql_result($result, $i, "products_description");
                $products_quantity = mysql_result($result, $i, "products_quantity");
                $min_order_quantity = mysql_result($result, $i, "min_order_quantity");
                $max_shelf_quantity = mysql_result($result, $i, "max_shelf_quantity");
                $min_shelf_quantity = mysql_result($result, $i, "min_shelf_quantity");
                $correct_aisle_id = mysql_result($result, $i, "correct_aisle_id");
                $aisle_description = mysql_result($result, $i, "aisle_description");
                $aisle_number = mysql_result($result, $i, "aisle_number");
                $pcnt = mysql_result($result, $i, "pcnt");
                $actual_aisle_id = mysql_result($result, $i, "actual_aisle_id");
                $warehouse_location_id = mysql_result($result, $i, "warehouse_location_id");
				$t2pid = mysql_result($result, $i, "t2pid");


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


                if (!isset($products_id) || empty($products_id)) {
                    $row['firstname'] = 'No Info';
                }
            }
            // print_r($pList);
        }

        //Running the report for all the active aisles

        $strSQL = "SELECT * FROM aisles where aisle_status=1 ORDER BY aisle_number";
        $result = $database->runQuery($strSQL); // Get the products count

        $num = mysql_num_rows($result);

        if ($num == 0) {
            echo "No Aisles Found";
        } else {

            echo '<form method="post" action="" name="PickListData">';

            for ($i = 0; $i < $num; $i++) {
                $aid = mysql_result($result, $i, "aisle_id");
                $ano = mysql_result($result, $i, "aisle_number");
                $adesc = mysql_result($result, $i, "aisle_description");
                echo "<h3>" . $ano . " - " . $adesc . "</h3>";
                ?>
                <table id="box-table-a" summary="Pick List Generated for Aisles">
                    <thead>
                        <tr>
                            <th scope="col" class="rounded-company">Product ID</th>
                            <th scope="col" class="rounded-q1">Product Name</th>
                            <th scope="col" >Aisle Number</th>
                            <th scope="col" >Aisle Description</th>
                            <th scope="col" >Qty Required to Refill</th>
                            <th scope="col" >Warehouse Location</th>
                        </tr>
                    </thead>
                    <tfoot>
                    </tfoot>
                    <tbody>

                        <?php
                        //Running the report for all the products in the aisle

                        $strSQL = "SELECT p.products_id,pi.aisle_id FROM products p, productsinventory pi 
                            WHERE aisle_id=" . $aid . "
                            AND p.products_id = pi.products_id";
                        $result2 = $database->runQuery($strSQL); // Get the products count

                        $num2 = mysql_num_rows($result2);

                        if ($num2 == 0) {
                            //  echo "No Products Found";
                        } else {
                            for ($j = 0; $j < $num2; $j++) {

                                //print_r(search($pList, 'products_id', mysql_result($result2, $j, "products_id")));  
                                $tmpResult = search($pList, 'products_id', mysql_result($result2, $j, "products_id"));

                                foreach ($tmpResult as $value) {


                                    $tmpResult2 = search($tmpResult, 'products_id', $value['products_id']);
                                    if (sizeof($tmpResult2) > 1) {
                                        //Product is in different aisles
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
												
												// If number of items on the shelf is less than or equal to min level, the items need to be replenished urgently
												// So we show it in Red & Bold
												if($count<=$value3['min_shelf_quantity']) 
												{
													//$line = '<td bgcolor="red">' . $qty_required . '</td>';
													$line = '<td><font color="red"><b>' . $qty_required . '</b></font></td>';
												}
												else
												{
													$line = "<td>" . $qty_required . "</td>";
												}
												
												echo "<tr>";
                                                echo "<td>" . $value3['products_id'] . "</td>";
                                                echo "<td>" . $value3['products_name'] . "</td>";
                                                echo "<td>" . $value3['aisle_number'] . "</td>";
                                                echo "<td>" . $value3['aisle_description'] . "</td>";
                                                echo $line;
                                                echo "<td>" . $value3['warehouse_location_id'] . "</td>";
                                                echo "</tr>";
                                            }
                                        }
                                        break;
                                    } else {
                                        //Either product is not found or found only in one location

                                        if (is_null($value['pcnt'])) {
                                            $count = 0;
                                        } else {
                                            $count = $value['pcnt'];
                                        }
                                        if ($value['actual_aisle_id'] != $value['correct_aisle_id']) {
                                            $count = 0;

                                        }
										
										        $qty_required = ($value['max_shelf_quantity'] - $count);
												
												// If number of items on the shelf is less than or equal to min level, the items need to be replenished urgently
												// So we show it in Red & Bold
												if($count<=$value['min_shelf_quantity']) 
												{
													$line = '<td><font color="red"><b>' . $qty_required . '</b></font></td>';
												}
												else
												{
													$line = "<td>" . $qty_required . "</td>";
												}
										
										

                                        echo "<tr>";
                                        echo "<td>" . $value['products_id'] . "</td>";
                                        echo "<td>" . $value['products_name'] . "</td>";
                                        echo "<td>" . $value['aisle_number'] . "</td>";
                                        echo "<td>" . $value['aisle_description'] . "</td>";
                                        echo $line;
                                        echo "<td>" . $value['warehouse_location_id'] . "</td>";
                                        echo "</tr>";
                                    }

                                }
                            }
                        }

                        ?>
                    </tbody>
                </table>

                <?php
            }
        }
        ?>



        <input type="button"
               onClick="window.print()"
               value="Print This Page"/>
        <input name="btnSavePickList" value="Save Pick List" type="submit"></form>
    <?php
}
?>
</body>
</html>
