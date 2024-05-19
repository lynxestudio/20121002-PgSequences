
CREATE TABLE Publishers
(
  publisherid serial primary key NOT NULL,
  publisher varchar(512) NOT NULL,
  created timestamp default current_timestamp
);