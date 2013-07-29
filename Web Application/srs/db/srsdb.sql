-- phpMyAdmin SQL Dump
-- version 3.5.2
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Sep 12, 2012 at 01:14 AM
-- Server version: 5.5.27
-- PHP Version: 5.3.16

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `177344`
--

-- --------------------------------------------------------

--
-- Table structure for table `aisles`
--

DROP TABLE IF EXISTS `aisles`;
CREATE TABLE IF NOT EXISTS `aisles` (
  `aisle_id` int(11) NOT NULL AUTO_INCREMENT,
  `aisle_description` varchar(255) DEFAULT NULL,
  `aisle_number` varchar(32) DEFAULT NULL,
  `aisle_status` int(11) DEFAULT '1',
  PRIMARY KEY (`aisle_id`),
  UNIQUE KEY `aisle_number_UNIQUE` (`aisle_number`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=6 ;

--
-- Dumping data for table `aisles`
--

INSERT INTO `aisles` (`aisle_id`, `aisle_description`, `aisle_number`, `aisle_status`) VALUES
(1, 'Milk & Dairy', '1', 1),
(2, 'Drinks and Snacks', '2', 1),
(3, 'Cleaning Products', '3', 1),
(4, 'Health & Beauty', '4', 1),
(5, 'Baby Items', '5', 1);

-- --------------------------------------------------------

--
-- Table structure for table `picklist`
--

DROP TABLE IF EXISTS `picklist`;
CREATE TABLE IF NOT EXISTS `picklist` (
  `picklist_id` int(11) NOT NULL AUTO_INCREMENT,
  `picklist_datetime` date DEFAULT NULL,
  `username` varchar(64) DEFAULT NULL,
  `stocktake_id` int(11) NOT NULL,
  PRIMARY KEY (`picklist_id`),
  KEY `fk_PickList_StockTake1_idx` (`stocktake_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `picklistdetails`
--

DROP TABLE IF EXISTS `picklistdetails`;
CREATE TABLE IF NOT EXISTS `picklistdetails` (
  `picklist_details_id` int(11) NOT NULL AUTO_INCREMENT,
  `picklist_id` int(11) DEFAULT NULL,
  `products_id` int(11) DEFAULT NULL,
  `required_qty` int(11) DEFAULT NULL,
  PRIMARY KEY (`picklist_details_id`),
  KEY `fk_products_id` (`products_id`),
  KEY `fk_picklist_id` (`picklist_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `products`
--

DROP TABLE IF EXISTS `products`;
CREATE TABLE IF NOT EXISTS `products` (
  `products_id` int(11) NOT NULL AUTO_INCREMENT,
  `barcode` varchar(64) DEFAULT NULL,
  `products_name` varchar(128) DEFAULT NULL,
  `products_description` varchar(255) DEFAULT NULL,
  `manufacturer` varchar(64) DEFAULT NULL,
  `sku` varchar(64) DEFAULT NULL,
  PRIMARY KEY (`products_id`),
  UNIQUE KEY `sku_UNIQUE` (`sku`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=16 ;

--
-- Dumping data for table `products`
--

INSERT INTO `products` (`products_id`, `barcode`, `products_name`, `products_description`, `manufacturer`, `sku`) VALUES
(1, '500001', 'Milk, whole 1L', 'Milk, whole 1L', 'NA', '500001'),
(2, '500002', 'Milk, skim 1L', 'Milk, skim 1L', 'NA', '500002'),
(3, '500003', 'Chocalate Milk 500ml', 'Chocalate Milk 500ml', 'Mars', '500003'),
(4, '500004', 'Soy Milk 500ml', 'Soy Milk 500ml', 'NA', '500004'),
(5, '500005', 'Eggs Pack of 6', 'Eggs Pack of 6', 'Free Rangers', '500005'),
(6, '600001', 'Baby wipes', 'Baby wipes', NULL, '600001'),
(7, '600002', 'Baby Diapers', 'Baby Diapers', NULL, '600002'),
(8, '95200', 'Air freshener', 'Air freshener', 'AirWick', '95200'),
(9, NULL, 'Glass''s cleaner', 'Glass''s cleaner', NULL, NULL),
(10, NULL, 'Deodorant', 'Deodorant', NULL, NULL),
(11, NULL, 'Shaving cream', 'Shaving cream', NULL, NULL),
(12, NULL, 'Mineral water', 'Mineral water', NULL, NULL),
(13, NULL, 'Sport drink', 'Sport drink', NULL, NULL),
(14, NULL, 'Club soda', 'Club soda', NULL, NULL),
(15, NULL, 'Cocal Cola 2L', 'Cocal Cola 2L', NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `productsinventory`
--

DROP TABLE IF EXISTS `productsinventory`;
CREATE TABLE IF NOT EXISTS `productsinventory` (
  `products_inventory_id` int(11) NOT NULL AUTO_INCREMENT,
  `products_id` int(11) NOT NULL,
  `products_quantity` int(11) DEFAULT NULL,
  `min_order_quantity` int(11) DEFAULT NULL,
  `max_shelf_quantity` int(11) DEFAULT NULL,
  `min_shelf_quantity` int(11) DEFAULT NULL,
  `aisle_id` int(11) DEFAULT NULL,
  `warehouse_location_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`products_inventory_id`),
  KEY `fk_products_id` (`products_id`),
  KEY `fk_aisle_number` (`aisle_id`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=16 ;

--
-- Dumping data for table `productsinventory`
--

INSERT INTO `productsinventory` (`products_inventory_id`, `products_id`, `products_quantity`, `min_order_quantity`, `max_shelf_quantity`, `min_shelf_quantity`, `aisle_id`, `warehouse_location_id`) VALUES
(1, 1, 20, 1, 5, 2, 1, 11),
(2, 2, 5, 1, 4, 2, 1, 11),
(3, 10, 5, 1, 3, 1, 4, 100),
(4, 11, 5, 1, 3, 0, 4, 100),
(5, 12, 50, 10, 20, 15, 2, 22),
(6, 13, 60, 20, 30, 10, 2, 22),
(7, 8, 50, 5, 10, 4, 3, 33),
(8, 6, 40, 5, 15, 10, 5, 55),
(9, 7, 90, 20, 20, 15, 5, 55),
(10, 15, 88, 12, 24, 5, 2, 22),
(11, 9, 16, 3, 6, 4, 3, 33),
(12, 14, 32, 2, 5, 2, 2, 22),
(13, 3, 16, 0, 4, 1, 1, 45),
(14, 4, 4, 0, 2, 0, 1, 85),
(15, 5, 35, 5, 8, 3, 1, 65);

-- --------------------------------------------------------

--
-- Table structure for table `rfidtagdata`
--

DROP TABLE IF EXISTS `rfidtagdata`;
CREATE TABLE IF NOT EXISTS `rfidtagdata` (
  `rfid_tag_id` varchar(128) NOT NULL,
  `products_id` int(11) NOT NULL,
  PRIMARY KEY (`rfid_tag_id`),
  KEY `fk_RFIDTagData_Products1_idx` (`products_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `rfidtagdata`
--

INSERT INTO `rfidtagdata` (`rfid_tag_id`, `products_id`) VALUES
('000000000000000000E50093', 1),
('0000000000000000C0001363', 1),
('00000000000000000A101780', 10),
('0000000008002216572D0501 ', 11),
('000000000000000000000239 ', 12),
('000000000000000000A50004 ', 13),
('00000000000000000A101698 ', 12),
('00000000000000000A101697', 14),
('00000000000000000A101699', 3),
('00000000000000000A103931', 4),
('00000000000000000A104963', 5),
('00000000000000000A103934 ', 6),
('00000000000000000A103942', 7),
('00000000000000000A103946', 8),
('00000000000000000A103939', 9);

-- --------------------------------------------------------

--
-- Table structure for table `stocktake`
--

DROP TABLE IF EXISTS `stocktake`;
CREATE TABLE IF NOT EXISTS `stocktake` (
  `stocktake_id` int(11) NOT NULL AUTO_INCREMENT,
  `mobile_stocktake_id` int(11) NOT NULL,
  `start_datetime` datetime DEFAULT NULL,
  `end_datetime` datetime DEFAULT NULL,
  `username` varchar(64) DEFAULT NULL,
  PRIMARY KEY (`stocktake_id`),
  KEY `fk_username` (`username`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `stocktakedetails`
--

DROP TABLE IF EXISTS `stocktakedetails`;
CREATE TABLE IF NOT EXISTS `stocktakedetails` (
  `stocktake_details_id` int(11) NOT NULL AUTO_INCREMENT,
  `stocktake_id` int(11) DEFAULT NULL,
  `rfid_tag_id` varchar(128) DEFAULT NULL,
  `aisle_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`stocktake_details_id`),
  KEY `fk_stocktakedetails_aisles1_idx` (`aisle_id`),
  KEY `fk_stocktakedetails_stocktake1_idx` (`stocktake_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `userroles`
--

DROP TABLE IF EXISTS `userroles`;
CREATE TABLE IF NOT EXISTS `userroles` (
  `user_role_id` int(11) NOT NULL AUTO_INCREMENT,
  `role_name` varchar(64) DEFAULT NULL,
  `mobile_user` int(11) DEFAULT NULL,
  PRIMARY KEY (`user_role_id`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=6 ;

--
-- Dumping data for table `userroles`
--

INSERT INTO `userroles` (`user_role_id`, `role_name`, `mobile_user`) VALUES
(1, 'Web Admin', 0),
(2, 'Mobile User', 1),
(3, 'System Admin', 1),
(4, 'Web User', 0),
(5, 'Mobile Admin', 1);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
CREATE TABLE IF NOT EXISTS `users` (
  `user_id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(64) DEFAULT NULL,
  `password` varchar(64) DEFAULT NULL,
  `user_role` int(11) DEFAULT NULL,
  PRIMARY KEY (`user_id`),
  UNIQUE KEY `username_UNIQUE` (`username`),
  KEY `fk_Users_UserRoles1_idx` (`user_role`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=6 ;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`user_id`, `username`, `password`, `user_role`) VALUES
(1, 'admin', 'password', 3),
(2, 'webadmin', '123', 1),
(3, 'mobileadmin', '123', 5),
(4, 'mobileuser', '123', 2),
(5, 'webuser', '123', 4);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
