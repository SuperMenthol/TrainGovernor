import { lineValidation } from "../Shared/Validation.js";
import { build, buildRelationCard, adjustStatFields, filterStationSelect } from "../Shared/HTMLElements/LineRelations.js";
import { buildStartingTimeSegment } from "../Shared/HTMLElements/LineStartingTime.js";
import { stationDto } from "../Shared/Models/LineStationDto.js";
import { calculateArrivalTime, calculateDepartureTime } from "../Shared/Calculations.js";

let stationSelect;

let oldNameInput;
let nameInput;

let saveBtn;

let usedRelations = { relationsArray: [] };

window.onload = function () {
    getUsedRelations(line.lineStations);
    build(usedRelations);

    buildStartingTimeSegment();

    oldNameInput = document.getElementById('oldname-input');
    nameInput = document.getElementById('name-input');

    stationSelect = document.querySelector('#station-select');
    stationSelect.addEventListener('change', stationSelectChange);

    saveBtn = document.getElementById('save-btn');
    saveBtn.addEventListener('click', save);

    filterStationSelect(usedRelations.relationsArray, usedRelations.relationsArray[usedRelations.relationsArray.length-1].neighbourRelationId);
}

function getUsedRelations(stations) {
    stations.forEach(x => usedRelations.relationsArray.push({
        id: x.id,
        neighbourRelationId: x.neighbourRelationId,
        stationId: x.trainStation.id,
        stationName: x.trainStation.name,
        stationOrder: x.stationOrder,
        neighbourId: x.neighbouringTrainStation.neighbourId,
        neighbourName: x.neighbouringTrainStation.neighbourStation.name,
        avgSpeed: x.avgSpeed,
        breakTime: x.breakInMinutes,
        distance: x.neighbouringTrainStation.distanceInKm,
        isActive: x.isActive,
        arrivalTime: x.getTimeToArrive,
        departureTime: x.getTimeToDepart,
        isNew: false
    }));

    usedRelations.relationsArray = usedRelations.relationsArray.sort((x, y) => x.stationOrder - y.stationOrder);
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
    let nameToValidate = nameInput.value.length > 0 ? oldNameInput : nameInput;
    let validationResult = lineValidation(nameToValidate, usedRelations);

    if (validationResult.validated) {
        let lineStations = [];

        for (let i = 0; i < usedRelations.relationsArray.length; i++) {
            let lineStation = usedRelations.relationsArray[i];

            lineStations.push(stationDto(lineStation, lineStation.stationOrder, lineStation.isNew));
        }

        let obj = {
            id: line.id,
            name: nameInput.value,
            isActive: true,
            lineStations: lineStations
        };

        fetch(`/Line/Update`, {
            method: 'PUT',
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