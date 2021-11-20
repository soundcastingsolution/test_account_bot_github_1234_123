<?php

// base URL
$_SESSION["resource_server_base_uri"]='https://api-so32.staging.nice-incontact.com/incontactapi';

$_SESSION["CXone_resource_server_base_uri"]='https://na1.staging.nice-incontact.com/';
//access_token

$_SESSION["access_token"]='eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpY0JVSWQiOjE0MTAxNCwibmFtZSI6Im5pa2hpbC5tQHNjMS5jb20iLCJpc3MiOiJodHRwczovL2FwaS5pbmNvbnRhY3QuY29tIiwic3ViIjoidXNlcjoyMDc5MzIiLCJhdWQiOiJpbnRlcm5hbEBpbkNvbnRhY3QgSW5jLiIsImV4cCI6MTUyNTk1NzY4OSwiaWF0IjoxNTI1OTU0MDkwLCJpY1Njb3BlIjoiMSwyLDQsNSw3LDgiLCJpY0NsdXN0ZXJJZCI6IlNDMSIsImljQWdlbnRJZCI6MjA3OTMyLCJpY1NQSWQiOjUyNzQsIm5iZiI6MTUyNTk1NDA4OX0.WZc58OCDFzxABFN9LePzoD-QUwlhOWkskt4AHF2PJxsqYhs6EnE9JfSKlYrGiJGKVocS2XLo7vY8-_jsN0fKySxwYEY6VarPPWqzZ91CXJgwNHijtAY7w_HkxON6pJFoLebsHgiqW4a0X-uGvNoANYcrdRj0MNkAFIAJ6Dba5ZHqLtUvODyaCAt03ba0i6u3_T-8hPYI4yOHmFwjcMbd6WUiwox0sT_8RbRfxcTqvlm7xj_zU5Zu3dj3_s8z3RQQ_SMXhspGFBEdok-rojPsqO8DNDlO4TOJlc0-RMp_tAUmd9QJAGX63zXciqV26K1X8VD9ugTE4LhHxj9c_WCk3g';

// header to construct output
$_SESSION["Header"] = "<table border = '1' id ='customers'>
	<tr>
	<th>APIName</th>
	<th>APISubType</th>
    <th>APIType</th>
    <th>Version</th>
    <th>APIResult</th>
    </tr>";

//result to display output
$_SESSION["Result"] = array();

$_SESSION["Result"] = $_SESSION["Header"];

?>