use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupLocation_Delete]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupLocation_Delete]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/22/2016
-- Description:	The procedure delete a new record for Location table.
-- =============================================
create procedure [dbo].[ptgroupLocation_Delete]
(
	@Id int
)

as

set nocount on

delete from [Location]
where [Id] = @Id
go
