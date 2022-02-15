let lineSelect;
let lineLabel;

window.onload = function () {
    lineSelect = document.getElementById('time-table-select');
    lineLabel = document.getElementById('time-table-label');
}

async function generatePdf() {
    if (lineSelect.value > 0) {
        lineInfo = await getBeginningStartTimes();
        let template = generateTemplate(lineInfo);

        return template;
    }
}

async function getBeginningStartTimes() {
    let lineId = lineSelect.value;

    let lineInfo = await fetch(`/LineStartTime/GetTimetableData/${lineId}`, {
        method: 'GET',
        mode: 'cors',
    })
        .then(response => response.json())
        .then(function (data) { return data; });

    return lineInfo;
}

async function downloadBtn_click() {
    affectLineLabel(lineSelect.value);
    html2pdf(await generatePdf()).save();
}

function lineSelect_change() {
    if (lineSelect.value > 0) {
        lineLabel.hidden = true;
    }
}

function affectLineLabel(lineId) {
    if (lineId > 0) {
        lineLabel.hidden = true;
    }
    else {
        lineLabel.hidden = false;
    }
}

function generateTemplate(lineInfo) {
    let mainDiv = document.createElement('div');
    mainDiv.classList.add('report-timetable-div');

    let titleDiv = document.createElement('div');

    let lineName = document.createElement('h1');
    lineName.classList.add('h1-header');
    lineName.textContent = `Line name: ${lineInfo.lineName}`;

    mainDiv.appendChild(titleDiv);
    mainDiv.appendChild(createTable(lineInfo.collection));

    return mainDiv;
}

function createTable(startTimesInfo) {
    let tbl = document.createElement('table');
    tbl.classList.add('table', 'table-bordered', 'table-striped', 'report-timetable-table');

    for (let stationInfo of startTimesInfo) {
        let newRow = tbl.insertRow(tbl.rows.length);
        let headerCell = newRow.insertCell(0);
        headerCell.innerText = stationInfo[0].stationName;
        headerCell.classList.add('report-timetable-table-cell');

        for (let i = 1; i < stationInfo.length + 1; i++) {
            let cellString = stationInfo[i - 1].departureTimeString === stationInfo[i - 1].arrivalTimeString ?
                stationInfo[i - 1].arrivalTimeString : stationInfo[i - 1].arrivalTimeString + '\r\n' + stationInfo[i - 1].departureTimeString;

            let newCell = newRow.insertCell(i);
            newCell.innerText = cellString;
            newCell.classList.add('report-timetable-table-cell');
        }
    }

    return tbl;
}