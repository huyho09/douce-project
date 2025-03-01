USE DOUCE_DB
GO

INSERT INTO [Perfume] ([Id], [Name], [Brand], [Manufacturer], [ShortDescription], [Description], [Perfumed_Notes], [Flacon], [Composition], [Gender], [FragranceFamily], [VolumeMl], [Ingredients], [Price], [UnitPrice], [Currency], [StockQuantity], [Status], [AverageRating], [ReviewCount], [ImageUrl], [CreatedAt], [UpdatedAt])  
VALUES  
(NEWID(), 'Black Opium', 'Yves Saint Laurent', 'YSL Beauty', 'A bold and seductive scent.', 
'A warm, sensual fragrance featuring coffee, vanilla, and floral notes.', 
N'{"Top": ["Coffee", "Orange Blossom"], "Middle": ["Jasmine", "Vanilla"], "Base": ["Cedar", "Patchouli"]}', 
'A sleek black bottle with shimmering accents.', 
'An intoxicating blend of rich coffee and creamy vanilla with floral undertones.', 
'Female', 'Floral Gourmand', 90,  
N'[
    {"Name": "Coffee", "ImageUrl": "/images/coffee.png", "Description": "Deep, roasted coffee note for warmth."},
    {"Name": "Vanilla", "ImageUrl": "/images/vanilla.png", "Description": "A creamy, sweet note with a long-lasting effect."},
    {"Name": "Orange Blossom", "ImageUrl": "/images/orange_blossom.png", "Description": "Fresh floral note with citrusy hints."}
]',  
120.00, '€120.00 per 90ml', '€', 50, 'Available', 4.7, 1500, '/images/black_opium.png', GETDATE(), GETDATE()),  

(NEWID(), 'Dior Sauvage', 'Dior', 'Christian Dior', 'A fresh and spicy masculine fragrance.', 
'A rugged yet sophisticated scent, combining citrus, spice, and deep woody notes.', 
N'{"Top": ["Bergamot", "Pepper"], "Middle": ["Lavender", "Geranium"], "Base": ["Cedar", "Vetiver"]}', 
'A dark blue gradient bottle representing the wilderness.', 
'Raw and powerful, blending fresh citrus with warm, spicy undertones.', 
'Male', 'Aromatic Fougere', 100,  
N'[
    {"Name": "Bergamot", "ImageUrl": "/images/bergamot.png", "Description": "A bright and fresh citrus note."},
    {"Name": "Pepper", "ImageUrl": "/images/pepper.png", "Description": "Spicy and bold, adding a fiery contrast."},
    {"Name": "Lavender", "ImageUrl": "/images/lavender.png", "Description": "Calming and floral, balancing the fragrance."}
]',  
150.00, '€150.00 per 100ml', '€', 75, 'Available', 4.8, 2100, '/images/dior_sauvage.png', GETDATE(), GETDATE()),  

(NEWID(), 'Chanel No. 5', 'Chanel', 'Chanel Parfums', 'An iconic and timeless fragrance.', 
'A legendary floral-aldehyde perfume that defines elegance and sophistication.', 
N'{"Top": ["Aldehydes", "Neroli"], "Middle": ["Jasmine", "Rose"], "Base": ["Sandalwood", "Vanilla"]}', 
'A simple yet luxurious bottle with golden hues.', 
'An intricate balance of soft florals and rich aldehydes with creamy undertones.', 
'Female', 'Floral Aldehyde', 50,  
N'[
    {"Name": "Aldehydes", "ImageUrl": "/images/aldehydes.png", "Description": "A fresh, sparkling note for a clean feel."},
    {"Name": "Jasmine", "ImageUrl": "/images/jasmine.png", "Description": "A rich, opulent floral heart."},
    {"Name": "Sandalwood", "ImageUrl": "/images/sandalwood.png", "Description": "A warm, woody base for longevity."}
]',  
180.00, '€180.00 per 50ml', '€', 30, 'Available', 4.9, 3200, '/images/chanel_no5.png', GETDATE(), GETDATE()),  

(NEWID(), 'Le Labo Santal 33', 'Le Labo', 'Le Labo Fragrances', 'A distinctive smoky sandalwood scent.', 
'An iconic unisex fragrance with spicy, woody, and leathery notes.', 
N'{"Top": ["Cardamom", "Iris"], "Middle": ["Sandalwood", "Cedar"], "Base": ["Leather", "Musk"]}', 
'A minimalist glass bottle with a personalized label.', 
'A blend of smooth woods, spices, and leather for a lasting impression.', 
'Unisex', 'Woody Spicy', 50,  
N'[
    {"Name": "Sandalwood", "ImageUrl": "/images/sandalwood.png", "Description": "A creamy and rich woody note."},
    {"Name": "Cardamom", "ImageUrl": "/images/cardamom.png", "Description": "A spicy, aromatic essence."},
    {"Name": "Leather", "ImageUrl": "/images/leather.png", "Description": "A smoky and bold accent for depth."}
]',  
210.00, '€210.00 per 50ml', '€', 20, 'Available', 4.8, 1900, '/images/le_labo_santal.png', GETDATE(), GETDATE()),  

(NEWID(), 'Tom Ford Noir', 'Tom Ford', 'Tom Ford Beauty', 'A bold and mysterious fragrance.', 
'A deep and sensual blend of oriental spices, florals, and warm woods.', 
N'{"Top": ["Bergamot", "Violet"], "Middle": ["Black Pepper", "Nutmeg"], "Base": ["Amber", "Vanilla"]}', 
'A sleek black bottle with golden detailing.', 
'An intense, captivating composition for an unforgettable presence.', 
'Male', 'Oriental Woody', 100,  
N'[
    {"Name": "Bergamot", "ImageUrl": "/images/bergamot.png", "Description": "A fresh and citrusy opening."},
    {"Name": "Black Pepper", "ImageUrl": "/images/black_pepper.png", "Description": "A spicy and bold heart note."},
    {"Name": "Amber", "ImageUrl": "/images/amber.png", "Description": "A warm and resinous base with a smooth finish."}
]',  
220.00, '€220.00 per 100ml', '€', 15, 'Available', 4.6, 1750, '/images/tom_ford_noir.png', GETDATE(), GETDATE()),
(NEWID(), 'Gucci Bloom', 'Gucci', 'Gucci Parfums', 'A lush white floral scent.', 
'A natural and elegant fragrance, capturing the essence of blooming flowers.', 
N'{"Top": ["Rangoon Creeper"], "Middle": ["Tuberose"], "Base": ["Jasmine"]}', 
'An elegant floral-designed bottle representing nature and femininity.', 
'A rich floral composition evoking a blooming garden.', 
'Female', 'Floral', 100,  
N'[  
    {"Name": "Rangoon Creeper", "ImageUrl": "/images/rangoon_creeper.png", "Description": "A unique floral note that deepens over time."},  
    {"Name": "Tuberose", "ImageUrl": "/images/tuberose.png", "Description": "A creamy, narcotic floral with great depth."},  
    {"Name": "Jasmine", "ImageUrl": "/images/jasmine.png", "Description": "A soft, romantic white floral note."}  
]',  
160.00, '€160.00 per 100ml', '€', 35, 'Available', 4.7, 2100, '/images/gucci_bloom.png', GETDATE(), GETDATE()),  

(NEWID(), 'Armani Code', 'Giorgio Armani', 'Armani Beauty', 'A seductive and sophisticated masculine scent.', 
'A charismatic blend of citrus, spice, and smooth woods.', 
N'{"Top": ["Lemon", "Bergamot"], "Middle": ["Star Anise", "Olive Blossom"], "Base": ["Leather", "Tonka Bean"]}', 
'A sleek dark bottle exuding mystery and elegance.', 
'A powerful yet smooth composition with a balance of freshness and warmth.', 
'Male', 'Oriental Spicy', 75,  
N'[  
    {"Name": "Lemon", "ImageUrl": "/images/lemon.png", "Description": "A bright and zesty opening citrus note."},  
    {"Name": "Olive Blossom", "ImageUrl": "/images/olive_blossom.png", "Description": "A unique floral accent with a hint of spice."},  
    {"Name": "Tonka Bean", "ImageUrl": "/images/tonka_bean.png", "Description": "A warm, slightly sweet gourmand note."}  
]',  
140.00, '€186.67 per 100ml', '€', 40, 'Available', 4.6, 1800, '/images/armani_code.png', GETDATE(), GETDATE()),  

(NEWID(), 'Creed Aventus', 'Creed', 'Creed Fragrances', 'A bold and sophisticated chypre scent.', 
'An iconic fragrance of success, combining fruity and smoky elements.', 
N'{"Top": ["Pineapple", "Blackcurrant"], "Middle": ["Rose", "Birch"], "Base": ["Oakmoss", "Musk"]}', 
'A heavy glass bottle with a regal black and silver crest.', 
'A well-balanced blend of fresh, floral, and woody notes.', 
'Male', 'Chypre Fruity', 100,  
N'[  
    {"Name": "Pineapple", "ImageUrl": "/images/pineapple.png", "Description": "A juicy, exotic note that adds brightness."},  
    {"Name": "Birch", "ImageUrl": "/images/birch.png", "Description": "A smoky, woody note for depth and intensity."},  
    {"Name": "Musk", "ImageUrl": "/images/musk.png", "Description": "A sensual and lingering base note."}  
]',  
300.00, '€300.00 per 100ml', '€', 25, 'Available', 4.9, 3500, '/images/creed_aventus.png', GETDATE(), GETDATE()),  
(NEWID(), 'Jo Malone Wood Sage & Sea Salt', 'Jo Malone', 'Jo Malone London', 'A fresh and earthy marine scent.', 
'A unique fragrance inspired by salty sea air and aromatic herbs.', 
N'{"Top": ["Ambrette Seeds"], "Middle": ["Sea Salt"], "Base": ["Sage"]}', 
'A minimalist, clear bottle with a silver cap.', 
'A refreshing and sophisticated balance of herbal and aquatic notes.', 
'Unisex', 'Woody Aquatic', 50,  
N'[  
    {"Name": "Ambrette Seeds", "ImageUrl": "/images/ambrette_seeds.png", "Description": "A warm, musky note with subtle sweetness."},  
    {"Name": "Sea Salt", "ImageUrl": "/images/sea_salt.png", "Description": "A mineral, fresh note reminiscent of ocean air."},  
    {"Name": "Sage", "ImageUrl": "/images/sage.png", "Description": "A herbal, earthy note with aromatic depth."}  
]',  
180.00, '€180.00 per 50ml', '€', 50, 'Available', 4.8, 2200, '/images/jo_malone_wood_sage.png', GETDATE(), GETDATE()),  

(NEWID(), 'Maison Francis Kurkdjian Baccarat Rouge 540', 'Maison Francis Kurkdjian', 'MFK Parfums', 'A luxurious and radiant amber floral scent.', 
'A warm, luminous fragrance with a blend of spicy saffron, sweet jasmine, and woody ambergris.', 
N'{"Top": ["Saffron", "Jasmine"], "Middle": ["Amberwood", "Ambergris"], "Base": ["Cedar", "Fir Resin"]}', 
'A transparent glass bottle with a golden cap.', 
'A harmonious balance of sweetness, spice, and rich woods.', 
'Unisex', 'Amber Floral', 70,  
N'[  
    {"Name": "Saffron", "ImageUrl": "/images/saffron.png", "Description": "A warm, slightly spicy note with exotic richness."},  
    {"Name": "Ambergris", "ImageUrl": "/images/ambergris.png", "Description": "A deep, oceanic scent with a musky finish."},  
    {"Name": "Fir Resin", "ImageUrl": "/images/fir_resin.png", "Description": "A balsamic, pine-like note with earthy depth."}  
]',  
320.00, '€457.14 per 100ml', '€', 10, 'Available', 5.0, 5000, '/images/mfk_baccarat_rouge.png', GETDATE(), GETDATE());

select * from Perfume