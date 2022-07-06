# SQL_interpreter_

## Program for creating/updating/deleting .dbf files with SQL language

---

### Supported Field types

C	Character string	String

D	Date	DateTime

L	Logical	Bool

M	Memo	String

N	Numeric	Double

T	DateTime	DateTime

---

### Commands (with examples)

CREATE TABLE tableName (char C(20), memo M, date D, logical L, numeric N(20,2))

UPDATE tableName SET column=5 WHERE column=3 AND/OR column<3

DELETE column FROM tableName WHERE column=privet

INSERT INTO tableName (char C, date D) VALUES (Ivan,19)

DROP TABLE tableName

EXIT

SELECT column1,column2 FROM tableName WHERE ...

/?

Полезные ссылки:
http://rema44.ru/resurs/study/other/dbf/dbf.html
