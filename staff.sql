CREATE TABLE [person] (
	[person_id] int IDENTITY(1,1) NOT NULL UNIQUE,
	[person_fullname] nvarchar(50) NOT NULL,
	[person_sex] nvarchar(50) NOT NULL,
	[person_birth] date NOT NULL,
	[person_rank] nvarchar(50) NOT NULL,
	[person_post] nvarchar(50) NOT NULL,
	[person_adress] nvarchar(100) NOT NULL,
	[person_passport] nvarchar(50) NOT NULL,
	[person_idcard] nvarchar(50) NOT NULL,
	[person_phone] nvarchar(50) NOT NULL,
	[person_unit]  nvarchar(100) NOT NULL,
	PRIMARY KEY ([person_id])
);

CREATE TABLE [users] (
	[user_id] int IDENTITY(1,1) NOT NULL UNIQUE,
	[user_fullname] nvarchar(100) NOT NULL,
	[user_login] nvarchar(100) NOT NULL,
	[user_password] nvarchar(100) NOT NULL,
	PRIMARY KEY ([user_id])
);
