create database shopy;
use shopy;

create table vendors
(
    id       int auto_increment,
    name     varchar(30)  not null,
    country  varchar(30),
    city     varchar(30),
    phone    varchar(20)  not null,
    email    varchar(60)  not null,
    password varchar(100) not null,
    primary key (id)
);

create table clients
(
    id       int auto_increment,
    name     varchar(30)  not null,
    country  varchar(30),
    city     varchar(30),
    phone    varchar(20)  not null,
    email    varchar(60)  not null,
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
    foreign key (clientId) references clients (id)
);

create table products
(
    id        int auto_increment,
    vendorId  int           not null,
    clientId  int           not null,
    category  varchar(20)   not null check (category in
                                            ('Ram', 'Monitor', 'SSD', 'HDD', 'K&M', 'Laptop', 'FlashDrive')),
    model     varchar(30),
    price     numeric(7, 2) not null,
    details   varchar(512),
    imagePath varchar(512),
    cartId    int,
    primary key (id),
    foreign key (vendorId) references vendors (id),
    foreign key (clientId) references clients (id),
    foreign key (cartId) references carts (id)
);

