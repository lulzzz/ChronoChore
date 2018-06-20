CREATE TABLE [dbo].[WebResourceIndex]
(
	[Url] NVARCHAR(512) NOT NULL PRIMARY KEY, 
    [Local] NVARCHAR(512) NOT NULL, 
    [DownloadTime] DATETIME NULL
)
