export {
    cityNameValidation,
    zipCodeValidation
}

let MIN_NAME_LENGTH = 2;
let ZIP_CODE_LENGTH = 5;

function cityNameValidation(objToValidate) {
    let str = objToValidate.value;

    if (str.length < MIN_NAME_LENGTH) {
        console.log(`City name must exceed ${MIN_NAME_LENGTH} characters`);
    }
    if (/\d/.test(str)) {
        console.log('City name cannot contain numbers');
    }
}

function zipCodeValidation(objToValidate) {
    let str = objToValidate.value;

    if (str.length != ZIP_CODE_LENGTH) {
        console.log(`Zip code has to contain ${ZIP_CODE_LENGTH} characters`);
    }
    if (/\w/.test(str)) {
        console.log('Zip code can contain only digits');
    }
}