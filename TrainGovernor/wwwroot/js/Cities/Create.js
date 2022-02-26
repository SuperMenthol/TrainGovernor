import { cityValidation } from "../Shared/Validation.js";
import { generateSwal } from "../Shared/swalGenerator.js";

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

    if (validationResult.validated) {
        let obj = {
            name: nameInput.value,
            zipCode: codeInput.value.length > 0 ? codeInput.value : '',
            isActive: true
        };

        fetch(`/City/Add/${obj.name}/${obj.zipCode}`, { method: 'POST' })
            .then((data) => data.json())
            .then((response) => generateSwal(response, refresh()));
    }
    else {
        swal.fire({
        icon: 'error',
        title: 'Fields not validated',
        text: validationResult.message
        });
    }
}

function refresh() {
    nameInput.value = '';
    codeInput.value = '';
}