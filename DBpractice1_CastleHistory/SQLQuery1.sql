CREATE TABLE westerncastle(
	id_num	INT	 not NULL IDENTITY(1,1) PRIMARY KEY
	,castle_name NVARCHAR(50)  not NULL
	,build_year INT
	,country_name NVARCHAR(50) not NULL
	,owner_name NVARCHAR(50)
	,important_grade INT not NULL
	);


Insert westerncastle (castle_name, build_year, country_name, owner_name, important_grade) VALUES ('�m�C�V���o���V���^�C����','1886','�h�C�c','���[�g���B�q2��','5');
