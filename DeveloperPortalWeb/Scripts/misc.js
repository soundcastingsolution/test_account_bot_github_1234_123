$(document).ready(function () {
	var format = getUrlParams()["format"];
	// if (window.location.pathname.toLowerCase().indexOf("documentation") < 0) {$('#downloadAsPdf').remove()}
	if (format == "PDF") {
		updateLinksForPdf();
		var styles = [".k-grid-content", ".prettyprint", ".k-grid"]
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
	}
});

function getUrlParams() {
	var params = [], hash;
	var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
	for (var i = 0; i < hashes.length; i++) {
		hash = hashes[i].split('=');
		params.push(hash[0]);
		params[hash[0]] = hash[1];
	}
	return params;
}

function updateLinksForPdf() {
	$("a").each(function () {
		if ((this.pathname.match("^/")) || (this.innerText != "")) {
			$(this).attr("href", this.href);
		}
	});
	$('#downloadAsPdf').remove();
}

$(document).ready(function () {
	$('.pop-over-menu-trigger').click(function () {
		$('.pop-over-menu').fadeIn(250);
		setTimeout(function () {
			if (typeof document.onmousedown !== 'function') {
				$(document).bind('mousedown', function () { $('.pop-over-menu').fadeOut(250); $(document).unbind('mousedown'); });
			}
		}, 100);
	});

	// In the sidebar, put the current page in bold.
	$("#menu-item-list a").each(function () {
		if ($(this).text() == document.title)
			$(this).css("font-weight", "bold");
	});

	// userIsAuthenticated is a dependent var from razor generated in _header.html
	if (userIsAuthenticated) {
		//set the business unit using the cookie created server-side
		setBusinessUnit();
	}
	else {
		$('.secure-content-link').attr('title', 'Sign in to view more.')
	}

	$('nav.sub-menu').each(function (navIndex, navElement) {
		$('<div>')
			.attr('id', $(navElement).attr('id') + '-section-1')
			.attr('class', 'float-left')
			.width(275)
			.appendTo(navElement);

		if ($(navElement).children('.navigation-link').size() > 1) {
			$('<div>')
				.attr('id', $(navElement).attr('id') + '-section-2')
				.attr('class', 'float-left')
				.width(275)
				.appendTo(navElement);
		}

		if ($(navElement).children().size('.navigation-link') > 2) {
			$('<div>')
				.attr('id', $(navElement).attr('id') + '-section-3')
				.attr('class', 'float-left')
				.width(275)
				.appendTo(navElement);
		}

		var numLinksPerColumn = Math.ceil($(navElement).children('.navigation-link').size() / 3);
		var column = 1;
		$(navElement).children('.navigation-link').each(function (index, linkElement) {
			if (index >= (numLinksPerColumn * column)) {
				column++;
			}
			$(linkElement).appendTo('#' + $(navElement).attr('id') + '-section-' + column);
		});
	});
});

function toggleSection(currentElement) {
	$(currentElement).siblings().toggle();
	$(currentElement).children('b.float-right').text($(currentElement).children('b.float-right').text() === '-' ? '+' : '-');
}

function setBusinessUnit() {
	try {
		var devCookieString = getCookie("zDevToken");
		var divBusinessUnit = document.getElementById("divBusinessUnit");
		var divBusinessUnitResponsive = document.getElementById("divBusinessUnitResponsive");

		if (devCookieString && divBusinessUnit && divBusinessUnitResponsive) {
			var devCookie = $.parseJSON(devCookieString);
			divBusinessUnit.innerText += devCookie.BusinessUnitNumber;
			divBusinessUnitResponsive.innerText += devCookie.BusinessUnitNumber;
		}
	}
	catch (ex) {
		//Disregard error here. End result: No business unit displayed. Could redirect to login I guess.
	}
}

function getCookie(cookieName) {
	var returnValue = null;
	var all = document.cookie;  // Get all cookies in one big string
	if (all && all !== "") {
		var list = all.split("; "); // Split into individual name=value pairs
		for (var i = 0; i < list.length; i++) {  // For each cookie
			var cookie = list[i];
			var p = cookie.indexOf("=");        // Find the first = sign
			var name = cookie.substring(0, p);   // Get cookie name
			if (name === cookieName) {
				returnValue = cookie.substring(p + 1);  // Get cookie value
				returnValue = decodeURIComponent(returnValue);  // Decode the value
				break;
			}
		}
	}
	return returnValue;
}

function ActivateSubMenu(subMenuId) {
	if ($('#' + subMenuId + '-link').hasClass('on')) {
		$('.sub-menu-container').slideUp('fast', function () { $('.primary > a.on').removeClass('on'); $('nav.sub-menu').hide(); });
	}
	else {
		$('.sub-menu-container').slideDown();

		var openNav = $('nav.sub-menu:not(:hidden)');
		var navToOpen = $('#' + subMenuId + '-sub-menu');

		if (openNav.length > 0) {
			openNav.slideUp("fast", function () {
				$('.primary > a.on').removeClass('on');
				navToOpen.slideToggle('fast');
				$('#' + subMenuId + '-link').addClass('on');
			});
		}
		else {
			navToOpen.slideToggle('fast');
			$('#' + subMenuId + '-link').addClass('on');
		};
	}
}

//Google Tag Manager
(function (w, d, s, l, i) {
	try {
		w[l] = w[l] || [];
		w[l].push({ 'gtm.start': new Date().getTime(), event: 'gtm.js' });
		var f = d.getElementsByTagName(s)[0], j = d.createElement(s), dl = l != 'dataLayer' ? '&l=' + l : '';
		j.async = true; j.src = '//www.googletagmanager.com/gtm.js?id=' + i + dl;
		f.parentNode.insertBefore(j, f);
	}
	catch (ex) {
		//Disregard error here. End result: no google analytics
	}
})(window, document, 'script', 'dataLayer', 'GTM-PJC75W');
