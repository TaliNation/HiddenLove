window.blazorCookies = {
    writeCookie: function(name, value, expirationTimeInSeconds) {
        var expires;

        if(expirationTimeInSeconds) {
            var expirationDate = new Date();
            expirationDate.setTime(expirationDate.getTime() + (expirationTimeInSeconds * 1000));
            expires = "; expires=" + expirationDate.toGMTString();
        }
        else {
            expires = "";
        }

        document.cookie = name + "=" + value + expires + "; path=/";
    },

    readCookie: function(name) {
        var fullName = name + "=";
        var decodedCookie = decodeURIComponent(document.cookie);
        var ca = decodedCookie.split(';');
        
        for(var i=0; i<ca.length; i++) {
            var c = ca[i];
            while(c.charAt(0) == ' ') {
                c = c.substring(1);
            }
            if(c.indexOf(name) == 0) {
                return c.substring(fullName.length, c.length);
            }
        }

        return "";
    }
}

window.blazorLogger = {
    log: (obj) => { console.log(obj); },
    
    error: (obj) => { console.error(obj); }
}