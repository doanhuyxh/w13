use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupPicture_ListAll]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupPicture_ListAll]
go

create procedure [dbo].[ptgroupPicture_ListAll]

as

set nocount on

select  [Id],
	[PicturePath],
	[MimeType],
	[SeoFilename],
	[AltAttribute],
	[TitleAttribute],
	[Album],
	[IsDel],
	[Private],
	[PicType]
from [Picture] WITH (NOLOCK)
go
