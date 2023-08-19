use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupTransactionLog_Update]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupTransactionLog_Update]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure update a new record for TransactionLog table.
-- =============================================
create procedure [dbo].[ptgroupTransactionLog_Update]
(
	@Id bigint,
	@TransactionLog nvarchar(max),
	@Message nvarchar(max),
	@PageUrl nvarchar(max),
	@CustomerId int,
	@IpAddress varchar(200),
	@CreatedOn datetime
)

as

set nocount on

update [TransactionLog]
set [TransactionLog] = @TransactionLog,
	[Message] = @Message,
	[PageUrl] = @PageUrl,
	[CustomerId] = @CustomerId,
	[IpAddress] = @IpAddress,
	[CreatedOn] = @CreatedOn
where [Id] = @Id
go
