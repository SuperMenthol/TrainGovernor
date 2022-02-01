import { stationValidation } from "../Shared/Validation.js";

let oldNameInput;
let oldZipCodeInput;
let oldStreet1Input;
let oldStreet2Input;

let nameInput;
let citySelect;
let zipCodeInput;
let street1Input;
let street2Input;
let activeCheck;
let saveBtn;

window.onload = function () {
    oldNameInput = document.getElementById('oldname-input');
    oldZipCodeInput = document.getElementById('oldzipcode-input');
    oldStreet1Input = document.getElementById('oldstreet1-input');
    oldStreet2Input = document.getElementById('oldstreet2-input');

    nameInput = document.getElementById('name-input');
    citySelect = document.getElementById('city-select');
    zipCodeInput = document.getElementById('zipcode-input');
    street1Input = document.getElementById('street1-input');
    street2Input = document.getElementById('street2-input');
    activeCheck = document.getElementById('active-check');
    saveBtn = document.getElementById('save-btn');

    citySelect.value = station.cityId;

    saveBtn.addEventListener('click', save);
    citySelect.addEventListener('change', citySelect_change);
}

function refresh() {
    nameInput.value = '';
    citySelect.value = 0;
    zipCodeInput.value = '';
    street1Input.value = '';
    street2Input.value = '';

    window.top.location.reload();
}

async function save() {
    let usedObjects = getUsedObjects();
    let validationResult = stationValidation(usedObjects.nameField, usedObjects.zipCodeField, usedObjects.streetNameField, usedObjects.streetNumberField);

    if (validationResult.validated) {
        station.name = usedObjects.nameField.value;
        station.cityId = citySelect.value.length > 0 ? citySelect.value : station.cityId;
        station.zipCode = usedObjects.zipCodeField.value;
        station.isActive = activeCheck.checked; //to do: prompt for deactivation
        station.address = {
            ZipCode: usedObjects.zipCodeField.value,
            CityId: citySelect.value > 0 ? citySelect.value : station.cityId,
            StreetName: usedObjects.streetNameField.value,
            StreetNumber: usedObjects.streetNumberField.value
        };

        console.log(station);

        fetch(`/TrainStation/Update`, {
            method: 'PUT',
            mode: 'cors',
            body: JSON.stringify(station),
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(() => swal.fire({
                icon: 'success',
                title: 'Success!',
                text: 'Station has been updated'
            }))
            .then(() => refresh());
    }
    else {
        swal.fire({
            icon: 'error',
            title: 'Fields not validated',
            text: validationResult.message
        });
    }
}

function getUsedObjects() {
    return {
        nameField: nameInput.value.length > 0 ? nameInput : oldNameInput,
        zipCodeField: zipCodeInput.value.length > 0 ? zipCodeInput : oldZipCodeInput,
        streetNameField: street1Input.value.length > 0 ? street1Input : oldStreet1Input,
        streetNumberField: street2Input.value.length > 0 ? street2Input : oldStreet2Input
    }
}

function citySelect_change() {
    saveBtn.disabled = citySelect.value == 0;
}