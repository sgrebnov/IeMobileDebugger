window.wpHtmlDebugger = {
    initialize: function () {
        this.bind();
    },
    bind: function () {
        document.addEventListener('deviceready', this.deviceready, false);
    },
    deviceready: function () {
        alert("Debug console installed and working");
    }
}.initialize();