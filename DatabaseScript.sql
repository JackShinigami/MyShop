USE [master]
GO

IF DB_ID('BOOKSHOP') IS NOT NULL
DROP DATABASE BOOKSHOP
/****** Object:  Database [BOOKSHOP]    Script Date: 1/5/2024 10:07:47 PM ******/
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
/****** Object:  Table [dbo].[Category]    Script Date: 1/5/2024 10:07:47 PM ******/
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
/****** Object:  Table [dbo].[Customer]    Script Date: 1/5/2024 10:07:47 PM ******/
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
/****** Object:  Table [dbo].[Order]    Script Date: 1/5/2024 10:07:47 PM ******/
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
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 1/5/2024 10:07:47 PM ******/
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
/****** Object:  Table [dbo].[Product]    Script Date: 1/5/2024 10:07:47 PM ******/
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
INSERT [dbo].[Category] ([ID], [CategoryName]) VALUES (N'KNS', N'Kỹ năng sống')
INSERT [dbo].[Category] ([ID], [CategoryName]) VALUES (N'KT', N'Kinh tế')
INSERT [dbo].[Category] ([ID], [CategoryName]) VALUES (N'KTTH', N'Kiến thức tổng hợp')
INSERT [dbo].[Category] ([ID], [CategoryName]) VALUES (N'SGK', N'Sách giáo khoa')
INSERT [dbo].[Category] ([ID], [CategoryName]) VALUES (N'TN', N'Thiếu nhi')
INSERT [dbo].[Category] ([ID], [CategoryName]) VALUES (N'VH', N'Văn học')
GO
INSERT [dbo].[Customer] ([ID], [CustomerName], [TelephoneNumber], [Address]) VALUES (N'C1', N'Lê Thị Ánh Minh', N'0945956844', N'16 Nguyễn Gia Thiều, Phường 4, Quận 3, TP. Hồ Chí Minh')
INSERT [dbo].[Customer] ([ID], [CustomerName], [TelephoneNumber], [Address]) VALUES (N'C2', N'Hoàng Anh Khoa', N'0914807796', N'24 Dạ Nam, Phường 2, Quận 8, TP. Hồ Chí Minh')
INSERT [dbo].[Customer] ([ID], [CustomerName], [TelephoneNumber], [Address]) VALUES (N'C3', N'Đỗ Tiến Nhật', N'0917953248', N'27/12 Điện Biên Phủ, Phường 15, Quận Bình Thạnh, TP. Hồ Chí Minh')
INSERT [dbo].[Customer] ([ID], [CustomerName], [TelephoneNumber], [Address]) VALUES (N'C4', N'Phan Gia Bảo', N'0934585545', N'120 Mạc Đĩnh Chi, Phường Đông Hòa, TP. Dĩ An, Tỉnh Bình Dương')
INSERT [dbo].[Customer] ([ID], [CustomerName], [TelephoneNumber], [Address]) VALUES (N'C5', N'Nguyễn Khánh Ngọc', N'0912904460', N'96 Phan Văn Trị, Phường 25, Quận Bình Thạnh, TP. Hồ Chí Minh')
GO
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'10', CAST(N'2023-11-06' AS Date), N'C2')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'11', CAST(N'2023-12-01' AS Date), N'C1')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'12', CAST(N'2023-12-01' AS Date), N'C4')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'13', CAST(N'2023-12-02' AS Date), N'C2')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'14', CAST(N'2023-12-03' AS Date), N'C1')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'15', CAST(N'2023-12-04' AS Date), N'C4')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'16', CAST(N'2023-12-05' AS Date), N'C2')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'17', CAST(N'2023-12-05' AS Date), N'C2')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'18', CAST(N'2023-12-06' AS Date), N'C3')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'19', CAST(N'2023-12-06' AS Date), N'C1')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'20', CAST(N'2023-12-06' AS Date), N'C2')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'21', CAST(N'2023-01-01' AS Date), N'C2')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'22', CAST(N'2024-01-02' AS Date), N'C5')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'23', CAST(N'2024-01-02' AS Date), N'C4')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'24', CAST(N'2024-01-02' AS Date), N'C3')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'25', CAST(N'2024-01-04' AS Date), N'C1')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'26', CAST(N'2024-01-05' AS Date), N'C4')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'27', CAST(N'2024-01-05' AS Date), N'C5')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'28', CAST(N'2024-01-06' AS Date), N'C3')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'29', CAST(N'2024-01-06' AS Date), N'C4')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'30', CAST(N'2024-01-06' AS Date), N'C1')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'31', CAST(N'2024-01-06' AS Date), N'C1')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'32', CAST(N'2024-01-02' AS Date), N'C5')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'33', CAST(N'2024-01-02' AS Date), N'C4')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'34', CAST(N'2024-01-02' AS Date), N'C3')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'35', CAST(N'2024-01-04' AS Date), N'C1')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'O1', CAST(N'2023-11-01' AS Date), N'C1')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'O2', CAST(N'2023-11-01' AS Date), N'C2')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'O3', CAST(N'2023-11-02' AS Date), N'C2')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'O4', CAST(N'2023-11-03' AS Date), N'C3')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'O5', CAST(N'2023-11-04' AS Date), N'C4')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'O6', CAST(N'2023-11-04' AS Date), N'C5')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'O7', CAST(N'2023-11-05' AS Date), N'C5')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'O8', CAST(N'2023-11-06' AS Date), N'C1')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'O9', CAST(N'2023-11-06' AS Date), N'C1')
GO
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'10', N'P9', 9)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'11', N'P2', 10)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'12', N'P4', 9)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'13', N'P5', 5)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'14', N'P31', 10)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'15', N'P2', 7)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'16', N'P11', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'17', N'P16', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'18', N'P3', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'19', N'P22', 4)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'20', N'P4', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'21', N'P5', 5)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'22', N'P3', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'23', N'P22', 4)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'24', N'P4', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'24', N'P5', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'24', N'P6', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'25', N'P5', 5)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'26', N'P31', 4)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'27', N'P2', 7)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'27', N'P3', 7)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'27', N'P6', 7)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'27', N'P7', 7)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'28', N'P14', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'29', N'P12', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'29', N'P16', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'29', N'P6', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'29', N'P9', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'30', N'P13', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'31', N'P1', 10)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'31', N'P12', 6)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'31', N'P32', 6)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'31', N'P33', 6)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O1', N'P1', 10)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O1', N'P2', 7)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O2', N'P11', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O2', N'P3', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O2', N'P6', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O3', N'P2', 10)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O4', N'P11', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O5', N'P5', 5)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O6', N'P1', 10)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O7', N'P15', 7)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O7', N'P25', 7)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O7', N'P34', 7)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O8', N'P12', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O9', N'P6', 9)
GO
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P1', N'Cây Cam Ngọt Của Tôi', N'José Mauro de Vasconcelos', 1968, N'Nhà Xuất Bản Hội Nhà Văn', 65000, 70000, N'VH', 100, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P10', N'Dế Mèn Phiêu Lưu Ký', N'Tô Hoài', 1941, N'Nhà Xuất Bản Kim Đồng', 39500, 40000, N'TN', 100, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P11', N'Lý Thuyết Trò Chơi', N'Trần Phách Hàm', 2014, N'NXB Dân Trí', 119000, 200000, N'KNS', 60, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P12', N'Giải Phóng Bộ Não Khỏi Tư Duy Độc Hại', N'Steven Schuster', 1974, N'Nhà Xuất Bản Lao Động', 104000, 150000, N'KNS', 50, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P13', N'Khi Mọi Điểm Tựa Đều Mất', N'Nhiều tác giả', 2020, N'Nhà Xuất Bản Tổng hợp TP.HCM', 73500, 88000, N'KNS', 140, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P14', N'Hiểu Về Trái Tim', N'Minh Niệm', 2011, N'Nhà Xuất Bản Tổng hợp TP.HCM', 130500, 150000, N'KNS', 30, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P15', N'Đi Tìm Lẽ Sống', N'Viktor Emil Frankl', 1946, N'Nhà Xuất Bản Tổng hợp TP.HCM', 66000, 70000, N'KNS', 80, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P16', N'Tâm Lý Học Về Tiền', N'Morgan Housel', 2003, N'NXB Dân Trí', 149000, 160000, N'KT', 100, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P17', N'Người Giàu Có Nhất Thành Babylon', N'GEORGE SAMUEL CLASON', 1926, N'Penguin Books', 67000, 80000, N'KT', 60, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P18', N'Lập Kế Hoạch Quản Lý Tài Chính Cá Nhân', N'Kristy Shen, Bryce Leung', 1991, N'Nhà Xuất Bản Hội Nhà Văn', 132300, 150000, N'KT', 40, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P19', N'Lãnh đạo là ngôn ngữ', N'L. David Marquet', 1974, N'Nhà Xuất Bản Hội Nhà Văn', 349000, 500000, N'KT', 40, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P2', N'Một Thoáng Ta Rực Rỡ Ở Nhân Gian', N'Ocean Vương', 2019, N'Nhà Xuất Bản Hội Nhà Văn', 100000, 120000, N'VH', 50, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P20', N'Nghệ Thuật Tư Duy Chiến Lược', N'Avinash K. Dixit, Barry J. Nalebuff', 2020, N'NXB Công Thương', 159000, 200000, N'KT', 100, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P21', N'Người Đua Diều', N'Khaled Hosseini', 2003, N'Nhà Xuất Bản Hội Nhà Văn', 81270, 100000, N'VH', 60, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P22', N'Mùa hè không tên', N'Nguyễn Nhật Ánh', 2023, N'NXB Trẻ', 87000, 100000, N'VH', 50, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P23', N'Đám Trẻ Ở Đại Dương Đen', N'Châu Sa Đáy Mắt', 2023, N'Nhà xuất bản Thế Giới', 70000, 79000, N'VH', 140, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P24', N'Nhà Giả Kim', N'Paulo Coelho', 2013, N'Nhà Xuất Bản Hội Nhà Văn', 58800, 90000, N'VH', 50, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P25', N'Điều Kỳ Diệu Của Tiệm Tạp Hóa Namiya', N'Higashino Keigo', 2016, N'Nhà Xuất Bản Hội Nhà Văn', 66150, 70000, N'VH', 140, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P26', N'Chủ nghĩa khắc kỷ - phong cách sống bản lĩnh và bình thản', N'William B.Irvine', 2018, N'NXB Công Thương', 106000, 150000, N'KTTH', 30, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P27', N'Thần Số Học Ứng Dụng', N'Joy Woodward', 2021, N'NXB Thanh Niên', 90300, 100000, N'KTTH', 80, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P28', N'Định luật Murphy – Mọi bí mật tâm lý thao túng cuộc đời bạn', N'Từ Thính Phong', 2020, N'Nhà xuất bản Thế Giới', 65461, 70000, N'KTTH', 40, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P29', N'Những Tù Nhân Của Địa Lý', N'Tim Marshall', 2020, N'Nhà Xuất Bản Hội Nhà Văn', 157000, 200000, N'KTTH', 40, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P3', N'Câu Chuyện Dòng Sông', N'Herman Hesse', 1922, N'Nhà Xuất Bản Hồng Đức', 78000, 80000, N'VH', 40, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P30', N'Khoa Học Về Chạy Bộ - Phân Tích Kỹ Thuật, 
Phòng Ngừa Chấn Thương, Đổi Mới Cách Tập Luyện', N'TS. Chris Napier', 2010, N'Nhà xuất bản Thế Giới', 261000, 500000, N'KTTH', 100, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P31', N'Kinh Tế Học Vĩ Mô', N'NGregory Mankiw', 2014, N'Nhà Xuất Bản Hồng Đức', 259200, 300000, N'SGK', 60, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P32', N'Tâm Lí Học Đại Cương', N'Bộ Giáo Dục Và Đào Tạo', 1997, N'Nhà Xuất Bản Đại Học Sư Phạm', 48500, 50000, N'SGK', 20, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P33', N'Tiếng Việt lớp 1 - Tập 1 (Bộ sách Giáo khoa Cánh Diều)', N'Nguyễn Minh Thuyết (Chủ biên)', 2018, N'nhà xuất bản Đại học sư phạm', 34000, 40000, N'SGK', 80, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P34', N'Tư Tưởng Hồ Chí Minh', N'Bộ Giáo Dục Và Đào Tạo', 2015, N'Nhà Xuất Bản Chính Trị Quốc Gia Sự Thật', 51000, 60000, N'SGK', 75, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P35', N'Cấp Thoát Nước Trong Nhà ', N'Bộ Xây dựng', 2020, N'NXB Xây Dựng', 68000, 70000, N'SGK', 50, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P4', N'Tết Ở Làng Địa Ngục', N'Thảo Trang', 2022, N'NXB Thanh Niên', 116000, 150000, N'VH', 40, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P5', N'Nỗi Nhục', N'Annie Ernaux', 1997, N'Nhà xuất bản Phụ Nữ', 120000, 140000, N'VH', 100, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P6', N'Chuyện Con Mèo Dạy Hải Âu Bay', N'Luis Sepúlveda', 1996, N'Nhà Xuất Bản Thế Giới', 36000, 40000, N'TN', 60, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P7', N'Hoàng Tử Bé', N'Antoine de Saint-Exupéry', 1943, N'Nhà Xuất Bản Hội Nhà Văn', 60000, 75000, N'TN', 60, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P8', N'Nhóc Nicolas', N'Goscinny & Sempé', 1959, N'Nhà Xuất Bản Hội Nhà Văn', 89000, 100000, N'TN', 40, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P9', N'Vừa Nhắm Mắt Vừa Mở Cửa Số', N'Nguyễn Ngọc Thuần ', 2002, N'NXB Trẻ', 50500, 70000, N'TN', 40, N'')
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
