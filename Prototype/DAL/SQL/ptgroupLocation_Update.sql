use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupLocation_Update]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupLocation_Update]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/22/2016
-- Description:	The procedure update a new record for Location table.
-- =============================================
create procedure [dbo].[ptgroupLocation_Update]
(
	@Id int,
	@Name nvarchar(50),
	@ParentId int,
	@CreatedDate datetime,
	@ModifiedDate datetime,
	@SlugName varchar(50),
	@lat nvarchar(50),
	@lng nvarchar(50)
)

as

set nocount on

update [Location]
set [Name] = @Name,
	[ParentId] = @ParentId,
	[CreatedDate] = @CreatedDate,
	[ModifiedDate] = @ModifiedDate,
	[SlugName] = @SlugName,
	[lat] = @lat,
	[lng] = @lng
where [Id] = @Id
go
