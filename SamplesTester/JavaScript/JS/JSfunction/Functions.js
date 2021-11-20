//var baseURI = "https://api-so32.ucnlabext.com/inContactAPI";
var baseURI = 'https://api-so32.staging.nice-incontact.com/InContactAPI'
var CXone_baseURI='https://na1.staging.nice-incontact.com'

var accessToken ;

function getAccesstoken(){ 
        //var createAuthenticationPayload = {'grant_type': 'password','password': 'Qwerty@123','username' :'nikhil.m@so32.com'}
		//var createAuthenticationPayload = {'grant_type': 'password','password': 'Servion@123','username' :'rafiq.j@hc8.com'}
		var createAuthenticationPayload = {'grant_type': 'password','password': 'Welcome@123','username' :'k.karishma@servion.com'}
        //var baseURI='https://api-so32.ucnlabext.com/';
		var baseURI = 'https://api-so32.staging.nice-incontact.com/'
        $.ajax({
            //The baseURI variable is created by the result.resource_server_base_uri,
            //which is returned when getting a token and should be used to create the URL base
            'url': baseURI + 'InContactAuthorizationServer/Token',
            'type': 'POST',
            'headers': {
                //Use access_token previously retrieved from inContact token service
                'Authorization': 'basic ' + 'aW50ZXJuYWxAaW5Db250YWN0IEluYy46UVVFNVFrTkdSRE0zUWpFME5FUkRSamczUlVORFJVTkRRakU0TlRrek5UYz0=',
                'content-Type': 'application/json'
            },
            'data': JSON.stringify(createAuthenticationPayload),
            'success': function (result, status, statusCode) {
                sessionStorage.setItem("token", result.access_token);
				accessToken = sessionStorage.getItem("token");
				GenerateOutput();
            },
            'error': function (XMLHttpRequest, textStatus, errorThrown) {
                //Process error actions
                return false;
            }
        });
    }


//Admin --> v13
function getAgents(updatedSince,isActive,searchString,fields,skip,top,orderBy) {
						
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v13.0/agents' + '?updatedSince=' + updatedSince + '&isActive=' + isActive + '&searchString=' + searchString + '&fields=' + fields + '&skip' + skip + '&top=' + top + '&orderBy=' + orderBy ,
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
        },
        //'data': getAgentsPayload,
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Agents","GET /agents","so32-v13.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Agents","GET /agents","so32-v13.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}

function PostAgentV13() { 
    var PostAgentJSON ={
        "agents": [
          {
              "firstName": "Firstso32",
              "middleName": "Midso32",
              "lastName": "Lastso32",
              "teamId": "",
              "reportToId": 1,
              "internalId": "",
              "profileId": 0,
              "roleId": "",
              "password": "",
              "forceChangeOnLogon": true,
              "emailAddress": "",
              "userName": "",
              "userId": "",
              "timeZone": "(GMT-07:00) Arizona",
              "country": "",
              "state": "",
              "city": "",
              "chatRefusalTimeout": 15,
              "phoneRefusalTimeout": 15,
              "workItemRefusalTimeout": 15,
              "defaultDialingPattern": 0,
              "maxConcurrentChats": 1,
              "useTeamMaxConcurrentChats": false,
              "isActive": true,
              "locationId": 0,
              "notes": "",
              "hireDate": "10/10/1800",
              "terminationDate": "10/10/1800",
              "hourlyCost": 0,
              "rehireStatus": true,
              "employmentType": 1,
              "referral": "",
              "atHome": false,
              "hiringSource": 0,
              "ntLoginName": "",
              "custom1": "",
              "custom2": "",
              "custom3": "",
              "custom4": "",
              "custom5": "",
              "scheduleNotification": 0,
              "federatedId": "",
              "maxEmailAutoParkingLimit": 1,
              "useTeamEmailAutoParkingLimit": false,
              "sipUser": "",
              "systemUser": "",
              "systemDomain": "",
              "crmUserName": "",
              "useAgentTimeZone": false,
              "timeDisplayFormat": 0,
              "sendEmailNotifications": false,
              "apiKey": "",
              "telephone1": "",
              "telephone2": "",
              "userType": "",
              "isWhatIfAgent": false,
              "requestContact": false,
              "useTeamRequestContact": false,
              "chatThreshold": 1,
              "useTeamChatThreshold": false,
              "emailThreshold": 1,
              "useTeamEmailThreshold": false,
              "workItemThreshold": 1,
              "useTeamWorkItemThreshold": false,
              "contactAutoFocus": false,
              "useTeamContactAutoFocus": false,
              "subject": "",
              "issuer": "",
              "isOpenIdProfileComplete": false,
              "recordingNumbers": [
                {
                    "number": ""
                }
              ]
          }
        ]
    }

    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v13.0/agents',
        'type': 'POST',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
        },
        'data': JSON.stringify(PostAgentJSON),
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Agents","POST /agents","so32-v13.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Agents","POST /agents","so32-v13.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}

function GetagentbyAgentIDV13(AgentId,fields){
 
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v13.0/agents/' + AgentId + '?fields=' + fields,
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
        },
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Agents","GET /agents/{agentId}","so32-v13.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Agents","GET /agents/{agentId}","so32-v13.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}

function PutAgentbyAgentIDV13(AgentID){
    var AgentJsonData = {
        "agents": [
          {
              "agentId": 1140,
              "userName": "vijiesh@hc8.com",
              "firstName": "viji",
              "middleName": "",
              "lastName": "hc8.com",
              "userId": null,
              "emailAddress": "vijayalakshmi.s@servion.com",
              "isActive": true,
              "teamId": 11079,
              "teamName": "Testadmin",
              "reportToId": 0,
              "reportToName": "",
              "isSupervisor": true,
              "lastLogin": "2017-02-28T09:22:38.430Z",
              "lastUpdated": "2017-04-05T10:32:22.583Z",
              "location": null,
              "custom1": "",
              "custom2": "",
              "custom3": "",
              "custom4": "",
              "custom5": "",
              "internalId": "",
              "profileId": 41,
              "profileName": "vgsecurity profile_1",
              "timeZone": "(GMT+05:30) Chennai, Kolkata, Mumbai, New Delhi",
              "country": "US",
              "countryName": "United States",
              "state": "Alabama",
              "city": "Chennai",
              "chatRefusalTimeout": null,
              "phoneRefusalTimeout": null,
              "workItemRefusalTimeout": null,
              "defaultDialingPattern": null,
              "defaultDialingPatternName": null,
              "useTeamMaxConcurrentChats": false,
              "maxConcurrentChats": 3,
              "notes": "",
              "createDate": "2015-06-08T07:28:02.140Z",
              "inactiveDate": null,
              "hireDate": null,
              "terminationDate": null,
              "hourlyCost": 0,
              "rehireStatus": true,
              "employmentType": null,
              "employmentTypeName": "",
              "referral": "",
              "atHome": false,
              "hiringSource": "",
              "ntLoginName": "",
              "scheduleNotification": 15,
              "federatedId": null,
              "useTeamEmailAutoParkingLimit": true,
              "maxEmailAutoParkingLimit": 3,
              "sipUser": null,
              "systemUser": null,
              "systemDomain": null,
              "crmUserName": null,
              "useAgentTimeZone": false,
              "timeDisplayFormat": "12 hour",
              "sendEmailNotifications": false,
              "apiKey": null,
              "telephone1": "",
              "telephone2": "",
              "userType": "Administrator",
              "isWhatIfAgent": false,
              "timeZoneOffset": "05:30",
              "requestContact": false,
              "chatThreshold": 0,
              "useTeamChatThreshold": false,
              "emailThreshold": 0,
              "useTeamEmailThreshold": false,
              "workItemThreshold": 0,
              "useTeamWorkItemThreshold": false,
              "contactAutoFocus": false,
              "useTeamContactAutoFocus": false,
              "useTeamRequestContact": false,
              "voiceThreshold": 0,
              "recordingNumbers": [],
              "subject": null,
              "issuer": null,
              "isOpenIdProfileComplete": false,
              "teamUuid": null
          }
        ]
    }

    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
 
        'url': baseURI + '/services/v13.0/agents/' + AgentID,
        'type': 'PUT',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8',
            'Accept':'application/json, text/javascript, */*; q=0.01'
        },
        'data': JSON.stringify(AgentJsonData),
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Agents","PUT /agents/{agentId}","so32-v13.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Agents","PUT /agents/{agentId}","so32-v13.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}

function getTeams(fields,updatedSince,isActive,searchString,skip,top,orderBy) {
	
    var queryString = "?fields=" + fields + "&updatedSince=" + updatedSince + "&isActive=" + isActive + 
                "&searchString=" + searchString + "&skip=" + skip + 
                "&top=" + top + "&orderBy=" + orderBy;
				
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v13.0/teams'+queryString,
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
            //'content-Type': 'application/x-www-form-urlencoded'
        },
        //'data': JSON.stringify(queryString),
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Agents","GET /teams","so32-v13.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Agents","GET /teams","so32-v13.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}

function postTeam(){
    var TeamJsonData = {
        "teams": [
          {
              "teamName": "team",
              "isActive": true,
              "maxConcurrentChats": 8,
              "wfoEnabled": false,
              "wfm2Enabled": false,
              "qm2Enabled": false,
              "inViewEnabled": false,
              "notes": "",
              "maxEmailAutoParkingLimit": 25,
              "inViewGamificationEnabled": false,
              "inViewChatEnabled": false,
              "inViewLMSEnabled": false,
              "analyticsEnabled": false,
              "requestContact": false,
              "chatThreshold": 1,
              "emailThreshold": 1,
              "workItemThreshold": 1,
              "voiceThreshold": 1,
              "contactAutoFocus": false,
              "teamUuid": ""
          }
        ]
    }

    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v13.0/teams',
        'type': 'POST',
        'async': false,
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
        },
        'data': JSON.stringify(TeamJsonData),
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Agents","POST /teams","so32-v13.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Agents","POST /teams","so32-v13.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}

function getTeamById(teamId,fields) {
    
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v13.0/teams/' + teamId + '?fields=' + fields,
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
        },
        
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Agents","GET /teams/{teamId}'","so32-v13.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Agents","GET /teams/{teamId}","so32-v13.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}

function PutTeambyTeamIDV13(Teamid) {
    var TeamJsonData = {
        "forceInactive": false,
        "team": {
            "teamName": "TeamName",
            "isActive": true,
            "maxConcurrentChats": 8,
            "wfoEnabled": false,
            "wfm2Enabled": false,
            "qm2Enabled": false,
            "inViewEnabled": false,
            "notes": "",
            "maxEmailAutoParkingLimit": 25,
            "inViewGamificationEnabled": false,
            "inViewChatEnabled": false,
            "inViewLMSEnabled": false,
            "analyticsEnabled": false,
            "requestContact": false,
            "chatThreshold": 1,
            "emailThreshold": 1,
            "workItemThreshold": 1,
            "voiceThreshold": 1,
            "contactAutoFocus": false,
            "teamUuid": ""
        }
    }
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v13.0/teams/' + Teamid,
        'type': 'PUT',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
        },
        'data': JSON.stringify(TeamJsonData),
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Agents","PUT /teams/{teamId}","so32-v13.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Agents","PUT /teams/{teamId}","so32-v13.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}
function getUnavailableCodesByTeam(teamId) {
    var getUnavailableCodesByTeamPayload = {
        'activeOnly': 'boolean'
    }
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v13.0/teams/' + teamId + '/unavailable-codes',
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
        },
        'data': getUnavailableCodesByTeamPayload,
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Agents","GET /teams/{teamId}/unavailable-codes","so32-v13.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Agents","GET /teams/{teamId}/unavailable-codes","so32-v13.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}
function PutUnavailableCodesbyTeamID(Teamid) {
    var unavailablecodeJsonData = {
        "unavailableCodes": [
          {
              "outStateId": "0"
          }
        ]
    }
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v13.0/teams/' + Teamid +"/unavailable-codes",
        'type': 'PUT',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
        },
        'data': JSON.stringify(unavailablecodeJsonData),
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Agents","PUT /teams/{teamId}/unavailable-codes","so32-v13.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Agents","PUT /teams/{teamId}/unavailable-codes","so32-v13.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}

function getUnavailableCodes() {
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v13.0/unavailable-codes',
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
        },
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","General","GET /unavailable-codes","so32-v13.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","General","GET /unavailable-codes","so32-v13.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}
function getSkills() {
    var getSkillsPayload = {
        'updatedSince': 'ISO 8601 formatted date/time',
        'mediaTypeId': 'integer',
        'outboundStrategy': 'string',
        'isActive': 'boolean',
        'searchString': 'string',
        'fields': 'string',
        'skip': 'integer',
        'top': 'integer',
        'orderBy': 'string'
    }
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v13.0/skills',
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
        },
        'data': getSkillsPayload,
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Skills","GET /skills","so32-v13.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Skills","GET /skills","so32-v13.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}
function PostCreateSkillV13(){
    var SkillJSONData = {
        "skills": [
          {
              "mediaTypeId": 4,
              "skillName": "",
              "isOutbound": true,
              "outboundStrategy": "Personal Connection",
              "campaignId": 1,
              "callerIdOverride": "8015554422",
              "emailFromAddress": "test@test.com",
              "emailFromEditable": false,
              "emailBccAddress": "",
              "scriptId": 2,
              "reskillHours": 4,
              "minWFIAgents": 4,
              "interruptible": false,
              "enableParking": false,
              "minWorkingTime": 4,
              "agentless": false,
              "agentlessPorts": 6,
              "notes": "this is a test note",
              "acwTypeId": 3,
              "requireDisposition": false,
              "allowSecondaryDisposition": false,
              "scriptDisposition": false,
              "stateIdACW": 2,
              "maxSecondsACW": 3,
              "acwPostTimeoutStateId": 53,
              "agentRestTime": 4,
              "displayThankyou": false,
              "thankYouLink": "no",
              "popThankYou": true,
              "popThankYouURL": "tester.com",
              "makeTranscriptAvailable": true,
              "transcriptFromAddress": "fromMe@email.com",
              "priorityBlending": false,
              "callSuppressionScriptId": 4,
              "useScreenPops": true,
              "screenPopTriggerEvent": "popTriggerEvent",
              "useCustomScreenPops": false,
              "screenPopType": "webpage",
              "screenPopDetails": "http://not",
              "initialPriority": 4,
              "acceleration": 5,
              "maxPriority": 10,
              "serviceLevelThreshold": 51,
              "serviceLevelGoal": 24,
              "enableShortAbandon": true,
              "shortAbandonThreshold": 123,
              "countShortAbandons": true,
              "countOtherAbandons": true,
              "chatWarningTheshold": 0,
              "agentTypingIndicator": false,
              "patronTypingPreview": false,
              "smsTransportCodeId": null,
              "messageTemplateId": null,
              "deliverMultipleNumbersSerially": false,
              "cradleToGrave": false,
              "priorityInterrupt": false,
              "treatProgressAsRinging": false,
              "preConnectCPAEnabled": false,
              "agentOverrideFax": true,
              "agentOverrideAnsweringMachine": true,
              "agentOverrideBadNumber": true,
              "dispositions": [
                {
                    "dispositionId": 1,
                    "priority": 1
                }
              ]
          }
        ]
    }
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v13.0/skills',
        'type': 'POST',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
        },
        'data': JSON.stringify(SkillJSONData),
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Skills","POST /skills","so32-v13.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Skills","POST /skills","so32-v13.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}

function getSkillById(skillId) {
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v13.0/skills/' + skillId,
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/x-www-form-urlencoded'
        },
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Skills","GET /skills/{skillId}","so32-v13.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Skills","GET /skills/{skillId}","so32-v13.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}
function PutSkillbySkillID(skillId){
    var SkillJsonData = {
        "skill": {
            "skillName": "string",
            "isActive": true,
            "campaignId": 2,
            "callerIdOverride": "string",
            "emailFromAddress": "test@test.com",
            "emailFromEditable": false,
            "emailBccAddress": "",
            "scriptId": 2,
            "reskillHours": 4,
            "minWFIAgents": 4,
            "interruptible": false,
            "enableParking": false,
            "minWorkingTime": 4,
            "agentless": false,
            "agentlessPorts": 6,
            "notes": "this is a test note",
            "acwTypeId": 3,
            "requireDisposition": false,
            "allowSecondaryDisposition": false,
            "scriptDisposition": false,
            "stateIdACW": 2,
            "maxSecondsACW": 3,
            "acwPostTimeoutStateId": 53,
            "agentRestTime": 4,
            "displayThankyou": false,
            "thankYouLink": "no",
            "popThankYou": true,
            "popThankYouURL": "tester.com",
            "makeTranscriptAvailable": true,
            "transcriptFromAddress": "fromMe@email.com",
            "priorityBlending": false,
            "callSuppressionScriptId": 4,
            "useScreenPops": true,
            "screenPopTriggerEvent": "bleh",
            "useCustomScreenPops": false,
            "screenPopType": "webpage",
            "screenPopDetails": "http://no",
            "initialPriority": 4,
            "acceleration": 5,
            "maxPriority": 10,
            "serviceLevelThreshold": 51,
            "serviceLevelGoal": 24,
            "enableShortAbandon": true,
            "shortAbandonThreshold": 123,
            "countShortAbandons": true,
            "countOtherAbandons": true,
            "chatWarningTheshold": 0,
            "agentTypingIndicator": false,
            "patronTypingPreview": false,
            "smsTransportCodeId": null,
            "messageTemplateId": null,
            "deliverMultipleNumbersSerially": false,
            "cradleToGrave": false,
            "priorityInterrupt": false,
            "treatProgressAsRinging": false,
            "preConnectCPAEnabled": false,
            "agentOverrideFax": true,
            "agentOverrideAnsweringMachine": true,
            "agentOverrideBadNumber": true,
            "dispositions": [
              {
                  "dispositionId": 1,
                  "priority": 1
              }
            ]
        }
    }
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v13.0/skills/' +skillId,
        'type': 'PUT',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8',
            'Accept':'application/json, text/javascript, */*; q=0.01'
        },
        'data': JSON.stringify(SkillJsonData),
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Skills","PUT /skills/{skillId}","so32-v13.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Skills","PUT /skills/{skillId}","so32-v13.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}
function GetSkillParameterGeneralSettingV13(skillId,fields){
	
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v13.0/skills/'+ skillId+ '/parameters/general-settings'+ '?fields='+ fields,
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/x-www-form-urlencoded'
        },
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Skills","GET parameters/general-settings","so32-v13.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Skills","GET parameters/general-settings","so32-v13.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}	
function PutSkillParameterGeneralSettingv13(skillId){
    var SkillJsonData = {
        "generalSettings": {
            "minimumRetryMinutes": 12,
            "maximumAttempts": 10,
            "defaultContactExpiration": 10,
            "getPriorityContactsOnContactinsertion": false,
            "loadCallbacks": false,
            "loadFresh": false,
            "loadNonFresh": false,
            "overrideBusinessUnitAbandonRate": false,
            "maximumRingingDuration": 1,
            "beginDampenPercentage": 1,
            "abandonRateCutoff": 1,
            "abandonRateThreshold": 1,
            "inactiveBlenderTimer": 1,
            "maximumRatio": 1,
            "aggressiveness": "conservative",
            "endOfListNotificationsDelay": 15,
            "notifyAgentsWhenListIsEmpty": false,
            "percentageOfAgentsBeforeOverdial": 5,
            "blockMultipleCalls": true,
            "consecutiveAttemptsWithoutALiveConnect": 5
        }
    }
 
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v13.0/skills/' + skillId + '/parameters/general-settings',
        'type': 'PUT',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8',
            'Accept':'application/json, text/javascript, */*; q=0.01'
        },
        'data': JSON.stringify(SkillJsonData),
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Skills","PUT /skills/{skillId}/parameters/general-settings","so32-v13.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Skills","PUT /skills/{skillId}/parameters/general-settings","so32-v13.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}
function getSkilCpamanagement(SkillId) {
    var getCpaPayload = {
        'fields': 'string'
    }
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v13.0/skills/'+SkillId+'/parameters/cpa-management',
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/x-www-form-urlencoded'
        },
        'data': getCpaPayload,
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Skills","GET services/v13.0/skills'","so32-v13.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Skills","GET services/v13.0/skills'","so32-v13.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}
function PutSkillParameterCpamanagement(skillId){
    var CpaJsonData = {
        "cpaSettings": {
            "abandonMessagePath": "string",
            "abandonMsgMode": 0,
            "abandonTimeout": 0,
            "agentNoResponseSeconds": 0,
            "agentOverrideOptionAnsweringMachine": true,
            "agentOverrideOptionBadNumber": true,
            "agentOverrideOptionFax": true,
            "agentResponseUtteranceMinimumSeconds": 0,
            "agentVoiceThreshold": 0,
            "ansMachineDetMode": 0,
            "ansMachineMsg": "string",
            "customerLiveSilenceSeconds": 0,
            "customerVoiceThreshold": 0,
            "enableCPALogging": true,
            "exceptions": [
              {
                  "attempt_No": 0,
                  "ansMachineDetMode": 0,
                  "ansMachineMsg": "string"
              }
            ],
            "machineEndSilenceSeconds": 0,
            "machineEndTimeoutSeconds": 0,
            "machineMinimumWithAgentSeconds": 0,
            "machineMinimumWithoutAgentSeconds": 0,
            "preConnectCPAEnabled": true,
            "preConnectCPARecording": true,
            "treatProgressAsRinging": true,
            "utteranceMinimumSeconds": 0
        }
    }

    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v13.0/skills/' + skillId + '/parameters/cpa-management',
        'type': 'PUT',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8',
            'Accept':'application/json, text/javascript, */*; q=0.01'
        },
        'data': JSON.stringify(CpaJsonData),
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Skills","GET services/v13.0/skills'","so32-v13.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Skills","GET services/v13.0/skills'","so32-v13.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}
function getSkillxssettings(SkillId) {
    var getXssettingsPayload = {
        'fields': 'string'
    }
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v13.0/skills/'+SkillId+'/parameters/xs-settings',
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/x-www-form-urlencoded'
        },
        'data': getXssettingsPayload,
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Skills","GET services/v13.0/skills'","so32-v13.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Skills","GET services/v13.0/skills'","so32-v13.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}
function PutSkillParameterXssettings(skillId){

    var XssettingsJsonData =	{
        "xsSettings": {
            "xsScriptID": 0,
            "xsCheckinScriptID": 0,
            "externalOutboundSkill_No": "string",
            "xsSkillChangedActive": true,
            "xsGetContactsActive": true,
            "xsFreshThreshold": 0,
            "xsAvailableThreshold": 0,
            "xsReadyThreshold": 0,
            "xsNumberToRetrieve": 0
        }
    }
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/{version}/skills/' + skillId + '/parameters/xs-settings',
        'type': 'PUT',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8',
            'Accept':'application/json, text/javascript, */*; q=0.01'
        },
        'data': JSON.stringify(XssettingsJsonData),
        'success': function (result, status, statusCode) {
            //Process success actions
            return result;
			
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            return false;
			
        }
    });
}
function getSkillDeliveryPreferences(SkillId) {
    var getDeliveryPrefPayload = {
        'fields': 'string'
    }
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v13.0/skills/'+SkillId+'/parameters/delivery-preferences',
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/x-www-form-urlencoded'
        },
        'data': getDeliveryPrefPayload,
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Skills","GET services/v13.0/skills'","so32-v13.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Skills","GET services/v13.0/skills'","so32-v13.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}
function PutSkillParameterDeliveryPreferences(skillId){
	
    var DeliveryPrefJsonData =	{
        "deliveryPreferences": {
            "confirmationRequiredDisabled": true,
            "confirmationRequiredDeliveryType": 0,
            "confirmationRequiredTimeout": 0,
            "confirmationRequiredTimeoutSubsequent": 0,
            "confirmationRequiredDefaultAccept": true,
            "confirmationRequiredDefault": true,
            "complianceRecordsDisabled": true,
            "complianceRecordsDeliveryType": 0,
            "complianceRecordsTimeout": 0,
            "complianceRecordsTimeoutSubsequent": 0,
            "complianceRecordsDefaultAccept": true,
            "showComplianceButtonReschedule": true,
            "showComplianceButtonRequeue": true,
            "showComplianceButtonSnooze": true,
            "showComplianceButtonDisposition": true,
            "showPreviewButtonReschedule": true,
            "showPreviewButtonRequeue": true,
            "showPreviewButtonSnooze": true,
            "showPreviewButtonDisposition": true
        }
    }
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v13.0/skills/' + skillId + '/parameters/delivery-preferences',
        'type': 'PUT',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8',
            'Accept':'application/json, text/javascript, */*; q=0.01'
        },
        'data': JSON.stringify(DeliveryPrefJsonData),
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Skills","GET services/v13.0/skills'","so32-v13.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Skills","GET services/v13.0/skills'","so32-v13.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}
function getSkillRetrysettings(SkillId) {
    var getRetrysettingsPayload = {
        'fields': 'string'
    }
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v13.0/skills/'+SkillId+'/parameters/retry-settings',
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/x-www-form-urlencoded'
        },
        'data': getRetrysettingsPayload,
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Skills","GET services/v13.0/skills'","so32-v13.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Skills","GET services/v13.0/skills'","so32-v13.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}
function PutSkillParameterRetrySettings(skillId){
    var RetrySettingsJsonData = {
        "retrySettings": {
            "loadNonFresh": true,
            "finalizeWhenExhausted": true,
            "maximumAttempts": 0,
            "minimumRetryMinutes": 0,
            "maximumNumberOfHandledCalls": 0,
            "restrictedCallingMinutes": 0,
            "restrictedCallingMaxAttempts": 0,
            "generalStaleMinutes": 0,
            "callbackRestMinutes": 0,
            "releaseAgentSpecificCalls": true,
            "maximumNumberOfCallbacks": 0,
            "callbackStaleMinutes": 0
        }
    }
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v13.0/skills/' + skillId + '/parameters/retry-settings',
        'type': 'PUT',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8',
            'Accept':'application/json, text/javascript, */*; q=0.01'
        },
        'data': JSON.stringify(RetrySettingsJsonData),
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Skills","GET services/v13.0/skills'","so32-v13.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Skills","GET services/v13.0/skills'","so32-v13.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}
function getSkillScheduleSettings(SkillId) {
	
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v13.0/skills/'+SkillId+'/parameters/schedule-settings',
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/x-www-form-urlencoded'
        },
		
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Skills","GET services/v13.0/skills'","so32-v13.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Skills","GET services/v13.0/skills'","so32-v13.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}
function PutSkillParameterScheduleSettings(skillId){
    var ScheduleSettingsJsonData = {
        "scheduleSettings": {
            "isScheduled": true,
            "sundayStartTime": "string",
            "sundayEndTime": "string",
            "sundayIsActive": true,
            "mondayStartTime": "string",
            "mondayEndTime": "string",
            "mondayIsActive": true,
            "tuesdayStartTime": "string",
            "tuesdayEndTime": "string",
            "tuesdayIsActive": true,
            "wednesdayStartTime": "string",
            "wednesdayEndTime": "string",
            "wednesdayIsActive": true,
            "thursdayStartTime": "string",
            "thursdayEndTime": "string",
            "thursdayIsActive": true,
            "fridayStartTime": "string",
            "fridayEndTime": "string",
            "fridayIsActive": true,
            "saturdayStartTime": "string",
            "saturdayEndTime": "string",
            "saturdayIsActive": true
        }
    }
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v13.0/skills/' + skillId + '/parameters/schedule-settings',
        'type': 'PUT',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8',
            'Accept':'application/json, text/javascript, */*; q=0.01'
        },
        'data': JSON.stringify(ScheduleSettingsJsonData),
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Skills","GET services/v13.0/skills'","so32-v13.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Skills","GET services/v13.0/skills'","so32-v13.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}
//Admin --> AddressBook v12
function getStandardAddressBookEntries(addressBookId,updatedSince,searchString,fields,skip,top,orderBy){
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v12.0/address-books/' + addressBookId + '/entries' + '?fields=' + fields + '&updatedSince=' + updatedSince + '&searchString=' + searchString + '&fields=' + fields + '&skip' + skip + '&top=' + top + '&orderBy=' + orderBy ,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/x-www-form-urlencoded',
			'Access-Control-Allow-Origin' : '*',
			async:true
		},
		'success': function (result, status, statusCode) {
			//Process success actions
		ConstructArray("Admin","AddressBook","GET services/v12.0/address-books/{addressBookId}/entries'","SC1-v12.0",'T'+statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","AddressBook","GET services/v12.0/address-books/' + addressBookId + '/entries'","SC1-v12.0",'F'+XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
//Admin --> Agent v12	
function PostAgent() { //done
	
  var PostAgentPayload ={
  "agents": [
    {
      "firstName": "so32",
      "middleName": "so32",
      "lastName": "",
      "teamId": "",
      "reportToId": 1,
      "internalId": "",
      "profileId": 0,
      "roleId": "",
      "password": "",
      "forceChangeOnLogon": true,
      "emailAddress": "",
      "userName": "",
      "userId": "",
      "timeZone": "(GMT-07:00) Arizona",
      "country": "",
      "state": "",
      "city": "",
      "chatRefusalTimeout": 15,
      "phoneRefusalTimeout": 15,
      "workItemRefusalTimeout": 15,
      "defaultDialingPattern": 0,
      "maxConcurrentChats": 1,
      "useTeamMaxConcurrentChats": false,
      "isActive": true,
      "locationId": 0,
      "notes": "",
      "hireDate": "10/10/1800",
      "terminationDate": "10/10/1800",
      "hourlyCost": 0,
      "rehireStatus": true,
      "employmentType": 1,
      "referral": "",
      "atHome": false,
      "hiringSource": 0,
      "ntLoginName": "",
      "custom1": "",
      "custom2": "",
      "custom3": "",
      "custom4": "",
      "custom5": "",
      "scheduleNotification": 0,
      "federatedId": "",
      "maxEmailAutoParkingLimit": 1,
      "useTeamEmailAutoParkingLimit": false,
      "sipUser": "",
      "systemUser": "",
      "systemDomain": "",
      "crmUserName": "",
      "useAgentTimeZone": false,
      "timeDisplayFormat": 0,
      "sendEmailNotifications": false,
      "apiKey": "",
      "telephone1": "",
      "telephone2": "",
      "userType": "",
      "isWhatIfAgent": false,
      "requestContact": false,
      "useTeamRequestContact": false,
      "chatThreshold": 1,
      "useTeamChatThreshold": false,
      "emailThreshold": 1,
      "useTeamEmailThreshold": false,
      "workItemThreshold": 1,
      "useTeamWorkItemThreshold": false,
      "contactAutoFocus": false,
      "useTeamContactAutoFocus": false,
      "subject": "",
      "issuer": "",
      "isOpenIdProfileComplete": false,
      "recordingNumbers": [
        {
          "number": ""
        }
      ]
    }
  ]
}
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v12.0/agents',
        'type': 'POST',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
        },
        'data': JSON.stringify(PostAgentPayload),
        'success': function (result, status, statusCode) {
            //Process success actions
			ConstructArray("Admin","AddressBook","POST services/v12.0/agents","SC1-v12.0",statusCode.status + ":" + statusCode.statusTex);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
			ConstructArray("Admin","AddressBook","POST services/v12.0/agents","SC1-v12.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
        }
    });
}
	
//Admin--> Agent 11

function getAgentSkillsByAgentId(AgentID,updatedSince,searchString,fields,skip,top,orderBy,MediaTypeId,outboundStrategy){ // done

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/agents/' + AgentID + '/skills'+ '?fields=' + fields + '&updatedSince=' + updatedSince + 
                '&searchString=' + searchString + '&fields=' + fields + '&skip' + skip + '&top=' + top + '&orderBy=' + orderBy + '&MediaTypeId=' + MediaTypeId + '&outboundStrategy=' + outboundStrategy ,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/x-www-form-urlencoded',
			'Access-Control-Allow-Origin' : '*',
			async:true
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","Agent","GET agents/{agentId}/skills","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","Agent","GET agents/{agentId}/skills","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function PutAgentbyAgentID(AgentID){ //done
	var getAllAgentsSkillsPayload = {
  "agent": {
    "firstName": "String",
    "middleName": "",
    "lastName": "String",
    "teamId": "0",
    "reportToId": 0,
    "internalId": "",
    "profileId": 8,
    "emailAddress": "sample@abc.com",
    "userName": "String",
    "userId": "Integer",
    "timeZone": "(GMT-07:00) Arizona",
    "country": "",
    "state": "",
    "city": "",
    "chatRefusalTimeout": 15,
    "phoneRefusalTimeout": 15,
    "workItemRefusalTimeout": 15,
    "defaultDialingPattern": 0,
    "maxConcurrentChats": 1,
    "useTeamMaxConcurrentChats": false,
    "isActive": true,
    "locationId": 0,
    "notes": "",
    "hireDate": "10/10/1800",
    "terminationDate": "10/10/1800",
    "hourlyCost": 0,
    "rehireStatus": true,
    "employmentType": 1,
    "referral": "",
    "atHome": false,
    "hiringSource": 0,
    "ntLoginName": "",
    "custom1": "",
    "custom2": "",
    "custom3": "",
    "custom4": "",
    "custom5": "",
    "scheduleNotification": 0,
    "federatedId": "",
    "maxEmailAutoParkingLimit": 1,
    "useTeamEmailAutoParkingLimit": false,
    "sipUser": "",
    "systemUser": "",
    "systemDomain": "",
    "crmUserName": "",
    "useAgentTimeZone": false,
    "timeDisplayFormat": 0,
    "sendEmailNotifications": false,
    "apiKey": "",
    "telephone1": "",
    "telephone2": "",
    "userType": "agent",
    "isWhatIfAgent": false,
    "requestContact": false,
    "useTeamRequestContact": false,
    "chatThreshold": 1,
    "useTeamChatThreshold": false,
    "emailThreshold": 1,
    "useTeamEmailThreshold": false,
    "workItemThreshold": 1,
    "useTeamWorkItemThreshold": false,
    "contactAutoFocus": false,
    "useTeamContactAutoFocus": false,
    "subject": "",
    "issuer": "",
    "isOpenIdProfileComplete": false,
    "recordingNumbers": [
      {
        "number": ""
      }
    ]
  }
}

    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v11.0/agents/' + AgentID,
        'type': 'PUT',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8',
			'Accept':'application/json, text/javascript, */*; q=0.01'
        },
        'data': JSON.stringify(getAllAgentsSkillsPayload),
        'success': function (result, status, statusCode) {
            //Process success actions
            return result;
			ConstructArray("Admin","Agent","PUT /agents/{agentId}","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
			ConstructArray("Admin","Agent","PUT /agents/{agentId}","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}
function GetagentbyAgentID(AgentId){ // done
	getAllAgentsSkillsPayload = {
		'updatedSince': 'string - ISO 8601 formatted date/time',
		'searchString': 'string',
		'fields': 'string',
		'skip': 'integer',
		'top': 'integer',
		'orderBy': 'string'
	}
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/agents/' + AgentId ,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/x-www-form-urlencoded'
		},
		//'data':JSON.stringify(getAllAgentsSkillsPayload),
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","Agent","GET /agents/{agentId}","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","Agent","GET /agents/{agentId}","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function GetAgentByAgentIDGroups(AgentId,fields){//done
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/agents/' + AgentId + '/groups' + '?fields=' + fields,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/x-www-form-urlencoded'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","Agent","GET /agents/{agentId}/groups","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","Agent","GET /agents/{agentId}/groups","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function PostAgentMessages(message,startDate,subject,targetId,targetType,validUntil,expireMinutes){//done

var createpostmessagesPayload = {
  "agentMessages": [
    {
      "message":  message ,
      "startDate":  startDate ,
      "subject":  subject ,
      "targetId":  targetId ,
      "targetType":  targetType ,
      "validUntil":  validUntil  ,
      "expireMinutes":  expireMinutes 
    }
  ]
}
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v11.0/agents/messages',
        'type': 'POST',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
        },
        'data': JSON.stringify(createpostmessagesPayload),
        'success': function (result, status, statusCode) {
            //Process success actions
			ConstructArray("Admin","Agent","POST /agents/messages","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
			ConstructArray("Admin","Agent","POST /agents/messages","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
        }
    });
}
function deleteAgentMessagesByMessageId(MessageId) { //done

    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v11.0/agents/messages/' + MessageId,
        'type': 'DELETE',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/x-www-form-urlencoded'
        },
        'success': function (result, status, statusCode) {
            //Process success actions
			ConstructArray("Admin","Agent","DELETE /agents/messages/{messageid}","SC1-v11.0",statusCode.status + ":" + statusCode.statusTex);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
			ConstructArray("Admin","Agent","DELETE /agents/messages/{messageid}","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
        }
    });
}
function getTeam(fields,updatedSince,searchString,fields,skip,top,orderBy){//done
	
	var queryString = "?fields=" + fields + "&updatedSince=" + updatedSince + 
                "&searchString=" + searchString + "&fields=" + fields + "&skip" + skip + 
                "&top=" + top + "&orderBy=" + orderBy;
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v11.0/teams' + queryString,
        'type': 'GET',
		'async': false,
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken ,
            'content-Type': 'application/x-www-form-urlencoded',
			'Access-Control-Allow-Origin' : '*'
        },
        'success': function (result, status, statusCode) {
       ConstructArray("Admin","Agent","GET /teams","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
	 //Process success actions
	 return result;
			
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
			ConstructArray("Admin","Agent","GET /teams","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
            //Process error actions
            return false;
        }
    });
}
function PutTeam(){//done
	
	var POSTTeamPayload = {
  "teams": [
    {
      "teamName": "team",
      "isActive": true,
      "maxConcurrentChats": 8,
      "wfoEnabled": false,
      "wfm2Enabled": false,
      "qm2Enabled": false,
      "inViewEnabled": false,
      "notes": "",
      "maxEmailAutoParkingLimit": 25,
      "inViewGamificationEnabled": false,
      "inViewChatEnabled": false,
      "inViewLMSEnabled": false,
      "analyticsEnabled": false,
      "requestContact": false,
      "chatThreshold": 1,
      "emailThreshold": 1,
      "workItemThreshold": 1,
      "voiceThreshold": 1,
      "contactAutoFocus": false,
      "teamUuid": ""
    }
  ]
}
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v11.0/teams',
        'type': 'PUT',
		'async': false,
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken ,
            'content-Type': 'application/x-www-form-urlencoded',
			'Access-Control-Allow-Origin' : '*'
        },
		'data': JSON.stringify(POSTTeamPayload),
        'success': function (result, status, statusCode) {
       ConstructArray("Admin","Agent","PUT /teams","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
	 //Process success actions
	 return result;
			
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
			ConstructArray("Admin","Agent","PUT /teams","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
            //Process error actions
            return false;
        }
    });
}
function PutTeambyTeamID(Teamid) { //done
	
  var updateTeambyTeamIdPayload = {
  "forceInactive": false,
  "team": {
    "teamName": "TeamName",
    "isActive": true,
    "maxConcurrentChats": 8,
    "wfoEnabled": false,
    "wfm2Enabled": false,
    "qm2Enabled": false,
    "inViewEnabled": false,
    "notes": "",
    "maxEmailAutoParkingLimit": 25,
    "inViewGamificationEnabled": false,
    "inViewChatEnabled": false,
    "inViewLMSEnabled": false,
    "analyticsEnabled": false,
    "requestContact": false,
    "chatThreshold": 1,
    "emailThreshold": 1,
    "workItemThreshold": 1,
    "voiceThreshold": 1,
    "contactAutoFocus": false,
    "teamUuid": ""
  }
}
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/V11.0/teams/' + Teamid,
        'type': 'POST',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
        },
        'data': JSON.stringify(updateTeambyTeamIdPayload),
        'success': function (result, status, statusCode) {
            //Process success actions
			ConstructArray("Admin","Agent","PUT /teams/","SC1-v11.0",statusCode.status + ":" + statusCode.statusTex);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
			ConstructArray("Admin","Agent","PUT /teams/","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
        }
    });
}
function GetTeamAgent(fields,updatedSince){//done
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/teams' + '?fields=' + fields + '&updatedSince=' + updatedSince,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/x-www-form-urlencoded'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","Agent","GET /teams/agents","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","Agent","GET /teams/agents","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function GetTeamByAgentID(teamId,fields){//done

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/teams/' + teamId +'/agents'+'?fields=' + fields,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/x-www-form-urlencoded'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","Agent","GET /teams/{teamId}/agents","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","Agent","GET /teams/{teamId}/agents","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}


// Admin --> General

function GetBrandingProfile(businessUnitId,fields){//done
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/branding-profiles' + '?businessUnitId=' + businessUnitId + '&fields=' + fields,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/x-www-form-urlencoded'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","General","GET /branding-profiles","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","General","GET /branding-profiles","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function GetDispostion(skip,top,searchString,fields,orderby,isPreviewDispositions,updatedSince){//done
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/dispositions' + '?skip=' + skip + '&top=' + top+ '&searchString=' + searchString+'&fields='+ fields+ '&orderby='+ orderby+ '&isPreviewDispositions='+ isPreviewDispositions+ '&updatedSince='+ updatedSince,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/x-www-form-urlencoded'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","General","GET /dispositions","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","General","GET /dispositions","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function DeleteFiles(fileName) {//done
var DeleteFiles=
{
  "fileName": fileName
}
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v11.0/files',
        'type': 'DELETE',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/x-www-form-urlencoded'
        },
		'data': JSON.stringify(DeleteFiles),
        'success': function (result, status, statusCode) {
            //Process success actions
			ConstructArray("Admin","General","DELETE /files","SC1-v11.0",statusCode.status + ":" + statusCode.statusTex);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
			ConstructArray("Admin","General","DELETE /files","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
        }
    });
}
function PostCreateFileName(fileName,file,overwrite){//done

var createpostFilePayload = {
  "fileName": fileName,
  "file": file,
  "overwrite": overwrite
}
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v11.0/files',
        'type': 'POST',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
        },
        'data': JSON.stringify(createpostFilePayload),
        'success': function (result, status, statusCode) {
            //Process success actions
			ConstructArray("Admin","General","POST /Files","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
			ConstructArray("Admin","General","POST /Files","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
        }
    });
}
function PutupdateFile(oldPath, newPath, overwrite ){//done

var PutfilesPayload = {
  "oldPath":  oldPath,
  "newPath":  newPath,
  "overwrite":  overwrite
}
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v11.0/files',
        'type': 'PUT',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8',
			'Accept':'application/json, text/javascript, */*; q=0.01'
        },
        'data': JSON.stringify(PutfilesPayload),
        'success': function (result, status, statusCode) {
			ConstructArray("Admin","General","PUT /files","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
            //Process success actions
            return result;
			
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
			ConstructArray("Admin","General","PUT /files","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}
function GetFilesExternal(folderPath){ //done
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/files/external' + '?folderPath=' + folderPath,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/x-www-form-urlencoded'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","General","GET files/external","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","General","GET files/external","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function PostCreateFileExternal(fileName,file,overwrite = true,needsProcessing = true){//done

var CreateFileexternalPayload = {
  "fileName":  fileName,
  "file":  file ,
  "overwrite":  overwrite,
  "needsProcessing":  needsProcessing 
}
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v11.0/files/external',
        'type': 'POST',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
        },
        'data': JSON.stringify(CreateFileexternalPayload),
        'success': function (result, status, statusCode) {
            //Process success actions
			ConstructArray("Admin","General","POST /files/external","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
			ConstructArray("Admin","General","POST /files/external","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
        }
    });
}
function PutupdateFilesExternal(fileName,needsProcessing = false){//done
	var PutfilesPayload = {
  "fileName":  fileName,
  "needsProcessing":  needsProcessing
}
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v11.0/files/external',
        'type': 'PUT',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8',
			'Accept':'application/json, text/javascript, */*; q=0.01'
        },
        'data': JSON.stringify(PutfilesPayload),
        'success': function (result, status, statusCode) {
            //Process success actions
            return result;
			ConstructArray("Admin","General","PUT /files/external","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
			ConstructArray("Admin","General","PUT /files/external","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}
function DeleteFolders(folderName) {//done

var DeleteFolder={"folderName":  folderName}

    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v11.0/folders',
        'type': 'DELETE',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/x-www-form-urlencoded'
        },
		'data': JSON.stringify(DeleteFolder),
        'success': function (result, status, statusCode) {
            //Process success actions
			ConstructArray("Admin","General","DELETE /folders","SC1-v11.0",statusCode.status + ":" + statusCode.statusTex);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
			ConstructArray("Admin","General","DELETE /folders","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
        }
    });
}
function GetFolders(folderName){//done
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/folders' + '?folderName='+ folderName,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/x-www-form-urlencoded'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","General","GET /folders","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","General","GET /folders","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}


//Admin--> skills 

function GetDispostionByID(dispositionId,fields){//done
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/dispositions/' + dispositionId + '? fields='+ fields,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/x-www-form-urlencoded'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","Skills","GET /dispositions/{dispositionId}","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","Skills","GET /dispositions/{dispositionId}","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function PutUpdateDispostionbydispositionId(dispositionId,dispositionName,isPreviewDisposition,classificationId,isActive){//done

var PutupdateDispostionbyIDpayload = {
  "dispositionName": dispositionName,
  "isPreviewDisposition": isPreviewDisposition,
  "classificationId": classificationId,
  "isActive": isActive
}
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v11.0/dispositions/' + dispositionId,
        'type': 'PUT',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8',
			'Accept':'application/json, text/javascript, */*; q=0.01'
        },
        'data': JSON.stringify(PutupdateDispostionbyIDpayload),
        'success': function (result, status, statusCode) {
			ConstructArray("Admin","Skills","PUT /dispositions/{dispositionId}","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
            //Process success actions
            return result;
			
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
			ConstructArray("Admin","Skills","PUT /dispositions/{dispositionId}","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}
function GetDispostionByClassification(fields ){//done
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/dispositions/classifications' + '? fields='+ fields,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/x-www-form-urlencoded'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","Skills","GET /dispositions/classifications","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","Skills","GET /dispositions/classifications","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function PostcreateDispositions(dispositionvalue,isPreviewDisposition,classificationId){//done
	
	var CreateDispostionpayload ={
  "dispositions": [
    {
      "dispositionName":  dispositionvalue,
      "isPreviewDisposition": isPreviewDisposition ,
      "classificationId": + classificationId
    }
  ]
}
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/dispositions',
		'type': 'POST',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/x-www-form-urlencoded',
			'content-Type': 'application/json; charset=UTF-8'
		},
		'data': JSON.stringify(CreateDispostionpayload),
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","Skills","Post /dispositions","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","Skills","Post /dispositions","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function PostCreateSkill(){//done
var CreateSkillPayload = {
  "skills": [
    {
      "mediaTypeId": 4,
      "skillName": "",
      "isOutbound": true,
      "outboundStrategy": "Personal Connection",
      "campaignId": 1,
      "callerIdOverride": "8015554422",
      "emailFromAddress": "test@test.com",
      "emailFromEditable": false,
      "emailBccAddress": "",
      "scriptId": 2,
      "reskillHours": 4,
      "minWFIAgents": 4,
      "interruptible": false,
      "enableParking": false,
      "minWorkingTime": 4,
      "agentless": false,
      "agentlessPorts": 6,
      "notes": "this is a test note",
      "acwTypeId": 3,
      "requireDisposition": false,
      "allowSecondaryDisposition": false,
      "scriptDisposition": false,
      "stateIdACW": 2,
      "maxSecondsACW": 3,
      "acwPostTimeoutStateId": 53,
      "agentRestTime": 4,
      "displayThankyou": false,
      "thankYouLink": "no",
      "popThankYou": true,
      "popThankYouURL": "tester.com",
      "makeTranscriptAvailable": true,
      "transcriptFromAddress": "fromMe@email.com",
      "priorityBlending": false,
      "callSuppressionScriptId": 4,
      "useScreenPops": true,
      "screenPopTriggerEvent": "popTriggerEvent",
      "useCustomScreenPops": false,
      "screenPopType": "webpage",
      "screenPopDetails": "http://not",
      "initialPriority": 4,
      "acceleration": 5,
      "maxPriority": 10,
      "serviceLevelThreshold": 51,
      "serviceLevelGoal": 24,
      "enableShortAbandon": true,
      "shortAbandonThreshold": 123,
      "countShortAbandons": true,
      "countOtherAbandons": true,
      "chatWarningTheshold": 0,
      "agentTypingIndicator": false,
      "patronTypingPreview": false,
      "smsTransportCodeId": null,
      "messageTemplateId": null,
      "deliverMultipleNumbersSerially": false,
      "cradleToGrave": false,
      "priorityInterrupt": false,
      "treatProgressAsRinging": false,
      "preConnectCPAEnabled": false,
      "agentOverrideFax": true,
      "agentOverrideAnsweringMachine": true,
      "agentOverrideBadNumber": true,
      "dispositions": [
        {
          "dispositionId": 1,
          "priority": 1
        }
      ]
    }
  ]
}
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v11.0/skills',
        'type': 'POST',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
        },
        'data': JSON.stringify(CreateSkillPayload),
        'success': function (result, status, statusCode) {
            //Process success actions
			ConstructArray("Admin","General","POST /Skills","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
			ConstructArray("Admin","General","POST /Skills","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
        }
    });
}
function PutSkillbySkillID(skillId){//done

var PutSkillbySkillIDpayload = {
  "skill": {
    "skillName": "string",
    "isActive": true,
    "campaignId": 2,
    "callerIdOverride": "string",
    "emailFromAddress": "test@test.com",
    "emailFromEditable": false,
    "emailBccAddress": "",
    "scriptId": 2,
    "reskillHours": 4,
    "minWFIAgents": 4,
    "interruptible": false,
    "enableParking": false,
    "minWorkingTime": 4,
    "agentless": false,
    "agentlessPorts": 6,
    "notes": "this is a test note",
    "acwTypeId": 3,
    "requireDisposition": false,
    "allowSecondaryDisposition": false,
    "scriptDisposition": false,
    "stateIdACW": 2,
    "maxSecondsACW": 3,
    "acwPostTimeoutStateId": 53,
    "agentRestTime": 4,
    "displayThankyou": false,
    "thankYouLink": "no",
    "popThankYou": true,
    "popThankYouURL": "tester.com",
    "makeTranscriptAvailable": true,
    "transcriptFromAddress": "fromMe@email.com",
    "priorityBlending": false,
    "callSuppressionScriptId": 4,
    "useScreenPops": true,
    "screenPopTriggerEvent": "bleh",
    "useCustomScreenPops": false,
    "screenPopType": "webpage",
    "screenPopDetails": "http://no",
    "initialPriority": 4,
    "acceleration": 5,
    "maxPriority": 10,
    "serviceLevelThreshold": 51,
    "serviceLevelGoal": 24,
    "enableShortAbandon": true,
    "shortAbandonThreshold": 123,
    "countShortAbandons": true,
    "countOtherAbandons": true,
    "chatWarningTheshold": 0,
    "agentTypingIndicator": false,
    "patronTypingPreview": false,
    "smsTransportCodeId": null,
    "messageTemplateId": null,
    "deliverMultipleNumbersSerially": false,
    "cradleToGrave": false,
    "priorityInterrupt": false,
    "treatProgressAsRinging": false,
    "preConnectCPAEnabled": false,
    "agentOverrideFax": true,
    "agentOverrideAnsweringMachine": true,
    "agentOverrideBadNumber": true,
    "dispositions": [
      {
        "dispositionId": 1,
        "priority": 1
      }
    ]
  }
}
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v11.0/skills/' +skillId,
        'type': 'PUT',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8',
			'Accept':'application/json, text/javascript, */*; q=0.01'
        },
        'data': JSON.stringify(PutSkillbySkillIDpayload),
        'success': function (result, status, statusCode) {
			ConstructArray("Admin","Skills","PUT /skills/{skillId}","so32-v13.0",statusCode.status + ":" + statusCode.statusText);
            //Process success actions
            return result;
			
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
			ConstructArray("Admin","Skills","PUT /skills/{skillId}","so32-v13.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}
function GetDispostionBySkillid(skillId,searchString,fields,skip,top,orderby ){//done
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/skills/' + skillId + '/dispositions'+
                 '?searchString='+ searchString+ '&fields='+ fields+ '&skip='+ skip+ '&top='+ top+ '&orderby='+ orderby ,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/x-www-form-urlencoded'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","Skills","GET /skills/{skillId}/dispositions","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","Skills","GET /skills/{skillId}/dispositions","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function GetDispostionBySkillidUnAssigned(skillId,searchString,fields,skip,top,orderby ){//done
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/skills/' + skillId + '/dispositions/unassigned' +
                 '?searchString=' + searchString + '&fields=' + fields + '&skip=' + skip + '&top=' + top + '&orderby=' + orderby ,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/x-www-form-urlencoded'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","Skills","GET /skills/{skillId}/dispositions/unassigned","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","Skills","GET /skills/{skillId}/dispositions/unassigned","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function GetSkillParameterGeneralSetting(skillId,fields ){	//done
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/skills/'+ skillId+ '/parameters/general-settings'+ '?fields='+ fields,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/x-www-form-urlencoded'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","Skills","GET /skills/{skillId}/parameters/general-settings","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","Skills","GET /skills/{skillId}/parameters/general-settings","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function PutSkillParameterGeneralSetting(skillId){//done

var SkillGeneralSettingPayload = {
  "generalSettings": {
    "minimumRetryMinutes": 12,
    "maximumAttempts": 10,
    "defaultContactExpiration": 10,
    "getPriorityContactsOnContactinsertion": false,
    "loadCallbacks": false,
    "loadFresh": false,
    "loadNonFresh": false,
    "overrideBusinessUnitAbandonRate": false,
    "maximumRingingDuration": 1,
    "beginDampenPercentage": 1,
    "abandonRateCutoff": 1,
    "abandonRateThreshold": 1,
    "inactiveBlenderTimer": 1,
    "maximumRatio": 1,
    "aggressiveness": "conservative",
    "endOfListNotificationsDelay": 15,
    "notifyAgentsWhenListIsEmpty": false,
    "percentageOfAgentsBeforeOverdial": 5,
    "blockMultipleCalls": true,
    "consecutiveAttemptsWithoutALiveConnect": 5
  }
}
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v11.0/skills/' + skillId + '/parameters/general-settings',
        'type': 'PUT',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8',
			'Accept':'application/json, text/javascript, */*; q=0.01'
        },
        'data': JSON.stringify(SkillGeneralSettingPayload),
        'success': function (result, status, statusCode) {
			ConstructArray("Admin","Skills","PUT /skills/{skillId}/parameters/general-settings","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
            //Process success actions
            return result;
			
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
			ConstructArray("Admin","Skills","PUT /skills/{skillId}/parameters/general-settings","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}

//Admin--> Contacts

function GetcontactbyContactID(contactId,fields ){	//done
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/contacts/' + contactId + '/files' + '?fields='+ fields,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/x-www-form-urlencoded'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","Contacts","GET /contacts/{contactId}/files","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","Contacts","GET /contacts/{contactId}/files","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}

//Admin--> List

function GetListCalllist(fields,top,skip,orderBy,startDate,endDate ){	//done

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/lists/call-lists/jobs'+
                    '?fields='+ fields+ '&top='+ top+'&skip='+ skip+ '&orderBy='+ orderBy+ '&startDate='+ startDate+ '&endDate='+ endDate,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/x-www-form-urlencoded'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","LIST","GET /lists/call-lists/jobs","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","LIST","GET /lists/call-lists/jobs","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function PostCallListbyListID(listId,skillId,fileName,forceOverwrite,defaultTimeZone,expirationDate,listFile,startSkill){//done

var PostCallListbyListIDpayload ={
  "skillId": skillId,
  "fileName": fileName,
  "forceOverwrite": forceOverwrite,
  "defaultTimeZone": defaultTimeZone,
  "expirationDate": expirationDate,
  "listFile": listFile,
  "startSkill": startSkill
}

    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v11.0/lists/call-lists/' + listId + '/upload',
        'type': 'POST',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
        },
        'data': JSON.stringify(PostCallListbyListIDpayload),
        'success': function (result, status, statusCode) {
            //Process success actions
			ConstructArray("Admin","LIST","POST /lists/call-lists/{listId}/upload","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
			ConstructArray("Admin","LIST","POST /lists/call-lists/{listId}/upload","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
        }
    });
}
function DeleteCallListByJobID(jobId) {//done

    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v11.0/lists/call-lists/jobs/' +jobId,
        'type': 'DELETE',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/x-www-form-urlencoded'
        },
        'success': function (result, status, statusCode) {
            //Process success actions
			ConstructArray("Admin","LIST","DELETE /lists/call-lists/jobs/{jobId}","SC1-v11.0",statusCode.status + ":" + statusCode.statusTex);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
			ConstructArray("Admin","LIST","DELETE /lists/call-lists/jobs/{jobId}","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
        }
    });
}
function GetCallingListbyJobID(jobId,fields){	//done

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/lists/call-lists/jobs/' + jobId + '?fields=' + fields,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/x-www-form-urlencoded'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","LIST","GET /lists/call-lists/jobs/{jobId}","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","LIST","GET /lists/call-lists/jobs/{jobId}","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}

//Admin --> Groups

function GetGroups(top,skip,orderby,searchString,isActive,fields){	//done

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/groups' + '?top=' + top + '&skip=' + skip+ 
        '&orderby=' + orderby + '&searchString=' + searchString + '&isActive='+ isActive + '&fields=' + fields,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","Groups","GET /groups","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","Groups","GET /groups","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function PostGroups(groupName, isActive, notes){	//done

var updateGroupPayload ={
  "groups": [
    {
      "groupName": groupName,
      "isActive": isActive,
      "notes": notes
    }
  ]
}

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/groups',
		'type': 'POST',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'data': JSON.stringify(updateGroupPayload),
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","Groups","POST /groups","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","Groups","POST /groups","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function GetGroupsByGroupID(groupId, fields){	//done

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/groups/' + groupId + '?fields='+ fields,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","Groups","GET /groups/{groupId}","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","Groups","GET /groups/{groupId}","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function PutGroupsByGroupID(groupId,groupName,isActive,notes){	//done
var PostGroupbyGroupIDPayload = {
  "groupName": groupName,
  "isActive": isActive,
  "notes": notes
}
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/groups/' + groupId,
		'type': 'put',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'data': JSON.stringify(PostGroupbyGroupIDPayload),
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","Groups","POST /groups/{groupId}","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","Groups","POST /groups/{groupId}","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function DeleteGroupsByAgentGroupID(groupId, agentId){	//done

var DeleteGroupbyGroupIDagentPayload = {"agents": [{"agentId": agentId} ] }
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/groups/'+ groupId +'/agents',
		'type': 'DELETE',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'data': JSON.stringify(DeleteGroupbyGroupIDagentPayload),
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","Groups","DELETE /groups/{groupId}/agents","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","Groups","DELETE /groups/{groupId}/agents","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function GetGroupsByAgentGroupID(groupId,top,skip,orderBy,searchString,assigned,fields){	//done

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/{version}/groups/' + groupId + '/agents' +'?top=' + top + '&skip=' + skip + '&orderby=' + orderBy + '&searchString=' + searchString + '&assigned=' + assigned + '&fields=' + fields,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","Groups","GET /groups/{groupId}/agents","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","Groups","GET /groups/{groupId}/agents","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function PostGroupsByAgentGroupID(groupId,agentId){	//done

var PostGroupbyGroupIDagentPayload = { "agents": [ { "agentId": 1 } ] }
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/groups/'+ groupId +'/agents',
		'type': 'POST',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'data': JSON.stringify(PostGroupbyGroupIDagentPayload),
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","Groups","POST /groups/{groupId}/agents","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","Groups","POST /groups/{groupId}/agents","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}

//Agent --> Chat Request

function postSessionIdInteractionsContactIdtyping(sessionId, contactId,isTyping= true,isTextEntered= true){	//done

var PostChatTypingPayload = {
  "isTyping": isTyping,
  "isTextEntered": isTextEntered
}

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/agent-sessions/' + sessionId + '/interactions/'+ contactId +'/typing',
		'type': 'POST',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'data': JSON.stringify(PostChatTypingPayload),
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Agents","Chat","POST /agent-sessions/{sessionId}/interactions/{contactId}/typing","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Agents","Chat","POST /agent-sessions/{sessionId}/interactions/{contactId}/typing","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function posttAgentSessionSessionIdInteractionAddEmail(sessionId){	//done

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/agent-sessions/'+ sessionId + '/interactions/add-email',
		'type': 'POST',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Agents","Email","POST /agent-sessions/{sessionId}/interactions/add-email","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Agents","Email","POST /agent-sessions/{sessionId}/interactions/add-email","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function posttAgentSessionIdInteractionConatactIdParkemail(sessionId,ConatctId,toAddress="",fromAddress="",ccAddress="",
           bccAddress="",subject="", bodyHtml="",attachments="",attachmentNames="",isDraft="false",originalAttachmentNames=""){//done

var PostEmailParkPayload = {
  "toAddress": toAddress,
  "fromAddress": fromAddress,
  "ccAddress": ccAddress,
  "bccAddress": bccAddress,
  "subject": subject,
  "bodyHtml": bodyHtml,
  "attachments": attachments,
  "attachmentNames": attachmentNames,
  "isDraft": isDraft,
  "originalAttachmentNames": originalAttachmentNames
}

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/agent-sessions/' + sessionId + '/interactions/' + ConatctId+ '/email-park',
		'type': 'POST',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'data': JSON.stringify(PostEmailParkPayload),
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Agents","Email","POST /agent-sessions/{sessionId}/interactions/{contactId}/email-park","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Agents","Email","POST /agent-sessions/{sessionId}/interactions/{contactId}/email-park","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function posttAgentSessionIdInteractionConatactIdUnparkemail( sessionId, ConatctId, isImmediate="false"){

var PostEmailunParkPayload = { "isImmediate": isImmediate }

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/agent-sessions/' + sessionId + '/interactions/' + ConatctId+ '/email-unpark',
		'type': 'POST',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'data': JSON.stringify(PostEmailunParkPayload),
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Agents","Email","POST /agent-sessions/{sessionId}/interactions/{contactId}/email-unpark","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Agents","Email","POST /agent-sessions/{sessionId}/interactions/{contactId}/email-unpark","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function posttAgentSessionIdInteractionConatactIdPreview( sessionId, ConatctId){//done


	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/agent-sessions/' + sessionId + '/interactions/' + ConatctId+ '/email-preview',
		'type': 'POST',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Agents","Email","POST /agent-sessions/{sessionId}/interactions/{contactId}/email-preview","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Agents","Email","POST /agent-sessions/{sessionId}/interactions/{contactId}/email-preview","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function posttAgentSessionIdInteractionConatactIdEmailRestore( sessionId, ConatctId){// done

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/agent-sessions/' + sessionId + '/interactions/' + ConatctId+ '/email-restore',
		'type': 'POST',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Agents","Email","POST /agent-sessions/{sessionId}/interactions/{contactId}/email-restore","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Agents","Email","POST /agent-sessions/{sessionId}/interactions/{contactId}/email-restore","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}

//Agent Personal Connection

function posttAgentSessionIdInteractionConatactIdSnooze( sessionId, ConatctId){

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/agent-sessions/' + sessionId + '/interactions/' + ConatctId+ '/snooze',
		'type': 'POST',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Agents","Callbacks","POST /agent-sessions/{sessionId}/interactions/{contactId}/snooze","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Agents","Callbacks","POST /agent-sessions/{sessionId}/interactions/{contactId}/snooze","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return false;
			
		}
	});
}

// Agent --> ScheduledCallbacks

function DialCallback( sessionId, callbackId){// done

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/agent-sessions/' + sessionId + '/interactions/'+ callbackId + '/dial',
		'type': 'POST',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Agents","Callbacks","POST /agent-sessions/{sessionId}/interactions/{callbackId}/dial","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Agents","Callbacks","POST /agent-sessions/{sessionId}/interactions/{callbackId}/dial","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function RescheduleCallback( sessionId, callbackId,rescheduleDate){// done

var PostRescheduleCallbackPayload = {"rescheduleDate" :  rescheduleDate }

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/agent-sessions/'+ sessionId +'/interactions/' + callbackId + '/reschedule',
		'type': 'POST',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'data': JSON.stringify(PostRescheduleCallbackPayload),
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Agents","Callbacks","POST /agent-sessions/{sessionId}/interactions/{callbackId}/reschedule","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Agents","Callbacks","POST /agent-sessions/{sessionId}/interactions/{callbackId}/reschedule","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function CancelCallback( sessionId, callbackId,notes){// done

var PostCancelCallbackPayload = {"notes" :  notes }

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/agent-sessions/'+ sessionId +'/interactions/' + callbackId + '/cancel',
		'type': 'POST',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'data': JSON.stringify(PostCancelCallbackPayload),
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Agents","Callbacks","POST /agent-sessions/{sessionid}/interactions/{callbackid}/cancel","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Agents","Callbacks","POST /agent-sessions/{sessionid}/interactions/{callbackid}/cancel","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}

//Admin Agent Session

function posttAgentSessionIdAddcontact(sessionId, chat="true", email="true", workitem="true"){// done

var PostAddcontacPayload = {
  "chat": chat,
  "email": email,
  "workitem": workitem
}

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/V11.0/agent-sessions/' + sessionId + '/add-contact',
		'type': 'POST',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'data': JSON.stringify(PostAddcontacPayload),
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Agents","Sessions","POST /agent-sessions/{sessionId}/add-contact","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Agents","Sessions","POST /agent-sessions/{sessionId}/add-contact","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function posttAgentSessionIdInteractionConatactIdActivate(sessionId, ConatctId){// done

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/agent-sessions/' + sessionId + '/interactions/' + ConatctId + '/activate',
		'type': 'POST',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Agents","Sessions","POST /agent-sessions/{sessionId}/interactions/{contactId}/activate","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Agents","Sessions","POST /agent-sessions/{sessionId}/interactions/{contactId}/activate","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}

//Reporting --> Historical Reporting

function GetSmstranscripts( startDate, endDate, transportCode , agentId="", top="",skip="",orderby=""){	//done

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/contacts/sms-transcripts' + '?top=' + top + '&skip=' + skip+ 
        '&orderby=' + orderby + '&startDate=' + startDate + '&endDate='+ endDate + '&transportCode=' + transportCode,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Reports","Reports","GET /contacts/sms-transcripts","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Reports","Reports","GET /contacts/sms-transcripts","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function GetSmstranscriptsbyContactid( contactId,startDate, endDate, transportCode, top="",skip=""){	//done
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v10.0/contacts/' + contactId + '/sms-transcripts' + '?top=' + top + '&skip=' + skip + '&startDate=' + startDate + '&endDate='+ endDate + '&transportCode=' + transportCode,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Reports","Reports","GET /contacts/{contactid}/sms-transcripts","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Reports","Reports","GET /contacts/{contactid}/sms-transcripts","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function GetSmsCallQuality( contactId){	//done

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/contacts/' + contactId + '/call-quality' ,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Reports","Reports","GET /contacts/{contactId}/call-quality","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Reports","Reports","GET /contacts/{contactId}/call-quality","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function GetTeamPerformanceTotal( startDate, endDate,fields = ""){	//done
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/teams/performance-total' 
						+ '?startDate=' + startDate + '&endDate=' + endDate + '&fields=' + fields,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Reports","Reports","GET /teams/performance-total","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Reports","Reports","GET /teams/performance-total","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function GetTeamPerformancebyTeamIDTotal( teamId, startDate, endDate,fields = ""){	//done

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v3.0/teams/' + teamId + '/performance-total' + '?startDate=' + startDate + '&endDate=' + endDate + '&fields=' + fields,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Reports","Reports","GET /teams/{teamId}/performance-total","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Reports","Reports","GET /teams/{teamId}/performance-total","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function getReportJobById(jobId,fields="") { // done
  
    $.ajax({
        //The baseURI variable is created by the result.base_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + 'services/v11.0/report-jobs/' + jobId + "?fields=" +fields,
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/x-www-form-urlencoded',
        },
        'success': function (result, status, statusCode) {
            //Process success actions
		ConstructArray("Reports","Reports","GET /report-jobs/{jobId}","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
		ConstructArray("Reports","Reports","GET /report-jobs/{jobId}","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
        }
    });
}
function GetdatadownloadbyReportID( reportId,fileName, startDate, endDate, saveAsFile = 'true',includeHeaders = false){	//done
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/{version}/report-jobs/datadownload/'+ reportId +
                         '?startDate=' + startDate + '&endDate=' + endDate + '&saveAsFile=' + saveAsFile + 
                         '&includeHeaders=' + includeHeaders,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Reports","Reports","GET /report-jobs/datadownload/{reportid}","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Reports","Reports","GET /report-jobs/datadownload/{reportid}","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function Getwfoascm(startDate,endDate,fields="" ){	//done

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/wfo-data/ascm' + '?startDate=' + startDate + '&endDate=' + endDate + '&fields=' + fields,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Reports","Reports","GET /wfo-data/ascm","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Reports","Reports","GET /wfo-data/ascm","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function Getwfoasi(startDate,endDate,fields="" ){	//done

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/wfo-data/asi' + '?startDate=' + startDate + '&endDate=' + endDate + '&fields=' + fields,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Reports","Reports","GET /wfo-data/asi","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Reports","Reports","GET /wfo-data/asi","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function Getwfocsi(startDate,endDate,fields="" ){	//done
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/wfo-data/csi' + '?startDate=' + startDate + '&endDate=' + endDate + '&fields=' + fields,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Reports","Reports","GET /wfo-data/csi","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Reports","Reports","GET /wfo-data/csi","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function Getwfoftci(startDate,endDate,fields="" ){	//done

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/wfo-data/ftci' + '?startDate=' + startDate + '&endDate=' + endDate + '&fields=' + fields,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Reports","Reports","GET /wfo-data/ftci","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Reports","Reports","GET /wfo-data/ftci","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function Getwfoosi(startDate,endDate,fields="" ){	//done
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/wfo-data/osi' + '?startDate=' + startDate + '&endDate=' + endDate + '&fields=' + fields,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Reports","Reports","GET /wfo-data/osi","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Reports","Reports","GET /wfo-data/osi","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function Getwfoqm(startDate,endDate,fields="" ){	//done

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/wfo-data/qm'+ '?startDate=' + startDate + '&endDate=' + endDate + '&fields=' + fields,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Reports","Reports","GET /wfo-data/qm","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Reports","Reports","GET /wfo-data/qm","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}

// Report --> WFM

function GetwfmSkillContacts(startDate,endDate,mediaTypeId,fields="" ){	//done

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/wfm-data/skills/contacts'+ '?startDate=' + startDate + '&endDate=' + endDate + '&fields=' + fields + '&mediaTypeId=' + mediaTypeId,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Reports","WFM","GET /wfm-data/skills/contact","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Reports","WFM","GET /wfm-data/skills/contact","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function Getwfmagetns(startDate,endDate,fields="" ){	//done

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/wfm-data/agents'+ 
						 '?startDate=' + startDate + '&endDate=' + endDate + '&fields=' + fields,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Reports","WFM","GET /wfm-data/agents","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Reports","WFM","GET /wfm-data/agents","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function GetwfmdailerContacts(startDate,endDate,fields="" ){	//done

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/wfm-data/skills/dialer-contacts'+ 
						 '?startDate=' + startDate + '&endDate=' + endDate + '&fields=' + fields,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Reports","WFM","GET /wfm-data/skills/dialer-contacts","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Reports","WFM","GET /wfm-data/skills/dialer-contacts","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function Getwfmscheduleadherence(startDate,endDate,fields="" ){	//done

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v9.0/wfm-data/agents/schedule-adherence'+ 
						 '?startDate=' + startDate + '&endDate=' + endDate + '&fields=' + fields,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Reports","WFM","GET /wfm-data/agents/schedule-adherence","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Reports","WFM","GET /wfm-data/agents/schedule-adherence","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function GetwfmAgentscorecard(startDate,endDate,fields="" ){	//done

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v11.0/wfm-data/agents/scorecards'+ 
						 '?startDate=' + startDate + '&endDate=' + endDate + '&fields=' + fields,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Reports","WFM","GET /wfm-data/agents/scorecard","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Reports","WFM","GET /wfm-data/agents/scorecard","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}
function GetwfmAgentperformance(startDate,endDate,fields="" ){	//done

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v9.0/wfm-data/skills/agent-performance'+ 
						 '?startDate=' + startDate + '&endDate=' + endDate + '&fields=' + fields,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Reports","WFM","GET /wfm-data/skills/agent-performance","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Reports","WFM","GET /wfm-data/skills/agent-performance","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}

function postContactsChatSessionTyping(chatSession,isTyping="",isTextEntered="",label="testLabel"){

	var CreateFileexternalPayload = {
	  "isTyping":  isTyping,
	  "isTextEntered":  isTextEntered ,
	  "label":  label
	}
		$.ajax({
			//The baseURI variable is created by the result.resource_server_base_uri,
			//which is returned when getting a token and should be used to create the URL base
			'url': baseURI + '/services/v8.0/contacts/chats/'+chatSession+'/typing',
			'type': 'POST',
			'headers': {
				//Use access_token previously retrieved from inContact token service
				'Authorization': 'bearer ' + accessToken,
				'content-Type': 'application/json; charset=UTF-8'
			},
			'data': JSON.stringify(CreateFileexternalPayload),
			'success': function (result, status, statusCode) {
				//Process success actions
				ConstructArray("Patron","chatRequest","POST /contacts/chats/{chatSession}/typing","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
				return result;
			},
			'error': function (XMLHttpRequest, textStatus, errorThrown) {
				//Process error actions
				ConstructArray("Patron","chatRequest","POST /contacts/chats/{chatSession}/typing","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
				return false;
			}
		});
	}

	function postContactsChatSessionTypingPreview(chatSession,previewText = "This text is 100% awesome!!",label = "testLabel"){//done

		var CreateFileexternalPayload = {
		  "previewText":  previewText,		  
		  "label":  label
		}
			$.ajax({
				//The baseURI variable is created by the result.resource_server_base_uri,
				//which is returned when getting a token and should be used to create the URL base
				'url': baseURI + '/services/v8.0/contacts/chats/'+chatSession+'/typing-preview',
				'type': 'POST',
				'headers': {
					//Use access_token previously retrieved from inContact token service
					'Authorization': 'bearer ' + accessToken,
					'content-Type': 'application/json; charset=UTF-8'
				},
				'data': JSON.stringify(CreateFileexternalPayload),
				'success': function (result, status, statusCode) {
					//Process success actions
					ConstructArray("Patron","chatRequest","POST contacts/chats/{chatSession}/typing-preview","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
					return result;
				},
				'error': function (XMLHttpRequest, textStatus, errorThrown) {
					//Process error actions
					ConstructArray("Patron","chatRequest","POST contacts/chats/{chatSession}/typing-preview","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
					return false;
				}
			});
		}

		function getChatprofileByPOCId(pointOfContactId){	//done

			$.ajax({
				//The baseURI variable is created by the result.resource_server_base_uri,
				//which is returned when getting a token and should be used to create the URL base
				'url': baseURI + '/services/v11.0/points-of-contact/'+pointOfContactId+'/chat-profile',
				'type': 'GET',
				'headers': {
					//Use access_token previously retrieved from inContact token service
					'Authorization': 'bearer ' + accessToken,
					'content-Type': 'application/json; charset=UTF-8'
				},
				'success': function (result, status, statusCode) {
					//Process success actions
					ConstructArray("Patron","chatRequest","GET points-of-contact/{pointOfContactId}/chat-profile","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
					return result;
				},
				'error': function (XMLHttpRequest, textStatus, errorThrown) {
					//Process error actions
					ConstructArray("Patron","chatRequest","GET points-of-contact/{pointOfContactId}/chat-profile","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
					return false;
					
				}
			});
		}
		function getThankYouForSkillId(SkillId){	//done

			$.ajax({
				//The baseURI variable is created by the result.resource_server_base_uri,
				//which is returned when getting a token and should be used to create the URL base
				'url': baseURI + '/services/v12.0/skills/'+SkillId+'/thank-you-page',
				'type': 'GET',
				'headers': {
					//Use access_token previously retrieved from inContact token service
					'Authorization': 'bearer ' + accessToken,
					'content-Type': 'application/json; charset=UTF-8'
				},
				'success': function (result, status, statusCode) {
					//Process success actions
					ConstructArray("Admin","Skill","Get /skills/{killId}/thank-you-page","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
					return result;
				},
				'error': function (XMLHttpRequest, textStatus, errorThrown) {
					//Process error actions
					ConstructArray("Admin","Skills","Get /skills/{killId}/thank-you-page","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
					return false;
					
				}
			});
		}

		function postContactChatSendEmail(fromAddress,toAddress,emailBody)
  {
	
	var CreateChatSendEmailpayload ={    
      "fromAddress": fromAddress,
      "toAddress": toAddress ,
      "emailBody": emailBody  
}
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v12.0/contacts/chats/send-email',
		'type': 'POST',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/x-www-form-urlencoded',
			'content-Type': 'application/json; charset=UTF-8'
		},
		'data': JSON.stringify(CreateChatSendEmailpayload),
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Patron","chatRequest","Post /contacts/chats/send-email","SC1-v11.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Patron","chatRequest","Post /contacts/chats/send-email","SC1-v11.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}

//Version 15
function getAccessKeys(agentId,fields,skip,top,orderBy) {
						
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v15.0/access-keys' + '?agentId=' + agentId  + '&fields=' + fields + '&skip' + skip + '&top=' + top + '&orderBy=' + orderBy ,
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
        },
        //'data': getAgentsPayload,
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Agents","GET /access-keys","so32-v15.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Agents","GET /access-keys","so32-v15.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}

function postAccessKeys(agentId)
  {

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v15.0/access-keys' + '?agentId=' + agentId ,
		'type': 'POST',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/x-www-form-urlencoded',
			'content-Type': 'application/json; charset=UTF-8'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","Agents","POST /access-keys","so32-v15.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","Agents","POST /access-keys","so32-v15.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}

function getAccessKeysByID(accessKeyId) {
						
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v15.0/access-keys/'+accessKeyId,
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
        },
        //'data': getAgentsPayload,
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Agents","GET /access-keys/{accessKeyId}","so32-v15.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Agents","GET /access-keys/{accessKeyId}","so32-v15.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}

function deleteAccessKeysByID(accessKeyId) { //done

    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v15.0/access-keys/'+accessKeyId,
        'type': 'DELETE',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/x-www-form-urlencoded'
        },
        'success': function (result, status, statusCode) {
            //Process success actions
			ConstructArray("Admin","Agent","DELETE /access-keys/{accessKeyId}","so32-v15.0",statusCode.status + ":" + statusCode.statusTex);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
			ConstructArray("Admin","Agent","DELETE /access-keys/{accessKeyId}","so32-v15.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
        }
    });
}

function PatchAccessKeysByID(accessKeyId,isActive) { //done

    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v15.0/access-keys/'+ accessKeyId + '?isActive=' + isActive,
        'type': 'PATCH',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/x-www-form-urlencoded'
        },
        'success': function (result, status, statusCode) {
            //Process success actions
			ConstructArray("Admin","Agent","PATCH /access-keys/{accessKeyId}","so32-v15.0",statusCode.status + ":" + statusCode.statusTex);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
			ConstructArray("Admin","Agent","PATCH /access-keys/{accessKeyId}","so32-v15.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
        }
    });
}

function PostCampaigns(campaignName, isActive, notes,description){	//done

var updateCampaignPayload ={
  "campaigns": [
    {
      "campaignName": campaignName,
      "isActive": isActive,
	  "description":description,
      "notes": notes
    }
  ]
}

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v15.0/campaigns',
		'type': 'POST',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'data': JSON.stringify(updateCampaignPayload),
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","Skills","POST /campaigns","so32-v15.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","Skills","POST /campaigns","so32-v15.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}

function PutCampaignsByID(campaignId,campaignName, isActive, notes,description){	//done

var updatePutCampaignPayload ={
  "campaigns": [
    {
      "campaignName": campaignName,
      "isActive": isActive,
	  "description":description,
      "notes": notes
    }
  ]
}

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v15.0/campaigns/'+campaignId,
		'type': 'PUT',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'data': JSON.stringify(updatePutCampaignPayload),
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","Skills","PUT /campaigns/{campaignId}","so32-v15.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","Skills","PUT /campaigns/{campaignId}","so32-v15.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}

function PostCampaignsBySkill(campaignId,skillId){	//done

var updateCampaignSkillPayload ={
  "skills": [
    {
      "skillId": skillId
    }
  ]
}

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v15.0/campaigns/'+ campaignId + '/skills',
		'type': 'POST',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'data': JSON.stringify(updateCampaignSkillPayload),
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","Skills","POST /campaigns/{campaignId}/skills","so32-v15.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","Skills","POST /campaigns/{campaignId}/skills","so32-v15.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}

function DeleteCampaignsBySkill(campaignId, skillId, transferCampaignId){	//done

var updateCampaigndeletePayload ={
  "skills": [
    {
      "skillId": skillId,
      "transferCampaignId": transferCampaignId
    }
  ]
}

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v15.0/campaigns/'+ campaignId + '/skills',
		'type': 'delete',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/x-www-form-urlencoded'
		},
		'data': JSON.stringify(updateCampaigndeletePayload),
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","Skills","DELETE /campaigns/{campaignId}/skills","so32-v15.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","Skills","DELETE /campaigns/{campaignId}/skills","so32-v15.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}

function getSmsHistoricalTranscriptByContactID(contactId,businessUnitId) {
var getSmsTranscriptpayload ={
  "businessUnitId":businessUnitId}
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v15.0/contacts/'+contactId+'/sms-historical-transcript',
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
        },
        'data': getSmsTranscriptpayload,
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Contacts","GET /contacts/{contactId}/sms-historical-transcript","so32-v15.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Contacts","GET /contacts/{contactId}/sms-historical-transcript","so32-v15.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}

function getSmsHistoricalContacts(ani,businessUnitId,skillId) {
var getSmscontactspayload ={
  "businessUnitId":businessUnitId,
  "ani":ani,
  "skillId":skillId}
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v15.0/contacts/sms-historical-contacts',
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
        },
        'data': getSmscontactspayload,
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Contacts","GET /contacts/sms-historical-contacts","so32-v15.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Contacts","GET /contacts/sms-historical-contacts","so32-v15.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}

function PutUnavailableCodesByID(unavailableCodeId,unavailableCodeName,postContact, agentTimeout, isActive){	//done

var PutUnavailableCodesByIDPayload ={

      "unavailableCodeName": unavailableCodeName,
      "postContact": postContact,
	  "agentTimeout":agentTimeout,
      "isActive": isActive
}

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v15.0/unavailable-codes/'+unavailableCodeId,
		'type': 'PUT',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'data': JSON.stringify(PutUnavailableCodesByIDPayload),
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","Agents","PUT /unavailable-codes/{unavailableCodeId}","so32-v15.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","Agents","PUT /unavailable-codes/{unavailableCodeId}","so32-v15.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}

function PostHoursofOperation(profileName){	//done

var PostHoursofOperationPayload ={
      "profileName": profileName
}

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v15.0/hours-of-operation',
		'type': 'POST',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'data': JSON.stringify(PostHoursofOperationPayload),
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","General","POST /hours-of-operation","so32-v15.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","General","POST /hours-of-operation","so32-v15.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}

function PutHoursofOperationByID(profileId){	//done

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v15.0/hours-of-operation/'+profileId,
		'type': 'PUT',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","General","PUT /hours-of-operation/{profileId}","so32-v15.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","General","PUT /hours-of-operation/{profileId}","so32-v15.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}

function PostPointofContact(){	//done

var PostPointofContactPayload ={
      "pointOfContact": "string",
      "pointOfContactName": "string",
      "skillId": 0,
      "isActive": true,
      "mediaTypeId": 0,
      "scriptName": "string",
      "ivrReportingEnabled": true
}

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v15.0/points-of-contact',
		'type': 'POST',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'data': JSON.stringify(PostPointofContactPayload),
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","General","POST /points-of-contact","so32-v15.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","General","POST /points-of-contact","so32-v15.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}

function PutPointofContactByID(pointOfContactId){	//done

var PutPointofContactByIDPayload ={

        "pointOfContactName": "string",
        "skillId": 0,
        "isActive": true,
        "scriptName": "string",
        "ivrReportingEnabled": true
}

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v15.0/points-of-contact/'+pointOfContactId,
		'type': 'PUT',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'data': JSON.stringify(PutPointofContactByIDPayload),
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","General","PUT /points-of-contact/{pointOfContactId}","so32-v15.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","General","PUT /points-of-contact/{pointOfContactId}","so32-v15.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}

function PostUnavailableCodes(){	//done

var PostUnavailableCodesPayload ={
      "unavailableCodeName": "string",
      "postContact": true,
      "isActive": true
}

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v15.0/unavailable-codes',
		'type': 'POST',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'data': JSON.stringify(PostUnavailableCodesPayload),
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","General","POST /unavailable-codes","so32-v15.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","General","POST /unavailable-codes","so32-v15.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}

function getPhonenumbers(searchString,skip,top) {
						
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v15.0/phone-numbers' + '?searchString=' + searchString + '&skip' + skip + '&top=' + top ,
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
        },
        //'data': getAgentsPayload,
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","General","GET /phone-numbers","so32-v15.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","General","GET /phone-numbers","so32-v15.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}

function getDispositionSkills(updatedSince,isActive,fields,searchString,skip,top,orderBy) {

    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v15.0/dispositions/skills' + '?searchString=' + searchString + '&skip' + skip + '&top=' + top + '&updatedSince=' + updatedSince + '&isActive' + isActive + '&fields=' + fields+'&orderBy=' + orderBy,
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
        },
        //'data': getAgentsPayload,
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Skills","GET /dispositions/skills","so32-v15.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Skills","GET /dispositions/skills","so32-v15.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}

function postEmailSaveDraft(sessionId,contactId)
  {
	
	var CreateEmailSaveDraftpayload ={
		"toAddress":"",
		"fromAddress":"",
		"bccAddress":"" ,
		"campaignId":0,
		"subject":"",
		"bodyHtml":"" ,
		"attachments":"",
		"attachmentNames":"",
		"draftEmailGuidStr":"",
		"primaryDispositionId":0,
		"secondaryDispositionId":0,
		"tags": "",
		"notes": "",
		"originalAttachmentNames":""
}
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v15.0/agent-sessions/'+sessionId +'/interactions/'+contactId+'/email-save-draft',
		'type': 'POST',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'data': JSON.stringify(CreateEmailSaveDraftpayload),
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Agents","Emails","POST /email-save-draft","so32-v15.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Agents","Emails","POST /email-save-draft","so32-v15.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}

function postAddText(sessionId)
  {
	
	var CreateAddTextpayload ={   
     
	  "MediaType": 0
}
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v15.0/agent-sessions/'+sessionId+'/interactions/add-text',
		'type': 'POST',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'data': JSON.stringify(CreateAddTextpayload),
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Agents","ChatRequests","POST /add-text","so32-v15.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Agents","ChatRequests","POST /add-text","so32-v15.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}

function putUnavailableCodesbyIdTeams(unavailableCodeId){
var putUnavailableCodesbyIdTeamPayload ={
        "securityUser": " ",
		"teams": [
    {
      "teamId": "string"
    }
  ]
}

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v15.0/unavailable-codes/'+unavailableCodeId+'/teams',
		'type': 'PUT',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'data': JSON.stringify(putUnavailableCodesbyIdTeamPayload),
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","Agents","PUT /unavailable-codesbyID/Teams","so32-v15.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","Agents","PUT /unavailable-codesbyID/Teams","so32-v15.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}

function postJobSync()
  {
	
	var CreateJobSyncpayload ={   
     
	  "entityName": "qm_workflows",
	  "version":"1",
	   "startDate": "2019-07-10",
	   "endDate": "2019-07-15"
}
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': CXone_baseURI + '/data-extraction/v1/jobs/sync',
		'type': 'POST',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'data': JSON.stringify(CreateJobSyncpayload),
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Data Extraction","Extract data","POST /jobs​/sync","so32-v15.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Data Extraction","Extract data","POST /jobs​/sync","so32-v15.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}

function getMediaplayBackByID(contactid) {

var getgetMediaplayBackByIDpayload={   
	  "media-type":"chat",
	   "exclude-waveform": "true"
}
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': CXone_baseURI + '/media-playback/v1/contacts/'+contactid,
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
        },
        'data': getgetMediaplayBackByIDpayload,
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("mediaplayback","AccessingRecordingMedia","GET /media-playback/v1/contacts/{contactId}","so32-v1.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("mediaplayback","AccessingRecordingMedia","GET /media-playback/v1/contacts/{contactId}","so32-v1.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}

function getMediaplayBackBysegmentID(contactid,segmentid) {

var getMediaplayBackBysegmentIDpayload={   
	  "media-type":"chat",
	   "exclude-waveform": "true"
}
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': CXone_baseURI + '/media-playback/v1/contacts/'+contactid+'/segments/'+segmentid,
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
        },
        'data': getMediaplayBackBysegmentIDpayload,
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("mediaplayback","AccessingRecordingMedia","/media-playback​/v1​/contacts​/{contactId}​/segments​/{segmentId}","so32-v1.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("mediaplayback","AccessingRecordingMedia","/media-playback​/v1​/contacts​/{contactId}​/segments​/{segmentId}","so32-v1.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}

function getMediaplayBackcontacts(acdCallId) {

var getMediaplayBackcontactspayload={   
	  "media-type":"chat",
	   "exclude-waveform": "true"
}
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': CXone_baseURI + '/media-playback/v1/contacts'+acdCallId,
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
        },
        'data': getMediaplayBackcontactspayload,
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("mediaplayback","AccessingRecordingMedia","​/media-playback​/v1​/contacts","so32-v1.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("mediaplayback","AccessingRecordingMedia","​/media-playback​/v1​/contacts","so32-v1.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}

function getActiveFlag(activeFlag) {

    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v16.0/workflow-data/list'+activeFlag,
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
        },
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("admin","workflow","GET /services/v16.0/workflow-data/list","so32-v16.0", statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("admin","workflow","GET /services/v16.0/workflow-data/list","so32-v16.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}

function postWorkflowData(){
	
	var postworkflowpayload ={
  "profile": {
    "ProfileName": "string",
    "Description": "string",
    "ProfileID": 0
  },
  "data": {
    "date": {
      "Value": [
        "string"
      ],
      "Visible": "string",
      "Type": "string",
      "Ref": "string"
    }
  }
}
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v16.0/workflow-data',
		'type': 'POST',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'data': JSON.stringify(postworkflowpayload),
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("admin","workflow","POST /services/v16.0/workflow-data","so32-v16.0", statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("admin","workflow","POST /services/v16.0/workflow-data","so32-v16.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}

function getWorkflowDataById(workflowDataId) {

    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v16.0/workflow-data/'+ workflowDataId,
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
        },
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("admin","workflow","GET /services/v16.0/workflow-data/ID","so32-v16.0", statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("admin","workflow","GET /services/v16.0/workflow-data/ID","so32-v16.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}

function putWorkflowDataById(workflowDataId){

 var putworkflowpayload ={
  "profile": {
    "ProfileName": "string",
    "Description": "string",
    "ProfileID": 0
  },
  "data": {
    "date": {
      "Value": [
        "string"
      ],
      "Visible": "string",
      "Type": "string",
      "Ref": "string"
    }
  }
}

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v16.0/workflow-data/'+workflowDataId,
		'type': 'PUT',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'data': JSON.stringify(putworkflowpayload),
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","workflow","PUT /workflow-data","so32-v16.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","workflow","PUT /workflow-data","so32-v16.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}

function putWorkflowDataByIdDeactivate(workflowDataId){

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v16.0/workflow-data/'+workflowDataId+'/deactivate',
		'type': 'PUT',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","workflow","PUT /workflow-data/id/deactivate","so32-v16.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","workflow","PUT /workflow-data/id/deactivate","so32-v16.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}

function putWorkflowDataByIdActivate(workflowDataId){

	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v16.0/workflow-data/'+workflowDataId+'/activate',
		'type': 'PUT',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Admin","workflow","PUT /workflow-data/id/activate","so32-v16.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","workflow","PUT /workflow-data/id/activate","so32-v16.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}

function getJobs() {

    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': CXone_baseURI + '/data-extraction/v1/jobs',
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
        },
        //'data': getAgentsPayload,
        'success': function (result, status, statusCode) {
            //Process success actions
           ConstructArray("Data Extraction","Extract data","get /jobs​","so32-v15.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Data Extraction","Extract data","get /jobs​","so32-v15.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
        }
    });
}

function postJobs()
  {
	
	var CreateJobspayload ={   
     
	  "entityName": "qm_workflows",
	  "version":"1",
	   "startDate": "2019-07-10",
	   "endDate": "2019-07-15"
}
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': CXone_baseURI + '/data-extraction/v1/jobs',
		'type': 'POST',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'data': JSON.stringify(CreateJobspayload),
		'success': function (result, status, statusCode) {
			//Process success actions
			ConstructArray("Data Extraction","Extract data","POST /jobs​","so32-v15.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Data Extraction","Extract data","POST /jobs​","so32-v15.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
		}
	});
}

function getJobsByID(jobsId) {

    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': CXone_baseURI + '/data-extraction/v1/jobs/'+jobsId,
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
        },
        //'data': getAgentsPayload,
        'success': function (result, status, statusCode) {
            //Process success actions
           ConstructArray("Data Extraction","Extract data","get /jobs​/jobsID","so32-v15.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Data Extraction","Extract data","get /jobs​/jobsID","so32-v15.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
        }
    });
}

function getBusinessunitTimezone() {
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + 'services/v17/business-unit/time-zones',
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/x-www-form-urlencoded'
        },
        'success': function (result, status, statusCode) {
            //Process success actions
           ConstructArray("Admin","General","get /business-unit/time-zones","so32-v17.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","General","get /business-unit/time-zones","so32-v17.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
        }
    });
}

function getSuppresedContacts() {
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + 'services/v17/suppressed-contact',
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/x-www-form-urlencoded'
        },
        'success': function (result, status, statusCode) {
            //Process success actions
           ConstructArray("Admin","General","get /suppressed-contact","so32-v17.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","General","get /suppressed-contact","so32-v17.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
        }
    });
}

function PostSuppresedContacts(){//done
    var PostSuppresedContactsPayload ={
        "startDate": "string",
        "endDate": "string",
        "value": "string",
        "skills":"string"}
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v17/suppressed-contact',
		'type': 'POST',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'data': JSON.stringify(PostSuppresedContactsPayload),
		'success': function (result, status, statusCode) {
            //Process success actions
           ConstructArray("Admin","General","Post /suppressed-contact","so32-v17.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","General","Post /suppressed-contact","so32-v17.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
        }
	});
}

function putbusinessunitTimezone(){
 var putbusinessunitTimezonepayload ={
  "profile": {
    "timezones": "string",
    "ProfileID": 0}}
$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v17/business-unit/time-zones',
		'type': 'PUT',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'data': JSON.stringify(putbusinessunitTimezonepayload),
		'success': function (result, status, statusCode) {
            //Process success actions
           ConstructArray("Admin","General","Put /business-unit/time-zones","so32-v17.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","General","Put /business-unit/time-zones","so32-v17.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
        }
	});
}

function PutSkillIDParameterTimezone(skillId) {
	var PutSkillIDParameterTimezonePayload = {
		'agents': [
			{
				'skillId': 'integer - required',
				'timeZoneSettings': 'string'
			}
		]
	}
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + 'services/v17/skills/' + skillId + '/parameters/time-zones',
		'type': 'PUT',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json'
		},
		'data': JSON.stringify(PutSkillIDParameterTimezonePayload),
		'success': function (result, status, statusCode) {
            //Process success actions
           ConstructArray("Admin","Skill","Put /parameters/time-zones","so32-v17.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","Skill","Put /parameters/time-zones","so32-v17.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
        }
	});
}

function getTimeZoneBySkillId(skillId) {
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + 'services/v17/skills/' + skillId + '/parameters/time-zones',
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/x-www-form-urlencoded'
        },
        'data': getAgentAddressBooksPayload,
        'success': function (result, status, statusCode) {
            //Process success actions
           ConstructArray("Admin","Skill","Get /parameters/time-zones","so32-v17.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","Skill","Get /parameters/time-zones","so32-v17.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
        }
    });
}
}

function deleteSuppressedContactBySuppressedContactId(int suppressedContactId) {
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + 'services/{version}/suppressed-contact/' + suppressedContactId,
        'type': 'DELETE',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/x-www-form-urlencoded'
        },
        'success': function (result, status, statusCode) {
            //Process success actions
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            return false;
        }
    });
}

function getSuppressedContactBySuppressedContactId(int suppressedContactId) {
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + 'services/{version}/suppressed-contact/' + suppressedContactId,
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/x-www-form-urlencoded'
		},
		'success': function (result, status, statusCode) {
			//Process success actions
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			return false;
		}
	});
}

function putSuppressedContactBySuppressedContactId(int suppressedContactId) {
	var putSuppressedContactBySuppressedContactIdPayload = {
  'suppressedContactData': {
    'startDate': '2019-12-04T13:06:48.485Z',
    'endDate': '2019-12-04T13:06:48.485Z',
    'value': 'Unknown Type: string,null',
    'skills': 'Unknown Type: string,null'
  }
}
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + 'services/{version}/suppressed-contact/' + suppressedContactId,
		'type': 'PUT',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json'
		},
		'data': JSON.stringify(putSuppressedContactBySuppressedContactIdPayload),
		'success': function (result, status, statusCode) {
			//Process success actions
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			return false;
		}
	});
}

function getContactsByIdHierachy(contactID) {
    var getContactsByIdHierachypayload ={
        "contactId":contactId}
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/{version}/contacts/{contactId}/hierarchy',
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8'
        },
        'data': getContactsByIdHierachypayload,
        'success': function (result, status, statusCode) {
            //Process success actions
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            return false;
			
        }
    });
}
}

function getBusinessunitTimezone() {
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + 'services/v17/business-unit/time-zones',
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/x-www-form-urlencoded'
        },
        'success': function (result, status, statusCode) {
            //Process success actions
           ConstructArray("Admin","General","get /business-unit/time-zones","so32-v17.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","General","get /business-unit/time-zones","so32-v17.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
        }
    });
}

function getSuppresedContacts() {
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + 'services/v17/suppressed-contact',
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/x-www-form-urlencoded'
        },
        'success': function (result, status, statusCode) {
            //Process success actions
           ConstructArray("Admin","General","get /suppressed-contact","so32-v17.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","General","get /suppressed-contact","so32-v17.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
        }
    });
}

function PostSuppresedContacts(){
    var PostSuppresedContactsPayload ={
        "startDate": "string",
        "endDate": "string",
        "value": "string",
        "skills":"string"}
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v17/suppressed-contact',
		'type': 'POST',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'data': JSON.stringify(PostSuppresedContactsPayload),
		'success': function (result, status, statusCode) {
            //Process success actions
           ConstructArray("Admin","General","Post /suppressed-contact","so32-v17.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","General","Post /suppressed-contact","so32-v17.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
        }
	});
}

function putbusinessunitTimezone(){
 var putbusinessunitTimezonepayload ={
  "profile": {
    "timezones": "string",
    "ProfileID": 0}}
$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v17/business-unit/time-zones',
		'type': 'PUT',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'data': JSON.stringify(putbusinessunitTimezonepayload),
		'success': function (result, status, statusCode) {
            //Process success actions
           ConstructArray("Admin","General","Put /business-unit/time-zones","so32-v17.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","General","Put /business-unit/time-zones","so32-v17.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
        }
	});
}

function PutSkillIDParameterTimezone(skillId) {
	var PutSkillIDParameterTimezonePayload = {
		'agents': [
			{
				'skillId': 'integer - required',
				'timeZoneSettings': 'string'
			}
		]
	}
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + 'services/v17/skills/' + skillId + '/parameters/time-zones',
		'type': 'PUT',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json'
		},
		'data': JSON.stringify(PutSkillIDParameterTimezonePayload),
		'success': function (result, status, statusCode) {
            //Process success actions
           ConstructArray("Admin","Skill","Put /parameters/time-zones","so32-v17.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","Skill","Put /parameters/time-zones","so32-v17.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
        }
	});
}

function getTimeZoneBySkillId(skillId) {
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + 'services/v17/skills/' + skillId + '/parameters/time-zones',
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/x-www-form-urlencoded'
        },
        'data': getAgentAddressBooksPayload,
        'success': function (result, status, statusCode) {
            //Process success actions
           ConstructArray("Admin","Skill","Get /parameters/time-zones","so32-v17.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","Skill","Get /parameters/time-zones","so32-v17.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
        }
    });
}

function deleteSkillsBySkillIdAgentId(skillId,agentId) {
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + 'services/{version}/skills/' + skillId + '/agents/' + agentId,
        'type': 'DELETE',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/x-www-form-urlencoded'
        },
        'success': function (result, status, statusCode) {
            return result;
            //Process success actions
            ConstructArray("Admin","Skill","Delete /skills/{skillId}/agents/{agentId}","so32-v17.0",statusCode.status + ":" + statusCode.statusText);
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            ConstructArray("Admin","Skill","Delete /skills/{skillId}/agents/{agentId}","so32-v17.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
        }
    });
}

function getScripts() {
    	var getScriptsPayload = {
		'scriptPath': 'scriptPath',
		'scriptId': 'scriptId'
        } 
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + 'services/{version}/scripts',
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/x-www-form-urlencoded'
		},
		'data': getScriptsPayload,
		'success': function (result, status, statusCode) {
			//Process success actions
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			return false;
		}
	});
}

function postScript() {
    var postScriptPayload = {
        'scriptPath': 'string - required',
        'body': 'string - required'
    }
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + 'services/{version}/scripts/',
        'type': 'POST',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json'
        },
        'data': JSON.stringify(postScriptPayload),
        'success': function (result, status, statusCode) {
            //Process success actions
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            return false;
        }
    });
}

PutupdateScripts(scriptPath, lockScript ){

var PutScriptsPayload = {
         "scriptPath":  scriptPath, 
         "lockScript":  lockScript
}
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/{version}/scripts',
        'type': 'PUT',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8',
			'Accept':'application/json, text/javascript, */*; q=0.01'
        },
        'data': JSON.stringify(PutScriptsPayload),
        'success': function (result, status, statusCode) {
            //Process success actions
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            return false;
			
        }
    });
}

function postScriptKick() {
    var postScriptKickPayload = {
        'scriptPath': 'string - required'
    }
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + 'services/{version}/scripts/kick',
        'type': 'POST',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json'
        },
        'data': JSON.stringify(postScriptPayload),
        'success': function (result, status, statusCode) {
            //Process success actions
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            return false;
        }
    });
}

function GetScriptsHistoryByName() {
    	var getScriptsHistoryByNamePayload = {
		'scriptPath': 'scriptPath',
		'scriptId': 'scriptId'
        } 
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + 'services/{version}/scripts',
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/x-www-form-urlencoded'
		},
		'data': getScriptsHistoryByNamePayload,
		'success': function (result, status, statusCode) {
			//Process success actions
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			return false;
		}
	});
}

function PutSkillParameterListManagement(skillId){
    var SkillJsonData = {
	"displayField1Name": "string",
	"displayField2Name": "string",
	"listOrderingOptions": [
    {
      "orderType": "string",
      "direction": "string"
    }
  ]
}
 
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v18.0/skills/' + skillId + '/parameters/list-management',
        'type': 'PUT',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8',
            'Accept':'application/json, text/javascript, */*; q=0.01'
        },
        'data': JSON.stringify(SkillJsonData),
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Skills","PUT /skills/{skillId}/parameters/list-management","so32-v18.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Skills","PUT /skills/{skillId}/parameters/list-management","so32-v18.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}

function GetSkillsParameters() {
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + 'services/{version}/skills/parameters',
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/x-www-form-urlencoded'
		},
		'data': getSkillsParametersPayload,
		'success': function (result, status, statusCode) {
			//Process success actions
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			return false;
		}
	});
}

function PutSkillParameterCadenceSettings(skillId){
    var SkillJsonData = {
	"attemptMode": "string",
	"recordRequestMode": "string",
	"destinationRetryRestMinutes": 0,
	"maximumAttempts": [
    {
      "fieldName": "string",
      "attempts": 0
    }
	],
	"cadences": [
    {
      "fieldName": "string",
      "attempts": 0,
      "timeConstraints": {
        "weekdayTimeConstraints": [
          {
            "startTime": "string",
            "endTime": "string"
          }
        ],
        "weekendTimeConstraints": [
          {
            "startTime": "string",
            "endTime": "string"
          }
        ]
      }
    }
  ]
}
 
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v18.0/skills/' + skillId + '/parameters/cadence-settings',
        'type': 'PUT',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8',
            'Accept':'application/json, text/javascript, */*; q=0.01'
        },
        'data': JSON.stringify(SkillJsonData),
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Skills","PUT /skills/{skillId}/parameters/cadence-settings","so32-v18.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Skills","PUT /skills/{skillId}/parameters/cadence-settings","so32-v18.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}

function GetSkillsSkillIdParameters(skillId) {
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + 'services/{version}/skills' +skillId+'/parameters',
		'type': 'GET',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/x-www-form-urlencoded'
		},
		'data': getSkillsParametersPayload,
		'success': function (result, status, statusCode) {
			//Process success actions
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			return false;
		}
	});
}
function PostContactsetDisposition(contactId){
    var PostContactsetDispositionPayload ={
	"dispositionInfo": {
    "Skill": "1007",
    "DispositionCode": "1",
    "CallbackNumber": "",
    "CallbackTime": "",
    "notes": "",
    "CommitmentAmount": ""
  }
}
	$.ajax({
		//The baseURI variable is created by the result.resource_server_base_uri,
		//which is returned when getting a token and should be used to create the URL base
		'url': baseURI + '/services/v18/contacts'+contactId+'/set-disposition',
		'type': 'POST',
		'headers': {
			//Use access_token previously retrieved from inContact token service
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/json; charset=UTF-8'
		},
		'data': JSON.stringify(PostContactsetDispositionPayload),
		'success': function (result, status, statusCode) {
            //Process success actions
           ConstructArray("Admin","General","Post /set-disposition,"so32-v18.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","General","Post /set-disposition","so32-v18.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
        }
	});
}
}

function getBusinessunitOutboundRoutes() {
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + 'services/v18/business-unit/outbound-routes',
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/x-www-form-urlencoded'
        },
        'success': function (result, status, statusCode) {
            //Process success actions
           ConstructArray("Admin","General","get /business-unit/outbound-routes","so32-v18.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","General","get /business-unit/outbound-routes","so32-v18.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
        }
    });
}

function getclientdata() {
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + 'services/v19/agents/client-data',
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/x-www-form-urlencoded'
        },
        'success': function (result, status, statusCode) {
            //Process success actions
           ConstructArray("Realtime","Realtime","get /agents/client-data","so32-v19.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Realtime","Realtime","get /agents/client-data","so32-v19.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
        }
    });
}

function putcientdata(){
    var ClientJsonData = {
  "agentId": "",
  "dataSet": "{ \"settings\": \"true\" }"
}
 
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v19/agents/client-data',
        'type': 'PUT',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8',
            'Accept':'application/json, text/javascript, */*; q=0.01'
        },
        'data': JSON.stringify(ClientJsonData),
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Skills","PUT /agents/client-data","so32-v19.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Skills","PUT /agents/client-data","so32-v19.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}
function postsmsoutbound(sessionId) {
    var postsmsoutboundPayload = {
  "phoneNumber": "",
  "skillId": 1000,
  "parentContactId": null
}
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + 'services/{version}/agent-sessions/'+ sessionId +'/interactions/sms-outbound',
        'type': 'POST',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json'
        },
        'data': JSON.stringify(postsmsoutboundPayload),
        'success': function (result, status, statusCode) {
            //Process success actions
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            return false;
        }
    });
}

function removeprospects (sourceName) {
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + 'services/{version}/lists/call-lists/' + sourceName + '/removeProspects',
        'type': 'DELETE',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/x-www-form-urlencoded'
        },
        'success': function (result, status, statusCode) {
            return result;
            //Process success actions
            ConstructArray("Admin","Lists","Delete /lists/call-lists/{sourceName}/removeProspects","so32-v19.0",statusCode.status + ":" + statusCode.statusText);
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            ConstructArray("Admin","Lists","Delete /lists/call-lists/{sourceName}/removeProspects","so32-v19.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
        }
    });
}

function putpersistentcontacts(contactId){
    var persistentcontactsJsonData = {
	"persistentContact": {
    "SkillId": 0,
    "TargetAgentId": 0,
    "InitialPriority": 0,
    "Acceleration": 0,
    "MaxPriority": 0
  }
	}
 
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + '/services/v19/persistent-contacts/'+ contactId,
        'type': 'PUT',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json; charset=UTF-8',
            'Accept':'application/json, text/javascript, */*; q=0.01'
        },
        'data': JSON.stringify(persistentcontactsJsonData),
        'success': function (result, status, statusCode) {
            //Process success actions
            ConstructArray("Admin","Contacts","PUT /persistent-contacts/{contactId}","so32-v19.0",statusCode.status + ":" + statusCode.statusText);
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            ConstructArray("Admin","Contacts","PUT /persistent-contacts/{contactId}","so32-v19.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
			
        }
    });
}

function getagentsettings(agentId) {
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + 'services/v19/agents/' + agentId + '/agent-settings',
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/x-www-form-urlencoded'
        },
        'success': function (result, status, statusCode) {
            //Process success actions
		ConstructArray("Admin","Agents","get /agents{agentId}/agent-settings","so32-v19.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
		ConstructArray("Admin","Agents","get /agents{agentId}/agent-settings","so32-v19.0",statusCode.status + ":" + errorThrown);
			return false;
			
        }
    });
}
function getscriptsbyId(scriptId) {
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + 'services/v19/scripts/' + scriptId,
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/x-www-form-urlencoded'
        },
        'success': function (result, status, statusCode) {
            //Process success actions
           ConstructArray("Admin","General","Get /scripts/{scriptId}","so32-v19.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","General","Get /scripts/{scriptId}","so32-v19.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
        }
    });
}
function getscriptssearch() {
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + 'services/v19/scripts/search',
        'type': 'GET',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/x-www-form-urlencoded'
        },
        'success': function (result, status, statusCode) {
            //Process success actions
           ConstructArray("Admin","General","get /scripts/search","so32-v19.0",statusCode.status + ":" + statusCode.statusText);
			return result;
		},
		'error': function (XMLHttpRequest, textStatus, errorThrown) {
			//Process error actions
			ConstructArray("Admin","General","get /scripts/search","so32-v19.0",XMLHttpRequest.status+ ":" + errorThrown);
			return false;
			
        }
    });
}

function deletescripts() {
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + 'services/{version}/scripts',
        'type': 'DELETE',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/x-www-form-urlencoded'
        },
        'success': function (result, status, statusCode) {
            return result;
            //Process success actions
            ConstructArray("Admin","Skill","Delete /scripts","so32-v19.0",statusCode.status + ":" + statusCode.statusText);
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            ConstructArray("Admin","Skill","Delete /scripts","so32-v19.0",XMLHttpRequest.status+ ":" + errorThrown);
            return false;
        }
    });
}

function posttransformnumbers(agentpatternId) {
    var posttransformnumbersPayload = {
  "inputPhoneNumbers": [
    {
      "inputPhoneNum": "",
      "externalId": ""
    },
    {
      "inputPhoneNum": "",
      "externalId": ""
    }
  ]
}
    $.ajax({
        //The baseURI variable is created by the result.resource_server_base_uri,
        //which is returned when getting a token and should be used to create the URL base
        'url': baseURI + 'services/{version}/agent-patterns/' + agentpatternId + '/transform-phonenumbers',
        'type': 'POST',
        'headers': {
            //Use access_token previously retrieved from inContact token service
            'Authorization': 'bearer ' + accessToken,
            'content-Type': 'application/json'
        },
        'data': JSON.stringify(posttransformnumbersPayload),
        'success': function (result, status, statusCode) {
            //Process success actions
            return result;
        },
        'error': function (XMLHttpRequest, textStatus, errorThrown) {
            //Process error actions
            return false;
        }
    });
}




