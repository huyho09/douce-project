USE DOUCE_DB
GO

INSERT INTO Perfume (Id, Name, Brand, Manufacturer, Description, Gender, FragranceFamily, VolumeMl, Concentration, ReleaseYear, CountryOfOrigin, FragranceType, Price, UnitPrice, Currency, StockQuantity, Status, AverageRating, ReviewCount, ImageUrl, CreatedAt, UpdatedAt)
VALUES
(NEWID(), 'Chanel No.5 Eau de Parfum', 'Chanel', 'Chanel', 'A timeless floral fragrance.', 'Female', 'Floral Aldehyde', 100, 'EDP', 1921, 'France', 'Spray', 275.00, '€275.00 per 100 ml', '€', 50, 'Available', 4.8, 1200, './images/per_5.png', GETDATE(), GETDATE()),
(NEWID(), 'Dior Sauvage Eau de Toilette', 'Dior', 'Dior', 'A bold, fresh fragrance.', 'Male', 'Woody Aromatic', 100, 'EDT', 2015, 'France', 'Spray', 99.99, '€99.99 per 100 ml', '€', 75, 'Available', 4.7, 980, './images/per_6.png', GETDATE(), GETDATE()),
(NEWID(), 'Tom Ford Black Orchid', 'Tom Ford', 'Tom Ford', 'A mysterious, sensual scent.', 'Unisex', 'Oriental Floral', 100, 'EDP', 2006, 'USA', 'Spray', 135.00, '€135.00 per 100 ml', '€', 60, 'Available', 4.6, 890, './images/per_7.png', GETDATE(), GETDATE()),
(NEWID(), 'Creed Aventus', 'Creed', 'Creed', 'A powerful and sophisticated scent.', 'Male', 'Chypre Fruity', 100, 'EDP', 2010, 'France', 'Spray', 295.00, '€295.00 per 100 ml', '€', 30, 'Available', 4.9, 1100, './images/per_8.png', GETDATE(), GETDATE()),
(NEWID(), 'YSL Black Opium', 'Yves Saint Laurent', 'YSL', 'An addictive floral-gourmand fragrance.', 'Female', 'Oriental Vanilla', 90, 'EDP', 2014, 'France', 'Spray', 120.00, '€133.33 per 100 ml', '€', 45, 'Available', 4.8, 1050, './images/per_9.png', GETDATE(), GETDATE()),
(NEWID(), 'Jo Malone Peony & Blush Suede', 'Jo Malone', 'Jo Malone', 'A delicate, floral elegance.', 'Female', 'Floral', 100, 'Cologne', 2013, 'UK', 'Spray', 150.00, '€150.00 per 100 ml', '€', 40, 'Available', 4.7, 750, './images/per_10.png', GETDATE(), GETDATE());

select * from Perfume