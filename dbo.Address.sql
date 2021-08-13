CREATE TABLE [dbo].[Address] (
    Id int primary key identity(1,1),
	[Username] NVARCHAR (30)  NOT NULL,
    [Street]   NVARCHAR (MAX) NOT NULL,
    [City]     NVARCHAR (100) NOT NULL,
    [State]    NVARCHAR (50)  NOT NULL,
    [ZipCode]  NVARCHAR (50)  NOT NULL
);

