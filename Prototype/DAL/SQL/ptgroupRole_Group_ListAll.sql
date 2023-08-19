use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupRole_Group_ListAll]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupRole_Group_ListAll]
go

create procedure [dbo].[ptgroupRole_Group_ListAll]

as

set nocount on

select  [ID],
	[Name]
from [Role_Group] WITH (NOLOCK)
go
