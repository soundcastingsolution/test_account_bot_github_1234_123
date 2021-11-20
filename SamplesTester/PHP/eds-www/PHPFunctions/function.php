<?php
 include_once 'Output.php';
  //The following code sample assumes that the access token is stored in the session
session_start();

function getAgents(){
  
  //Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {
    $api_URL="/services/v13.0/agents";
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    //Set to TRUE to return the output of curl_exec as a string
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true);
    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request
    $response = curl_exec($handle);
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      //echo "Status Code of the Response:".$http_code;
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);
      //echo "Response Header:".$response_headers;
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
        //echo "Response Body:";
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);
        //Use the response data
        //print_r($parsed_json);
        ConstructArray('Admin','Agent','GET /agents','v13.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
    // Close the curl session
    curl_close($handle);
    //Report all PHP errors (notices, errors, warnings, etc.)
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
	ConstructArray('Admin','Skills','GET /agents','v13.0','No access Token');
    //echo "No access Token";
    }
function PostAgentV13(){	
if($_SESSION["access_token"]!="")
  {	
	$data = array(
	  "firstName"=>"hC8 First",
	  "middleName"=>"hC8 Mid"
	  );
    $data_json = json_encode($data);
    $api_URL='/services/v13.0/agents';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($ch, CURLOPT_POST, 1);
    curl_setopt($ch, CURLOPT_POSTFIELDS,data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);
		ConstructArray('Admin','Agent','POST /services/v13.0/agents','v13.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	}
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
	}
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Agent','GET','v13.0','No access Token');
}
function updateAgents($agentId)
{
  if($_SESSION["access_token"]!="")
   {
//create post data agent object
     $data = array(      
      'agents'=>[array(
      "firstName"=>"First Agent",
    "middleName"=>"Mid Agent",
    "lastName"=>"string",
    "teamId"=> "string",
    "teamUuId"=> "string",
    "reportToId"=> 0,
    "internalId"=> "string",
    "profileId"=> 0,
    "roleId"=> "string",
    "emailAddress"=> "string",
    "userName"=> "string",
    "userId"=> "string",
    "timeZone"=> "string",
    "country"=> "string",
    "state"=> "string",
    "city"=> "string",
    "chatRefusalTimeout"=> 0,
    "phoneRefusalTimeout"=> 0,
    "workItemRefusalTimeout"=> 0,
    "defaultDialingPattern"=> 0,
    "useTeamMaxConcurrentChats"=> true,
    "maxConcurrentChats"=> 0,
    "isActive"=> true,
    "locationId"=> 0,
    "notes"=> "string",
    "hireDate"=> "2018-12-19T17=>56=>35.151Z",
    "terminationDate"=> "2018-12-19T17=>56=>35.151Z",
    "hourlyCost"=> 0,
    "rehireStatus"=> true,
    "employmentType"=> "1",
    "referral"=> "string",
    "atHome"=> true,
    "hiringSource"=> 0,
    "custom1"=> "string",
    "custom2"=> "string",
    "custom3"=> "string",
    "custom4"=> "string",
    "custom5"=> "string",
    "scheduleNotification"=> "5",
    "federatedId"=> "string",
    "useTeamEmailAutoParkingLimit"=> true,
    "maxEmailAutoParkingLimit"=> 0,
    "sipUser"=> "string",
    "useAgentTimeZone"=> true,
    "timeDisplayFormat"=> 0,
    "sendEmailNotifications"=> true,
    "isWhatIfAgent"=> true)]);
    $data_json = json_encode($data);
    $api_URL='/services/v13.0/agents/'.$agentId;
    //Creating the endpoint for the request   
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
	curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
	curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
	$headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
	  $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
		  $parsed_json=json_decode($response_body);
    	  ConstructArray('Admin','Agent','PUT /agents/{agentId}','v13.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Agent','GET','v13.0','No access Token');
}

function getTeam($fields,$updatedSince){
//Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {
	//Optional parameters
    //$param=rawurlencode($_POST[$fields]);    
    //$param1=rawurlencode($_POST['updatedSince']);
    $api_URL="/services/v13.0/teams?fields=".$fields."&updatedSince=".$updatedSince;
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
        
    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      //echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      //echo "<br>";      
      //echo "Response Header:".$response_headers;
      //echo "<br>";
      //echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
        //echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);
		ConstructArray('Admin','Agent','GET /teams','v13.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
        //Use the response data
        //print_r($parsed_json);
      }
    }
    // Close the curl session  
    curl_close($handle);
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
}
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Agent','GET /teams','v13.0','No access Token');
}
function CreateTeamV13($teamName='',$isActive='')
{
if($_SESSION["access_token"]!="")
  {
     //create post team json data
$data = array(
'teams'=> [array(
   
   "teamName"=>$teamName,
      "isActive"=> $isActive,
      "maxConcurrentChats"=> 8,
      "wfoEnabled"=> false,
      "wfm2Enabled"=> false,
      "qm2Enabled"=> false,
      "inViewEnabled"=> false,
      "notes"=> "",
      "maxEmailAutoParkingLimit"=> 25,
      "inViewGamificationEnabled"=> false,
      "inViewChatEnabled"=> false,
      "inViewLMSEnabled"=> false,
      "analyticsEnabled"=> false,
      "requestContact"=> false,
      "chatThreshold"=> 1,
      "emailThreshold"=> 1,
      "workItemThreshold"=> 1,
      "voiceThreshold"=> 1,
      "contactAutoFocus"=> false,
      "teamUuid"=> ""
)]);
    
	$data_json = json_encode($data);
	$api_URL='/services/v13.0/teams';
    //Creating the endpoint for the request
     $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
        //echo $parsed_json;
		ConstructArray('Admin','Agent','POST /teams','v13.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Agent','POST /teams','v13.0','No access Token');
}
function getTeambyTeamID($teamId){
if($_SESSION["access_token"]!="")
  {
    $apiURL="/services/v13.0/teams/".$teamId;
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$apiURL;
        
    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    //By default, cURL is setup to not trust any CAs
    //Hence doesnot support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($ch, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($ch, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
    //Handling valid response
    if($response!=FALSE)
    {
      $httpCode = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      //echo "Status Code of the Response:".$httpCode;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      //echo "<br>";      
      //echo "Response Header:".$response_headers;
      //echo "<br>";
      //echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
        //echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsedjson=json_decode($response_body);  
        //Use the response data
        //print_r($parsedjson);
		ConstructArray('Admin','Agent','GET /teams/{teamId}','v13.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
    // Close the curl session  
    curl_close($handle);
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Agent','GET','v13.0','No access Token');
}
function UpdateTeamV13($teamID,$teamName='',$isActive='')
{
if($_SESSION["access_token"]!="")
  {
    //create post team json data
    $data = array(
   'teams'=> [array(
   "teamName"=> $teamName,
    "isActive"=> $isActive,
    "maxConcurrentChats"=> 8,
    "wfoEnabled"=> false,
    "wfm2Enabled"=> false,
    "qm2Enabled"=> false,
    "inViewEnabled"=> false,
    "notes"=> "",
    "maxEmailAutoParkingLimit"=> 25,
    "inViewGamificationEnabled"=> false,
    "inViewChatEnabled"=> false,
    "inViewLMSEnabled"=> false,
    "analyticsEnabled"=> false,
    "requestContact"=> false,
    "chatThreshold"=> 1,
    "emailThreshold"=> 1,
    "workItemThreshold"=> 1,
    "voiceThreshold"=> 1,
    "contactAutoFocus"=> false,
    "teamUuid"=> "")]);
 
    $data_json = json_encode($data);
    $api_URL='/services/v13.0/teams/'.$teamID;
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
        //echo $parsed_json;
		ConstructArray('Admin','Agent','PUT /services/{version}/teams/','v13.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Agent','PUT /services/{version}/teams/','v13.0','No access Token');
}
function getTeamUnavailableCode($teamId){
if($_SESSION["access_token"]!="")
  {
    //team_id is a required field
    //$param=rawurlencode($_POST['teamId']);
    $api_URL="/services/v13.0/teams/".$teamId."/unavailable-codes";
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
 
    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
 
    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
 
    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      //echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      //echo "<br>";     
      //echo "Response Header:".$response_headers;
      //echo "<br>";
      //echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
        //echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  
 
        //Use the response data
        //print_r($parsed_json);
		ConstructArray('Admin','Agent','GET /teams/{teamId}/unavailable-codes','v13.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Agent','GET /teams/{teamId}/unavailable-codes','No access Token');
}
function putTeamUnavailableCode($teamId){
if($_SESSION["access_token"]!="")
  {
    //team_id is a required field
    //$param=rawurlencode($_POST['team_id']);
	$param=rawurlencode($_POST[$teamId]);
    $api_URL="/services/v13.0/teams/".$teamId."/unavailable-codes";
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
 
    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
 
    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
 
    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      //echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      //echo "";     
      //echo "Response Header:".$response_headers;
      //echo "";
      //echo "";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
        //echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  
 
        //Use the response data
        //print_r($parsed_json);
		ConstructArray('Admin','Agent','PUT /teams/{teamId}/unavailable-codes','v13.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Agent','PUT /teams/{teamId}/unavailable-codes','No access Token');
}
function getGeneralUnavailableCode(){
	//Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {
    $api_URL="/services/v13.0/unavailable-codes";
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
 
    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
 
    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
 
    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      //echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      //echo "<br>";      
      //echo "Response Header:".$response_headers;
      //echo "<br>";
      //echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
        //echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  
 
        //Use the response data
        //print_r($parsed_json);
		ConstructArray('Admin','General','GET /unavailable-codes','v13.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','General','GET /unavailable-codes','v13.0','No access Token');
}
function getSkills(){
	//Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {
    $api_URL="/services/v13.0/skills";
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      //echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      //echo "<br>";     
      //echo "Response Header:".$response_headers;
      //echo "<br>";
      //echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
        //echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  
        //Use the response data
        //print_r($parsed_json);
		ConstructArray('Admin','Skills','GET /skills','v13.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
    // Close the curl session  
    curl_close($handle);
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Skills','GET /skills','v13.0','No access Token');
}
function postSkill($skillName='')
{
if($_SESSION["access_token"]!="")
  {
$data = array(
'skills'=>[array(
	  "mediaTypeId"=> 4,
      "skillName"=> $skillName,
      "isOutbound"=> true,
      "outboundStrategy"=> "Personal Connection",
      "campaignId"=> 1,
      "callerIdOverride"=> "8015554422",
      "emailFromAddress"=> "test@test.com",
      "emailFromEditable"=> false,
      "emailBccAddress"=> "",
      "scriptId"=> 2,
      "reskillHours"=> 4,
      "minWFIAgents"=> 4,
      "interruptible"=> false,
      "enableParking"=> false,
      "minWorkingTime"=> 4,
      "agentless"=> false,
      "agentlessPorts"=> 6,
      "notes"=> "this is a test note",
      "acwTypeId"=> 3,
      "requireDisposition"=> false,
      "allowSecondaryDisposition"=> false,
      "scriptDisposition"=> false,
      "stateIdACW"=> 2,
      "maxSecondsACW"=> 3,
      "acwPostTimeoutStateId"=> 53,
      "agentRestTime"=> 4,
      "displayThankyou"=> false,
      "thankYouLink"=> "no",
      "popThankYou"=> true,
      "popThankYouURL"=> "tester.com",
      "makeTranscriptAvailable"=> true,
      "transcriptFromAddress"=> "fromMe@email.com",
      "priorityBlending"=> false,
      "callSuppressionScriptId"=> 4,
      "useScreenPops"=> true,
      "screenPopTriggerEvent"=> "popTriggerEvent",
      "useCustomScreenPops"=> false,
      "screenPopType"=> "webpage",
      "screenPopDetails"=> "http://not",
      "initialPriority"=> 4,
      "acceleration"=> 5,
      "maxPriority"=> 10,
      "serviceLevelThreshold"=> 51,
      "serviceLevelGoal"=> 24,
      "enableShortAbandon"=> true,
      "shortAbandonThreshold"=> 123,
      "countShortAbandons"=> true,
      "countOtherAbandons"=> true,
      "chatWarningTheshold"=> 0,
      "agentTypingIndicator"=> false,
      "patronTypingPreview"=> false,
      "smsTransportCodeId"=> null,
      "messageTemplateId"=> null,
      "deliverMultipleNumbersSerially"=> false,
      "cradleToGrave"=> false,
      "priorityInterrupt"=> false,
      "treatProgressAsRinging"=> false,
      "preConnectCPAEnabled"=> false,
      "agentOverrideFax"=> true,
      "agentOverrideAnsweringMachine"=> true,
      "agentOverrideBadNumber"=> true,
      "dispositions"=> [array(
         "dispositionId"=> 1,
         "priority"=> 1
        
  )]
)]);
//create post json data
$data_json = json_encode($data);
$api_URL='/services/v13.0/skills';
//Creating the endpoint for the request  
$endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
 $handle = curl_init($endpoint);
 curl_setopt($handle, CURLOPT_HEADER, true);
 curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
 curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
 curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);	
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
	  $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		  $parsed_json=json_decode($response_body);  
            //echo $parsed_json;
		ConstructArray('Admin','Skills','POST /skills','v13.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }	 
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Skills','POST /skills','v13.0','No access Token');
}
function getSkillsBySkillId($skills_id){
	//Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {
    //skills_id is a required field
    //$param=rawurlencode($_POST['skills_id']);
    $api_URL="/services/v13.0/skills/".$skills_id;
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      //echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      //echo "<br>";      
      //echo "Response Header:".$response_headers;
      //echo "<br>";
      //echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
        //echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  
        //Use the response data
        //print_r($parsed_json);
		ConstructArray('Admin','Skills','GET /skills/{skillId}','v13.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
    // Close the curl session  
    curl_close($handle);
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Skills','GET /skills/{skillId}','v13.0','No access Token');
}
function updateSkill($skillName,$skillId)
{
if($_SESSION["access_token"]!="")
  {
  $data = array(
'skill'=>[array(
"skillName"=> $skillName,
    "isActive"=> true,
    "campaignId"=> 2,
    "callerIdOverride"=> "string",
    "emailFromAddress"=> "test@test.com",
    "emailFromEditable"=> false,
    "emailBccAddress"=> "",
    "scriptId"=> 2,
    "reskillHours"=> 4,
    "minWFIAgents"=> 4,
    "interruptible"=> false,
    "enableParking"=> false,
    "minWorkingTime"=> 4,
    "agentless"=> false,
    "agentlessPorts"=> 6,
    "notes"=> "this is a test note",
    "acwTypeId"=> 3,
    "requireDisposition"=> false,
    "allowSecondaryDisposition"=> false,
    "scriptDisposition"=> false,
    "stateIdACW"=> 2,
    "maxSecondsACW"=> 3,
    "acwPostTimeoutStateId"=> 53,
    "agentRestTime"=> 4,
    "displayThankyou"=> false,
    "thankYouLink"=> "no",
    "popThankYou"=> true,
    "popThankYouURL"=> "tester.com",
    "makeTranscriptAvailable"=> true,
    "transcriptFromAddress"=> "fromMe@email.com",
    "priorityBlending"=> false,
    "callSuppressionScriptId"=> 4,
    "useScreenPops"=> true,
    "screenPopTriggerEvent"=> "bleh",
    "useCustomScreenPops"=> false,
    "screenPopType"=> "webpage",
    "screenPopDetails"=> "http://no",
    "initialPriority"=> 4,
    "acceleration"=> 5,
    "maxPriority"=> 10,
    "serviceLevelThreshold"=> 51,
    "serviceLevelGoal"=> 24,
    "enableShortAbandon"=> true,
    "shortAbandonThreshold"=> 123,
    "countShortAbandons"=> true,
    "countOtherAbandons"=> true,
    "chatWarningTheshold"=> 0,
    "agentTypingIndicator"=> false,
    "patronTypingPreview"=> false,
    "smsTransportCodeId"=> null,
    "messageTemplateId"=> null,
    "deliverMultipleNumbersSerially"=> false,
    "cradleToGrave"=> false,
    "priorityInterrupt"=> false,
    "treatProgressAsRinging"=> false,
    "preConnectCPAEnabled"=> false,
    "agentOverrideFax"=> true,
    "agentOverrideAnsweringMachine"=> true,
    "agentOverrideBadNumber"=> true,
    "dispositions"=>[array(
        "dispositionId"=> 1,
        "priority"=> 1
    )]
)]);
//create post json data
    $data_json = json_encode($data);	
    $api_URL='/services/v13.0/skills/'.$skillId;
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);      
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
         	// echo $parsed_json;
		ConstructArray('Admin','Skills','PUT /skills/{skillId}','v13.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Skills','PUT /skills/{skillId}','v13.0','No access Token');
}
function getSkillsGeneralSettingBySkillId($skillId,$fields='')
{
if($_SESSION["access_token"]!="")
  {  
    $data = array(
	  'fields'=>$fields	  
	  );
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';   
    $api_URL='/services/v13.0/skills/'.$skillId.'/parameters/general-settings';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL.'?'.$params;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);      
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
    		 //echo $parsed_json;
		ConstructArray('Admin','Skills','GET /skills/{skillId}/parameters/general-settings','v13.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
	  }
	  
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Skills','GET /skills/{skillId}/parameters/general-settings','v13.0','No access Token');
}
function updateSkillsGeneralSettingBySkillId($skillId)
{
     if($_SESSION["access_token"]!="")
   {
    $data = array(
'generalSettings'=>[array(
"minimumRetryMinutes"=> 12,
    "maximumAttempts"=> 10,
    "defaultContactExpiration"=> 10,
    "getPriorityContactsOnContactinsertion"=> false,
    "loadCallbacks"=> false,
    "loadFresh"=> false,
    "loadNonFresh"=> false,
    "overrideBusinessUnitAbandonRate"=> false,
    "maximumRingingDuration"=> 1,
    "beginDampenPercentage"=> 1,
    "abandonRateCutoff"=> 1,
    "abandonRateThreshold"=>1,
    "inactiveBlenderTimer"=> 1,
    "maximumRatio"=> 1,
    "aggressiveness"=> "conservative",
    "endOfListNotificationsDelay"=> 15,
    "notifyAgentsWhenListIsEmpty"=> false,
    "percentageOfAgentsBeforeOverdial"=> 5,
    "blockMultipleCalls"=>true,
    "consecutiveAttemptsWithoutALiveConnect"=> 5
)]);
    //create post json data
    $data_json = json_encode($data);	
    $api_URL='/services/v13.0/skills/'.$skillId.'/parameters/general-settings';
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);      
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
     	//echo $parsed_json;
		ConstructArray('Admin','Skills','PUT /skills/{skillId}/parameters/general-settings','v13.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Skills','PUT /skills/{skillId}/parameters/general-settings','v13.0','No access Token');
}
function getCpamanagementBySkillId($skillId,$fields='')
{
if($_SESSION["access_token"]!="")
  {  
    $data = array(	  
	  'fields'=>$fields,
	  );
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';   
    $api_URL='/services/v13.0/skills'.$skillId.'/parameters/cpa-management';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL.'?'.$params;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);      
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
    		//echo $parsed_json;
		ConstructArray('Admin','Skills','GET /skills/{skillId}/parameters/cpa-management','v13.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
	  }
	  
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Skills','GET /skills/{skillId}/parameters/cpa-management','v13.0','No access Token');
}
function updateSkillsCpamanagementBySkillId($skillId)
{
     if($_SESSION["access_token"]!="")
   {
     $data = array(
	'cpaSettings'=>[array(
	"abandonMessagePath"=>"string",
    "abandonMsgMode"=> 0,
    "abandonTimeout"=> 0,
    "agentNoResponseSeconds"=> 0,
    "agentOverrideOptionAnsweringMachine"=> true,
    "agentOverrideOptionBadNumber"=> true,
    "agentOverrideOptionFax"=> true,
    "agentResponseUtteranceMinimumSeconds"=> 0,
    "agentVoiceThreshold"=>0,
    "ansMachineDetMode"=> 0,
    "ansMachineMsg"=> "string",
    "customerLiveSilenceSeconds"=> 0,
    "customerVoiceThreshold"=> 0,
    "enableCPALogging"=> true,
    "exceptions"=> [array(
      
        "attempt_No"=> 0,
        "ansMachineDetMode"=> 0,
        "ansMachineMsg"=> "string"
      
    )],
    "machineEndSilenceSeconds"=> 0,
    "machineEndTimeoutSeconds"=> 0,
    "machineMinimumWithAgentSeconds"=> 0,
    "machineMinimumWithoutAgentSeconds"=> 0,
    "preConnectCPAEnabled"=>true,
    "preConnectCPARecording"=> true,
    "treatProgressAsRinging"=> true,
    "utteranceMinimumSeconds"=> 0
)]);
    //create post json data
    $data_json = json_encode($data);	
    $api_URL='/services/v13.0/skills/'.$skillId.'/parameters/cpa-management';
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);      
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
     	//echo $parsed_json;
		ConstructArray('Admin','Skills','PUT /skills/{skillId}/parameters/cpa-management','v13.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Skills','PUT /skills/{skillId}/parameters/cpa-management','v13.0','No access Token');
}
function getXsSettingsSettingsBySkillId($skillId,$fields='')
{
if($_SESSION["access_token"]!="")
  {  
    $data = array(	  
	  'fields'=>$fields,
	  );
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';   
    $api_URL='/services/v13.0/skills'.$skillId.'/parameters/xs-settings';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL.'?'.$params;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);      
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
    		//echo $parsed_json;
		ConstructArray('Admin','Skills','GET /skills/{skillId}/parameters/xs-settings','v13.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
	  }
	  
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Skills','GET /skills/{skillId}/parameters/xs-settings','v13.0','No access Token');
}
function updateSkillsXsSettingsBySkillId($skillId)
{
     if($_SESSION["access_token"]!="")
   {
    $data = array(
'xsSettings'=>[array(
"xsScriptID"=> 0,
    "xsCheckinScriptID"=> 0,
    "externalOutboundSkill_No"=> "string",
    "xsSkillChangedActive"=> true,
    "xsGetContactsActive"=> true,
    "xsFreshThreshold"=> 0,
    "xsAvailableThreshold"=> 0,
    "xsReadyThreshold"=> 0,
    "xsNumberToRetrieve"=>0
)]);
    //create post json data
    $data_json = json_encode($data);	
    $api_URL='/services/v13.0/skills/'.$skillId.'/parameters/xs-settings';
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);      
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
     	//echo $parsed_json;
		ConstructArray('Admin','Skills','PUT /skills/{skillId}/parameters/xs-settings','v13.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Skills','PUT /skills/{skillId}/parameters/xs-settings','v13.0','No access Token');
}
function getDeliveryPreferencesBySkillId($skillId,$fields='')
{
if($_SESSION["access_token"]!="")
  {  
    $data = array(	  
	  'fields'=>$fields,
	  );
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';   
    $api_URL='/services/v13.0/skills'.$skillId.'/parameters/delivery-preferences';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL.'?'.$params;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);      
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
    	//echo $parsed_json;
		ConstructArray('Admin','Skills','GET /skills/{skillId}/parameters/delivery-preferences','v13.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
	  }
	  
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Skills','GET /skills/{skillId}/parameters/delivery-preferences','v13.0','No access Token');
}

function updateSkillsDeliveryPreferencesBySkillId($skillId)
{
     if($_SESSION["access_token"]!="")
   {
    $data = array(
'deliveryPreferences'=>[array(
"confirmationRequiredDisabled"=> true,
    "confirmationRequiredDeliveryType"=> 0,
    "confirmationRequiredTimeout"=> 0,
    "confirmationRequiredTimeoutSubsequent"=> 0,
    "confirmationRequiredDefaultAccept"=> true,
    "confirmationRequiredDefault"=> true,
    "complianceRecordsDisabled"=> true,
    "complianceRecordsDeliveryType"=> 0,
    "complianceRecordsTimeout"=> 0,
    "complianceRecordsTimeoutSubsequent"=> 0,
    "complianceRecordsDefaultAccept"=> true,
    "showComplianceButtonReschedule"=> true,
    "showComplianceButtonRequeue"=> true,
    "showComplianceButtonSnooze"=> true,
    "showComplianceButtonDisposition"=> true,
    "showPreviewButtonReschedule"=> true,
    "showPreviewButtonRequeue"=> true,
    "showPreviewButtonSnooze"=> true,
    "showPreviewButtonDisposition"=> true
)]);
    //create post json data
    $data_json = json_encode($data);	
    $api_URL='/services/v13.0/skills/'.$skillId.'/parameters/delivery-preferences';
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);      
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
     	//echo $parsed_json;
		ConstructArray('Admin','Skills','PUT /skills/{skillId}/parameters/delivery-preferences','v13.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Skills','PUT /skills/{skillId}/parameters/delivery-preferences','v13.0','No access Token');
}

function getRetrySettingsBySkillId($skillId,$fields='')
{
if($_SESSION["access_token"]!="")
  {  
    $data = array(	  
	  'fields'=>$fields,
	  );
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';   
    $api_URL='/services/v13.0/skills'.$skillId.'/parameters/retry-settings';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL.'?'.$params;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);      
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
    		//echo $parsed_json;
		ConstructArray('Admin','Skills','GET /skills/{skillId}/parameters/retry-settings','v13.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
	  }
	  
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Skills','GET /skills/{skillId}/parameters/retry-settings','v13.0','No access Token');
}

function updateSkillsRetrySettingsBySkillId($skillId)
{
     if($_SESSION["access_token"]!="")
   {
    $data = array(
	'retrySettings'=>[array(
	"loadNonFresh"=> true,
    "finalizeWhenExhausted"=> true,
    "maximumAttempts"=> 0,
    "minimumRetryMinutes"=> 0,
    "maximumNumberOfHandledCalls"=> 0,
    "restrictedCallingMinutes"=> 0,
    "restrictedCallingMaxAttempts"=> 0,
    "generalStaleMinutes"=> 0,
    "callbackRestMinutes"=> 0,
    "releaseAgentSpecificCalls"=> true,
    "maximumNumberOfCallbacks"=> 0,
    "callbackStaleMinutes"=> 0
)]);
    //create post json data
    $data_json = json_encode($data);	
    $api_URL='/services/v13.0/skills/'.$skillId.'/parameters/retry-settings';
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);      
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
     	//echo $parsed_json;
		ConstructArray('Admin','Skills','PUT /skills/{skillId}/parameters/retry-settings','v13.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Skills','PUT /skills/{skillId}/parameters/retry-settings','v13.0','No access Token');
}

function getScheduleSettingsBySkillId($skillId)
{
if($_SESSION["access_token"]!="")
  {  
    $params .= $key.'='.$value.'&';   
    $api_URL='/services/v13.0/skills'.$skillId.'/parameters/schedule-settings';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL.'?'.$params;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);      
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
    		//echo $parsed_json;
		ConstructArray('Admin','Skills','GET /skills/{skillId}/parameters/schedule-settings','v13.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
	  }
	  
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Skills','GET /skills/{skillId}/parameters/schedule-settings','v13.0','No access Token');
}

function updateSkillsSchedulesettingsBySkillId($skillId)
{
     if($_SESSION["access_token"]!="")
   {
    $data = array(
'scheduleSettings'=>[array(
	"isScheduled"=> true,
    "sundayStartTime"=>"string",
    "sundayEndTime"=>"string",
    "sundayIsActive"=>true,
    "mondayStartTime"=> "string",
    "mondayEndTime"=> "string",
    "mondayIsActive"=> true,
    "tuesdayStartTime"=> "string",
    "tuesdayEndTime"=> "string",
    "tuesdayIsActive"=> true,
    "wednesdayStartTime"=> "string",
    "wednesdayEndTime"=> "string",
    "wednesdayIsActive"=> true,
    "thursdayStartTime"=>"string",
    "thursdayEndTime"=>"string",
    "thursdayIsActive"=> true,
    "fridayStartTime"=>"string",
    "fridayEndTime"=>"string",
    "fridayIsActive"=> true,
    "saturdayStartTime"=> "string",
    "saturdayEndTime"=> "string",
    "saturdayIsActive"=> true
)]);
    //create post json data
    $data_json = json_encode($data);	
    $api_URL='/services/v13.0/skills/'.$skillId.'/parameters/schedule-settings';
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);      
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
     	//echo $parsed_json;
		ConstructArray('Admin','Skills','PUT /skills/{skillId}/parameters/schedule-settings','v13.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Skills','PUT /skills/{skillId}/parameters/schedule-settings','v13.0','No access Token');
}


  //Check if you have obtained a token
if($_SESSION["access_token"] != "")
  {
function getAddressbook($params = "", $verb = 'GET', $format = 'json')
{
	//echo "<script type='text/javascript'>alert(resultt ### + 'function called');</script>";
	$api_URL="/services/v7.0/address-books";
	$url=$_SESSION["resource_server_base_uri"].$api_URL;
	$opts = array('http' =>
    array(
        'method'  => $verb,
        'header'  => 'Authorization: bearer '.$_SESSION["access_token"],'Accept: application/json',
		'Content-type: application/x-www-form-urlencoded; charset=utf-8'
    ),
);
 if (empty($params)) {
   $params = http_build_query($params);
   if ($verb == 'POST') {
      $cparams['http']['content'] = $params;
    }
	else {
     $url .= '?' . $params;
    }
  }

  $context = stream_context_create($opts);
  $fp = fopen($url, 'rb', false, $context);
  
  if (!$fp) {
    $res = false;
  } else {
     // If you're trying to troubleshoot problems, try uncommenting the
     // next two lines; it will show you the HTTP response headers across
     // all the redirects:
     // $meta = stream_get_meta_data($fp);
     // var_dump($meta['wrapper_data']);
	 // to get response code.
	 // var_dump(http_response_code());
	 // to get response code
     ConstructArray('Admin','AddressBook','GetAddressBook','v11.0','Success');
     $res = stream_get_contents($fp);
  }

  if ($res === false) {
    throw new Exception("$verb $url failed: $php_errormsg");
  }
 if(empty($format))
  {
  switch ($format) {
    case 'json':
	if (empty($res)) 
      $parsed_json = json_decode($res);
      else {
        throw new Exception("failed to decode $res as json");
      }
      return $parsed_json;

    case 'xml':
      $parsed_json = simplexml_load_string($res);
      if ($parsed_json === null) {
        throw new Exception("failed to decode $res as xml");
      }
      return $parsed_json;
  }
  }
  return $res;
  echo $res;
  }
}
// No token - get one or handle error
else
{  echo "No access Token";
   ConstructArray('Admin','AddressBook','GetAddressBook','v11.0','Failure');
}

function Demo()
{
if($_SESSION["access_token"]!="")
  {
    $api_URL="/services/v11.0/address-books";
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
 
    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
 
    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
 
    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      //echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      //echo "<br>";      
     // echo "Response Header:".$response_headers;
     // echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
        // echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  
 
        //Use the response data
        //print_r($parsed_json);
		ConstructArray('Admin','AddressBook','GetAddressBook','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
// No token - get one or handle error
function Demo2($addressBookName,$addressBookType)
{
if($_SESSION["access_token"]!="")
  {
$data = array('addressBookName'=>$addressBookName,'addressBookType'=>$addressBookType);
$data_json = json_encode($data);
    $api_URL="/services/v11.0/address-books";
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
 
    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
	
	//curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-Type: application/json'));
    curl_setopt($handle, CURLOPT_HEADER, true);
 
    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
 
    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	
	//curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-Type: application/json'));
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      //echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      //echo "<br>";      
     // echo "Response Header:".$response_headers;
     // echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
        // echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  
 
        //Use the response data
        //print_r($parsed_json);
		ConstructArray('Demo2','AddressBook','GetAddressBook','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
function getAgentGroupsByAgentId($agentId,$fields)
{
if($_SESSION["access_token"]!="")
  {
    $api_URL='/services/v9.0/agents/'.$agentId.'/groups';
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL;
  //echo $endpoint; 
    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
 
    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
 
    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
     // echo "Status Code of the Response:".$response;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
     // echo "<br>";      
     // echo "Response Header:".$response_headers;
     // echo "<br>";
	 // echo'before body is parsed';
	 // echo $response_body;
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
        // echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  
		//echo 'last step';
 
        //Use the response data
        //print_r($parsed_json);
		//ConstructArray('Admin','AddressBook','getAgentGroupsByAgentId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else 
	  
		 // echo 'else block';
		 ConstructArray('Admin','AddressBook','getAgentGroupsByAgentId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
	  
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
function DeleteSkillIDAgentID($skillId,$agentID)
{
	// session_start();
  //Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {
	  
	  $data = array(
	  
	  'skills'=>array([
	  'skillId'=>$skillId
	  ]
	  )
	  );
	  $data_json = json_encode($data);
	  //print_r($data_json);
	  //echo 'input data'.$data_json;
    //addressBookId is a required field
    $api_URL='/services/v7.0/agents/'.$agentId.'/skills';
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
 
    //Creating a HTTP DELETE request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true); 
    curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "DELETE");
	curl_setopt($handle, CURLOPT_POSTFIELDS, $data_json);
 
    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
 
    //By default, cURL is setup to not trust any CAs 
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security 
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=UTF-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
     // echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);   
      echo "<br>";      
      //echo "Response Header:".$response_headers;
      echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
       // echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  
 
        //Use the response data
       // print_r($parsed_json);
		ConstructArray('Admin','AddressBook','DeleteSkillIDAgentID','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
function getAgentSkillsByAgentId($agentID,$updatedSince,$fields)
{
	// session_start();
  //Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {
	  
	  $data = array(
	  'updatedSince'=>$updatedSince,
	  'fields'=>$fields	 
	  );
	  foreach($data as $key=>$value)
         $params .= $key.'='.$value.'&';
         
        $params = trim($params, '&');  
	  //echo 'input data'.$data_json;
    //addressBookId is a required field
    $api_URL='/services/v7.0/agents/'.$agentId.'/skills'.'?'.$params;
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
	//echo 'Requested URL:'.$endpoint;
 
    //Creating a HTTP DELETE request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);     
	curl_setopt($handle, CURLOPT_POSTFIELDS, $data);
 
    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
 
    //By default, cURL is setup to not trust any CAs 
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security 
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=UTF-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
     // echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);   
      echo "<br>";      
      //echo "Response Header:".$response_headers;
      echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
       // echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  
 
        //Use the response data
       // print_r($parsed_json);
		ConstructArray('Admin','Agent','getAgentSkillsByAgentId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
function AssignSkillsToAgent($agentID,$skillId,$isActive,$proficiency)
{
if($_SESSION["access_token"]!="")
  {
$data = array(
'skills'=>[array('skillId'=>$skillId,'isActive'=>$isActive,'proficiency'=>$proficiency
)]);
$data_json = json_encode($data);
    $api_URL='/services/v7.0/agents/'.$agentID.'/skills';
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
 
    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
	
	//curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-Type: application/json'));
    curl_setopt($handle, CURLOPT_HEADER, true);
 
    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
 
    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	
	//curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-Type: application/json'));
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      //echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      //echo "<br>";      
     // echo "Response Header:".$response_headers;
     // echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
        // echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  
 
        //Use the response data
        //print_r($parsed_json);
		ConstructArray('Admin','Agent','AssignSkillsToAgent','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
function UpdateAgentSkill($agentID,$skillId,$isActive,$proficiency)
{
if($_SESSION["access_token"]!="")
  {
$data = array(
'skills'=>[array('skillId'=>$skillId,'isActive'=>$isActive,'proficiency'=>$proficiency
)]);
$data_json = json_encode($data);
    $api_URL='/services/v7.0/agents/'.$agentID.'/skills';
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
 
    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
	
	//curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-Type: application/json'));
    curl_setopt($handle, CURLOPT_HEADER, true);
 
    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
 
    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	
	//curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-Type: application/json'));
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      //echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      //echo "<br>";      
     // echo "Response Header:".$response_headers;
     // echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
        // echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  
 
        //Use the response data
        //print_r($parsed_json);
		ConstructArray('Admin','Agent','UpdateAgentSkill','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
function GetAgentUnassignedSkills($agentID,$mediaTypeId,$outboundStrategy,$searchString,$fields,$skip,$top,$orderby,$isSkillActive)
{
	// session_start();
  //Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {
	  
	  $data = array(
	  'mediaTypeId'=>$mediaTypeId,
	  'outboundStrategy'=>$outboundStrategy,
	  'searchString'=>$searchString,
	  'fields'=>$fields,
	  'skip'=>$skip,
	  'top'=>$top,
	  'orderby'=>$orderby,
	  'isSkillActive'=>$isSkillActive	 
	  );
	  foreach($data as $key=>$value)
         $params .= $key.'='.$value.'&';
         
        $params = trim($params, '&');  
	  //echo 'input data'.$data_json;
    //addressBookId is a required field
    $api_URL='/services/v7.0/agents/'.$agentID.'/skills/unassigned'.'?'.$params;
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
	//echo 'Requested URL:'.$endpoint;
 
    //Creating a HTTP DELETE request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);     
	//curl_setopt($handle, CURLOPT_POSTFIELDS, $data);
 
    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
 
    //By default, cURL is setup to not trust any CAs 
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security 
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=UTF-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
     // echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);   
      echo "<br>";      
      //echo "Response Header:".$response_headers;
      echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON 

        //Parsing the json response
       // echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  
 
        //Use the response data
       // print_r($parsed_json);
		ConstructArray('Admin','Agent','GetAgentUnassignedSkills','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
function PostAgentMessages($message='',$startDate='018-03-08T11:06:15.606Z',$subject='',$targetId=0,$targetType='Agent',$validUntil='',$expireMinutes=5)
{
if($_SESSION["access_token"]!="")
  {
$data = array(
'agentMessages'=>[array('message'=>$message,'startDate'=>$startDate,'subject'=>$subject,
'targetId'=>$targetId,'targetType'=>$targetType,'validUntil'=>$validUntil,'expireMinutes'=>$expireMinutes
)]);
$data_json = json_encode($data);
//echo'input data'.$data_json;
    $api_URL='/services/v11.0/agents/messages';
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
	//echo'Requested url:'.$endpoint;
 
    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
	
	//curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-Type: application/json'));
    curl_setopt($handle, CURLOPT_HEADER, true);
 
    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
 
    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	
	//curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-Type: application/json'));
	curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      //echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      //echo "<br>";      
     // echo "Response Header:".$response_headers;
     // echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
        // echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  
         //echo 'Response body'.$response_body;
        //Use the response data
        //print_r($parsed_json);
		ConstructArray('Admin','Agent','PostAgentMessages','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
function postAgentState($agentID,$state='',$outStateId='')
{
if($_SESSION["access_token"]!="")
  {
$data = array(
'state'=>$state,'outStateId'=>$outStateId
);
$data_json = json_encode($data);
//echo'input data'.$data_json;
    $api_URL='/services/v7.0/agents/'.$agentID.'/state';
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
	//echo'Requested url:'.$endpoint;
 
    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
	
	//curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-Type: application/json'));
    curl_setopt($handle, CURLOPT_HEADER, true);
 
    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
 
    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	
	//curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-Type: application/json'));
	curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
    //echo'Response postAgentState :'.$response;
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      //echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      //echo "<br>";      
     // echo "Response Header:".$response_headers;
     // echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
        // echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  
         //echo 'Response body'.$response_body;
        //Use the response data
        //print_r($parsed_json);
		ConstructArray('Admin','Agent','postAgentState','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
function Authentication($username='nikhil.m@sc1.com',$password='Qwerty@123',$grant_type='password')
{
if($_SESSION["access_token"]!="")
  {
$data = array(
'username'=>$username,
'password'=>$password,
'grant_type'=>$grant_type
);
$data_json = json_encode($data);
 //echo'input data Authentication'.$data_json;
    $api_URL='/InContactAuthorizationServer/Token';
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint='https://api-sc1.ucnlabext.com'.$api_URL;
	//echo'Requested url:'.$endpoint;
 
    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
	
	//curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-Type: application/json'));
    curl_setopt($handle, CURLOPT_HEADER, true);
 
    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
 
    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-Type: application/json';
    $headers[] = 'Authorization: basic'.'aW50ZXJuYWxAaW5Db250YWN0IEluYy46UVVFNVFrTkdSRE0zUWpFME5FUkRSamczUlVORFJVTkRRakU0TlRrek5UYz0=';
    $headers[] = 'Accept: application/json,text/javascript, */*; q=0.01';
	
	//curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-Type: application/json'));
	curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
    //echo'Response postAgentState :'.$response;
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      //echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      //echo "<br>";      
     // echo "Response Header:".$response_headers;
     // echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
        // echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  
         //echo 'Response body'.$response_body;
        //Use the response data
        //print_r($parsed_json);
		ConstructArray('Admin','Agent','Authentication','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
function deleteAgentMessagesByMessageId($messageId)
{
	// session_start();
  //Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {
	  
	  $data = array();
	  $data_json = json_encode($data);
	  //print_r($data_json);
	  //echo 'input data'.$data_json;
    //addressBookId is a required field
    $api_URL='/services/v11.0/agents/messages/'.$messageId;
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
 
    //Creating a HTTP DELETE request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true); 
    curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "DELETE");
	curl_setopt($handle, CURLOPT_POSTFIELDS, $data_json);
 
    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
 
    //By default, cURL is setup to not trust any CAs 
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security 
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=UTF-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
     // echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);   
      echo "<br>";      
      //echo "Response Header:".$response_headers;
      echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
       // echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  
 
        //Use the response data
       // print_r($parsed_json);
		ConstructArray('Admin','Agent','deleteAgentMessagesByMessageId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
if($_SESSION["access_token"]!="")
  {     
    $api_URL='/services/v7.0/agent-patterns';
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL;
  //echo $endpoint; 
    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
 
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
function getAgentPatterns()
{
    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
 
    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
     // echo "Status Code of the Response:".$response;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
     // echo "<br>";      
     // echo "Response Header:".$response_headers;
     // echo "<br>";
	 // echo'before body is parsed';
	 // echo $response_body;
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
        // echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  
		//echo 'last step';
 
        //Use the response data
        //print_r($parsed_json);
		//ConstructArray('Admin','AddressBook','getAgentGroupsByAgentId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else 
	  
		 // echo 'else block';
		 ConstructArray('Admin','Agent','getAgentPatterns','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
	  
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
function getAgentStates()
{
if($_SESSION["access_token"]!="")
  {
    $api_URL='/services/v6.0/agents-states';
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL;
  //echo $endpoint; 
    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
 
    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
 
    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
     // echo "Status Code of the Response:".$response;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
     // echo "<br>";      
     // echo "Response Header:".$response_headers;
     // echo "<br>";
	 // echo'before body is parsed';
	 // echo $response_body;
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
         echo 'Response Body:getAgentGroupsByAgentId'.$response_body;  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  
		//echo 'last step';
 
        //Use the response data
        //print_r($parsed_json);
		//ConstructArray('Admin','AddressBook','getAgentGroupsByAgentId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else 
	  
		 // echo 'else block';
		 ConstructArray('Admin','Agent','getAgentStates','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
	  
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
function CreateTeam( )
{
if($_SESSION["access_token"]!="")
  {
$data = array(
'teams'=> [array(
   'teamName'=> 'team',
      'isActive'=> true,
      'maxConcurrentChats'=> 8,
      'wfoEnabled'=> false,
      'wfm2Enabled'=> false,
      'qm2Enabled'=> false,
      'inViewEnabled'=> false,
      'notes'=> '',
      'maxEmailAutoParkingLimit'=> 25,
      'inViewGamificationEnabled'=> false,
      'inViewChatEnabled'=> false,
      'inViewLMSEnabled'=> false,
      'analyticsEnabled'=> false,
      'requestContact'=> false,
      'chatThreshold'=> 1,
      'emailThreshold'=> 1,
      'workItemThreshold'=> 1,
      'voiceThreshold'=> 1,
      'contactAutoFocus'=> false,
      'teamUuid'=> '' 
      
)]);
    
	$data_json = json_encode($data);
	//echo 'Input for createteam :'.$data_json;
    $api_URL='/services/v11.0/teams';
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
 
    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
	
	//curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-Type: application/json'));
    curl_setopt($handle, CURLOPT_HEADER, true);
 
    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
 
    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	
	//curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-Type: application/json'));
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      //echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      //echo "<br>";      
     // echo "Response Header:".$response_headers;
     // echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
        //echo "Response Body: CreateTeam".$response_body;  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  
 
        //Use the response data
        //print_r($parsed_json);
		ConstructArray('Admin','Agent','CreateTeam','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
function UpdateTeam($teamID)
{
if($_SESSION["access_token"]!="")
  {
$data = array(
  'team'=> array(
    'teamName'=> 'teamnameUpdated',
    'isActive'=> true,
    'maxConcurrentChats'=> 8,
    'wfoEnabled'=> false,
    'wfm2Enabled'=> false,
    'qm2Enabled'=> false,
    'inViewEnabled'=> false,
    'notes'=> '',
    'maxEmailAutoParkingLimit'=> 25,
    'inViewGamificationEnabled'=> false,
    'inViewChatEnabled'=> false,
    'inViewLMSEnabled'=> false,
    'analyticsEnabled'=> false,
    'requestContact'=> false,
    'chatThreshold'=> 1,
    'emailThreshold'=> 1,
    'workItemThreshold'=> 1,
    'voiceThreshold'=> 1,
    'contactAutoFocus'=> false
  )
);
$data_json = json_encode($data);
//echo 'input data for Updateteam:'.$data_json;
    $api_URL='/services/v10.0/teams/'.$teamID;
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    //echo 'Input data for UpdateTeam:'.$endpoint.
    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
	
	//curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-Type: application/json'));
    curl_setopt($handle, CURLOPT_HEADER, true);
 
    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
 
    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	
	//curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-Type: application/json'));
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      //echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      //echo "<br>";      
     // echo "Response Header:".$response_headers;
     // echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
        // echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  
 
        //Use the response data
        //print_r($parsed_json);
		ConstructArray('Admin','Agent','UpdateTeam','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
function getTeamsAgents($fields='',$updatedSince='')
{
if($_SESSION["access_token"]!="")
  {	  
	  $data = array(	  
	  'fields'=>$fields,
	  'updatedSince'=>$updatedSince
	  );
	  foreach($data as $key=>$value)
         $params .= $key.'='.$value.'&';
         
        $params = trim($params, '&'); 
    $api_URL='/services/v10.0/teams/agents';
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL.'?'.$params;
  //echo $endpoint; 
    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
 
    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
 
    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
     // echo "Status Code of the Response:".$response;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
     // echo "<br>";      
     // echo "Response Header:".$response_headers;
     // echo "<br>";
	 // echo'before body is parsed';
	 // echo $response_body;
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
         //echo 'Response Body:getAgentGroupsByAgentId'.$response_body;  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  
		//echo 'last step';
 
        //Use the response data
        //print_r($parsed_json);
	ConstructArray('Admin','AddressBook','getAgentGroupsByAgentId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else 
	  
		 // echo 'else block';
		 ConstructArray('Admin','Agent','getTeamsAgentbyTeamId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
	  
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
function deleteTeamsAgentbyTeamId($teamId,$transferTeamId,$agentId='')
{
	// session_start();
  //Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {
	  
    $data = array(
      'transferTeamId'=>$transferTeamId,
      'agents'=>[array(
      'agentId'=>$agentId
      )]
      );
      $data_json = json_encode($data);
	  //print_r($data_json);
	  //echo 'input data deleteTeamsAgentbyTeamId'.$data_json;
    //addressBookId is a required field
    $api_URL='/services/v7.0/teams/'.$teamId.'/agents';
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
 
    //Creating a HTTP DELETE request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true); 
    curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "DELETE");
	  curl_setopt($handle, CURLOPT_POSTFIELDS, $data_json);
 
    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
 
    //By default, cURL is setup to not trust any CAs 
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security 
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=UTF-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    $headers[] = 'Access-Control-Allow-Origin:http://eng-ngpcl07';

    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
    echo "Response Body:deleteTeamsAgentbyTeamId".$response; 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
     // echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);   
      echo "<br>";      
      //echo "Response Header:".$response_headers;
      echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
          
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  
 
        //Use the response data
       // print_r($parsed_json);
		ConstructArray('Admin','AddressBook','DeleteSkillIDAgentID','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
function getTeamsAgentbyTeamId($teamId,$fields='')
{
if($_SESSION["access_token"]!="")
  {	  
	  $data = array(	  
	  'fields'=>$fields	  
	  );
	  foreach($data as $key=>$value)
         $params .= $key.'='.$value.'&';
         
        $params = trim($params, '&'); 
    $api_URL='/services/v10.0/teams/'.$teamId.'/agents';
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL.'?'.$params;
  //echo $endpoint; 
    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
 
    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
 
    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
     // echo "Status Code of the Response:".$response;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
     // echo "<br>";      
     // echo "Response Header:".$response_headers;
     // echo "<br>";
	 // echo'before body is parsed';
	 // echo $response_body;
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
         //echo 'Response Body:getAgentGroupsByAgentId'.$response_body;  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  
		//echo 'last step';
 
        //Use the response data
        //print_r($parsed_json);
	ConstructArray('Admin','AddressBook','getTeamsAgentbyTeamId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else 
	  
		 // echo 'else block';
		 ConstructArray('Admin','Agent','getTeamsAgentbyTeamId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
	  
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
function createTeamsAgentbyTeamId($teamId,$agentID)
{
if($_SESSION["access_token"]!="")
  {
$data = array(
'agents'=>[array('agentId'=>$agentID)]);
//create query string 
  $data_json = json_encode($data);
  $api_URL='/services/v11.0/teams/'.$teamId.'/agents';
  $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
  $handle = curl_init($endpoint);	
	//curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-Type: application/json'));
    curl_setopt($handle, CURLOPT_HEADER, true);
	curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
	curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
	$headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
	  $parsed_json=json_decode($response_body);  
      ConstructArray('Admin','Agent','createTeamsAgentbyTeamId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
	ConstructArray('Admin','Agent','createTeamsAgentbyTeamId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
function deleteTeamsUnavailablebyTeamId($teamId,$outstateId)
{
	// session_start();
  //Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {
	  
    $data = array(      
      'codes'=>[array(
      'outstateId'=>$outstateId)]);
	  //create post data
      $data_json = json_encode($data);
	  $api_URL='/services/v7.0/teams/'.$teamId.'/unavailable-codes';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true); 
    curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "DELETE");
	curl_setopt($handle, CURLOPT_POSTFIELDS, $data_json);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=UTF-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    $headers[] = 'Access-Control-Allow-Origin:http://eng-ngpcl07';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers); 
    // Make the request         
    $response = curl_exec($handle);
	//Handling valid response
    if($response!=FALSE)
    {		
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);   
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
        ConstructArray('Admin','Agent','deleteTeamsUnavailablebyTeamId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  ConstructArray('Admin','Agent','deleteTeamsUnavailablebyTeamId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
function createTeamsUnavailablebyTeamId($teamId,$outstateId)
{
if($_SESSION["access_token"]!="")
  {
$data = array(      
      'codes'=>[array(
      'outstateId'=>$outstateId)]);
	  //create post data
     $data_json = json_encode($data);
    $api_URL='/services/v7.0/teams/'.$teamId.'/unavailable-codes';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($ch, CURLOPT_POST,1);
	curl_setopt($ch, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);   
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
        ConstructArray('Admin','Agent','createTeamsUnavailablebyTeamId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	 
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
function GetCountries()
{
if($_SESSION["access_token"]!="")
  {
	  $data = array();
	 
    $api_URL='/services/v7.0/countries';
    //Creating the endpoint for the request   
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;  
	//echo $endpoint;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);    
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers); 
    // Make the request         
    $response = curl_exec($handle); 
    echo $response_body;
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);    
      $parts = explode("\r\n\r\nHTTP/", $response);     
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);      
      if (!empty($response_body))
      {
        //The Response from the api is in JSON        
        $parsed_json=json_decode($response_body); 
        ConstructArray('Admin','General','GetCountries','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 		
	   }
	  else
		 ConstructArray('Admin','General','GetCountries','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
		 //echo $parsed_json;
	  
    } 
     
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function GetCountriesBycountryId($countryId)
{
if($_SESSION["access_token"]!="")
  {
	  $data = array();
	 
    $api_URL='/services/v7.0/countries/'.$countryId.'/states';
    //Creating the endpoint for the request   
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;  
	//echo $endpoint;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);    
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers); 
    // Make the request         
    $response = curl_exec($handle); 
    echo $response_body;
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);    
      $parts = explode("\r\n\r\nHTTP/", $response);     
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);      
      if (!empty($response_body))
      {
        //The Response from the api is in JSON        
        $parsed_json=json_decode($response_body); 
        ConstructArray('Admin','General','GetCountriesBycountryId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 		
	   }
	  else
		 ConstructArray('Admin','General','GetCountriesBycountryId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
		 //echo $parsed_json;
	  
    } 
     
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function getDataDefinitionDataTypes()
{
if($_SESSION["access_token"]!="")
  {
	  $data = array();
	 
    $api_URL='/services/v7.0/data-definitions/data-types';
    //Creating the endpoint for the request   
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;  
	echo $endpoint;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);    
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers); 
    // Make the request         
    $response = curl_exec($handle); 
    echo $response_body;
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);    
      $parts = explode("\r\n\r\nHTTP/", $response);     
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);      
      if (!empty($response_body))
      {
        //The Response from the api is in JSON        
        $parsed_json=json_decode($response_body); 
        ConstructArray('Admin','General','getDataDefinitionDataTypes','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 		
	   }
	  else
		 ConstructArray('Admin','General','getDataDefinitionDataTypes','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
		 //echo $parsed_json;
	  
    } 
     
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function getDispositions($skip= 0,$top= 100,$searchString= "",$fields="",$orderby="",$isPreviewDispositions='true',
$updatedSince="")
{
if($_SESSION["access_token"]!="")
  {
	  $data = array(
	  'skip'=>$skip,
	  'top'=>$top,
	  'searchString'=>$searchString,
	  'fields'=>$fields,
	  'orderby'=>$orderby,
	  'isPreviewDispositions'=>$isPreviewDispositions);
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';
         //Build the query string 
	 
    $api_URL='/services/v10.0/dispositions';
    //Creating the endpoint for the request   
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL.'?'.$params; 
	
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);    
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers); 
    // Make the request         
    $response = curl_exec($handle);     
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);    
      $parts = explode("\r\n\r\nHTTP/", $response);     
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);      
      if (!empty($response_body))
      {
        //The Response from the api is in JSON        
        $parsed_json=json_decode($response_body); 
        ConstructArray('Admin','General','getDispositions','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 		
	   }
	  else
	  {
		 ConstructArray('Admin','General','getDispositions','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
		 //echo $parsed_json;
	  }
    } 
     
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function deleteFileByName($fileName='')
{
  //Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {	  
	  $data = array(
	  'fileName'=>$fileName
	  );
	  //create post json data
	  $data_json = json_encode($data);
	  $api_URL='/services/v8.0/files';
    //Creating the endpoint for the request
     $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    //Creating a HTTP DELETE request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true); 
    curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "DELETE");
	curl_setopt($handle, CURLOPT_POSTFIELDS, $data_json);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=UTF-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers); 
    // Make the request         
    $response = curl_exec($handle);     
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);   
      if (!empty($response_body))
      {
       $parsed_json=json_decode($response_body);  
       ConstructArray('Admin','General','deleteFileByName','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else
	  {
		  ConstructArray('Admin','General','deleteFileByName','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
	  }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}

function createFile($fileName='',$file='',$overwrite='false')
{
if($_SESSION["access_token"]!="")
  {
$data = array(      
      'fileName'=>$fileName,
	  'file'=>$file,
	  'overwrite'=>$overwrite,
	  );
	  //create post data
     $data_json = json_encode($data);
    $api_URL='/services/v8.0/files';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);    
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
        ConstructArray('Admin','General','createFile','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else
	  {
		  ConstructArray('Admin','General','createFile','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
	  }
	 
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function updateFile($oldPath='',$newPath='',$overwrite='false')
{
if($_SESSION["access_token"]!="")
  {
$data = array(  
    'oldPath'=> '$oldPath',
    'newPath'=> $newPath,
    'overwrite'=> $overwrite
);
//create post json data
    $data_json = json_encode($data);
    $api_URL='/services/v8.0/files';
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);       
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
     	ConstructArray('Admin','General','updateFile','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else
	  {
		  ConstructArray('Admin','General','updateFile','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
	  }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
function createFilesExternal($fileName,$file='',$overwrite=false,$needsProcessing=false)
{
if($_SESSION["access_token"]!="")
  {
$data = array(
'fileName'=>$fileName,'file'=>$file,
'overwrite'=>$overwrite,
'needsProcessing'=>$needsProcessing
);
//create post json data
$data_json = json_encode($data);
$api_URL='/services/v9.0/files/external';
//Creating the endpoint for the request  
$endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
 $handle = curl_init($endpoint);
 curl_setopt($handle, CURLOPT_HEADER, true);
 curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
 curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
 curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);   
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
	  $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		  $parsed_json=json_decode($response_body);  
         ConstructArray('Admin','Agent','createFilesExternal','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else
	  {
		  ConstructArray('Admin','Agent','createFilesExternal','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
	  }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
function deleteFolderByName($folderName='')
{
  //Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {	  
	  $data = array(
	  'folderName'=>$folderName
	  );
	  //create post json data
	  $data_json = json_encode($data);
	  $api_URL='/services/v8.0/folders';
    //Creating the endpoint for the request
     $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    //Creating a HTTP DELETE request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true); 
    curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "DELETE");
	curl_setopt($handle, CURLOPT_POSTFIELDS, $data_json);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=UTF-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
     echo $response;
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);   
      if (!empty($response_body))
      {
       $parsed_json=json_decode($response_body);  
       ConstructArray('Admin','Agent','deleteFolderByName','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
function getFolders($folderName='')
{
if($_SESSION["access_token"]!="")
  {
	  $data = array(	  
	  'folderName'=>$folderName);
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';
         //Build the query string 
	 
    $api_URL='/services/v10.0/dispositions';
    //Creating the endpoint for the request   
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL.'?'.$params; 
	echo $endpoint;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);    
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers); 
    // Make the request         
    $response = curl_exec($handle);     
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);    
      $parts = explode("\r\n\r\nHTTP/", $response);     
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);      
      if (!empty($response_body))
      {
        //The Response from the api is in JSON        
        $parsed_json=json_decode($response_body); 
        ConstructArray('Admin','General','getFolders','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 		
	   }
	  else
		 ConstructArray('Admin','General','getFolders','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
		 //echo $parsed_json;
	  
    } 
     
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function GetHiringSources()
{
if($_SESSION["access_token"]!="")
  {
	  
    $api_URL='/services/v7.0/hiring-sources';
    //Creating the endpoint for the request   
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
	echo $endpoint;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);    
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers); 
    // Make the request         
    $response = curl_exec($handle);     
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);    
      $parts = explode("\r\n\r\nHTTP/", $response);     
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);      
      if (!empty($response_body))
      {
        //The Response from the api is in JSON        
        $parsed_json=json_decode($response_body); 
        ConstructArray('Admin','General','GethiringSources','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 		
	   }
	  else
		 ConstructArray('Admin','General','GethiringSources','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
		 //echo $parsed_json;
	  
    } 
     
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function postHiringSources($sourceName='')
{
if($_SESSION["access_token"]!="")
  {
$data = array(      
      'sourceName'=>$sourceName	  
	  );
	  //create post data
     $data_json = json_encode($data);
    $api_URL='/services/v7.0/hiring-sources';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);     
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
        ConstructArray('Admin','General','postHiringSources','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	 
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function GetLocations($includeAgents='false')
{
if($_SESSION["access_token"]!="")
  {
	$data = array(	  
	  'includeAgents'=>$includeAgents);
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';
    $api_URL='/services/v7.0/locations';
    //Creating the endpoint for the request   
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL.'?'.$params;	
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);    
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers); 
    // Make the request         
    $response = curl_exec($handle);     
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);    
      $parts = explode("\r\n\r\nHTTP/", $response);     
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);      
      if (!empty($response_body))
      {
        //The Response from the api is in JSON        
        $parsed_json=json_decode($response_body); 
        ConstructArray('Admin','General','GethiringSources','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 		
	   }
	  else
		 ConstructArray('Admin','General','GethiringSources','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
		 //echo $parsed_json;
	  
    } 
     
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function PostMessageTemplate($templateName='',$templateTypeId='-1',$smsContent='')
{
if($_SESSION["access_token"]!="")
  {
$data = array(      
      'templateName'=>$templateName,
	  'templateTypeId'=>$templateTypeId,
	  'smsContent'=>$smsContent,
	  );
	  //create post data
     $data_json = json_encode($data);
    $api_URL='/services/v7.0/message-templates';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);
    echo $response;
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
        ConstructArray('Admin','General','PostMessageTemplate','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else
	  {
		ConstructArray('Admin','General','PostMessageTemplate','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );  
	  }
	 
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function getMessageTemplatesbyTemplateId($templateId='')
{
if($_SESSION["access_token"]!="")
  {
	$data = array(	  
	  'templateId'=>$templateId);
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';
    $api_URL='/services/v8.0/message-templates/'.$templateId;
    //Creating the endpoint for the request   
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL.'?'.$params;	
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);    
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers); 
    // Make the request         
    $response = curl_exec($handle);     
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);    
      $parts = explode("\r\n\r\nHTTP/", $response);     
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);      
      if (!empty($response_body))
      {
        //The Response from the api is in JSON        
        $parsed_json=json_decode($response_body); 
        ConstructArray('Admin','General','getMessageTemplatesbyTemplateId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 		
	   }
	  else
		 ConstructArray('Admin','General','getMessageTemplatesbyTemplateId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
		 //echo $parsed_json;
	  
    } 
     
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function UpdateMessageTemplateByTemplateId($templateId,$templateName='',$body='',$isHtml='true',
$ccAddress='',$bccAddress='',$replyToAddress='',$fromName='',$subject='',$isActive='true')
{
if($_SESSION["access_token"]!="")
  {
$data = array(  
  'templateName'=>$templateName,
  'body'=>$body,
  'isHtml'=> $isHtml,
  'ccAddress'=>$ccAddress,
  'bccAddress'=>$bccAddress,
  'replyToAddress'=>$replyToAddress,
  'fromName'=>$fromName,
  'fromAddress'=>$fromAddress,
  'subject'=>$subject,
  'isActive'=> $isActive
);
//create post json data
    $data_json = json_encode($data);
    $api_URL='/services/v8.0/message-templates/'.$templateId;
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);       
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
     	ConstructArray('Admin','General','UpdateMessageTemplateByTemplateId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function GetPhoneCodes()
{
if($_SESSION["access_token"]!="")
  {
	
    $api_URL='/services/v7.0/phone-codes';
    //Creating the endpoint for the request   
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);    
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers); 
    // Make the request         
    $response = curl_exec($handle);       
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);    
      $parts = explode("\r\n\r\nHTTP/", $response);     
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);      
      if (!empty($response_body))
      {
        //The Response from the api is in JSON        
        $parsed_json=json_decode($response_body); 
        ConstructArray('Admin','General','GetPhoneCodes','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 		
	   }
	  else
		 ConstructArray('Admin','General','GetPhoneCodes','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
		 //echo $parsed_json;
	  
    } 
     
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function GetSecurityProfilesById($profileId=0)
{
if($_SESSION["access_token"]!="")
  {
	$data = array(	  
	  'profileId'=>$profileId);
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';
    $api_URL='/services/v7.0/security-profiles/'.$profileId;
    //Creating the endpoint for the request   
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL.'?'.$params;	
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);    
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers); 
    // Make the request         
    $response = curl_exec($handle);     
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);    
      $parts = explode("\r\n\r\nHTTP/", $response);     
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);      
      if (!empty($response_body))
      {
        //The Response from the api is in JSON        
        $parsed_json=json_decode($response_body); 
        ConstructArray('Admin','General','GetSecurityProfilesById','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 		
	   }
	  else
		 ConstructArray('Admin','General','GetSecurityProfilesById','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
		 //echo $parsed_json;
	  
    } 
     
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
function GetTags($searchString=' ',$isActive=' ')
{
if($_SESSION["access_token"]!="")
  {
	$data = array(	  
	  'searchString'=>$searchString,
	  'isActive'=>$searchString,
	  );
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';
    $api_URL='/services/v7.0/tags';
    //Creating the endpoint for the request   
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL.'?'.$params;	
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);    
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers); 
    // Make the request         
    $response = curl_exec($handle);     
    //Handling valid response
    if($response!=FALSE)
    {
     // $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);    
      $parts = explode("\r\n\r\nHTTP/", $response);     
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);      
      if (!empty($response_body))
      {
        //The Response from the api is in JSON        
        $parsed_json=json_decode($response_body); 
        ConstructArray('Admin','General','GetTags','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 		
	   }
	  else
		 ConstructArray('Admin','General','GetTags','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
		 //echo $parsed_json;
    } 
     
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function PostTags($tagName='',$notes='')
{
if($_SESSION["access_token"]!="")
  {
$data = array(      
      'tagName'=>$tagName,
	  'notes'=>$notes	  
	  );
	  //create post data
     $data_json = json_encode($data);
    $api_URL='/services/v7.0/tags';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);
    echo $response;
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
        ConstructArray('Admin','General','PostTags','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else
		 ConstructArray('Admin','General','PostTags','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
	 
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function getTagsByTagId($tagId)
{
if($_SESSION["access_token"]!="")
  {
	$data = array(	  
	  'profileId'=>$profileId);
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';
    $api_URL='/services/v7.0/security-profiles/'.$profileId;
    //Creating the endpoint for the request   
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL.'?'.$params;	
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);    
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers); 
    // Make the request         
    $response = curl_exec($handle);     
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);    
      $parts = explode("\r\n\r\nHTTP/", $response);     
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);      
      if (!empty($response_body))
      {
        //The Response from the api is in JSON        
        $parsed_json=json_decode($response_body); 
        ConstructArray('Admin','General','getTagsByTagId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 		
	   }
	  else
		 ConstructArray('Admin','General','getTagsByTagId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
		 //echo $parsed_json; 
    } 
     
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

} 
function UpdateTagsByTagId($tagId,$tagName='',$notes='',$isActive='')
{
if($_SESSION["access_token"]!="")
  {
$data = array(  
  'tagName'=>$tagName,
  'notes'=>$notes,
  'isActive'=> $isActive
);
    //create post json data
    $data_json = json_encode($data);
    $api_URL='/services/v7.0/tags/'.$tagId;
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);       
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
     	ConstructArray('Admin','General','UpdateTagsByTagId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else
		  ConstructArray('Admin','General','UpdateTagsByTagId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
function createDispositions($dispositionName='',$isPreviewDisposition=false,$classificationId=1)
{
if($_SESSION["access_token"]!="")
  {
$data = array(
'dispositions'=>[array('dispositionName'=>$dispositionName,
'isPreviewDisposition'=>$isPreviewDisposition,
'classificationId'=>$classificationId
)]);

//create post json data
$data_json = json_encode($data);
$api_URL='/services/v10.0/dispositions';
//Creating the endpoint for the request  
$endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
 $handle = curl_init($endpoint);
 curl_setopt($handle, CURLOPT_HEADER, true);
 curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
 curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
 curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);	
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
	  $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		  $parsed_json=json_decode($response_body);  
         ConstructArray('Admin','Skills','createDispositions','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }	 
	  else
		  ConstructArray('Admin','Skills','createDispositions','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
function getDispositionsByDispositionID($dispositionId,$fields='')
{
if($_SESSION["access_token"]!="")
  {  
    $data = array(	  
	  'fields'=>$fields);
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';   
    $api_URL='/services/v10.0/dispositions/'.$dispositionId;
    //Creating the endpoint for the request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL.'?'.$params;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);      
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
		ConstructArray('Admin','Skills','getDispositionsByDispositionID','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
	  }
	  else
		 ConstructArray('Admin','Skills','getDispositionsByDispositionID','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );  
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function updateDispositionsByDispositionID($dispositionId,$dispositionName='',$isPreviewDisposition=false,$classificationId='1',$isActive=true)
{
if($_SESSION["access_token"]!="")
  {
$data = array(  
    'dispositionName'=> $dispositionName,
    'isPreviewDisposition'=>$isPreviewDisposition,
    'classificationId'=> $classificationId,
    'isActive'=> $isActive    
);
//create post json data
    $data_json = json_encode($data);	
    $api_URL='/services/v10.0/dispositions/'.$dispositionId;
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);      
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
     	ConstructArray('Admin','Skills','updateDispositionsByDispositionID','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else
		 ConstructArray('Admin','Skills','updateDispositionsByDispositionID','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}


function getDispositionsByClassifications($fields='')
{
if($_SESSION["access_token"]!="")
  {
	$data = array(	  
	  'fields'=>$fields);
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';
    $api_URL='/services/v10.0/dispositions/classifications';
    //Creating the endpoint for the request   
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL.'?'.$params;	
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);    
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers); 
    // Make the request         
    $response = curl_exec($handle);
    echo $response;
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);    
      $parts = explode("\r\n\r\nHTTP/", $response);     
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);      
      if (!empty($response_body))
      {
        //The Response from the api is in JSON        
        $parsed_json=json_decode($response_body); 
        ConstructArray('Admin','Skills','getDispositionsByClassifications','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 		
	   }
	  else
		 ConstructArray('Admin','skills','getDispositionsByClassifications','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
		 //echo $parsed_json;
	  
    } 
     
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function DeleteSkillIDForAgentID($skillId,$agentId=-1)
{
  //Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {	  
	  $data = array(
	  'fileName'=>$fileName
	  );
	  //create post json data
	  $data_json = json_encode($data);
	  $api_URL='/services/v1.0/skills/'.$skillId.'/agents';
    //Creating the endpoint for the request
     $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    //Creating a HTTP DELETE request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true); 
    curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "DELETE");
	curl_setopt($handle, CURLOPT_POSTFIELDS, $data_json);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=UTF-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers); 
    // Make the request         
    $response = curl_exec($handle);     
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);   
      if (!empty($response_body))
      {
       $parsed_json=json_decode($response_body);  
       ConstructArray('Admin','skills','DeleteSkillIDForAgentID','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}

function PostSkillsToAgent($skillId,$agentId='',$isActive='',$proficiency='',$overwrite='')
{
if($_SESSION["access_token"]!="")
  {
$data = array(
'agents'=>[array(
'agentId'=>$agentId,
'isActive'=>$isActive,
'overwrite'=>$overwrite,
'proficiency'=>$proficiency
)]);
//create post json data
$data_json = json_encode($data);
$api_URL='/services/v7.0/skills/'.$skillId.'/agents';
//Creating the endpoint for the request  
$endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
 $handle = curl_init($endpoint);
 curl_setopt($handle, CURLOPT_HEADER, true);
 curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
 curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
 curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);   
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
	  $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		  $parsed_json=json_decode($response_body);  
         ConstructArray('Admin','Skills','PostSkillsToAgent','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
function AssignSkillsToAgents($skillId,$agentId='',$isActive='',$proficiency='',$overwrite='')
{
if($_SESSION["access_token"]!="")
  {
	$data = array(
	'agents'=>[array(
	'agentId'=>$agentId,
	'isActive'=>$isActive,
	'overwrite'=>$overwrite,
	'proficiency'=>$proficiency
    )]);
    //create post json data
    $data_json = json_encode($data);
    $api_URL='/services/v7.0/skills/'.$skillId.'/agents';
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);       
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
     	ConstructArray('Admin','Skills','AssignSkillsToAgents','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function GetEmailTranscript($contactId,$includeAttachments='')
{
if($_SESSION["access_token"]!="")
  {  
    $data = array(
	  'includeAttachments'=>$includeAttachments
	  );
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';   
    $api_URL='/services/v7.0/contacts/'.$contactId.'/email-transcript';
    //Creating the endpoint for the request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL.'?'.$params;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);    
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
		ConstructArray('Admin','Skills','GetEmailTranscript','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
	  }
	  ConstructArray('Admin','Skills','GetEmailTranscript','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function getContactFileByContactId($contactId,$fields='')
{
if($_SESSION["access_token"]!="")
  {  
    $data = array(
	  'includeAttachments'=>$includeAttachments
	  );
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';   
    $api_URL='/services/v8.0/contacts/'.$contactId.'/files';
    //Creating the endpoint for the request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL.'?'.$params;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);    
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
		ConstructArray('Admin','Skills','getContactFileByContactId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
	  }
	  ConstructArray('Admin','Skills','getContactFileByContactId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function getListsCallListListIdAttempts($listId,$updatedSince='',$finalized='',$fields='',
$skip='',$top='',$orderby='')
{
if($_SESSION["access_token"]!="")
  {  
    $data = array(
	  'updatedSince'=>$updatedSince,
	  'finalized'=>$finalized,
	  'fields'=>$fields,
	  'skip'=>$skip,
	  'top'=>$top,
	  'orderby'=>$orderby
	  );
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';   
    $api_URL='/services/v8.0/contacts/'.$contactId.'/files';
    //Creating the endpoint for the request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL.'?'.$params;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
     echo 'GetEmailTranscript'.$response;    
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
		ConstructArray('Admin','Skills','getListsCallListListIdAttempts','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
	  }
	 
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function getListsCallListJobs($fields='',$top=10,$skip=0,$orderBy='jobId asc',$startDate='2017-12-1',$endDate='2018-1-1')
{
if($_SESSION["access_token"]!="")
  {  
    $data = array(
	  'updatedSince'=>$updatedSince,
	  'finalized'=>$finalized,
	  'fields'=>$fields,
	  'skip'=>$skip,
	  'top'=>$top,
	  'orderby'=>$orderby
	  );
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';   
    $api_URL='/services/v10.0/lists/call-lists/jobs';
    //Creating the endpoint for the request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL.'?'.$params;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
     echo 'getListsCallListJobs'.$response;    
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
		ConstructArray('Admin','Skills','getListsCallListJobs','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
	  }
	 
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function getListsCallListJobsByJobId($jobId,$fields='')
{
if($_SESSION["access_token"]!="")
  {  
    $data = array(
	  'includeAttachments'=>$includeAttachments
	  );
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';   
    $api_URL='/services/v10.0/lists/call-lists/jobs/'.$jobI;
    //Creating the endpoint for the request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL.'?'.$params;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);    
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
		ConstructArray('Admin','Skills','getListsCallListJobsByJobId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
	  }
	  ConstructArray('Admin','Skills','getListsCallListJobsByJobId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function postContactsTagsByContactId($contactId,$tagId='')
{
if($_SESSION["access_token"]!="")
  {
$data = array(
'tags'=>[array('tagId'=>$tagId
)]);

//create post json data
$data_json = json_encode($data);
$api_URL='/services/v7.0/contacts/'.$contactId.'/tags';
//Creating the endpoint for the request  
$endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
 $handle = curl_init($endpoint);
 curl_setopt($handle, CURLOPT_HEADER, true);
 curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
 curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
 curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);	
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
	  $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		  $parsed_json=json_decode($response_body);  
         ConstructArray('Admin','Skills','postContactsTagsByContactId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }	 
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function postCalllistListUpload($listId,$skillId='',$fileName,$forceOverwrite='false',$defaultTimeZone='',$expirationDate='',$listFile='',$startSkill='false')
{
if($_SESSION["access_token"]!="")
  {
$data = array(
'skillId'=>$skillId,
'fileName'=>$fileName,
'forceOverwrite'=>$forceOverwrite,
'defaultTimeZone'=>$defaultTimeZone,
'expirationDate'=>$expirationDate,
'listFile'=>$listFile,
'startSkill'=>$startSkill,
);

//create post json data
$data_json = json_encode($data);
$api_URL='/services/v10.0/lists/call-lists/'.$listId.'/upload';
//Creating the endpoint for the request  
$endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
 $handle = curl_init($endpoint);
 curl_setopt($handle, CURLOPT_HEADER, true);
 curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
 curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
 curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);	
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
	  $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		  $parsed_json=json_decode($response_body);  
         ConstructArray('Admin','list','postCalllistListUpload','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }	 
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function deleteListsCallListsJobsByJobId($jobId)
{
  //Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {	  
	  $data = array();
	  //create post json data
	  $data_json = json_encode($data);
	  $api_URL='/services/v10.0/lists/call-lists/jobs/'.$jobId;
    //Creating the endpoint for the request
     $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    //Creating a HTTP DELETE request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true); 
    curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "DELETE");
	curl_setopt($handle, CURLOPT_POSTFIELDS, $data_json);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=UTF-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);   
      if (!empty($response_body))
      {
       $parsed_json=json_decode($response_body);  
       ConstructArray('Admin','Agent','deleteListsCallListsJobsByJobId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}

function getGroups($top=10000,$skip=0,$orderBy='groupId',$searchString='',$isActive='true',$fields='')
{
if($_SESSION["access_token"]!="")
  {  
    $data = array(	  
	  'top'=>$top,
	  'skip'=>$skip,
	  'orderBy'=>$orderBy,
	  'searchString'=>$searchString,
	  'isActive'=>$isActive,
	  'fields'=>$fields
	  );
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';   
    $api_URL='/services/v9.0/groups';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL.'?'.$params;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);
    echo $response;
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
		ConstructArray('Admin','Groups','getGroups','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
	  }
	  ConstructArray('Admin','Groups','getGroups','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function createGroups($groupName='',$isActive=true,$notes='')
{
if($_SESSION["access_token"]!="")
  {
$data = array(
'groups'=>[array(
'groupName'=>$groupName,
'isActive'=>$isActive,
'notes'=>$notes
)]);

//create post json data
$data_json = json_encode($data);
$api_URL='/services/v9.0/groups';
//Creating the endpoint for the request  
$endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
 $handle = curl_init($endpoint);
 curl_setopt($handle, CURLOPT_HEADER, true);
 curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
 curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
 curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);	
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
	  $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		  $parsed_json=json_decode($response_body);  
         ConstructArray('Admin','Groups','createGroups','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }	 
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function getGroupsByGroupId($groupId,$fields='')
{
if($_SESSION["access_token"]!="")
  {  
    $data = array(
	  
	  'fields'=>$fields
	  );
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';   
    $api_URL='/services/v9.0/groups/'.$groupId;
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL.'?'.$params;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);
    echo $response;
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
		ConstructArray('Admin','Groups','getGroupsByGroupId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
	  }	 
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function updateGroupsByGroupId($groupId,$groupName='',$isActive='true',$fields='')
{
if($_SESSION["access_token"]!="")
  {
$data = array(
'groupName'=>$groupName,'isActive'=>$isActive,'fields'=>$fields
);
//Create post json data
    $data_json = json_encode($data);
    $api_URL='/services/v9.0/groups/'.$groupId;
    //Creating the endpoint for the request   
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
	curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
	curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
	$headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
    echo 'updateGroupsByGroupId'.$response;
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
	  $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
		  $parsed_json=json_decode($response_body);
		  ConstructArray('Admin','Groups','updateGroupsByGroupId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}


function deleteAgentGroupsByGroupId($groupId,$agentID=1)
{
  //Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {	  
	  $data = array(	  
	  'skills'=>array([
	  'skillId'=>$skillId]));
	  $data_json = json_encode($data);	  
    $api_URL='/services/v9.0/groups/'.$groupId.'/agents';
    //Creating the endpoint for the request   
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true); 
    curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "DELETE");
	curl_setopt($handle, CURLOPT_POSTFIELDS, $data_json);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=UTF-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers); 
    // Make the request         
    $response = curl_exec($handle); 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);  
      if (!empty($response_body))
      {
        //The Response from the api is in JSON        
        $parsed_json=json_decode($response_body);  
		ConstructArray('Admin','Groups','deleteAgentGroupsByGroupId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  ConstructArray('Admin','Groups','deleteAgentGroupsByGroupId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
    } 
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}

function getAgentGroupsByGroupId($groupId,$top=10000,$skip=0,$orderBy='',$searchString='',$assigned='true',$fields='')
{
if($_SESSION["access_token"]!="")
  {  
    $data = array(	  
	  'top'=>$top,
	  'skip'=>$skip,
	  'orderBy'=>$orderBy,
	  'searchString'=>$searchString,
	  'isActive'=>$isActive,
	  'fields'=>$fields
	  );
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';   
    $api_URL='/services/v9.0/groups/'.$groupId.'/agents';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL.'?'.$params;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);
    echo $response;
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
		ConstructArray('Admin','Groups','getAgentGroupsByGroupId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
	  }	 
    ConstructArray('Admin','Groups','getAgentGroupsByGroupId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
  }
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function postAgentGroupsByGroupId($groupId,$agentId=1)
{
if($_SESSION["access_token"]!="")
  {
$data = array(
'agents'=>[array(
'agentId'=>$agentId
)]);
//create post json data
$data_json = json_encode($data);
$api_URL='/services/v9.0/groups/'.$groupId.'/agents';
//Creating the endpoint for the request  
$endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
 $handle = curl_init($endpoint);
 curl_setopt($handle, CURLOPT_HEADER, true);
 curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
 curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
 curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);	
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
	  $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		  $parsed_json=json_decode($response_body);  
         ConstructArray('Admin','Groups','postAgentGroupsByGroupId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
     
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function getThankYouForSkillId($SkillId)
{
if($_SESSION["access_token"]!="")
  {  
    
    $api_URL='/services/v12.0/skills/'.$SkillId.'/thank-you-page';
	echo 'url requested :'.$api_URL;
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);
    echo $response;
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
		ConstructArray('Patron','chatrequesr','getThankYouForSkillId','v12.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
	  }	 
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function postContactChatSendEmail($fromAddress='',$toAddress='',$emailBody='')
{
if($_SESSION["access_token"]!="")
  {
$data = array(
'fromAddress'=>$fromAddress,
'toAddress'=>$toAddress,
'emailBody'=>$emailBody
);
//create post json data
$data_json = json_encode($data);
echo 'input postContactChatSendEmail'.$data_json;
$api_URL='/services/v12.0/contacts/chats/send-email';
//Creating the endpoint for the request  
$endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
 $handle = curl_init($endpoint);
 curl_setopt($handle, CURLOPT_HEADER, true);
 curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
 curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
 curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);	
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
	  $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		  echo $response_body;
		  $parsed_json=json_decode($response_body);  
         ConstructArray('Patron','Chatrequest','postContactChatSendEmail','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }	 
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

// Reporting WFM

function GetwfmSkillContacts($startDate,$endDate,$mediaTypeId,$fields='')
{
if($_SESSION["access_token"]!="")
  {	  
	  $data = array(
	  'startDate'=>$startDate,
	  'endDate'=>$endDate,
	  'fields'=>$fields,
	  'mediaTypeId'=>$mediaTypeId
	  );
	  
	  // Creating Query string
	  foreach($data as $key=>$value)
         $params .= $key.'='.$value.'&';
         
        $params = trim($params, '&'); 
    $api_URL='/services/v11.0/wfm-data/skills/contacts';

    //Creating the endpoint for the request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL.'?'.$params ;
	
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);

	ConstructArray('Reports','WFM','GetwfmSkillContacts','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else 
		 ConstructArray('Reports','WFM','GetwfmSkillContacts','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
function Getwfmagetns($startDate,$endDate,$fields='')
{
if($_SESSION["access_token"]!="")
  {	  
	  $data = array(
	  'startDate'=>$startDate,
	  'endDate'=>$endDate,
	  'fields'=>$fields
	  );
	    // Creating Query string
	  foreach($data as $key=>$value)
         $params .= $key.'='.$value.'&';
         
        $params = trim($params, '&'); 
    $api_URL='/services/v11.0/wfm-data/agents';
    //Creating the endpoint for the request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL.'?'.$params ;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
		
	ConstructArray('Reports','WFM','Getwfmagetns','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else 
		 // echo 'else block';
		 ConstructArray('Reports','WFM','Getwfmagetns','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
function GetwfmdailerContacts($startDate,$endDate,$fields='')
{
if($_SESSION["access_token"]!="")
  {	  
	  $data = array(
	  'startDate'=>$startDate,
	  'endDate'=>$endDate,
	  'fields'=>$fields
	  );
	  // Creating Query string
	  foreach($data as $key=>$value)
         $params .= $key.'='.$value.'&';
         
        $params = trim($params, '&'); 
    $api_URL='/services/v11.0/wfm-data/skills/dialer-contacts';
    //Creating the endpoint for the request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL.'?'.$params ;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
	
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		  //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
	ConstructArray('Reports','WFM','GetwfmdailerContacts','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else
		 // echo 'else block';
		 ConstructArray('Reports','WFM','GetwfmdailerContacts','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
function Getwfmscheduleadherence($startDate,$endDate,$fields="")
{
if($_SESSION["access_token"]!="")
  {	  
	  $data = array(
	  'startDate'=>$startDate,
	  'endDate'=>$endDate,
	  'fields'=>$fields
	  );
	  // Creating Query string
	  foreach($data as $key=>$value)
         $params .= $key.'='.$value.'&';
         
        $params = trim($params, '&'); 

    $api_URL='/services/v11.0/wfm-data/agents/schedule-adherence';
    //Creating the endpoint for the request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL.'?'.$params ;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
	
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    $response = curl_exec($handle);  
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body)){
		  //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
	ConstructArray('Reports','WFM','Getwfmscheduleadherence','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else 
	  
		 // echo 'else block';
		 ConstructArray('Reports','WFM','Getwfmscheduleadherence','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
	  
    }
    curl_close($handle);
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
function GetwfmAgentscorecard($startDate,$endDate,$fields="")
{
if($_SESSION["access_token"]!="")
  {	  
	  $data = array(
	  'startDate'=>$startDate,
	  'endDate'=>$endDate,
	  'fields'=>$fields
	  );
	  // Creating Query String 
	  foreach($data as $key=>$value)
         $params .= $key.'='.$value.'&';
         
        $params = trim($params, '&'); 

    $api_URL='/services/v11.0/wfm-data/agents/scorecards';
    //Creating the endpoint for the request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL.'?'.$params ;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);

    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		  //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
	ConstructArray('Reports','WFM','GetwfmAgentscorecard','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else 
	   // echo 'else block';
		 ConstructArray('Reports','WFM','GetwfmAgentscorecard','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
function GetwfmAgentperformance($startDate,$endDate,$fields="")
{
if($_SESSION["access_token"]!="")
  {	  
	  $data = array(
	  'startDate'=>$startDate,
	  'endDate'=>$endDate,
	  'fields'=>$fields
	  );
	  // Creating Query string
	  foreach($data as $key=>$value)
        $params .= $key.'='.$value.'&';
        $params = trim($params, '&'); 

    $api_URL='/services/v11.0/wfm-data/skills/agent-performance';
    //Creating the endpoint for the request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL.'?'.$params ;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
 
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
	ConstructArray('Reports','WFM','GetwfmAgentperformance','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else 
		 // echo 'else block';
		 ConstructArray('Reports','WFM','GetwfmAgentperformance','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle); 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
function Getwfoascm($startDate,$endDate,$fields="")
{
if($_SESSION["access_token"]!="")
  {	  
	  $data = array(
	  'startDate'=>$startDate,
	  'endDate'=>$endDate,
	  'fields'=>$fields
	  );
	  // Creating Query string
	  foreach($data as $key=>$value)
        $params .= $key.'='.$value.'&';
        $params = trim($params, '&'); 

    $api_URL='/services/v11.0/wfo-data/ascm';
    //Creating the endpoint for the request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL.'?'.$params ;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
 
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
	ConstructArray('Reports','WFO','Getwfoascm','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else 
		 // echo 'else block';
		 ConstructArray('Reports','WFO','Getwfoascm','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle); 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
function Getwfoasi($startDate,$endDate,$fields="")
{
if($_SESSION["access_token"]!="")
  {	  
	  $data = array(
	  'startDate'=>$startDate,
	  'endDate'=>$endDate,
	  'fields'=>$fields
	  );
	  // Creating Query string
	  foreach($data as $key=>$value)
        $params .= $key.'='.$value.'&';
        $params = trim($params, '&'); 

    $api_URL='/services/v11.0/wfo-data/asi';
    //Creating the endpoint for the request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL.'?'.$params ;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
 
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
	ConstructArray('Reports','WFO','Getwfoasi','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else 
		 // echo 'else block';
		 ConstructArray('Reports','WFO','Getwfoasi','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle); 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
function Getwfocsi($startDate,$endDate,$fields="")
{
if($_SESSION["access_token"]!="")
  {	  
	  $data = array(
	  'startDate'=>$startDate,
	  'endDate'=>$endDate,
	  'fields'=>$fields
	  );
	  // Creating Query string
	  foreach($data as $key=>$value)
        $params .= $key.'='.$value.'&';
        $params = trim($params, '&'); 

    $api_URL='/services/v11.0/wfo-data/csi';
    //Creating the endpoint for the request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL.'?'.$params ;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
 
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
	ConstructArray('Reports','WFO','Getwfocsi','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else 
		 // echo 'else block';
		 ConstructArray('Reports','WFO','Getwfocsi','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle); 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
function Getwfoftci($startDate,$endDate,$fields="")
{
if($_SESSION["access_token"]!="")
  {	  
	  $data = array(
	  'startDate'=>$startDate,
	  'endDate'=>$endDate,
	  'fields'=>$fields
	  );
	  // Creating Query string
	  foreach($data as $key=>$value)
        $params .= $key.'='.$value.'&';
        $params = trim($params, '&'); 

    $api_URL='/services/v11.0/wfo-data/ftci';
    //Creating the endpoint for the request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL.'?'.$params ;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
 
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
	ConstructArray('Reports','WFO','Getwfoftci','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else 
		 // echo 'else block';
		 ConstructArray('Reports','WFO','Getwfoftci','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle); 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
function Getwfoosi($startDate,$endDate,$fields="")
{
if($_SESSION["access_token"]!="")
  {	  
	  $data = array(
	  'startDate'=>$startDate,
	  'endDate'=>$endDate,
	  'fields'=>$fields
	  );
	  // Creating Query string
	  foreach($data as $key=>$value)
        $params .= $key.'='.$value.'&';
        $params = trim($params, '&'); 

    $api_URL='/services/v11.0/wfo-data/osi';
    //Creating the endpoint for the request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL.'?'.$params ;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
 
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
	ConstructArray('Reports','WFO','Getwfoosi','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else 
		 // echo 'else block';
		 ConstructArray('Reports','WFO','Getwfoosi','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle); 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
function Getwfoqm($startDate,$endDate,$fields="")
{
if($_SESSION["access_token"]!="")
  {	  
	  $data = array(
	  'startDate'=>$startDate,
	  'endDate'=>$endDate,
	  'fields'=>$fields
	  );
	  // Creating Query string
	  foreach($data as $key=>$value)
        $params .= $key.'='.$value.'&';
        $params = trim($params, '&'); 

    $api_URL='/services/v11.0/wfo-data/qm';
    //Creating the endpoint for the request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL.'?'.$params ;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
 
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
	ConstructArray('Reports','WFO','Getwfoqm','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else 
		 // echo 'else block';
		 ConstructArray('Reports','WFO','Getwfoqm','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle); 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
function getAgentLoginHistory($agentId,$startDate,$endDate,$top="",$searchString="",$skip="",$orderBy="",$fields="")
{
if($_SESSION["access_token"]!="")
  {	  
	  $data = array(
	  'startDate'=>$startDate,
	  'endDate'=>$endDate,
	  'fields'=>$fields,
	  'top'=>$top,
	  'skip'=>$skip,
	  'searchString'=>$searchString,
	  'orderBy'=>$orderBy
	  );
	  // Creating Query string
	  foreach($data as $key=>$value)
        $params .= $key.'='.$value.'&';
        $params = trim($params, '&'); 

    $api_URL='services/v11.0/agents/' . $agentId . '/login-history';
    //Creating the endpoint for the request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL.'?'.$params ;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
 
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
	ConstructArray('Reports','History','getAgentLoginHistory','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else 
		 // echo 'else block';
		 ConstructArray('Reports','History','getAgentLoginHistory','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle); 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
function GetSmstranscripts( $startDate, $endDate, $transportCode , $agentId="", $top="",$skip="",$orderby="")
{
if($_SESSION["access_token"]!="")
  {	  
	  $data = array(
	  'startDate'=>$startDate,
	  'endDate'=>$endDate,
	  'transportCode'=>$transportCode,
	  'top'=>$top,
	  'skip'=>$skip,
	  'orderBy'=>$orderBy,
	  'agentId'=>$agentId
	  );
	  // Creating Query string
	  foreach($data as $key=>$value)
        $params .= $key.'='.$value.'&';
        $params = trim($params, '&'); 

    $api_URL='/services/v11.0/contacts/sms-transcripts';
    //Creating the endpoint for the request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL.'?'.$params ;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
 
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
	ConstructArray('Reports','History','GetSmstranscripts','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else 
		 // echo 'else block';
		 ConstructArray('Reports','History','GetSmstranscripts','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle); 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
function GetSmstranscriptsbyContactid( $contactId,$startDate, $endDate, $transportCode, $top="",$skip="")
{
if($_SESSION["access_token"]!="")
  {
	  $data = array(
	  'startDate'=>$startDate,
	  'endDate'=>$endDate,
	  'transportCode'=>$transportCode,
	  'top'=>$top,
	  'skip'=>$skip,
	  );
	  // Creating Query string
	  foreach($data as $key=>$value)
        $params .= $key.'='.$value.'&';
        $params = trim($params, '&'); 

    $api_URL='/services/v11.0/contacts/'.$contactId.'/sms-transcripts';
    //Creating the endpoint for the request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL.'?'.$params ;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
 
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
	ConstructArray('Reports','History','GetSmstranscriptsbyContactid','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else 
		 // echo 'else block';
		 ConstructArray('Reports','History','GetSmstranscriptsbyContactid','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle); 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
function GetSmsCallQuality($contactId)
{
if($_SESSION["access_token"]!="")
  {
    $api_URL='/services/v11.0/contacts/' . $contactId . '/call-quality';
    //Creating the endpoint for the request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
 
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
		ConstructArray('Reports','History','GetSmsCallQuality','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else 
		 // echo 'else block';
		 ConstructArray('Reports','History','GetSmsCallQuality','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle); 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
function GetTeamPerformanceTotal($startDate,$endDate,$fields="")
{
if($_SESSION["access_token"]!="")
  {	  
	  $data = array(
	  'startDate'=>$startDate,
	  'endDate'=>$endDate,
	  'fields'=>$fields
	  );
	  
	  // Creating Query string
	  foreach($data as $key=>$value)
        $params .= $key.'='.$value.'&';
        $params = trim($params, '&'); 
	  
    $api_URL='/services/v11.0/teams/performance-total';
    //Creating the endpoint for the request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL.'?'.$params ;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
 
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
		ConstructArray('Reports','History','GetTeamPerformanceTotal','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else 
		 // echo 'else block';
		 ConstructArray('Reports','History','GetTeamPerformanceTotal','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle); 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
function GetTeamPerformancebyTeamIDTotal( $teamId,$startDate,$endDate,$fields="")
{
if($_SESSION["access_token"]!="")
  {	  
	  $data = array(
	  'startDate'=>$startDate,
	  'endDate'=>$endDate,
	  'fields'=>$fields
	  );
	  
	  // Creating Query string
	  foreach($data as $key=>$value)
        $params .= $key.'='.$value.'&';
        $params = trim($params, '&'); 
	  
    $api_URL='/services/v11.0/teams/'.$teamId.'/performance-total';
    //Creating the endpoint for the request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL.'?'.$params ;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
 
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
		ConstructArray('Reports','History','GetTeamPerformancebyTeamIDTotal','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else 
		 // echo 'else block';
		 ConstructArray('Reports','History','GetTeamPerformancebyTeamIDTotal','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle); 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
function PostdatadownloadbyReportID( $reportId,$fileName, $startDate, $endDate, $saveAsFile = 'true',$includeHeaders = false)
{
if($_SESSION["access_token"]!="")
  {
$data = array(
  'saveAsFile'=>$saveAsFile,
  'fileName'=>$fileName,
  'startDate'=>$startDate,
  'endDate'=>$endDate,
  'includeHeaders' =>$includeHeaders
);
    
	$data_json = json_encode($data);
	
    $api_URL='/services/v11.0/report-jobs/datadownload/'. $reportId;
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';

	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
		ConstructArray('Admin','Agent','GetdatadownloadbyReportID','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else
		  //echo else block;
		   ConstructArray('Reports','History','GetdatadownloadbyReportID','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
function postContactsChatSessionTyping( $chatSession,$isTyping="",$isTextEntered="",$label="testLabel")
{
if($_SESSION["access_token"]!="")
  {
$data = array(
  'isTyping'=>$isTyping,
  'isTextEntered'=>$isTextEntered,
  'label'=>$label
);
	$data_json = json_encode($data);
    $api_URL='/services/v11.0/contacts/chats/'.$chatSession .'/typing';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';

	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
		ConstructArray('Chat','ChatRequest','postContactsChatSessionTyping','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else
		  //echo else block;
		   ConstructArray('Chat','ChatRequest','postContactsChatSessionTyping','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}
function postContactsChatSessionTypingPreview($chatSession,$previewText = "This text is 100% awesome!!",$label = "testLabel")
{
if($_SESSION["access_token"]!="")
  {
$data = array(
  'previewText'=>$previewText,
  'label'=>$label
);
	$data_json = json_encode($data);
    $api_URL='/services/v11.0/contacts/chats/'.$chatSession .'/typing-preview';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';

	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
		ConstructArray('Chat','ChatRequest','postContactsChatSessionTypingPreview','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else
		  //echo else block;
		   ConstructArray('Chat','ChatRequest','postContactsChatSessionTypingPreview','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
function getChatprofileByPOCId($pointOfContactId)
{
if($_SESSION["access_token"]!="")
  {
    $api_URL='/services/v11.0/points-of-contact/' .$pointOfContactId .'/chat-profile';
    //Creating the endpoint for the request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
 
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
		ConstructArray('Reports','History','getChatprofileByPOCId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else 
		 // echo 'else block';
		 ConstructArray('Reports','History','getChatprofileByPOCId','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle); 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
function postSessionIdInteractionsContactIdtyping($sessionId, $contactId,$isTyping= true,$isTextEntered= true)
{
if($_SESSION["access_token"]!="")
  {
$data = array(
  'isTyping'=>$isTyping,
  'isTextEntered'=>$isTextEntered
);
	$data_json = json_encode($data);
    $api_URL='/services/v11.0/agent-sessions/'.$sessionId.'/interactions/'.$contactId.'/typing';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';

	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
		ConstructArray('Chat','ChatRequest','postSessionIdInteractionsContactIdtyping','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else
		  //echo else block;
		   ConstructArray('Chat','ChatRequest','postSessionIdInteractionsContactIdtyping','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
function posttAgentSessionSessionIdInteractionAddEmail($sessionId)
{
if($_SESSION["access_token"]!="")
  {
    $api_URL='/services/v11.0/agent-sessions/'.$sessionId.'/interactions/add-email';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';

	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
		ConstructArray('Agent','Email','posttAgentSessionSessionIdInteractionAddEmail','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else
		  //echo else block;
		   ConstructArray('Agent','Email','posttAgentSessionSessionIdInteractionAddEmail','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
function posttAgentSessionIdInteractionConatactIdParkemail($sessionId,$ConatctId,$toAddress="",$fromAddress="",$ccAddress="",$bccAddress="",$subject="", $bodyHtml="",$attachments="",$attachmentNames="",$isDraft="false",$originalAttachmentNames="")
{
if($_SESSION["access_token"]!="")
  {
$data = array(
  'toAddress'=>$toAddress,
  'fromAddress'=>$fromAddress,
  'ccAddress'=>$ccAddress,
  'bccAddress'=>$bccAddress,
  'subject'=>$subject,
  'bodyHtml'=>$bodyHtml,
  'attachments'=>$attachments,
  'attachmentNames'=>$attachmentNames,
  'isDraft'=>$isDraft,
  'originalAttachmentNames'=>$originalAttachmentNames
);
	$data_json = json_encode($data);
    $api_URL='/services/v11.0/agent-sessions/'.$sessionId.'/interactions/'.$ConatctId.'/email-park';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';

	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
		ConstructArray('Agent','Email','posttAgentSessionIdInteractionConatactIdParkemail','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else
		  //echo else block;
		   ConstructArray('Agent','Email','posttAgentSessionIdInteractionConatactIdParkemail','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
function posttAgentSessionIdInteractionConatactIdUnparkemail($sessionId,$ConatctId,$isImmediate="")
{
if($_SESSION["access_token"]!="")
  {
$data = array('isImmediate'=>$isImmediate);

	$data_json = json_encode($data);
    $api_URL='/services/v11.0/agent-sessions/'.$sessionId.'/interactions/'.$ConatctId.'/email-unpark';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';

	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
		ConstructArray('Agent','Email','posttAgentSessionIdInteractionConatactIdUnparkemail','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else
		  //echo else block;
		   ConstructArray('Agent','Email','posttAgentSessionIdInteractionConatactIdUnparkemail','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
function posttAgentSessionIdInteractionConatactIdPreview($sessionId,$ConatctId)
{
if($_SESSION["access_token"]!="")
  {
    $api_URL='/services/v11.0/agent-sessions/' .$sessionId . '/interactions/' .$ConatctId. '/email-preview';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';

	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
		ConstructArray('Agent','Email','posttAgentSessionIdInteractionConatactIdPreview','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else
		  //echo else block;
		   ConstructArray('Agent','Email','posttAgentSessionIdInteractionConatactIdPreview','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
function posttAgentSessionIdInteractionConatactIdEmailRestore($sessionId,$ConatctId)
{
if($_SESSION["access_token"]!="")
  {
    $api_URL='/services/v11.0/agent-sessions/' .$sessionId . '/interactions/' .$ConatctId. '/email-restore';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);
		ConstructArray('Agent','Email','posttAgentSessionIdInteractionConatactIdEmailRestore','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE));
      }
	  else
		  //echo else block;
	   ConstructArray('Agent','Email','posttAgentSessionIdInteractionConatactIdEmailRestore','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
function posttAgentSessionIdInteractionConatactIdSnooze($sessionId,$ConatctId)
{
if($_SESSION["access_token"]!="")
  {
    $api_URL='/services/v11.0/agent-sessions/' .$sessionId . '/interactions/' .$ConatctId. '/snooze';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
		 ConstructArray('Agent','Email','posttAgentSessionIdInteractionConatactIdSnooze','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
      }
	  else
		  //echo else block;
	  ConstructArray('Agent','Email','posttAgentSessionIdInteractionConatactIdSnooze','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
function CancelCallback($sessionId,$callbackId,$notes)
{
if($_SESSION["access_token"]!="")
  {
	  $data = array(
	  'notes'=>$notes,
	  );

	$data_json = json_encode($data);
    $api_URL='/services/{version}/agent-sessions/'.$sessionId.+'/interactions/'.$callbackId.'/cancel';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS,data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
		 ConstructArray('Agent','Session','CancelCallback','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
      }
	  else
		  //echo else block;
	  ConstructArray('Agent','Session','CancelCallback','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
function posttAgentSessionIdAddcontact($sessionId,$chat="true",$email="true",$workitem="true")
{
if($_SESSION["access_token"]!="")
  {
	  $data = array(
	  'email'=>$email,
	  'chat'=>$chat,
	  'workitem'=>$workitem
	  );

	$data_json = json_encode($data);
    $api_URL='/services/v11.0/agent-sessions/'.$sessionId.'/add-contact';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS,data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
		 ConstructArray('Agent','Session','posttAgentSessionIdAddcontact','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
      }
	  else
		  //echo else block;
	  ConstructArray('Agent','Session','posttAgentSessionIdAddcontact','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
function posttAgentSessionIdInteractionConatactIdActivate($sessionId, $ConatctId)
{
if($_SESSION["access_token"]!="")
  {
    $api_URL='/services/v11.0/agent-sessions/'.$sessionId.'/interactions/'.$ConatctId.'/activate';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
		 ConstructArray('Agent','Session','posttAgentSessionIdInteractionConatactIdActivate','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
      }
	  else
		  //echo else block;
	  ConstructArray('Agent','Session','posttAgentSessionIdInteractionConatactIdActivate','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
function getContactsParked( $updatedSince='',$mediaTypeId='',$skillId='',$fields="",$campaignId='',$agentId='',$teamId='',$toAddr='',$fromAddr='')
{
if($_SESSION["access_token"]!="")
  {	  
	  $data = array(
	   'updatedSince'=> $updatedSince,
	    'mediaTypeId'=> $mediaTypeId,
		'skillId'=> $skillId,
		'fields'=> $fields,
		'campaignId'=> $campaignId,
		'agentId'=> $agentId,
		'teamId'=> $teamId,
		'toAddr'=> $toAddr,
		'fromAddr'=> $fromAddr
	  );
	  $data_json = json_encode($data);
	  // Creating Query string
	  foreach($data as $key=>$value)
        $params .= $key.'='.$value.'&';
        $params = trim($params, '&'); 
	  
    $api_URL='services/v11.0/contacts/parked';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL.'?'.$params ;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
 
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
		ConstructArray('Realtime','Reporting','getContactsParked','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else 
		 // echo 'else block';
		 ConstructArray('Realtime','Reporting','getContactsParked','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle); 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}

//Version 12 API

function getStandardAddressBookEntries($addressBookId,$updatedSince='',$searchString='',$fields='',$skip='',$top='',$orderBy=''){
if($_SESSION["access_token"]!="")
  {	  
	  $data = array(
	  'updatedSince'=>$updatedSince,
	  'searchString'=>$searchString,
	  'fields'=>$fields,
	  'skip'=>$skip,
	  'top'=>$top,
	  'orderBy'=>$orderBy
	  );
	  // Creating Query string
	  foreach($data as $key=>$value)
        $params .= $key.'='.$value.'&';
        $params = trim($params, '&'); 

    $api_URL='/services/v12.0/address-books/' .$addressBookId.'/entries';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL.'?'.$params ;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
 
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
	ConstructArray('Admin','AddressBook','GetAddressBookEntities','v12.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else 
		 // echo 'else block';
		 ConstructArray('Admin','AddressBook','GetAddressBookEntities','v12.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle); 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function PostAgent(){
	
if($_SESSION["access_token"]!="")
  {
	  $data = array(
	  "firstName"=>"Sample",
	  "middleName"=>"Sample"
	  );
	$data_json = json_encode($data);
    $api_URL='/services/v12.0/agents';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS,data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
		 ConstructArray('Admin','Agent','PostAgent','v12.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
      }
	  else
		  //echo else block;
	  ConstructArray('Admin','Agent','PostAgent','v12.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
//Version 15
function PutUnavailableCodesByID($unavailableCodeId)
{
if($_SESSION["access_token"]!="")
  {
    //create post team json data
    $data = array(
       "unavailableCodeName"=> "sample",
       "postContact"=> true,
       "agentTimeout"=> 0,
       "isActive"=> true);
 
    $data_json = json_encode($data);
    $api_URL='/services/v15.0/unavailable-codes/'.$unavailableCodeId;
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
        //echo $parsed_json;
		ConstructArray('Admin','Agent','PUT /unavailable-codes/{unavailableCodeId}','v15.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Agent','PUT /unavailable-codes/{unavailableCodeId}','v15.0','No access Token');
}
function getAccessKeys ($agentId,$fields='',$skip='',$top='',$orderBy=''){
if($_SESSION["access_token"]!="")
  {	  
	  $data = array(
	  'agentId'=>$agentId,
	  'fields'=>$fields,
	  'skip'=>$skip,
	  'top'=>$top,
	  'orderBy'=>$orderBy
	  );
	  // Creating Query string
	  foreach($data as $key=>$value)
        $params .= $key.'='.$value.'&';
        $params = trim($params, '&'); 

    $api_URL='/services/v15.0/access-keys';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL.'?'.$params ;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
 
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
	ConstructArray('Admin','Agent','GET /access-keys','v15.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else 
		 // echo 'else block';
		 ConstructArray('Admin','Agent','GET /access-keys','v15.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle); 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function PostAccessKeys($agentId){
	
if($_SESSION["access_token"]!="")
  {
	  $data = array(
	  "agentId"=>$agentId);
	$data_json = json_encode($data);
    $api_URL='/services/V15.0/access-keys';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS,data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
		 ConstructArray('Admin','Agent','POST /access-keys','v15.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
      }
	  else
		  //echo else block;
	  ConstructArray('Admin','Agent','POST /access-keys','v15.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}

function deleteAccessKeysByID($accesskeyId)
{
	// session_start();
  //Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {
	  
	  $data = array();
	  $data_json = json_encode($data);
	  //print_r($data_json);
	  //echo 'input data'.$data_json;
    //addressBookId is a required field
    $api_URL='/services/v15.0/access-keys/'.$accesskeyId;
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
 
    //Creating a HTTP DELETE request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true); 
    curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "DELETE");
	curl_setopt($handle, CURLOPT_POSTFIELDS, $data_json);
 
    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
 
    //By default, cURL is setup to not trust any CAs 
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security 
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=UTF-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
     // echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);   
      echo "<br>";      
      //echo "Response Header:".$response_headers;
      echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
       // echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  
 
        //Use the response data
       // print_r($parsed_json);
		ConstructArray('Admin','Agent','DELETE /access-keys/{accessKeyId}','v15.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}

function getACcesskeysByID($accesskeyId){
  
  //Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {
    $api_URL="/services/v15.0/access-keys/".$accesskeyId;
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    //Set to TRUE to return the output of curl_exec as a string
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true);
    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request
    $response = curl_exec($handle);
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      //echo "Status Code of the Response:".$http_code;
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);
      //echo "Response Header:".$response_headers;
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
        //echo "Response Body:";
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);
        //Use the response data
        //print_r($parsed_json);
        ConstructArray('Admin','Agent','GET /access-keys/{accessKeyId}','v15.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
    // Close the curl session
    curl_close($handle);
    //Report all PHP errors (notices, errors, warnings, etc.)
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
	ConstructArray('Admin','Agent','GET /access-keys/{accessKeyId}','v15.0','No access Token');
    //echo "No access Token";
    }

function PatchAccessKeysByID($accesskeyId,$isActive='')
{
if($_SESSION["access_token"]!="")
  {
    //create post team json data
    $data = array(
       "isActive"=> $isActive);
 
    $data_json = json_encode($data);
    $api_URL='/services/v15.0/access-keys/'.$accesskeyId;
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PATCH");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
        //echo $parsed_json;
		ConstructArray('Admin','Agent','PATCH /access-keys/{accessKeyId}','v15.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Agent','PATCH /access-keys/{accessKeyId}','v15.0','No access Token');
}

function PostHoursofOperation(){
	
if($_SESSION["access_token"]!="")
  {
	  $data = array(
	  "profileName"=> "string");
	$data_json = json_encode($data);
    $api_URL='/services/v15.0/hours-of-operation';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS,data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
		 ConstructArray('Admin','General','POST /hours-of-operation','v15.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
      }
	  else
		  //echo else block;
	  ConstructArray('Admin','General','POST /hours-of-operation','v15.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}

function PutHoursofOperationByID($profileId)
{
if($_SESSION["access_token"]!="")
  {
    //create post team json data
    $data = array(
       "profileName"=> "sample",
       "description"=> "",
       "notes"=> "",
       "days"=> "");
 
    $data_json = json_encode($data);
    $api_URL='/services/v15.0/hours-of-operation/'.$profileId;
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
        //echo $parsed_json;
		ConstructArray('Admin','General','PUT /hours-of-operation/{profileId}','v15.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','General','PUT /hours-of-operation/{profileId}','v15.0','No access Token');
}

function PostPointofContact(){
	
if($_SESSION["access_token"]!="")
  {
	  $data = array(
	  "pointOfContact"=> "string",
      "pointOfContactName"=> "string",
      "skillId"=> 0,
      "isActive"=> true,
      "mediaTypeId"=> 0,
      "scriptName"=> "string",
      "ivrReportingEnabled"=> true);
	$data_json = json_encode($data);
    $api_URL='/services/v15.0/points-of-contact';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS,data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
		 ConstructArray('Admin','General','POST /points-of-contact','v15.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
      }
	  else
		  //echo else block;
	  ConstructArray('Admin','General','POST /points-of-contact','v15.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}

function PutPointofContactByID($pointOfContactId)
{
if($_SESSION["access_token"]!="")
  {
    //create post team json data
    $data = array(
       "pointOfContactName"=> "string",
       "skillId"=> 0,
       "isActive"=> true,
       "scriptName"=> "string",
       "ivrReportingEnabled"=> true);
 
    $data_json = json_encode($data);
    $api_URL='/services/v15.0/points-of-contact/'.$pointOfContactId;
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
        //echo $parsed_json;
		ConstructArray('Admin','General','PUT /points-of-contact/{pointOfContactId}','v15.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','General','PUT /points-of-contact/{pointOfContactId}','v15.0','No access Token');
}

function PostUnavailableCodes(){
	
if($_SESSION["access_token"]!="")
  {
	  $data = array(
	  "unavailableCodeName"=> "string",
      "postContact"=> true,
      "isActive"=> true);
	$data_json = json_encode($data);
    $api_URL='/services/v15.0/unavailable-codes';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS,data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
		 ConstructArray('Admin','General','POST /unavailable-codes','v15.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
      }
	  else
		  //echo else block;
	  ConstructArray('Admin','General','POST /unavailable-codes','v15.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}

function getPhonenumbers($searchString,$skip='',$top=''){
if($_SESSION["access_token"]!="")
  {	  
	  $data = array(
	  'searchString'=>$searchString,
	  'skip'=>$skip,
	  'top'=>$top
	  );
	  // Creating Query string
	  foreach($data as $key=>$value)
        $params .= $key.'='.$value.'&';
        $params = trim($params, '&'); 

    $api_URL='/services/v15.0/phone-numbers';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL.'?'.$params ;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
 
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
	ConstructArray('Admin','General','GET /phone-numbers','v15.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
	  else 
		 // echo 'else block';
		 ConstructArray('Admin','General','GET /phone-numbers','v15.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle); 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function PostCampaigns(){
	
if($_SESSION["access_token"]!="")
  {
	  $data = array(
	  "campaigns"=> [array
    (
      "campaignName"=> "string",
      "isActive"=> true,
      "description"=> "string",
      "notes"=> "string"
    )]);
	$data_json = json_encode($data);
    $api_URL='/services/v15.0/campaigns';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS,data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
		 ConstructArray('Admin','Skills','POST /campaigns','v15.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
      }
	  else
		  //echo else block;
	  ConstructArray('Admin','Skills','POST /campaigns','v15.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}

function PutCampaignsByID($campaignId)
{
if($_SESSION["access_token"]!="")
  {
    //create post team json data
    $data = array(
       "campaigns"=> [array
    (
      "campaignName"=> "string",
      "isActive"=> true,
      "description"=> "string",
      "notes"=> "string"
    )]);
 
    $data_json = json_encode($data);
    $api_URL='/services/v15.0/campaigns/'.$campaignId;
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
        //echo $parsed_json;
		ConstructArray('Admin','Skills','PUT /campaigns/{campaignId}','v15.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Skills','PUT /campaigns/{campaignId}','v15.0','No access Token');
}

function getDispostionBySkill($isActive,$updatedSince='',$searchString='',$fields='',
$skip='',$top='',$orderby='')
{
if($_SESSION["access_token"]!="")
  {  
    $data = array(
	  'updatedSince'=>$updatedSince,
	  'searchString'=>$searchString,
	  'fields'=>$fields,
	  'skip'=>$skip,
	  'top'=>$top,
	  'orderby'=>$orderby,
	  'isActive'=>$isActive
	  );
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';   
    $api_URL='/services/v15.0/dispositions/skills';
    //Creating the endpoint for the request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL.'?'.$params;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
     echo 'GetEmailTranscript'.$response;    
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
		ConstructArray('Admin','Skills','GET /dispositions/skills','v15.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
	  }
	 
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function DeleteCampaignsBySkill($campaignId)
{
	// session_start();
  //Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {
	  
	  $data = array(
	  
	  "skills"=> [array
    (
      "skillId"=> 0,
      "transferCampaignId"=> 0
    )
  ]
	  );
	  $data_json = json_encode($data);
	  //print_r($data_json);
	  //echo 'input data'.$data_json;
    //addressBookId is a required field
    $api_URL='/services/v15.0/campaigns/'.$campaignId.'/skills';
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
 
    //Creating a HTTP DELETE request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true); 
    curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "DELETE");
	curl_setopt($handle, CURLOPT_POSTFIELDS, $data_json);
 
    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
 
    //By default, cURL is setup to not trust any CAs 
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security 
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=UTF-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
     // echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);   
      echo "<br>";      
      //echo "Response Header:".$response_headers;
      echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
       // echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  
 
        //Use the response data
       // print_r($parsed_json);
		ConstructArray('Admin','Skills','DELETE /campaigns/{campaignId}/skills','v15.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}


function PostCampaignsBySkill($campaignId){
	
if($_SESSION["access_token"]!="")
  {
	  $data = array(
	  "skills"=> [array
    (
      "skillId"=> 0
    )
  ]);
	$data_json = json_encode($data);
    $api_URL='/services/v15.0/campaigns/'.$campaignId.'/skills';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS,data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
		 ConstructArray('Admin','Skills','POST /campaigns/{campaignId}/skills','v15.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
      }
	  else
		  //echo else block;
	  ConstructArray('Admin','Skills','POST /campaigns/{campaignId}/skills','v15.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}

function getSmsHistoricalTranscripts($businessUnitId,$contactId)
{
if($_SESSION["access_token"]!="")
  {  
    $data = array(
	  'businessUnitId'=>$businessUnitId
	  );
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';   
    $api_URL='/services/v15.0/'.$contactId.'/sms-historical-transcript';
    //Creating the endpoint for the request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL.'?'.$params;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
     echo 'GetEmailTranscript'.$response;    
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
		ConstructArray('Admin','Contacts','GET /contacts/{contactId}/sms-historical-transcript','v15.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
	  }
	 
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}


function getSmsHistoricalContacts($businessUnitId,$skillId,$ani)
{
if($_SESSION["access_token"]!="")
  {  
    $data = array(
	  'businessUnitId'=>$businessUnitId,
	  'skillId'=>$skillId,
	  'ani'=>$ani
	  );
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';   
    $api_URL='/services/v15.0/contacts/sms-historical-contacts';
    //Creating the endpoint for the request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL.'?'.$params;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
     echo 'GetEmailTranscript'.$response;    
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
		ConstructArray('Admin','Contacts','GET /contacts/sms-historical-contacts','v15.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
	  }
	 
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";

}

function PutUnavailableCodesByIDTeams($unavailableCodeId,$securityUser)
{
if($_SESSION["access_token"]!="")
  {
    //create post team json data
    $data = array(
      'securityUser'=>$securityUser,
       "teams"=> [array
    (
      "teamId"=> "string")]);
 
    $data_json = json_encode($data);
    $api_URL='/services/v15.0/unavailable-codes/'.$unavailableCodeId.'/teams';
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
        //echo $parsed_json;
		ConstructArray('Admin','Agents','PUT /unavailable-codes/{unavailableCodeId}/teams','v15.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Agents','PUT /unavailable-codes/{unavailableCodeId}/teams','v15.0','No access Token');
}

function postEmailSaveDraft($sessionId,$contactId){
	
if($_SESSION["access_token"]!="")
  {
	  $data = array(
	  "toAddress"=> "",	
      "fromAddress"=> "",
	  "bccAddress"=> "" ,	
      "campaignId"=> 0,
      "subject"=>"",
	  "bodyHtml"=>"" ,	
      "fromAddress"=> "",
      "attachments"=> "",
	  "attachmentNames"=> "" ,	
      "draftEmailGuidStr"=> "",
      "primaryDispositionId"=> 0,
	  "secondaryDispositionId"=> 0,
	  "tags"=> "",
	  "notes"=> "",
	  "originalAttachmentNames"=> ""
	  );
	$data_json = json_encode($data);
    $api_URL='/services/V15.0/agent-sessions/'.$sessionId.'/interactions/'.$contactId.'/email-save-draft';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS,data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
		 ConstructArray('Agents','Email','POST /email-save-draft','v15.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
      }
	  else
		  //echo else block;
	  ConstructArray('Agents','Email','POST /email-save-draft','v15.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}


function postJobSync(){
	
if($_SESSION["access_token"]!="")
  {
	  $data = array(
	  "entityName"=> "qm-workflows",
	  "version"=>"1",
	  "startDate"=>"2019-07-10",
	  "endDate"=>"2019-07-11"
	  );
	$data_json = json_encode($data);
    $api_URL='data-extraction/v1/jobs/sync';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["CXone_resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS,data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
		 ConstructArray('Data Extraction','Extract data','POST /job-sync','v1.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
      }
	  else
		  //echo else block;
	  ConstructArray('Data Extraction','Extract data','POST /job-sync','v1.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}

function getmediaplaybackByID($contactID,$mediatype,$excludewaveform)
{
if($_SESSION["access_token"]!="")
  {  
    $data = array(
	  'mediatype'=>$mediatype,
	  'excludewaveform'=>$excludewaveform
	  );
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';   
    $api_URL='media-playback/v1/contacts/'.$contactID;
    //Creating the endpoint for the request
    $endpoint=$_SESSION["CXone_resource_server_base_uri"].$api_URL.'?'.$params;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);      
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
    		 //echo $parsed_json;
		ConstructArray('mediaplayback','AccessingRecordingData','GET /media-playback/v1/contacts/{contactId}','v1.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
	  }
	  
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('mediaplayback','AccessingRecordingData','GET /media-playback/v1/contacts/{contactId}','v11.0','No access Token');
}

function getmediaplaybackBysegemntID($contactID,$segmentID,$mediatype,$excludewaveform)
{
if($_SESSION["access_token"]!="")
  {  
    $data = array(
	  'mediatype'=>$mediatype,
	  'excludewaveform'=>$excludewaveform
	  );
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';   
    $api_URL='media-playback/v1/contacts/'.$contactID.'/segments/'.$segmentID;
    //Creating the endpoint for the request
    $endpoint=$_SESSION["CXone_resource_server_base_uri"].$api_URL.'?'.$params;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);      
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
    		 //echo $parsed_json;
		ConstructArray('mediaplayback','AccessingRecordingData','GET /media-playback​/v1​/contacts​/{contactId}​/segments​/{segmentId}','v1.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
	  }
	  
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('mediaplayback','AccessingRecordingData','GET /media-playback​/v1​/contacts​/{contactId}​/segments​/{segmentId}','v1.0','No access Token');
}

function getmediaplaybackcontacts($acdID,$mediatype,$excludewaveform)
{
if($_SESSION["access_token"]!="")
  {  
    $data = array(
	  'mediatype'=>$mediatype,
	  'excludewaveform'=>$excludewaveform
	  );
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';   
    $api_URL='media-playback/v1/contacts/'.$acdID;
    //Creating the endpoint for the request
    $endpoint=$_SESSION["CXone_resource_server_base_uri"].$api_URL.'?'.$params;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);      
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
    		 //echo $parsed_json;
		ConstructArray('mediaplayback','AccessingRecordingData','GET /media-playback/v1/contacts','v1.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
	  }
	  
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('mediaplayback','AccessingRecordingData','/media-playback/v1/contacts','v1.0','No access Token');
}

function getActiveFlag($activeFlag)
{
if($_SESSION["access_token"]!="")
  {  
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';   
    $api_URL='/services/v16.0/workflow-data/list/'.$activeFlag;
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);      
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
    		 //echo $parsed_json;
		ConstructArray('Admin','Workflow ','GET /workflow-data/list/activeFlag','v16.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
	  }
	  
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Workflow ','GET /workflow-data/list/activeFlag','v16.0','No access Token');
}

function postWorkflowData(){
	
if($_SESSION["access_token"]!="")
  {
	  $data = array(
  "profile" => array(
    "ProfileName" => "new",
    "Description" => "abc",
    "ProfileID" => 0
  ),
  "data" => array(
    "date" => array(
      "Value" => [
        "string"
      ],
      "Visible" => "1 ",
      "Type" => " ",
      "Ref" => " "
    )
  )
);
	$data_json = json_encode($data);
    $api_URL='/services/v16.0/workflow-data';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS,data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
		 ConstructArray('Admin','Workflow ','POST /workflow-data','v16.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
      }
	  else
		  //echo else block;
	  ConstructArray('Admin','Workflow ','POST /workflow-data','v16.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}

function getWorkflowDataById($workflowDataId)
{
if($_SESSION["access_token"]!="")
  {  
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';   
    $api_URL='/services/v16.0/workflow-data/'.$workflowDataId;
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);      
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
    		 //echo $parsed_json;
		ConstructArray('Admin','Workflow ','GET /workflow-data/Id','v16.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
	  }
	  
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Workflow ','GET /workflow-data/Id','v16.0','No access Token');
}

function PutWorkflowDataById($workflowDataId)
{
if($_SESSION["access_token"]!="")
  {
    //create post team json data
    $data = array(
  "profile" => array(
    "ProfileName" => "new",
    "Description" => "abc",
    "ProfileID" => 0
  ),
  "data" => array(
    "date" => array(
      "Value" => [
        "string"
      ],
      "Visible" => "1 ",
      "Type" => " ",
      "Ref" => " "
    )
  )
);

    $data_json = json_encode($data);
    $api_URL='/services/v16.0/workflow-data/'.$workflowDataId;
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
        //echo $parsed_json;
		ConstructArray('Admin','Workflow ','PUT /workflow-data/Id','v16.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Workflow ','PUT /workflow-data/Id','v16.0','No access Token');
}

function PutWorkflowDataByIdActivate($workflowDataId)
{
if($_SESSION["access_token"]!="")
  {
    //create post team json data
    $api_URL='/services/v16.0/workflow-data/'.$workflowDataId.'/activate';
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
        //echo $parsed_json;
		ConstructArray('Admin','Workflow ','PUT /workflow-data/activate','v16.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Workflow ','PUT /workflow-data/activate','v16.0','No access Token');
}


function PutWorkflowDataByIdDeativate($workflowDataId)
{
if($_SESSION["access_token"]!="")
  {
    //create post team json data
    $api_URL='/services/v16.0/workflow-data/'.$workflowDataId.'/deactivate';
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
        //echo $parsed_json;
		ConstructArray('Admin','Workflow ','PUT /workflow-data/Id/deactivate','v16.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Workflow ','PUT /workflow-data/deactivate','v16.0','No access Token');
}

function getJobs()
{
if($_SESSION["access_token"]!="")
  {  
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';   
    $api_URL='data-extraction/v1/jobs';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["CXone_resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);      
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
    		 //echo $parsed_json;
		ConstructArray('Data Extraction','Extract data','get /job','v1.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
	  }
	  
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Data Extraction','Extract data','POST /job','v1.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
}

function postJobs(){
	
if($_SESSION["access_token"]!="")
  {
	  $data = array(
	  "entityName"=> "qm-workflows",
	  "version"=>"1",
	  "startDate"=>"2019-07-10",
	  "endDate"=>"2019-07-11"
	  );
	$data_json = json_encode($data);
    $api_URL='data-extraction/v1/jobs/sync';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["CXone_resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS,data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
		 ConstructArray('Data Extraction','Extract data','POST /job','v1.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
      }
	  else
		  //echo else block;
	  ConstructArray('Data Extraction','Extract data','POST /job','v1.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) ); 
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}

function getJobsByID($jobsID)
{
if($_SESSION["access_token"]!="")
  {  
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';   
    $api_URL='data-extraction/v1/jobs/'.$jobsID;
    //Creating the endpoint for the request
    $endpoint=$_SESSION["CXone_resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);      
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
    		 //echo $parsed_json;
		ConstructArray('Data Extraction','Extract data','POST /job/jobId','v1.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
	  }
	  
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Data Extraction','Extract data','get /job/jobId','v1.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
}

function getBusinessUnitTimezone()
{
if($_SESSION["access_token"]!="")
  {  
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';   
    $api_URL='/services/v17/business-unit/time-zones';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["CXone_resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);      
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
    		 //echo $parsed_json;
		ConstructArray('Admin','General','Get /business-unit/time-zones','v17.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
	  }
	  
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','General','get /business-unit/time-zones','v17.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
}

function getSuppresedContacts()
{
if($_SESSION["access_token"]!="")
  {  
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';   
    $api_URL='/services/v17/suppressed-contact';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["CXone_resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);      
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
    		 //echo $parsed_json;
		ConstructArray('Admin','General','Get /suppressed-contact','v17.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
	  }
	  
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','General','get /suppressed-contact','v17.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
}

function PostSuppresedContacts(){
if($_SESSION["access_token"]!="")
  {
     $data = array(
      "startDate"=> "string",
      "endDate"=> "string",
      "value"=> "string",
       "skills"=>"string");
    $data_json = json_encode($data);
    $api_URL='/services/v17/suppressed-contact';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($ch, CURLOPT_POST, 1);
    curl_setopt($ch, CURLOPT_POSTFIELDS,data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
		ConstructArray('Admin','General','Post /suppressed-contact','v17.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
     else
       //echo else block;
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    ConstructArray('Admin','General','Post /suppressed-contact','v17.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
}

function PutbusinessunitTimezone()
{
if($_SESSION["access_token"]!="")
  {
   //create post team json data
  $data = array(
    "timezones" => "new",
    "items" => "abc",
  );
    $data_json = json_encode($data);
    $api_URL='/services/v17/business-unit/time-zones';
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
        ConstructArray('Admin','General','Put /business-unit/time-zones','v17.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    ConstructArray('Admin','General','Put /business-unit/time-zones','v17.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
}

 function PutSkillIDParameterTimezone($skillId,timeZoneSettings='')
{
if($_SESSION["access_token"]!="")
  {
	$data = array(
	'timeZoneSettings'=>[array(
	'timeZoneSettings'=>timeZoneSettings
    )]);
    //create post json data
    $data_json = json_encode($data);
    $api_URL='/services/{version}/skills/'.$skillId.'/parameters/time-zones";
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, ; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);       
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
		ConstructArray('Admin','Skill','Put /parameters/time-zones','v17.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
         	echo $parsed_json;
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
   ConstructArray('Admin','Skill','Put /parameters/time-zones','v17.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
}

function getSkillIDParameterTimezone($skillId)
{
if($_SESSION["access_token"]!="")
  {  
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';   
    $api_URL='/services/{version}/skills/'.$skillId.'/parameters/time-zones';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["CXone_resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);      
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
    		 //echo $parsed_json;
		ConstructArray('Admin','Skill','Get /parameters/time-zones','v17.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
	  }
	  
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Skill','get /parameters/time-zones','v17.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
}


function deleteSuppressedContactBySuppressedContactId($suppressedContactId)
{
if($_SESSION["access_token"]!="")
  {
	$data = array(	  
	  'profileId'=>$profileId);
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';
    $api_URL='/services/{version}/suppressed-contact/'.$suppressedContactId;
    //Creating the endpoint for the request   
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL.'?'.$params;	
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);    
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers); 
    // Make the request         
    $response = curl_exec($handle);     
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);    
      $parts = explode("\r\n\r\nHTTP/", $response);     
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);      
      if (!empty($response_body))
      {
        //The Response from the api is in JSON        
        $parsed_json=json_decode($response_body); 
        echo $parsed_json;
	   }
	  
    } 
     
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}

function getContactsByIdHierachy($contactId)
{
if($_SESSION["access_token"]!="")
  {  
    $data = array(
      'contactId'=>contactId
     );
    foreach($data as $key=>$value)
    $params .= $key.'='.$value.'&';   
    $api_URL='/services/{version}/contacts/{contactId}/hierarchy';
    //Creating the endpoint for the request
    $endpoint='https://api-sc1.ucnlabext.com/InContactAPI/'.$api_URL.'?'.$params;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
     echo 'GetEmailTranscript'.$response;    
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
      }
    }
    // Close the curl session  
    curl_close($handle);
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}

function getSuppressedContactBySuppressedContactId($suppressedContactId)
{
if($_SESSION["access_token"]!="")
  {
	$data = array(	  
	  'suppressedContactId'=>$suppressedContactId);
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';
    $api_URL='/services/{version}/suppressed-contact/'.$suppressedContactId;
    //Creating the endpoint for the request   
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL.'?'.$params;	
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);    
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers); 
    // Make the request         
    $response = curl_exec($handle);     
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);    
      $parts = explode("\r\n\r\nHTTP/", $response);     
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);      
      if (!empty($response_body))
      {
        //The Response from the api is in JSON        
        $parsed_json=json_decode($response_body); 
        echo $parsed_json;
	   }
	  
    } 
     
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}

function putSuppressedContactBySuppressedContactId($suppressedContactId,$startDate='',$endDate='',$value='',$skills='')
{
if($_SESSION["access_token"]!="")
  {
$data = array(  
  'startDate'=>$startDate,
  'endDate'=>$endDate,
  'value'=> $value,
  'skills'=> $skills,
);
    //create post json data
    $data_json = json_encode($data);
    $api_URL='/services/{version}/suppressed-contact/'.$suppressedContactId;
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);       
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
        echo $parsed_json;
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}


function getBusinessUnitTimezone()
{
if($_SESSION["access_token"]!="")
  {  
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';   
    $api_URL='/services/v17/business-unit/time-zones';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["CXone_resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);      
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
    		 //echo $parsed_json;
		ConstructArray('Admin','General','Get /business-unit/time-zones','v17.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
	  }
	  
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','General','get /business-unit/time-zones','v17.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
}

function getSuppresedContacts()
{
if($_SESSION["access_token"]!="")
  {  
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';   
    $api_URL='/services/v17/suppressed-contact';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["CXone_resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);      
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
    		 //echo $parsed_json;
		ConstructArray('Admin','General','Get /suppressed-contact','v17.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
	  }
	  
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','General','get /suppressed-contact','v17.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
}

function PostSuppresedContacts(){
if($_SESSION["access_token"]!="")
  {
     $data = array(
      "startDate"=> "string",
      "endDate"=> "string",
      "value"=> "string",
       "skills"=>"string");
    $data_json = json_encode($data);
    $api_URL='/services/v17/suppressed-contact';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($ch, CURLOPT_POST, 1);
    curl_setopt($ch, CURLOPT_POSTFIELDS,data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
		ConstructArray('Admin','General','Post /suppressed-contact','v17.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
     else
       //echo else block;
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    ConstructArray('Admin','General','Post /suppressed-contact','v17.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
}

function PutbusinessunitTimezone()
{
if($_SESSION["access_token"]!="")
  {
   //create post team json data
  $data = array(
    "timezones" => "new",
    "items" => "abc",
  );
    $data_json = json_encode($data);
    $api_URL='/services/v17/business-unit/time-zones';
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
        ConstructArray('Admin','General','Put /business-unit/time-zones','v17.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    ConstructArray('Admin','General','Put /business-unit/time-zones','v17.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
}

 function PutSkillIDParameterTimezone($skillId,timeZoneSettings='')
{
if($_SESSION["access_token"]!="")
  {
	$data = array(
	'timeZoneSettings'=>[array(
	'timeZoneSettings'=>timeZoneSettings
    )]);
    //create post json data
    $data_json = json_encode($data);
    $api_URL='/services/{version}/skills/'.$skillId.'/parameters/time-zones";
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, ; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);       
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
		ConstructArray('Admin','Skill','Put /parameters/time-zones','v17.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
         	echo $parsed_json;
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
   ConstructArray('Admin','Skill','Put /parameters/time-zones','v17.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
}

function getSkillIDParameterTimezone($skillId)
{
if($_SESSION["access_token"]!="")
  {  
	  foreach($data as $key=>$value)
      $params .= $key.'='.$value.'&';   
    $api_URL='/services/{version}/skills/'.$skillId.'/parameters/time-zones';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["CXone_resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);      
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
		$parsed_json=json_decode($response_body);  
    		 //echo $parsed_json;
		ConstructArray('Admin','Skill','Get /parameters/time-zones','v17.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
	  }
	  
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Skill','get /parameters/time-zones','v17.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
}

function DeleteskillsSkillIDAgentID($skillId,$agentID)
{
	// session_start();
  //Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {
	  //print_r($data_json);
	  //echo 'input data'.$data_json;
    //addressBookId is a required field
    $api_URL='/services/v7.0/skills/'.$skillId.'/agents/'.$agentID;
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
 
    //Creating a HTTP DELETE request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true); 
    curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "DELETE");
	curl_setopt($handle, CURLOPT_POSTFIELDS, $data_json);
 
    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
 
    //By default, cURL is setup to not trust any CAs 
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security 
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=UTF-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
     // echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);   
      echo "<br>";      
      //echo "Response Header:".$response_headers;
      echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
       // echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  
 
        //Use the response data
       // print_r($parsed_json);
		ConstructArray('Admin','Skills','Delete /skills/{skillId}/agents/{agentId}','v11.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
function getScripts18($scriptPath='',$scriptId='')
{
  //The following code sample assumes that the access token is stored in the session
  session_start();
  //Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {
    $scriptPath=$_GET['scriptPath'];
    $scriptId=$_GET['scriptId'];

    $api_URL="services/{version}/scripts?scriptPath=".$scriptPath"&scriptId=".$scriptId;

    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;

    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);

    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 

    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);

    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);

    // Make the request         
    $response = curl_exec($handle);  

    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      echo "<br>";      
      echo "Response Header:".$response_headers;
      echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
        echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  

        //Use the response data
        print_r($parsed_json);
      }
    }

    // Close the curl session  
    curl_close($handle);

    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}

function postScripts18($scriptPath='',$body='')
{
 //The following code sample assumes that the access token is stored in the session
  session_start();
  //Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {
    $scriptPath=$_POST['$scriptPath'];
    $body=$_POST['body'];

    $api_URL="services/{version}/scripts";

    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;

    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);

    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 

    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);

    //Setting POST data
    //The post data for a script is not JSON 
    //It is actually a large HTML String.
    //This example is hard coded for one full entry.
    $post_data = "scriptId=".$scriptId.
                "&skillId=".$skillId.
    curl_setopt($handle, CURLOPT_POSTFIELDS, $post_data);

    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);

    // Make the request         
    $response = curl_exec($handle);  

    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      echo "<br>";      
      echo "Response Header:".$response_headers;
      echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
        echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  

        //Use the response data
        print_r($parsed_json);
      }
    }

    // Close the curl session  
    curl_close($handle);

    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
	}
	
function updateScript($scriptPath='',$lockScript='false')
{
if($_SESSION["access_token"]!="")
  {
$data = array(  
    'scriptPath'=> '$scriptPath',
    'lockScript'=> $lockScript
);
//create post json data
    $data_json = json_encode($data);
    $api_URL='/services/{version}/scripts';
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);       
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
         echo $parsed_json;
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}	
function postScriptsKick($scriptPath='')
{
//The following code sample assumes that the access token is stored in the session
  session_start();
  //Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {
    $scriptPath=$_POST['$scriptPath'];

    $api_URL="services/{version}/scripts/kick";

    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;

    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);

    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 

    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);

    //Setting POST data
    //The post data for a script is not JSON 
    //It is actually a large HTML String.
    //This example is hard coded for one full entry.
    $post_data = "scriptId=".$scriptId.
                "&skillId=".$skillId.
    curl_setopt($handle, CURLOPT_POSTFIELDS, $post_data);

    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);

    // Make the request         
    $response = curl_exec($handle);  

    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      echo "<br>";      
      echo "Response Header:".$response_headers;
      echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
        echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  

        //Use the response data
        print_r($parsed_json);
      }
    }

    // Close the curl session  
    curl_close($handle);

    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
	
function getScriptsHistoryByName($scriptPath='')
{
 //The following code sample assumes that the access token is stored in the session
  session_start();
  //Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {
    $scriptPath=$_GET['scriptPath'];

    $api_URL="services/{version}/scripts/historyByName/{scriptPath}";

    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;

    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);

    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 

    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);

    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);

    // Make the request         
    $response = curl_exec($handle);  

    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      echo "<br>";      
      echo "Response Header:".$response_headers;
      echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
        echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  //Use the response data
        print_r($parsed_json);
      }
    }

    // Close the curl session  
    curl_close($handle);

    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
	}
	
	
function putSkillsListManagementSkillId($skillId)
{
     if($_SESSION["access_token"]!="")
   {
    $data = array(
	"displayField1Name"=> "",
	"displayField2Name": => "",
	'listOrderingOptions'=array(
      "orderType"=> "",
      "direction"=> ""
    )
  );
    //create post json data
    $data_json = json_encode($data);	
    $api_URL='/services/v18.0/skills/'.$skillId.'/parameters/list-management';
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);      
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
     	//echo $parsed_json;
		ConstructArray('Admin','Skills','PUT /skills/{skillId}/parameters/list-management','v18.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Skills','PUT /skills/{skillId}/parameters/list-management','v18.0','No access Token');
}

function getSkillsParameters()
{
 //The following code sample assumes that the access token is stored in the session
  session_start();
  //Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {

    $api_URL="services/{version}/skills/parameters";

    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;

    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);

    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 

    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);

    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);

    // Make the request         
    $response = curl_exec($handle);  

    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      echo "<br>";      
      echo "Response Header:".$response_headers;
      echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
        echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  //Use the response data
        print_r($parsed_json);
      }
    }

    // Close the curl session  
    curl_close($handle);

    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
	}


function putSkillsCadenceSettingsSkillId($skillId)
{
     if($_SESSION["access_token"]!="")
   {
    $data = array(
	"attemptMode"=> "string",
	"recordRequestMode"=> "string",
	"destinationRetryRestMinutes"=> 0,
	"maximumAttempts"=> array(
    "fieldName"=> "string",
      "attempts"=> 0
	  ),
    "cadences" => array(
	"fieldName"=> "string",
      "attempts"=> 0,
      "timeConstraints": => array(
       "weekdayTimeConstraints"=> array(
			"startTime"=> "string",
            "endTime"=> "string"
	  ),
	  "weekendTimeConstraints"=> array(
            "startTime"=> "string",
            "endTime"=> "string"
			)
	)
	)
  );

    //create post json data
    $data_json = json_encode($data);	
    $api_URL='/services/v18.0/skills/'.$skillId.'/parameters/cadence-settings';
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);      
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
     	//echo $parsed_json;
		ConstructArray('Admin','Skills','PUT /skills/{skillId}/parameters/cadence-settings','v18.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Skills','PUT /skills/{skillId}/parameters/cadence-settings','v18.0','No access Token');
}	
	
function getSkillsSkillIdParameters($skillId)
{
 //The following code sample assumes that the access token is stored in the session
  session_start();
  //Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {

    $api_URL='services/{version}/skills/'.$skillId.'/parameters';

    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;

    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);

    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 

    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);

    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);

    // Make the request         
    $response = curl_exec($handle);  

    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      echo "<br>";      
      echo "Response Header:".$response_headers;
      echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
        echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  //Use the response data
        print_r($parsed_json);
      }
    }

    // Close the curl session  
    curl_close($handle);

    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
	}
	
function postSetDisposition($contactId){
if($_SESSION["access_token"]!="")
  {
     $data = array(
	"dispositionInfo"= array(
    "Skill"=> "1007",
    "DispositionCode"=> "1",
    "CallbackNumber"=> "",
    "CallbackTime"=> "",
    "notes"=> "string",
    "CommitmentAmount"=> "string"
  ));
    $data_json = json_encode($data);
    $api_URL='/services/v18/contacts/'.$contactId.'/set-disposition';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($ch, CURLOPT_POST, 1);
    curl_setopt($ch, CURLOPT_POSTFIELDS,data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
		ConstructArray('Admin','Contacts','Post /set-disposition','v18.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
     else
       //echo else block;
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    ConstructArray('Admin','Contacts','Post /set-disposition','v18.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
}
	
function getBusinessUnitOutboundRoutes(){
 //The following code sample assumes that the access token is stored in the session
  session_start();
  //Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {

    $api_URL='services/{version}/business-unit/outbound-routes';

    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;

    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);

    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 

    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);

    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);

    // Make the request         
    $response = curl_exec($handle);  

    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      echo "<br>";      
      echo "Response Header:".$response_headers;
      echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
        echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  //Use the response data
        print_r($parsed_json);
      }
    }

    // Close the curl session  
    curl_close($handle);

    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
	}

//v19
	
function getclientdata(){
 //The following code sample assumes that the access token is stored in the session
  session_start();
  //Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {

    $api_URL='services/{version}/agents/client-data';

    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;

    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);

    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 

    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);

    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);

    // Make the request         
    $response = curl_exec($handle);  

    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      echo "<br>";      
      echo "Response Header:".$response_headers;
      echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
        echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  //Use the response data
        print_r($parsed_json);
      }
    }

    // Close the curl session  
    curl_close($handle);

    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
	}
	
	function putclientdata()
{
     if($_SESSION["access_token"]!="")
   {
    $data = array(
  "agentId"=> 1001,
  "dataSet"=> array(
  "settings"=> "true" )
  );

    //create post json data
    $data_json = json_encode($data);	
    $api_URL='/services/v18.0/skills/agents/client-data';
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);      
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
     	//echo $parsed_json;
		ConstructArray('Realtime','Realtime','PUT /agents/client-data','v19.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Realtime','Realtime','PUT /agents/client-data','v19.0','No access Token');
}	
	
function postsmsoutbound($sessionId){
if($_SESSION["access_token"]!="")
  {
     $data = array(
	"dispositionInfo"= array(
    "Skill"=> "1007",
    "DispositionCode"=> "1",
    "CallbackNumber"=> "",
    "CallbackTime"=> "",
    "notes"=> "string",
    "CommitmentAmount"=> "string"
  ));
    $data_json = json_encode($data);
    $api_URL='/services/v18/agent-sessions/'. $sessionId .'/interactions/sms-outbound';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($ch, CURLOPT_POST, 1);
    curl_setopt($ch, CURLOPT_POSTFIELDS,data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
	  ConstructArray('Agent','Sessions','Post /agent-sessions/{sessionId}/interactions/sms-outbound','v19.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
     else
       //echo else block;
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    ConstructArray('Agent','Sessions','Post /agent-sessions/{sessionId}/interactions/sms-outbound','v19.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
}
	
	function removeprospects($sourceName)
{
	// session_start();
  //Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {
	  //print_r($data_json);
	  //echo 'input data'.$data_json;
    //addressBookId is a required field
    $api_URL='/services/v19.0/lists/call-lists/'.$sourceName.'/removeProspects';
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
 
    //Creating a HTTP DELETE request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true); 
    curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "DELETE");
	curl_setopt($handle, CURLOPT_POSTFIELDS, $data_json);
 
    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
 
    //By default, cURL is setup to not trust any CAs 
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security 
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=UTF-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
     // echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);   
      echo "<br>";      
      //echo "Response Header:".$response_headers;
      echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
       // echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  
 
        //Use the response data
       // print_r($parsed_json);
		ConstructArray('Admin','Skills','Delete /lists/call-lists/{sourceName}/removeProspects','v19.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
	
	function putpersistentcontacts($contactId)
{
     if($_SESSION["access_token"]!="")
   {
    $data = array(
  "persistentContact"=> array(
    "SkillId"=> "",
    "TargetAgentId"=> "",
    "InitialPriority"=> "",
    "Acceleration"=> "",
    "MaxPriority"=> ""
  ));

    //create post json data
    $data_json = json_encode($data);	
    $api_URL='/services/v19.0/persistent-contacts/'. contactId;
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
	curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript,*/*; q=0.01';
	curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($handle, CURLOPT_POST, 1);
	curl_setopt($handle, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);      
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
       if (!empty($response_body))
      {
        $parsed_json=json_decode($response_body);  
     	//echo $parsed_json;
	  ConstructArray('Admin','Contacts','PUT /persistent-contacts/{contactId}','v19.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    //echo "No access Token";
	ConstructArray('Admin','Contacts','PUT /persistent-contacts/{contactId}','v19.0','No access Token');
}	
	
  function getagentsettings($agentId){
 //The following code sample assumes that the access token is stored in the session
  session_start();
  //Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {

    $api_URL='services/{version}/agents/' . $agentId . '/agent-settings'';

    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;

    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);

    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 

    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);

    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);

    // Make the request         
    $response = curl_exec($handle);  

    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      echo "<br>";      
      echo "Response Header:".$response_headers;
      echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
        echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  //Use the response data
        print_r($parsed_json);
      }
    }

    // Close the curl session  
    curl_close($handle);

    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
	}
	
function getscriptsbyId($scriptId){
 //The following code sample assumes that the access token is stored in the session
  session_start();
  //Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {

    $api_URL='services/{version}/scripts/' . $scriptId;

    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;

    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);

    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 

    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);

    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);

    // Make the request         
    $response = curl_exec($handle);  

    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      echo "<br>";      
      echo "Response Header:".$response_headers;
      echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
        echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  //Use the response data
        print_r($parsed_json);
      }
    }

    // Close the curl session  
    curl_close($handle);

    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
	}
	
	function getscriptssearch(){
 //The following code sample assumes that the access token is stored in the session
  session_start();
  //Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {

    $api_URL='services/{version}/scripts/search';

    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;

    //Creating a HTTP GET request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);

    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 

    //By default, cURL is setup to not trust any CAs
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);

    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=utf-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);

    // Make the request         
    $response = curl_exec($handle);  

    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      echo "<br>";      
      echo "Response Header:".$response_headers;
      echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
        echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  //Use the response data
        print_r($parsed_json);
      }
    }

    // Close the curl session  
    curl_close($handle);

    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
	}
	
	function deletescripts()
{
	// session_start();
  //Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {
	  //print_r($data_json);
	  //echo 'input data'.$data_json;
    //addressBookId is a required field
    $api_URL='/services/v19.0/scripts';
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
 
    //Creating a HTTP DELETE request to the api
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true); 
    curl_setopt($handle, CURLOPT_CUSTOMREQUEST, "DELETE");
	curl_setopt($handle, CURLOPT_POSTFIELDS, $data_json);
 
    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
 
    //By default, cURL is setup to not trust any CAs 
    //Hence does not support any server's certificate
    //Here we configure cURL to accept any server(peer) certificate
    //Does not verify if the CA is trusted
    //For more security 
    //Set the CURLOPT_CAINFO parameter to a CA certificate that cURL should trust
    //curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, true);
    //curl_setopt($handle, CURLOPT_SSL_VERIFYHOST, 2);
    //curl_setopt($handle, CURLOPT_CAINFO, getcwd()."/cert/certificate.crt");
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
 
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-type: application/x-www-form-urlencoded; charset=UTF-8';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
 
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
     // echo "Status Code of the Response:".$http_code;
	
      //Parsing the response
      //Each property in header is a line by itself.
      //The header and the web page content sent together are separated by \r\n\r\n
      $parts = explode("\r\n\r\nHTTP/", $response);
      //For HTTP status code 100 interim responses exists before the final response
      //Getting the final response header using array_pop
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2);   
      echo "<br>";      
      //echo "Response Header:".$response_headers;
      echo "<br>";
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        //Parsing the json response
       // echo "Response Body:";  
        //json_decode converts a json string to a PHP variable
        $parsed_json=json_decode($response_body);  
 
        //Use the response data
       // print_r($parsed_json);
		ConstructArray('Admin','Skills','Delete /scripts','v19.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
    }
 
    // Close the curl session  
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    echo "No access Token";
}
	
	
function posttransformnumbers($agentpatternId){
if($_SESSION["access_token"]!="")
  {
     $data = array(
	"inputPhoneNumbers"= array(
    "inputPhoneNum"=> "",
    "externalId"=> ""));
    $data_json = json_encode($data);
    $api_URL='/services/v19/agent-patterns/' . $agentpatternId . '/transform-phonenumbers';
    //Creating the endpoint for the request
    $endpoint=$_SESSION["resource_server_base_uri"].$api_URL;
    $handle = curl_init($endpoint);
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, true); 
    curl_setopt($handle, CURLOPT_FOLLOWLOCATION, true);
    curl_setopt($handle, CURLOPT_SSL_VERIFYPEER, false);
    
    $headers=array();
    $headers[] = 'Content-type: Content-Type: application/json';
    $headers[] = 'Authorization: bearer '.$_SESSION["access_token"];
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    curl_setopt($ch, CURLOPT_POST, 1);
    curl_setopt($ch, CURLOPT_POSTFIELDS,data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
      $http_code = curl_getinfo($handle, CURLINFO_HTTP_CODE);
      $parts = explode("\r\n\r\nHTTP/", $response);
      $parts = (count($parts) > 1 ? 'HTTP/' : '').array_pop($parts);
      list($response_headers, $response_body) = explode("\r\n\r\n", $parts, 2); 
      if (!empty($response_body))
      {
        //The Response from the api is in JSON
        $parsed_json=json_decode($response_body);  
	  ConstructArray('Agent','Sessions','Post /agent-patterns/{agentpatternId}/transform-phonenumbers','v19.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
      }
     else
       //echo else block;
    }
    curl_close($handle);
 
    //Report all PHP errors (notices, errors, warnings, etc.)  
    error_reporting(E_ALL);
  }
  else
    // No token - get one or handle error
    ConstructArray('Agent','Sessions','Post /agent-patterns/{agentpatternId}/transform-phonenumbers','v19.0',curl_getinfo($handle, CURLINFO_HTTP_CODE) );
}
	
?>