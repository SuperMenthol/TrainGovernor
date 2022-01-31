let nameInput;
let citySelect;
let zipCodeInput;
let street1Input;
let street2Input;
let activeCheck;
let cancelBtn;
let saveBtn;

window.onload = function () {
    nameInput = document.getElementById('name-input');
    citySelect = document.getElementById('city-select');
    zipCodeInput = document.getElementById('zipcode-input');
    street1Input = document.getElementById('street1-input');
    street2Input = document.getElementById('street2-input');
    activeCheck = document.getElementById('active-check');
    cancelBtn = document.getElementById('cancel-btn');
    saveBtn = document.getElementById('save-btn');

    citySelect.value = station.cityId;

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
    station.name = nameInput.value.length > 0 ? nameInput.value : station.name;
    station.cityId = citySelect.value.length > 0 ? citySelect.value : station.cityId;
    station.zipCode = zipCodeInput.value.length > 0 ? zipCodeInput.value : station.zipCode;
    station.isActive = activeCheck.checked;
    station.address = {
        ZipCode: zipCodeInput.value.length > 0 ? zipCodeInput.value : station.zipCode,
        CityId: citySelect.value > 0 ? citySelect.value : station.cityId,
        StreetName: street1Input.value.length > 0 ? street1Input.value : station.address.StreetName,
        StreetNumber: street2Input.value.length > 0 ? street2Input.value : station.address.StreetNumber
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
        .then(() => refresh());
}