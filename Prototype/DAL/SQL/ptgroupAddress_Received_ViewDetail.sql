use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupAddress_Received_ViewDetail]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupAddress_Received_ViewDetail]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/22/2016
-- Description:	The procedure view detail a new record for Address_Received table.
-- =============================================
create procedure [dbo].[ptgroupAddress_Received_ViewDetail]
(
	@Id int
)

as

set nocount on

select  [Id],
	[CustomerId],
	[FullName],
	[Phone],
	[Email],
	[CityId],
	[Address],
	[Note],
	[CreatedDate]
from [Address_Received]
where [Id] = @Id
go
