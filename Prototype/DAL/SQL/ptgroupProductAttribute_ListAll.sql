use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupProductAttribute_ListAll]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupProductAttribute_ListAll]
go

create procedure [dbo].[ptgroupProductAttribute_ListAll]

as

set nocount on

select  [Id],
	[Attribute],
	[CreatedDate],
	[ModifiedDate]
from [ProductAttribute] WITH (NOLOCK)
go
