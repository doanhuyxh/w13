use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupProductAttributeValue_Delete]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupProductAttributeValue_Delete]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure delete a new record for ProductAttributeValue table.
-- =============================================
create procedure [dbo].[ptgroupProductAttributeValue_Delete]
(
	@Id int
)

as

set nocount on

delete from [ProductAttributeValue]
where [Id] = @Id
go
