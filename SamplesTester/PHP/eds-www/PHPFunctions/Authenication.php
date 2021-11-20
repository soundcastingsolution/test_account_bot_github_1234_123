<?php
  //Each of the following attributes are necessary for the POST request
  //Check if the values are passed  
      
 
  //The following code sample assumes that the access token is stored in the session
 // session_start();
  function Authentication($username='k.karishma@servion.com', $password='Welcome@123', $grant_type = 'password',$format = 'json')
  {
  $entries=array(
            'username'=>$username,
            'password'=>$password,
			'grant_type'=>$grant_type,
			);
			$data_json = json_encode($entries);
  //Check if you have obtained a token
  if($_SESSION["access_token"]!="")
  {
    //address_book_id is a required field
    $api_URL="InContactAuthorizationServer/Token";
    //Creating the endpoint for the request
    //Appending api uri with the base uri obtained from the successful token request
    $endpoint="https://api-so32.staging.nice-incontact.com/".$api_URL;
 
    //Creating HTTP POST request to the resource
    $handle = curl_init($endpoint);
	
    curl_setopt($handle, CURLOPT_HEADER, true);
    curl_setopt($handle, CURLOPT_POST, true);
 
    //Set to TRUE to return the output of curl_exec as a string 
    curl_setopt($handle, CURLOPT_RETURNTRANSFER, false); 
 
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
    //The post data for an address book entry is not JSON 
    //It is actually a large HTML String. 
    //To be taken into consideration when looping through multiple address book entries.
    //Continue to add to the string with "&addressBookEntries[i]" to keep adding
    //This example is hard coded for one full entry.
    
    curl_setopt($handle, CURLOPT_POSTFIELDS, $data_json); 
 
 
    //Setting necessary header options
    $headers=array();
    $headers[] = 'Content-Type: application/json';
    $headers[] = 'Accept: application/json, text/javascript, */*; q=0.01';
    $headers[] = 'Authorization: basic'.'aW50ZXJuYWxAaW5Db250YWN0IEluYy46UVVFNVFrTkdSRE0zUWpFME5FUkRSamczUlVORFJVTkRRakU0TlRrek5UYz0=';
    $headers[]='Location : https://api-so32.staging.nice-incontact.com/';
   curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS,$data_json);
    curl_setopt($handle, CURLOPT_HTTPHEADER, $headers);
   
 
    // Make the request         
    $response = curl_exec($handle);  
 
    //Handling valid response
    if($response!=FALSE)
    {
		echo(response);
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
	}
	}