let oldName;
let newName;
let oldCode;
let newCode;
let activeCheck;
let cancelBtn;
let saveBtn;

let obj;

document.onload = function () {
    oldName = document.getElementById('oldname-input');
    newName = document.getElementById('newname-input');
    oldCode = document.getElementById('oldcode-input');
    newCode = document.getElementById('newcode-input');
    activeCheck = document.getElementById('deactivate-check');
    cancelBtn = document.getElementById('cancelbtn');
    saveBtn = document.getElementById('savebtn');

    newName.addEventListener('change', editValueChange);
    newCode.addEventListener('change', editValueChange);
    activeCheck.addEventListener('change', editValueChange);

    saveBtn.addEventListener('click', savebtn_click);

    obj = city;
    activeCheck.checked = obj.isActive;
}();

function cancelbtn_click() {
    newName.value = '';
    newCode.value = '';
}

function editValueChange() {
    if ((newName.value != oldName.value && newName.value.length > 0)
        || newCode.value != oldCode.value && newCode.value.length > 0
        || activeCheck.checked != obj.isActive) {
        saveBtn.disabled = false;
    }
    else {
        saveBtn.disabled = true;
    }
}

function savebtn_click() {
    obj.name = newName.value.length > 0 ? newName.value : oldName.value;
    obj.postCode = newCode.value.length > 0 ? newCode.value : oldCode.value;
    obj.isActive = activeCheck.checked;

    fetch(`/City/UpdateCity`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(obj)
    })
        .then(() => saveBtn.disabled = true)
        .then(() => window.top.location.reload());
}