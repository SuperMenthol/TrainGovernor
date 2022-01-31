let nameInput;
let citySelect;

let showallBtn;
let resetBtn;
let searchBtn;
let tbl;

window.onload = function () {
    nameInput = document.getElementById('namesearch-input');
    citySelect = document.getElementById('citysearch-input');

    searchBtn = document.getElementById('btn-search')
    showallBtn = document.getElementById('btn-all');
    resetBtn = document.getElementById('btn-reset');
    tbl = document.getElementById('stations-table');

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

function appendRow(data) {
    let cityObj = cities.find(x => x.id === data.cityId);

    let row = tbl.insertRow(tbl.rows.length);
    let btnCell = row.insertCell(0);
    let nameCell = row.insertCell(1);
    let zipcodeCell = row.insertCell(2);
    let cityNameCell = row.insertCell(3);
    let addrCell = row.insertCell(4);
    let activeCell = row.insertCell(5);
    let linesCountCell = row.insertCell(6);

    btnCell.classList.add('d-md-table-cell');
    btnCell.innerHTML = `<button class="btn btn-info" onclick="location.href='./Stations/Edit/${data.id}'">View</button>`;
    nameCell.innerText = data.name;
    zipcodeCell.innerText = data.zipCode;
    cityNameCell.innerText = cityObj.name;
    addrCell.innerText = data.streetAddress;
    activeCell.innerText = data.isActive ? "Yes" : "No";
    linesCountCell.innerText = '0';
}

function showAllStations() {
    for (let data of stations) {
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