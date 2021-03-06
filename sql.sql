USE [RCM]
GO
/****** Object:  Database [RCM]    Script Date: 09/03/2019 12:12:21 PM ******/

ALTER DATABASE [RCM] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RCM].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RCM] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RCM] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RCM] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RCM] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RCM] SET ARITHABORT OFF 
GO
ALTER DATABASE [RCM] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RCM] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RCM] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RCM] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RCM] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RCM] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RCM] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RCM] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RCM] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RCM] SET  ENABLE_BROKER 
GO
ALTER DATABASE [RCM] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RCM] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RCM] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RCM] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RCM] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RCM] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [RCM] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RCM] SET RECOVERY FULL 
GO
ALTER DATABASE [RCM] SET  MULTI_USER 
GO
ALTER DATABASE [RCM] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RCM] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RCM] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RCM] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RCM] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RCM] SET QUERY_STORE = OFF
GO
USE [RCM]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [RCM]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 09/03/2019 12:12:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 09/03/2019 12:12:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 09/03/2019 12:12:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 09/03/2019 12:12:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 09/03/2019 12:12:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 09/03/2019 12:12:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 09/03/2019 12:12:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[IsBanned] [bit] NOT NULL,
	[Connection] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[LocationId] [int] NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 09/03/2019 12:12:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssignedCollectors]    Script Date: 09/03/2019 12:12:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssignedCollectors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Status] [int] NOT NULL,
	[UserId] [nvarchar](450) NULL,
	[ReceivableId] [int] NOT NULL,
 CONSTRAINT [PK_AssignedCollectors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CollectionProgresses]    Script Date: 09/03/2019 12:12:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CollectionProgresses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Status] [int] NOT NULL,
	[ReceivableId] [int] NOT NULL,
	[ProfileId] [int] NOT NULL,
 CONSTRAINT [PK_CollectionProgresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 09/03/2019 12:12:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Type] [int] NOT NULL,
	[IdNo] [nvarchar](15) NULL,
	[Name] [nvarchar](100) NULL,
	[Phone] [nvarchar](15) NULL,
	[Address] [nvarchar](100) NULL,
	[ReceivableId] [int] NOT NULL,
 CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 09/03/2019 12:12:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Code] [nvarchar](15) NULL,
	[Name] [nvarchar](100) NULL,
	[Phone] [nvarchar](15) NULL,
	[Address] [nvarchar](100) NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HubUserConnections]    Script Date: 09/03/2019 12:12:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HubUserConnections](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NULL,
	[Connection] [nvarchar](max) NULL,
 CONSTRAINT [PK_HubUserConnections] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Locations]    Script Date: 09/03/2019 12:12:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Locations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Description] [nvarchar](100) NULL,
	[Longitude] [float] NOT NULL,
	[Latitude] [float] NOT NULL,
 CONSTRAINT [PK_Locations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notifications]    Script Date: 09/03/2019 12:12:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Type] [nvarchar](max) NULL,
	[Title] [nvarchar](max) NULL,
	[Body] [nvarchar](max) NULL,
	[NData] [nvarchar](max) NULL,
	[IsSeen] [bit] NOT NULL,
	[UserId] [nvarchar](450) NULL,
 CONSTRAINT [PK_Notifications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProfileMessageForms]    Script Date: 09/03/2019 12:12:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfileMessageForms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Content] [nvarchar](max) NULL,
	[Type] [int] NOT NULL,
 CONSTRAINT [PK_ProfileMessageForms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Profiles]    Script Date: 09/03/2019 12:12:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profiles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[DebtAmountTo] [bigint] NOT NULL,
	[DebtAmountFrom] [bigint] NOT NULL,
 CONSTRAINT [PK_Profiles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProfileStageActions]    Script Date: 09/03/2019 12:12:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfileStageActions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[StartTime] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[Frequency] [smallint] NOT NULL,
	[ProfileStageId] [int] NOT NULL,
	[ProfileMessageFormId] [int] NULL,
 CONSTRAINT [PK_ProfileStageActions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProfileStages]    Script Date: 09/03/2019 12:12:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfileStages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Duration] [int] NOT NULL,
	[Sequence] [int] NOT NULL,
	[ProfileId] [int] NOT NULL,
 CONSTRAINT [PK_ProfileStages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProgressMessageForms]    Script Date: 09/03/2019 12:12:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProgressMessageForms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Content] [nvarchar](max) NULL,
	[Type] [int] NOT NULL,
 CONSTRAINT [PK_ProgressMessageForms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProgressStageActions]    Script Date: 09/03/2019 12:12:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProgressStageActions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[StartTime] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[ExcutionDay] [int] NOT NULL,
	[DoneAt] [int] NULL,
	[ProgressStageId] [int] NOT NULL,
	[ProgressMessageFormId] [int] NULL,
 CONSTRAINT [PK_ProgressStageActions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProgressStages]    Script Date: 09/03/2019 12:12:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProgressStages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Duration] [int] NOT NULL,
	[Sequence] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[CollectorComment] [nvarchar](max) NULL,
	[CollectionProgressId] [int] NOT NULL,
 CONSTRAINT [PK_ProgressStages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Receivables]    Script Date: 09/03/2019 12:12:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Receivables](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[ClosedDay] [int] NULL,
	[PayableDay] [int] NULL,
	[PrepaidAmount] [bigint] NOT NULL,
	[DebtAmount] [bigint] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[LocationId] [int] NULL,
 CONSTRAINT [PK_Receivables] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190227102640_Init', N'2.2.1-servicing-10028')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190305075123_UpdateReceivablePayable', N'2.2.2-servicing-10034')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'35c5e777-40a4-42df-ad45-2ec7a735a834', N'Admin', N'ADMIN', N'22e1d4a5-ba06-4a8a-8ecb-8d5a8ca0c2d9')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'8cf3e9ab-b9b1-4d58-858b-1c4a232f490d', N'Collector', N'COLLECTOR', N'3261b9f3-8dcb-4b48-ad9a-22107715f622')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'9b105f17-39fb-4955-b892-c81d52d01bee', N'Manager', N'MANAGER', N'6d59a811-81a3-4389-ac88-5778d9442ee2')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'3fffe6fe-793b-40b3-842a-f549d476511a', N'35c5e777-40a4-42df-ad45-2ec7a735a834')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a2f90c2f-5205-488a-8bd4-41c8b48588e2', N'8cf3e9ab-b9b1-4d58-858b-1c4a232f490d')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'bb1c689c-49c6-4d94-99ee-579eb7b4e6d8', N'8cf3e9ab-b9b1-4d58-858b-1c4a232f490d')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'db97453d-2ccf-4e8c-91d9-07074e6b2775', N'8cf3e9ab-b9b1-4d58-858b-1c4a232f490d')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b86a0483-3f84-445f-91f2-4657e457e502', N'9b105f17-39fb-4955-b892-c81d52d01bee')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [IsBanned], [Connection], [Address], [LocationId]) VALUES (N'3fffe6fe-793b-40b3-842a-f549d476511a', N'sa', N'SA', NULL, NULL, 0, N'AQAAAAEAACcQAAAAEIy2RXuKWg0aiCLDwqU/6F8UIOuLfQx3GK+usGfUa9DEuIdl4xISibnEOe7SopcigQ==', N'ZRDEFSLNBMVV2DV7ILCMGZPZNSJ45BA3', N'2cb66850-d17f-4a94-8f84-c71a3c6e7981', NULL, 0, 0, NULL, 1, 0, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [IsBanned], [Connection], [Address], [LocationId]) VALUES (N'a2f90c2f-5205-488a-8bd4-41c8b48588e2', N'thongvh', N'THONGVH', NULL, NULL, 0, N'AQAAAAEAACcQAAAAELwjanIQXOgVLeJFe0bWWF8g62ohg6fs9ihtay+cUOumNXHbiffNbc/H4oQKKrowVA==', N'S54XPIXXHEG7XGC6EWG7Q53IX363ALNR', N'98d58e3f-add6-4580-bdce-0bcf147edc36', NULL, 0, 0, NULL, 1, 0, N'Thong', N'Vo', 0, NULL, N'string', NULL)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [IsBanned], [Connection], [Address], [LocationId]) VALUES (N'b86a0483-3f84-445f-91f2-4657e457e502', N'manager', N'MANAGER', NULL, NULL, 0, N'AQAAAAEAACcQAAAAEBDe3owb6Xr12p/o1F5+1POvR3QzJXfxo+jWe8qP0+AvmeezzZsySFB9CAvfes3YSA==', N'YCPXICVRLPM2RSBNK3DBTZEAFDMXU3NU', N'935c0e65-a77e-4088-8b0f-21e97e081ff3', NULL, 0, 0, NULL, 1, 0, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [IsBanned], [Connection], [Address], [LocationId]) VALUES (N'bb1c689c-49c6-4d94-99ee-579eb7b4e6d8', N'collector', N'COLLECTOR', NULL, NULL, 0, N'AQAAAAEAACcQAAAAEMytA7BwyHP9XHtSFUnVQ/zTqwIwE44JfER+6nXuUgkjYNSO5gfmmnXpXtMNN5xgog==', N'VOYYOWGIGT7JZIDWVDTXOMRY4SP2KNEI', N'fca97f68-3c0e-4e96-91a9-fe4f959c0263', NULL, 0, 0, NULL, 1, 0, N'Hưng', N'Đặng', 0, NULL, N'string', NULL)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [IsBanned], [Connection], [Address], [LocationId]) VALUES (N'db97453d-2ccf-4e8c-91d9-07074e6b2775', N'namlt', N'NAMLT', NULL, NULL, 0, N'AQAAAAEAACcQAAAAEFCezUNfu+akHYhdm6ZMcnQD17f+bciNAdhttkbCnqZ7PAQqHHdRXYFdx+srlwktew==', N'HORCV447KX7RI3ADMRZRMX7WAE4AZVLV', N'f2d1169a-974d-4298-ae05-89646b8b3b45', NULL, 0, 0, NULL, 1, 0, N'Nam', N'Le', 0, NULL, N'29 Lê Trọng Tấn', NULL)
SET IDENTITY_INSERT [dbo].[AssignedCollectors] ON 

INSERT [dbo].[AssignedCollectors] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Status], [UserId], [ReceivableId]) VALUES (1, NULL, CAST(N'2019-03-07T11:21:13.3985842' AS DateTime2), 0, 1, N'db97453d-2ccf-4e8c-91d9-07074e6b2775', 1)
INSERT [dbo].[AssignedCollectors] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Status], [UserId], [ReceivableId]) VALUES (2, NULL, CAST(N'2019-03-07T11:21:13.4469263' AS DateTime2), 0, 1, N'db97453d-2ccf-4e8c-91d9-07074e6b2775', 2)
INSERT [dbo].[AssignedCollectors] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Status], [UserId], [ReceivableId]) VALUES (3, NULL, CAST(N'2019-03-07T11:21:13.4492939' AS DateTime2), 0, 1, N'db97453d-2ccf-4e8c-91d9-07074e6b2775', 3)
INSERT [dbo].[AssignedCollectors] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Status], [UserId], [ReceivableId]) VALUES (4, NULL, CAST(N'2019-03-07T12:49:23.4696487' AS DateTime2), 0, 1, N'db97453d-2ccf-4e8c-91d9-07074e6b2775', 4)
INSERT [dbo].[AssignedCollectors] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Status], [UserId], [ReceivableId]) VALUES (5, NULL, CAST(N'2019-03-07T12:49:23.5034807' AS DateTime2), 0, 1, N'db97453d-2ccf-4e8c-91d9-07074e6b2775', 5)
INSERT [dbo].[AssignedCollectors] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Status], [UserId], [ReceivableId]) VALUES (6, NULL, CAST(N'2019-03-07T12:49:23.5055957' AS DateTime2), 0, 1, N'db97453d-2ccf-4e8c-91d9-07074e6b2775', 6)
INSERT [dbo].[AssignedCollectors] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Status], [UserId], [ReceivableId]) VALUES (7, NULL, CAST(N'2019-03-07T13:15:39.2077629' AS DateTime2), 0, 1, N'db97453d-2ccf-4e8c-91d9-07074e6b2775', 7)
INSERT [dbo].[AssignedCollectors] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Status], [UserId], [ReceivableId]) VALUES (8, NULL, CAST(N'2019-03-07T13:15:39.3203060' AS DateTime2), 0, 1, N'db97453d-2ccf-4e8c-91d9-07074e6b2775', 8)
INSERT [dbo].[AssignedCollectors] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Status], [UserId], [ReceivableId]) VALUES (9, NULL, CAST(N'2019-03-07T13:15:39.3224216' AS DateTime2), 0, 1, N'db97453d-2ccf-4e8c-91d9-07074e6b2775', 9)
INSERT [dbo].[AssignedCollectors] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Status], [UserId], [ReceivableId]) VALUES (10, NULL, CAST(N'2019-03-07T14:02:16.6255813' AS DateTime2), 0, 1, N'a2f90c2f-5205-488a-8bd4-41c8b48588e2', 10)
INSERT [dbo].[AssignedCollectors] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Status], [UserId], [ReceivableId]) VALUES (11, NULL, CAST(N'2019-03-07T14:02:16.7719739' AS DateTime2), 0, 1, N'a2f90c2f-5205-488a-8bd4-41c8b48588e2', 11)
INSERT [dbo].[AssignedCollectors] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Status], [UserId], [ReceivableId]) VALUES (12, NULL, CAST(N'2019-03-07T14:02:16.7768570' AS DateTime2), 0, 1, N'a2f90c2f-5205-488a-8bd4-41c8b48588e2', 12)
SET IDENTITY_INSERT [dbo].[AssignedCollectors] OFF
SET IDENTITY_INSERT [dbo].[CollectionProgresses] ON 

INSERT [dbo].[CollectionProgresses] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Status], [ReceivableId], [ProfileId]) VALUES (1, NULL, CAST(N'2019-03-07T11:21:13.4467494' AS DateTime2), 0, 1, 1, 1)
INSERT [dbo].[CollectionProgresses] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Status], [ReceivableId], [ProfileId]) VALUES (2, NULL, CAST(N'2019-03-07T11:21:13.4492810' AS DateTime2), 0, 1, 2, 1)
INSERT [dbo].[CollectionProgresses] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Status], [ReceivableId], [ProfileId]) VALUES (3, NULL, CAST(N'2019-03-07T11:21:13.4511305' AS DateTime2), 0, 1, 3, 1)
INSERT [dbo].[CollectionProgresses] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Status], [ReceivableId], [ProfileId]) VALUES (4, NULL, CAST(N'2019-03-07T12:49:23.5034054' AS DateTime2), 0, 1, 4, 2)
INSERT [dbo].[CollectionProgresses] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Status], [ReceivableId], [ProfileId]) VALUES (5, NULL, CAST(N'2019-03-07T12:49:23.5055832' AS DateTime2), 0, 1, 5, 2)
INSERT [dbo].[CollectionProgresses] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Status], [ReceivableId], [ProfileId]) VALUES (6, NULL, CAST(N'2019-03-07T12:49:23.5071355' AS DateTime2), 0, 1, 6, 2)
INSERT [dbo].[CollectionProgresses] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Status], [ReceivableId], [ProfileId]) VALUES (7, NULL, CAST(N'2019-03-07T13:15:39.3202140' AS DateTime2), 0, 1, 7, 2)
INSERT [dbo].[CollectionProgresses] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Status], [ReceivableId], [ProfileId]) VALUES (8, NULL, CAST(N'2019-03-07T13:15:39.3224084' AS DateTime2), 0, 1, 8, 2)
INSERT [dbo].[CollectionProgresses] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Status], [ReceivableId], [ProfileId]) VALUES (9, NULL, CAST(N'2019-03-07T13:15:39.3239500' AS DateTime2), 0, 1, 9, 2)
INSERT [dbo].[CollectionProgresses] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Status], [ReceivableId], [ProfileId]) VALUES (10, NULL, CAST(N'2019-03-07T14:02:16.7701017' AS DateTime2), 0, 5, 10, 2)
INSERT [dbo].[CollectionProgresses] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Status], [ReceivableId], [ProfileId]) VALUES (11, NULL, CAST(N'2019-03-07T14:02:16.7768353' AS DateTime2), 0, 1, 11, 2)
INSERT [dbo].[CollectionProgresses] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Status], [ReceivableId], [ProfileId]) VALUES (12, NULL, CAST(N'2019-03-07T14:02:16.7788591' AS DateTime2), 0, 1, 12, 2)
SET IDENTITY_INSERT [dbo].[CollectionProgresses] OFF
SET IDENTITY_INSERT [dbo].[Contacts] ON 

INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (1, NULL, CAST(N'2019-03-07T11:21:13.4469011' AS DateTime2), 0, 0, N'45785241', N'Nguyễn Hồng Đức', N'84366031479', N'343D Lạc Long Quân Street, District 11, Hồ Chí Minh City', 1)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (2, NULL, CAST(N'2019-03-07T11:21:13.4469095' AS DateTime2), 0, 1, N'15475412', N'Nguyễn Thị B', N'84366031479', N'3 Dương Đình Nghệ Street, District 11, Hồ Chí Minh City', 1)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (3, NULL, CAST(N'2019-03-07T11:21:13.4469110' AS DateTime2), 0, 1, N'', N'Nguyễn Bé C', N'84366031479', N'', 1)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (4, NULL, CAST(N'2019-03-07T11:21:13.4492835' AS DateTime2), 0, 1, N'12465478', N'Lê Văn D', N'84366031479', N'14 Nguyễn Tri Phương Street, District 5, Hồ Chí Minh City', 2)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (5, NULL, CAST(N'2019-03-07T11:21:13.4492847' AS DateTime2), 0, 0, N'32564987', N'Thái Phú Cường', N'84366031479', N'2 Hàm Nghi Street, District 1, Hồ Chí Minh City', 2)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (6, NULL, CAST(N'2019-03-07T11:21:13.4492860' AS DateTime2), 0, 1, N'', N'Đặng Văn F', N'84366031479', N'', 2)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (7, NULL, CAST(N'2019-03-07T11:21:13.4492871' AS DateTime2), 0, 1, N'32478512', N'Ngô Thị G', N'84366031479', N'25 Trần Hưng Đạo Street, District 5, Hồ Chí Minh City', 2)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (8, NULL, CAST(N'2019-03-07T11:21:13.4492882' AS DateTime2), 0, 1, N'25478964', N'Tống Văn H', N'84366031479', N'3 Đinh Tiên Hoàng Street, District 1, Hồ Chí Minh City', 2)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (9, NULL, CAST(N'2019-03-07T11:21:13.4511330' AS DateTime2), 0, 0, N'25478964', N'Tiến Đại Ca', N'84981346599', N'3 Đinh Tiên Hoàng Street, District 1, Hồ Chí Minh City', 3)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (10, NULL, CAST(N'2019-03-07T11:21:13.4511343' AS DateTime2), 0, 1, N'', N'Vũ', N'84347656005', N'', 3)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (11, NULL, CAST(N'2019-03-07T12:49:23.5034629' AS DateTime2), 0, 0, N'45785241', N'Nguyễn Hồng Đức', N'84366031479', N'343D Lạc Long Quân Street, District 11, Hồ Chí Minh City', 4)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (12, NULL, CAST(N'2019-03-07T12:49:23.5034702' AS DateTime2), 0, 1, N'15475412', N'Nguyễn Thị B', N'84366031479', N'3 Dương Đình Nghệ Street, District 11, Hồ Chí Minh City', 4)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (13, NULL, CAST(N'2019-03-07T12:49:23.5034716' AS DateTime2), 0, 1, N'', N'Nguyễn Bé C', N'84366031479', N'', 4)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (14, NULL, CAST(N'2019-03-07T12:49:23.5055857' AS DateTime2), 0, 1, N'12465478', N'Lê Văn D', N'84366031479', N'14 Nguyễn Tri Phương Street, District 5, Hồ Chí Minh City', 5)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (15, NULL, CAST(N'2019-03-07T12:49:23.5055869' AS DateTime2), 0, 0, N'32564987', N'Thái Phú Cường', N'84366031479', N'2 Hàm Nghi Street, District 1, Hồ Chí Minh City', 5)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (16, NULL, CAST(N'2019-03-07T12:49:23.5055880' AS DateTime2), 0, 1, N'', N'Đặng Văn F', N'84366031479', N'', 5)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (17, NULL, CAST(N'2019-03-07T12:49:23.5055891' AS DateTime2), 0, 1, N'32478512', N'Ngô Thị G', N'84366031479', N'25 Trần Hưng Đạo Street, District 5, Hồ Chí Minh City', 5)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (18, NULL, CAST(N'2019-03-07T12:49:23.5055902' AS DateTime2), 0, 1, N'25478964', N'Tống Văn H', N'84366031479', N'3 Đinh Tiên Hoàng Street, District 1, Hồ Chí Minh City', 5)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (19, NULL, CAST(N'2019-03-07T12:49:23.5071380' AS DateTime2), 0, 0, N'25478964', N'Tiến Đại Ca', N'84981346599', N'3 Đinh Tiên Hoàng Street, District 1, Hồ Chí Minh City', 6)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (20, NULL, CAST(N'2019-03-07T12:49:23.5071391' AS DateTime2), 0, 1, N'', N'Vũ', N'84347656005', N'', 6)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (21, NULL, CAST(N'2019-03-07T13:15:39.3202811' AS DateTime2), 0, 0, N'45785241', N'Nguyễn Hồng Đức', N'84366031479', N'343D Lạc Long Quân Street, District 11, Hồ Chí Minh City', 7)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (22, NULL, CAST(N'2019-03-07T13:15:39.3202929' AS DateTime2), 0, 1, N'15475412', N'Nguyễn Thị B', N'84366031479', N'3 Dương Đình Nghệ Street, District 11, Hồ Chí Minh City', 7)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (23, NULL, CAST(N'2019-03-07T13:15:39.3202944' AS DateTime2), 0, 1, N'', N'Nguyễn Bé C', N'84366031479', N'', 7)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (24, NULL, CAST(N'2019-03-07T13:15:39.3224110' AS DateTime2), 0, 1, N'12465478', N'Lê Văn D', N'84366031479', N'14 Nguyễn Tri Phương Street, District 5, Hồ Chí Minh City', 8)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (25, NULL, CAST(N'2019-03-07T13:15:39.3224123' AS DateTime2), 0, 0, N'32564987', N'Thái Phú Cường', N'84366031479', N'2 Hàm Nghi Street, District 1, Hồ Chí Minh City', 8)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (26, NULL, CAST(N'2019-03-07T13:15:39.3224135' AS DateTime2), 0, 1, N'', N'Đặng Văn F', N'84366031479', N'', 8)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (27, NULL, CAST(N'2019-03-07T13:15:39.3224147' AS DateTime2), 0, 1, N'32478512', N'Ngô Thị G', N'84366031479', N'25 Trần Hưng Đạo Street, District 5, Hồ Chí Minh City', 8)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (28, NULL, CAST(N'2019-03-07T13:15:39.3224159' AS DateTime2), 0, 1, N'25478964', N'Tống Văn H', N'84366031479', N'3 Đinh Tiên Hoàng Street, District 1, Hồ Chí Minh City', 8)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (29, NULL, CAST(N'2019-03-07T13:15:39.3239525' AS DateTime2), 0, 0, N'25478964', N'Tiến Đại Ca', N'84981346599', N'3 Đinh Tiên Hoàng Street, District 1, Hồ Chí Minh City', 9)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (30, NULL, CAST(N'2019-03-07T13:15:39.3239538' AS DateTime2), 0, 1, N'', N'Vũ', N'84347656005', N'', 9)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (31, NULL, CAST(N'2019-03-07T14:02:16.7710024' AS DateTime2), 0, 0, N'45785241', N'Nguyễn Hồng Đức', N'84366031479', N'343D Lạc Long Quân Street, District 11, Hồ Chí Minh City', 10)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (32, NULL, CAST(N'2019-03-07T14:02:16.7710206' AS DateTime2), 0, 1, N'15475412', N'Nguyễn Thị B', N'84366031479', N'3 Dương Đình Nghệ Street, District 11, Hồ Chí Minh City', 10)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (33, NULL, CAST(N'2019-03-07T14:02:16.7710212' AS DateTime2), 0, 1, N'', N'Nguyễn Bé C', N'84366031479', N'', 10)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (34, NULL, CAST(N'2019-03-07T14:02:16.7768401' AS DateTime2), 0, 1, N'12465478', N'Lê Văn D', N'84366031479', N'14 Nguyễn Tri Phương Street, District 5, Hồ Chí Minh City', 11)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (35, NULL, CAST(N'2019-03-07T14:02:16.7768407' AS DateTime2), 0, 0, N'32564987', N'Thái Phú Cường', N'84366031479', N'2 Hàm Nghi Street, District 1, Hồ Chí Minh City', 11)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (36, NULL, CAST(N'2019-03-07T14:02:16.7768407' AS DateTime2), 0, 1, N'', N'Đặng Văn F', N'84366031479', N'', 11)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (37, NULL, CAST(N'2019-03-07T14:02:16.7768413' AS DateTime2), 0, 1, N'32478512', N'Ngô Thị G', N'84366031479', N'25 Trần Hưng Đạo Street, District 5, Hồ Chí Minh City', 11)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (38, NULL, CAST(N'2019-03-07T14:02:16.7768419' AS DateTime2), 0, 1, N'25478964', N'Tống Văn H', N'84366031479', N'3 Đinh Tiên Hoàng Street, District 1, Hồ Chí Minh City', 11)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (39, NULL, CAST(N'2019-03-07T14:02:16.7788627' AS DateTime2), 0, 0, N'25478964', N'Tiến Đại Ca', N'84981346599', N'3 Đinh Tiên Hoàng Street, District 1, Hồ Chí Minh City', 12)
INSERT [dbo].[Contacts] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [IdNo], [Name], [Phone], [Address], [ReceivableId]) VALUES (40, NULL, CAST(N'2019-03-07T14:02:16.7788633' AS DateTime2), 0, 1, N'', N'Vũ', N'84347656005', N'', 12)
SET IDENTITY_INSERT [dbo].[Contacts] OFF
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Code], [Name], [Phone], [Address]) VALUES (1, NULL, CAST(N'2019-03-07T11:20:53.8929768' AS DateTime2), 0, N'ACB', N'ACB Bank', N'12345678', N'HCM')
SET IDENTITY_INSERT [dbo].[Customers] OFF
SET IDENTITY_INSERT [dbo].[HubUserConnections] ON 

INSERT [dbo].[HubUserConnections] ([Id], [UserId], [Connection]) VALUES (9, N'3fffe6fe-793b-40b3-842a-f549d476511a', N'xDqC05L-o6lZw0rG5svczQ')
INSERT [dbo].[HubUserConnections] ([Id], [UserId], [Connection]) VALUES (19, N'3fffe6fe-793b-40b3-842a-f549d476511a', N'63ajYPFdfxFS_YFsa1vyEw')
INSERT [dbo].[HubUserConnections] ([Id], [UserId], [Connection]) VALUES (20, N'3fffe6fe-793b-40b3-842a-f549d476511a', N'X_TcYO1wu8vruDS6kP5yNA')
INSERT [dbo].[HubUserConnections] ([Id], [UserId], [Connection]) VALUES (21, N'3fffe6fe-793b-40b3-842a-f549d476511a', N'nqWsoCA7aMjTGFGWxfkVhQ')
INSERT [dbo].[HubUserConnections] ([Id], [UserId], [Connection]) VALUES (64, N'bb1c689c-49c6-4d94-99ee-579eb7b4e6d8', N'AfR5WsaZgPmAm5chyzNDgg')
SET IDENTITY_INSERT [dbo].[HubUserConnections] OFF
SET IDENTITY_INSERT [dbo].[Notifications] ON 

INSERT [dbo].[Notifications] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [Title], [Body], [NData], [IsSeen], [UserId]) VALUES (1, NULL, CAST(N'2019-02-20T19:57:20.5139108' AS DateTime2), 0, N'Hello', N'Mr.T Hello', N'Mr.T Hello everyone!!!', NULL, 0, N'3fffe6fe-793b-40b3-842a-f549d476511a')
INSERT [dbo].[Notifications] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [Title], [Body], [NData], [IsSeen], [UserId]) VALUES (2, NULL, CAST(N'2019-02-20T20:15:48.4278073' AS DateTime2), 0, N'Hello', N'Mr.H Hello', N'Mr.H Hello everyone!!!', NULL, 0, N'3fffe6fe-793b-40b3-842a-f549d476511a')
INSERT [dbo].[Notifications] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [Title], [Body], [NData], [IsSeen], [UserId]) VALUES (3, NULL, CAST(N'2019-02-21T01:30:51.9153691' AS DateTime2), 0, N'SMS', N'Send SMS', N'SMS need to be send to Nguyen Van A', NULL, 0, N'3fffe6fe-793b-40b3-842a-f549d476511a')
INSERT [dbo].[Notifications] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [Title], [Body], [NData], [IsSeen], [UserId]) VALUES (4, NULL, CAST(N'2019-02-21T01:36:05.2298378' AS DateTime2), 0, N'SMS', N'Send SMS', N'SMS need to be send to Nguyen Van B', NULL, 0, N'3fffe6fe-793b-40b3-842a-f549d476511a')
INSERT [dbo].[Notifications] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [Title], [Body], [NData], [IsSeen], [UserId]) VALUES (6, NULL, CAST(N'2019-02-21T01:40:16.7854290' AS DateTime2), 0, N'SMS sa', N'Send SMS', N'SMS need to be send to Nguyen Van B', NULL, 0, N'3fffe6fe-793b-40b3-842a-f549d476511a')
INSERT [dbo].[Notifications] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [Title], [Body], [NData], [IsSeen], [UserId]) VALUES (7, NULL, CAST(N'2019-02-21T02:39:24.1529862' AS DateTime2), 0, N'sa noti', N'string', N'sa sa', NULL, 0, N'3fffe6fe-793b-40b3-842a-f549d476511a')
INSERT [dbo].[Notifications] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [Title], [Body], [NData], [IsSeen], [UserId]) VALUES (9, NULL, CAST(N'2019-02-21T13:28:19.2449215' AS DateTime2), 0, N'Chịu hết nỗi', N'Giao diện thấy gớm', N'Không biết viết giao diện suốt ngày cứ demo API', NULL, 0, N'3fffe6fe-793b-40b3-842a-f549d476511a')
INSERT [dbo].[Notifications] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [Title], [Body], [NData], [IsSeen], [UserId]) VALUES (10, NULL, CAST(N'2019-03-03T12:59:26.7648266' AS DateTime2), 0, N'Test Mobile', N'Xamarin', N'Xamarin cùi bắp', NULL, 0, N'3fffe6fe-793b-40b3-842a-f549d476511a')
INSERT [dbo].[Notifications] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [Title], [Body], [NData], [IsSeen], [UserId]) VALUES (11, NULL, CAST(N'2019-03-03T13:00:44.9347904' AS DateTime2), 0, N'Test Mobile', N'Xamarin', N'Xamarin cùi bắp', NULL, 0, N'3fffe6fe-793b-40b3-842a-f549d476511a')
INSERT [dbo].[Notifications] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [Title], [Body], [NData], [IsSeen], [UserId]) VALUES (12, NULL, CAST(N'2019-03-03T13:01:42.3743313' AS DateTime2), 0, N'Test Mobile', N'Xamarin', N'Xamarin cùi bắp', NULL, 0, N'3fffe6fe-793b-40b3-842a-f549d476511a')
INSERT [dbo].[Notifications] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [Title], [Body], [NData], [IsSeen], [UserId]) VALUES (13, NULL, CAST(N'2019-03-03T13:10:12.3105648' AS DateTime2), 0, N'mobile', N'xamarin', N'Hello', NULL, 0, N'3fffe6fe-793b-40b3-842a-f549d476511a')
INSERT [dbo].[Notifications] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [Title], [Body], [NData], [IsSeen], [UserId]) VALUES (14, NULL, CAST(N'2019-03-03T13:11:45.0432548' AS DateTime2), 0, N'mobile', N'xamarin', N'Hello', NULL, 0, N'3fffe6fe-793b-40b3-842a-f549d476511a')
INSERT [dbo].[Notifications] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [Title], [Body], [NData], [IsSeen], [UserId]) VALUES (15, NULL, CAST(N'2019-03-03T13:19:57.8489256' AS DateTime2), 0, N'mobile', N'xamarin', N'Hello', NULL, 0, N'3fffe6fe-793b-40b3-842a-f549d476511a')
INSERT [dbo].[Notifications] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [Title], [Body], [NData], [IsSeen], [UserId]) VALUES (16, NULL, CAST(N'2019-03-05T11:39:27.0894383' AS DateTime2), 0, N'Collector noti', N'Collector title', N'Hello', NULL, 0, N'bb1c689c-49c6-4d94-99ee-579eb7b4e6d8')
INSERT [dbo].[Notifications] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Type], [Title], [Body], [NData], [IsSeen], [UserId]) VALUES (17, NULL, CAST(N'2019-03-05T11:40:21.5282076' AS DateTime2), 0, N'Collector noti', N'Collector title hihi', N'Hello anh Thông', NULL, 0, N'bb1c689c-49c6-4d94-99ee-579eb7b4e6d8')
SET IDENTITY_INSERT [dbo].[Notifications] OFF
SET IDENTITY_INSERT [dbo].[ProfileMessageForms] ON 

INSERT [dbo].[ProfileMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (1, NULL, CAST(N'2019-03-06T21:06:24.5765834' AS DateTime2), 0, N'Cuộc gọi dành cho khách hàng có khoảng nợ nhỏ', N'Xin kính chào [NAME], cuộc gọi này được gửi đến bạn đang nợ [AMOUNT], bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 1)
INSERT [dbo].[ProfileMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (2, NULL, CAST(N'2019-03-06T21:06:37.2779752' AS DateTime2), 0, N'Tin nhắn dành cho khách hàng có khoảng nợ nhỏ', N'Chào [NAME], tin nhắn này được gửi đến bạn vì bạn đang nợ [AMOUNT], bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 0)
SET IDENTITY_INSERT [dbo].[ProfileMessageForms] OFF
SET IDENTITY_INSERT [dbo].[Profiles] ON 

INSERT [dbo].[Profiles] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [DebtAmountTo], [DebtAmountFrom]) VALUES (1, NULL, CAST(N'2019-03-07T11:20:10.5932248' AS DateTime2), 0, N'Standard process', 0, 0)
INSERT [dbo].[Profiles] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [DebtAmountTo], [DebtAmountFrom]) VALUES (2, NULL, CAST(N'2019-03-07T12:47:45.2403314' AS DateTime2), 0, N'Main process', 0, 0)
SET IDENTITY_INSERT [dbo].[Profiles] OFF
SET IDENTITY_INSERT [dbo].[ProfileStageActions] ON 

INSERT [dbo].[ProfileStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Frequency], [ProfileStageId], [ProfileMessageFormId]) VALUES (1, NULL, CAST(N'2019-03-07T11:20:10.5928157' AS DateTime2), 0, N'SMS', 730, 0, 1, 1, 2)
INSERT [dbo].[ProfileStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Frequency], [ProfileStageId], [ProfileMessageFormId]) VALUES (2, NULL, CAST(N'2019-03-07T11:20:10.5928445' AS DateTime2), 0, N'Phone call', 730, 1, 1, 1, 1)
INSERT [dbo].[ProfileStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Frequency], [ProfileStageId], [ProfileMessageFormId]) VALUES (3, NULL, CAST(N'2019-03-07T11:20:10.5928461' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 1, 1, NULL)
INSERT [dbo].[ProfileStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Frequency], [ProfileStageId], [ProfileMessageFormId]) VALUES (4, NULL, CAST(N'2019-03-07T11:20:10.5928836' AS DateTime2), 0, N'SMS', 730, 0, 3, 2, 2)
INSERT [dbo].[ProfileStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Frequency], [ProfileStageId], [ProfileMessageFormId]) VALUES (5, NULL, CAST(N'2019-03-07T11:20:10.5928855' AS DateTime2), 0, N'Phone call', 730, 1, 5, 2, 1)
INSERT [dbo].[ProfileStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Frequency], [ProfileStageId], [ProfileMessageFormId]) VALUES (6, NULL, CAST(N'2019-03-07T11:20:10.5928867' AS DateTime2), 0, N'Visit', 730, 2, 10, 2, NULL)
INSERT [dbo].[ProfileStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Frequency], [ProfileStageId], [ProfileMessageFormId]) VALUES (7, NULL, CAST(N'2019-03-07T12:47:45.2400063' AS DateTime2), 0, N'SMS', 730, 0, 15, 3, 2)
INSERT [dbo].[ProfileStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Frequency], [ProfileStageId], [ProfileMessageFormId]) VALUES (8, NULL, CAST(N'2019-03-07T12:47:45.2400261' AS DateTime2), 0, N'Phone call', 730, 1, 15, 3, 1)
INSERT [dbo].[ProfileStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Frequency], [ProfileStageId], [ProfileMessageFormId]) VALUES (9, NULL, CAST(N'2019-03-07T12:47:45.2400277' AS DateTime2), 0, N'Báo cáo tiến độ lần 1', 730, 3, 25, 3, NULL)
INSERT [dbo].[ProfileStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Frequency], [ProfileStageId], [ProfileMessageFormId]) VALUES (10, NULL, CAST(N'2019-03-07T12:47:45.2400420' AS DateTime2), 0, N'SMS', 730, 0, 7, 4, 2)
INSERT [dbo].[ProfileStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Frequency], [ProfileStageId], [ProfileMessageFormId]) VALUES (11, NULL, CAST(N'2019-03-07T12:47:45.2400435' AS DateTime2), 0, N'Phone call', 730, 1, 15, 4, 1)
INSERT [dbo].[ProfileStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Frequency], [ProfileStageId], [ProfileMessageFormId]) VALUES (12, NULL, CAST(N'2019-03-07T12:47:45.2400446' AS DateTime2), 0, N'Báo cáo tiến độ lần 2', 730, 3, 25, 4, NULL)
INSERT [dbo].[ProfileStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Frequency], [ProfileStageId], [ProfileMessageFormId]) VALUES (13, NULL, CAST(N'2019-03-07T12:47:45.2400456' AS DateTime2), 0, N'Visit', 730, 2, 25, 4, NULL)
SET IDENTITY_INSERT [dbo].[ProfileStageActions] OFF
SET IDENTITY_INSERT [dbo].[ProfileStages] ON 

INSERT [dbo].[ProfileStages] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Duration], [Sequence], [ProfileId]) VALUES (1, NULL, CAST(N'2019-03-07T11:20:10.5928475' AS DateTime2), 0, N'Stage 1', 30, 1, 1)
INSERT [dbo].[ProfileStages] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Duration], [Sequence], [ProfileId]) VALUES (2, NULL, CAST(N'2019-03-07T11:20:10.5928878' AS DateTime2), 0, N'Stage 2', 30, 2, 1)
INSERT [dbo].[ProfileStages] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Duration], [Sequence], [ProfileId]) VALUES (3, NULL, CAST(N'2019-03-07T12:47:45.2400290' AS DateTime2), 0, N'Stage 1', 30, 1, 2)
INSERT [dbo].[ProfileStages] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Duration], [Sequence], [ProfileId]) VALUES (4, NULL, CAST(N'2019-03-07T12:47:45.2400468' AS DateTime2), 0, N'Stage 2', 30, 2, 2)
SET IDENTITY_INSERT [dbo].[ProfileStages] OFF
SET IDENTITY_INSERT [dbo].[ProgressMessageForms] ON 

INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Tin nhắn dành cho khách hàng có khoảng nợ nhỏ', N'Chào Nguyễn Hồng Đức, tin nhắn này được gửi đến bạn vì bạn đang nợ 1000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 0)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (2, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Cuộc gọi dành cho khách hàng có khoảng nợ nhỏ', N'Xin kính chào Nguyễn Hồng Đức, cuộc gọi này được gửi đến bạn đang nợ 1000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 1)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (3, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Tin nhắn dành cho khách hàng có khoảng nợ nhỏ', N'Chào Nguyễn Hồng Đức, tin nhắn này được gửi đến bạn vì bạn đang nợ 1000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 0)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (4, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Cuộc gọi dành cho khách hàng có khoảng nợ nhỏ', N'Xin kính chào Nguyễn Hồng Đức, cuộc gọi này được gửi đến bạn đang nợ 1000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 1)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (5, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Tin nhắn dành cho khách hàng có khoảng nợ nhỏ', N'Chào Thái Phú Cường, tin nhắn này được gửi đến bạn vì bạn đang nợ 50000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 0)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (6, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Cuộc gọi dành cho khách hàng có khoảng nợ nhỏ', N'Xin kính chào Thái Phú Cường, cuộc gọi này được gửi đến bạn đang nợ 50000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 1)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (7, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Tin nhắn dành cho khách hàng có khoảng nợ nhỏ', N'Chào Thái Phú Cường, tin nhắn này được gửi đến bạn vì bạn đang nợ 50000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 0)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (8, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Cuộc gọi dành cho khách hàng có khoảng nợ nhỏ', N'Xin kính chào Thái Phú Cường, cuộc gọi này được gửi đến bạn đang nợ 50000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 1)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (9, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Tin nhắn dành cho khách hàng có khoảng nợ nhỏ', N'Chào Tiến Đại Ca, tin nhắn này được gửi đến bạn vì bạn đang nợ 1000000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 0)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (10, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Cuộc gọi dành cho khách hàng có khoảng nợ nhỏ', N'Xin kính chào Tiến Đại Ca, cuộc gọi này được gửi đến bạn đang nợ 1000000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 1)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (11, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Tin nhắn dành cho khách hàng có khoảng nợ nhỏ', N'Chào Tiến Đại Ca, tin nhắn này được gửi đến bạn vì bạn đang nợ 1000000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 0)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (12, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Cuộc gọi dành cho khách hàng có khoảng nợ nhỏ', N'Xin kính chào Tiến Đại Ca, cuộc gọi này được gửi đến bạn đang nợ 1000000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 1)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (13, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Tin nhắn dành cho khách hàng có khoảng nợ nhỏ', N'Chào Nguyễn Hồng Đức, tin nhắn này được gửi đến bạn vì bạn đang nợ 1000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 0)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (14, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Cuộc gọi dành cho khách hàng có khoảng nợ nhỏ', N'Xin kính chào Nguyễn Hồng Đức, cuộc gọi này được gửi đến bạn đang nợ 1000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 1)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (15, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Tin nhắn dành cho khách hàng có khoảng nợ nhỏ', N'Chào Nguyễn Hồng Đức, tin nhắn này được gửi đến bạn vì bạn đang nợ 1000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 0)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (16, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Cuộc gọi dành cho khách hàng có khoảng nợ nhỏ', N'Xin kính chào Nguyễn Hồng Đức, cuộc gọi này được gửi đến bạn đang nợ 1000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 1)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (17, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Tin nhắn dành cho khách hàng có khoảng nợ nhỏ', N'Chào Thái Phú Cường, tin nhắn này được gửi đến bạn vì bạn đang nợ 50000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 0)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (18, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Cuộc gọi dành cho khách hàng có khoảng nợ nhỏ', N'Xin kính chào Thái Phú Cường, cuộc gọi này được gửi đến bạn đang nợ 50000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 1)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (19, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Tin nhắn dành cho khách hàng có khoảng nợ nhỏ', N'Chào Thái Phú Cường, tin nhắn này được gửi đến bạn vì bạn đang nợ 50000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 0)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (20, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Cuộc gọi dành cho khách hàng có khoảng nợ nhỏ', N'Xin kính chào Thái Phú Cường, cuộc gọi này được gửi đến bạn đang nợ 50000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 1)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (21, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Tin nhắn dành cho khách hàng có khoảng nợ nhỏ', N'Chào Tiến Đại Ca, tin nhắn này được gửi đến bạn vì bạn đang nợ 1000000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 0)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (22, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Cuộc gọi dành cho khách hàng có khoảng nợ nhỏ', N'Xin kính chào Tiến Đại Ca, cuộc gọi này được gửi đến bạn đang nợ 1000000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 1)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (23, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Tin nhắn dành cho khách hàng có khoảng nợ nhỏ', N'Chào Tiến Đại Ca, tin nhắn này được gửi đến bạn vì bạn đang nợ 1000000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 0)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (24, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Cuộc gọi dành cho khách hàng có khoảng nợ nhỏ', N'Xin kính chào Tiến Đại Ca, cuộc gọi này được gửi đến bạn đang nợ 1000000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 1)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (25, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Tin nhắn dành cho khách hàng có khoảng nợ nhỏ', N'Chào Nguyễn Hồng Đức, tin nhắn này được gửi đến bạn vì bạn đang nợ 1000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 0)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (26, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Cuộc gọi dành cho khách hàng có khoảng nợ nhỏ', N'Xin kính chào Nguyễn Hồng Đức, cuộc gọi này được gửi đến bạn đang nợ 1000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 1)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (27, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Tin nhắn dành cho khách hàng có khoảng nợ nhỏ', N'Chào Nguyễn Hồng Đức, tin nhắn này được gửi đến bạn vì bạn đang nợ 1000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 0)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (28, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Cuộc gọi dành cho khách hàng có khoảng nợ nhỏ', N'Xin kính chào Nguyễn Hồng Đức, cuộc gọi này được gửi đến bạn đang nợ 1000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 1)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (29, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Tin nhắn dành cho khách hàng có khoảng nợ nhỏ', N'Chào Thái Phú Cường, tin nhắn này được gửi đến bạn vì bạn đang nợ 50000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 0)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (30, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Cuộc gọi dành cho khách hàng có khoảng nợ nhỏ', N'Xin kính chào Thái Phú Cường, cuộc gọi này được gửi đến bạn đang nợ 50000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 1)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (31, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Tin nhắn dành cho khách hàng có khoảng nợ nhỏ', N'Chào Thái Phú Cường, tin nhắn này được gửi đến bạn vì bạn đang nợ 50000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 0)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (32, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Cuộc gọi dành cho khách hàng có khoảng nợ nhỏ', N'Xin kính chào Thái Phú Cường, cuộc gọi này được gửi đến bạn đang nợ 50000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 1)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (33, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Tin nhắn dành cho khách hàng có khoảng nợ nhỏ', N'Chào Tiến Đại Ca, tin nhắn này được gửi đến bạn vì bạn đang nợ 1000000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 0)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (34, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Cuộc gọi dành cho khách hàng có khoảng nợ nhỏ', N'Xin kính chào Tiến Đại Ca, cuộc gọi này được gửi đến bạn đang nợ 1000000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 1)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (35, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Tin nhắn dành cho khách hàng có khoảng nợ nhỏ', N'Chào Tiến Đại Ca, tin nhắn này được gửi đến bạn vì bạn đang nợ 1000000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 0)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (36, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Cuộc gọi dành cho khách hàng có khoảng nợ nhỏ', N'Xin kính chào Tiến Đại Ca, cuộc gọi này được gửi đến bạn đang nợ 1000000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 1)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (37, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Tin nhắn dành cho khách hàng có khoảng nợ nhỏ', N'Chào Nguyễn Hồng Đức, tin nhắn này được gửi đến bạn vì bạn đang nợ 1000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 0)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (38, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Cuộc gọi dành cho khách hàng có khoảng nợ nhỏ', N'Xin kính chào Nguyễn Hồng Đức, cuộc gọi này được gửi đến bạn đang nợ 1000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 1)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (39, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Tin nhắn dành cho khách hàng có khoảng nợ nhỏ', N'Chào Nguyễn Hồng Đức, tin nhắn này được gửi đến bạn vì bạn đang nợ 1000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 0)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (40, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Cuộc gọi dành cho khách hàng có khoảng nợ nhỏ', N'Xin kính chào Nguyễn Hồng Đức, cuộc gọi này được gửi đến bạn đang nợ 1000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 1)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (41, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Tin nhắn dành cho khách hàng có khoảng nợ nhỏ', N'Chào Thái Phú Cường, tin nhắn này được gửi đến bạn vì bạn đang nợ 50000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 0)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (42, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Cuộc gọi dành cho khách hàng có khoảng nợ nhỏ', N'Xin kính chào Thái Phú Cường, cuộc gọi này được gửi đến bạn đang nợ 50000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 1)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (43, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Tin nhắn dành cho khách hàng có khoảng nợ nhỏ', N'Chào Thái Phú Cường, tin nhắn này được gửi đến bạn vì bạn đang nợ 50000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 0)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (44, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Cuộc gọi dành cho khách hàng có khoảng nợ nhỏ', N'Xin kính chào Thái Phú Cường, cuộc gọi này được gửi đến bạn đang nợ 50000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 1)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (45, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Tin nhắn dành cho khách hàng có khoảng nợ nhỏ', N'Chào Tiến Đại Ca, tin nhắn này được gửi đến bạn vì bạn đang nợ 1000000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 0)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (46, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Cuộc gọi dành cho khách hàng có khoảng nợ nhỏ', N'Xin kính chào Tiến Đại Ca, cuộc gọi này được gửi đến bạn đang nợ 1000000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 1)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (47, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Tin nhắn dành cho khách hàng có khoảng nợ nhỏ', N'Chào Tiến Đại Ca, tin nhắn này được gửi đến bạn vì bạn đang nợ 1000000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 0)
INSERT [dbo].[ProgressMessageForms] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Content], [Type]) VALUES (48, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Cuộc gọi dành cho khách hàng có khoảng nợ nhỏ', N'Xin kính chào Tiến Đại Ca, cuộc gọi này được gửi đến bạn đang nợ 1000000000, bạn vui lòng trả chúng tôi đúng thời hạn. Mong nhận được sự hợp tác của bạn.', 1)
SET IDENTITY_INSERT [dbo].[ProgressMessageForms] OFF
SET IDENTITY_INSERT [dbo].[ProgressStageActions] ON 

INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (1, CAST(N'2019-04-05T13:09:19.8989049' AS DateTime2), CAST(N'2019-03-07T11:21:13.4438129' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190307, NULL, 1, 1)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (2, CAST(N'2019-04-05T13:09:19.9639973' AS DateTime2), CAST(N'2019-03-07T11:21:13.4445188' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190325, NULL, 1, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (3, CAST(N'2019-04-05T13:09:19.9936531' AS DateTime2), CAST(N'2019-03-07T11:21:13.4445169' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190324, NULL, 1, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (4, CAST(N'2019-04-05T13:09:20.0222167' AS DateTime2), CAST(N'2019-03-07T11:21:13.4445148' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190323, NULL, 1, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (5, CAST(N'2019-04-05T13:09:20.0507787' AS DateTime2), CAST(N'2019-03-07T11:21:13.4445130' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190322, NULL, 1, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (6, CAST(N'2019-04-05T13:09:20.0793730' AS DateTime2), CAST(N'2019-03-07T11:21:13.4445109' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190321, NULL, 1, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (7, CAST(N'2019-04-05T13:09:20.1079482' AS DateTime2), CAST(N'2019-03-07T11:21:13.4445091' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190320, NULL, 1, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (8, CAST(N'2019-04-05T13:09:20.1365126' AS DateTime2), CAST(N'2019-03-07T11:21:13.4445071' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190319, NULL, 1, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (9, CAST(N'2019-04-05T13:09:20.1650843' AS DateTime2), CAST(N'2019-03-07T11:21:13.4445051' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190318, NULL, 1, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (10, CAST(N'2019-04-05T13:09:20.1982229' AS DateTime2), CAST(N'2019-03-07T11:21:13.4445033' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190317, NULL, 1, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (11, CAST(N'2019-04-05T13:09:20.2279321' AS DateTime2), CAST(N'2019-03-07T11:21:13.4445013' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190316, NULL, 1, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (12, CAST(N'2019-04-05T13:09:20.2565075' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444991' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190315, NULL, 1, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (13, CAST(N'2019-04-05T13:09:20.2850758' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444972' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190314, NULL, 1, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (14, CAST(N'2019-04-05T13:09:20.3136537' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444952' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190313, NULL, 1, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (15, CAST(N'2019-04-05T13:09:20.3422127' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444932' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190312, NULL, 1, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (16, CAST(N'2019-04-05T13:09:20.3707932' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444868' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190311, NULL, 1, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (17, CAST(N'2019-04-05T13:09:20.3993853' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444848' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190310, NULL, 1, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (18, CAST(N'2019-04-05T13:09:20.4279590' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444829' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190309, NULL, 1, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (19, CAST(N'2019-04-05T13:09:20.4565108' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444809' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190308, NULL, 1, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (20, CAST(N'2019-04-05T13:09:20.4850754' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444787' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190307, NULL, 1, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (21, CAST(N'2019-05-04T12:51:39.0757911' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444735' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190405, NULL, 1, 2)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (22, CAST(N'2019-04-05T13:09:20.5195094' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444716' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190404, NULL, 1, 2)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (23, CAST(N'2019-04-05T13:09:20.5525255' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444697' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190403, NULL, 1, 2)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (24, CAST(N'2019-04-05T13:09:20.5822457' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444678' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190402, NULL, 1, 2)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (25, CAST(N'2019-04-05T13:09:20.6108087' AS DateTime2), CAST(N'2019-03-07T11:21:13.4445208' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190326, NULL, 1, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (26, CAST(N'2019-04-05T13:09:20.6393712' AS DateTime2), CAST(N'2019-03-07T11:21:13.4445228' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190327, NULL, 1, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (27, CAST(N'2019-04-05T13:09:20.6679542' AS DateTime2), CAST(N'2019-03-07T11:21:13.4445248' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190328, NULL, 1, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (28, CAST(N'2019-04-05T13:09:20.6965178' AS DateTime2), CAST(N'2019-03-07T11:21:13.4445268' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190329, NULL, 1, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (29, CAST(N'2019-05-04T12:51:41.1312109' AS DateTime2), CAST(N'2019-03-07T11:21:13.4466683' AS DateTime2), 0, N'Visit', 730, 2, 3, 20190406, NULL, 2, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (30, CAST(N'2019-05-04T12:51:41.4193612' AS DateTime2), CAST(N'2019-03-07T11:21:13.4466641' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190501, NULL, 2, 4)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (31, CAST(N'2019-05-04T12:51:41.6847097' AS DateTime2), CAST(N'2019-03-07T11:21:13.4466614' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190426, NULL, 2, 4)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (32, CAST(N'2019-05-04T12:51:41.9330928' AS DateTime2), CAST(N'2019-03-07T11:21:13.4466595' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190421, NULL, 2, 4)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (33, CAST(N'2019-05-04T12:51:42.0975091' AS DateTime2), CAST(N'2019-03-07T11:21:13.4466576' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190416, NULL, 2, 4)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (34, CAST(N'2019-05-04T12:51:42.2880527' AS DateTime2), CAST(N'2019-03-07T11:21:13.4466556' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190411, NULL, 2, 4)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (35, CAST(N'2019-05-04T12:51:42.5408999' AS DateTime2), CAST(N'2019-03-07T11:21:13.4466525' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190406, NULL, 2, 4)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (36, CAST(N'2019-05-04T12:51:42.8081882' AS DateTime2), CAST(N'2019-03-07T11:21:13.4462663' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190503, NULL, 2, 3)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (37, CAST(N'2019-05-04T12:51:43.2932967' AS DateTime2), CAST(N'2019-03-07T11:21:13.4462641' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190430, NULL, 2, 3)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (38, CAST(N'2019-05-04T12:51:43.9227507' AS DateTime2), CAST(N'2019-03-07T11:21:13.4462621' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190427, NULL, 2, 3)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (39, CAST(N'2019-05-04T12:51:45.4005176' AS DateTime2), CAST(N'2019-03-07T11:21:13.4462602' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190424, NULL, 2, 3)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (40, CAST(N'2019-04-05T13:09:20.7251093' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444659' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190401, NULL, 1, 2)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (41, CAST(N'2019-05-04T12:51:46.5687043' AS DateTime2), CAST(N'2019-03-07T11:21:13.4462582' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190421, NULL, 2, 3)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (42, CAST(N'2019-05-04T12:51:47.1540942' AS DateTime2), CAST(N'2019-03-07T11:21:13.4462535' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190415, NULL, 2, 3)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (43, CAST(N'2019-05-04T12:51:47.6305541' AS DateTime2), CAST(N'2019-03-07T11:21:13.4462438' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190412, NULL, 2, 3)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (44, CAST(N'2019-05-04T12:51:47.9543290' AS DateTime2), CAST(N'2019-03-07T11:21:13.4462418' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190409, NULL, 2, 3)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (45, CAST(N'2019-05-04T12:51:48.2394943' AS DateTime2), CAST(N'2019-03-07T11:21:13.4462385' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190406, NULL, 2, 3)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (46, CAST(N'2019-05-04T12:51:48.4091174' AS DateTime2), CAST(N'2019-03-07T11:21:13.4445406' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190405, NULL, 1, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (47, CAST(N'2019-04-05T13:09:20.7536693' AS DateTime2), CAST(N'2019-03-07T11:21:13.4445385' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190404, NULL, 1, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (48, CAST(N'2019-04-05T13:09:20.7822438' AS DateTime2), CAST(N'2019-03-07T11:21:13.4445365' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190403, NULL, 1, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (49, CAST(N'2019-04-05T13:09:20.8108147' AS DateTime2), CAST(N'2019-03-07T11:21:13.4445345' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190402, NULL, 1, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (50, CAST(N'2019-04-05T13:09:20.8393678' AS DateTime2), CAST(N'2019-03-07T11:21:13.4445326' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190401, NULL, 1, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (51, CAST(N'2019-04-05T13:09:20.8679478' AS DateTime2), CAST(N'2019-03-07T11:21:13.4445306' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190331, NULL, 1, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (52, CAST(N'2019-04-05T13:09:20.8965213' AS DateTime2), CAST(N'2019-03-07T11:21:13.4445287' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190330, NULL, 1, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (53, CAST(N'2019-05-04T12:51:51.7939935' AS DateTime2), CAST(N'2019-03-07T11:21:13.4462556' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190418, NULL, 2, 3)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (54, CAST(N'2019-05-04T12:51:52.0980724' AS DateTime2), CAST(N'2019-03-07T11:21:13.4466705' AS DateTime2), 0, N'Visit', 730, 2, 3, 20190416, NULL, 2, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (55, CAST(N'2019-04-05T13:09:20.9250833' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444639' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190331, NULL, 1, 2)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (56, CAST(N'2019-04-05T13:09:20.9536759' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444600' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190329, NULL, 1, 2)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (57, CAST(N'2019-04-05T13:09:20.9822407' AS DateTime2), CAST(N'2019-03-07T11:21:13.4438696' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190330, NULL, 1, 1)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (58, CAST(N'2019-04-05T13:09:21.0108008' AS DateTime2), CAST(N'2019-03-07T11:21:13.4438676' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190329, NULL, 1, 1)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (59, CAST(N'2019-04-05T13:09:21.0393901' AS DateTime2), CAST(N'2019-03-07T11:21:13.4438657' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190328, NULL, 1, 1)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (60, CAST(N'2019-04-05T13:09:21.0679705' AS DateTime2), CAST(N'2019-03-07T11:21:13.4438637' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190327, NULL, 1, 1)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (61, CAST(N'2019-04-05T13:09:21.0965175' AS DateTime2), CAST(N'2019-03-07T11:21:13.4438618' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190326, NULL, 1, 1)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (62, CAST(N'2019-04-05T13:09:21.1309783' AS DateTime2), CAST(N'2019-03-07T11:21:13.4438599' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190325, NULL, 1, 1)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (63, CAST(N'2019-04-05T13:09:21.1594174' AS DateTime2), CAST(N'2019-03-07T11:21:13.4438579' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190324, NULL, 1, 1)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (64, CAST(N'2019-04-05T13:09:21.1879774' AS DateTime2), CAST(N'2019-03-07T11:21:13.4438484' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190323, NULL, 1, 1)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (65, CAST(N'2019-04-05T13:09:21.2165397' AS DateTime2), CAST(N'2019-03-07T11:21:13.4438465' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190322, NULL, 1, 1)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (66, CAST(N'2019-04-05T13:09:21.2496948' AS DateTime2), CAST(N'2019-03-07T11:21:13.4438446' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190321, NULL, 1, 1)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (67, CAST(N'2019-04-05T13:09:21.2793964' AS DateTime2), CAST(N'2019-03-07T11:21:13.4438427' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190320, NULL, 1, 1)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (68, CAST(N'2019-04-05T13:09:21.3079732' AS DateTime2), CAST(N'2019-03-07T11:21:13.4438407' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190319, NULL, 1, 1)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (69, CAST(N'2019-04-05T13:09:21.3365626' AS DateTime2), CAST(N'2019-03-07T11:21:13.4438388' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190318, NULL, 1, 1)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (70, CAST(N'2019-04-05T13:09:21.3651107' AS DateTime2), CAST(N'2019-03-07T11:21:13.4438368' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190317, NULL, 1, 1)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (71, CAST(N'2019-04-05T13:09:21.3936867' AS DateTime2), CAST(N'2019-03-07T11:21:13.4438348' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190316, NULL, 1, 1)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (72, CAST(N'2019-04-05T13:09:21.4222451' AS DateTime2), CAST(N'2019-03-07T11:21:13.4438325' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190315, NULL, 1, 1)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (73, CAST(N'2019-04-05T13:09:21.4508594' AS DateTime2), CAST(N'2019-03-07T11:21:13.4438305' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190314, NULL, 1, 1)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (74, CAST(N'2019-04-05T13:09:21.4794007' AS DateTime2), CAST(N'2019-03-07T11:21:13.4438287' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190313, NULL, 1, 1)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (75, CAST(N'2019-04-05T13:09:21.5526658' AS DateTime2), CAST(N'2019-03-07T11:21:13.4438267' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190312, NULL, 1, 1)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (76, CAST(N'2019-04-05T13:09:21.5879999' AS DateTime2), CAST(N'2019-03-07T11:21:13.4438241' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190311, NULL, 1, 1)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (77, CAST(N'2019-04-05T13:09:21.6165333' AS DateTime2), CAST(N'2019-03-07T11:21:13.4438220' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190310, NULL, 1, 1)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (78, CAST(N'2019-04-05T13:09:21.6497053' AS DateTime2), CAST(N'2019-03-07T11:21:13.4438198' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190309, NULL, 1, 1)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (79, CAST(N'2019-04-05T13:09:21.6794036' AS DateTime2), CAST(N'2019-03-07T11:21:13.4438175' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190308, NULL, 1, 1)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (80, CAST(N'2019-04-05T13:09:21.7079694' AS DateTime2), CAST(N'2019-03-07T11:21:13.4438715' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190331, NULL, 1, 1)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (81, CAST(N'2019-04-05T13:09:21.7365543' AS DateTime2), CAST(N'2019-03-07T11:21:13.4438736' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190401, NULL, 1, 1)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (82, CAST(N'2019-04-05T13:09:21.7651177' AS DateTime2), CAST(N'2019-03-07T11:21:13.4438756' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190402, NULL, 1, 1)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (83, CAST(N'2019-04-05T13:09:21.7936919' AS DateTime2), CAST(N'2019-03-07T11:21:13.4438776' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190403, NULL, 1, 1)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (84, CAST(N'2019-04-05T13:09:21.8222534' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444581' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190328, NULL, 1, 2)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (85, CAST(N'2019-04-05T13:09:21.8508350' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444563' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190327, NULL, 1, 2)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (86, CAST(N'2019-04-05T13:09:21.8793979' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444543' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190326, NULL, 1, 2)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (87, CAST(N'2019-04-05T13:09:21.9079751' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444524' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190325, NULL, 1, 2)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (88, CAST(N'2019-04-05T13:09:21.9365350' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444505' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190324, NULL, 1, 2)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (89, CAST(N'2019-04-05T13:09:21.9651131' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444484' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190323, NULL, 1, 2)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (90, CAST(N'2019-04-05T13:09:21.9937130' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444464' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190322, NULL, 1, 2)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (91, CAST(N'2019-04-05T13:09:22.0222545' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444445' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190321, NULL, 1, 2)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (92, CAST(N'2019-04-05T13:09:22.0508244' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444426' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190320, NULL, 1, 2)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (93, CAST(N'2019-04-05T13:09:22.0794319' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444407' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190319, NULL, 1, 2)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (94, CAST(N'2019-04-05T13:09:22.1079987' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444388' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190318, NULL, 1, 2)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (95, CAST(N'2019-04-05T13:09:22.1412956' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444620' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190330, NULL, 1, 2)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (96, CAST(N'2019-04-05T13:09:22.1708411' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444368' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190317, NULL, 1, 2)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (97, CAST(N'2019-04-05T13:09:22.1994059' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444323' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190315, NULL, 1, 2)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (98, CAST(N'2019-04-05T13:09:22.2280011' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444231' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190314, NULL, 1, 2)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (99, CAST(N'2019-04-05T13:09:22.2565596' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444211' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190313, NULL, 1, 2)
GO
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (100, CAST(N'2019-04-05T13:09:22.2851279' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444191' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190312, NULL, 1, 2)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (101, CAST(N'2019-04-05T13:09:22.3136982' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444167' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190311, NULL, 1, 2)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (102, CAST(N'2019-04-05T13:09:22.3480107' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444146' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190310, NULL, 1, 2)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (103, CAST(N'2019-04-05T13:09:22.3765618' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444125' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190309, NULL, 1, 2)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (104, CAST(N'2019-04-05T13:09:22.4051396' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444104' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190308, NULL, 1, 2)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (105, CAST(N'2019-04-05T13:09:22.4337066' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444071' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190307, NULL, 1, 2)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (106, CAST(N'2019-05-04T12:52:08.0229897' AS DateTime2), CAST(N'2019-03-07T11:21:13.4438815' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190405, NULL, 1, 1)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (107, CAST(N'2019-04-05T13:09:22.4622689' AS DateTime2), CAST(N'2019-03-07T11:21:13.4438794' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190404, NULL, 1, 1)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (108, CAST(N'2019-04-05T13:09:22.4908769' AS DateTime2), CAST(N'2019-03-07T11:21:13.4444348' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190316, NULL, 1, 2)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (109, CAST(N'2019-05-04T12:52:09.3178531' AS DateTime2), CAST(N'2019-03-07T11:21:13.4466725' AS DateTime2), 0, N'Visit', 730, 2, 3, 20190426, NULL, 2, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (110, CAST(N'2019-04-05T13:09:22.5221166' AS DateTime2), CAST(N'2019-03-07T11:21:13.4478824' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190307, NULL, 3, 5)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (111, CAST(N'2019-04-05T13:09:22.5537263' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484516' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190325, NULL, 3, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (112, CAST(N'2019-04-05T13:09:22.5823067' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484496' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190324, NULL, 3, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (113, CAST(N'2019-04-05T13:09:22.6108372' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484475' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190323, NULL, 3, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (114, CAST(N'2019-04-05T13:09:22.6442043' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484456' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190322, NULL, 3, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (115, CAST(N'2019-04-05T13:09:22.6737124' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484437' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190321, NULL, 3, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (116, CAST(N'2019-04-05T13:09:22.7022950' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484418' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190320, NULL, 3, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (117, CAST(N'2019-04-05T13:09:22.7308554' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484399' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190319, NULL, 3, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (118, CAST(N'2019-04-05T13:09:22.7594194' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484380' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190318, NULL, 3, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (119, CAST(N'2019-04-05T13:09:22.7880081' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484360' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190317, NULL, 3, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (120, CAST(N'2019-04-05T13:09:22.8165768' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484341' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190316, NULL, 3, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (121, CAST(N'2019-04-05T13:09:22.8451449' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484320' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190315, NULL, 3, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (122, CAST(N'2019-04-05T13:09:22.8737333' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484301' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190314, NULL, 3, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (123, CAST(N'2019-04-05T13:09:22.9022773' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484281' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190313, NULL, 3, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (124, CAST(N'2019-04-05T13:09:22.9308403' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484262' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190312, NULL, 3, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (125, CAST(N'2019-04-05T13:09:22.9594288' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484239' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190311, NULL, 3, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (126, CAST(N'2019-04-05T13:09:22.9881815' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484220' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190310, NULL, 3, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (127, CAST(N'2019-04-05T13:09:23.0165848' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484200' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190309, NULL, 3, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (128, CAST(N'2019-04-05T13:09:23.0451703' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484179' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190308, NULL, 3, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (129, CAST(N'2019-04-05T13:09:23.0737360' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484108' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190307, NULL, 3, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (130, CAST(N'2019-04-06T14:09:51.3512926' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484051' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190405, NULL, 3, 6)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (131, CAST(N'2019-04-05T13:09:23.1022930' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484032' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190404, NULL, 3, 6)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (132, CAST(N'2019-04-05T13:09:23.1356376' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484012' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190403, NULL, 3, 6)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (133, CAST(N'2019-04-05T13:09:23.1651625' AS DateTime2), CAST(N'2019-03-07T11:21:13.4483992' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190402, NULL, 3, 6)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (134, CAST(N'2019-04-05T13:09:23.1937520' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484535' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190326, NULL, 3, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (135, CAST(N'2019-04-05T13:09:23.2223008' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484554' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190327, NULL, 3, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (136, CAST(N'2019-04-05T13:09:23.2508842' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484573' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190328, NULL, 3, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (137, CAST(N'2019-04-05T13:09:23.2794505' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484592' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190329, NULL, 3, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (138, NULL, CAST(N'2019-03-07T11:21:13.4492731' AS DateTime2), 0, N'Visit', 730, 2, 1, 20190406, NULL, 4, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (139, NULL, CAST(N'2019-03-07T11:21:13.4492688' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190501, NULL, 4, 8)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (140, NULL, CAST(N'2019-03-07T11:21:13.4492662' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190426, NULL, 4, 8)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (141, NULL, CAST(N'2019-03-07T11:21:13.4492642' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190421, NULL, 4, 8)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (142, NULL, CAST(N'2019-03-07T11:21:13.4492621' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190416, NULL, 4, 8)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (143, NULL, CAST(N'2019-03-07T11:21:13.4492601' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190411, NULL, 4, 8)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (144, NULL, CAST(N'2019-03-07T11:21:13.4492569' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190406, NULL, 4, 8)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (145, NULL, CAST(N'2019-03-07T11:21:13.4488919' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190503, NULL, 4, 7)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (146, NULL, CAST(N'2019-03-07T11:21:13.4488898' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190430, NULL, 4, 7)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (147, NULL, CAST(N'2019-03-07T11:21:13.4488878' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190427, NULL, 4, 7)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (148, NULL, CAST(N'2019-03-07T11:21:13.4488859' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190424, NULL, 4, 7)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (149, CAST(N'2019-04-05T13:09:23.3080316' AS DateTime2), CAST(N'2019-03-07T11:21:13.4483973' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190401, NULL, 3, 6)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (150, NULL, CAST(N'2019-03-07T11:21:13.4488838' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190421, NULL, 4, 7)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (151, NULL, CAST(N'2019-03-07T11:21:13.4488794' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190415, NULL, 4, 7)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (152, NULL, CAST(N'2019-03-07T11:21:13.4488773' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190412, NULL, 4, 7)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (153, NULL, CAST(N'2019-03-07T11:21:13.4488752' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190409, NULL, 4, 7)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (154, NULL, CAST(N'2019-03-07T11:21:13.4488721' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190406, NULL, 4, 7)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (155, CAST(N'2019-04-06T14:09:51.4652941' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484728' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190405, NULL, 3, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (156, CAST(N'2019-04-05T13:09:23.3366029' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484709' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190404, NULL, 3, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (157, CAST(N'2019-04-05T13:09:23.3651741' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484690' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190403, NULL, 3, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (158, CAST(N'2019-04-05T13:09:23.3937330' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484670' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190402, NULL, 3, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (159, CAST(N'2019-04-05T13:09:23.4222838' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484651' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190401, NULL, 3, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (160, CAST(N'2019-04-05T13:09:23.4508525' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484631' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190331, NULL, 3, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (161, CAST(N'2019-04-05T13:09:23.4794450' AS DateTime2), CAST(N'2019-03-07T11:21:13.4484612' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190330, NULL, 3, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (162, NULL, CAST(N'2019-03-07T11:21:13.4488813' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190418, NULL, 4, 7)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (163, NULL, CAST(N'2019-03-07T11:21:13.4492752' AS DateTime2), 0, N'Visit', 730, 2, 1, 20190416, NULL, 4, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (164, CAST(N'2019-04-05T13:09:23.5080266' AS DateTime2), CAST(N'2019-03-07T11:21:13.4483953' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190331, NULL, 3, 6)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (165, CAST(N'2019-04-05T13:09:23.5365843' AS DateTime2), CAST(N'2019-03-07T11:21:13.4483915' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190329, NULL, 3, 6)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (166, CAST(N'2019-04-05T13:09:23.5651738' AS DateTime2), CAST(N'2019-03-07T11:21:13.4479374' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190330, NULL, 3, 5)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (167, CAST(N'2019-04-05T13:09:23.5937155' AS DateTime2), CAST(N'2019-03-07T11:21:13.4479354' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190329, NULL, 3, 5)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (168, CAST(N'2019-04-05T13:09:23.6271006' AS DateTime2), CAST(N'2019-03-07T11:21:13.4479335' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190328, NULL, 3, 5)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (169, CAST(N'2019-04-05T13:09:23.6566078' AS DateTime2), CAST(N'2019-03-07T11:21:13.4479316' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190327, NULL, 3, 5)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (170, CAST(N'2019-04-05T13:09:23.6851708' AS DateTime2), CAST(N'2019-03-07T11:21:13.4479296' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190326, NULL, 3, 5)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (171, CAST(N'2019-04-05T13:09:23.7137331' AS DateTime2), CAST(N'2019-03-07T11:21:13.4479276' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190325, NULL, 3, 5)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (172, CAST(N'2019-04-05T13:09:23.7423099' AS DateTime2), CAST(N'2019-03-07T11:21:13.4479256' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190324, NULL, 3, 5)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (173, CAST(N'2019-04-05T13:09:23.7708719' AS DateTime2), CAST(N'2019-03-07T11:21:13.4479234' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190323, NULL, 3, 5)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (174, CAST(N'2019-04-05T13:09:23.7994420' AS DateTime2), CAST(N'2019-03-07T11:21:13.4479214' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190322, NULL, 3, 5)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (175, CAST(N'2019-04-05T13:09:23.8280479' AS DateTime2), CAST(N'2019-03-07T11:21:13.4479194' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190321, NULL, 3, 5)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (176, CAST(N'2019-04-05T13:09:23.8566116' AS DateTime2), CAST(N'2019-03-07T11:21:13.4479172' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190320, NULL, 3, 5)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (177, CAST(N'2019-04-05T13:09:23.8851731' AS DateTime2), CAST(N'2019-03-07T11:21:13.4479084' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190319, NULL, 3, 5)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (178, CAST(N'2019-04-05T13:09:23.9137287' AS DateTime2), CAST(N'2019-03-07T11:21:13.4479064' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190318, NULL, 3, 5)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (179, CAST(N'2019-04-05T13:09:23.9422935' AS DateTime2), CAST(N'2019-03-07T11:21:13.4479045' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190317, NULL, 3, 5)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (180, CAST(N'2019-04-05T13:09:23.9708896' AS DateTime2), CAST(N'2019-03-07T11:21:13.4479026' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190316, NULL, 3, 5)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (181, CAST(N'2019-04-05T13:09:23.9994590' AS DateTime2), CAST(N'2019-03-07T11:21:13.4479004' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190315, NULL, 3, 5)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (182, CAST(N'2019-04-05T13:09:24.0280527' AS DateTime2), CAST(N'2019-03-07T11:21:13.4478984' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190314, NULL, 3, 5)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (183, CAST(N'2019-04-05T13:09:24.0623403' AS DateTime2), CAST(N'2019-03-07T11:21:13.4478964' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190313, NULL, 3, 5)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (184, CAST(N'2019-04-05T13:09:24.0908871' AS DateTime2), CAST(N'2019-03-07T11:21:13.4478944' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190312, NULL, 3, 5)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (185, CAST(N'2019-04-05T13:09:24.1300881' AS DateTime2), CAST(N'2019-03-07T11:21:13.4478918' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190311, NULL, 3, 5)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (186, CAST(N'2019-04-05T13:09:24.1594697' AS DateTime2), CAST(N'2019-03-07T11:21:13.4478898' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190310, NULL, 3, 5)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (187, CAST(N'2019-04-05T13:09:24.1880457' AS DateTime2), CAST(N'2019-03-07T11:21:13.4478876' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190309, NULL, 3, 5)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (188, CAST(N'2019-04-05T13:09:24.2166085' AS DateTime2), CAST(N'2019-03-07T11:21:13.4478855' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190308, NULL, 3, 5)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (189, CAST(N'2019-04-05T13:09:24.2451895' AS DateTime2), CAST(N'2019-03-07T11:21:13.4479393' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190331, NULL, 3, 5)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (190, CAST(N'2019-04-05T13:09:24.2737511' AS DateTime2), CAST(N'2019-03-07T11:21:13.4479412' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190401, NULL, 3, 5)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (191, CAST(N'2019-04-05T13:09:24.3023411' AS DateTime2), CAST(N'2019-03-07T11:21:13.4479432' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190402, NULL, 3, 5)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (192, CAST(N'2019-04-05T13:09:24.3309118' AS DateTime2), CAST(N'2019-03-07T11:21:13.4479451' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190403, NULL, 3, 5)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (193, CAST(N'2019-04-05T13:09:24.3594792' AS DateTime2), CAST(N'2019-03-07T11:21:13.4483895' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190328, NULL, 3, 6)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (194, CAST(N'2019-04-05T13:09:24.3880314' AS DateTime2), CAST(N'2019-03-07T11:21:13.4483876' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190327, NULL, 3, 6)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (195, CAST(N'2019-04-05T13:09:24.4166017' AS DateTime2), CAST(N'2019-03-07T11:21:13.4483857' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190326, NULL, 3, 6)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (196, CAST(N'2019-04-05T13:09:24.4451828' AS DateTime2), CAST(N'2019-03-07T11:21:13.4483837' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190325, NULL, 3, 6)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (197, CAST(N'2019-04-05T13:09:24.4737558' AS DateTime2), CAST(N'2019-03-07T11:21:13.4483818' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190324, NULL, 3, 6)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (198, CAST(N'2019-04-05T13:09:24.5023317' AS DateTime2), CAST(N'2019-03-07T11:21:13.4483794' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190323, NULL, 3, 6)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (199, CAST(N'2019-04-05T13:09:24.5309012' AS DateTime2), CAST(N'2019-03-07T11:21:13.4483775' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190322, NULL, 3, 6)
GO
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (200, CAST(N'2019-04-05T13:09:24.5594675' AS DateTime2), CAST(N'2019-03-07T11:21:13.4483755' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190321, NULL, 3, 6)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (201, CAST(N'2019-04-05T13:09:24.5880309' AS DateTime2), CAST(N'2019-03-07T11:21:13.4483734' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190320, NULL, 3, 6)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (202, CAST(N'2019-04-05T13:09:24.6213859' AS DateTime2), CAST(N'2019-03-07T11:21:13.4483708' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190319, NULL, 3, 6)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (203, CAST(N'2019-04-05T13:09:24.6509167' AS DateTime2), CAST(N'2019-03-07T11:21:13.4483484' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190318, NULL, 3, 6)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (204, CAST(N'2019-04-05T13:09:24.6794779' AS DateTime2), CAST(N'2019-03-07T11:21:13.4483934' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190330, NULL, 3, 6)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (205, CAST(N'2019-04-05T13:09:24.7080497' AS DateTime2), CAST(N'2019-03-07T11:21:13.4483465' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190317, NULL, 3, 6)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (206, CAST(N'2019-04-05T13:09:24.7366182' AS DateTime2), CAST(N'2019-03-07T11:21:13.4483424' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190315, NULL, 3, 6)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (207, CAST(N'2019-04-05T13:09:24.7652001' AS DateTime2), CAST(N'2019-03-07T11:21:13.4483404' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190314, NULL, 3, 6)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (208, CAST(N'2019-04-05T13:09:24.7937772' AS DateTime2), CAST(N'2019-03-07T11:21:13.4483385' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190313, NULL, 3, 6)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (209, CAST(N'2019-04-05T13:09:24.8223438' AS DateTime2), CAST(N'2019-03-07T11:21:13.4483363' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190312, NULL, 3, 6)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (210, CAST(N'2019-04-05T13:09:24.8509157' AS DateTime2), CAST(N'2019-03-07T11:21:13.4483217' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190311, NULL, 3, 6)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (211, CAST(N'2019-04-05T13:09:24.8843641' AS DateTime2), CAST(N'2019-03-07T11:21:13.4483198' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190310, NULL, 3, 6)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (212, CAST(N'2019-04-05T13:09:24.9194985' AS DateTime2), CAST(N'2019-03-07T11:21:13.4483177' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190309, NULL, 3, 6)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (213, CAST(N'2019-04-05T13:09:24.9480752' AS DateTime2), CAST(N'2019-03-07T11:21:13.4483156' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190308, NULL, 3, 6)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (214, CAST(N'2019-04-05T13:09:24.9766790' AS DateTime2), CAST(N'2019-03-07T11:21:13.4483126' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190307, NULL, 3, 6)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (215, CAST(N'2019-04-06T14:09:51.5384268' AS DateTime2), CAST(N'2019-03-07T11:21:13.4479490' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190405, NULL, 3, 5)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (216, CAST(N'2019-04-05T13:09:25.0052285' AS DateTime2), CAST(N'2019-03-07T11:21:13.4479470' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190404, NULL, 3, 5)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (217, CAST(N'2019-04-05T13:09:25.0337811' AS DateTime2), CAST(N'2019-03-07T11:21:13.4483445' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190316, NULL, 3, 6)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (218, NULL, CAST(N'2019-03-07T11:21:13.4492771' AS DateTime2), 0, N'Visit', 730, 2, 1, 20190426, NULL, 4, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (219, CAST(N'2019-04-05T13:09:25.0647752' AS DateTime2), CAST(N'2019-03-07T11:21:13.4497537' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190307, NULL, 5, 9)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (220, CAST(N'2019-04-05T13:09:25.1081096' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502903' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190325, NULL, 5, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (221, CAST(N'2019-04-05T13:09:25.1414087' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502884' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190324, NULL, 5, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (222, CAST(N'2019-04-05T13:09:25.1709399' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502861' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190323, NULL, 5, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (223, CAST(N'2019-04-05T13:09:25.2099757' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502842' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190322, NULL, 5, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (224, CAST(N'2019-04-05T13:09:25.2395109' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502823' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190321, NULL, 5, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (225, CAST(N'2019-04-05T13:09:25.2680753' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502804' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190320, NULL, 5, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (226, CAST(N'2019-04-05T13:09:25.2966507' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502784' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190319, NULL, 5, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (227, CAST(N'2019-04-05T13:09:25.3252331' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502766' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190318, NULL, 5, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (228, CAST(N'2019-04-05T13:09:25.3595229' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502745' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190317, NULL, 5, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (229, CAST(N'2019-04-05T13:09:25.3880784' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502725' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190316, NULL, 5, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (230, CAST(N'2019-04-05T13:09:25.4166470' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502659' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190315, NULL, 5, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (231, CAST(N'2019-04-05T13:09:25.4452263' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502640' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190314, NULL, 5, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (232, CAST(N'2019-04-05T13:09:25.4737890' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502621' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190313, NULL, 5, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (233, CAST(N'2019-04-05T13:09:25.5023793' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502601' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190312, NULL, 5, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (234, CAST(N'2019-04-05T13:09:25.5309437' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502580' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190311, NULL, 5, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (235, CAST(N'2019-04-05T13:09:25.5595202' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502561' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190310, NULL, 5, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (236, CAST(N'2019-04-05T13:09:25.5939311' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502541' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190309, NULL, 5, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (237, CAST(N'2019-04-05T13:09:25.6282506' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502522' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190308, NULL, 5, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (238, CAST(N'2019-04-05T13:09:25.6566853' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502500' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190307, NULL, 5, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (239, CAST(N'2019-04-06T14:09:51.5830589' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502450' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190405, NULL, 5, 10)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (240, CAST(N'2019-04-05T13:09:25.6852390' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502431' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190404, NULL, 5, 10)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (241, CAST(N'2019-04-05T13:09:25.7138034' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502411' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190403, NULL, 5, 10)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (242, CAST(N'2019-04-05T13:09:25.7423728' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502392' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190402, NULL, 5, 10)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (243, CAST(N'2019-04-05T13:09:25.7709391' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502922' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190326, NULL, 5, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (244, CAST(N'2019-04-05T13:09:25.7995241' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502942' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190327, NULL, 5, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (245, CAST(N'2019-04-05T13:09:25.8280925' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502960' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190328, NULL, 5, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (246, CAST(N'2019-04-05T13:09:25.8566634' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502980' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190329, NULL, 5, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (247, NULL, CAST(N'2019-03-07T11:21:13.4511226' AS DateTime2), 0, N'Visit', 730, 2, 1, 20190406, NULL, 6, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (248, NULL, CAST(N'2019-03-07T11:21:13.4511183' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190501, NULL, 6, 12)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (249, NULL, CAST(N'2019-03-07T11:21:13.4511156' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190426, NULL, 6, 12)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (250, NULL, CAST(N'2019-03-07T11:21:13.4511137' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190421, NULL, 6, 12)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (251, NULL, CAST(N'2019-03-07T11:21:13.4511114' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190416, NULL, 6, 12)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (252, NULL, CAST(N'2019-03-07T11:21:13.4511014' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190411, NULL, 6, 12)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (253, NULL, CAST(N'2019-03-07T11:21:13.4510984' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190406, NULL, 6, 12)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (254, NULL, CAST(N'2019-03-07T11:21:13.4507408' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190503, NULL, 6, 11)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (255, NULL, CAST(N'2019-03-07T11:21:13.4507387' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190430, NULL, 6, 11)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (256, NULL, CAST(N'2019-03-07T11:21:13.4507367' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190427, NULL, 6, 11)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (257, NULL, CAST(N'2019-03-07T11:21:13.4507347' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190424, NULL, 6, 11)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (258, CAST(N'2019-04-05T13:09:25.8852882' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502372' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190401, NULL, 5, 10)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (259, NULL, CAST(N'2019-03-07T11:21:13.4507327' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190421, NULL, 6, 11)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (260, NULL, CAST(N'2019-03-07T11:21:13.4507283' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190415, NULL, 6, 11)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (261, NULL, CAST(N'2019-03-07T11:21:13.4507263' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190412, NULL, 6, 11)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (262, NULL, CAST(N'2019-03-07T11:21:13.4507243' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190409, NULL, 6, 11)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (263, NULL, CAST(N'2019-03-07T11:21:13.4507212' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190406, NULL, 6, 11)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (264, CAST(N'2019-04-06T14:09:51.6083939' AS DateTime2), CAST(N'2019-03-07T11:21:13.4503117' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190405, NULL, 5, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (265, CAST(N'2019-04-05T13:09:25.9137895' AS DateTime2), CAST(N'2019-03-07T11:21:13.4503097' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190404, NULL, 5, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (266, CAST(N'2019-04-05T13:09:25.9423874' AS DateTime2), CAST(N'2019-03-07T11:21:13.4503078' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190403, NULL, 5, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (267, CAST(N'2019-04-05T13:09:25.9709413' AS DateTime2), CAST(N'2019-03-07T11:21:13.4503058' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190402, NULL, 5, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (268, CAST(N'2019-04-05T13:09:25.9995182' AS DateTime2), CAST(N'2019-03-07T11:21:13.4503039' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190401, NULL, 5, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (269, CAST(N'2019-04-05T13:09:26.0280770' AS DateTime2), CAST(N'2019-03-07T11:21:13.4503018' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190331, NULL, 5, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (270, CAST(N'2019-04-05T13:09:26.0566490' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502999' AS DateTime2), 0, N'Báo cáo cho sếp', 730, 3, 3, 20190330, NULL, 5, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (271, NULL, CAST(N'2019-03-07T11:21:13.4507302' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190418, NULL, 6, 11)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (272, NULL, CAST(N'2019-03-07T11:21:13.4511247' AS DateTime2), 0, N'Visit', 730, 2, 1, 20190416, NULL, 6, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (273, CAST(N'2019-04-05T13:09:26.0909760' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502353' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190331, NULL, 5, 10)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (274, CAST(N'2019-04-05T13:09:26.1195365' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502314' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190329, NULL, 5, 10)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (275, CAST(N'2019-04-05T13:09:26.1481121' AS DateTime2), CAST(N'2019-03-07T11:21:13.4498120' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190330, NULL, 5, 9)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (276, CAST(N'2019-04-05T13:09:26.1766850' AS DateTime2), CAST(N'2019-03-07T11:21:13.4498100' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190329, NULL, 5, 9)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (277, CAST(N'2019-04-05T13:09:26.2052676' AS DateTime2), CAST(N'2019-03-07T11:21:13.4498076' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190328, NULL, 5, 9)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (278, CAST(N'2019-04-05T13:09:26.2338015' AS DateTime2), CAST(N'2019-03-07T11:21:13.4497950' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190327, NULL, 5, 9)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (279, CAST(N'2019-04-05T13:09:26.2623793' AS DateTime2), CAST(N'2019-03-07T11:21:13.4497930' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190326, NULL, 5, 9)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (280, CAST(N'2019-04-05T13:09:26.2909623' AS DateTime2), CAST(N'2019-03-07T11:21:13.4497912' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190325, NULL, 5, 9)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (281, CAST(N'2019-04-05T13:09:26.3195422' AS DateTime2), CAST(N'2019-03-07T11:21:13.4497892' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190324, NULL, 5, 9)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (282, CAST(N'2019-04-05T13:09:26.3480906' AS DateTime2), CAST(N'2019-03-07T11:21:13.4497871' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190323, NULL, 5, 9)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (283, CAST(N'2019-04-05T13:09:26.3766547' AS DateTime2), CAST(N'2019-03-07T11:21:13.4497852' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190322, NULL, 5, 9)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (284, CAST(N'2019-04-05T13:09:26.4052590' AS DateTime2), CAST(N'2019-03-07T11:21:13.4497833' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190321, NULL, 5, 9)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (285, CAST(N'2019-04-05T13:09:26.4338124' AS DateTime2), CAST(N'2019-03-07T11:21:13.4497814' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190320, NULL, 5, 9)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (286, CAST(N'2019-04-05T13:09:26.4623795' AS DateTime2), CAST(N'2019-03-07T11:21:13.4497794' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190319, NULL, 5, 9)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (287, CAST(N'2019-04-05T13:09:26.4909476' AS DateTime2), CAST(N'2019-03-07T11:21:13.4497775' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190318, NULL, 5, 9)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (288, CAST(N'2019-04-05T13:09:26.5195188' AS DateTime2), CAST(N'2019-03-07T11:21:13.4497756' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190317, NULL, 5, 9)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (289, CAST(N'2019-04-05T13:09:26.5480789' AS DateTime2), CAST(N'2019-03-07T11:21:13.4497736' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190316, NULL, 5, 9)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (290, CAST(N'2019-04-05T13:09:26.5825388' AS DateTime2), CAST(N'2019-03-07T11:21:13.4497715' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190315, NULL, 5, 9)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (291, CAST(N'2019-04-05T13:09:26.6109812' AS DateTime2), CAST(N'2019-03-07T11:21:13.4497696' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190314, NULL, 5, 9)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (292, CAST(N'2019-04-05T13:09:26.6395269' AS DateTime2), CAST(N'2019-03-07T11:21:13.4497674' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190313, NULL, 5, 9)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (293, CAST(N'2019-04-05T13:09:26.6681113' AS DateTime2), CAST(N'2019-03-07T11:21:13.4497654' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190312, NULL, 5, 9)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (294, CAST(N'2019-04-05T13:09:26.6966676' AS DateTime2), CAST(N'2019-03-07T11:21:13.4497629' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190311, NULL, 5, 9)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (295, CAST(N'2019-04-05T13:09:26.7252568' AS DateTime2), CAST(N'2019-03-07T11:21:13.4497608' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190310, NULL, 5, 9)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (296, CAST(N'2019-04-05T13:09:26.7538381' AS DateTime2), CAST(N'2019-03-07T11:21:13.4497588' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190309, NULL, 5, 9)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (297, CAST(N'2019-04-05T13:09:26.7823986' AS DateTime2), CAST(N'2019-03-07T11:21:13.4497567' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190308, NULL, 5, 9)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (298, CAST(N'2019-04-05T13:09:26.8109802' AS DateTime2), CAST(N'2019-03-07T11:21:13.4498139' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190331, NULL, 5, 9)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (299, CAST(N'2019-04-05T13:09:26.8395202' AS DateTime2), CAST(N'2019-03-07T11:21:13.4498159' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190401, NULL, 5, 9)
GO
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (300, CAST(N'2019-04-05T13:09:26.8681063' AS DateTime2), CAST(N'2019-03-07T11:21:13.4498178' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190402, NULL, 5, 9)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (301, CAST(N'2019-04-05T13:09:26.8966795' AS DateTime2), CAST(N'2019-03-07T11:21:13.4498198' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190403, NULL, 5, 9)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (302, CAST(N'2019-04-05T13:09:26.9252597' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502295' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190328, NULL, 5, 10)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (303, CAST(N'2019-04-05T13:09:26.9538165' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502276' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190327, NULL, 5, 10)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (304, CAST(N'2019-04-05T13:09:26.9824006' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502256' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190326, NULL, 5, 10)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (305, CAST(N'2019-04-05T13:09:27.0109615' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502238' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190325, NULL, 5, 10)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (306, CAST(N'2019-04-05T13:09:27.0395327' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502218' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190324, NULL, 5, 10)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (307, CAST(N'2019-04-05T13:09:27.0681115' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502196' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190323, NULL, 5, 10)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (308, CAST(N'2019-04-05T13:09:27.1014813' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502177' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190322, NULL, 5, 10)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (309, CAST(N'2019-04-05T13:09:27.1309934' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502157' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190321, NULL, 5, 10)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (310, CAST(N'2019-04-05T13:09:27.1595665' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502138' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190320, NULL, 5, 10)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (311, CAST(N'2019-04-05T13:09:27.1881166' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502117' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190319, NULL, 5, 10)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (312, CAST(N'2019-04-05T13:09:27.2166979' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502032' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190318, NULL, 5, 10)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (313, CAST(N'2019-04-05T13:09:27.2452799' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502334' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190330, NULL, 5, 10)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (314, CAST(N'2019-04-05T13:09:27.2738313' AS DateTime2), CAST(N'2019-03-07T11:21:13.4502013' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190317, NULL, 5, 10)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (315, CAST(N'2019-04-05T13:09:27.3024188' AS DateTime2), CAST(N'2019-03-07T11:21:13.4501971' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190315, NULL, 5, 10)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (316, CAST(N'2019-04-05T13:09:27.3357717' AS DateTime2), CAST(N'2019-03-07T11:21:13.4501953' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190314, NULL, 5, 10)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (317, CAST(N'2019-04-05T13:09:27.3652757' AS DateTime2), CAST(N'2019-03-07T11:21:13.4501933' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190313, NULL, 5, 10)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (318, CAST(N'2019-04-05T13:09:27.3938602' AS DateTime2), CAST(N'2019-03-07T11:21:13.4501913' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190312, NULL, 5, 10)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (319, CAST(N'2019-04-05T13:09:27.4224145' AS DateTime2), CAST(N'2019-03-07T11:21:13.4501888' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190311, NULL, 5, 10)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (320, CAST(N'2019-04-05T13:09:27.4509828' AS DateTime2), CAST(N'2019-03-07T11:21:13.4501869' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190310, NULL, 5, 10)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (321, CAST(N'2019-04-05T13:09:27.4795739' AS DateTime2), CAST(N'2019-03-07T11:21:13.4501848' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190309, NULL, 5, 10)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (322, CAST(N'2019-04-05T13:09:27.5081314' AS DateTime2), CAST(N'2019-03-07T11:21:13.4501828' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190308, NULL, 5, 10)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (323, CAST(N'2019-04-05T13:09:27.5367055' AS DateTime2), CAST(N'2019-03-07T11:21:13.4501797' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190307, NULL, 5, 10)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (324, CAST(N'2019-04-06T14:09:51.6870273' AS DateTime2), CAST(N'2019-03-07T11:21:13.4498237' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190405, NULL, 5, 9)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (325, CAST(N'2019-04-05T13:09:27.5652871' AS DateTime2), CAST(N'2019-03-07T11:21:13.4498218' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190404, NULL, 5, 9)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (326, CAST(N'2019-04-05T13:09:27.5986327' AS DateTime2), CAST(N'2019-03-07T11:21:13.4501993' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190316, NULL, 5, 10)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (327, NULL, CAST(N'2019-03-07T11:21:13.4511267' AS DateTime2), 0, N'Visit', 730, 2, 1, 20190426, NULL, 6, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (328, CAST(N'2019-04-05T13:09:27.6305672' AS DateTime2), CAST(N'2019-03-07T12:49:23.5007854' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190307, NULL, 7, 13)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (329, CAST(N'2019-04-05T13:09:27.6624447' AS DateTime2), CAST(N'2019-03-07T12:49:23.5007902' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190322, NULL, 7, 13)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (330, CAST(N'2019-04-05T13:09:27.6910489' AS DateTime2), CAST(N'2019-03-07T12:49:23.5012324' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190307, NULL, 7, 14)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (331, CAST(N'2019-04-05T13:09:27.7195716' AS DateTime2), CAST(N'2019-03-07T12:49:23.5012358' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190322, NULL, 7, 14)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (332, CAST(N'2019-04-05T13:09:27.7481317' AS DateTime2), CAST(N'2019-03-07T12:49:23.5012400' AS DateTime2), 0, N'Báo cáo tiến độ lần 1', 730, 3, 3, 20190307, NULL, 7, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (333, NULL, CAST(N'2019-03-07T12:49:23.5029669' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190406, NULL, 8, 15)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (334, NULL, CAST(N'2019-03-07T12:49:23.5029704' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190413, NULL, 8, 15)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (335, NULL, CAST(N'2019-03-07T12:49:23.5029725' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190420, NULL, 8, 15)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (336, NULL, CAST(N'2019-03-07T12:49:23.5029745' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190427, NULL, 8, 15)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (337, NULL, CAST(N'2019-03-07T12:49:23.5033347' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190406, NULL, 8, 16)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (338, NULL, CAST(N'2019-03-07T12:49:23.5033379' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190421, NULL, 8, 16)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (339, NULL, CAST(N'2019-03-07T12:49:23.5033426' AS DateTime2), 0, N'Báo cáo tiến độ lần 2', 730, 3, 1, 20190406, NULL, 8, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (340, NULL, CAST(N'2019-03-07T12:49:23.5033450' AS DateTime2), 0, N'Visit', 730, 2, 1, 20190406, NULL, 8, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (341, CAST(N'2019-04-05T13:09:27.7790023' AS DateTime2), CAST(N'2019-03-07T12:49:23.5044005' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190307, NULL, 9, 17)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (342, CAST(N'2019-04-05T13:09:27.8109895' AS DateTime2), CAST(N'2019-03-07T12:49:23.5044037' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190322, NULL, 9, 17)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (343, CAST(N'2019-04-05T13:09:27.8395589' AS DateTime2), CAST(N'2019-03-07T12:49:23.5048057' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190307, NULL, 9, 18)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (344, CAST(N'2019-04-05T13:09:27.8681367' AS DateTime2), CAST(N'2019-03-07T12:49:23.5048087' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190322, NULL, 9, 18)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (345, CAST(N'2019-04-05T13:09:27.8967119' AS DateTime2), CAST(N'2019-03-07T12:49:23.5048130' AS DateTime2), 0, N'Báo cáo tiến độ lần 1', 730, 3, 3, 20190307, NULL, 9, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (346, NULL, CAST(N'2019-03-07T12:49:23.5052058' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190406, NULL, 10, 19)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (347, NULL, CAST(N'2019-03-07T12:49:23.5052088' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190413, NULL, 10, 19)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (348, NULL, CAST(N'2019-03-07T12:49:23.5052111' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190420, NULL, 10, 19)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (349, NULL, CAST(N'2019-03-07T12:49:23.5052131' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190427, NULL, 10, 19)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (350, NULL, CAST(N'2019-03-07T12:49:23.5055700' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190406, NULL, 10, 20)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (351, NULL, CAST(N'2019-03-07T12:49:23.5055729' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190421, NULL, 10, 20)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (352, NULL, CAST(N'2019-03-07T12:49:23.5055776' AS DateTime2), 0, N'Báo cáo tiến độ lần 2', 730, 3, 1, 20190406, NULL, 10, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (353, NULL, CAST(N'2019-03-07T12:49:23.5055800' AS DateTime2), 0, N'Visit', 730, 2, 1, 20190406, NULL, 10, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (354, CAST(N'2019-04-05T13:09:27.9274202' AS DateTime2), CAST(N'2019-03-07T12:49:23.5060260' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190307, NULL, 11, 21)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (355, CAST(N'2019-04-05T13:09:27.9595546' AS DateTime2), CAST(N'2019-03-07T12:49:23.5060290' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190322, NULL, 11, 21)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (356, CAST(N'2019-04-05T13:09:27.9881300' AS DateTime2), CAST(N'2019-03-07T12:49:23.5063762' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190307, NULL, 11, 22)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (357, CAST(N'2019-04-05T13:09:28.0167066' AS DateTime2), CAST(N'2019-03-07T12:49:23.5063790' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190322, NULL, 11, 22)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (358, CAST(N'2019-04-05T13:09:28.0453079' AS DateTime2), CAST(N'2019-03-07T12:49:23.5063831' AS DateTime2), 0, N'Báo cáo tiến độ lần 1', 730, 3, 3, 20190307, NULL, 11, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (359, NULL, CAST(N'2019-03-07T12:49:23.5067748' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190406, NULL, 12, 23)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (360, NULL, CAST(N'2019-03-07T12:49:23.5067777' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190413, NULL, 12, 23)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (361, NULL, CAST(N'2019-03-07T12:49:23.5067798' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190420, NULL, 12, 23)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (362, NULL, CAST(N'2019-03-07T12:49:23.5067818' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190427, NULL, 12, 23)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (363, NULL, CAST(N'2019-03-07T12:49:23.5071224' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190406, NULL, 12, 24)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (364, NULL, CAST(N'2019-03-07T12:49:23.5071253' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190421, NULL, 12, 24)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (365, NULL, CAST(N'2019-03-07T12:49:23.5071298' AS DateTime2), 0, N'Báo cáo tiến độ lần 2', 730, 3, 1, 20190406, NULL, 12, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (366, NULL, CAST(N'2019-03-07T12:49:23.5071323' AS DateTime2), 0, N'Visit', 730, 2, 1, 20190406, NULL, 12, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (367, CAST(N'2019-04-06T14:09:51.7163006' AS DateTime2), CAST(N'2019-03-07T13:15:39.3174630' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190322, NULL, 13, 25)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (368, NULL, CAST(N'2019-03-07T13:15:39.3174687' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190406, NULL, 13, 25)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (369, CAST(N'2019-04-06T14:09:51.7394863' AS DateTime2), CAST(N'2019-03-07T13:15:39.3179742' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190322, NULL, 13, 26)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (370, NULL, CAST(N'2019-03-07T13:15:39.3179775' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190406, NULL, 13, 26)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (371, CAST(N'2019-04-06T14:09:51.7641978' AS DateTime2), CAST(N'2019-03-07T13:15:39.3179821' AS DateTime2), 0, N'Báo cáo tiến độ lần 1', 730, 3, 3, 20190401, NULL, 13, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (372, NULL, CAST(N'2019-03-07T13:15:39.3197632' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190413, NULL, 14, 27)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (373, NULL, CAST(N'2019-03-07T13:15:39.3197661' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190420, NULL, 14, 27)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (374, NULL, CAST(N'2019-03-07T13:15:39.3197681' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190427, NULL, 14, 27)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (375, NULL, CAST(N'2019-03-07T13:15:39.3197701' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190504, NULL, 14, 27)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (376, NULL, CAST(N'2019-03-07T13:15:39.3201306' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190421, NULL, 14, 28)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (377, NULL, CAST(N'2019-03-07T13:15:39.3201399' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190506, NULL, 14, 28)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (378, NULL, CAST(N'2019-03-07T13:15:39.3201453' AS DateTime2), 0, N'Báo cáo tiến độ lần 2', 730, 3, 1, 20190501, NULL, 14, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (379, NULL, CAST(N'2019-03-07T13:15:39.3201479' AS DateTime2), 0, N'Visit', 730, 2, 1, 20190501, NULL, 14, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (380, CAST(N'2019-04-06T14:09:51.7922074' AS DateTime2), CAST(N'2019-03-07T13:15:39.3212785' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190322, NULL, 15, 29)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (381, NULL, CAST(N'2019-03-07T13:15:39.3212816' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190406, NULL, 15, 29)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (382, CAST(N'2019-04-06T14:09:51.8209748' AS DateTime2), CAST(N'2019-03-07T13:15:39.3216452' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190322, NULL, 15, 30)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (383, NULL, CAST(N'2019-03-07T13:15:39.3216482' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190406, NULL, 15, 30)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (384, CAST(N'2019-04-06T14:09:51.8541176' AS DateTime2), CAST(N'2019-03-07T13:15:39.3216527' AS DateTime2), 0, N'Báo cáo tiến độ lần 1', 730, 3, 3, 20190401, NULL, 15, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (385, NULL, CAST(N'2019-03-07T13:15:39.3220314' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190413, NULL, 16, 31)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (386, NULL, CAST(N'2019-03-07T13:15:39.3220344' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190420, NULL, 16, 31)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (387, NULL, CAST(N'2019-03-07T13:15:39.3220365' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190427, NULL, 16, 31)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (388, NULL, CAST(N'2019-03-07T13:15:39.3220385' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190504, NULL, 16, 31)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (389, NULL, CAST(N'2019-03-07T13:15:39.3223878' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190421, NULL, 16, 32)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (390, NULL, CAST(N'2019-03-07T13:15:39.3223971' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190506, NULL, 16, 32)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (391, NULL, CAST(N'2019-03-07T13:15:39.3224024' AS DateTime2), 0, N'Báo cáo tiến độ lần 2', 730, 3, 1, 20190501, NULL, 16, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (392, NULL, CAST(N'2019-03-07T13:15:39.3224050' AS DateTime2), 0, N'Visit', 730, 2, 1, 20190501, NULL, 16, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (393, CAST(N'2019-04-06T14:09:51.8999245' AS DateTime2), CAST(N'2019-03-07T13:15:39.3228352' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190322, NULL, 17, 33)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (394, NULL, CAST(N'2019-03-07T13:15:39.3228382' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190406, NULL, 17, 33)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (395, CAST(N'2019-04-06T14:09:51.9251141' AS DateTime2), CAST(N'2019-03-07T13:15:39.3231995' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190322, NULL, 17, 34)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (396, NULL, CAST(N'2019-03-07T13:15:39.3232025' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190406, NULL, 17, 34)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (397, CAST(N'2019-04-06T14:09:51.9528793' AS DateTime2), CAST(N'2019-03-07T13:15:39.3232069' AS DateTime2), 0, N'Báo cáo tiến độ lần 1', 730, 3, 3, 20190401, NULL, 17, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (398, NULL, CAST(N'2019-03-07T13:15:39.3235873' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190413, NULL, 18, 35)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (399, NULL, CAST(N'2019-03-07T13:15:39.3235903' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190420, NULL, 18, 35)
GO
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (400, NULL, CAST(N'2019-03-07T13:15:39.3235922' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190427, NULL, 18, 35)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (401, NULL, CAST(N'2019-03-07T13:15:39.3235943' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190504, NULL, 18, 35)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (402, NULL, CAST(N'2019-03-07T13:15:39.3239365' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190421, NULL, 18, 36)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (403, NULL, CAST(N'2019-03-07T13:15:39.3239394' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190506, NULL, 18, 36)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (404, NULL, CAST(N'2019-03-07T13:15:39.3239442' AS DateTime2), 0, N'Báo cáo tiến độ lần 2', 730, 3, 1, 20190501, NULL, 18, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (405, NULL, CAST(N'2019-03-07T13:15:39.3239468' AS DateTime2), 0, N'Visit', 730, 2, 1, 20190501, NULL, 18, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (406, CAST(N'2019-04-06T14:09:51.9844269' AS DateTime2), CAST(N'2019-03-07T14:02:16.7564709' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190322, NULL, 19, 37)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (407, NULL, CAST(N'2019-03-07T14:02:16.7568953' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190406, NULL, 19, 37)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (408, CAST(N'2019-04-06T14:09:52.0085309' AS DateTime2), CAST(N'2019-03-07T14:02:16.7604061' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190322, NULL, 19, 38)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (409, NULL, CAST(N'2019-03-07T14:02:16.7604460' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190406, NULL, 19, 38)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (410, CAST(N'2019-04-06T14:09:52.0325824' AS DateTime2), CAST(N'2019-03-07T14:02:16.7604641' AS DateTime2), 0, N'Báo cáo tiến độ lần 1', 730, 3, 3, 20190401, NULL, 19, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (411, NULL, CAST(N'2019-03-07T14:02:16.7672290' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190413, NULL, 20, 39)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (412, NULL, CAST(N'2019-03-07T14:02:16.7672345' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190420, NULL, 20, 39)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (413, NULL, CAST(N'2019-03-07T14:02:16.7672369' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190427, NULL, 20, 39)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (414, NULL, CAST(N'2019-03-07T14:02:16.7672393' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190504, NULL, 20, 39)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (415, NULL, CAST(N'2019-03-07T14:02:16.7687245' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190421, NULL, 20, 40)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (416, NULL, CAST(N'2019-03-07T14:02:16.7687305' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190506, NULL, 20, 40)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (417, NULL, CAST(N'2019-03-07T14:02:16.7687420' AS DateTime2), 0, N'Báo cáo tiến độ lần 2', 730, 3, 1, 20190501, NULL, 20, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (418, NULL, CAST(N'2019-03-07T14:02:16.7687469' AS DateTime2), 0, N'Visit', 730, 2, 1, 20190501, NULL, 20, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (419, CAST(N'2019-04-06T14:09:52.0623479' AS DateTime2), CAST(N'2019-03-07T14:02:16.7753109' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190322, NULL, 21, 41)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (420, NULL, CAST(N'2019-03-07T14:02:16.7753157' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190406, NULL, 21, 41)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (421, CAST(N'2019-04-06T14:09:52.0876087' AS DateTime2), CAST(N'2019-03-07T14:02:16.7758729' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190322, NULL, 21, 42)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (422, NULL, CAST(N'2019-03-07T14:02:16.7758759' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190406, NULL, 21, 42)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (423, CAST(N'2019-04-06T14:09:52.1152440' AS DateTime2), CAST(N'2019-03-07T14:02:16.7758808' AS DateTime2), 0, N'Báo cáo tiến độ lần 1', 730, 3, 3, 20190401, NULL, 21, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (424, NULL, CAST(N'2019-03-07T14:02:16.7763861' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190413, NULL, 22, 43)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (425, NULL, CAST(N'2019-03-07T14:02:16.7763885' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190420, NULL, 22, 43)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (426, NULL, CAST(N'2019-03-07T14:02:16.7763897' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190427, NULL, 22, 43)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (427, NULL, CAST(N'2019-03-07T14:02:16.7763915' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190504, NULL, 22, 43)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (428, NULL, CAST(N'2019-03-07T14:02:16.7768214' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190421, NULL, 22, 44)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (429, NULL, CAST(N'2019-03-07T14:02:16.7768238' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190506, NULL, 22, 44)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (430, NULL, CAST(N'2019-03-07T14:02:16.7768287' AS DateTime2), 0, N'Báo cáo tiến độ lần 2', 730, 3, 1, 20190501, NULL, 22, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (431, NULL, CAST(N'2019-03-07T14:02:16.7768311' AS DateTime2), 0, N'Visit', 730, 2, 1, 20190501, NULL, 22, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (432, CAST(N'2019-04-06T14:09:52.1484973' AS DateTime2), CAST(N'2019-03-07T14:02:16.7774505' AS DateTime2), 0, N'SMS', 730, 0, 3, 20190322, NULL, 23, 45)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (433, NULL, CAST(N'2019-03-07T14:02:16.7774529' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190406, NULL, 23, 45)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (434, CAST(N'2019-04-06T14:09:52.1718714' AS DateTime2), CAST(N'2019-03-07T14:02:16.7779148' AS DateTime2), 0, N'Phone call', 730, 1, 3, 20190322, NULL, 23, 46)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (435, NULL, CAST(N'2019-03-07T14:02:16.7779172' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190406, NULL, 23, 46)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (436, CAST(N'2019-04-06T14:09:52.1949020' AS DateTime2), CAST(N'2019-03-07T14:02:16.7779214' AS DateTime2), 0, N'Báo cáo tiến độ lần 1', 730, 3, 3, 20190401, NULL, 23, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (437, NULL, CAST(N'2019-03-07T14:02:16.7783724' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190413, NULL, 24, 47)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (438, NULL, CAST(N'2019-03-07T14:02:16.7783749' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190420, NULL, 24, 47)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (439, NULL, CAST(N'2019-03-07T14:02:16.7783761' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190427, NULL, 24, 47)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (440, NULL, CAST(N'2019-03-07T14:02:16.7783773' AS DateTime2), 0, N'SMS', 730, 0, 1, 20190504, NULL, 24, 47)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (441, NULL, CAST(N'2019-03-07T14:02:16.7788458' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190421, NULL, 24, 48)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (442, NULL, CAST(N'2019-03-07T14:02:16.7788476' AS DateTime2), 0, N'Phone call', 730, 1, 1, 20190506, NULL, 24, 48)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (443, NULL, CAST(N'2019-03-07T14:02:16.7788531' AS DateTime2), 0, N'Báo cáo tiến độ lần 2', 730, 3, 1, 20190501, NULL, 24, NULL)
INSERT [dbo].[ProgressStageActions] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [StartTime], [Type], [Status], [ExcutionDay], [DoneAt], [ProgressStageId], [ProgressMessageFormId]) VALUES (444, NULL, CAST(N'2019-03-07T14:02:16.7788549' AS DateTime2), 0, N'Visit', 730, 2, 1, 20190501, NULL, 24, NULL)
SET IDENTITY_INSERT [dbo].[ProgressStageActions] OFF
SET IDENTITY_INSERT [dbo].[ProgressStages] ON 

INSERT [dbo].[ProgressStages] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Duration], [Sequence], [Status], [CollectorComment], [CollectionProgressId]) VALUES (1, NULL, CAST(N'2019-03-07T11:21:13.4256028' AS DateTime2), 0, N'Stage 1', 30, 1, 1, NULL, 1)
INSERT [dbo].[ProgressStages] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Duration], [Sequence], [Status], [CollectorComment], [CollectionProgressId]) VALUES (2, NULL, CAST(N'2019-03-07T11:21:13.4445979' AS DateTime2), 0, N'Stage 2', 30, 2, 1, NULL, 1)
INSERT [dbo].[ProgressStages] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Duration], [Sequence], [Status], [CollectorComment], [CollectionProgressId]) VALUES (3, NULL, CAST(N'2019-03-07T11:21:13.4474928' AS DateTime2), 0, N'Stage 1', 30, 1, 1, NULL, 2)
INSERT [dbo].[ProgressStages] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Duration], [Sequence], [Status], [CollectorComment], [CollectionProgressId]) VALUES (4, NULL, CAST(N'2019-03-07T11:21:13.4484811' AS DateTime2), 0, N'Stage 2', 30, 2, 1, NULL, 2)
INSERT [dbo].[ProgressStages] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Duration], [Sequence], [Status], [CollectorComment], [CollectionProgressId]) VALUES (5, NULL, CAST(N'2019-03-07T11:21:13.4493722' AS DateTime2), 0, N'Stage 1', 30, 1, 1, NULL, 3)
INSERT [dbo].[ProgressStages] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Duration], [Sequence], [Status], [CollectorComment], [CollectionProgressId]) VALUES (6, NULL, CAST(N'2019-03-07T11:21:13.4503160' AS DateTime2), 0, N'Stage 2', 30, 2, 1, NULL, 3)
INSERT [dbo].[ProgressStages] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Duration], [Sequence], [Status], [CollectorComment], [CollectionProgressId]) VALUES (7, NULL, CAST(N'2019-03-07T12:49:23.4836610' AS DateTime2), 0, N'Stage 1', 30, 1, 1, NULL, 4)
INSERT [dbo].[ProgressStages] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Duration], [Sequence], [Status], [CollectorComment], [CollectionProgressId]) VALUES (8, NULL, CAST(N'2019-03-07T12:49:23.5012916' AS DateTime2), 0, N'Stage 2', 30, 2, 1, NULL, 4)
INSERT [dbo].[ProgressStages] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Duration], [Sequence], [Status], [CollectorComment], [CollectionProgressId]) VALUES (9, NULL, CAST(N'2019-03-07T12:49:23.5040195' AS DateTime2), 0, N'Stage 1', 30, 1, 1, NULL, 5)
INSERT [dbo].[ProgressStages] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Duration], [Sequence], [Status], [CollectorComment], [CollectionProgressId]) VALUES (10, NULL, CAST(N'2019-03-07T12:49:23.5048169' AS DateTime2), 0, N'Stage 2', 30, 2, 1, NULL, 5)
INSERT [dbo].[ProgressStages] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Duration], [Sequence], [Status], [CollectorComment], [CollectionProgressId]) VALUES (11, NULL, CAST(N'2019-03-07T12:49:23.5056607' AS DateTime2), 0, N'Stage 1', 30, 1, 1, NULL, 6)
INSERT [dbo].[ProgressStages] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Duration], [Sequence], [Status], [CollectorComment], [CollectionProgressId]) VALUES (12, NULL, CAST(N'2019-03-07T12:49:23.5063868' AS DateTime2), 0, N'Stage 2', 30, 2, 1, NULL, 6)
INSERT [dbo].[ProgressStages] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Duration], [Sequence], [Status], [CollectorComment], [CollectionProgressId]) VALUES (13, NULL, CAST(N'2019-03-07T13:15:39.2538383' AS DateTime2), 0, N'Stage 1', 30, 1, 1, NULL, 7)
INSERT [dbo].[ProgressStages] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Duration], [Sequence], [Status], [CollectorComment], [CollectionProgressId]) VALUES (14, NULL, CAST(N'2019-03-07T13:15:39.3180292' AS DateTime2), 0, N'Stage 2', 30, 2, 1, NULL, 7)
INSERT [dbo].[ProgressStages] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Duration], [Sequence], [Status], [CollectorComment], [CollectionProgressId]) VALUES (15, NULL, CAST(N'2019-03-07T13:15:39.3208753' AS DateTime2), 0, N'Stage 1', 30, 1, 1, NULL, 8)
INSERT [dbo].[ProgressStages] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Duration], [Sequence], [Status], [CollectorComment], [CollectionProgressId]) VALUES (16, NULL, CAST(N'2019-03-07T13:15:39.3216567' AS DateTime2), 0, N'Stage 2', 30, 2, 1, NULL, 8)
INSERT [dbo].[ProgressStages] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Duration], [Sequence], [Status], [CollectorComment], [CollectionProgressId]) VALUES (17, NULL, CAST(N'2019-03-07T13:15:39.3224775' AS DateTime2), 0, N'Stage 1', 30, 1, 1, NULL, 9)
INSERT [dbo].[ProgressStages] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Duration], [Sequence], [Status], [CollectorComment], [CollectionProgressId]) VALUES (18, NULL, CAST(N'2019-03-07T13:15:39.3232108' AS DateTime2), 0, N'Stage 2', 30, 2, 1, NULL, 9)
INSERT [dbo].[ProgressStages] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Duration], [Sequence], [Status], [CollectorComment], [CollectionProgressId]) VALUES (19, NULL, CAST(N'2019-03-07T14:02:16.6745048' AS DateTime2), 0, N'Stage 1', 30, 1, 1, NULL, 10)
INSERT [dbo].[ProgressStages] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Duration], [Sequence], [Status], [CollectorComment], [CollectionProgressId]) VALUES (20, NULL, CAST(N'2019-03-07T14:02:16.7612677' AS DateTime2), 0, N'Stage 2', 30, 2, 1, NULL, 10)
INSERT [dbo].[ProgressStages] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Duration], [Sequence], [Status], [CollectorComment], [CollectionProgressId]) VALUES (21, NULL, CAST(N'2019-03-07T14:02:16.7741685' AS DateTime2), 0, N'Stage 1', 30, 1, 1, NULL, 11)
INSERT [dbo].[ProgressStages] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Duration], [Sequence], [Status], [CollectorComment], [CollectionProgressId]) VALUES (22, NULL, CAST(N'2019-03-07T14:02:16.7758862' AS DateTime2), 0, N'Stage 2', 30, 2, 1, NULL, 11)
INSERT [dbo].[ProgressStages] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Duration], [Sequence], [Status], [CollectorComment], [CollectionProgressId]) VALUES (23, NULL, CAST(N'2019-03-07T14:02:16.7770025' AS DateTime2), 0, N'Stage 1', 30, 1, 1, NULL, 12)
INSERT [dbo].[ProgressStages] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [Name], [Duration], [Sequence], [Status], [CollectorComment], [CollectionProgressId]) VALUES (24, NULL, CAST(N'2019-03-07T14:02:16.7779335' AS DateTime2), 0, N'Stage 2', 30, 2, 1, NULL, 12)
SET IDENTITY_INSERT [dbo].[ProgressStages] OFF
SET IDENTITY_INSERT [dbo].[Receivables] ON 

INSERT [dbo].[Receivables] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [ClosedDay], [PayableDay], [PrepaidAmount], [DebtAmount], [CustomerId], [LocationId]) VALUES (1, NULL, CAST(N'2019-03-07T11:21:13.4515051' AS DateTime2), 0, NULL, 20190307, 200000, 1000000, 1, NULL)
INSERT [dbo].[Receivables] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [ClosedDay], [PayableDay], [PrepaidAmount], [DebtAmount], [CustomerId], [LocationId]) VALUES (2, NULL, CAST(N'2019-03-07T11:21:13.8694865' AS DateTime2), 0, NULL, 20190307, 0, 50000000, 1, NULL)
INSERT [dbo].[Receivables] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [ClosedDay], [PayableDay], [PrepaidAmount], [DebtAmount], [CustomerId], [LocationId]) VALUES (3, NULL, CAST(N'2019-03-07T11:21:13.9619620' AS DateTime2), 0, NULL, 20190307, 300000000, 1000000000, 1, NULL)
INSERT [dbo].[Receivables] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [ClosedDay], [PayableDay], [PrepaidAmount], [DebtAmount], [CustomerId], [LocationId]) VALUES (4, NULL, CAST(N'2019-03-07T12:49:23.5074704' AS DateTime2), 0, NULL, 20190307, 200000, 1000000, 1, NULL)
INSERT [dbo].[Receivables] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [ClosedDay], [PayableDay], [PrepaidAmount], [DebtAmount], [CustomerId], [LocationId]) VALUES (5, NULL, CAST(N'2019-03-07T12:49:23.7060923' AS DateTime2), 0, NULL, 20190307, 0, 50000000, 1, NULL)
INSERT [dbo].[Receivables] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [ClosedDay], [PayableDay], [PrepaidAmount], [DebtAmount], [CustomerId], [LocationId]) VALUES (6, NULL, CAST(N'2019-03-07T12:49:23.7299080' AS DateTime2), 0, NULL, 20190307, 300000000, 1000000000, 1, NULL)
INSERT [dbo].[Receivables] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [ClosedDay], [PayableDay], [PrepaidAmount], [DebtAmount], [CustomerId], [LocationId]) VALUES (7, NULL, CAST(N'2019-03-07T13:15:39.3243009' AS DateTime2), 0, NULL, 20190307, 200000, 1000000, 1, NULL)
INSERT [dbo].[Receivables] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [ClosedDay], [PayableDay], [PrepaidAmount], [DebtAmount], [CustomerId], [LocationId]) VALUES (8, NULL, CAST(N'2019-03-07T13:15:39.7839279' AS DateTime2), 0, NULL, 20190307, 0, 50000000, 1, NULL)
INSERT [dbo].[Receivables] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [ClosedDay], [PayableDay], [PrepaidAmount], [DebtAmount], [CustomerId], [LocationId]) VALUES (9, NULL, CAST(N'2019-03-07T13:15:39.8102455' AS DateTime2), 0, NULL, 20190307, 300000000, 1000000000, 1, NULL)
INSERT [dbo].[Receivables] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [ClosedDay], [PayableDay], [PrepaidAmount], [DebtAmount], [CustomerId], [LocationId]) VALUES (10, NULL, CAST(N'2019-03-07T14:02:16.7793916' AS DateTime2), 0, 20190405, 20190307, 200000, 1000000, 1, NULL)
INSERT [dbo].[Receivables] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [ClosedDay], [PayableDay], [PrepaidAmount], [DebtAmount], [CustomerId], [LocationId]) VALUES (11, NULL, CAST(N'2019-03-07T14:02:17.5825467' AS DateTime2), 0, NULL, 20190307, 0, 50000000, 1, NULL)
INSERT [dbo].[Receivables] ([Id], [UpdatedDate], [CreatedDate], [IsDeleted], [ClosedDay], [PayableDay], [PrepaidAmount], [DebtAmount], [CustomerId], [LocationId]) VALUES (12, NULL, CAST(N'2019-03-07T14:02:17.6290860' AS DateTime2), 0, NULL, 20190307, 300000000, 1000000000, 1, NULL)
SET IDENTITY_INSERT [dbo].[Receivables] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 09/03/2019 12:12:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 09/03/2019 12:12:23 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 09/03/2019 12:12:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 09/03/2019 12:12:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 09/03/2019 12:12:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 09/03/2019 12:12:23 PM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_AspNetUsers_LocationId]    Script Date: 09/03/2019 12:12:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUsers_LocationId] ON [dbo].[AspNetUsers]
(
	[LocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 09/03/2019 12:12:23 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_AssignedCollectors_ReceivableId]    Script Date: 09/03/2019 12:12:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_AssignedCollectors_ReceivableId] ON [dbo].[AssignedCollectors]
(
	[ReceivableId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AssignedCollectors_UserId]    Script Date: 09/03/2019 12:12:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_AssignedCollectors_UserId] ON [dbo].[AssignedCollectors]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CollectionProgresses_ProfileId]    Script Date: 09/03/2019 12:12:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_CollectionProgresses_ProfileId] ON [dbo].[CollectionProgresses]
(
	[ProfileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CollectionProgresses_ReceivableId]    Script Date: 09/03/2019 12:12:23 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_CollectionProgresses_ReceivableId] ON [dbo].[CollectionProgresses]
(
	[ReceivableId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Contacts_ReceivableId]    Script Date: 09/03/2019 12:12:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_Contacts_ReceivableId] ON [dbo].[Contacts]
(
	[ReceivableId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_HubUserConnections_UserId]    Script Date: 09/03/2019 12:12:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_HubUserConnections_UserId] ON [dbo].[HubUserConnections]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Notifications_UserId]    Script Date: 09/03/2019 12:12:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_Notifications_UserId] ON [dbo].[Notifications]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProfileStageActions_ProfileMessageFormId]    Script Date: 09/03/2019 12:12:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProfileStageActions_ProfileMessageFormId] ON [dbo].[ProfileStageActions]
(
	[ProfileMessageFormId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProfileStageActions_ProfileStageId]    Script Date: 09/03/2019 12:12:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProfileStageActions_ProfileStageId] ON [dbo].[ProfileStageActions]
(
	[ProfileStageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProfileStages_ProfileId]    Script Date: 09/03/2019 12:12:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProfileStages_ProfileId] ON [dbo].[ProfileStages]
(
	[ProfileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProgressStageActions_ProgressMessageFormId]    Script Date: 09/03/2019 12:12:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProgressStageActions_ProgressMessageFormId] ON [dbo].[ProgressStageActions]
(
	[ProgressMessageFormId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProgressStageActions_ProgressStageId]    Script Date: 09/03/2019 12:12:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProgressStageActions_ProgressStageId] ON [dbo].[ProgressStageActions]
(
	[ProgressStageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProgressStages_CollectionProgressId]    Script Date: 09/03/2019 12:12:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProgressStages_CollectionProgressId] ON [dbo].[ProgressStages]
(
	[CollectionProgressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Receivables_CustomerId]    Script Date: 09/03/2019 12:12:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_Receivables_CustomerId] ON [dbo].[Receivables]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Receivables_LocationId]    Script Date: 09/03/2019 12:12:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_Receivables_LocationId] ON [dbo].[Receivables]
(
	[LocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUsers_Locations_LocationId] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Locations] ([Id])
GO
ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_AspNetUsers_Locations_LocationId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AssignedCollectors]  WITH CHECK ADD  CONSTRAINT [FK_AssignedCollectors_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[AssignedCollectors] CHECK CONSTRAINT [FK_AssignedCollectors_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AssignedCollectors]  WITH CHECK ADD  CONSTRAINT [FK_AssignedCollectors_Receivables_ReceivableId] FOREIGN KEY([ReceivableId])
REFERENCES [dbo].[Receivables] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AssignedCollectors] CHECK CONSTRAINT [FK_AssignedCollectors_Receivables_ReceivableId]
GO
ALTER TABLE [dbo].[CollectionProgresses]  WITH CHECK ADD  CONSTRAINT [FK_CollectionProgresses_Profiles_ProfileId] FOREIGN KEY([ProfileId])
REFERENCES [dbo].[Profiles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CollectionProgresses] CHECK CONSTRAINT [FK_CollectionProgresses_Profiles_ProfileId]
GO
ALTER TABLE [dbo].[CollectionProgresses]  WITH CHECK ADD  CONSTRAINT [FK_CollectionProgresses_Receivables_ReceivableId] FOREIGN KEY([ReceivableId])
REFERENCES [dbo].[Receivables] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CollectionProgresses] CHECK CONSTRAINT [FK_CollectionProgresses_Receivables_ReceivableId]
GO
ALTER TABLE [dbo].[Contacts]  WITH CHECK ADD  CONSTRAINT [FK_Contacts_Receivables_ReceivableId] FOREIGN KEY([ReceivableId])
REFERENCES [dbo].[Receivables] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Contacts] CHECK CONSTRAINT [FK_Contacts_Receivables_ReceivableId]
GO
ALTER TABLE [dbo].[HubUserConnections]  WITH CHECK ADD  CONSTRAINT [FK_HubUserConnections_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[HubUserConnections] CHECK CONSTRAINT [FK_HubUserConnections_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD  CONSTRAINT [FK_Notifications_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Notifications] CHECK CONSTRAINT [FK_Notifications_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[ProfileStageActions]  WITH CHECK ADD  CONSTRAINT [FK_ProfileStageActions_ProfileMessageForms_ProfileMessageFormId] FOREIGN KEY([ProfileMessageFormId])
REFERENCES [dbo].[ProfileMessageForms] ([Id])
GO
ALTER TABLE [dbo].[ProfileStageActions] CHECK CONSTRAINT [FK_ProfileStageActions_ProfileMessageForms_ProfileMessageFormId]
GO
ALTER TABLE [dbo].[ProfileStageActions]  WITH CHECK ADD  CONSTRAINT [FK_ProfileStageActions_ProfileStages_ProfileStageId] FOREIGN KEY([ProfileStageId])
REFERENCES [dbo].[ProfileStages] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProfileStageActions] CHECK CONSTRAINT [FK_ProfileStageActions_ProfileStages_ProfileStageId]
GO
ALTER TABLE [dbo].[ProfileStages]  WITH CHECK ADD  CONSTRAINT [FK_ProfileStages_Profiles_ProfileId] FOREIGN KEY([ProfileId])
REFERENCES [dbo].[Profiles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProfileStages] CHECK CONSTRAINT [FK_ProfileStages_Profiles_ProfileId]
GO
ALTER TABLE [dbo].[ProgressStageActions]  WITH CHECK ADD  CONSTRAINT [FK_ProgressStageActions_ProgressMessageForms_ProgressMessageFormId] FOREIGN KEY([ProgressMessageFormId])
REFERENCES [dbo].[ProgressMessageForms] ([Id])
GO
ALTER TABLE [dbo].[ProgressStageActions] CHECK CONSTRAINT [FK_ProgressStageActions_ProgressMessageForms_ProgressMessageFormId]
GO
ALTER TABLE [dbo].[ProgressStageActions]  WITH CHECK ADD  CONSTRAINT [FK_ProgressStageActions_ProgressStages_ProgressStageId] FOREIGN KEY([ProgressStageId])
REFERENCES [dbo].[ProgressStages] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProgressStageActions] CHECK CONSTRAINT [FK_ProgressStageActions_ProgressStages_ProgressStageId]
GO
ALTER TABLE [dbo].[ProgressStages]  WITH CHECK ADD  CONSTRAINT [FK_ProgressStages_CollectionProgresses_CollectionProgressId] FOREIGN KEY([CollectionProgressId])
REFERENCES [dbo].[CollectionProgresses] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProgressStages] CHECK CONSTRAINT [FK_ProgressStages_CollectionProgresses_CollectionProgressId]
GO
ALTER TABLE [dbo].[Receivables]  WITH CHECK ADD  CONSTRAINT [FK_Receivables_Customers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Receivables] CHECK CONSTRAINT [FK_Receivables_Customers_CustomerId]
GO
ALTER TABLE [dbo].[Receivables]  WITH CHECK ADD  CONSTRAINT [FK_Receivables_Locations_LocationId] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Locations] ([Id])
GO
ALTER TABLE [dbo].[Receivables] CHECK CONSTRAINT [FK_Receivables_Locations_LocationId]
GO
USE [master]
GO
ALTER DATABASE [RCM] SET  READ_WRITE 
GO
