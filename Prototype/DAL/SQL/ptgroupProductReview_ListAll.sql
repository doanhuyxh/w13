create procedure [dbo].[ptgroupProductReview_ListAll]
	@id bigint
as

set nocount on

select  pv.[Id],
	[CustomerId],
	[ProductId],
	[IsApproved],
	[Title],
	[ReviewText],
	[Rating],
	[CreatedOn],
	[Email]
from [ProductReview] pv WITH (NOLOCK) inner join Customers c on pv.CustomerId = c.Id where ProductId = @id and IsApproved = 1
go
