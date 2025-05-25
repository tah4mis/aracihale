CREATE TABLE [Vehicles] (
    [Id] int NOT NULL IDENTITY,
    [Brand] nvarchar(max) NOT NULL,
    [Model] nvarchar(max) NOT NULL,
    [Year] int NOT NULL,
    [Mileage] int NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [Description] nvarchar(max) NULL,
    [ImageUrl] nvarchar(max) NULL,
    [IsAvailable] bit NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NULL,
    [ABS] bit NOT NULL,
    [ASR] bit NOT NULL,
    [CocukKilidi] bit NOT NULL,
    [Distronic] bit NOT NULL,
    [ESP] bit NOT NULL,
    [GeceGorusSistemi] bit NOT NULL,
    [HavaYastigiSurucu] bit NOT NULL,
    [HavaYastigiYolcu] bit NOT NULL,
    [Immobilizer] bit NOT NULL,
    [Isofix] bit NOT NULL,
    [KorNoktaUyariSistemi] bit NOT NULL,
    [MerkeziKilit] bit NOT NULL,
    [SeritTakipSistemi] bit NOT NULL,
    [YokusKalkisDesteği] bit NOT NULL,
    [YorgunlukTespitSistemi] bit NOT NULL,
    [ZirhliArac] bit NOT NULL,
    [HidrolikDireksiyon] bit NOT NULL,
    [UcuncuSiraKoltuklar] bit NOT NULL,
    [DeriKoltuk] bit NOT NULL,
    [KumasKoltuk] bit NOT NULL,
    [ElektrikliCamlar] bit NOT NULL,
    [Klima] bit NOT NULL,
    [OtmKararanDikizAynasi] bit NOT NULL,
    [OnGorusKamerasi] bit NOT NULL,
    [OnKoltukKolDayamasi] bit NOT NULL,
    [AnahtarsizGiris] bit NOT NULL,
    [FonksiyonelDireksiyon] bit NOT NULL,
    [IsitmaliDireksiyon] bit NOT NULL,
    [KoltuklarElektrikli] bit NOT NULL,
    [KoltuklarHafizali] bit NOT NULL,
    [KoltuklarIsitmali] bit NOT NULL,
    [KoltuklarSogutmali] bit NOT NULL,
    [HizSabitlemeSistemi] bit NOT NULL,
    [SogutmaliTorpido] bit NOT NULL,
    [YolBilgisayari] bit NOT NULL,
    [HeadUpDisplay] bit NOT NULL,
    [StartStop] bit NOT NULL,
    [GeriGorusKamerasi] bit NOT NULL,
    [AyaklaAcilanBagajKapagi] bit NOT NULL,
    [Hardtop] bit NOT NULL,
    [FarAdaptif] bit NOT NULL,
    [AynalarElektrikli] bit NOT NULL,
    [AynalarIsitmali] bit NOT NULL,
    [AynalarHafizali] bit NOT NULL,
    [ParkSensoruArka] bit NOT NULL,
    [ParkSensoruOn] bit NOT NULL,
    [ParkAsistani] bit NOT NULL,
    [Sunroof] bit NOT NULL,
    [AkilliBagajKapagi] bit NOT NULL,
    [PanoramikCamTavan] bit NOT NULL,
    [RomorkCekiDemiri] bit NOT NULL,
    [AndroidAuto] bit NOT NULL,
    [AppleCarPlay] bit NOT NULL,
    [Bluetooth] bit NOT NULL,
    [UsbAux] bit NOT NULL,
    CONSTRAINT [PK_Vehicles] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Auctions] (
    [Id] int NOT NULL IDENTITY,
    [VehicleId] int NOT NULL,
    [StartingPrice] decimal(18,2) NOT NULL,
    [MinimumIncrement] decimal(18,2) NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [Status] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Auctions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Auctions_Vehicles_VehicleId] FOREIGN KEY ([VehicleId]) REFERENCES [Vehicles] ([Id]) ON DELETE NO ACTION
);
GO


CREATE TABLE [BankAccounts] (
    [Id] int NOT NULL IDENTITY,
    [BankName] nvarchar(100) NOT NULL,
    [IBAN] nvarchar(26) NOT NULL,
    [BranchCode] nvarchar(5) NULL,
    [AccountNumber] nvarchar(10) NULL,
    [AccountHolder] nvarchar(100) NOT NULL,
    [IsDefault] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_BankAccounts] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Bids] (
    [Id] int NOT NULL IDENTITY,
    [AuctionId] int NOT NULL,
    [UserId] int NOT NULL,
    [Amount] decimal(18,2) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [Status] int NOT NULL,
    [UserId1] int NULL,
    CONSTRAINT [PK_Bids] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Bids_Auctions_AuctionId] FOREIGN KEY ([AuctionId]) REFERENCES [Auctions] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [Companies] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [TaxNumber] nvarchar(10) NOT NULL,
    [Email] nvarchar(100) NULL,
    [Phone] nvarchar(11) NULL,
    [Address] nvarchar(500) NULL,
    [AuthorizedPerson] nvarchar(100) NULL,
    [AuthorizedPersonPhone] nvarchar(20) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NULL,
    [IsActive] bit NOT NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_Companies] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(50) NOT NULL,
    [LastName] nvarchar(50) NOT NULL,
    [Email] nvarchar(100) NOT NULL,
    [Password] nvarchar(100) NOT NULL,
    [Phone] nvarchar(11) NOT NULL,
    [Address] nvarchar(500) NULL,
    [UserType] int NOT NULL,
    [CompanyId] int NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Users_Companies_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Companies] ([Id]) ON DELETE SET NULL
);
GO


CREATE INDEX [IX_Auctions_VehicleId] ON [Auctions] ([VehicleId]);
GO


CREATE INDEX [IX_BankAccounts_UserId] ON [BankAccounts] ([UserId]);
GO


CREATE INDEX [IX_Bids_AuctionId] ON [Bids] ([AuctionId]);
GO


CREATE INDEX [IX_Bids_UserId] ON [Bids] ([UserId]);
GO


CREATE INDEX [IX_Bids_UserId1] ON [Bids] ([UserId1]);
GO


CREATE UNIQUE INDEX [IX_Companies_UserId] ON [Companies] ([UserId]);
GO


CREATE INDEX [IX_Users_CompanyId] ON [Users] ([CompanyId]);
GO


ALTER TABLE [BankAccounts] ADD CONSTRAINT [FK_BankAccounts_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE;
GO


ALTER TABLE [Bids] ADD CONSTRAINT [FK_Bids_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION;
GO


ALTER TABLE [Bids] ADD CONSTRAINT [FK_Bids_Users_UserId1] FOREIGN KEY ([UserId1]) REFERENCES [Users] ([Id]);
GO


ALTER TABLE [Companies] ADD CONSTRAINT [FK_Companies_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION;
GO


