use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupOrderProcess_Delete]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupOrderProcess_Delete]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure delete a new record for OrderProcess table.
-- =============================================
create procedure [dbo].[ptgroupOrderProcess_Delete]
(
	@Id bigint
)

as

set nocount on

delete from [OrderProcess]
where [Id] = @Id
go
