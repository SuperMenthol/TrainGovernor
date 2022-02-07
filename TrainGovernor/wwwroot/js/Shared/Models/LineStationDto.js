export function stationDto(relationObj, order, isNew) {
    return {
        id: relationObj.id,
        stationId: relationObj.stationId,
        neighbourRelationId: relationObj.neighbourRelationId,
        stationOrder: order,
        breakInMinutes: relationObj.breakTime,
        avgSpeed: relationObj.avgSpeed,
        neighbouringTrainStation: relationObj.neighbourStation,
        isActive: isNew ? isNew : relationObj.isActive
    };
}