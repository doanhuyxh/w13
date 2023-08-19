use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupCategory_Insert]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupCategory_Insert]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure insert a new record for Category table.
-- =============================================
create procedure [dbo].[ptgroupCategory_Insert]
(
	@Name nvarchar(250),
	@ParentId int,
	@TitleSeo nvarchar(250),
	@KeywordSeo nvarchar(250),
	@Description nvarchar(2000),
	@Sort int,
	@IsDel bit,
	@PositionMenu int,
	@PictureId int,
	@CreatedDate datetime,
	@ModifiedDate datetime,
	@Id int OUTPUT
)

as

set nocount on

insert into [Category]
(
	[Name],
	[ParentId],
	[TitleSeo],
	[KeywordSeo],
	[Description],
	[Sort],
	[IsDel],
	[PositionMenu],
	[PictureId],
	[CreatedDate],
	[ModifiedDate]
)
values
(
	@Name,
	@ParentId,
	@TitleSeo,
	@KeywordSeo,
	@Description,
	@Sort,
	@IsDel,
	@PositionMenu,
	@PictureId,
	@CreatedDate,
	@ModifiedDate
)

set @Id = scope_identity()
go
