// provide our own console if it does not exist, huge dev aid!
if (typeof window.console == "undefined") {
    window.console = { log: function (str) { window.external.Notify(str); } };
}

// output any errors to console log, created above.
window.onerror = function (e) {
    console.log("window.onerror ::" + JSON.stringify(e));
};

console.log("Installed console ! ");