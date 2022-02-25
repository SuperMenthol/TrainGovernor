import { lineValidation } from "../Shared/Validation.js";
import { build, buildRelationCard, adjustStatFields, filterStationSelect } from "../Shared/HTMLElements/LineRelations.js";
import { stationDto } from "../Shared/Models/LineStationDto.js";
import { calculateArrivalTime, calculateDepartureTime } from "../Shared/Calculations.js";

let stationSelect;
let nameInput;
let stationContainer;

let saveBtn;
let usedRelations = { relationsArray: [] };

window.onload = function () {
    build(usedRelations);
    stationContainer = document.getElementById('stations');

    nameInput = document.getElementById('name-input');

    stationSelect = document.querySelector('#station-select');
    stationSelect.addEventListener('change', stationSelectChange);

    saveBtn = document.getElementById('save-btn');
    saveBtn.addEventListener('click', save);

    filterStationSelect(neighbours, 0);
}

function stationSelectChange() {
    let relObj = addNewRelation(stationSelect.value);
    buildRelationCard(relObj);
    filterStationSelect(usedRelations.relationsArray, stationSelect.value);
}

function addNewRelation(relationId) {
    let rel = neighbours.find(x => x.id == relationId);

    let relationObj = {
        neighbourRelationId: rel.id,
        stationOrder: usedRelations.relationsArray.length + 1,
        stationId: rel.stationId,
        stationName: rel.stationName,
        neighbourId: rel.neighbourStation.id,
        neighbourName: rel.neighbourStation.name,
        avgSpeed: 1,
        breakTime: 0,
        arrivalTime: calculateArrivalTime(rel.distanceInKm, 1),
        departureTime: calculateDepartureTime(rel.distanceInKm, 1, 0),
        distance: rel.distanceInKm,
        isNew: true,
        isActive: rel.isActive
    };

    usedRelations.relationsArray.push(relationObj);

    adjustStatFields();

    return relationObj;
}

function save() {
    let validationResult = lineValidation(nameInput, usedRelations.relationsArray);

    if (validationResult.validated) {
        let lineStations = [];

        for (let i = 0; i < usedRelations.relationsArray.length; i++) {
            let lineStation = usedRelations.relationsArray[i];

            lineStations.push(stationDto(lineStation, i + 1, true));
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