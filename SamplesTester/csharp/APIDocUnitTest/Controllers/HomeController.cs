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
            propertiesOb.APIresultStatus = operationOb.PutUnavailableCodeByID(3619);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "PutUnavailableCodeByID", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getAccessKeys("27730");
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "getAccessKeys", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.postAccessKeys(27730);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "postAccessKeys", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.deleteAccessKeyByID("4FE4QQJVVDVJZWQ7NGBE4YF35O7DEHRJ74XPPQNQ3MYYKFNHIYBA====");
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "deleteAccessKeyByID", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getAccessKeysByID("4FE4QQJVVDVJZWQ7NGBE4YF35O7DEHRJ74XPPQNQ3MYYKFNHIYBA====");
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "getAccessKeysByID", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.patchAccessKeysByID("4FE4QQJVVDVJZWQ7NGBE4YF35O7DEHRJ74XPPQNQ3MYYKFNHIYBA====", true);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "patchAccessKeysByID", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.PostHoursofOperation();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "PostHoursofOperation", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.putHoursofOperationByID(28);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "putHoursofOperationByID", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.PostPointofContact();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "PostPointofContact", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.PutPointofContactByID(123546);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "PutPointofContactByID", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.PostUnavailableCodes();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "PostUnavailableCodes", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getPhoneNumbers("");
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "getPhoneNumbers", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.PostCampaigns();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Skills", MethodName = "PostCampaigns", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.PutCampaignsByID(8913);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Skills", MethodName = "PutCampaignsByID", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getDispositionByskill("10/10/1800", true);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Skills", MethodName = "getDispositionByskill", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.DeleteCampaignBySkill(8913);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Skills", MethodName = "DeleteCampaignBySkill", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.PostCampaignBySkill(8913);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Skills", MethodName = "PostCampaignBySkill", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getSmsHistoricalTranscript(268480840, 141014);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Skills", MethodName = "getSmsHistoricalTranscript", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getSmsHistoricalContacts(141014, 454, 247361);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Skills", MethodName = "getSmsHistoricalContacts", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.postEmailSaveDraft("WVVsLzlGTkc1T1hUTVpNVDlXR2Z2WC9KQWo3K3ZxZzFFTXNDQUxISA==", 9471795);
            ResultString.Add(new APIproperties() { APIType = "Agent", APISubType = "Emails", MethodName = "postEmailSaveDraft", APIversion = "v-15", APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.postAddText("WVVsLzlGTkc1T1hUTVpNVDlXR2Z2WC9KQWo3K3ZxZzFFTXNDQUxISA==");
            ResultString.Add(new APIproperties() { APIType = "Agent", APISubType = "ChatRequests", MethodName = "postAddText", APIversion = "v-15", APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.putUnavailableCodebyIDTeams("1234");
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agents", MethodName = "putUnavailablecodesbyIDTeam", APIversion = "v15.0", APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.postjobsync();
            ResultString.Add(new APIproperties() { APIType = "DataExtraction", APISubType = "ExtractData", MethodName = "postjobsync", APIversion = "v1.0", APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.PutSkillListManagement(165451);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Skills", MethodName = "PutSkillListManagement", APIversion = "v18.0", APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getSkillParameters();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Skills", MethodName = "getSkillParameters", APIversion = "v18.0", APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.PutSkillCadenceSettings(165451);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Skills", MethodName = "PutSkillCadenceSettings", APIversion = "v18.0", APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getSkillSkillIdParameters(165451);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Skills", MethodName = "getSkillSkillIdParameters", APIversion = "v18.0", APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.postSetDisposition(2233650423);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Contacts", MethodName = "postSetDisposition", APIversion = "v18.0", APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getBusinessUnitOutboundRoutes();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "getBusinessUnitOutboundRoutes", APIversion = "v18.0", APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getClientData();
            ResultString.Add(new APIproperties() { APIType = "Realtime", APISubType = "Realtime", MethodName = "getClientData", APIversion = "v19.0", APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.PutClientData();
            ResultString.Add(new APIproperties() { APIType = "Realtime", APISubType = "Realtime", MethodName = "PutClientData", APIversion = "v19.0", APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.Putpersistentcontacts(268480840);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Contacts", MethodName = "Putpersistentcontacts", APIversion = "v19.0", APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getAgentSettings(165451);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agents", MethodName = "getAgentSettings", APIversion = "v19.0", APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.postSmsOutbound("");
            ResultString.Add(new APIproperties() { APIType = "Agent", APISubType = "Sessions", MethodName = "postSmsOutbound", APIversion = "v19.0", APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getScriptsById(1654);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "getScriptsById", APIversion = "v19.0", APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getScriptsSearch();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "getScriptsSearch", APIversion = "v19.0", APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.posttransformnumbers(268480840);
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agents", MethodName = "posttransformnumbers", APIversion = "v19.0", APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.deleteremoveprospects("");
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Lists", MethodName = "deleteremoveprospects", APIversion = "v19.0", APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.deletescripts();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "deletescripts", APIversion = "v19.0", APIresultStatus = propertiesOb.APIresultStatus });



            //propertiesOb.APIresultStatus = operationOb.GetMediaplaybackByID("21bf4a34-f768-45d4-aed6-6bd754dd49d2","chat","true");
            //ResultString.Add(new APIproperties() { APIType = "MediaPlayBAck", APISubType = "Accessing the Recording Media", MethodName = "GetMediaplaybackByID", APIversion = "v1.0", APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.GetMediaplaybackBySegmentID("21bf4a34-f768-45d4-aed6-6bd754dd49d2", "b171d215-a152-4572-bd45-56670cb7a859", "chat", "true");
            //ResultString.Add(new APIproperties() { APIType = "MediaPlayBAck", APISubType = "Accessing the Recording Media", MethodName = "GetMediaplaybackBySegmentID", APIversion = "v1.0", APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.GetMediaplaybackContacts("master id 1", "chat", "true");
            //ResultString.Add(new APIproperties() { APIType = "MediaPlayBAck", APISubType = "Accessing the Recording Media", MethodName = "GetMediaplaybackContacts", APIversion = "v1.0", APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.GetActiveFlag("1");
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Workflow Data", MethodName = "GetActiveFlag", APIversion = "v16.0", APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.PostWorkflowData();
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Workflow Data", MethodName = "PostWorkflowData", APIversion = "v16.0", APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.GetWorkflowById(56765);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Workflow Data", MethodName = "GetWorkflowById", APIversion = "v16.0", APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.PutWorkflowById(56765);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Workflow Data", MethodName = "PutWorkflowById", APIversion = "v16.0", APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.PutWorkflowById(56765);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Workflow Data", MethodName = "PutWorkflowByIdActivate", APIversion = "v16.0", APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.PutWorkflowById(56765);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Workflow Data", MethodName = "PutWorkflowByIdDeactivate", APIversion = "v16.0", APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getJobs();
            //ResultString.Add(new APIproperties() { APIType = "DataExtraction", APISubType = "ExtractData", MethodName = "getjobs", APIversion = "v1.0", APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.postjobs();
            //ResultString.Add(new APIproperties() { APIType = "DataExtraction", APISubType = "ExtractData", MethodName = "postjobs", APIversion = "v1.0", APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getJobsByID("12345");
            //ResultString.Add(new APIproperties() { APIType = "DataExtraction", APISubType = "ExtractData", MethodName = "postjobsbyid", APIversion = "v1.0", APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.deleteSuppressedContactBySuppressedContactId(12345);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "deleteSuppressedContactBySuppressedContactId", APIversion = "v17.0", APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getSuppressedContactBySuppressedContactId(12345);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "getSuppressedContactBySuppressedContactId", APIversion = "v17.0", APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.putSuppressedContactBySuppressedContactId(12345);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "putSuppressedContactBySuppressedContactId", APIversion = "v17.0", APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getContactsByIdHierachy(12345);
            //ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "getContactsByIdHierachy", APIversion = "v17.0", APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getScripts18(12345);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "getScripts18", APIversion = "v18.0", APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.postScripts18(12345);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "postScripts18", APIversion = "v18.0", APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.putScripts18(12345);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "putScripts18", APIversion = "v18.0", APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.postScripts18Kick(12345);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "postScripts18Kick", APIversion = "v18.0", APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getScripts18HistoryByName(12345);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "getScripts18HistoryByName", APIversion = "v18.0", APIresultStatus = propertiesOb.APIresultStatus });








            /* propertiesOb.APIresultStatus = operationOb.getAgents("", "10/10/1800", true, "", 0, 100, "");
            ResultString.Add(new APIproperties() { APIType = "Admin", AP0ISubType = "Agent", MethodName = "getAgents", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.PostAgentsV13("Test 123", "Mid33", "Last333", "0", "", "1", "", "0", "", "", true, "test@gmail.com",
            "Test@gmail.com", "111111111111111111111111111111111111", "(GMT-07:00) Arizona", "India", "Tamilnadu", "Chennai", "15", "15", "15", "0", "1",
             false, true, "0", "", "10/10/1800", "10/10/1800", "0", true, "1", "", false, "0", "", "", "", "", "", "", "0", "", "1", "false", "", "", "",
             "", false, "0", false, "", "", "", "", false, false, false, "1", false, "1", false, "1", false, false, false, "", "", false, "12345");
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "PostAgents", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.GetAgentID(1140, ""); //int agentID, string fields
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "GetAgentID", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.UpdateAgent(1140); //int agentID
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "UpdateAgent", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.GetAgentTeams("", "", true, "", 0, 100, ""); //string fields, string updatedSince, bool isActive, string searchString,int skip, int top, string orderBy
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "GetAgentTeams", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.CreateTeam();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "CreateTeam", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.GetAgentTeamByID(1042, ""); //int teamID, string fields
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "GetAgentTeamByID", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.UpdateTeam(1042); //int teamID
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "UpdateTeam", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.GetUnavailableCodesTeamID(1042, "true");//int teamID, string activeOnly
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "GetUnavailableCodesTeamID", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.GetUnavailableCodes("true");//string activeOnly
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "GetUnavailableCodes", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.GetSkills();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "GetSkills", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.postSkill();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "postSkill", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.GetSkillID(12473,"");
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "GetSkillID", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.updateSkill(14856);//int skillID
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "updateSkill", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.getSkillsGeneralSettingBySkillIdV13(14856, "");//int skillId, string fields = ""
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "getSkillsGeneralSettingBySkillId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.updateSkillsGeneralSettingBySkillIdV13(14856);//int skillId
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "updateSkillsGeneralSettingBySkillIdV13", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.GetCPAManagement(14856, "");//int skillId, string fields = ""
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "GetCPAManagement", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.Putskillcpamanagement(14856);//int skillId
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "Putskillcpamanagement", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.Getxssettings(14856, "");//int skillId , string fields = ""
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "Getxssettings", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.Putxssettings(14856);//int skillId
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "Putxssettings", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.Getdeliverypreference(14856, "");//int skillId, string fields = ""
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "Getdeliverypreference", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.Putskilldeliverypreference("14856");//String skillId
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "Putskilldeliverypreference", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.Getskillretrysetting(14856, "");//int skillId, string fields = ""
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "Getskillretrysetting", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.Putskillretrysetting(14856);//int skillId
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "Putskillretrysetting", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.Getschedulesettings(14856);//int skillId
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "Getschedulesettings", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.Putschedulesettings(14856);//int skillId
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "Putschedulesettings", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            propertiesOb.APIresultStatus = operationOb.GetAddress();
            ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "AddressBook", MethodName = "GetAddress", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus }); */


            //propertiesOb.APIresultStatus = operationOb.GetAgentSkills();
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "GetAgentSkills", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });
            //propertiesOb.APIresultStatus = operationOb.postDncGroupsScrubbedSkillsByGroupIdSkillId(369, 165451);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Skills", MethodName = "postDncGroupsScrubbedSkillsByGroupIdSkillId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });
            //propertiesOb.APIresultStatus = operationOb.getAgentStates();
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "getAgentStates", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getTeams();
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "getTeams", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getTeamByTeamId(55581);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Team", MethodName = "getTeamByTeamId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getAgentSkillsByAgentId(8176); 
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Team", MethodName = "getAgentSkillsByAgentId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getAgentGroupsByAgentId(210452);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "getAgentGroupsByAgentId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.PostAgentMessages("hell22231", DateTime.Now, "test", DateTime.Now);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "PostAgentMessages", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getAgentPatterns();
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "getAgentPatterns", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.deleteAgentMessagesByMessageId(1073741835);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "deleteAgentMessagesByMessageId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.postAgentState(27498, "Available", "");
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "postAgentState", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.deleteTeamsAgentbyTeamId(34241, 34560, 166041);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "deleteTeamsAgentbyTeamId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getTeamsAgentbyTeamId(55581);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "getTeamsAgentbyTeamId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.createTeamsAgentbyTeamId(55581, "210452");
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "createTeamsAgentbyTeamId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.createTeamsUnavailablebyTeamId(34241, "");
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "createTeamsUnavailablebyTeamId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.deleteTeamsUnavailablebyTeamId(34241, "");
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "deleteTeamsUnavailablebyTeamId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getBrandingProfile(141014, "");
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "getBrandingProfile", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getBusinessUnit("", false);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "getBusinessUnit", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getDispositions();
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "getDispositions", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.postHiringSources("maktest");
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "postHiringSources", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getMessageTemplates(1);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "getMessageTemplates", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getMessageTemplatesbyTemplateId(1);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "getMessageTemplatesbyTemplateId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.UpdateMessageTemplateByTemplateId(1, "pasha", false, "hello");
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "UpdateMessageTemplateByTemplateId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getSecurityProfiles(1073744989);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "getSecurityProfiles", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.postTags("testfromloacl", "local");
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "postTags", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getTagsByTagId(66);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "getTagsByTagId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.UpdateTagsByTagId(66, "General", " hellofromconsole", "true");
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "UpdateTagsByTagId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.createFile("testchrihhh", "MTAwMQlNaXNzaW5", true);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "createFile", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.deleteFileByName("PashaCallinglist");
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "deleteFileByName", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getFolders("CallingLists");
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "getFolders", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.deleteFolderByName("test");
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "deleteFolderByName", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getFilesExternal("CallingLists");
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "getFilesExternal", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.createFilesExternal("testpash", "MTAwMQlNaXNzaW5nIEV4dGVybmFsIElELg0KMTAwMglNaXNzaW5nIEV4dGVybmFsIElELg0KMTAwMwlNaXNzaW5nIEV4dGVybmFsIElELg0KMTAwNAlNaXNzaW5nIEV4dGVybmFsIElELg0KMTAwNQlNaXNzaW5nIEV4dGVybmFsIElELg0KMTAwNglNaXNzaW5nIEV4dGVybmFsIElELg0K", true, true);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "createFilesExternal", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.updateFile("testfileexternal", "CallingLists", false);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "updateFile", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.createDispositions("pashaDispos", true, 90);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "ScheduledCallbacks", MethodName = "createDispositions", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getDispositionsByDispositionID(4552);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "ScheduledCallbacks", MethodName = "getDispositionsByDispositionID", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.createFile("testpashaaa", "MTAwMQlNaXNzaW5nIEV4dGVybmFsIElELg0KMTAwMglNaXNzaW5nIEV4dGVybmFsIElELg0KMTAwMwlNaXNzaW5nIEV4dGVybmFsIElELg0KMTAwNAlNaXNzaW5nIEV4dGVybmFsIElELg0KMTAwNQlNaXNzaW5nIEV4dGVybmFsIElELg0KMTAwNglNaXNzaW5nIEV4dGVybmFsIElELg0K", true);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "General", MethodName = "createFile", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.updateDispositionsByDispositionID(4552, "pinit", false, 1, true);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "ScheduledCallbacks", MethodName = "updateDispositionsByDispositionID", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.createDispositions("testpashamaansss", false, 1);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "ScheduledCallbacks", MethodName = "createDispositions", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.updateDispositionsByDispositionID(4551, "Preview - Record Fina1");
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "ScheduledCallbacks", MethodName = "updateDispositionsByDispositionID", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getDispositionsByClassifications();
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "ScheduledCallbacks", MethodName = "getDispositionsByClassifications", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getSkillIdAgentsUnassigned(165451);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Skills", MethodName = "getSkillIdAgentsUnassigned", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getDispositionsBySkillId(165451);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Skills", MethodName = "getDispositionsBySkillId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getUnassignedDispositionsBySkillId(165451);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Skills", MethodName = "getUnassignedDispositionsBySkillId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getSkillsGeneralSettingBySkillId(152344);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Skills", MethodName = "getSkillsGeneralSettingBySkillId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getGroups();
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Group", MethodName = "getGroups", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.createGroups("PashaGroup2","true", "test");
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Group", MethodName = "createGroups", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getGroupsByGroupId(1073741996);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Group", MethodName = "getGroupsByGroupId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.updateGroupsByGroupId(1073741996, "test2", "true", "hello");
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Group", MethodName = "updateGroupsByGroupId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.postAgentGroupsByGroupId(1073741996, 210452);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Group", MethodName = "postAgentGroupsByGroupId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.deleteAgentGroupsByGroupId(1073741996, 210452);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Group", MethodName = "getTeams", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getContactFileByContactId(2233650423, "");
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Contacts", MethodName = "deleteAgentGroupsByGroupId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.postContactsTagsByContactId(2233650423, "");
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Contacts", MethodName = "postContactsTagsByContactId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.postDncGroups("SampledncPic", "dncgroup");
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "List", MethodName = "postDncGroups", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.UpdateDncGroupsByGroupId(369, "testedok", "fromlocal", "false");
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "List", MethodName = "UpdateDncGroupsByGroupId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.deleteDncGroupsByGroupIdSkillId(369, 165451);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "List", MethodName = "deleteDncGroupsByGroupIdSkillId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.postDncGroupsByGroupIdSkillId(369, 165451);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "List", MethodName = "postDncGroupsByGroupIdSkillId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.postDncGroupsScrubbedSkillsByGroupIdSkillId(369, 165451);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "List", MethodName = "postDncGroupsScrubbedSkillsByGroupIdSkillId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.deleteDncGroupsScrubbedSkillsByGroupIdSkillId(369, 165451);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "List", MethodName = "deleteDncGroupsScrubbedSkillsByGroupIdSkillId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getListsCallListJobs();
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "List", MethodName = "getListsCallListJobs", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.deleteListsCallListsJobsByJobId(1001);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "List", MethodName = "deleteListsCallListsJobsByJobId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getListsCallListJobs("100");
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "List", MethodName = "getListsCallListJobs", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getListsCallListListIdAttempts(100);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "List", MethodName = "getListsCallListListIdAttempts", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.deleteListsCallListsByListId(100);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "List", MethodName = "deleteListsCallListsByListId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.postSessionIdInteractionsContactIdtyping("QnlNeXg1UnVIVUswa09yazBpeU9TbGhtUEdnQ2FoYXNyRXZnbWwrWTdPbFpKTy9a", "2237331756");
            //ResultString.Add(new APIproperties() { APIType = "Agent", APISubType = "ChatRequest", MethodName = "postSessionIdInteractionsContactIdtyping", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.posttAgentSessionIdInteractionConatactIdParkemail("QnlNeXg1UnVIVUswa09yazBpeU9TbGhtUEdnQ2FoYXNyRXZnbWwrWTdPbFpKTy9a", "2237331856");
            //ResultString.Add(new APIproperties() { APIType = "Agent", APISubType = "Email", MethodName = "posttAgentSessionIdInteractionConatactIdParkemail", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.posttAgentSessionIdInteractionConatactIdUnparkemail("QnlNeXg1UnVIVUswa09yazBpeU9TbGhtUEdnQ2FoYXNyRXZnbWwrWTdPbFpKTy9a", "2237331856");
            //ResultString.Add(new APIproperties() { APIType = "Agent", APISubType = "Email", MethodName = "posttAgentSessionIdInteractionConatactIdUnparkemail", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.posttAgentSessionIdInteractionConatactIdPreview("QnlNeXg1UnVIVUswa09yazBpeU9TbGhtUEdnQ2FoYXNyRXZnbWwrWTdPbFpKTy9a", "2237331856");
            //ResultString.Add(new APIproperties() { APIType = "Agent", APISubType = "Email", MethodName = "getTeams", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.posttAgentSessionIdInteractionConatactIdEmailRestore("QnlNeXg1UnVIVUswa09yazBpeU9TbGhtUEdnQ2FoYXNyRXZnbWwrWTdPbFpKTy9a", "2237331856");
            //ResultString.Add(new APIproperties() { APIType = "Agent", APISubType = "Email", MethodName = "posttAgentSessionIdInteractionConatactIdPreview", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.posttAgentSessionIdInteractionConatactIdSnooze("QnlNeXg1UnVIVUswa09yazBpeU9TbGhtUEdnQ2FoYXNyRXZnbWwrWTdPbFpKTy9a", "2237331856");
            //ResultString.Add(new APIproperties() { APIType = "Agent", APISubType = "PersonalCon", MethodName = "posttAgentSessionIdInteractionConatactIdSnooze", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.posttAgentSessionIdAddcontact("QnlNeXg1UnVIVUswa09yazBpeU9Tb2lVVU9uWHFWREVqQ2pOTzZOeUp6YTU5WHhz");
            //ResultString.Add(new APIproperties() { APIType = "Agent", APISubType = "Sessions", MethodName = "posttAgentSessionIdAddcontact", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.posttAgentSessionIdInteractionConatactIdActivate("QnlNeXg1UnVIVUswa09yazBpeU9Tb2lVVU9uWHFWREVqQ2pOTzZOeUp6YTU5WHhz", "2237609862");
            //ResultString.Add(new APIproperties() { APIType = "Agent", APISubType = "Sessions", MethodName = "posttAgentSessionIdInteractionConatactIdActivate", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getContactsSmsTranscripts("1234");
            //ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "getContactsSmsTranscripts", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getContactsContactIdCallquality("2237609862");
            //ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "getContactsContactIdCallquality", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getReportJobsByJobId("123");
            //ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "getReportJobsByJobId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getwfodataqm();
            //ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "getwfodataqm", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.postContactsChatSessionTyping("1234");
            //ResultString.Add(new APIproperties() { APIType = "Patron", APISubType = "ChatRequests", MethodName = "postContactsChatSessionTyping", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.postContactsChatSessionTypingPreview("1234");
            //ResultString.Add(new APIproperties() { APIType = "Patron", APISubType = "ChatRequests", MethodName = "postContactsChatSessionTypingPreview", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });            

            //propertiesOb.APIresultStatus = operationOb.getAgentsAgentIdLoginhistory(1234);
            //ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "getAgentsAgentIdLoginhistory", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getAgentsAgentIdLoginhistory(1234);
            //ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "getAgentsAgentIdLoginhistory", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getContactsContactIdCallquality("1234");
            //ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "getContactsContactIdCallquality", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getReportJobsByJobId("1234");
            //ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "getReportJobsByJobId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.postReportjobsDatadownlaodByReportId("123");
            //ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "postReportjobsDatadownlaodByReportId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getwfodataascm("", "09/01/2016", "09/01/2017");
            //ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "getwfodataascm", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getwfodataasi("", "09/01/2016", "09/14/2016");
            //ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "getwfodataasi", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getwfodatacsi("", "09/01/2016", "09/01/2016");
            //ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "getwfodatacsi", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getwfodataftci("", "09/01/2016", "09/01/2016");
            //ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "getwfodataftci", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getwfodataosi("", "09/01/2016", "09/01/2016");
            //ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "getwfodataosi", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getwfodataqm("", " ", " ");
            //ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "getwfodataqm", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getwfmdataSkillsContacts("", "09/01/2016", "09/01/2016");
            //ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "WFM Data", MethodName = "getwfmdataSkillsContacts", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getwfmdataAgents("", "09/01/2016", "09/01/2016");
            //ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "WFM Data", MethodName = "getwfmdataAgents", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getwfmdataSkillsDialerContacts("", "09/01/2016", "09/01/2016");
            //ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "WFM Data", MethodName = "getwfmdataSkillsDialerContacts", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getwfmdataAgentsScheduleAdherence("", "09/01/2016", "09/01/2016");
            //ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "WFM Data", MethodName = "getwfmdataAgentsScheduleAdherence", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getwfmdataAgentsScorecards("", "09/01/2016", "09/01/2016");
            //ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "WFM Data", MethodName = "getwfmdataAgentsScorecards", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getwfmdataSkillsAgentPerformance("", "09/01/2016", "09/01/2016");
            ////get the poc of chat profile based in the BU and it will work based which version is enabled 
            ////for nikhil@hc8 use this 2d102835 - fa77 - 4c93 - 906e-cfd1889ed168
            //ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "WFM Data", MethodName = "getwfmdataSkillsAgentPerformance", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getChatprofileByPOCId("2d102835-fa77-4c93-906e-cfd1889ed168");
            //ResultString.Add(new APIproperties() { APIType = "Patron", APISubType = "ChatRequests", MethodName = "getChatprofileByPOCId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getTeamIDPerformance(55581, "09/01/2016", "09/01/2016");
            //ResultString.Add(new APIproperties() { APIType = "Reporting", APISubType = "Reporting", MethodName = "getTeamIDPerformance", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getThankYouForSkillId(162575);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Skill", MethodName = "getThankYouForSkillId", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.postContactChatSendEmail("maktum.pasha@hc8.com", "rafiq.j@servion.com", "test12222");
            //ResultString.Add(new APIproperties() { APIType = "Patron", APISubType = "ChatRequests", MethodName = "postContactChatSendEmail", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.getStandardAddressBookEntries(12345);
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Addressbook", MethodName = "getStandardAddressBookEntries", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            //propertiesOb.APIresultStatus = operationOb.PostAgents();
            //ResultString.Add(new APIproperties() { APIType = "Admin", APISubType = "Agent", MethodName = "PostAgents", APIversion = apiversion, APIresultStatus = propertiesOb.APIresultStatus });

            ViewBag.data = ResultString;
            return View();
        }
    }
}