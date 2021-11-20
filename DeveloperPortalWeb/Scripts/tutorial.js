window.tutorial = {};
tutorial.url = document.URL;
tutorial.text = '';
tutorial.pages = [];
tutorial.pager = 0;
tutorial.step = tutorial.pager + 1;
tutorial.steps = 0;
tutorial.next = '#next_page';
tutorial.prev = '#prev_page';
tutorial.proc = function (container, steps) {
    var width = 0;
    var item_width = 0;
    this.text = $(container).html();
    this.pages = this.text.split('<!-- page -->');
    if (steps !== undefined) {
        this.steps = steps;
        var prog_content = '';
        width = $(".progress").width();
        item_width = parseInt(width / this.steps) - 4;
        for (var n = 0; n < tutorial.steps; n++) {
            var text = tutorial.pages[n];
            var exp = /<h3>.+<\/h3>/i;
            var hint = text.match(exp);
            prog_content += '<div id="step' + parseInt(n + 1) + '" class="ti-progress fl" title="' + hint[0].replace(/<h3>(.+)<\/h3>/i, "$1") + '"></div>';
        }
        prog_content += '<div class="cb"></div>';
        this.pages.forEach(function (el, index) {
            tutorial.pages[index] = el.replace(/<div class="progress"><\/div>/, '<div class="progress">' + prog_content + '</div>');
        });
    }
    this.navigate();
    $(container).html(this.pages[this.pager]);

    $(document).on("click", ".ti-progress", function () {
        var id = $(this).attr("id").replace(/step/, '');
        tutorial.pager = id - 1;
        tutorial.step = tutorial.pager + 1;
        $(container).html(tutorial.pages[tutorial.pager]);
        tutorial.progress(item_width);
    });
    $(document).on("click", tutorial.next, function (e) {
        e.preventDefault();
        tutorial.pager++;
        if (tutorial.pager >= tutorial.steps)
            tutorial.pager = tutorial.steps - 1;
        tutorial.step = tutorial.pager + 1;
        $(container).html(tutorial.pages[tutorial.pager]);
        $("html, body").animate({ scrollTop: 0 }, 'slow');
        tutorial.progress(item_width);
    });
    $(document).on("click", tutorial.prev, function (e) {
        e.preventDefault();
        tutorial.pager--;
        if (tutorial.pager < 0)
            tutorial.pager = 0;
        tutorial.step = tutorial.pager + 1;
        $(container).html(tutorial.pages[tutorial.pager]);
        $("html, body").animate({ scrollTop: 0 }, 'slow');
        tutorial.progress(item_width);
    });

    this.progress(item_width);
};
tutorial.progress = function (item_width) {
    $(".ti-progress").css({ "width": item_width, "margin": "2px" });
    $("#step" + this.step).css("background", "#EA8110");
    if (tutorial.pager == tutorial.steps - 1)
        $(this.next).hide();
    else
        $(this.next).show();
    if (tutorial.pager == 0)
        $(this.prev).hide();
    else
        $(this.prev).show();
};
tutorial.navigate = function () {
    var step;
    if (/.+=(\d+)/i.test(this.url))
        step = this.url.replace(/.+=(\d+)/i, "$1");
    if (step !== undefined) {
        this.pager = step - 1;
        this.step = step;
    }
};