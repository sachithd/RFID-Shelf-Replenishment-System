************************************************
        RFID Shelf Replenishment System
          http://srsrfid.eu5.org/srs/
*************************************************

By Sachith Dassanayake

To view a web demo : http://srsrfid.eu5.org/srs/

------------
Introduction

------------

The RFID shelf replenishment system enables the user to capture data using an RFID handheld device and upload the data onto a web server to generate reports to assist with the shelf replenishment process

--------------------------
List of files and folders 
-------------------------

1. Mobile Application - Includes the source code for the handheld application created in Visual Studio 2008
2. Mobile Application Cab Installer - Includes the source code for the handheld application and the .CAB installation project source code
3. SDKs - Required software development kits and frameworks
4. Web Application - Source code for the web application
5. PROJ_DassanayakeS.docx - Project Report created in MS Word 2010
6. PROJ_DassanayakeS.pdf - Project Report in PDF
7. README.txt - The text file contains more information about the project and installation



--------------------------------------
Requirements for Handheld Application
--------------------------------------

The application was developed for Motorola MC9090G-RFID reader and so the application is only compatible with that model.

_______Hardware_________
Wireless LAN 802.11 b/g
RFID Reader support EPC Gen 2

______Software________
Windows Mobile 6.1

*** If not already installed on the device you will need the following components to be able to run the application
.NET Compact Framework 3.5 Redistributable
http://www.microsoft.com/en-us/download/details.aspx?id=65

SQL Server Compact 3.5 on a Device
http://msdn.microsoft.com/en-us/library/13kw2t64%28v=vs.90%29.aspx



--------------------------------------
Requirements for Web Application
--------------------------------------

A server which supports PHP (Eg Apache)
Tested on PHP Version 5.3.5
MySQL database V5 or later


--------------------------------------
How to use the application
--------------------------------------

Please refer the user manual for more information

--------------------------------------
Installation of the web application
--------------------------------------

1. Upload the "srs" folder on to the web server (Can be found in Web Application folder)
2. Create the MySQL database by importing the srsdb.sql file provided in srs\db folder
3. Delete the "db" folder if the database is created sucessfully
4. Edit the database configuration file "connection.class.php" in srs\ws\classes
	private $DB_NAME = 'databasename';
	private $HOSTNAME ='hostname';
	private $USERNAME = 'username';
	private $PASSWORD = 'password';

5. Edit the file crud\application\config\database.php to match the database settings
	$db['default']['hostname'] = 'hostname';

	$db['default']['username'] = 'username';

	$db['default']['password'] = 'password';

	$db['default']['database'] = 'databasename';

6. The web application is now installed successfully
	type http://yourhostname.com/srs/ to access the web application


--------------------------------------
Installation of the mobile application
--------------------------------------

Method 1 : using a .cab installation file

1. Copy the installation files on to the handheld device through Active Sync/Windows Mobile Device Center or use an SD card  (\Mobile Application Cab Installer\SRSInstaller\SRSInstaller\Release)
2. Execute the SRSInstaller.cab file
3. The application will be installed and a short cut will be created in the programs
4. Connect the handheld device to the PC via Active Sync (Windows Xp) or Windows Mobile Device Center (Windows 7) and browse the application directory
5. Copy the App.config on to the local hard drive
5. Edit the App.config file to change the web service url 
	The format would be : http://yourhost.com/srs/ws/srswebservice.php
6. You can change the web service time out settings if you wish
7. Save the file and upload on to the same directory
8. Ensure the device is connected to the internet and the application is ready to use

Method 2: Copy the application folder on to the handheld device (Can be found in Mobile Application folder \Srs.Mobile\Srs.Mobile\bin\Release)

1. Edit the App.config file as above and save
2. Copy the Release folder on to the handheld device via Active Sync/Windows Mobile Device Center or use an SD card 
3. Browse the folder through File Explorer utility on the device and execute Srs.Mobile.exe



--------------------------------------
Additional Information
--------------------------------------

1. To generate a pick list automatically, create a scheduled task to execute the following file as required

http://yourhost.com/srs/cron/cron.php


A demo web application can be accessed on : http://srsrfid.eu5.org/srs/
username: admin
password: password