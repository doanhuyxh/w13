use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupCustomer_ViewDetail]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupCustomer_ViewDetail]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure view detail a new record for Customers table.
-- =============================================
create procedure [dbo].[ptgroupCustomer_ViewDetail]
(
	@Id int
)

as

set nocount on

select  [Id],
	[Name],
	[Address],
	[Email],
	[Phone],
	[Country],
	[IdGuid]
from [Customers]
where [Id] = @Id
go
