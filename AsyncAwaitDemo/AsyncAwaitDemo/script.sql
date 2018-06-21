USE [master]
GO
/****** Object:  Database [PERSISTENCESTORE.MDF]    Script Date: 21-06-2018 09:15:08 ******/
CREATE DATABASE [PERSISTENCESTORE.MDF]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PersistenceStore', FILENAME = N'D:\COMPANY\GITHUB\sndjones007\ChronoChore\AsyncAwaitDemo\AsyncAwaitDemo\PersistenceStore.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PersistenceStore_log', FILENAME = N'D:\COMPANY\GITHUB\sndjones007\ChronoChore\AsyncAwaitDemo\AsyncAwaitDemo\PersistenceStore_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [PERSISTENCESTORE.MDF] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PERSISTENCESTORE.MDF].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PERSISTENCESTORE.MDF] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PERSISTENCESTORE.MDF] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PERSISTENCESTORE.MDF] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PERSISTENCESTORE.MDF] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PERSISTENCESTORE.MDF] SET ARITHABORT OFF 
GO
ALTER DATABASE [PERSISTENCESTORE.MDF] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [PERSISTENCESTORE.MDF] SET AUTO_SHRINK ON 
GO
ALTER DATABASE [PERSISTENCESTORE.MDF] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PERSISTENCESTORE.MDF] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PERSISTENCESTORE.MDF] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PERSISTENCESTORE.MDF] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PERSISTENCESTORE.MDF] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PERSISTENCESTORE.MDF] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PERSISTENCESTORE.MDF] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PERSISTENCESTORE.MDF] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PERSISTENCESTORE.MDF] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PERSISTENCESTORE.MDF] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PERSISTENCESTORE.MDF] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PERSISTENCESTORE.MDF] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PERSISTENCESTORE.MDF] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PERSISTENCESTORE.MDF] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PERSISTENCESTORE.MDF] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PERSISTENCESTORE.MDF] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PERSISTENCESTORE.MDF] SET  MULTI_USER 
GO
ALTER DATABASE [PERSISTENCESTORE.MDF] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PERSISTENCESTORE.MDF] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PERSISTENCESTORE.MDF] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PERSISTENCESTORE.MDF] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PERSISTENCESTORE.MDF] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PERSISTENCESTORE.MDF] SET QUERY_STORE = OFF
GO
USE [PERSISTENCESTORE.MDF]
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
USE [PERSISTENCESTORE.MDF]
GO
/****** Object:  Table [dbo].[WebResourceIndex]    Script Date: 21-06-2018 09:15:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebResourceIndex](
	[Url] [nvarchar](4000) NOT NULL,
	[Local] [nvarchar](4000) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[DownloadTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Url] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [PERSISTENCESTORE.MDF] SET  READ_WRITE 
GO
