CREATE TABLE FolderAction
(
	ID INT PRIMARY KEY 
	,RunTime TEXT
	,FolderAction TEXT
	,Direction TEXT
	,SourceFolder TEXT
	,DestinationFolder TEXT
	,Host TEXT
	,Port INT
	,Protocol TEXT
	,Username TEXT
	,Comment TEXT
);


CREATE TABLE RFile
(
	ID INT PRIMARY KEY 
	,Name TEXT
	,FullName TEXT
	,FileLength INT
	,ParentDirectory TEXT
	,IsDirectory INT
	,LastWriteTime TEXT
	,LastWriteTimeUTC TEXT
	,FileOperation TEXT
	,NewFileName TEXT
	,MD5 TEXT
	,FolderActionID INT
	,FOREIGN KEY(FolderActionID) REFERENCES FolderAction(ID)
);