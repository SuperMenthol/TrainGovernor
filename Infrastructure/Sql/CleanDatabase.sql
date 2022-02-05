USE ASP_TrainGovernor;

DELETE FROM Cities;
DBCC CHECKIDENT('Cities', RESEED, 1)

DELETE FROM Stations;
DBCC CHECKIDENT('Stations', RESEED, 1)

DELETE FROM Lines;
DBCC CHECKIDENT('Lines', RESEED, 1)

DELETE FROM LineStations;
DBCC CHECKIDENT('LineStations', RESEED, 1)

DELETE FROM NeighbouringStations;
DBCC CHECKIDENT('NeighbouringStations', RESEED, 1)

SELECT *
FROM dbo.Cities
SELECT *
FROM dbo.Lines
SELECT *
FROM dbo.LineStations
SELECT *
FROM dbo.NeighbouringStations
SELECT *
FROM dbo.Stations