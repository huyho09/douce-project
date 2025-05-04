SELECT 
    i.name AS IndexName,
    i.type_desc AS IndexType,
    i.is_unique,
    i.is_primary_key,
    i.fill_factor,
    c.name AS ColumnName,
    ic.is_included_column,
    ic.key_ordinal
FROM sys.indexes i
JOIN sys.index_columns ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id
JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
JOIN sys.tables t ON i.object_id = t.object_id
WHERE t.name = 'Perfume'
ORDER BY i.name, ic.key_ordinal;

SELECT 
    OBJECT_NAME(i.object_id) AS TableName,
    i.name AS IndexName,
    i.index_id,
    user_seeks,
    user_scans,
    user_lookups,
    user_updates,
    last_user_seek,
    last_user_scan
FROM sys.dm_db_index_usage_stats s
JOIN sys.indexes i ON i.object_id = s.object_id AND i.index_id = s.index_id
WHERE OBJECT_NAME(i.object_id) = 'Perfume';

SELECT 
    OBJECT_NAME(ps.object_id) AS TableName,
    i.name AS IndexName,
    ips.index_type_desc,
    ips.avg_fragmentation_in_percent,
    ips.page_count
FROM sys.dm_db_index_physical_stats(DB_ID(), OBJECT_ID('Perfume'), NULL, NULL, 'LIMITED') ips
JOIN sys.indexes i ON i.object_id = ips.object_id AND i.index_id = ips.index_id
JOIN sys.partitions ps ON ps.object_id = ips.object_id AND ps.index_id = ips.index_id
WHERE ips.page_count > 100; -- Ignore small indexes

-- remove index
-- DROP INDEX IX_Perfume_Name ON Perfume;
-- DROP INDEX IX_Perfume_ShortDescription ON Perfume;

/****** Object:  Index [IX_Perfume_Name]    Script Date: 5/4/2025 10:29:42 PM ******/
--CREATE NONCLUSTERED INDEX [IX_Perfume_Name] ON [dbo].[Perfume]
--(
--	[Name] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO
--SET ANSI_PADDING ON
--GO
--/****** Object:  Index [IX_Perfume_ShortDescription]    Script Date: 5/4/2025 10:29:42 PM ******/
--CREATE NONCLUSTERED INDEX [IX_Perfume_ShortDescription] ON [dbo].[Perfume]
--(
--	[ShortDescription] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO

ALTER TABLE Perfume
ADD MinPrice AS CAST(JSON_VALUE(PriceInfo, '$[0].Price') AS DECIMAL(18,2)) PERSISTED,
    MinVolume AS CAST(JSON_VALUE(PriceInfo, '$[0].VolumeMl') AS INT) PERSISTED;

CREATE INDEX IX_Perfume_MinPrice ON Perfume(MinPrice);

