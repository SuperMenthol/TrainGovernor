import { cityValidation } from "../Shared/Validation.js";

let nameInput;
let codeInput;
let saveBtn;

window.onload = function () {
    nameInput = document.getElementById('name-input');
    codeInput = document.getElementById('postcode-input');
    saveBtn = document.getElementById('save-button');

    nameInput.addEventListener('change', nameInput_change);

    saveBtn.addEventListener('click', save);
}

function nameInput_change() {
    if (nameInput.value.length > 2) {
        saveBtn.disabled = false;
    }
    else {
        saveBtn.disabled = true;
    }
}

function save() {
    let validationResult = cityValidation(nameInput, codeInput);

    console.log(validationResult);

    if (validationResult.validated) {
        let obj = {
            name: nameInput.value,
            zipCode: codeInput.value.length > 0 ? codeInput.value : '',
            isActive: true
        };

        fetch(`/City/Add/${obj.name}/${obj.zipCode}`, { method: 'POST' })
            .then(swal.fire({
                icon: 'success',
                title: `${obj.name} saved as a new city!`
            }));
    }
    else {
        swal.fire({
        icon: 'error',
        title: 'Fields not validated',
        text: validationResult.message
        });
    }
}