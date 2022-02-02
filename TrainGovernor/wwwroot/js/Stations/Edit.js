import { stationValidation } from "../Shared/Validation.js";

//todo: export neighbouring train station functions to different file, refactor save function

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

let stationsContainer;
let connectionSelect;

window.onload = function () {
    oldNameInput = document.getElementById('oldname-input');
    oldZipCodeInput = document.getElementById('oldzipcode-input');
    oldStreet1Input = document.getElementById('oldstreet1-input');
    oldStreet2Input = document.getElementById('oldstreet2-input');

    stationsContainer = document.getElementById('stations');
    connectionSelect = document.getElementById('neighbour-select');

    nameInput = document.getElementById('name-input');
    citySelect = document.getElementById('city-select');
    zipCodeInput = document.getElementById('zipcode-input');
    street1Input = document.getElementById('street1-input');
    street2Input = document.getElementById('street2-input');
    activeCheck = document.getElementById('active-check');
    saveBtn = document.getElementById('save-btn');

    citySelect.value = station.cityId;

    let distanceInputs = document.querySelectorAll('#distance-input');
    distanceInputs.forEach(x => x.addEventListener('change', connectionDistance_change));

    let connectionChecks = document.querySelectorAll('#connection-check');
    connectionChecks.forEach(x => x.addEventListener('change', connectionActive_change));

    saveBtn.addEventListener('click', save);
    citySelect.addEventListener('change', citySelect_change);
    connectionSelect.addEventListener('change', connectionSelect_change);
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
    let newConnections = getNewNeighbourConnections();

    if (newConnections.length > 0) {
        fetch('/TrainStation/AddNeighbouringStation', {
            method: 'POST',
            mode: 'cors',
            body: JSON.stringify(newConnections),
            headers: {
                'Content-Type': 'application/json'
            }
        });
    }

    let connectionsToUpdate = getConnectionsToUpdate();

    if (connectionsToUpdate.length > 0) {
        fetch('/TrainStation/UpdateNeighbouringStations', {
            method: 'PUT',
            mode: 'cors',
            body: JSON.stringify(connectionsToUpdate),
            headers: {
                'Content-Type': 'application/json'
            }
        });
    }

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

function connectionSelect_change() {
    if (connectionSelect.value > 0) {
        appendUnsavedConnection();
    }
}

function appendUnsavedConnection() {
    let dataInStrings = connectionSelect.options[connectionSelect.selectedIndex].text.split(' - ');
    let neighbourData = {
        Id: connectionSelect.value,
        Name: dataInStrings[0],
        Addrees: dataInStrings[1],
        IsActive: true
    };

    let htmlString = `<div class="unsaved-station-card table-responsive">
                                <table class="table table-responsive">
                                    <tr>
                                        <th>Station Id</th>
                                        <th>Station name</th>
                                        <th>Station address</th>
                                        <th>Distance to station</th>
                                        <th>Abort</th>
                                    </tr>
                                    <tr>
                                        <td>${neighbourData.Id}</td>
                                        <td>${neighbourData.Name}</td>
                                        <td>${neighbourData.Addrees}</td>
                                        <td><input type="number" class="form-control" min=1 value=1></td>
                                        <td><button id="delete-unsaved-btn" type="button" class="btn-close" aria-label="close"></button></td>
                                    </tr>
                                </table>
                        </div>`;

    stationsContainer.insertAdjacentHTML('beforeend', htmlString);
    document.getElementById('delete-unsaved-btn').addEventListener('click', function (e) {
        let card = getStationCard(e);
        card.remove();
    });
}

function connectionActive_change() {
    let card = getStationCard(this);
    let cardData = getCardData(card);

    changeCardColor(card, this.checked != neighbours.find(x => x.Id == cardData.id).isActive);
}

function connectionDistance_change() {
    let card = getStationCard(this);
    let cardData = getCardData(card);

    let condition = this.value != neighbours.find(x => x.Id == cardData.id).distanceInKm;

    changeCardColor(card, condition);
}

function changeCardColor(card, toEdited) {
    if (toEdited) {
        card.classList.remove('station-card');
        card.classList.add('edited-station-card');
    }
    else {
        card.classList.remove('edited-station-card');
        card.classList.add('station-card');

        if (document.querySelectorAll('#edited-station-card, #unsaved-station-card').length == 0) {
            saveBtn.disabled = true;
        }
    }
}

function getCardData(card) {
    let cardData = card.querySelectorAll('td');
    let isNew = cardData[4].querySelector('#connection-check') == null;

    return {
        Id: cardData[0].textContent,
        Name: cardData[1].textContent,
        Distance: cardData[3].querySelector('input').value,
        IsActive: isNew ? true : cardData[4].querySelector('input').checked
    };
}

function createNeighbouringStationValueObject(cardData) {
    console.log(station);
    return {
        StationId: station.id,
        NeighbourId: cardData.Id,
        DistanceInKm: cardData.Distance,
        IsActive: cardData.IsActive
    };
}

function reverseNeighbouringStationValueObject(valueObject) {
    return {
        StationId: valueObject.NeighbourId,
        NeighbourId: valueObject.StationId,
        DistanceInKm: valueObject.DistanceInKm,
        IsActive: valueObject.IsActive
    };
}

function getStationCard(e) {
    let currentElement = e;

    while (currentElement.parentElement != null) {
        let parent = currentElement.parentElement;
        if (parent.classList.contains('station-card')
            || parent.classList.contains('edited-station-card')) {
            return parent;
        }
        currentElement = parent;
    }

    return null;
}

function getNewNeighbourConnections() {
    let cards = document.querySelectorAll('.unsaved-station-card');
    let newConnections = [];
    let allNewConnections = [];

    cards.forEach(x => newConnections.push(createNeighbouringStationValueObject(getCardData(x))));
    newConnections.forEach(x => allNewConnections.push(reverseNeighbouringStationValueObject(x)));
    newConnections.forEach(x => allNewConnections.push(x));

    return allNewConnections;
}

function getConnectionsToUpdate() {
    let cards = document.querySelectorAll('.edited-station-card');
    let editedConnections = [];
    let allEditedConnections = [];

    cards.forEach(x => editedConnections.push(createNeighbouringStationValueObject(getCardData(x))));
    editedConnections.forEach(x => allEditedConnections.push(reverseNeighbouringStationValueObject(x)));
    editedConnections.forEach(x => allEditedConnections.push(x));

    return allEditedConnections;
}