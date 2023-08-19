use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupProductAttribute_Update]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupProductAttribute_Update]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure update a new record for ProductAttribute table.
-- =============================================
create procedure [dbo].[ptgroupProductAttribute_Update]
(
	@Id int,
	@Attribute nvarchar(max),
	@CreatedDate datetime,
	@ModifiedDate datetime
)

as

set nocount on

update [ProductAttribute]
set [Attribute] = @Attribute,
	[CreatedDate] = @CreatedDate,
	[ModifiedDate] = @ModifiedDate
where [Id] = @Id
go
