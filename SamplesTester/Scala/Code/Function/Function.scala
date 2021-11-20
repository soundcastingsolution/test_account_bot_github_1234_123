package scalaj.http

//Agent --> Chat Request

def postSessionIdInteractionsContactIdtyping(sessionId:Int, contactId:Int,isTyping:Boolean,isTextEntered:Boolean)
{
PostChatTypingPayload -> {
"isTyping"-> isTyping,
"isTextEntered"-> isTextEntered}

val response=Http(baseURI + "/services/v11.0/agent-sessions/" + sessionId + "/interactions/"+ contactId +"/typing").postdata{PostChatTypingPayload}.headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString

if(assertEquals(200, response.code))
	{
		//Process success action
		ConstructArray("Agents","Chat","POST /agent-sessions/{sessionId}/interactions/{contactId}/typing","SC1-v11.0",response.code + "->" + response.statusLine)
		return response
}
else{
		//Process error action
		ConstructArray("Agents","Chat","POST /agent-sessions/{sessionId}/interactions/{contactId}/typing","SC1-v11.0",response.code + "->" + response.statusLine)
		false
}
}

def posttAgentSessionSessionIdInteractionAddEmail(sessionId:Int)
{
val response=Http(baseURI + "/services/v11.0/agent-sessions/"+ sessionId + "/interactions/add-email").postdata("").headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString

if(assertEquals(200, response.code)){
	//Process success action
	ConstructArray("Agents","Email","POST /agent-sessions/{sessionId}/interactions/add-email","SC1-v11.0",response.code + "->" + response.statusLine)
	return response
}
else{
		//Process error action
		ConstructArray("Agents","Email","POST /agent-sessions/{sessionId}/interactions/add-email","SC1-v11.0",response.code + "->" + response.statusLine)
		false
}
}

def posttAgentSessionIdInteractionConatactIdParkemail(sessionId:Int,ConatctId:Int,toAddress:String,fromAddress:String,ccAddress:String,bccAddress:String,subject:String, bodyHtml:String,attachments:String,attachmentNames:String,isDraft:Boolean,originalAttachmentNames:String)
{
PostEmailParkPayload ->{
	"toAddress"-> toAddress,
	"fromAddress"-> fromAddress,
	"ccAddress"-> ccAddress,
	"bccAddress"-> bccAddress,
	"subject"-> subject,
	"bodyHtml"-> bodyHtml,
	"attachments"-> attachments,
	"attachmentNames"-> attachmentNames,
	"isDraft"-> isDraft,
	"originalAttachmentNames"-> originalAttachmentNames}

val response=Http(baseURI + "/services/v11.0/agent-sessions/" + sessionId + "/interactions/" + ConatctId+ "/email-park").postdata(PostEmailParkPayload).headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString

if(assertEquals(200, response.code)){
	//Process success action
	ConstructArray("Agents","Email","POST /agent-sessions/{sessionId}/interactions/{contactId}/email-park","SC1-v11.0",response.code + "->" + response.statusLine)
	return response
}
else{
		//Process error action
		ConstructArray("Agents","Email","POST /agent-sessions/{sessionId}/interactions/{contactId}/email-park","SC1-v11.0",response.code + "->" + response.statusLine)
		false
}
}

def posttAgentSessionIdInteractionConatactIdUnparkemail( sessionId:Int, ConatctId:Int, isImmediate:Boolean)
{
PostEmailunParkPayload -> { "isImmediate"-> isImmediate }

val response=Http(baseURI + "/services/v11.0/agent-sessions/" + sessionId + "/interactions/" + ConatctId+ "/email-unpark").postdata(PostEmailunParkPayload).headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString

if(assertEquals(200, response.code)){
	//Process success action
	ConstructArray("Agents","Email","POST /agent-sessions/{sessionId}/interactions/{contactId}/email-unpark","SC1-v11.0",response.code + "->" + response.statusLine)
	return response
}
else{
		//Process error action
		ConstructArray("Agents","Email","POST /agent-sessions/{sessionId}/interactions/{contactId}/email-unpark","SC1-v11.0",response.code + "->" + response.statusLine)
		false
}
}

def posttAgentSessionIdInteractionConatactIdPreview( sessionId:Int, ConatctId:Int)
{
val response=Http(baseURI + "/services/v11.0/agent-sessions/" + sessionId + "/interactions/" + ConatctId+ "/email-preview").postdata("").headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString

if(assertEquals(200, response.code)){
	//Process success action
	ConstructArray("Agents","Email","POST /agent-sessions/{sessionId}/interactions/{contactId}/email-preview","SC1-v11.0",response.code + "->" + response.statusLine)
	return response
}
else{
		//Process error action
		ConstructArray("Agents","Email","POST /agent-sessions/{sessionId}/interactions/{contactId}/email-preview","SC1-v11.0",response.code + "->" + response.statusLine)
		false
}
}

def posttAgentSessionIdInteractionConatactIdEmailRestore( sessionId:Int, ConatctId:Int)
{
val response=Http(baseURI + "/services/v11.0/agent-sessions/" + sessionId + "/interactions/" + ConatctId+ "/email-restore").postdata("").headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString

if(assertEquals(200, response.code)){
	//Process success action
	ConstructArray("Agents","Email","POST /agent-sessions/{sessionId}/interactions/{contactId}/email-restore","SC1-v11.0",response.code + "->" + response.statusLine)
	return response
}
else{
		//Process error action
		ConstructArray("Agents","Email","POST /agent-sessions/{sessionId}/interactions/{contactId}/email-restore","SC1-v11.0",response.code + "->" + response.statusLine)
		false
}
}

// Agent Personal Connection

def posttAgentSessionIdInteractionConatactIdSnooze( sessionId:Int, ConatctId:Int)
{
val response=Http(baseURI + "/services/v11.0/agent-sessions/" + sessionId + "/interactions/" + ConatctId+ "/snooze").postdata("").headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString

if(assertEquals(200, response.code)){
	//Process success action
	ConstructArray("Agents","Callbacks","POST /agent-sessions/{sessionId}/interactions/{contactId}/snooze","SC1-v11.0",response.code + "->" + response.statusLine)
	return response
}
else{
		//Process error action
		ConstructArray("Agents","Callbacks","POST /agent-sessions/{sessionId}/interactions/{contactId}/snooze","SC1-v11.0",response.code + "->" + response.statusLine)
		false
}
}

// Agent --> ScheduledCallbacks

def DialCallback( sessionId:Int, callbackId:Int)
{
val response=Http(baseURI + "/services/v11.0/agent-sessions/" + sessionId + "/interactions/"+ callbackId + "/dial").postdata("").headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString

if(assertEquals(200, response.code)){
	//Process success action
	ConstructArray("Agents","Callbacks","POST /agent-sessions/{sessionId}/interactions/{callbackId}/dial","SC1-v11.0",response.code + "->" + response.statusLine)
	return response
}
else{
		//Process error action
		ConstructArray("Agents","Callbacks","POST /agent-sessions/{sessionId}/interactions/{callbackId}/dial","SC1-v11.0",response.code + "->" + response.statusLine)
		false
}
}

def RescheduleCallback( sessionId, callbackId,rescheduleDate)
{
PostRescheduleCallbackPayload ->{"rescheduleDate" ->  rescheduleDate }

val response=Http(baseURI + "/services/v11.0/agent-sessions/"+ sessionId +"/interactions/" + callbackId + "/reschedule").postdata(PostRescheduleCallbackPayload).headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString

if(assertEquals(200, response.code)){
	//Process success action
	ConstructArray("Agents","Callbacks","POST /agent-sessions/{sessionId}/interactions/{callbackId}/reschedule","SC1-v11.0",response.code + "->" + response.statusLine)
	return response
}
else{
		//Process error action
		ConstructArray("Agents","Callbacks","POST /agent-sessions/{sessionId}/interactions/{callbackId}/reschedule","SC1-v11.0",response.code + "->" + response.statusLine)
		false
}
}

def CancelCallback( sessionId:Int, callbackId:Int,notes:String)
{
PostCancelCallbackPayload -> {"notes" ->  notes }

val response=Http(baseURI + "/services/v11.0/agent-sessions/"+ sessionId +"/interactions/" + callbackId + "/cancel").postdata(PostCancelCallbackPayload).headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString

if(assertEquals(200, response.code)){
	//Process success action
	ConstructArray("Agents","Callbacks","POST /agent-sessions/{sessionid}/interactions/{callbackid}/cancel","SC1-v11.0",response.code + "->" + response.statusLine)
	return response
}
else{
		//Process error action
		ConstructArray("Agents","Callbacks","POST /agent-sessions/{sessionid}/interactions/{callbackid}/cancel","SC1-v11.0",response.code + "->" + response.statusLine)
		false
}
}

// Admin Agent Session

def posttAgentSessionIdAddcontact(sessionId:Int, chat:Boolean, email:Boolean, workitem:Boolean)
{
PostAddcontacPayload -> {
	"chat"-> chat,
	"email"-> email,
	"workitem"-> workitem}

val response=Http(baseURI + "/services/V11.0/agent-sessions/" + sessionId + "/add-contact").postdata(PostAddcontacPayload).headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString

if(assertEquals(200, response.code)){
	//Process success action
	ConstructArray("Agents","Sessions","POST /agent-sessions/{sessionId}/add-contact","SC1-v11.0",response.code + "->" + response.statusLine)
	return response
}
else{
		//Process error action
		ConstructArray("Agents","Sessions","POST /agent-sessions/{sessionId}/add-contact","SC1-v11.0",response.code + "->" + response.statusLine)
		false
}
}

def posttAgentSessionIdInteractionConatactIdActivate(sessionId, ConatctId)
{
val response=Http(baseURI + "/services/v11.0/agent-sessions/" + sessionId + "/interactions/" + ConatctId + "/activate").postdata("").headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString

if(assertEquals(200, response.code)){
	//Process success action
	ConstructArray("Agents","Sessions","POST /agent-sessions/{sessionId}/interactions/{contactId}/activate","SC1-v11.0",response.code + "->" + response.statusLine)
	return response
}
else{
		//Process error action
		ConstructArray("Agents","Sessions","POST /agent-sessions/{sessionId}/interactions/{contactId}/activate","SC1-v11.0",response.code + "->" + response.statusLine)
		false
}
}

def GetSmstranscripts( startDate:Int, endDate:Int, transportCode:Int , agentId:String, top:String,skip:String,orderby:String)
{
val response=Http(baseURI + "/services/v11.0/contacts/sms-transcripts" + "?top=" + top + "&skip=" + skip+ 
	"&orderby=" + orderby + "&startDate=" + startDate + "&endDate="+ endDate + "&transportCode=" + transportCode).headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString

if(assertEquals(200, response.code)){
	//Process success action
	ConstructArray("Reports","Reports","GET /contacts/sms-transcripts","SC1-v11.0",response.code + "->" + response.statusLine)
	return response
}
else{
		//Process error action
		ConstructArray("Reports","Reports","GET /contacts/sms-transcripts","SC1-v11.0",response.code + "->" + response.statusLine)
		false
}
}

def GetSmstranscriptsbyContactid( contactId:Int,startDate:Int, endDate:Int, transportCode:Int, top:String,skip:String)
{
val response=Http(baseURI + "/services/v10.0/contacts/" + contactId + "/sms-transcripts" + "?top=" + top + "&skip=" + skip + "&startDate=" + startDate + "&endDate="+ endDate + "&transportCode=" + transportCode).headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString

if(assertEquals(200, response.code)){
	//Process success action
	ConstructArray("Reports","Reports","GET /contacts/{contactid}/sms-transcripts","SC1-v11.0",response.code + "->" + response.statusLine)
	return response
}
else{
		//Process error action
		ConstructArray("Reports","Reports","GET /contacts/{contactid}/sms-transcripts","SC1-v11.0",response.code + "->" + response.statusLine)
		false
}
}

def GetSmsCallQuality( contactId:Int)
{
val response=Http(baseURI + "/services/v11.0/contacts/" + contactId + "/call-quality").headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString

if(assertEquals(200, response.code)){
	//Process success action
	ConstructArray("Reports","Reports","GET /contacts/{contactId}/call-quality","SC1-v11.0",response.code + "->" + response.statusLine)
	return response
}
else{
		//Process error action
		ConstructArray("Reports","Reports","GET /contacts/{contactId}/call-quality","SC1-v11.0",response.code + "->" + response.statusLine)
		false
}
}

def GetTeamPerformanceTotal( startDate:Int, endDate:Int,fields :String)
{
val response=Http(baseURI + "/services/v11.0/teams/performance-total" + "?startDate=" + startDate + "&endDate=" + endDate + "&fields=" + fields).headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString

if(assertEquals(200, response.code)){
	//Process success action
	ConstructArray("Reports","Reports","GET /teams/performance-total","SC1-v11.0",response.code + "->" + response.statusLine)
	return response
}
else{
		//Process error action
		ConstructArray("Reports","Reports","GET /teams/performance-total","SC1-v11.0",response.code + "->" + response.statusLine)
		false
}
}

def GetTeamPerformancebyTeamIDTotal( teamId:Int, startDate:Int, endDate:Int,fields :String)
{
val response=Http(baseURI + "/services/v3.0/teams/" + teamId + "/performance-total" + "?startDate=" + startDate + "&endDate=" + endDate + "&fields=" + fields).headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString

if(assertEquals(200, response.code)){
	//Process success action
	ConstructArray("Reports","Reports","GET /teams/{teamId}/performance-total","SC1-v11.0",response.code + "->" + response.statusLine)
	return response
}
else{
		//Process error action
		ConstructArray("Reports","Reports","GET /teams/{teamId}/performance-total","SC1-v11.0",response.code + "->" + response.statusLine)
		false
}
}

def getReportJobById(jobId:Int,fields:String)
{
val response=Http(baseURI + "services/v11.0/report-jobs/" + jobId + "?fields=" +fields).headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString

if(assertEquals(200, response.code)){
	//Process success action
	ConstructArray("Reports","Reports","GET /report-jobs/{jobId}","SC1-v11.0",response.code + "->" + response.statusLine)
	return response
}
else{
		//Process error action
		ConstructArray("Reports","Reports","GET /report-jobs/{jobId}","SC1-v11.0",response.code + "->" + response.statusLine)
		false
}
}

def GetdatadownloadbyReportID( reportId:Int,fileName:String, startDate:Int, endDate:Int, saveAsFile :Boolean,includeHeaders :Boolean)
{
val response=Http(baseURI + "/services/{version}/report-jobs/datadownload/"+ reportId +
	"?startDate=" + startDate + "&endDate=" + endDate + "&saveAsFile=" + saveAsFile + 
	"&includeHeaders=" + includeHeaders).headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString

if(assertEquals(200, response.code)){
	//Process success action
	ConstructArray("Reports","Reports","GET /report-jobs/datadownload/{reportid}","SC1-v11.0",response.code + "->" + response.statusLine)
	return response
}
else{
		//Process error action
		ConstructArray("Reports","Reports","GET /report-jobs/datadownload/{reportid}","SC1-v11.0",response.code + "->" + response.statusLine)
		false
}
}

def Getwfoascm(startDate:Int,endDate:Int,fields:String )
{
val response=Http(baseURI + "/services/v11.0/wfo-data/ascm" + "?startDate=" + startDate + "&endDate=" + endDate + "&fields=" + fields).headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString

if(assertEquals(200, response.code)){
	//Process success action
	ConstructArray("Reports","Reports","GET /wfo-data/ascm","SC1-v11.0",response.code + "->" + response.statusLine)
	return response
}
else{
		//Process error action
		ConstructArray("Reports","Reports","GET /wfo-data/ascm","SC1-v11.0",response.code + "->" + response.statusLine)
		false
}
}

def Getwfoasi(startDate:Int,endDate:Int,fields:String)
{
val response=Http(baseURI + "/services/v11.0/wfo-data/asi" + "?startDate=" + startDate + "&endDate=" + endDate + "&fields=" + fields).headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString

if(assertEquals(200, response.code)){
	//Process success action
	ConstructArray("Reports","Reports","GET /wfo-data/asi","SC1-v11.0",response.code + "->" + response.statusLine)
	return response
}
else{
		//Process error action
		ConstructArray("Reports","Reports","GET /wfo-data/asi","SC1-v11.0",response.code + "->" + response.statusLine)
		false
}
}

def Getwfocsi(startDate:Int,endDate:Int,fields:String)
{
val response=Http(baseURI + "/services/v11.0/wfo-data/csi" + "?startDate=" + startDate + "&endDate=" + endDate + "&fields=" + fields).headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString

if(assertEquals(200, response.code)){
	//Process success action
	ConstructArray("Reports","Reports","GET /wfo-data/csi","SC1-v11.0",response.code + "->" + response.statusLine)
	return response
}
else{
		//Process error action
		ConstructArray("Reports","Reports","GET /wfo-data/csi","SC1-v11.0",response.code + "->" + response.statusLine)
		false
}
}

def Getwfoftci(startDate:Int,endDate:Int,fields:String)
{
val response=Http(baseURI + "/services/v11.0/wfo-data/ftci" + "?startDate=" + startDate + "&endDate=" + endDate + "&fields=" + fields).headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString

if(assertEquals(200, response.code)){
	//Process success action
	ConstructArray("Reports","Reports","GET /wfo-data/ftci","SC1-v11.0",response.code + "->" + response.statusLine)
	return response
}
else{
		//Process error action
		ConstructArray("Reports","Reports","GET /wfo-data/ftci","SC1-v11.0",response.code + "->" + response.statusLine)
		false
}
}

def Getwfoosi(startDate:Int,endDate:Int,fields:String)
{
val response=Http(baseURI + "/services/v11.0/wfo-data/osi" + "?startDate=" + startDate + "&endDate=" + endDate + "&fields=" + fields).headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString

if(assertEquals(200, response.code)){
	//Process success action
	ConstructArray("Reports","Reports","GET /wfo-data/osi","SC1-v11.0",response.code + "->" + response.statusLine)
	return response
}
else{
		//Process error action
		ConstructArray("Reports","Reports","GET /wfo-data/osi","SC1-v11.0",response.code + "->" + response.statusLine)
		false
}
}

def Getwfoqm(startDate:Int,endDate:Int,fields:String)
{
val response=Http(baseURI + "/services/v11.0/wfo-data/qm"+ "?startDate=" + startDate + "&endDate=" + endDate + "&fields=" + fields).headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString

if(assertEquals(200, response.code)){
	//Process success action
	ConstructArray("Reports","Reports","GET /wfo-data/qm","SC1-v11.0",response.code + "->" + response.statusLine)
	return response
}
else{
		//Process error action
		ConstructArray("Reports","Reports","GET /wfo-data/qm","SC1-v11.0",response.code + "->" + response.statusLine)
		false
}
}

// Report --> WFM

def GetwfmSkillContacts(startDate:Int,endDate:Int,mediaTypeId:Int,fields:String)
{
val response=Http(baseURI + "/services/v11.0/wfm-data/skills/contacts"+ "?startDate=" + startDate + "&endDate=" + endDate + "&fields=" + fields + "&mediaTypeId=" + mediaTypeId).headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString

if(assertEquals(200, response.code)){
	//Process success action
	ConstructArray("Reports","WFM","GET /wfm-data/skills/contact","SC1-v11.0",response.code + "->" + response.statusLine)
	return response
}
else{
		//Process error action
		ConstructArray("Reports","WFM","GET /wfm-data/skills/contact","SC1-v11.0",response.code + "->" + response.statusLine)
		false
}
}

def Getwfmagetns(startDate:Int,endDate:Int,fields:String)
{
val response=Http(baseURI + "/services/v11.0/wfm-data/agents"+  "?startDate=" + startDate + "&endDate=" + endDate + "&fields=" + fields).headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString

if(assertEquals(200, response.code)){
	//Process success action
	ConstructArray("Reports","WFM","GET /wfm-data/agents","SC1-v11.0",response.code + "->" + response.statusLine)
	return response
}
else{
		//Process error action
		ConstructArray("Reports","WFM","GET /wfm-data/agents","SC1-v11.0",response.code + "->" + response.statusLine)
		false
}
}

def GetwfmdailerContacts(startDate:Int,endDate:Int,fields:String)
{
val response=Http(baseURI + "/services/v11.0/wfm-data/skills/dialer-contacts"+  "?startDate=" + startDate + "&endDate=" + endDate + "&fields=" + fields).headers(("Authorization", "Bearer " + accessToken),("content-type", "application/json")).asString

if(assertEquals(200, response.code)){
	//Process success action
	ConstructArray("Reports","WFM","GET /wfm-data/skills/dialer-contacts","SC1-v11.0",response.code + "->" + response.statusLine)
	return response
}
else{
		//Process error action
		ConstructArray("Reports","WFM","GET /wfm-data/skills/dialer-contacts","SC1-v11.0",response.code + "->" + response.statusLine)
		false
}
}

def 