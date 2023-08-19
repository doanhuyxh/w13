use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupCategory_Update]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupCategory_Update]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure update a new record for Category table.
-- =============================================
create procedure [dbo].[ptgroupCategory_Update]
(
	@Id int,
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
	@ModifiedDate datetime
)

as

set nocount on

update [Category]
set [Name] = @Name,
	[ParentId] = @ParentId,
	[TitleSeo] = @TitleSeo,
	[KeywordSeo] = @KeywordSeo,
	[Description] = @Description,
	[Sort] = @Sort,
	[IsDel] = @IsDel,
	[PositionMenu] = @PositionMenu,
	[PictureId] = @PictureId,
	[CreatedDate] = @CreatedDate,
	[ModifiedDate] = @ModifiedDate
where [Id] = @Id
go
