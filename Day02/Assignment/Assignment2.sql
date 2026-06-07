ALTER TABLE Insurance
ADD
    ValidFrom DATETIME2
        GENERATED ALWAYS AS ROW START HIDDEN
        CONSTRAINT DF_Insurance_From
        DEFAULT SYSUTCDATETIME(),

    ValidTo DATETIME2
        GENERATED ALWAYS AS ROW END HIDDEN
        CONSTRAINT DF_Insurance_To
        DEFAULT '9999-12-31 23:59:59.9999999',

    PERIOD FOR SYSTEM_TIME (ValidFrom, ValidTo);


ALTER TABLE Insurance
SET (
    SYSTEM_VERSIONING = ON
    (
        HISTORY_TABLE = dbo.Insurance_History
    )
);

UPDATE Insurance SET Payer = 'HDFC Ergo' WHERE PatientId = 1;

SELECT
	PatientId,
    InsuranceId,
    Payer,
	PolicyNumber,
    ValidFrom,
    ValidTo
FROM Insurance
FOR SYSTEM_TIME ALL
WHERE InsuranceId = 1
ORDER BY ValidFrom;



