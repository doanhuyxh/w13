use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupNew_ListAll]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupNew_ListAll]
go

create procedure [dbo].[ptgroupNew_ListAll]

as

set nocount on

select  [Id],
	[Title],
	[Description],
	[Content],
	[ImageUrl],
	[MetaKeyword],
	[MetaDescription],
	[TitleSeo],
	[IsHot],
	[IsDel],
	[Published],
	[CreatedDate],
	[ModifiedDate]
from [News] WITH (NOLOCK)
go
