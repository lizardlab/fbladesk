# FBLA Desktop Application

This program was created in partial compliance with the 2013 Future Business Leader's of America Desktop Application [specifications](http://www.fbla.org/data/images/2013-14%20guidelines.pdf). In short, the program helps register participants for the 2013 FBLA Fall National Leadership Conference. It deviates from the specifications by using a MySQL database instead of a flat file, and also the schema is different. This project was originally made for an quarter project in Sarasota County Institute and has been re-prepared for public release. The program was developed in C# with Visual Studio 2010. It makes use of 2 libraries which are iTextSharp and the MySQL C# connector. The DLL's are included in the build folders.

This program cannot be run out of the box because it needs a MySQL server to retrieve the records. A basic database is included under the filename *database.sql* and it comes with one uber-admin user.

This program was made in the timespan of 5 weeks, and had an emphasis on security (passwords are hashed and salted). The program is free from most large bugs, but if any have lasted until now, they won't be fixed since this project is finished. If you wish to fix any bug and make a pull reuest, I'll happily accept it as long as the rest of the code is left (mostly) the same.

*Finalized: 2014-05-29*

## Building

The project is preconfigured to work and a binary is already included, see below on how to run.

## Running

1. For quick start up create a user with the name of **fbladesk** and a database with all privileges under the same name. Set the user password to be **futurebusinessleader**. The database must be located on localhost.
2. Import the *database.sql* file into the database.
3. Run the FBLA Desktop Application.
4. Login with the following credentials. **Username:** admin, **Password:** password.
5. Now add new workshops/users and interact to your heart's content.

## Editing the database connection

In the file *DBConnect.cs* the parameters for the connection string are defined. Simply scroll down and then find the connector line and edit the respective variables to your configuration.

## License

This program is released under [GPLv3](https://www.gnu.org/licenses/gpl.html).
