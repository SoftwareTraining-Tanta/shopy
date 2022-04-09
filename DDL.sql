drop database if exists shopy;
create database shopy;
use shopy;

create table vendors
(
    id       int auto_increment,
    name     varchar(30)  not null,
    country  varchar(30),
    city     varchar(30),
    phone    varchar(20)  not null unique,
    email    varchar(60)  not null unique,
    password varchar(100) not null,
    primary key (id)
);

create table clients
(
    id       int auto_increment,
    name     varchar(30)  not null,
    country  varchar(30),
    city     varchar(30),
    phone    varchar(20)  not null unique,
    email    varchar(60)  not null unique,
    password varchar(100) not null,
    primary key (id)
);

create table carts
(
    id       int auto_increment,
    clientId int         not null,
    country  varchar(30),
    city     varchar(30),
    phone    varchar(20) not null,
    email    varchar(60) not null,
    primary key (id),
    foreign key (clientId) references clients (id) on delete cascade
);

create table models
(
    name      varchar(300) primary key,
    imagePath varchar(512)  not null,
    price     numeric(9, 2) not null,
    salePrice decimal default null,
    brand     varchar(100),
    color     varchar(50),
    features  varchar(600)
);

create table products
(
    id       int auto_increment,
    vendorId int          not null,
    category varchar(20)  not null check (category in
                                          ('RAM', 'SSD', 'K&M', 'Mouse')),
    model    varchar(300) not null,
    clientId int,
    cartId   int,
    rate     numeric(2, 1) default 0 check (rate between 0.0 and 5.0),
    primary key (id),
    foreign key (vendorId) references vendors (id),
    foreign key (clientId) references clients (id),
    foreign key (cartId) references carts (id),
    foreign key (model) references models (name)
);
