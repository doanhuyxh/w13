use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupNew_Insert]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupNew_Insert]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure insert a new record for News table.
-- =============================================
create procedure [dbo].[ptgroupNew_Insert]
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

insert into [News]
(
	[Id],
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
)
values
(
	@Id,
	@Title,
	@Description,
	@Content,
	@ImageUrl,
	@MetaKeyword,
	@MetaDescription,
	@TitleSeo,
	@IsHot,
	@IsDel,
	@Published,
	@CreatedDate,
	@ModifiedDate
)
go
