-- =====================必要に応じてコメント解除===========================
--sqlcmd -E -S localhost\SQLEXPRESS

--CREATE DATABASE emdb
--go

--USE emdb
--go
-- =====================必要に応じてコメント解除===========================

DROP TABLE IF exists employee;

CREATE TABLE employee (
     id_employee     INT NOT NULL IDENTITY(10000,1) PRIMARY KEY
    ,nm_employee     NVARCHAR(50)         NOT NULL
    ,kn_employee     NVARCHAR(50)         NOT NULL
    ,mail_address    NVARCHAR(100)        NOT NULL
    ,password        NVARCHAR(10)         NOT NULL
    ,id_department   int                 NOT NULL
);

DROP TABLE IF exists department;

CREATE TABLE department (
     id_department   INT NOT NULL IDENTITY(10,1) PRIMARY KEY
    ,nm_department   NVARCHAR(25)         NOT NULL
);

INSERT INTO employee (nm_employee, kn_employee, mail_address, password, id_department) VALUES ('山田龍也','ヤマダタツヤ','yamada@hoge.jp','aaa',10);
INSERT INTO employee (nm_employee, kn_employee, mail_address, password, id_department) VALUES ('高橋準','タカハシジュン','jtaka@hoge.jp','bbb',10);
INSERT INTO employee (nm_employee, kn_employee, mail_address, password, id_department) VALUES ('麻枝史明','マエダフミアキ','fmak@hoge.jp','ccc',11);
INSERT INTO employee (nm_employee, kn_employee, mail_address, password, id_department) VALUES ('丸戸章介','マルトショウスケ','marusyo@hoge.jp','ddd',12);
INSERT INTO employee (nm_employee, kn_employee, mail_address, password, id_department) VALUES ('三宅武','ミヤケタケシ','miya@hoge.jp','eee',13);
INSERT INTO employee (nm_employee, kn_employee, mail_address, password, id_department) VALUES ('丸井悠','マルイハルカ','haru@hoge.jp','fff',13);
INSERT INTO employee (nm_employee, kn_employee, mail_address, password, id_department) VALUES ('涼元香織','スズモトカオリ','suzumoto@hoge.jp','ggg',13);
INSERT INTO employee (nm_employee, kn_employee, mail_address, password, id_department) VALUES ('葉山稔','ハヤマミノル','mhayama@hoge.jp','hhh',14);
INSERT INTO employee (nm_employee, kn_employee, mail_address, password, id_department) VALUES ('川上朱里','カワカミアカリ','kawakami@hoge.jp','iii',14);
INSERT INTO employee (nm_employee, kn_employee, mail_address, password, id_department) VALUES ('杉井肇','スギイハジメ','sugi@hoge.jp','jjj',15);

INSERT INTO department (nm_department) VALUES ('役員');
INSERT INTO department (nm_department) VALUES ('人事・総務部');
INSERT INTO department (nm_department) VALUES ('経理部');
INSERT INTO department (nm_department) VALUES ('システム開発事業部');
INSERT INTO department (nm_department) VALUES ('教育事業部');
INSERT INTO department (nm_department) VALUES ('営業企画部');
go