export function calculateArrivalTime(distance, avgSpeed) {
    if (avgSpeed == 0 || avgSpeed === null) {
        return 0;
    }

    return 60 * parseFloat(distance) / parseFloat(avgSpeed);
}

export function calculateDepartureTime(distance, avgSpeed, brkTime) {
    return parseFloat(calculateArrivalTime(distance, avgSpeed)) + parseFloat(brkTime);
}