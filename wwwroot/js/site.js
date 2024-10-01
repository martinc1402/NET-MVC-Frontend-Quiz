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

const toggleSwitch = document.querySelector("input[id=toggle]");

window.addEventListener("load", () => {
  html.setAttribute("data-theme", currentThemeSetting);
  if (currentThemeSetting === "dark") {
    toggleSwitch.checked = true;
  }
});

toggleSwitch.addEventListener("change", () => {
  let newTheme = currentThemeSetting === "dark" ? "light" : "dark";

  // Update theme attribute on HTML to switch css properties
  html.setAttribute("data-theme", newTheme);

  // Update local storage key-value pair
  localStorage.setItem("theme", newTheme);

  // Update the currentThemeSetting in memory
  currentThemeSetting = newTheme;
});
