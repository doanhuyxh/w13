use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupTransactionLog_Delete]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupTransactionLog_Delete]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure delete a new record for TransactionLog table.
-- =============================================
create procedure [dbo].[ptgroupTransactionLog_Delete]
(
	@Id bigint
)

as

set nocount on

delete from [TransactionLog]
where [Id] = @Id
go
