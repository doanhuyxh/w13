use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupPicture_Insert]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupPicture_Insert]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure insert a new record for Picture table.
-- =============================================
create procedure [dbo].[ptgroupPicture_Insert]
(
	@PicturePath varchar(2000),
	@MimeType nvarchar(40),
	@SeoFilename nvarchar(300),
	@AltAttribute nvarchar(max),
	@TitleAttribute nvarchar(max),
	@Album nvarchar(max),
	@IsDel bit,
	@Private bit,
	@PicType tinyint,
	@Id bigint OUTPUT
)

as

set nocount on

insert into [Picture]
(
	[PicturePath],
	[MimeType],
	[SeoFilename],
	[AltAttribute],
	[TitleAttribute],
	[Album],
	[IsDel],
	[Private],
	[PicType]
)
values
(
	@PicturePath,
	@MimeType,
	@SeoFilename,
	@AltAttribute,
	@TitleAttribute,
	@Album,
	@IsDel,
	@Private,
	@PicType
)

set @Id = scope_identity()
go
