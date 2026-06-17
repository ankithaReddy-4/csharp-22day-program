CREATE VIEW vw_BillingClaims
AS
SELECT
    ClaimId,
    EncounterId,
    InsuranceId,
    BilledAmount,
    ReimbursedAmt,
    Status
FROM Claim;
GO


CREATE PROCEDURE sp1_RevenueLeakageReport
AS
BEGIN

    SELECT
        Status AS ClaimStatus,

        COUNT(*) AS TotalClaims,

        SUM(BilledAmount) AS TotalBilledAmount,

        SUM(ReimbursedAmt) AS TotalReimbursedAmount,

        SUM(BilledAmount - ReimbursedAmt)
            AS OutstandingAmount,

        RANK() OVER
        (
            ORDER BY
            SUM(BilledAmount - ReimbursedAmt) DESC
        ) AS LossRank

    FROM Claim

    GROUP BY Status

    ORDER BY OutstandingAmount DESC;

END;
GO

EXEC sp1_RevenueLeakageReport;