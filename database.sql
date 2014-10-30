-- phpMyAdmin SQL Dump
-- http://www.phpmyadmin.net

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";

--
-- Database: `fbladesk`
--
CREATE DATABASE `fbladesk` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `fbladesk`;

-- --------------------------------------------------------

--
-- Table structure for table `participants`
--

CREATE TABLE IF NOT EXISTS `participants` (
  `uuid` char(36) NOT NULL,
  `confcode` char(3) NOT NULL,
  `type` tinyint(1) NOT NULL,
  `fname` varchar(255) NOT NULL,
  `lname` varchar(255) NOT NULL,
  `chapnum` smallint(3) unsigned NOT NULL,
  PRIMARY KEY (`uuid`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `participants`
--

INSERT INTO `participants` (`uuid`, `confcode`, `type`, `fname`, `lname`, `chapnum`) VALUES
('ec1e919a-6dce-4e93-9f2b-242f1f56491e', '', 1, 'Admin', 'User', 0);

-- --------------------------------------------------------

--
-- Table structure for table `registrations`
--

CREATE TABLE IF NOT EXISTS `registrations` (
  `partid` char(36) NOT NULL,
  `wkshpid` char(36) NOT NULL,
  UNIQUE KEY `partid_2` (`partid`,`wkshpid`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE IF NOT EXISTS `users` (
  `partid` char(36) NOT NULL,
  `username` varchar(255) NOT NULL,
  `passwd` varchar(255) NOT NULL,
  `salt` char(25) NOT NULL,
  UNIQUE KEY `partid` (`partid`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`partid`, `username`, `passwd`, `salt`) VALUES
('ec1e919a-6dce-4e93-9f2b-242f1f56491e', 'admin', 'wcWjeR4QRrWd7vQ9Oa8EFyDUczxkctPurjalu4Y+aRSa5WEzue41sclS7C9tw3+myW0F8T6u4JHvKPLoVXjKZg==', 'gltc5CgvAjXAVX4zLtXKVgmnh');

-- --------------------------------------------------------

--
-- Table structure for table `workshops`
--

CREATE TABLE IF NOT EXISTS `workshops` (
  `uuid` char(36) NOT NULL,
  `confcode` char(3) NOT NULL,
  `wname` varchar(255) NOT NULL,
  `wdesc` text NOT NULL,
  `wdate` datetime NOT NULL,
  PRIMARY KEY (`uuid`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Constraints for table `users`
--
ALTER TABLE `users`
  ADD CONSTRAINT `users_ibfk_1` FOREIGN KEY (`partid`) REFERENCES `participants` (`uuid`);
