﻿//---------------- On Ready ------------------------

$(function () {


    //$('a[href$=".gif"], a[href$=".jpg"], a[href$=".png"], a[href$=".bmp"], a[href$=".jpeg"]')

    ChangeLanguage();

    // Attach files click handler
    ShowFileUploadClickHandler();

    DisplayWaitForPostUploadClickHandler();

    // make code pretty
    window.prettyPrint && prettyPrint();

    $('input, textarea').placeholder();

    // Sort the date of the member
    SortWhosOnline();

    //---------------- On Click------------------------

    // We add the post click events like this, so we can reattach when we do the show more posts
    AddPostClickEvents();

    var mobilenavbutton = $('.showmobilenavbar');
    if (mobilenavbutton.length > 0) {
        mobilenavbutton.click(function (e) {
            e.preventDefault();
            $('.mobilenavbar-inner ul.nav').slideToggle();
        });
    }

    var topicName = $(".createtopicname");
    if (topicName.length > 0) {
        topicName.focusout(function () {
            var tbValue = $.trim(topicName.val());
            var length = tbValue.length;
            if (length > 5) {
                // Someone has entered some text more than 5 charactors and now clicked
                // out of the textbox, so search
                $.ajax({
                    url: app_base + 'Topic/GetSimilarTopics',
                    type: 'POST',
                    dataType: 'html',
                    data: { 'searchTerm': tbValue },
                    success: function (data) {
                        if (data != '') {
                            $('.relatedtopicskey').html(data);
                            $('.relatedtopicsholder').show();
                        }
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        ShowUserMessage("Error: " + xhr.status + " " + thrownError);
                    }
                });
            }
        });
    }

    $(".showmoreposts").click(function (e) {
        var topicId = $('#topicId').val();
        var pageIndex = $('#pageIndex');
        var totalPages = parseInt($('#totalPages').val());
        var activeText = $('span.smpactive');
        var loadingText = $('span.smploading');
        var showMoreLink = $(this);

        activeText.hide();
        loadingText.show();

        var getMorePostsViewModel = new Object();
        getMorePostsViewModel.TopicId = topicId;
        getMorePostsViewModel.PageIndex = pageIndex.val();
        getMorePostsViewModel.Order = $.QueryString["order"];

        // Ajax call to post the view model to the controller
        var strung = JSON.stringify(getMorePostsViewModel);

        $.ajax({
            url: app_base + 'Topic/AjaxMorePosts',
            type: 'POST',
            dataType: 'html',
            data: strung,
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // Now add the new posts
                AddNewPosts(showMoreLink, data);

                // Update the page index value
                var newPageIdex = (parseInt(pageIndex.val()) + parseInt(1));
                pageIndex.val(newPageIdex);

                // If the new pageindex is greater than the total pages, then hide the show more button
                if (newPageIdex > totalPages) {
                    showMoreLink.hide();
                }

                // Lastly reattch the click events
                AddPostClickEvents();
                activeText.show();
                loadingText.hide();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                ShowUserMessage("Error: " + xhr.status + " " + thrownError);
                activeText.show();
                loadingText.hide();
            }
        });
        e.preventDefault();
        e.stopImmediatePropagation();
    });

    $(".privatemessagedelete").click(function (e) {
        var linkClicked = $(this);
        var messageId = linkClicked.attr('rel');
        var deletePrivateMessageViewModel = new Object();
        deletePrivateMessageViewModel.Id = messageId;

        // Ajax call to post the view model to the controller
        var strung = JSON.stringify(deletePrivateMessageViewModel);

        $.ajax({
            url: app_base + 'PrivateMessage/Delete',
            type: 'POST',
            data: strung,
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // deleted, remove table row
                RemovePrivateMessageTableRow(linkClicked);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                ShowUserMessage("Error: " + xhr.status + " " + thrownError);
            }
        });
        e.preventDefault();
        e.stopImmediatePropagation();
    });

    $(".emailsubscription").click(function (e) {
        var entityId = $(this).attr('rel');
        $(this).hide();
        var subscriptionType = $(this).find('span').attr('rel');

        var subscribeEmailViewModel = new Object();
        subscribeEmailViewModel.Id = entityId;
        subscribeEmailViewModel.SubscriptionType = subscriptionType;

        // Ajax call to post the view model to the controller
        var strung = JSON.stringify(subscribeEmailViewModel);

        $.ajax({
            url: app_base + 'Email/Subscribe',
            type: 'POST',
            data: strung,
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $(".emailunsubscription").fadeIn();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                ShowUserMessage("Error: " + xhr.status + " " + thrownError);
            }
        });

        e.preventDefault();
        e.stopImmediatePropagation();
    });

    $(".emailunsubscription").click(function (e) {
        var entityId = $(this).attr('rel');
        $(this).hide();
        var subscriptionType = $(this).find('span').attr('rel');

        var unSubscribeEmailViewModel = new Object();
        unSubscribeEmailViewModel.Id = entityId;
        unSubscribeEmailViewModel.SubscriptionType = subscriptionType;

        // Ajax call to post the view model to the controller
        var strung = JSON.stringify(unSubscribeEmailViewModel);

        $.ajax({
            url: app_base + 'Email/UnSubscribe',
            type: 'POST',
            data: strung,
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $(".emailsubscription").fadeIn();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                ShowUserMessage("Error: " + xhr.status + " " + thrownError);
            }
        });

        e.preventDefault();
        e.stopImmediatePropagation();
    });

    // Some manual ajax badge checks
    if ($.QueryString["postbadges"] == "true") {
        // Do a post badge check
        UserPost();
    }

    // Poll Answer counter
    //var counter = 0;

    // This check is for the edit page
    //if (typeof counter === 'undefined') {
    //    // variable is undefined
    //} else {
    //    ShowHideRemovePollAnswerButton(counter);
    //}    

    // Remove the polls
    $(".removepollbutton").click(function (e) {
        e.preventDefault();
        //Firstly Show the Poll Section
        $('.pollanswerholder').hide();
        $('.pollanswerlist').html("");
        // Hide this button now
        $(this).hide();
        // Show the add poll button
        $(".createpollbutton").show();
        counter = 0;
    });

    // Create Polls
    $(".createpollbutton").click(function (e) {
        e.preventDefault();
        //Firstly Show the Poll Section
        $('.pollanswerholder').show();
        // Now add in the first row
        AddNewPollAnswer(counter);
        counter++;
        // Hide this button now
        $(this).hide();
        // Show the remove poll button
        $(".removepollbutton").show();
    });

    // Add a new answer
    $(".addanswer").click(function (e) {
        e.preventDefault();
        AddNewPollAnswer(counter);
        counter++;
        //ShowHideRemovePollAnswerButton(counter);
    });

    // Remove a poll answer
    $(".removeanswer").click(function (e) {
        e.preventDefault();
        if (counter > 0) {
            counter--;
            $("#answer" + counter).remove();
            //ShowHideRemovePollAnswerButton(counter);
        }
    });

    // Poll vote radio button click
    $(".pollanswerselect").click(function () {
        //Firstly Show the submit poll button
        $('.pollvotebuttonholder').show();
        // set the value of the hidden input to the answer value
        var answerId = $(this).data("answerid");
        $('.selectedpollanswer').val(answerId);
    });

    $(".pollvotebutton").click(function (e) {
        e.preventDefault();

        var pollId = $('#Poll_Id').val();
        var answerId = $('.selectedpollanswer').val();

        var UpdatePollViewModel = new Object();
        UpdatePollViewModel.PollId = pollId;
        UpdatePollViewModel.AnswerId = answerId;

        // Ajax call to post the view model to the controller
        var strung = JSON.stringify(UpdatePollViewModel);

        $.ajax({
            url: app_base + 'Poll/UpdatePoll',
            type: 'POST',
            dataType: 'html',
            data: strung,
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $(".pollcontainer").html(data);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                ShowUserMessage("Error: " + xhr.status + " " + thrownError);
            }
        });

    });



});

function ChangeLanguage() {
    var languageSelect = $(".languageselector select");
    if (languageSelect.length > 0) {
        languageSelect.change(function () {
            var langVal = this.value;
            $.ajax({
                url: '/Language/ChangeLanguage',
                type: 'POST',
                data: { lang: langVal },
                success: function (data) {
                    location.reload();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    ShowUserMessage("Error: " + xhr.status + " " + thrownError);
                }
            });
        });
    }
}

function DisplayWaitForPostUploadClickHandler() {
    var postUploadButton = $('.postuploadbutton');
    if (postUploadButton.length > 0) {
        postUploadButton.click(function (e) {
            var uploadHolder = $(this).closest("div.postuploadholder");
            var ajaxSpinner = uploadHolder.find("span.ajaxspinner");
            ajaxSpinner.show();
            $(this).hide();
        });
    }
}

function ShowFileUploadClickHandler() {
    var attachButton = $('.postshowattach');
    if (attachButton.length > 0) {
        attachButton.click(function (e) {
            e.preventDefault();
            var postHolder = $(this).closest("div.post");
            var uploadHolder = postHolder.find("div.postuploadholder");
            uploadHolder.toggle();
        });
    }
}

function SortWhosOnline() {
    $.getJSON(app_base + 'Members/LastActiveCheck');
}

function AddPostClickEvents() {

    ShowExpandedVotes();

    $(".issolution").click(function (e) {
        var solutionHolder = $(this);
        var postId = solutionHolder.attr('rel');

        var MarkAsSolutionViewModel = new Object();
        MarkAsSolutionViewModel.Post = postId;

        // Ajax call to post the view model to the controller
        var strung = JSON.stringify(MarkAsSolutionViewModel);

        $.ajax({
            url: app_base + 'Vote/MarkAsSolution',
            type: 'POST',
            data: strung,
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                MarkAsSolution(solutionHolder);
                BadgeMarkAsSolution(postId);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                ShowUserMessage("Error: " + xhr.status + " " + thrownError);
            }
        });
        e.preventDefault();
        e.stopImmediatePropagation();
    });

    $(".thumbuplink").click(function (e) {
        var postId = $(this).attr('rel');
        var karmascore = ".karmascore-" + postId;
        var karmathumbholder = ".postkarmathumbs-" + postId;
        $(karmathumbholder).remove();

        var VoteUpViewModel = new Object();
        VoteUpViewModel.Post = postId;

        // Ajax call to post the view model to the controller
        var strung = JSON.stringify(VoteUpViewModel);

        $.ajax({
            url: app_base + 'Vote/VoteUpPost',
            type: 'POST',
            data: strung,
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                SuccessfulThumbUp(karmascore);
                BadgeVoteUp(postId);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                ShowUserMessage("Error: " + xhr.status + " " + thrownError);
            }
        });
        e.preventDefault();
        e.stopImmediatePropagation();
    });

    $(".thumbdownlink").click(function (e) {
        var postId = $(this).attr('rel');
        var karmascore = ".karmascore-" + postId;
        var karmathumbholder = ".postkarmathumbs-" + postId;
        $(karmathumbholder).remove();

        var VoteDownViewModel = new Object();
        VoteDownViewModel.Post = postId;

        // Ajax call to post the view model to the controller
        var strung = JSON.stringify(VoteDownViewModel);

        $.ajax({
            url: app_base + 'Vote/VoteDownPost',
            type: 'POST',
            data: strung,
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                SuccessfulThumbDown(karmascore);
                BadgeVoteDown(postId);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                ShowUserMessage("Error: " + xhr.status + " " + thrownError);
            }
        });
        e.preventDefault();
        e.stopImmediatePropagation();
    });

    $(".thumbuplinkstudio").click(function (e) {
        var postId = $(this).attr('rel');
        var karmathumbholder = ".postkarmathumbs-" + postId;
        $(karmathumbholder).remove();

        var VoteUpViewModel = new Object();
        VoteUpViewModel.Post = postId;

        // Ajax call to post the view model to the controller
        var strung = JSON.stringify(VoteUpViewModel);

        $.ajax({
            url: app_base + 'Vote/VoteUpPost',
            type: 'POST',
            data: strung,
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                BadgeVoteUp(postId);
                ShowExpandedVotes();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                ShowUserMessage("Error: " + xhr.status + " " + thrownError);
            }
        });
        e.preventDefault();
        e.stopImmediatePropagation();
    });

    $(".thumbdownlinkstudio").click(function (e) {
        var postId = $(this).attr('rel');
        var karmathumbholder = ".postkarmathumbs-" + postId;
        $(karmathumbholder).remove();

        var VoteDownViewModel = new Object();
        VoteDownViewModel.Post = postId;

        // Ajax call to post the view model to the controller
        var strung = JSON.stringify(VoteDownViewModel);

        $.ajax({
            url: app_base + 'Vote/VoteDownPost',
            type: 'POST',
            data: strung,
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                BadgeVoteDown(postId);
                ShowExpandedVotes();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                ShowUserMessage("Error: " + xhr.status + " " + thrownError);
            }
        });
        e.preventDefault();
        e.stopImmediatePropagation();
    });
}

function AddNewPosts(showMoreLink, posts) {
    showMoreLink.before(posts);
}

function ShowExpandedVotes() {
    var category = $("#ParentTopic_Category_Name").val();
    var categoryList = $(".expandvotes").data("id");

    if (typeof (category) != 'undefined' && typeof (categoryList) != 'undefined' && categoryList.indexOf(category) > -1) {
        $(".expandvotes").each(GetVotesStudio);
    }
    else {
        var expandVotes = $(".expandvotes");
        if (expandVotes.length > 0) {
            expandVotes.click(function (e) {
                e.preventDefault();

                var votesSpan = $(this).find("span.votenumber");
                var expandVotesViewModel = new Object();
                expandVotesViewModel.Post = votesSpan.attr("id");

                // Ajax call to post the view model to the controller
                var strung = JSON.stringify(expandVotesViewModel);
                var thisExpandVotes = $(this);
                $.ajax({
                    url: app_base + 'Vote/GetVotes',
                    type: 'POST',
                    dataType: 'html',
                    data: strung,
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        votesSpan.html(data);
                        // remove the css class to remove the click pointer and show multple calls to this
                        thisExpandVotes.removeClass('expandvotes');
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        ShowUserMessage("Error: " + xhr.status + " " + thrownError);
                    }
                });

            });
        }
    }
}

function GetVotesStudio() {
    var expandvotes = $(this);
    var expandVotesViewModel = new Object();
    expandVotesViewModel.Post = expandvotes.attr("id");
    
    // Ajax call to post the view model to the controller
    var strung = JSON.stringify(expandVotesViewModel);
    $.ajax({
        url: app_base + 'Vote/GetVotes?Category=' + $("#ParentTopic_Category_Name").val(),
        type: 'POST',
        dataType: 'html',
        data: strung,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            expandvotes.html(data);
       },
        error: function (xhr, ajaxOptions, thrownError) {
            ShowUserMessage("Error: " + xhr.status + " " + thrownError);
        }
    });
}
// Unused now
function AddShowVoters() {
    var showVoters = $(".showvoters");
    if (showVoters.length > 0) {
        // Container/Parent
        showVoters.click(function (e) {
            e.preventDefault();
            // This the child box
            var voterBox = $(this).find('.showvotersbox');

            // firstly set the left position
            voterBox.css("left", voterBox.parent().width() + 2);

            // Now show it
            voterBox.toggle();

            if (voterBox.is(":visible")) {
                // Is being shown so do the Ajax call

                var GetVotersViewModel = new Object();
                GetVotersViewModel.Post = voterBox.attr("id");

                // Ajax call to post the view model to the controller
                var strung = JSON.stringify(GetVotersViewModel);

                $.ajax({
                    url: app_base + 'Vote/GetVoters',
                    type: 'POST',
                    dataType: 'html',
                    data: strung,
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        voterBox.html(data);
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        ShowUserMessage("Error: " + xhr.status + " " + thrownError);
                    }
                });

            }
        });
    }
}

function AddNewPollAnswer(counter) {
    var placeHolder = $('#pollanswerplaceholder').val();
    var liHolder = $(document.createElement('li')).attr("id", 'answer' + counter);
    liHolder.html('<input type="text" name="PollAnswers[' + counter + '].Answer" id="PollAnswers_' + counter + '_Answer" value="" placeholder="' + placeHolder + '" />');
    liHolder.appendTo(".pollanswerlist");
}

//function ShowHideRemovePollAnswerButton(counter) {
//    var removeButton = $('.removeanswer');
//    if(counter > 1) {
//        removeButton.show();
//    } else {
//        removeButton.hide();
//    }
//}

//---------------- Functions------------------------
function RemovePrivateMessageTableRow(linkClicked) {
    linkClicked.parents('tr').first().fadeOut();
}

function BadgeMarkAsSolution(postId) {

    // Ajax call to post the view model to the controller
    var markAsSolutionBadgeViewModel = new Object();
    markAsSolutionBadgeViewModel.PostId = postId;

    // Ajax call to post the view model to the controller
    var strung = JSON.stringify(markAsSolutionBadgeViewModel);

    $.ajax({
        url: app_base + 'Badge/MarkAsSolution',
        type: 'POST',
        data: strung,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            // No need to do anything
        },
        error: function (xhr, ajaxOptions, thrownError) {
            ShowUserMessage("Error: " + xhr.status + " " + thrownError);
        }
    });
}

function BadgeVoteUp(postId) {

    // Ajax call to post the view model to the controller
    var voteUpBadgeViewModel = new Object();
    voteUpBadgeViewModel.PostId = postId;

    // Ajax call to post the view model to the controller
    var strung = JSON.stringify(voteUpBadgeViewModel);

    $.ajax({
        url: app_base + 'Badge/VoteUpPost',
        type: 'POST',
        data: strung,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            // No need to do anything
        },
        error: function (xhr, ajaxOptions, thrownError) {
            ShowUserMessage("Error: " + xhr.status + " " + thrownError);
        }
    });
}

function BadgeVoteDown(postId) {

    // Ajax call to post the view model to the controller
    var voteUpBadgeViewModel = new Object();
    voteUpBadgeViewModel.PostId = postId;

    // Ajax call to post the view model to the controller
    var strung = JSON.stringify(voteUpBadgeViewModel);

    $.ajax({
        url: app_base + 'Badge/VoteDownPost',
        type: 'POST',
        data: strung,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            // No need to do anything
        },
        error: function (xhr, ajaxOptions, thrownError) {
            ShowUserMessage("Error: " + xhr.status + " " + thrownError);
        }
    });
}

function UserPost() {

    $.ajax({
        url: app_base + 'Badge/Post',
        type: 'POST',
        success: function (data) {
            // No need to do anything
        },
        error: function (xhr, ajaxOptions, thrownError) {
            ShowUserMessage("Error: " + xhr.status + " " + thrownError);
        }
    });
}

function MarkAsSolution(solutionHolder) {
    $(solutionHolder).removeClass("issolution")
    .addClass("issolution-solved")
    .attr('title', '')
    .unbind("click");
    $('.issolution').hide();   
}

function SuccessfulThumbUp(karmascore) {
    var currentKarma = parseInt($(karmascore).text());
    $(karmascore).text((currentKarma + 1));
}

function SuccessfulThumbDown(karmascore) {
    var currentKarma = parseInt($(karmascore).text());
    $(karmascore).text((currentKarma - 1));
}

function ShowUserMessage(message) {
    if (message != null) {
        var jsMessage = $('#jsquickmessage');
        var toInject = "<div class=\"alert alert-block alert-info fade in\"><a href=\"#\" data-dismiss=\"alert\" class=\"close\">&times;<\/a>" + message + "<\/div>";
        jsMessage.html(toInject);
        jsMessage.show();
        $('div.alert').delay(2200).fadeOut();
    }
}

function AjaxPostSuccess() {
    // Grab the span the newly added post is in
    var postHolder = $('#newpostmarker');

    // Now add a new span after with the key class
    // In case the user wants to add another ajax post straight after
    postHolder.after('<span id="newpostmarker"></span>');

    // Finally chnage the name of this element so it doesn't insert it into the same one again
    postHolder.attr('id', 'tonystarkrules');

    // And more finally clear the post box
    $('.createpost').val('');
    if ($(".bbeditorholder textarea").length > 0) {
        $(".bbeditorholder textarea").data("sceditor").val('');
    }
    if ($('.wmd-input').length > 0) {
        $(".wmd-input").val('');
        $(".wmd-preview").html('');
    }
    if (typeof tinyMCE != "undefined") {
        tinyMCE.activeEditor.setContent('');
    }

    // Re-enable the button
    $('#createpostbutton').attr("disabled", false);

    // Finally do an async badge check
    UserPost();
    
    // Attached the upload click events
    ShowFileUploadClickHandler();
    DisplayWaitForPostUploadClickHandler();
    //Trigger once again to register events on buttons that were just added
    AddPostClickEvents();
}

function AjaxPostBegin() {
    $('#createpostbutton').attr("disabled", true);
}

function AjaxPostError(message) {
    ShowUserMessage(message);
    $('#createpostbutton').attr("disabled", false);
}

(function ($) {
    $.QueryString = (function (a) {
        if (a == "") return {};
        var b = {};
        for (var i = 0; i < a.length; ++i) {
            var p = a[i].split('=');
            if (p.length != 2) continue;
            b[p[0]] = decodeURIComponent(p[1].replace(/\+/g, " "));
        }
        return b;
    })(window.location.search.substr(1).split('&'));
})(jQuery);