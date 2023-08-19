use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupTransactionLog_ListAll]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupTransactionLog_ListAll]
go

create procedure [dbo].[ptgroupTransactionLog_ListAll]

as

set nocount on

select  [Id],
	[TransactionLog],
	[Message],
	[PageUrl],
	[CustomerId],
	[IpAddress],
	[CreatedOn]
from [TransactionLog] WITH (NOLOCK)
go
