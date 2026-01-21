IF NOT EXISTS (
    SELECT name 
    FROM sys.databases 
    WHERE name = 'TODO_LIST'
)
BEGIN
	CREATE DATABASE TODO_LIST;
END;
GO

IF NOT EXISTS (
    SELECT name 
    FROM sys.schemas
    WHERE name = 'ref'
)
BEGIN
	CREATE SCHEMA ref;
END
GO

IF NOT EXISTS (
    SELECT name 
    FROM sys.schemas
    WHERE name = 'enum'
)
BEGIN
	CREATE SCHEMA enum;
END
GO

IF NOT EXISTS (
    SELECT name 
    FROM sys.tables
    WHERE name = 'TODO_ItemStatus'
)
BEGIN
	CREATE TABLE [TODO_LIST].[enum].[TODO_ItemStatus]
	(
		IdStatus int not null,
		[Label] varchar(128) null,
		CONSTRAINT PK_TODO_ItemStatus PRIMARY KEY(IdStatus)
	)
END

IF NOT EXISTS (
    SELECT name 
    FROM sys.tables
    WHERE name = 'TODO_Item'
)
BEGIN
	CREATE TABLE [TODO_LIST].[dbo].TODO_Item
	(
		IdItem int IDENTITY(1,1) not null,
		Title varchar(128) null,
		[Description] varchar(500) null,
		IdStatus int not null,
		CreatedBy int not null,
		Created DateTime not null,
		CONSTRAINT PK_TODO_Item_TODO PRIMARY KEY(IdItem),
		CONSTRAINT FK_TODO_Item_TODO_ItemStatus FOREIGN KEY(IdStatus) REFERENCES [TODO_LIST].[enum].TODO_ItemStatus(IdStatus)
	)
END

INSERT INTO [TODO_LIST].[enum].TODO_ItemStatus (IdStatus, Label)
VALUES
(1,'A faire'),
(2,'En cours'),
(3,'Terminée')

INSERT INTO [TODO_LIST].[dbo].[TODO_Item] (Title, Description, IdStatus, CreatedBy, Created)
VALUES
('Acheter des courses', 'Acheter du lait, des œufs et du pain', 1, 2, '2025-01-05 09:30:00'),
('Réviser Blazor', 'Travailler sur les composants et le data binding', 1, 1, '2025-01-06 14:00:00'),
('Envoyer le rapport', 'Envoyer le rapport mensuel au manager', 1, 2, '2025-01-03 10:15:00'),
('Nettoyer le bureau', 'Ranger les documents et nettoyer le bureau', 1, 1, '2025-01-02 16:45:00'),
('Planifier la réunion', 'Préparer l’ordre du jour pour la réunion d’équipe', 1, 3, '2025-01-07 11:00:00'),
('Mettre à jour le CV', 'Ajouter les dernières expériences professionnelles', 1, 1, '2025-01-04 18:20:00'),
('Sauvegarder le projet', 'Créer une sauvegarde du projet sur le cloud', 1,3, '2025-01-01 08:50:00'),
('Tester l’application', 'Tester les fonctionnalités principales de l’application', 1, 1, '2025-01-06 15:30:00');
