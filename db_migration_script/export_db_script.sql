USE [douce_db]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 3/10/2025 2:05:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 3/10/2025 2:05:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[Id] [uniqueidentifier] NOT NULL,
	[CustomerInfo] [nvarchar](max) NOT NULL,
	[InvoiceItems] [nvarchar](max) NOT NULL,
	[PaymentDate] [nvarchar](50) NOT NULL,
	[PaymentMethod] [nvarchar](200) NOT NULL,
	[Status] [int] NOT NULL,
	[OtherCost] [decimal](18, 2) NOT NULL,
	[Shipping] [decimal](18, 2) NOT NULL,
	[Discount] [decimal](18, 2) NOT NULL,
	[Noted] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Perfume]    Script Date: 3/10/2025 2:05:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Perfume](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Brand] [nvarchar](255) NOT NULL,
	[Manufacturer] [nvarchar](255) NOT NULL,
	[ShortDescription] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Perfumed_Notes] [nvarchar](max) NOT NULL,
	[Flacon] [nvarchar](max) NOT NULL,
	[Composition] [nvarchar](max) NOT NULL,
	[Gender] [nvarchar](10) NOT NULL,
	[FragranceFamily] [nvarchar](100) NOT NULL,
	[VolumeMl] [int] NOT NULL,
	[Ingredients] [nvarchar](max) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[UnitPrice] [nvarchar](50) NOT NULL,
	[Currency] [nvarchar](10) NOT NULL,
	[StockQuantity] [int] NOT NULL,
	[Status] [nvarchar](20) NOT NULL,
	[AverageRating] [decimal](18, 2) NOT NULL,
	[ReviewCount] [int] NOT NULL,
	[ImageUrl] [nvarchar](500) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Perfume] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250228185930_InitialCreate', N'9.0.2')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250305055528_InitialCreate', N'9.0.2')
GO
INSERT [dbo].[Invoice] ([Id], [CustomerInfo], [InvoiceItems], [PaymentDate], [PaymentMethod], [Status], [OtherCost], [Shipping], [Discount], [Noted]) VALUES (N'4436a156-9f7a-440d-bbba-45cffd65138f', N'{"Title":"Dr","FirstName":"Pop","LastName":"Kei","Country":"France","Address":"123 Abc","Email":null,"Phone":null}', N'[{"Product":{"Id":"8e48e9c1-924e-41b3-b34a-438e4b20d22f","Name":"Tom Ford Noir","Brand":"Tom Ford","Manufacturer":"Tom Ford Beauty","ShortDescription":"A bold and mysterious fragrance.","Description":"A deep and sensual blend of oriental spices, florals, and warm woods.","Perfumed_Notes":"Top Notes: Bergamot, Pepper\u003Cbr\u003E\r\nMiddle Notes: Lavender, Geranium\u003Cbr\u003E\r\nBase Notes: Cedar, Vetiver","Flacon":"A sleek black bottle with golden detailing.","Composition":"An intense, captivating composition for an unforgettable presence.","Gender":"Male","FragranceFamily":"Woody","VolumeMl":100,"Ingredients":"[\r\n    {\u0022Name\u0022: \u0022Bergamot\u0022, \u0022ImageUrl\u0022: \u0022/images/bergamot.png\u0022, \u0022Description\u0022: \u0022A bright and fresh citrus note.\u0022},\r\n\r\n    {\u0022Name\u0022: \u0022Lavender\u0022, \u0022ImageUrl\u0022: \u0022/images/lavender.png\u0022, \u0022Description\u0022: \u0022Calming and floral, balancing the fragrance.\u0022}, \r\n    {\u0022Name\u0022: \u0022Pepper\u0022, \u0022ImageUrl\u0022: \u0022/images/pepper.png\u0022, \u0022Description\u0022: \u0022Spicy and bold, adding a fiery contrast.\u0022} \r\n]","Price":220.00,"UnitPrice":"\u20AC220.00 per 100ml","Currency":"\u20AC","StockQuantity":15,"Status":"Available","AverageRating":4.60,"ReviewCount":1750,"ImageUrl":"/images/dior-sauvage-elixir.jpg","CreatedAt":"2025-03-01T22:04:31.63","UpdatedAt":"2025-03-01T22:04:31.63"},"Quantity":1}]', N'2025-03-10 06-51', N'Cash', 3, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'')
GO
INSERT [dbo].[Perfume] ([Id], [Name], [Brand], [Manufacturer], [ShortDescription], [Description], [Perfumed_Notes], [Flacon], [Composition], [Gender], [FragranceFamily], [VolumeMl], [Ingredients], [Price], [UnitPrice], [Currency], [StockQuantity], [Status], [AverageRating], [ReviewCount], [ImageUrl], [CreatedAt], [UpdatedAt]) VALUES (N'c2b3770c-edb7-4e93-97dd-30c9db06c1d7', N'Armani Code', N'Giorgio Armani', N'Armani Beauty', N'A seductive and sophisticated masculine scent.', N'A charismatic blend of citrus, spice, and smooth woods.', N'Top Notes: Bergamot, Pepper<br>
Middle Notes: Lavender, Geranium<br>
Base Notes: Cedar, Vetiver', N'A sleek dark bottle exuding mystery and elegance.', N'A powerful yet smooth composition with a balance of freshness and warmth.', N'Male', N'Woody', 75, N'[
    {"Name": "Bergamot", "ImageUrl": "/images/bergamot.png", "Description": "A bright and fresh citrus note."},

    {"Name": "Lavender", "ImageUrl": "/images/lavender.png", "Description": "Calming and floral, balancing the fragrance."}, 
    {"Name": "Pepper", "ImageUrl": "/images/pepper.png", "Description": "Spicy and bold, adding a fiery contrast."} 
]', CAST(140.00 AS Decimal(18, 2)), N'€186.67 per 100ml', N'€', 40, N'Available', CAST(4.60 AS Decimal(18, 2)), 1800, N'/images/dior-sauvage-elixir.jpg', CAST(N'2025-03-01T22:04:31.6300000' AS DateTime2), CAST(N'2025-03-01T22:04:31.6300000' AS DateTime2))
GO
INSERT [dbo].[Perfume] ([Id], [Name], [Brand], [Manufacturer], [ShortDescription], [Description], [Perfumed_Notes], [Flacon], [Composition], [Gender], [FragranceFamily], [VolumeMl], [Ingredients], [Price], [UnitPrice], [Currency], [StockQuantity], [Status], [AverageRating], [ReviewCount], [ImageUrl], [CreatedAt], [UpdatedAt]) VALUES (N'8e48e9c1-924e-41b3-b34a-438e4b20d22f', N'Tom Ford Noir', N'Tom Ford', N'Tom Ford Beauty', N'A bold and mysterious fragrance.', N'A deep and sensual blend of oriental spices, florals, and warm woods.', N'Top Notes: Bergamot, Pepper<br>
Middle Notes: Lavender, Geranium<br>
Base Notes: Cedar, Vetiver', N'A sleek black bottle with golden detailing.', N'An intense, captivating composition for an unforgettable presence.', N'Male', N'Woody', 100, N'[
    {"Name": "Bergamot", "ImageUrl": "/images/bergamot.png", "Description": "A bright and fresh citrus note."},

    {"Name": "Lavender", "ImageUrl": "/images/lavender.png", "Description": "Calming and floral, balancing the fragrance."}, 
    {"Name": "Pepper", "ImageUrl": "/images/pepper.png", "Description": "Spicy and bold, adding a fiery contrast."} 
]', CAST(220.00 AS Decimal(18, 2)), N'€220.00 per 100ml', N'€', 15, N'Available', CAST(4.60 AS Decimal(18, 2)), 1750, N'/images/dior-sauvage-elixir.jpg', CAST(N'2025-03-01T22:04:31.6300000' AS DateTime2), CAST(N'2025-03-01T22:04:31.6300000' AS DateTime2))
GO
INSERT [dbo].[Perfume] ([Id], [Name], [Brand], [Manufacturer], [ShortDescription], [Description], [Perfumed_Notes], [Flacon], [Composition], [Gender], [FragranceFamily], [VolumeMl], [Ingredients], [Price], [UnitPrice], [Currency], [StockQuantity], [Status], [AverageRating], [ReviewCount], [ImageUrl], [CreatedAt], [UpdatedAt]) VALUES (N'bbf8a160-e683-4030-9236-536de44f6d99', N'Chanel No. 5', N'Chanel', N'Chanel Parfums', N'An iconic and timeless fragrance.', N'A legendary floral-aldehyde perfume that defines elegance and sophistication.', N'Top Notes: Bergamot, Pepper<br>
Middle Notes: Lavender, Geranium<br>
Base Notes: Cedar, Vetiver', N'A simple yet luxurious bottle with golden hues.', N'An intricate balance of soft florals and rich aldehydes with creamy undertones.', N'Female', N'Floral', 50, N'[
    {"Name": "Bergamot", "ImageUrl": "/images/bergamot.png", "Description": "A bright and fresh citrus note."},

    {"Name": "Lavender", "ImageUrl": "/images/lavender.png", "Description": "Calming and floral, balancing the fragrance."}, 
    {"Name": "Pepper", "ImageUrl": "/images/pepper.png", "Description": "Spicy and bold, adding a fiery contrast."} 
]', CAST(180.00 AS Decimal(18, 2)), N'€180.00 per 50ml', N'€', 30, N'Available', CAST(4.90 AS Decimal(18, 2)), 3200, N'/images/dior-sauvage-elixir.jpg', CAST(N'2025-03-01T22:04:31.6300000' AS DateTime2), CAST(N'2025-03-01T22:04:31.6300000' AS DateTime2))
GO
INSERT [dbo].[Perfume] ([Id], [Name], [Brand], [Manufacturer], [ShortDescription], [Description], [Perfumed_Notes], [Flacon], [Composition], [Gender], [FragranceFamily], [VolumeMl], [Ingredients], [Price], [UnitPrice], [Currency], [StockQuantity], [Status], [AverageRating], [ReviewCount], [ImageUrl], [CreatedAt], [UpdatedAt]) VALUES (N'854c21ee-4e6e-4780-ada8-5f69aff8e47f', N'Maison Francis Kurkdjian Baccarat Rouge 540', N'Maison Francis Kurkdjian', N'MFK Parfums', N'A luxurious and radiant amber floral scent.', N'A warm, luminous fragrance with a blend of spicy saffron, sweet jasmine, and woody ambergris.', N'Top Notes: Bergamot, Pepper<br>
Middle Notes: Lavender, Geranium<br>
Base Notes: Cedar, Vetiver', N'A transparent glass bottle with a golden cap.', N'A harmonious balance of sweetness, spice, and rich woods.', N'Unisex', N'Floral', 70, N'[
    {"Name": "Bergamot", "ImageUrl": "/images/bergamot.png", "Description": "A bright and fresh citrus note."},

    {"Name": "Lavender", "ImageUrl": "/images/lavender.png", "Description": "Calming and floral, balancing the fragrance."}, 
    {"Name": "Pepper", "ImageUrl": "/images/pepper.png", "Description": "Spicy and bold, adding a fiery contrast."} 
]', CAST(320.00 AS Decimal(18, 2)), N'€457.14 per 100ml', N'€', 10, N'Available', CAST(5.00 AS Decimal(18, 2)), 5000, N'/images/dior-sauvage-elixir.jpg', CAST(N'2025-03-01T22:04:31.6300000' AS DateTime2), CAST(N'2025-03-01T22:04:31.6300000' AS DateTime2))
GO
INSERT [dbo].[Perfume] ([Id], [Name], [Brand], [Manufacturer], [ShortDescription], [Description], [Perfumed_Notes], [Flacon], [Composition], [Gender], [FragranceFamily], [VolumeMl], [Ingredients], [Price], [UnitPrice], [Currency], [StockQuantity], [Status], [AverageRating], [ReviewCount], [ImageUrl], [CreatedAt], [UpdatedAt]) VALUES (N'90e86a90-5caf-4078-970a-608cf8999fc0', N'Dior Sauvage', N'Dior', N'Christian Dior', N'A fresh and spicy masculine fragrance.', N'A rugged yet sophisticated scent, combining citrus, spice, and deep woody notes.', N'Top Notes: Bergamot, Pepper<br>
Middle Notes: Lavender, Geranium<br>
Base Notes: Cedar, Vetiver', N'A dark blue gradient bottle representing the wilderness.', N'Raw and powerful, blending fresh citrus with warm, spicy undertones.', N'Male', N'Opulent', 100, N'[
    {"Name": "Bergamot", "ImageUrl": "/images/bergamot.png", "Description": "A bright and fresh citrus note."},

    {"Name": "Lavender", "ImageUrl": "/images/lavender.png", "Description": "Calming and floral, balancing the fragrance."}, 
    {"Name": "Pepper", "ImageUrl": "/images/pepper.png", "Description": "Spicy and bold, adding a fiery contrast."} 
]', CAST(150.00 AS Decimal(18, 2)), N'€150.00 per 100ml', N'€', 75, N'Available', CAST(4.80 AS Decimal(18, 2)), 2100, N'/images/dior-sauvage-elixir.jpg', CAST(N'2025-03-01T22:04:31.6300000' AS DateTime2), CAST(N'2025-03-01T22:04:31.6300000' AS DateTime2))
GO
INSERT [dbo].[Perfume] ([Id], [Name], [Brand], [Manufacturer], [ShortDescription], [Description], [Perfumed_Notes], [Flacon], [Composition], [Gender], [FragranceFamily], [VolumeMl], [Ingredients], [Price], [UnitPrice], [Currency], [StockQuantity], [Status], [AverageRating], [ReviewCount], [ImageUrl], [CreatedAt], [UpdatedAt]) VALUES (N'd1665d2a-8b02-494c-adcf-65edcc2ab13b', N'Gucci Bloom', N'Gucci', N'Gucci Parfums', N'A lush white floral scent.', N'A natural and elegant fragrance, capturing the essence of blooming flowers.', N'Top Notes: Bergamot, Pepper<br>
Middle Notes: Lavender, Geranium<br>
Base Notes: Cedar, Vetiver', N'An elegant floral-designed bottle representing nature and femininity.', N'A rich floral composition evoking a blooming garden.', N'Female', N'Floral', 100, N'[
    {"Name": "Bergamot", "ImageUrl": "/images/bergamot.png", "Description": "A bright and fresh citrus note."},

    {"Name": "Lavender", "ImageUrl": "/images/lavender.png", "Description": "Calming and floral, balancing the fragrance."}, 
    {"Name": "Pepper", "ImageUrl": "/images/pepper.png", "Description": "Spicy and bold, adding a fiery contrast."} 
]', CAST(160.00 AS Decimal(18, 2)), N'€160.00 per 100ml', N'€', 35, N'Available', CAST(4.70 AS Decimal(18, 2)), 2100, N'/images/dior-sauvage-elixir.jpg', CAST(N'2025-03-01T22:04:31.6300000' AS DateTime2), CAST(N'2025-03-01T22:04:31.6300000' AS DateTime2))
GO
INSERT [dbo].[Perfume] ([Id], [Name], [Brand], [Manufacturer], [ShortDescription], [Description], [Perfumed_Notes], [Flacon], [Composition], [Gender], [FragranceFamily], [VolumeMl], [Ingredients], [Price], [UnitPrice], [Currency], [StockQuantity], [Status], [AverageRating], [ReviewCount], [ImageUrl], [CreatedAt], [UpdatedAt]) VALUES (N'f431aae5-395a-49ef-a5f1-a536a25ccc0f', N'Creed Aventus', N'Creed', N'Creed Fragrances', N'A bold and sophisticated chypre scent.', N'An iconic fragrance of success, combining fruity and smoky elements.', N'Top Notes: Bergamot, Pepper<br>
Middle Notes: Lavender, Geranium<br>
Base Notes: Cedar, Vetiver', N'A heavy glass bottle with a regal black and silver crest.', N'A well-balanced blend of fresh, floral, and woody notes.', N'Male', N'Fresh', 100, N'[
    {"Name": "Bergamot", "ImageUrl": "/images/bergamot.png", "Description": "A bright and fresh citrus note."},

    {"Name": "Lavender", "ImageUrl": "/images/lavender.png", "Description": "Calming and floral, balancing the fragrance."}, 
    {"Name": "Pepper", "ImageUrl": "/images/pepper.png", "Description": "Spicy and bold, adding a fiery contrast."} 
]', CAST(300.00 AS Decimal(18, 2)), N'€300.00 per 100ml', N'€', 25, N'Available', CAST(4.90 AS Decimal(18, 2)), 3500, N'/images/dior-sauvage-elixir.jpg', CAST(N'2025-03-01T22:04:31.6300000' AS DateTime2), CAST(N'2025-03-01T22:04:31.6300000' AS DateTime2))
GO
INSERT [dbo].[Perfume] ([Id], [Name], [Brand], [Manufacturer], [ShortDescription], [Description], [Perfumed_Notes], [Flacon], [Composition], [Gender], [FragranceFamily], [VolumeMl], [Ingredients], [Price], [UnitPrice], [Currency], [StockQuantity], [Status], [AverageRating], [ReviewCount], [ImageUrl], [CreatedAt], [UpdatedAt]) VALUES (N'44d41f3a-8f01-416d-a66b-afb5f3fe39b3', N'Le Labo Santal 33', N'Le Labo', N'Le Labo Fragrances', N'A distinctive smoky sandalwood scent.', N'An iconic unisex fragrance with spicy, woody, and leathery notes.', N'Top Notes: Bergamot, Pepper<br>
Middle Notes: Lavender, Geranium<br>
Base Notes: Cedar, Vetiver', N'A minimalist glass bottle with a personalized label.', N'A blend of smooth woods, spices, and leather for a lasting impression.', N'Unisex', N'Woody', 50, N'[
    {"Name": "Bergamot", "ImageUrl": "/images/bergamot.png", "Description": "A bright and fresh citrus note."},

    {"Name": "Lavender", "ImageUrl": "/images/lavender.png", "Description": "Calming and floral, balancing the fragrance."}, 
    {"Name": "Pepper", "ImageUrl": "/images/pepper.png", "Description": "Spicy and bold, adding a fiery contrast."} 
]', CAST(210.00 AS Decimal(18, 2)), N'€210.00 per 50ml', N'€', 20, N'Available', CAST(4.80 AS Decimal(18, 2)), 1900, N'/images/dior-sauvage-elixir.jpg', CAST(N'2025-03-01T22:04:31.6300000' AS DateTime2), CAST(N'2025-03-01T22:04:31.6300000' AS DateTime2))
GO
INSERT [dbo].[Perfume] ([Id], [Name], [Brand], [Manufacturer], [ShortDescription], [Description], [Perfumed_Notes], [Flacon], [Composition], [Gender], [FragranceFamily], [VolumeMl], [Ingredients], [Price], [UnitPrice], [Currency], [StockQuantity], [Status], [AverageRating], [ReviewCount], [ImageUrl], [CreatedAt], [UpdatedAt]) VALUES (N'8368e163-3f60-4870-a10d-d0d134a12748', N'Black Opium', N'Yves Saint Laurent', N'YSL Beauty', N'A bold and seductive scent.', N'A warm, sensual fragrance featuring coffee, vanilla, and floral notes.', N'Top Notes: Bergamot, Pepper<br>
Middle Notes: Lavender, Geranium<br>
Base Notes: Cedar, Vetiver', N'A sleek black bottle with shimmering accents.', N'An intoxicating blend of rich coffee and creamy vanilla with floral undertones.', N'Female', N'Floral', 90, N'[
    {"Name": "Bergamot", "ImageUrl": "/images/bergamot.png", "Description": "A bright and fresh citrus note."},

    {"Name": "Lavender", "ImageUrl": "/images/lavender.png", "Description": "Calming and floral, balancing the fragrance."}, 
    {"Name": "Pepper", "ImageUrl": "/images/pepper.png", "Description": "Spicy and bold, adding a fiery contrast."} 
]', CAST(120.00 AS Decimal(18, 2)), N'€120.00 per 90ml', N'€', 50, N'Available', CAST(4.70 AS Decimal(18, 2)), 1500, N'/images/dior-sauvage-elixir.jpg', CAST(N'2025-03-01T22:04:31.6300000' AS DateTime2), CAST(N'2025-03-01T22:04:31.6300000' AS DateTime2))
GO
INSERT [dbo].[Perfume] ([Id], [Name], [Brand], [Manufacturer], [ShortDescription], [Description], [Perfumed_Notes], [Flacon], [Composition], [Gender], [FragranceFamily], [VolumeMl], [Ingredients], [Price], [UnitPrice], [Currency], [StockQuantity], [Status], [AverageRating], [ReviewCount], [ImageUrl], [CreatedAt], [UpdatedAt]) VALUES (N'c61140ee-47b8-413b-b3aa-dc1e30c18bdd', N'Jo Malone Wood Sage & Sea Salt', N'Jo Malone', N'Jo Malone London', N'A fresh and earthy marine scent.', N'A unique fragrance inspired by salty sea air and aromatic herbs.', N'Top Notes: Bergamot, Pepper<br>
Middle Notes: Lavender, Geranium<br>
Base Notes: Cedar, Vetiver', N'A minimalist, clear bottle with a silver cap.', N'A refreshing and sophisticated balance of herbal and aquatic notes.', N'Unisex', N'Woody', 50, N'[  
    {"Name": "Ambrette Seeds", "ImageUrl": "/images/ambrette_seeds.png", "Description": "A warm, musky note with subtle sweetness."},  
    {"Name": "Sea Salt", "ImageUrl": "/images/sea_salt.png", "Description": "A mineral, fresh note reminiscent of ocean air."},  
    {"Name": "Sage", "ImageUrl": "/images/sage.png", "Description": "A herbal, earthy note with aromatic depth."}  
]', CAST(180.00 AS Decimal(18, 2)), N'€180.00 per 50ml', N'€', 50, N'Available', CAST(4.80 AS Decimal(18, 2)), 2200, N'/images/dior-sauvage-elixir.jpg', CAST(N'2025-03-01T22:04:31.6300000' AS DateTime2), CAST(N'2025-03-01T22:04:31.6300000' AS DateTime2))
GO
USE [master]
GO
ALTER DATABASE [douce_db] SET  READ_WRITE 
GO
