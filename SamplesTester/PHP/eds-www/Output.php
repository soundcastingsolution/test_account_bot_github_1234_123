<?php

 include 'table.css';
 
 global $rows;
 global $NewResult;
 $NewResult = array();
 //$rows = array();

function ConstructArray($APIName,$APISubType,$APIType,$Version,$response)
{
	 $addNewReuslt = "<tr>
     <td>$APIName</td>
     <td>$APISubType </td>
	 <td>$APIType </td>
	 <td>$Version</td>
     <td>$response</td>
     </tr>";
	 $_SESSION["Header"] = $_SESSION["Header"] . $addNewReuslt;
	 $_SESSION["Result"] = explode(',', $_SESSION["Header"]); // string into array
	 array_push($_SESSION["Result"],  $_SESSION["Result"]);
}

function Generateoutput()
{
	global $rows;
	error_reporting(0);
	foreach($_SESSION["Result"] as $rows) {
     echo implode(',',$rows);// array into string
     }
     echo "</table>";
}
?>