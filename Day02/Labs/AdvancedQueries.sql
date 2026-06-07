ALTER TABLE Encounter
ALTER COLUMN ProviderId INT NULL;

SELECT
    d.Name AS Department,
    COUNT(*) AS FluEncounters
FROM Encounter e
JOIN Diagnosis dx
    ON dx.EncounterId = e.EncounterId
JOIN Department d
    ON d.DepartmentId = e.DepartmentId
WHERE dx.IcdCode LIKE 'J1[01]%'
  AND e.AdmitDate >= CAST(GETDATE() AS DATE)
GROUP BY d.Name
ORDER BY FluEncounters DESC;

ALTER TABLE Patient
ADD
    ValidFrom DATETIME2
        GENERATED ALWAYS AS ROW START HIDDEN
        CONSTRAINT DF_Patient_From
        DEFAULT SYSUTCDATETIME(),

    ValidTo DATETIME2
        GENERATED ALWAYS AS ROW END HIDDEN
        CONSTRAINT DF_Patient_To
        DEFAULT '9999-12-31 23:59:59.9999999',

    PERIOD FOR SYSTEM_TIME (ValidFrom, ValidTo);

ALTER TABLE Patient
SET (
    SYSTEM_VERSIONING = ON
    (
        HISTORY_TABLE = dbo.Patient_History
    )
);

UPDATE Patient
SET City = 'Pune'
WHERE PatientId = 1;

SELECT
    PatientId,
    City,
    ValidFrom,
    ValidTo
FROM Patient
FOR SYSTEM_TIME ALL
WHERE PatientId = 1
ORDER BY ValidFrom;


WITH OrderedEncounters AS (

    SELECT

        PatientId,
        EncounterId,
        AdmitDate,
        DischargeDate,

        LAG(DischargeDate)
            OVER (
                PARTITION BY PatientId
                ORDER BY AdmitDate
            ) AS PreviousDischarge

    FROM Encounter

    WHERE EncounterType = 'Inpatient'
)

SELECT

    PatientId,
    EncounterId,
    AdmitDate,
    PreviousDischarge,

    -- Number of days between visits

    DATEDIFF(
        DAY,
        PreviousDischarge,
        AdmitDate
    ) AS DaysBetweenVisits

FROM OrderedEncounters

WHERE PreviousDischarge IS NOT NULL

AND DATEDIFF(
        DAY,
        PreviousDischarge,
        AdmitDate
    ) <= 30;


SELECT

    e.EncounterId,

    d.Name AS Department,

    -- Length of stay for this encounter

    DATEDIFF(
        DAY,
        e.AdmitDate,
        e.DischargeDate
    ) AS LengthOfStay,

    -- Average LOS for all encounters
    -- in the same department

    AVG(
        DATEDIFF(
            DAY,
            e.AdmitDate,
            e.DischargeDate
        )
    )
    OVER (
        PARTITION BY e.DepartmentId
    ) AS DepartmentAverageLOS

FROM Encounter e

JOIN Department d
    ON d.DepartmentId = e.DepartmentId

WHERE e.DischargeDate IS NOT NULL;



SELECT

    p.FullName,

    COUNT(e.EncounterId) AS EncounterCount,

    RANK()
    OVER (
        ORDER BY COUNT(e.EncounterId) DESC
    ) AS VolumeRank

FROM Provider p

LEFT JOIN Encounter e
    ON e.ProviderId = p.ProviderId

GROUP BY
    p.ProviderId,
    p.FullName;


select EncounterType, AdmitDate from [dbo].[Encounter]

order by AdmitDate;

SET STATISTICS IO ON;
SET STATISTICS TIME ON;





