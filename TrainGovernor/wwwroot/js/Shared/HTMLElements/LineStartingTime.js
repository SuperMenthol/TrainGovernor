let container;
let hourInput;
let minuteInput;
let inactiveLbl;

let saveBtn;
let activateBtn;

let timeSelect;

let startTimesObject = {
    collection: [{
        id: 1,
        hour: 4,
        minute: 40,
        isActive: true
    },
        {
            id: 2,
            hour: 5,
            minute: 13,
            isActive: false
        },
        {
            id: 3,
            hour: 6,
            minute: 57,
            isActive: true
        },
        {
            id: 4,
            hour: 9,
            minute: 6,
            isActive: false
        },
        {
            id: 5,
            hour: 11,
            minute: 10,
            isActive: true
        },
        {
            id: 6,
            hour: 13,
            minute: 7,
            isActive: true
        }
    ]
};

export function buildStartingTimeSegment(timesObject) {
    //startTimesObject = timesObject;
    container = document.getElementById('starttime-container');

    let containerDiv = buildContainingDiv();
    container.appendChild(containerDiv);

    loadSelectOptions();
}

function buildContainingDiv() {
    let containingDiv = document.createElement('div');
    containingDiv.classList.add('start-time-div');

    buildTimeInputsContainer(containingDiv);
    buildButtonsContainer(containingDiv);
    buildSelectContainer(containingDiv);

    return containingDiv;
}

function buildTimeInputsContainer(containingDiv) {
    let cnt = document.createElement('div');
    cnt.classList.add('start-time-time-boxes-cnt');

    let timeBoxesPart = document.createElement('div');
    timeBoxesPart.classList.add('start-time-body-time');

    hourInput = document.createElement('input');
    hourInput.min = 0;
    hourInput.max = 23;
    hourInput.type = 'number';
    hourInput.classList.add('form-control', 'start-time-element-time-box');
    hourInput.id = 'hour-input';
    hourInput.addEventListener('change', (e) => timeInput_correctShort(e));

    minuteInput = hourInput.cloneNode(false);
    minuteInput.max = 59;
    minuteInput.id = 'minute-input';
    minuteInput.addEventListener('change', (e) => timeInput_correctShort(e));

    let lblPart = document.createElement('div');
    lblPart.classList.add('start-time-body-time');

    inactiveLbl = document.createElement('p');
    inactiveLbl.classList.add('start-time-element-lbl');
    inactiveLbl.textContent = 'This starting time is not active';
    inactiveLbl.hidden = true;

    lblPart.appendChild(inactiveLbl);

    let sep = document.createElement('p');
    sep.classList.add('start-time-element-p');
    sep.textContent = ':';

    hourInput.classList.add('start-time-element-hour-box');

    timeBoxesPart.appendChild(hourInput);
    timeBoxesPart.appendChild(sep);
    timeBoxesPart.appendChild(minuteInput);

    cnt.appendChild(timeBoxesPart);
    cnt.appendChild(lblPart);

    containingDiv.appendChild(cnt);
}

function buildButtonsContainer(containingDiv) {
    let buttonsPart = document.createElement('div');
    buttonsPart.classList.add('start-time-body-buttons');

    saveBtn = document.createElement('button');
    saveBtn.classList.add('btn', 'btn-success', 'start-time-element-button');
    saveBtn.textContent = 'Save';
    saveBtn.addEventListener('click', saveBtn_click);

    let separator = document.createElement('div');
    separator.classList.add('separator-div-md');

    activateBtn = document.createElement('button');
    activateBtn.classList.add('btn', 'btn-warning', 'start-time-element-button');
    activateBtn.disabled = true;
    activateBtn.textContent = 'Activate';
    activateBtn.addEventListener('click', activateBtn_click);

    buttonsPart.appendChild(saveBtn);
    buttonsPart.appendChild(separator);
    buttonsPart.appendChild(activateBtn);

    containingDiv.appendChild(buttonsPart);
}

function buildSelectContainer(containingDiv) {
    let selectPart = document.createElement('div');
    selectPart.classList.add('start-time-body-select');

    timeSelect = document.createElement('select');
    timeSelect.classList.add('form-select', 'start-time-select');
    timeSelect.size = 4;
    timeSelect.multiple = false;
    timeSelect.addEventListener('change', (e) => timeSelect_change(e));

    selectPart.appendChild(timeSelect);
    containingDiv.appendChild(selectPart);
}

function timeInput_correctShort(e) {
    if (e.target.value.length == 1) {
        e.target.value = '0' + e.target.value;
    }
}

function saveBtn_click() {

}

function activateBtn_click() {

}

function timeSelect_change(e) {
    let selectedId = e.explicitOriginalTarget.value;
    let selectedObject = startTimesObject.collection.filter(x => x.id == selectedId)[0];
    if (selectedId > 0) {
        hourInput.value = selectedObject.hour;
        minuteInput.value = selectedObject.minute;
    }
    else {
        hourInput.value = 0;
        minuteInput.value = 0;
        affectActivateBtn(selectedId);
        return;
    }

    affectActivateBtn(selectedId, selectedObject.isActive);
}

function affectActivateBtn(id, isActive) {
    if (id == 0) {
        activateBtn.disabled = true;
        return;
    }

    activateBtn.disabled = false;
    inactiveLbl.hidden = isActive;

    if (isActive) {
        activateBtn.textContent = 'Deactivate';
    }
    else {
        activateBtn.textContent = 'Activate';
    }
}

function loadSelectOptions() {
    timeSelect.options.add(selectFirstOption());
    for (let item of startTimesObject.collection) {
        timeSelect.options.add(startTimeSelectOption(item));
    }
}

function startTimeSelectOption(item) {
    let opt = document.createElement('option');
    opt.value = item.id;
    opt.text = `${item.hour}:${item.minute}`;

    return opt;
}

function selectFirstOption() {
    let selectFirstOption = document.createElement('option');
    selectFirstOption.value = 0;
    selectFirstOption.text = 'Insert new';

    return selectFirstOption;
}