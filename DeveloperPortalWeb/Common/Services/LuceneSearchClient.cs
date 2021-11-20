using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InContact.DeveloperPortal.Web.LuceneSearch;


namespace InContact.DeveloperPortal.Web.Common.Services
{
	public interface ILuceneSearchDevClient
	{
		List<SearchResult> GetSearchData(string searchTerm);
	}
	public class LuceneSearchDevClient : ILuceneSearchDevClient
	{

		public List<SearchResult> GetSearchData(string searchTerm)
		{
			LuceneSearchClient client = null;
			List<SearchResult> result = null;

			// Exceptions for new routing (suport new DEVone rebranbding v2)
			var exceptions = new Dictionary<string, string>
			{
				{ "Documentation/RegisterYourApplication", "Documentation/GettingStarted?accordionId=RegisterYourApplication" },
				{ "Documentation/CreateAnAuthorizationKey", "Documentation/GettingStarted?accordionId=CreateAnAuthorizationKey" },
				{ "Documentation/SendATokenRequest", "Documentation/GettingStarted?accordionId=SendATokenRequest" },
				{ "Documentation/SendAnImplicitRequest", "Documentation/GettingStarted?accordionId=SendAnImplicitRequest" },
				{ "Documentation/RetrieveTheToken", "Documentation/GettingStarted?accordionId=RetrieveTheToken" },
				{ "Documentation/UseTheToken", "Documentation/GettingStarted?accordionId=UseTheToken" },
				{ "Documentation/ExpiredTokens", "Documentation/GettingStarted?accordionId=ExpiredTokens" },
				{ "Documentation/TokenRequestExampleCode", "Documentation/GettingStarted?accordionId=TokenRequestExampleCode" },

				{ "Documentation/AddingMobileSDK", "Documentation/MobileSDK?accordionId=AddingMobileSDK" },
				{ "Documentation/AddingChatWidget", "Documentation/MobileSDK?accordionId=AddingChatWidget" },
				{ "Documentation/AddingCallbackWidget", "Documentation/MobileSDK?accordionId=AddingCallbackWidget" },
				{ "Documentation/CustomizeWidgetInterface", "Documentation/MobileSDK?accordionId=CustomizeWidgetInterface" },
				{ "Documentation/CustomizeCallbackWidget", "Documentation/MobileSDK?accordionId=CustomizeCallbackWidget" },
				{ "Documentation/UsingQueueStats", "Documentation/MobileSDK?accordionId=UsingQueueStats" },
				{ "Documentation/KeyResourceClasses", "Documentation/MobileSDK?accordionId=KeyResourceClasses" },

				{ "Documentation/CreateEndAnAgentSession", "Documentation/UsingEvents?accordionId=CreateEndAnAgentSession" },
				{ "Documentation/RequestingEvents", "Documentation/UsingEvents?accordionId=RequestingEvents" },
				{ "Documentation/EventTypes", "Documentation/UsingEvents?accordionId=EventTypes" },

				{ "Documentation/AgentEvents", "Documentation/AgentSessionEvents?accordionId=AgentEvents" },
				{ "Documentation/PersonalConnectionEvents", "Documentation/AgentSessionEvents?accordionId=PersonalConnectionEvents" },
				{ "Documentation/CallContactEvents", "Documentation/AgentSessionEvents?accordionId=CallContactEvents" },
				{ "Documentation/WorkItemContactEvents", "Documentation/AgentSessionEvents?accordionId=WorkItemContactEvents" },
				{ "Documentation/IndicatorEvents", "Documentation/AgentSessionEvents?accordionId=IndicatorEvents" },
				{ "Documentation/PageOpenEvents", "Documentation/AgentSessionEvents?accordionId=PageOpenEvents" },
				{ "Documentation/ChatContactEvents", "Documentation/AgentSessionEvents?accordionId=ChatContactEvents" },
				{ "Documentation/SupervisorEvents", "Documentation/AgentSessionEvents?accordionId=SupervisorEvents" },
				{ "Documentation/OtherEvents", "Documentation/AgentSessionEvents?accordionId=OtherEvents" }
			};

			try
			{
				client = new LuceneSearchClient();
				result = client.SearchForData(searchTerm);

				foreach (var item in result)
				{
					foreach (var exception in exceptions)
					{
						if (item.Link == exception.Key)
						{
							item.Link = exception.Value;
						}
					}
				}
			}
			catch
			{
				client.Abort();
				throw;
			}

			return result;
		}

	}
}