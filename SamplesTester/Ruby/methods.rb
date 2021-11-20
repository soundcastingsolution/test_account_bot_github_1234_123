require 'net/http'
require 'json'
require 'uri'
require 'parseconfig'

$contactId = '8882032'
$agentId = '27730'
$skillId = '1268'
$teamId = '1138'
$groupId = '74'
$accessKeyId='4FE4QQJVVDVJZWQ7NGBE4YF35O7DEHRJ74XPPQNQ3MYYKFNHIYBA===='
$contactId='21bf4a34-f768-45d4-aed6-6bd754dd49d2'
$segmentId='b171d215-a152-4572-bd45-56670cb7a859'
$acdId='master id 1'
$campaignId='8913'
$profileId='4'
$pointOfContactId='1234'
$unavailablecodeId='3403'
$workflowDataId='56765'
$jobID='12345'
$suppressedContactId='12345'
$accessToken
# $baseURI='http://hc-c8web01.in.lab/InContactAPI/'
$fileHtml = File.new("result.html", "w+")

my_config = ParseConfig.new('data.cfg')
$username=my_config['key1']
$password=my_config['key2']
$baseURI=my_config['key3']
$tokenServiceUri=my_config['key4']
$phoneNumber=my_config['key5']
$CXone_baseURI=my_config['key6']

def init()

$fileHtml.puts "<HTML><head><link rel='stylesheet' type='text/css' href='table.css'></head>"
$fileHtml.puts "<body class='rTable'>"
$fileHtml.puts "<h2>API Test Results For Ruby Functions </h2>"
$fileHtml.puts "<title>Ruby</title>"
$fileHtml.puts "<div class='rTableHead small'><strong>APIName</strong></div>"
$fileHtml.puts "<div class='rTableHead small'><span style='font-weight: bold;'>APISubType</span></div>"
$fileHtml.puts "<div class='rTableHead med'><span style='font-weight: bold;'>APIType</span></div>"
$fileHtml.puts "<div class='rTableHead small'><span style='font-weight: bold;'>Version</span></div>"
$fileHtml.puts "<div class='rTableHead'><span style='font-weight: bold;'>APIResult</span></div>"
$fileHtml.puts "</body>"
$fileHtml.puts"</HTML>"

end


def construct(apiName,apiSubType,apiType,version,status,description)
$fileHtml.puts "<div class='rTableRow'>"
$fileHtml.puts "<div class='rTableCell small'>"+apiName+"</div>"
$fileHtml.puts "<div class='rTableCell small'>"+apiSubType+"</div>"
$fileHtml.puts "<div class='rTableCell med'>"+apiType+"</div>"
$fileHtml.puts "<div class='rTableCell small'>"+version+"</div>"
$fileHtml.puts "<div class='rTableCell'>"+status+":"+description+"</div>"
$fileHtml.puts "</div>"
end

def getaccesstoken()
uri = URI.parse($tokenServiceUri)

data={"grant_type"=> "password",
          "username" => $username,
        "password"=> $password}
         params=data.to_json

headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "basic " + "aW50ZXJuYWxAaW5Db250YWN0IEluYy46UVVFNVFrTkdSRE0zUWpFME5FUkRSamczUlVORFJVTkRRakU0TlRrek5UYz0=",
      "Content-Type" => "application/json ;charset=utf-8",
      "Data-Type" => "json",
	  "Content-Length" => "10",
	  "Accept-Encoding" => "identity"}
		
		  #Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  #Make the request.
http.use_ssl = true
  res = http.post(uri.path,params,headers)
  $accessToken=JSON.parse(res.body)['access_token']
  puts $accessToken
end

def getAgents(updatedSince,isActive,searchString,fields,skip,top,orderBy)
if($accessToken!='')
uri = URI.parse(($baseURI) +'/services/v13.0/agents'+'?updatedSince='+updatedSince+'&isActive='+isActive+ '&searchString='+searchString+'&fields='+fields+'&skip'+ skip+'&top='+ top+'&orderBy='+orderBy)
 
  # Create the GET request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json",
		"Accept-Encoding" => "deflate"}
	  
	
 
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # Make the request.
  http.use_ssl = true
  res = http.get(uri.path , headers)
construct('Admin','Agents','GET /agents','v13.0',res.code,res.message)
  # puts res.code
	# puts res.message
else
construct('Admin','Agents','GET /agents','v13.0',res.code,res.message)
end
end

def PostAgent()
if($accessToken!='')
uri = URI.parse('https://hc8.ucnlabext.com/InContactAPI/services/v16.0/agents')
 
 
    body ={
        "agents": [
          {
              "firstName"  => "Firstso32" ,
			  "middleName"=> "Midso32",
              "lastName"=> "Lastso32",
              "teamId"=> "",
              "reportToId"=> 1,
              "internalId"=> "",
              "profileId"=> 0,
              "roleId"=> "",
              "password"=> "",
              "emailAddress"=> "",
              "userName"=> "",
              "userId"=> "",
              "timeZone"=> "(GMT-07=>00) Arizona",
              "country"=> "",
              "state"=> "",
              "city"=> "",
			  "isBillable"=>true
          }
        ]
   }
   params=body.to_json()
   # paramLength = params.bytesize
   
   
  # Create the Post request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "gzip,deflate"}
	  

  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  
  
  http.use_ssl= true

  res = http.post(uri.path, params,headers)

  construct('Admin','Agents','POST /agents','v16.0',res.code,res.message)
else
construct('Admin','Agents','POST /agents','v16.0',res.code,res.message)
    end
end

def GetagentbyAgentIDV13(fields)
if($accessToken!='')
uri = URI.parse(($baseURI)+'/services/v13.0/agents/'+($agentId)+'?fields='+fields)
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json, charset=UTF-8",
      "Data-Type" => "json",
		"Accept-Encoding" => "gzip,deflate"}
 
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  res = http.get(uri.path , headers)

  construct('Admin','Agents','GET /agents/{agentId}','v13.0',res.code,res.message)
else
construct('Admin','Agents','GET /agents/{agentId}','v13.0',res.code,res.message)
    end
	end
	
def PostAgentByID()
if($accessToken!='')
uri = URI.parse(($baseURI)+'/services/v16.0/agents/'+($agentId))
 
 
    body ={
        "agents": [
          {
              "firstName"  => "Firstso32" ,
			  "middleName"=> "Midso32",
              "lastName"=> "Lastso32",
              "teamId"=> "",
              "reportToId"=> 1,
              "internalId"=> "",
              "profileId"=> 0,
              "roleId"=> "",
              "password"=> "",
              "emailAddress"=> "",
              "userName"=> "",
              "userId"=> "",
              "timeZone"=> "(GMT-07=>00) Arizona",
              "country"=> "",
              "state"=> "",
              "city"=> "",
			  "isBillable"=>true
          }
        ]
   }
   params=body.to_json()
   # paramLength = params.bytesize
   
   
  # Create the Post request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "gzip,deflate"}
	  

  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  
  
  http.use_ssl= true

  res = http.post(uri.path, params,headers)

  construct('Admin','Agents','PUT/agents​/{agentId|userId}','v16.0',res.code,res.message)
else
construct('Admin','Agents','PUT/agents​/{agentId|userId}','v16.0',res.code,res.message)
    end
end

	
def getTeams(fields,updatedSince,isActive,searchString,skip,top,orderBy)
if($accessToken!='')
uri = URI.parse(($baseURI) +'/services/v13.0/teams'+'?fields='+fields+'&updatedSince='+updatedSince+'&isActive='+isActive+ '&searchString='+searchString+'&skip'+ skip+'&top='+ top+'&orderBy='+orderBy)

  # Create the GET request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json",
		"Accept-Encoding" => "gzip,deflate"}
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  res = http.get(uri.path , headers)

  construct('Admin','Agents','GET /teams','v13.0',res.code,res.message)
  else
  construct('Admin','Agents','GET /teams','v13.0',res.code,res.message)
	
end
    end
	
def postTeam()
if($accessToken!='')
uri= URI.parse(($baseURI)+'/services/v13.0/teams')
    body =
	{"teams": [
          {
              "teamName"=> "team"
          }
        ]
    }
   
   params=body.to_json()
   # paramLength = params.bytesize

  # Create the Post request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "gzip,deflate"}
	  

  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  
  
  #http.use_ssl= true

  res = http.post(uri.path, params,headers)

  construct('Admin','Agents','POST /teams','v13.0',res.code,res.message)
  else
  construct('Admin','Agents','POST /teams','v13.0',res.code,res.message)
end
    end

	
def getTeamById(fields) 
if($accessToken!='')
uri= URI.parse(($baseURI)+'/services/v13.0/teams/'+($teamId)+'?fields=' + fields)

headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json, charset=UTF-8",
      "Data-Type" => "json",
		"Accept-Encoding" => "gzip,deflate"}
 
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  res = http.get(uri.path , headers)

 construct('Admin','Agents','GET /team/{teamId}','v13.0',res.code,res.message)
else
construct('Admin','Agents','GET /team/{teamId}','v13.0',res.code,res.message)
    end
end
#-----------------------------------------------------

def PutAgentbyAgentIDV13()
if($accessToken!='')
uri = URI.parse(($baseURI)+'/services/v13.0/agents/' + ($agentId))
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "gzip,deflate"}
		
		body = {
        "agents": [
          {
              "agentId"=> 1140,
              "userName"=> "vijiesh@hc8.com",
              "firstName"=> "viji",
              "middleName"=> "",
              "lastName"=> "hc8.com",
              "userId"=> '',
              "emailAddress"=> "vijayalakshmi.s@servion.com",
              "isActive"=> true,
              "teamId"=> 11079,
              "teamName"=> "Testadmin",
              "reportToId" =>0,
              "reportToName"=> "",
              "isSupervisor"=> true,
              "lastLogin"=> "2017-02-28T09:22:38.430Z",
              "lastUpdated"=> "2017-04-05T10:32:22.583Z",
              "location"=> '',
              "custom1"=> "",
              "custom2"=> "",
              "custom3"=> "",
              "custom4"=>"",
              "custom5"=> "",
              "internalId"=> "",
              "profileId"=> 41,
              "profileName"=> "vgsecurity profile_1",
              "timeZone"=>"(GMT+05:30) Chennai, Kolkata, Mumbai, New Delhi",
              "country"=> "US",
              "countryName"=> "United States",
              "state"=> "Alabama",
              "city"=> "Chennai",
              "chatRefusalTimeout"=> '',
              "phoneRefusalTimeout"=> '',
              "workItemRefusalTimeout"=> '',
              "defaultDialingPattern"=> '',
              "defaultDialingPatternName"=> '',
              "useTeamMaxConcurrentChats"=>false,
              "maxConcurrentChats"=> 3,
              "notes"=> "",
              "createDate"=> "2015-06-08T07:28:02.140Z",
              "inactiveDate"=> '',
              "hireDate"=> '',
              "terminationDate"=>'',
              "hourlyCost"=>0,
              "rehireStatus"=> true,
              "employmentType"=> '',
              "employmentTypeName"=> "",
              "referral"=> "",
              "atHome"=> false,
              "hiringSource"=> "",
              "ntLoginName"=> "",
              "scheduleNotification"=>15,
              "federatedId"=> '',
              "useTeamEmailAutoParkingLimit"=>true,
              "maxEmailAutoParkingLimit"=> 3,
              "sipUser"=> '',
              "systemUser"=> '',
              "systemDomain"=> '',
              "crmUserName"=> '',
              "useAgentTimeZone"=> false,
              "timeDisplayFormat"=> "12 hour",
              "sendEmailNotifications"=> false,
              "apiKey"=> '',
              "telephone1"=> "",
              "telephone2"=> "",
              "userType"=> "Administrator",
              "isWhatIfAgent"=> false,
              "timeZoneOffset"=> "05:30",
              "requestContact"=> false,
              "chatThreshold"=> 0,
              "useTeamChatThreshold"=> false,
              "emailThreshold"=> 0,
              "useTeamEmailThreshold"=> false,
              "workItemThreshold"=> 0,
              "useTeamWorkItemThreshold"=> false,
              "contactAutoFocus"=> false,
              "useTeamContactAutoFocus"=> false,
              "useTeamRequestContact"=> false,
              "voiceThreshold"=> 0,
              "recordingNumbers"=>[],
              "subject"=> '',
              "issuer"=> '',
              "isOpenIdProfileComplete"=> false,
              "teamUuid"=> ''
          }
        ]
    }
	
	params= body.to_json
 
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  res = http.put(uri.path , params, headers)

   construct('Admin','Agents','PUT /agents/{agentId}','so32-v13.0',res.code,res.message)
else
construct('Admin','Agents','PUT /agents/{agentId}','so32-v13.0',res.code,res.message)
    end
end

#------------------------

def PutTeambyTeamIDV13()
if($accessToken!='')
uri = URI.parse(($baseURI)+'/services/v13.0/teams/'+($teamId))
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "deflate"}
		
		body = {
        "forceInactive"=> false,
        "team": {
            "teamName"=> "TeamName",
            "isActive"=> true,
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
        }
    }
	
	params= body.to_json
 # Set up an HTTP object.
  http = Net::HTTP.new uri.host,uri.port
  # puts headers
  # Make the request.
  res = http.put(uri.path , params, headers)

   construct('Admin','Agents','PUT /teams/{teamId}','so32-v13.0',res.code,res.message)
else
construct('Admin','Agents','PUT /teams/{teamId}','so32-v13.0',res.code,res.message)
    end
end
#-------------------------
def getUnavailableCodesByTeam(activeOnly)
if($accessToken!='')
uri= URI.parse(($baseURI)+'/services/v13.0/teams/'+($teamId)+'/unavailable-codes'+'?activeOnly='+activeOnly)

headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json; charset=UTF-8",
      "Data-Type" => "json",
		"Accept-Encoding" => "gzip,deflate"}
 
	
	# Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  res = http.get(uri.path , headers)

 construct('Admin','Agents','GET /teams/{teamId}/unavailable-codes','so32-v13.0',res.code,res.message)
else
construct('Admin','Agents','GET /teams/{teamId}/unavailable-codes','so32-v13.0',res.code,res.message)
    end
end
#----------------------------------------
def PutUnavailableCodesbyTeamID()
if($accessToken!='')
uri = URI.parse(($baseURI)+'/services/v13.0/teams/'+ $teamId +'/unavailable-codes')
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "gzip,deflate"}
		
		body ={
        "unavailableCodes"=> [
          {
              "outStateId"=> "0"
          }
        ]
    }
	params= body.to_json
 # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  res = http.put(uri.path , params, headers)

   construct('Admin','Agents','PUT /teams/{teamId}/unavailable-codes','so32-v13.0',res.code,res.message)
else
construct('Admin','Agents','PUT /teams/{teamId}/unavailable-codes','so32-v13.0',res.code,res.message)
    end
end	
#------------------------
def getUnavailableCodes()
if($accessToken!='')
uri= URI.parse(($baseURI)+'/services/v13.0/unavailable-codes')
 headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json, charset=UTF-8",
	  "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "gzip,deflate"}
	  
	# Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  res = http.get(uri.path , headers)

   construct('Admin','Agents','GET /unavailable-codes','so32-v13.0',res.code,res.message)
else
construct('Admin','Agents','GET /unavailable-codes','so32-v13.0',res.code,res.message)
    end
end	  
#------------
def getSkills(updatedSince,mediaTypeId,outboundStrategy,isActive,searchString,fields,skip,top,orderBy)
if($accessToken!='')
uri= URI.parse(($baseURI)+'/services/v13.0/skills'+'?updatedSince='+updatedSince+'&isActive='+isActive+ '&searchString='+searchString+'&fields='+fields+'&skip'+ skip+'&top='+ top+'&orderBy='+orderBy+'&mediaTypeId='+mediaTypeId+'&outboundStrategy='+outboundStrategy)

headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json; charset=UTF-8",
      "Data-Type" => "json",
		"Accept-Encoding" => "gzip,deflate"}
 
	# Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  res = http.get(uri.path ,headers)

   construct('Admin','Skills','GET /skills','so32-v13.0',res.code,res.message)
else
construct('Admin','Skills','GET /skills','so32-v13.0',res.code,res.message)
    end
end	  
#-------------------
def PostCreateSkillV13()
if($accessToken!='')
uri= URI.parse(($baseURI)+'/services/v13.0/skills')

headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "gzip,deflate"}
 
body =
    {"skills"=> [
          {
              "mediaTypeId"=> 4,
              "skillName"=> "",
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
              "screenPopDetails"=> "http=>//not",
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
              "smsTransportCodeId"=> '',
              "messageTemplateId"=> '',
              "deliverMultipleNumbersSerially"=> false,
              "cradleToGrave"=> false,
              "priorityInterrupt"=> false,
              "treatProgressAsRinging"=> false,
              "preConnectCPAEnabled"=> false,
              "agentOverrideFax"=> true,
              "agentOverrideAnsweringMachine"=> true,
              "agentOverrideBadNumber"=> true,
              "dispositions"=> []
}
        ]
    }
params= body.to_json
	# Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  res = http.post(uri.path ,params,  headers)

   construct('Admin','Skills','POST /skills','so32-v13.0',res.code,res.message)
else
construct('Admin','Skills','POST /skills','so32-v13.0',res.code,res.message)
    end
end	  
#-----------------	
def getSkillById()
if($accessToken!='')
uri= URI.parse(($baseURI)+'/services/v13.0/skills/'+($skillId))

headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json",
		"Accept-Encoding" => "gzip,deflate"}
	# Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  res = http.get(uri.path ,  headers)

   construct('Admin','Skills','GET /skills/{skillId}','so32-v13.0',res.code,res.message)
else
construct('Admin','Skills','GET /skills/{skillId}','so32-v13.0',res.code,res.message)
    end
end	  	
#----------------
def PutSkillbySkillID()
if($accessToken!='')
uri= URI.parse(($baseURI)+'/services/v13.0/skills/'+($skillId))

headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "gzip,deflate"}
body ={
        "skill"=> {
            "skillName"=> "string",
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
            "smsTransportCodeId"=> '',
            "messageTemplateId"=> '',
            "deliverMultipleNumbersSerially"=> false,
            "cradleToGrave"=> false,
            "priorityInterrupt"=> false,
            "treatProgressAsRinging"=> false,
            "preConnectCPAEnabled"=> false,
            "agentOverrideFax"=> true,
            "agentOverrideAnsweringMachine"=> true,
            "agentOverrideBadNumber"=> true,
            "dispositions"=> [
              {
                  "dispositionId"=> 1,
                  "priority"=> 1
              }
            ]
        }
    }		
		
		params= body.to_json
			# Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  res = http.put(uri.path , params, headers)

   construct('Admin','Skills','PUT /skills/{skillId}','so32-v13.0',res.code,res.message)
else
construct('Admin','Skills','PUT /skills/{skillId}','so32-v13.0',res.code,res.message)
    end
end	  	
#----------------
def GetSkillParameterGeneralSettingV13(fields)
if($accessToken!='')
uri= URI.parse(($baseURI)+'/services/v13.0/skills/'+($skillId)+'/parameters/general-settings'+'?fields='+fields)

headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json",
		"Accept-Encoding" => "gzip,deflate"}
	# Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  res = http.get(uri.path ,  headers)

   construct('Admin','Skills','GET parameters/general-settings','so32-v13.0',res.code,res.message)
else
construct('Admin','Skills','GET parameters/general-settings','so32-v13.0',res.code,res.message)
    end
end	 
#------------------
def PutSkillParameterGeneralSettingv13()
 	if($accessToken!='')
uri= URI.parse(($baseURI)+'/services/v13.0/skills/'+($skillId)+'/parameters/general-settings')

headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "gzip,deflate"}
		body={
        "generalSettings"=> {
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
            "abandonRateThreshold"=> 1,
            "inactiveBlenderTimer"=> 1,
            "maximumRatio"=> 1,
            "aggressiveness"=> "conservative",
            "endOfListNotificationsDelay"=> 15,
            "notifyAgentsWhenListIsEmpty"=> false,
            "percentageOfAgentsBeforeOverdial"=> 5,
            "blockMultipleCalls"=> true,
            "consecutiveAttemptsWithoutALiveConnect"=> 5
        }
    }
	params= body.to_json
			# Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  res = http.put(uri.path , params, headers)

   construct('Admin','Skills','PUT /skills/{skillId}/parameters/general-settings','so32-v13.0',res.code,res.message)
else
construct('Admin','Skills','PUT /skills/{skillId}/parameters/general-settings','so32-v13.0',res.code,res.message)
    end
end	  	
#-------------------------------------
def getSkillCpamanagement(fields)
if($accessToken!='')
uri= URI.parse(($baseURI)+'/services/v13.0/skills/'+($skillId)+'/parameters/cpa-management'+'?fields='+fields)

headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json",
		"Accept-Encoding" => "gzip,deflate"}
	# Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  res = http.get(uri.path ,  headers)

   construct('Admin','Skills','GET parameters/cpa-management','so32-v13.0',res.code,res.message)
else
construct('Admin','Skills','GET parameters/cpa-management','so32-v13.0',res.code,res.message)
    end
end	 
#-------------------------------------
def PutSkillParameterCpamanagement()
if($accessToken!='')
uri= URI.parse(($baseURI)+'/services/v16.0/skills/'+($skillId)+'/parameters/cpa-management')

headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "gzip,deflate"}
	body={
        "cpaSettings"=> {
            "abandonMessagePath"=> "string",
            "abandonMsgMode"=> 0,
            "abandonTimeout"=> 0,
            "agentNoResponseSeconds"=> 0,
            "agentOverrideOptionAnsweringMachine"=> true,
            "agentOverrideOptionBadNumber"=> true,
            "agentOverrideOptionFax"=> true,
            "agentResponseUtteranceMinimumSeconds"=> 0,
            "agentVoiceThreshold"=> 0,
            "ansMachineDetMode"=> 0,
			"ansMachineOverrideSeconds"=>0,
            "ansMachineMsg"=> "string",
            "customerLiveSilenceSeconds"=> 0,
            "customerVoiceThreshold"=> 0,
            "enableCPALogging"=> true,
            "exceptions"=> [
              {
                  "attempt_No"=> 0,
                  "ansMachineDetMode"=> 0,
                  "ansMachineMsg"=> "string"
              }
            ],
            "machineEndSilenceSeconds"=> 0,
            "machineEndTimeoutSeconds"=> 0,
            "machineMinimumWithAgentSeconds"=> 0,
            "machineMinimumWithoutAgentSeconds"=> 0,
            "preConnectCPAEnabled"=> true,
            "preConnectCPARecording"=> true,
            "treatProgressAsRinging"=> true,
            "utteranceMinimumSeconds"=> 0
        }
    }
params= body.to_json
	# Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  res = http.put(uri.path ,params, headers)

   construct('Admin','Skills','PUT services/v16.0/skills{skillId}/parameters/cpa-management','so32-v13.0',res.code,res.message)
else
construct('Admin','Skills','GET services/v16.0/skills{skillId}/parameters/cpa-management','so32-v13.0',res.code,res.message)
    end
end	  	
#-------------------------------------
def getSkillxssettings(fields)
if($accessToken!='')
uri= URI.parse(($baseURI)+'/services/v13.0/skills/'+($skillId)+'/parameters/xs-settings'+'?fields='+fields)

headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json",
		"Accept-Encoding" => "gzip,deflate"}
	# Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  res = http.get(uri.path ,  headers)

   construct('Admin','Skills','GET /parameters/xs-settings','so32-v13.0',res.code,res.message)
else
construct('Admin','Skills','GET /parameters/xs-settings','so32-v13.0',res.code,res.message)
    end
end	  	
#-------------------------------------	
def PutSkillParameterXssettings()
if($accessToken!='')
uri= URI.parse(($baseURI)+'/services/v13.0/skills/'+($skillId)+'/parameters/xs-settings')

headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "gzip,deflate"}
		body= 
				{
        "xsSettings"=> {
            "xsScriptID"=> 0,
            "xsCheckinScriptID"=> 0,
            "externalOutboundSkill_No"=> "string",
            "xsSkillChangedActive"=> true,
            "xsGetContactsActive"=> true,
            "xsFreshThreshold"=> 50,
            "xsAvailableThreshold"=> 150,
            "xsReadyThreshold"=> 100,
            "xsNumberToRetrieve"=> 50
        }
    }
	params=body.to_json	
		# Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  res = http.put(uri.path ,params, headers)

   construct('Admin','Skills','PUT /parameters/xs-settings','so32-v13.0',res.code,res.message)
else
construct('Admin','Skills','PUT /parameters/xs-settings','so32-v13.0',res.code,res.message)
    end
end	  	
#-----------------------------------
def getSkillDeliveryPreferences(fields)
if($accessToken!='')
uri= URI.parse(($baseURI)+'/services/v13.0/skills/'+($skillId)+'/parameters/delivery-preferences'+'?fields='+fields)

headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json",
		"Accept-Encoding" => "gzip,deflate"}
	# Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  res = http.get(uri.path ,  headers)

   construct('Admin','Skills','GET parameters/delivery-preferences','so32-v13.0',res.code,res.message)
else
construct('Admin','Skills','GET parameters/delivery-preferences','so32-v13.0',res.code,res.message)
    end
end	  	
#------------------------
def PutSkillParameterDeliveryPreferences()
if($accessToken!='')
uri= URI.parse(($baseURI)+'/services/v13.0/skills/'+($skillId)+'/parameters/delivery-preferences')

headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "gzip,deflate"}
		body= 
				{
        "deliveryPreferences"=> {
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
        }
    }
	params=body.to_json	
		# Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  res = http.put(uri.path ,params, headers)

   construct('Admin','Skills','PUT parameters/delivery-preferences','so32-v13.0',res.code,res.message)
else
construct('Admin','Skills','PUT parameters/delivery-preferences','so32-v13.0',res.code,res.message)
    end
end	  	
#--------------------
def getSkillRetrysettings(fields)
if($accessToken!='')
uri= URI.parse(($baseURI)+'/services/v13.0/skills/'+($skillId)+'/parameters/retry-settings'+'?fields='+fields)

headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json",
		"Accept-Encoding" => "gzip,deflate"}
	# Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  res = http.get(uri.path ,  headers)

   construct('Admin','Skills','GET parameters/retry-settings','so32-v13.0',res.code,res.message)
else
construct('Admin','Skills','GET parameters/retry-settings','so32-v13.0',res.code,res.message)
    end
end	  	
#--------------------	
def PutSkillParameterRetrySettings()
if($accessToken!='')
uri= URI.parse(($baseURI)+'/services/v13.0/skills/'+($skillId)+'/parameters/retry-settings')

headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "gzip,deflate"}
		body= 
				{
        "retrySettings"=> {
            "loadNonFresh"=> true,
            "finalizeWhenExhausted"=> true,
            "maximumAttempts"=> 10,
            "minimumRetryMinutes"=> 240,
            "maximumNumberOfHandledCalls"=> 10,
            "restrictedCallingMinutes"=> 0,
            "restrictedCallingMaxAttempts"=> 0,
            "generalStaleMinutes"=> 30,
            "callbackRestMinutes"=> 1440,
            "releaseAgentSpecificCalls"=> true,
            "maximumNumberOfCallbacks"=> 10,
            "callbackStaleMinutes"=> 15
        }
    }
	params=body.to_json	
		# Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  res = http.put(uri.path ,params, headers)

   construct('Admin','Skills','PUT parameters/retry-settings','so32-v13.0',res.code,res.message)
else
construct('Admin','Skills','PUT parameters/retry-settings','so32-v13.0',res.code,res.message)
    end
end	  	
#-------------------
def getSkillScheduleSettings()
if($accessToken!='')
uri= URI.parse(($baseURI)+'/services/v13.0/skills/'+($skillId)+'/parameters/schedule-settings')

headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json",
		"Accept-Encoding" => "gzip,deflate"}
	# Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  res = http.get(uri.path ,  headers)

   construct('Admin','Skills','GET parameters/schedule-settings','so32-v13.0',res.code,res.message)
else
construct('Admin','Skills','GET parameters/schedule-settings','so32-v13.0',res.code,res.message)
    end
end	 
#-----------------
def PutSkillParameterScheduleSettings()
if($accessToken!='')
uri= URI.parse(($baseURI)+'/services/v13.0/skills/'+($skillId)+'/parameters/schedule-settings')

headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "gzip,deflate"}
		body= 
				{
        "scheduleSettings"=> {
            "isScheduled"=> true,
            "sundayStartTime"=> "string",
            "sundayEndTime"=> "string",
            "sundayIsActive"=> true,
            "mondayStartTime"=> "string",
            "mondayEndTime"=> "string",
            "mondayIsActive"=> true,
            "tuesdayStartTime"=> "string",
            "tuesdayEndTime"=> "string",
            "tuesdayIsActive"=> true,
            "wednesdayStartTime"=> "string",
            "wednesdayEndTime"=> "string",
            "wednesdayIsActive"=> true,
            "thursdayStartTime"=> "string",
            "thursdayEndTime"=> "string",
            "thursdayIsActive"=> true,
            "fridayStartTime"=> "string",
            "fridayEndTime"=> "string",
            "fridayIsActive"=> true,
            "saturdayStartTime"=> "string",
            "saturdayEndTime"=> "string",
            "saturdayIsActive"=> true
        }
    }
	params=body.to_json	
		# Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  res = http.put(uri.path ,params, headers)

   construct('Admin','Skills','PUT parameters/schedule-settings','so32-v13.0',res.code,res.message)
else
construct('Admin','Skills','PUT parameters/schedule-settings','so32-v13.0',res.code,res.message)
    end
end	  	

#Version 15

def getAccessKeys(agentId,fields,skip,top,orderBy)
if($accessToken!='')
uri = URI.parse(($baseURI) +'/services/v15.0/access-keys'+'?agentId='+agentId+'&fields='+fields+'&skip'+ skip+'&top='+ top+'&orderBy='+orderBy)
 
  # Create the GET request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json",
      "Accept-Encoding" => "deflate"}
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # Make the request.
  res = http.get(uri.path , headers)
construct('Admin','Agents','GET /access-keys','v15.0',res.code,res.message)
  # puts res.code
  # puts res.message
else
construct('Admin','Agents','GET /access-keys','v15.0',res.code,res.message)
end
end

def postAccessKeys(agentId)
if($accessToken!='')
uri= URI.parse(($baseURI)+'/services/V15.0/access-keys')

  # Create the Post request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
      "Content-Length" => "0",
      "Accept-Encoding" => "gzip,deflate"}
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  #http.use_ssl= true
  res = http.post(uri.path,headers)
  construct('Admin','Agents','POST /access-keys','v15.0',res.code,res.message)
  else
  construct('Admin','Agents','POST /access-keys','v15.0',res.code,res.message)
end
  end

def getAccessKeysByID()
if($accessToken!='')
uri = URI.parse(($baseURI) +'/services/v15.0/access-keys'+($accessKeyId))
 
  # Create the GET request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json",
		"Accept-Encoding" => "deflate"}
	  
	
 
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # Make the request.
  res = http.get(uri.path , headers)
construct('Admin','Agents','GET /access-keys/{accessKeyId}','v15.0',res.code,res.message)
  # puts res.code
	# puts res.message
else
construct('Admin','Agents','GET /access-keys/{accessKeyId}','v15.0',res.code,res.message)
end
end

def DeleteAccesskeysByID()
if($accessToken!='')
uri= URI.parse(($baseURI)+'/services/v15.0/access-keys'+($accessKeyId))

  # Create the Post request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "gzip,deflate"}
	  

  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  
  
  #http.use_ssl= true

  res = http.delete(uri.path,headers)

  construct('Admin','Agents','DELETE /access-keys/{accessKeyId}','v15.0',res.code,res.message)
  else
  construct('Admin','Agents','DELETE /access-keys/{accessKeyId}','v15.0',res.code,res.message)
end
    end

def patchAccesskeysByID()
if($accessToken!='')
uri= URI.parse(($baseURI)+'/services/v15.0/access-keys'+($accessKeyId))

  # Create the Post request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "gzip,deflate"}
	  

  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  
  
  #http.use_ssl= true

  res = http.patch(uri.path,headers)

  construct('Admin','Agents','PATCH /access-keys/{accessKeyId}','v15.0',res.code,res.message)
  else
  construct('Admin','Agents','PATCH /access-keys/{accessKeyId}','v15.0',res.code,res.message)
end
    end

def PostCampaigns()
if($accessToken!='')
uri = URI.parse(($baseURI)+'/services/v15.0/campaigns')
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "deflate"}
		
		body = {
        "campaigns": [{
            "campaignName"=> "new campaign",
            "isActive"=> true,
           "description"=> "description",
		   "notes"=> "notes"
        }]
    }
	
	params= body.to_json
 # Set up an HTTP object.
  http = Net::HTTP.new uri.host,uri.port
  # puts headers
  # Make the request.
  res = http.put(uri.path , params, headers)

   construct('Admin','Skills','POST /campaigns','so32-v15.0',res.code,res.message)
else
construct('Admin','Skills','POST /campaigns','so32-v15.0',res.code,res.message)
    end
end

def PutCampaignsByID()
if($accessToken!='')
uri = URI.parse(($baseURI)+'/services/v15.0/campaigns'+($campaignId))
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "deflate"}
		
		body = {
        "campaigns": [{
            "campaignName"=> "campaign",
            "isActive"=> true,
           "description"=> "description",
		   "notes"=> "notes"
        }]
    }
	
	params= body.to_json
 # Set up an HTTP object.
  http = Net::HTTP.new uri.host,uri.port
  # puts headers
  # Make the request.
  res = http.put(uri.path , params, headers)

   construct('Admin','Skills','PUT /campaigns/{campaignId}','so32-v15.0',res.code,res.message)
else
construct('Admin','Skills','PUT /campaigns/{campaignId}','so32-v15.0',res.code,res.message)
    end
end

def DeleteCampaignsBySkill()
if($accessToken!='')
uri = URI.parse(($baseURI)+'/services/v15.0/campaigns'+($campaignId)+'/skills')
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "deflate"}
		
		body = {
        "skills": [{
            "skillId"=> 1268,
            "transferCampaignId"=> 0
        }]
    }
	
	params= body.to_json
 # Set up an HTTP object.
  http = Net::HTTP.new uri.host,uri.port
  # puts headers
  # Make the request.
  res = http.delete(uri.path , params, headers)

   construct('Admin','Skills','DELETE /campaigns/{campaignId}/skills','so32-v15.0',res.code,res.message)
else
construct('Admin','Skills','DELETE /campaigns/{campaignId}/skills','so32-v15.0',res.code,res.message)
    end
end

def PostCampaignsBySkill()
if($accessToken!='')
uri = URI.parse(($baseURI)+'/services/v15.0/campaigns'+($campaignId)+'/skills')
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "deflate"}
		
		body = {
        "skills": [{
            "skillId"=> 1268
        }]
    }
	
	params= body.to_json
 # Set up an HTTP object.
  http = Net::HTTP.new uri.host,uri.port
  # puts headers
  # Make the request.
  res = http.post(uri.path , params, headers)

   construct('Admin','Skills','POST /campaigns/{campaignId}/skills','so32-v15.0',res.code,res.message)
else
construct('Admin','Skills','POST /campaigns/{campaignId}/skills','so32-v15.0',res.code,res.message)
    end
end

def getSmsHistoricalTranscript(businessUnitId)
if($accessToken!='')
uri = URI.parse(($baseURI) +'/services/v15.0/contacts/'+($contactId)+'/sms-historical-transcript')
 
  # Create the GET request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json",
		"Accept-Encoding" => "deflate"}
	  
	body = {
        "businessUnitId"=>businessUnitId
    }
 
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # Make the request.
  res = http.get(uri.path ,params, headers)
construct('Admin','Agents','GET /contacts/{contactId}/sms-historical-transcript','v15.0',res.code,res.message)
  # puts res.code
	# puts res.message
else
construct('Admin','Agents','GET /contacts/{contactId}/sms-historical-transcript','v15.0',res.code,res.message)
end
end

def getSmsHistoricalContact(ani,businessUnitId,skillId)
if($accessToken!='')
uri = URI.parse(($baseURI) +'/services/v15.0/contacts/'+($contactId)+'/sms-historical-contacts')
 
  # Create the GET request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json",
		"Accept-Encoding" => "deflate"}
	  
	body = {
        "businessUnitId"=>businessUnitId,
		"ani"=>ani,
		"skillId"=>skillId
    }
 
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # Make the request.
  res = http.get(uri.path ,params, headers)
construct('Admin','Agents','GET /contacts/{contactId}/sms-historical-transcript','v15.0',res.code,res.message)
  # puts res.code
	# puts res.message
else
construct('Admin','Agents','GET /contacts/{contactId}/sms-historical-transcript','v15.0',res.code,res.message)
end
end

def getdisposition(updatedSince,isActive,searchString,fields,skip,top,orderBy)
if($accessToken!='')
uri = URI.parse(($baseURI) +'/services/v15.0/dispositions/skills'+'?updatedSince='+updatedSince+'&isActive='+isActive+ '&searchString='+searchString+'&fields='+fields+'&skip'+ skip+'&top='+ top+'&orderBy='+orderBy)
 
  # Create the GET request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json",
		"Accept-Encoding" => "deflate"}
	  
	
 
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # Make the request.
  res = http.get(uri.path , headers)
construct('Admin','Skills','GET /dispositions/skills','v13.0',res.code,res.message)
  # puts res.code
	# puts res.message
else
construct('Admin','Skills','GET /dispositions/skills','v13.0',res.code,res.message)
end
end

def PostHoursofOperation()
if($accessToken!='')
uri = URI.parse(($baseURI)+'/services/v15.0/hours-of-operation')
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "deflate"}
		
		body = {
            "profileName"=> "new"
    }
	
	params= body.to_json
 # Set up an HTTP object.
  http = Net::HTTP.new uri.host,uri.port
  # puts headers
  # Make the request.
  res = http.post(uri.path , params, headers)

   construct('Admin','General','POST /hours-of-operation','so32-v15.0',res.code,res.message)
else
construct('Admin','General','POST /hours-of-operation','so32-v15.0',res.code,res.message)
    end
end

def PutHoursofOperationByID()
if($accessToken!='')
uri = URI.parse(($baseURI)+'/services/v15.0/hours-of-operation'+($profileId))
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "deflate"}
	
 # Set up an HTTP object.
  http = Net::HTTP.new uri.host,uri.port
  # puts headers
  # Make the request.
  res = http.post(uri.path , headers)

   construct('Admin','General','PUT /hours-of-operation/{profileId}','so32-v15.0',res.code,res.message)
else
construct('Admin','General','PUT /hours-of-operation/{profileId}','so32-v15.0',res.code,res.message)
    end
end

def PostPointofContact()
if($accessToken!='')
uri = URI.parse(($baseURI)+'/services/v15.0/points-of-contact')
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "deflate"}
		
		body = {
             "pointOfContact": "string",
             "pointOfContactName": "string",
             "skillId": 0,
             "isActive": true,
             "mediaTypeId": 0,
             "scriptName": "string",
             "ivrReportingEnabled": true
    }
	
	params= body.to_json
 # Set up an HTTP object.
  http = Net::HTTP.new uri.host,uri.port
  # puts headers
  # Make the request.
  res = http.post(uri.path , params, headers)

   construct('Admin','General','POST /points-of-contact','so32-v15.0',res.code,res.message)
else
construct('Admin','General','POST /points-of-contact','so32-v15.0',res.code,res.message)
    end
end

def PutPointofContactByID()
if($accessToken!='')
uri = URI.parse(($baseURI)+'/services/v15.0/points-of-contact'+($pointOfContactId))
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "deflate"}
		
		body = {
             "pointOfContactName": "new",
             "skillId": 0,
             "isActive": true,
             "scriptName": "new1",
             "ivrReportingEnabled": true
    }
	
	params= body.to_json
 # Set up an HTTP object.
  http = Net::HTTP.new uri.host,uri.port
  # puts headers
  # Make the request.
  res = http.put(uri.path , params, headers)

   construct('Admin','General','PUT /points-of-contact/{pointOfContactId}','so32-v15.0',res.code,res.message)
else
construct('Admin','General','PUT /points-of-contact/{pointOfContactId}','so32-v15.0',res.code,res.message)
    end
end

def PostUnavailableCode()
if($accessToken!='')
uri = URI.parse(($baseURI)+'/services/v15.0/unavailable-codes')
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "deflate"}
		
		body = {
              "unavailableCodeName": "string",
              "postContact": true,
              "isActive": true
    }
	
	params= body.to_json
 # Set up an HTTP object.
  http = Net::HTTP.new uri.host,uri.port
  # puts headers
  # Make the request.
  res = http.post(uri.path , params, headers)

   construct('Admin','General','POST /unavailable-codes','so32-v15.0',res.code,res.message)
else
construct('Admin','General','POST /unavailable-codes','so32-v15.0',res.code,res.message)
    end
end

def getPhonenumbers(searchString,skip,top)
if($accessToken!='')
uri = URI.parse(($baseURI) +'/services/v15.0/phone-numbers'+'?searchString='+searchString+'&skip'+ skip+'&top='+ top)
 
  # Create the GET request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json",
		"Accept-Encoding" => "deflate"}
	  
	
 
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # Make the request.
  res = http.get(uri.path , headers)
construct('Admin','General','GET /phone-numbers','v15.0',res.code,res.message)
  # puts res.code
	# puts res.message
else
construct('Admin','General','GET /phone-numbers','v15.0',res.code,res.message)
end
end

def PutUnavailableCodeByID()
if($accessToken!='')
uri = URI.parse(($baseURI)+'/services/v15.0/unavailable-codes/'+($unavailablecodeId))
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "deflate"}
		
		body = {
             "unavailableCodeName"=> "string",
             "postContact"=> true,
             "agentTimeout"=> 0,
             "isActive"=> true
    }
	
	params= body.to_json
 # Set up an HTTP object.
  http = Net::HTTP.new uri.host,uri.port
  # puts headers
  # Make the request.
  res = http.put(uri.path , params, headers)

   construct('Admin','Agents','PUT /unavailable-codes/{unavailableCodeId}','so32-v15.0',res.code,res.message)
else
construct('Admin','Agents','PUT /unavailable-codes/{unavailableCodeId}','so32-v15.0',res.code,res.message)
    end
end

def PutUnavailableCodeByIDTeams()
if($accessToken!='')
uri = URI.parse(($baseURI)+'/services/v15.0/unavailable-codes/'+($unavailablecodeId)+'/teams')
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "deflate"}
		
		body = {
        "securityUser"=>"string",
        "teams"=> [{
            "teamId"=> "string",
            "isActive"=> true,
           "description"=> "description",
		   "notes"=> "notes"
        }]
    }
  params= body.to_json
 # Set up an HTTP object.
  http = Net::HTTP.new uri.host,uri.port
  # puts headers
  # Make the request.
  res = http.put(uri.path , params, headers)

   construct('Admin','Agents','PUT /unavailable-codes/{unavailableCodeId}/teams','so32-v15.0',res.code,res.message)
else
construct('Admin','Agents','PUT /unavailable-codes/{unavailableCodeId}/teams','so32-v15.0',res.code,res.message)
    end
end

#version 15
#AgentAPI
def PostEmailSaveDraft()
if($accessToken!='')
uri= URI.parse(($baseURI)+'/services/v15.0/agent-sessions/'+($sessionId)+'/interactions/'+($contactId)+'/email-save-draft')

headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "gzip,deflate"}
 
body =
    {
              "toAddress"=> "",
              "fromAddress"=> "",
              "ccAddress"=> " ",
              "bccAddress"=> "",
              "campaignId"=> "",
              "subject"=> "",
              "bodyHtml"=> "",
              "attachments"=> "",
              "attachmentNames"=> "",
              "draftEmailGuidStr"=> "",
              "primaryDispositionId"=> "",
              "secondaryDispositionId"=> "",
			  "tags"=> "",
			  "notes"=> "",
			  "originalAttachmentNames"=> ""
    }
params= body.to_json
	# Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  res = http.post(uri.path ,params,  headers)

   construct('Agents','Emails','POST /save-email-draft','so32-v15.0',res.code,res.message)
else
construct('Agents','Emails','POST /save-email-draft','so32-v15.0',res.code,res.message)
    end
end	  

def PostAddText()
if($accessToken!='')
uri= URI.parse(($baseURI)+'/services/v15.0/agent-sessions/'+($sessionId)+'/interactions/add-text')

headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "gzip,deflate"}
 
body =
    {
              "mediaType"=> 0,
             }
params= body.to_json
	# Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  res = http.post(uri.path ,params,  headers)

   construct('Admin','Skills','POST interactions/add-text','so32-v15.0',res.code,res.message)
else
construct('Admin','Skills','POST interactions/add-text','so32-v15.0',res.code,res.message)
    end
end	  

def PostJobSync()
if($accessToken!='')
uri= URI.parse(($CXone_baseURI)+'data-extraction/v1/jobs/sync')

headers = 
 { 
 "Accept" => "application/json, text/javascript, */*; q=0.01",
 "Authorization" => "bearer " + ($accessToken),
 "Content-Type" => "application/json",
 "Data-Type" => "json",
 "Content-Length" => "0",
 "Accept-Encoding" => "gzip,deflate"}
 
body =
 {
  "entityName"=>"qm-workflows",
  "version"=>"1",
  "startDate"=>"2019-07-10",
  "endDate"=>"2019-07-12"
 }
params= body.to_json
	# Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  http.use_ssl = true
  res = http.post(uri.path ,params,  headers)

   construct('DataExtraction','ExtractData','POST jobs-sync','so32-v1.0',res.code,res.message)
else
construct('DataExtraction','ExtractData','POST POST jobs-sync','so32-v1.0',res.code,res.message)
    end
end	  

def getMediaPlayBack(mediaType,excludeWaveform)
if($accessToken!='')
uri = URI.parse(($CXone_baseURI) +'media-playback/v1/contacts/'+$contactId+'?mediaType='+mediaType+'&excludeWaveform='+excludeWaveform)
 
  # Create the GET request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json",
      "Accept-Encoding" => "deflate"}
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # Make the request.
  http.use_ssl = true
  res = http.get(uri.path , headers)
construct('Mediaplayback','AccessingRecordedMedia','GET /media-playback/v1/contacts/{contactId}','so32-v1.0',res.code,res.message)
  # puts res.code
  # puts res.message
else
construct('Mediaplayback','AccessingRecordedMedia','GET /media-playback/v1/contacts/{contactId}','so32-v1.0',res.code,res.message)
end
end

def getMediaPlayBackByID(mediaType,excludeWaveform)
if($accessToken!='')
uri = URI.parse(($CXone_baseURI) +'media-playback/v1/contacts/'+$contactId+ '/segments/'+$segmentId +'?mediaType='+mediaType+'&excludeWaveform='+excludeWaveform)
 
  # Create the GET request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json",
      "Accept-Encoding" => "deflate"}
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # Make the request.
  http.use_ssl = true
  res = http.get(uri.path , headers)
construct('Mediaplayback','AccessingRecordedMedia','GET /media-playback​/v1​/contacts​/{contactId}​/segments​/{segmentId}}','so32-v1.0',res.code,res.message)
  # puts res.code
  # puts res.message
else
construct('Mediaplayback','AccessingRecordedMedia','GET ​/media-playback​/v1​/contacts​/{contactId}​/segments​/{segmentId}','so32-v1.0',res.code,res.message)
end
end

def getMediaPlayBackContacts(acdId,mediaType,excludeWaveform)
if($accessToken!='')
uri = URI.parse(($CXone_baseURI) +'media-playback/v1/contacts/'+'?acdId='+acdId+'&mediaType='+mediaType+'&excludeWaveform='+excludeWaveform)
 
  # Create the GET request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json",
      "Accept-Encoding" => "deflate"}
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # Make the request.
  http.use_ssl = true
  res = http.get(uri.path , headers)
construct('Mediaplayback','AccessingRecordedMedia','GET /media-playback/v1/contacts','so32-v1.0',res.code,res.message)
  # puts res.code
   #puts res.message
else
construct('Mediaplayback','AccessingRecordedMedia','GET ​/media-playback/v1/contacts','so32-v1.0',res.code,res.message)
end
end

def getActiveFlag(activeFlag)
if($accessToken!='')
uri = URI.parse(($baseURI) +'services/v16.0/workflow-data/list/'+activeFlag)
  # Create the GET request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json",
      "Accept-Encoding" => "deflate"}
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # Make the request.
  http.use_ssl = true
  res = http.get(uri.path , headers)
construct('Admin','Workflow','GET /workflow-data/list','so32-v16.0',res.code,res.message)
  # puts res.code
   #puts res.message
else
construct('Admin','Workflow','GET ​/workflow-data/list','so32-v16.0',res.code,res.message)
end
end

def PostWorkflowData()
if($accessToken!='')
uri= URI.parse(($baseURI) + 'services/v16.0/workflow-data')

headers = 
 { 
 "Accept" => "application/json, text/javascript, */*; q=0.01",
 "Authorization" => "bearer " + ($accessToken),
 "Content-Type" => "application/json",
 "Data-Type" => "json",
 "Content-Length" => "0",
 "Accept-Encoding" => "gzip,deflate"}
 
body =
 {
  "profile" => {
    "ProfileName" => "new",
    "Description" => "abc",
    "ProfileID" => 0
  },
  "data" => {
    "date" => {
      "Value" => [
        "string"
      ],
      "Visible" => "1 ",
      "Type" => " ",
      "Ref" => " "
    }
  }
}
params= body.to_json
	# Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  http.use_ssl = true
  res = http.post(uri.path ,params,  headers)

   construct('Admin','Workflow','POST /workflow-data','so32-v16.0',res.code,res.message)
else
construct('Admin','Workflow','POST /workflow-data','so32-v16.0',res.code,res.message)
    end
end	  

def getWorkflowDataById()
if($accessToken!='')
uri = URI.parse(($baseURI)+'services/v16.0/workflow-data/'+$workflowDataId)
 
  # Create the GET request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json",
      "Accept-Encoding" => "deflate"}
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # Make the request.
  http.use_ssl = true
  res = http.get(uri.path , headers)
construct('Admin','Workflow','GET /workflow-data/ID','so32-v16.0',res.code,res.message)
  # puts res.code
   #puts res.message
else
construct('Admin','Workflow','GET /workflow-data/ID','so32-v16.0',res.code,res.message)
end
end

def PutWorkflowDataById()
if($accessToken!='')
uri = URI.parse(($baseURI)+'services/v16.0/workflow-data/'+$workflowDataId)
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "gzip,deflate"}
		
body =
 {
  "profile" => {
    "ProfileName" => "new",
    "Description" => "abc",
    "ProfileID" => 0
  },
  "data" => {
    "date" => {
      "Value" => [
        "string"
      ],
      "Visible" => "1 ",
      "Type" => " ",
      "Ref" => " "
    }
  }
}
  params= body.to_json
 # Set up an HTTP object.
  http = Net::HTTP.new uri.host,uri.port
  http.use_ssl = true
  # puts headers
  # Make the request.
  res = http.put(uri.path , params, headers)

   construct('Admin','Workflow','PUT /workflow-data/ID','so32-v16.0',res.code,res.message)
else
construct('Admin','Workflow','PUT /workflow-data/ID','so32-v16.0',res.code,res.message)
    end
end

def PutWorkflowDataByIdActivate()
if($accessToken!='')
uri = URI.parse(($baseURI)+'services/v16.0/workflow-data/'+$workflowDataId+'/activate')
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "gzip,deflate"}
	  body={}
params=body.to_json()
 # Set up an HTTP object.
  http = Net::HTTP.new uri.host,uri.port
  http.use_ssl = true
  # puts headers
  # Make the request.
  res = http.put(uri.path , params, headers)

   construct('Admin','Workflow','PUT /workflow-data/ID/activate','so32-v16.0',res.code,res.message)
else
construct('Admin','Workflow','PUT /workflow-data/ID/activate','so32-v16.0',res.code,res.message)
    end
end

def PutWorkflowDataByIdDeactivate()
if($accessToken!='')
uri = URI.parse(($baseURI)+'services/v16.0/workflow-data/'+$workflowDataId+'/deactivate')
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "gzip,deflate"}
	  	  body={}
params=body.to_json()
 # Set up an HTTP object.
  http = Net::HTTP.new uri.host,uri.port
  http.use_ssl = true
  # puts headers
  # Make the request.
  res = http.put(uri.path ,params, headers)

   construct('Admin','Workflow','PUT /workflow-data/ID/deactivate','so32-v16.0',res.code,res.message)
else
construct('Admin','Workflow','PUT /workflow-data/ID/deactivate','so32-v16.0',res.code,res.message)
    end
end

def getJobs()
if($accessToken!='')
uri= URI.parse(($CXone_baseURI)+'data-extraction/v1/jobs')
 
  # Create the GET request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json",
		"Accept-Encoding" => "deflate"}
	  
	
 
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # Make the request.
  res = http.get(uri.path , headers)
construct('DataExtraction','ExtractData','GET jobs','so32-v1.0',res.code,res.message)
else
construct('DataExtraction','ExtractData','GET jobs','so32-v1.0',res.code,res.message)
    end
end
end

def PostJobs()
if($accessToken!='')
uri= URI.parse(($CXone_baseURI)+'data-extraction/v1/jobs')

headers = 
 { 
 "Accept" => "application/json, text/javascript, */*; q=0.01",
 "Authorization" => "bearer " + ($accessToken),
 "Content-Type" => "application/json",
 "Data-Type" => "json",
 "Content-Length" => "0",
 "Accept-Encoding" => "gzip,deflate"}
 
body =
 {
  "entityName"=>"qm-workflows",
  "version"=>"1",
  "startDate"=>"2019-07-10",
  "endDate"=>"2019-07-12"
 }
params= body.to_json
	# Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  http.use_ssl = true
  res = http.post(uri.path ,params,  headers)

   construct('DataExtraction','ExtractData','POST jobs','so32-v1.0',res.code,res.message)
else
construct('DataExtraction','ExtractData','POST POST jobs','so32-v1.0',res.code,res.message)
    end
end	  

def getJobsByID()
if($accessToken!='')
uri= URI.parse(($CXone_baseURI)+'data-extraction/v1/jobs/' +$jobID)
 
  # Create the GET request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json",
		"Accept-Encoding" => "deflate"}
	  
	
 
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # Make the request.
  res = http.get(uri.path , headers)
construct('DataExtraction','ExtractData','GET jobs/jobID','so32-v1.0',res.code,res.message)
else
construct('DataExtraction','ExtractData','GET jobs/jobID','so32-v1.0',res.code,res.message)
    end
end

def getBusinessunitTimezone()
 if($accessToken!='')
 uri = URI.parse(($baseURI) +'/services/v17/business-unit/time-zones')
 
  # Create the GET request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json",
      "Accept-Encoding" => "deflate"}
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # Make the request.
  res = http.get(uri.path , headers)
  construct('Admin','General','GET /business-unit/time-zones','so32-v17.0',res.code,res.message)
else
construct('Admin','General','GET /business-unit/time-zones','so32-v17.0',res.code,res.message)
    end
end

def getSuppresedContacts()
 if($accessToken!='')
 uri = URI.parse(($baseURI) +'/services/v17/suppressed-contact')
 
  # Create the GET request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json",
      "Accept-Encoding" => "deflate"}
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # Make the request.
  res = http.get(uri.path , headers)
  onstruct('Admin','General','GET /suppressed-contact','so32-v17.0',res.code,res.message)
else
construct('Admin','General','GET /suppressed-contact','so32-v17.0',res.code,res.message)
    end
end

def PostSuppresedContacts()
 if($accessToken!='')
 uri = URI.parse(($baseURI)+'/services/v17/suppressed-contact')
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
      "Content-Length" => "0",
      "Accept-Encoding" => "deflate"}
       body = {
              "startDate": "string",
              "endDate": "string",
              "value": "string",
              "skills":"string"
     }
  params= body.to_json
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host,uri.port
  onstruct('Admin','General','Post /suppressed-contact','so32-v17.0',res.code,res.message)
else
construct('Admin','General','Post /suppressed-contact','so32-v17.0',res.code,res.message)
    end
end

def getBusinessunitTimezone()
 if($accessToken!='')
 uri = URI.parse(($baseURI) +'/services/v17/business-unit/time-zones')
 
  # Create the GET request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json",
      "Accept-Encoding" => "deflate"}
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # Make the request.
  res = http.get(uri.path , headers)
  construct('Admin','General','GET /business-unit/time-zones','so32-v17.0',res.code,res.message)
else
construct('Admin','General','GET /business-unit/time-zones','so32-v17.0',res.code,res.message)
    end
end

def getSuppresedContacts()
 if($accessToken!='')
 uri = URI.parse(($baseURI) +'/services/v17/suppressed-contact')
 
  # Create the GET request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json",
      "Accept-Encoding" => "deflate"}
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # Make the request.
  res = http.get(uri.path , headers)
  onstruct('Admin','General','GET /suppressed-contact','so32-v17.0',res.code,res.message)
else
construct('Admin','General','GET /suppressed-contact','so32-v17.0',res.code,res.message)
    end
end

def PostSuppresedContacts()
 if($accessToken!='')
 uri = URI.parse(($baseURI)+'/services/v17/suppressed-contact')
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
      "Content-Length" => "0",
      "Accept-Encoding" => "deflate"}
       body = {
              "startDate": "string",
              "endDate": "string",
              "value": "string",
              "skills":"string"
     }
  params= body.to_json
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host,uri.port
  onstruct('Admin','General','Post /suppressed-contact','so32-v17.0',res.code,res.message)
else
construct('Admin','General','Post /suppressed-contact','so32-v17.0',res.code,res.message)
    end
end

def PutbusinessunitTimezone()
if($accessToken!='')
uri = URI.parse(($baseURI)+'services/v17/business-unit/time-zones')
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "deflate"}
		
 body =
 { 
    "timezones" => "string",
    "items" => "string"
  }
  params= body.to_json
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host,uri.port
 onstruct('Admin','General','Put /business-unit/time-zones','so32-v17.0',res.code,res.message)
else
construct('Admin','General','Put /business-unit/time-zones','so32-v17.0',res.code,res.message)
    end
end

def PutSkillIDParameterTimezone()
if($accessToken!='')
uri = URI.parse(($baseURI)+'/services/v17/skills'+($skillId)+'/parameters/time-zones')
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "deflate"}
		
 body =
 { 
    "timezones" => "string",
    "items" => "string"
  }
  params= body.to_json
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host,uri.port
 onstruct('Admin','Skills','Put /parameters/time-zones','so32-v17.0',res.code,res.message)
else
construct('Admin','Skills','Put /parameters/time-zones','so32-v17.0',res.code,res.message)
    end
end

def getTimeZoneBySkillId()
 if($accessToken!='')
 uri = URI.parse(($baseURI) +'/services/v17/skills'+($skillId)+'/parameters/time-zones')
  #Create the GET request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json",
      "Accept-Encoding" => "deflate"}
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # Make the request.
  res = http.get(uri.path , headers)
  onstruct('Admin','Skills','Get /parameters/time-zones','so32-v17.0',res.code,res.message)
else
construct('Admin','Skills','Get /parameters/time-zones','so32-v17.0',res.code,res.message)
    end
end
end

def deleteSuppressedContactBySuppressedContactId()
 if($accessToken!='')
 uri = URI.parse(($baseURI)+'/services/{version}/suppressed-contact'+($suppressedContactId))
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
      "Content-Length" => "0",
      "Accept-Encoding" => "deflate"}
     
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host,uri.port
  # puts headers
  # Make the request.
  res = http.delete(uri.path, headers)
 else
  #no token get one or handle error
    end
end

def getSuppressedContactBySuppressedContactId()
  # Check that the token is a valid token.
  unless !(token["access_token"] == nil && 
           token["resource_server_base_uri"] == nil)
    raise ArgumentError, "the function didn't receive a token it could understand"
  end
      
  # Pull the access token and base URL from the response body.
  accessToken = token["access_token"]
  baseURL = token["resource_server_base_uri"]

  # Create the URL that accesses the API.
  apiURL = "services/{version}/suppressed-contact/#{suppressedContactId}"
  uri = URI.parse(baseURL + apiURL)

  # Create the GET request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer #{accessToken}",
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json" }

  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port

  # Make the GET request an HTTPS GET request.
  http.use_ssl = true

  # Delete this line and be sure you have a valid certificate.
  # The default Ruby library, net/https, doesn't check the validity of a 
  # certificate during a handshake. Using VERIFY_NONE is a simple, and insecure,
  # hack to get around this limitation.
  http.verify_mode = OpenSSL::SSL::VERIFY_NONE

  # Make the request and store the response.
  response = http.request_get(baseURL + apiURL, headers)
    
  # The data the API returns is in JSON, so parse it if it's valid.
  data = if response.body != ""
      JSON.parse(response.body)
    else
      "The response was empty."
  end
  # Now you can do something with the data the API returned.
end

def putSuppressedContactBySuppressedContactId()
 if($accessToken!='')
 uri = URI.parse(($baseURI)+'/services/{version}/suppressed-contact'+($suppressedContactId))
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
      "Content-Length" => "0",
      "Accept-Encoding" => "deflate"}
      body = {
             "suppressedContactData":
{
             "startDate": "2019-12-04T13:27:01.766Z" ,
             "endDate": "2019-12-04T13:27:01.766Z",
             "value": "new",
             "skills": "string"
}
     }
  params= body.to_json
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host,uri.port
  # puts headers
  # Make the request.
  res = http.put(uri.path , params, headers)
 else
  #no token get one or handle error
    end
end

def getContactsByIdHierachy()
 if($accessToken!='')
 uri = URI.parse(($baseURI) +'/services/{version}/contacts/'+($contactId)+'/hierarchy')
 
  # Create the GET request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json",
      "Accept-Encoding" => "deflate"}
      body = {
        "contactId"=>contactId}
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # Make the request.
  res = http.get(uri.path ,body, headers)
  # puts res.code
  # puts res.message
 else
  #no token get one or handle error
end
end

end

def PutbusinessunitTimezone()
if($accessToken!='')
uri = URI.parse(($baseURI)+'services/v17/business-unit/time-zones')
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "deflate"}
		
 body =
 { 
    "timezones" => "string",
    "items" => "string"
  }
  params= body.to_json
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host,uri.port
 onstruct('Admin','General','Put /business-unit/time-zones','so32-v17.0',res.code,res.message)
else
construct('Admin','General','Put /business-unit/time-zones','so32-v17.0',res.code,res.message)
    end
end

def PutSkillIDParameterTimezone()
if($accessToken!='')
uri = URI.parse(($baseURI)+'/services/v17/skills'+($skillId)+'/parameters/time-zones')
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "deflate"}
		
 body =
 { 
    "timezones" => "string",
    "items" => "string"
  }
  params= body.to_json
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host,uri.port
 onstruct('Admin','Skills','Put /parameters/time-zones','so32-v17.0',res.code,res.message)
else
construct('Admin','Skills','Put /parameters/time-zones','so32-v17.0',res.code,res.message)
    end
end

def getTimeZoneBySkillId()
 if($accessToken!='')
 uri = URI.parse(($baseURI) +'/services/v17/skills'+($skillId)+'/parameters/time-zones')
  #Create the GET request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json",
      "Accept-Encoding" => "deflate"}
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # Make the request.
  res = http.get(uri.path , headers)
  onstruct('Admin','Skills','Get /parameters/time-zones','so32-v17.0',res.code,res.message)
else
construct('Admin','Skills','Get /parameters/time-zones','so32-v17.0',res.code,res.message)
    end
end

def DeleteskillbyagentID()
if($accessToken!='')
uri= URI.parse(($baseURI)+'/services/v15.0 /skills/'+($skillId)+'/agents/'+($agentId))

  # Create the Post request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "gzip,deflate"}
	  

  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  
  
  #http.use_ssl= true

  res = http.delete(uri.path,headers)

  construct('Admin','skill','Delete /skills/{skillId}/agents/{agentId}','v15.0',res.code,res.message)
  else
  construct('Admin','skill','Delete /skills/{skillId}/agents/{agentId}','v15.0',res.code,res.message)
end
    end
	
def getScripts(token, scriptName)
  # Check that the token is a valid token.
  unless !(token["access_token"] == nil && 
           token["resource_server_base_uri"] == nil)
    raise ArgumentError, "the function didn't receive a token it could understand"
  end
  
  # Pull the access token and base URL from the response body.
  accessToken = token["access_token"]
  baseURL = token["resource_server_base_uri"]
  
  # Create the URL that accesses the API.    
  apiURL = "/services/{version}/scripts?scriptPath=#{scriptPath}&scriptId=#{scriptId}"
  uri = URI.parse(baseURL + apiURL)

  # Create the GET request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer #{accessToken}",
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json" }

  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port

  # Make the GET request an HTTPS GET request.
  http.use_ssl = true

  # Delete this line and be sure you have a valid certificate.
  # The default Ruby library, net/https, doesn't check the validity of a 
  # certificate during a handshake. Using VERIFY_NONE is a simple, and insecure,
  # hack to get around this limitation.
  http.verify_mode = OpenSSL::SSL::VERIFY_NONE

  # Make the request and store the response.
  response = http.request_get(uri.to_s, headers)

  # The data the API returns is in JSON, so parse it if it's valid.
  data = if response.body != ""
      JSON.parse(response.body)
    else
      "The response was empty."
  end

  # Now you can do something with the data the API returned.
end	
	
def postScripts(token, scriptName="inContactDNCTest", scriptPath="", body="")
  # Check that the token is a valid token.
  if (token["access_token"] == nil || 
      token["resource_server_base_uri"] == nil)
    raise ArgumentError, "The function didn't get a token it could understand"
  end
    
  # Pull the access token and base URL from the response body.
  accessToken = token["access_token"]
  baseURL = token["resource_server_base_uri"]

  # Create the URL that accesses the API.
  apiURL = "services/{version}/scripts?scriptPath=#{scriptPath}&"\
           "body=#{body}"
  uri = URI.parse(baseURL + apiURL)

  # Create the request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer #{accessToken}",
      "Content-Length" => "0",
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json" }

  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port

  # Make the request an HTTPS request.
  http.use_ssl = true

  # Delete this line and be sure you have a valid certificate.
  # The default Ruby library, net/https, doesn't check the validity of a 
  # certificate during a handshake. Using VERIFY_NONE is a simple, and insecure,
  # hack to get around this limitation.
  http.verify_mode = OpenSSL::SSL::VERIFY_NONE

  # Make the request and store the response.
  response = http.post(uri.path, "", headers)
end

def putScripts(token, scriptName)
  # Check that the token is a valid token.
  unless !(token["access_token"] == nil && 
           token["resource_server_base_uri"] == nil)
    raise ArgumentError, "the function didn't receive a token it could understand"
  end
  
  # Pull the access token and base URL from the response body.
  accessToken = token["access_token"]
  baseURL = token["resource_server_base_uri"]
  
  # Create the URL that accesses the API.    
  apiURL = "/services/{version}/scripts?scriptPath=#{scriptPath}&scriptId=#{scriptId}"
  uri = URI.parse(baseURL + apiURL)

  # Create the PUT request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer #{accessToken}",
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json" }

  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port

  # Make the PUT request an HTTPS PUT request.
  http.use_ssl = true

  # Delete this line and be sure you have a valid certificate.
  # The default Ruby library, net/https, doesn't check the validity of a 
  # certificate during a handshake. Using VERIFY_NONE is a simple, and insecure,
  # hack to put around this limitation.
  http.verify_mode = OpenSSL::SSL::VERIFY_NONE

  # Make the request and store the response.
  response = http.request_put(uri.to_s, headers)

  # The data the API returns is in JSON, so parse it if it's valid.
  data = if response.body != ""
      JSON.parse(response.body)
    else
      "The response was empty."
  end

  # Now you can do something with the data the API returned.
end

def postScriptsKick(token, scriptName="inContactDNCTest", scriptPath="")
  # Check that the token is a valid token.
  if (token["access_token"] == nil || 
      token["resource_server_base_uri"] == nil)
    raise ArgumentError, "The function didn't get a token it could understand"
  end
    
  # Pull the access token and base URL from the response body.
  accessToken = token["access_token"]
  baseURL = token["resource_server_base_uri"]

      # Create the URL that accesses the API.
  apiURL = "services/{version}/scripts/kick?scriptPath=#{scriptPath}"
  uri = URI.parse(baseURL + apiURL)

  # Create the request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer #{accessToken}",
      "Content-Length" => "0",
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json" }

  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port

  # Make the request an HTTPS request.
  http.use_ssl = true

  # Delete this line and be sure you have a valid certificate.
  # The default Ruby library, net/https, doesn't check the validity of a 
  # certificate during a handshake. Using VERIFY_NONE is a simple, and insecure,
  # hack to get around this limitation.
  http.verify_mode = OpenSSL::SSL::VERIFY_NONE

  # Make the request and store the response.
  response = http.post(uri.path, "", headers)
end

def getScriptsHistoryByName(token, scriptName)
  # Check that the token is a valid token.
  unless !(token["access_token"] == nil && 
           token["resource_server_base_uri"] == nil)
    raise ArgumentError, "the function didn't receive a token it could understand"
  end
  
  # Pull the access token and base URL from the response body.
  accessToken = token["access_token"]
  baseURL = token["resource_server_base_uri"]
  
  # Create the URL that accesses the API.    
  apiURL = "/services/{version}/scripts/historyByName/{scriptPath}"
  uri = URI.parse(baseURL + apiURL)

  # Create the GET request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer #{accessToken}",
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json" }

  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port

  # Make the GET request an HTTPS GET request.
  http.use_ssl = true

  # Delete this line and be sure you have a valid certificate.
  # The default Ruby library, net/https, doesn't check the validity of a 
  # certificate during a handshake. Using VERIFY_NONE is a simple, and insecure,
  # hack to get around this limitation.
  http.verify_mode = OpenSSL::SSL::VERIFY_NONE

  # Make the request and store the response.
  response = http.request_get(uri.to_s, headers)

  # The data the API returns is in JSON, so parse it if it's valid.
  data = if response.body != ""
      JSON.parse(response.body)
    else
      "The response was empty."
  end

  # Now you can do something with the data the API returned.
end	

def PutSkillListManagement()
if($accessToken!='')
uri= URI.parse(($baseURI)+'/services/v18.0/skills/'+($skillId)+'/parameters/list-management')

headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "gzip,deflate"}
		body= {
        "displayField1Name"=> "",
		"displayField2Name": => "",
		"listOrderingOptions": {
		"orderType"=> "",
		"direction"=> ""
		}
    }
	params=body.to_json	
		# Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  res = http.put(uri.path ,params, headers)

   construct('Admin','Skills','PUT parameters/list-management','so32-v18.0',res.code,res.message)
else
construct('Admin','Skills','PUT parameters/list-management','so32-v18.0',res.code,res.message)
    end
end

def getSkillsParameters()
  unless !(token["access_token"] == nil && 
           token["resource_server_base_uri"] == nil)
    raise ArgumentError, "the function didn't receive a token it could understand"
  end
  
  accessToken = token["access_token"]
  baseURL = token["resource_server_base_uri"]
  
  apiURL = "/services/{version}/skills/parameters}"
  uri = URI.parse(baseURL + apiURL)

  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer #{accessToken}",
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json" }

  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port

  # Make the GET request an HTTPS GET request.
  http.use_ssl = true

  http.verify_mode = OpenSSL::SSL::VERIFY_NONE

  response = http.request_get(uri.to_s, headers)

  data = if response.body != ""
      JSON.parse(response.body)
    else
      "The response was empty."
  end

  # Now you can do something with the data the API returned.
end

def PutSkillParameterCadenceSettings()
if($accessToken!='')
uri= URI.parse(($baseURI)+'/services/v18.0/skills/'+($skillId)+'/parameters/cadence-settings')

headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "gzip,deflate"}
		body= 
				{
				"attemptMode"=> "string",
				"recordRequestMode"=> "string",
				"destinationRetryRestMinutes"=> 0,
				"maximumAttempts": {
				"fieldName"=> "string",
				"attempts"=> 0
				},
				"cadences" => : {
				"fieldName"=> "string",
				"attempts"=> 0,
				"timeConstraints": {
				"weekdayTimeConstraints": {
				"startTime"=> "string",
				"endTime"=> "string"
				},
				"weekendTimeConstraints":{
				"startTime"=> "string",
				"endTime"=> "string"
				}}}
        }
    }
	params=body.to_json	
		# Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  res = http.put(uri.path ,params, headers)

   construct('Admin','Skills','PUT parameters/cadence-settings','so32-v18.0',res.code,res.message)
else
construct('Admin','Skills','PUT parameters/cadence-settings','so32-v18.0',res.code,res.message)
    end
end

def getSkillsSkillIdParameters()
  unless !(token["access_token"] == nil && 
           token["resource_server_base_uri"] == nil)
    raise ArgumentError, "the function didn't receive a token it could understand"
  end
  
  accessToken = token["access_token"]
  baseURL = token["resource_server_base_uri"]
    
  apiURL = "/services/{version}/skills/"+$skillId+"/parameters}"
  uri = URI.parse(baseURL + apiURL)

  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer #{accessToken}",
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json" }

  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port

  # Make the GET request an HTTPS GET request.
  http.use_ssl = true

  http.verify_mode = OpenSSL::SSL::VERIFY_NONE

  response = http.request_get(uri.to_s, headers)

  data = if response.body != ""
      JSON.parse(response.body)
    else
      "The response was empty."
  end

  # Now you can do something with the data the API returned.
end

def PostSetDisposition()
 if($accessToken!='')
 uri = URI.parse(($baseURI)+'/services/v18/contacts/'+$contactId+'/set-disposition')
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
      "Content-Length" => "0",
      "Accept-Encoding" => "deflate"}
       body = {
			"dispositionInfo": {
			"Skill": "1007",
			"DispositionCode": "1",
			"CallbackNumber": "",
			"CallbackTime": "",
			"notes": "",
			"CommitmentAmount": ""
			}
}
  params= body.to_json
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host,uri.port
  construct('Admin','General','Post /set-disposition','so32-v18.0',res.code,res.message)
else
construct('Admin','General','Post /set-disposition','so32-v18.0',res.code,res.message)
    end
end

def getBusinessUnitOutboundRoutes()
  unless !(token["access_token"] == nil && 
           token["resource_server_base_uri"] == nil)
    raise ArgumentError, "the function didn't receive a token it could understand"
  end
  
  accessToken = token["access_token"]
  baseURL = token["resource_server_base_uri"]
    
  apiURL = "/services/{version}/business-unit/outbound-routes}"
  uri = URI.parse(baseURL + apiURL)

  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer #{accessToken}",
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json" }

  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port

  # Make the GET request an HTTPS GET request.
  http.use_ssl = true

  http.verify_mode = OpenSSL::SSL::VERIFY_NONE

  response = http.request_get(uri.to_s, headers)

  data = if response.body != ""
      JSON.parse(response.body)
    else
      "The response was empty."
  end

  # Now you can do something with the data the API returned.
end

def getclientdata()
  unless !(token["access_token"] == nil && 
           token["resource_server_base_uri"] == nil)
    raise ArgumentError, "the function didn't receive a token it could understand"
  end
  
  accessToken = token["access_token"]
  baseURL = token["resource_server_base_uri"]
    
  apiURL = "/services/{version}/agents/client-data"
  uri = URI.parse(baseURL + apiURL)
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer #{accessToken}",
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json" }

  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port

  # Make the GET request an HTTPS GET request.
  http.use_ssl = true

  http.verify_mode = OpenSSL::SSL::VERIFY_NONE

  response = http.request_get(uri.to_s, headers)

  data = if response.body != ""
      JSON.parse(response.body)
    else
      "The response was empty."
  end

  # Now you can do something with the data the API returned.
end

def putclientdata()
if($accessToken!='')
uri= URI.parse(($baseURI)+'/services/v19.0/agents/client-data')
headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "gzip,deflate"}
		body= 
		{
		$data = array(
		"agentId"=> 1001,
		"dataSet"=> array(
		"settings"=> "true" )
		)
    }
	params=body.to_json	
		# Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  res = http.put(uri.path ,params, headers)

   construct('Realtime','Realtime','PUT /agents/client-data','so32-v19.0',res.code,res.message)
else
construct('Realtime','Realtime','PUT /agents/client-data','so32-v19.0',res.code,res.message)
    end
end

def Postsmsoutbound($sessionId)
 if($accessToken!='')
 uri = URI.parse(($baseURI)+'/services/v19/agent-sessions/'+ sessionId +'/interactions/sms-outbound')
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
      "Content-Length" => "0",
      "Accept-Encoding" => "deflate"}
       body = {
			"phoneNumber": "",
			"skillId": 1000,
			"parentContactId": null
			}

  params= body.to_json
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host,uri.port
  construct('Agent','Sessions','Post /agent-sessions/{sessionId}/interactions/sms-outbound','so32-v19.0',res.code,res.message)
else
construct('Agent','Sessions','Post /agent-sessions/{sessionId}/interactions/sms-outbound','so32-v19.0',res.code,res.message)
    end
end

def removeprospects($sourceName)
if($accessToken!='')
uri= URI.parse(($baseURI)+'/services/v19.0/lists/call-lists/'+ ($sourceName) +'/removeProspects')

  # Create the Post request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "gzip,deflate"}
	  

  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  
  
  #http.use_ssl= true

  res = http.delete(uri.path,headers)

  construct('Admin','Lists','Delete /lists/call-lists/{sourceName}/removeProspects','v19.0',res.code,res.message)
  else
  construct('Admin','Lists','Delete /lists/call-lists/{sourceName}/removeProspects','v19.0',res.code,res.message)
end
    end

def putpersistentcontacts($contactId)
if($accessToken!='')
uri= URI.parse(($baseURI)+'/services/v19/persistent-contacts/'+ ($contactId))
headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "gzip,deflate"}
		body= 
		{
		$data = array(
		 "persistentContact"=> array(
		"SkillId"=> "",
		"TargetAgentId"=> "",
		"InitialPriority"=> "",
		"Acceleration"=> "",
		"MaxPriority"=> ""))
    }
	params=body.to_json	
		# Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  res = http.put(uri.path ,params, headers)

   construct('Admin','Contacts','PUT /persistent-contacts/{contactId}','so32-v19.0',res.code,res.message)
else
construct('Admin','Contacts','PUT /persistent-contacts/{contactId}','so32-v19.0',res.code,res.message)
    end
end

def getagentsettings($agentId)
  unless !(token["access_token"] == nil && 
           token["resource_server_base_uri"] == nil)
    raise ArgumentError, "the function didn't receive a token it could understand"
  end
  
  accessToken = token["access_token"]
  baseURL = token["resource_server_base_uri"]
    
  apiURL = "/services/v19/agents/" + ($agentId) + "/agent-settings"
  uri = URI.parse(baseURL + apiURL)
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer #{accessToken}",
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json" }

  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port

  # Make the GET request an HTTPS GET request.
  http.use_ssl = true

  http.verify_mode = OpenSSL::SSL::VERIFY_NONE

  response = http.request_get(uri.to_s, headers)

  data = if response.body != ""
      JSON.parse(response.body)
    else
      "The response was empty."
  end

  # Now you can do something with the data the API returned.
end

def getscriptsbyId($scriptId)
  unless !(token["access_token"] == nil && 
           token["resource_server_base_uri"] == nil)
    raise ArgumentError, "the function didn't receive a token it could understand"
  end
  
  accessToken = token["access_token"]
  baseURL = token["resource_server_base_uri"]
    
  apiURL = "/services/v19/scripts/" + ($scriptId)
  uri = URI.parse(baseURL + apiURL)
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer #{accessToken}",
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json" }

  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port

  # Make the GET request an HTTPS GET request.
  http.use_ssl = true

  http.verify_mode = OpenSSL::SSL::VERIFY_NONE

  response = http.request_get(uri.to_s, headers)

  data = if response.body != ""
      JSON.parse(response.body)
    else
      "The response was empty."
  end

  # Now you can do something with the data the API returned.
end

def getscriptssearch()
  unless !(token["access_token"] == nil && 
           token["resource_server_base_uri"] == nil)
    raise ArgumentError, "the function didn't receive a token it could understand"
  end
  
  accessToken = token["access_token"]
  baseURL = token["resource_server_base_uri"]
    
  apiURL = "/services/{version}/scripts/search}"
  uri = URI.parse(baseURL + apiURL)
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer #{accessToken}",
      "Content-Type" => "application/x-www-form-urlencoded",
      "Data-Type" => "json" }

  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port

  # Make the GET request an HTTPS GET request.
  http.use_ssl = true

  http.verify_mode = OpenSSL::SSL::VERIFY_NONE

  response = http.request_get(uri.to_s, headers)

  data = if response.body != ""
      JSON.parse(response.body)
    else
      "The response was empty."
  end

  # Now you can do something with the data the API returned.
end

def deletescripts()
if($accessToken!='')
uri= URI.parse(($baseURI)+'/services/v19.0/scripts')

  # Create the Post request headers.
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
	  "Content-Length" => "0",
	  "Accept-Encoding" => "gzip,deflate"}
	  

  # Set up an HTTP object.
  http = Net::HTTP.new uri.host, uri.port
  # puts headers
  # Make the request.
  
  
  #http.use_ssl= true

  res = http.delete(uri.path,headers)

  construct('Admin','General','Delete /scripts','v19.0',res.code,res.message)
  else
  construct('Admin','General','Delete /scripts','v19.0',res.code,res.message)
end
    end

def Posttransformnumbers($agentpatternId)
 if($accessToken!='')
 uri = URI.parse(($baseURI)+'/services/v19/agent-patterns/' + ($agentpatternId) + '/transform-phonenumbers')
  headers = 
    { "Accept" => "application/json, text/javascript, */*; q=0.01",
      "Authorization" => "bearer " + ($accessToken),
      "Content-Type" => "application/json",
      "Data-Type" => "json",
      "Content-Length" => "0",
      "Accept-Encoding" => "deflate"}
       body = {
			"phoneNumber": "",
			"skillId": 1000,
			"parentContactId": null
			}

  params= body.to_json
  # Set up an HTTP object.
  http = Net::HTTP.new uri.host,uri.port
  construct('Agent','Sessions','Post /agent-patterns/{agentpatternId}/transform-phonenumbers','so32-v19.0',res.code,res.message)
else
construct('Agent','Sessions','Post /agent-patterns/{agentpatternId}/transform-phonenumbers','so32-v19.0',res.code,res.message)
    end
end

end


init()
getaccesstoken()
getBusinessunitTimezone()
getSuppresedContacts()
PostSuppresedContacts()
PutbusinessunitTimezone()
PutSkillIDParameterTimezone
getTimeZoneBySkillId()
DeleteskillbyagentID
#version 16
getBusinessunitTimezone()
getSuppresedContacts()
PostSuppresedContacts()
PutbusinessunitTimezone()
PutSkillIDParameterTimezone
getTimeZoneBySkillId()
#version 16
PostJobSync()
getMediaPlayBack('chat','true')
getMediaPlayBackByID('chat','true')
getMediaPlayBackContacts('master id 1','chat','true')
getActiveFlag('1')
PostWorkflowData()
getWorkflowDataById()
PutWorkflowDataById()
PutWorkflowDataByIdActivate()
PutWorkflowDataByIdDeactivate()
getJobs()
PostJobs()
getJobsByID()
#v17
deleteSuppressedContactBySuppressedContactId()
getSuppressedContactBySuppressedContactId()
putSuppressedContactBySuppressedContactId()
getContactsByIdHierachy()
getScripts()
postScripts()
putScripts()
postScriptsKick()
getScriptsHistoryByName()
PutSkillListManagement()
getSkillsParameters()
PutSkillParameterCadenceSettings()
getSkillsSkillIdParameters()
PostSetDisposition()
getBusinessUnitOutboundRoutes()

#v19

getclientdata()
putcientdata()
postsmsoutbound('')
removeprospects('')
persistentcontacts(9471795)
agentsettings(12365)
getscriptsbyId(12358)
scriptssearch()
deletescripts()
transformnumbers(12365)


# getAgents('201217','','','','','','')
# getAccessKeys('27730','','','','')
#postAccessKeys('27730')
# getAccessKeysByID()
# DeleteAccesskeysByID()
# PostCampaigns()
# PutCampaignsByID()
# DeleteCampaignsBySkill()
# PostCampaignsBySkill()
# getSmsHistoricalTranscript('10001')
# getSmsHistoricalContact('4005150002','10001','1268')
# getdisposition('','','','','','','')
# PostHoursofOperation()
# PutHoursofOperationByID()
# PostPointofContact()
# PutPointofContactByID()
# PostUnavailableCode()
# getPhonenumbers()
# PutUnavailableCodeByID()
# PutUnavailableCodeByIDTeams()
# PostEmailSaveDraft()
# PostAddText()

# PostAgent()
# PostAgentByID()
# GetagentbyAgentIDV13('')
# getTeams('','','','','','','')
# postTeam()
# getTeamById('')
# PutAgentbyAgentIDV13()
# PutTeambyTeamIDV13()
# getUnavailableCodesByTeam('')
# PutUnavailableCodesbyTeamID()
# getUnavailableCodes()
# getSkills('','','','','','','','','')
# PostCreateSkillV13()
# getSkillById()
# PutSkillbySkillID()
# GetSkillParameterGeneralSettingV13('')
# PutSkillParameterGeneralSettingv13()
# getSkillCpamanagement('')
#PutSkillParameterCpamanagement()
# getSkillDeliveryPreferences('')
# PutSkillParameterDeliveryPreferences()
# getSkillRetrysettings('')
# PutSkillParameterRetrySettings()
# getSkillScheduleSettings()
# PutSkillParameterScheduleSettings()
# getSkillxssettings('')
# PutSkillParameterXssettings()