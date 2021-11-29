-- phpMyAdmin SQL Dump
-- version 5.0.4
-- https://www.phpmyadmin.net/
--
-- Host: 160.10.217.6:3306
-- Generation Time: Nov 29, 2021 at 05:50 PM
-- Server version: 8.0.22
-- PHP Version: 7.4.6

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `cs3230f21h`
--

DELIMITER $$
--
-- Procedures
--
$$

$$

$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `category`
--

CREATE TABLE `category` (
  `categoryName` varchar(20) NOT NULL,
  `categoryType` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `category`
--

INSERT INTO `category` (`categoryName`, `categoryType`) VALUES
('Bed', 'Bed'),
('Bookcase', 'Bookcase'),
('Chair', 'Chair'),
('Computer Desk', 'Desk'),
('Metal Table', 'Table'),
('Wood Table', 'Table');

-- --------------------------------------------------------

--
-- Table structure for table `customer`
--

CREATE TABLE `customer` (
  `cID` int NOT NULL,
  `firstName` varchar(50) NOT NULL,
  `lastName` varchar(50) NOT NULL,
  `gender` char(1) DEFAULT NULL,
  `dob` date DEFAULT NULL,
  `registrationDate` date NOT NULL,
  `phoneNumber` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `address1` varchar(100) NOT NULL,
  `address2` varchar(100) NOT NULL,
  `city` varchar(50) NOT NULL,
  `state` varchar(50) NOT NULL,
  `zipcode` varchar(5) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `customer`
--

INSERT INTO `customer` (`cID`, `firstName`, `lastName`, `gender`, `dob`, `registrationDate`, `phoneNumber`, `address1`, `address2`, `city`, `state`, `zipcode`) VALUES
(2, 'Cody', 'Vollrath', 'M', '1997-09-15', '2021-10-20', '4702239444', '35 Snow Road', '560 Lee Williams Road', 'Carrollton', 'Georgia', '30117'),
(6, 'Katie', 'Elliott', 'F', '2000-03-02', '2021-10-21', '4567891234', '35 Address Road', '', 'Huntsville', 'Alabama', '35749'),
(7, 'gooma', 'kib', 'F', '2020-10-13', '2021-10-21', '6666666666', '912 lovern road', '1111', 'hell', 'Hawaii', '66666'),
(23, 'Noah', 'Garret', 'M', '1999-09-05', '2021-11-02', '6789015532', '12 Not Fake Road', '13 Fake Road', 'Bremem', 'Guam', '30111'),
(24, 'Gabe', 'Newell', 'M', '1799-11-01', '2021-11-02', '4789991223', '123 New York St', '', 'New Braska', 'Nebraska', '69696'),
(25, 'Zac', 'Smith', 'M', '1999-10-12', '2021-11-02', '4046671723', '12 Sus Street', '', 'Pheniox', 'Alabama', '36870'),
(26, 'Cody', 'Vollrath', 'M', '2021-11-01', '2021-11-05', '1234567890', '21 Test Street', '123', 'Test', 'Arkansas', '30117');

-- --------------------------------------------------------

--
-- Table structure for table `employee`
--

CREATE TABLE `employee` (
  `eID` int NOT NULL,
  `username` varchar(20) NOT NULL,
  `password` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `firstName` varchar(50) NOT NULL,
  `lastName` varchar(50) NOT NULL,
  `admin` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `employee`
--

INSERT INTO `employee` (`eID`, `username`, `password`, `firstName`, `lastName`, `admin`) VALUES
(12345, 'test', 'YZCjH1ulCD6l0q9fFIdoww==', 'fname', 'lname', 1),
(41198, 'test2', 'YZCjH1ulCD6l0q9fFIdoww==', 'Test', 'User', 0),
(41199, 'test3', 'YZCjH1ulCD6l0q9fFIdoww==', 'Test', 'User', 0),
(912560, 'admin', '8dS21esUwg63NsfI9Vv2PQ==', 'Admin', 'Admin', 1),
(9123412, 'cvollrath97', 'AMJsG1+t7kKiHLEkfT9r7V6RUl68emZMYef+QRPpkOc=', 'Cody', 'Vollrath', 0),
(9124578, 'john_doe', 'YZCjH1ulCD6l0q9fFIdoww==', 'john', 'doe', 0);

-- --------------------------------------------------------

--
-- Table structure for table `furniture`
--

CREATE TABLE `furniture` (
  `fID` int NOT NULL,
  `itemName` varchar(30) NOT NULL,
  `itemDescription` varchar(50) NOT NULL,
  `styleName` varchar(20) NOT NULL,
  `categoryName` varchar(20) NOT NULL,
  `quantity` int NOT NULL,
  `daily_rental_rate` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `furniture`
--

INSERT INTO `furniture` (`fID`, `itemName`, `itemDescription`, `styleName`, `categoryName`, `quantity`, `daily_rental_rate`) VALUES
(2, 'Computer Desk', 'a simple desk', 'Pine', 'Computer Desk', 7, 10.99),
(3, 'Metal Table', 'a simple metal table', 'Stainless Steel', 'Metal Table', 1, 5),
(4, 'Birch Wood Table', 'a simple birch wood table', 'Birch', 'Wood Table', 1, 2),
(5, 'Pine Wood Table', 'a simple pine wood table', 'Pine', 'Wood Table', 0, 1),
(6, 'Catalina Creek King Panel Bed', 'A Catalina Creek King Panel Bed', 'Pine', 'Bed', 2, 7.99),
(7, 'Catalina Creek Queen Panel Bed', 'A Catalina Creek Queen Panel Bed', 'Pine', 'Bed', 4, 7.59),
(8, 'Belden King Scalloped Bed', 'A Belden King Scalloped Bed', 'Pine', 'Bed', 2, 7.99),
(9, 'Lawrence Twin Bed', 'Lawrence Twin Bed', 'Pine', 'Bed', 1, 5.99),
(10, 'Aquarius Desk with Pedestal', 'A Aquarius Desk with Single Pedestal', 'Birch', 'Computer Desk', 4, 2.99),
(11, 'Business Office Pro', 'A Business Office Pro Computer Desk', 'Birch', 'Computer Desk', 4, 2.99),
(12, 'Logan U-Shape Workstation', 'A Logan U-Shape Workstation', 'Ash', 'Computer Desk', 1, 6.98),
(13, 'Stakmore Folding Chair', 'A Stakmore Solid Wood Folding Chair', 'Pine', 'Chair', 10, 1.99),
(14, 'Alera SL Series Nesting Chair', 'A Alera SL Series Nesting Chair', 'Iron', 'Chair', 4, 1.98);

-- --------------------------------------------------------

--
-- Table structure for table `item_check_in`
--

CREATE TABLE `item_check_in` (
  `tID` int NOT NULL,
  `fID` int NOT NULL,
  `fQuantity` int NOT NULL,
  `returnTID` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `item_check_in`
--

INSERT INTO `item_check_in` (`tID`, `fID`, `fQuantity`, `returnTID`) VALUES
(63, 2, 1, 75),
(63, 2, 1, 77),
(69, 3, 1, 86),
(70, 2, 1, 87),
(71, 3, 1, 78),
(79, 2, 1, 80),
(81, 2, 1, 84),
(82, 4, 1, 84),
(83, 5, 1, 85),
(88, 2, 1, 89),
(88, 2, 1, 91),
(90, 2, 1, 91);

-- --------------------------------------------------------

--
-- Table structure for table `item_check_out`
--

CREATE TABLE `item_check_out` (
  `fID` int NOT NULL,
  `tID` int NOT NULL,
  `fQuantity` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `item_check_out`
--

INSERT INTO `item_check_out` (`fID`, `tID`, `fQuantity`) VALUES
(2, 62, 0),
(2, 63, 0),
(2, 70, 0),
(2, 79, 0),
(2, 81, 0),
(2, 88, 0),
(2, 90, 4),
(3, 69, 0),
(3, 71, 0),
(4, 62, 0),
(4, 63, 0),
(4, 82, 0),
(5, 83, 0),
(5, 90, 1);

-- --------------------------------------------------------

--
-- Table structure for table `rental`
--

CREATE TABLE `rental` (
  `tID` int NOT NULL,
  `estimatedCost` double NOT NULL,
  `estimatedFees` double NOT NULL,
  `rentalDate` date NOT NULL,
  `dueDate` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `rental`
--

INSERT INTO `rental` (`tID`, `estimatedCost`, `estimatedFees`, `rentalDate`, `dueDate`) VALUES
(62, 12.99, 1, '2021-11-05', '2021-11-15'),
(63, 34.97, 1, '2021-11-05', '2021-11-15'),
(67, 10.99, 1, '2021-11-05', '2021-11-15'),
(68, 2, 1, '2021-11-05', '2021-11-15'),
(69, 5, 1, '2021-11-05', '2021-11-15'),
(70, 10.99, 1, '2021-11-05', '2021-11-15'),
(71, 5, 1, '2021-11-05', '2021-11-15'),
(79, 10.99, 1, '2021-11-05', '2021-11-15'),
(81, 10.99, 1, '2021-11-19', '2021-11-23'),
(82, 10.8575879928495, 1, '2021-11-19', '2021-11-25'),
(83, 4.4272338338588, 1, '2021-11-19', '2021-11-24'),
(88, -291.583784483532, 1, '2021-11-19', '2021-11-19'),
(90, 35652.8592260324, 1, '2021-11-19', '2021-12-01');

-- --------------------------------------------------------

--
-- Table structure for table `return`
--

CREATE TABLE `return` (
  `tID` int NOT NULL,
  `returnDate` date NOT NULL,
  `lateFees` decimal(10,0) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `return`
--

INSERT INTO `return` (`tID`, `returnDate`, `lateFees`) VALUES
(78, '2021-11-19', '5'),
(80, '2021-11-19', '11'),
(84, '2021-11-19', '13'),
(87, '2021-11-19', '0'),
(89, '2021-11-19', '0'),
(91, '2021-11-19', '0');

-- --------------------------------------------------------

--
-- Table structure for table `style`
--

CREATE TABLE `style` (
  `styleName` varchar(20) NOT NULL,
  `styleType` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `style`
--

INSERT INTO `style` (`styleName`, `styleType`) VALUES
('Aluminium', 'Metal'),
('Ash', 'Wood'),
('Birch', 'Wood'),
('Iron', 'Metal'),
('Pine', 'Wood'),
('Stainless Steel', 'Metal');

-- --------------------------------------------------------

--
-- Table structure for table `transaction`
--

CREATE TABLE `transaction` (
  `tID` int NOT NULL,
  `eID` int NOT NULL,
  `cID` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `transaction`
--

INSERT INTO `transaction` (`tID`, `eID`, `cID`) VALUES
(62, 912560, 2),
(63, 912560, 2),
(64, 912560, 2),
(65, 912560, 2),
(66, 912560, 2),
(67, 912560, 2),
(68, 912560, 6),
(69, 912560, 25),
(70, 912560, 23),
(71, 912560, 2),
(72, 912560, 2),
(73, 912560, 2),
(74, 912560, 2),
(75, 912560, 2),
(76, 912560, 2),
(77, 912560, 2),
(78, 912560, 2),
(79, 912560, 2),
(80, 912560, 2),
(81, 912560, 2),
(82, 912560, 2),
(83, 912560, 2),
(84, 912560, 2),
(85, 912560, 2),
(86, 912560, 25),
(87, 912560, 23),
(88, 912560, 2),
(89, 912560, 2),
(90, 9123412, 2),
(91, 9123412, 2);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `category`
--
ALTER TABLE `category`
  ADD PRIMARY KEY (`categoryName`);

--
-- Indexes for table `customer`
--
ALTER TABLE `customer`
  ADD PRIMARY KEY (`cID`);

--
-- Indexes for table `employee`
--
ALTER TABLE `employee`
  ADD PRIMARY KEY (`eID`);

--
-- Indexes for table `furniture`
--
ALTER TABLE `furniture`
  ADD PRIMARY KEY (`fID`),
  ADD KEY `styleName` (`styleName`),
  ADD KEY `categoryName` (`categoryName`);

--
-- Indexes for table `item_check_in`
--
ALTER TABLE `item_check_in`
  ADD PRIMARY KEY (`tID`,`fID`,`returnTID`),
  ADD KEY `fID` (`fID`),
  ADD KEY `returnTID` (`returnTID`);

--
-- Indexes for table `item_check_out`
--
ALTER TABLE `item_check_out`
  ADD PRIMARY KEY (`fID`,`tID`),
  ADD KEY `tID` (`tID`);

--
-- Indexes for table `rental`
--
ALTER TABLE `rental`
  ADD PRIMARY KEY (`tID`);

--
-- Indexes for table `return`
--
ALTER TABLE `return`
  ADD PRIMARY KEY (`tID`);

--
-- Indexes for table `style`
--
ALTER TABLE `style`
  ADD PRIMARY KEY (`styleName`);

--
-- Indexes for table `transaction`
--
ALTER TABLE `transaction`
  ADD PRIMARY KEY (`tID`),
  ADD KEY `FK_customer_id` (`cID`),
  ADD KEY `Fk_employee_id` (`eID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `customer`
--
ALTER TABLE `customer`
  MODIFY `cID` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=27;

--
-- AUTO_INCREMENT for table `furniture`
--
ALTER TABLE `furniture`
  MODIFY `fID` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT for table `transaction`
--
ALTER TABLE `transaction`
  MODIFY `tID` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=92;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `furniture`
--
ALTER TABLE `furniture`
  ADD CONSTRAINT `furniture_ibfk_1` FOREIGN KEY (`styleName`) REFERENCES `style` (`styleName`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `furniture_ibfk_2` FOREIGN KEY (`categoryName`) REFERENCES `category` (`categoryName`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `item_check_in`
--
ALTER TABLE `item_check_in`
  ADD CONSTRAINT `item_check_in_ibfk_1` FOREIGN KEY (`tID`) REFERENCES `transaction` (`tID`),
  ADD CONSTRAINT `item_check_in_ibfk_2` FOREIGN KEY (`fID`) REFERENCES `furniture` (`fID`),
  ADD CONSTRAINT `item_check_in_ibfk_3` FOREIGN KEY (`returnTID`) REFERENCES `transaction` (`tID`);

--
-- Constraints for table `item_check_out`
--
ALTER TABLE `item_check_out`
  ADD CONSTRAINT `item_check_out_ibfk_1` FOREIGN KEY (`fID`) REFERENCES `furniture` (`fID`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `item_check_out_ibfk_2` FOREIGN KEY (`tID`) REFERENCES `transaction` (`tID`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `rental`
--
ALTER TABLE `rental`
  ADD CONSTRAINT `rental_ibfk_1` FOREIGN KEY (`tID`) REFERENCES `transaction` (`tID`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `return`
--
ALTER TABLE `return`
  ADD CONSTRAINT `return_ibfk_1` FOREIGN KEY (`tID`) REFERENCES `transaction` (`tID`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `transaction`
--
ALTER TABLE `transaction`
  ADD CONSTRAINT `FK_customer_id` FOREIGN KEY (`cID`) REFERENCES `customer` (`cID`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `Fk_employee_id` FOREIGN KEY (`eID`) REFERENCES `employee` (`eID`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
