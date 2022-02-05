export function calculateArrivalTime(distance, avgSpeed) {
    if (avgSpeed == 0 || avgSpeed === null) {
        return 0;
    }

    return 60 * parseFloat(distance) / parseFloat(avgSpeed);
}

export function calculateDepartureTime(distance, avgSpeed, brkTime) {
    return parseFloat(calculateArrivalTime(distance, avgSpeed)) + parseFloat(brkTime);
}

export function timeStringFromMinutes(input) {
    let hours = Math.floor(input / 60);
    let minutes = (input - hours * 60).toPrecision(2);

    return `${hours}:${minutes}:00`;
}