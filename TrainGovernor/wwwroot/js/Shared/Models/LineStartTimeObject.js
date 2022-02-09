export function lineStartTimeObject(lineId, hour, minute) {
    return {
        id: -1,
        lineId: lineId,
        hour: hour,
        minute: minute,
        isActive: true
    };
}

export function objectToSave(startTime) {
    return {
        id: startTime.id,
        lineId: startTime.lineId,
        hour: startTime.hour,
        minute: startTime.minute,
        isActive: startTime.isActive
    };
}