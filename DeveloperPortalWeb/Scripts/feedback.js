window.hobofeedback = {};
hobofeedback.init = function(){
	if($ !== undefined){
		$(".feedback").click(function(){
			hobofeedback.show();
		});
	}
};
hobofeedback.show = function(){
	var width = 500;
	$("#feedback_form").remove();
	$("body").append("<div id=\"feedback_form\" style=\"background: white; display: none; width: " + width + "px; position: fixed; top: 30%; left: 50%; margin-top: -" + parseInt($("#feedback_form").height()/2) + "px; margin-left: -" + width/2 + "px; -webkit-box-shadow: 0px 0px 30px 0px rgba(50, 50, 50, 0.75); -moz-box-shadow: 0px 0px 30px 0px rgba(50, 50, 50, 0.75); box-shadow: 0px 0px 30px 0px rgba(50, 50, 50, 0.75);\"></div>");
	$("#feedback_form").append("<form action=\"/php/sndfdbck.php\" method=\"post\"><div style=\"background-color: #FA8C00; text-align: center; padding: 10px; font-weight: bold; color: white; font-size: 1.1em;\">Feedback</div><div style=\"background: #5b6c74; padding: 10px; color: white;\"><div><span>Username<font color=\"yellow\" size=\"4\">*</font>:</span>&nbsp;<input type=\"text\" id=\"user\" name=\"user\" size=\"35\" style=\"float: right;\"></div><div style=\"clear: both;\"></div><div><span>E-mail<font color=\"yellow\" size=\"4\">*</font>:</span>&nbsp;<input type=\"text\" id=\"mail\" name=\"mail\" size=\"35\" style=\"float: right;\"></div><div style=\"clear: both;\"></div><div><span>Send copy to:</span>&nbsp;<input type=\"text\" id=\"copy\" name=\"copy\" size=\"35\" style=\"float: right;\"></div><div style=\"clear: both;\"></div><div><span>Phone:</span>&nbsp;<input type=\"text\" size=\"35\" id=\"phone\" name=\"phone\" style=\"float: right;\"></div><div style=\"clear: both;\"></div><div><span>Subject<font color=\"yellow\" size=\"4\">*</font>:</span>&nbsp;<input type=\"text\" id=\"subject\" name=\"subject\"size=\"35\" style=\"float: right;\"></div><div style=\"clear: both;\"></div><br><div>Your Comment<font color=\"yellow\" size=\"4\">*</font>:</div><textarea id=\"message\" rows=\"10\" style=\"width: 99%; resize: none;\"></textarea><br><font color=\"yellow\" size=\"5\">*</font> - Required fields<br><br><div style=\"text-align: center;\"><button id=\"f_cancel\">Cancel</button>&emsp;<button id=\"f_submit\">Submit</button></div></div></form>");
	$("#feedback_form").slideDown("fast");
	$("#f_cancel").click(function(e){
		e.preventDefault();
		$("#feedback_form").fadeOut("fast", function(){
			$("#feedback_form").remove();
		});
		return false;
	});
	$("#f_submit").click(function(e){
		e.preventDefault();
		$("#feedback_form input").removeClass("error");
		$("#feedback_form textarea").removeClass("error");
		$("#feedback_form input").attr("placeholder", "");
		var valid = hobofeedback.validate();
		console.log(valid);
		if(valid)
			$("#feedback_form form").submit();
	});
};
hobofeedback.validate = function(){
	var user = $("#user").val().trim();
	var mail = $("#mail").val().trim();
	var copy = $("#copy").val().trim();
	var subject = $("#subject").val().trim();
	var phone = $("#phone").val().trim();
	var message = $("#message").val().trim();
	var mail_exp = new RegExp("[a-z0-9\._-]+@[a-z0-9]+\.[a-z]{2,4}", "i");
	var phone_exp = new RegExp("^([\+]?)[0-9]+$", "");
	var subject_exp = new RegExp("^[a-z0-9\.,\s\(\)_\-]+$", "i");
	var message_exp = new RegExp("^[a-z0-9\.:!@\$\/\\\*&\#,\s\(\)_\-]+$", "i");
	if((user === '') || (mail === '') || (subject === '') || (message === '')){
		alert("Fill in all required fields!");
		return false;
	}
	var ret = true;
	if(!mail_exp.test(user)){
		$("#user").addClass("error");
		$("#user").val("");
		$("#user").attr("placeholder", "Invalid username");
		ret = false;
	}
	if(!mail_exp.test(mail)){
		$("#mail").addClass("error");
		$("#mail").val("");
		$("#mail").attr("placeholder", "Invalid e-mail");
		ret = false;
	}
	if(copy !== ''){
		var mails = copy.split(",");
		mails.forEach(function(el){
			if(mail_exp.test(el.trim())){
				$("#copy").addClass("error");
				ret = false;
			}
		});
	}
	if(phone !== ''){
		if(!phone_exp.test(phone)){
			$("#phone").addClass("error");
			$("#phone").val("");
			$("#phone").attr("placeholder", "Contains invalid characters");
			ret = false;
		}
	}
	if(!subject_exp.test(subject)){
		$("#subject").addClass("error");
		$("#subject").val("");
		$("#subject").attr("placeholder", "Contains invalid characters");
		ret = false;
	}
	if(!message_exp.test(message)){
		$("#message").addClass("error");
		ret = false;
	}
	return ret;
};