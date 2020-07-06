CREATE TABLE itword(
id_num INT not null IDENTITY(1,1) PRIMARY KEY
, word_name NVARCHAR(50) not null
, system_no1 NVARCHAR(50)
, system_no2 NVARCHAR(50)
, system_no3 NVARCHAR(50)
, detail NVARCHAR(200) not null
, recordtime DATETIME not null
);

INSERT itword (word_name, system_no1, system_no2, system_no3, detail, recordtime) 
VALUES ('C#', 'Dポラリス', '', '', 'プログラミング言語の一つでオブジェクト指向が特徴', CURRENT_TIMESTAMP);
