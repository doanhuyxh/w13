use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupLocation_Insert]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupLocation_Insert]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/22/2016
-- Description:	The procedure insert a new record for Location table.
-- =============================================
create procedure [dbo].[ptgroupLocation_Insert]
(
	@Name nvarchar(50),
	@ParentId int,
	@CreatedDate datetime,
	@ModifiedDate datetime,
	@SlugName varchar(50),
	@lat nvarchar(50),
	@lng nvarchar(50),
	@Id int OUTPUT
)

as

set nocount on

insert into [Location]
(
	[Name],
	[ParentId],
	[CreatedDate],
	[ModifiedDate],
	[SlugName],
	[lat],
	[lng]
)
values
(
	@Name,
	@ParentId,
	@CreatedDate,
	@ModifiedDate,
	@SlugName,
	@lat,
	@lng
)

set @Id = scope_identity()
go
