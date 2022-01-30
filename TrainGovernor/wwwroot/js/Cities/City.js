let allCities;
let tbl;
let sel;

window.onload = function () {
    allCities = cities;
    sel = document.getElementById('citySelect');
    tbl = document.getElementById('cityTable');

    sel.addEventListener('change', selectCity);
}();

function getAllCities() {
    clearTable();
    for (let dataRow of allCities) {
        appendRow(dataRow);
    }
}

function selectCity() {
    let val = sel.selectedOptions[0].value;

    if (val > 0) {
        clearTable();
        let row = allCities.filter(x => x.id == val)[0];
        appendRow(row);
    }
}

function clearTable() {
    for (let i = tbl.rows.length - 1; i > 0; i--) {
        tbl.deleteRow(i);
    }
}

function appendRow(data) {
    let row = tbl.insertRow(tbl.rows.length);
    let cell1 = row.insertCell(0);
    let cell2 = row.insertCell(1);
    let cell3 = row.insertCell(2);

    cell1.classList.add('d-md-table-cell');
    cell1.innerHTML = `<button class="btn btn-info" onclick="location.href='./ Cities / Edit /${@city.Id}'">View</button>`;
    cell2.innerText = data.name;
    cell3.innerText = data.stationsCount;
}