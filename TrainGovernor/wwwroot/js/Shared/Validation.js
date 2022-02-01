export {
    cityValidation,
    stationValidation
}

let REGEX_ZIPCODE_LENGTH = new RegExp("/[0-9]{2}-[0-9]{3}/");
let REGEX_LETTERS_NUMBERS_SPACE_DASH = new RegExp("/[a-zA-Z0-9 ąĄćĆęĘłŁśŚńŃżŻźŹóÓ]*/");
let REGEX_STREET_NUMBER = new RegExp("[0-9]+[\/]?[a-zA-Z0-9]+");

let MIN_CITY_NAME_LENGTH = 2;
let ZIP_CODE_LENGTH = 6;

let CITY_NAME_LENGTH_MESSAGE = `City name must have at least ${MIN_CITY_NAME_LENGTH} characters`;
let CITY_REGEX_MESSAGE = 'City name can only contain letters, digits, space( ) and dash (-) symbol';
let ZIP_CODE_REGEX_MESSAGE = 'Zip code can contain only digits';
let ZIP_CODE_LENGTH_MESSAGE = `Zip code has to contain ${ZIP_CODE_LENGTH} characters if the field contains any text`;

let MIN_STREET_NAME_LENGTH = 2;

let STREET_NAME_LENGTH_MESSAGE = `Street name must have at least ${MIN_STREET_NAME_LENGTH} characters`;
let STREET_NAME_REGEX_MESSAGE = 'Street name can only contains letters, digits, space( ) and dash (-) symbol';

let STREET_NUMBER_REGEX_MESSAGE = 'Street number can only contain letters, digits and dash(/) symbol';


function cityValidation(cityNameField, zipCodeField) {
    let result = {
        validated: false,
        message: ''
    }

    let nameResult = validateBase(cityNameField, cityNameHooks);
    let codeResult = validateBase(zipCodeField, zipCodeHooks);

    result.validated = codeResult.validated && nameResult.validated;
    result.message = concatenateResultMessage([nameResult.message, codeResult.message]);

    return result;
}

function stationValidation(stationNameField, zipCodeField, streetNameField, streetNumberField) {
    let result = {
        validated: false,
        message: ''
    }

    let nameResult = validateBase(stationNameField, cityNameHooks);
    let codeResult = validateBase(zipCodeField, nonEmptyZipCodeHooks);
    let streetResult = validateBase(streetNameField, streetNameHooks);
    let streetNumberResult = validateBase(streetNumberField, streetNumberHooks);

    result.validated = codeResult.validated && nameResult.validated && streetResult.validated && streetNumberResult.validated;
    result.message = concatenateResultMessage([nameResult.message, codeResult.message, streetResult.message, streetNumberResult.message]);

    return result;
}

function validateBase(objToValidate, hook) {
    let result = {
        validated: false,
        message: ''
    };

    let messages = [];

    let input = objToValidate.value;

    for (let act of hook) {
        messages.push(act(input));
    }

    result.message = concatenateResultMessage(messages);

    if (result.message.length == 0) {
        result.validated = true;
    }
    return result;
}

function concatenateResultMessage(messages) {
    let result = '';


    for (let msg of messages) {
        result = result.length > 0 && msg.length > 0 ? result + '; ' + msg :
            msg.length > 0 ? msg : result;
    }

    return result;
}

let cityNameHooks = [
    function (input) {
        if (input.length < MIN_CITY_NAME_LENGTH) {
            return CITY_NAME_LENGTH_MESSAGE;
        }
        return '';
    },
    function (input) {
        if (input.match(REGEX_LETTERS_NUMBERS_SPACE_DASH)) {
            return CITY_REGEX_MESSAGE;
        }
        return '';
    }
];

let zipCodeHooks = [
    function (input) {
        if (input.length != 0 && input.length != ZIP_CODE_LENGTH) {
            return ZIP_CODE_LENGTH_MESSAGE;
        }
        return '';
    },
    function (input) {
        if (input.match(REGEX_ZIPCODE_LENGTH)) {
            return ZIP_CODE_REGEX_MESSAGE;
        }
        return '';
    }
];

let nonEmptyZipCodeHooks = [
    function (input) {
        if (input.length != ZIP_CODE_LENGTH) {
            return ZIP_CODE_LENGTH_MESSAGE;
        }
        return '';
    },
    function (input) {
        if (input.match(REGEX_ZIPCODE_LENGTH)) {
            return ZIP_CODE_REGEX_MESSAGE;
        }
        return '';
    }
];

let streetNameHooks = [
    function (input) {
        if (input.length < MIN_STREET_NAME_LENGTH) {
            return STREET_NAME_LENGTH_MESSAGE;
        }
        return '';
    },
    function (input) {
        if (input.match(REGEX_LETTERS_NUMBERS_SPACE_DASH)) {
            return STREET_NAME_REGEX_MESSAGE;
        }
        return '';
    }
];

let streetNumberHooks = [
    function (input) {
        if (!input.match(REGEX_STREET_NUMBER)) {
            return STREET_NUMBER_REGEX_MESSAGE;
        }
        return '';
    }
]