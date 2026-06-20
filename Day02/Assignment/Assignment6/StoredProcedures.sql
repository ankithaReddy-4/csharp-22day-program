CREATE VIEW vw_Billing
AS
SELECT
    ClaimId,
    InsuranceId,
    BilledAmount,
    ReimbursedAmt,
    Status
FROM Claim;
GO


CREATE VIEW vw_Analytics_DeId
AS
SELECT
    CASE
        WHEN DATEDIFF(YEAR, p.DateOfBirth, GETDATE()) < 18
            THEN '0-17'

        WHEN DATEDIFF(YEAR, p.DateOfBirth, GETDATE()) BETWEEN 18 AND 35
            THEN '18-35'

        WHEN DATEDIFF(YEAR, p.DateOfBirth, GETDATE()) BETWEEN 36 AND 55
            THEN '36-55'

        ELSE '56+'
    END AS AgeBand,

    e.EncounterType

FROM Patient p
INNER JOIN Encounter e
    ON p.PatientId = e.PatientId;
GO


SELECT TOP 5 * FROM vw_Clinical;