use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupProductAttribute_ViewDetail]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupProductAttribute_ViewDetail]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure view detail a new record for ProductAttribute table.
-- =============================================
create procedure [dbo].[ptgroupProductAttribute_ViewDetail]
(
	@Id int
)

as

set nocount on

select  [Id],
	[Attribute],
	[CreatedDate],
	[ModifiedDate]
from [ProductAttribute]
where [Id] = @Id
go
