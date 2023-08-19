use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupNew_Update]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupNew_Update]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure update a new record for News table.
-- =============================================
create procedure [dbo].[ptgroupNew_Update]
(
	@Id bigint,
	@Title nvarchar(250),
	@Description nvarchar(500),
	@Content nvarchar(max),
	@ImageUrl varchar(500),
	@MetaKeyword nvarchar(350),
	@MetaDescription nvarchar(500),
	@TitleSeo nvarchar(250),
	@IsHot bit,
	@IsDel bit,
	@Published bit,
	@CreatedDate datetime,
	@ModifiedDate datetime
)

as

set nocount on

update [News]
set [Title] = @Title,
	[Description] = @Description,
	[Content] = @Content,
	[ImageUrl] = @ImageUrl,
	[MetaKeyword] = @MetaKeyword,
	[MetaDescription] = @MetaDescription,
	[TitleSeo] = @TitleSeo,
	[IsHot] = @IsHot,
	[IsDel] = @IsDel,
	[Published] = @Published,
	[CreatedDate] = @CreatedDate,
	[ModifiedDate] = @ModifiedDate
where [Id] = @Id
go
