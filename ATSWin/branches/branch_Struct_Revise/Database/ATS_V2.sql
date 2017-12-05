USE [master]
GO

/****** Object:  Database [ATS_V2]    Script Date: 05/05/2015 10:01:57 ******/
CREATE DATABASE [ATS_V2] ON  PRIMARY 
( NAME = N'NewDebug', FILENAME = N'E:\Database_Home\MSSQL10.ATS_HOME\MSSQL\DATA\NewDebug.mdf' , SIZE = 13504KB , MAXSIZE = UNLIMITED, FILEGROWTH = 5%)
 LOG ON 
( NAME = N'NewDebug_log', FILENAME = N'E:\Database_Home\MSSQL10.ATS_HOME\MSSQL\DATA\NewDebug.ldf' , SIZE = 21504KB , MAXSIZE = 2048GB , FILEGROWTH = 5%)
GO

ALTER DATABASE [ATS_V2] SET COMPATIBILITY_LEVEL = 100
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ATS_V2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [ATS_V2] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [ATS_V2] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [ATS_V2] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [ATS_V2] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [ATS_V2] SET ARITHABORT OFF 
GO

ALTER DATABASE [ATS_V2] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [ATS_V2] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [ATS_V2] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [ATS_V2] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [ATS_V2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [ATS_V2] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [ATS_V2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [ATS_V2] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [ATS_V2] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [ATS_V2] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [ATS_V2] SET  DISABLE_BROKER 
GO

ALTER DATABASE [ATS_V2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [ATS_V2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [ATS_V2] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [ATS_V2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [ATS_V2] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [ATS_V2] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [ATS_V2] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [ATS_V2] SET  READ_WRITE 
GO

ALTER DATABASE [ATS_V2] SET RECOVERY FULL 
GO

ALTER DATABASE [ATS_V2] SET  MULTI_USER 
GO

ALTER DATABASE [ATS_V2] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [ATS_V2] SET DB_CHAINING OFF 
GO
