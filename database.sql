USE [Art_Gallery]
GO
/****** Object:  Table [dbo].[Artist]    Script Date: 3/12/2024 1:42:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Artist](
	[ArtistId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Nationality] [nvarchar](50) NULL,
	[Age] [int] NULL,
	[Sex] [nvarchar](10) NULL,
	[Image] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[ArtistId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Artwork]    Script Date: 3/12/2024 1:42:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Artwork](
	[CategoryId] [int] NULL,
	[Status] [nvarchar](50) NULL,
	[CountAuction] [int] NULL,
	[AuctionPrice] [float] NULL,
	[Discount] [float] NULL,
	[Name] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[Price] [float] NULL,
	[OrderId] [int] NULL,
	[EmployeeId] [int] NULL,
	[CustomerId] [int] NULL,
	[ArtistId] [int] NULL,
	[ArtworkId] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ArtworkId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Auctions]    Script Date: 3/12/2024 1:42:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Auctions](
	[AuctionId] [int] IDENTITY(1,1) NOT NULL,
	[EndDate] [char](18) NULL,
	[StartDate] [char](18) NULL,
	[Name] [char](18) NULL,
PRIMARY KEY CLUSTERED 
(
	[AuctionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 3/12/2024 1:42:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 3/12/2024 1:42:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Email] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NULL,
	[ReditCard] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[PhoneNumber] [nvarchar](12) NULL,
	[Age] [int] NULL,
	[Sex] [nvarchar](10) NULL,
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[FavoriteArtwork] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 3/12/2024 1:42:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](60) NULL,
	[Name] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Adress] [char](18) NULL,
	[PhoneNumber] [char](18) NULL,
	[Age] [int] NULL,
	[Sex] [nvarchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Functions]    Script Date: 3/12/2024 1:42:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Functions](
	[FunctionId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[FunctionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 3/12/2024 1:42:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[GroupId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NULL,
	[FunctionId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[New]    Script Date: 3/12/2024 1:42:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[New](
	[NewId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Title] [nvarchar](50) NULL,
	[Description] [nvarchar](200) NULL,
	[NewDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[NewId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 3/12/2024 1:42:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[NotificationId] [int] IDENTITY(1,1) NOT NULL,
	[RequestMessage] [nvarchar](200) NULL,
	[BidPrice] [float] NULL,
	[ArtworkId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[NotificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment_gateways]    Script Date: 3/12/2024 1:42:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment_gateways](
	[PaymentId] [int] IDENTITY(1,1) NOT NULL,
	[PaymentMethod] [nvarchar](50) NULL,
	[Amount] [int] NULL,
	[PaymentDate] [datetime] NULL,
	[EmployeeId] [int] NULL,
	[OrderId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Purcher_order]    Script Date: 3/12/2024 1:42:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purcher_order](
	[TotalAmount] [int] NULL,
	[OrderDate] [datetime] NULL,
	[CustomerId] [int] NULL,
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[ArtworkId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rel_Artwork_Auctions]    Script Date: 3/12/2024 1:42:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rel_Artwork_Auctions](
	[ArtworkId] [int] NOT NULL,
	[AuctionId] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ArtworkId] ASC,
	[AuctionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rel_Employee_Group]    Script Date: 3/12/2024 1:42:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rel_Employee_Group](
	[EmployeeId] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC,
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rel_Group_Function]    Script Date: 3/12/2024 1:42:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rel_Group_Function](
	[GroupId] [int] NOT NULL,
	[FunctionId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC,
	[FunctionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rel_Shipping_Payment]    Script Date: 3/12/2024 1:42:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rel_Shipping_Payment](
	[ShippingId] [int] NOT NULL,
	[PaymentId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ShippingId] ASC,
	[PaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Review]    Script Date: 3/12/2024 1:42:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Review](
	[ReviewId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NULL,
	[ArtworkId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ReviewId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shipping]    Script Date: 3/12/2024 1:42:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shipping](
	[ShippingId] [int] IDENTITY(1,1) NOT NULL,
	[ActualDeliveryDate] [datetime] NULL,
	[EstimateDeliveryDate] [datetime] NULL,
	[ShippingDate] [datetime] NULL,
	[PaymentId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ShippingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Artwork]  WITH CHECK ADD FOREIGN KEY([ArtistId])
REFERENCES [dbo].[Artist] ([ArtistId])
GO
ALTER TABLE [dbo].[Artwork]  WITH CHECK ADD FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
GO
ALTER TABLE [dbo].[Artwork]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[Artwork]  WITH CHECK ADD FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[Artwork]  WITH CHECK ADD FOREIGN KEY([OrderId])
REFERENCES [dbo].[Purcher_order] ([OrderId])
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD FOREIGN KEY([ArtworkId])
REFERENCES [dbo].[Artwork] ([ArtworkId])
GO
ALTER TABLE [dbo].[Payment_gateways]  WITH CHECK ADD FOREIGN KEY([OrderId])
REFERENCES [dbo].[Purcher_order] ([OrderId])
GO
ALTER TABLE [dbo].[Purcher_order]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[Rel_Artwork_Auctions]  WITH CHECK ADD FOREIGN KEY([ArtworkId])
REFERENCES [dbo].[Artwork] ([ArtworkId])
GO
ALTER TABLE [dbo].[Rel_Artwork_Auctions]  WITH CHECK ADD FOREIGN KEY([AuctionId])
REFERENCES [dbo].[Auctions] ([AuctionId])
GO
ALTER TABLE [dbo].[Rel_Employee_Group]  WITH CHECK ADD FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[Rel_Employee_Group]  WITH CHECK ADD FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([GroupId])
GO
ALTER TABLE [dbo].[Rel_Group_Function]  WITH CHECK ADD FOREIGN KEY([FunctionId])
REFERENCES [dbo].[Functions] ([FunctionId])
GO
ALTER TABLE [dbo].[Rel_Group_Function]  WITH CHECK ADD FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([GroupId])
GO
ALTER TABLE [dbo].[Rel_Shipping_Payment]  WITH CHECK ADD FOREIGN KEY([PaymentId])
REFERENCES [dbo].[Payment_gateways] ([PaymentId])
GO
ALTER TABLE [dbo].[Rel_Shipping_Payment]  WITH CHECK ADD FOREIGN KEY([ShippingId])
REFERENCES [dbo].[Shipping] ([ShippingId])
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD FOREIGN KEY([ArtworkId])
REFERENCES [dbo].[Artwork] ([ArtworkId])
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
