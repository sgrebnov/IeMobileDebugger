// a place for hacks

console.log("installing wp-hacks");

weinreIE9Hacks = function () {

    // check for quirks mode
    if(typeof document.addEventListener === 'undefined')
    {
        alert ("Opps. It seems the page runs in compatibility mode. Please fix and try again.");
        return;
    }


    if (typeof(Element) != "object") {
        Element = function () {};
    }

    if (typeof(Node) != "object") {
        Node = function () {};
    }
}

var isIE = window.navigator.userAgent.indexOf("MSIE") != -1;

if (isIE) {
    weinreIE9Hacks();

    if (navigator.notification && navigator.notification.alert) {
        window.alert = navigator.notification.alert;
    }
}