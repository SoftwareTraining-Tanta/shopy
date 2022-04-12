drop database if exists shopy;
create database shopy;
use shopy;

create table vendors
(
    username         varchar(30),
    name             varchar(30)  not null,
    country          varchar(30),
    city             varchar(30),
    phone            varchar(20)  not null unique,
    email            varchar(60)  not null unique,
    password         varchar(100) not null,
    verificationCode char(6)      not null,
    isVerified       TinyInt(1) default 0,
    primary key (username)
);

create table clients
(
    username         varchar(30),
    name             varchar(30)  not null,
    country          varchar(30),
    city             varchar(30),
    phone            varchar(20)  not null unique,
    email            varchar(60)  not null unique,
    password         varchar(100) not null,
    verificationCode char(6)      not null,
    isVerified       TinyInt(1) default 0,
    primary key (username)
);

create table carts
(
    id             int auto_increment,
    clientUsername varchar(30) not null,
    country        varchar(30),
    city           varchar(30),
    phone          varchar(20) not null,
    email          varchar(60) not null,
    primary key (id),
    foreign key (clientUsername) references clients (username) on delete cascade
);

create table models
(
    name       varchar(300) primary key,
    imagePath  varchar(512)  not null,
    vendorUsername varchar(30)   not null, -- added
    price      numeric(9, 2) not null,
    salePrice  decimal       default null,
    brand      varchar(100),
    color      varchar(50),
    features   varchar(600),
    rate       numeric(2, 1) default 0 check (rate between 0.0 and 5.0),
    foreign key (vendorUsername) REFERENCES vendors (username)
);

create table products
(
    id             int auto_increment,
    -- vendorUsername varchar(30)  not null,  delete it
    category       varchar(20)  not null check (category in
                                                ('RAM', 'SSD', 'K&M', 'Mouse')),
    model          varchar(300) not null,
    clientUsername varchar(30),
    cartId         int,
    rate           numeric(2, 1) default 0 check (rate between 0.0 and 5.0),
    primary key (id),
    foreign key (clientUsername) references clients (username),
    foreign key (cartId) references carts (id),
    foreign key (model) references models (name)
);
