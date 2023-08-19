use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupMaker_Update]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupMaker_Update]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure update a new record for Maker table.
-- =============================================
create procedure [dbo].[ptgroupMaker_Update]
(
	@Id int,
	@MakerName nvarchar(500),
	@Address nvarchar(2000),
	@Phone varchar(50),
	@IsActive bit,
	@CreatedDate datetime,
	@ModifiedDate datetime
)

as

set nocount on

update [Maker]
set [MakerName] = @MakerName,
	[Address] = @Address,
	[Phone] = @Phone,
	[IsActive] = @IsActive,
	[CreatedDate] = @CreatedDate,
	[ModifiedDate] = @ModifiedDate
where [Id] = @Id
go
