﻿USE [AFDDev]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 06/15/2017 00:50:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customer](
	[Property] [varchar](50) NULL,
	[CustomerName] [varchar](50) NULL,
	[Value] [int] NOT NULL,
	[Action] [varchar](50) NULL,
	[File] [varchar](50) NULL,
	[Status] [varchar](10) NULL,
	[Hash] [varchar](50) NULL,
	CONSTRAINT PK_CUSTOMER_VALUE PRIMARY KEY (Value)
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[spUpdateCustomerData]    Script Date: 06/15/2017 00:50:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spUpdateCustomerData] 	
	@Status varchar(10),
	@Hash varchar(50),
	@Value int
AS
BEGIN	
   SET NOCOUNT ON
   Update Customer
   set Status = @Status,
   Hash = @Hash
   WHERE Value = @Value
   --SET @RecordCount = @@ROWCOUNT
   
END
GO
/****** Object:  StoredProcedure [dbo].[spGetCustomerStatus]    Script Date: 06/15/2017 00:50:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spGetCustomerStatus] 
	-- Add the parameters for the stored procedure here
	@totalCount int OUTPUT,
	@statusTrue int OUTPUT
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	   
    
	SELECT @totalCount = COUNT(*) from Customer
	SELECT @statusTrue = COUNT(*) from Customer WHERE STATUS = 'true'
		
END
GO
/****** Object:  StoredProcedure [dbo].[spGetCustomerData]    Script Date: 06/15/2017 00:50:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spGetCustomerData] 
	-- Add the parameters for the stored procedure here
	@flag int,
	@SEARCH VARCHAR(50) =  NULL,
	@VALUE INT = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    if (@flag =1) -- This will rows without any condition to check if data is present in the table or not
    BEGIN
	SELECT Value from Customer
	END
	
	if (@flag =2) -- This will rows without any condition to check if data is present in the table or not
    BEGIN
	SELECT CustomerName, Value from Customer where CustomerName like '%' +@SEARCH +  '%'
	END
	
	if (@flag =3) -- This will rows without any condition to check if data is present in the table or not
    BEGIN
	SELECT * from Customer where Value = @VALUE
	END
	
	if (@flag =4) -- This will rows without any condition to check if data is present in the table or not
    BEGIN
	SELECT * from Customer
	END
	
END
GO
/****** Object:  StoredProcedure [dbo].[spDeleteCustomerData]    Script Date: 06/15/2017 00:50:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[spDeleteCustomerData] 	
	@RecordCount INT OUTPUT
AS
BEGIN	
   SET NOCOUNT OFF
   Delete from Customer
   SET @RecordCount = @@ROWCOUNT
   
END
GO
