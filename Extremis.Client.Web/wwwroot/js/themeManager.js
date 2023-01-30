window.getThemeMode = () => {
    return document.body.classList.contains("theme-dark");
};

window.setThemeMode = (darkMode) => {
    document.body.classList.remove("theme-dark", "theme-light");
    if(darkMode === undefined) {
        document.body.classList.add("theme-light");
        return false;
    } else if(darkMode === true) {
        document.body.classList.add("theme-dark");
        return true;
    } else {
        document.body.classList.add("theme-light");
        return false;
    }
} 