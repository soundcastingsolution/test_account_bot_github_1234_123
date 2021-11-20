#!/usr/bin/python

import requests
import configparser
import os

configParser = configparser.RawConfigParser()
configFilePath = r'data.txt'
configParser.read(configFilePath)
accessToken = 'test'
baseURI = configParser.get('user-config', 'baseURI')
CXone_baseURI=configParser.get('user-config','CXone_baseURI')
username = configParser.get('user-config', 'username')
password = configParser.get('user-config', 'password')
cluster = configParser.get('user-config', 'cluster')
phoneNumber = configParser.get('user-config', 'phoneNumber')
tokenServiceUri = configParser.get('user-config', 'tokenServiceUri')

#remove existing file
if os.path.exists("result.html"):
    os.remove("result.html")

outputFile = open("result.html", "x")

def init():
    global outputFile
    content = """
    <!DOCTYPE html>
    <html>
    <head>
    <link rel="stylesheet" type="text/css" href="table.css">
    </head>
    <body class="rTable">
    <h2>API Test Results for Python Functions</h2>
    <title>Python</title>
    <br/>
    <div class="rTableHead small"><strong>APIName</strong></div>
    <div class="rTableHead small"><span style="font-weight: bold;">APISubType</span></div>
    <div class="rTableHead med"><span style="font-weight: bold;">APIType</span></div>
    <div class="rTableHead small"><span style="font-weight: bold;">Version</span></div>
    <div class="rTableHead"><span style="font-weight: bold;">APIResult</span></div>
    </div>
    </body>
    </html>
    """
    outputFile.write(content)

def construct(APIName,APISubType,APIType,Version,Status,Description):
    global outputFile
    result = """
    <div class="rTableRow">
    <div class="rTableCell small">{0}</div>
    <div class="rTableCell small">{1}</div>
    <div class="rTableCell med">{2}</div>
    <div class="rTableCell small">{3}</div>
    <div class="rTableCell">{4}:{5}</div>
    </div>
    """;
    writeData = """{0}--{1}--{2}--{3}--{4}--{5}""";
    execute = (result).format(APIName,APISubType,APIType,Version,Status,Description)
    print((writeData).format(APIName,APISubType,APIType,Version,Status,Description))
    print("_________________________________")
    outputFile.write(execute)


def getAccessToken():
    global accessToken
    header_param = {'Authorization': 'basic ' + 'aW50ZXJuYWxAaW5Db250YWN0IEluYy46UVVFNVFrTkdSRE0zUWpFME5FUkRSamczUlVORFJVTkRRakU0TlRrek5UYz0=','content-Type': 'application/x-www-form-urlencoded','Accept': 'application/json, text/javascript, */*'}
    payload = {
        'grant_type': 'password',
        'password': password,
        'username' : username
    }

    print("_________________________________")
    print("generate access token")
    print("_________________________________")
    response = requests.post(tokenServiceUri, headers = header_param, data= payload)
    accessToken = response.json().get('access_token')
    print(accessToken)
    return accessToken


def generateAgentSession():
		print(baseURI)
		if accessToken!="":
			payload={
			"stationId": "",
			"stationPhoneNumber": phoneNumber,
			"inactivityTimeout": "100",
			"inactivityForceLogout":"true",
			"asAgentId":""} 
			#add all necessary headers
			header_param={
			'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'}
        # Make http post request
			print("_________________________________")
			print("generate agent session")
			print("_________________________________")
			response = requests.post(baseURI + '/services/v2.0/agent-sessions', headers = header_param, data=payload) 
			print(response.text)
			sessionId = response.json().get('sessionId')
			print(sessionId)
			return sessionId

#Admin --> AddressBook v12
def getStandardAddressBookEntries(addressBookId,updatedSince,searchString,fields,ski,top,orderBy):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param = {'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/x-www-form-urlencoded'}

        response=requests.get(baseURI + '/services/v12.0/address-books/' + addressBookId + '/entries' + '?fields=' + fields + '&updatedSince=' + updatedSince + '&searchString=' + searchString + '&fields=' + fields + '&skip' + skip + '&top=' + top + '&orderBy=' + orderBy,headers=header_param)
        #print response appropriately
        construct('Admin','AddressBook','GET services/v12.0/address-books/{addressBookId}/entries',cluster + ' v12.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

#Admin --> Agent v12
def PostAgent(): #done
    #Check if accessToken is empty or null
    if accessToken!="":

        PostAgentPayload={"agents": [
        {
        "firstName": "sc1",
        "middleName": "sc1",
        "lastName": "",
        "teamId": "",
        "reportToId": 1,
        "internalId": "",
        "profileId": 0,
        "roleId": "",
        "password": "",
        "forceChangeOnLogon": "true",
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
        "useTeamMaxConcurrentChats": "false",
        "isActive": "true",
        "locationId": 0,
        "notes": "",
        "hireDate": "10/10/1800",
        "terminationDate": "10/10/1800",
        "hourlyCost": 0,
        "rehireStatus": "true",
        "employmentType": 1,
        "referral": "",
        "atHome": "false",
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
        "useTeamEmailAutoParkingLimit": "false",
        "sipUser": "",
        "systemUser": "",
        "systemDomain": "",
        "crmUserName": "",
        "useAgentTimeZone": "false",
        "timeDisplayFormat": 0,
        "sendEmailNotifications": "false",
        "apiKey": "",
        "telephone1": "",
        "telephone2": "",
        "userType": "",
        "isWhatIfAgent": "false",
        "requestContact": "false",
        "useTeamRequestContact": "false",
        "chatThreshold": 1,
        "useTeamChatThreshold": "false",
        "emailThreshold": 1,
        "useTeamEmailThreshold": "false",
        "workItemThreshold": 1,
        "useTeamWorkItemThreshold": "false",
        "contactAutoFocus": "false",
        "useTeamContactAutoFocus": "false",
        "subject": "",
        "issuer": "",
        "isOpenIdProfileComplete": "false",
        "recordingNumbers": [{
        "number": ""
        }
        ]
        }
        ]
        }

        header_param={ 
        #Use access_token previously retrieved from inContact token service
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.post(baseURI + '/services/v12.0/agents',headers=header_param,data=PostAgentPayload)
        #print response appropriately
        construct('Admin','AddressBook','POST services/v12.0/agents',cluster + ' v12.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

#Admin--> Agent 11
def getAgentSkillsByAgentId(AgentID,updatedSince,searchString,fields,skip,top,orderBy,MediaTypeId,outboundStrategy): #done
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/x-www-form-urlencoded'}

        response=requests.get(baseURI + '/services/v11.0/agents/' + AgentID + '/skills'+ '?fields=' + fields + '&updatedSince=' + updatedSince + 
        '&searchString=' + searchString + '&fields=' + fields + '&skip' + skip + '&top=' + top + '&orderBy=' + orderBy + '&MediaTypeId=' + MediaTypeId + '&outboundStrategy=' + outboundStrategy,headers=header_param)

        #print response appropriately
        construct('Admin','Agent','GET agents/{agentId}/skills',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def PutAgentbyAgentID(AgentID):
    #Check if accessToken is empty or null
    if accessToken!="":

        getAllAgentsSkillsPayload={"agent": {
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
        "useTeamMaxConcurrentChats": "false",
        "isActive": "true",
        "locationId": 0,
        "notes": "",
        "hireDate": "10/10/1800",
        "terminationDate": "10/10/1800",
        "hourlyCost": 0,
        "rehireStatus": "true",
        "employmentType": 1,
        "referral": "",
        "atHome": "false",
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
        "useTeamEmailAutoParkingLimit": "false",
        "sipUser": "",
        "systemUser": "",
        "systemDomain": "",
        "crmUserName": "",
        "useAgentTimeZone": "false",
        "timeDisplayFormat": 0,
        "sendEmailNotifications": "false",
        "apiKey": "",
        "telephone1": "",
        "telephone2": "",
        "userType": "agent",
        "isWhatIfAgent": "false",
        "requestContact": "false",
        "useTeamRequestContact": "false",
        "chatThreshold": 1,
        "useTeamChatThreshold": "false",
        "emailThreshold": 1,
        "useTeamEmailThreshold": "false",
        "workItemThreshold": 1,
        "useTeamWorkItemThreshold": "false",
        "contactAutoFocus": "false",
        "useTeamContactAutoFocus": "false",
        "subject": "",
        "issuer": "",
        "isOpenIdProfileComplete": "false",
        "recordingNumbers": [
        {
        "number": ""
        }
        ]
        }
        }

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8',
        'Accept':'application/json, text/javascript, */*; q=0.01'}

        response=requests.put(baseURI + '/services/v11.0/agents/' + AgentID,headers=header_param,data=getAllAgentsSkillsPayload)
        #print response appropriately
        construct('Admin','Agent','PUT /agents/{agentId}',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def GetagentbyAgentID(AgentId):
    #Check if accessToken is empty or null
    if accessToken!="":

        getAllAgentsSkillsPayload = {
        'updatedSince': 'string - ISO 8601 formatted date/time',
        'searchString': 'string',
        'fields': 'string',
        'skip': 'integer',
        'top': 'integer',
        'orderBy': 'string'
        }

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/x-www-form-urlencoded'}

        response=requests.get(baseURI + '/services/v11.0/agents/' + AgentId ,headers=header_param,params=getAllAgentsSkillsPayload)
        #print response appropriately
        construct('Admin','Agent','GET /agents/{agentId}',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def GetAgentByAgentIDGroups(AgentId,fields):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/x-www-form-urlencoded'}

        response=requests.get(baseURI + '/services/v11.0/agents/' + AgentId + '/groups' + '?fields=' + fields,headers=header_param)
        #print response appropriately
        construct('Admin','Agent','GET /agents/{agentId}/groups',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def PostAgentMessages(message,startDate,subject,targetId,targetType,validUntil,expireMinutes):
    #Check if accessToken is empty or null
    if accessToken!="":

        createpostmessagesPayload = {
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

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.post(baseURI + '/services/v11.0/agents/messages',headers=header_param,data=createpostmessagesPayload)
        #print response appropriately
        construct('Admin','Agent','POST /agents/messages',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def deleteAgentMessagesByMessageId(MessageId):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.delete(baseURI + '/services/v11.0/agents/messages/' + MessageId,headers=header_param)
        #print response appropriately
        construct('Admin','Agent','DELETE /agents/messages/{messageid}',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def getTeam(fields,updatedSince,searchString,skip,top,orderBy):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v11.0/teams' +"?fields=" + fields + "&updatedSince=" + updatedSince + 
        "&searchString=" + searchString + "&fields=" + fields + "&skip" + skip + "&top=" + top + "&orderBy=" + orderBy,headers=header_param)
        #print response appropriately
        construct('Admin','Agent','GET /teams',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def PutTeam():
    #Check if accessToken is empty or null
    if accessToken!="":

        POSTTeamPayload = {
          "teams": [
        {
        "teamName": "team",
        "isActive": "true",
        "maxConcurrentChats": 8,
        "wfoEnabled": "false",
        "wfm2Enabled": "false",
        "qm2Enabled": "false",
        "inViewEnabled": "false",
        "notes": "",
        "maxEmailAutoParkingLimit": 25,
        "inViewGamificationEnabled": "false",
        "inViewChatEnabled": "false",
        "inViewLMSEnabled": "false",
        "analyticsEnabled": "false",
        "requestContact": "false",
        "chatThreshold": 1,
        "emailThreshold": 1,
        "workItemThreshold": 1,
        "voiceThreshold": 1,
        "contactAutoFocus": "false",
        "teamUuid": ""
        }
        ]
        }

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.put(baseURI + '/services/v11.0/teams',headers=header_param,data=POSTTeamPayload)
        #print response appropriately
        construct('Admin','Agent','PUT /teams',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def PutTeambyTeamID(Teamid):
    #Check if accessToken is empty or null
    if accessToken!="":

        updateTeambyTeamIdPayload = {
        "forceInactive": "false",
        "team": {
        "teamName": "TeamName",
        "isActive": "true",
        "maxConcurrentChats": 8,
        "wfoEnabled": "false",
        "wfm2Enabled": "false",
        "qm2Enabled": "false",
        "inViewEnabled": "false",
        "notes": "",
        "maxEmailAutoParkingLimit": 25,
        "inViewGamificationEnabled": "false",
        "inViewChatEnabled": "false",
        "inViewLMSEnabled": "false",
        "analyticsEnabled": "false",
        "requestContact": "false",
        "chatThreshold": 1,
        "emailThreshold": 1,
        "workItemThreshold": 1,
        "voiceThreshold": 1,
        "contactAutoFocus": "false",
        "teamUuid": ""
        }
        }

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.put(baseURI + '/services/V11.0/teams/' + Teamid,headers=header_param,data=updateTeambyTeamIdPayload)
        #print response appropriately
        construct('Admin','Agent','PUT /teams/',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def GetTeamAgent(fields,updatedSince):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v11.0/teams' + '?fields=' + fields + '&updatedSince=' + updatedSince,headers=header_param)
        #print response appropriately
        construct('Admin','Agent','GET /teams/agents',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def GetTeamByAgentID(teamId,fields):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get( baseURI + '/services/v11.0/teams/' + teamId +'/agents'+'?fields=' + fields,headers=header_param)
        #print response appropriately
        construct('Admin','Agent','GET /teams/{teamId}/agents',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')


# Admin --> General

def GetBrandingProfile(businessUnitId,fields):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v11.0/branding-profiles' + '?businessUnitId=' + businessUnitId + '&fields=' + fields,headers=header_param)
        #print response appropriately
        construct('Admin','General','GET /branding-profiles',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def GetDispostion(skip,top,searchString,fields,orderby,isPreviewDispositions,updatedSince):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v11.0/dispositions' + '?skip=' + skip + '&top=' + top+ '&searchString=' + searchString+'&fields='+ fields+ '&orderby='+ orderby+ '&isPreviewDispositions='+ isPreviewDispositions+ '&updatedSince='+ updatedSince,headers=header_param)
        #print response appropriately
        construct('Admin','General','GET /dispositions',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def DeleteFiles(fileName):
    #Check if accessToken is empty or null
    if accessToken!="":

        DeleteFiles={"fileName": fileName}

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.delete(baseURI + '/services/v11.0/files',headers=header_param,data=DeleteFiles)
        #print response appropriately
        construct('Admin','General','DELETE /files',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def PostCreateFileName(fileName,file,overwrite):
    #Check if accessToken is empty or null
    if accessToken!="":

        createpostFilePayload = {
        "fileName": fileName,
        "file": file,
        "overwrite": overwrite}

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.post(baseURI + '/services/v11.0/files',headers=header_param,data=createpostFilePayload)
        #print response appropriately
        construct('Admin','General','POST /Files',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def PutupdateFile(oldPath, newPath, overwrite ):
    #Check if accessToken is empty or null
    if accessToken!="":

        PutfilesPayload = {
        "oldPath":  oldPath,
        "newPath":  newPath,
        "overwrite":  overwrite}

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.put(baseURI + '/services/v11.0/files',headers=header_param,data=PutfilesPayload)
        #print response appropriately
        construct('Admin','General','PUT /files',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def GetFilesExternal(folderPath):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v11.0/files/external' + '?folderPath=' + folderPath,headers=header_param)
        #print response appropriately
        construct('Admin','General','GET files/external',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def PostCreateFileExternal(fileName,file,overwrite,needsProcessing):
    #Check if accessToken is empty or null
    if accessToken!="":

        CreateFileexternalPayload = {
        "fileName":  fileName,
        "file":  file ,
        "overwrite":  overwrite,
        "needsProcessing":  needsProcessing }

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.post(baseURI + '/services/v11.0/files/external',headers=header_param,data=CreateFileexternalPayload)
        #print response appropriately
        construct('Admin','General','POST /files/external',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def PutupdateFilesExternal(fileName,needsProcessing):
    #Check if accessToken is empty or null
    if accessToken!="":

        PutfilesPayload = {
        "fileName":  fileName,
        "needsProcessing":  needsProcessing}

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.put(baseURI + '/services/v11.0/files/external',headers=header_param,data=PutfilesPayload)
        #print response appropriately
        construct('Admin','General','PUT /files/external',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def DeleteFolders(folderName):
    #Check if accessToken is empty or null
    if accessToken!="":

        DeleteFolder={"folderName":  folderName}

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.delete(baseURI + '/services/v11.0/folders',headers=header_param,data=DeleteFolder)
        #print response appropriately
        construct('Admin','General','DELETE /folders',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def GetFolders(folderName):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get( baseURI + '/services/v11.0/folders' + '?folderName='+ folderName,headers=header_param)
        #print response appropriately
        construct('Admin','General','GET /folders',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

#Admin--> skills 

def GetDispostionByID(dispositionId,fields):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v11.0/dispositions/' + dispositionId + '? fields='+ fields,headers=header_param)
        #print response appropriately
        construct('Admin','Skills','GET /dispositions/{dispositionId}',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def PutUpdateDispostionbydispositionId(dispositionId,dispositionName,isPreviewDisposition,classificationId,isActive):
    #Check if accessToken is empty or null
    if accessToken!="":

        PutupdateDispostionbyIDpayload = {
        "dispositionName": dispositionName,
        "isPreviewDisposition": isPreviewDisposition,
        "classificationId": classificationId,
        "isActive": isActive}

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8',
        'Accept':'application/json, text/javascript, */*; q=0.01'}

        response=requests.put(baseURI + '/services/v11.0/dispositions/' + dispositionId,headers=header_param,data=PutupdateDispostionbyIDpayload)
        #print response appropriately
        construct('Admin','Skills','PUT /dispositions/{dispositionId}',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def GetDispostionByClassification(fields ):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v11.0/dispositions/classifications' + '? fields='+ fields,headers=header_param)
        #print response appropriately
        construct('Admin','Skills','GET /dispositions/classifications',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def PostcreateDispositions(dispositionvalue,isPreviewDisposition,classificationId):
    #Check if accessToken is empty or null
    if accessToken!="":

        CreateDispostionpayload ={
        "dispositions": [
        {
        "dispositionName":  dispositionvalue,
        "isPreviewDisposition": isPreviewDisposition ,
        "classificationId": + classificationId}]}

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8',
        'content-Type': 'application/x-www-form-urlencoded'}

        response=requests.get(baseURI + '/services/v11.0/dispositions',headers=header_param,data=CreateDispostionpayload)
        #print response appropriately
        construct('Admin','Skills','Post /dispositions',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def PostCreateSkill():
    #Check if accessToken is empty or null
    if accessToken!="":

        CreateSkillPayload = {
        "skills": [
        {
        "mediaTypeId": 4,
        "skillName": "",
        "isOutbound": "true",
        "outboundStrategy": "Personal Connection",
        "campaignId": 1,
        "callerIdOverride": "8015554422",
        "emailFromAddress": "test@test.com",
        "emailFromEditable": "false",
        "emailBccAddress": "",
        "scriptId": 2,
        "reskillHours": 4,
        "minWFIAgents": 4,
        "interruptible": "false",
        "enableParking": "false",
        "minWorkingTime": 4,
        "agentless": "false",
        "agentlessPorts": 6,
        "notes": "this is a test note",
        "acwTypeId": 3,
        "requireDisposition": "false",
        "allowSecondaryDisposition": "false",
        "scriptDisposition": "false",
        "stateIdACW": 2,
        "maxSecondsACW": 3,
        "acwPostTimeoutStateId": 53,
        "agentRestTime": 4,
        "displayThankyou": "false",
        "thankYouLink": "no",
        "popThankYou": "true",
        "popThankYouURL": "tester.com",
        "makeTranscriptAvailable": "true",
        "transcriptFromAddress": "fromMe@email.com",
        "priorityBlending": "false",
        "callSuppressionScriptId": 4,
        "useScreenPops": "true",
        "screenPopTriggerEvent": "popTriggerEvent",
        "useCustomScreenPops": "false",
        "screenPopType": "webpage",
        "screenPopDetails": "http://not",
        "initialPriority": 4,
        "acceleration": 5,
        "maxPriority": 10,
        "serviceLevelThreshold": 51,
        "serviceLevelGoal": 24,
        "enableShortAbandon": "true",
        "shortAbandonThreshold": 123,
        "countShortAbandons": "true",
        "countOtherAbandons": "true",
        "chatWarningTheshold": 0,
        "agentTypingIndicator": "false",
        "patronTypingPreview": "false",
        "smsTransportCodeId": 'null',
        "messageTemplateId": 'null',
        "deliverMultipleNumbersSerially": "false",
        "cradleToGrave": "false",
        "priorityInterrupt": "false",
        "treatProgressAsRinging": "false",
        "preConnectCPAEnabled": "false",
        "agentOverrideFax": "true",
        "agentOverrideAnsweringMachine": "true",
        "agentOverrideBadNumber": "true",
        "dispositions": [
        {
        "dispositionId": 1,
        "priority": 1}]
        }]}

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.post( baseURI + '/services/v11.0/skills',headers=header_param,data=CreateSkillPayload)
        #print response appropriately
        construct('Admin','General','POST /Skills',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def PutSkillbySkillID(skillId):
    #Check if accessToken is empty or null
    if accessToken!="":

        PutSkillbySkillIDpayload = {
        "skill": {
        "skillName": "string",
        "isActive": "true",
        "campaignId": 2,
        "callerIdOverride": "string",
        "emailFromAddress": "test@test.com",
        "emailFromEditable": "false",
        "emailBccAddress": "",
        "scriptId": 2,
        "reskillHours": 4,
        "minWFIAgents": 4,
        "interruptible": "false",
        "enableParking": "false",
        "minWorkingTime": 4,
        "agentless": "false",
        "agentlessPorts": 6,
        "notes": "this is a test note",
        "acwTypeId": 3,
        "requireDisposition": "false",
        "allowSecondaryDisposition": "false",
        "scriptDisposition": "false",
        "stateIdACW": 2,
        "maxSecondsACW": 3,
        "acwPostTimeoutStateId": 53,
        "agentRestTime": 4,
        "displayThankyou": "false",
        "thankYouLink": "no",
        "popThankYou": "true",
        "popThankYouURL": "tester.com",
        "makeTranscriptAvailable": "true",
        "transcriptFromAddress": "fromMe@email.com",
        "priorityBlending": "false",
        "callSuppressionScriptId": 4,
        "useScreenPops": "''true''",
        "screenPopTriggerEvent": "bleh",
        "useCustomScreenPops": "false",
        "screenPopType": "webpage",
        "screenPopDetails": "http://no",
        "initialPriority": 4,
        "acceleration": 5,
        "maxPriority": 10,
        "serviceLevelThreshold": 51,
        "serviceLevelGoal": 24,
        "enableShortAbandon": "''true''",
        "shortAbandonThreshold": 123,
        "countShortAbandons": "true",
        "countOtherAbandons": "true",
        "chatWarningTheshold": 0,
        "agentTypingIndicator": "false",
        "patronTypingPreview": "false",
        "smsTransportCodeId": 'null',
        "messageTemplateId": 'null',
        "deliverMultipleNumbersSerially": "false",
        "cradleToGrave": "false",
        "priorityInterrupt": "false",
        "treatProgressAsRinging": "false",
        "preConnectCPAEnabled": "false",
        "agentOverrideFax": "true",
        "agentOverrideAnsweringMachine": "true",
        "agentOverrideBadNumber": "true",
        "dispositions": [
        {
        "dispositionId": 1,
        "priority": 1}]
        }
        }

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8',
        'Accept':'application/json, text/javascript, */*; q=0.01'}

        response=requests.put(baseURI + '/services/v11.0/skills/' +skillId,headers=header_param,data=PutSkillbySkillIDpayload)
        #print response appropriately
        construct('Admin','Skills','PUT /skills/{skillId}',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def GetDispostionBySkillid(skillId,searchString,fields,skip,top,orderby ):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v11.0/skills/' + skillId + '/dispositions'+
        '?searchString='+ searchString+ '&fields='+ fields+ '&skip='+ skip+ '&top='+ top+ '&orderby='+ orderby,headers=header_param)
        #print response appropriately
        construct('Admin','Skills','GET /skills/{skillId}/dispositions',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def GetDispostionBySkillidUnAssigned(skillId,searchString,fields,skip,top,orderby ):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v11.0/skills/' + skillId + '/dispositions/unassigned' +
        '?searchString=' + searchString + '&fields=' + fields + '&skip=' + skip + '&top=' + top + '&orderby=' + orderby,headers=header_param )
        #print response appropriately
        construct('Admin','Skills','GET /skills/{skillId}/dispositions/unassigned',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def GetSkillParameterGeneralSetting(skillId,fields ):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v11.0/skills/'+ skillId+ '/parameters/general-settings'+ '?fields='+ fields,headers=header_param)
        #print response appropriately
        construct('Admin','Skills','GET /skills/{skillId}/parameters/general-settings',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def PutSkillParameterGeneralSetting(skillId):
    #Check if accessToken is empty or null
    if accessToken!="":

        SkillGeneralSettingPayload = {
        "generalSettings": {
        "minimumRetryMinutes": 12,
        "maximumAttempts": 10,
        "defaultContactExpiration": 10,
        "getPriorityContactsOnContactinsertion": "false",
        "loadCallbacks": "false",
        "loadFresh": "false",
        "loadNonFresh": "false",
        "overrideBusinessUnitAbandonRate": "false",
        "maximumRingingDuration": 1,
        "beginDampenPercentage": 1,
        "abandonRateCutoff": 1,
        "abandonRateThreshold": 1,
        "inactiveBlenderTimer": 1,
        "maximumRatio": 1,
        "aggressiveness": "conservative",
        "endOfListNotificationsDelay": 15,
        "notifyAgentsWhenListIsEmpty": "false",
        "percentageOfAgentsBeforeOverdial": 5,
        "blockMultipleCalls": "''true''",
        "consecutiveAttemptsWithoutALiveConnect": 5
        }}

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8',
        'Accept':'application/json, text/javascript, */*; q=0.01'}

        response=requests.put(baseURI + '/services/v11.0/skills/' + skillId + '/parameters/general-settings',headers=header_param,data=SkillGeneralSettingPayload)
        #print response appropriately
        construct('Admin','Skills','PUT /skills/{skillId}/parameters/general-settings',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

# Admin--> Contacts

def GetcontactbyContactID(contactId,fields ):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v11.0/contacts/' + contactId + '/files' + '?fields='+ fields,headers=header_param)
        #print response appropriately
        construct('Admin','Contacts','GET /contacts/{contactId}/files',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

# Admin--> List

def GetListCalllist(fields,top,skip,orderBy,startDate,endDate ):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v11.0/lists/call-lists/jobs'+
        '?fields='+ fields+ '&top='+ top+'&skip='+ skip+ '&orderBy='+ orderBy+ '&startDate='+ startDate+ '&endDate='+ endDate,headers=header_param)
        #print response appropriately
        construct('Admin','LIST','GET /lists/call-lists/jobs',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def PostCallListbyListID(listId,skillId,fileName,forceOverwrite,defaultTimeZone,expirationDate,listFile,startSkill):
    #Check if accessToken is empty or null
    if accessToken!="":

        PostCallListbyListIDpayload ={
        "skillId": skillId,
        "fileName": fileName,
        "forceOverwrite": forceOverwrite,
        "defaultTimeZone": defaultTimeZone,
        "expirationDate": expirationDate,
        "listFile": listFile,
        "startSkill": startSkill}

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.post(baseURI + '/services/v11.0/lists/call-lists/' + listId + '/upload',headers=header_param,data=PostCallListbyListIDpayload)
        #print response appropriately
        construct('Admin','LIST','POST /lists/call-lists/{listId}/upload',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def GetCallingListbyJobID(jobId):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.delete(baseURI + '/services/v11.0/lists/call-lists/jobs/' +jobId,headers=header_param)
        #print response appropriately
        construct('Admin','LIST','DELETE /lists/call-lists/jobs/{jobId}',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def GetCallingListbyJobID(jobId,fields):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v11.0/lists/call-lists/jobs/' + jobId + '?fields=' + fields,headers=header_param)
        #print response appropriately
        construct('Admin','LIST','GET /lists/call-lists/jobs/{jobId}',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

#Admin --> Groups

def GetGroups(top,skip,orderby,searchString,isActive,fields):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v9.0/groups' + '?top=' + top + '&skip=' + skip+ 
        '&orderby=' + orderby + '&searchString=' + searchString + '&isActive='+ isActive + '&fields=' + fields,headers=header_param)
        #print response appropriately
        construct('Admin','Groups','GET /groups',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def PostGroups(groupName, isActive, notes):
    #Check if accessToken is empty or null
    if accessToken!="":

        updateGroupPayload ={
        "groups": [
        {
        "groupName": groupName,
        "isActive": isActive,
        "notes": notes
        }]}

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.post(baseURI + '/services/v11.0/groups',headers=header_param,data=updateGroupPayload)
        #print response appropriately
        construct('Admin','Groups','POST /groups',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def GetGroupsByGroupID(groupId, fields):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v11.0/groups/' + groupId + '?fields='+ fields,headers=header_param)
        #print response appropriately
        construct('Admin','Groups','GET /groups/{groupId}',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def PutGroupsByGroupID(groupId,groupName,isActive,notes):
    #Check if accessToken is empty or null
    if accessToken!="":

        PostGroupbyGroupIDPayload = {
        "groupName": groupName,
        "isActive": isActive,
        "notes": notes}

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.put(baseURI + '/services/v11.0/groups/' + groupId,headers=header_param,data=PostGroupbyGroupIDPayload)
        #print response appropriately
        construct('Admin','Groups','POST /groups/{groupId}',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def DeleteGroupsByAgentGroupID(groupId, agentId):
    #Check if accessToken is empty or null
    if accessToken!="":

        DeleteGroupbyGroupIDagentPayload = {"agents": [{"agentId": agentId} ] }

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.delete(baseURI + '/services/v11.0/groups/'+ groupId +'/agents',headers=header_param,data=DeleteGroupbyGroupIDagentPayload)
        #print response appropriately
        construct('Admin','Groups','DELETE /groups/{groupId}/agents',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def GetGroupsByAgentGroupID(groupId,top,skip,orderBy,searchString,assigned,fields):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/{version}/groups/' + groupId + '/agents' +'?top=' + top + '&skip=' + skip + '&orderby=' + orderBy + '&searchString=' + searchString + '&assigned=' + assigned + '&fields=' + fields,headers=header_param)
        #print response appropriately
        construct('Admin','Groups','GET /groups/{groupId}/agents',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def PostGroupsByAgentGroupID(groupId,agentId):
    #Check if accessToken is empty or null
    if accessToken!="":

        PostGroupbyGroupIDagentPayload = { "agents": [ { "agentId": 1 } ] }

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.post(baseURI + '/services/v11.0/groups/'+ groupId +'/agents',headers=header_param,data=PostGroupbyGroupIDagentPayload)
        #print response appropriately
        construct('Admin','Groups','POST /groups/{groupId}/agents',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

#Agent --> Chat Request

def postSessionIdInteractionsContactIdtyping(sessionId, contactId,isTyping,isTextEntered):
    #Check if accessToken is empty or null
    if accessToken!="":

        PostChatTypingPayload = {
        "isTyping": isTyping,
        "isTextEntered": isTextEntered}

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.post(baseURI + '/services/v11.0/agent-sessions/' + sessionId + '/interactions/'+ contactId +'/typing',headers=header_param,data=PostChatTypingPayload)
        #print response appropriately
        construct('Agents','Chat','POST /agent-sessions/{sessionId}/interactions/{contactId}/typing',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def posttAgentSessionSessionIdInteractionAddEmail(sessionId):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.post(baseURI + '/services/v11.0/agent-sessions/'+ sessionId + '/interactions/add-email',headers=header_param)
        #print response appropriately
        construct('Agents','Email','POST /agent-sessions/{sessionId}/interactions/add-email',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def posttAgentSessionIdInteractionConatactIdParkemail(sessionId,ConatctId,toAddress="",fromAddress="",ccAddress="",bccAddress="",subject="", bodyHtml="",attachments="",attachmentNames="",isDraft="false",originalAttachmentNames=""):
    #Check if accessToken is empty or null
    if accessToken!="":

        PostEmailParkPayload = {
        "toAddress": toAddress,
        "fromAddress": fromAddress,
        "ccAddress": ccAddress,
        "bccAddress": bccAddress,
        "subject": subject,
        "bodyHtml": bodyHtml,
        "attachments": attachments,
                "attachmentNames": attachmentNames,
        "isDraft": isDraft,
        "originalAttachmentNames": originalAttachmentNames}

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.post(baseURI + '/services/v11.0/agent-sessions/' + sessionId + '/interactions/' + ConatctId+ '/email-park',headers=header_param,data=PostEmailParkPayload)
        #print response appropriately
        construct('Agents','Email','POST /agent-sessions/{sessionId}/interactions/{contactId}/email-park',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def posttAgentSessionIdInteractionConatactIdUnparkemail( sessionId, ConatctId, isImmediate="false"):
    #Check if accessToken is empty or null
    if accessToken!="":

        PostEmailunParkPayload = { "isImmediate": isImmediate }
    
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.post(baseURI + '/services/v11.0/agent-sessions/' + sessionId + '/interactions/' + ConatctId+ '/email-unpark',headers=header_param,data=PostEmailunParkPayload)
        #print response appropriately
        construct('Agents','Email','POST /agent-sessions/{sessionId}/interactions/{contactId}/email-unpark',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def posttAgentSessionIdInteractionConatactIdPreview( sessionId, ConatctId):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.post(baseURI + '/services/v11.0/agent-sessions/' + sessionId + '/interactions/' + ConatctId+ '/email-preview',headers=header_param)
        #print response appropriately
        construct('Agents','Email','POST /agent-sessions/{sessionId}/interactions/{contactId}/email-preview',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def posttAgentSessionIdInteractionConatactIdEmailRestore( sessionId, ConatctId):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.post(baseURI + '/services/v11.0/agent-sessions/' + sessionId + '/interactions/' + ConatctId+ '/email-restore',headers=header_param)
        #print response appropriately
        construct('Agents','Email','POST /agent-sessions/{sessionId}/interactions/{contactId}/email-restore',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

# Agent Personal Connection

def posttAgentSessionIdInteractionConatactIdSnooze( sessionId, ConatctId):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.post(baseURI + '/services/v11.0/agent-sessions/' + sessionId + '/interactions/' + ConatctId+ '/snooze',headers=header_param)
        #print response appropriately
        construct('Agents','Callbacks','POST /agent-sessions/{sessionId}/interactions/{contactId}/snooze',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

# Agent --> ScheduledCallbacks

def DialCallback( sessionId, callbackId):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.post(baseURI + '/services/v11.0/agent-sessions/' + sessionId + '/interactions/'+ callbackId + '/dial',headers=header_param)
        #print response appropriately
        construct('Agents','Callbacks','POST /agent-sessions/{sessionId}/interactions/{callbackId}/dial',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def RescheduleCallback( sessionId, callbackId,rescheduleDate):
    #Check if accessToken is empty or null
    if accessToken!="":

        PostRescheduleCallbackPayload = {"rescheduleDate" :  rescheduleDate }

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.post(baseURI + '/services/v11.0/agent-sessions/'+ sessionId +'/interactions/' + callbackId + '/reschedule',headers=header_param,data=PostRescheduleCallbackPayload)
        #print response appropriately
        construct('Agents','Callbacks','POST /agent-sessions/{sessionId}/interactions/{callbackId}/reschedule',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def CancelCallback( sessionId, callbackId,notes):
    #Check if accessToken is empty or null
    if accessToken!="":

        PostCancelCallbackPayload = {"notes" :  notes }

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.post(baseURI + '/services/v11.0/agent-sessions/'+ sessionId +'/interactions/' + callbackId + '/cancel',headers=header_param,data=PostCancelCallbackPayload)
        #print response appropriately
        construct('Agents','Callbacks','POST /agent-sessions/{sessionid}/interactions/{callbackid}/cancel',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

# Admin Agent Session

def posttAgentSessionIdAddcontact(sessionId, chat="''true''", email="''true''", workitem="''true''"):
    #Check if accessToken is empty or null
    if accessToken!="":

        PostAddcontacPayload = {
        "chat": chat,
        "email": email,
        "workitem": workitem}

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.post(baseURI + '/services/V11.0/agent-sessions/' + sessionId + '/add-contact',headers=header_param,data=PostAddcontacPayload)
        #print response appropriately
        construct('Agents','Sessions','POST /agent-sessions/{sessionId}/add-contact',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')


def posttAgentSessionIdInteractionConatactIdActivate(sessionId, ConatctId):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.post(baseURI + '/services/v11.0/agent-sessions/' + sessionId + '/interactions/' + ConatctId + '/activate',headers=header_param)
        #print response appropriately
        construct('Agents','Sessions','POST /agent-sessions/{sessionId}/interactions/{contactId}/activate',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def GetSmstranscripts( startDate, endDate, transportCode , agentId="", top="",skip=""):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v11.0/contacts/sms-transcripts' + '?top=' + top + '&skip=' + skip + '&startDate=' + startDate + '&endDate='+ endDate + '&transportCode=' + transportCode,headers=header_param)
        #print response appropriately
        construct('Reports','Reports','GET /contacts/sms-transcripts',cluster + ' v11.0',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def GetSmstranscriptsbyContactid( contactId,startDate, endDate, transportCode, top="",skip=""):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v10.0/contacts/' + contactId + '/sms-transcripts' + '?top=' + top + '&skip=' + skip + '&startDate=' + startDate + '&endDate='+ endDate + '&transportCode=' + transportCode,headers=header_param)
        #print response appropriately
        construct('Reports','Reports','GET /contacts/{contactid}/sms-transcripts',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def GetSmsCallQuality( contactId):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v11.0/contacts/' + contactId + '/call-quality',headers=header_param)
        #print response appropriately
        construct('Reports','Reports','GET /contacts/{contactId}/call-quality',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def GetTeamPerformanceTotal( startDate, endDate,fields = ""):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v11.0/teams/performance-total' + '?startDate=' + startDate + '&endDate=' + endDate + '&fields=' + fields,headers=header_param)
        #print response appropriately
        construct('Reports','Reports','GET /teams/performance-total',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def GetTeamPerformancebyTeamIDTotal( teamId, startDate, endDate,fields = ""):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v3.0/teams/' + teamId + '/performance-total' + '?startDate=' + startDate + '&endDate=' + endDate + '&fields=' + fields,headers=header_param)
        #print response appropriately
        construct('Reports','Reports','GET /teams/{teamId}/performance-total',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def getReportJobById(jobId,fields=""):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + 'services/v11.0/report-jobs/' + jobId + "?fields=" +fields,headers=header_param)
        #print response appropriately
        construct('Reports','Reports','GET /report-jobs/{jobId}',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def GetdatadownloadbyReportID( reportId,fileName, startDate, endDate, saveAsFile ,includeHeaders):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v10.0/report-jobs/datadownload/'+ reportId +
        '?startDate=' + startDate + '&endDate=' + endDate + '&saveAsFile=' + saveAsFile + 
        '&includeHeaders=' + includeHeaders,headers=header_param)
        #print response appropriately
        construct('Reports','Reports','GET /report-jobs/datadownload/{reportid}',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def Getwfoascm(startDate,endDate,fields="" ):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v11.0/wfo-data/ascm' + '?startDate=' + startDate + '&endDate=' + endDate + '&fields=' + fields,headers=header_param)
        #print response appropriately
        construct('Reports','Reports','GET /wfo-data/ascm',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def Getwfoasi(startDate,endDate,fields="" ):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v11.0/wfo-data/asi' + '?startDate=' + startDate + '&endDate=' + endDate + '&fields=' + fields,headers=header_param)
        #print response appropriately
        construct('Reports','Reports','GET /wfo-data/asi',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def Getwfocsi(startDate,endDate,fields="" ):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v11.0/wfo-data/csi' + '?startDate=' + startDate + '&endDate=' + endDate + '&fields=' + fields,headers=header_param)
        #print response appropriately
        construct('Reports','Reports','GET /wfo-data/csi',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def Getwfoftci(startDate,endDate,fields="" ):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v11.0/wfo-data/ftci' + '?startDate=' + startDate + '&endDate=' + endDate + '&fields=' + fields,headers=header_param)
        #print response appropriately
        construct('Reports','Reports','GET /wfo-data/ftci',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def Getwfoosi(startDate,endDate,fields="" ):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v11.0/wfo-data/osi' + '?startDate=' + startDate + '&endDate=' + endDate + '&fields=' + fields,headers=header_param)
        #print response appropriately
        construct('Reports','Reports','GET /wfo-data/osi',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def Getwfoqm(startDate,endDate,fields="" ):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v5.0/wfo-data/qm'+ '?startDate=' + startDate + '&endDate=' + endDate + '&fields=' + fields,headers=header_param)
        #print response appropriately
        construct('Reports','Reports','GET /wfo-data/qm',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

# Report --> WFM

def GetwfmSkillContacts(startDate,endDate,mediaTypeId,fields="" ):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v11.0/wfm-data/skills/contacts'+ '?startDate=' + startDate + '&endDate=' + endDate + '&fields=' + fields + '&mediaTypeId=' + mediaTypeId,headers=header_param)
        #print response appropriately
        construct('Reports','WFM','GET /wfm-data/skills/contact',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def Getwfmagetns(startDate,endDate,fields="" ):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v11.0/wfm-data/agents'+  '?startDate=' + startDate + '&endDate=' + endDate + '&fields=' + fields,headers=header_param)
        #print response appropriately
        construct('Reports','WFM','GET /wfm-data/agents',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def GetwfmdailerContacts(startDate,endDate,fields="" ):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v11.0/wfm-data/skills/dialer-contacts'+  '?startDate=' + startDate + '&endDate=' + endDate + '&fields=' + fields,headers=header_param)
        #print response appropriately
        construct('Reports','WFM','GET /wfm-data/skills/dialer-contacts',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def Getwfmscheduleadherence(startDate,endDate,fields="" ):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v9.0/wfm-data/agents/schedule-adherence' +  '?startDate=' + startDate + '&endDate=' + endDate + '&fields=' + fields,headers=header_param)
        #print response appropriately
        construct('Reports','WFM','GET /wfm-data/agents/schedule-adherence',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def GetwfmAgentscorecard(startDate,endDate,fields="" ):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v11.0/wfm-data/agents/scorecards' +  '?startDate=' + startDate + '&endDate=' + endDate + '&fields=' + fields,headers=header_param)
        #print response appropriately
        construct('Reports','WFM','GET /wfm-data/agents/scorecard',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def GetwfmAgentperformance(startDate,endDate,fields="" ):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v9.0/wfm-data/skills/agent-performance' +  '?startDate=' + startDate + '&endDate=' + endDate + '&fields=' + fields,headers=header_param)
        #print response appropriately
        construct('Reports','WFM','GET /wfm-data/skills/agent-performance',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def  postContactsChatSessionTyping(chatSession,isTyping="",isTextEntered="",label="testLabel"):
    #Check if accessToken is empty or null
    if accessToken!="":

        CreateFileexternalPayload = {
        "isTyping":  isTyping,
        "isTextEntered":  isTextEntered ,
        "label":  label}

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.post(baseURI + '/services/v8.0/contacts/chats/'+chatSession+'/typing',headers=header_param,data=CreateFileexternalPayload)
        #print response appropriately
        construct('Patron','chatRequest','POST /contacts/chats/{chatSession}/typing',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def postContactsChatSessionTypingPreview(chatSession,previewText = "This text is 100% awesome!!",label = "testLabel"):
    #Check if accessToken is empty or null
    if accessToken!="":
    #Give necessary parameters for http request
        CreateFileexternalPayload = {
        "previewText":  previewText,
        "label":  label}

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.post(baseURI + '/services/v8.0/contacts/chats/'+chatSession+'/typing-preview',headers=header_param,data=CreateFileexternalPayload)
        #print response appropriately
        construct('Patron','chatRequest','POST contacts/chats/{chatSession}/typing-preview',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def getChatprofileByPOCId(pointOfContactId):

    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v11.0/points-of-contact/'+pointOfContactId+'/chat-profile',headers=header_param)
        #print response appropriately
        construct('Patron','chatRequest','GET points-of-contact/{pointOfContactId}/chat-profile',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def getThankYouForSkillId(SkillId):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.get(baseURI + '/services/v12.0/skills/'+SkillId+'/thank-you-page',headers=header_param)
        #print response appropriately
        construct('Admin','Skill','Get /skills/{killId}/thank-you-page',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def postContactChatSendEmail(fromAddress,toAddress,emailBody):
    
    #Check if accessToken is empty or null
    if accessToken!="":

    #Give necessary parameters for http request
        CreateChatSendEmailpayload ={    
        "fromAddress": fromAddress,
        "toAddress": toAddress ,
        "emailBody": emailBody  }

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.post(baseURI + '/services/v12.0/contacts/chats/send-email',headers=header_param,data=CreateChatSendEmailpayload)
    #print response appropriately
        construct('Patron','chatRequest','Post /contacts/chats/send-email',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
    #no token get one or handle error
        construct('error')

def getAddressBooks():
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/address-books' , headers = header_param) 
 
#print response appropriately
        construct('Admin','AddressBook','get /address books',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def createAddressBook():
#Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make post http request
        response = requests.post(baseURI + 'services/v11.0/address-books' , headers = header_param) 
 
#print response appropriately
        construct('Admin','AddressBook','post /address books',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def deleteAddressBook(addressBookId):
#Check if accessToken is empty or null
    if accessToken!="":
 
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make delete http request
        response = requests.delete(baseURI + 'services/v11.0/address-books/' + addressBookId , headers = header_param) 
 
#print response appropriately
        construct('Admin','AddressBook','DELETE /address-books/{addressBookId}',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def assignAddressBook(addressBookId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'entityType': 'string - required - Agent, Skill, Team, Campaign or Everyone',
        'addressBookAssignments': [
        {'entityId': 'integer - required'}]}
 

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/address-books/' + addressBookId + '/assignment', headers = header_param, data=payload) 
 
#print response appropriately
        construct('Admin','AddressBook','POST /address-books/{addressBookId}/assignment',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getDynamicAddressBookEntries(addressBookId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'top': 'integer',
        'skip': 'integer',
        'orderBy': 'string',
        'fullLoad': 'boolean - required - ''true'' or false',
        'updatedSince': 'ISO 8601 formatted date/time'}
 

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/address-books/' + addressBookId + '/dynamic-entries' , headers = header_param, params=payload) 
 
#print response appropriately
        construct('Admin','AddressBook','GET /address-books/{addressBookId}/dynamic-entries',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def addDynamicAddressBookEntries(addressBookId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'fullLoad': 'boolean - required - ''true'' or false',
        'addressBookEntries': [
        {
        'externalId': 'integer - required',
        'stateId': 'integer',
        'externalState': 'string',
        'firstName': 'string',
        'middleName': 'string',
        'lastName': 'string',
        'company': 'string',
        'phone': 'integer',
        'mobile': 'integer',
        'email': 'string'},
        {
        'externalId': 'integer - required',
        'stateId': 'integer',
        'externalState': 'string',
        'firstName': 'string',
        'middleName': 'string',
        'lastName': 'string',
        'company': 'string',
        'phone': 'integer',
        'mobile': 'integer',
        'email': 'string'}]}
 

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http put request
        response = requests.put(baseURI +  'services/v11.0/address-books/' + addressBookId + '/dynamic-entries' , headers = header_param, data=payload) 
 
#print response appropriately
        construct('Admin','AddressBook','PUT /address-books/{addressBookId}/dynamic-entries',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def deleteDynamicAddressBookEntry(addressBookId,externalId):
#Check if accessToken is empty or null
    if accessToken!="":


        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http delete request
        response =requests.delete(baseURI+'services/v4.0/address-books/'+addressBookId+'/dynamic-entries/'+externalId,headers =header_param)
#print response appropriately
        construct('Admin','AddressBook','DELETE /address-books/{addressBookId}/dynamic-entries/{externalId}',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def getStandardAddressBookEntries(addressBookId,updatedSince='',searchString='',fields='',skip='',top='',orderBy=''):
#Check if accessToken is empty or null
    if accessToken!="":
 

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + '/services/v11.0/address-books/' + addressBookId + '/entries' + 
        '?fields=' + fields + '&updatedSince=' + updatedSince + '&searchString=' + searchString + 
        '&skip' + skip + '&top=' + top + '&orderBy=' + orderBy  , headers = header_param) 
 
#print response appropriately
        construct('Admin','AddressBook','GET /address-books/{addressBookId}/entries',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def createStandardAddressBookEntries(addressBookID):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'addressBookEntries': [
        {
        'firstName': 'string',
        'middleName': 'string',
        'lastName': 'string',
        'company': 'string',
        'phone': 'integer',
        'mobile': 'integer',
        'email': 'string'
        },
        {
        'firstName': 'string',
        'middleName': 'string',
        'lastName': 'string',
        'company': 'string',
        'phone': 'integer',
        'mobile': 'integer',
        'email': 'string'
        }
        ]}
 

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
# Make http post request
 
        response = requests.post(baseURI +  'services/v3.0/address-books/' + addressBookID  + '/entries' , headers = header_param, data=payload)
#print response appropriately
 
        construct('Admin','AddressBook','POST /address-books/{addressBookId}/entries',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def updateStandardAddressBookEntries(addressBookId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'addressBookEntries': [
        {
        'addressBookEntryId': 'integer - required',
        'firstName': 'string',
        'middleName': 'string',
        'lastName': 'string',
        'company': 'string',
        'phone': 'integer',
        'mobile': 'integer',
        'email': 'string'
        },
        {
        'addressBookEntryId': 'integer - required',
        'firstName': 'string',
        'middleName': 'string',
        'lastName': 'string',
        'company': 'string',
        'phone': 'integer',
        'mobile': 'integer',
        'email': 'string'
        }
        ]}
 

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http put request
        response = requests.put(baseURI +  'services/v3.0/address-books/' + addressBookId + '/entries', headers = header_param, data=payload)
 
#print response appropriately
        construct('Admin','AddressBook','PUT /address-books/{addressBookId}/entries',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def deleteAddressBookEntry(addressBookId,entryId):
#Check if accessToken is empty or null
    if accessToken!="":
 

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http delete request
        response = requests.delete(baseURI + 'services/v3.0/address-books/' + addressBookId + '/entries/' + entryId , headers = header_param)
 
#print response appropriately
        construct('Admin','AddressBook','DELETE /address-books/{addressBookId}/entries/{addressBookEntryId}',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def getAgentAddressBooks(agentId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'includeEntries':'boolean - ''true'' or false',
        'updatedSince': 'ISO 8601 formatted date/time'}
 

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/agents/' + agentId + '/address-books' , headers = header_param, params=payload)
 
#print response appropriately
        construct('Admin','AddressBook','GET /agents/{agentId}/address-books',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getCampaignAddressBooks(campaignId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'includeEntries': 'boolean - ''true'' or false',
        'updatedSince': 'ISO 8601 formatted date/time'}
 

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/campaigns/' + campaignId + '/address-books' , headers = header_param, params=payload) 
 
#print response appropriately
        construct('Admin','AddressBook','GET /campaigns/{campaignId}/address-books',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getTeamAddressBooks(teamId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'includeEntries': 'boolean - ''true'' or false',
        'updatedSince': 'ISO 8601 formatted date/time'}

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/teams/' + teamId + '/address-books' , headers = header_param, params=payload) 
 
#print response appropriately
        construct('Admin','AddressBook','GET /teams/{teamId}/address-books',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getAgents():
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'updatedSince': '8601 formatted date/time',
        'isActive': 'boolean',
        'searchString': 'string',
        'fields': 'string',
        'skip': 'string',
        'top': 'string',
        'orderBy': 'string'}
 

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/agents' , headers = header_param, params=payload) 
 
#print response appropriately
        construct('Admin','Agents','GET /agents',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def changeAgentState(agentId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'state': 'string - "Available" or "Unavailable"',
        'outStateId': 'integer'}
 

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http post request
        response = requests.post(baseURI + 'services/v11.0/agents/' + agentId + '/state' , headers = header_param, data=payload) 
 
#print response appropriately
        construct('Admin','Agents','POST /agents/{agentId}/state',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def getAllAgentsSkills():
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'updatedSince': 'string - ISO 8601 formatted date/time',
        'searchString': 'string',
        'fields': 'string',
        'skip': 'integer',
        'top': 'integer',
        'orderBy': 'string'}
 

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
# Make get http request
        response = requests.get(baseURI +  'services/v11.0/agents/skills' , headers = header_param, params=payload) 
 
#print response appropriately
        construct('Admin','Agents','GET /agents/skills',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def deleteSkillsFromAgent(agentId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'skills': [
        {
        'skillId': 'integer - required'}]}
 

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http delete request
        response = requests.delete(baseURI + 'services/v11.0/agents/' + agentId + '/skills' , headers = header_param,data=payload)
 
#print response appropriately
        construct('Admin','Agents','DELETE /agents/{agentId}/skills',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def assignSkillsToAgent(agentId):
    #Check if accessToken is empty or null
    if accessToken!="":
        #Give necessary parameters for http request
        payload = {'assignSkillsToAgentPayload' :{
        'skills': [{'skillId': 'integer - required','isActive': 'boolean','proficiency': 'integer - 1-Highest, 2-High, 3-Medium, 4-Low, 5-Lowest'}]
        }}

        header_param = {'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/x-www-form-urlencoded'}

        # Make http post request
        response = requests.post(baseURI + 'services/v11.0/agents/' + agentId + '/skills', headers = header_param, data=payload) 
 
#print response appropriately
        construct('Admin','Agents','POST /agents/{agentId}/skills',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def modifySkillsForAgent(agentId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload = {'skills': [
        {
        'skillId': 'integer - required',
        'isActive': 'boolean',
        'proficiency': 'integer - 1-Highest, 2-High, 3-Medium, 4-Low, 5-Lowest'}]}
 

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http put request
        response = requests.put(baseURI +  'services/v11.0/agents/' + agentId+ '/skills' , headers = header_param, data=payload) 
 
#print response appropriately
        construct('Admin','Agents','PUT /agents/{agentId}/skills',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getAgentUnassignedSkills(agentId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'fields': 'string',
        'mediaTypeId': 'integer',
        'outboundStrategy': 'string',
        'isSkillActive': 'boolean',
        'searchString': 'string',
        'skip': 'integer',
        'top': 'integer',
        'orderBy': 'string'}
 

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/agents/' + agentId + '/skills/unassigned' , headers = header_param, params=payload) 
 
#print response appropriately
        construct('Admin','Agents','GET /agents/{agentId}/skills/unassigned',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getAgentsContactsBySkill():
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'startDate': 'ISO 8601 formatted date/time',
        'endDate': 'ISO 8601 formatted date/time'}
 

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI +'services/v11.0/agents/skill-data' , headers = header_param, params=payload) 
 
#print response appropriately
        construct('Admin','Agents','GET /agents/skill-data',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getAgentContactsBySkill(agentId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'startDate': 'ISO 8601 formatted date/time',
        'endDate': 'ISO 8601 formatted date/time'}
 

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/agents/' + agentId + '/skill-data' , headers = header_param, params=payload) 
 
#print response appropriately
        construct('Admin','Agents','GET /agents/{agentId}/skill-data',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def createCustomEvent(agentId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'eventName': 'string',
        'persistInMemory': 'boolean',
        'data': 'string'}
 

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http put request
        response = requests.put(baseURI +  'services/v11.0/agents/' + agentId + '/custom-event' , headers = header_param, data=payload) 
 
#print response appropriately
        construct('Admin','Agents','GET /agents/{agentId}/skill-data',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getQuickReplies():
#Check if accessToken is empty or null
    if accessToken!="":
 

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/agents/quick-replies' , headers = header_param) 
 
#print response appropriately
        construct('Admin','Agents','GET /agents/quick-replies',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getAgentQuickReplies(agentId):
#Check if accessToken is empty or null
    if accessToken!="":
 

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/agents/' + agentId + '/quick-replies' , headers = header_param) 
 
#print response appropriately
        construct('Admin','Agents','GET /agents/{agentId}/quick-replies',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def getAgentMessages(agentId):
#Check if accessToken is empty or null
    if accessToken!="":
 

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/agents/' + agentId + '/messages' , headers = header_param) 
 
#print response appropriately
        construct('Admin','Agents','GET /agents/{agentId}/messages',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def getAgentIndicators(agentId):
#Check if accessToken is empty or null
    if accessToken!="":
 

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/agents/' + agentId + '/indicators' , headers = header_param) 
 
#print response appropriately
        construct('Admin','Agents','GET /agents/{agentId}/indicators',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def endAgentSession(agentId):
#Check if accessToken is empty or null
    if accessToken!="":
 

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http post request
        response = requests.post(baseURI + 'services/v11.0/agents/' + agentId + '/logout' , headers = header_param) 
 
#print response appropriately
        construct('Admin','Agents','POST /agents/{agentId}/logout',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def getAgentsDialingPatterns():
#Check if accessToken is empty or null
    if accessToken!="":
 

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/agent-patterns' , headers = header_param) 
 
#print response appropriately
        construct('Admin','Agents','GET /agent-patterns',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def getAgentsStates():
#Check if accessToken is empty or null
    if accessToken!="":
 

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/agents-states' , headers = header_param) 
 
#print response appropriately
        construct('Admin','Agents','GET /agents-states',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def getTeams():
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'fields': 'string',
        'updatedSince': 'string - ISO 8601 formatted date/time',
        'isActive': 'boolean',
        'searchString': 'string',
        'skip': 'integer',
        'top': 'integer',
        'orderBy': 'string'}
 

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/teams' , headers = header_param, params=payload) 
 
#print response appropriately
        construct('Admin','Agents','GET /teams',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getTeamById(teamId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'fields': 'string'}
 

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/teams/' + teamId , headers = header_param, params=payload) 
 
#print response appropriately
        construct('Admin','Agents','GET /teams/{teamId}',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def deleteAgentsFromTeam(teamId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'transferTeamId': 'integer - required',
        'agents': [
        {
        'agentId': 'integer - required'
        }]}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http delete request
        response = requests.delete(baseURI + 'services/v11.0/teams/' + teamId + '/agents' , headers = header_param,data=payload)
 
#print response appropriately
        construct('Admin','Agents','DELETE /teams/{teamId}/agents',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def assignAgentsToTeam(teamId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'agents': [
        {
        'agentId': 'integer - required'
        }]}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http post request
        response = requests.post(baseURI + 'services/v11.0/teams/' + teamId + '/agents', headers = header_param, data=payload) 
 
#print response appropriately
        construct('Admin','Agents','POST /teams/{teamId}/agents',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def deleteUnavailableCodesByTeam(teamId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'codes': [
        {
        'outstateId': 'required - integer'
        }]}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http delete request
        response = requests.delete(baseURI + 'services/v11.0/teams/' + teamId + '/unavailable-codes' , headers = header_param,data=payload)
 
#print response appropriately
        construct('Admin','Agents','DELETE /teams/{teamId}/unavailable-codes',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getUnavailableCodesByTeam(teamId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'activeOnly': 'boolean'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/teams/' + teamId + '/unavailable-codes' , headers = header_param, params=payload) 
 
#print response appropriately
        construct('Admin','Agents','GET /teams/{teamId}/unavailable-codes',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def assignUnavailableCodesToTeam(teamId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'codes': [
        {
        'outstateId': 'required - integer'
        }]}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http post request
        response = requests.post(baseURI + 'services/v11.0/teams/' + teamId + '/unavailable-codes', headers = header_param, data=payload) 
 
#print response appropriately
        construct('Admin','Agents','POST /teams/{teamId}/unavailable-codes',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def PutTeamidunavailablecodes(teamid):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={
        'unavailableCodes': [
        {
        'outStateId': 'integer - required'
        }]}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http put request
        response = requests.put(baseURI +  'services/v13.0/teams/' + teamid + '/unavailablecode' , headers = header_param, data=payload)
 
#print response appropriately
        construct('Admin','Agents','PUT /teams/{teamId}/unavailable-codes',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getAgentScheduledCallbacks(agentId): 

#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/agents/' + agentId + '/scheduled-callbacks' , headers = header_param) 
#print response appropriately
        construct('Admin','ScheduledCallbacks','GET /agents/{agentId}/scheduled-callbacks',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def createScheduledCallback():
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'phoneNumber': 'string - required',
        'skillId': 'integer - required',
        'scheduleDate': 'ISO 8601 formatted date/time - required',
        'firstName': 'string',
        'lastName': 'string',
        'targetAgentId': 'integer - Do not supply if queuing to Skill',
        'notes': 'string'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http post request
        response = requests.post(baseURI + 'services/v11.0/scheduled-callbacks', headers = header_param, data=payload) 
 
#print response appropriately
        construct('Admin','ScheduledCallbacks','POST /scheduled-callbacks',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def deleteScheduledCallback(callbackId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http delete request
        response = requests.delete(baseURI + 'services/v11.0/scheduled-callbacks/' + callbackId , headers = header_param)
 
#print response appropriately
        construct('Admin','ScheduledCallbacks','DELETE /scheduled-callbacks/{callbackId}',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def updateScheduledCallback(callbackId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'phoneNumber': 'string - required',
        'skillId': 'integer - required',
        'scheduleDate': 'ISO 8601 formatted date/time - required',
        'firstName': 'string',
        'lastName': 'string',
        'targetAgentId': 'integer - Do not supply if queuing to Skill',
        'notes': 'string'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http put request
        response = requests.put(baseURI +  'services/v11.0/scheduled-callbacks/' + callbackId , headers = header_param, data=payload) 
 
#print response appropriately
        construct('Admin','ScheduledCallbacks','PUT /scheduled-callbacks/{callbackId}',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getSkillScheduledCallbacks(skillId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/skills/' + skillId + '/scheduled-callbacks' , headers = header_param) 
 
#print response appropriately
        construct('Admin','ScheduledCallbacks','GET /skills/{skillId}/scheduled-callbacks',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def getBusinessUnit():
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'fields': 'string'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/business-unit' , headers = header_param, params=payload) 
 
#print response appropriately
        construct('Admin','General','GET /business-unit',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getCountries():
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/countries' , headers = header_param) 
 
#print response appropriately
        construct('Admin','General','GET /countries',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def getStatesByCountryId(countryId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/countries/' + countryId + '/states' , headers = header_param) 
 
#print response appropriately
        construct('Admin','General','GET /countries/{countryId}/states',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def getDataDefinitionTypes():
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/data-definitions/data-types' , headers = header_param) 
 
#print response appropriately
        construct('Admin','General','GET /data-definitions/data-types',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def getFile():
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={ 'fileName': 'hajsh'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v4.0.0/files' , headers = header_param, params=payload) 
 
#print response appropriately
        construct('Admin','General','GET /files',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getFeedbackCategories():
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/feedback-categories-and-priorities' , headers = header_param) 
 
#print response appropriately
        construct('Admin','General','GET /feedback-categories-and-priorities',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def getHiringSources():
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/hiring-sources' , headers = header_param) 
 
#print response appropriately
        construct('Admin','General','GET /hiring-sources',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def createHiringSource():
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'sourceName': 'string - required'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http post request
        response = requests.post(baseURI + 'services/v11.0/hiring-sources', headers = header_param, data=payload) 
 
#print response appropriately
        construct('Admin','General','POST /hiring-sources',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getHoursOfOperationProfiles():
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'fields': 'string',
        'updatedSince': 'string - ISO 8601 formatted date/time',
        'skip': 'integer',
        'top': 'integer',
        'orderBy': 'string'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/hours-of-operation', headers = header_param, params=payload) 
 
#print response appropriately
        construct('Admin','General','GET /hours-of-operation',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getHoursOfOperationProfileById(profileId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'fields': 'string'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/hours-of-operation/' + profileId , headers = header_param, params=payload) 
 
#print response appropriately
        construct('Admin','General','GET /hours-of-operation/{profileId}',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getLocations():
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'includeAgents': 'boolean'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/locations' , headers = header_param, params=payload) 
 
#print response appropriately
        construct('Admin','General','GET /locations',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getMediaTypes():
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/media-types' , headers = header_param) 
 
#print response appropriately
        construct('Admin','General','GET /media-types',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def getMediaTypesById(mediaTypeId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/media-types/' + mediaTypeId , headers = header_param) 
 
#print response appropriately
        construct('Admin','General','GET /media-types/{mediaTypeId}',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def getMessageTemplates():
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'templateTypeId': 'string'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/message-templates' , headers = header_param, params=payload) 
 
#print response appropriately
        construct('Admin','General','GET /message-templates',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def createMessageTemplate():
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'templateName': 'string',
        'templateTypeId': 'integer',
        'smsContent': 'string'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http post request
        response = requests.post(baseURI + 'services/v11.0/message-templates', headers = header_param, data=payload) 
 
#print response appropriately
        construct('Admin','General','POST /message-templates',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getMessageTemplatesById(templateId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v8.0.0/message-templates/' + templateId , headers = header_param) 
 
#print response appropriately
        construct('Admin','General','GET /message-templates/{templateId}',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def updateMessageTemplate(templateId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'templateName': 'string',
        'isActive': 'boolean',
        'smsContent': 'string'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http put request
        response = requests.put(baseURI +  'services/v8.0.0/message-templates/' + templateId , headers = header_param, data=payload) 
 
#print response appropriately
        construct('Admin','General','PUT /message-templates/{templateId}',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getPermissions():
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/permissions' , headers = header_param) 
 
#print response appropriately
        construct('Admin','General','GET /permissions',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def getPermissionsById(agentId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/permissions/' + agentId , headers = header_param) 
 
#print response appropriately
        construct('Admin','General','GET /permissions/{agentId}',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def getPhoneCodes():
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/phone-codes' , headers = header_param) 
 
#print response appropriately
        construct('Admin','General','GET /phone-codes',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def getPointsOfContact():
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/points-of-contact' , headers = header_param) 
 
#print response appropriately
        construct('Admin','General','GET /points-of-contact',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def getPointsOfContactById(pocId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/points-of-contact/' + pocId , headers = header_param) 
 
#print response appropriately
        construct('Admin','General','GET /points-of-contact/{pointOfContactId}',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def getSecurityProfiles():
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'isExternal': 'boolean',
        'isActive': 'boolean'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/security-profiles' , headers = header_param, params=payload) 
 
#print response appropriately
        construct('Admin','General','GET /security-profiles',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getSecurityProfileById(profileId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/security-profiles/' + profileId , headers = header_param) 
 
#print response appropriately
        construct('Admin','General','GET /security-profiles/{profileId}',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def getScripts():
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'mediaTypeId': 'integer',
        'isActive': 'boolean',
        'searchString': 'string',
        'fields': 'string',
        'skip': 'integer',
        'top': 'integer',
        'orderBy': 'string'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/scripts' , headers = header_param, params=payload) 
 
#print response appropriately
        construct('Admin','General','GET /scripts',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def startScript(scriptId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'skillId': 'integer - required',
        'startDate': 'string - ISO 8601',
        'parameters': 'string'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http post request
        response = requests.post(baseURI + 'services/v11.0/scripts/' + scriptId + '/start', headers = header_param, data=payload) 
 
#print response appropriately
        construct('Admin','General','POST /scripts/{scriptId}/start',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getServerTime():
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/server-time' , headers = header_param) 
 
#print response appropriately
        construct('Admin','General','GET /server-time',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def getTags():
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'searchString': 'string',
        'isActive': 'boolean'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/tags' , headers = header_param, params=payload) 
 
#print response appropriately
        construct('Admin','General','GET /tags',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def createTag():
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'tagName': 'string - required',
        'notes': 'string'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http post request
        response = requests.post(baseURI + 'services/v11.0/tags', headers = header_param, data=payload) 
 
#print response appropriately
        construct('Admin','General','POST /tags',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getTagById(tagId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/tags/' + tagId , headers = header_param) 
 
#print response appropriately
        construct('Admin','General','GET /tags/{tagId}',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def updateTag(tagId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'tagName': 'string',
        'notes': 'string',
        'isActive': 'boolean'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http put request
        response = requests.put(baseURI +  'services/v11.0/tags/' + tagId , headers = header_param, data=payload) 
 
#print response appropriately
        construct('Admin','General','PUT /tags/{tagId}',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getTimeZones():
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/timezones' , headers = header_param) 
 
#print response appropriately
        construct('Admin','General','GET /timezones',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def getUnavailableCodes():
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/unavailable-codes' , headers = header_param) 
 
#print response appropriately
        construct('Admin','General','GET /unavailable-codes',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def getCampaigns():
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'isActive': 'boolean',
        'fields': 'string',
        'searchString': 'string',
        'skip': 'integer',
        'top': 'integer',
        'orderby': 'string'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/campaigns' , headers = header_param, params=payload) 
 
#print response appropriately
        construct('Admin','Skills','GET /campaigns',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getCampaignById(campaignId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/campaigns/' + campaignId , headers = header_param) 
 
#print response appropriately
        construct('Admin','Skills','GET /campaigns/{campaignId}',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def getSkills():
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'updatedSince': 'ISO 8601 formatted date/time',
        'mediaTypeId': 'integer',
        'outboundStrategy': 'string',
        'isActive': 'boolean',
        'searchString': 'string',
        'fields': 'string',
        'skip': 'integer',
        'top': 'integer',
        'orderBy': 'string'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/skills' , headers = header_param, params=payload) 
 
#print response appropriately
        construct('Admin','Skills','GET /skills',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getSkillById(skillId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/skills/' + skillId , headers = header_param) 
 
#print response appropriately
        construct('Admin','Skills','GET /skills/{skillId}',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def startPersonalConnectionSkill(skillId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http post request
        response = requests.post(baseURI + 'services/v11.0/skills/' + skillId + '/start' , headers = header_param) 
 
#print response appropriately
        construct('Admin','Skills','POST /skills/{skillId}/start',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def stopPersonalConnectionSkill(skillId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'force': 'boolean'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http post request
        response = requests.post(baseURI + 'services/v11.0/skills/' + skillId + '/stop', headers = header_param, data=payload) 
 
#print response appropriately
        construct('Admin','Skills','POST /skills/{skillId}/stop',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getAgentsAssignedToSkills():
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'updatedSince': 'ISO 8601 formatted date/time',
        'fields': 'string'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/skills/agents' , headers = header_param, params=payload) 
 
#print response appropriately
        construct('Admin','Skills','GET /skills/agents',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def deleteAgentsFromSkill(skillId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'agents': [
        {
        'skillId': 'integer - required'
        }]}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http delete request
        response = requests.delete(baseURI + 'services/v11.0/skills/' + skillId + '/agents', headers = header_param,data=payload)
 
#print response appropriately
        construct('Admin','Skills','DELETE /skills/{skillId}/agents',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getAgentsAssignedToSkill(skillId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'updatedSince': 'ISO 8601 formatted date/time',
        'searchString': 'string',
        'fields': 'string',
        'skip': 'integer',
        'top': 'integer',
        'orderBy': 'string'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/skills/' + skillId + '/agents' , headers = header_param, params=payload) 
 
#print response appropriately
        construct('Admin','Skills','GET /skills/{skillId}/agents',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def assignAgentsToSkill(skillId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'agents': [
        {
        'agentId': 'integer - required',
        'isActive': 'boolean',
        'proficiency': 'integer - 1-Highest, 2-High, 3-Medium, 4-Low, 5-Lowest',
        }]}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http post request
        response = requests.post(baseURI + 'services/v11.0/skills/' + skillId + '/agents', headers = header_param, data=payload) 
 
#print response appropriately
        construct('Admin','Skills','POST /skills/{skillId}/agents',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def modifyAgentsForSkill(skillId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'agents': [
        {
        'skillId': 'integer - required',
        'isActive': 'boolean',
        'proficiency': 'integer - 1-Highest, 2-High, 3-Medium, 4-Low, 5-Lowest'}]}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http put request
        response = requests.put(baseURI + 'services/v11.0/skills/' + skillId + '/agents' , headers = header_param, data=payload) 
 
#print response appropriately
        construct('Admin','Skills','PUT /skills/{skillId}/agents',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getAgentsNotAssignedToSkill(skillId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'searchString': 'string',
        'fields': 'string',
        'skip': 'integer',
        'top': 'integer',
        'orderBy': 'string'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/skills/' + skillId + '/agents/unassigned' , headers = header_param, params=payload) 
 
#print response appropriately
        construct('Admin','Skills','GET /skills/{skillId}/agents/unassigned',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getSkills():
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/skills/call-data' , headers = header_param) 
 
#print response appropriately
        construct('Admin','Skills','GET /skills/call-data',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def getCallDataBySkill(skillId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'startDate': 'ISO 8601 formatted date/time',
        'endDate': 'ISO 8601 formatted date/time'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/skills/' + skillId + '/call-data' , headers = header_param, params=payload) 
 
#print response appropriately
        construct('Admin','Skills','GET /skills/{skillId}/call-data',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def removeTagsFromSkill(skillId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'tags': [
        {
        'tagId': 'integer - required',
        }]}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http delete request
        response = requests.delete(baseURI + 'services/v11.0/skills/' + skillId + '/tags' , headers = header_param,data=payload)
 
#print response appropriately
        construct('Admin','Skills','DELETE /skills/{skillId}/tags',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getSkillTags(skillId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/skills/' + skillId + '/tags' , headers = header_param) 
 
#print response appropriately
        construct('Admin','Skills','GET /skills/{skillId}/tags',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def assignTagsToSkill(skillId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'tags': [
        {
        'tagId': 'integer - required',
        }]}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http post request
        response = requests.post(baseURI + 'services/v11.0/skills/' + skillId + '/tags', headers = header_param, data=payload) 
 
#print response appropriately
        construct('Admin','Skills','POST /skills/{skillId}/tags',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def GetCPAManagement(skillId,fields):
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + '/services/v13.0/skills/'+ skillId + '/parameters/cpa-management'+ '?fields='+ fields , headers = header_param) 
 
#print response appropriately
        construct('Admin','Skills','GET /skills/{skillId}/parameters/cpa-management',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def PutCPAManagement(skillId):
#Check if accessToken is empty or null
    if accessToken!="":
 
 #Give necessary parameters for http request
        payload={"cpaSettings": {
        "abandonMessagePath": "string",
        "abandonMsgMode": 0,
        "abandonTimeout": 0,
        "agentNoResponseSeconds": 0,
        "agentOverrideOptionAnsweringMachine": 'true',
        "agentOverrideOptionBadNumber": 'true',
        "agentOverrideOptionFax": 'true',
        "agentResponseUtteranceMinimumSeconds": 0,
        "agentVoiceThreshold": 0,
        "ansMachineDetMode": 0,
        "ansMachineMsg": "string",
        "customerLiveSilenceSeconds": 0,
        "customerVoiceThreshold": 0,
        "enableCPALogging": 'true',
        "exceptions": [{
        "attempt_No": 0,
        "ansMachineDetMode": 0,
        "ansMachineMsg": "string"}],
        "machineEndSilenceSeconds": 0,
        "machineEndTimeoutSeconds": 0,
        "machineMinimumWithAgentSeconds": 0,
        "machineMinimumWithoutAgentSeconds": 0,
        "preConnectCPAEnabled": 'true',
        "preConnectCPARecording": 'true',
        "treatProgressAsRinging": 'true',
        "utteranceMinimumSeconds": 0}}

#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http put request
        response = requests.put(baseURI +'/services/v13.0/skills/' + skillId+ '/parameters/cpa-management' , headers = header_param,data=payload) 
 
#print response appropriately
        construct('Admin','Skills','PUT /skills/{skillId}/parameters/cpa-management',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def Getxssettings(skillId,fields ):
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + '/services/v13.0/skills/'+ skillId+ '/parameters/XS-settings'+ '?fields='+ fields , headers = header_param)
 
#print response appropriately
        construct('Admin','Skills','GET /skills/{skillId}/parameters/xs-settings',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def Putxssettings(skillId):
#Check if accessToken is empty or null
    if accessToken!="":
 
 #Give necessary parameters for http request
        payload={ "xsSettings": {
        "xsScriptID": 0,
        "xsCheckinScriptID": 0,
        "externalOutboundSkill_No": "string",
        "xsSkillChangedActive": 'true',
        "xsGetContactsActive": 'true',
        "xsFreshThreshold": 0,
        "xsAvailableThreshold": 0,
        "xsReadyThreshold": 0,
        "xsNumberToRetrieve": 0}}

#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http put request
        response = requests.put(baseURI +'/services/v13.0/skills/' + skillId + '/parameters/XS-settings' , headers = header_param,data=payload) 
 
#print response appropriately
        construct('Admin','Skills','PUT /skills/{skillId}/parameters/xs-settings',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error') 

def Getdeliverypreference(skillId,fields ):
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + '/services/v13.0/skills/'+ skillId+ '/parameters/delivery-preferences'+ '?fields='+ fields , headers = header_param)
 
#print response appropriately
        construct('Admin','Skills','GET /skills/{skillId}/parameters/delivery-preferences',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error') 

def Putskilldeliverypreference(skillId):
#Check if accessToken is empty or null
    if accessToken!="":
 
 #Give necessary parameters for http request
        payload={ "deliveryPreferences": {
        "confirmationRequiredDisabled": 'true',
        "confirmationRequiredDeliveryType": 0,
        "confirmationRequiredTimeout": 0,
        "confirmationRequiredTimeoutSubsequent": 0,
        "confirmationRequiredDefaultAccept": 'true',
        "confirmationRequiredDefault": 'true',
        "complianceRecordsDisabled": 'true',
        "complianceRecordsDeliveryType": 0,
        "complianceRecordsTimeout": 0,
        "complianceRecordsTimeoutSubsequent": 0,
        "complianceRecordsDefaultAccept": 'true',
        "showComplianceButtonReschedule": 'true',
        "showComplianceButtonRequeue": 'true',
        "showComplianceButtonSnooze": 'true',
        "showComplianceButtonDisposition": 'true',
        "showPreviewButtonReschedule": 'true',
        "showPreviewButtonRequeue": 'true',
        "showPreviewButtonSnooze": 'true',
        "showPreviewButtonDisposition": 'true'}}

#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http put request
        response = requests.put(baseURI +'/services/v13.0/skills/' + skillId + '/parameters/delivery-preferences' , headers = header_param,data=payload) 
 
#print response appropriately
        construct('Admin','Skills','PUT /skills/{skillId}/parameters/delivery-preferences',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error') 

def Getskillretrysetting(skillId,fields ):
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + '/services/v13.0/skills/'+ skillId+ '/parameters/retry-settings'+ '?fields='+ fields , headers = header_param)
 
#print response appropriately
        construct('Admin','Skills','GET /skills/{skillId}/parameters/retry-settings',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error') 

def Putskillretrysetting(skillId):
#Check if accessToken is empty or null
    if accessToken!="":
 
 #Give necessary parameters for http request
        payload={ "retrySettings": {
        "loadNonFresh": 'true',
        "finalizeWhenExhausted": 'true',
        "maximumAttempts": 0,
        "minimumRetryMinutes": 0,
        "maximumNumberOfHandledCalls": 0,
        "restrictedCallingMinutes": 0,
        "restrictedCallingMaxAttempts": 0,
        "generalStaleMinutes": 0,
        "callbackRestMinutes": 0,
        "releaseAgentSpecificCalls": 'true',
        "maximumNumberOfCallbacks": 0,
        "callbackStaleMinutes": 0}}

#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http put request
        response = requests.put(baseURI +'/services/v13.0/skills/' + skillId + '/parameters/retry-settings' , headers = header_param,data=payload) 
 
#print response appropriately
        construct('Admin','Skills','PUT /skills/{skillId}/parameters/retry-settings',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error') 

def Getschedulesettings(skillId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + '/services/v13.0/skills/'+ skillId+ '/parameters/schedule-settings', headers = header_param)
 
#print response appropriately
        construct('Admin','Skills','GET /skills/{skillId}/parameters/schedule-settings',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error') 

def Putschedulesettings(skillId):
#Check if accessToken is empty or null
    if accessToken!="":
 
 #Give necessary parameters for http request
        payload={ "scheduleSettings": {
        "isScheduled": 'true',
        "sundayStartTime": "string",
        "sundayEndTime": "string",
        "sundayIsActive": 'true',
        "mondayStartTime": "string",
        "mondayEndTime": "string",
        "mondayIsActive": 'true',
        "tuesdayStartTime": "string",
        "tuesdayEndTime": "string",
        "tuesdayIsActive": 'true',
        "wednesdayStartTime": "string",
        "wednesdayEndTime": "string",
        "wednesdayIsActive": 'true',
        "thursdayStartTime": "string",
        "thursdayEndTime": "string",
        "thursdayIsActive": 'true',
        "fridayStartTime": "string",
        "fridayEndTime": "string",
        "fridayIsActive": 'true',
        "saturdayStartTime": "string",
        "saturdayEndTime": "string",
        "saturdayIsActive": 'true'}}

#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http put request
        response = requests.put(baseURI +'/services/v13.0/skills/' + skillId + '/parameters/schedule-settings'  , headers = header_param,data=payload) 
 
#print response appropriately
        construct('Admin','Skills','PUT /skills/{skillId}/parameters/schedule-settings',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error') 

def getChatTranscript(contactId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/contacts/' + contactId + '/chat-transcript' , headers = header_param) 
 
#print response appropriately
        construct('Admin','Contacts','GET /contacts/{contactId}/chat-transcript',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error') 

def getEmailTranscript(contactId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'includeAttachments': 'boolean'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/contacts/' + contactId + '/email-transcript' , headers = header_param, params=payload) 
 
#print response appropriately
        construct('Admin','Contacts','GET /contacts/{contactId}/email-transcript',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error') 

def endContact(contactId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http post request
        response = requests.post(baseURI + 'services/v1.0/contacts/' + contactId + '/end' , headers = header_param) 
 
#print response appropriately
        construct('Admin','Contacts','POST /contacts/{contactId}/end',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error') 

def monitorContact(contactId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'phoneNumber': 'string - required'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http post request
        response = requests.post(baseURI + 'services/v11.0/contacts/' + contactId + '/monitor', headers = header_param, data=payload) 
 
#print response appropriately
        construct('Admin','Contacts','POST /contacts/{contactId}/monitor',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error') 

def recordContact(contactId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http post request
        response = requests.post(baseURI + 'services/v1.0/contacts/' + contactId + '/record' , headers = header_param) 
 
#print response appropriately
        construct('Admin','Contacts','POST /contacts/{contactId}/record',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error') 

def assignTagsToContact(contactId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'tags': [
        {
        'tagId': 'integer - required',
        }]}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http post request
        response = requests.post(baseURI + 'services/v11.0/contacts/' + contactId + '/tags', headers = header_param, data=payload) 
 
#print response appropriately
        construct('Admin','Contacts','POST /contacts/{contactId}/tags',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error') 

def getContactStateDescriptions():
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/contact-state-descriptions' , headers = header_param) 
 
#print response appropriately
        construct('Admin','Contacts','GET /contact-state-descriptions',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error') 

def getContactStateDescriptionById(contactStateId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/contact-state-descriptions/' + contactStateId , headers = header_param) 
 
#print response appropriately
        construct('Admin','Contacts','GET /contact-state-descriptions/{contactStateId}',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error') 

def signalContact(contactId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'p1': 'string',
        'p2': 'string',
        'p3': 'string',
        'p4': 'string',
        'p5': 'string',
        'p6': 'string',
        'p7': 'string',
        'p8': 'string',
        'p9': 'string'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http post request
        response = requests.post(baseURI + 'services/v11.0/interactions/' + contactId + '/signal', headers = header_param, data=payload) 
 
#print response appropriately
        construct('Admin','Contacts','POST /interactions/{contactId}/signal',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error') 

def getDncGroups():
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'fields': 'string',
        'updatedSince': 'string - ISO 8601 formatted date/time'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/dnc-groups' , headers = header_param, params=payload) 
 
#print response appropriately
        construct('Admin','Lists','GET /dnc-groups',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def createDncGroup():
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'dncGroupName': 'required - string',
        'dncGroupDescription': 'string'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http post request
        response = requests.post(baseURI + 'services/v11.0/dnc-groups', headers = header_param, data=payload) 
 
#print response appropriately
        construct('Admin','Lists','POST /dnc-groups',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getDncGroupById(groupId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'fields': 'string'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/dnc-groups/' + groupId , headers = header_param, params=payload) 
 
#print response appropriately
        construct('Admin','Lists','GET /dnc-groups/{groupId}',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def updateDncGroup(dncGroupId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'dncGroupName': 'string',
        'dncGroupDescription': 'string',
        'isActive': 'boolean'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http put request
        response = requests.put(baseURI + 'services/v11.0/dnc-groups/' + dncGroupId , headers = header_param, data=payload) 
 
#print response appropriately
        construct('Admin','Lists','PUT /dnc-groups/{groupId}',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getContributingSkillsForDncGroup(groupId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/dnc-groups/' + groupId + '/contributing-skills' , headers = header_param) 
 
#print response appropriately
        construct('Admin','Lists','GET /dnc-groups/{groupId}/contributing-skills',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def deleteContributingSkill(groupId,skillId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http delete request
        response = requests.delete(baseURI + 'services/v11.0/dnc-groups/' + groupId + '/contributing-skills/' + skillId , headers = header_param)
 
#print response appropriately
        construct('Admin','Lists','DELETE /dnc-groups/{groupId}/contributing-skills/{skillId}',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def assignContributingSkill(groupId,skillId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http post request
        response = requests.post(baseURI + 'services/v11.0/dnc-groups/' + groupId + '/contributing-skills/' + skillId , headers = header_param) 
 
#print response appropriately
        construct('Admin','Lists','POST /dnc-groups/{groupId}/contributing-skills/{skillId}',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def deleteDncGroupRecords(groupId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={ 'dncGroupRecords': [
        {
        'phoneNumber': 'string - required'
        }
        ]}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http delete request
        response = requests.delete(baseURI + 'services/v11.0/dnc-groups/' + groupId + '/records' , headers = header_param,data=payload)
 
#print response appropriately
        construct('Admin','Lists','DELETE /dnc-groups/{groupId}/records',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getDncGroupRecords(groupId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'fields': 'string',
        'skip': 'integer',
        'top': 'integer',
        'orderBy': 'string'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/dnc-groups/' + groupId + '/records' , headers = header_param, params=payload) 
 
#print response appropriately
        construct('Admin','Lists','GET /dnc-groups/{groupId}/records',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def postDncGroupRecords(groupId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'dncGroupRecords': [
        {
        'phoneNumber': 'string - required',
        'expiredDate': 'string - ISO 8601 formatted date/time'
        }
        ]}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http post request
        response = requests.post(baseURI + 'services/v11.0/dnc-groups/' + groupId + '/records', headers = header_param, data=payload) 
 
#print response appropriately
        construct('Admin','Lists','POST /dnc-groups/{groupId}/records',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getScrubbedSkillsForDncGroup(groupId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/dnc-groups/' + groupId + '/scrubbed-skills' , headers = header_param) 
 
#print response appropriately
        construct('Admin','Lists','GET /dnc-groups/{groupId}/scrubbed-skills',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def deleteScrubbedSkill(groupId,skillId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http delete request
        response = requests.delete(baseURI + 'services/v11.0/dnc-groups/' + groupId + '/scrubbed-skills/' + skillId , headers = header_param)
 
#print response appropriately
        construct('Admin','Lists','DELETE /dnc-groups/{groupId}/scrubbed-skills/{skillId}',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def assignScrubbedSkill(groupId,skillId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http post request
        response = requests.post(baseURI + 'services/v11.0/dnc-groups/' + groupId + '/scrubbed-skills/' + skillId , headers = header_param) 
 
#print response appropriately
        construct('Admin','Lists','POST /dnc-groups/{groupId}/scrubbed-skills/{skillId}',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def postDncGroupsSearchPhoneNumber():
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={ 'phoneNumber': 'string'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http post request
        response = requests.post(baseURI + 'services/v11.0/dnc-groups/search', headers = header_param, data=payload) 
 
#print response appropriately
        construct('Admin','Lists','POST /dnc-groups/search',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getCallingLists():
#Check if accessToken is empty or null
    if accessToken!="":
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/lists/call-lists' , headers = header_param) 
 
#print response appropriately
        construct('Admin','Lists','GET /lists/call-lists',cluster + ' v11.0',str(response.status_code), response.reason)
 
    else:
        construct('error')

def createCallingListMapping():
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'listName': 'string - required',
        'listExpirationDate': 'ISO 8601 formatted date/time',
        'externalIdColumn': 'string - required',
        'scoreColumn': 'string',
        'customer1Column': 'string',
        'customer2Column': 'string',
        'callerIdColumn': 'string',
        'priorityColumn': 'string',
        'complianceReqColumn': 'string',
        'firstNameColumn': 'string',
        'lastNameColumn': 'string',
        'addressColumn': 'string',
        'cityColumn': 'string',
        'stateColumn': 'string',
        'zipColumn': 'string',
        'timeZoneColumn': 'string',
        'confirmReqColumn': 'string',
        'overrideFinalizationColumn': 'string',
        'agentIdColumn': 'string',
        'callRequestTimeColumn': 'string',
        'callRequestStaleColumn': 'string',
        'notesColumn': 'string',
        'expirationDateColumn': 'string',}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http post request
        response = requests.post(baseURI + 'services/v11.0/lists/call-lists', headers = header_param, data=payload) 
 
#print response appropriately
        construct('Admin','Lists','POST /lists/call-lists',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def deleteCallingList(listId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'forceInactive': 'boolean',
        'forceDelete': 'boolean'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http delete request
        response = requests.delete(baseURI + 'services/v11.0/lists/call-lists/' + listId , headers = header_param,data=payload)
 
#print response appropriately
        construct('Admin','Lists','DELETE /lists/call-lists/{listId}',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getCallingListById(listId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'updatedSince': 'ISO 8601 formatted date/time',
        'finalized': 'boolean',
        'includeRecords': 'boolean',
        'fields': 'string',
        'skip': 'integer',
        'top': 'integer',
        'orderBy': 'string'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI +'services/v11.0/lists/call-lists/' + listId , headers = header_param, params=payload) 
 
#print response appropriately
        construct('Admin','Lists','GET /lists/call-lists/{listId}',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getCallingListAttempts(listId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'updatedSince': 'ISO 8601 formatted date/time',
        'finalized': 'boolean',
        'fields': 'string',
        'skip': 'integer',
        'top': 'integer',
        'orderBy':  'string'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make get http request
        response = requests.get(baseURI + 'services/v11.0/lists/call-lists/' + listId + '/attempts', headers = header_param, params=payload) 
 
#print response appropriately
        construct('Admin','Lists','GET /lists/call-lists/{listId}/attempts',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def resetAgentPassword(agentId):
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'requestedPassword': 'string','forceChangeOnLogon': 'boolean - true or false'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http put request
        response = requests.put(baseURI + 'services/v11.0/agents/' + agentId + '/reset-password' , headers = header_param, data=payload) 
 
#print response appropriately
        construct('Authentication','Authenticate','PUT /agents/{agentId}/reset-password',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')


def changeAgentPassword():
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'currentPassword': 'required - string','newPassword': 'required - string'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http put request
        response = requests.put(baseURI + 'services/v11.0/agents/change-password' , headers = header_param, data=payload) 
 
#print response appropriately
        construct('Authentication','Authenticate','PUT /agents/change-password',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def queueCallback():
#Check if accessToken is empty or null
    if accessToken!="":
 
#Give necessary parameters for http request
        payload={'phoneNumber': 'string',
        'callerID': 'string',
        'callDelaySec': 'integer',
        'skill': 'integer',
        'taretAgent': 'integer',
        'priorityManagement': 'string - DefaultFromSkill or Custom',
        'intialPriority': 'integer',
        'acceleration': 'integer',
        'maxPriority': 'integer',
        'sequence': 'string',
        'zipTone': 'string - NoZipTone or BeforeSequence or AfterSequence',
        'screenPopSrc': 'string - DefaultFromSkill or UseOnPageOpen or Custom',
        'screenPopUrl': 'string',
        'timeout': 'integer'}
 
#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
    # Make http post request
        response = requests.post(baseURI + 'services/v11.0/queuecallback' , headers = header_param, data=payload) 

    #print response appropriately
        construct('Patron','Callback','POST /queuecallback',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def createPromiseKeeper():
#Check if accessToken is empty or null
    if accessToken!="":

    #Give necessary parameters for http request
        payload={'firstName': 'string',
        'lastName': 'string',
        'phoneNumber': 'string',
        'skill': 'integer',
        'taretAgent': 'integer',
        'promiseDate': 'date',
        'promiseTime': 'time',
        'notes': 'string',
        'timeZone': 'string'}

    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

    # Make get http request
        response = requests.get(baseURI + 'services/v11.0/promise', headers = header_param, data=payload) 

    #print response appropriately
        construct('Patron','Callback','POST /promise',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def createChatSession():
    #Check if accessToken is empty or null
    if accessToken!="":
     
    #Give necessary parameters for http request
        payload={'pointOfContact': 'string - required',
        'fromAddress': 'string',
        'chatRoomID': 'integer',
        'parameters': 'array of strings'}
     
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
     
    # Make http post request
        response = requests.post(baseURI + 'services/v11.0/contacts/chats' , headers = header_param, data=payload) 
     
    #print response appropriately
        construct('Patron','ChatRequests','POST /contacts/chats',cluster + ' v11.0',str(response.status_code), response.reason)
     
    else:
        construct('error')

def endChatSession(chatSessionId):
    #Check if accessToken is empty or null
    if accessToken!="":
     
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
     
    # Make http delete request
        response = requests.delete(baseURI + 'services/v11.0/contacts/chats/' + chatSessionId , headers = header_param) 
     
    #print response appropriately
        construct('Patron','ChatRequests','DELETE /contacts/chats/{chatSession}',cluster + ' v11.0',str(response.status_code), response.reason)
     
    else:
        construct('error')

def getChatText(chatSessionId, timeout):
    #Check if accessToken is empty or null
    if accessToken!="":
     
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
     
    # Make get http request
        response = requests.get(baseURI + 'services/v11.0/contacts/chats/' + chatSessionId + '?timeout=' + timeout , headers = header_param) 
     
    #print response appropriately
        construct('Patron','ChatRequests','GET /contacts/chats/{chatSession}',cluster + ' v11.0',str(response.status_code), response.reason)
     
    else:
        construct('error')

def sendSingleChatMessage(chatSessionId):
    #Check if accessToken is empty or null
    if accessToken!="":
     
    #Give necessary parameters for http request
        payload={'label': 'string',
        'message': 'string'}
     
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
     
    # Make http post request
        response = requests.post(baseURI + 'services/v11.0/contacts/chats/' + chatSessionId + '/send-text' , headers = header_param, data=payload) 
     
    #print response appropriately
        construct('Patron','ChatRequests','POST /contacts/chats/{chatSession}/send-text',cluster + ' v11.0',str(response.status_code), response.reason)
     
    else:
        construct('error')

def createWorkItem():
    #Check if accessToken is empty or null
    if accessToken!="":
     
    #Give necessary parameters for http request
        payload={'pointOfContact': 'string',
        'workItemId': 'string',
        'workItemPayload': 'string',
        'workItemType': 'string',
        'from': 'string'}
     
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
     
    # Make http post request
        response = requests.post(baseURI + 'services/v11.0/interactions/work-items' , headers = header_param, data=payload) 
     
    #print response appropriately
        construct('Patron','Workitem','POST /interactions/work-items',cluster + ' v11.0',str(response.status_code), response.reason)
     
    else:
        construct('error')

def getAgentsState():
    #Check if accessToken is empty or null
    if accessToken!="":
     
    #Give necessary parameters for http request
        payload={'updatedSince': 'string - ISO 8601 formatted date/time',
        'fields': 'string'}
     
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
     
    # Make get http request
        response = requests.get(baseURI + 'services/v11.0/agents/states', headers = header_param, params=payload) 
     
    #print response appropriately
        construct('Real-Time Data','Real-Time','GET /agents/states',cluster + ' v11.0',str(response.status_code), response.reason)
     
    else:
        construct('error')

def getAgentStateById(agentId):
    #Check if accessToken is empty or null
    if accessToken!="":
     
    #Give necessary parameters for http request
        payload={'updatedSince': 'string - ISO 8601 formatted date/time',
        'fields': 'string'}
     
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
     
    # Make get http request
        response = requests.get(baseURI + 'services/v11.0/agents/' + agentId + '/states' , headers = header_param, params=payload) 
     
    #print response appropriately
        construct('Real-Time Data','Real-Time','GET /agents/{agentId}/states',cluster + ' v11.0',str(response.status_code), response.reason)
     
    else:
        construct('error')

def getContactsActive():
    #Check if accessToken is empty or null
    if accessToken!="":
     
    #Give necessary parameters for http request
        payload={'updatedSince': 'string - ISO 8601 formatted date/time',
        'fields': 'string',
        'mediaTypeId': 'integer',
        'skillId': 'integer',
        'campaignId': 'integer',
        'agentId': 'integer',
        'teamId': 'integer',
        'toAddr': 'string',
        'fromAddr': 'string',
        'stateId': 'integer'}
     
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
     
    # Make get http request
        response = requests.get(baseURI + 'services/v11.0/contacts/active' , headers = header_param, params=payload) 
     
    #print response appropriately
        construct('Real-Time Data','Real-Time','GET /contacts/active',cluster + ' v11.0',str(response.status_code), response.reason)
     
    else:
        construct('error')

def getContactsParked():
    #Check if accessToken is empty or null
    if accessToken!="":
     
    #Give necessary parameters for http request
        payload={'updatedSince': 'string - ISO 8601 formatted date/time',
        'fields': 'string',
        'mediaTypeId': 'integer',
        'skillId': 'integer',
        'campaignId': 'integer',
        'agentId': 'integer',
        'teamId': 'integer',
        'toAddr': 'string',
        'fromAddr': 'string'}
     
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
     
    # Make get http request
        response = requests.get(baseURI + 'services/v11.0/contacts/parked', headers = header_param, params=payload) 
     
    #print response appropriately
        construct('Real-Time Data','Real-Time','GET /contacts/parked',cluster + ' v11.0',str(response.status_code), response.reason)
     
    else:
        construct('error')

def getContactsState():
    #Check if accessToken is empty or null
    if accessToken!="":
     
    #Give necessary parameters for http request
        payload={'agentId': 'integer',
        'updatedSince': 'string - ISO 8601 formatted date/time'}
     
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
     
    # Make get http request
        response = requests.get(baseURI + 'services/v1.0/contacts/states', headers = header_param, params=payload) 
     
    #print response appropriately
        construct('Real-Time Data','Real-Time','GET /contacts/states',cluster + ' v11.0',str(response.status_code), response.reason)
     
    else:
        construct('error')

def getSkillsActivity():
    #Check if accessToken is empty or null
    if accessToken!="":
     
    #Give necessary parameters for http request
        payload={'fields': 'string',
        'updatedSince': 'string - ISO 8601 formatted date/time'}
     
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
     
    # Make get http request
        response = requests.get(baseURI + 'services/v11.0/skills/activity' , headers = header_param, params=payload) 
     
    #print response appropriately
        construct('Real-Time Data','Real-Time','GET /skills/activity',cluster + ' v11.0',str(response.status_code), response.reason)
     
    else:
        construct('error')

def getSkillActivityById(skillId):
    #Check if accessToken is empty or null
    if accessToken!="":
     
    #Give necessary parameters for http request
        payload={'fields': 'string',
        'updatedSince': 'string - ISO 8601 formatted date/time'}
     
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
     
    # Make get http request
        response = requests.get(baseURI + 'services/v8.0/skills/' + skillId + '/activity', headers = header_param, params=payload) 
     
    #print response appropriately
        construct('Real-Time Data','Real-Time','GET /skills/{skillId}/activity',cluster + ' v11.0',str(response.status_code), response.reason)
     
    else:
        construct('error')

def getAgentContactHistory(agentID):
    #Check if accessToken is empty or null
    if accessToken!="":
     
    #Give necessary parameters for http request
        payload={'startDate':'ISO 8601 formatted date/time', 
        'endDate':'ISO 8601 formatted date/time',
        'updatedSince':'ISO 8601 formatted date/time',
        'mediaTypeId':'integer',
        'fields':'string',
        'skip':'integer',
        'top':'integer',
        'orderby':'string'}
     
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
     
    # Make get http request
        response = requests.get(baseURI + 'services/v11.0/agents/' + agentID + '/interaction-history' , headers = header_param, params=payload) 
     
    #print response appropriately
        construct('Reporting','Reporting','GET /agents/{agentId}/interaction-history',cluster + ' v11.0',str(response.status_code), response.reason)
     
    else:
        construct('error')

def getRecentContactsInfo(agentID):
    #Check if accessToken is empty or null
    if accessToken!="":
     
    #Give necessary parameters for http request
        payload={'startDate':'ISO 8601 formatted date/time', 
        'endDate':'ISO 8601 formatted date/time',
        'fields':'string',
        'top':'integer'}
     
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
     
    # Make get http request
        response = requests.get(baseURI +  'services/v11.0/agents/' + agentID + '/interaction-recent' , headers = header_param, params=payload) 
     
    #print response appropriately
        construct('Reporting','Reporting','GET /agents/{agentId}/interaction-recent',cluster + ' v11.0',str(response.status_code), response.reason)
     
    else:
        construct('error')

def getAgentLoginHistory(agentID):
    #Check if accessToken is empty or null
    if accessToken!="":
     
    #Give necessary parameters for http request
        payload={'startDate':'ISO 8601 formatted date/time', 
        'endDate':'ISO 8601 formatted date/time',
        'searchString':'string',
        'fields':'string',
        'skip':'string',
        'top':'string',
        'orderby':'string'}
     
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
     
    # Make get http request
        response = requests.get(baseURI + 'services/v11.0/agents/' + agentID + '/login-history' , headers = header_param, params=payload) 
     
    #print response appropriately
        construct('Reporting','Reporting','GET /agents/{agentId}/login-history',cluster + ' v11.0',str(response.status_code), response.reason)
     
    else:
        construct('error')

def getAgentStateHistory(agentID):
    #Check if accessToken is empty or null
    if accessToken!="":
     
    #Give necessary parameters for http request
        payload={'startDate':'ISO 8601 formatted date/time', 
        'endDate':'ISO 8601 formatted date/time',
        'mediaType':'string'}
     
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
     
    # Make get http request
        response = requests.get(baseURI +  'services/v11.0/agents/' + agentID + '/statehistory' , headers = header_param, params=payload) 
     
     
    #print response appropriately
        construct('Reporting','Reporting','GET /agents/{agentId}/state-history',cluster + ' v11.0',str(response.status_code), response.reason)
     
    else:
        construct('error')

def getAgentsPerformance():
    #Check if accessToken is empty or null
    if accessToken!="":
     
    #Give necessary parameters for http request
        payload={'startDate':'ISO 8601 formatted date/time', 
        'endDate':'ISO 8601 formatted date/time',
        'fields':'string'}
     
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
     
    # Make get http request
        response = requests.get(baseURI + 'services/v3.0/agents/performance' , headers = header_param, params=payload) 
     
     
    #print response appropriately
        construct('Reporting','Reporting','GET /agents/performance',cluster + ' v11.0',str(response.status_code), response.reason)
     
    else:
        construct('error')

def getAgentPerformanceById(agentID):
    #Check if accessToken is empty or null
    if accessToken!="":
     
    #Give necessary parameters for http request
        payload={'startDate':'ISO 8601 formatted date/time', 
        'endDate':'ISO 8601 formatted date/time',
        'fields':'string'}
     
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
     
    # Make get http request
        response = requests.get(baseURI +  'services/v3.0/agents/' + agentID + '/performance' , headers = header_param, params=payload) 
     
    #print response appropriately
        construct('Reporting','Reporting','GET /agents/{agentId}/performance',cluster + ' v11.0',str(response.status_code), response.reason)
     
    else:
        construct('error')

def getContactHistory(contactId):
    #Check if accessToken is empty or null
    if accessToken!="":
     
    #Give necessary parameters for http request
        payload={'fields':'string'}
     
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
     
    # Make get http request
        response = requests.get(baseURI +  'services/v11.0/contacts/' + contactId , headers = header_param, params=payload) 
     
    #print response appropriately
        construct('Reporting','Reporting','GET /contacts/{contactId}',cluster + ' v11.0',str(response.status_code), response.reason)
     
    else:
        construct('error')

def getCompletedContacts():
    #Check if accessToken is empty or null
    if accessToken!="":
     
    #Give necessary parameters for http request
        payload={'startDate':'ISO 8601 formatted date/time', 
        'endDate':'ISO 8601 formatted date/time',
        'mediaType':'string',
        'updatedSince': 'string - ISO 8601 formatted date/time',
        'fields': 'string',
        'skip': 'integer',
        'top': 'integer',
        'orderBy': 'string',
        'skillId': 'integer',
        'campaignId': 'integer',
        'agentId': 'integer',
        'teamId': 'integer',
        'toAddr': 'string',
        'fromAddr': 'string',
        'isLogged': 'boolean',
        'isRefused': 'boolean',
        'isTakeover': 'boolean',
        'tags': 'string',
        'analyticsProcessed': 'boolean'}
     
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
     
    # Make get http request
        response = requests.get(baseURI +  'services/v11.0/contacts/completed' , headers = header_param, params=payload) 
     
    #print response appropriately
        construct('Reporting','Reporting','GET /contacts/completed',cluster + ' v11.0',str(response.status_code), response.reason)
     
    else:
        construct('error')

def callquality(contactId):
    #Check if accessToken is empty or null
    if accessToken!="":
     
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
     
    # Make get http request
        response = requests.get(baseURI + '/services/v11.0/contacts/' + contactId + '/call-quality' , headers = header_param) 
     
    #print response appropriately
        construct('Reporting','Reporting','GET /contacts/{contactId}/call-quality',cluster + ' v11.0',str(response.status_code), response.reason)
     
    else:
        construct('error')

def getContactStateHistory(contactId):
    #Check if accessToken is empty or null
    if accessToken!="":
     
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
     
    # Make get http request
        response = requests.get(baseURI + 'services/v11.0/contacts/' + contactId + '/statehistory' , headers = header_param) 
     
    #print response appropriately
        construct('Reporting','Reporting','GET /contacts/{contactId}/statehistory',cluster + ' v11.0',str(response.status_code), response.reason)
     
    else:
        construct('error')

def getContactCustomData(contactId):
    #Check if accessToken is empty or null
    if accessToken!="":
     
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
     
    # Make get http request
        response = requests.get(baseURI + 'services/v11.0/contacts/' + contactId + '/custom-data' , headers = header_param) 
     
    #print response appropriately
        construct('Reporting','Reporting','GET /contacts/{contactId}/custom-data',cluster + ' v11.0',str(response.status_code), response.reason)
     
    else:
        construct('error')

def getSkillsSummary():
    #Check if accessToken is empty or null
    if accessToken!="":
     
    #Give necessary parameters for http request
        payload={'startDate':'ISO 8601 formatted date/time required' , 
        'endDate':'ISO 8601 formatted date/time required',
        'mediaTypeId': 'integer',
        'isOutbound': 'boolean',
        'fields': 'string'}
     
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
     
    # Make get http request
        response = requests.get(baseURI + 'services/v11.0/skills/summary' , headers = header_param, params=payload) 
     
    #print response appropriately
        construct('Reporting','Reporting','GET /skills/summary',cluster + ' v11.0',str(response.status_code), response.reason)
     
    else:
        construct('error')

def getSkillSummaryById(skillId):
    #Check if accessToken is empty or null
    if accessToken!="":
     
    #Give necessary parameters for http request
        payload={'startDate':'ISO 8601 formatted date/time - required', 
        'endDate':'ISO 8601 formatted date/time - required',
        'fields':'string'}
     
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
     
    # Make get http request
        response = requests.get(baseURI + 'services/v11.0/skills/' + skillId + '/summary' , headers = header_param, params=payload) 
     
    #print response appropriately
        construct('Reporting','Reporting','GET /skills/{skillId}/summary',cluster + ' v11.0',str(response.status_code), response.reason)
     
    else:
        construct('error')

def getSkillsSlaSummary():
    #Check if accessToken is empty or null
    if accessToken!="":
     
    #Give necessary parameters for http request
        payload={'startDate':'ISO 8601 formatted date/time- required', 
        'endDate':'ISO 8601 formatted date/time- required'}
     
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
     
    # Make get http request
        response = requests.get(baseURI + 'services/v11.0/skills/sla-summary' , headers = header_param, params=payload) 
     
    #print response appropriately
        construct('Reporting','Reporting','GET /skills/sla-summary',cluster + ' v11.0',str(response.status_code), response.reason)
     
    else:
        construct('error')

def getSkillSlaSummaryById(skillId):
    #Check if accessToken is empty or null
    if accessToken!="":
     
    #Give necessary parameters for http request
        payload={'startDate':'ISO 8601 formatted date/time', 
        'endDate':'ISO 8601 formatted date/time'}
     
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
     
    # Make get http request
        response = requests.get(baseURI + 'services/v11.0/skills/' + skillId + '/sla-summary' , headers = header_param, params=payload) 
     
    #print response appropriately
        construct('Reporting','Reporting','GET /skills/{skillId}/sla-summary',cluster + ' v11.0',str(response.status_code), response.reason)
     
    else:
        construct('error')

def getReports():
    #Check if accessToken is empty or null
    if accessToken!="":
     
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
     
    # Make get http request
        response = requests.get(baseURI + 'services/v11.0/reports' , headers = header_param)
     
    #print response appropriately
        construct('Reporting','Reporting','GET /reports',cluster + ' v11.0',str(response.status_code), response.reason)
     
    else:
        construct('error')

def getReportJobs():
    #Check if accessToken is empty or null
    if accessToken!="":
     
    #Give necessary parameters for http request
        payload={'fields': 'string',
        'reportId': 'integer',
        'jobStatus': 'string',
        'jobSpan': 'integer'}
     
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
     
    # Make get http request
        response = requests.get(baseURI + 'services/v11.0/report-jobs' , headers = header_param, params=payload) 
     
    #print response appropriately
        construct('Reporting','Reporting','GET /report-jobs',cluster + ' v11.0',str(response.status_code), response.reason)
     
    else:
        construct('error')

def startReportJob(reportId):
    #Check if accessToken is empty or null
    if accessToken!="":
     
    #Give necessary parameters for http request
        payload={'fileType': 'string - CSV, TAB, XML, Excel',
        'includeHeaders': 'boolean - true or false',
        'appendDate': 'boolean - true or false',
        'deleteAfter': 'integer',
        'overwrite': 'boolean - true or false',
        'startDate': 'string - ISO 8601 formatted date/time',
        'endDate': 'string - ISO 8601 formatted date/time'}
     
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
     
    # Make http post request
        response = requests.post(baseURI +'services/v11.0/report-jobs/' + reportId , headers = header_param, data=payload) 
     
    #print response appropriately
        construct('Reporting','Reporting','POST /report-jobs/{reportId}',cluster + ' v11.0',str(response.status_code), response.reason)
     
    else:
        construct('error')

def postAgentPhoneDial(sessionID):
    #Check if accessToken is empty or null
    if accessToken!="":
    
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID + '/agent-phone/dial' , headers = header_param)

    #print response appropriately
        construct('Agent','AgentPhone','POST /agent-sessions/{sessionId}/agent-phone/dial',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentEndCall(sessionID):
    if accessToken!="":
    
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
        
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID + '/agent-phone/end' , headers = header_param) 

    #print response appropriately
        construct('Agent','AgentPhone','POST /agent-sessions/{sessionId}/agent-phone/end',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def muteAgentLeg(sessionID):
    if accessToken!="":
    
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
        
    # Make http post request
        response = requests.post(baseURI+'services/v11.0/agent-sessions/' + sessionID + '/agent-phone/mute' , headers = header_param) 

    #print response appropriately
        construct('Agent','AgentPhone','POST /agent-sessions/{sessionId}/agent-phone/mute',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def unmuteAgentLeg(sessionID):
    #Check if accessToken is empty or null
    if accessToken!="":

    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
        
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID + '/agent-phone/unmute' , headers = header_param)
        
    #print response appropriately
        construct('Agent','AgentPhone','POST /agent-sessions/{sessionId}/agent-phone/unmute',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentPostCancelCallBack(sessionID,callbackId,notes):
    #Check if accessToken is empty or null
    if accessToken!="":

    #Give necessary parameters for http request
        payload={"notes":notes} 
        
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
        
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ callbackId + '/cancel' , headers = header_param, data=payload) 
        
    #print response appropriately
        construct('Agent','ScheduledCallbacks','POST /agent-sessions/{sessionid}/interactions/{callbackid}/cancel',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentDialCallBack(sessionID,callbackId):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ callbackId + '/dial' , headers = header_param) 
    #print response appropriately
        construct('Agent','ScheduledCallbacks','POST /agent-sessions/{sessionId}/interactions/{callbackId}/dial',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentRescheduleCallBack(sessionID,callbackId,rescheduleDate):
    #Check if accessToken is empty or null
    if accessToken!="":
    #Give necessary parameters for http request
        payload={"rescheduleDate":rescheduleDate} 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ callbackId + '/reschedule' , headers = header_param, data=payload) 
    #print response appropriately
        construct('Agent','ScheduledCallbacks','POST /agent-sessions/{sessionId}/interactions/{callbackId}/reschedule',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentAddChat(sessionID):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID + '/interactions/add-chat' , headers = header_param) 
    #print response appropriately
        construct('Agent','ChatRequests','POST /agent-sessions/{sessionId}/interactions/add-chat',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentAcceptContact(sessionID,contactID):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/accept' , headers = header_param) 
    #print response appropriately
        construct('Agent','ChatRequests','POST /agent-sessions/{sessionId}/interactions/{contactId}/accept',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentActivateChat(sessionID,contactID):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/activate-chat' , headers = header_param) 
    #print response appropriately
        construct('Agent','ChatRequests','POST /agent-sessions/{sessionId}/interactions/{contactId}/activate-chat',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentEndContact(sessionID,contactID):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/end' , headers = header_param) 
    #print response appropriately
        construct('Agent','ChatRequests','POST /agent-sessions/{sessionid}/interactions/{contactId}/end',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentRejectContact(sessionID,contactID):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/reject' , headers = header_param) 
    #print response appropriately
        construct('Agent','ChatRequests','POST /agent-sessions/{sessionId}/interactions/{contactId}/reject',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentSendChatText(sessionID,contactID,chatText):
    #Check if accessToken is empty or null
    if accessToken!="":
    #Give necessary parameters for http request
        payload={"chatText":chatText} 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/send-chat-text' , headers = header_param, data=payload) 
    #print response appropriately
        construct('Agent','ChatRequests','POST /agent-sessions/{sessionId}/interactions/{contactId}/send-chat-text',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def transferChatToAgent(sessionID,contactID,targetAgentId):
    #Check if accessToken is empty or null
    if accessToken!="":
    #Give necessary parameters for http request
        payload={"targetAgentId":targetAgentId} 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/transfer-chat-to-agent' , headers = header_param, data=payload) 
    #print response appropriately
        construct('Agent','ChatRequests','POST /agent-sessions/{sessionId}/interactions/{contactId}/transfer-chat-to-agent',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def transferChatToSkill(sessionID,contactID,targetSkillId):
    #Check if accessToken is empty or null
    if accessToken!="":
    #Give necessary parameters for http request
        payload={"targetSkillId":targetSkillId} 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/transfer-chat-to-skill' , headers = header_param, data=payload) 
    #print response appropriately
        construct('Agent','ChatRequests','POST /agent-sessions/{sessionId}/interactions/{contactId}/transfer-chat-to-skill',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def PostTyping(sessionID,contactID,isTyping,isTextEntered):
    #Check if accessToken is empty or null
    if accessToken!="":
    #Give necessary parameters for http request
        payload={"isTyping":isTyping,
        "isTextEntered":isTextEntered} 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/typing' , headers = header_param, data=payload) 
    #print response appropriately
        construct('Agent','ChatRequests','POST /agent-sessions/{sessionId}/interactions/{contactId}/typing',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentAddEmail(sessionID):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/add-email' , headers = header_param) 
    #print response appropriately
        construct('Agent','Emails','POST /agent-sessions/{sessionId}/interactions/add-email',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentEmailForward(sessionID,contactID,skillId,toAddress,fromAddress,ccAddress,bccAddress,subject,bodyHtml,attachments,attachmentNames,originalAttachmentNames):
    #Check if accessToken is empty or null
    if accessToken!="":
    #Give necessary parameters for http request
        payload={"skillId":skillId,
        "toAddress":toAddress,
        "fromAddress":fromAddress,
        "ccAddress":ccAddress,
        "bccAddress":bccAddress,
        "subject":subject,
        "bodyHtml":bodyHtml,
        "attachments":attachments,
        "attachmentNames":attachmentNames,
        "originalAttachmentNames":originalAttachmentNames} 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/email-forward' , headers = header_param, data=payload) 
    #print response appropriately
        construct('Agent','Emails','POST /agent-sessions/{sessionId}/interactions/{contactId}/email-forward',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentEmailOutbound(sessionID,toAddress,parentContactId):
    #Check if accessToken is empty or null
    if accessToken!="":
    #Give necessary parameters for http request
        payload={"toAddress":toAddress,"parentContactId":parentContactId} 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/email-outbound' , headers = header_param, data=payload) 
    #print response appropriately
        construct('Agent','Emails','POST /agent-sessions/{sessionId}/interactions/email-outbound',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentEmailPark(sessionID,contactID,toAddress,fromAddress,ccAddress,bccAddress,subject,bodyHtml,attachments,isDraft,primaryDispositionId,secondaryDispositionId,tags,notes,attachmentNames,originalAttachmentNames):
    if accessToken!="":
    #Give necessary parameters for http request
        payload={"toAddress":toAddress,
        "fromAddress":fromAddress,
        "ccAddress":ccAddress,
        "bccAddress":bccAddress,
        "subject":subject,
        "bodyHtml":bodyHtml,
        "attachments":attachments,
        "attachmentNames":attachmentNames,
        "isDraft":isDraft,
        "primaryDispositionId":primaryDispositionId,
        "secondaryDispositionId":secondaryDispositionId,
        "tags":tags,
        "notes":notes,
        "originalAttachmentNames":originalAttachmentNames} 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/email-park' , headers = header_param, data=payload) 
    #print response appropriately
        construct('Agent','Emails','POST /agent-sessions/{sessionId}/interactions/{contactId}/email-park',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentEmailPreview(sessionID,contactID):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/email-preview' , headers = header_param) 
    #print response appropriately
        construct('Agent','Emails','POST /agent-sessions/{sessionId}/interactions/{contactId}/email-preview',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentEmailReply(sessionID,contactID,skillId,toAddress,fromAddress,ccAddress,bccAddress,subject,bodyHtml,attachments,attachmentNames,originalAttachmentNames):
    #Check if accessToken is empty or null
    if accessToken!="":
    #Give necessary parameters for http request
        payload={"skillId":skillId,
        "toAddress":toAddress,
        "fromAddress":fromAddress,
        "ccAddress":ccAddress,
        "bccAddress":bccAddress,
        "subject":subject,
        "bodyHtml":bodyHtml,
        "attachments":attachments,
        "attachmentNames":attachmentNames,
        "originalAttachmentNames":originalAttachmentNames} 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/email-reply' , headers = header_param, data=payload) 
    #print response appropriately
        construct('Agent','Emails','POST /agent-sessions/{sessionId}/interactions/{contactId}/email-reply',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentEmailRestore(sessionID,contactID):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/email-restore' , headers = header_param) 
    #print response appropriately
        construct('Agent','Emails','POST /agent-sessions/{sessionId}/interactions/{contactId}/email-restore',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentEmailSend(sessionID,contactID,skillId,toAddress,fromAddress,ccAddress,bccAddress,subject,bodyHtml,attachments,attachmentNames):
    #Check if accessToken is empty or null
    if accessToken!="":
    #Give necessary parameters for http request
        payload={"skillId":skillId,
        "toAddress":toAddress,
        "fromAddress":fromAddress,
        "ccAddress":ccAddress,
        "bccAddress":bccAddress,
        "subject":subject,
        "bodyHtml":bodyHtml,
        "attachments":attachments,
        "attachmentNames":attachmentNames} 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/email-send' , headers = header_param, data=payload) 
    #print response appropriately
        construct('Agent','Emails','POST /agent-sessions/{sessionId}/interactions/{contactId}/email-send',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def transferEmailToAgent(sessionID,contactID,targetAgentId):
    #Check if accessToken is empty or null
    if accessToken!="":
    #Give necessary parameters for http request
        payload={"targetAgentId":targetAgentId} 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/transfer-email-to-agent' , headers = header_param, data=payload) 
    #print response appropriately
        construct('Agent','Emails','POST /agent-sessions/{sessionId}/interactions/{contactId}/transfer-email-to-agent',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def transferEmailToSkill(sessionID,contactID,targetSkillId):
    #Check if accessToken is empty or null
    if accessToken!="":
    #Give necessary parameters for http request
        payload={"targetSkillId":targetSkillId} 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/transfer-email-to-skill' , headers = header_param, data=payload) 
    #print response appropriately
        construct('Agent','Emails','POST /agent-sessions/{sessionId}/interactions/{contactId}/transfer-email-to-skill',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentEmailUnPark(sessionID,contactID,isImmediate):
    #Check if accessToken is empty or null
    if accessToken!="":
    #Give necessary parameters for http request
        payload={"isImmediate":isImmediate} 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/email-unpark' , headers = header_param, data=payload) 
    #print response appropriately
        construct('Agent','Emails','POST /agent-sessions/{sessionId}/interactions/{contactId}/email-unpark',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentDialerLogin(sessionID,skillName):
    #Check if accessToken is empty or null
    if accessToken!="":
    #Give necessary parameters for http request
        payload={"skillName":skillName} 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/dialer-login' , headers = header_param, data=payload) 
    #print response appropriately
        construct('Agent','PersonalConnection','POST /agent-sessions/{sessionid}/dialer-login',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentDialerLogout(sessionID):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/dialer-logout' , headers = header_param) 
    #print response appropriately
        construct('Agent','PersonalConnection','POST /agent-sessions/{sessionid}/dialer-logout',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentSnooze(sessionID,contactID):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/snooze' , headers = header_param) 
    #print response appropriately
        construct('Agent','PersonalConnection','POST /agent-sessions/{sessionId}/interactions/{contactId}/snooze',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentIndependentDial(sessionID,contactID):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/independent-dial' , headers = header_param) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionId}/interactions/{contactId}/independent-dial',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentDialOutcome(sessionID,contactID):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/independent-dial-outcome' , headers = header_param) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionId}/interactions/{contactId}/independent-dial-outcome',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def phoneConsultAgent(sessionID,targetAgentId,parentContactId):
    #Check if accessToken is empty or null
    if accessToken!="":
    #Give necessary parameters for http request
        payload={"targetAgentId":targetAgentId,"parentContactId":parentContactId} 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/consult-agent' , headers = header_param, data=payload) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionid}/consult-agent',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def phoneDialAgent(sessionID,targetAgentId,parentContactId):
    #Check if accessToken is empty or null
    if accessToken!="":
    #Give necessary parameters for http request
        payload={"targetAgentId":targetAgentId,"parentContactId":parentContactId} 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/dial-agent' , headers = header_param, data=payload) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionid}/dial-agent',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentDialPhone(sessionID,phoneNumber,skillName,ParentContactId):
    #Check if accessToken is empty or null
    if accessToken!="":
    #Give necessary parameters for http request
        payload={"phoneNumber":phoneNumber,"skillName":skillName,"ParentContactId":ParentContactId} 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/dial-phone' , headers = header_param, data=payload) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionId}/dial-phone',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def phoneDialSkill(sessionID,skillId,parentContactId):
    #Check if accessToken is empty or null
    if accessToken!="":
    #Give necessary parameters for http request
        payload={"skillId":skillId,"parentContactId":parentContactId} 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v2.0/agent-sessions/' + sessionID +'/dial-skil' , headers = header_param, data=payload) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionId}/dial-skill',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentConferenceCall(sessionID):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/conference-calls' , headers = header_param) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionid}/interactions/conference-calls',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentAcceptConsult(sessionID,contactID):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/accept-consult' , headers = header_param) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionid}/interactions/{contactId}/accept-consult',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentAmdOveride(sessionID,contactID,overrideType):
    #Check if accessToken is empty or null
    if accessToken!="":
    #Give necessary parameters for http request
        payload={"overrideType":overrideType} 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/amd-override' , headers = header_param, data=payload) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionId}/interactions/{contactId}/amd-override',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentSessionDisposition(sessionID,contactID,primaryDispositionId,primaryDispositionNotes,primaryCommitmentAmount,primaryCallbackTime,primaryCallbackNumber,secondaryDispositionId,previewDispositionId):
#Check if accessToken is empty or null
    if accessToken!="":
    #Give necessary parameters for http request
        payload={"primaryDispositionId":primaryDispositionId,
        "primaryDispositionNotes":primaryDispositionNotes,
        "primaryCommitmentAmount":primaryCommitmentAmount,
        "primaryCallbackTime":primaryCallbackTime,
        "primaryCallbackNumber":primaryCallbackNumber,
        "secondaryDispositionId":secondaryDispositionId,
        "previewDispositionId":previewDispositionId} 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/disposition' , headers = header_param, data=payload) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionId}/interactions/{contactId}/disposition',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentContactEnd(sessionID,contactID):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/end' , headers = header_param) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionId}/interactions/{contactId}/end',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentContactHold(sessionID,contactID):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/hold' , headers = header_param) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionId}/interactions/{contactId}/hold',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentSessionMask(sessionID,contactID):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v2.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/mask' , headers = header_param) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionId}/interactions/{contactId}/mask',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentSessionRecord(sessionID,contactID):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/record' , headers = header_param) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionId}/interactions/{contactId}/record',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentContactResume(sessionID,contactID):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/resume' , headers = header_param) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionId}/interactions/{contactId}/resume',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentSessionUnmask(sessionID,contactID):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/unmask' , headers = header_param) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionId}/interactions/{contactId}/unmask',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def transferCall(sessionID):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/transfer-calls' , headers = header_param) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionid}/interactions/transfer-calls',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def  phoneSendDtmf(sessionID,sequence,duration,spacing):
    #Check if accessToken is empty or null
    if accessToken!="":
    #Give necessary parameters for http request
        payload={"sequence":sequence,"duration":duration,"spacing":spacing} 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v3.0/agent-sessions/' + sessionID +'/send-dtmf' , headers = header_param, data=payload) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionId}/send-dtmf',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentDeleteSession(sessionID):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http delete request
        response = requests.delete(baseURI + 'services/v11.0/agent-sessions/'+ sessionID , headers = header_param) 
    #print response appropriately
        construct('Agent','PhoneCalls','DELETE /agent-sessions/{sessionId}',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getNextEvent(sessionID):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make get http request
        response = requests.get(baseURI + '/services/v2.0/sessionID/get-next-event', headers = header_param) 
    #print response appropriately
        construct('Agent','PhoneCalls','GET /agent-sessions/{sessionId}/get-next-event',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentAddContact(sessionID,chat,email,workitem):
    #Check if accessToken is empty or null
    if accessToken!="":
    #Give necessary parameters for http request
        payload={"chat":chat,"email":email,"workitem":workitem} 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID + '/add-contact' , headers = header_param, data=payload) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionId}/add-contact',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentSession(stationPhoneNumber,inactivityTimeout,inactivityForceLogout,asAgentId):
    #Check if accessToken is empty or null
    if accessToken!="":
    #Give necessary parameters for http request
        payload={"stationPhoneNumber":stationPhoneNumber,
        "inactivityTimeout":inactivityTimeout,
        "inactivityForceLogout":inactivityForceLogout,
        "asAgentId":asAgentId} 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' , headers = header_param, data=payload) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentSessionJoin(asAgentId):
    #Check if accessToken is empty or null
    if accessToken!="":
    #Give necessary parameters for http request
        payload={"asAgentId":asAgentId} 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/join' , headers = header_param, data=payload) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/join',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentContactActivate(sessionID,contactID):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/activate' , headers = header_param) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionId}/interactions/{contactId}/activate',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentReSkill(sessionID,continueReskill):
    #Check if accessToken is empty or null
    if accessToken!="":
    #Give necessary parameters for http request
        payload={"continueReskill":continueReskill} 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID + '/continue-reskill' , headers = header_param, data=payload) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionId}/continue-reskill',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentCustomData(sessionID,contactID,indicatorName,data):
    #Check if accessToken is empty or null
    if accessToken!="":
    #Give necessary parameters for http request
        payload={"indicatorName":indicatorName,
        "data":data} 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/custom-data' , headers = header_param, data=payload) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionId}/interactions/{contactId}/custom-data',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentFeedback(sessionID,categoryId,priority,comment,customData):
    #Check if accessToken is empty or null
    if accessToken!="":
    #Give necessary parameters for http request
        payload={"categoryId":categoryId,"priority":priority,"comment":comment,"customData":customData} 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v3.0/agent-sessions/' + sessionID + '/submit-feedback' , headers = header_param, data=payload) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionId}/submit-feedback',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentSessionState(sessionID,state):
    #Check if accessToken is empty or null
    if accessToken!="":
    #Give necessary parameters for http request
        payload={"state":state} 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID + '/state' , headers = header_param, data=payload) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionId}/state',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentSessionBarge(sessionID):
#Check if accessToken is empty or null
    if accessToken!="": 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'services/v2.0/agent-sessions/' + sessionID + '/barge' , headers = header_param) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionid}/barge',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentSessionCoach(sessionID):
#Check if accessToken is empty or null
    if accessToken!="": 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+ 'services/v2.0/agent-sessions/' + sessionID + '/coach' , headers = header_param) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionid}/coach',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentSessionMonitor(sessionID,targetAgentId):
    #Check if accessToken is empty or null
    if accessToken!="":
    #Give necessary parameters for http request
        payload={"targetAgentId":targetAgentId} 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/monitor' , headers = header_param, data=payload) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionid}/monitor',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentSessionTakeOver(sessionID):
#Check if accessToken is empty or null
    if accessToken!="": 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+ 'services/v2.0/agent-sessions/' + sessionID + '/take-over' , headers = header_param) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionid}/take-over',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentEndVoiceMail(sessionID,contactID):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/end' , headers = header_param) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionId}/interactions/{contactId}/end',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentPauseVoiceMail(sessionID,contactID):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v4.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/pause-voicemail' , headers = header_param) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionId}/interactions/{contactId}/pause-voicemail',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentPlayVoiceMail(sessionID,contactID,playTimestamp,position):
    #Check if accessToken is empty or null
    if accessToken!="":
    #Give necessary parameters for http request
        payload={"playTimestamp":playTimestamp,
        "position":position} 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/play-voicemail' , headers = header_param, data=payload) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionId}/interactions/{contactId}/pause-voicemail',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def tranferVoicemailToAgent(sessionID,contactID,targetAgentId):
    #Check if accessToken is empty or null
    if accessToken!="":
    #Give necessary parameters for http request
        payload={"targetAgentId":targetAgentId} 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/transfer-voicemail-to-agent' , headers = header_param, data=payload) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionId}/interactions/{contactId}/transfer-voicemail-to-agent',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def transferChatToSkill(sessionID,contactID,targetSkillId):
    #Check if accessToken is empty or null
    if accessToken!="":
    #Give necessary parameters for http request
        payload={"targetSkillId":targetSkillId} 
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/transfer-voicemail-to-skill' , headers = header_param, data=payload) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionId}/interactions/{contactId}/transfer-voicemail-to-skill',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentAcceptWorkitem(sessionID,contactID):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/accept' , headers = header_param) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionid}/interactions/{contactId}/accept',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentEndWorkitem(sessionID,contactID):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/end' , headers = header_param) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionid}/interactions/{contactId}/end',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentHoldWorkitem(sessionID,contactID):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/hold' , headers = header_param) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionid}/interactions/{contactId}/hold',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentRejectWorkitem(sessionID,contactID):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v11.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/reject' , headers = header_param) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionid}/interactions/{contactId}/reject',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

def agentResumeWorkitem(sessionID,contactID):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v2.0/agent-sessions/' + sessionID +'/interactions/'+ contactID +'/resume' , headers = header_param) 
    #print response appropriately
        construct('Agent','PhoneCalls','POST /agent-sessions/{sessionid}/interactions/{contactId}/resume',cluster + ' v11.0',str(response.status_code), response.reason)
    else:
        construct('error')

#Version 15
def getAccessKeys(agentId,fields,skip,top,orderBy):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param = {'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/x-www-form-urlencoded'}

        response=requests.get(baseURI + '/services/v15.0/access-keys'+ '?fields=' + fields + '&skip' + skip + '&top=' + top + '&orderBy=' + orderBy,headers=header_param)
        #print response appropriately
        construct('Admin','Agents','GET /access-keys',cluster + ' v15.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')
		
def PostAccessKeys(agentId):
    #Check if accessToken is empty or null
    if accessToken!="":
    #payload
        PostAccessKeysPayload = {
        'agentId': 'Integer'}
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/V15.0/access-keys', headers = header_param,data=PostAccessKeysPayload) 
    #print response appropriately
        construct('Admin','Agents','POST /access-keys',cluster + ' v15.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getAccessKeysByID(accessKeyId):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param = {'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/x-www-form-urlencoded'}

        response=requests.get(baseURI + '/services/v15.0/access-keys/'+ accessKeyId,headers=header_param)
        #print response appropriately
        construct('Admin','Agents','GET /access-keys/{accessKeyId}',cluster + ' v15.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')
		
def deleteAccessKeysByID(accessKeyId):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.delete(baseURI + '/services/v15.0/access-keys/'+ accessKeyId,headers=header_param)
        #print response appropriately
        construct('Admin','Agent','DELETE /access-keys/{accessKeyId}',cluster + ' v15.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')
		
def patchAccessKeysByID(accessKeyId,isActive):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}

        response=requests.patch(baseURI + '/services/v15.0/access-keys/'+ accessKeyId + '?isActive=' + isActive,headers=header_param)
        #print response appropriately
        construct('Admin','Agent','PATCH /access-keys/{accessKeyId}',cluster + ' v15.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')
		
def Postcampaigns():
    #Check if accessToken is empty or null
    if accessToken!="":
    #payload
        PostcampaignsPayload = {
        "campaigns": [
        {
        "campaignName": "new",
        "isActive": "true",
        "description": "",
        "notes": ""}]}
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v15.0/campaigns', headers = header_param,data=PostcampaignsPayload) 
    #print response appropriately
        construct('Admin','Skills','POST /campaigns',cluster + ' v15.0',str(response.status_code), response.reason)
    else:
        construct('error')
		
def PutcampaignsByID(campaignId):
    #Check if accessToken is empty or null
    if accessToken!="":
    #payload
        PutcampaignsByIDPayload = {
        "campaigns": [
        {
        "campaignName": "filess",
        "isActive": "true",
        "description": "",
        "notes": ""
        }]}
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.put(baseURI+'/services/v15.0/campaigns/'+campaignId, headers = header_param,data=PutcampaignsByIDPayload) 
    #print response appropriately
        construct('Admin','Skills','PUT /campaigns/{campaignId}',cluster + ' v15.0',str(response.status_code), response.reason)
    else:
        construct('error')
		
def PostcampaignsBySkill(campaignId):
    #Check if accessToken is empty or null
    if accessToken!="":
    #payload
        PostcampaignsBySkillPayload = {
        "skills": [
        {"skillId": 162779
        }]}
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v15.0/campaigns/'+campaignId+'/skills', headers = header_param,data=PostcampaignsBySkillPayload) 
    #print response appropriately
        construct('Admin','Skills','POST /campaigns/{campaignId}/skills',cluster + ' v15.0',str(response.status_code), response.reason)
    else:
        construct('error')
		
def deletecampaignsBySkill(campaignId):
    #Check if accessToken is empty or null
    if accessToken!="":
    #payload
        deletecampaignsBySkillPayload = {
        "skills": [
        {"skillId": 162779,
        "transferCampaignId": 0
        }]}
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v15.0/campaigns/'+campaignId+'/skills', headers = header_param,data=deletecampaignsBySkillPayload) 
    #print response appropriately
        construct('Admin','Skills','DELETE /campaigns/{campaignId}/skills',cluster + ' v15.0',str(response.status_code), response.reason)
    else:
        construct('error')
		
def getSmsHistoricalTranscripts(contactId,businessUnitId):
    #Check if accessToken is empty or null
    if accessToken!="":
        getSmsHistoricalTranscriptsPayload = {
        'businessUnitId': businessUnitId}
        header_param = {'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/x-www-form-urlencoded'}

        response=requests.get(baseURI + 'services/v15.0/contacts/'+ contactId+'/sms-historical-transcript',headers=header_param,params=getSmsHistoricalTranscriptsPayload)
        #print response appropriately
        construct('Admin','Agents','GET /contacts/{contactId}/sms-historical-transcript',cluster + ' v15.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')
		
def getSmsHistoricalContacts(ani,businessUnitId,skillId):
    #Check if accessToken is empty or null
    if accessToken!="":
        getSmsHistoricalContactsPayload = {
        'businessUnitId': businessUnitId,
        'ani':ani,
        'skillId':skillId}
        header_param = {'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/x-www-form-urlencoded'}

        response=requests.get(baseURI + 'services/v15.0/contacts/sms-historical-contacts',headers=header_param,params=getSmsHistoricalContactsPayload)
        #print response appropriately
        construct('Admin','Agents','GET /contacts/sms-historical-contacts',cluster + ' v15.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def PutUnavailableCodeByID(unavailableCodeId):
    #Check if accessToken is empty or null
    if accessToken!="":
    #payload
        PutUnavailableCodeByIDPayload = {
        "unavailableCodeName": "string",
        "postContact": "true",
        "agentTimeout": 0,
        "isActive": "true"}
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.put(baseURI+'/services/v15.0/unavailable-codes/'+unavailableCodeId, headers = header_param,data=PutUnavailableCodeByIDPayload) 
    #print response appropriately
        construct('Admin','Agents','PUT /unavailable-codes/{unavailableCodeId}',cluster + ' v15.0',str(response.status_code), response.reason)
    else:
        construct('error')

def PostHoursofOperation():
    #Check if accessToken is empty or null
    if accessToken!="":
    #payload
        PostHoursofOperationPayload = {
        "profileName": "string"}
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v15.0/hours-of-operation', headers = header_param,data=PostHoursofOperationPayload) 
    #print response appropriately
        construct('Admin','General','POST /hours-of-operation',cluster + ' v15.0',str(response.status_code), response.reason)
    else:
        construct('error')

def PutHoursofOperationByID(profileId):
    #Check if accessToken is empty or null
    if accessToken!="":
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.put(baseURI+'/services/v15.0/hours-of-operation/'+profileId, headers = header_param) 
    #print response appropriately
        construct('Admin','General','PUT /hours-of-operation/{profileId}',cluster + ' v15.0',str(response.status_code), response.reason)
    else:
        construct('error')

def PostPointofContact():
    #Check if accessToken is empty or null
    if accessToken!="":
    #payload
        PostPointofContactPayload = {
        "pointOfContact": "string",
        "pointOfContactName": "string",
        "skillId": 0,
        "isActive": "true",
        "mediaTypeId": 0,
        "scriptName": "string",
        "ivrReportingEnabled": "true"}
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v15.0/points-of-contact', headers = header_param,data=PostPointofContactPayload) 
    #print response appropriately
        construct('Admin','General','POST /points-of-contact',cluster + ' v15.0',str(response.status_code), response.reason)
    else:
        construct('error')

def PutPointofContactByID(pointOfContactId):
    #Check if accessToken is empty or null
    if accessToken!="":
    #payload
        PutPointofContactByIDPayload = {
        "pointOfContactName": "string",
        "skillId": 0,
        "isActive": "true",
        "scriptName": "string",
        "ivrReportingEnabled": "true"}
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.put(baseURI+'/services/v15.0/points-of-contact/'+pointOfContactId, headers = header_param,data=PutPointofContactByIDPayload) 
    #print response appropriately
        construct('Admin','General','PUT /points-of-contact/{pointOfContactId}',cluster + ' v15.0',str(response.status_code), response.reason)
    else:
        construct('error')

def PostUnavailableCodes():
    #Check if accessToken is empty or null
    if accessToken!="":
    #payload
        PostUnavailableCodesPayload = {
        "unavailableCodeName": "string",
        "postContact": "true",
        "isActive": "true"}
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v15.0/unavailable-codes', headers = header_param,data=PostUnavailableCodesPayload) 
    #print response appropriately
        construct('Admin','General','POST /unavailable-codes',cluster + ' v15.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getPhonenumbers(searchString,skip,top):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param = {'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/x-www-form-urlencoded'}

        response=requests.get(baseURI + '/services/v15.0/access-keys'+ '?searchString=' + searchString + '&skip' + skip + '&top=' + top ,headers=header_param)
        #print response appropriately
        construct('Admin','General','GET /phone-numbers',cluster + ' v15.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def getDispositionSkills(updatedSince,isActive,searchString,fields,skip,top,orderBy):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param = {'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/x-www-form-urlencoded'}

        response=requests.get(baseURI + '/services/v15.0/access-keys'+ '?fields=' + fields + '&skip' + skip + '&top=' + top + '&orderBy=' + orderBy +'&updatedSince' + updatedSince + '&isActive=' + isActive + '&searchString=' + searchString,headers=header_param)
        #print response appropriately
        construct('Admin','Skills','GET /dispositions/skills',cluster + ' v15.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def postEmailSaveDraft(sessionID,contactId):
	#Check if accessToken is empty or null
		if accessToken!="":
		#payload
			PostEmailSaveDraftPayload={
			"toAddress":"",
			"fromAddress":"",
			"bccAddress": "",
			"campaignId": 0,
			"subject": "",
			"bodyHtml": "" ,
			"fromAddress": "",
			"attachments": "",
			"attachmentNames": "" ,
			"draftEmailGuidStr": "",
			"primaryDispositionId": 0,
			"secondaryDispositionId": 0,
			"tags": "",
			"notes": "",
			"originalAttachmentNames": ""
			}
			header_param = {'Authorization': 'bearer ' + accessToken,
			'content-Type': 'application/x-www-form-urlencoded'}
	
			response=requests.post(baseURI + '/services/v15.0/agent-sessions/'+ sessionID + '/interactions/' + contactId + '/email-save-draft', headers = header_param, data=PostEmailSaveDraftPayload)
			#print response appropriately
			construct('Agent','Email','POST /email-save-draft',cluster + ' v15.0',str(response.status_code), response.reason)
		else:
			#no token get one or handle error
			construct('error')

def postAddText(sessionID):
		#Check if accessToken is empty or null
		if accessToken!="":
		#payload
			 PostAddTextPayload = {
			 "mediaType": 0}
		  
			 header_param = {'Authorization': 'bearer ' + accessToken,
			 'content-Type': 'application/x-www-form-urlencoded'}

			 response=requests.post(baseURI + '/services/v15.0/agent-sessions/'+ sessionID + '/interactions/add-text', headers = header_param, data=PostAddTextPayload)
			#print response appropriately
			 construct('Agent','ChatRequests','POST /add-text',cluster + ' v15.0',str(response.status_code), response.reason)
		else:
			#no token get one or handle error
			 construct('error')

def PutUnavailableCodeByIDTeams(unavailableCodeId):
    #Check if accessToken is empty or null
    if accessToken!="":
    #payload
        PutUnavailableCodeByIDTeamsPayload = {
        "securityUser": " ",
        "teamId": "string"
}
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.put(baseURI+'/services/v15.0/unavailable-codes/'+unavailableCodeId+'/teams', headers = header_param,data=PutUnavailableCodeByIDTeamsPayload) 
    #print response appropriately
        construct('Admin','Agents','PUT /unavailableCodeIdbyTeam',cluster + ' v15.0',str(response.status_code), response.reason)
    else:
        construct('error')
		
def postjobSync():
		#Check if accessToken is empty or null
		if accessToken!="":
		#payload
			 PostjobSyncPayload = {
			 "entityName": "qm-workflows",
			 "version":"1",
			 "startDate":"2019-07-10",
			 "endDate":"2019-07-15"}

			 header_param = {'Authorization': 'bearer ' + accessToken,
			 'content-Type': 'application/x-www-form-urlencoded'}

			 response=requests.post(CXone_baseURI + '/data-extraction/v1/jobs/sync', headers = header_param, data=PostjobSyncPayload)
			#print response appropriately
			 construct('Data extraction','Extract Data','POST /job-sync',cluster + ' v1.0',str(response.status_code), response.reason)
		else:
			#no token get one or handle error
			 construct('error')

def GetmediaplaybackByID(contactID):
    #Check if accessToken is empty or null
    if accessToken!="":

        GetmediaplaybackByIDPayload = {
        'media-type': 'chat',
        'exclude-waveform': 'true'
        }

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/x-www-form-urlencoded'}

        response=requests.get(CXone_baseURI + '/media-playback/v1/contacts/'+ contactID ,headers=header_param,params=GetmediaplaybackByIDPayload)
        #print response appropriately
        construct('Mediaplayback','AccessingRecordedMedia','GET /media-playback/v1/contacts/{contactId}',cluster + ' v1.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def GetmediaplaybackBySegmentID(contactID,segmentID):
    #Check if accessToken is empty or null
    if accessToken!="":

        GetmediaplaybackBySegmentIDPayload = {
        'media-type': 'chat',
        'exclude-waveform': 'true'
        }

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/x-www-form-urlencoded'}

        response=requests.get(CXone_baseURI + '/media-playback/v1/contacts/'+contactID +'/segments/'+segmentID ,headers=header_param,params=GetmediaplaybackBySegmentIDPayload)
        #print response appropriately
        construct('Mediaplayback','AccessingRecordedMedia','GET /media-playback/v1/contacts/{contactId}/segments/{segmentId}',cluster + ' v1.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')

def GetmediaplaybackContacts(acdID):
    #Check if accessToken is empty or null
    if accessToken!="":

        GetmediaplaybackContactsPayload = {
        'media-type': 'chat',
        'exclude-waveform': 'true'
        }

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/x-www-form-urlencoded'}

        response=requests.get(CXone_baseURI + '/media-playback/v1/contacts/'+acdID,headers=header_param,params=GetmediaplaybackContactsPayload)
        #print response appropriately
        construct('Mediaplayback','AccessingRecordedMedia','GET /media-playback/v1/contacts',cluster + ' v1.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')
		
		
def getActiveFlag(activeFlag):
    #Check if accessToken is empty or null
    if accessToken!="":

        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/x-www-form-urlencoded'}

        response=requests.get(baseURI + '/services/v16.0/workflow-data/list'+activeFlag,headers=header_param)
        #print response appropriately
        construct('Admin','Workflow ','GET /services/v16.0/workflow-data/list',cluster + ' v16.0',str(response.status_code), response.reason)
    else:
        #no token get one or handle error
        construct('error')		

def postWorkflowData():
		#Check if accessToken is empty or null
	if accessToken!="":
		#payload
		postWorkflowDataPayload = {
		"profile": {
		"ProfileName": "string",
		"Description": "string",
		"ProfileID": 0},
		"data": {
		"date": {
		"Value": [
		"string"],
		"Visible": "string",
		"Type": "string",
		"Ref": "string"}}}
		header_param = {'Authorization': 'bearer ' + accessToken,
		'content-Type': 'application/x-www-form-urlencoded'}
		response=requests.post(baseURI + '/services/v16.0/workflow-data', headers = header_param, data=postWorkflowDataPayload)
		#print response appropriately
		construct('Admin','Workflow ','POST /services/v16.0/workflow-data',cluster + ' v16.0',str(response.status_code), response.reason)
	else:
		#no token get one or handle error
		 construct('error')
		
def getWorkflowDataById(workflowDataId ):
    #Check if accessToken is empty or null
	if accessToken!="":

		header_param={
		'Authorization': 'bearer ' + accessToken,
		'content-Type': 'application/x-www-form-urlencoded'}

		response=requests.get(baseURI + '/services/v16.0/workflow-data/'+workflowDataId ,headers=header_param)
		#print response appropriately
		construct('Admin','Workflow ','GET /services/v16.0/workflow-data/ID',cluster + ' v16.0',str(response.status_code), response.reason)
	else:
		#no token get one or handle error
		construct('error')
		
		
def putWorkflowDataById(workflowDataId ):
	#Check if accessToken is empty or null
	if accessToken!="":
	#payload
		PutWorkflowDataByIDPayload = {
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
		#add all necessary headers
		header_param={
		'Authorization': 'bearer ' + accessToken,
		'content-Type': 'application/json; charset=UTF-8'}
		# Make http post request
		response = requests.put(baseURI+'/services/v16.0/workflow-data/'+workflowDataId ,headers=header_param,data=PutWorkflowDataByIDPayload) 
		#print response appropriately
		construct('Admin','Workflow','PUT /services/v16.0/workflow-data/ID',cluster + ' v16.0',str(response.status_code), response.reason)
	else:
		construct('error')
		
def putWorkflowDataByIdActivate(workflowDataId ):
    #Check if accessToken is empty or null
    if accessToken!="":
   
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.put(baseURI+'/services/v16.0/workflow-data/'+workflowDataId+'/activate' ,headers=header_param) 
    #print response appropriately
        construct('Admin','Workflow','PUT /services/v16.0/workflow-data/ID/activate',cluster + ' v16.0',str(response.status_code), response.reason)
    else:
        construct('error')

def putWorkflowDataByIdDeactivate(workflowDataId ):
    #Check if accessToken is empty or null
    if accessToken!="":
   
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.put(baseURI+'/services/v16.0/workflow-data/'+workflowDataId+'/deactivate' ,headers=header_param) 
    #print response appropriately
        construct('Admin','Workflow','PUT /services/v16.0/workflow-data/ID/deactivate',cluster + ' v16.0',str(response.status_code), response.reason)
    else:
        construct('error')

def getJobs():
    #Check if accessToken is empty or null
	if accessToken!="":

		header_param = {'Authorization': 'bearer ' + accessToken,
		'content-Type': 'application/x-www-form-urlencoded'}

		response=requests.post(CXone_baseURI + '/data-extraction/v1/jobs', headers = header_param)
		#print response appropriately
		construct('Data extraction','Extract Data','get /job',cluster + ' v1.0',str(response.status_code), response.reason)
	else:
		#no token get one or handle error
		 construct('error')

def postjobs():
		#Check if accessToken is empty or null
		if accessToken!="":
		#payload
			 PostjobsPayload = {
			 "entityName": "qm-workflows",
			 "version":"1",
			 "startDate":"2019-07-10",
			 "endDate":"2019-07-15"}

			 header_param = {'Authorization': 'bearer ' + accessToken,
			 'content-Type': 'application/x-www-form-urlencoded'}

			 response=requests.post(CXone_baseURI + '/data-extraction/v1/jobs', headers = header_param, data=PostjobsPayload)
			#print response appropriately
			 construct('Data extraction','Extract Data','POST /jobs',cluster + ' v1.0',str(response.status_code), response.reason)
		else:
			#no token get one or handle error
			 construct('error')

def getJobsByID(JobID):
    #Check if accessToken is empty or null
	if accessToken!="":

		header_param = {'Authorization': 'bearer ' + accessToken,
		'content-Type': 'application/x-www-form-urlencoded'}

		response=requests.post(CXone_baseURI + '/data-extraction/v1/jobs/'+ JobID, headers = header_param)
		#print response appropriately
		construct('Data extraction','Extract Data','get /job/jobid',cluster + ' v1.0',str(response.status_code), response.reason)
	else:
		#no token get one or handle error
		 construct('error')
		 
# importing the requests library 
import requests 

def getBusinessunitTimezone():

# api-endpoint 
#Give the specified url ,accessToken  
	Baseuri = "{Baseuri}"
	accessToken = "{accessToken}"

#Check if accessToken is empty or null
	if accessToken!="":

#add all necessary headers
		header_param = {'Authorization': 'bearer ' + '{accessToken}','content-Type': 'application/x-www-form-urlencoded','Accept': 'application/json, text/javascript, */*'}

# Make get http request
		response = requests.get(Baseuri + 'services/v17/business-unit/time-zones' , headers = header_param) 

#print response appropriately
		construct('Admin','General','get /business-unit/time-zones',cluster + ' v17.0',str(response.status_code), response.reason)
	else
		 construct('error')

# importing the requests library 
import requests 

def getSuppresedContacts():

# api-endpoint 
#Give the specified url ,accessToken  
	Baseuri = "{Baseuri}"
	accessToken = "{accessToken}"

#Check if accessToken is empty or null
	if accessToken!="":

#add all necessary headers
		header_param = {'Authorization': 'bearer ' + '{accessToken}','content-Type': 'application/x-www-form-urlencoded','Accept': 'application/json, text/javascript, */*'}

# Make get http request
		response = requests.get(Baseuri + 'services/v17/suppressed-contact' , headers = header_param) 

#print response appropriately
		construct('Admin','General','get /suppressed-contact',cluster + ' v17.0',str(response.status_code), response.reason)

	else:
		construct('error')

def PostSuppresedContacts():
    #Check if accessToken is empty or null
    if accessToken!="":
    #payload
        PostSuppresedContactsPayload = {
        "startDate": "string",
        "endDate": "string",
        "value": "string",
        "skills":"string"}
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v17/suppressed-contact', headers = header_param,data=PostSuppresedContactsPayload) 
        construct('Admin','General','Post /suppressed-contact',cluster + ' v17.0',str(response.status_code), response.reason)
    #print response appropriately
    else:
        construct('error')
		
def putbusinessunitTimezone( ):
    #Check if accessToken is empty or null
	if accessToken!="":
		#payload
		putbusinessunitTimezoneIDPayload = {
		"timezones": "string",
		"items": "string"}
		#add all necessary headers
		header_param={
		'Authorization': 'bearer ' + accessToken,
		'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
		response = requests.put(baseURI+'/services/v17/business-unit/time-zones' ,headers=header_param,data=putbusinessunitTimezoneIDPayload) 
    #print response appropriately
		construct('Admin','General','Put /business-unit/time-zones',cluster + ' v17.0',str(response.status_code), response.reason)
	else:
		construct('error')
		

def PutSkillIDParameterTimezone(skillId):
# api-endpoint 
#Give the specified url ,accessToken
	Baseuri = "{Baseuri}"
	accessToken = "{accessToken}"
#Check if accessToken is empty or null
	if accessToken!="":
#Give necessary parameters for http request
		payload={'timezone': [
		{
		'timeZoneSettings': 'string'}]}
#add all necessary headers
		header_param = {'Authorization': 'bearer ' + accessToken,'content-Type': 'application/x-www-form-urlencoded','Accept': 'application/json, text/javascript, */*'}
# Make http put request
		response = requests.put(Baseuri + 'services/v17/skills/' + skillId + '/parameters/time-zones' , headers = header_param, data=payload) 
#print response appropriately
		construct('Admin','Skill','Put /parameters/time-zones',cluster + ' v17.0',str(response.status_code), response.reason)
	else:
		construct('error')
		
def getTimeZoneBySkillId(skillId):

# api-endpoint
#Give the specified url ,accessToken
	Baseuri = "{Baseuri}"
	accessToken = "{accessToken}"

#Check if accessToken is empty or null
	if accessToken!="":

#add all necessary headers
		header_param = {'Authorization': 'bearer ' + accessToken,'content-Type': 'application/x-www-form-urlencoded','Accept': 'application/json, text/javascript, */*'}

# Make get http request
		response = requests.get(Baseuri + 'services/v17/skills/' + skillId + '/parameters/time-zones' , headers = header_param)

#print response appropriately
		construct('Admin','Skill','Get /parameters/time-zones',cluster + ' v17.0',str(response.status_code), response.reason)
	else:
		construct('error')
		 construct('error')

def deleteSuppressedContactBySuppressedContactId(suppressedContactId):
# api-endpoint
#Give the specified url ,accessToken
	Baseuri = "{Baseuri}"
	accessToken = "{accessToken}"
#Check if accessToken is empty or null
	if accessToken!="":
#add all necessary headers
		header_param = {'Authorization': 'bearer ' + accessToken,'content-Type': 'application/x-www-form-urlencoded','Accept': 'application/json, text/javascript, */*'}
# Make http delete request
		response = requests.delete(Baseuri + 'services/{version}/suppressed-contact/' + {suppressedContactId} , headers = header_param)
#print response appropriately
		print (response)
	else:
		#no token get one or handle error

def getSuppressedContactBySuppressedContactId(suppressedContactId):

# api-endpoint 
#Give the specified url ,accessToken  
	Baseuri = "{Baseuri}"
	accessToken = "{accessToken}"

#Check if accessToken is empty or null
	if accessToken!="":

#add all necessary headers
		header_param = {'Authorization': 'bearer ' + accessToken,'content-Type': 'application/x-www-form-urlencoded','Accept': 'application/json, text/javascript, */*'}

# Make get http request
		response = requests.get(Baseuri + 'services/{version}/suppressed-contact/' + {suppressedContactId} , headers = header_param) 

#print response appropriately
		print (response)

	else:
		#no token get one or handle error

def putSuppressedContactBySuppressedContactId(suppressedContactId):
# api-endpoint 
#Give the specified url ,accessToken
	Baseuri = "{Baseuri}"
	accessToken = "{accessToken}"
#Check if accessToken is empty or null
	if accessToken!="":
#Give necessary parameters for http request
		payload={ 'startDate': 'date-time',
		'endDate': 'date-time',
		'value': 'string',
		'skills;': 'string'}
#add all necessary headers
		header_param = {'Authorization': 'bearer ' + accessToken,'content-Type': 'application/x-www-form-urlencoded','Accept': 'application/json, text/javascript, */*'}
# Make http put request
		response = requests.put(Baseuri +  'services/{version}/suppressed-contact/' + {suppressedContactId} , headers = header_param, data=payload) 
#print response appropriately
		print (response)
	else:
		#no token get one or handle error

def getContactsByIdHierachy(contactID):
    #Check if accessToken is empty or null
    if accessToken!="":
         getContactsByIdHierachyPayload = {
        'contactID':contactID}
        header_param = {'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/x-www-form-urlencoded'}
        response=requests.get(baseURI + 'services/{version}/contacts/{contactId}/hierarchy',headers=header_param,params=getContactsByIdHierachyPayload)
        #print response appropriately
    else:
        #no token get one or handle error		
		 
		 construct('error')
		 
# importing the requests library 
import requests 

def getBusinessunitTimezone():

# api-endpoint 
#Give the specified url ,accessToken  
	Baseuri = "{Baseuri}"
	accessToken = "{accessToken}"

#Check if accessToken is empty or null
	if accessToken!="":

#add all necessary headers
		header_param = {'Authorization': 'bearer ' + '{accessToken}','content-Type': 'application/x-www-form-urlencoded','Accept': 'application/json, text/javascript, */*'}

# Make get http request
		response = requests.get(Baseuri + 'services/v17/business-unit/time-zones' , headers = header_param) 

#print response appropriately
		construct('Admin','General','get /business-unit/time-zones',cluster + ' v17.0',str(response.status_code), response.reason)
	else
		 construct('error')

# importing the requests library 
import requests 

def getSuppresedContacts():

# api-endpoint 
#Give the specified url ,accessToken  
	Baseuri = "{Baseuri}"
	accessToken = "{accessToken}"

#Check if accessToken is empty or null
	if accessToken!="":

#add all necessary headers
		header_param = {'Authorization': 'bearer ' + '{accessToken}','content-Type': 'application/x-www-form-urlencoded','Accept': 'application/json, text/javascript, */*'}

# Make get http request
		response = requests.get(Baseuri + 'services/v17/suppressed-contact' , headers = header_param) 

#print response appropriately
		construct('Admin','General','get /suppressed-contact',cluster + ' v17.0',str(response.status_code), response.reason)

	else:
		construct('error')

def PostSuppresedContacts():
    #Check if accessToken is empty or null
    if accessToken!="":
    #payload
        PostSuppresedContactsPayload = {
        "startDate": "string",
        "endDate": "string",
        "value": "string",
        "skills":"string"}
    #add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
        response = requests.post(baseURI+'/services/v17/suppressed-contact', headers = header_param,data=PostSuppresedContactsPayload) 
        construct('Admin','General','Post /suppressed-contact',cluster + ' v17.0',str(response.status_code), response.reason)
    #print response appropriately
    else:
        construct('error')
		
def putbusinessunitTimezone( ):
    #Check if accessToken is empty or null
	if accessToken!="":
		#payload
		putbusinessunitTimezoneIDPayload = {
		"timezones": "string",
		"items": "string"}
		#add all necessary headers
		header_param={
		'Authorization': 'bearer ' + accessToken,
		'content-Type': 'application/json; charset=UTF-8'}
    # Make http post request
		response = requests.put(baseURI+'/services/v17/business-unit/time-zones' ,headers=header_param,data=putbusinessunitTimezoneIDPayload) 
    #print response appropriately
		construct('Admin','General','Put /business-unit/time-zones',cluster + ' v17.0',str(response.status_code), response.reason)
	else:
		construct('error')
		

def PutSkillIDParameterTimezone(skillId):
# api-endpoint 
#Give the specified url ,accessToken
	Baseuri = "{Baseuri}"
	accessToken = "{accessToken}"
#Check if accessToken is empty or null
	if accessToken!="":
#Give necessary parameters for http request
		payload={'timezone': [
		{
		'timeZoneSettings': 'string'}]}
#add all necessary headers
		header_param = {'Authorization': 'bearer ' + accessToken,'content-Type': 'application/x-www-form-urlencoded','Accept': 'application/json, text/javascript, */*'}
# Make http put request
		response = requests.put(Baseuri + 'services/v17/skills/' + skillId + '/parameters/time-zones' , headers = header_param, data=payload) 
#print response appropriately
		construct('Admin','Skill','Put /parameters/time-zones',cluster + ' v17.0',str(response.status_code), response.reason)
	else:
		construct('error')
		
def getTimeZoneBySkillId(skillId):

# api-endpoint
#Give the specified url ,accessToken
	Baseuri = "{Baseuri}"
	accessToken = "{accessToken}"

#Check if accessToken is empty or null
	if accessToken!="":

#add all necessary headers
		header_param = {'Authorization': 'bearer ' + accessToken,'content-Type': 'application/x-www-form-urlencoded','Accept': 'application/json, text/javascript, */*'}

# Make get http request
		response = requests.get(Baseuri + 'services/v17/skills/' + skillId + '/parameters/time-zones' , headers = header_param)

#print response appropriately
		construct('Admin','Skill','Get /parameters/time-zones',cluster + ' v17.0',str(response.status_code), response.reason)
	else:
		construct('error')
		
def deleteSkillBySkillIdAgentId(skillId,agentId):

# api-endpoint 
#Give the specified url ,accessToken
	Baseuri = "{Baseuri}"
	accessToken = "{accessToken}"

#Check if accessToken is empty or null
	if accessToken!="":

#add all necessary headers
    		header_param = {'Authorization': 'bearer ' + accessToken,'content-Type': 'application/x-www-form-urlencoded','Accept': 'application/json, text/javascript, */*'}

# Make http delete request
		response = requests.delete(Baseuri + 'services/{version}/skills/' + {skillId} + '/agents/' + {agentId} , headers = header_param) 

#print response appropriately
		construct('Admin','Skill','Delete /skills/{skillId}/agents/{agentId}',cluster + ' v17.0',str(response.status_code), response.reason)
	else:
		construct('error')
		
def getScripts():

# api-endpoint  
#Give the specified url ,accessToken
	Baseuri = "{Baseuri}"
	accessToken = "{accessToken}"

#Check if accessToken is empty or null
	if accessToken!="":

#Give necessary parameters for http request
		payload={'scriptPath': 'string',
		'scriptId': 'integer'}

#add all necessary headers
		header_param = {'Authorization': 'bearer ' + accessToken,'content-Type': 'application/x-www-form-urlencoded','Accept': 'application/json, text/javascript, */*'}

# Make get http request
		response = requests.get(Baseuri + 'services/{version}/scripts' , headers = header_param, params=payload) 

#print response appropriately
		print (response)
		construct('Admin','General','Get /scripts',cluster + ' v18.0',str(response.status_code), response.reason)
	else:
		#no token get one or handle error
				construct('error')

def postScript():

# api-endpoint 
#Give the specified url ,accessToken 
	Baseuri = "{Baseuri}"
	accessToken = "{accessToken}"

#Check if accessToken is empty or null
	if accessToken!="":

#Give necessary parameters for http request
		payload={'scriptPath': 'string - required',
		'body': 'string - required'}

#add all necessary headers
		header_param = {'Authorization': 'bearer ' + accessToken,'content-Type': 'application/x-www-form-urlencoded','Accept': 'application/json, text/javascript, */*'}

# Make http post request
		response = requests.post(Baseuri + 'services/{version}/scripts', headers = header_param, data=payload) 

#print response appropriately
		print (response)
		construct('Admin','General','Post /scripts',cluster + ' v18.0',str(response.status_code), response.reason)

	else:
		#no token get one or handle error
		construct('error')
		
def PutupdateScript(scriptPath, lockScript ):
# api-endpoint 
#Give the specified url ,accessToken
	Baseuri = "{Baseuri}"
	accessToken = "{accessToken}"

#Check if accessToken is empty or null
	if accessToken!="":

#Give necessary parameters for http request
		payload={"scriptPath":  scriptPath,
		"lockScript":  lockScript}

#add all necessary headers
		header_param = {'Authorization': 'bearer ' + accessToken,'content-Type': 'application/x-www-form-urlencoded','Accept': 'application/json, text/javascript, */*'}

# Make http put request
		response = requests.put(Baseuri +  '/services/{version}/scripts' , headers = header_param, data=payload) 

#print response appropriately
		construct('Admin','General','Put /scripts',cluster + ' v18.0',str(response.status_code), response.reason)
		print (response)
	else:
		#no token get one or handle error
		construct('error')

def postScriptKick():

# api-endpoint 
#Give the specified url ,accessToken 
	Baseuri = "{Baseuri}"
	accessToken = "{accessToken}"

#Check if accessToken is empty or null
	if accessToken!="":

#Give necessary parameters for http request
		payload={'scriptPath': 'string - required'}

#add all necessary headers
		header_param = {'Authorization': 'bearer ' + accessToken,'content-Type': 'application/x-www-form-urlencoded','Accept': 'application/json, text/javascript, */*'}

# Make http post request
		response = requests.post(Baseuri + 'services/{version}/scripts/kick', headers = header_param, data=payload) 

#print response appropriately
		construct('Admin','General','Post/scripts/kick',cluster + ' v18.0',str(response.status_code), response.reason)
		print (response)
	else:
		#no token get one or handle error
		construct('error')
		
def getScriptsHistoryByName():

# api-endpoint  
#Give the specified url ,accessToken
	Baseuri = "{Baseuri}"
	accessToken = "{accessToken}"

#Check if accessToken is empty or null
	if accessToken!="":

#Give necessary parameters for http request
		payload={'scriptPath': 'string'}

#add all necessary headers
		header_param = {'Authorization': 'bearer ' + accessToken,'content-Type': 'application/x-www-form-urlencoded','Accept': 'application/json, text/javascript, */*'}

# Make get http request
		response = requests.get(Baseuri + 'services/{version}/scripts/historyByName/{scriptPath}' , headers = header_param, params=payload) 

#print response appropriately
		construct('Admin','General','Post/scripts/kick',cluster + ' v18.0',str(response.status_code), response.reason)
		print (response)
	else:
		#no token get one or handle error		
		construct('error')
		
def PutSkillListManagement(skillId):
#Check if accessToken is empty or null
    if accessToken!="":
 
 #Give necessary parameters for http request
        payload={
        "displayField1Name": "string",
        "displayField2Name": "string",
        "listOrderingOptions": [{
		"orderType": "string",
        "direction": "string"}]}

#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http put request
        response = requests.put(baseURI +'/services/v18.0/skills/' + skillId + '/parameters/list-management'  , headers = header_param,data=payload) 
 
#print response appropriately
        construct('Admin','Skills','PUT /skills/{skillId}/parameters/list-management',cluster + ' v18.0',str(response.status_code), response.reason)
 
    else:
        construct('error') 
		
		
def getSkillParameters():

# api-endpoint  
#Give the specified url ,accessToken
	Baseuri = "{Baseuri}"
	accessToken = "{accessToken}"

#Check if accessToken is empty or null
	if accessToken!="":


#add all necessary headers
		header_param = {'Authorization': 'bearer ' + accessToken,'content-Type': 'application/x-www-form-urlencoded','Accept': 'application/json, text/javascript, */*'}

# Make get http request
		response = requests.get(Baseuri + 'services/{version}/skills/parameters' , headers = header_param) 

#print response appropriately
		construct('Admin','General','Post/skills/parameters',cluster + ' v18.0',str(response.status_code), response.reason)
		print (response)
	else:
		#no token get one or handle error		
		construct('error')		
		
		
def PutSkillCadenceSettings(skillId):
#Check if accessToken is empty or null
    if accessToken!="":
 
 #Give necessary parameters for http request
        payload={
        "attemptMode": "string",
        "recordRequestMode": "string",
        "destinationRetryRestMinutes": 0,
		"maximumAttempts": [{
        "fieldName": "string"
		"attempts": 0 }],
        "cadences": [{
        "fieldName": "string"
		"attempts": 0,
        "timeConstraints": {
		"weekdayTimeConstraints": [{
        "startTime": "string",
        "endTime": "string" }],
		"weekendTimeConstraints": [{
        "startTime": "string",
        "endTime": "string" }]}}]}

#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http put request
        response = requests.put(baseURI +'/services/v18.0/skills/' + skillId + '/parameters/cadence-settings'  , headers = header_param,data=payload) 
 
#print response appropriately
        construct('Admin','Skills','PUT /skills/{skillId}/parameters/cadence-settings',cluster + ' v18.0',str(response.status_code), response.reason)
 
    else:
        construct('error') 
		
def getSkillSkillIdParameters(skillId):

# api-endpoint  
#Give the specified url ,accessToken
	Baseuri = "{Baseuri}"
	accessToken = "{accessToken}"

#Check if accessToken is empty or null
	if accessToken!="":


#add all necessary headers
		header_param = {'Authorization': 'bearer ' + accessToken,'content-Type': 'application/x-www-form-urlencoded','Accept': 'application/json, text/javascript, */*'}

# Make get http request
		response = requests.get(Baseuri + 'services/{version}/skills/'+skillId+'parameters' , headers = header_param) 

#print response appropriately
		construct('Admin','General','Post/skills/skillId/parameters',cluster + ' v18.0',str(response.status_code), response.reason)
		print (response)
	else:
		#no token get one or handle error		
		construct('error')
		
def postSetDisposition(contactId):
		#Check if accessToken is empty or null
		if accessToken!="":
		#payload
			 postSetDispositionPayload = {
			 "dispositionInfo": {
			 "Skill": "1007",
			 "DispositionCode": "1",
			 "CallbackNumber": "",
			 "CallbackTime": "",
			 "notes": "",
			 "CommitmentAmount": ""}}

			 header_param = {'Authorization': 'bearer ' + accessToken,
			 'content-Type': 'application/x-www-form-urlencoded'}

			 response=requests.post(CXone_baseURI + '/contacts/'+contactId+'/set-disposition', headers = header_param, data=postSetDispositionPayload)
			#print response appropriately
			 construct('Data extraction','Extract Data','POST /contacts/{contactId}/set-disposition',cluster + ' v18.0',str(response.status_code), response.reason)
		else:
			#no token get one or handle error
			 construct('error')

def getBusinessunitOutboundRoutes():

# api-endpoint  
#Give the specified url ,accessToken
	Baseuri = "{Baseuri}"
	accessToken = "{accessToken}"

#Check if accessToken is empty or null
	if accessToken!="":


#add all necessary headers
		header_param = {'Authorization': 'bearer ' + accessToken,'content-Type': 'application/x-www-form-urlencoded','Accept': 'application/json, text/javascript, */*'}

# Make get http request
		response = requests.get(Baseuri + 'services/{version}/business-unit/outbound-routes' , headers = header_param) 

#print response appropriately
		construct('Admin','General','Post/business-unit/outbound-routes',cluster + ' v18.0',str(response.status_code), response.reason)
		print (response)
	else:
		#no token get one or handle error		
		construct('error')

def getclientdata():

# api-endpoint  
#Give the specified url ,accessToken
	Baseuri = "{Baseuri}"
	accessToken = "{accessToken}"

#Check if accessToken is empty or null
	if accessToken!="":


#add all necessary headers
		header_param = {'Authorization': 'bearer ' + accessToken,'content-Type': 'application/x-www-form-urlencoded','Accept': 'application/json, text/javascript, */*'}

# Make get http request
		response = requests.get(Baseuri + 'services/{version}/agents/client-data' , headers = header_param) 

#print response appropriately
		construct('Realtime','Realtime','Get /agents/client-data',cluster + ' v19.0',str(response.status_code), response.reason)
		print (response)
	else:
		#no token get one or handle error		
		construct('error')

def putclientdata():
#Check if accessToken is empty or null
    if accessToken!="":
 
 #Give necessary parameters for http request
        payload={
		"agentId": "",
		"dataSet": "{ \"settings\": \"true\" }"
}

#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http put request
        response = requests.put(baseURI +'/services/v19.0/agents/client-data'  , headers = header_param,data=payload) 
 
#print response appropriately
        construct('Realtime','Realtime','PUT /agents/client-data',cluster + ' v19.0',str(response.status_code), response.reason)
 
    else:
        construct('error') 
		
def postsmsoutbound(sessionId):
#Check if accessToken is empty or null
	if accessToken!="":
#payload
		postsmsoutboundPayload = {
		"phoneNumber": "",
		"skillId": 1000,
		"parentContactId": null
}

		header_param = {'Authorization': 'bearer ' + accessToken,
		'content-Type': 'application/x-www-form-urlencoded'}

	response=requests.post(CXone_baseURI + '/agent-sessions/'+ sessionId +'/interactions/sms-outbound', headers = header_param, data=postsmsoutboundPayload)
#print response appropriately
		construct('Agent','Sessions','POST /agent-sessions/{sessionId}/interactions/sms-outbound',cluster + ' v18.0',str(response.status_code), response.reason)
	else:
#no token get one or handle error
		construct('error')
			 
def removeprospects(sourceName):
# api-endpoint 
#Give the specified url ,accessToken
	Baseuri = "{Baseuri}"
	accessToken = "{accessToken}"

#Check if accessToken is empty or null
	if accessToken!="":

#add all necessary headers
    		header_param = {'Authorization': 'bearer ' + accessToken,'content-Type': 'application/x-www-form-urlencoded','Accept': 'application/json, text/javascript, */*'}

# Make http delete request
		response = requests.delete(Baseuri + 'services/{version}/lists/call-lists/' + sourceName + '/removeProspects' , headers = header_param) 

#print response appropriately
		construct('Admin','Lists','Delete /lists/call-lists/{sourceName}/removeProspects',cluster + ' v19.0',str(response.status_code), response.reason)
	else:
		construct('error')
		
def putpersistentcontacts(contactId):
#Check if accessToken is empty or null
    if accessToken!="":
 
 #Give necessary parameters for http request
        payload={
		"persistentContact": {
		"SkillId": 0,
		"TargetAgentId": 0,
		"InitialPriority": 0,
		"Acceleration": 0,
		"MaxPriority": 0
		}
	}

#add all necessary headers
        header_param={
        'Authorization': 'bearer ' + accessToken,
        'content-Type': 'application/json; charset=UTF-8'}
 
# Make http put request
        response = requests.put(baseURI +'/services/v19.0/persistent-contacts/'+ contactId  , headers = header_param,data=payload) 
 
#print response appropriately
        construct('Admin','Contacts','PUT /persistent-contacts/{contactId}',cluster + ' v19.0',str(response.status_code), response.reason)
 
    else:
        construct('error') 
		
def getagentsettings(agentId):

# api-endpoint  
#Give the specified url ,accessToken
	Baseuri = "{Baseuri}"
	accessToken = "{accessToken}"

#Check if accessToken is empty or null
	if accessToken!="":


#add all necessary headers
		header_param = {'Authorization': 'bearer ' + accessToken,'content-Type': 'application/x-www-form-urlencoded','Accept': 'application/json, text/javascript, */*'}

# Make get http request
		response = requests.get(Baseuri + 'services/{version}/agents/' + agentId + '/agent-settings' , headers = header_param) 

#print response appropriately
		construct('Admin','Agents','Get /agents{agentId}/agent-settings',cluster + ' v19.0',str(response.status_code), response.reason)
		print (response)
	else:
		#no token get one or handle error		
		construct('error')
		
def getscriptsbyId(scriptId):

# api-endpoint  
#Give the specified url ,accessToken
	Baseuri = "{Baseuri}"
	accessToken = "{accessToken}"

#Check if accessToken is empty or null
	if accessToken!="":


#add all necessary headers
		header_param = {'Authorization': 'bearer ' + accessToken,'content-Type': 'application/x-www-form-urlencoded','Accept': 'application/json, text/javascript, */*'}

# Make get http request
		response = requests.get(Baseuri + 'services/{version}/scripts/' + scriptId , headers = header_param) 

#print response appropriately
		construct('Admin','General','Get /scripts/{scriptId}',cluster + ' v19.0',str(response.status_code), response.reason)
		print (response)
	else:
		#no token get one or handle error		
		construct('error')
		
def getscriptssearch():

# api-endpoint  
#Give the specified url ,accessToken
	Baseuri = "{Baseuri}"
	accessToken = "{accessToken}"

#Check if accessToken is empty or null
	if accessToken!="":


#add all necessary headers
		header_param = {'Authorization': 'bearer ' + accessToken,'content-Type': 'application/x-www-form-urlencoded','Accept': 'application/json, text/javascript, */*'}

# Make get http request
		response = requests.get(Baseuri + 'services/{version}/scripts/search' , headers = header_param) 

#print response appropriately
		construct('Admin','General','Get /scripts/search',cluster + ' v19.0',str(response.status_code), response.reason)
		print (response)
	else:
		#no token get one or handle error		
		construct('error')
		
def deletescripts():
# api-endpoint 
#Give the specified url ,accessToken
	Baseuri = "{Baseuri}"
	accessToken = "{accessToken}"

#Check if accessToken is empty or null
	if accessToken!="":

#add all necessary headers
    		header_param = {'Authorization': 'bearer ' + accessToken,'content-Type': 'application/x-www-form-urlencoded','Accept': 'application/json, text/javascript, */*'}

# Make http delete request
		response = requests.delete(Baseuri + 'services/{version}/scripts' , headers = header_param) 

#print response appropriately
		construct('Admin','General','Delete /scripts',cluster + ' v19.0',str(response.status_code), response.reason)
	else:
		construct('error')
		
def posttransformnumbers(agentpatternId):
# api-endpoint 
#Give the specified url ,accessToken
	Baseuri = "{Baseuri}"
	accessToken = "{accessToken}"

#Check if accessToken is empty or null
	if accessToken!="":

#add all necessary headers
    		header_param = {'Authorization': 'bearer ' + accessToken,'content-Type': 'application/x-www-form-urlencoded','Accept': 'application/json, text/javascript, */*'}

# Make http delete request
		response = requests.delete(Baseuri + 'services/{version}/agent-patterns/' + agentpatternId + '/transform-phonenumbers' , headers = header_param) 

#print response appropriately
		construct('Admin','Lists','Delete /agent-patterns/{agentpatternId}/transform-phonenumbers',cluster + ' v19.0',str(response.status_code), response.reason)
	else:
		construct('error')