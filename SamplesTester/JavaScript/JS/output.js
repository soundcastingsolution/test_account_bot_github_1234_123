
function ConstructArray(APIName,APISubType,APIType,Version,response)
{
	/*var table = document.getElementById("customers");
    var row = table.insertRow(1);
    var APINameCell = row.insertCell(0);
    var APISubTypeCell = row.insertCell(1);
	var APITypeCell = row.insertCell(2);  
	var VersionCell = row.insertCell(3);
	var responseCell = row.insertCell(4);
	
	APINameCell.innerHTML = APIName;
	APISubTypeCell.innerHTML = APISubType;
	APITypeCell.innerHTML = APIType;
	VersionCell.innerHTML = Version;
	responseCell.innerHTML = response;*/


 var addNewReuslt = "<tr><td>" + APIName + "</td><td>" + APISubType +" </td><td>" +APIType+ "</td><td>" +Version + "</td><td>" +response+"</td></tr>";
 $( "#customers" ).append( addNewReuslt );
 
 // sorting result
 SortingResult();
}

function GenerateOutput()
{
	
	
     //Version 15
	 getAccessKeys(1140,'','','','');
	 postAccessKeys(1140);
	 getAccessKeysByID('4FE4QQJVVDVJZWQ7NGBE4YF35O7DEHRJ74XPPQNQ3MYYKFNHIYBA====');
	 PatchAccessKeysByID('4FE4QQJVVDVJZWQ7NGBE4YF35O7DEHRJ74XPPQNQ3MYYKFNHIYBA====','');
	 deleteAccessKeysByID('4FE4QQJVVDVJZWQ7NGBE4YF35O7DEHRJ74XPPQNQ3MYYKFNHIYBA====');
	 PostCampaigns('new','true','','');
	 PutCampaignsByID(72432,'file','true','','');
	 PostCampaignsBySkill(72432,247361);
	 DeleteCampaignsBySkill(72432,247361,0);
	 getSmsHistoricalTranscriptByContactID(268480840,141014);
	getSmsHistoricalContacts(4005150006,141014,247361);
	PutUnavailableCodesByID(3588,'new','false',120,'true');
	PostHoursofOperation('hours');
	PutHoursofOperationByID(4);
	PostPointofContact();
	PutPointofContactByID(1234);
	PostUnavailableCodes();
	getPhonenumbers('','','');
	getDispositionSkills('','','','','','','');
	postEmailSaveDraft('WVVsLzlGTkc1T1hUTVpNVDlXR2Z2WC9KQWo3K3ZxZzFFTXNDQUxISA==',9471795);
	postAddText('WVVsLzlGTkc1T1hUTVpNVDlXR2Z2WC9KQWo3K3ZxZzFFTXNDQUxISA==');
	putUnavailableCodesbyIdTeams(1234);
	
	//version 16
	postJobSync();
	getMediaplayBackByID('21bf4a34-f768-45d4-aed6-6bd754dd49d2');
	getMediaplayBackBysegmentID('21bf4a34-f768-45d4-aed6-6bd754dd49d2','b171d215-a152-4572-bd45-56670cb7a859');
	getMediaplayBackcontacts('master id 1');
	getActiveFlag('1');
	postWorkflowData();
	getWorkflowDataById(5645);
	putWorkflowDataById(5645);
	putWorkflowDataByIdDeactivate(5645);
	putWorkflowDataByIdActivate(5645);
	getJobs();
	postJobs();
	getJobsByID('12345');
	deleteSuppressedContactBySuppressedContactId('12345');
	getSuppressedContactBySuppressedContactId('12345');
	putSuppressedContactBySuppressedContactId('12345');
	getContactsByIdHierachy('12345');
	
	
	
	//version 17
	getBusinessunitTimezone();
	getSuppresedContacts();
	PostSuppresedContacts();
	putbusinessunitTimezone();
	PutSkillIDParameterTimezone(1234);
	getTimeZoneBySkillId(12345);
	
	//version 17
	getBusinessunitTimezone();
	getSuppresedContacts();
	PostSuppresedContacts();
	putbusinessunitTimezone();
	PutSkillIDParameterTimezone(1234);
	getTimeZoneBySkillId(12345);
	deleteSkillsBySkillIdAgentId(12345,12365);
	getScripts("Nikhil\Script with PAGE CLOSE - IB chat",891660);
	postScripts("Nikhil\Script with PAGE CLOSE - IB chat",body);
	updateScripts("Nikhil\Script with PAGE CLOSE - IB chat","true");
	postScriptsKick("Nikhil\Script with PAGE CLOSE - IB chat");
	getScriptsHistoryByName("Nikhil\Script with PAGE CLOSE - IB chat");
	PutSkillParameterListManagement(247361);
	GetSkillsParameters();
	PutSkillParameterCadenceSettings(247361);
	GetSkillsSkillIdParameters(247361);
	PostContactsetDisposition(2237919921);
	getBusinessunitOutboundRoutes();
	
	//v19
	
	getclientdata();
	putcientdata();
	postsmsoutbound('');
	removeprospects("");
	persistentcontacts(9471795);
	agentsettings(12365);
	getscriptsbyId(12358);
	scriptssearch();
	deletescripts();
	transformnumbers(12365);
	
  //version 13
    // getAgents('2018-03-01T15:53:00', 'true', 'vr', ' ', '0', '100', ' ');
    // PostAgentV13();
    // GetagentbyAgentIDV13(1140, '');
    // PutAgentbyAgentIDV13(1140);
    // getTeams('', '', 'true', '', '0', '10000', '');
    // postTeam();
    // getTeamById(1042, '');
    // PutTeambyTeamIDV13(1042);
    // getUnavailableCodesByTeam(1042);
    // PutUnavailableCodesbyTeamID(1042);
    // getUnavailableCodes();
    // getSkills();
    // PostCreateSkillV13();
    // getSkillById(1268);
    // PutSkillbySkillID(1268);
    // GetSkillParameterGeneralSettingV13(1268, '');
    // PutSkillParameterGeneralSettingv13(1268);
    // getSkilCpamanagement(1268);
    // PutSkillParameterCpamanagement(1268);
    // getSkillxssettings(1268);
    // PutSkillParameterXssettings(1268);
    // getSkillDeliveryPreferences(1268);
    // PutSkillParameterDeliveryPreferences(1268);
    // getSkillRetrysettings(1268);
    // PutSkillParameterRetrySettings(1268);
    // getSkillScheduleSettings(1268);
    // PutSkillParameterScheduleSettings(1268);

	//version 12
	// getStandardAddressBookEntries(12345,'','','','','','');
	// PostAgent();
	
	//version 11
	// getAgentSkillsByAgentId('210452','','','','','','','','');
	// PutAgentbyAgentID('210452');
	// GetagentbyAgentID('210452');
	// GetAgentByAgentIDGroups('210452','');
	// PostAgentMessages('hi','2018-02-07T06:22:27.143Z','hi','210452','Agent','2018-02-07T06:23:27.143Z','5');
	// deleteAgentMessagesByMessageId('12345');
	// getTeam('','','','','','','');
	// PutTeam();
	// PutTeambyTeamID('38077');
	// GetTeamAgent('','');
	// GetTeamByAgentID('38077','');
	// GetBrandingProfile('1043','');
	// GetDispostion('','','','','','','');
	// DeleteFiles('sample');
	// PostCreateFileName('rafiqSample', 'MTAwMQlNaXNzaW5nIEV4dGVybmFsIElELg0KMTAwMglNaXNzaW5nIEV4dGVybmFsIElELg0KMTAwMwlNaXNzaW5nIEV4dGVybmFsIElELg0KMTAwNAlNaXNzaW5nIEV4dGVybmFsIElELg0KMTAwNQlNaXNzaW5nIEV4dGVybmFsIElELg0KMTAwNglNaXNzaW5nIEV4dGVybmFsIElELg0K', 'true');
	// PutupdateFile( 'rafiqSample', 'rafiqSample', true);
	// GetFilesExternal('CallingLists');
	// PostCreateFileExternal('testfile','MTAwMQlNaXNzaW5nIEV4dGVybmFsIElELg0KMTAwMglNaXNzaW5nIEV4dGVybmFsIElELg0KMTAwMwlNaXNzaW5nIEV4dGVybmFsIElELg0KMTAwNAlNaXNzaW5nIEV4dGVybmFsIElELg0KMTAwNQlNaXNzaW5nIEV4dGVybmFsIElELg0KMTAwNglNaXNzaW5nIEV4dGVybmFsIElELg0K',true,true);
	// PutupdateFilesExternal('testfile',false);
	// DeleteFolders('folderName');
	// GetFolders('CallingLists');
	// PostcreateDispositions('dip2332',false,'5');
	// GetDispostionByID('4552');
	// PutUpdateDispostionbydispositionId('5','dip2332',false,1, true);
	// GetDispostionByClassification('');
	// PostCreateSkill();
	// PutSkillbySkillID('162779');
	// GetDispostionBySkillid('162779','','','','','' );
	// GetDispostionBySkillidUnAssigned('162779','','','','','');
	// GetSkillParameterGeneralSetting('165455',''); // personal call skill id
	// PutSkillParameterGeneralSetting('165455'); // personal call skill id
	// GetcontactbyContactID('2233650423','');
	// GetListCalllist('','','','','2017-09-15T15:53:00','2017-09-15T15:53:00');
	// PostCallListbyListID('1001',162779,'',true,'','','',false);
	// DeleteCallListByJobID('1001'); // job ID is sample
	// GetCallingListbyJobID('1001');
	// GetGroups('','','','','','');
	// PostGroups('sample',true,'sample');
	// GetGroupsByGroupID('10001','');    //sample Group ID
	// PutGroupsByGroupID('10001','sample',true,'sample');   // sample Group ID
	// DeleteGroupsByAgentGroupID('10001',101212); // sample Group ID
	// GetGroupsByAgentGroupID('10001','','','','','','');// sample Group ID
	// PostGroupsByAgentGroupID('10001',101212);// sample Group ID
	// postSessionIdInteractionsContactIdtyping('QzJWa2JUZUthTmcwb1dPMXcwd2xTa2FJamY3QTZGQ25wUFlVR1FTZzF1Tld4Tm89', '2237919812',true,true);
	// posttAgentSessionSessionIdInteractionAddEmail('QzJWa2JUZUthTmcwb1dPMXcwd2xTa2FJamY3QTZGQ25wUFlVR1FTZzF1Tld4Tm89');
	// posttAgentSessionIdInteractionConatactIdParkemail('QzJWa2JUZUthTmcwb1dPMXcwd2xTa2FJamY3QTZGQ25wUFlVR1FTZzF1Tld4Tm89','2237919921');
	// posttAgentSessionIdInteractionConatactIdUnparkemail( 'QzJWa2JUZUthTmcwb1dPMXcwd2xTa2FJamY3QTZGQ25wUFlVR1FTZzF1Tld4Tm89', '2237919921');
	// posttAgentSessionIdInteractionConatactIdPreview( 'QzJWa2JUZUthTmcwb1dPMXcwd2xTa2FJamY3QTZGQ25wUFlVR1FTZzF1Tld4Tm89', '2237919921');
	// posttAgentSessionIdInteractionConatactIdEmailRestore('QzJWa2JUZUthTmcwb1dPMXcwd2xTa2FJamY3QTZGQ25wUFlVR1FTZzF1Tld4Tm89', '2237919921');
	// posttAgentSessionIdInteractionConatactIdSnooze( 'QzJWa2JUZUthTmcwb1dPMXcwd2xTa2FJamY3QTZGQ25wUFlVR1FTZzF1Tld4Tm89', '2237919921');
	// DialCallback('QzJWa2JUZUthTmcwb1dPMXcwd2xTa2FJamY3QTZGQ25wUFlVR1FTZzF1Tld4Tm89', '2237919921');
	// RescheduleCallback( 'QzJWa2JUZUthTmcwb1dPMXcwd2xTa2FJamY3QTZGQ25wUFlVR1FTZzF1Tld4Tm89', '2237919921','2008-09-15T15:53:00');
	// CancelCallback('QzJWa2JUZUthTmcwb1dPMXcwd2xTa2FJamY3QTZGQ244YW5CckZjWGdQWEhlUjg9','2237919921' , 'hi');
	// posttAgentSessionIdAddcontact('QzJWa2JUZUthTmcwb1dPMXcwd2xTa2FJamY3QTZGQ244YW5CckZjWGdQWEhlUjg9');
	// posttAgentSessionIdInteractionConatactIdActivate('QzJWa2JUZUthTmcwb1dPMXcwd2xTa2FJamY3QTZGQ244YW5CckZjWGdQWEhlUjg9', 2237919921);
	// GetSmstranscripts( '2018-03-01T15:53:00', '2018-03-15T15:53:00','12345');
	// GetSmstranscriptsbyContactid( '2238306091','2018-03-01T15:53:00', '2018-03-15T15:53:00','1245', top="",skip="");
	//GetSmsCallQuality( '2238306091');
	// GetTeamPerformancebyTeamIDTotal( 55538, '2018-02-28', '2018-03-01');
	// getReportJobById('12345');
	// GetdatadownloadbyReportID(1,'sample.csv','2018-02-28','2018-03-01'); // It seems that the server to which you are sending the Post request(your Site's server) has been configured to block Post request.
	// Getwfoascm('2018-02-28T15:53:00','2018-02-28T15:53:00' );
	// Getwfoasi('2018-02-28T15:53:00','2018-02-28T15:53:00' );
	// Getwfocsi('2018-02-28T15:53:00','2018-02-28T15:53:00' );
	// Getwfoftci('2018-02-28T15:53:00','2018-02-28T15:53:00' );
	// Getwfoosi('2018-02-28T15:53:00','2018-02-28T15:53:00' );
	// Getwfoqm('2018-02-28T15:53:00','2018-02-28T15:53:00' );
	// GetTeamPerformanceTotal('2018-02-28','2018-03-01');
	// GetwfmSkillContacts('2018-02-28T15:53:00','2018-03-02T15:53:00','1');
	// Getwfmagetns('2018-02-28T15:53:00','2018-03-02T15:53:00');
	// GetwfmdailerContacts('2018-02-28T15:53:00','2018-03-02T15:53:00');
	// Getwfmscheduleadherence('2018-02-28T15:50:00','2018-02-28T15:53:00');
	// GetwfmAgentscorecard('2018-02-28T15:50:00','2018-02-28T15:53:00');
	// GetwfmAgentperformance('2018-02-28T15:50:00','2018-02-28T15:53:00');
	// postContactsChatSessionTyping('1234');
	// postContactsChatSessionTypingPreview('1234');
	// getChatprofileByPOCId("2d102835-fa77-4c93-906e-cfd1889ed168");
	// getThankYouForSkillId('12365');
	// postContactChatSendEmail("maktum.pasha@hc8.com","rafiq.j@servion.com","test12222");
}
function SortingResult(){
var table, rows, switching, i, x, y, shouldSwitch;
  table = document.getElementById("customers");
  switching = true;
  /*Make a loop that will continue until
  no switching has been done:*/
  while (switching) {
    //start by saying: no switching is done:
    switching = false;
    rows = table.getElementsByTagName("TR");
    /*Loop through all table rows (except the
    first, which contains table headers):*/
    for (i = 1; i < (rows.length - 1); i++) {
      //start by saying there should be no switching:
      shouldSwitch = false;
      /*Get the two elements you want to compare,
      one from current row and one from the next:*/
      x = rows[i].getElementsByTagName("TD")[1];
	  

      y = rows[i + 1].getElementsByTagName("TD")[1];
      //check if the two rows should switch place:
      if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
        //if so, mark as a switch and break the loop:
        shouldSwitch= true;
        break;
      }
    }
    if (shouldSwitch) {
      /*If a switch has been marked, make the switch
      and mark that a switch has been done:*/
      rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
      switching = true;
    }
  }
  
  /**************************/
  var table = $('table')[0];
var rowGroups = {};
//loop through the rows excluding the first row (the header row)
while(table.rows.length > 1){
    var row = table.rows[1];
    var id = $(row.cells[1]).text();
   
    if(!rowGroups[id]) rowGroups[id] = [];
    if(rowGroups[id].length > 0){
        row.className = 'subrow';
        $(row).slideUp();
    }
    rowGroups[id].push(row);
    table.deleteRow(1);
}
//loop through the row groups to build the new table content
for(var id in rowGroups){
    var group = rowGroups[id];
    for(var j = 0; j < group.length; j++){
        var row = group[j];
        if(group.length > 1 && j == 0) {
            //add + button
            var lastCell = row.cells[row.cells.length - 1];           
            $("<span class='collapsed'>").appendTo(lastCell).click(plusClick);                                         
        }
        table.tBodies[0].appendChild(row);        
    }
}
//function handling button click
function plusClick(e){
    var collapsed = $(this).hasClass('collapsed');
    var fontSize = collapsed ? 14 : 0;
    $(this).closest('tr').nextUntil(':not(.subrow)').slideToggle(400)
           .css('font-size', fontSize);
    $(this).toggleClass('collapsed');        
}

}

