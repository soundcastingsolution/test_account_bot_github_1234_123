// importing the scalaj-http library and spray.json 
package scalaj.http
import java.io._
import com.typesafe.config.ConfigFactory
import spray.json._
import spray.json.DefaultJsonProtocol

object Functions extends DefaultJsonProtocol {


var writer = new PrintWriter(new File("result.html" ))
case class accessTokenStruct(access_token:String)
case class sessionIdStruct(sessionId:String)
implicit var AccessTokenFormat = jsonFormat1(accessTokenStruct)
implicit var SessionIdFormat = jsonFormat1(sessionIdStruct)

//loading config file here
var configData = ConfigFactory.parseFile(new File("data.conf"))
var baseURI = configData.getString("data.baseURI")
var username = configData.getString("data.username")
var password = configData.getString("data.password")
var tokenServiceUri = configData.getString("data.tokenServiceUri")
var stationPhoneNumber = configData.getString("data.phoneNumber")
var cluster = configData.getString("data.cluster")
var CXone_baseURI=configData.getString("data.CXone_baseURI")
var accessToken = "";

    def init() = {
	initTable();
	initAccessToken();
	//initSessionId();
}

    def initAccessToken()
	{



		var response = Http(tokenServiceUri)
				.postData(raw"""{"grant_type": "password","password": "$password","username" :"$username"}""")
				.headers(("Authorization", "basic " + "aW50ZXJuYWxAaW5Db250YWN0IEluYy46UVVFNVFrTkdSRE0zUWpFME5FUkRSamczUlVORFJVTkRRakU0TlRrek5UYz0="),("content-type", "application/json; charset=UTF-8"), ("Accept", "application/json, text/javascript, */*; q=0.01"),("Accept-Encoding", "gzip")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
				
		var data: accessTokenStruct = response.body.parseJson.convertTo[accessTokenStruct]
		println("------------------------------------------")
		println("generating access token")
		println(data.access_token)
		accessToken = data.access_token;
				
    }
    def initTable()
	{
    var content = """<!DOCTYPE html>
    <html>
    <head>
    <link rel="stylesheet" type="text/css" href="table.css">
    </head>
    <body class="rTable">
    <h2>API Test Results using Scala</h2>
    <title>Scala</title>
    <br/>
    <div class="rTableHead small"><strong>APIName</strong></div>
    <div class="rTableHead small"><span style="font-weight: bold;">APISubType</span></div>
    <div class="rTableHead med"><span style="font-weight: bold;">APIType</span></div>
    <div class="rTableHead small"><span style="font-weight: bold;">Version</span></div>
    <div class="rTableHead"><span style="font-weight: bold;">APIResult</span></div>
    </div>
    </body>
    </html>"""
    writer.write(content)
	writer.close()
    }

def construct(APIName:String,APISubType:String,APIType:String,Version:String,Status:Int,Description:String)
   {
	var result = """
		<div class="rTableRow">
		<div class="rTableCell small">%s</div>
		<div class="rTableCell small">%s</div>
		<div class="rTableCell med">%s</div>
		<div class="rTableCell small">%s</div>
		<div class="rTableCell">%d:%s</div>
		</div>
		""";
		var execute = (result).format(APIName,APISubType,APIType,Version,Status,Description);
		var fw = new FileWriter("result.html", true)
		try {
			fw.write(execute)
		}
		finally fw.close() 
		println(APIName +" => "+ APISubType +" => "+ APIType +" => "+ Version +" => "+ Status +" : "+ Description)
		 println("----------------------------------------------------------------------------------------------")
    }

//Admin--> AddressBook V4
def getAddressBooks() 
    {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v4.0/address-books")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","AddressBook","GET /address-books",cluster + " - v4.0" ,response.code, response.statusLine)
			
		}
		else{
			//No token - get one or handle error
		}
	}

//Admin--> AddressBook V6
def createAddressBook(data:String) {
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
{
		//Add all necessary headers and Make the http request
		var response = Http(baseURI+ "/services/v6.0/address-books")
				.postData(data)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","AddressBook","POST /address-books",cluster + " - v6.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}

//Admin--> AddressBook V4	
def deleteAddressBook(addressBookId:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v4.0/address-books/" + addressBookId)
					.method("DELETE")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString

			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		construct("Admin","AddressBook","DELETE /address-books/{addressBookId}",cluster + " - v4.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> AddressBook V4
def assignAddressBook(addressBookId:String,data:String) {
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v4.0/address-books/" + addressBookId + "/assignment")
				.postData(data)
			    .headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
			    .option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","AddressBook","POST /address-books/{addressBookId}/assignment",cluster + " - v4.0" ,response.code, response.statusLine)
		 }
		else{
			   //No token - get one or handle error
		 }
	}


//Admin--> AddressBook V4
def getDynamicAddressBookEntries(addressBookId:String,fullLoad:String,updatedSince:String,skip:String,top:String,orderBy:String) {
       		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI +"/services/v4.0/address-books/" + addressBookId + "/dynamic-entries")
				.params("fullLoad"-> fullLoad,
				"updatedSince" -> updatedSince,
				"skip"-> skip,
				"top"-> top,
				"orderBy"-> orderBy)
			.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
			.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
				
		construct("Admin","AddressBook","GET /address-books/{addressBookId}/dynamic-entries",cluster + " - v4.0" ,response.code, response.statusLine)
          	
		 }
      else{
			   //No token - get one or handle error
		 }
	}
	

	
//Admin--> AddressBook V4
def addDynamicAddressBookEntries(addressBookId:String,data:String){
	
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v4.0/address-books/" + addressBookId + "/dynamic-entries")
				.postData(data)
				.method("put")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString

			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","AddressBook","PUT /address-books/{addressBookId}/dynamic-entries",cluster + " - v4.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}

//Admin--> AddressBook V4
def deleteDynamicAddressBookEntry(addressBookId:String,externalId:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v4.0/address-books/" + addressBookId + "/dynamic-entries/" + externalId)
					.method("DELETE")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString

			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","AddressBook","DELETE /address-books/{addressBookId}/dynamic-entries/{externalId}",cluster + " - v4.0",response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> AddressBook V12	
def getStandardAddressBookEntries(addressBookId:String,updatedSince:String,searchString:String,fields:String,skip:String,top:String,orderBy:String) {

		//Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v12.0/address-books/" + addressBookId + "/entries")
				.params("updatedSince" -> updatedSince,
				 "searchString"-> searchString,
				 "fields"->fields,
				 "skip"-> skip,
				 "top"-> top,
				 "orderBy"-> orderBy)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","AddressBook","GET /address-books/{addressBookId}/entries",cluster + " - v12.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> AddressBook V3	
def createStandardAddressBookEntries(addressBookID:String,data:String) {
     		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v3.0/address-books/" + addressBookID + "/entries")
					.postData(data)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
					.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","AddressBook","POST /address-books/{addressBookId}/entries",cluster + " - v3.0" ,response.code, response.statusLine)
		 }
		else{
			   //No token - get one or handle error
		 }
	}

	
//Admin--> AddressBook V3	
def updateStandardAddressBookEntries(addressBookId:String,data:String){
	 
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v3.0/address-books/" + addressBookId + "/entries" )
				.postData(data)
				.method("put")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","AddressBook","PUT /address-books/{addressBookId}/entries",cluster + " - v3.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}	


//Admin--> AddressBook V3	
def deleteAddressBookEntry(addressBookID:String,entryID:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v3.0/address-books/" + addressBookID + "/entries/" + entryID)
					.method("DELETE")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString

			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","AddressBook","DELETE /address-books/{addressBookId}/entries/{addressBookEntryId}",cluster + " - v3.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}	
	
	

//Admin--> AddressBook V8	
def getAgentAddressBooks(agentId:Int,includeEntries:String,updatedSince:String) {
      		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI +"/services/v8.0/agents/" + agentId + "/address-books")
					.params("includeEntries"-> includeEntries,
					"updatedSince"-> updatedSince)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","AddressBook","GET /agents/{agentId}/address-books",cluster + " - v8.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}	
	
	
//Admin--> AddressBook V8	
def getCampaignAddressBooks(campaignId:String,includeEntries:String,updatedSince:String) {
       		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI +"/services/v8.0/campaigns/" + campaignId + "/address-books")
					.params("includeEntries"-> includeEntries,
					"updatedSince"-> updatedSince)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","AddressBook","GET /campaigns/{campaignId}/address-books",cluster + " - v8.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}


//Admin--> AddressBook V8	
def getAddressBooksBySkillId(skillId:Int,includeEntries:String,updatedSince:String) {
      		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI +"/services/v8.0/skills/" + skillId + "/address-books")
					.params("includeEntries"-> includeEntries,
					"updatedSince"-> updatedSince)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","AddressBook","GET /skills/{skillId}/address-books",cluster + " - v8.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}		

//Admin--> AddressBook V8
def getTeamAddressBooks(teamId:String,includeEntries:String,updatedSince:String) {
       
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI +"/services/v8.0/teams/" + teamId + "/address-books")
					.params("includeEntries"-> includeEntries,
					"updatedSince"-> updatedSince)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","AddressBook","GET /teams/{teamId}/address-books",cluster + " - v8.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}


//Admin--> Agents V13	
def getAgents(updatedSince:String,isActive:String,searchString:String,fields:String,skip:String,top:String,orderBy:String) {
      		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request
		     var response = Http( baseURI +"/services/v13.0/agents")
					.params("updatedSince"-> updatedSince,
					"isActive"-> isActive,
					"searchString"-> searchString,
					"fields"-> fields,
					"skip"-> skip,
					"top"-> top,
					"orderBy"-> orderBy)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","Agents","GET /agents",cluster + " - v13.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}

	
//Admin--> Agents V13	
def PostAgent(data:String) {

         //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v13.0/agents")
				.postData(data)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Agents","POST /agents",cluster + " - v13.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		 }
	}
	

//Admin--> Agents V13	
def GetagentbyAgentID(AgentId:Int,fields:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v13.0/agents/" + AgentId)
					.params("fields"-> fields)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Agents","GET /agents/{agentId}",cluster + " - v13.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
	

//Admin--> Agents V11
def PutAgentbyAgentID(AgentID:Int,data:String) {
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v11.0/agents/" + AgentID)
				.postData(data) 
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Agents","PUT /agents/{agentId}",cluster + " - v11.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		 }
	}	
	
	
//Admin--> Agents V7	
def changeAgentState(agentId:String,data:String) {

          //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v7.0/agents/" + agentId + "/state")
					.postData(data)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
					.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Agents","POST /agents/{agentId}/state",cluster + " - v7.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		}
	}	
	
	
//Admin--> Agents V7.0
def getAllAgentsSkills(updatedSince:String,mediaTypeId:String,outboundStrategy:String,searchString:String,fields:String,skip:String,top:String,orderBy:String,
isSkillActive:String) {
    		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI +"services/v7.0/agents/skills")
					.params("updatedSince"-> updatedSince,
					"mediaTypeId"-> mediaTypeId,
					"outboundStrategy" -> outboundStrategy,
					"searchString"-> searchString,
					"fields"-> fields,
					"skip"-> skip,
					"top"-> top,
					"orderBy"-> orderBy,
					"isSkillActive"-> isSkillActive)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","Agents","GET /agents/skills",cluster + " - v7.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}


//Admin--> Agents V9
def GetAgentByAgentIDGroups(AgentId:Int,fields:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v9.0/agents/" + AgentId + "/groups")
			        .params("fields"-> fields)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Agents","GET /agents/{agentId}/groups",cluster + " - v9.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}		
	

//Admin--> Agents V7	
def deleteSkillsFromAgent(agentId:String,data:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v7.0/agents/" + agentId + "/skills")
				.postData(data)
				.method("DELETE")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString

			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Agents","DELETE /agents/{agentId}/skills",cluster + " - v7.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}	
	
	
//Admin--> Agents V11	
def getAgentSkillsByAgentId(AgentID:Int,updatedSince:String,searchString:String,fields:String,skip:String,top:String,orderBy:String,
MediaTypeId:String,
outboundStrategy:String){

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v11.0/agents/" + AgentID + "/skills")
					.params("updatedSince"-> updatedSince,
					"searchString"-> searchString,
					"fields"-> fields,
					"skip"-> skip,
					"top"-> top,
					"orderBy"-> orderBy,
					"MediaTypeId"-> MediaTypeId,
					"outboundStrategy"-> outboundStrategy)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Agents","GET /agents/{agentId}/skills",cluster + " - v11.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}	
	

//Admin--> Agents V7	
def assignSkillsToAgent(agentId:String,data:String) {
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+"services/v7.0/agents/" + agentId + "/skills")
				.postData(data)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Agents","POST /agents/{agentId}/skills",cluster + " - v7.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		}
	}
	

//Admin--> Agents V7	
def modifySkillsForAgent(agentId:String,data:String){
	
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v7.0/agents/" + agentId + "/skills")
					.postData(data)
					.method("put")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString

			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Agents","PUT /agents/{agentId}/skills",cluster + " - v7.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}		
	
	
//Admin--> Agents V7	
def getAgentUnassignedSkills(agentId:Int,mediaTypeId:String,outboundStrategy:String,searchString:String,fields:String,skip:String,top:String,
orderBy:String,
isSkillActive:String) {
       		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI +"services/v7.0/agents/" + agentId + "/skills/unassigned")
					.params("mediaTypeId"-> mediaTypeId,
					"outboundStrategy"-> outboundStrategy,
					"searchString"-> searchString,
					"fields"-> fields,
					"skip"-> skip,
					"top"-> top,
					"orderBy"-> orderBy,
					"isSkillActive"-> isSkillActive)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","Agents","GET /agents/{agentId}/skills/unassigned",cluster + " - v7.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}	
	
	
//Admin--> Agents V7		
def getAgentsContactsBySkill(startDate:String,endDate:String,mediaTypeId:String,isOutbound:String) {

        //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI +"services/v7.0/agents/skill-data")
					.params("startDate"-> startDate,
					"endDate"-> endDate,
					"mediaTypeId"->mediaTypeId,
					"isOutbound"-> isOutbound)
			.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
			.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","Agents","GET /agents/skill-data",cluster + " - v7.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}	
	
//Admin--> Agents V7
def getAgentContactsBySkill(agentId:Int,startDate:String,endDate:String,mediaTypeId:String,isOutbound:String) {
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
      {
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI +"services/v7.0/agents/" + agentId + "/skill-data")
					.params("startDate"-> "",
					"endDate"-> "",
					"mediaTypeId"->"",
					"isOutbound"-> "")
			.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
			.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","Agents","GET /agents/{agentId}/skill-data",cluster + " - v7.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}
	
//Admin--> Agents V3	
def createCustomEvent(agentId:String,data:String){
	
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v3.0/agents/" + agentId + "/custom-event")
					.postData(data)
					.method("put")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString

			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Agents","PUT /agents/{agentId}/custom-event",cluster + " - v3.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
		
	
//Admin--> Agents V4
def getQuickReplies() {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v4.0/agents/quick-replies")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Agents","GET /agents/quick-replies",cluster + " - v4.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}	
	
	
//Admin--> Agents V4.0
def getAgentQuickReplies(agentId:Int) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v4.0/agents/" + agentId + "/quick-replies")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Agents","GET /agents/{agentId}/quick-replies",cluster + " - v4.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
		
//Admin--> Agents V11	
def PostAgentMessages(data:String) {

         //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v11.0/agents/messages")
				.postData(data)
			    .headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
			    .option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Agents","POST /agents/messages",cluster + " - v11.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		}
	}
		
//Admin--> Agents V11	
def deleteAgentMessagesByMessageId(MessageId:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v11.0/agents/messages/:" + MessageId)
					.method("DELETE")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString

			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Agents","DELETE /agents/messages/{messageid}",cluster + " - v11.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> Agents V3
def getAgentMessages(agentId:Int) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v3.0/agents/" + agentId + "/messages")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Agents","GET /agents/{agentId}/messages",cluster + " - v3.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> Agents V3
def getAgentIndicators(agentId:Int) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v3.0/agents/" + agentId + "/indicators")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Agents","GET /agents/{agentId}/indicators",cluster + " - v3.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}	
	
//Admin--> Agents V1
def endAgentSession(agentId:String) {
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v1.0/agents/" + agentId + "/logout")
					.postData("")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
					.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Agents","POST /agents/{agentId}/logout",cluster + " - v1.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		}
	}


//Admin--> Agents V7
def getAgentsDialingPatterns() {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v7.0/agent-patterns")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Agents","GET /agent-patterns",cluster + " - v7.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}



//Admin--> Agents V6
def getAgentsStates() {
  
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v6.0/agents-states")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Agents","GET /agents-states",cluster + " - v6.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> Agents V13	
def getTeams(fields:String,updatedSince:String,isActive:String,searchString:String,skip:String,top:String,orderBy:String) {
      
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI +"services/v13.0/teams")
					.params("fields"-> fields,
					"updatedSince"-> updatedSince,
					"isActive"-> isActive,
					"searchString"-> searchString,
					"skip"-> skip,
					"top"-> top,
					"orderBy"-> orderBy)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","Agents","GET /teams",cluster + " - v13.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}


//Admin--> Agents V13
def PutTeam(data:String) {

         //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v13.0/teams")
				.postData(data)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Agents","POST /teams",cluster + " - v13.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		 }
	}


//Admin-->  Agents V12
def getTeamById(teamId:Int,fields:String) {

       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI +"services/v12.0/teams/" + teamId)
				.params("fields"-> fields)
			    .headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
			    .option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","Agents","GET /teams/{teamId}",cluster + " - v12.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}

//Admin-->  Agents V12
def PutTeambyTeamID(Teamid:String,data:String) {

         //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v12.0/teams/" + Teamid)
				.postData(data) 
				.method("put")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Agents","PUT /teams/{teamId}",cluster + " - v12.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		 }
	}


//Admin--> Agents V10		
def getTeamAgent(fields:String,updatedSince:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v10.0/teams/agents")
				.params("fields"-> fields,
				"updatedSince"-> updatedSince)
		    	.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Agents","GET /teams/agents",cluster + " - v10.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> Agents V7		
def deleteAgentsFromTeam(teamId:String,data:String) {
  
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v7.0/teams/" + teamId + "/agents")
				.postData(data)
				.method("DELETE")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Agents","DELETE /teams/{teamId}/agents",cluster + " - v7.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
	

//Admin--> Agents V10	
def GetTeamByAgentID(teamId:Int,fields:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v10.0/teams/" + teamId +"/agents")
			.params("fields"-> fields)
			.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Agents","GET /teams/{teamId}/agents",cluster + " - v10.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> Agents V11	
def assignAgentsToTeam(teamId:String,data:String) {
    		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v11.0/teams/" + teamId + "/agents")
				.postData(data)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Agents","POST /teams/{teamId}/agents",cluster + " - v11.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		}
	}

//Admin--> Agents V7
def deleteUnavailableCodesByTeam(teamId:String,data:String) {
 
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v7.0/teams/" + teamId + "/unavailable-codes")
					.postData(data)
					.method("DELETE")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Agents","DELETE /teams/{teamId}/unavailable-codes ",cluster + " - v7.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
	
	
//Admin--> Agents V13
def getUnavailableCodesByTeam(teamId:Int,activeOnly:String) {

        //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI + "services/v13.0/teams/" + teamId + "/unavailable-codes")
			.params("activeOnly"-> activeOnly)
			.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
			.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","Agents","GET /teams/{teamId}/unavailable-codes",cluster + " - v13.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}	

//Admin--> Agents V13
def assignUnavailableCodesToTeam(teamId:String,data:String) {
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v7.0/teams/" + teamId + "/unavailable-codes")
				.postData(data)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Agents","POST /teams/{teamId}/unavailable-codes",cluster + " - v7.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		}
	}
	

//Admin--> ScheduledCallbacks V4
def getAgentScheduledCallbacks(agentId:Int) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v4.0/agents/" + agentId + "/scheduled-callbacks")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","ScheduledCallbacks","GET /agents/{agentId}/scheduled-callbacks ",cluster + " - v4.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> ScheduledCallbacks V4
def createScheduledCallback(data:String) {

       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v4.0/scheduled-callbacks")
			.postData(data)
			.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
			.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","ScheduledCallbacks","POST /scheduled-callbacks",cluster + " - v4.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		}
	}
	
//Admin--> ScheduledCallbacks V4
def deleteScheduledCallback(callbackId:String,data:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI + "services/v4.0/scheduled-callbacks/" + callbackId)
				.postData(data)
				.method("DELETE")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString

			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","ScheduledCallbacks","DELETE /scheduled-callbacks/{callbackId}",cluster + " - v4.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> ScheduledCallbacks V4	
def updateScheduledCallback(callbackId:String,data:String){

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v4.0/scheduled-callbacks/" + callbackId)
					.postData(data)
					.method("put")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","ScheduledCallbacks","PUT /scheduled-callbacks/{callbackId}",cluster + " - v4.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}


//Admin--> ScheduledCallbacks V4		
def getSkillScheduledCallbacks(skillId:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v4.0/skills/" + skillId + "/scheduled-callbacks")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","ScheduledCallbacks","GET /skills/{skillId}/scheduled-callbacks",cluster + " - v4.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> General V11
def GetBrandingProfile(businessUnitId:String,fields:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v11.0/branding-profiles")
				.params("businessUnitId"-> businessUnitId,
				"fields"-> fields)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","GET /branding-profiles",cluster + " - v11.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> General V10
def getBusinessUnit(fields:String,includeTrustedBusinessUnits:String) {

       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI +"services/v10.0/business-unit")
					.params("fields"-> fields,
					"includeTrustedBusinessUnits"-> includeTrustedBusinessUnits)
			.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
			.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","General","GET /business-unit",cluster + " - v10.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}

//Admin--> General V7
def getCountries() {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v7.0/countries")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","GET /countries",cluster + " - v7.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> General V7
def getStatesByCountryId(countryId:String,countryid:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v7.0/countries/" + countryId + "/states")
				.params("countryid"-> countryid)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","GET /countries/{countryId}/states",cluster + " - v7.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
	

//Admin--> General V7
def getDataDefinitionTypes() {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v7.0/data-definitions/data-types")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","GET /data-definitions/data-types",cluster + " - v7.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}

//Admin--> General V10
def GetDispostion(skip:String,top:String,searchString:String,fields:String,orderby:String,isPreviewDispositions:String,updatedSince:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v10.0/dispositions")
				.params("skip"-> skip,
				"top"-> top,
				"searchString"-> searchString,
				"fields"-> fields,
				"orderby"-> orderby,
				"isPreviewDispositions"-> isPreviewDispositions,
				"updatedSince"-> updatedSince)
     			.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","GET /dispositions",cluster + " - v10.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> General V8
def DeleteFiles(data:String) {
 
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v8.0/files")
				.postData(data)
				.method("DELETE")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","DELETE /files",cluster + " - v8.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> General V4
def getFile(fileName:String) {
     
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI +"services/v4.0/files")
				.params("fileName"-> fileName)
			    .headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","General","GET /files",cluster + " - v4.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}

//Admin--> General V8
def PostCreateFileName(data:String) {

         //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v8.0/files")
				.postData(data)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","General","POST /files ",cluster + " - v8.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		}
	}

//Admin--> General V8
def PutupdateFile(data:String){
	
	//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v8.0/files")
					.postData(data)
					.method("put")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString

			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","PUT /files",cluster + " - v8.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	

//Admin--> General V9	
def GetFilesExternal(folderPath:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v9.0/files/external")
				.params("folderPath"-> folderPath)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","GET /files/external",cluster + " - v9.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> General V9	
def PostCreateFileExternal(reportId:String,data:String) {

       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v9.0/files/external")
				.postData(data)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","General","POST /files/external",cluster + " - v9.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		}
	}
	

//Admin--> General V9	
def PutupdateFilesExternal(data:String){
	
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v9.0/files/external")
				.postData(data)
				.method("put")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString

			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","PUT /files/external",cluster + " - v9.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}

//Admin--> General V8
def DeleteFolders(data:String) {
 
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v8.0/folders")
				.postData(data)
				.method("DELETE")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","DELETE /folders",cluster + " - v8.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> General V8
def GetFolders(folderName:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v8.0/folders")
				.params("folderName"-> folderName)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","GET /folders",cluster + " - v8.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> General V3
def getFeedbackCategories() {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v3.0/feedback-categories-and-priorities")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","GET /feedback-categories-and-priorities",cluster + " - v3.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> General V7	
def getHiringSources() {

	//Check if accessToken is empty or null
	if(accessToken!= null && !accessToken.isEmpty())
	{
		//Add all necessary headers and Make the http request
		var response = Http( baseURI +"services/v7.0/hiring-sources")
			.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
		//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		construct("Admin","General","GET /hiring-sources",cluster + " - v7.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}

	
//Admin--> General V7
def createHiringSource(data:String) {

         //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v7.0/hiring-sources")
				.postData(data)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","General","POST /hiring-sources",cluster + " - v7.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		}
	}
	

//Admin--> General V7
def getHoursOfOperationProfiles(fields:String,updatedSince:String,skip:String,top:String,orderBy:String) {
    		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI +"services/v7.0/hours-of-operation")
					.params("fields"-> fields,
					"updatedSince"-> updatedSince,
					"skip"-> skip,
					"top"-> top,
					"orderBy"-> orderBy)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","General","GET /hours-of-operation",cluster + " - v7.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}


//Admin--> General V7	
def getHoursOfOperationProfileById(profileId:Int,fields:String,updatedSince:String,skip:String,top:String,orderBy:String) {

       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI +"services/v7.0/hours-of-operation/" + profileId)
					.params("fields"-> fields,
					"updatedSince"-> updatedSince,
					"skip"-> skip,
					"top"-> top,
					"orderBy"-> orderBy)
			.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
			.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","General","GET /hours-of-operation/{profileId}",cluster + " - v7.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}


//Admin--> General V7	
def getLocations(includeAgents:String) {
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI +"services/v7.0/locations")
				.params("includeAgents"-> includeAgents)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","General","GET /locations",cluster + " - v7.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}



//Admin--> General V1
def getMediaTypes() {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v1.0/media-types")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","GET /media-types",cluster + " - v1.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> General V1	
def getMediaTypesById(mediaTypeId:Int) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v1.0/media-types/" + mediaTypeId)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","GET /media-types/{mediaTypeId}",cluster + " - v8.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
	

	
//Admin--> General V8
def getMessageTemplates(templateTypeId:String) {

       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI + "services/v8.0/message-templates")
				.params("templateTypeId"-> templateTypeId)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","General","GET /message-templates",cluster + " - v8.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}


//Admin--> General V8
def createMessageTemplate(data:String) {

        //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v8.0/message-templates")
				.postData(data)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","General","POST /message-templates",cluster + " - v8.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		}
	}


//Admin--> General V8
def getMessageTemplatesById(templateId:String,templateTypeId:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v8.0/message-templates/" + templateId)
				.params("templateTypeId"-> templateTypeId)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","GET /message-templates/{templateId}",cluster + " - v8.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> General V8
def updateMessageTemplate(templateId:String,data:String){
	
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v8.0/message-templates/" + templateId)
				.postData(data)
				.method("put")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString

			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","PUT /message-templates/{templateId}",cluster + " - v8.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}


//Admin--> General V9
def getPermissions(profileId:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v9.0/permissions")
					.params("profileId"-> profileId)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","GET /permissions",cluster + " - v9.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> General V9
def getPermissionsById(agentId:Int,profileId:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v9.0/permissions/" + agentId)
				.params("profileId"-> profileId)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","GET /permissions/{agentId}",cluster + " - v9.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> General V7
def getPhoneCodes() {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v7.0/phone-codes")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","GET /phone-codes",cluster + " - v7.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> General V1
def getPointsOfContact() {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v1.0/points-of-contact")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","GET /points-of-contact",cluster + " - v1.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> General V1	
def getPointsOfContactById(pocId:Int) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v1.0/points-of-contact/" + pocId)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","GET /points-of-contact/{pointOfContactId}",cluster + " - v1.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> General V12	
def getSecurityProfiles(securityProfileStatusName:String,isInternal:String,searchString:String,fields:String,skip:String,top:String,orderBy:String) {
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI +"services/v12.0/security-profiles")
					.params("securityProfileStatusName"-> securityProfileStatusName,
					"isInternal"-> isInternal,
					"searchString"-> searchString,
					"fields"-> fields,
					"skip"-> skip,
					"top"-> top,
					"orderBy"-> orderBy)
			.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
			.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","General","GET /security-profiles",cluster + " - v12.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}
	
	
//Admin--> General V12	
def getSecurityProfileById(profileId:Int,securityProfileStatusName:String,isInternal:String,searchString:String,fields:String,skip:String,
top:String,orderBy:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v12.0/security-profiles/" + profileId)
				.params("securityProfileStatusName"-> securityProfileStatusName,
					"isInternal"-> isInternal,
					"searchString"-> searchString,
					"fields"-> fields,
					"skip"-> skip,
					"top"-> top,
					"orderBy"-> orderBy)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","GET /security-profiles/{profileId}",cluster + " - v12.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}	
	
	
//Admin--> General V7		
def getScripts(mediaTypeId:String,isActive:String,searchString:String,fields:String,skip:String,top:String,orderBy:String) {
      
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI +"services/v7.0/scripts" )
					.params("mediaTypeId"-> mediaTypeId,
					"isActive"-> isActive,
					"searchString"-> searchString,
					"fields"-> fields,
					"skip"-> skip,
					"top"-> top,
					"orderBy"-> orderBy)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","General","GET /scripts",cluster + " - v7.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}


//Admin--> General V4	
def startScript(scriptId:String,data:String) {
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v4.0/scripts/" + scriptId + "/start")
				.postData(data)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","General","POST /scripts/{scriptId}/start",cluster + " - v4.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		}
	}


//Admin--> General V2		
def getServerTime() {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v2.0/server-time")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","GET /server-time",cluster + " - v2.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}



//Admin--> General V7	
def getTags(searchString:String,isActive:String) {

         //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI +"services/v7.0/tags")
					.params("searchString"-> searchString,
					"isActive"-> isActive)
			.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
			.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","General","GET /tags",cluster + " - v7.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}


//Admin--> General V7
def createTag(data:String) {

         //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v7.0/tags")
				.postData(data)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","General","POST /tags",cluster + " - v7.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		}
	}
	

//Admin--> General V7
def getTagById(tagId:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v7.0/tags/" + tagId)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","GET /tags/{tagId}",cluster + " - v7.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}

//Admin--> General V7
def updateTag(tagId:Int,data:String){
	
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v7.0/tags/" + tagId)
					.postData(data)
					.method("put")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString

			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","PUT /tags/{tagId}",cluster + " - v7.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}


//Admin--> General V5
def getTimeZones() {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v5.0/timezones")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","GET /timezones",cluster + " - v5.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
   }


//Admin--> General V13	
def getUnavailableCodes() {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v13.0/unavailable-codes")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","GET /unavailable-codes",cluster + " - v13.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
	

//Admin--> Skills V7
def getCampaigns(isActive:String,fields:String,searchString:String,skip:String,top:String,orderby:String) {
    
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI + "services/v7.0/campaigns" )
					.params("isActive"-> isActive,
					"fields"-> fields,
					"searchString"-> searchString,
					"skip"-> skip,
					"top"-> top,
					"orderby"-> orderby)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","Skills","GET /campaigns",cluster + " - v7.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}


//Admin--> Skills V7
def getCampaignById(campaignId:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v7.0/campaigns/" + campaignId)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Skills","GET /campaigns/{campaignId}",cluster + " - v7.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> Skills V10
def PostcreateDispositions(data:String) {
	 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v10.0/dispositions")
					.postData(data)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
					.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Skills","POST /dispositions",cluster + " - v10.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		}
	}
	
	
//Admin--> Skills V10	
def GetDispostionByID(dispositionId:String, fields:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v10.0/dispositions/" + dispositionId)
				.params("fields"-> fields)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Skills","GET /dispositions/{dispositionId}",cluster + " - v10.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> Skills V10		
def PutUpdateDispostionbydispositionId(dispositionId:String,data:String){

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v10.0/dispositions/" + dispositionId)
					.postData(data)
					.method("put")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString

			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Skills","PUT /dispositions/{dispositionId}",cluster + " - v10.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}


//Admin--> Skills V10	
def GetDispostionByClassification(fields:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v10.0/dispositions/classifications")
				.params("fields"-> fields)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Skills","GET /dispositions/classifications",cluster + " - v10.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> Skills V13	
def getSkills(updatedSince:String,mediaTypeId:String,outboundStrategy:String,isActive:String,searchString:String,fields:String,skip:String,
top:String,orderBy:String) {
      
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI +"services/v13.0/skills")
					.params("updatedSince"-> updatedSince,
					"mediaTypeId"-> mediaTypeId,
					"outboundStrategy" -> outboundStrategy,
					"isActive"-> isActive,
					"searchString"-> searchString,
					"fields"-> fields,
					"skip"-> skip,
					"top"-> top,
					"orderBy"-> orderBy)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","Skills","GET /skills",cluster + " - v13.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}


//Admin--> Skills V13	
def PostCreateSkill(data:String) {
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v13.0/skills")
			.postData(data)
			.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
			.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Skills","POST /skills",cluster + " - v13.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		 }
	}


//Admin--> Skills V13
def getSkillById(skillId:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v13.0/skills/"+ skillId)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Skills","GET /skills/{skillId}",cluster + " - v13.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> Skills V13
def PutSkillbySkillID(skillId:String,data:String) {
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v13.0/skills/" + skillId)
				.postData(data)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Skills","PUT /skills/{skillId}",cluster + " - v13.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		 }
	}
	

//Admin--> Skills V13
def getThankYouForSkillId(SkillId:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v13.0/skills/"+ SkillId +"/thank-you-page")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Skills","GET /skills/{skillId}",cluster + " - v13.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> Skills V6
def startPersonalConnectionSkill(skillId:String) {
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v6.0/skills/" + skillId + "/start")
					.postData("")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
					.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Skills","POST /skills/{skillId}/start",cluster + " - v6.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		}
	}


//Admin--> Skills V6
def stopPersonalConnectionSkill(skillId:String,data:String) {

       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v6.0/skills/" + skillId + "/stop")
			.postData(data)
			.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
			.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Skills","POST /skills/{skillId}/stop",cluster + " - v6.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		}
	}	


	
//Admin--> Skills V10
def getAgentsAssignedToSkills(updatedSince:String,fields:String) {
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v10.0/skills/agents")
					.params("updatedSince"-> updatedSince,
					"fields"-> fields)
			.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
			.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","Skills","GET /skills/agents",cluster + " - v10.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}


//Admin--> Skills V7
def deleteAgentsFromSkill(skillId:String,data:String) {
  
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v7.0/skills/" + skillId + "/agents")
				.postData(data)
				.method("DELETE")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Skills","DELETE /skills/{skillId}/agents",cluster + " - v7.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


	
//Admin--> Skills V7
def getAgentContactHistory(skillId:String,updatedSince:String,searchString:String,fields:String,skip:String,top:String,orderBy:String) {
       		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI +"services/v7.0/skills/" + skillId + "/agents")
					.params("updatedSince"-> updatedSince,
					"searchString"-> searchString,
					"fields"-> fields,
					"skip"-> skip,
					"top"-> top,
					"orderBy"-> orderBy)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","Skills","GET /skills/{skillId}/agents",cluster + " - v7.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}

//Admin--> Skills V7
def assignAgentsToSkill(skillId:String,data:String) {
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v7.0/skills/" + skillId + "/agents")
				.postData(data)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Skills","POST /skills/{skillId}/agents",cluster + " - v7.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		}
	}


//Admin--> Skills V7
def modifyAgentsForSkill(skillId:String,data:String){
	
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v7.0/skills/" + skillId + "/agents")
					.postData(data)
					.method("put")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString

			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Skills","PUT /skills/{skillId}/agents ",cluster + " - v7.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}



//Admin--> Skills V7
def getAgentsNotAssignedToSkill(skillId:String,searchString:String,fields:String,skip:String,top:String,orderBy:String) {
       		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI +"services/v7.0/skills/" + skillId + "/agents/unassigned")
					.params("searchString"-> searchString,
					"fields"-> fields,
					"skip"-> skip,
					"top"-> top,
					"orderBy"-> orderBy)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","Skills","GET /skills/{skillId}/agents/unassigned",cluster + " - v7.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}


//Admin--> Skills V13
def getSkillsData(updatedSince:String,mediaTypeId:String,outboundStrategy:String,isActive:String,searchString:String,fields:String,skip:String,top:String,
orderBy:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v13.0/skills/call-data")
				.params("updatedSince"-> updatedSince,
					"mediaTypeId"-> mediaTypeId,
					"outboundStrategy" -> outboundStrategy,
					"isActive"-> isActive,
					"searchString"-> searchString,
					"fields"-> fields,
					"skip"-> skip,
					"top"-> top,
					"orderBy"-> orderBy)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Skills","GET /skills/call-data",cluster + " - v13.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> Skills V3 	
def getCallDataBySkill(skillId:String,startDate:String,endDate:String) {

         //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI +"services/v3.0/skills/" + skillId + "/call-data")
					.params("startDate"-> startDate,
					"endDate"-> endDate)
			.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
			.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","Skills","GET /skills/{skillId}/call-data",cluster + " - v3.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}
	

//Admin--> Skills V9		
def GetDispostionBySkillid(skillId:String,searchString:String,fields:String,skip:String,top:String,orderBy:String ) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v9.0/skills/" + skillId + "/dispositions")
				.params("searchString"-> searchString,
					"fields"-> fields,
					"skip"-> skip,
					"top"-> top,
					"orderBy"-> orderBy)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Skills","GET /skills/{skillId}/dispositions",cluster + " - v9.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}



//Admin--> Skills V9 	
def GetDispostionBySkillidUnAssigned(skillId:String,searchString:String,fields:String,skip:String,top:String,orderBy:String ) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v9.0/skills/" + skillId + "/dispositions/unassigned")
					.params("searchString"-> searchString,
					"fields"-> fields,
					"skip"-> skip,
					"top"-> top,
					"orderBy"-> orderBy)
     				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Skills","GET /skills/{skillId}/dispositions/unassigned",cluster + " - v9.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}

//Admin--> Skills V7 
def removeTagsFromSkill(skillId:String,data:String) {
 
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v7.0/skills/" + skillId + "/tags")
				.postData(data)
				.method("DELETE")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Skills","DELETE /skills/{skillId}/tags",cluster + " - v7.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> Skills V7
def getSkillTags(skillId:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v7.0/skills/" + skillId + "/tags")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Skills","GET /skills/{skillId}/tags",cluster + " - v7.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}

	
//Admin--> Skills V7
def assignTagsToSkill(skillId:String,data:String) {

       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v7.0/skills/" + skillId + "/tags")
					.postData(data)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
					.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Skills","POST /skills/{skillId}/tags",cluster + " - v7.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		}
	}


//Admin--> Skills V13
def GetSkillParameterGeneralSetting(skillId:String,fields:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI + "/services/v13.0/skills/"+ skillId + "/parameters/general-settings")
			.params("fields"-> fields)
			.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Skills","GET /skills/{skillId}/parameters/general-settings",cluster + " - v13.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}

//Admin--> Skills V13
def PutSkillParameterGeneralSetting(skillId:String,data:String) {
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v13.0/skills/" + skillId + "/parameters/general-settings")
				.postData(data)
				.method("put")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Skills","PUT /skills/{skillId}/parameters/general-settings",cluster + " - v13.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		 }
	}


	
//Admin--> Skills V13	
def GetCPAManagement(skillId:String,fields:String ) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v13.0/skills/"+ skillId + "/parameters/cpa-management")
				.params("fields"-> fields)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Skills","GET /skills/{skillId}/parameters/cpa-management",cluster + " - v13.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> Skills V13	
def PutCPAManagementbySkillId(skillId:Int,data:String) {

        //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v13.0/skills/" + skillId + "/parameters/cpa-management")
				.postData(data)
				.method("put")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Skills","PUT /skills/{skillId}/parameters/cpa-management",cluster + " - v13.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		 }
	}
	

//Admin--> Skills V13
def Getxssettings(skillId:String,fields:String) {
 
	
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI + "/services/v13.0/skills/"+ skillId+ "/parameters/XS-settings")
				.params("fields"-> fields)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Skills","GET /skills/{skillId}/parameters/xs-settings",cluster + " - v13.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> Skills V13 
def Putxssettings(skillId:String,data:String) {
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v13.0/skills/" + skillId + "/parameters/XS-settings")
				.postData(data) 
				.method("put")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Skills","PUT /skills/{skillId}/parameters/xs-settings",cluster + " - v13.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		 }
	}


//Admin--> Skills V13
def Getdeliverypreference(skillId:Int,fields:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v13.0/skills/"+ skillId + "/parameters/delivery-preferences")
			.params("fields"-> fields)
			.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Skills","GET /skills/{skillId}/parameters/delivery-preferences",cluster + " - v13.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> Skills V13
def Putskilldeliverypreference(skillId:String,data:String) {

         //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v13.0/skills/" + skillId + "/parameters/delivery-preferences")
				.postData(data)
				.method("put")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Skills","PUT /skills/{skillId}/parameters/delivery-preferences",cluster + " - v13.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		 }
	}
	


//Admin--> Skills V13	
def Getskillretrysetting(skillId:String,fields:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v13.0/skills/"+ skillId + "/parameters/retry-settings")
				.params("fields"-> fields)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Skills","GET /skills/{skillId}/parameters/retry-settings",cluster + " - v13.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> Skills V13	
def Putskillretrysetting(skillId:String,data:String) {

        //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v13.0/skills/" + skillId + "/parameters/retry-settings")
				.postData(data)
				.method("put")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Skills","PUT /skills/{skillId}/parameters/retry-settings",cluster + " - v13.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		 }
	}


//Admin--> Skills V13	
def Getschedulesettings(skillId:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v13.0/skills/"+ skillId + "/parameters/schedule-settings")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Skills","GET /skills/{skillId}/parameters/schedule-settings",cluster + " - v13.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> Skills V13	
def Putschedulesettings(skillId:String,data:String) {
	 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v13.0/skills/" + skillId + "/parameters/schedule-settings")
				.postData(data) 
				.method("put")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Skills","PUT /skills/{skillId}/parameters/schedule-settings",cluster + " - v13.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		 }
	}



//Admin--> Contacts V2
def getChatTranscript(contactId:Int) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI + "services/v2.0/contacts/" + contactId + "/chat-transcript" )
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Contacts","GET /contacts/{contactId}/chat-transcript",cluster + " - v2.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}

//Admin--> contacts V7
def getEmailTranscript(contactId:Int,includeAttachments:String) {

     
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI +"services/v7.0/contacts/" + contactId + "/email-transcript")
					.params("includeAttachments"-> includeAttachments)
			.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
			.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","Contacts","GET /contacts/{contactId}/email-transcript",cluster + " - v7.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}
	

//Admin--> contacts V8
def GetcontactbyContactID(contactId:Int, fields:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v8.0/contacts/" + contactId + "/files")
				.params("fields"-> fields)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Contacts","GET /contacts/{contactId}/files",cluster + " - v8.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}	
	
	
//Admin--> contacts V1	
def endContact(contactId:String) {

          //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v1.0/contacts/" + contactId + "/end")
					.postData("")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
					.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Contacts","POST /contacts/{contactId}/end",cluster + " - v1.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		}
	}
	

//Admin--> contacts V1
def monitorContact(contactId:Int,data:String) {

           //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v1.0/contacts/" + contactId + "/monitor")
				.postData(data)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Contacts","POST /contacts/{contactId}/monitor",cluster + " - v1.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		}
	}


//Admin--> contacts V1
def recordContact(contactId:String) {

         //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v1.0/contacts/" + contactId + "/record")
					.postData("")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
					.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Contacts","POST /contacts/{contactId}/record",cluster + " - v1.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		}
	}


//Admin--> contacts V7
def assignTagsToContact(contactId:String,data:String) {

         //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v7.0/contacts/" + contactId + "/tags")
				.postData(data)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Contacts","POST /contacts/{contactId}/tags",cluster + " - v7.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		}
	}
	
//Admin--> contacts V1
def getContactStateDescriptions() {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v1.0/contact-state-descriptions")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Contacts","GET /contact-state-descriptions",cluster + " - v1.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> contacts V1
def getContactStateDescriptionById(contactStateId:Int) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v1.0/contact-state-descriptions/" + contactStateId)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Contacts","GET /contact-state-descriptions/{contactStateId}",cluster + " - v1.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}


//Admin--> contacts V3
def signalContact(contactId:String,data:String) {
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v3.0/interactions/" + contactId + "/signal")
			.postData(data)
			.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
			.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Contacts","POST /interactions/{contactId}/signal",cluster + " - v3.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		}
	}


//Admin--> Lists V5
def getDncGroups(fields:String,updatedSince:String) {
    		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI +"services/v5.0/dnc-groups")
					.params("fields"-> fields,
					"updatedSince"-> updatedSince)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","Lists","GET /dnc-groups ",cluster + " - v5.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}


//Admin--> Lists V6
def createDncGroup(data:String) {
       
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI +"services/v6.0/dnc-groups")
				.postData(data)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","Lists","POST /dnc-groups",cluster + " - v6.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}


//Admin--> Lists V5 
def getDncGroupById(groupId:String,fields:String) {

         //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI +"services/v5.0/dnc-groups/" + groupId)
					.params("fields"-> fields)
			.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
			.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","Lists","GET /dnc-groups/{groupId}",cluster + " - v5.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}	


//Admin--> Lists V6 
def updateDncGroup(dncGroupId:String,data:String){
	
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v6.0/dnc-groups/" + dncGroupId)
					.postData(data)
					.method("put")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString

			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Lists","PUT /dnc-groups/{groupId}",cluster + " - v6.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	

//Admin--> Lists V5  	
def getContributingSkillsForDncGroup(groupId:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v5.0/dnc-groups/" + groupId + "/contributing-skills")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Lists","GET /dnc-groups/{groupId}/contributing-skills",cluster + " - v5.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}

	
//Admin--> Lists V6	
def deleteContributingSkill(groupId:String,skillId:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v6.0/dnc-groups/" + groupId + "/contributing-skills/" + skillId)
				.method("DELETE")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString

			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Lists","DELETE /dnc-groups/{groupId}/contributing-skills/{skillId}",cluster + " - v6.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
		

//Admin--> Lists V6		
def assignContributingSkill(groupId:String,skillId:String) {

          //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v6.0/dnc-groups/" + groupId + "/contributing-skills/" + skillId)
					.postData("")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
					.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","ListsLists","POST /dnc-groups/{groupId}/contributing-skills/{skillId}",cluster + " - v6.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		}
	}	
	

//Admin--> Lists V5	
def deleteDncGroupRecords(groupId:String,data:String) {
  
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v5.0/dnc-groups/" + groupId + "/records")
				.postData(data)
				.method("DELETE")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Lists","DELETE /dnc-groups/{groupId}/records",cluster + " - v5.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}	


//Admin--> Lists V5 
def getDncGroupRecords(groupId:String,fields:String,skip:String,top:String,orderBy:String) {

       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI +"services/v5.0/dnc-groups/" + groupId + "/records")
					.params("fields"-> fields,
					"skip"-> skip,
					"top"-> top,
					"orderBy"-> orderBy)
			.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
			.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","Lists","GET /dnc-groups/{groupId}/records",cluster + " - v5.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}


//Admin--> Lists V5 
def postDncGroupRecords(groupId:String,data:String) {
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v5.0/dnc-groups/" + groupId + "/records")
				.postData(data)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Lists","POST /dnc-groups/{groupId}/records",cluster + " - v5.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		}
	}


//Admin--> Lists V5	
def getScrubbedSkillsForDncGroup(groupId:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI + "services/v5.0/dnc-groups/" + groupId + "/scrubbed-skills")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Lists","GET /dnc-groups/{groupId}/scrubbed-skills",cluster + " - v5.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
	

//Admin--> Lists V6	
def deleteScrubbedSkill(groupId:String,skillId:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v6.0/dnc-groups/" + groupId + "/scrubbed-skills/" + skillId)
				.method("DELETE")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString

			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Lists","DELETE /dnc-groups/{groupId}/scrubbed-skills/{skillId}",cluster + " - v6.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}	
	

//Admin--> Lists V6	
def assignScrubbedSkill(groupId:String,skillId:String) {

         //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+"services/v6.0/dnc-groups/" + groupId + "/scrubbed-skills/" + skillId)
					.postData("")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
					.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Lists","POST /dnc-groups/{groupId}/scrubbed-skills/{skillId} ",cluster + " - v6.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		}
	}
	

//Admin--> Lists V5	
def postDncGroupsSearchPhoneNumber(data:String) {
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v5.0/dnc-groups/search")
			.postData(data)
			.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
			.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Lists","POST /dnc-groups/search",cluster + " - v5.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		}
	}
	

//Admin--> Lists V6
def getCallingLists() {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v6.0/lists/call-lists")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Lists","GET /lists/call-lists",cluster + " - v6.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}	


//Admin--> Lists V8
def createCallingListMapping(data:String) {

        //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v8.0/lists/call-lists")
				.postData(data)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Lists","POST /lists/call-lists",cluster + " - v8.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		}
	}


//Admin--> Lists V6
def deleteCallingList(listId:String,data:String) {
 
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v6.0/lists/call-lists/" + listId)
				.postData(data)
				.method("DELETE")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Lists","DELETE /lists/call-lists/{listId}",cluster + " - v6.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}	

//Admin--> Lists V7 	
def getCallingListById(listId:Int,updatedSince:String,finalized:String,includeRecords:String,fields:String,skip:String,top:String,
orderBy:String) {
   
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI +"services/v7.0/lists/call-lists/" + listId)
					.params("updatedSince"-> updatedSince,
					"finalized"-> finalized,
					"includeRecords"-> includeRecords,
					"fields"-> fields,
					"skip"-> skip,
					"top"-> top,
					"orderBy"-> orderBy)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","Lists","GET /lists/call-lists/{listId}",cluster + " - v7.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}
	

	
//Admin--> Lists V7
def getCallingListAttempts(listId:String,updatedSince:String,finalized:String,fields:String,skip:String,top:String,orderBy:String) {
    		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request
		    var response = Http( baseURI +"services/v7.0/lists/call-lists/" + listId + "/attempts")
					.params("updatedSince"-> updatedSince,
					"finalized"-> finalized,
					"fields"-> fields,
					"skip"-> skip,
					"top"-> top,
					"orderBy"-> orderBy)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	construct("Admin","Lists","GET /lists/call-lists/{listId}/attempts",cluster + " - v7.0" ,response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}
	

//Admin--> Lists V10	
def  PostCallListbyListID(listId:String,data:String) {
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v10.0/lists/call-lists/" + listId + "/upload")
					.postData(data)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
					.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Lists","POST /lists/call-lists/{listId}/upload",cluster + " - v10.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		}
	}
	
	
//Admin--> Lists V10	
def GetListCalllist(fields:String,top:String,skip:String,orderBy:String,startDate:String,endDate:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v10.0/lists/call-lists/jobs")
			 .params("fields"-> fields,
			 "top"-> top,
			 "skip"-> skip,
			 "orderBy"-> orderBy,
			 "startDate"-> startDate,
			 "endDate"-> endDate)
			.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Lists","GET /lists/call-lists/jobs",cluster + " - v10.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
		
	
//Admin--> Lists V10	
def DeleteCallListByJobID(jobId:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v10.0/lists/call-lists/jobs/" +jobId)
				.method("DELETE")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Lists","DELETE /lists/call-lists/jobs/{jobId}",cluster + " - v10.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
		
	
//Admin--> Lists V10	
def GetCallingListbyJobID(jobId:Int,fields:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v10.0/lists/call-lists/jobs/" + jobId)
				.params("fields"-> fields)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Lists","GET /lists/call-lists/jobs/{jobId}",cluster + " - v10.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
	
	
//Admin--> Groups V9
def GetGroups(top:String,skip:String,orderby:String,searchString:String,isActive:String,fields:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v9.0/groups")
				.params("top"->top,
				"skip"->skip,
				"orderby"-> orderby,
				"searchString"-> searchString,
				"isActive"-> isActive,
				"fields"-> fields)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Groups","GET /groups",cluster + " - v9.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}	


//Admin--> Groups V9	
def PostGroups(data:String) {

         //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v9.0/groups")
					.postData(data)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
					.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Groups","POST /groups",cluster + " - v9.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		}
	}	
	
	
//Admin--> Groups V9   
def GetGroupsByGroupID(groupId:String, fields:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v9.0/groups/" + groupId )
				 .params("fields"-> fields)
				 .headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Groups","GET /groups/{groupId}",cluster + " - v9.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}	

	
//Admin--> Groups V9 	
def PutGroupsByGroupID(groupId:String,data:String){
	
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v9.0/groups/" + groupId)
				.postData(data)
				.method("put")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString

			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Groups","PUT /groups/{groupId}",cluster + " - v9.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	

//Admin--> Groups V9 		
def DeleteGroupsByAgentGroupID(groupId:String, data:String) {
 
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v9.0/groups/"+ groupId +"/agents" )
				.postData(data)
				.method("DELETE")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Groups","DELETE /groups/{groupId}/agents",cluster + " - v9.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}	
	

//Admin--> Groups V10	
def GetGroupsByAgentGroupID(groupId:Int,top:String,skip:String,orderBy:String,searchString:String,assigned:String,fields:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v10.0/groups/" + groupId + "/agents")
				.params("top"-> top,
				"skip"-> skip,
				"orderBy"-> orderBy,
				"searchString"-> searchString,
				"assigned"-> assigned,
				"fields"-> fields)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Groups","GET /groups/{groupId}/agents",cluster + " - v10.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}	
	


//Admin--> Groups V9 
def PostGroupsByAgentGroupID(groupId:String,data:String) {

         //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+  "/services/v9.0/groups/"+ groupId +"/agents")
				.postData(data)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Groups","POST /groups/{groupId}/agents",cluster + " - v9.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		}
	}
	
	
	
//agent APIs

def postAgentPhoneDial(sessionID:String) {
           
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+"/services/v2.0/agent-sessions/" + sessionID + "/agent-phone/dial")
					.postData("")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Agent","AgentPhone","POST /agent-sessions/{sessionId}/agent-phone/dial",cluster + " - v2.0",response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
	

def mute(sessionID:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v2.0/agent-sessions/"+ sessionID +"/agent-phone/mute")
							.postData("")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","AgentPhone","POST /agent-sessions/{sessionId}/agent-phone/mute",cluster + " - v2.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def unmute(sessionID:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v2.0/agent-sessions/"+ sessionID +"/agent-phone/unmute")
							.postData("")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","AgentPhone","POST /agent-sessions/{sessionId}/agent-phone/unmute",cluster + " - v2.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	
def endCall(sessionID:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v2.0/agent-sessions/"+ sessionID +"/agent-phone/end")
							.postData("")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","AgentPhone","POST /agent-sessions/{sessionId}/agent-phone/end",cluster + " - v2.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	
def addChat(sessionID:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v2.0/agent-sessions/"+ sessionID +"/interactions/add-chat")
							.postData("")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","ChatRequests","POST /agent-sessions/{sessionId}/interactions/add-chat",cluster + " - v2.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	
def acceptContact(sessionID:String,contactID:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v2.0/agent-sessions/"+ sessionID +"/interactions/"+ contactID +"/accept")
							.postData("")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","ChatRequests ","POST /agent-sessions/{sessionId}/interactions/{contactId}/accept",cluster + " - v2.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	
def rejectContact(sessionID:String,contactID:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v2.0/agent-sessions/"+ sessionID +"/interactions/"+ contactID +"/reject")
							.postData("")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","ChatRequests ","POST /agent-sessions/{sessionId}/interactions/{contactId}/reject",cluster + " - v2.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	
def activateChat(sessionID:String,contactID:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v2.0/agent-sessions/"+ sessionID +"/interactions/"+ contactID +"/activate-chat")
							.postData("")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","ChatRequests ","POST /agent-sessions/{sessionId}/interactions/{contactId}/activate-chat",cluster + " - v2.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def sendChatText(sessionID:String,contactID:String,chatText:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v4.0/agent-sessions/"+ sessionID +"/interactions/"+ contactID +"/send-chat-text")
							.postData(raw"""{"chatText":$chatText}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","ChatRequests ","POST /agent-sessions/{sessionId}/interactions/{contactId}/send-chat-text",cluster + " - v4.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	
def chatToAgent(sessionID:String,contactID:String,targetAgentId:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v4.0/agent-sessions/"+ sessionID +"/interactions/"+ contactID +"/transfer-chat-to-agent")
							.postData(raw"""{"targetAgentId":$targetAgentId}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","ChatRequests ","POST /agent-sessions/{sessionId}/interactions/{contactId}/transfer-chat-to-agent",cluster + " - v4.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def chatToSkill(sessionID:String,contactID:String,targetSkillId:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v4.0/agent-sessions/"+ sessionID +"/interactions/"+ contactID +"/transfer-chat-to-skill")
							.postData(raw"""{"targetSkillId":$targetSkillId}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","ChatRequests ","POST /agent-sessions/{sessionId}/interactions/{contactId}/transfer-chat-to-skill",cluster + " - v4.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def agentTyping(sessionID:String,contactID:String,isTyping:String,isTextEntered:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v8.0/agent-sessions/"+ sessionID +"/interactions/"+ contactID +"/typing")
							.postData(raw"""{"isTyping":$isTyping,"isTextEntered":$isTextEntered}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","ChatRequests ","POST /agent-sessions/{sessionId}/interactions/{contactId}/typing",cluster + " - v8.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	
def endVoiceMail(sessionID:String,contactID:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v2.0/agent-sessions/"+ sessionID +"/interactions/"+ contactID +"/end")
							.postData("")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","agent","POST /agent-sessions/{sessionId}/interactions/{contactId}/end",cluster + " - v2.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def playVoiceMail(sessionID:String,contactID:String,playTimestamp:Boolean,position:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v4.0/agent-sessions/"+ sessionID +"/interactions/"+ contactID +"/play-voicemail")
							.postData(raw"""{"playTimestamp":$playTimestamp,"position":$position}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","Voicemails","POST /agent-sessions/{sessionId}/interactions/{contactId}/play-voicemail",cluster + " - v4.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def pauseVoiceMail(sessionID:String,contactID:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v4.0/agent-sessions/"+ sessionID +"/interactions/"+ contactID +"/pause-voicemail")
							.postData("")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","Voicemails","POST /agent-sessions/{sessionId}/interactions/{contactId}/pause-voicemail",cluster + " - v4.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def transferVoiceMail(sessionID:String,contactID:String,targetAgentId:Int) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v4.0/agent-sessions/"+ sessionID +"/interactions/"+ contactID +"/transfer-voicemail-to-agent")
							.postData(raw"""{"targetAgentId":$targetAgentId}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","Voicemails","POST /agent-sessions/{sessionId}/interactions/{contactId}/transfer-voicemail-to-agent",cluster + " - v4.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def voiceMailToSkill(sessionID:String,contactID:String,targetSkillId:Int) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v4.0/agent-sessions/"+ sessionID +"/interactions/"+ contactID +"/transfer-voicemail-to-skill")
							.postData(raw"""{"targetSkillId":$targetSkillId}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","Voicemails","POST /agent-sessions/{sessionId}/interactions/{contactId}/transfer-voicemail-to-skill",cluster + " - v4.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def addEmail(sessionID:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v8.0/agent-sessions/"+ sessionID +"/interactions/add-email")
							.postData("")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","Emails","POST /agent-sessions/{sessionId}/interactions/add-email",cluster + " - v8.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	
def emailOutbound(sessionID:String,toAddress:String,parentContactId:Int,skillId:Int) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v10.0/agent-sessions/"+ sessionID +"/interactions/email-outbound")
							.postData(raw"""{"toAddress":$toAddress,"parentContactId":$parentContactId,"skillId": $skillId}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","Emails","POST /agent-sessions/{sessionId}/interactions/email-outbound",cluster + " - v10.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def emailForward(sessionID:String,contactID:String,skillId:Int,toAddress:String,fromAddress:String,
ccAddress:String,bccAddress:String,subject:String,bodyHtml:String,attachments:String,attachmentNames:String,originalAttachmentNames:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v7.0/agent-sessions/"+ sessionID +"/interactions/"+ contactID +"/email-forward")
							.postData(raw"""{"skillId":$skillId,
                                          "toAddress":$toAddress,
                                          "fromAddress":$fromAddress,
                                          "ccAddress":$ccAddress,
                                          "bccAddress":$bccAddress,
                                          "subject":$subject,
                                          "bodyHtml":$bodyHtml,
                                          "attachments":$attachments,
                                          "attachmentNames":$attachmentNames,
                                          "originalAttachmentNames":$originalAttachmentNames}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","Emails","POST /agent-sessions/{sessionId}/interactions/{contactId}/email-forward",cluster + " - v7.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	
def emailReply(sessionID:String,contactID:String,skillId:Int,toAddress:String,fromAddress:String,
ccAddress:String,bccAddress:String,subject:String,bodyHtml:String,attachments:String,attachmentNames:String,originalAttachmentNames:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v4.0/agent-sessions/"+ sessionID +"/interactions/"+ contactID +"/email-reply")
							.postData(raw"""{"skillId":$skillId,
                                          "toAddress":$toAddress,
                                          "fromAddress":$fromAddress,
                                          "ccAddress":$ccAddress,
                                          "bccAddress":$bccAddress,
                                          "subject":$subject,
                                          "bodyHtml":$bodyHtml,
                                          "attachments":$attachments,
                                          "attachmentNames":$attachmentNames,
                                          "originalAttachmentNames":$originalAttachmentNames}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","Emails","POST /agent-sessions/{sessionId}/interactions/{contactId}/email-reply",cluster + " - v4.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	
def emailSend(sessionID:String,contactID:String,skillId:Int,toAddress:String,fromAddress:String,ccAddress:String,bccAddress:String,subject:String,bodyHtml:String,attachments:String,attachmentNames:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v4.0/agent-sessions/"+ sessionID +"/interactions/"+ contactID +"/email-send")
							.postData(raw"""{"skillId":$skillId,
                                          "toAddress":$toAddress,
                                          "fromAddress":$fromAddress,
                                          "ccAddress":$ccAddress,
                                          "bccAddress":$bccAddress,
                                          "subject":$subject,
                                          "bodyHtml":$bodyHtml,
                                          "attachments":$attachments,
                                          "attachmentNames":$attachmentNames}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","Emails","POST /agent-sessions/{sessionId}/interactions/{contactId}/email-send",cluster + " - v4.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def emailToAgent(sessionID:String,contactID:String,targetAgentId:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v4.0/agent-sessions/"+ sessionID +"/interactions/"+ contactID +"/transfer-email-to-agent")
							.postData(raw"""{"targetAgentId":$targetAgentId}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","Emails","POST /agent-sessions/{sessionId}/interactions/{contactId}/transfer-email-to-agent",cluster + " - v4.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	
def emailToSkill(sessionID:String,contactID:String,targetSkillId:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v4.0/agent-sessions/"+ sessionID +"/interactions/"+ contactID +"/transfer-email-to-skill")
							.postData(raw"""{"targetSkillId":$targetSkillId}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","Emails","POST /agent-sessions/{sessionId}/interactions/{contactId}/transfer-email-to-skill",cluster + " - v4.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def emailPark(sessionID:String,contactID:String,toAddress:String,fromAddress:String,ccAddress:String,bccAddress:String,subject:String,bodyHtml:String,attachments:String,attachmentNames:String,isDraft:String,primaryDispositionId:String,secondaryDispositionId:String,tags:String,notes:String,originalAttachmentNames:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v7.0/agent-sessions/"+ sessionID +"/interactions/"+ contactID +"/email-park")
							.postData(raw"""{"toAddress":$toAddress,
                                          "fromAddress":$fromAddress,
                                          "ccAddress":$ccAddress,
                                          "bccAddress":$bccAddress,
                                          "subject":$subject,
                                          "bodyHtml":$bodyHtml,
                                          "attachments":$attachments,
                                          "attachmentNames":$attachmentNames,
                                          "isDraft":$isDraft,
                                          "primaryDispositionId":$primaryDispositionId,
                                          "secondaryDispositionId":$secondaryDispositionId,
                                          "tags":$tags,
                                          "notes":$notes,
                                          "originalAttachmentNames":$originalAttachmentNames}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","Emails","POST /agent-sessions/{sessionId}/interactions/{contactId}/email-park",cluster + " - v7.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def emailUnpark(sessionID:String,contactID:String,isImmediate:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v7.0/agent-sessions/"+ sessionID +"/interactions/"+ contactID +"/email-unpark")
							.postData(raw"""{"isImmediate":$isImmediate}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","Emails","POST /agent-sessions/{sessionId}/interactions/{contactId}/email-unpark",cluster + " - v7.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def emailPreview(sessionID:String,contactID:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v7.0/agent-sessions/"+ sessionID +"/interactions/"+ contactID +"/email-preview")
							.postData("")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","Emails","POST /agent-sessions/{sessionId}/interactions/{contactId}/email-preview",cluster + " - v7.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	
def emailRestore(sessionID:String,contactID:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v7.0/agent-sessions/"+ sessionID +"/interactions/"+ contactID +"/email-restore")
							.postData("")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","Emails","POST /agent-sessions/{sessionId}/interactions/{contactId}/email-restore",cluster + " - v7.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def dialerLogin(sessionID:String,skillName:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v2.0/agent-sessions/"+ sessionID +"/dialer-login")
							.postData(raw"""{"skillName":$skillName}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","PersonalCon","POST /agent-sessions/{sessionid}/dialer-login",cluster + " - v13.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	
def dialerLogout(sessionID:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v2.0/agent-sessions/"+ sessionID +"/dialer-logout")
							.postData("")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","PersonalCon","POST /agent-sessions/{sessionid}/dialer-logout",cluster + " - v2.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def snooze(sessionID:String,contactID:Int) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v9.0/agent-sessions/"+ sessionID +"/interactions/"+ contactID +"/snooze")
							.postData("")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","PersonalCon","POST /agent-sessions/{sessionId}/interactions/{contactId}/snooze",cluster + " - v9.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def dialAgent(sessionID:String,targetAgentId:Int,parentContactId:Int) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v10.0/agent-sessions/"+ sessionID +"/dial-agent")
							.postData(raw"""{"targetAgentId":$targetAgentId,"parentContactId":$parentContactId}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","PhoneCalls","POST /agent-sessions/{sessionid}/dial-agent",cluster + " - v10.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def dialPhone(sessionID:String,phoneNumber:String,skillId:Int,ParentContactId:Int) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v10.0/agent-sessions/"+ sessionID +"/dial-phone")
							.postData(raw"""{"phoneNumber":$phoneNumber,
                                          "skillId":$skillId,
                                          "parentContactId":$ParentContactId}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","PhoneCalls","POST /agent-sessions/{sessionId}/dial-phone",cluster + " - v10.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def dialSkill(sessionID:String,skillId:Int,parentContactId:Int) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v10.0/agent-sessions/"+ sessionID +"/dial-skill")
							.postData(raw"""{"skillId":$skillId,"parentContactId":$parentContactId}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","PhoneCalls","POST /agent-sessions/{sessionId}/dial-skill",cluster + " - v10.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def sendDtmf(sessionID:String,sequence:String,duration:Int,spacing:Int) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v3.0/agent-sessions/"+ sessionID +"/send-dtmf")
							.postData(raw"""{"sequence":$sequence,"duration":$duration,"spacing":$spacing}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","PhoneCalls","POST /agent-sessions/{sessionId}/send-dtmf",cluster + " - v3.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	
	
def consultAgent(sessionID:String,targetAgentId:Int,parentContactId:Int) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v10.0/agent-sessions/"+ sessionID +"/consult-agent")
							.postData(raw"""{"targetAgentId":$targetAgentId,"parentContactId":$parentContactId}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","PhoneCalls","POST /agent-sessions/{sessionid}/consult-agent",cluster + " - v10.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	
	
def transferCall(sessionID:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v2.0/agent-sessions/"+ sessionID +"/interactions/transfer-calls")
							.postData("")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","PhoneCalls","POST /agent-sessions/{sessionid}/interactions/transfer-calls",cluster + " - v2.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def conferenceCall(sessionID:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v2.0/agent-sessions/"+ sessionID +"/interactions/conference-calls")
							.postData("")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","PhoneCalls","POST /agent-sessions/{sessionid}/interactions/conference-calls",cluster + " - v2.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def acceptConsult(sessionID:String,contactID:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v2.0/agent-sessions/"+ sessionID +"/interactions/" + contactID + "/accept-consult")
							.postData("")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","PhoneCalls","POST /agent-sessions/{sessionid}/interactions/{contactId}/accept-consult",cluster + " - v2.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	
def amdOveride(sessionID:String,contactID:String,overrideType:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v2.0/agent-sessions/"+ sessionID +"/interactions/" + contactID + "/amd-override")
							.postData(raw"""{"overrideType":$overrideType}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","PhoneCalls","POST /agent-sessions/{sessionId}/interactions/{contactId}/amd-override",cluster + " - v2.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def sessionRecord(sessionID:String,contactID:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v2.0/agent-sessions/"+ sessionID +"/interactions/" + contactID + "/record")
							.postData("")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","PhoneCalls","POST /agent-sessions/{sessionid}/interactions/{contactId}/record",cluster + " - v2.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def sessionMask(sessionID:String,contactID:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v2.0/agent-sessions/"+ sessionID +"/interactions/" + contactID + "/mask")
							.postData("")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","PhoneCalls","POST /agent-sessions/{sessionid}/interactions/{contactId}/mask",cluster + " - v2.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def sessionUnmask(sessionID:String,contactID:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v2.0/agent-sessions/"+ sessionID +"/interactions/" + contactID + "/unmask")
							.postData("")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","PhoneCalls","POST /agent-sessions/{sessionid}/interactions/{contactId}/unmask",cluster + " - v2.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def independentDial(sessionID:String,contactID:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v4.0/agent-sessions/"+ sessionID +"/interactions/" + contactID + "/independent-dial")
							.postData("")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","PhoneCalls","POST /agent-sessions/{sessionId}/interactions/{contactId}/independent-dial",cluster + " - v4.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	
def dialOutcome(sessionID:String,contactID:String,outcome:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v4.0/agent-sessions/"+ sessionID +"/interactions/" + contactID + "/independent-dial-outcome")
							.postData("")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","PhoneCalls","POST /agent-sessions/{sessionId}/interactions/{contactId}/independent-dial-outcome",cluster + " - v4.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def holdWorkitem(sessionID:String,contactID:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v2.0/agent-sessions/"+ sessionID +"/interactions/" + contactID + "/hold")
							.postData("")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","WorkItems","POST /agent-sessions/{sessionid}/interactions/{contactId}/hold",cluster + " - v2.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def resumeWorkitem(sessionID:String,contactID:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v2.0/agent-sessions/"+ sessionID +"/interactions/" + contactID + "/resume")
							.postData("")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","WorkItems","POST /agent-sessions/{sessionid}/interactions/{contactId}/resume",cluster + " - v2.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	
def endWorkitem(sessionID:String,contactID:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v2.0/agent-sessions/"+ sessionID +"/interactions/" + contactID + "/end")
							.postData("")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","WorkItems","POST /agent-sessions/{sessionid}/interactions/{contactId}/end",cluster + " - v2.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def acceptWorkitem(sessionID:String,contactID:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v2.0/agent-sessions/"+ sessionID +"/interactions/" + contactID + "/accept")
							.postData("")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","WorkItems","POST /agent-sessions/{sessionid}/interactions/{contactId}/accept",cluster + " - v2.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def rejectWorkitem(sessionID:String,contactID:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v2.0/agent-sessions/"+ sessionID +"/interactions/" + contactID + "/reject")
							.postData("")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","WorkItems","POST /agent-sessions/{sessionid}/interactions/{contactId}/reject",cluster + " - v2.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def dialCallBack(sessionID:String,callbackId:Int) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v4.0/agent-sessions/"+ sessionID +"/interactions/" + callbackId + "/dial")
							.postData("")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","ScheduledCallbacks","POST /agent-sessions/{sessionId}/interactions/{callbackId}/dial",cluster + " - v4.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def rescheduleCallBack(sessionID:String,callbackId:Int,rescheduleDate:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v4.0/agent-sessions/"+ sessionID +"/interactions/" + callbackId + "/reschedule")
							.postData(raw"""{"rescheduleDate":$rescheduleDate}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","ScheduledCallbacks","POST /agent-sessions/{sessionId}/interactions/{callbackId}/reschedule",cluster + " - v4.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def postCancelCallBack(sessionID:String,callbackId:Int,notes:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v10.0/agent-sessions/"+ sessionID +"/interactions/" + callbackId + "/cancel")
							.postData(raw"""{"notes":$notes}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","ScheduledCallbacks","POST /agent-sessions/{sessionid}/interactions/{callbackid}/cancel",cluster + " - v10.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	
def session(stationPhoneNumber:String,inactivityTimeout:Int,inactivityForceLogout:Boolean,asAgentId:Int) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v2.0/agent-sessions/")
							.postData(raw"""{"stationPhoneNumber":$stationPhoneNumber,
                                          "inactivityTimeout":$inactivityTimeout,
                                          "inactivityForceLogout":$inactivityForceLogout,
                                          "asAgentId":$asAgentId}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","ScheduledCallbacks","POST /agent-sessions",cluster + " - v2.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def sessionJoin(asAgentId:Int) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v2.0/agent-sessions/join")
							.postData(raw"""{"asAgentId":$asAgentId}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","Sessions","POST /agent-sessions/join",cluster + " - v2.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def deleteSession(sessionID:String,chatSessionIdInt:Int) {
		//Give the specified url ,accessToken
			var accessToken = "";
			var baseURI = "";
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v2.0/agent-sessions/"+ sessionID)
					.method("DELETE")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Agent","Sessions","DELETE /agent-sessions/{sessionId}",cluster + " - v2.0",response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
	
def nextEvent(sessionID:String) {
		//Give the specified url ,accessToken
			var accessToken = "";
			var baseURI = "";
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v2.0/sessionID/get-next-event")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Agent","Sessions","GET /agent-sessions/{sessionId}/get-next-event",cluster + " - v2.0",response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	

def reSkill(sessionID:String,continueReskill:Boolean) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v2.0/agent-sessions/"+ sessionID +"/continue-reskill")
							.postData(raw"""{"continueReskill":$continueReskill}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","Sessions","POST /agent-sessions/{sessionId}/continue-reskill",cluster + " - v2.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	
def sessionDisposition(sessionID:String,contactID:Int,primaryDispositionId:Int,primaryDispositionNotes:String,primaryCommitmentAmount:Int,primaryCallbackTime:String,primaryCallbackNumber:String,secondaryDispositionId:Int,previewDispositionId:Int) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v9.0/agent-sessions/"+ sessionID +"/interactions/"+ contactID +"/disposition")
							.postData(raw"""{"primaryDispositionId":$primaryDispositionId,
                                          "primaryDispositionNotes":$primaryDispositionNotes,
                                          "primaryCommitmentAmount":$primaryCommitmentAmount,
                                          "primaryCallbackTime":$primaryCallbackTime,
                                          "primaryCallbackNumber":$primaryCallbackNumber,
                                          "secondaryDispositionId":$secondaryDispositionId,
                                          "previewDispositionId":$previewDispositionId}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","Sessions","POST /agent-sessions/{sessionId}/interactions/{contactId}/disposition",cluster + " - v9.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	
	
def sessionState(sessionID:String,state:Boolean) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v2.0/agent-sessions/"+ sessionID +"/state")
							.postData(raw"""{"state":$state}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","Sessions","POST /agent-sessions/{sessionId}/state",cluster + " - v2.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def feedback(sessionID:String,categoryId:Boolean,priority:String,comment:String,customData:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v3.0/agent-sessions/"+ sessionID +"/submit-feedback")
							.postData(raw"""{"categoryId":$categoryId,"priority":$priority,"comment":$comment,"customData":$customData}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","Sessions","POST /agent-sessions/{sessionId}/submit-feedback",cluster + " - v3.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def customData(sessionID:String,contactID:String,indicatorName:String,data:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v4.0/agent-sessions/"+ sessionID +"/interactions/"+ contactID +"/custom-data")
							.postData(raw"""{"indicatorName":$indicatorName,
                                          "data":$data}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","Sessions","POST /agent-sessions/{sessionId}/interactions/{contactId}/custom-data",cluster + " - v4.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def addContact(sessionID:String,chat:Boolean,email:Boolean,workitem:Boolean) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v10.0/agent-sessions/"+ sessionID +"/add-contact")
							.postData(raw"""{"chat":$chat,"email":$email,"workitem":$workitem}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","Sessions","POST /agent-sessions/{sessionId}/add-contact",cluster + " - v10.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def contactActivate(sessionID:String,contactID:Int) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v10.0/agent-sessions/"+ sessionID +"/interactions/"+ contactID +"/activate")
							.postData("")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","Sessions","POST /agent-sessions/{sessionId}/interactions/{contactId}/activate",cluster + " - v10.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def sessionMonitor(sessionID:String,targetAgentId:Int) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v2.0/agent-sessions/"+ sessionID +"/monitor")
							.postData(raw"""{"targetAgentId":$targetAgentId}""")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","Supervisor","POST /agent-sessions/{sessionid}/monitor",cluster + " - v2.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	
def sessionCoach(sessionID:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v2.0/agent-sessions/"+ sessionID +"/coach")
							.postData("")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","Supervisor","POST /agent-sessions/{sessionid}/coach",cluster + " - v2.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	

def sessionBarge(sessionID:String,targetAgentId:Int) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v2.0/agent-sessions/"+ sessionID +"/barge")
							.postData("")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","Supervisor","POST /agent-sessions/{sessionid}/barge",cluster + " - v2.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	
def sessionTakeOver(sessionID:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v2.0/agent-sessions/"+ sessionID +"/take-over")
							.postData("")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","Supervisor","POST /agent-sessions/{sessionid}/take-over",cluster + " - v2.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	
//auth, reporting , chat and real time APISubType

def resetAgentPassword(requestedPassword:String, agentId:Int) {
 
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v11.0/agents/" + {agentId} + "/reset-password")
					.postData(raw"""{"requestedPassword" -> $requestedPassword,
						"forceChangeOnLogon" -> ""}""")
					.method("put")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Authentication","Authenticate","PUT /agents/{agentId}/reset-password Resets an agent's password",cluster + " - v11.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	
	def changeAgentPassword(currentPassword:String, newPassword:String, agentId:Int){
 
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/V11.0/agents/" + {agentId} + "/reset-password")
					.postData(raw"""{"currentPassword" -> $currentPassword,
						"newPassword" -> $newPassword}""")
					.method("put")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Authentication ","Authenticate","PUT /agents/change-password",cluster + " - v11.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	

	
	def queueCallback(phoneNumber:String,callerID:String,callDelaySec:Int,skill:Int,targetAgent:Int,priorityManagement:String,intialPriority:Int,acceleration:Int,maxPriority:Int,sequence:String,zipTone:String,screenPopSrc:String,screenPopUrl:String,timeout:Int) {
 
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+"services/v1.0/queuecallback")
					.postData(raw"""{"phoneNumber" -> $phoneNumber,
						"callerID" -> $callerID,
						"callDelaySec" -> $callDelaySec,
						"skill" -> $skill,
						"targetAgent" -> $targetAgent,
						"priorityManagement" -> $priorityManagement,
						"intialPriority" -> $intialPriority,
						"acceleration" -> $acceleration,
						"maxPriority" -> $maxPriority,
						"sequence" -> $sequence,
						"zipTone" -> $zipTone,
						"screenPopSrc" -> $screenPopSrc,
						"screenPopUrl" -> $screenPopUrl,
						"timeout" -> $timeout,"json" ->""}""")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Patron","Callback","POST /queuecallback Request an immediate callback",cluster + " - v1.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
	
	def createPromiseKeeper(firstName:String,lastName:String,phoneNumber:String,skill:Int,targetAgent:Int,promiseDate:String,promiseTime:String,notes:String,timeZone:String) {
 
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+"services/v1.0/promise")
					.postData(raw"""{"firstName" ->$firstName,
						"lastName" -> $lastName,
						"phoneNumber" -> $phoneNumber,
						"skill" -> $skill,
						"targetAgent" -> $targetAgent,
						"promiseDate" -> $promiseDate,
						"promiseTime" -> $promiseTime,
						"notes" -> $notes,
						"timeZone" -> $timeZone}""")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Patron","Callback","POST /promise",cluster + " - v1.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
	
	
	def createChatSession(pointOfContact:String,fromAddress:String,chatRoomID:Int,parameters:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+"services/v9.0/contacts/chats")
					.postData(raw"""{"pointOfContact" -> $pointOfContact,
						"fromAddress" -> $fromAddress,
						"chatRoomID" -> $chatRoomID,
						"parameters" -> $parameters}""")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Patron","ChatRequests","POST /contacts/chats",cluster + " - v9.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}

	def endChatSession(chatSessionIdInt:Int) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v1.0/contacts/chats/" + chatSessionIdInt)
					.method("DELETE")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Patron","ChatRequests","DELETE /contacts/chats/{chatSession}",cluster + " - v1.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
	

	def getChatText(chatSessionId:String,timeout:Int) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v3.0/contacts/chats/" + chatSessionId + "?timeout=" + timeout)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Patron","ChatRequests","GET /contacts/chats/{chatSession}",cluster + " - v3.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
	
	def sendSingleChatMessage(chatSessionId:String, label:String, message:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v10.0/contacts/chats/" + {chatSessionId} + "/send-text")
					.postData(raw"""{"label" ->$label,
						"message" ->$message}""")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Patron","ChatRequests","POST /contacts/chats/{chatSession}/send-text",cluster + " - v10.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
	
	def postContactsChatSessionTyping(chatSessionId:String,isTyping:String,isTextEntered:String,label:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+"services/v8.0/contacts/chats/" + chatSessionId + "/typing")
					.postData(raw"""{"isTyping" -> $isTyping,
						"isTextEntered" -> $isTextEntered,
						"label" ->$label"}""")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Patron","ChatRequests","POST /contacts/chats/{chatSession}/typing",cluster + " - v8.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
	

	def postContactsChatSessionTypingPreview(chatSessionId:String,previewText:String,label:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+"/services/v8.0/contacts/chats/"+chatSessionId+"/typing-preview")
					.postData(raw"""{"previewText" ->$previewText,
						"label" ->$label"}""")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Patron","ChatRequests","POST /contacts/chats/{chatSessionId}/typing-preview",cluster + " - v8.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
	
	def postContactChatSendEmail(fromAddress:String,toAddress:String,emailBody:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+"/services/v12.0/contacts/chats/send-email")
					.postData(raw"""{"fromAddress" -> $fromAddress,
						"toAddress" -> $toAddress ,
						"emailBody" -> $emailBody"}""")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Patron","ChatRequests","POST /contacts/chats/send-email",cluster + " - v12.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
	
	def getChatprofileByPOCId(pointOfContactId:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v11.0/points-of-contact/"+pointOfContactId+"/chat-profile")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Patron","ChatRequests","GET /points-of-contact/{pointsOfContactId}/chat-profile",cluster + " - v11.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
	
	def createWorkItem(pointOfContact:String,workItemId:String,workItemPayload:String,workItemType:String,from:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "services/v2.0/interactions/work-items")
					.postData(raw"""{"pointOfContact" ->$pointOfContact,
						"workItemId" ->$workItemId,
						"workItemPayload" ->$workItemPayload,
						"workItemType" ->$workItemType,
						"from" ->$from}""")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Patron","WorkItem","POST /interactions/work-items",cluster + " - v2.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
	
	def getAgentsState(updatedSince:String,fields:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v12.0/agents/states")
					.params("updatedSince" -> updatedSince,
						"fields" -> fields)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Real-Time Data","RealTime","GET /agents/states",cluster + " - v12.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def getAgentStateById(updatedSince:String,fields:String, agentId: Int) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v4.0/agents/" + {agentId} + "/states")
					.params("updatedSince" ->updatedSince,
						"fields" ->fields)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Real-Time Data","RealTime","GET /agents/{agentId}/states",cluster + " - v4.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def getContactsActive(updatedSince:String,skillId:Int,fields:String,mediaTypeId:Int,campaignId:Int,agentId:Int,teamId:Int,toAddr:String,fromAddr:String,stateId:Int) {

		//Check if accessToken is empty or null
			if(accessToken!= null && !accessToken.isEmpty())
			{
				//Add all necessary headers and Make the http request
				var response = Http( baseURI +"services/v7.0/contacts/active")
						.params("updatedSince" ->updatedSince,
							"skillId" -> skillId.toString,
							"fields" -> fields,
							"mediaTypeId" -> mediaTypeId.toString,
							"campaignId" -> campaignId.toString,
							"agentId" -> agentId.toString,
							"teamId" -> teamId.toString,
							"toAddr" -> toAddr,
							"fromAddr" -> fromAddr,
							"stateId" -> stateId.toString)
						.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
				construct("Real-Time Data","RealTime","GET /contacts/active",cluster + " - v7.0" ,response.code, response.statusLine)
			}
			else{
			//No token - get one or handle error
			}
	}
	
	def getContactsParked(updatedSince:String,skillId:Int,fields:String,mediaTypeId:Int,campaignId:Int,agentId:Int,teamId:Int,toAddr:String,fromAddr:String) {

		//Check if accessToken is empty or null
			if(accessToken!= null && !accessToken.isEmpty())
			{
				//Add all necessary headers and Make the http request
				var response = Http( baseURI +"services/v7.0/contacts/parked")
						.params("updatedSince" ->updatedSince,
							"skillId" ->skillId.toString,
							"fields" ->fields,
							"mediaTypeId" ->mediaTypeId.toString,
							"campaignId" ->campaignId.toString,
							"agentId" ->agentId.toString,
							"teamId" ->teamId.toString,
							"toAddr" ->toAddr,
							"fromAddr" ->fromAddr)
						.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
				//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
				construct("Real-Time Data","RealTime","GET /contacts/parked",cluster + " - v7.0" ,response.code, response.statusLine)
			}
			else{
			//No token - get one or handle error
			}
		}
		
	def getContactsState(updatedSince:String,agentId:Int) {

		//Check if accessToken is empty or null
			if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v1.0/contacts/states")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
				construct("Real-Time Data","RealTime","GET /contacts/states",cluster + " - v1.0" ,response.code, response.statusLine)
		}
			else{
				//No token - get one or handle error
			}
	}
	
	def getSkillsActivity(updatedSince:String,fields:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v8.0/skills/activity")
					.params("updatedSince" ->updatedSince,
						"fields" ->fields)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Real-Time Data","RealTime","GET /skills/activity",cluster + " - v8.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
	
	def getSkillActivityById(updatedSince:String,fields:String, skillId:Int) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI + "services/v8.0/skills/" + skillId + "/activity")
					.params("updatedSince" ->updatedSince,
						"fields" ->fields)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Real-Time Data","RealTime","GET /skills/{skillId}/activity",cluster + " - v8.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
	

	def getAgentContactHistory(startDate:String,endDate:String,updatedSince:String,mediaTypeId:Int,fields:String,skip:Int,top:Int,orderby:String, agentID:Int) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v5.0/agents/" + {agentID} + "/interaction-history")
					.params("startDate" ->startDate,
						"endDate" ->endDate,
						"updatedSince" ->updatedSince,
						"mediaTypeId" -> mediaTypeId.toString,
						"fields" ->fields,
						"skip" ->skip.toString,
						"top" ->top.toString,
						"orderby" ->orderby)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Reporting","Historical Reporting","GET /agents/{agentId}/interaction-history",cluster + " - v5.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def getRecentContactsInfo(startDate:String,endDate:String,fields:String,top:Int, agentID:Int) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v5.0/agents/" + {agentID} + "/interaction-recent")
					.params("startDate" ->startDate,
						"endDate" ->endDate,
						"fields" ->fields,
						"top" -> top.toString)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Reporting","Historical Reporting","GET /agents/{agentId}/interaction-recent",cluster + " - v5.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def getAgentLoginHistory(startDate:String,endDate:String,searchString:String,fields:String,skip:Int,top:Int,orderby:String, agentID:Int) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v7.0/agents/" + {agentID} + "/login-history")
					.params("startDate" ->startDate,
						"endDate" ->endDate,
						"searchString" ->searchString,
						"fields" -> fields,
						"skip" -> skip.toString,
						"top" -> top.toString,
						"orderby" ->orderby)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Reporting","Historical Reporting","GET /agents/{agentId}/login-history",cluster + " - v7.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	

	def getAgentStateHistory(startDate:String,endDate:String,mediaType:String,agentId:Int) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v6.0/agents/" + {agentId} + "/statehistory")
					.params("startDate" ->startDate,
						"endDate" ->endDate,
						"mediaType" ->mediaType)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Reporting","Historical Reporting","GET /agents/{agentId}/state-history",cluster + " - v6.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def getAgentsPerformance(startDate:String,endDate:String,fields:String, agentID:Int) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v3.0/agents/performance")
					.params("startDate" ->startDate,
						"endDate" ->endDate,
						"fields" ->fields)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Reporting","Historical Reporting","GET /agents/performance",cluster + " - v3.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def getAgentPerformanceById(startDate:String,endDate:String,fields:String, agentID:Int) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v3.0/agents/" + {agentID} + "/performance")
					.params("startDate" ->startDate,
						"endDate" ->endDate,
						"fields" ->fields)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Reporting","Historical Reporting","GET /agents/{agentId}/performance",cluster + " - v3.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def getContactHistory(fields:String, contactId:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v7.0/contacts/" + {contactId})      
					.params("fields" ->fields)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Reporting","Historical Reporting","GET /contacts/{contactId}",cluster + " - v7.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def GetSmstranscripts(startDate:String,endDate:String,transportCode:String,agentID:String,skip:Int,top:Int,orderby:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v10.0/contacts/sms-transcripts")
					.params("startDate" ->startDate,
						"endDate" ->endDate,
						"transportCode" ->transportCode,
						"agentID" -> agentID,
						"skip" -> skip.toString,
						"top" -> top.toString,
						"orderby" ->orderby)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Reporting","Historical Reporting","GET /contacts/sms-transcripts",cluster + " - v10.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def GetSmstranscriptsbyContactid(contactId:String, startDate:String,endDate:String,transportCode:String,skip:Int,top:Int) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v10.0/contacts/" + {contactId.toInt} + "/sms-transcripts")
					.params("startDate" -> startDate,
						"endDate" -> endDate,
						"transportCode" ->transportCode,
						"skip" -> skip.toString,
						"top" -> top.toString )
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Reporting","Historical Reporting","GET /contacts/{contactid}/sms-transcripts",cluster + " - v10.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def getCompletedContacts(startDate:String,endDate:String,updatedSince:String,mediaType:String,fields:String,skip:Int,top:Int,orderby:String,skillId:Int,campaignId:Int,agentId:Int,teamId:Int,toAddr:String,fromAddr:String,isLogged:Boolean,isRefused:Boolean,isTakeover:Boolean,tags:String,analyticsProcessed:Boolean) {

			//Check if accessToken is empty or null
			if(accessToken!= null && !accessToken.isEmpty())
			{
				//Add all necessary headers and Make the http request
				var response = Http( baseURI +"services/v8.0/contacts/completed")
						.params("startDate" -> startDate,
							"endDate" -> endDate,
							"updatedSince" -> updatedSince,
							"mediaType" -> mediaType,
							"fields" -> fields,
							"skip" -> skip.toString,
							"top" -> top.toString,
							"orderby" ->orderby,
							"skillId" -> skillId.toString,
							"campaignId" -> campaignId.toString,
							"agentId" -> agentId.toString,
							"teamId" -> teamId.toString,
							"toAddr" -> toAddr,
							"fromAddr" ->fromAddr,
							"isLogged" ->isLogged.toString,
							"isRefused" ->isRefused.toString,
							"isTakeover" ->isTakeover.toString,
							"tags" ->tags,
							"analyticsProcessed" ->analyticsProcessed.toString)
						.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
				//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
				construct("Reporting","Historical Reporting","GET /contacts/completed",cluster + " - v8.0" ,response.code,response.statusLine)
			}
		else{
		//No token - get one or handle error
		}
	}
	
	def callquality(contactID:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v8.0/contacts/" + {contactID} + "/call-quality")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Reporting","Historical Reporting","GET /contacts/{contactID}/call-quality",cluster + " - v8.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def getContactStateHistory(contactID:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v6.0/contacts/" + {contactID} + "/statehistory")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
 
			construct("Reporting","Historical Reporting","GET /contacts/{contactID}/statehistory",cluster + " - v6.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def getContactCustomData(contactID:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v6.0/contacts/" + {contactID} + "/custom-data")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
 
			construct("Reporting","Historical Reporting","GET /contacts/{contactId}/custom-data",cluster + " - v6.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def getSkillsSummary(startDate:String,endDate:String,mediaTypeId:Int,isOutboundSkill: Boolean,fields:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v7.0/skills/summary")
					.params("startDate" ->startDate,
						"endDate" ->endDate,
						"mediaTypeId" -> mediaTypeId.toString,
						"isOutbound" -> isOutboundSkill.toString,
						"fields" ->fields)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Reporting","Historical Reporting","GET /skills/summary",cluster + " - v7.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	

	def getSkillSummaryById(startDate:String,endDate:String,fields:String, skillId:Int) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v7.0/skills/" + {skillId} + "/summary")
					.params("startDate" ->startDate,
						"endDate" ->endDate,
						"fields" ->fields)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Reporting","Historical Reporting","GET /skills/{skillId}/summary",cluster + " - v7.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def getSkillsSlaSummary(startDate:String,endDate:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v1.0/skills/sla-summary")
					.params("startDate" ->startDate,
						"endDate" ->endDate)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Reporting","Historical Reporting","GET /skills/sla-summary",cluster + " - v1.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def getSkillSlaSummaryById(startDate:String,endDate:String, skillId:Int) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v2.0/skills/" + {skillId} + "/sla-summary")
					.params("startDate" ->startDate,
						"endDate" ->endDate)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Reporting","Historical Reporting","GET /skills/{skillId}/sla-summary",cluster + " - v2.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	

	def GetTeamPerformanceTotal(startDate:String,endDate:String,fields:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v3.0/teams/performance-total")
					.params("startDate" ->startDate,
						"endDate" ->endDate,
						"fields" ->fields)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Reporting","Historical Reporting","GET /teams/performance-total Returns",cluster + " - v3.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	
	def GetTeamPerformancebyTeamIDTotal(teamId:Int,startDate:String,endDate:String,fields:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI + "/services/v3.0/teams/" + {teamId} + "/performance-total")
					.params("startDate" ->startDate,
						"endDate" ->endDate,
						"fields" ->fields)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Reporting","Historical Reporting","GET /teams/{teamId}/performance-total",cluster + " - v3.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def getReports() {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v11.0/reports")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Reporting","Historical Reporting","GET /reports",cluster + " - v11.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def getReportJobs(fields:String,reportId:Int,jobStatus:String,jobSpan:Int) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v5.0/report-jobs")
					.params("fields" ->fields,
						"reportId" -> reportId.toString,
						"jobStatus" ->jobStatus,
						"jobSpan" -> jobSpan.toString)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Reporting","Historical Reporting","GET /report-jobs",cluster + " - v5.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def getReportJobById(jobId:Int,fields:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"services/v5.0/report-jobs/" + jobId + "?fields=" +fields)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Reporting","Historical Reporting","GET /report-jobs/{jobId}",cluster + " - v5.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def startReportJob(fileType:String,includeHeaders: Boolean,isAppendDate: Boolean,deleteAfter:Int,overwrite:Boolean,startDate:String,endDate:String, reportId:Int) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+"services/v7.0/report-jobs/" + {reportId})
					.postData(raw"""{"fileType" -> $fileType,
						"includeHeaders" -> $includeHeaders,
						"appendDate" -> $isAppendDate,
						"deleteAfter" -> $deleteAfter,
						"overwrite" ->$overwrite,
						"startDate" ->$startDate,
						"endDate" -> $endDate}""")
 
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Reporting","Historical Reporting","POST /report-jobs/{reportId}",cluster + " - v7.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def GetdatadownloadbyReportID(reportId:Int,fileName:String,startDate:String,endDate:String,saveFile:Boolean,includeHeaders:Boolean) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+"/services/v10.0/report-jobs/datadownload/"+ {reportId})
					.postData(raw"""{"reportId" -> $reportId,
						"fileName" ->$fileName,
						"startDate" ->$startDate,
						"endDate" ->$endDate,
						"saveFile" ->$saveFile,
						"includeHeaders" ->$includeHeaders}""")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
 
			construct("Reporting","Historical Reporting","POST /report-jobs/datadownload/{reportid}",cluster + " - v10.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def Getwfoascm(startDate:String,endDate:String,fields:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v5.0/wfo-data/ascm")
					.params("startDate" ->startDate,
						"endDate" ->endDate,
						"fields" ->fields)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Reporting","Historical Reporting","GET /wfo-data/ascm",cluster + " - v5.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def Getwfoasi(startDate:String,endDate:String,fields:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v5.0/wfo-data/asi")
					.params("startDate" ->startDate,
						"endDate" ->endDate,
						"fields" ->fields)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Reporting","Historical Reporting","GET /wfo-data/asi",cluster + " - v5.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def Getwfocsi(startDate:String,endDate:String,fields:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v5.0/wfo-data/csi")
					.params("startDate" ->startDate,
						"endDate" ->endDate,
						"fields" ->fields)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Reporting","Historical Reporting","GET /wfo-data/csi",cluster + " - v5.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def Getwfoftci(startDate:String,endDate:String,fields:String) {
 
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v5.0/wfo-data/ftci")
					.params("startDate" ->startDate,
						"endDate" ->endDate,
						"fields" ->fields)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Reporting","Historical Reporting","GET /wfo-data/ftci",cluster + " - v5.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def Getwfoosi(startDate:String,endDate:String,fields:String) {
 
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v5.0/wfo-data/osi")
					.params("startDate" ->startDate,
						"endDate" ->endDate,
						"fields" ->fields)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Reporting","Historical Reporting","GET /wfo-data/osi",cluster + " - v5.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def Getwfoqm(startDate:String,endDate:String,fields:String) {
 
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v5.0/wfo-data/qm")
					.params("startDate" ->startDate,
						"endDate" ->endDate,
						"fields" ->fields)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Reporting","Historical Reporting","GET /wfo-data/qm",cluster + " - v5.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def GetwfmSkillContacts(startDate:String,endDate:String,fields:String,mediaTypeId:Int) {
 
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v10.0/wfm-data/skills/contacts")
					.params("startDate" ->startDate,
						"endDate" ->endDate,
						"fields" ->fields,
						"mediaTypeId" ->mediaTypeId.toString)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Reporting","WFM Data","GET /wfm-data/skills/contacts",cluster + " - v10.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	

		def Getwfmagetns(startDate:String,endDate:String,fields:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v9.0/wfm-data/agents")
					.params("startDate" ->startDate,
						"endDate" ->endDate,
						"fields" ->fields)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Reporting","WFM Data","GET /wfm-data/agents",cluster + " - v9.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def GetwfmdailerContacts(startDate:String,endDate:String,fields:String) {
 
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v9.0/wfm-data/skills/dialer-contacts")
					.params("startDate" ->startDate,
						"endDate" ->endDate,
						"fields" ->fields)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Reporting","WFM Data","GET /wfm-data/skills/dialer-contacts",cluster + " - v9.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def Getwfmscheduleadherence(startDate:String,endDate:String,fields:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v9.0/wfm-data/agents/schedule-adherence")
					.params("startDate" ->startDate,
						"endDate" ->endDate,
						"fields" ->fields)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Reporting","WFM Data","GET /wfm-data/agents/schedule-adherence",cluster + " - v9.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def GetwfmAgentscorecard(startDate:String,endDate:String,fields:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v9.0/wfm-data/agents/scorecards")
					.params("startDate" ->startDate,
						"endDate" ->endDate,
						"fields" ->fields)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Reporting","WFM Data","GET /wfm-data/agents/scorecards",cluster + " - v9.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def GetwfmAgentperformance(startDate:String,endDate:String,fields:String) {
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v9.0/wfm-data/skills/agent-performance")
					.params("startDate" ->startDate,
						"endDate" ->endDate,
						"fields" ->fields)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Reporting","WFM Data","GET /wfm-data/skills/agent-performance",cluster + " - v9.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	//Version 15
	
	def GetAccesskeys(agentId:String,fields:String,skip:String,top:String,orderby:String)
	{
	//Check if accessToken is empty or null
	if(accessToken!= null && !accessToken.isEmpty()){
	//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v15.0/access-keys")
			.params("agentId" ->agentId,
						"skip" ->skip,
						"fields" ->fields,
						"top"->top,
						"orderBy"->orderby)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Agents","GET /access-keys",cluster + " - v15.0" ,response.code, response.statusLine)
			}
			else{
		//No token - get one or handle error
		}
	
	}
	
	def postAccesskeys(agentID:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+"/services/V15.0/access-keys")
					.postData(raw"""{"agentID":$agentID}""")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Agents","POST /access-keys",cluster + " - v15.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
	def deleteAccesskey(AccesskeyId:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v15.0/access-keys/"+{AccesskeyId})
					.method("DELETE")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString

			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		construct("Admin","Agents","DELETE /access-keys/{accesskeyid}",cluster + " - v15.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
	def GetAccesskeysByID(AccesskeyId:String)
	{
	//Check if accessToken is empty or null
	if(accessToken!= null && !accessToken.isEmpty()){
	//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v15.0/access-keys/"+{AccesskeyId})
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Agents","GET /access-keys/{accesskeyid}",cluster + " - v15.0" ,response.code, response.statusLine)
			}
			else{
		//No token - get one or handle error
		}
	
	}
	
	def PatchAccesskeys(AccesskeyId:String,isActive:Boolean){
	
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v15.0/access-keys/"+{AccesskeyId})
				.postData(raw"""{"isActive":$isActive}""")
				.method("patch")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString

			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Agents","PATCH /access-keys/{accesskeyid}",cluster + " - v15.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def PostCampaigns(data:String) {

         //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v15.0/campaigns")
				.postData(data)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"),("Accept-Encoding", "gzip")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Skills","POST /campaigns",cluster + " - v15.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		 }
	}
	def PutCampaignsByID(campaignId:Int,data:String){
	
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v15.0/campaigns/"+campaignId)
				.postData(data)
				.method("put")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000))
				.asString

			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Skills","PUT /campaigns/{campaignId}",cluster + " - v15.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	def PostCampaignsBySkill(campaignId:Int,data:String) {

         //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v15.0/campaigns/"+campaignId+"/skills")
				.postData(data)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"),("Accept-Encoding", "gzip")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000))
				.asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Skills","POST /campaigns/{campaignId}/skills",cluster + " - v15.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		 }
	}
	def deleteCampaignsBySkill(campaignId:Int,data:String) {

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v15.0/campaigns/"+campaignId+"/skills")
					.postData(data)
					.method("DELETE")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"),("Accept-Encoding", "gzip")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString

			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		construct("Admin","Skills","DELETE /campaigns/{campaignId}/skills",cluster + " - v15.0" ,response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
	def GetContactidSmsHistoricalTranscript(contactId:Int,businessUnitId:String)
	{
	//Check if accessToken is empty or null
	if(accessToken!= null && !accessToken.isEmpty()){
	//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v15.0/contacts/"+{contactId}+"/sms-historical-transcript")
			.params("businessUnitId" ->businessUnitId)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Contacts","GET /contacts/{contactid}/sms-historical-transcript",cluster + " - v15.0" ,response.code, response.statusLine)
			}
			else{
		//No token - get one or handle error
		}
	
	}
	
	def GetSmsHistoricalTranscript(ani:String,SkillID:String,businessUnitId:String)
	{
	//Check if accessToken is empty or null
	if(accessToken!= null && !accessToken.isEmpty()){
	//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v15.0/contacts/sms-historical-contacts")
			.params("businessUnitId" ->businessUnitId,
					"skillId"->SkillID,
					"ani"->ani)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Contacts","GET /contacts/sms-historical-contacts",cluster + " - v15.0" ,response.code, response.statusLine)
			}
			else{
		//No token - get one or handle error
		}
	
	}
	
	def PutUnavailableCodeByID(unavailableCodeId:Int,data:String){
	
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v15.0/unavailable-codes/"+unavailableCodeId)
				.postData(data)
				.method("put")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000))
				.asString

			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Agents","PUT /unavailable-codes/{unavailableCodeId}",cluster + " - v15.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	def PostHoursofOperation(data:String) {

         //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v15.0/hours-of-operation")
				.postData(data)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"),("Accept-Encoding", "gzip")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000))
				.asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","General","POST /hours-of-operation",cluster + " - v15.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		 }
	}
	def PutHoursofOperationByID(profileId:Int){
	
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v15.0/hours-of-operation/"+profileId)
				.method("put")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000))
				.asString

			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","PUT /hours-of-operation/{profileId}",cluster + " - v15.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	def PostPointofContact(data:String) {

         //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v15.0/points-of-contact")
				.postData(data)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"),("Accept-Encoding", "gzip")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000))
				.asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","General","POST /points-of-contact",cluster + " - v15.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		 }
	}
	def PutPointofContactByID(pointOfContactId:Int,data:String){
	
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v15.0/points-of-contact/"+pointOfContactId)
				.postData(data)
				.method("put")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000))
				.asString

			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","PUT /points-of-contact/{pointOfContactId}",cluster + " - v15.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	def PostUnavailableCodes(data:String) {

         //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v15.0/unavailable-codes")
				.postData(data)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"),("Accept-Encoding", "gzip")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000))
				.asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","General","POST /unavailable-codes",cluster + " - v15.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		 }
	}
	def GetPhonenumbers(searchString:String,skip:String,top:String)
	{
	//Check if accessToken is empty or null
	if(accessToken!= null && !accessToken.isEmpty()){
	//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v15.0/access-keys")
			.params("searchString" ->searchString,
						"skip" ->skip,
						"top"->top)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","GET /phone-numbers",cluster + " - v15.0" ,response.code, response.statusLine)
			}
			else{
		//No token - get one or handle error
		}
	
	}
	def GetDipositionBySkill(isactive:String,updatedSince:String,searchString:String,fields:String,skip:String,top:String,orderby:String)
	{
	//Check if accessToken is empty or null
	if(accessToken!= null && !accessToken.isEmpty()){
	//Add all necessary headers and Make the http request
			var response = Http( baseURI +"/services/v15.0/access-keys")
			.params("searchString" ->searchString,
						"skip" ->skip,
						"fields" ->fields,
						"top"->top,
						"orderBy"->orderby,
						"updatedSince"->updatedSince,
						"isactive"->isactive)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Skills","GET /dispositions/skills",cluster + " - v15.0" ,response.code, response.statusLine)
			}
			else{
		//No token - get one or handle error
		}
	
	}
	
	def postAddText(sessionID:String,data:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v15.0/agent-sessions/"+ sessionID +"/interactions/add-text")
							.postData(data)
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","ChatRequests","POST /agent-sessions/{sessionID}/interactions/add-text",cluster + " - v15.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	def postEmailSaveDraft(sessionID:String,contactId:Int,data:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( baseURI +"services/v15.0/agent-sessions/"+sessionID+"/interactions/"+contactId+ "/email-save-draft")
							.postData(data)
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
							.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString	
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Agent","ChatRequests","POST /agent-sessions/{sessionID}/interactions/{contactId}/add-text",cluster + " - v15.0",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	def PutUnavailableCodeByIDTeams(unavailableCodeId:Int,data:String,securityUser:String){
	
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v15.0/unavailable-codes/"+unavailableCodeId+"/teams")
				.postData(data)
				.method("put")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000))
				.asString

			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Skills","PUT /campaigns/{campaignId}",cluster + " - v15.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}


def postJobSync(data:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( CXone_baseURI +"data-extraction/v1/jobs/sync")
							.postData(data)
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("DataExtraction","ExtractData","POST jobs/Sync",cluster + " - v1",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	
	def GetMediaplayBack(contactID:String,mediatype:String,excludewaveform:String)
	{
	//Check if accessToken is empty or null
	if(accessToken!= null && !accessToken.isEmpty()){
	//Add all necessary headers and Make the http request
			var response = Http( CXone_baseURI +"media-playback/v1/contacts/"+contactID)
			.params("mediatype" ->mediatype,
						"excludewaveform" ->excludewaveform)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Mediaplayback","AccessingRecordingMedia","GET /media-playback/v1/contacts/{contactId}",cluster + " - v1.0" ,response.code, response.statusLine)
			}
			else{
		//No token - get one or handle error
		}
	
	}
	def GetMediaplayBackByID(contactID:String,segmentID:String,mediatype:String,excludewaveform:String)
	{
	//Check if accessToken is empty or null
	if(accessToken!= null && !accessToken.isEmpty()){
	//Add all necessary headers and Make the http request
			var response = Http( CXone_baseURI +"media-playback/v1/contacts/"+contactID+ "/segments/"+segmentID)
			.params("mediatype" ->mediatype,
						"excludewaveform" ->excludewaveform)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Mediaplayback","AccessingRecordingMedia","GET ​/media-playback​/v1​/contacts​/{contactId}​/segments​/{segmentId}",cluster + " - v1.0" ,response.code, response.statusLine)
			}
			else{
		//No token - get one or handle error
		}
	
	}
	def GetMediaplayBackContacts(acdID:String,mediatype:String,excludewaveform:String)
	{
	//Check if accessToken is empty or null
	if(accessToken!= null && !accessToken.isEmpty()){
	//Add all necessary headers and Make the http request
			var response = Http( CXone_baseURI +"media-playback/v1/contacts/"+acdID)
			.params("mediatype" ->mediatype,
						"excludewaveform" ->excludewaveform)
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Mediaplayback","AccessingRecordingMedia","GET /media-playback/v1/contacts",cluster + " - v1.0" ,response.code, response.statusLine)
			}
			else{
		//No token - get one or handle error
		}
	
	}
	
def getActiveFlag(activeFlag:String){
	//Check if accessToken is empty or null
	if(accessToken!= null && !accessToken.isEmpty()){
	//Add all necessary headers and Make the http request
			var response = Http(baseURI +"/services/v16.0/workflow-data/list/"+activeFlag)
			
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","WorkFlow ","GET /services/v16.0/workflow-data/list",cluster + " - v16.0" ,response.code, response.statusLine)
			}
			else{
		//No token - get one or handle error
		}
	
	}

def postWorkFlow(data:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http(baseURI +"/services/v16.0/workflow-data")
							.postData(data)
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("Admin","WorkFlow ","POST /services/v16.0/workflow-data",cluster + " - v16.0" ,response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}	
def getWorkFlowById(workflowDataId:String){
	//Check if accessToken is empty or null
	if(accessToken!= null && !accessToken.isEmpty()){
	//Add all necessary headers and Make the http request
			var response = Http(baseURI +"/services/v16.0/workflow-data/"+workflowDataId )
			
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
 
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","WorkFlow ","GET /services/v16.0/workflow-data/ID",cluster + " - v16.0" ,response.code, response.statusLine)
			}
			else{
		//No token - get one or handle error
		}
	}	
	
def putWorkFlowById(workflowDataId:String, data:String){
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v16.0/workflow-data/"+workflowDataId)
				.postData(data)
				.method("put")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000))
				.asString

			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","WorkFlow ","PUT /services/v16.0/workflow-data/ID",cluster + " - v16.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}	
	
def putWorkFlowByIdActivate(workflowDataId:String){
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v16.0/workflow-data/"+workflowDataId+"/activate")
				.method("put")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000))
				.asString

			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","WorkFlow ","PUT /services/v16.0/workflow-data/ID/activate",cluster + " - v16.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}	
	
def putWorkFlowByIdDeactivate(workflowDataId:String){
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v16.0/workflow-data/"+workflowDataId+"/deactivate")
				.method("put")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000))
				.asString

			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","WorkFlow ","PUT /services/v16.0/workflow-data/ID/deactivate",cluster + " - v16.0" ,response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	def getjobs(){
	//Check if accessToken is empty or null
	if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( CXone_baseURI +"data-extraction/v1/jobs")
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("DataExtraction","ExtractData","GET jobs",cluster + " - v1",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}
	}
	
	def postJobs(data:String) {
		//Check if accessToken is empty or null
		if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( CXone_baseURI +"data-extraction/v1/jobs")
							.postData(data)
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("DataExtraction","ExtractData","POST jobs",cluster + " - v1",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}				
	}
	
	def getjobsByID(jobId:String){
	//Check if accessToken is empty or null
	if(accessToken != null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request 
		    var response = Http( CXone_baseURI +"data-extraction/v1/jobs/" +jobId)
							.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON 
			//package to handle the response appropriately
			construct("DataExtraction","ExtractData","GET jobs/jobId",cluster + " - v1",response.code, response.statusLine)
		}else{
			   //No token - get one or handle error
		}
	}
	
	def getBusinessunitTimezone() {

		//Give the specified url ,accessToken

			val accessToken = "";
			val Baseuri = "";

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			val response = Http( Baseuri +"services/v17/business-unit/time-zones")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","GET /business-unit/time-zones",cluster + " - v17",response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
	
	def getSuppresedContacts() {

		//Give the specified url ,accessToken

			val accessToken = "";
			val Baseuri = "";

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			val response = Http( Baseuri +"services/v17/suppressed-contact")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","GET /suppressed-contact",cluster + " - v17",response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
	
	def PostSuppresedContacts(data:String) {
//Check if accessToken is empty or null
if(accessToken!= null && !accessToken.isEmpty())
	{
		//Add all necessary headers and Make the http request
		var response = Http(baseURI+ "/services/v17/suppressed-contact")
				.postData(data)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"),("Accept-Encoding", "gzip")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000))
				.asString
			construct("Admin","General","Post /suppressed-contact",cluster + " - v17",response.code, response.statusLine)
	}
else{
		//No token - get one or handle error
	}
}

def putbusinessunitTimezone(data:String)
{
	//Check if accessToken is empty or null
	if(accessToken!= null && !accessToken.isEmpty())
	{
		//Add all necessary headers and Make the http request
		var response = Http(baseURI+ "/services/v17/business-unit/time-zones")
				.postData(data)
				.method("put")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000))
				.asString
			construct("Admin","General","Put /business-unit/time-zones",cluster + " - v17",response.code, response.statusLine)
	}
	else
    {
		//No token - get one or handle error
	}
}

def PutSkillIDParameterTimezone(skillId:String){
	//Give the specified url,accessToken
			val accessToken="";
			val baseURI="";
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			val response = Http(baseURI+ "/services/v17/skills/" + skillId + "/parameters/time-zones")
					.postData("""{"timeZoneSettings"-> [
				{
					
					"timeZoneSettings"-> "string"}]}""")
					.method("put")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString
			construct("Admin","Skill","Put /parameters/time-zones",cluster + " - v17",response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def getTimeZoneBySkillId(skillId:String) {
       //Give the specified url ,accessToken
       
         val accessToken = "";
	    val baseURI = "";
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request
		    val response = Http( baseURI +"/services/v17/skills/" + skillId + "/parameters/time-zones")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.asString
			construct("Admin","Skill","Get /parameters/time-zones",cluster + " - v17",response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}
	
	def deleteSuppressedContactBySuppressedContactId(suppressedContactId:Int){
//Check if accessToken is empty or null
if(accessToken!= null && !accessToken.isEmpty())
	{
		//Add all necessary headers and Make the http request
		var response = Http(baseURI+ "/services/{version}/suppressed-contact/"+suppressedContactId)
				.postData(data)
				.method("delete")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000))
				.asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
	}
else{
	//No token - get one or handle error
	}
	}

def getSuppressedContactBySuppressedContactId() {

		//Give the specified url ,accessToken

			val accessToken = "";
			val Baseuri = "";

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			val response = Http( Baseuri +"services/{version}/suppressed-contact/" + {suppressedContactId})
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			println(response);
		}
		else{
			//No token - get one or handle error
		}
	}

def putSuppressedContactBySuppressedContactId(suppressedContactId:Int,data:String){
//Check if accessToken is empty or null
if(accessToken!= null && !accessToken.isEmpty())
	{
		//Add all necessary headers and Make the http request
		var response = Http(baseURI+ "/services/{version}/suppressed-contact/"+suppressedContactId)
				.postData(data)
				.method("put")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000))
				.asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
	}
else{
	//No token - get one or handle error
	}
}

def getContactsByIdHierachy(contactID:String)
{
//Check if accessToken is empty or null
if(accessToken!= null && !accessToken.isEmpty())
    {
	//Add all necessary headers and Make the http request
	var response = Http( baseURI +"/services/{version}/contacts/{contactId}/hierarchy")
			.params("contactId" ->contactId)
			.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
	}
else{
		//No token - get one or handle error
	}
}	
	
	def getBusinessunitTimezone() {

		//Give the specified url ,accessToken

			val accessToken = "";
			val Baseuri = "";

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			val response = Http( Baseuri +"services/v17/business-unit/time-zones")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","GET /business-unit/time-zones",cluster + " - v17",response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
	
	def getSuppresedContacts() {

		//Give the specified url ,accessToken

			val accessToken = "";
			val Baseuri = "";

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			val response = Http( Baseuri +"services/v17/suppressed-contact")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","General","GET /suppressed-contact",cluster + " - v17",response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
	
	def PostSuppresedContacts(data:String) {
//Check if accessToken is empty or null
if(accessToken!= null && !accessToken.isEmpty())
	{
		//Add all necessary headers and Make the http request
		var response = Http(baseURI+ "/services/v17/suppressed-contact")
				.postData(data)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"),("Accept-Encoding", "gzip")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000))
				.asString
			construct("Admin","General","Post /suppressed-contact",cluster + " - v17",response.code, response.statusLine)
	}
else{
		//No token - get one or handle error
	}
}

def putbusinessunitTimezone(data:String)
{
	//Check if accessToken is empty or null
	if(accessToken!= null && !accessToken.isEmpty())
	{
		//Add all necessary headers and Make the http request
		var response = Http(baseURI+ "/services/v17/business-unit/time-zones")
				.postData(data)
				.method("put")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000))
				.asString
			construct("Admin","General","Put /business-unit/time-zones",cluster + " - v17",response.code, response.statusLine)
	}
	else
    {
		//No token - get one or handle error
	}
}

def PutSkillIDParameterTimezone(skillId:String){
	//Give the specified url,accessToken
			val accessToken="";
			val baseURI="";
		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			val response = Http(baseURI+ "/services/v17/skills/" + skillId + "/parameters/time-zones")
					.postData("""{"timeZoneSettings"-> [
				{
					
					"timeZoneSettings"-> "string"}]}""")
					.method("put")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString
			construct("Admin","Skill","Put /parameters/time-zones",cluster + " - v17",response.code, response.statusLine)
		}
		else{
		//No token - get one or handle error
		}
	}
	
	def getTimeZoneBySkillId(skillId:String) {
       //Give the specified url ,accessToken
       
         val accessToken = "";
	    val baseURI = "";
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request
		    val response = Http( baseURI +"/services/v17/skills/" + skillId + "/parameters/time-zones")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.asString
			construct("Admin","Skill","Get /parameters/time-zones",cluster + " - v17",response.code, response.statusLine)
		 }
      else{
			   //No token - get one or handle error
		 }
	}
	
	def object SkillBySkillIDAgentID () {

		//Give the specified url ,accessToken

			val accessToken = "";
			val Baseuri = "";

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			val response = Http( Baseuri +"/services/{version}/skills/" + skillId + "/agents/" + agentId)
					.method("DELETE")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString

			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			construct("Admin","Skill","Delete /skills/{skillId}/agents/{agentId}",cluster + " - v17",response.code, response.statusLine)
		}
		else{
			//No token - get one or handle error
		}
	}
	def getScripts() {
       //Give the specified url ,accessToken
       
         val accessToken = "";
	    val baseURI = "";
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request
		    val response = Http( baseURI +"services/{version}/scripts" )
					.params("stringPath"-> "string",
					"stringId"-> "integer",
					)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	println(response);
		 }
      else{
			   //No token - get one or handle error
		 }
	}
	def postScript() {

       //Give the specified url ,accessToken
       
         val accessToken = "";
	    val baseURI = "";
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			val response = Http(baseURI+ "services/{version}/scripts")
					.postData("""{"scriptPath"-> "string - required",
					"body"-> "string - required"}""")

			.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
			.asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			println(response);
		}
		else{
			   //No token - get one or handle error
		}
	}
	def PutupdateScript(scriptPath:String, lockScript:Boolean ){
	//Give the specified url,accessToken

			val accessToken="";
			val baseURI="";

		//Check if accessToken is empty or null
		if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			val response = Http(baseURI+ "/services/{version}/files")
					.postData("""{"oldPath"->oldPath,
					"newPath"->newPath,
					"overwrite"->overwrite}""")
					.method("put")
					.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString

			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
			println(response);
		}
		else{
		//No token - get one or handle error
		}
	}
	def postScriptKick() {

       //Give the specified url ,accessToken
       
         val accessToken = "";
	     val baseURI = "";
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			val response = Http(baseURI+ "services/{version}/scripts/kick")
					.postData("""{"scriptPath"-> "string - required"}""")

			.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
			.asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			println(response);
		}
		else{
			   //No token - get one or handle error
		}
	}
	def getScriptsHistoryByName() {
       //Give the specified url ,accessToken
       
         val accessToken = "";
	    val baseURI = "";
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request
		    val response = Http( baseURI +"services/{version}/scripts/historyByName/{scriptPath}" )
					.params("stringPath"-> "string",
					"stringId"-> "integer",
					)
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	println(response);
		 }
      else{
			   //No token - get one or handle error
		 }
	}


//Admin--> Skills V18	
def PutSkillListManagement(skillId:String,data:String) {
	 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v18.0/skills/" + skillId + "/parameters/list-management")
				.postData(data) 
				.method("put")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Skills","PUT /skills/{skillId}/parameters/list-management",cluster + " - v18.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		 }
	}

def getSkillsParameters() {
       //Give the specified url ,accessToken
       
         val accessToken = "";
	    val baseURI = "";
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request
		    val response = Http( baseURI +"services/{version}/skills/parameters" )
					.params()
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	println(response);
		 }
      else{
			   //No token - get one or handle error
		 }
	
	}	
def PutCadencesettings(skillId:String,data:String) {
	 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v13.0/skills/" + skillId + "/parameters/cadence-settings")
				.postData(data) 
				.method("put")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Skills","PUT /skills/{skillId}/parameters/cadence-settings",cluster + " - v18.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		 }
	}

def getSkillsSkillIdParameters(SkillId: String) {
       //Give the specified url ,accessToken
       
         val accessToken = "";
	    val baseURI = "";
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request
		    val response = Http( baseURI +"services/{version}/skills/"+skillId+"/parameters" )
					.params()
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	println(response);
		 }
      else{
			   //No token - get one or handle error
		 }
	
	}
def postSetDisposition(contactId:Int) {

       //Give the specified url ,accessToken
       
         val accessToken = "";
	    val baseURI = "";
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			val response = Http(baseURI+ "services/{version}/contacts/"+contactId+"/set-disposition")
					.postData("""{"dispositionInfo"{"Skill"-> "1007","DispositionCode"-> "1","CallbackNumber"-> "","CallbackTime"-> "","notes"-> "string",
					"CommitmentAmount"-> "string "}}""")
			.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
			.asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			println(response);
		}
		else{
			   //No token - get one or handle error
		}
	}

def getBusinessUnitOutboundRoutes() {
       //Give the specified url ,accessToken
       
         val accessToken = "";
	    val baseURI = "";
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request
		    val response = Http( baseURI +"services/{version}/business-unit/outbound-routes" )
					.params()
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	println(response);
		 }
      else{
			   //No token - get one or handle error
		 }
	
	}
	
def getclientdata() {
       //Give the specified url ,accessToken
       
         val accessToken = "";
	    val baseURI = "";
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request
		    val response = Http( baseURI +"services/{version}/agents/client-data" )
					.params()
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	println(response);
		 }
      else{
			   //No token - get one or handle error
		 }
	
	}
	
def putclientdata(skillId:String,data:String) {
	 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v19.0/agents/client-data")
				.postData(data) 
				.method("put")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Skills","PUT /agents/client-data",cluster + " - v19.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		 }
	}	
	
def postsmsoutbound(sessionId:String) {

       //Give the specified url ,accessToken
       
         val accessToken = "";
	    val baseURI = "";
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			val response = Http(baseURI+ "services/{version}/agent-sessions/"+ sessionId +"/interactions/sms-outbound")
					.postData("""{"dispositionInfo"{"Skill"-> "1007","DispositionCode"-> "1","CallbackNumber"-> "","CallbackTime"-> "","notes"-> "string",
					"CommitmentAmount"-> "string "}}""")
			.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
			.asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			println(response);
		}
		else{
			   //No token - get one or handle error
		}
	}
	
def removeprospects(sourceName:String){
//Check if accessToken is empty or null
if(accessToken!= null && !accessToken.isEmpty())
	{
		//Add all necessary headers and Make the http request
		var response = Http(baseURI+ "/services/{version}/lists/call-lists/" + sourceName + "/removeProspects")
				.postData(data)
				.method("delete")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000))
				.asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
	}
else{
	//No token - get one or handle error
	}
	}
	
def putpersistentcontacts(contactId:Int) {
	 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			var response = Http(baseURI+ "/services/v19.0/persistent-contacts/"+ contactId)
				.postData(data) 
				.method("put")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000)).asString
			//TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			construct("Admin","Skills","PUT /persistent-contacts/{contactId}",cluster + " - v19.0" ,response.code, response.statusLine)
		}
		else{
			   //No token - get one or handle error
		 }
	}

def getagentsettings(agentId:Int) {
       //Give the specified url ,accessToken
       
         val accessToken = "";
	    val baseURI = "";
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request
		    val response = Http( baseURI +"services/{version}/agents/" + agentId + "/agent-settings" )
					.params()
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	println(response);
		 }
      else{
			   //No token - get one or handle error
		 }
	
	}

def getscriptsbyId(scriptId) {
       //Give the specified url ,accessToken
       
         val accessToken = "";
	    val baseURI = "";
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request
		    val response = Http( baseURI +"services/{version}/scripts/" + scriptId)
					.params()
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	println(response);
		 }
      else{
			   //No token - get one or handle error
		 }
	
	}
	
def getscriptssearch() {
       //Give the specified url ,accessToken
       
         val accessToken = "";
	    val baseURI = "";
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty()){
			//Add all necessary headers and Make the http request
		    val response = Http( baseURI +"services/{version}/scripts/search" )
					.params()
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
				.asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
          	println(response);
		 }
      else{
			   //No token - get one or handle error
		 }
	
	}

def deletescripts(){
//Check if accessToken is empty or null
if(accessToken!= null && !accessToken.isEmpty())
	{
		//Add all necessary headers and Make the http request
		var response = Http(baseURI+ "/services/{version}/scripts")
				.postData(data)
				.method("delete")
				.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).option(HttpOptions.connTimeout(10000)).option(HttpOptions.readTimeout(50000))
				.asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
	}
else{
	//No token - get one or handle error
	}
	}

def posttransformnumbers(agentpatternId:String) {

       //Give the specified url ,accessToken
       
         val accessToken = "";
	    val baseURI = "";
		 
       //Check if accessToken is empty or null
		 if(accessToken!= null && !accessToken.isEmpty())
		{
			//Add all necessary headers and Make the http request
			val response = Http(baseURI+ "services/{version}/agent-patterns/" + agentpatternId + "/transform-phonenumbers")
					.postData("""{"dispositionInfo"{"Skill"-> "1007","DispositionCode"-> "1","CallbackNumber"-> "","CallbackTime"-> "","notes"-> "string",
					"CommitmentAmount"-> "string "}}""")
			.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json"))
			.asString
			//TODO-> the response comes back as a JSON document. Replace the following code with your preferred JSON //package to handle the response appropriately
		
			println(response);
		}
		else{
			   //No token - get one or handle error
		}
	}
	
	
}