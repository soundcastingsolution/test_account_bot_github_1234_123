// importing the scalaj-http library and spray.json 

package scalaj.http
import spray.json._

object MainApp {

	var sessionID = "WVVsLzlGTkc1T1hUTVpNVDlXR2Z2WC9KQWo3K3ZxZzFFTXNDQUxISA==";//String
	var chatText = "test";//String
	var targetAgentId = "test";//String
	var targetAgentIdInt = 1234;//Int
	var isTyping = "test";//String
	var isTextEntered = "test";//String
	var playTimestamp = true;//Boolean
	var position = "test";//String
	var targetSkillIdInt = 1234;//
	var targetSkillId = "test";//String
	var bccAddress = "test";//String
	var ccAddress = "test";//String
	var parentContactId = 1234;//int
	var skillId = 1234;//int
	var SkillID="1234"//String
	var fromAddress = "test";//String
	var subject = "test";//String
	var bodyHtml = "test";//String
	var attachments = "test";//String
	var attachmentNames = "test";//String
	var isDraft = "test";//String
	var primaryDispositionId = "test";//String
	var primaryDispositionIdInt = 123;//String
	var tags = "test";//String
	var originalAttachmentNames = "test";//String
	var isImmediate = "test";//String
	var contactID = "test";//String
	var contactIDInt = 213432;//Int
	var skillName = "test";//String
	var phoneNumber = "12345";//String
	var sequence = "test";//String
	var duration = 13;//int
	var spacing = 1;//int
	var overrideType = "test";//String
	var outcome = "test";//String
	var callbackId = 123;//int
	var rescheduleDate = "2018-02-07T06:22:27.143Z";//String
	var notes = "test";//String
	var stationPhoneNumber = "test";//String
	var inactivityTimeout = 123;//int
	var inactivityForceLogout = true;//Boolean
	var asAgentId = 12;//int
	var continueReskill = true;//Boolean
	var primaryDispositionNotes = "test";//String
	var primaryCommitmentAmount = 3211;//int
	var primaryCallbackTime = "2018-02-07T06:22:27.143Z";//String
	var primaryCallbackNumber = "test";//String
	var secondaryDispositionIdInt = 1;//int
	var secondaryDispositionId = "test";//String
	var previewDispositionId = 123;//int
	var state = true;//Boolean
	var categoryId = true;//Boolean
	var priority = "test";//String
	var comment = "test";//String
	var customData = "test";//String
	var indicatorName = "test";//String
	var data = "test";//String
	var chat = true;//Boolean
	var email = true;//Boolean
	var workitem = true;//Boolean
	var requestedPassword = "test1234"; //String
	var currentPassword = "test1234"; //String
	var newPassword = "test1234"; //String
	var callerID = "test1234"; //String
	var callDelaySec = 1234; //Int
	var skill = 1234; //Int
	var targetAgent = 1234; //Int
	var priorityManagement = "test1234"; //String
	var intialPriority = 1234; //Int
	var acceleration = 1234; //Int
	var maxPriority = 1234; //Int
	var zipTone = "test1234"; //String
	var screenPopSrc = "test1234"; //String
	var screenPopUrl = "test1234"; //String
	var timeout = 1234; //Int
	var firstName = "test1234"; //String
	var lastName = "test1234"; //String
	var promiseDate = "2018-02-07T06:22:27.143Z"; //String
	var promiseTime = "2018-02-07T06:22:27.143Z"; //String
	var timeZone = "test1234"; //String
	var pointOfContact = "test1234"; //String
	var chatRoomID = 1234; //Int
	var parameters = "test1234"; //String
	var chatSessionIdInt = 1234; //Int
	var chatSessionId = "test1234"; //String
	var label = "test1234"; //String
	var message = "test1234"; //String
	var previewText = "test1234"; //String
	var toAddress = "test1234@gmail.com"; //String
	var emailBody = "test1234"; //String
	var pointOfContactId = "test1234"; //String
	var workItemId = "test1234"; //String
	var workItemPayload = "test1234"; //String
	var workItemType = "test1234"; //String
	var from = "test1234"; //String
	var updatedSince = "2018-02-07T06:22:27.143Z"; //String
	var fields = "test1234"; //String
	var mediaTypeId = 1234; //Int
	var campaignId = 1234; //Int
	var agentId = 1234; //Int
	var teamId = 1234; //Int
	var toAddr = "test1234@gmail.com"; //String
	var fromAddr = "test1234@gmail.com"; //String
	var stateId = 1234; //Int
	var startDate = "2018-02-07T06:22:27.143Z"; //String
	var endDate = "2018-02-07T06:22:28.143Z"; //String
	var skip = "1234"; //String
	var top = "1234"; //String
	var orderby = "12"; //String
	var searchString = "test1234"; //String
	var mediaType = "123"; //String
	var transportCode = "123"; //String
	var agentID = "123"; //String
	var contactId = 12345678; //Int 
	var isLogged = true; //Boolean
	var isRefused = true; //Boolean
	var isTakeover = true; //Boolean
	var analyticsProcessed = true; //Boolean
	var isOutboundSkill = true; //Boolean
	var reportId = 1234; //Int
	var jobStatus = "test1234"; //String
	var jobSpan = 1234; //Int
	var jobId = 1234; //Int
	var fileType = "txt"; //String
	var isAppendDate = true; // Boolean
	var deleteAfter = 1234; //Int
	var overwrite = true; //Boolean
	var fileName = "test1234"; //String
	var saveFile = true; //Boolean
	var includeHeaders = true; //Boolean
	var businessUnitId="141014";//String
	var AccesskeyId="4FE4QQJVVDVJZWQ7NGBE4YF35O7DEHRJ74XPPQNQ3MYYKFNHIYBA====";//String
	var isActive=true;//Boolean
	var isactive="true";//String
	var JobID="1234";//String
  def main(args: Array[String]) {
  //var sessionID = 
	    Functions.init()
		Functions.getBusinessunitTimezone();
		Functions.getSuppresedContacts();
		Functions.PostSuppresedContacts(s"""{"value": "qm-workflows","skills":"1","startDate":"2019-07-10","endDate":"2019-07-15"}""");
		putbusinessunitTimezone(s"""{"timezones": "qm-workflows","items":"1"}""");
		Functions.PutSkillIDParameterTimezone(skillId);
		Functions.getTimeZoneBySkillId(skillId);
		Functions.postJobs(s"""{"entityName": "qm-workflows","version":"1","startDate":"2019-07-10","endDate":"2019-07-15"}""");
		Functions.getjobs();
		Functions.getjobsByID(JobID);
		Functions.getActiveFlag("1");
		Functions.postWorkFlow(s"""{"profile": [{"ProfileName": "new","Description": "new1","ProfileID": "0"},"data":{"Value": "vary","Visible": "1","Type": " ","Ref": " " }]}""");
		Functions.getWorkFlowById("56765");
		Functions.putWorkFlowById("56765",s"""{"profile": [{"ProfileName": "new","Description": "new1","ProfileID": "0"},"data":{"Value": "vary","Visible": "1","Type": " ","Ref": " " }]}""");
		Functions.putWorkFlowByIdActivate("56765");
		Functions.putWorkFlowByIdDeactivate("56765");
		Functions.GetMediaplayBack("21bf4a34-f768-45d4-aed6-6bd754dd49d2","chat","true");
		Functions.GetMediaplayBackByID("21bf4a34-f768-45d4-aed6-6bd754dd49d2","b171d215-a152-4572-bd45-56670cb7a859","chat","true");
		Functions.GetMediaplayBackContacts("master id 1","chat","true");
		Functions.PutUnavailableCodeByIDTeams(12345,s"""{teams": [{"teamId": "string"}]}""","");
		Functions.postJobSync(s"""{"entityName": "qm-workflows","version":"1","startDate":"2019-07-10","endDate":"2019-07-15"}""");
	/*	Functions.GetAccesskeys(agentID,fields,skip,top,orderby);
		Functions.postAccesskeys(agentID);
		Functions.deleteAccesskey(AccesskeyId);
		Functions.PatchAccesskeys(AccesskeyId,isActive);
		Functions.PostCampaigns(s"""{campaigns": [{"campaignName": "string","isActive": true,"description": "string","notes": "string"}]}"""); 
		Functions.PutCampaignsByID(72432,s"""{"campaign": {"campaignName": "string","isActive": true,"description": "string","notes": "string"}}""");
		Functions.deleteCampaignsBySkill(72432,s"""{"skills": [{"skillId": 0,"transferCampaignId": 0}]}""");
		Functions.GetContactidSmsHistoricalTranscript(contactId,businessUnitId);
		Functions.GetSmsHistoricalTranscript("4005150002",SkillID,businessUnitId);
		Functions.PutUnavailableCodeByID(3590,s"""{"unavailableCodeName": "string","postContact": "true","agentTimeout": 0,"isActive": "true"}""");
		Functions.PostHoursofOperation(s"""{"profileName": "string"}""");
		Functions.PutHoursofOperationByID(4);
		Functions.PostPointofContact(s"""{"pointOfContact": "string","pointOfContactName": "string","skillId": 0,"isActive": true,"mediaTypeId": 0,"scriptName": "string","ivrReportingEnabled": true}""");
		Functions.PutPointofContactByID(12345,s"""{"pointOfContactName": "string","skillId": 0,"isActive": true,"scriptName": "string","ivrReportingEnabled": true}""");
		Functions.PostUnavailableCodes(s"""{"unavailableCodeName": "string","postContact": true,"isActive": true}""");
		Functions.GetPhonenumbers(searchString,skip,top);
		Functions.GetDipositionBySkill(isactive,updatedSince,searchString,fields,skip,top,orderby);  */
		Functions.postEmailSaveDraft(sessionID,9471795,s"""{"toAddress": "","fromAddress": "","bccAddress": "" ,	
      "campaignId": 0, "subject": "","bodyHtml": "" ,"fromAddress": "", "attachments": "","attachmentNames": "" , "draftEmailGuidStr": "","primaryDispositionId": 0,"secondaryDispositionId": 0,"tags": "", "notes": "","originalAttachmentNames": ""}""");
		Functions.postAddText(sessionID,s"""{"mediaType": 0}""");
		Functions.PutUnavailableCodeByIDTeams(12345,s"""{teams": [{"teamId": "string"}]}""","");
		
		Functions.deleteSuppressedContactBySuppressedContactId(12345)
		Functions.getSuppressedContactBySuppressedContactId(12345)
		Functions.putSuppressedContactBySuppressedContactId(12345,s"""{suppressedContactData": {"startDate": "2019-11-18T09:03:52.050Z", "endDate": "2018-11-15T09:03:52.050Z","Value": "string","Skill": "string"}}""","")
		Functions.getContactsByIdHierachy(12345)
		
		
	/*	 Functions.getAgents("10/10/1800","true","","","0","100","");
		 Functions.PostCampaignsBySkill(72432,s"""{{"skills": [{"skillId": 0}]}""");
		 Functions.getAgentSkillsByAgentId(207932,"10/10/1800","","","0","100","","4","");
		 Functions.getAddressBooks();
		 Functions.getAgentAddressBooks(207932,"false","");
		 Functions.getCampaignAddressBooks("72422","false","");
		 Functions.getDynamicAddressBookEntries("1811","","10/10/1800","0","100","");
		 Functions.getStandardAddressBookEntries("1614","10/10/1800","","","0","100","");
		 Functions.getTeamAddressBooks("55578","false","");
		 Functions.GetAgentByAgentIDGroups(207932,"");
		 Functions.getAgentsDialingPatterns();
		 Functions.GetagentbyAgentID(207932,"");
		 Functions.getAgentScheduledCallbacks(207932);
		 Functions.getAgentIndicators(207932);
		 Functions.getAgentMessages(207932);
		 Functions.getQuickReplies();
		 Functions.getAgentQuickReplies(207932);
		 Functions.getAllAgentsSkills("10/10/1800","","","","","0","100","","true");
		 Functions.getAgentContactsBySkill(207932,"11/17/2018", "12/17/2018","","");
		 Functions.getAgentsContactsBySkill("11/17/2018", "12/17/2018","","");
		 Functions.getTeamAgent("","10/10/1800");
		 Functions.GetTeamByAgentID(55578,"");
		 Functions.getTeamById(55578,"");
		 Functions.getTeams("","","true","","0","10000","");
		 Functions.getAgentUnassignedSkills(207932,"","","","","","","","true");
		 Functions.getChatTranscript(207885257);
		 Functions.GetcontactbyContactID(207885287,"");
		 Functions.getEmailTranscript(207885287,"");
		 Functions.getFeedbackCategories();
		 Functions.getMediaTypesById(4);
		 Functions.getMediaTypes();
		 Functions.getPointsOfContact();
		 Functions.getPointsOfContactById(105524);
		 Functions.getScripts("","","","","","","");
		 Functions.getServerTime();
		 Functions.getContactStateDescriptions();
		 Functions.getContactStateDescriptionById(17);
		 Functions.getUnavailableCodesByTeam(55578,"false");
		 Functions.getUnavailableCodes();
		 Functions.GetBrandingProfile("12345","");
		 Functions.getBusinessUnit("","false");
		 Functions.getCountries();
		 Functions.getDataDefinitionTypes();
		 Functions.GetDispostion("0","100","","","","true","");
		 Functions.getFile("");
		 Functions.GetFilesExternal("");
		 Functions.GetFolders("");
		 Functions.getHiringSources();
		 Functions.getHoursOfOperationProfiles("","","","","");
		 Functions.getHoursOfOperationProfileById(1073741939,"","","","","");
		 Functions.getLocations("false");
		 Functions.getMessageTemplates("1");
		 Functions.getMessageTemplatesById("1073741946","1");
		 Functions.getPermissions("1");
		 Functions.getPermissionsById(207932,"1");
		 Functions.getPhoneCodes();
		 Functions.getSecurityProfiles("","true","","","0","100","");
		 Functions.getSecurityProfileById(1,"","true","","","0","100","");
		 Functions.getStatesByCountryId("44","");
		 Functions.getTagById("66");
		 Functions.getTags("","");
		 Functions.getTimeZones();
		 Functions.GetGroups("10000","0","groupId","","true","");
		 Functions.GetGroupsByGroupID("179","");
		 Functions.GetListCalllist("","10","0","jobId asc","2016-12-1","2017-1-1");
		 Functions.Getdeliverypreference(152167,""); 
		 Functions.getCallingListById(1073743880,"","","","","","","")
		 Functions.getCallingListAttempts("1073743880","","","","","","");
		 Functions.getCallingLists();
		 Functions.getDncGroups("","");
		 Functions.getDncGroupById("364","");
		 Functions.getContributingSkillsForDncGroup("1073742783");
		 Functions.getDncGroupRecords("364","","","","");
		 Functions.getScrubbedSkillsForDncGroup("364");
		 Functions.getAgentsNotAssignedToSkill("218853","","","","","");
		 Functions.getSkillScheduledCallbacks("218853");
		 Functions.getCampaignById("36496");
		 Functions.getCampaigns("false","","","0","10000","");
		 Functions.GetDispostionByID("4586","");
		 Functions.GetDispostionByClassification("");
		 Functions.Getskillretrysetting("152167","");
		 Functions.Getschedulesettings("152167");
		 Functions.getAgentsAssignedToSkills("","10/10/1800");
		 Functions.GetCPAManagement("152167","");
		 Functions.getSkillById("152167");
		 Functions.getAgentContactHistory("152167","","","","","","");
		 Functions.getCallDataBySkill("152167","","");
		 Functions.GetDispostionBySkillid("152167","","","0","100","");
		 Functions.GetDispostionBySkillidUnAssigned("152167","","","0","100","");
		 Functions.getSkills("10/10/1800","4","","true","","","0","100","");
		 Functions.getSkills("10/10/1800","4","","true","","","0","100","");
		 Functions.GetSkillParameterGeneralSetting("152167","");
		 Functions.getThankYouForSkillId("152167");
		 Functions.deleteDynamicAddressBookEntry("1810","4");
		 Functions.deleteAddressBook("1073743156");
		 Functions.createAddressBook(s"""{"addressBookName" : "IncAddBook", "addressBookType" : "Standard"}""");
		 Functions.createStandardAddressBookEntries("1810",s"""{ "addressBookEntries": [{"firstName": "","middleName": "","lastName": "","company": "","phone":  "","mobile": "","email": ""}]}""");
		 Functions.deleteAddressBook("1073743156");
		 Functions.assignAddressBook("1810",s"""{"entityType": "","addressBookAssignments":[{"entityId": ""}]}}""");
		 
		 Functions.addDynamicAddressBookEntries("1810",s"""{"fullLoad": "","addressBookEntries":[{"externalId": "","stateId": "","externalState":"",
		 "firstName": "","middleName": "","lastName": "","company": "","phone": "","mobile": "","email": ""}]}""");
		 
		 Functions.deleteAgentMessagesByMessageId("3343");
		 Functions.deleteSkillsFromAgent("207932",s"""{"skills": [{"skillId": "218854"}]}""");
		 Functions.deleteAgentsFromTeam("105209",s"""{"transferTeamId": "1","agents": [{"agentId": ""}]}""");
		 Functions.endAgentSession("207932");
		 Functions.PostAgentMessages(s"""{"agentMessages":[{"message": "","startDate": "2018-12-19T09:03:52.050Z","subject": "","targetId": 0,
		 "targetType": "Agent","validUntil": "","expireMinutes": 5}]}""");
		 Functions.assignSkillsToAgent("207932",s"""{"skills":[{"skillId":"","isActive":"","proficiency":""}]}""");
		 Functions.changeAgentState("207932",s"""{"state":"Available","outStateId":""}""");
		 Functions.assignAgentsToTeam("55852",s"""{"agents":[{"agentId":"207932"}]}""");
		 Functions.PutTeam(s"""{"teams":[{"teamName":"team","isActive":true,"maxConcurrentChats":8,"wfoEnabled":false,"wfm2Enabled":false,"qm2Enabled":false,"inViewEnabled":false,"notes":"","maxEmailAutoParkingLimit":25,"inViewGamificationEnabled":false,"inViewChatEnabled":false,"inViewLMSEnabled":false,"analyticsEnabled":false,"requestContact":false,"chatThreshold":1,"emailThreshold":1,"workItemThreshold":1,"voiceThreshold":1,"contactAutoFocus":false,"teamUuid":"","teamLeadId":""}]}""");
		   
		
		
		
		 Functions.createCustomEvent("207932",s"""{"eventName":"","persistInMemory":"","data":""}""");
		 Functions.modifySkillsForAgent("207932",s"""{"skills":[{"skillId":"","isActive":"","proficiency":""}]}""");
		 Functions.PutTeambyTeamID("55852",s"""{"forceInactive":false,"team":{"teamName":"team","isActive":true,"maxConcurrentChats":8,"wfoEnabled":false,"wfm2Enabled":false,"qm2Enabled":false,"inViewEnabled":false,"notes":"","maxEmailAutoParkingLimit":25,"inViewGamificationEnabled":false,"inViewChatEnabled":false,"inViewLMSEnabled":false,"analyticsEnabled":false,"requestContact":false,"chatThreshold":1,"emailThreshold":1,"workItemThreshold":1,"voiceThreshold":1,"contactAutoFocus":false,"teamUuid":"","teamLeadUuid":"","NICEAnalyticsEnabled":false,"NICEAudioRecordingEnabled":false,"NICECoachingEnabled":false,"NICEDesktopAnalyticsEnabled":false,"NICELessonManagementEnabled":false,"NICEPerformanceManagementEnabled":false,"NICEQmEnabled":false,"NICEQualityOptimizationEnabled":false,"NICEScreenRecordingEnabled":false,"NICEShiftBiddingEnabled":false,"NICESpeechAnalyticsEnabled":false,"NICEStrategicPlannerEnabled":false,"NICESurvey_CustomerEnabled":false,"NICEWfmEnabled":false}}""");
		
		 Functions.deleteUnavailableCodesByTeam("207932",s"""{"codes":[{"outstateId":""}]}""");
		 Functions.endContact("208631681");
		 Functions.monitorContact(208631681,s"""{"phoneNumber":""}""");
		 Functions.recordContact("208631681");
		 Functions.signalContact("208701379",s"""{"p1":"","p2":"","p3":"","p4":"","p5":"","p6":"","p7":"","p8":"","p9":""}""");
		 Functions.assignTagsToContact("208701379",s"""{"tags":[{"tagId":""}]}""");
		 Functions.startScript("14562",s"""{"skillId":"","parameters":"","startDate":""}""");
		 Functions.assignUnavailableCodesToTeam("55852",s"""{"codes":[{"outstateId":""}]}""");
		 Functions.deleteCallingList("55852",s"""{"forceInactive":"false","forceDelete":"false"}""");
		 Functions.DeleteFiles(s"""{"fileName":""}""");
		 Functions.DeleteFolders(s"""{"folderName":""}""");
		 Functions.PostcreateDispositions(s"""{"dispositions":[{"dispositionName":"","isPreviewDisposition":false,"classificationId":1}]}""");
		 Functions.PostCreateFileName(s"""{"fileName":"","file":"","overwrite":false}""");
		 Functions.PostCreateFileExternal("12345",s"""{"fileName":"","file":"","overwrite":false,"needsProcessing":false}""");
		 Functions.createHiringSource(s"""{"sourceName":""}"""); 
		 Functions.createMessageTemplate(s"""{"templateName":"","templateTypeId":-1,"body":"","isHtml":true,"ccAddress":"","bccAddress":"","fromName":"","fromAddress":"","replyToAddress":"","subject":""}""");
		   
		 Functions.createTag(s"""{"tagName":"","notes":""}""");
		 Functions.PutupdateFile(s"""{"oldPath":"","newPath":"","overwrite":false}""");
		 Functions.PutupdateFilesExternal(s"""{"fileName":"","needsProcessing":false}""");
		 Functions.updateMessageTemplate("54689",s"""{"templateName":"","body":"","isHtml":true,"ccAddress":"","bccAddress":"","replyToAddress":"","fromName":"","fromAddress":"","subject":"","isActive":true}""");
		
		 Functions.updateTag(54689,s"""{"tagName":"","notes":"","isActive":""}""");
		 Functions.GetGroupsByAgentGroupID(10000,"","0","","","true","");
		 Functions.DeleteGroupsByAgentGroupID("12345",s"""{"agents":[{"agentId":1}]}""");
		 Functions.PostGroups(s"""{"groups":[{"groupName":"","isActive":true,"notes":""}]}""");
		 Functions.PutGroupsByGroupID("493",s"""{"groupName":"","isActive":true,"notes":""}""");
		 Functions.PostGroupsByAgentGroupID("493",s"""{"agents":[{"agentId":1}]}""");
		 Functions.DeleteCallListByJobID("12345");
		 Functions.PostCallListbyListID("12345",s"""{"skillId":"","fileName":"","forceOverwrite":"false","defaultTimeZone":"","expirationDate":"","listFile":"","startSkill":"false"}""");
		
		 Functions.deleteContributingSkill("12345","56789");
		 Functions.deleteDncGroupRecords("12345",s"""{"dncGroupRecords":[{"phoneNumber":"","expiredDate":""}]}""");
		 Functions.deleteScrubbedSkill("12345","56789");
		 Functions.createCallingListMapping(s"""{"listName":"","listExpirationDate":"","externalIdColumn":"","scoreColumn":"","customer1Column":"","customer2Column":"","callerIdColumn":"","priorityColumn":"","complianceReqColumn":"","firstNameColumn":"","lastNameColumn":"","addressColumn":"","cityColumn":"","stateColumn":"","zipColumn":"","timeZoneColumn":"","confirmReqColumn":"","overrideFinalizationColumn":"","agentIdColumn":"","callRequestTimeColumn":"","callRequestStaleColumn":"","notesColumn":"","expirationDateColumn":"","destinationMappings":[{"fieldName":"","fieldValue":""}],"customFieldMappings":[{"fieldName":"","fieldValue":""}]}""");
		   
		 Functions.assignContributingSkill("12345","56789");
		 Functions.createDncGroup(s"""{"dncGroupName":"","dncGroupDescription":""}""");
		 Functions.postDncGroupRecords("12345",s"""{"dncGroupRecords":[{"phoneNumber":"","expiredDate":""}]}""");
		 Functions.assignScrubbedSkill("12345","56789");
		 Functions.updateDncGroup("12345",s"""{"dncGroupName":"","dncGroupDescription":"","isActive":""}""");
		 Functions.postDncGroupsSearchPhoneNumber(s"""{"phoneNumber":""}""");
		 Functions.deleteScheduledCallback("12345",s"""{"description":""}""");
		 Functions.createScheduledCallback(s"""{"firstName":"","lastName":"","phoneNumber":"","skillId":"","targetAgentId":"","scheduleDate":"","notes":""}""");
		 Functions.deleteAgentsFromSkill("12345",s"""{"agents":[{"agentId":""}]}""");
		 Functions.removeTagsFromSkill("12345",s"""{"tags":[{"tagId":""}]}""");
		 Functions.removeTagsFromSkill("12345",s"""{"tags":[{"tagId":""}]}""");
		 Functions.assignAgentsToSkill("12345",s"""{"agents":[{"agentId":"","isActive":"","proficiency":""}]}""");
		 Functions.assignTagsToSkill("12345",s"""{"tags":[{"tagId":""}]}""");
		 Functions.startPersonalConnectionSkill("12345");
		 Functions.PutUpdateDispostionbydispositionId("12345",s"""{"dispositionName":"","isPreviewDisposition":false,"classificationId":1,"isActive":true}""");
		 Functions.Putskillretrysetting("12345",s"""{"retrySettings":{"loadNonFresh":true,"finalizeWhenExhausted":true,"maximumAttempts":10,"minimumRetryMinutes":240,"maximumNumberOfHandledCalls":10,"restrictedCallingMinutes":0,"restrictedCallingMaxAttempts":0,"generalStaleMinutes":30,"callbackRestMinutes":1440,"releaseAgentSpecificCalls":true,"maximumNumberOfCallbacks":10,"callbackStaleMinutes":15}}""");
		 Functions.Putschedulesettings("12345",s"""{"scheduleSettings":{"isScheduled":false,"sundayStartTime":"08:00","sundayEndTime":"21:00","sundayIsActive":true,"mondayStartTime":"08:00","mondayEndTime":"21:00","mondayIsActive":true,"tuesdayStartTime":"08:00","tuesdayEndTime":"21:00","tuesdayIsActive":true,"wednesdayStartTime":"08:00","wednesdayEndTime":"21:00","wednesdayIsActive":true,"thursdayStartTime":"08:00","thursdayEndTime":"21:00","thursdayIsActive":true,"fridayStartTime":"08:00","fridayEndTime":"21:00","fridayIsActive":true,"saturdayStartTime":"08:00","saturdayEndTime":"21:00","saturdayIsActive":true}}""");
		
		Functions.PutCPAManagementbySkillId(152167,s"""{"cpaSettings":{"abandonTimeout":2,"abandonMessagePath":"","abandonMsgMode":1,"ansMachineDetMode":2,"ansMachineMsg":"","exceptions":[{"attemptNo":1,"ansMachineDetMode":2,"ansMachineMsg":""},{"attemptNo":2,"ansMachineDetMode":2,"ansMachineMsg":""}],"treatProgressAsRinging":true,"preConnectCPAEnabled":false,"agentOverrideOptionFax":true,"agentOverrideOptionAnsweringMachine":true,"agentOverrideOptionBadNumber":false,"utteranceMinimumSeconds":0.2,"customerLiveSilenceSeconds":1.1,"machineMinimumWithAgentSeconds":3,"machineMinimumWithoutAgentSeconds":3,"machineEndSilenceSeconds":1,"machineEndTimeoutSeconds":20,"agentResponseUtteranceMinimumSeconds":0.5,"agentNoResponseSeconds":1.4,"agentVoiceThreshold":10000,"customerVoiceThreshold":16000,"preConnectCPARecording":false,"enableCPALogging":false}}""");
		
		 Functions.modifyAgentsForSkill("152167",s"""{"agents":[{"agentId":"","isActive":"","proficiency":""}]}""");
			
		 Functions.Putxssettings("152167",s"""{"xsSettings":{"xsScriptID":123,"xsCheckinScriptID":345,"externalOutboundSkill_No":"152167","xsSkillChangedActive":true,"xsGetContactsActive":true,"xsFreshThreshold":789,"xsAvailableThreshold":123,"xsReadyThreshold":456,"xsNumberToRetrieve":478}}""");
		
		 Functions.Putskilldeliverypreference("152167",s"""{"deliveryPreferences":{"ConfirmationRequiredDisabled":false,"ConfirmationRequiredDeliveryType":1,"ConfirmationRequiredTimeout":null,"ConfirmationRequiredTimeoutSubsequent":null,"ConfirmationRequiredDefaultAccept":true,"ConfirmationRequiredDefault":false,"ComplianceRecordsDisabled":false,"ComplianceRecordsDeliveryType":4,"ComplianceRecordsTimeout":null,"ComplianceRecordsTimeoutSubsequent":null,"ComplianceRecordsDefaultAccept":false,"ShowComplianceButtonReschedule":false,"ShowComplianceButtonRequeue":false,"ShowComplianceButtonSnooze":false,"ShowComplianceButtonDisposition":false,"ShowPreviewButtonReschedule":false,"ShowPreviewButtonRequeue":false,"ShowPreviewButtonSnooze":false,"ShowPreviewButtonDisposition":false}}""");
		 
		 Functions.PutSkillParameterGeneralSetting("152167",s"""{"generalSettings":{"minimumRetryMinutes":12,"maximumAttempts":10,"defaultContactExpiration":10,"getPriorityContactsOnContactinsertion":false,"loadCallbacks":false,"loadFresh":false,"loadNonFresh":false,"overrideBusinessUnitAbandonRate":false,"maximumRingingDuration":1,"beginDampenPercentage":1,"abandonRateCutoff":1,"abandonRateThreshold":1,"inactiveBlenderTimer":15,"maximumRatio":1,"aggressiveness":"conservative","endOfListNotificationsDelay":15,"notifyAgentsWhenListIsEmpty":false,"percentageOfAgentsBeforeOverdial":5,"blockMultipleCalls":true,"consecutiveAttemptsWithoutALiveConnect":5,"enableDialingByProficiency":false,"proficiencyFactor":1,"waitTimeFactor":10,"maxConcurrentCallsPerAgent":7,"maxWaitTimeSeconds":180}}""");
		 
		 Functions.PostAgent(s"""{"agents":[{"firstName":"Nikhil","middleName":"","lastName":"M","teamId":"0","teamUuid":"","reportToId":1,"internalId":"","profileId":0,"roleId":"","password":"","forceChangeOnLogon":true,"emailAddress":"nikhil.m@incontact.com","userName":"nikhil.m@sc1.com","timeZone":"(GMT-07:00)Arizona","country":"India","state":"","city":"Chennai","chatRefusalTimeout":15,"phoneRefusalTimeout":15,"workItemRefusalTimeout":15,"defaultDialingPattern":0,"maxConcurrentChats":1,"useTeamMaxConcurrentChats":false,"isActive":true,"locationId":0,"notes":"","hireDate":"10/10/1800","terminationDate":"10/10/1800","hourlyCost":0,"rehireStatus":true,"employmentType":1,"referral":"","atHome":false,"hiringSource":0,"ntLoginName":"","custom1":"","custom2":"","custom3":"","custom4":"","custom5":"","scheduleNotification":0,"federatedId":"","maxEmailAutoParkingLimit":1,"useTeamEmailAutoParkingLimit":false,"sipUser":"","systemUser":"","systemDomain":"","crmUserName":"","useAgentTimeZone":false,"timeDisplayFormat":0,"sendEmailNotifications":false,"apiKey":"","telephone1":"","telephone2":"","userType":"","isWhatIfAgent":false,"requestContact":false,"useTeamRequestContact":false,"chatThreshold":1,"useTeamChatThreshold":false,"emailThreshold":1,"useTeamEmailThreshold":false,"workItemThreshold":1,"useTeamWorkItemThreshold":false,"contactAutoFocus":false,"useTeamContactAutoFocus":false,"subject":"","issuer":"","isOpenIdProfileComplete":false,"recordingNumbers":[{"number":"2"}]}]}""");
		 
		 
		 Functions.PostCreateSkill(s"""{"skills":[{"mediaTypeId":4,"skillName":"My_IB_Call","isOutbound":true,"outboundStrategy":"PersonalConnection","campaignId":1,"callerIdOverride":"8015554422","emailFromAddress":"test@test.com","emailFromEditable":false,"emailBccAddress":"","scriptId":2,"reskillHours":4,"minWFIAgents":null,"minWFIAvailableAgents":null,"interruptible":false,"enableParking":false,"minWorkingTime":4,"agentless":false,"agentlessPorts":6,"notes":"thisisatestnote","acwTypeId":3,"requireDisposition":false,"allowSecondaryDisposition":false,"scriptDisposition":false,"stateIdACW":2,"maxSecondsACW":3,"acwPostTimeoutStateId":53,"agentRestTime":4,"displayThankyou":false,"thankYouLink":"no","popThankYou":true,"popThankYouURL":"tester.com","makeTranscriptAvailable":true,"transcriptFromAddress":"fromMe@email.com","priorityBlending":false,"callSuppressionScriptId":4,"useScreenPops":true,"screenPopTriggerEvent":1,"useCustomScreenPops":false,"screenPopType":"webpage","screenPopDetails":"http://not//","initialPriority":4,"acceleration":5,"maxPriority":10,"serviceLevelThreshold":51,"serviceLevelGoal":24,"enableShortAbandon":true,"shortAbandonThreshold":123,"countShortAbandons":true,"countOtherAbandons":true,"chatWarningTheshold":0,"agentTypingIndicator":false,"patronTypingPreview":false,"smsTransportCodeId":null,"messageTemplateId":null,"deliverMultipleNumbersSerially":false,"cradleToGrave":false,"priorityInterrupt":false,"dispositions":[{"dispositionId":1,"priority":1}]}]}"""); 
		 Functions.updateStandardAddressBookEntries("12345",s"""{"addressBookEntries":[{"addressBookEntryId":"","firstName":"","middleName":"","lastName":"","company":"","phone":"","mobile":"","email":""}]}"""");
		 
		 Functions.getAddressBooksBySkillId(72422,"false","");
		 Functions.getAgentsStates();
		 Functions.updateScheduledCallback("12345",s"""{"firstName":"","lastName":"","phoneNumber":"","skillId":"","targetAgentId":"","scheduleDate":"","notes":""}""");
		
		 Functions.GetCallingListbyJobID(154,"");
		 Functions.Getxssettings("152167",""); 
		 Functions.PostCreateFileExternal("12345",s"""{"fileName":"","file":"","overwrite":false,"needsProcessing":false}""");
		 Functions.GetGroupsByAgentGroupID(12345,"10000","0","","","true","");
		 
		 //version 18
		Functions.getScripts("","");
		Functions.postScripts("","");
		Functions.putScripts("","");
		Functions.postScriptsKick("");
		Functions.getScriptsHistoryByName("");
		Functions.PutSkillListManagement("152167",s"""{"displayField1Name":"sam1","displayField2Name":"sam2","listOrderingOptions":[{"orderType":"","direction":""}]}""");
		Functions.getSkillsParameters();
		Functions.PutCadencesettings("152167");
		Functions.getSkillsSkillIdParameters("152167");
		Functions.postSetDisposition(207885287);
		Functions.getBusinessUnitOutboundRoutes();
		//v19
		Functions.getclientdata();
		Functions.putclientdata();
		Functions.postsmsoutbound("");
		Functions.removeprospects("");
		Functions.putpersistentcontacts(207885287);
		Functions.getagentsettings(12365);
		Functions.getscriptsbyId("");
		Functions.getscriptssearch();
		Functions.deletescripts();
		Functions.posttransformnumbers("");
		
		 
		//agent APIs
		Functions.postAgentPhoneDial(sessionID);
		Functions.mute(sessionID);
		Functions.unmute(sessionID);
		Functions.endCall(sessionID);
		Functions.addChat(sessionID);
		Functions.acceptContact(sessionID,contactID);
		Functions.rejectContact(sessionID,contactID);
		Functions.activateChat(sessionID,contactID);
		Functions.sendChatText(sessionID,contactID,chatText);
		Functions.chatToAgent(sessionID,contactID,targetAgentId);
		Functions.chatToSkill(sessionID,contactID,targetSkillId);
		Functions.agentTyping(sessionID,contactID,isTyping,isTextEntered);
		Functions.endVoiceMail(sessionID,contactID);
		Functions.playVoiceMail(sessionID,contactID,playTimestamp,position);
		Functions.pauseVoiceMail(sessionID,contactID);
		Functions.transferVoiceMail(sessionID,contactID,targetAgentIdInt);
		Functions.voiceMailToSkill(sessionID,contactID,targetSkillIdInt);
		Functions.addEmail(sessionID);
		Functions.emailOutbound(sessionID,toAddress,parentContactId,skillId);
		Functions.emailForward(sessionID,contactID,skillId,toAddress,fromAddress,ccAddress,bccAddress,subject,bodyHtml,attachments,attachmentNames,originalAttachmentNames)
		Functions.emailReply(sessionID,contactID,skillId,toAddress,fromAddress,ccAddress,bccAddress,subject,bodyHtml,attachments,attachmentNames,originalAttachmentNames)
		Functions.emailSend(sessionID,contactID,skillId,toAddress,fromAddress,ccAddress,bccAddress,subject,bodyHtml,attachments,attachmentNames);
		Functions.emailToAgent(sessionID,contactID,targetAgentId);
		Functions.emailToSkill(sessionID,contactID,targetSkillId);
		 Functions.emailPark(sessionID,contactID,toAddress,fromAddress,ccAddress,bccAddress,subject,bodyHtml,attachments,attachmentNames,isDraft,primaryDispositionId,secondaryDispositionId,tags,notes,originalAttachmentNames);
		Functions.emailUnpark(sessionID,contactID,isImmediate);
		Functions.emailPreview(sessionID,contactID);
		Functions.emailRestore(sessionID,contactID);
		Functions.dialerLogin(sessionID,skillName);
		Functions.dialerLogout(sessionID);
		Functions.snooze(sessionID,contactIDInt);
		Functions.dialAgent(sessionID,targetAgentIdInt,parentContactId);
		Functions.dialPhone(sessionID,phoneNumber,skillId,parentContactId);
		Functions.dialSkill(sessionID,skillId,parentContactId);
		Functions.sendDtmf(sessionID,sequence,duration,spacing);
		Functions.consultAgent(sessionID,targetAgentIdInt,parentContactId);
		Functions.transferCall(sessionID);
		Functions.conferenceCall(sessionID);
		Functions.acceptConsult(sessionID,contactID);
		Functions.amdOveride(sessionID,contactID,overrideType);
		Functions.sessionRecord(sessionID,contactID);
		Functions.sessionMask(sessionID,contactID);
		Functions.sessionUnmask(sessionID,contactID);
		Functions.independentDial(sessionID,contactID);
		Functions.dialOutcome(sessionID,contactID,outcome);
		Functions.holdWorkitem(sessionID,contactID);
		Functions.resumeWorkitem(sessionID,contactID);
		Functions.endWorkitem(sessionID,contactID);
		Functions.acceptWorkitem(sessionID,contactID);
		Functions.rejectWorkitem(sessionID,contactID);
		Functions.dialCallBack(sessionID,callbackId);
		Functions.rescheduleCallBack(sessionID,callbackId,rescheduleDate);
		Functions.postCancelCallBack(sessionID,callbackId,notes);
		Functions.session(stationPhoneNumber,inactivityTimeout,inactivityForceLogout,asAgentId);
		Functions.sessionJoin(asAgentId);
		Functions.deleteSession(sessionID,chatSessionIdInt);
		Functions.nextEvent(sessionID);
		Functions.reSkill(sessionID,continueReskill);
		 Functions.sessionDisposition(sessionID,contactIDInt,primaryDispositionIdInt,primaryDispositionNotes,primaryCommitmentAmount,primaryCallbackTime,primaryCallbackNumber,secondaryDispositionIdInt,previewDispositionId);
		Functions.sessionState(sessionID,state);
		Functions.feedback(sessionID,categoryId,priority,comment,customData);
		Functions.customData(sessionID,contactID,indicatorName,data);
		Functions.addContact(sessionID,chat,email,workitem);
		Functions.contactActivate(sessionID,contactIDInt);
		Functions.sessionMonitor(sessionID,targetAgentIdInt);
		Functions.sessionCoach(sessionID);
		Functions.sessionBarge(sessionID,targetAgentIdInt);
		Functions.sessionTakeOver(sessionID);
		
		//auth,reporting,real time, chat APIs
		Functions.resetAgentPassword(requestedPassword,agentId);
		Functions.changeAgentPassword(currentPassword, newPassword, agentId);
		Functions.queueCallback(phoneNumber,callerID,callDelaySec,skill,targetAgent,priorityManagement,intialPriority,acceleration,maxPriority,sequence,zipTone,screenPopSrc,screenPopUrl,timeout);
		Functions.createPromiseKeeper(firstName,lastName,phoneNumber,skill,targetAgent,promiseDate,promiseTime,notes,timeZone);
		Functions.createChatSession(pointOfContact,fromAddress,chatRoomID,parameters);
		Functions.endChatSession(chatSessionIdInt);
		Functions.getChatText(chatSessionId,timeout);
		Functions.sendSingleChatMessage(chatSessionId, label, message);
		Functions.postContactsChatSessionTyping(chatSessionId,isTyping,isTextEntered,label);
		Functions.postContactsChatSessionTypingPreview(chatSessionId,previewText,label);
		Functions.postContactChatSendEmail(fromAddress,toAddress,emailBody);
		Functions.getChatprofileByPOCId(pointOfContactId);
		Functions.createWorkItem(pointOfContact,workItemId,workItemPayload,workItemType,from);
		Functions.getAgentsState(updatedSince,fields);
		Functions.getAgentStateById(updatedSince,fields, agentId);
		Functions.getContactsActive(updatedSince,skillId,fields,mediaTypeId,campaignId,agentId,teamId,toAddr,fromAddr,stateId);
		Functions.getContactsParked(updatedSince,skillId,fields,mediaTypeId,campaignId,agentId,teamId,toAddr,fromAddr);
		Functions.getContactsState(updatedSince,agentId); 
		Functions.getSkillsActivity(updatedSince,fields);
		Functions.getSkillActivityById(updatedSince,fields,skillId);
		Functions.getAgentContactHistory(startDate,endDate,updatedSince,mediaTypeId,fields,skip,top,orderby, agentId);
		Functions.getRecentContactsInfo(startDate,endDate,fields,top, agentId);
		Functions.getAgentLoginHistory(startDate,endDate,searchString,fields,skip,top,orderby,agentId);
		Functions.getAgentStateHistory(startDate,endDate,mediaType, agentId);
		Functions.getAgentsPerformance(startDate,endDate,fields,agentId);
		Functions.getAgentPerformanceById(startDate,endDate,fields,agentId);
		Functions.getContactHistory(fields,contactId);
		Functions.GetSmstranscripts(startDate,endDate,transportCode,agentID,skip,top,orderby);
		Functions.GetSmstranscriptsbyContactid(contactId, startDate,endDate,transportCode,skip,top);
		Functions.getCompletedContacts(startDate,endDate,updatedSince,mediaType,fields,skip,top,orderby,skillId,campaignId,agentId,teamId,toAddr,fromAddr,isLogged,isRefused,isTakeover,tags,analyticsProcessed);
		Functions.callquality(contactID);
		Functions.getContactStateHistory(contactID);
		Functions.getContactCustomData(contactID);
		Functions.getSkillsSummary(startDate,endDate,mediaTypeId,isOutboundSkill: Boolean,fields);
		Functions.getSkillSummaryById(startDate,endDate,fields, skillId);
		Functions.getSkillsSlaSummary(startDate,endDate);
		Functions.getSkillSlaSummaryById(startDate,endDate, skillId);
		Functions.GetTeamPerformanceTotal(startDate,endDate,fields);
		Functions.GetTeamPerformancebyTeamIDTotal(teamId,startDate,endDate,fields);
		Functions.getReports();
		Functions.getReportJobs(fields,reportId,jobStatus,jobSpan);
		Functions.getReportJobById(jobId,fields);
		Functions.startReportJob(fileType,includeHeaders: Boolean,isAppendDate: Boolean,deleteAfter,overwrite,startDate,endDate, reportId);
		Functions.GetdatadownloadbyReportID(reportId,fileName,startDate,endDate,saveFile,includeHeaders);
		Functions.Getwfoascm(startDate,endDate,fields);
		Functions.Getwfoasi(startDate,endDate,fields);
		Functions.Getwfocsi(startDate,endDate,fields);
		Functions.Getwfoftci(startDate,endDate,fields);
		Functions.Getwfoosi(startDate,endDate,fields);
		Functions.Getwfoqm(startDate,endDate,fields);
		Functions.GetwfmSkillContacts(startDate,endDate,fields,mediaTypeId);
		Functions.Getwfmagetns(startDate,endDate,fields);
		Functions.GetwfmdailerContacts(startDate,endDate,fields);
		Functions.Getwfmscheduleadherence(startDate,endDate,fields);
		Functions.GetwfmAgentscorecard(startDate,endDate,fields);
		Functions.GetwfmAgentperformance(startDate,endDate,fields); */
  }
}