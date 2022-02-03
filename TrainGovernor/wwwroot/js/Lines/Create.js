import { lineValidation } from "../Shared/Validation.js";
import { speedInput, breakInput, dropButton, getStationCard, adjustStatFields, createStatPanel, buildStationSelect, filterStationSelect } from "../Shared/HTMLElements/Line.js";

let stationContainer;
let stationSelect;
let nameInput;

let saveBtn;

let usedRelations = [];

window.onload = function () {
    stationContainer = document.getElementById('stations');

    createStatPanel(usedRelations);

    nameInput = document.getElementById('name-input');

    stationSelect = buildStationSelect();
    stationSelect.addEventListener('change', stationSelectChange);

    saveBtn = document.getElementById('save-btn');
    saveBtn.addEventListener('click', save);

    filterStationSelect(neighbours, 0);
}

function stationSelectChange() {
    addNewRelation(stationSelect.value);
    filterStationSelect(usedRelations, stationSelect.value);
}

function addStationCard(usedRelation) {
    let stationCard = document.createElement('div');
    stationCard.classList.add('station-card', 'table-responsive');

    let tblElem = document.createElement('table');
    tblElem.classList.add('table', 'table-fixed', 'table-responsive');

    let row = tblElem.insertRow(tblElem.rows.length);

    let stationNameCell = row.insertCell(0);
    let targetNameCell = row.insertCell(1);
    let distanceCell = row.insertCell(2);
    let avgSpeedCell = row.insertCell(3);
    let breakCell = row.insertCell(4);
    let arrivalTimeCell = row.insertCell(5);
    let departureTimeCell = row.insertCell(6);
    let dropCell = row.insertCell(7);

    let drp = dropButton();
    drp.addEventListener('click', (e) => {
        let card = getStationCard(e.target);
        usedRelations = usedRelations.filter(x => x.id != usedRelation.id);
        adjustStatFields(usedRelations);
        card.remove();
        filterStationSelect(usedRelations, usedRelations.length > 0 ? usedRelations[usedRelations.length - 1].neighbourId : 0);
    });

    stationNameCell.innerText = usedRelation.stationName;
    targetNameCell.innerText = usedRelation.neighbourName;
    distanceCell.innerText = usedRelation.distance;
    avgSpeedCell.appendChild(speedInput(usedRelations, usedRelation));
    breakCell.appendChild(breakInput(usedRelations, usedRelation));
    arrivalTimeCell.innerText = '-';
    departureTimeCell.innerText = '-';
    dropCell.appendChild(drp);

    stationCard.appendChild(tblElem);
    stationContainer.appendChild(stationCard);
}

function addNewRelation(relationId) {
    let rel = neighbours.find(x => x.id == relationId);

    let relationObj = {
        id: relationId,
        lineId: 0,
        stationId: rel.stationId,
        stationName: rel.stationName,
        neighbourId: rel.neighbourStation.id,
        neighbourName: rel.neighbourStation.name,
        avgSpeed: 1,
        breakTime: 0,
        arrivalTime: 0,
        departureTime: 0,
        distance: rel.distanceInKm,
        isActive: rel.isActive
    };

    addStationCard(relationObj);
    usedRelations.push(relationObj);

    adjustStatFields(usedRelations);
}

function save() {
    let validationResult = lineValidation(nameInput, usedRelations);

    if (validationResult.validated) {
        let lineStations = [];

        for (let i = 0; i < usedRelations.length; i++) {
            let lineStation = usedRelations[i];

            lineStations.push({
                stationId: lineStation.stationId,
                neighbourRelationId: lineStation.id,
                stationOrder: i + 1,
                breakInMinutes: lineStation.breakTime,
                avgSpeed: lineStation.avgSpeed,
                neighbouringTrainStation: lineStation.neighbourStation,
                isActive: true
            });
        }

        let obj = {
            name: nameInput.value,
            isActive: true,
            lineStations: lineStations
        };

        fetch(`/Line/Add`, {
            method: 'POST',
            mode: 'cors',
            body: JSON.stringify(obj),
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(() => swal.fire({
                icon: 'success',
                title: 'Success!',
                text: 'Line and its stations have been created'
            }))
    }
    else {
        swal.fire({
            icon: 'error',
            title: 'Fields not validated',
            text: validationResult.message
        });
    }
}