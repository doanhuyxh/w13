use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupOrderProcess_ListAll]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupOrderProcess_ListAll]
go

create procedure [dbo].[ptgroupOrderProcess_ListAll]

as

set nocount on

select  [Id],
	[OrderId],
	[Status],
	[ProcessDate],
	[EmployeeId]
from [OrderProcess] WITH (NOLOCK)
go
