use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupModule_ListAll]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupModule_ListAll]
go

create procedure [dbo].[ptgroupModule_ListAll]

as

set nocount on

select  [ID],
	[Name]
from [Module] WITH (NOLOCK)
go
