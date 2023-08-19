use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupAddress_Received_ListAll]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupAddress_Received_ListAll]
go

create procedure [dbo].[ptgroupAddress_Received_ListAll]

as

set nocount on

select  [Id],
	[CustomerId],
	[FullName],
	[Phone],
	[Email],
	[CityId],
	[Address],
	[Note],
	[CreatedDate]
from [Address_Received] WITH (NOLOCK)
go
