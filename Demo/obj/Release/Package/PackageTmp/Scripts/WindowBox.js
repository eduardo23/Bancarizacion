var link = document.createElement('link');
link.rel = 'stylesheet';
link.type = 'text/css';
link.href = 'Css/jquery-ui1.css';
link.media = 'all';
document.getElementsByTagName("head")[0].appendChild(link);

var e = document.createElement("script");
e.src = 'Scripts/jquery.min.js_1_8_3.js';
e.type = "text/javascript";
document.getElementsByTagName("head")[0].appendChild(e);

var e = document.createElement("script");
e.src = 'Scripts/jquery-ui.min_1_10.js';
e.type = "text/javascript";
document.getElementsByTagName("head")[0].appendChild(e);

function fnLoadPageInDialog(url, contID, titleNewDialog, widthNewDialog, heightNewDialog) {
    $.ajaxSetup({
        cache: false
    });
    $("#dialog").css('backgroundColor', 'White');
    //$("#dialog").css('overflow', 'hidden');
    $("#dialog").dialog({
        autoOpen: false,
        position: 'top',
        show: { effect: 'fade', duration: 1000 },
        hide: { effect: 'fade', duration: 300 },
        modal: true,
        title: titleNewDialog,
        width: widthNewDialog,
        height: heightNewDialog,
        dialogClass: 'ui-widget-shadow',
        open: function (ev, ui) {
            $('#myIframe').css('display', 'block');
            $('#myIframe').attr('src', url);
            $('#myIframe').attr('width', widthNewDialog - 30);
            $('#myIframe').attr('Height', heightNewDialog - 40);
        },
        close: function (event, ui) {
            $('#myIframe').attr('src', 'blank.html');
        }
    });
    $("#dialog").dialog("open");
}