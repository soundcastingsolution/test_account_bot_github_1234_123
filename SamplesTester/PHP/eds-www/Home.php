<?php
//C:\EasyPHP-Devserver-17\eds-binaries\php\php713vc14x86x180115144606\ext\php_curl.dll
 include 'PHPFunctions\function.php';
 //include 'PHPFunctions\Authenication.php';
 include 'SessionGlobal.php';
 include_once 'Output.php';
?>
<!DOCTYPE HTML>
<html>  
<body >
<h2>API Test Results for PHP Functions</h2> <title>PHP</title>
<?php

//version 18
@getScripts18();
@postScripts18();
@putScripts18();
@postScripts18Kick();
@getScripts18HistoryByName();

//version 17
@getBusinessUnitTimezone();
@getSuppresedContacts();
@PostSuppresedContacts();
@PutbusinessunitTimezone();
@PutSkillIDParameterTimezone(123,'');
@getSkillIDParameterTimezone(123);
//version 17
@getBusinessUnitTimezone();
@getSuppresedContacts();
@PostSuppresedContacts();
@PutbusinessunitTimezone();
@PutSkillIDParameterTimezone(123,'');
@getSkillIDParameterTimezone(123);
@DeleteskillsSkillIDAgentID(12345,12365);
//version 16
@postJobSync();
@getmediaplaybackByID('21bf4a34-f768-45d4-aed6-6bd754dd49d2','chat',true);
@getmediaplaybackBysegemntID('21bf4a34-f768-45d4-aed6-6bd754dd49d2','b171d215-a152-4572-bd45-56670cb7a859','chat',true);
@getmediaplaybackcontacts('master id 1','chat',true);
@getActiveFlag('1');
@postWorkflowData();
@getWorkflowDataById(56765);
@PutWorkflowDataById(56765);
@PutWorkflowDataByIdActivate(56765);
@PutWorkflowDataByIdDeactivate(56765);
@getJobs();
@postJobs();
@getJobsByID('12345');
//version 15
@PutUnavailableCodesByID(3591);
@getAccessKeys(27730,'','','','');
@PostAccessKeys(27730);
@deleteAccessKeysByID('4FE4QQJVVDVJZWQ7NGBE4YF35O7DEHRJ74XPPQNQ3MYYKFNHIYBA====');
@getACcesskeysByID('4FE4QQJVVDVJZWQ7NGBE4YF35O7DEHRJ74XPPQNQ3MYYKFNHIYBA====');
@PatchAccessKeysByID('4FE4QQJVVDVJZWQ7NGBE4YF35O7DEHRJ74XPPQNQ3MYYKFNHIYBA====',true);
@PostHoursofOperation();
@PutHoursofOperationByID(4);
@PostPointofContact();
@PutPointofContactByID(12345);
@PostUnavailableCodes();
@PostCampaigns();
@PutCampaignsByID(9207);
@getDispostionBySkill('','','','','','','');
@DeleteCampaignsBySkill(9207);
@PostCampaignsBySkill(9207);
@getSmsHistoricalTranscripts(141014,268480840);
@getSmsHistoricalContacts(141014,454,247361);
@PutUnavailableCodesByIDTeams(12345,'');
@postEmailSaveDraft('WVVsLzlGTkc1T1hUTVpNVDlXR2Z2VzhVSzJrdTJ4djY5SDl2Q3lTQw==',247361);
@postAddText('WVVsLzlGTkc1T1hUTVpNVDlXR2Z2VzhVSzJrdTJ4djY5SDl2Q3lTQw==');

 version 17
 
 @deleteSuppressedContactBySuppressedContactId(12345)
 @getContactsByIdHierachy(12345)
 @getSuppressedContactBySuppressedContactId(12345)
 @putSuppressedContactBySuppressedContactId(12345)

//version 18

@putSkillsListManagementSkillId(12473)
@getSkillsParameters()
@putSkillsCadenceSettingsSkillId(12473)
@getSkillsSkillIdParameters(12473)
@postSetDisposition(2237919921)
@getBusinessUnitOutboundRoutes()

//v19

@getclientdata()
@putcientdata()
@postsmsoutbound('')
@removeprospects("")
@persistentcontacts(9471795)
@agentsettings(12365)
@getscriptsbyId(12358)
@scriptssearch()
@deletescripts()
@transformnumbers(12365)

//version 13
/* @getAgents();
@PostAgentV13();
@updateAgents(1143);
@getTeam('','');
@CreateTeamV13('newTeam',true);
@getTeambyTeamID(1042);
@UpdateTeam(1042,'myTeam',true);
@getTeamUnavailableCode(1042);
@putTeamUnavailableCode(1042);
@getGeneralUnavailableCode();
@getSkills();
@UpdateTeamV13();
@postSkill('newSkill');
@getSkillsBySkillId(12473);
@updateSkill($skillobj,12473);
@getSkillsGeneralSettingBySkillId(12473,$fields='');
@updateSkillsGeneralSettingBySkillId(12473);
@getCpamanagementBySkillId(12473,$fields='');
@updateSkillsCpamanagementBySkillId(12473);
@getXsSettingsSettingsBySkillId(12473,$fields='');
@updateSkillsXsSettingsBySkillId(12473);
@getDeliveryPreferencesBySkillId(12473,$fields='');
@updateSkillsDeliveryPreferencesBySkillId(12473);
@getRetrySettingsBySkillId(12473,$fields='');
@updateSkillsRetrySettingsBySkillId(12473);
@getScheduleSettingsBySkillId(12473);
@updateSkillsSchedulesettingsBySkillId(12473); */


//version 12

//@getStandardAddressBookEntries(12345);
//@PostAgent();

//@getAddressbook($_SESSION["resource_server_base_uri"]);
//@Demo($_SESSION["resource_server_base_uri"]);
//@Demo2('1','2');
//@Authentication();
//@postReportjobsDatadownlaodByReportId('1234');
//pass this in SC1
//@postAgentState('207932', 'Available','');
//@getAgentGroupsByAgentId('207932','');
//skill id 152264 agentid 207932
//@DeleteSkillIDAgentID('152266','207932');
//@getAgentSkillsByAgentId('207932','','');
//@createTeamsAgentbyTeamId('111','111');
//@deleteTeamsUnavailablebyTeamId('34241','');
//@createTeamsUnavailablebyTeamId(' ','');

//@GetCountries();
//@GetCountriesBycountryId('1');
//@getDispositions();
//@getDataDefinitionDataTypes();
//@deleteFileByName('hello');
//@createFile();
//@updateFile();
//@deleteFolderByName();

//@getFolders();
//@GetLocations();
//@GetHiringSources();
//@postHiringSources('posthir');
//@PostMessageTemplate();
//@getMessageTemplatesbyTemplateId();
//@UpdateMessageTemplateByTemplateId('1');
//@GetPhoneCodes();
//@GetSecurityProfilesById();

//@GetTags();
//@getTagsByTagId('1');
//@UpdateTagsByTagId(1);
//@PostTags('testtags','test');
//@createDispositions('test123',false,1);
//@getDispositionsByDispositionID(4587,'');
//@updateDispositionsByDispositionID(4587,'testthree',false,'1',true);
//@DeleteSkillIDForAgentID(165451,-1);
//@getDispositionsByClassifications();
//@createFilesExternal('pashamaktum','',false,false);
//@deleteFolderByName();

//@PostSkillsToAgent(123,345);
//@AssignSkillsToAgents();
//@GetEmailTranscript(2237331756,'');
//@getContactFileByContactId(2237331756,'');
//@getListsCallListListIdAttempts(100);
//@getListsCallListJobs();
//@getListsCallListJobsByJobId(100);
//@postContactsTagsByContactId(2237331756,'');
//@postCalllistListUpload(100);
//@deleteListsCallListsJobsByJobId(100);
//@getGroups();

@createGroups('testgroup',true,'test the group creation');
//@getGroupsByGroupId(313,'');
//@updateGroupsByGroupId(313,'testgroup',true,'test');
//@deleteAgentGroupsByGroupId(313,1);
//@getAgentGroupsByGroupId(313);
//@postAgentGroupsByGroupId(313);
//@getThankYouForSkillId(12365);
//@postContactChatSendEmail('maktum.pasha@hc8.com', 'rafiq.j@servion.com', 'test12222');

//@AssignSkillsToAgent('207932','152264','','');
//@UpdateAgentSkill('207932','152264','','');
//@GetAgentUnassignedSkills('207932','','','','','','','','true');
//@PostAgentMessages();
//@deleteAgentMessagesByMessageId('1234');
//@getAgentPatterns();
//@getAgentStates();
//@CreateTeam();
//@UpdateTeam('38077');
//@getTeamsAgents();
//@deleteTeamsAgentbyTeamId('34560','34241');
//@getTeamsAgentbyTeamId('34241');
//@GetwfmSkillContacts('2018-02-28T15:50:00','2018-02-28T15:53:00','1');
//@Getwfmagetns('2018-02-28T15:53:00','2018-03-02T15:53:00');
//@GetwfmdailerContacts('2018-02-28T15:53:00','2018-03-02T15:53:00');
//@Getwfmscheduleadherence('2018-02-28T15:50:00','2018-02-28T15:53:00');
//@GetwfmAgentscorecard('2018-02-28T15:50:00','2018-02-28T15:53:00');
//@GetwfmAgentperformance('2018-02-28T15:50:00','2018-02-28T15:53:00');
//@Getwfoascm('2018-02-28T15:53:00','2018-02-28T15:53:00' );
//@Getwfoasi('2018-02-28T15:53:00','2018-02-28T15:53:00' );
//@Getwfocsi('2018-02-28T15:53:00','2018-02-28T15:53:00' );
//@Getwfoftci('2018-02-28T15:53:00','2018-02-28T15:53:00' );
//@Getwfoosi('2018-02-28T15:53:00','2018-02-28T15:53:00' );
//@Getwfoqm('2018-02-28T15:53:00','2018-02-28T15:53:00' );
//@getAgentLoginHistory('207932','2018-02-28T15:53:00','2018-02-28T15:53:00');
//@GetSmstranscripts( '2018-03-01T15:53:00', '2018-03-15T15:53:00','12345');
//@GetSmstranscriptsbyContactid( '2238306091','2018-03-01T15:53:00', '2018-03-15T15:53:00','1245');
//@GetSmsCallQuality( '2238306091');
//@GetTeamPerformanceTotal('2018-02-28','2018-02-28' );
//@GetTeamPerformancebyTeamIDTotal( 55538, '2018-02-28', '2018-03-01');
//@PostdatadownloadbyReportID(1,'sample.csv','2018-02-28','2018-03-01'); // method not allowed

// Patron
//@postContactsChatSessionTyping('T0xZcHJzV1VkU3dPZHFMb0FVMTdQdkdscU1zZDc0WjJyWEUvdVhBTWR3PT0=',true,true,$label="testLabel");
//@postContactsChatSessionTypingPreview('T0xZcHJzV1VkU3dPZHFMb0FVMTdQdkdscU1zZDc0WjJyWEUvdVhBTWR3PT0=');
//@getChatprofileByPOCId('226fca34-e379-469b-8534-5b25a35e51a6');

// Agents
//@postSessionIdInteractionsContactIdtyping('b1Exa29hMTVkaHFFdGJsUGhGUFA4YjZmWEpDOUVGN28wdERmRlVjanRnS0RVK1E9',2244010089);
//@posttAgentSessionSessionIdInteractionAddEmail('b1Exa29hMTVkaHFFdGJsUGhGUFA4YjZmWEpDOUVGN28wdERmRlVjanRnS0RVK1E9');
//@posttAgentSessionIdInteractionConatactIdParkemail('b1Exa29hMTVkaHFFdGJsUGhGUFA4ZGlOR1ZPK3N1SFNyblcwSEJnRGNRc1M4Vkk9','2237919921');
//@posttAgentSessionIdInteractionConatactIdUnparkemail('b1Exa29hMTVkaHFFdGJsUGhGUFA4ZGlOR1ZPK3N1SFNyblcwSEJnRGNRc1M4Vkk9','2237919921');
//@posttAgentSessionIdInteractionConatactIdPreview('b1Exa29hMTVkaHFFdGJsUGhGUFA4ZGlOR1ZPK3N1SFNyblcwSEJnRGNRc1M4Vkk9', '2237919921');
//@posttAgentSessionIdInteractionConatactIdEmailRestore('b1Exa29hMTVkaHFFdGJsUGhGUFA4ZGlOR1ZPK3N1SFNyblcwSEJnRGNRc1M4Vkk9', '2237919921');
//@posttAgentSessionIdInteractionConatactIdSnooze('b1Exa29hMTVkaHFFdGJsUGhGUFA4ZGlOR1ZPK3N1SFNyblcwSEJnRGNRc1M4Vkk9', '2237919921');
//@CancelCallback('QzJWa2JUZUthTmcwb1dPMXcwd2xTa2FJamY3QTZGQ244YW5CckZjWGdQWEhlUjg9','2237919921' , 'hi');
//@posttAgentSessionIdAddcontact('QzJWa2JUZUthTmcwb1dPMXcwd2xTa2FJamY3QTZGQ244YW5CckZjWGdQWEhlUjg9');
//@posttAgentSessionIdInteractionConatactIdActivate('QzJWa2JUZUthTmcwb1dPMXcwd2xTa2FJamY3QTZGQ244YW5CckZjWGdQWEhlUjg9', 2237919921);
//@getContactsParked();

// function to display the output
generateoutput();

?>

</body>
</html>

