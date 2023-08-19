use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupTransactionLog_ViewDetail]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupTransactionLog_ViewDetail]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure view detail a new record for TransactionLog table.
-- =============================================
create procedure [dbo].[ptgroupTransactionLog_ViewDetail]
(
	@Id bigint
)

as

set nocount on

select  [Id],
	[TransactionLog],
	[Message],
	[PageUrl],
	[CustomerId],
	[IpAddress],
	[CreatedOn]
from [TransactionLog]
where [Id] = @Id
go
