use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupProductAttribute_Delete]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupProductAttribute_Delete]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure delete a new record for ProductAttribute table.
-- =============================================
create procedure [dbo].[ptgroupProductAttribute_Delete]
(
	@Id int
)

as

set nocount on

delete from [ProductAttribute]
where [Id] = @Id
go
