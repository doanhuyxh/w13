use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupProduct_Delete]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupProduct_Delete]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure delete a new record for Products table.
-- =============================================
create procedure [dbo].[ptgroupProduct_Delete]
(
	@Id bigint
)

as

set nocount on

delete from [Products]
where [Id] = @Id
go
