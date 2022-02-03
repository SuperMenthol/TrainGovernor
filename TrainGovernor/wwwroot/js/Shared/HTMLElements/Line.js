import { calculateArrivalTime, calculateDepartureTime } from "../Calculations.js";

let totalStationsField;
let startingStationField;
let endingStationField;
let totalTravelTimeField;

let stationSelect;

export function createStatPanel(usedRelations) {
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

    if (usedRelations.length > 0) {
        adjustStatFields(usedRelations);
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

function generateStatInput(id) {
    let totalStations = document.createElement('input');
    totalStations.disabled = true;
    totalStations.value = '';
    totalStations.classList.add('form-control');
    totalStations.id = id;
    totalStations.type = 'text';

    return totalStations;
}

export function speedInput(usedRelations, relationObj) {
    let spdInput = document.createElement('input');
    spdInput.id = `rel-${relationObj.id}-speed-input`;
    spdInput.type = 'number';
    spdInput.classList.add('form-control');
    spdInput.min = 1;
    spdInput.value = 1;
    spdInput.addEventListener('change', (e) => fillTimeField(e, usedRelations, relationObj, true));
    spdInput.addEventListener('change', (e) => fillTimeField(e, usedRelations, relationObj, false));

    return spdInput;
}

export function breakInput(usedRelations, relationObj) {
    let brkInput = document.createElement('input');
    brkInput.id = `rel-${relationObj.id}-break-input`;
    brkInput.type = 'number';
    brkInput.classList.add('form-control');
    brkInput.min = 0;
    brkInput.value = 0;
    brkInput.addEventListener('change', (e) => fillTimeField(e, usedRelations, relationObj, false));

    return brkInput;
}

export function dropButton() {
    let dropButton = document.createElement('button');
    dropButton.id = 'delete-unsaved-btn';
    dropButton.type = 'button';
    dropButton.classList.add('btn-close');

    return dropButton;
}

export function fillTimeField(e, usedRelations, relationObj, isArrival) {
    let cardData = getCardData(getStationCard(e.target));
    let elementToChange = isArrival ? cardData.ArrivalTimeCell : cardData.DepartureTimeCell;

    relationObj.avgSpeed = parseFloat(cardData.AvgSpeed);
    relationObj.breakTime = parseFloat(cardData.BreakTime);

    adjustStatFields(usedRelations);

    let timeToInput = isArrival ?
        calculateArrivalTime(relationObj.distance, relationObj.avgSpeed) :
        calculateDepartureTime(relationObj.distance, relationObj.avgSpeed, relationObj.breakTime);

    if (timeToInput == 0) {
        elementToChange.textContent = '-';
    }
    else {
        elementToChange.textContent = timeToInput;
    }
}

export function getStationCard(e) {
    let currentElement = e;

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
        StationName: cardData[0].textContent,
        TargetName: cardData[1].textContent,
        DistanceInKm: parseFloat(cardData[2].textContent),
        AvgSpeed: cardData[3].querySelector('input').value,
        BreakTime: cardData[4].querySelector('input').value,
        ArrivalTimeCell: cardData[5],
        DepartureTimeCell: cardData[6]
    };
}

export function adjustStatFields(usedRelations) {
    if (totalStationsField == null) totalStationsField = document.getElementById('stations-count');
    if (totalTravelTimeField == null) totalTravelTimeField = document.getElementById('travel-time');
    if (startingStationField == null) startingStationField = document.getElementById('starting-station');
    if (endingStationField == null) endingStationField = document.getElementById('final-station');

    totalStationsField.value = usedRelations.length > 0 ? usedRelations.length + 1 : 0;
    startingStationField.value = usedRelations.length > 0 ? usedRelations[0].stationName : '';
    endingStationField.value = usedRelations.length > 0 ? usedRelations[usedRelations.length - 1].neighbourName : '';

    let totalTravelTime = 0;
    usedRelations.forEach(function (x) {
        totalTravelTime += parseFloat(calculateDepartureTime(x.distance, x.avgSpeed, x.breakTime));
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