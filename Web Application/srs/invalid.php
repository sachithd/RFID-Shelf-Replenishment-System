<?php
require_once(dirname(__FILE__)."/ws/classes/connection.class.php");

session_start();
//If not login redirect to the main page
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
                Invalid Items Found on the shop floor
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
                <input name="btnSubmit" value="Generate Invalid Items" type="submit"></div>
        </form></p>

    <?php
    if (!isset($_POST['btnSubmit']) || !isset($_POST['stocktake'])) {
        exit();
    } else {
        $stockid = $_POST['stocktake'];
        //Invalid RFID Tags
        $strSQL = "SELECT std.rfid_tag_id,std.aisle_id,a.aisle_number,a.aisle_Description FROM stocktakedetails std, aisles a 
           WHERE std.rfid_tag_id NOT IN (SELECT r.rfid_tag_id FROM rfidtagdata r ) 
           AND a.aisle_id = std.aisle_id
           AND stocktake_id = " . $stockid;
        $result = $database->runQuery($strSQL); // Get the products count


        $num = mysql_num_rows($result);

        if ($num == 0) {
            echo "No Invalid Tags Found.<br/><br/>";
        } else {

            echo '<form method="post" action="" name="PickListData">';
            $tagList = array();
            for ($i = 0; $i < $num; $i++) {

                $tempArray = array(
                    'rfid_tag_id' => mysql_result($result, $i, "rfid_tag_id"),
                    'aisle_number' => mysql_result($result, $i, "aisle_number"),
                    'aisle_Description' => mysql_result($result, $i, "aisle_Description"),
                );
                array_push($tagList, $tempArray);
            }


            //Running the report for miss placed items
            ?>
            <table id="box-table-a" summary="Invalid Tags">
                <thead>
                    <tr>
                        <th scope="col" class="rounded-company">Invalid Tags ID</th>
                        <th scope="col" class="rounded-q1">Aisle</th>  
                    </tr>
                </thead>
                <tfoot>
                </tfoot>
                <tbody>

                    <?php
                    foreach ($tagList as $value) {


                        echo "<tr>";
                        echo "<td>" . $value['rfid_tag_id'] . "</td>";
                        echo "<td>" . $value['aisle_number'] . " - " . $value['aisle_Description'] . "</td>";
                        echo "</tr>";
                    }
                }
            }
            ?>
        </tbody>
    </table>


    <input type="button"
           onClick="window.print()"
           value="Print This Page"/>
    <input name="btnSavePickList" value="Save Pick List" type="submit"></form>

</body>
</html>
