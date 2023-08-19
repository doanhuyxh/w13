use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupPicture_Update]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupPicture_Update]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure update a new record for Picture table.
-- =============================================
create procedure [dbo].[ptgroupPicture_Update]
(
	@Id bigint,
	@PicturePath varchar(2000),
	@MimeType nvarchar(40),
	@SeoFilename nvarchar(300),
	@AltAttribute nvarchar(max),
	@TitleAttribute nvarchar(max),
	@Album nvarchar(max),
	@IsDel bit,
	@Private bit,
	@PicType tinyint
)

as

set nocount on

update [Picture]
set [PicturePath] = @PicturePath,
	[MimeType] = @MimeType,
	[SeoFilename] = @SeoFilename,
	[AltAttribute] = @AltAttribute,
	[TitleAttribute] = @TitleAttribute,
	[Album] = @Album,
	[IsDel] = @IsDel,
	[Private] = @Private,
	[PicType] = @PicType
where [Id] = @Id
go
