SELECT

    p.FullName,
	d.Name as DepartmentName,

    COUNT(e.EncounterId) AS EncounterCount,

    RANK()
    OVER (
        ORDER BY COUNT(e.EncounterId) DESC
    ) AS VolumeRank

FROM Provider p

LEFT JOIN Encounter e
    ON e.ProviderId = p.ProviderId

JOIN Department d
	ON d.DepartmentId = p.DepartmentId

GROUP BY
    p.ProviderId,
	d.Name,
    p.FullName;