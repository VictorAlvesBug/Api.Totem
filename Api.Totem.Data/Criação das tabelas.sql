## DDLs PARA CRIAÇÃO DA ESTRUTURA DO BANCO
/*
CREATE DATABASE totem;

CREATE TABLE `product` (
	`id` VARCHAR(36) NOT NULL PRIMARY KEY,
    `name` VARCHAR(100) NOT NULL UNIQUE,
    `description` VARCHAR(500) NULL,
    `price` DECIMAL(10, 2) NOT NULL,
    `available` BOOL NOT NULL
);

CREATE TABLE `category` (
	`id` VARCHAR(36) NOT NULL PRIMARY KEY,
    `category_type` TINYINT UNSIGNED NOT NULL,
    `name` VARCHAR(100) NOT NULL UNIQUE,
    `complement_type` TINYINT UNSIGNED NOT NULL,
    `combo_additional_price` DECIMAL(10, 2) NULL
);

CREATE TABLE `side_dish_set` (
	`id` VARCHAR(36) NOT NULL PRIMARY KEY,
    `amount` TINYINT UNSIGNED NOT NULL,
    `category_id` VARCHAR(36) NOT NULL,
    FOREIGN KEY (`category_id`) REFERENCES `category`(`id`)
);

CREATE TABLE `pager` (
	`id` INT UNSIGNED NOT NULL PRIMARY KEY,
    `status` TINYINT UNSIGNED NOT NULL,
    `created_date` TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE `order` (
	`id` VARCHAR(36) NOT NULL PRIMARY KEY,
    `delivery_type` TINYINT UNSIGNED NULL,
    `total_price` DECIMAL(10, 2) NOT NULL,
    `payment_method` TINYINT UNSIGNED NULL,
    `comment` VARCHAR(500) NULL,
    `status` TINYINT UNSIGNED NOT NULL,
    `pager_id` INT UNSIGNED NULL,
    `created_date` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    `ordered_date` TIMESTAMP NULL,
    FOREIGN KEY (`pager_id`) REFERENCES `pager`(`id`)
);

CREATE TABLE `order_item` (
	`id` VARCHAR(36) NOT NULL PRIMARY KEY,
    `category_id` VARCHAR(36) NOT NULL,
    `main_product_id` VARCHAR(36) NOT NULL,
    `price` DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (`category_id`) REFERENCES `category`(`id`),
    FOREIGN KEY (`main_product_id`) REFERENCES `product`(`id`)
);

CREATE TABLE `order_item_side_product` (
	`id` VARCHAR(36) NOT NULL PRIMARY KEY,
    `order_item_id` VARCHAR(36) NOT NULL,
    `side_product_id` VARCHAR(36) NOT NULL,
    FOREIGN KEY (`order_item_id`) REFERENCES `order_item`(`id`),
    FOREIGN KEY (`side_product_id`) REFERENCES `product`(`id`)
);

CREATE TABLE `category_product` (
	`id` VARCHAR(36) NOT NULL PRIMARY KEY,
    `product_id` VARCHAR(36) NOT NULL,
    `category_id` VARCHAR(36) NOT NULL,
    FOREIGN KEY (`product_id`) REFERENCES `product`(`id`),
    FOREIGN KEY (`category_id`) REFERENCES `category`(`id`)
);

CREATE TABLE `category_side_dish_set` (
	`id` VARCHAR(36) NOT NULL PRIMARY KEY,
    `category_id` VARCHAR(36) NOT NULL,
    `side_dish_set_id` VARCHAR(36) NOT NULL,
    FOREIGN KEY (`category_id`) REFERENCES `category`(`id`),
    FOREIGN KEY (`side_dish_set_id`) REFERENCES `side_dish_set`(`id`)
);*/


SELECT * FROM `product`;
SELECT * FROM `category`;
SELECT * FROM `side_dish_set`;
SELECT * FROM `pager`;
SELECT * FROM `order`;
SELECT * FROM `order_item`;
SELECT * FROM `order_item_side_product`;
SELECT * FROM `category_product`;
SELECT * FROM `category_side_dish_set`;
