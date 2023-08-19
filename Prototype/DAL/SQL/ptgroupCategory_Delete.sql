use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupCategory_Delete]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupCategory_Delete]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure delete a new record for Category table.
-- =============================================
create procedure [dbo].[ptgroupCategory_Delete]
(
	@Id int
)

as

set nocount on

delete from [Category]
where [Id] = @Id
go
