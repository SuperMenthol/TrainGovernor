let nameInput;
let citySelect;
let zipCodeInput;
let street1Input;
let street2Input;
let cancelBtn;
let saveBtn;

window.onload = function () {
    nameInput = document.getElementById('name-input');
    citySelect = document.getElementById('city-select');
    zipCodeInput = document.getElementById('zipcode-input');
    street1Input = document.getElementById('street1-input');
    street2Input = document.getElementById('street2-input');
    cancelBtn = document.getElementById('cancel-btn');
    saveBtn = document.getElementById('save-btn');

    cancelBtn.addEventListener('click', refresh);
    saveBtn.addEventListener('click', save);
}

function refresh() {
    nameInput.value = '';
    citySelect.value = 0;
    zipCodeInput.value = '';
    street1Input.value = '';
    street2Input.value = '';

    window.top.location.reload();
}

function validate() {

}

async function save() {
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
        .then(() => refresh());
}