USE [master]
GO

IF DB_ID('BOOKSHOP') IS NOT NULL
DROP DATABASE BOOKSHOP

/****** Object:  Database [BOOKSHOP]    Script Date: 1/4/2024 7:21:21 PM ******/
CREATE DATABASE [BOOKSHOP]
 CONTAINMENT = NONE
GO
ALTER DATABASE [BOOKSHOP] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BOOKSHOP].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BOOKSHOP] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BOOKSHOP] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BOOKSHOP] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BOOKSHOP] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BOOKSHOP] SET ARITHABORT OFF 
GO
ALTER DATABASE [BOOKSHOP] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BOOKSHOP] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BOOKSHOP] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BOOKSHOP] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BOOKSHOP] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BOOKSHOP] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BOOKSHOP] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BOOKSHOP] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BOOKSHOP] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BOOKSHOP] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BOOKSHOP] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BOOKSHOP] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BOOKSHOP] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BOOKSHOP] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BOOKSHOP] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BOOKSHOP] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BOOKSHOP] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BOOKSHOP] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BOOKSHOP] SET  MULTI_USER 
GO
ALTER DATABASE [BOOKSHOP] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BOOKSHOP] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BOOKSHOP] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BOOKSHOP] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BOOKSHOP] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BOOKSHOP] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [BOOKSHOP] SET QUERY_STORE = ON
GO
ALTER DATABASE [BOOKSHOP] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [BOOKSHOP]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 1/4/2024 7:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[ID] [varchar](10) NOT NULL,
	[CategoryName] [nvarchar](100) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 1/4/2024 7:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[ID] [varchar](10) NOT NULL,
	[CustomerName] [nvarchar](100) NULL,
	[TelephoneNumber] [nchar](10) NULL,
	[Address] [nvarchar](200) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 1/4/2024 7:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[ID] [varchar](10) NOT NULL,
	[OrderDate] [date] NULL,
	[CustomerID] [varchar](10) NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 1/4/2024 7:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[OrderID] [varchar](10) NOT NULL,
	[ProductID] [varchar](10) NOT NULL,
	[Quantity] [int] NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 1/4/2024 7:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [varchar](10) NOT NULL,
	[ProductName] [nvarchar](100) NULL,
	[Author] [nvarchar](100) NULL,
	[PublishYear] [int] NULL,
	[Publisher] [nvarchar](100) NULL,
	[CostPrice] [int] NULL,
	[SellingPrice] [int] NULL,
	[CategoryID] [varchar](10) NULL,
	[Quantity] [int] NULL,
	[ImagePath] [nvarchar](100) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([ID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Customer]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([ID])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Order]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Product]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([ID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
USE [master]
GO
ALTER DATABASE [BOOKSHOP] SET  READ_WRITE 
GO

USE BOOKSHOP
GO


-------------- INSERT DATA -----------------
INSERT [dbo].[Category] ([ID], [CategoryName]) VALUES ('VH', N'Văn học')
INSERT [dbo].[Category] ([ID], [CategoryName]) VALUES ('KT', N'Kinh tế')
INSERT [dbo].[Category] ([ID], [CategoryName]) VALUES ('TN', N'Thiếu nhi')
INSERT [dbo].[Category] ([ID], [CategoryName]) VALUES ('KNS', N'Kỹ năng sống')
INSERT [dbo].[Category] ([ID], [CategoryName]) VALUES ('KTTH', N'Kiến thức tổng hợp')
INSERT [dbo].[Category] ([ID], [CategoryName]) VALUES ('SGK', N'Sách giáo khoa')
GO
INSERT [dbo].[Customer] ([ID], [CustomerName], [TelephoneNumber], [Address]) VALUES ('C1', N'Lê Thị Ánh Minh', N'0945956844', N'16 Nguyễn Gia Thiều, Phường 4, Quận 3, TP. Hồ Chí Minh')
INSERT [dbo].[Customer] ([ID], [CustomerName], [TelephoneNumber], [Address]) VALUES ('C2', N'Hoàng Anh Khoa', N'0914807796', N'24 Dạ Nam, Phường 2, Quận 8, TP. Hồ Chí Minh')
INSERT [dbo].[Customer] ([ID], [CustomerName], [TelephoneNumber], [Address]) VALUES ('C3', N'Đỗ Tiến Nhật', N'0917953248', N'27/12 Điện Biên Phủ, Phường 15, Quận Bình Thạnh, TP. Hồ Chí Minh')
INSERT [dbo].[Customer] ([ID], [CustomerName], [TelephoneNumber], [Address]) VALUES ('C4', N'Phan Gia Bảo', N'0934585545', N'120 Mạc Đĩnh Chi, Phường Đông Hòa, TP. Dĩ An, Tỉnh Bình Dương')
INSERT [dbo].[Customer] ([ID], [CustomerName], [TelephoneNumber], [Address]) VALUES ('C5', N'Nguyễn Khánh Ngọc', N'0912904460', N'96 Phan Văn Trị, Phường 25, Quận Bình Thạnh, TP. Hồ Chí Minh')

GO
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'SACH001', N'Đắc nhân tâm', N'Dale Carnegie', 1936, N'NXB Trẻ', 100000, 150000, N'KNS', 100, NULL)
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES ('P1' , N'Cây Cam Ngọt Của Tôi', N'José Mauro de Vasconcelos', 1968, N'Nhà Xuất Bản Hội Nhà Văn', 70000, 65000, 'VH', 100, NULL);
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES ('P2' , N'Một Thoáng Ta Rực Rỡ Ở Nhân Gian', N'Ocean Vương', 2019, N'Nhà Xuất Bản Hội Nhà Văn', 120000, 100000, 'VH', 50, NULL);
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES ('P3' , N'Câu Chuyện Dòng Sông', N'Herman Hesse', 1922, N'Nhà Xuất Bản Hồng Đức', 80000, 78000, 'VH', 40, NULL);
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES ('P4' , N'Tết Ở Làng Địa Ngục', N'Thảo Trang', 2022, N'NXB Thanh Niên', 150000, 116000, 'VH', 40, NULL);
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES ('P5' , N'Nỗi Nhục', N'Annie Ernaux', 1997, N'Nhà xuất bản Phụ Nữ', 140000, 120000, 'VH', 100, NULL);
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES ('P6' , N'Chuyện Con Mèo Dạy Hải Âu Bay', N'Luis Sepúlveda', 1996, N'Nhà Xuất Bản Thế Giới', 40000, 36000, 'TN', 60, NULL);
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES ('P7' , N'Hoàng Tử Bé', N'Antoine de Saint-Exupéry', 1943, N'Nhà Xuất Bản Hội Nhà Văn', 75000, 60000, 'TN', 60, NULL);
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES ('P8' , N'Nhóc Nicolas', N'Goscinny & Sempé', 1959, N'Nhà Xuất Bản Hội Nhà Văn', 100000, 89000, 'TN', 40, NULL);
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES ('P9' , N'Vừa Nhắm Mắt Vừa Mở Cửa Số', N'Nguyễn Ngọc Thuần', 2002, N'NXB Trẻ', 70000, 50500, 'TN', 40, NULL);
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES ('P11', N'Lý Thuyết Trò Chơi', N'Trần Phách Hàm', 2014, N'NXB Dân Trí', 20000, 119000, 'KNS', 60, NULL);
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES ('P12', N'Giải Phóng Bộ Não Khỏi Tư Duy Độc Hại', N'Steven Schuster', 1974, N'Nhà Xuất Bản Lao Động', 150000, 104000, 'KNS', 50, NULL);
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES ('P13', N'Khi Mọi Điểm Tựa Đều Mất', N'Nhiều tác giả', 2020, N'Nhà Xuất Bản Tổng hợp TP.HCM', 88000, 73500, 'KNS', 140, NULL);
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES ('P14', N'Hiểu Về Trái Tim', N'Minh Niệm', 2011, N'Nhà Xuất Bản Tổng hợp TP.HCM', 150000, 130500, 'KNS', 30, NULL);
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES ('P15', N'Đi Tìm Lẽ Sống', N'Viktor Emil Frankl', 1946, N'Nhà Xuất Bản Tổng hợp TP.HCM', 70000, 66000, 'KNS', 80, NULL);
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES ('P16', N'Tâm Lý Học Về Tiền', N'Morgan Housel', 2003, N'NXB Dân Trí', 160000, 149000, 'KT', 100, NULL);
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES ('P17', N'Người Giàu Có Nhất Thành Babylon', N'GEORGE SAMUEL CLASON', 1926, N'Penguin Books', 80000, 67000, 'KT', 60, NULL);
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES ('P18', N'Lập Kế Hoạch Quản Lý Tài Chính Cá Nhân', N'Kristy Shen, Bryce Leung', 1991, N'Nhà Xuất Bản Hội Nhà Văn', 150000, 132300, 'KT', 40, NULL);
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES ('P19', N'Lãnh đạo là ngôn ngữ', N'L. David Marquet', 1974, N'Nhà Xuất Bản Hội Nhà Văn', 500000, 349000, 'KT', 40, NULL);
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES ('P21', N'Người Đua Diều', N'Khaled Hosseini', 2003, N'Nhà Xuất Bản Hội Nhà Văn', 100000, 81270, 'VH', 60, NULL);
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES ('P22', N'Mùa hè không tên', N'Nguyễn Nhật Ánh', 2023, N'NXB Trẻ', 100000, 87000, 'VH', 50, NULL);
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES ('P23', N'Đám Trẻ Ở Đại Dương Đen', N'Châu Sa Đáy Mắt', 2023, N'Nhà xuất bản Thế Giới', 79000, 70000, 'VH', 140, NULL);
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES ('P24', N'Nhà Giả Kim', N'Paulo Coelho', 2013, N'Nhà Xuất Bản Hội Nhà Văn', 90000, 58800, 'VH', 50, NULL);
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES ('P25', N'Điều Kỳ Diệu Của Tiệm Tạp Hóa Namiya', N'Higashino Keigo', 2016, N'Nhà Xuất Bản Hội Nhà Văn', 70000, 66150, 'VH', 140, NULL);
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES ('P26', N'Chủ nghĩa khắc kỷ - phong cách sống bản lĩnh và bình thản', N'William B.Irvine', 2018, N'NXB Công Thương', 150000, 106000, 'KTTH', 30, NULL);
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES ('P27', N'Thần Số Học Ứng Dụng', N'Joy Woodward', 2021, N'NXB Thanh Niên', 100000, 90300, 'KTTH', 80, NULL);
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES ('P28', N'Định luật Murphy – Mọi bí mật tâm lý thao túng cuộc đời bạn', N'Từ Thính Phong', 2020, N'Nhà xuất bản Thế Giới', 70000, 65461, 'KTTH', 40, NULL);
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES ('P29', N'Những Tù Nhân Của Địa Lý', N'Tim Marshall', 2020, N'Nhà Xuất Bản Hội Nhà Văn', 200000, 157000, 'KTTH', 40, NULL);
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES ('P31', N'Kinh Tế Học Vĩ Mô', N'Gregory Mankiw', 2014, N'Nhà Xuất Bản Hồng Đức', 300000, 259200, 'SGK', 60, NULL);
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES ('P32', N'Tâm Lí Học Đại Cương', N'Nhiều tác giả', 1997, N'Nhà Xuất Bản Đại Học Sư Phạm', 50000, 48500, 'SGK', 20, NULL);
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES ('P33', N'Tiếng Việt lớp 1 - Tập 1 (Bộ sách Giáo khoa Cánh Diều)', N'Nguyễn Minh Thuyết (Chủ biên)', 2018, N'Nhà xuất bản Đại học sư phạm', 40000, 34000, 'SGK', 80, NULL);
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES ('P34', N'Tư Tưởng Hồ Chí Minh', N'Bộ Giáo Dục Và Đào Tạo', 2015, N'Nhà Xuất Bản Chính Trị Quốc Gia Sự Thật', 60000, 51000, 'SGK', 75, NULL);
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES ('P35', N'Cấp Thoát Nước Trong Nhà', N'Bộ Xây dựng', 2020, N'NXB Xây Dựng', 70000, 68000, 'SGK', 50, NULL);

GO

INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('O1', CAST(N'2023-11-01' AS Date), 'C1')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('O2', CAST(N'2023-11-01' AS Date), 'C2')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('O3', CAST(N'2023-11-02' AS Date), 'C2')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('O4', CAST(N'2023-11-03' AS Date), 'C3')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('O5', CAST(N'2023-11-04' AS Date), 'C4')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('O6', CAST(N'2023-11-04' AS Date), 'C5')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('O7', CAST(N'2023-11-05' AS Date), 'C5')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('O8', CAST(N'2023-11-06' AS Date), 'C1')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('O9', CAST(N'2023-11-06' AS Date), 'C1')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('10', CAST(N'2023-11-06' AS Date), 'C2')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('11', CAST(N'2023-12-01' AS Date), 'C1')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('12', CAST(N'2023-12-01' AS Date), 'C4')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('13', CAST(N'2023-12-02' AS Date), 'C2')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('14', CAST(N'2023-12-03' AS Date), 'C1')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('15', CAST(N'2023-12-04' AS Date), 'C4')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('17', CAST(N'2023-12-05' AS Date), 'C2')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('16', CAST(N'2023-12-05' AS Date), 'C2')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('18', CAST(N'2023-12-06' AS Date), 'C3')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('19', CAST(N'2023-12-06' AS Date), 'C1')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('20', CAST(N'2023-12-06' AS Date), 'C2')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('21', CAST(N'2023-01-01' AS Date), 'C2')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('22', CAST(N'2024-01-02' AS Date), 'C5')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('23', CAST(N'2024-01-02' AS Date), 'C4')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('24', CAST(N'2024-01-02' AS Date), 'C3')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('25', CAST(N'2024-01-04' AS Date), 'C1')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('27', CAST(N'2024-01-05' AS Date), 'C5')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('26', CAST(N'2024-01-05' AS Date), 'C4')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('28', CAST(N'2024-01-06' AS Date), 'C3')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('29', CAST(N'2024-01-06' AS Date), 'C4')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('30', CAST(N'2024-01-06' AS Date), 'C1')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('31', CAST(N'2024-01-06' AS Date), 'C1')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('32', CAST(N'2024-01-02' AS Date), 'C5')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('33', CAST(N'2024-01-02' AS Date), 'C4')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('34', CAST(N'2024-01-02' AS Date), 'C3')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES ('35', CAST(N'2024-01-04' AS Date), 'C1')
GO
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('O1', 'P1', 10)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('O1', 'P2', 7)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('O2', 'P11', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('O2', 'P6', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('O2', 'P3', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('O3', 'P2', 10)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('O4', 'P11', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('O5', 'P5', 5)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('O6', 'P1', 10)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('O7', 'P25', 7)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('O7', 'P15', 7)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('O7', 'P34', 7)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('O8', 'P12', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('O9', 'P6', 9)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('10', 'P9', 9)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('11', 'P2', 10)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('12', 'P4', 9)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('13', 'P5', 5)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('14', 'P31', 10)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('15', 'P2', 7)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('16', 'P11', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('17', 'P16', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('18', 'P3', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('19', 'P22', 4)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('20', 'P4', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('21', 'P5', 5)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('22', 'P3', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('23', 'P22', 4)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('24', 'P4', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('24', 'P5', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('24', 'P6', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('25', 'P5', 5)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('26', 'P31', 4)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('27', 'P2', 7)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('27', 'P3', 7)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('27', 'P6', 7)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('27', 'P7', 7)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('28', 'P14', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('29', 'P6', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('29', 'P9', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('29', 'P12', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('29', 'P16', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('30', 'P13', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('31', 'P32', 6)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('31', 'P33', 6)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('31', 'P12', 6)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES ('31', 'P1', 10)