wpHtmlDebugger = {
    initialize: function () {
        this.bind();
    },
    bind: function () {
        document.addEventListener('deviceready', this.deviceready, false);
    },
    deviceready: function () {
        alert("Debug console installed and working");
    },
	applyXSSpatch: function() {
		var xhr = function() {
			var xdr = new window.XDomainRequest();

			xdr.onload = function(){
				xdr.status = 200;
				xdr.statusText = "OK";
				xdr.readyState = 4;
				xdr.onreadystatechange({target: xdr});
			};

			xdr.onerror = function () {
				if (xdr.onreadystatechange != null) {
					xdr.readyState = 4;
					xdr.status = 0;
					xdr.statusText = ''; // ???
					xdr.responseText = '';

					xdr.onreadystatechange({target: xdr});
				}
			};
			xdr.onprogress = function () {
				if (xdr.onreadystatechange != null && xdr.status != 3) {
					xdr.readyState = 3;
					xdr.status = 3;
					xdr.statusText = '';
					xdr.onreadystatechange({target: xdr});
				}
			};
			
			xdr.setRequestHeader = function() {};

			return xdr;
		}

		window.XMLHttpRequestPatched = xhr;
	}
};

wpHtmlDebugger.initialize();