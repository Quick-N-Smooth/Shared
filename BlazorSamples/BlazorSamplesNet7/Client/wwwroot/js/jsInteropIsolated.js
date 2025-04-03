export function PopupAlert(message) {
    alert("Message from isolated script: " + message);
}
export function ToggleSection(element) {
    if (element == undefined) {
        return;
    }
    if (element.style.display == "none") {
        element.style.display = "block";
    }
    else {
        element.style.display = "none";
    }
}
export function ColorSection(element, color) {
    if (element == undefined) {
        return;
    }
    element.style.color = color;
}
export function ClickOnElement(element) {
    if (element == undefined) {
        return;
    }
    element.click();
}

