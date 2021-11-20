package com.incontact;


public class Output{
	
	public static String postAgent_param = "{\"agents\":[{\"firstName\":\"Nikhil\",\"middleName\":\"\",\"lastName\":\"M\",\"teamId\":\"0\",\"teamUuid\":\"\",\"reportToId\":1,\"internalId\":\"\",\"profileId\":0,\"roleId\":\"\",\"password\":\"\",\"forceChangeOnLogon\":true,\"emailAddress\":\"nikhil.m@incontact.com\",\"userName\":\"nikhil.m@sc1.com\",\"timeZone\":\"(GMT-07:00)Arizona\",\"country\":\"India\",\"state\":\"\",\"city\":\"Chennai\",\"chatRefusalTimeout\":15,\"phoneRefusalTimeout\":15,\"workItemRefusalTimeout\":15,\"defaultDialingPattern\":0,\"maxConcurrentChats\":1,\"useTeamMaxConcurrentChats\":false,\"isActive\":true,\"locationId\":0,\"notes\":\"\",\"hireDate\":\"10/10/1800\",\"terminationDate\":\"10/10/1800\",\"hourlyCost\":0,\"rehireStatus\":true,\"employmentType\":1,\"referral\":\"\",\"atHome\":false,\"hiringSource\":0,\"ntLoginName\":\"\",\"custom1\":\"\",\"custom2\":\"\",\"custom3\":\"\",\"custom4\":\"\",\"custom5\":\"\",\"scheduleNotification\":0,\"federatedId\":\"\",\"maxEmailAutoParkingLimit\":1,\"useTeamEmailAutoParkingLimit\":false,\"sipUser\":\"\",\"systemUser\":\"\",\"systemDomain\":\"\",\"crmUserName\":\"\",\"useAgentTimeZone\":false,\"timeDisplayFormat\":0,\"sendEmailNotifications\":false,\"apiKey\":\"\",\"telephone1\":\"\",\"telephone2\":\"\",\"userType\":\"\",\"isWhatIfAgent\":false,\"requestContact\":false,\"useTeamRequestContact\":false,\"chatThreshold\":1,\"useTeamChatThreshold\":false,\"emailThreshold\":1,\"useTeamEmailThreshold\":false,\"workItemThreshold\":1,\"useTeamWorkItemThreshold\":false,\"contactAutoFocus\":false,\"useTeamContactAutoFocus\":false,\"subject\":\"\",\"issuer\":\"\",\"isOpenIdProfileComplete\":false,\"recordingNumbers\":[{\"number\":\"2\"}]}]}";
	public static String putAgentbyAgentID_param = "{\"agents\":[{\"firstName\":\"\",\"middleName\":\"\",\"lastName\":\"M\",\"teamId\":\"0\",\"teamUuid\":\"\",\"reportToId\":1,\"internalId\":\"\",\"profileId\":0,\"roleId\":\"\",\"password\":\"\",\"forceChangeOnLogon\":true,\"emailAddress\":\"nikhil.m@incontact.com\",\"userName\":\"nikhil.m@sc1.com\",\"timeZone\":\"(GMT-07:00)Arizona\",\"country\":\"India\",\"state\":\"\",\"city\":\"Chennai\",\"chatRefusalTimeout\":15,\"phoneRefusalTimeout\":15,\"workItemRefusalTimeout\":15,\"defaultDialingPattern\":0,\"maxConcurrentChats\":1,\"useTeamMaxConcurrentChats\":false,\"isActive\":true,\"locationId\":0,\"notes\":\"\",\"hireDate\":\"10/10/1800\",\"terminationDate\":\"10/10/1800\",\"hourlyCost\":0,\"rehireStatus\":true,\"employmentType\":1,\"referral\":\"\",\"atHome\":false,\"hiringSource\":0,\"ntLoginName\":\"\",\"custom1\":\"\",\"custom2\":\"\",\"custom3\":\"\",\"custom4\":\"\",\"custom5\":\"\",\"scheduleNotification\":0,\"federatedId\":\"\",\"maxEmailAutoParkingLimit\":1,\"useTeamEmailAutoParkingLimit\":false,\"sipUser\":\"\",\"systemUser\":\"\",\"systemDomain\":\"\",\"crmUserName\":\"\",\"useAgentTimeZone\":false,\"timeDisplayFormat\":0,\"sendEmailNotifications\":false,\"apiKey\":\"\",\"telephone1\":\"\",\"telephone2\":\"\",\"userType\":\"\",\"isWhatIfAgent\":false,\"requestContact\":false,\"useTeamRequestContact\":false,\"chatThreshold\":1,\"useTeamChatThreshold\":false,\"emailThreshold\":1,\"useTeamEmailThreshold\":false,\"workItemThreshold\":1,\"useTeamWorkItemThreshold\":false,\"contactAutoFocus\":false,\"useTeamContactAutoFocus\":false,\"subject\":\"\",\"issuer\":\"\",\"isOpenIdProfileComplete\":false,\"recordingNumbers\":[{\"number\":\"2\"}]}]}";
	public static String putCPAManagementbySkillId_param = "{\"cpaSettings\":{\"abandonTimeout\":2,\"abandonMessagePath\":\"\",\"abandonMsgMode\":1,\"ansMachineDetMode\":2,\"ansMachineMsg\":\"\",\"exceptions\":[{\"attemptNo\":1,\"ansMachineDetMode\":2,\"ansMachineMsg\":\"\"},{\"attemptNo\":2,\"ansMachineDetMode\":2,\"ansMachineMsg\":\"\"}],\"treatProgressAsRinging\":true,\"preConnectCPAEnabled\":false,\"agentOverrideOptionFax\":true,\"agentOverrideOptionAnsweringMachine\":true,\"agentOverrideOptionBadNumber\":false,\"utteranceMinimumSeconds\":0.2,\"customerLiveSilenceSeconds\":1.1,\"machineMinimumWithAgentSeconds\":3,\"machineMinimumWithoutAgentSeconds\":3,\"machineEndSilenceSeconds\":1,\"machineEndTimeoutSeconds\":20,\"agentResponseUtteranceMinimumSeconds\":0.5,\"agentNoResponseSeconds\":1.4,\"agentVoiceThreshold\":10000,\"customerVoiceThreshold\":16000,\"preConnectCPARecording\":false,\"enableCPALogging\":false}}";
	public static String putskilldeliverypreference_param = "{\"deliveryPreferences\":{\"ConfirmationRequiredDisabled\":false,\"ConfirmationRequiredDeliveryType\":1,\"ConfirmationRequiredTimeout\":null,\"ConfirmationRequiredTimeoutSubsequent\":null,\"ConfirmationRequiredDefaultAccept\":true,\"ConfirmationRequiredDefault\":false,\"ComplianceRecordsDisabled\":false,\"ComplianceRecordsDeliveryType\":4,\"ComplianceRecordsTimeout\":null,\"ComplianceRecordsTimeoutSubsequent\":null,\"ComplianceRecordsDefaultAccept\":false,\"ShowComplianceButtonReschedule\":false,\"ShowComplianceButtonRequeue\":false,\"ShowComplianceButtonSnooze\":false,\"ShowComplianceButtonDisposition\":false,\"ShowPreviewButtonReschedule\":false,\"ShowPreviewButtonRequeue\":false,\"ShowPreviewButtonSnooze\":false,\"ShowPreviewButtonDisposition\":false}}";
	public static String putskillretrysetting_param = "{\"retrySettings\":{\"loadNonFresh\":true,\"finalizeWhenExhausted\":true,\"maximumAttempts\":10,\"minimumRetryMinutes\":240,\"maximumNumberOfHandledCalls\":10,\"restrictedCallingMinutes\":0,\"restrictedCallingMaxAttempts\":0,\"generalStaleMinutes\":30,\"callbackRestMinutes\":1440,\"releaseAgentSpecificCalls\":true,\"maximumNumberOfCallbacks\":10,\"callbackStaleMinutes\":15}}";
	public static String putschedulesettings_param = "{\"scheduleSettings\":{\"isScheduled\":false,\"sundayStartTime\":\"08:00\",\"sundayEndTime\":\"21:00\",\"sundayIsActive\":true,\"mondayStartTime\":\"08:00\",\"mondayEndTime\":\"21:00\",\"mondayIsActive\":true,\"tuesdayStartTime\":\"08:00\",\"tuesdayEndTime\":\"21:00\",\"tuesdayIsActive\":true,\"wednesdayStartTime\":\"08:00\",\"wednesdayEndTime\":\"21:00\",\"wednesdayIsActive\":true,\"thursdayStartTime\":\"08:00\",\"thursdayEndTime\":\"21:00\",\"thursdayIsActive\":true,\"fridayStartTime\":\"08:00\",\"fridayEndTime\":\"21:00\",\"fridayIsActive\":true,\"saturdayStartTime\":\"08:00\",\"saturdayEndTime\":\"21:00\",\"saturdayIsActive\":true}}";
	public static String putxssettings_param = "{\"xsSettings\":{\"xsScriptID\":123,\"xsCheckinScriptID\":345,\"externalOutboundSkill_No\":\"152167\",\"xsSkillChangedActive\":true,\"xsGetContactsActive\":true,\"xsFreshThreshold\":789,\"xsAvailableThreshold\":123,\"xsReadyThreshold\":456,\"xsNumberToRetrieve\":478}}";
	public static String putTeam_param = "{\"teams\":[{\"teamName\":\"team\",\"isActive\":true,\"maxConcurrentChats\":8,\"wfoEnabled\":false,\"wfm2Enabled\":false,\"qm2Enabled\":false,\"inViewEnabled\":false,\"notes\":\"\",\"maxEmailAutoParkingLimit\":25,\"inViewGamificationEnabled\":false,\"inViewChatEnabled\":false,\"inViewLMSEnabled\":false,\"analyticsEnabled\":false,\"requestContact\":false,\"chatThreshold\":1,\"emailThreshold\":1,\"workItemThreshold\":1,\"voiceThreshold\":1,\"contactAutoFocus\":false,\"teamUuid\":\"\",\"teamLeadId\":\"\"}]}";
	public static String putTeambyTeamID_param = "{\"forceInactive\":false,\"team\":{\"teamName\":\"team\",\"isActive\":true,\"maxConcurrentChats\":8,\"wfoEnabled\":false,\"wfm2Enabled\":false,\"qm2Enabled\":false,\"inViewEnabled\":false,\"notes\":\"\",\"maxEmailAutoParkingLimit\":25,\"inViewGamificationEnabled\":false,\"inViewChatEnabled\":false,\"inViewLMSEnabled\":false,\"analyticsEnabled\":false,\"requestContact\":false,\"chatThreshold\":1,\"emailThreshold\":1,\"workItemThreshold\":1,\"voiceThreshold\":1,\"contactAutoFocus\":false,\"teamUuid\":\"\",\"teamLeadUuid\":\"\",\"NICEAnalyticsEnabled\":false,\"NICEAudioRecordingEnabled\":false,\"NICECoachingEnabled\":false,\"NICEDesktopAnalyticsEnabled\":false,\"NICELessonManagementEnabled\":false,\"NICEPerformanceManagementEnabled\":false,\"NICEQmEnabled\":false,\"NICEQualityOptimizationEnabled\":false,\"NICEScreenRecordingEnabled\":false,\"NICEShiftBiddingEnabled\":false,\"NICESpeechAnalyticsEnabled\":false,\"NICEStrategicPlannerEnabled\":false,\"NICESurvey_CustomerEnabled\":false,\"NICEWfmEnabled\":false}}";
	public static String assignUnavailableCodesToTeam_param = "{\"codes\":[{\"outstateId\":\"\"}]}";
	public static String postCampaign_param="{\"campaigns\":[{\"campaignName\":\"New\",\"isActive\":true,\"description\":\"\",\"notes\":\"\"}]}";
	public static String putCampaignsByID_param="{\"campaigns\":{\"campaignName\":\"Next\",\"isActive\":false,\"description\":\"\",\"notes\":\"\"}}";
	public static String postCampaignsBySkill_param="{\"skills\":[{\"skillId\":247361}]}";
	public static String deleteCamapaignBySkill_param="{\"skills\":[{\"skillId\":247361,\"transferCampaignId\":1}]}";
    public static String putunavailablecodeByID_param="{\"unavailableCodeName\":\"New\",\"postContact\":true,\"agentTimeout\":3,\"isActive\":true}";
    public static String posthours_param="{\"profileName\":\"New\"}";
	public static String postpointofcontact_param="{\"pointOfContact\":\"New\",\"pointOfContactName\":\"New\",\"skillId\":3,\"isActive\":true,\"mediaTypeId\":0,\"scriptName\":\"script\",\"ivrReportingEnabled\":true}";
    public static String putpointofcontactByID_param="{\"pointOfContactName\":\"New\",\"skillId\":3,\"isActive\":true,\"scriptName\":\"script\"}";
    public static String postUnavailableCodes_param="{\"unavailableCodeName\":\"New\",\"postContact\":true,\"isActive\":true}";
    public static String putunavailablecodebyteam_param = "{\"teams\":[{\"teamId\":\"\"}]}";
    public static String postjobsync_param= "{\"entityName\":\"qm-workflows\",\"version\":\"1\",\"startDate\":\"2019-07-10\",\"endDate\":\"2019-07-15\"}";
    //public static String getmediaplaybackbyid_param="{\"contactid\":\"21bf4a34-f768-45d4-aed6-6bd754dd49d2\",\"media-type\":\"chat\",\"exclude-waveform\":\"true\"}";
    public static String postWorkflowData_param= "{\"profile\":[{\"ProfileName\":\"New22\",\"Description\":\" \",\"ProfileID\":\"0\"},{\"date\":[{\"date\": \" \",\"Value\": [{\"abc\"}],\"Visible\": \"\", \"Type\":\"\", \"Ref\":\"\"}]}}";
    public static String postjobs_param= "{\"entityName\":\"qm-workflows\",\"version\":\"1\",\"startDate\":\"2019-07-10\",\"endDate\":\"2019-07-15\"}";
	
	public static String postsuppresedcontact_param= "{\"suppressedContactData\":[{\"startDate\":\"2019-07-10\",\"endDate\":\" 2019-07-15\",\"value\":\"0\",\"skills\":\"0\"}}";
    
	public static String Putbusinessunittimezone_param= "{\"timezones\":\"qm-workflows\",\"items\":\"1\"}";
	
	public static String PutListManagement_param = "{\"displayField1Name\":\"sam1\",\"displayField2Name\":\"sam2\",\"listOrderingOptions\":[{\"orderType\":\"\",\"direction\":\"\"}]}";
	
	public static String PutCadenceSettings_param = "{\"attemptMode\":\"\",\"recordRequestMode\":\"\",\"destinationRetryRestMinutes\":\"\",\"maximumAttempts\":[{\"fieldName\":\"\",\"attempts\":0}],\"cadences\":[{\"fieldName\":\"\",\"attempts\":0,\"timeConstraints\":{\"weekdayTimeConstraintsweekdayTimeConstraints\":[{\"startTime\":\"2019-07-10\",\"endTime\":\"2019-07-15\"}],\"weekendTimeConstraints\":[{\"startTime\":\"2019-07-10\",\"endTime\":\"2019-07-15\"}]}}]}";
	
	public static String PostSetDisposition_param="{\"dispositionInfo\":{\"Skill\":1007,\"DispositionCode\":1,\"CallbackNumber\":0,\"CallbackTime\":\"2019-07-10\,\"notes\":\"1\",\"CommitmentAmount\":\"\"}}";
	
	
	public static void main(String[] args) throws Exception {

		  Methods m = new Methods();
		  m.initTable();
	      m.readingProperties();
	      m.verifyAccessToken(); 
	      m.getAgents();
		  m.GetagentbyAgentID("207932","");
		  m.getUnavailableCodes("false");
		  m.GetCPAManagement("152167","");
		  m.Getdeliverypreference("152167","");
		  m.GetSkillParameterGeneralSetting("152167","");
		  m.Getskillretrysetting("152167","");
		  m.Getschedulesettings("152167");
		  m.Getxssettings("152167","");
		  m.PostAgent(postAgent_param);
		  m.PutAgentbyAgentID("207932",putAgentbyAgentID_param);
		  m.PutCPAManagementbySkillId("152167",putCPAManagementbySkillId_param);
		  m.Putskilldeliverypreference("152167",putskilldeliverypreference_param);
		  m.Putskillretrysetting("152167",putskillretrysetting_param);
          m.Putschedulesettings("12345",putschedulesettings_param);	
          m.Putxssettings("152167",putxssettings_param);
          m.PutTeam(putTeam_param);
          m.PutTeambyTeamID("12345",putTeambyTeamID_param);
          m.assignUnavailableCodesToTeam("55852",assignUnavailableCodesToTeam_param);  
		  m.GetAccesskeys("207932", "", "", "", "");
		  m.PostAccessKeys("207932");
		  m.GetAccesskeysById("4FE4QQJVVDVJZWQ7NGBE4YF35O7DEHRJ74XPPQNQ3MYYKFNHIYBA====");
		  m.DeleteAccessKeysByID("4FE4QQJVVDVJZWQ7NGBE4YF35O7DEHRJ74XPPQNQ3MYYKFNHIYBA====");
		  m.PatchAccessKeyByID("4FE4QQJVVDVJZWQ7NGBE4YF35O7DEHRJ74XPPQNQ3MYYKFNHIYBA====","true");
		  m.PostCamapigns(postCampaign_param);
		  m.PutCampaignsByID(putCampaignsByID_param, 72432);
		  m.PostCamapignsBySkill(postCampaignsBySkill_param,72432 );
		  m.DeleteCamapaignsBySkill(deleteCamapaignBySkill_param,72432);
		  m.GetSmsHistoricalTranscriptsByContactID(268480840,"141014");
		  m.GetSmsHistoricalTranscripts("141014","454","247361");
		  m.PutUnavailableCodesByID(putunavailablecodeByID_param,1234);
		  m.PostHoursofOperation(posthours_param);
		  m.PuthoursofOperationByID(4);
		  m.PostPointofContact(postpointofcontact_param);
		  m.PutPointofContactByID(12345,putpointofcontactByID_param);
		  m.PostUnavailableCodes(postUnavailableCodes_param);
		  m.GetPhonenumbers("", "", "");
		  m.GetDispositionBySkills("", "", "", "", "", "","");
		  m.PutUnavailableCodeByIDTeams(putunavailablecodebyteam_param, 12345,"");
		  m.PostActivate("WVVsLzlGTkc1T1hUTVpNVDlXR2Z2WC9KQWo3K3ZxZzFFTXNDQUxISA==",9471795); 
		  m.getTeamsAgentbyTeamId("1150", "", "", "", "", "", "");
		  m.PostjobsSync(postjobsync_param);
		  m.GetMediaplaybackbyID("21bf4a34-f768-45d4-aed6-6bd754dd49d2", "chat", "true");
		  m.GetMediaplaybackbysegmentID("00add115-4071-44ec-a099-27ceaee2ecca", "8e4fed5a-155d-4179-b8d5-785b2f140377", "chat", "true");
		  m.GetMediaplaybackContacts("master id 1", "chat", "true");
		  m.GetActiveFlag("1");
		  m.PostWorkflowData(postWorkflowData_param);
		  m.GetWorkflowDataByID("5645");
		  m.PutWorkflowDataByID("5645");
		  m.PutWorkflowDataByIdActivate("5645");
		  m.PutWorkflowDataByIdDeactivate("5645");
		  m.GetJobs();
		  m.Postjobs(postjobs_param);
		  m.GetJobsByID("12345");
		  m.GetBusinessunitTimezone();
		  m.GetSuppresedContacts();
		  m.PostUnavailableCodes(postsuppresedcontact_param);
		  m.Putbusinessunittimezone(Putbusinessunittimezone_param);
		  m.GetTiemZoneBySkillId(12358);
		  m.PutTiemZoneBySkillId(12345);
		  m.deleteSuppressedContactsbyId("12345");
		  m.getSuppressedContactsbyId("12345");
		  m.putSuppressedContactsbyId("12345", param);
		  m.getContactsByIdHierachy("12345");
		  
		  m.GetBusinessunitTimezone();
		  m.GetSuppresedContacts();
		  m.PostUnavailableCodes(postsuppresedcontact_param);
		  m.Putbusinessunittimezone(Putbusinessunittimezone_param);
		  m.GetTiemZoneBySkillId(12358);
		  m.PutTiemZoneBySkillId(12345);
		  m.DeleteskillsbyskillIdagentId(1234,12365);
          m.getScripts("Nikhil\Script with PAGE CLOSE - IB chat",891660);
		  m.postScript("Nikhil\Script with PAGE CLOSE - IB chat",param);
		  m.updateScripts("Nikhil\Script with PAGE CLOSE - IB chat","true");
	      m.PostScriptKick("Nikhil\Script with PAGE CLOSE - IB chat");
		  m.getScriptsHistoryByName("Nikhil\Script with PAGE CLOSE - IB chat");
		  m.PutListManagement("152167",PutListManagement_param);
		  m.GetSkillsParameters();
		  m.PutCadenceSettings("152167",PutCadenceSettings_param);
		  m.GetSkillsSkillIdParameters("152167");
		  m.PostSetDisposition(PostSetDisposition_param, 268480840);
		  m.GetBusinessunitOutboundRoutes();
		  
		  m.getclientdata();
		  m.putclientdata();
		  m.smsoutbound("");
		  m.removeprospects("");
		  m.persistentcontacts(9471795);
		  m.agentsettings(12365);
		  m.getscriptsbyId(12358);
		  m.scriptssearch();
		  m.deletescripts();
		  m.transformnumbers(12365);
		  
		  
		  
	}
}
