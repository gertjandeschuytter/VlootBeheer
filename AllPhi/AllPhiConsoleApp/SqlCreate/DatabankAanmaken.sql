CREATE DATABASE Fleet

GO

USE [Fleet];
CREATE TABLE [dbo].[Adres](
	[AdresId] [int] IDENTITY(1,1) NOT NULL,
	[Straat] [nvarchar](50) NOT NULL,
	[Huisnummer] [nvarchar](10) NOT NULL,
	[Gemeente] [nvarchar](50) NOT NULL,
	[Postcode] [int] NOT NULL,
 CONSTRAINT [PK_Adres] PRIMARY KEY CLUSTERED 
(
	[AdresId] ASC
))

GO

USE [Fleet]
CREATE TABLE [dbo].[Tankkaart](
	[TankkaartId] [int] IDENTITY(1,1) NOT NULL,
	[Kaartnummer] [nvarchar](50) NOT NULL,
	[Geldigheidsdatum] [date] NOT NULL,
	[Pincode] [nvarchar](4) NULL,
	[BestuurderId] [int] NULL,
	[Isgeblokeerd] [bit] NULL,
	[Brandstoftype] [nvarchar](50) NULL,
 CONSTRAINT [PK_Tankkaart] PRIMARY KEY CLUSTERED 
(
	[TankkaartId] ASC
))

GO

USE [Fleet]
CREATE TABLE [dbo].[Voertuig](
	[VoertuigId] [int] IDENTITY(1,1) NOT NULL,
	[Merk] [nvarchar](50) NOT NULL,
	[Model] [nvarchar](50) NOT NULL,
	[Chassisnummer] [nvarchar](50) NOT NULL,
	[Nummerplaat] [nvarchar](50) NOT NULL,
	[Brandstoftype] [nvarchar](50) NOT NULL,
	[Wagentype] [nvarchar](50) NOT NULL,
	[Kleur] [nvarchar](50) NULL,
	[Aantaldeuren] [int] NULL,
	[BestuurderId] [int] NULL,
 CONSTRAINT [PK_Voertuig] PRIMARY KEY CLUSTERED 
(
	[VoertuigId] ASC
))

GO

USE [Fleet]
CREATE TABLE [dbo].[Bestuurder](
	[BestuurderId] [int] IDENTITY(1,1) NOT NULL,
	[Voornaam] [nvarchar](50) NOT NULL,
	[Naam] [nvarchar](50) NOT NULL,
	[Geboortedatum] [date] NOT NULL,
	[AdresId] [int] NULL,
	[Rijksregisternummer] [nvarchar](20) NOT NULL,
	[VoertuigId] [int] NULL,
	[TankkaartId] [int] NULL,
 CONSTRAINT [PK_Bestuurder] PRIMARY KEY CLUSTERED 
(
	[BestuurderId] ASC
))

GO

USE [Fleet]
CREATE TABLE [dbo].[BestuurderRijbewijs](
	[TypeRijbewijs] [nvarchar](10) NOT NULL,
	[BestuurderId] [int] NOT NULL
)

GO

ALTER TABLE [dbo].[Voertuig]  WITH CHECK ADD  CONSTRAINT [FK_Voertuig_Bestuurder] FOREIGN KEY([BestuurderId])
REFERENCES [dbo].[Bestuurder] ([BestuurderId])
GO

ALTER TABLE [dbo].[Voertuig] CHECK CONSTRAINT [FK_Voertuig_Bestuurder]
GO

ALTER TABLE [dbo].[Tankkaart]  WITH CHECK ADD  CONSTRAINT [FK_Tankkaart_Bestuurder] FOREIGN KEY([BestuurderId])
REFERENCES [dbo].[Bestuurder] ([BestuurderId])
GO

ALTER TABLE [dbo].[Tankkaart] CHECK CONSTRAINT [FK_Tankkaart_Bestuurder]
GO


ALTER TABLE [dbo].[Bestuurder]  WITH CHECK ADD  CONSTRAINT [FK_Bestuurder_Adres] FOREIGN KEY([AdresId])
REFERENCES [dbo].[Adres] ([AdresId])
ON UPDATE CASCADE
ON DELETE SET NULL
GO

ALTER TABLE [dbo].[Bestuurder] CHECK CONSTRAINT [FK_Bestuurder_Adres]
GO

ALTER TABLE [dbo].[Bestuurder]  WITH CHECK ADD  CONSTRAINT [FK_Bestuurder_Tankkaart] FOREIGN KEY([TankkaartId])
REFERENCES [dbo].[Tankkaart] ([TankkaartId])
ON UPDATE CASCADE
ON DELETE SET NULL
GO

ALTER TABLE [dbo].[Bestuurder] CHECK CONSTRAINT [FK_Bestuurder_Tankkaart]
GO

ALTER TABLE [dbo].[Bestuurder]  WITH CHECK ADD  CONSTRAINT [FK_Bestuurder_Voertuig] FOREIGN KEY([VoertuigId])
REFERENCES [dbo].[Voertuig] ([VoertuigId])
ON UPDATE CASCADE
ON DELETE SET NULL
GO

ALTER TABLE [dbo].[Bestuurder] CHECK CONSTRAINT [FK_Bestuurder_Voertuig]
GO

ALTER TABLE [dbo].[BestuurderRijbewijs]  WITH CHECK ADD  CONSTRAINT [FK_BestuurderRijbewijs_Bestuurder1] FOREIGN KEY([BestuurderId])
REFERENCES [dbo].[Bestuurder] ([BestuurderId])
GO

ALTER TABLE [dbo].[BestuurderRijbewijs] CHECK CONSTRAINT [FK_BestuurderRijbewijs_Bestuurder1]
GO

INSERT INTO VOERTUIG (Merk,Model,Chassisnummer,Nummerplaat,Brandstoftype,Wagentype, Kleur, Aantaldeuren, BestuurderId)
VALUES('Jeep','Flow', '0A9810C206477L348', '1ABC001','Elektrisch','Personenwagen','Rood', 8, NULL)
GO
INSERT INTO VOERTUIG (Merk,Model,Chassisnummer,Nummerplaat,Brandstoftype,Wagentype, Kleur, Aantaldeuren, BestuurderId)
VALUES('McLaren','Windrider', '226A247841D336F49', '2DFE152','Hybride_Diesel','Personenwagen', NULL,8,NULL)
GO
INSERT INTO VOERTUIG (Merk,Model,Chassisnummer,Nummerplaat,Brandstoftype,Wagentype, Kleur, Aantaldeuren, BestuurderId)
VALUES('Maserati','Jumper', '515114B503050O229', '1LKD561','Hybride_Benzine','Personenwagen','Groen',NULL,NULL)
GO
INSERT INTO VOERTUIG (Merk,Model,Chassisnummer,Nummerplaat,Brandstoftype,Wagentype, Kleur, Aantaldeuren, BestuurderId)
VALUES('Mitsubishi','Volt', '958T491J53600G476', '1CAC541','Diesel','Personenwagen','Bruin',9,NULL)
GO
INSERT INTO VOERTUIG (Merk,Model,Chassisnummer,Nummerplaat,Brandstoftype,Wagentype, Kleur, Aantaldeuren, BestuurderId)
VALUES('Saab','340', '63N59624A90302B31', '1DLC181','Benzine','Personenwagen','Zwart',6,NULL)
GO
INSERT INTO VOERTUIG (Merk,Model,Chassisnummer,Nummerplaat,Brandstoftype,Wagentype, Kleur, Aantaldeuren, BestuurderId)
VALUES('Lamborghini','Surfer', '43586I959226R161T', '2DEF926','Elektrisch','Personenwagen','Zwart',8,NULL)
GO
INSERT INTO VOERTUIG (Merk,Model,Chassisnummer,Nummerplaat,Brandstoftype,Wagentype, Kleur, Aantaldeuren, BestuurderId)
VALUES('Lexus','RX', '46F03T54903414A02', '1ADP821','Hybride_Diesel','Personenwagen','Geel',NULL,NULL)
GO
INSERT INTO VOERTUIG (Merk,Model,Chassisnummer,Nummerplaat,Brandstoftype,Wagentype, Kleur, Aantaldeuren, BestuurderId)
VALUES('Alfa Romeo','Tonale', '24P8811H22298A659', '1FBS215','Hybride_Benzine','Personenwagen',NULL, 9,NULL)
GO
INSERT INTO VOERTUIG (Merk,Model,Chassisnummer,Nummerplaat,Brandstoftype,Wagentype, Kleur, Aantaldeuren, BestuurderId)
VALUES('Saab','Turbo', '7791H205456C307K0', '1PLC371','Diesel','Personenwagen',NULL, NULL,NULL)
GO
INSERT INTO VOERTUIG (Merk,Model,Chassisnummer,Nummerplaat,Brandstoftype,Wagentype, Kleur, Aantaldeuren, BestuurderId)
VALUES('Jaguar','F-Pace', '94416A405L719K605', '1LDP241','Benzine','Personenwagen','Zwart',NULL,NULL)
GO

INSERT INTO ADRES(Straat,Huisnummer,Gemeente,Postcode) VALUES('Zandstraatsesteenweg','125b','Jabbeke',8490)
GO
INSERT INTO ADRES(Straat,Huisnummer,Gemeente,Postcode) VALUES('Legewegstraat','12','Brugge',8000)
GO
INSERT INTO ADRES(Straat,Huisnummer,Gemeente,Postcode) VALUES('Dampoortstraat','91','Gent',9000)
GO
INSERT INTO ADRES(Straat,Huisnummer,Gemeente,Postcode) VALUES('Vorstlaan','64','Brussel',1000)
GO
INSERT INTO ADRES(Straat,Huisnummer,Gemeente,Postcode) VALUES('Provenhofstraat','1','Jabbeke',8490)
GO

INSERT INTO Tankkaart (Kaartnummer, Geldigheidsdatum, Pincode, BestuurderId, Isgeblokeerd, Brandstoftype) 
VALUES ('0123456789111213145', '12/17/2022', '2546', NULL, 0, 'Benzine');
GO
INSERT INTO Tankkaart (Kaartnummer,Geldigheidsdatum, Pincode, BestuurderId, Isgeblokeerd, Brandstoftype) 
VALUES ('0123456349111213145', '11/16/2023', '1234', NULL, 1, 'Diesel');
GO
INSERT INTO Tankkaart (Kaartnummer, Geldigheidsdatum, Pincode, BestuurderId, Isgeblokeerd, Brandstoftype) 
VALUES ('0123456789111343145', '02/12/2022', '4321', NULL, 0, 'Elektrisch');
GO
INSERT INTO Tankkaart (Kaartnummer, Geldigheidsdatum, Pincode, BestuurderId, Isgeblokeerd, Brandstoftype) 
VALUES ('0343456789111213145', '07/08/2024', '1324', NULL, 1, 'Benzine_Elektrisch');
GO
INSERT INTO Tankkaart (Kaartnummer, Geldigheidsdatum, Pincode, BestuurderId, Isgeblokeerd, Brandstoftype) 
VALUES ('0123456734111213145', '10/26/2022', '4213', NULL, 0, 'Diesel_Elektrisch');
GO

INSERT INTO Bestuurder (Voornaam,Naam,Geboortedatum,AdresId,Rijksregisternummer,VoertuigId,TankkaartId)
VALUES ('Gertjan','Deschuytter','01/01/1990',1,'90010100123',NULL,NULL)
GO
INSERT INTO Bestuurder (Voornaam,Naam,Geboortedatum,AdresId,Rijksregisternummer,VoertuigId,TankkaartId)
VALUES ('Jarne','De Keyser','05/02/1985',2,'85050200212',NULL,NULL)
GO
INSERT INTO Bestuurder (Voornaam,Naam,Geboortedatum,AdresId,Rijksregisternummer,VoertuigId,TankkaartId)
VALUES ('Thomas','Volckaert','10/09/1995',3,'95091000111',NULL,NULL)
GO
INSERT INTO Bestuurder (Voornaam,Naam,Geboortedatum,AdresId,Rijksregisternummer,VoertuigId,TankkaartId)
VALUES ('Bruno','Peters','12/21/2000',4,'00122100263',NULL,NULL)
GO
INSERT INTO Bestuurder (Voornaam,Naam,Geboortedatum,AdresId,Rijksregisternummer,VoertuigId,TankkaartId)
VALUES ('Tom','Van De Casteele','09/30/2005',5,'05093000271',NULL,NULL)
GO

INSERT INTO BestuurderRijbewijs (TypeRijbewijs,BestuurderId)
VALUES ('A', 1)
GO
INSERT INTO BestuurderRijbewijs (TypeRijbewijs,BestuurderId)
VALUES ('B', 2)
GO
INSERT INTO BestuurderRijbewijs (TypeRijbewijs,BestuurderId)
VALUES ('C', 3)
GO
INSERT INTO BestuurderRijbewijs (TypeRijbewijs,BestuurderId)
VALUES ('A', 4)
GO
INSERT INTO BestuurderRijbewijs (TypeRijbewijs,BestuurderId)
VALUES ('B', 5)
GO

UPDATE Bestuurder SET AdresId = 1, VoertuigId = 1 ,TankkaartId = 1 WHERE BestuurderId = 1;
GO
UPDATE Bestuurder SET AdresId = 2, VoertuigId = 2 ,TankkaartId = 2 WHERE BestuurderId = 2;
GO
UPDATE Bestuurder SET AdresId = 3, VoertuigId = 3 ,TankkaartId = 3 WHERE BestuurderId = 3;
GO
UPDATE Bestuurder SET AdresId = 4, VoertuigId = 4 ,TankkaartId = 4 WHERE BestuurderId = 4;
GO
UPDATE Bestuurder SET AdresId = 5, VoertuigId = 5 ,TankkaartId = 5 WHERE BestuurderId = 5;
GO

UPDATE Voertuig SET BestuurderId = 1 WHERE VoertuigId = 1;
GO
UPDATE Voertuig SET BestuurderId = 2 WHERE VoertuigId = 2;
GO
UPDATE Voertuig SET BestuurderId = 3 WHERE VoertuigId = 3;
GO
UPDATE Voertuig SET BestuurderId = 4 WHERE VoertuigId = 4;
GO
UPDATE Voertuig SET BestuurderId = 5 WHERE VoertuigId = 5;
GO

UPDATE Tankkaart SET BestuurderId = 1 WHERE TankkaartId = 1;
GO
UPDATE Tankkaart SET BestuurderId = 2 WHERE TankkaartId = 2;
GO
UPDATE Tankkaart SET BestuurderId = 3 WHERE TankkaartId = 3;
GO
UPDATE Tankkaart SET BestuurderId = 4 WHERE TankkaartId = 4;
GO
UPDATE Tankkaart SET BestuurderId = 5 WHERE TankkaartId = 5;
GO