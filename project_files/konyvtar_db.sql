CREATE DATABASE KONYVTAR
GO
USE KONYVTAR
GO
CREATE TABLE konyvek(
	konyv_id bigint PRIMARY KEY NOT NULL, 
	azon varchar(12) UNIQUE NOT NULL, 
	cim varchar(30) NULL,
	szerzo varchar(50) NULL,
	peldanyszam int NOT NULL, 
	szabad_peldanyszam int NOT NULL) 
GO
CREATE TABLE statuszok(
	statusz_id smallint PRIMARY KEY NOT NULL,
	statusz varchar(10),
	tagdij int)
GO
CREATE TABLE tagok(
	tag_id bigint PRIMARY KEY NOT NULL, 
	nev varchar(30) NOT NULL,
	cim varchar(50),
	azon varchar(8) UNIQUE NOT NULL,
	konyvtarjegy int UNIQUE NOT NULL,
	statusz_id smallint NOT NULL,
	FOREIGN KEY(statusz_id) REFERENCES statuszok(statusz_id))
GO
CREATE TABLE kolcsonzesek(
	kolcson_id bigint PRIMARY KEY NOT NULL,
	tag_id bigint NOT NULL,
	konyv_id bigint NOT NULL,
	kezdte datetime NOT NULL,
	vissza DATETIME,
	FOREIGN KEY(tag_id) REFERENCES tagok(tag_id),
	FOREIGN KEY(konyv_id) REFERENCES konyvek(konyv_id))
GO
CREATE TABLE befizetesek(
	bizonylatszam bigint PRIMARY KEY NOT NULL,
	tag_id bigint NOT NULL,
	osszeg int NOT NULL,
	datum datetime,
	FOREIGN KEY(tag_id) REFERENCES tagok(tag_id))
GO
CREATE TABLE olvasasok(
	olvas_id bigint  PRIMARY KEY NOT NULL,
	tag_id bigint NOT NULL,
	belepes datetime NOT NULL,
	kilepes datetime,
	FOREIGN KEY(tag_id) REFERENCES tagok(tag_id))
GO
INSERT INTO konyvek VALUES (1,'aaaaaaaaaaaa','A szamuráj útja','Josikava Eidzsi',3,3);
INSERT INTO konyvek VALUES (2,'bbbbbbbbbbbb','A háború művészete','Josikava Eidzsi',3,3);
INSERT INTO konyvek VALUES (3,'cccccccccccc','A kard útja','Josikava Eidzsi',3,3);
INSERT INTO konyvek VALUES (4,'dddddddddddd','A Busidó-kód','Josikava Eidzsi',3,3);
INSERT INTO konyvek VALUES (5,'eeeeeeeeeeee','Az élet és a halál útja','Josikava Eidzsi',3,3);
INSERT INTO konyvek VALUES (6,'ffffffffffff','1984','George Orwell',2,2);
INSERT INTO konyvek VALUES (7,'gggggggggggg','Állatfarm','George Orwell',2,2);
INSERT INTO konyvek VALUES (8,'hhhhhhhhhhhh','A vörös oroszlán','Szepes Mária',1,1);
INSERT INTO konyvek VALUES (9,'iiiiiiiiiiii','Az őrület hegyei','Howard Phillips Lovecraft',3,3);
INSERT INTO konyvek VALUES (10,'jjjjjjjjjjjj','Az áruló szív','Edgar Allan Poe',2,2);
INSERT INTO konyvek VALUES (11,'kkkkkkkkkkkk','Farkasének','Ryan Hawkwood',1,1);
INSERT INTO konyvek VALUES (12,'llllllllllll','Árnyjáték','Ryan Hawkwood',2,2);
INSERT INTO konyvek VALUES (13,'mmmmmmmmmmmm','Szellemtánc','Ryan Hawkwood',3,3);
INSERT INTO statuszok VALUES (1,'felnőtt',1000);
INSERT INTO statuszok VALUES (2,'diák',500);
INSERT INTO statuszok VALUES (3,'nyugdíjas',500);
INSERT INTO statuszok VALUES (4,'tanár',750);
INSERT INTO tagok VALUES (1,'Cene Balázs','3300 Eger Kistályai út 11','aaaaaaaa',1,1);
INSERT INTO tagok VALUES (2,'Hudák Bianka','3300 Eger Kistályai út 13','bbbbbb',2,2);
INSERT INTO tagok VALUES (3,'Oravecz Eszter','3300 Eger Kistályai út 14','cccccc',3,3);
INSERT INTO tagok VALUES (4,'Cene Bernadett','3300 Eger Kistályai út 15','dddddd',4,4);
INSERT INTO tagok VALUES (5,'Hudák Melina','3300 Eger Kistályai út 16','eeeeee',5,2);
INSERT INTO tagok VALUES (6,'Oravecz Henrik','3300 Eger Kistályai út 17','ffffff',6,1);
INSERT INTO tagok VALUES (7,'Cene Szigfrid','3300 Eger Kistályai út 18','gggggg',7,3);
INSERT INTO tagok VALUES (8,'Hudák Erika','3300 Eger Kistályai út 19','hhhhhh',8,4);
INSERT INTO tagok VALUES (9,'Oravecz Ermelinda','3300 Eger Kistályai út 20','iiiiii',9,1);
INSERT INTO tagok VALUES (10,'Cene Gottfrid','3300 Eger Kistályai út 21','jjjjjj',10,2);
INSERT INTO kolcsonzesek VALUES (1,1,3,'2017-10-09 10:10:10','2017-11-09 10:10:10');
INSERT INTO kolcsonzesek VALUES (2,4,12,'2017-10-09 10:09:10','2017-10-09 10:10:10');
INSERT INTO kolcsonzesek VALUES (3,6,5,'2017-11-09 10:10:10','2017-12-09 10:10:10');
INSERT INTO kolcsonzesek VALUES (4,10,7,'2017-10-09 10:10:10','2017-11-09 10:10:10');
INSERT INTO kolcsonzesek VALUES (5,5,13,'2017-10-09 10:10:10','2017-11-09 10:10:10');
INSERT INTO befizetesek VALUES (1,1,500,'2017-10-09 10:10:10');
INSERT INTO befizetesek VALUES (2,2,1000,'2017-11-09 10:10:10');
INSERT INTO befizetesek VALUES (3,3,750,'2017-09-09 10:10:10');
INSERT INTO befizetesek VALUES (4,4,500,'2017-12-09 10:10:10');
INSERT INTO olvasasok VALUES (1,1,'2017-12-09 10:11:11','2017-12-09 11:11:11');
INSERT INTO olvasasok VALUES (2,3,'2017-11-12 10:13:11','2017-11-12 11:13:11');
INSERT INTO olvasasok VALUES (3,5,'2017-10-22 10:14:11','2017-10-22 12:14:11');
INSERT INTO olvasasok VALUES (4,7,'2017-11-09 10:31:11','2017-11-09 13:31:11');
INSERT INTO olvasasok VALUES (5,9,'2017-12-11 10:21:11','2017-12-11 14:21:11');
GO