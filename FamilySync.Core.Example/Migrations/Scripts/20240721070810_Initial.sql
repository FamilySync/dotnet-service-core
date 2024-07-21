CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `Examples` (
    `ID` char(36) COLLATE ascii_general_ci NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Age` int NOT NULL,
    `Email` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedAt` datetime(6) NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `UpdatedAt` datetime(6) NULL,
    `UpdatedBy` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Examples` PRIMARY KEY (`ID`)
) CHARACTER SET=utf8mb4;

CREATE UNIQUE INDEX `IX_Examples_Email` ON `Examples` (`Email`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240721070810_Initial', '8.0.7');

COMMIT;

