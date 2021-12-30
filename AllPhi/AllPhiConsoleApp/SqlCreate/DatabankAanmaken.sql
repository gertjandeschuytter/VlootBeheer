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