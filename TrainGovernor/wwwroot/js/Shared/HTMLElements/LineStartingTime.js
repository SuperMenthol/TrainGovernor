import { lineStartTimeObject } from "../Models/LineStartTimeObject.js";

let container;
let hourInput;
let minuteInput;
let inactiveLbl;

let saveBtn;
let activateBtn;

const ACTIVATE_BTN_TEXT = {
    IS_ACTIVE: 'Deactivate',
    IS_INACTIVE: 'Activate',
    IS_NULL: 'Remove'
};

const NEWLY_ADDED_TIME_ID = -1;

let timeSelect;

let line;
let startTimesObject;

export function buildStartingTimeSegment(lineId, timesObject) {
    line = lineId;
    startTimesObject = timesObject;
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
    activateBtn.textContent = ACTIVATE_BTN_TEXT.IS_INACTIVE;
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
    if (timeSelect.value == 0) {
        let newObject = lineStartTimeObject(line, parseInt(hourInput.value), parseInt(minuteInput.value));
        startTimesObject.collection.push(newObject);
        timeSelect.options.add(startTimeSelectOption(newObject));
    }
    else {
        let objectToUpdate = startTimesObject.collection.filter(x => x.id == timeSelect.value)[0];
        objectToUpdate.hour = parseInt(hourInput.value);
        objectToUpdate.minute = parseInt(minuteInput.value);

        timeSelect.selectedOptions[0].textContent = generateOptionTextContent(objectToUpdate.hour, objectToUpdate.minute);
    }
}

function activateBtn_click() {
    if (timeSelect.value == NEWLY_ADDED_TIME_ID) {
        removeUnsavedStartTime();
        return;
    }

    let selectedObject = startTimesObject.collection.filter(x => x.id == timeSelect.value)[0];

    selectedObject.isActive = !selectedObject.isActive;
    inactiveLbl.hidden = selectedObject.isActive;

    activateBtn.textContent = selectedObject.isActive ? "Deactivate" : "Activate";
    manageTimeSelectOptionStyle(timeSelect.selectedOptions[0], selectedObject.isActive);
}

function removeUnsavedStartTime() {
    let objectToDelete = startTimesObject.collection.filter(x => x.id == NEWLY_ADDED_TIME_ID && x.hour == parseInt(hourInput.value && x.minute == parseInt(minuteInput.value)));
    startTimesObject.collection = startTimesObject.collection.filter(x => x.id != NEWLY_ADDED_TIME_ID && x.hour != objectToDelete.hour && x.minute != objectToDelete.minute);

    let selectedOption = timeSelect.selectedOptions[0];
    timeSelect.selectedIndex = 0;
    timeSelect.options.remove(selectedOption);
    affectActivateBtn(0);
}

function timeSelect_change(e) {
    let selectedId = e.explicitOriginalTarget.value;
    console.log(selectedId);

    let selectedObject = startTimesObject.collection.filter(x => x.id == selectedId)[0];
    if (selectedId != 0) {
        hourInput.value = generateHourText(selectedObject.hour);
        minuteInput.value = generateMinuteText(selectedObject.minute);

        if (selectedId == -1) {
            changeActivateBtnText(null);
        }
    }
    else {
        hourInput.value = 0;
        minuteInput.value = 0;
        affectActivateBtn(selectedId, true);
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

    changeActivateBtnText(isActive);
}

function changeActivateBtnText(isActive) {
    if (isActive) {
        activateBtn.textContent = ACTIVATE_BTN_TEXT.IS_ACTIVE;
    }
    else if (isActive === false) {
        activateBtn.textContent = ACTIVATE_BTN_TEXT.IS_INACTIVE;
    }
    else if (isActive == null || isActive == undefined) {
        activateBtn.textContent = ACTIVATE_BTN_TEXT.IS_NULL;
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
    opt.text = generateOptionTextContent(item.hour, item.minute);
    manageTimeSelectOptionStyle(opt, item.isActive);

    return opt;
}

function manageTimeSelectOptionStyle(option, isActive) {
    if (isActive) {
        option.classList.remove('start-time-select-option-inactive');
    }
    else {
        option.classList.add('start-time-select-option-inactive');
    }
}

function selectFirstOption() {
    let selectFirstOption = document.createElement('option');
    selectFirstOption.value = 0;
    selectFirstOption.text = 'Insert new';

    return selectFirstOption;
}

function generateOptionTextContent(hour, minute) {
    return `${generateHourText(hour)}:${generateMinuteText(minute)}`;
}

function generateHourText(hour) {
    if (hour.toString().length == 1) {
        hour = '0' + hour;
    }

    return hour;
}

function generateMinuteText(minute) {
    if (minute.toString().length == 1) {
        minute = '0' + minute;
    }

    return minute;
}