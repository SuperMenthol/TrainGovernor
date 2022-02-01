import { cityValidation } from "../Shared/Validation.js";

let nameInput;
let codeInput;
let saveBtn;

window.onload = function () {
    nameInput = document.getElementById('name-input');
    codeInput = document.getElementById('postcode-input');
    saveBtn = document.getElementById('save-button');

    saveBtn.addEventListener('click', save);
}

function save() {
    let validationResult = cityValidation(nameInput, codeInput);

    if (validationResult.validated) {
        obj.name = newName.value.length > 0 ? newName.value : oldName.value;
        obj.postCode = newCode.value.length > 0 ? newCode.value : oldCode.value;
        obj.isActive = activeCheck.checked;

        fetch(`/City/Add/${name}/${zipCode}`, { method: 'POST' })
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