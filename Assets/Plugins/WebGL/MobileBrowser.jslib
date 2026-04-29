mergeInto(LibraryManager.library, {
    IsMobileBrowser: function () {
        var userAgent = navigator.userAgent || navigator.vendor || window.opera;

        var isMobile =
            /android/i.test(userAgent) ||
            /iPad|iPhone|iPod/.test(userAgent) ||
            /Mobile|Tablet/i.test(userAgent);

        return isMobile ? 1 : 0;
    }
});