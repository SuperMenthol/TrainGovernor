import { calculateArrivalTime, calculateDepartureTime, timeStringFromMinutes, minutesFromTimeString } from "../Calculations.js";

const RELATION_CARD_HEADER_CELLS = [
    'Relation ID',
    'From',
    'To',
    'Distance (km)',
    'Time to arrive',
    'Time to depart',
    'Average speed',
    'Break time',
    'Cancel'
];

let totalStationsField;
let startingStationField;
let endingStationField;
let totalTravelTimeField;

let stationSelect;
let relationsDiv;

let relationsObject;

export function build(usedRelations) {
    relationsObject = usedRelations;

    createStatPanel(relationsObject.relationsArray);
    buildStationSelect();
    buildRelationsContainer();
}

export function createStatPanel() {
    let mainBox = document.createElement('div');
    mainBox.classList.add('input-box');

    let header = document.createElement('h1');
    header.classList.add('h1-style');

    let lblStation = document.createElement('label');
    lblStation.textContent = 'Stations:'
    lblStation.htmlFor = 'stations-count';

    let lblStart = document.createElement('label');
    lblStart.textContent = 'Starting station:'
    lblStart.htmlFor = 'starting-station';

    let lblFinal = document.createElement('label');
    lblFinal.textContent = 'Final station:'
    lblFinal.htmlFor = 'final-station';

    let lblTravelTime = document.createElement('label');
    lblTravelTime.textContent = 'Total travel time:'
    lblTravelTime.htmlFor = 'travel-time';

    let separatorDiv = document.createElement('div');
    separatorDiv.classList.add('separator-div-sm');

    totalStationsField = generateStatInput('stations-count');
    startingStationField = generateStatInput('starting-station');
    endingStationField = generateStatInput('final-station');
    totalTravelTimeField = generateStatInput('travel-time');

    mainBox.appendChild(header);
    mainBox.appendChild(lblStation);
    mainBox.appendChild(totalStationsField);
    mainBox.appendChild(separatorDiv);
    mainBox.appendChild(lblStart);
    mainBox.appendChild(startingStationField);
    mainBox.appendChild(separatorDiv.cloneNode());
    mainBox.appendChild(lblFinal);
    mainBox.appendChild(endingStationField);
    mainBox.appendChild(separatorDiv.cloneNode());
    mainBox.appendChild(lblTravelTime);
    mainBox.appendChild(totalTravelTimeField);

    if (relationsObject.relationsArray.length > 0) {
        adjustStatFields(relationsObject.relationsArray);
    }

    let statSeparator = document.getElementById('stat-separator');
    statSeparator.parentElement.insertBefore(mainBox, statSeparator);
}

export function buildStationSelect() {
    stationSelect = document.createElement('select');
    stationSelect.id = 'station-select';
    stationSelect.classList.add('form-select');

    stationSelect.options.add(stationSelectOption(0,'Select a station to add'));

    let container = document.getElementById('select-container');
    container.appendChild(stationSelect);

    return stationSelect;
}

export function buildRelationsContainer() {
    let container = document.querySelector('.relations-container');

    let hdr = document.createElement('h1');
    hdr.classList.add('h1-style');
    hdr.textContent = 'Line stations:';

    container.appendChild(hdr);

    relationsDiv = document.createElement('div');
    relationsDiv.classList.add('relations-div','table-responsive');
    relationsDiv.id = 'stations';

    relationsObject.relationsArray.forEach((x) => buildRelationCard(x));

    container.appendChild(relationsDiv);
}

export function buildRelationCard(relationObj) {
    let cardStyle = relationObj.isNew ? 'unsaved-station-card' : 'station-card';
    let card = document.createElement('div');
    card.classList.add(cardStyle, 'table-responsive');

    let tbl = document.createElement('table');
    tbl.classList.add('table', 'table-fixed', 'table-responsive');

    let header = tbl.createTHead();
    let hdrRow = header.insertRow(0);
    for (let i = 0; i < RELATION_CARD_HEADER_CELLS.length; i++) {
        hdrRow.insertCell(i).outerHTML = `<th>${RELATION_CARD_HEADER_CELLS[i]}</th>`;
    }

    let bodyRow = tbl.insertRow(1);
    bodyRow.insertCell(0).outerHTML = `<td>${relationObj.stationOrder}</td>`;
    bodyRow.insertCell(1).outerHTML = `<td>${relationObj.stationName}</td>`;
    bodyRow.insertCell(2).outerHTML = `<td>${relationObj.neighbourName}</td>`;
    bodyRow.insertCell(3).outerHTML = `<td>${relationObj.distance}</td>`;
    bodyRow.insertCell(4).outerHTML = `<td>${timeStringFromMinutes(relationObj.arrivalTime)}</td >`;
    bodyRow.insertCell(5).outerHTML = `<td>${timeStringFromMinutes(relationObj.departureTime)}</td>`;
    let spdCell = bodyRow.insertCell(6);
    spdCell.appendChild(speedInput(relationObj));
    let brkCell = bodyRow.insertCell(7);
    brkCell.appendChild(breakInput(relationObj));
    let xRow = bodyRow.insertCell(8);
    xRow.appendChild(dropButton(relationObj));

    card.appendChild(tbl);
    relationsDiv.appendChild(card);
}

function deleteUnsavedRelation(e, usedRelation) {
    let card = getStationCard(e);

    while (card.nextSibling != null) {
        let nextCard = card.nextSibling;
        nextCard.remove();
    }

    relationsObject.relationsArray.length = relationsObject.relationsArray.indexOf(usedRelation);
    card.remove();
    adjustStatFields(relationsObject.relationsArray);
    filterStationSelect(relationsObject.relationsArray, relationsObject.relationsArray.length > 0 ? relationsObject.relationsArray[relationsObject.relationsArray.length - 1].neighbourId : 0);
}

function discardChanges(e, relationObj) {
    let card = getStationCard(e);
    let cardData = getCardData(card);

    relationObj.avgSpeed = relationObj.baseAvgSpeed;
    relationObj.breakTime = relationObj.baseBreakTime;

    cardData.AvgSpeedInput.value = relationObj.avgSpeed;
    cardData.BreakTimeInput.value = relationObj.breakTime;

    adjustStatFields(relationsObject.relationsArray);
}

function generateStatInput(id) {
    let totalStations = document.createElement('input');
    totalStations.disabled = true;
    totalStations.value = '';
    totalStations.classList.add('form-control');
    totalStations.id = id;
    totalStations.type = 'text';

    return totalStations;
}

export function speedInput(relationObj) {
    let spdInput = document.createElement('input');
    spdInput.id = `rel-${relationObj.id}-speed-input`;
    spdInput.type = 'number';
    spdInput.classList.add('form-control');
    spdInput.min = 1;
    spdInput.value = relationObj.avgSpeed ?? 1;
    spdInput.addEventListener('change', (e) => fillTimeField(e, relationObj, true));
    spdInput.addEventListener('change', (e) => fillTimeField(e, relationObj, false));
    if (!relationObj.isNew) {
        relationObj.baseAvgSpeed = relationObj.avgSpeed;
    }

    return spdInput;
}

export function breakInput(relationObj) {
    let brkInput = document.createElement('input');
    brkInput.id = `rel-${relationObj.id}-break-input`;
    brkInput.type = 'number';
    brkInput.classList.add('form-control');
    brkInput.min = 0;
    brkInput.value = relationObj.breakTime ?? 0;
    brkInput.addEventListener('change', (e) => fillTimeField(e, relationObj, false));
    if (!relationObj.isNew) {
        relationObj.baseBreakTime = relationObj.breakTime;
    }

    return brkInput;
}

export function dropButton(relationObj) {
    let dropButton = document.createElement('button');
    dropButton.id = 'delete-unsaved-btn';
    dropButton.type = 'button';
    dropButton.classList.add('btn-close');
    let dropListener = relationObj.isNew ?
        (e) => { deleteUnsavedRelation(e, relationObj); } :
        (e) => { discardChanges(e, relationObj); };

    dropButton.addEventListener('click', dropListener);

    return dropButton;
}

export function fillTimeField(e, relationObj, isArrival) {
    let cardData = getCardData(getStationCard(e.target));
    let elementToChange = isArrival ? cardData.ArrivalTimeCell : cardData.DepartureTimeCell;

    relationObj.avgSpeed = parseFloat(cardData.AvgSpeedInput.value);
    relationObj.breakTime = parseFloat(cardData.BreakTimeInput.value);

    adjustStatFields();

    let timeToInput = isArrival ?
        timeStringFromMinutes(calculateArrivalTime(relationObj.distance, relationObj.avgSpeed)) :
        timeStringFromMinutes(calculateDepartureTime(relationObj.distance, relationObj.avgSpeed, relationObj.breakTime));

    if (timeToInput == 0) {
        elementToChange.textContent = '-';
    }
    else {
        elementToChange.textContent = timeToInput.toFixed(2);
    }
}

export function getStationCard(e) {
    let currentElement = e.target != null ? e.target : e;

    while (currentElement.parentElement != null) {
        let parent = currentElement.parentElement;
        if (parent.classList.contains('station-card')
            || parent.classList.contains('edited-station-card')
            || parent.classList.contains('unsaved-station-card')) {
            return parent;
        }
        currentElement = parent;
    }

    return null;
}

export function getCardData(card) {
    let cardData = card.querySelectorAll('td');

    return {
        StationName: cardData[1].textContent,
        TargetName: cardData[2].textContent,
        DistanceInKm: parseFloat(cardData[3].textContent),
        AvgSpeedInput: cardData[6].querySelector('input'),
        BreakTimeInput: cardData[7].querySelector('input'),
        ArrivalTimeCell: cardData[4],
        DepartureTimeCell: cardData[5]
    };
}

export function adjustStatFields() {
    totalStationsField.value = relationsObject.relationsArray.length > 0 ? relationsObject.relationsArray.length + 1 : 0;
    startingStationField.value = relationsObject.relationsArray.length > 0 ? relationsObject.relationsArray[0].stationName : '';
    endingStationField.value = relationsObject.relationsArray.length > 0 ? relationsObject.relationsArray[relationsObject.relationsArray.length - 1].neighbourName : '';

    let totalTravelTime = 0;
    relationsObject.relationsArray.forEach(function (x) {
        totalTravelTime += parseFloat(calculateDepartureTime(x.distance, x.avgSpeed, x.breakTime)).toFixed(2);
    });

    totalTravelTimeField.value = totalTravelTime;
}

export function filterStationSelect(usedRelations, selectVal) {
    let filteredStations = neighbours;

    if (selectVal > 0) {
        filteredStations = filteredStations.filter(x => x.stationId == usedRelations[usedRelations.length - 1].neighbourId);
        filteredStations = filteredStations.filter(x => x.neighbourId != usedRelations[usedRelations.length - 1].stationId);
    }

    clearStationSelect();
    fillStationSelect(filteredStations);
}

function clearStationSelect() {
    for (let i = stationSelect.options.length - 1; i > 0; i--) {
        stationSelect.options[i].remove();
    }
}

function fillStationSelect(stations) {
    for (let station of stations) {
        stationSelect.options.add(
            stationSelectOption(station.id, station.stationName + ' - ' + station.neighbourStation.name)
        );
    }
}

function stationSelectOption(optionValue, text) {
    var opt = document.createElement('option');
    opt.value = optionValue;
    opt.text = text;

    return opt;
}