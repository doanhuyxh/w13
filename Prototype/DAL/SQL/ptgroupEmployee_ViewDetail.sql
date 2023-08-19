use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupEmployee_ViewDetail]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupEmployee_ViewDetail]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure view detail a new record for Employee table.
-- =============================================
create procedure [dbo].[ptgroupEmployee_ViewDetail]
(
	@Id int
)

as

set nocount on

select  [Id],
	[FirstName],
	[LastName],
	[Address],
	[Phone],
	[PictureId],
	[IsActive],
	[CreatedDate],
	[ModifiedDate]
from [Employee]
where [Id] = @Id
go
