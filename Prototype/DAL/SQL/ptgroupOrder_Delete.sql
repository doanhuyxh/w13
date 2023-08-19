use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupOrder_Delete]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupOrder_Delete]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure delete a new record for Orders table.
-- =============================================
create procedure [dbo].[ptgroupOrder_Delete]
(
	@Id int
)

as

set nocount on

delete from [Orders]
where [Id] = @Id
go
