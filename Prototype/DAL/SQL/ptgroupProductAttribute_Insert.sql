use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupProductAttribute_Insert]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupProductAttribute_Insert]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure insert a new record for ProductAttribute table.
-- =============================================
create procedure [dbo].[ptgroupProductAttribute_Insert]
(
	@Attribute nvarchar(max),
	@CreatedDate datetime,
	@ModifiedDate datetime,
	@Id int OUTPUT
)

as

set nocount on

insert into [ProductAttribute]
(
	[Attribute],
	[CreatedDate],
	[ModifiedDate]
)
values
(
	@Attribute,
	@CreatedDate,
	@ModifiedDate
)

set @Id = scope_identity()
go
