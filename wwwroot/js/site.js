function calculateSettingsAsThemeString({
  localStorageTheme,
  systemSettingDark,
}) {
  if (localStorageTheme !== null) {
    return localStorageTheme;
  }

  if (systemSettingDark.matches) {
    return "dark";
  }

  return "light";
}

let localStorageTheme = localStorage.getItem("theme");
let systemSettingDark = window.matchMedia("(prefers-color-scheme: dark)");
const html = document.querySelector("html");

let currentThemeSetting = calculateSettingsAsThemeString({
  localStorageTheme,
  systemSettingDark,
});

window.addEventListener("load", () => {
  html.setAttribute("data-theme", currentThemeSetting);
});

const toggleSwitch = document.querySelector("input[id=checkbox-theme]");

toggleSwitch.addEventListener("change", () => {
  let newTheme = currentThemeSetting === "dark" ? "light" : "dark";

  // Update theme attribute on HTML to switch css properties
  html.setAttribute("data-theme", newTheme);

  // Update local storage key-value pair
  localStorage.setItem("theme", newTheme);

  // Update the currentThemeSetting in memory
  currentThemeSetting = newTheme;
});
