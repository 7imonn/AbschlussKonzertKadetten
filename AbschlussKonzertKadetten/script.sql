dbug: Microsoft.EntityFrameworkCore.Infrastructure[10401]
      An 'IServiceProvider' was created for internal use by Entity Framework.
info: Microsoft.EntityFrameworkCore.Infrastructure[10403]
      Entity Framework Core 2.1.3-rtm-32065 initialized 'KadettenContext' using provider 'Pomelo.EntityFrameworkCore.MySql' with options: MaxPoolSize=128 ServerVersion 5.7.17 MySql 
dbug: Microsoft.EntityFrameworkCore.ChangeTracking[10800]
      DetectChanges starting for 'KadettenContext'.
dbug: Microsoft.EntityFrameworkCore.ChangeTracking[10801]
      DetectChanges completed for 'KadettenContext'.
dbug: Microsoft.EntityFrameworkCore.Migrations[20404]
      Generating up script for migration '20191125173627_InitialCreate'.
CREATE TABLE `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);

CREATE TABLE `Clients` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `LastName` longtext NULL,
    `FirstName` longtext NULL,
    `Email` longtext NULL,
    `Phone` longtext NULL,
    CONSTRAINT `PK_Clients` PRIMARY KEY (`Id`)
);

CREATE TABLE `FormularActive` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Active` bit NOT NULL,
    CONSTRAINT `PK_FormularActive` PRIMARY KEY (`Id`)
);

CREATE TABLE `Kadett` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `LastName` longtext NULL,
    `FirstName` longtext NULL,
    `KadettInKader` bit NOT NULL,
    CONSTRAINT `PK_Kadett` PRIMARY KEY (`Id`)
);

CREATE TABLE `Redactor` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext NULL,
    `Text` longtext NULL,
    CONSTRAINT `PK_Redactor` PRIMARY KEY (`Id`)
);

CREATE TABLE `Ticket` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Type` longtext NOT NULL,
    `Price` int NOT NULL,
    CONSTRAINT `PK_Ticket` PRIMARY KEY (`Id`)
);

CREATE TABLE `User` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `FirstName` longtext NULL,
    `LastName` longtext NULL,
    `Username` longtext NULL,
    `Password` longtext NULL,
    CONSTRAINT `PK_User` PRIMARY KEY (`Id`)
);

CREATE TABLE `Orders` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Bemerkung` longtext NULL,
    `OrderDate` datetime(6) NOT NULL,
    `ClientId` int NOT NULL,
    `KadettId` int NOT NULL,
    CONSTRAINT `PK_Orders` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Orders_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Clients` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Orders_Kadett_KadettId` FOREIGN KEY (`KadettId`) REFERENCES `Kadett` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `TicketOrders` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `OrderId` int NOT NULL,
    `TicketId` int NOT NULL,
    `Day` longtext NULL,
    `Quantity` int NOT NULL,
    CONSTRAINT `PK_TicketOrders` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_TicketOrders_Orders_OrderId` FOREIGN KEY (`OrderId`) REFERENCES `Orders` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_TicketOrders_Ticket_TicketId` FOREIGN KEY (`TicketId`) REFERENCES `Ticket` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_Orders_ClientId` ON `Orders` (`ClientId`);

CREATE INDEX `IX_Orders_KadettId` ON `Orders` (`KadettId`);

CREATE INDEX `IX_TicketOrders_OrderId` ON `TicketOrders` (`OrderId`);

CREATE INDEX `IX_TicketOrders_TicketId` ON `TicketOrders` (`TicketId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20191125173627_InitialCreate', '2.1.3-rtm-32065');


