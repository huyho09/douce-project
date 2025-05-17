-- add new column
ALTER TABLE Perfume
ADD DescriptionImages NVARCHAR(MAX) NULL DEFAULT '[]';

-- change allow null for columns
ALTER TABLE dbo.Perfume ALTER COLUMN Brand nvarchar(100) NULL;
ALTER TABLE dbo.Perfume ALTER COLUMN Manufacturer nvarchar(100) NULL;
ALTER TABLE dbo.Perfume ALTER COLUMN ShortDescription nvarchar(300) NULL;
ALTER TABLE dbo.Perfume ALTER COLUMN Description nvarchar(MAX) NULL;
ALTER TABLE dbo.Perfume ALTER COLUMN Composition nvarchar(MAX) NULL;
ALTER TABLE dbo.Perfume ALTER COLUMN Ingredients nvarchar(MAX) NULL;
ALTER TABLE dbo.Perfume ALTER COLUMN FragranceNotes nvarchar(MAX) NULL;
ALTER TABLE dbo.Perfume ALTER COLUMN TopPerfumed nvarchar(100) NULL;
ALTER TABLE dbo.Perfume ALTER COLUMN MiddlePerfumed nvarchar(100) NULL;
ALTER TABLE dbo.Perfume ALTER COLUMN BasePerfumed nvarchar(100) NULL;
ALTER TABLE dbo.Perfume ALTER COLUMN PriceInfo nvarchar(MAX) NULL;
ALTER TABLE dbo.Perfume ALTER COLUMN Status nvarchar(MAX) NULL;
ALTER TABLE dbo.Perfume ALTER COLUMN ImageUrl nvarchar(MAX) NULL;
ALTER TABLE dbo.Perfume ALTER COLUMN DescriptionImages nvarchar(MAX) NULL;
