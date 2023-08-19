use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupMaker_Insert]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupMaker_Insert]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure insert a new record for Maker table.
-- =============================================
create procedure [dbo].[ptgroupMaker_Insert]
(
	@MakerName nvarchar(500),
	@Address nvarchar(2000),
	@Phone varchar(50),
	@IsActive bit,
	@CreatedDate datetime,
	@ModifiedDate datetime,
	@Id int OUTPUT
)

as

set nocount on

insert into [Maker]
(
	[MakerName],
	[Address],
	[Phone],
	[IsActive],
	[CreatedDate],
	[ModifiedDate]
)
values
(
	@MakerName,
	@Address,
	@Phone,
	@IsActive,
	@CreatedDate,
	@ModifiedDate
)

set @Id = scope_identity()
go
