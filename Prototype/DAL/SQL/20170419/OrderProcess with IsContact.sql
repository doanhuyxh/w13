USE [PhucThanh_Ecomerce]
GO

/****** Object:  StoredProcedure [dbo].[ptgroupOrderProcess_Insert]    Script Date: 4/17/2017 10:10:50 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure insert a new record for OrderProcess table.
-- =============================================
ALTER procedure [dbo].[ptgroupOrderProcess_Insert]
(
	@Id bigint output,
	@OrderId int,
	@Status tinyint,
	@ProcessDate datetime,
	@EmployeeId int,
	@Reason nvarchar(2000),
	@IsContact bit
)

as

set nocount on

insert into [OrderProcess]
(	
	[OrderId],
	[Status],
	[ProcessDate],
	[EmployeeId],
	[Reason],
	[IsContact]
)
values
(
	@OrderId,
	@Status,
	@ProcessDate,
	@EmployeeId,
	@Reason,
	@IsContact
)
SET @Id = SCOPE_IDENTITY()


GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure update a new record for OrderProcess table.
-- =============================================
ALTER procedure [dbo].[ptgroupOrderProcess_Update]
(
	@Id bigint,
	@OrderId int,
	@Status tinyint,
	@ProcessDate datetime,
	@EmployeeId int,
	@IsContact bit
)

as

set nocount on

update [OrderProcess]
set [OrderId] = @OrderId,
	[Status] = @Status,
	[ProcessDate] = @ProcessDate,
	[EmployeeId] = @EmployeeId,
	[IsContact] = @IsContact
where [Id] = @Id



GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure view detail a new record for OrderProcess table.
-- =============================================
ALTER procedure [dbo].[ptgroupOrderProcess_ViewDetail]
(
	@Id bigint
)

as

set nocount on

select  [Id],
	[OrderId],
	[Status],
	[ProcessDate],
	[EmployeeId],
	[IsContact]
from [OrderProcess]
where [Id] = @Id



GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		LongNDG
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[OrderProcess_ListByOrder]
	@OrderId int
AS
BEGIN
	SELECT p.*, (e.FirstName + ' ' + e.LastName) FullName
	FROM dbo.OrderProcess p 
	LEFT JOIN dbo.Employee e ON p.EmployeeId = e.Id
	WHERE p.OrderId = @OrderId
	ORDER BY p.ProcessDate DESC
END

GO


