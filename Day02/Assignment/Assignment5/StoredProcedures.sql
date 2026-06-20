CREATE PROCEDURE sp_ProviderWorkload
AS
BEGIN
    SELECT
        ProviderId,
        COUNT(*) AS TotalEncounters
    FROM Encounter
    GROUP BY ProviderId
    ORDER BY TotalEncounters DESC;
END;
GO

CREATE PROCEDURE sp_HighRiskPatients
AS
BEGIN
    SELECT
        PatientId,
        COUNT(*) AS EncounterCount
    FROM Encounter
    GROUP BY PatientId
    HAVING COUNT(*) >= 3;
END;
GO


CREATE PROCEDURE sp_Readmissions30Days
AS
BEGIN
    SELECT
        PatientId,
        COUNT(*) AS Admissions
    FROM Encounter
    GROUP BY PatientId
    HAVING COUNT(*) > 1;
END;
GO


CREATE PROCEDURE usp_HighRiskPatients
AS
BEGIN
    SELECT
        PatientId,
        COUNT(*) AS AdmissionCount
    FROM Encounter
    GROUP BY PatientId
    HAVING COUNT(*) >= 3;
END
GO