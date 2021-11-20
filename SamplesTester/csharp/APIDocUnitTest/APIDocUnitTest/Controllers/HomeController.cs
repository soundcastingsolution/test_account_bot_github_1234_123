using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APIDocUnitTest.Models;
using System.Configuration;

namespace APIDocUnitTest.Controllers
{
    public class HomeController : Controller
    {
        Operation operationOb = new Operation();
        APIproperties propertiesOb = new APIproperties();
        List<APIproperties> ResultString = new List<APIproperties>();
        string apiversion = ConfigurationManager.AppSettings["APIVersion"].ToString();
        // GET: Home
        public ActionResult Index()
        {

            propertiesOb.APIresultStatus = operationOb.getimezonebyskillId();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Skills", MethodName = "getimezonebyskillId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });
            propertiesOb.APIresultStatus = operationOb.PutSkillIDParameterTimezone();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Skills", MethodName = "PutSkillIDParameterTimezone", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });
            propertiesOb.APIresultStatus = operationOb.Putbusinessunittimezone();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "Putbusinessunittimezone", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });
            propertiesOb.APIresultStatus = operationOb.PostSuppressedContacts();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "PostSuppressedContacts", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });
            propertiesOb.APIresultStatus = operationOb.getbusinessunitTimezone();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "getbusinessunitTimezone", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });
            propertiesOb.APIresultStatus = operationOb.getSuppresedContacts();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "getSuppresedContacts", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });
            propertiesOb.APIresultStatus = operationOb.DeleteSkillbyagentID(1234,12345);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Skills", MethodName = "DeleteSkillbyagentID", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });
            propertiesOb.APIresultStatus = operationOb.getimezonebyskillId();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Skills", MethodName = "getimezonebyskillId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });
            propertiesOb.APIresultStatus = operationOb.PutSkillIDParameterTimezone();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Skills", MethodName = "PutSkillIDParameterTimezone", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });
            propertiesOb.APIresultStatus = operationOb.Putbusinessunittimezone();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "Putbusinessunittimezone", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });
            propertiesOb.APIresultStatus = operationOb.PostSuppressedContacts();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "PostSuppressedContacts", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });
            propertiesOb.APIresultStatus = operationOb.getbusinessunitTimezone();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "getbusinessunitTimezone", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });
            propertiesOb.APIresultStatus = operationOb.getSuppresedContacts();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "getSuppresedContacts", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });
            propertiesOb.APIresultStatus = operationOb.GetAddress();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "AddressBook", MethodName = "GetAddress", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });
            propertiesOb.APIresultStatus = operationOb.GetAgentSkills();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "GetAgentSkills", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });
            //propertiesOb.APIresultStatus = operationOb.postDncGroupsScrubbedSkillsByGroupIdSkillId(369, 165451);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Skills", MethodName = "postDncGroupsScrubbedSkillsByGroupIdSkillId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });
            propertiesOb.APIresultStatus = operationOb.getAgentStates();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "getAgentStates", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getTeams();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "getTeams", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getTeamByTeamId(55581);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Team", MethodName = "getTeamByTeamId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getAgentSkillsByAgentId(8176); 
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Team", MethodName = "getAgentSkillsByAgentId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getAgentGroupsByAgentId(210452);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "getAgentGroupsByAgentId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.PostAgentMessages("hell22231", DateTime.Now, "test", DateTime.Now);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "PostAgentMessages", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getAgentPatterns();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "getAgentPatterns", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.deleteAgentMessagesByMessageId(1073741835);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "deleteAgentMessagesByMessageId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.postAgentState(27498, "Available", "");
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "postAgentState", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.deleteTeamsAgentbyTeamId(34241, 34560, 166041);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "deleteTeamsAgentbyTeamId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getTeamsAgentbyTeamId(55581);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "getTeamsAgentbyTeamId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.createTeamsAgentbyTeamId(55581, "210452");
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "createTeamsAgentbyTeamId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.createTeamsUnavailablebyTeamId(34241, "");
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "createTeamsUnavailablebyTeamId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.deleteTeamsUnavailablebyTeamId(34241, "");
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "deleteTeamsUnavailablebyTeamId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });
           
            propertiesOb.APIresultStatus = operationOb.getBrandingProfile(141014, "");
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "getBrandingProfile", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getBusinessUnit("", false);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "getBusinessUnit", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getDispositions();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "getDispositions", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.postHiringSources("maktest");
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "postHiringSources", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getMessageTemplates(1);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "getMessageTemplates", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getMessageTemplatesbyTemplateId(1);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "getMessageTemplatesbyTemplateId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.UpdateMessageTemplateByTemplateId(1, "pasha", false, "hello");
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "UpdateMessageTemplateByTemplateId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getSecurityProfiles(1073744989);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "getSecurityProfiles", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.postTags("testfromloacl", "local");
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "postTags", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getTagsByTagId(66);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "getTagsByTagId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.UpdateTagsByTagId(66, "General", " hellofromconsole", "true");
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "UpdateTagsByTagId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.createFile("testchrihhh", "MTAwMQlNaXNzaW5", true);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "createFile", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.deleteFileByName("PashaCallinglist");
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "deleteFileByName", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getFolders("CallingLists");
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "getFolders", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.deleteFolderByName("test");
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "deleteFolderByName", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getFilesExternal("CallingLists");
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "getFilesExternal", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.createFilesExternal("testpash", "MTAwMQlNaXNzaW5nIEV4dGVybmFsIElELg0KMTAwMglNaXNzaW5nIEV4dGVybmFsIElELg0KMTAwMwlNaXNzaW5nIEV4dGVybmFsIElELg0KMTAwNAlNaXNzaW5nIEV4dGVybmFsIElELg0KMTAwNQlNaXNzaW5nIEV4dGVybmFsIElELg0KMTAwNglNaXNzaW5nIEV4dGVybmFsIElELg0K", true, true);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "createFilesExternal", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.updateFile("testfileexternal", "CallingLists", false);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "updateFile", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.createDispositions("pashaDispos", true, 90);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "ScheduledCallbacks", MethodName = "createDispositions", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getDispositionsByDispositionID(4552);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "ScheduledCallbacks", MethodName = "getDispositionsByDispositionID", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.createFile("testpashaaa", "MTAwMQlNaXNzaW5nIEV4dGVybmFsIElELg0KMTAwMglNaXNzaW5nIEV4dGVybmFsIElELg0KMTAwMwlNaXNzaW5nIEV4dGVybmFsIElELg0KMTAwNAlNaXNzaW5nIEV4dGVybmFsIElELg0KMTAwNQlNaXNzaW5nIEV4dGVybmFsIElELg0KMTAwNglNaXNzaW5nIEV4dGVybmFsIElELg0K", true);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "createFile", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.updateDispositionsByDispositionID(4552, "pinit", false, 1, true);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "ScheduledCallbacks", MethodName = "updateDispositionsByDispositionID", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.createDispositions("testpashamaansss", false, 1);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "ScheduledCallbacks", MethodName = "createDispositions", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //from here 
           
            propertiesOb.APIresultStatus = operationOb.updateDispositionsByDispositionID(4551, "Preview - Record Fina1");
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "ScheduledCallbacks", MethodName = "updateDispositionsByDispositionID", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getDispositionsByClassifications();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "ScheduledCallbacks", MethodName = "getDispositionsByClassifications", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getSkillIdAgentsUnassigned(165451);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Skills", MethodName = "getSkillIdAgentsUnassigned", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getDispositionsBySkillId(165451);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Skills", MethodName = "getDispositionsBySkillId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getUnassignedDispositionsBySkillId(165451);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Skills", MethodName = "getUnassignedDispositionsBySkillId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getSkillsGeneralSettingBySkillId(152344);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Skills", MethodName = "getSkillsGeneralSettingBySkillId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getGroups();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Group", MethodName = "getGroups", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.createGroups("PashaGroup2","true", "test");
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Group", MethodName = "createGroups", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getGroupsByGroupId(1073741996);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Group", MethodName = "getGroupsByGroupId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.updateGroupsByGroupId(1073741996, "test2", "true", "hello");
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Group", MethodName = "updateGroupsByGroupId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.postAgentGroupsByGroupId(1073741996, 210452);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Group", MethodName = "postAgentGroupsByGroupId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.deleteAgentGroupsByGroupId(1073741996, 210452);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Group", MethodName = "getTeams", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getContactFileByContactId(2233650423, "");
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Contacts", MethodName = "deleteAgentGroupsByGroupId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.postContactsTagsByContactId(2233650423, "");
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Contacts", MethodName = "postContactsTagsByContactId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.postDncGroups("SampledncPic", "dncgroup");
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "List", MethodName = "postDncGroups", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.UpdateDncGroupsByGroupId(369, "testedok", "fromlocal", "false");
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "List", MethodName = "UpdateDncGroupsByGroupId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.deleteDncGroupsByGroupIdSkillId(369, 165451);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "List", MethodName = "deleteDncGroupsByGroupIdSkillId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.postDncGroupsByGroupIdSkillId(369, 165451);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "List", MethodName = "postDncGroupsByGroupIdSkillId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.postDncGroupsScrubbedSkillsByGroupIdSkillId(369, 165451);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "List", MethodName = "postDncGroupsScrubbedSkillsByGroupIdSkillId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.deleteDncGroupsScrubbedSkillsByGroupIdSkillId(369, 165451);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "List", MethodName = "deleteDncGroupsScrubbedSkillsByGroupIdSkillId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getCalllistListUpload(100, "152266", "nikhiltest", "false", "", "MTAwMQlNaXNzaW5nIEV4dGVybmFsIElELg0KMTAwMglNaXN");
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "getCalllistListUpload", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getListsCallListJobs();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "List", MethodName = "getListsCallListJobs", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.deleteListsCallListsJobsByJobId(1001);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "List", MethodName = "deleteListsCallListsJobsByJobId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getListsCallListJobs("100");
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "List", MethodName = "getListsCallListJobs", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getListsCallListListIdAttempts(100);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "List", MethodName = "getListsCallListListIdAttempts", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.deleteListsCallListsByListId(100);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "List", MethodName = "deleteListsCallListsByListId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.postSessionIdInteractionsContactIdtyping("QnlNeXg1UnVIVUswa09yazBpeU9TbGhtUEdnQ2FoYXNyRXZnbWwrWTdPbFpKTy9a", "2237331756");
            ResultString.Add(new APIproperties() { APIType = "Agent", APISubType = "ChatRequest", MethodName = "postSessionIdInteractionsContactIdtyping", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.posttAgentSessionIdInteractionAddEmail("QnlNeXg1UnVIVUswa09yazBpeU9TbGhtUEdnQ2FoYXNyRXZnbWwrWTdPbFpKTy9a");
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "posttAgentSessionIdInteractionAddEmail", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.posttAgentSessionIdInteractionConatactIdParkemail("QnlNeXg1UnVIVUswa09yazBpeU9TbGhtUEdnQ2FoYXNyRXZnbWwrWTdPbFpKTy9a", "2237331856");
            ResultString.Add(new APIproperties() { APIType = "Agent", APISubType = "Email", MethodName = "posttAgentSessionIdInteractionConatactIdParkemail", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.posttAgentSessionIdInteractionConatactIdUnparkemail("QnlNeXg1UnVIVUswa09yazBpeU9TbGhtUEdnQ2FoYXNyRXZnbWwrWTdPbFpKTy9a", "2237331856");
            ResultString.Add(new APIproperties() { APIType = "Agent", APISubType = "Email", MethodName = "posttAgentSessionIdInteractionConatactIdUnparkemail", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.posttAgentSessionIdInteractionConatactIdPreview("QnlNeXg1UnVIVUswa09yazBpeU9TbGhtUEdnQ2FoYXNyRXZnbWwrWTdPbFpKTy9a", "2237331856");
            ResultString.Add(new APIproperties() { APIType = "Agent", APISubType = "Email", MethodName = "getTeams", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.posttAgentSessionIdInteractionConatactIdEmailRestore("QnlNeXg1UnVIVUswa09yazBpeU9TbGhtUEdnQ2FoYXNyRXZnbWwrWTdPbFpKTy9a", "2237331856");
            ResultString.Add(new APIproperties() { APIType = "Agent", APISubType = "Email", MethodName = "posttAgentSessionIdInteractionConatactIdPreview", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.posttAgentSessionIdInteractionConatactIdSnooze("QnlNeXg1UnVIVUswa09yazBpeU9TbGhtUEdnQ2FoYXNyRXZnbWwrWTdPbFpKTy9a", "2237331856");
            ResultString.Add(new APIproperties() { APIType = "Agent", APISubType = "PersonalCon", MethodName = "posttAgentSessionIdInteractionConatactIdSnooze", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.posttAgentSessionIdAddcontact("QnlNeXg1UnVIVUswa09yazBpeU9Tb2lVVU9uWHFWREVqQ2pOTzZOeUp6YTU5WHhz");
            ResultString.Add(new APIproperties() { APIType = "Agent", APISubType = "Sessions", MethodName = "posttAgentSessionIdAddcontact", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.posttAgentSessionIdInteractionConatactIdActivate("QnlNeXg1UnVIVUswa09yazBpeU9Tb2lVVU9uWHFWREVqQ2pOTzZOeUp6YTU5WHhz", "2237609862");
            ResultString.Add(new APIproperties() { APIType = "Agent", APISubType = "Sessions", MethodName = "posttAgentSessionIdInteractionConatactIdActivate", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getContactsSmsTranscripts("1234");
            ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "getContactsSmsTranscripts", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getContactsContactIdCallquality("2237609862");
            ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "getContactsContactIdCallquality", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getReportJobsByJobId("123");
            ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "getReportJobsByJobId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getwfodataqm();
            ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "getwfodataqm", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.postContactsChatSessionTyping("1234");
            ResultString.Add(new APIproperties() { APIType = "Patron", APISubType = "ChatRequests", MethodName = "postContactsChatSessionTyping", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.postContactsChatSessionTypingPreview("1234");
            ResultString.Add(new APIproperties() { APIType = "Patron", APISubType = "ChatRequests", MethodName = "postContactsChatSessionTypingPreview", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.postPointofContactPOCIdChatprofile("1234");
            //ResultString.Add(new APIproperties() { APIType = "Patron", APISubType = "ChatRequests", MethodName = "postPointofContactPOCIdChatprofile", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getAgentsAgentIdLoginhistory(1234);
            ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "getAgentsAgentIdLoginhistory", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getAgentsAgentIdLoginhistory(1234);
            ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "getAgentsAgentIdLoginhistory", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getContactsContactIdCallquality("1234");
            ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "getContactsContactIdCallquality", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getReportJobsByJobId("1234");
            ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "getReportJobsByJobId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.postReportjobsDatadownlaodByReportId("123");
            ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "postReportjobsDatadownlaodByReportId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getwfodataascm("", "09/01/2016", "09/01/2017");
            ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "getwfodataascm", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getwfodataasi("", "09/01/2016", "09/14/2016");
            ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "getwfodataasi", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getwfodatacsi("", "09/01/2016", "09/01/2016");
            ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "getwfodatacsi", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getwfodataftci("", "09/01/2016", "09/01/2016");
            ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "getwfodataftci", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getwfodataosi("", "09/01/2016", "09/01/2016");
            ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "getwfodataosi", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getwfodataqm("", " ", " ");
            ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "getwfodataqm", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getwfmdataSkillsContacts("", "09/01/2016", "09/01/2016");
            ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "WFM Data", MethodName = "getwfmdataSkillsContacts", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getwfmdataAgents("", "09/01/2016", "09/01/2016");
            ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "WFM Data", MethodName = "getwfmdataAgents", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getwfmdataSkillsDialerContacts("", "09/01/2016", "09/01/2016");
            ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "WFM Data", MethodName = "getwfmdataSkillsDialerContacts", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getwfmdataAgentsScheduleAdherence("", "09/01/2016", "09/01/2016");
            ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "WFM Data", MethodName = "getwfmdataAgentsScheduleAdherence", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getwfmdataAgentsScorecards("", "09/01/2016", "09/01/2016");
            ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "WFM Data", MethodName = "getwfmdataAgentsScorecards", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getwfmdataSkillsAgentPerformance("", "09/01/2016", "09/01/2016");
            //get the poc of chat profile based in the BU and it will work based which version is enabled 
            //for nikhil@hc8 use this 2d102835 - fa77 - 4c93 - 906e-cfd1889ed168
            ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "WFM Data", MethodName = "getwfmdataSkillsAgentPerformance", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getChatprofileByPOCId("2d102835-fa77-4c93-906e-cfd1889ed168");
            ResultString.Add(new APIproperties() { APIType = "Patron", APISubType = "ChatRequests", MethodName = "getChatprofileByPOCId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getTeamIDPerformance(55581, "09/01/2016", "09/01/2016");
            ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "getTeamIDPerformance", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getThankYouForSkillId(162575);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Skill", MethodName = "getThankYouForSkillId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.postContactChatSendEmail("maktum.pasha@hc8.com", "rafiq.j@servion.com", "test12222");
            ResultString.Add(new APIproperties() { APIType = "Patron", APISubType = "ChatRequests", MethodName = "postContactChatSendEmail", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getStandardAddressBookEntries(12345);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Addressbook", MethodName = "getStandardAddressBookEntries", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.PostAgents();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "PostAgents", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            ViewBag.data = ResultString;
            return View();
        }
    }
}