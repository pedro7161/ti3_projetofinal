CREATE TABLE  animal (
 idanimal NCHAR(50)PRIMARY key
);
CREATE TABLE  Race (
 idrace NCHAR(50)PRIMARY key
);
CREATE TABLE  Food (
 idfood INT IDENTITY PRIMARY KEY,
 idanimal NCHAR(50) FOREIGN KEY REFERENCES animal(idanimal),
 idrace NCHAR(50) FOREIGN KEY REFERENCES Race(idrace) ,
 nome NVARCHAR(120),
 data_validade DATETIME,
 time_remain AS DATEDIFF(HOUR,CURRENT_TIMESTAMP,data_validade),
 foto NVARCHAR(MAX)
);
