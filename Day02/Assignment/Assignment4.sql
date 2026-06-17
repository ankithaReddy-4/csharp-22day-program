CREATE PROCEDURE sp_CEOBoardMeetingReport
AS
BEGIN

    SELECT
        COUNT(DISTINCT PatientId) AS TotalActivePatients
    FROM Encounter;
    SELECT TOP 5
        d.Name,
        COUNT(*) AS TotalEncounters
    FROM dbo.Encounter e
    JOIN Department d
        ON e.DepartmentId = d.DepartmentId
    GROUP BY d.Name
    ORDER BY TotalEncounters DESC;
    SELECT
        COUNT(*) AS DeniedClaims,
        SUM(BilledAmount) AS DeniedClaimAmount
    FROM Claim
    WHERE Status = 'Denied';

END;
GO

EXEC sp_CEOBoardMeetingReport;