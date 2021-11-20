package com.incontact;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileWriter;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.PrintWriter;
import java.lang.reflect.Type;
import java.net.URL;
import javax.net.ssl.HttpsURLConnection;
import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import java.util.Map;
import java.util.Properties;
import java.util.stream.Collectors;



public class Methods {

	public String TOKEN_SERVICE_URL;
	public String VERSION;
	public String SERVICES_PATH;
	public String RESOURCE_SERVER_BASE_URI_PROPERTY;
	public String CXONE_RESOURCE_SERVER_BASE_URI_PROPERTY;
	public String AUTHORIZATION_HEADER;
	public String AUTHORIZATION_BASIC_PREFIX;
	public String BASIC_ACCESS_TOKEN;
	public String TOKEN_AUTHORIZATION_HEADER_VALUE;
	public String AUTHORIZATION_BEARER_PREFIX;
	public String CONTENT_TYPE_HEADER;
	public String CONTENT_LENGTH;
	public String Content_Length;
	public String ACCEPT_HEADER;
	public String APPLICATION_JSON_VALUE;
	public String USER_NAME;
	public String PASSWORD;
	public String IS_MOCK_EVOLVE_VAL;
	public String TOKEN_REQUEST_BODY;
	public String ACCESS_TOKEN_PROPERTY;
	public String CLUSTER;
	public String response;
	public PrintWriter writer;
	public String CONTENT_LENGTH_HEADER;

	public void initTable() {
		String content = "<!DOCTYPE html>\r\n" + "	    	    <html>\r\n" + "	    	    <head>\r\n"
				+ "	    	    <link rel=\"stylesheet\" type=\"text/css\" href=\"table.css\">\r\n"
				+ "	    	    </head>\r\n" + "	    	    <body class=\"rTable\">\r\n"
				+ "	    	    <h2>API Test Results using Java</h2>\r\n" + "	    	    <title>Java</title>\r\n"
				+ "	    	    <br/>\r\n"
				+ "	    	    <div class=\"rTableHead small\"><strong>APIName</strong></div>\r\n"
				+ "	    	    <div class=\"rTableHead small\"><span style=\"font-weight: bold;\">APISubType</span></div>\r\n"
				+ "	    	    <div class=\"rTableHead med\"><span style=\"font-weight: bold;\">APIType</span></div>\r\n"
				+ "	    	    <div class=\"rTableHead small\"><span style=\"font-weight: bold;\">Version</span></div>\r\n"
				+ "	    	    <div class=\"rTableHead\"><span style=\"font-weight: bold;\">APIResult</span></div>\r\n"
				+ "	    	    </div>\r\n" + "	    	    </body>\r\n" + "	    	    </html>";
		try {
			writer = new PrintWriter(new File("result.html"));
			writer.write(content);
			writer.close();

		} catch (Exception e) {

			e.printStackTrace();
		}
	}

	public void construct(String aPIName, String aPISubType, String aPIType, String version, int status,
			String description) throws IOException {
		String result = "<div class=\"rTableRow\">\r\n" + "			<div class=\"rTableCell small\">%s</div>\r\n"
				+ "			<div class=\"rTableCell small\">%s</div>\r\n"
				+ "			<div class=\"rTableCell med\">%s</div>\r\n"
				+ "			<div class=\"rTableCell small\">%s</div>\r\n"
				+ "			<div class=\"rTableCell\">%d:%s</div>\r\n" + "			</div>";

		String execute = String.format(result, aPIName, aPISubType, aPIType, version, status, description);
		FileWriter fw = new FileWriter("result.html", true);
		try {
			fw.write(execute);
		} finally {
			fw.close();
		}
	}

	public void readingProperties() throws IOException {

		try {

			Properties prop = new Properties();
			String propFileName = "com/incontact/config.properties";

			InputStream inputStream = getClass().getClassLoader().getResourceAsStream(propFileName);

			if (inputStream != null) {
				prop.load(inputStream);
			} else {
				throw new FileNotFoundException("property file '" + propFileName + "' not found in the classpath");
			}

			TOKEN_SERVICE_URL = prop.getProperty("TOKEN_SERVICE_URL");
			RESOURCE_SERVER_BASE_URI_PROPERTY = prop.getProperty("RESOURCE_SERVER_BASE_URI_PROPERTY");
			CXONE_RESOURCE_SERVER_BASE_URI_PROPERTY = prop.getProperty("CXONE_RESOURCE_SERVER_BASE_URI_PROPERTY");
			AUTHORIZATION_HEADER = prop.getProperty("AUTHORIZATION_HEADER");
			AUTHORIZATION_BASIC_PREFIX = prop.getProperty("AUTHORIZATION_BASIC_PREFIX");
			BASIC_ACCESS_TOKEN = prop.getProperty("BASIC_ACCESS_TOKEN");
			TOKEN_AUTHORIZATION_HEADER_VALUE = AUTHORIZATION_BASIC_PREFIX + BASIC_ACCESS_TOKEN;
			AUTHORIZATION_BEARER_PREFIX = prop.getProperty("AUTHORIZATION_BEARER_PREFIX");
			CONTENT_TYPE_HEADER = prop.getProperty("CONTENT_TYPE_HEADER");
			ACCEPT_HEADER = prop.getProperty("ACCEPT_HEADER");
			APPLICATION_JSON_VALUE = prop.getProperty("APPLICATION_JSON_VALUE");
			USER_NAME = prop.getProperty("USER_NAME");
			PASSWORD = prop.getProperty("PASSWORD");
			IS_MOCK_EVOLVE_VAL = prop.getProperty("IS_MOCK_EVOLVE_VAL");
			TOKEN_REQUEST_BODY = String.format(
					"{grant_type:\"password\",username:\"%s\",password:\"%s\",isMockEvolve:\"%s\"}", USER_NAME,
					PASSWORD, IS_MOCK_EVOLVE_VAL);
			CLUSTER = prop.getProperty("CLUSTER");
		} catch (Exception e) {
			System.out.println("Exception: " + e);
		}

	}

	public void verifyAccessToken() throws IOException {

		HttpsURLConnection tokenService = (HttpsURLConnection) (new URL((TOKEN_SERVICE_URL)).openConnection());
		tokenService.setRequestMethod("POST");
		tokenService.setDoOutput(true);
		tokenService.setDoInput(true);
		tokenService.setRequestProperty(AUTHORIZATION_HEADER, TOKEN_AUTHORIZATION_HEADER_VALUE);
		tokenService.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
		tokenService.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
		tokenService.connect();

		tokenService.getOutputStream().write(TOKEN_REQUEST_BODY.getBytes());

		response = new BufferedReader(new InputStreamReader(tokenService.getInputStream())).lines()
				.collect(Collectors.joining());

		HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL((TOKEN_SERVICE_URL)).openConnection());
		urlConnection.setRequestMethod("POST");
		urlConnection.setDoOutput(true);
		urlConnection.setRequestProperty(AUTHORIZATION_HEADER, TOKEN_AUTHORIZATION_HEADER_VALUE);
		urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.getOutputStream().write(TOKEN_REQUEST_BODY.getBytes());
		response = new BufferedReader(new InputStreamReader(urlConnection.getInputStream())).lines()
				.collect(Collectors.joining());

		Gson gson = new Gson();
		Type mapType = new TypeToken<Map<String, String>>() {
		}.getType();
		Map<String, String> ser = gson.fromJson(response, mapType);
		ACCESS_TOKEN_PROPERTY = ser.get("access_token");
		System.out.println("Access Token = " + ACCESS_TOKEN_PROPERTY);

	}

	// Admin--> Agents V13
	public void getAgents() throws IOException {

		final String API_PATH = "/agents";
		final String API_URL = "services/v13.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.connect();
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Agents", "GET /agents", CLUSTER + "- v13", responseCode, responseLine);
		}

		else {

			// No token - get one or handle error
		}
	}

	// Admin--> Agents V13
	public void GetagentbyAgentID(String agentId, String fields) throws IOException {

		final String API_PATH = "/agents/".concat(agentId);
		final String API_URL = "services/v13.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.connect();
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Agents", "GET /agents/{agentId}", CLUSTER + "- v13", responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	// Admin--> General V13
	public void getUnavailableCodes(String activeOnly) throws IOException {

		final String API_PATH = "/unavailable-codes";
		final String API_URL = "services/v13.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty("activeOnly", activeOnly);
			urlConnection.connect();
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "General", "GET /unavailable-codes", CLUSTER + "- v13", responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	// Admin--> Skills V13
	public void GetCPAManagement(String skillId, String fields) throws IOException {

		final String API_PATH = "/skills/".concat(skillId) + "/parameters/cpa-management";
		final String API_URL = "services/v13.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty("fields", fields);
			urlConnection.connect();
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Skills", "GET /skills/{skillId}/parameters/cpa-management", CLUSTER + "- v13",
					responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	// Admin--> Skills V13
	public void Getdeliverypreference(String skillId, String fields) throws IOException {

		final String API_PATH = "/skills/".concat(skillId) + "/parameters/delivery-preferences";
		final String API_URL = "services/v13.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty("fields", fields);
			urlConnection.connect();
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Skills", "GET /skills/{skillId}/parameters/delivery-preferences", CLUSTER + "- v13",
					responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	// Admin--> Skills V13
	public void GetSkillParameterGeneralSetting(String skillId, String fields) throws IOException {

		final String API_PATH = "/skills/".concat(skillId) + "/parameters/general-settings";
		final String API_URL = "services/v13.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty("fields", fields);
			urlConnection.connect();
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Skills", "GET /skills/{skillId}/parameters/general-settings", CLUSTER + "- v13",
					responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	// Admin--> Skills V13
	public void Getskillretrysetting(String skillId, String fields) throws IOException {

		final String API_PATH = "/skills/".concat(skillId) + "/parameters/retry-settings";
		final String API_URL = "services/v13.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty("fields", fields);
			urlConnection.connect();
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Skills", "GET /skills/{skillId}/parameters/retry-settings", CLUSTER + "- v13",
					responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	// Admin--> Skills V13
	public void Getschedulesettings(String skillId) throws IOException {

		final String API_PATH = "/skills/".concat(skillId) + "/parameters/schedule-settings";
		final String API_URL = "services/v13.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.connect();
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Skills", "GET /skills/{skillId}/parameters/schedule-settings", CLUSTER + "- v13",
					responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	// Admin--> Skills V13
	public void Getxssettings(String skillId, String fields) throws IOException {

		final String API_PATH = "/skills/".concat(skillId) + "/parameters/xs-settings";
		final String API_URL = "services/v13.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty("fields", fields);
			urlConnection.connect();
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Skills", "GET /skills/{skillId}/parameters/xs-settings", CLUSTER + "- v13",
					responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	// Admin--> Agents V13
	public void PostAgent(String param) throws IOException {

		final String API_PATH = "/agents";
		final String API_URL = "services/v13.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("POST");
			urlConnection.connect();
			OutputStream os = urlConnection.getOutputStream();
			os.write(param.getBytes("UTF-8"));
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Agents", "POST /agents", CLUSTER + "- v13", responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	// Admin--> Agents V13
	public void PutAgentbyAgentID(String agentId, String param) throws IOException {

		final String API_PATH = "/agents/".concat(agentId);
		final String API_URL = "services/v13.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("PUT");
			urlConnection.connect();
			OutputStream os = urlConnection.getOutputStream();
			os.write(param.getBytes("UTF-8"));
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Agents", "PUT /agents/{agentId}", CLUSTER + "- v13", responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	// Admin--> Skills V13
	public void PutCPAManagementbySkillId(String skillId, String param) throws IOException {

		final String API_PATH = "/skills/".concat(skillId) + "/parameters/cpa-management";
		final String API_URL = "services/v13.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("PUT");
			urlConnection.connect();
			OutputStream os = urlConnection.getOutputStream();
			os.write(param.getBytes("UTF-8"));
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Skills", "PUT /skills/{skillId}/parameters/cpa-management", CLUSTER + "- v13",
					responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	// Admin--> Skills V13
	public void Putskilldeliverypreference(String skillId, String param) throws IOException {

		final String API_PATH = "/skills/".concat(skillId) + "/parameters/delivery-preferences";
		final String API_URL = "services/v13.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("PUT");
			urlConnection.connect();
			OutputStream os = urlConnection.getOutputStream();
			os.write(param.getBytes("UTF-8"));
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Skills", "PUT /skills/{skillId}/parameters/delivery-preferences", CLUSTER + "- v13",
					responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	// Admin--> Skills V13
	public void Putskillretrysetting(String skillId, String param) throws IOException {

		final String API_PATH = "/skills/".concat(skillId) + "/parameters/retry-settings";
		final String API_URL = "services/v13.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("PUT");
			urlConnection.connect();
			OutputStream os = urlConnection.getOutputStream();
			os.write(param.getBytes("UTF-8"));
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Skills", "PUT /skills/{skillId}/parameters/retry-settings", CLUSTER + "- v13",
					responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	// Admin--> Skills V13
	public void Putschedulesettings(String skillId, String param) throws IOException {

		final String API_PATH = "/skills/".concat(skillId) + "/parameters/schedule-settings";
		final String API_URL = "services/v13.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("PUT");
			urlConnection.connect();
			OutputStream os = urlConnection.getOutputStream();
			os.write(param.getBytes("UTF-8"));
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Skills", "PUT /skills/{skillId}/parameters/schedule-settings", CLUSTER + "- v13",
					responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	// Admin--> Skills V13
	public void Putxssettings(String skillId, String param) throws IOException {

		final String API_PATH = "/skills/".concat(skillId) + "/parameters/xs-settings";
		final String API_URL = "services/v13.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("PUT");
			urlConnection.connect();
			OutputStream os = urlConnection.getOutputStream();
			os.write(param.getBytes("UTF-8"));
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Skills", "PUT /skills/{skillId}/parameters/xs-settings", CLUSTER + "- v13",
					responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	// Admin--> Agents V13
	public void PutTeam(String param) throws IOException {

		final String API_PATH = "/teams";
		final String API_URL = "services/v13.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("POST");
			urlConnection.connect();
			OutputStream os = urlConnection.getOutputStream();
			os.write(param.getBytes("UTF-8"));
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Agents", "POST /teams", CLUSTER + "- v13", responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	// Admin--> Agents V13
	public void PutTeambyTeamID(String teamId, String param) throws IOException {

		final String API_PATH = "/teams/".concat(teamId);
		final String API_URL = "services/v13.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("PUT");
			urlConnection.connect();
			OutputStream os = urlConnection.getOutputStream();
			os.write(param.getBytes("UTF-8"));
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Agents", "PUT /teams/{teamId}", CLUSTER + "- v13", responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	// Admin--> Agents V13
	public void assignUnavailableCodesToTeam(String teamId, String param) throws IOException {

		final String API_PATH = "/teams/".concat(teamId) + "/unavailable-codes";
		final String API_URL = "services/v13.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("PUT");
			urlConnection.connect();
			OutputStream os = urlConnection.getOutputStream();
			os.write(param.getBytes("UTF-8"));
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Agents", "PUT /teams/{teamId}/unavailable-codes", CLUSTER + "- v13", responseCode,
					responseLine);
		} else {
			// No token - get one or handle error
		}
	}
	// Version 15

	// Admin
	public void GetAccesskeys(String agentId, String fields, String skip, String top, String orderBy)
			throws IOException {

		final String API_PATH = "/access-keys";
		final String API_URL = "services/v16.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty("fields", fields);
			urlConnection.setRequestProperty("skip", skip);
			urlConnection.setRequestProperty("top", top);
			urlConnection.setRequestProperty("orderBy", orderBy);
			urlConnection.connect();
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Agents", "GET /access-keys", CLUSTER + "- v15", responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	public void PostAccessKeys(String agentId) throws IOException {

		final String API_PATH = "/access-keys";
		final String API_URL = "services/v16.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("POST");
			urlConnection.connect();
			OutputStream os = urlConnection.getOutputStream();
			os.write(agentId.getBytes("UTF-8"));
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Agents", "POST /access-keys", CLUSTER + "- v15", responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	public void GetAccesskeysById(String accessKeyId) throws IOException {

		final String API_PATH = "/access-keys/".concat(accessKeyId);
		final String API_URL = "services/v16.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.connect();
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Agents", "GET /access-keys/{accesskeyId}", CLUSTER + "- v15", responseCode,
					responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	public void DeleteAccessKeysByID(String accessKeyId) throws IOException {

		final String API_PATH = "/access-keys/".concat(accessKeyId);
		final String API_URL = "services/v16.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("DELETE");
			urlConnection.connect();
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Agents", "DELETE /access-keys/{accesskeyId}", CLUSTER + "- v15", responseCode,
					responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	public void PatchAccessKeyByID(String accessKeyId, String isActive) throws IOException {

		final String API_PATH = "/access-keys/".concat(accessKeyId);
		final String API_URL = "services/v16.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty("X-HTTP-Method-Override", "PATCH");
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("POST");
			urlConnection.connect();
			OutputStream os = urlConnection.getOutputStream();
			os.write(isActive.getBytes("UTF-8"));
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Agents", "PATCH /access-keys/{accessKeyId}", CLUSTER + "- v15", responseCode,
					responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	public void PostCamapigns(String param) throws IOException {

		final String API_PATH = "/campaigns";
		final String API_URL = "services/v16.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("POST");
			urlConnection.connect();
			OutputStream os = urlConnection.getOutputStream();
			os.write(param.getBytes("UTF-8"));
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Skills", "POST /campaigns", CLUSTER + "- v15", responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	public void PutCampaignsByID(String param, int CampaignID) throws IOException {

		final String API_PATH = "/campaigns/" + CampaignID;
		final String API_URL = "services/v16.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("PUT");
			urlConnection.connect();
			OutputStream os = urlConnection.getOutputStream();
			os.write(param.getBytes("UTF-8"));
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Skills", "PUT /campaigns/{campaignId}", CLUSTER + "- v15", responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	public void PostCamapignsBySkill(String param, int CampaignID) throws IOException {

		final String API_PATH = "/campaigns/" + CampaignID + "/skills";
		final String API_URL = "services/v16.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("POST");
			urlConnection.connect();
			OutputStream os = urlConnection.getOutputStream();
			os.write(param.getBytes("UTF-8"));
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Skills", "POST /campaigns/{campaignId}/skills", CLUSTER + "- v15", responseCode,
					responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	public void DeleteCamapaignsBySkill(String param, int CampaignID) throws IOException {

		final String API_PATH = "/campaigns/" + CampaignID + "/skills";
		final String API_URL = "services/v16.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("DELETE");
			urlConnection.connect();
			OutputStream os = urlConnection.getOutputStream();
			os.write(param.getBytes("UTF-8"));
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Skills", "DELETE /campaigns/{campaignId}/skills", CLUSTER + "- v15", responseCode,
					responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	public void GetSmsHistoricalTranscriptsByContactID(int contactId, String businessUnitId) throws IOException {

		final String API_PATH = "/contacts/" + contactId + "/sms-historical-transcript";
		final String API_URL = "services/v16.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty("businessUnitId", businessUnitId);
			urlConnection.connect();
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Agents", "GET /contacts/{contactId}/sms-historical-transcript", CLUSTER + "- v15",
					responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	public void GetSmsHistoricalTranscripts(String businessUnitId, String ani, String SkillID) throws IOException {

		final String API_PATH = "/contacts/sms-historical-contacts";
		final String API_URL = "services/v16.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.connect();
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Agents", "GET /contacts/sms-historical-contacts", CLUSTER + "- v15", responseCode,
					responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	public void PutUnavailableCodesByID(String param, int unavailableCodeId) throws IOException {

		final String API_PATH = "/unavailable-codes/" + unavailableCodeId;
		final String API_URL = "services/v16.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("PUT");
			urlConnection.connect();
			OutputStream os = urlConnection.getOutputStream();
			os.write(param.getBytes("UTF-8"));
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Agents", "PUT /unavailable-codes/{unavailableCodeId}", CLUSTER + "- v15", responseCode,
					responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	public void PostHoursofOperation(String param) throws IOException {

		final String API_PATH = "/hours-of-operation";
		final String API_URL = "services/v16.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("POST");
			urlConnection.connect();
			OutputStream os = urlConnection.getOutputStream();
			os.write(param.getBytes("UTF-8"));
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "General", "POST /hours-of-operation", CLUSTER + "- v15", responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	public void PuthoursofOperationByID(int profileId) throws IOException {

		final String API_PATH = "/hours-of-operation/" + profileId;
		final String API_URL = "services/v16.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("PUT");
			urlConnection.connect();
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "General", "PUT /hours-of-operation/{profileId}", CLUSTER + "- v15", responseCode,
					responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	public void PostPointofContact(String param) throws IOException {

		final String API_PATH = "/points-of-contact";
		final String API_URL = "services/v16.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("POST");
			urlConnection.connect();
			OutputStream os = urlConnection.getOutputStream();
			os.write(param.getBytes("UTF-8"));
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "General", "POST /points-of-contact", CLUSTER + "- v15", responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	public void PutPointofContactByID(int pointOfContactId, String param) throws IOException {

		final String API_PATH = "/points-of-contact/" + pointOfContactId;
		final String API_URL = "services/v16.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("PUT");
			urlConnection.connect();
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "General", "PUT /points-of-contact/{pointOfContactId}", CLUSTER + "- v15", responseCode,
					responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	public void PostUnavailableCodes(String param) throws IOException {

		final String API_PATH = "/unavailable-codes";
		final String API_URL = "services/v16.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("POST");
			urlConnection.connect();
			OutputStream os = urlConnection.getOutputStream();
			os.write(param.getBytes("UTF-8"));
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "General", "POST /unavailable-codes", CLUSTER + "- v15", responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	public void GetPhonenumbers(String searchString, String skip, String top) throws IOException {

		final String API_PATH = "/phone-numbers";
		final String API_URL = "services/v16.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty("searchString", searchString);
			urlConnection.setRequestProperty("skip", skip);
			urlConnection.setRequestProperty("top", top);
			urlConnection.connect();
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "General", "GET /phone-numbers", CLUSTER + "- v15", responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	public void GetDispositionBySkills(String updatedSince, String searchString, String fields, String skip, String top,
			String orderBy, String isactive) throws IOException {

		final String API_PATH = "/dispositions/skills";
		final String API_URL = "services/v16.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty("fields", fields);
			urlConnection.setRequestProperty("skip", skip);
			urlConnection.setRequestProperty("top", top);
			urlConnection.setRequestProperty("orderBy", orderBy);
			urlConnection.connect();
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Skills", "GET /dispositions/skills", CLUSTER + "- v15", responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	public void PutUnavailableCodeByIDTeams(String param, int unavailableCodeId, String securityUser)
			throws IOException {

		final String API_PATH = "/unavailable-codes/" + unavailableCodeId + "/teams";
		final String API_URL = "services/v16.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("PUT");
			urlConnection.connect();
			OutputStream os = urlConnection.getOutputStream();
			os.write(param.getBytes("UTF-8"));
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Agents", "PUT /unavailable-codes/{unavailableCodeId}/teams", CLUSTER + "- v15",
					responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	public void PostActivate(String sessionId, int contactId) throws IOException {

		final String API_PATH = "/agent-sessions/" + sessionId + "/interactions/" + contactId + "/activate";
		final String API_URL = "services/v16.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty("Content_Length", "CONTENT_LENGTH");
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("POST");
			urlConnection.connect();
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Agents", "Sessions", "POST /activate", CLUSTER + "- V15.0", responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	public void getTeamsAgentbyTeamId(String teamId, String searchString, String fields, String skip, String top,
			String orderBy, String updatedSince) throws IOException {
		final String API_PATH = "/teams/" + teamId + "/agents";
		final String API_URL = "services/v16.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty("fields", fields);
			urlConnection.setRequestProperty("skip", skip);
			urlConnection.setRequestProperty("top", top);
			urlConnection.setRequestProperty("orderBy", orderBy);
			urlConnection.connect();
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Agents", "GET /teamsbyIdAgents", CLUSTER + "V15.0", responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	public void PostjobsSync(String param) throws IOException {

		final String API_PATH = "data-extraction/v1/jobs/sync";

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(CXONE_RESOURCE_SERVER_BASE_URI_PROPERTY) + API_PATH).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("POST");
			urlConnection.connect();
			OutputStream os = urlConnection.getOutputStream();
			os.write(param.getBytes("UTF-8"));
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("DataExtraction", "Extracting Data", "POST /jobs/sync", CLUSTER + "- v1", responseCode,
					responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	public void GetMediaplaybackbyID(String contactID, String mediaType, String excludeWaveform) throws IOException {

		final String API_PATH = "media-playback/v1/contacts/" + contactID;
		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(CXONE_RESOURCE_SERVER_BASE_URI_PROPERTY) + API_PATH).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty("mediaType", mediaType);
			urlConnection.setRequestProperty("excludeWaveform", excludeWaveform);
			urlConnection.connect();
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Mediaplayback", "Accessing the Recording Media", "GET /media-playback/v1/contacts/{contactId}",
					CLUSTER + "- v1", responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	public void GetMediaplaybackbysegmentID(String contactID, String segmentID, String mediaType,
			String excludeWaveform) throws IOException {

		final String API_PATH = "media-playback/v1/contacts/" + contactID + "/segments/" + segmentID;
		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(CXONE_RESOURCE_SERVER_BASE_URI_PROPERTY) + API_PATH).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty("mediaType", mediaType);
			urlConnection.setRequestProperty("excludeWaveform", excludeWaveform);
			urlConnection.connect();
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Mediaplayback", "Accessing the Recording Media",
					"GET /media-playback/v1/contacts/{contactId}/segments/{segmentId}", CLUSTER + "- v1", responseCode,
					responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	public void GetMediaplaybackContacts(String acdID, String mediaType, String excludeWaveform) throws IOException {

		final String API_PATH = "media-playback/v1/contacts/";
		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(CXONE_RESOURCE_SERVER_BASE_URI_PROPERTY) + API_PATH).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty("acdID", acdID);
			urlConnection.setRequestProperty("mediaType", mediaType);
			urlConnection.setRequestProperty("excludeWaveform", excludeWaveform);
			urlConnection.connect();
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Mediaplayback", "Accessing the Recording Media", "GET /media-playback/v1/contacts",
					CLUSTER + "- v1", responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}

	public void GetActiveFlag(String activeFlag) throws IOException {

		final String API_PATH = "services/v16.0/workflow-data/list/"+ activeFlag ;
		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_PATH).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.connect();
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Workflow Data", "GET /workflow-data/list/ID",
					CLUSTER + "- v16", responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}
	
	public void PostWorkflowData(String param) throws IOException {

		final String API_PATH = "services/v16.0/workflow-data";

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_PATH).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("POST");
			urlConnection.connect();
			OutputStream os = urlConnection.getOutputStream();
			os.write(param.getBytes("UTF-8"));
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Workflow Data", "POST /workflow-data/", CLUSTER + "- v16", responseCode,
					responseLine);
		} else {
			// No token - get one or handle error
		}
	}
	
	public void GetWorkflowDataByID(String workflowDataId ) throws IOException {

		final String API_PATH = "services/v16.0/workflow-data/"+ workflowDataId  ;
		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_PATH).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.connect();
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Workflow Data", "GET /workflow-data/list/ID",
					CLUSTER + "- v16", responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}	
	
	
	public void PutWorkflowDataByID(String workflowDataId) throws IOException {

		final String API_PATH = "/workflow-data/"+ workflowDataId  ;
		final String API_URL = "services/v16.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("PUT");
			urlConnection.connect();
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Workflow", "PUT /workflow-data/Id", CLUSTER + "- v16 ", responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}
	
	public void PutWorkflowDataByIdActivate(String workflowDataId) throws IOException {

		final String API_PATH = "/workflow-data/" + workflowDataId + "/activate"  ;
		final String API_URL = "services/v16.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("PUT");
			urlConnection.connect();
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Workflow", "PUT /workflow-data/Id/Activate", CLUSTER + "- v16 ", responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}
	
	public void PutWorkflowDataByIdDeactivate(String workflowDataId) throws IOException {

		final String API_PATH = "/workflow-data/" + workflowDataId + "/deactivate"  ;
		final String API_URL = "services/v16.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("PUT");
			urlConnection.connect();
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Workflow", "PUT /workflow-data/Id/Deactivate", CLUSTER + "- v16 ", responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}
	
	public void GetJobs() throws IOException {

		final String API_PATH = "data-extraction/v1/jobs";
		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(CXONE_RESOURCE_SERVER_BASE_URI_PROPERTY) + API_PATH).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.connect();
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("DataExtraction", "Extracting Data", "POST /jobs", CLUSTER + "- v1", responseCode,responseLine);
		} else {
			// No token - get one or handle error
		}
	}
	
	public void GetJobsByID(String jobsID) throws IOException {

		final String API_PATH = "data-extraction/v1/jobs/"+ jobsID;
		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(CXONE_RESOURCE_SERVER_BASE_URI_PROPERTY) + API_PATH).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.connect();
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("DataExtraction", "Extracting Data", "POST /jobs/jobsid", CLUSTER + "- v1", responseCode,responseLine);
		} else {
			// No token - get one or handle error
		}
	}

    public void deleteSuppressedContacts(String suppressedContactId) throws IOException
    {
        // Open a connection to the inContact Token Authorization Service
        URLConnection  tokenService = new URL(TOKEN_SERVICE_URL).openConnection();
        
        // Set required headers
        tokenService.setRequestProperty(AUTHORIZATION_HEADER, TOKEN_AUTHORIZATION_HEADER_VALUE);
        tokenService.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
        
        // Post the token request to the connection stream
        tokenService.setDoOutput(true);        
        tokenService.getOutputStream().write((TOKEN_REQUEST_BODY).getBytes());
        // Read the response
        String  response = new BufferedReader(new InputStreamReader(tokenService.getInputStream())).lines().collect(Collectors.joining());
        // TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON package to
        // handle the response appropriately
        Map<String, String> map = Pattern.compile(",").splitAsStream(response.replaceAll("\"|\\{|\\}", "")).map(nvp->nvp.split(":", 2)).collect(Collectors.toMap(nvp->nvp[0], nvp->nvp[1]));
        // Create a connection to the API using the appropriate base uri
        HttpURLConnection   urlConnection = (HttpURLConnection)new URL(map.get(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection();
       
        // Set the required headers and connect
        urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + map.get(ACCESS_TOKEN_PROPERTY));
        urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
        urlConnection.setRequestMethod(METHOD_NAME);
        
        urlConnection.connect();
        // Read the response
        response = new BufferedReader(new InputStreamReader(urlConnection.getInputStream())).lines().collect(Collectors.joining());
        
        // TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON package to
        // handle the response appropriately
        System.out.println(response);
    }
}

    public void getSuppressedContact(String suppressedContactId) throws IOException 
    {
        // Open a connection to the inContact Token Authorization Service
        URLConnection  tokenService = new URL(TOKEN_SERVICE_URL).openConnection();
        
        // Set required headers
        tokenService.setRequestProperty(AUTHORIZATION_HEADER, TOKEN_AUTHORIZATION_HEADER_VALUE);
        tokenService.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
        
        // Post the token request to the connection stream
        tokenService.setDoOutput(true);        
        tokenService.getOutputStream().write(TOKEN_REQUEST_BODY.getBytes());

        // Read the response
        String  response = new BufferedReader(new InputStreamReader(tokenService.getInputStream())).lines().collect(Collectors.joining());

        // TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON package to
        // handle the response appropriately
        Map<String, String> map = Pattern.compile(",").splitAsStream(response.replaceAll("\"|\\{|\\}", "")).map(nvp->nvp.split(":", 2)).collect(Collectors.toMap(nvp->nvp[0], nvp->nvp[1]));

        // Create a connection to the API using the appropriate base uri
        URLConnection  urlConnection = new URL(map.get(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection();
       
        // Set the required headers and connect
        urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + map.get(ACCESS_TOKEN_PROPERTY));
        urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
        
        urlConnection.connect();

        // Read the response
        response = new BufferedReader(new InputStreamReader(urlConnection.getInputStream())).lines().collect(Collectors.joining());
        
        // TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON package to
        // handle the response appropriately
        System.out.println(response);
    }
}

public void putSuppressedContactId(int suppressedContactId,String param) throws IOException
	{
	final String API_PATH = "/suppressed-contact/" + suppressedContactId;
	final String API_URL = "services/{version}" + API_PATH;
	if (ACCESS_TOKEN_PROPERTY!= null) {
		HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL((RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
		urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
		urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.setDoInput(true);
		urlConnection.setDoOutput(true);
		urlConnection.setRequestMethod("PUT");
		urlConnection.connect();
		int responseCode = urlConnection.getResponseCode();
		String responseLine = urlConnection.getResponseMessage();
		//Print Response
	}
	else
	{
		// No token - get one or handle error
	}
	}

			construct("DataExtraction", "Extracting Data", "POST /jobs/jobsid", CLUSTER + "- v1", responseCode,responseLine);
		} else {
			// No token - get one or handle error
		}
	}
	
	public void GetBusinessunitTimezone() throws IOException 
	{
	final String API_PATH = "/business-unit/time-zones";
	final String API_URL = "services/v17" + API_PATH;
	if (ACCESS_TOKEN_PROPERTY!= null) 
	{
		HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL((RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
		urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
		urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.connect();
		int responseCode = urlConnection.getResponseCode();
		String responseLine = urlConnection.getResponseMessage();
		construct("Admin", "General", "Get /business-unit/time-zones", CLUSTER + "- v17", responseCode,responseLine);
		//Print Response
	} 
	else 
		{
		// No token - get one or handle error
		}
	}
	
	public void GetSuppresedContacts() throws IOException 
	{
	final String API_PATH = "/suppressed-contact";
	final String API_URL = "services/v17" + API_PATH;
	if (ACCESS_TOKEN_PROPERTY!= null) 
	{
		HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL((RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
		urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
		urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.connect();
		int responseCode = urlConnection.getResponseCode();
		String responseLine = urlConnection.getResponseMessage();
		construct("Admin", "General", "Get /suppressed-contact", CLUSTER + "- v17", responseCode,responseLine);
		//Print Response
	} 
	else 
		{
		// No token - get one or handle error
		}
	}
	
	 public void PostUnavailableCodes(String param) throws IOException
	{
	final String API_PATH = "/suppressed-contact";
	final String API_URL = "services/v17" + API_PATH;
	if (ACCESS_TOKEN_PROPERTY!= null) {
		HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL((RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
		urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
		urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.setDoInput(true);
		urlConnection.setDoOutput(true);
		urlConnection.setRequestMethod("POST");
		urlConnection.connect();
		OutputStream os = urlConnection.getOutputStream();
		os.write(param.getBytes("UTF-8"));
		int responseCode = urlConnection.getResponseCode();
		String responseLine = urlConnection.getResponseMessage();
		construct("Admin", "General", "Post /suppressed-contact", CLUSTER + "- v17", responseCode,responseLine);
		//Print Response
	}
	else
	{
		// No token - get one or handle error
	}
	}
	
	public void Putbusinessunittimezone(String param) throws IOException {
	final String API_PATH = "/business-unit/time-zones" ;
	final String API_URL = "services/v17.0" + API_PATH;
	if (ACCESS_TOKEN_PROPERTY != null) {
		HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
		(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
		urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
		urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.setDoInput(true);
		urlConnection.setDoOutput(true);
		urlConnection.setRequestMethod("PUT");
		urlConnection.connect();
		int responseCode = urlConnection.getResponseCode();
		String responseLine = urlConnection.getResponseMessage();
		construct("Admin", "General", "Put "/business-unit/time-zones", CLUSTER + "- v17", responseCode,responseLine);
	}
    else {
			// No token - get one or handle error
		}
	}
	
	public void GetJobsByID(String jobsID) throws IOException {

		final String API_PATH = "data-extraction/v1/jobs/"+ jobsID;
		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(CXONE_RESOURCE_SERVER_BASE_URI_PROPERTY) + API_PATH).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.connect();
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("DataExtraction", "Extracting Data", "POST /jobs/jobsid", CLUSTER + "- v1", responseCode,responseLine);
		} else {
			// No token - get one or handle error
		}
	}
	
	public void GetBusinessunitTimezone() throws IOException 
	{
	final String API_PATH = "/business-unit/time-zones";
	final String API_URL = "services/v17" + API_PATH;
	if (ACCESS_TOKEN_PROPERTY!= null) 
	{
		HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL((RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
		urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
		urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.connect();
		int responseCode = urlConnection.getResponseCode();
		String responseLine = urlConnection.getResponseMessage();
		construct("Admin", "General", "Get /business-unit/time-zones", CLUSTER + "- v17", responseCode,responseLine);
		//Print Response
	} 
	else 
		{
		// No token - get one or handle error
		}
	}
	
	public void GetSuppresedContacts() throws IOException 
	{
	final String API_PATH = "/suppressed-contact";
	final String API_URL = "services/v17" + API_PATH;
	if (ACCESS_TOKEN_PROPERTY!= null) 
	{
		HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL((RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
		urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
		urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.connect();
		int responseCode = urlConnection.getResponseCode();
		String responseLine = urlConnection.getResponseMessage();
		construct("Admin", "General", "Get /suppressed-contact", CLUSTER + "- v17", responseCode,responseLine);
		//Print Response
	} 
	else 
		{
		// No token - get one or handle error
		}
	}
	
	 public void PostUnavailableCodes(String param) throws IOException
	{
	final String API_PATH = "/suppressed-contact";
	final String API_URL = "services/v17" + API_PATH;
	if (ACCESS_TOKEN_PROPERTY!= null) {
		HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL((RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
		urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
		urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.setDoInput(true);
		urlConnection.setDoOutput(true);
		urlConnection.setRequestMethod("POST");
		urlConnection.connect();
		OutputStream os = urlConnection.getOutputStream();
		os.write(param.getBytes("UTF-8"));
		int responseCode = urlConnection.getResponseCode();
		String responseLine = urlConnection.getResponseMessage();
		construct("Admin", "General", "Post /suppressed-contact", CLUSTER + "- v17", responseCode,responseLine);
		//Print Response
	}
	else
	{
		// No token - get one or handle error
	}
	}
	
	public void Putbusinessunittimezone(String param) throws IOException {
	final String API_PATH = "/business-unit/time-zones" ;
	final String API_URL = "services/v17.0" + API_PATH;
	if (ACCESS_TOKEN_PROPERTY != null) {
		HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
		(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
		urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
		urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.setDoInput(true);
		urlConnection.setDoOutput(true);
		urlConnection.setRequestMethod("PUT");
		urlConnection.connect();
		int responseCode = urlConnection.getResponseCode();
		String responseLine = urlConnection.getResponseMessage();
		construct("Admin", "General", "Put /business-unit/time-zones", CLUSTER + "- v17", responseCode,responseLine);
	}
    else {
			// No token - get one or handle error
		}
	}
	
	public void GetTiemZoneBySkillId(String skillId) throws IOException 
	{
	final String API_PATH = "/skills/" +skillId + "/parameters/time-zones" ;
	final String API_URL = "services/v17" + API_PATH;
	if (ACCESS_TOKEN_PROPERTY!= null) 
	{
		HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL((RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
		urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
		urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.connect();
		int responseCode = urlConnection.getResponseCode();
		String responseLine = urlConnection.getResponseMessage();
		//Print 
		construct("Admin", "Skills", "Get /skills/skillId/parameters/time-zones", CLUSTER + "- v17", responseCode,responseLine);
	} 
	else 
		{
		// No token - get one or handle error
		}
	}
	
	public void PutTiemZoneBySkillId(String skillId) throws IOException {
	final String API_PATH = "/skills/" +skillId + "/parameters/time-zones" ;
	final String API_URL = "services/v17" + API_PATH;
	if (ACCESS_TOKEN_PROPERTY != null) {
		HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
		(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
		urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
		urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.setDoInput(true);
		urlConnection.setDoOutput(true);
		urlConnection.setRequestMethod("PUT");
		urlConnection.connect();
		int responseCode = urlConnection.getResponseCode();
		String responseLine = urlConnection.getResponseMessage();
		construct("Admin", "Skills", "Put /skills/skillId/parameters/time-zones", CLUSTER + "- v17", responseCode,responseLine);
	}
    else {
			// No token - get one or handle error
		}
	}
	
	 public void DeleteskillsbyskillIdagentId(String agentId,String skillId) throws IOException 
	{
	final String API_PATH = "/skills/" + skillId +"/agents/" + agentId;
	final String API_URL = "services/{version}" + API_PATH;
	if (ACCESS_TOKEN_PROPERTY!= null) 
	{
		HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL((RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
		urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
		urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.setDoInput(true);
		urlConnection.setDoOutput(true);
		urlConnection.setRequestMethod("DELETE");
		urlConnection.connect();
		int responseCode = urlConnection.getResponseCode();
		String responseLine = urlConnection.getResponseMessage();
		//Print Response
		construct("Admin", "Skills", "Delete /skills/{skillId}/agents/{agentId}", CLUSTER + "- v17", responseCode,responseLine);
	}
	else 
	{
		// No token - get one or handle error
	}
	}
	public void getScripts() throws IOException
    {
        // Open a connection to the inContact Token Authorization Service
        URLConnection  tokenService = new URL(TOKEN_SERVICE_URL).openConnection();
        
        // Set required headers
        tokenService.setRequestProperty(AUTHORIZATION_HEADER, TOKEN_AUTHORIZATION_HEADER_VALUE);
        tokenService.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
        
        // Post the token request to the connection stream
        tokenService.setDoOutput(true);        
        tokenService.getOutputStream().write(TOKEN_REQUEST_BODY.getBytes());

        // Read the response
        String  response = new BufferedReader(new InputStreamReader(tokenService.getInputStream())).lines().collect(Collectors.joining());

        // TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON package to
        // handle the response appropriately
        Map<String, String> map = Pattern.compile(",").splitAsStream(response.replaceAll("\"|\\{|\\}", "")).map(nvp->nvp.split(":", 2)).collect(Collectors.toMap(nvp->nvp[0], nvp->nvp[1]));

        // Create a connection to the API using the appropriate base uri
        URLConnection  urlConnection = new URL(map.get(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection();
       
        // Set the required headers and connect
        urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + map.get(ACCESS_TOKEN_PROPERTY));
        urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
        
        urlConnection.connect();

        // Read the response
        response = new BufferedReader(new InputStreamReader(urlConnection.getInputStream())).lines().collect(Collectors.joining());
        
        // TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON package to
        // handle the response appropriately
        System.out.println(response);
    }
	public void postScript() throws IOException
    {
        // Open a connection to the inContact Token Authorization Service
        URLConnection  tokenService = new URL(TOKEN_SERVICE_URL).openConnection();
        
        // Set required headers
        tokenService.setRequestProperty(AUTHORIZATION_HEADER, TOKEN_AUTHORIZATION_HEADER_VALUE);
        tokenService.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
        
        // Post the token request to the connection stream
        tokenService.setDoOutput(true);        
        tokenService.getOutputStream().write(TOKEN_REQUEST_BODY.getBytes());

        // Read the response
        String  response = new BufferedReader(new InputStreamReader(tokenService.getInputStream())).lines().collect(Collectors.joining());

        // TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON package to
        // handle the response appropriately
        Map<String, String> map = Pattern.compile(",").splitAsStream(response.replaceAll("\"|\\{|\\}", "")).map(nvp->nvp.split(":", 2)).collect(Collectors.toMap(nvp->nvp[0], nvp->nvp[1]));

        // Create a connection to the API using the appropriate base uri
        URLConnection  urlConnection = new URL(map.get(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection();
       
        // Set the required headers and connect
        urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + map.get(ACCESS_TOKEN_PROPERTY));
        urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
                
        // Post an empty request to the connection stream (forces a POST)
        urlConnection.setDoOutput(true);        
        urlConnection.getOutputStream().write(0);

        // Read the response
        response = new BufferedReader(new InputStreamReader(urlConnection.getInputStream())).lines().collect(Collectors.joining());
        
        // TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON package to
        // handle the response appropriately
        System.out.println(response);
    }
	 public void updateScripts() throws IOException
    {
        // Open a connection to the inContact Token Authorization Service
        URLConnection  tokenService = new URL(TOKEN_SERVICE_URL).openConnection();
        
        // Set required headers
        tokenService.setRequestProperty(AUTHORIZATION_HEADER, TOKEN_AUTHORIZATION_HEADER_VALUE);
        tokenService.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
        
        // Post the token request to the connection stream
        tokenService.setDoOutput(true);        
        tokenService.getOutputStream().write((TOKEN_REQUEST_BODY).getBytes());

        // Read the response
        String  response = new BufferedReader(new InputStreamReader(tokenService.getInputStream())).lines().collect(Collectors.joining());

        // TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON package to
        // handle the response appropriately
        Map<String, String> map = Pattern.compile(",").splitAsStream(response.replaceAll("\"|\\{|\\}", "")).map(nvp->nvp.split(":", 2)).collect(Collectors.toMap(nvp->nvp[0], nvp->nvp[1]));

        // Create a connection to the API using the appropriate base uri
        HttpURLConnection   urlConnection = (HttpURLConnection)new URL(map.get(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection();
       
        // Set the required headers and connect
        urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + map.get(ACCESS_TOKEN_PROPERTY));
        urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
        urlConnection.setRequestMethod(METHOD_NAME);
        
        // Post an empty request to the connection stream (forces a PUT)
        urlConnection.setDoOutput(true);        
        urlConnection.getOutputStream().write(0);

        // Read the response
        response = new BufferedReader(new InputStreamReader(urlConnection.getInputStream())).lines().collect(Collectors.joining());
        
        // TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON package to
        // handle the response appropriately
        System.out.println(response);
    }
	public void PostScriptKick() throws IOException
    {
        // Open a connection to the inContact Token Authorization Service
        URLConnection  tokenService = new URL(TOKEN_SERVICE_URL).openConnection();
        
        // Set required headers
        tokenService.setRequestProperty(AUTHORIZATION_HEADER, TOKEN_AUTHORIZATION_HEADER_VALUE);
        tokenService.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
        
        // Post the token request to the connection stream
        tokenService.setDoOutput(true);        
        tokenService.getOutputStream().write(TOKEN_REQUEST_BODY.getBytes());

        // Read the response
        String  response = new BufferedReader(new InputStreamReader(tokenService.getInputStream())).lines().collect(Collectors.joining());

        // TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON package to
        // handle the response appropriately
        Map<String, String> map = Pattern.compile(",").splitAsStream(response.replaceAll("\"|\\{|\\}", "")).map(nvp->nvp.split(":", 2)).collect(Collectors.toMap(nvp->nvp[0], nvp->nvp[1]));

        // Create a connection to the API using the appropriate base uri
        URLConnection  urlConnection = new URL(map.get(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection();
       
        // Set the required headers and connect
        urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + map.get(ACCESS_TOKEN_PROPERTY));
        urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
                
        // Post an empty request to the connection stream (forces a POST)
        urlConnection.setDoOutput(true);        
        urlConnection.getOutputStream().write(0);

        // Read the response
        response = new BufferedReader(new InputStreamReader(urlConnection.getInputStream())).lines().collect(Collectors.joining());
        
        // TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON package to
        // handle the response appropriately
        System.out.println(response);
    }
	public void getScriptsHistoryByName() throws IOException
    {
        // Open a connection to the inContact Token Authorization Service
        URLConnection  tokenService = new URL(TOKEN_SERVICE_URL).openConnection();
        
        // Set required headers
        tokenService.setRequestProperty(AUTHORIZATION_HEADER, TOKEN_AUTHORIZATION_HEADER_VALUE);
        tokenService.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
        
        // Post the token request to the connection stream
        tokenService.setDoOutput(true);        
        tokenService.getOutputStream().write(TOKEN_REQUEST_BODY.getBytes());

        // Read the response
        String  response = new BufferedReader(new InputStreamReader(tokenService.getInputStream())).lines().collect(Collectors.joining());

        // TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON package to
        // handle the response appropriately
        Map<String, String> map = Pattern.compile(",").splitAsStream(response.replaceAll("\"|\\{|\\}", "")).map(nvp->nvp.split(":", 2)).collect(Collectors.toMap(nvp->nvp[0], nvp->nvp[1]));

        // Create a connection to the API using the appropriate base uri
        URLConnection  urlConnection = new URL(map.get(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection();
       
        // Set the required headers and connect
        urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + map.get(ACCESS_TOKEN_PROPERTY));
        urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
        
        urlConnection.connect();

        // Read the response
        response = new BufferedReader(new InputStreamReader(urlConnection.getInputStream())).lines().collect(Collectors.joining());
        
        // TODO: the response comes back as a JSON document. Replace the following code with your preferred JSON package to
        // handle the response appropriately
        System.out.println(response);
    }
	public void PutListManagement(String skillId, String param) throws IOException {

		final String API_PATH = "/skills/".concat(skillId) + "/parameters/list-management";
		final String API_URL = "services/v18.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("PUT");
			urlConnection.connect();
			OutputStream os = urlConnection.getOutputStream();
			os.write(param.getBytes("UTF-8"));
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Skills", "PUT /skills/{skillId}/parameters/list-management", CLUSTER + "- v18",
					responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}
	public void GetSkillsParameters() throws IOException {
	final String API_PATH = "/skills/parameters";
	final String API_URL = "services/v18" + API_PATH;
	if (ACCESS_TOKEN_PROPERTY!= null) 
	{
		HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL((RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
		urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
		urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.connect();
		int responseCode = urlConnection.getResponseCode();
		String responseLine = urlConnection.getResponseMessage();
		construct("Admin", "General", "Get /skills/parameters", CLUSTER + "- v18", responseCode,responseLine);
		//Print Response
	} 
	else 
		{
		// No token - get one or handle error
		}
	}
	public void PutCadenceSettings(String skillId, String param) throws IOException {

		final String API_PATH = "/skills/".concat(skillId) + "/parameters/cadence-settings";
		final String API_URL = "services/v18.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("PUT");
			urlConnection.connect();
			OutputStream os = urlConnection.getOutputStream();
			os.write(param.getBytes("UTF-8"));
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Skills", "PUT /skills/{skillId}/parameters/cadence-settings", CLUSTER + "- v18",
					responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}	
	public void GetSkillsSkillIdParameters(String skillId) throws IOException {
	final String API_PATH = "/skills/".concat(skillId)+"/parameters";
	final String API_URL = "services/v18" + API_PATH;
	if (ACCESS_TOKEN_PROPERTY!= null) 
	{
		HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL((RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
		urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
		urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.connect();
		int responseCode = urlConnection.getResponseCode();
		String responseLine = urlConnection.getResponseMessage();
		construct("Admin", "General", "Get /skills/skillid/parameters", CLUSTER + "- v18", responseCode,responseLine);
		//Print Response
	} 
	else 
		{
		// No token - get one or handle error
		}
	}
	public void PostSetDisposition(String param, int contactId) throws IOException {

		final String API_PATH = "/contacts/" + contactId + "/set-disposition";
		final String API_URL = "services/v18.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("POST");
			urlConnection.connect();
			OutputStream os = urlConnection.getOutputStream();
			os.write(param.getBytes("UTF-8"));
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Skills", "POST /contacts/{contactId}/set-disposition", CLUSTER + "- v18", responseCode,
					responseLine);
		} else {
			// No token - get one or handle error
		}
	}
	public void GetBusinessunitOutboundRoutes() throws IOException {
	final String API_PATH = "/business-unit/outbound-routes";
	final String API_URL = "services/v18" + API_PATH;
	if (ACCESS_TOKEN_PROPERTY!= null) 
	{
		HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL((RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
		urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
		urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.connect();
		int responseCode = urlConnection.getResponseCode();
		String responseLine = urlConnection.getResponseMessage();
		construct("Admin", "General", "Get /business-unit/outbound-routes", CLUSTER + "- v18", responseCode,responseLine);
		//Print Response
	} 
	else 
		{
		// No token - get one or handle error
		}
	}
	
	//v19
	
	public void getclientdata() throws IOException {
	final String API_PATH = "/agents/client-data";
	final String API_URL = "services/v19" + API_PATH;
	if (ACCESS_TOKEN_PROPERTY!= null) 
	{
		HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL((RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
		urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
		urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.connect();
		int responseCode = urlConnection.getResponseCode();
		String responseLine = urlConnection.getResponseMessage();
		construct("Realtime", "Realtime", "Get /agents/client-data", CLUSTER + "- v19", responseCode,responseLine);
		//Print Response
	} 
	else 
		{
		// No token - get one or handle error
		}
	}
	public void putclientdata() throws IOException {

		final String API_PATH = "/agents/client-data";
		final String API_URL = "services/v19.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("PUT");
			urlConnection.connect();
			OutputStream os = urlConnection.getOutputStream();
			os.write(param.getBytes("UTF-8"));
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Skills", "PUT /agents/client-data", CLUSTER + "- v19",
					responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}	
	public void smsoutbound(String sessionId) throws IOException {

		final String API_PATH = "/agent-sessions/" + sessionId + "/interactions/sms-outbound";
		final String API_URL = "services/v19.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("POST");
			urlConnection.connect();
			OutputStream os = urlConnection.getOutputStream();
			os.write(param.getBytes("UTF-8"));
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Skills", "POST /agent-sessions/{sessionId}/interactions/sms-outbound", CLUSTER + "- v19", responseCode,
					responseLine);
		} else {
			// No token - get one or handle error
		}
	}
	public void removeprospects(String sourceName) throws IOException {
	final String API_PATH = "/lists/call-lists/" + sourceName + "/removeProspects";
	final String API_URL = "services/{version}" + API_PATH;
	if (ACCESS_TOKEN_PROPERTY!= null) 
	{
		HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL((RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
		urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
		urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.setDoInput(true);
		urlConnection.setDoOutput(true);
		urlConnection.setRequestMethod("DELETE");
		urlConnection.connect();
		int responseCode = urlConnection.getResponseCode();
		String responseLine = urlConnection.getResponseMessage();
		//Print Response
	construct("Admin", "Skills", "Delete /lists/call-lists/{sourceName}/removeProspects", CLUSTER + "- v19", responseCode,responseLine);
	}
	else 
	{
		// No token - get one or handle error
	}
	}
	public void persistentcontacts(int contactId) throws IOException {

		final String API_PATH = "/persistent-contacts/"+ contactId;
		final String API_URL = "services/v19.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("PUT");
			urlConnection.connect();
			OutputStream os = urlConnection.getOutputStream();
			os.write(param.getBytes("UTF-8"));
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Contacts", "PUT /persistent-contacts/{contactId}", CLUSTER + "- v19",
					responseCode, responseLine);
		} else {
			// No token - get one or handle error
		}
	}
	public void agentsettings(int agentId) throws IOException {
	final String API_PATH = "/agents/" + agentId + "/agent-settings";
	final String API_URL = "services/v19" + API_PATH;
	if (ACCESS_TOKEN_PROPERTY!= null) 
	{
		HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL((RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
		urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
		urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.connect();
		int responseCode = urlConnection.getResponseCode();
		String responseLine = urlConnection.getResponseMessage();
		construct("Admin", "Agents", "Get /agents/{agentId}/agent-settings", CLUSTER + "- v19", responseCode,responseLine);
		//Print Response
	} 
	else 
		{
		// No token - get one or handle error
		}
	}
	public void getscriptsbyId(int scriptId) throws IOException {
	final String API_PATH = "/scripts/" + scriptId;
	final String API_URL = "services/v19" + API_PATH;
	if (ACCESS_TOKEN_PROPERTY!= null) 
	{
		HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL((RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
		urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
		urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.connect();
		int responseCode = urlConnection.getResponseCode();
		String responseLine = urlConnection.getResponseMessage();
		construct("Admin", "Agents", "Get /scripts/{scriptId}", CLUSTER + "- v19", responseCode,responseLine);
		//Print Response
	} 
	else 
		{
		// No token - get one or handle error
		}
	}
	public void scriptssearch() throws IOException {
	final String API_PATH = "/scripts/search";
	final String API_URL = "services/v19" + API_PATH;
	if (ACCESS_TOKEN_PROPERTY!= null) 
	{
		HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL((RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
		urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
		urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.connect();
		int responseCode = urlConnection.getResponseCode();
		String responseLine = urlConnection.getResponseMessage();
		construct("Admin", "Agents", "Get /scripts/search", CLUSTER + "- v19", responseCode,responseLine);
		//Print Response
	} 
	else 
		{
		// No token - get one or handle error
		}
	}
	public void deletescripts() throws IOException {
	final String API_PATH = "/scripts";
	final String API_URL = "services/{version}" + API_PATH;
	if (ACCESS_TOKEN_PROPERTY!= null) 
	{
		HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL((RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
		urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
		urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
		urlConnection.setDoInput(true);
		urlConnection.setDoOutput(true);
		urlConnection.setRequestMethod("DELETE");
		urlConnection.connect();
		int responseCode = urlConnection.getResponseCode();
		String responseLine = urlConnection.getResponseMessage();
		//Print Response
	construct("Admin", "Skills", "Delete /scripts", CLUSTER + "- v19", responseCode,responseLine);
	}
	else 
	{
		// No token - get one or handle error
	}
	}
	public void transformnumbers(int agentpatternId) throws IOException {

		final String API_PATH = "/agent-patterns/" + agentpatternId + "/transform-phonenumbers";
		final String API_URL = "services/v19.0" + API_PATH;

		if (ACCESS_TOKEN_PROPERTY != null) {

			HttpsURLConnection urlConnection = (HttpsURLConnection) (new URL(
					(RESOURCE_SERVER_BASE_URI_PROPERTY) + API_URL).openConnection());
			urlConnection.setRequestProperty(AUTHORIZATION_HEADER, AUTHORIZATION_BEARER_PREFIX + ACCESS_TOKEN_PROPERTY);
			urlConnection.setRequestProperty(ACCEPT_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setRequestProperty(CONTENT_TYPE_HEADER, APPLICATION_JSON_VALUE);
			urlConnection.setDoInput(true);
			urlConnection.setDoOutput(true);
			urlConnection.setRequestMethod("POST");
			urlConnection.connect();
			OutputStream os = urlConnection.getOutputStream();
			os.write(param.getBytes("UTF-8"));
			int responseCode = urlConnection.getResponseCode();
			String responseLine = urlConnection.getResponseMessage();

			construct("Admin", "Agents", "POST /agent-patterns/{agentpatternId}/transform-phonenumbers", CLUSTER + "- v19", responseCode,
					responseLine);
		} else {
			// No token - get one or handle error
		}
	}
}







