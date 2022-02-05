import { stationValidation } from "../Shared/Validation.js";

let nameInput;
let citySelect;
let zipCodeInput;
let street1Input;
let street2Input;
let saveBtn;

window.onload = function () {
    nameInput = document.getElementById('name-input');
    citySelect = document.getElementById('city-select');
    zipCodeInput = document.getElementById('zipcode-input');
    street1Input = document.getElementById('street1-input');
    street2Input = document.getElementById('street2-input');
    saveBtn = document.getElementById('save-btn');

    saveBtn.addEventListener('click', save);
    nameInput.addEventListener('change', checkSavePossibility);
    citySelect.addEventListener('change', citySelect_change);
    zipCodeInput.addEventListener('change', checkSavePossibility);
    street1Input.addEventListener('change', checkSavePossibility);
    street2Input.addEventListener('change', checkSavePossibility);
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
    let validationResult = stationValidation(nameInput, zipCodeInput, street1Input, street2Input);

    if (validationResult.validated) {
        station.name = nameInput.value;
        station.cityId = citySelect.value;
        station.zipCode = zipCodeInput.value;
        station.isActive = true;
        station.address = {
            ZipCode: zipCodeInput.value,
            CityId: citySelect.value,
            StreetName: street1Input.value,
            StreetNumber: street2Input.value
        };

        fetch(`/TrainStation/Add`, {
            method: 'POST',
            mode: 'cors',
            body: JSON.stringify(station),
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(() => swal.fire({
                icon: 'success',
                title: 'Success!',
                text: 'Station has been created'
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

function citySelect_change() {
    if (citySelect.value > 0) {
        let city = cities.find(x => x.id == citySelect.value)

        if (city.zipCode != null) {
        zipCodeInput.value = city.zipCode;
        }
    }
}

function checkSavePossibility() {
    if (citySelect.value > 0
        && zipCodeInput.value.length > 0
        && street1Input.value.length > 0
        && nameInput.value.length > 0
        && street2Input.value.length > 0) {
        saveBtn.disabled = false;
    }
    else {
        saveBtn.disabled = true;
    }
}