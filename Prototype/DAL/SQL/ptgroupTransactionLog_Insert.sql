use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupTransactionLog_Insert]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupTransactionLog_Insert]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure insert a new record for TransactionLog table.
-- =============================================
create procedure [dbo].[ptgroupTransactionLog_Insert]
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

insert into [TransactionLog]
(
	[Id],
	[TransactionLog],
	[Message],
	[PageUrl],
	[CustomerId],
	[IpAddress],
	[CreatedOn]
)
values
(
	@Id,
	@TransactionLog,
	@Message,
	@PageUrl,
	@CustomerId,
	@IpAddress,
	@CreatedOn
)
go
