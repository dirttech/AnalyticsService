CREATE TABLE `event_log` (
  `ID` char(38) NOT NULL,
  `event_id` varchar(45) NOT NULL,
  `dated` datetime NOT NULL,
  `user_id` varchar(45) NOT NULL,
  PRIMARY KEY (`event_id`,`dated`,`user_id`),
  UNIQUE KEY `ID_UNIQUE` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8