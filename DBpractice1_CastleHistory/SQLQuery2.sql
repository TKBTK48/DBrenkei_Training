CREATE TABLE japanesecastle(
	id_num INT not NULL IDENTITY(1,1) PRIMARY KEY
	,castle_name NVARCHAR(50) not null
	,build_year INT
	,prefecture_name NVARCHAR(50) not NULL
	,owner_name NVARCHAR(50)
	,important_grade INT not NULL
	,defence_power_grade INT
	,exist_flg INT
	);
INSERT japanesecastle (castle_name, build_year, prefecture_name, owner_name, important_grade, defence_power_grade, exist_flg) VALUES ('�P�H��', '1346', '���Ɍ�', '�ԏ����','5','4','1' );