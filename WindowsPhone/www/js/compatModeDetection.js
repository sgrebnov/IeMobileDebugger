isInCompatMode = function() {
    if (typeof document.addEventListener == "undefined") {
        return true;
    }

    return false;
};

isInCompatMode().toString();