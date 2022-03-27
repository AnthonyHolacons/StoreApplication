--USE master DROP DATABASE "StoreDB";

CREATE DATABASE "StoreDB";

USE "StoreDB";

CREATE TABLE Customer
(
	[CustomerId] INT NOT NULL IDENTITY(1,1),
	[Dni] CHAR(10) NOT NULL,
	[Name] VARCHAR(50) NOT NULL,
	[LastName] VARCHAR(100),
);
ALTER TABLE Customer ADD CONSTRAINT PK_Customer PRIMARY KEY([CustomerId]);

CREATE TABLE ArticleType
(
	[ArticleTypeId] INT NOT NULL,
	[Description] VARCHAR(25)
);
ALTER TABLE ArticleType ADD CONSTRAINT PK_ArticleType PRIMARY KEY([ArticleTypeId]);

CREATE TABLE Article
(
	[ArticleId] INT NOT NULL ,
	[ArticleTypeId] INT NOT NULL,
	[UnitPrice] DECIMAL(16,4)
);
ALTER TABLE Article ADD CONSTRAINT PK_Article PRIMARY KEY([ArticleId]);
ALTER TABLE Article ADD CONSTRAINT FK_Article_ArticleType FOREIGN KEY([ArticleTypeId]) REFERENCES ArticleType([ArticleTypeId]);

CREATE TABLE Purchase
(
	[PurchaseId] INT NOT NULL IDENTITY(1,1),	
	[ArticleId] INT NOT NULL,
	[CustomerId] INT NOT NULL,
	[Quantity] INT DEFAULT(0),
	[TotalPrice] DECIMAL(16,4)
);

ALTER TABLE Purchase ADD CONSTRAINT PK_Purchase PRIMARY KEY([PurchaseId]);
ALTER TABLE Purchase ADD CONSTRAINT FK_Purchase_Article FOREIGN KEY([ArticleId]) REFERENCES Article([ArticleId]);
ALTER TABLE Purchase ADD CONSTRAINT FK_Purchase_Customer FOREIGN KEY([CustomerId]) REFERENCES Customer([CustomerId]);

BEGIN TRAN

Insert into ArticleType(ArticleTypeId,Description)
VALUES(1,'Pear'),
(2,'Apple'),
(3,'Orange');

INSERT INTO Article(ArticleId,ArticleTypeId,UnitPrice)
VALUES (1,1,2.0),
(2,2,1.0),
(3,3,2.5);

INSERT INTO Customer(Dni,Name,LastName)
VALUES('12345678W','Juan','Roldos'),
('12345678Y','Ana','Milia'),
('12345678Z','Jose','Jose');

INSERT INTO Purchase(ArticleId,CustomerId,Quantity,TotalPrice)
VALUES(1,1,10,20.0),
(1,2,5,5.0),
(1,3,4,7.5);

COMMIT
