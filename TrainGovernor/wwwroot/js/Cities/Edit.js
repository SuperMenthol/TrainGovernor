import { cityValidation } from "../Shared/Validation.js";
import { generateSwal } from "../Shared/swalGenerator.js";

let oldName;
let newName;
let oldCode;
let newCode;
let saveBtn;

let obj;

document.onload = function () {
    oldName = document.getElementById('oldname-input');
    newName = document.getElementById('newname-input');
    oldCode = document.getElementById('oldcode-input');
    newCode = document.getElementById('newcode-input');
    saveBtn = document.getElementById('savebtn');

    newName.addEventListener('change', editValueChange);
    newCode.addEventListener('change', editValueChange);

    saveBtn.addEventListener('click', savebtn_click);

    obj = city;
}();

function editValueChange() {
    if ((newName.value != oldName.value && newName.value.length > 0)
        || newCode.value != oldCode.value && newCode.value.length > 0) {
        saveBtn.disabled = false;
    }
    else {
        saveBtn.disabled = true;
    }
}

function savebtn_click() {
    let objectsToInput = {
        nameField: newName.value.length > 0 ? newName : oldName,
        zipCodeField: newCode.value.length > 0 ? newCode : oldCode
    }
    let validationResult = cityValidation(objectsToInput.nameField, objectsToInput.zipCodeField);

    if (validationResult.validated) {
        obj.name = objectsToInput.nameField.value;
        obj.zipCode = objectsToInput.zipCodeField.value;

        fetch(`/City/UpdateCity`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(obj)
        })
            .then(data => data.json())
            .then(response => {
                saveBtn.disabled = true;
                generateSwal(response, refresh);
            });
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
    oldName.value = newName.value;
    oldCode.value = newCode.value;
    newName.value = '';
    newCode.value = '';

    window.top.location.reload();
}