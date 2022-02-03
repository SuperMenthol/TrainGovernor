let searchInput;
let citySelect;

let tbl;

let searchBtn;
let showallBtn;
let addBtn;
let resetBtn;

window.onload = function () {
    searchInput = document.getElementById('namesearch-input');
    citySelect = document.getElementById('citysearch-input');

    searchBtn = document.getElementById('btn-search')
    showallBtn = document.getElementById('btn-showall');
    resetBtn = document.getElementById('btn-reset');
    tbl = document.getElementById('lines-table');

    showallBtn.addEventListener('click', showAllStations);
    searchBtn.addEventListener('click', search);
    resetBtn.addEventListener('click', reset);
}

function search() {
    clearTable();

    let filteredStations = stations;

    if (citySelect.value == 0 && nameInput.value.length == 0) {
        showAllStations();
    }
    else if (citySelect.value == -1 && nameInput.value.length == 0) {
        return;
    }

    if (citySelect.value > 0) {
        filteredStations = stations.filter(x => x.cityId == citySelect.value);
    }

    if (nameInput.value.length > 0) {
        filteredStations = filteredStations.filter(x => x.name.includes(nameInput.value));
    }

    showStations(filteredStations);
}

function showAllStations() {
    for (let data of lines) {
        appendRow(data);
    }
}

function showStations(stat) {
    for (let data of stat) {
        appendRow(data);
    }
}

function clearTable() {
    for (let i = tbl.rows.length - 1; i > 0; i--) {
        tbl.deleteRow(i);
    }
}

function reset() {
    clearTable();

    nameInput.value = '';
    citySelect.value = -1;
}

function appendRow(data) {
    let lineObj = lines.find(x => x.id === data.id);

    console.log(lineObj);

    let row = tbl.insertRow(tbl.rows.length);
    let btnCell = row.insertCell(0);
    let nameCell = row.insertCell(1);
    let stationsCountCell = row.insertCell(2);
    let travelTimeCell = row.insertCell(3);
    let startStationCell = row.insertCell(4);
    let endStationCell = row.insertCell(5);

    btnCell.classList.add('d-md-table-cell');
    btnCell.innerHTML = `<button class="btn btn-info" onclick="location.href='./Lines/Edit/${data.id}'">View</button>`;
    nameCell.innerText = lineObj.name;
    stationsCountCell.innerText = lineObj.allStations;
    travelTimeCell.innerText = lineObj.totalTravelTimeString;
    startStationCell.innerText = lineObj.startingStation.name;
    endStationCell.innerText = lineObj.endingStation.name;
}