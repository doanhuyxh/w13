use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupMaker_Delete]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupMaker_Delete]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure delete a new record for Maker table.
-- =============================================
create procedure [dbo].[ptgroupMaker_Delete]
(
	@Id int
)

as

set nocount on

delete from [Maker]
where [Id] = @Id
go
