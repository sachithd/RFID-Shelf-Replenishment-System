<?php

class Connection
{
	private $DB_NAME = 'srs_maindb';
	private $HOSTNAME ='localhost';
	private $USERNAME = 'root';
	private $PASSWORD = '';
	private $LINK;
	private $DB;
	
	function __construct()
	{
		$this->LINK = mysql_connect($this->HOSTNAME, $this->USERNAME, $this->PASSWORD) or die(mysql_error());
		$this->DB = mysql_select_db($this->DB_NAME, $this->LINK) or die(mysql_error());
	}		
	
	function runQuery($query)
	{
		return mysql_query($query,$this->LINK);	
	}
	
	function runQueryReturnid($query)
	{
		mysql_query($query,$this->LINK);	
		return mysql_insert_id();
	}
	
	function __destruct()
	{
		mysql_close($this->LINK);
	}
}

?>