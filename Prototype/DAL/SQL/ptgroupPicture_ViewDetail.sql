use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupPicture_ViewDetail]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupPicture_ViewDetail]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure view detail a new record for Picture table.
-- =============================================
create procedure [dbo].[ptgroupPicture_ViewDetail]
(
	@Id bigint
)

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
from [Picture]
where [Id] = @Id
go
