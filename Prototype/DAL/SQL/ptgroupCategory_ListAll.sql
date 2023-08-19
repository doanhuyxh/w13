use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupCategory_ListAll]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupCategory_ListAll]
go

create procedure [dbo].[ptgroupCategory_ListAll]

as

set nocount on

select  [Id],
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
from [Category] WITH (NOLOCK)
go
