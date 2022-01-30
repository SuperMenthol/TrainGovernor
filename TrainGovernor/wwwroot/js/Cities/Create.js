let nameInput;
let codeInput;
let saveBtn;

window.onload = function () {
    nameInput = document.getElementById('name-input');
    codeInput = document.getElementById('postcode-input');
    saveBtn = document.getElementById('save-button');
}

function save() {
    let name = nameInput.value;
    let zipCode = codeInput.value;

    fetch(`/City/Add/${name}/${zipCode}`, { method: 'POST'});
}