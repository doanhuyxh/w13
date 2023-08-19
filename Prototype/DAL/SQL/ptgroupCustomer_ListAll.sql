use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupCustomer_ListAll]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupCustomer_ListAll]
go

create procedure [dbo].[ptgroupCustomer_ListAll]

as

set nocount on

select  [Id],
	[Name],
	[Address],
	[Email],
	[Phone],
	[Country],
	[IdGuid]
from [Customers] WITH (NOLOCK)
go
