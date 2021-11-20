var inContact = inContact || {};

inContact.Swagger =
    {
	    apiResourceListingUrl: null,
	    APIResourceBaseurl: null,
	    tryItOutUrl: null,
	    codeExampleUrl: null,
	    getAccessTokenUrl: null,
	    throbberImageUrl: null,
	    disableTryItOut: false,
	    authenticated: false,
	    authToken: null,
	    apiHost: "ACD",
	    init: function (apiResourceListingUrl, codeExampleUrl, getAccessTokenUrl, throbberImageUrl, disableTryItOut, apiHost) {

	        $("body").on("click", "div.opblock-summary", function (e) {
	            setTimeout(function () { inContact.Swagger.customMethod(); }, 100);
	        });

	        $("body").on("click", "h4.opblock-tag", function (e) {
	            var _e = e;
	            if (e.currentTarget.attributes["data-is-open"].value == "false") {
	                setTimeout(function () { inContact.Swagger.replaceStringBySection(_e, true); }, 10);
	            }
	        });

	        //remember the apiResourceListingUrl	       
	        this.apiResourceListingUrl = apiResourceListingUrl;
	        this.codeExampleUrl = codeExampleUrl;
	        this.getAccessTokenUrl = getAccessTokenUrl;
	        this.throbberImageUrl = throbberImageUrl;
	        this.disableTryItOut = disableTryItOut !== undefined && disableTryItOut;
	        if (apiHost) {
	            this.apiHost = apiHost;
	        }
	        this.show();
	    },


	    show: function () {
	        try {
                //Load swagger
	            window.swaggerUi = SwaggerUIBundle({
	                url: this.apiResourceListingUrl,
	                validatorUrl: null,
	                dom_id: "#swagger-ui-container",
	                deepLinking: true,
	                configs: {
	                    preFetch: function (req) {
	                        var _url = null;
	                        if (inContact.Swagger.authToken) {
	                            _url = req.url;
	                            req.headers["Authorization"] = inContact.Swagger.authToken;
	                            req.url = _url.replace(/(https:|http:)(^|\/\/)(.*?\/)/g, inContact.Swagger.tryItOutUrl);

	                        }
	                        return req;
	                    }
	                },
	                docExpansion: "none",
                    onComplete: function (e) {
                        var currentLocation = window.location.href.split("#/");
	                    if (currentLocation.length > 1) {
	                        var path = currentLocation[1].split("/");
	                        var element = $("h3#operations-tag-" + path[0]);//tagName = path[0]
                            setTimeout(function () { inContact.Swagger.replaceStringBySection(element); }, 10);
	                    }
	                }
	            });
	        }
	        catch (ex) {
	            this.logMessage("Could not init swagger: " + ex);
	        }
	    },
    replaceString: function (element) {
	        if ($(element).html() != "") {
	            $(element).html($(element).html().replace("%UPDATED_IN_V22%", "<b><i><span style='color:red'>(Updated in v22.0)</span></i></b>"));
	            $(element).html($(element).html().replace("%NEW_IN_V22%", "<b><i><span style='color:red'>(New in v22.0)</span></i></b>"));
	        }
	    },
	    replaceStringBySection: function (e, isCurrentTargetAvailable) {
            var selector = isCurrentTargetAvailable ? e.currentTarget.id : e[0].id;
            var descriptionList = $("#" + selector).siblings().find('div.opblock-summary-description');
	        if (descriptionList.length == 0) {
	            setTimeout(function () { inContact.Swagger.replaceStringBySection(e); }, 10);
	            return 0;
	        }
            descriptionList.each(function (i, el) {
	            inContact.Swagger.replaceString(el);
	            if (Array.from($(el).parents(':eq(2)')[0].classList).includes("is-open")) {
	                //it will creates sample codes for opened APIs if we open API section(e.g callback)
	                inContact.Swagger.customMethod();
	            }
	        });
	    },
	    customMethod: function () {
	        inContact.Swagger.getApiCredentials();

	        //Display the code sample tabs
	        inContact.Swagger.renderCodeSamples();

	        //$(".responses-inner").append("span class='response_throbber'");

	        //Fix the "throbber" image relative path
	        $(".response_throbber").attr("src", inContact.Swagger.throbberImageUrl);

	        //Style the Try It Out! buttons look like inContact buttons
	        $(".try-out__btn").addClass('incontact-button');

	        if (inContact.Swagger.disableTryItOut) {
	            $(".try-out__btn").hide();
	            $("<h4>Try it out will be enabled after release!</h4>").insertAfter(".try-out__btn");
	        }

            if (!inContact.Swagger.authenticated) {
	            $(".try-out__btn").html("Sign In to Try It Out");
	            $(".try-out__btn").click(function (e) {
	                window.location = '/Home/Login?returnUrl=' + encodeURIComponent(window.location.pathname + window.location.hash);
	                return false;
	            });
	        }
	    },
	    expandBlocks: function () {
	        var styles = [".content", "ul.endpoints"]
	        $.each(styles, function (i, style) {
	            var divs = $(style);
	            $.each(divs, function (i, div) {
	                $(this).removeAttr('style');
	                $(this).css({
	                    overflow: 'hidden',
	                    overflowX: 'hidden'
	                })
	            });
	        });

	        $(".snippet").hide();
	        $(".description").show();

	    },
	    ///Injects tabs for code samples inside the notes areas of each API described by swagger
	    renderCodeSamples: function () {
	        try {
	            //load the code example tabs into each method
	            var $codeSampleTabs = $("div .codeSampleTabs");

	            var $codeSampleWrapperList = $("div .tabs_wrapper");
	            $codeSampleWrapperList.html($codeSampleTabs.html()).show();

	            //Gather code samples for each method
	            $codeSampleWrapperList.each(function () {
	                var $codeSampleWrapper = $(this);

	                var codeSamplePath = $codeSampleWrapper.attr("data-path");

	                var jsCodeSampleUrl = inContact.Swagger.codeExampleUrl + "/js/js_" + codeSamplePath;
	                var cCodeSampleUrl = inContact.Swagger.codeExampleUrl + "/c/cs_" + codeSamplePath;
	                var rubyCodeSampleUrl = inContact.Swagger.codeExampleUrl + "/ruby/rb_" + codeSamplePath;
	                var phpCodeSampleUrl = inContact.Swagger.codeExampleUrl + "/php/php_" + codeSamplePath;
	                var javaCodeSampleUrl = inContact.Swagger.codeExampleUrl + "/java/java_" + codeSamplePath;
	                var scalaCodeSampleUrl = inContact.Swagger.codeExampleUrl + "/scala/sc_" + codeSamplePath;
	                var pythonCodeSampleUrl = inContact.Swagger.codeExampleUrl + "/python/python_" + codeSamplePath;

	                var tabs = $codeSampleWrapper.kendoTabStrip({
	                    contentUrls: [
							jsCodeSampleUrl,
							cCodeSampleUrl,
							rubyCodeSampleUrl,
							phpCodeSampleUrl,
							javaCodeSampleUrl,
                            scalaCodeSampleUrl,
                            pythonCodeSampleUrl
	                    ],
	                    collapsible: true,
	                    contentLoad: prettyPrint //prettyPrint() comes from prettify.js
	                });
	            });


	        }
	        catch (ex) {
	            inContact.Swagger.logMessage("Could not render code samples: " + ex);
	        }
	    },
	    getApiCredentials: function () {
	        try {

	            var response = null;
	            var $request = $.ajax
					({
					    url: this.getAccessTokenUrl,
					    type: "POST",
					    data: { apiHost: inContact.Swagger.apiHost },
					    async: false
					});

	            $request.done(function (data) {
	                if (data.Authenticated == true) {
	                    inContact.Swagger.authenticated = true;
	                    //Set the header to supply when using the Try It Out! feature
	                    inContact.Swagger.authToken = data.TokenType + " " + data.AccessToken;
	                    //Set up the url to use when exercising the API	                    
	                    inContact.Swagger.APIResourceBaseurl = data.ResourceServerBaseUri;
	                    inContact.Swagger.tryItOutUrl = data.ResourceServerBaseUri;
	                }
	                else {
	                    inContact.Swagger.authenticated = false;
	                }
	            });

	            $request.fail(function (jqXHR, textStatus, errorThrown) {
	                inContact.Swagger.logMessage("Could not get access token: " + errorThrown)
	            });

	            return response;
	        }
	        catch (ex) {
	            this.logMessage("Could not set API credentials: " + ex);
	        }
	    },
	    logMessage: function (message) {
	        if (console && console.log)
	            console.log(message);
	    },


	};

