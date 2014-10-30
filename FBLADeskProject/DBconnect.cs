/* DBConnect.cs
 * Acts as the connecting class with the MySQL database. Handles 
 * all the necessary functions for interaction with the data elements
 * stored within the database, and also user authentication.
 * Programmer: Logan Lopez
 */
using System;
using System.Windows.Forms;
using System.Text;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace FBLADeskProject
{

    class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        // Constructor
        public DBConnect()
        {
            Initialize();
        }

        // Initialize values
        private void Initialize()
        {
            // credentials
            server = "localhost";
            database = "fbladesk";
            uid = "fbladesk";
            password = "futurebusinessleader";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        // open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                // When handling errors, you can your application's response based 
                // on the error number.
                // The two most common error numbers when connecting are as follows:
                // 0: Cannot connect to server.
                // 1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Database Error", "Cannot connect to database. Make sure to be within SAUL domain", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case 1045:
                        MessageBox.Show("Database Error", "Invalid username/password, please try again", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }
                return false;
            }
        }

        // Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        // get the code for the conference they're attending
        public string GetConf(string uuid)
        {
            string query = "SELECT confcode FROM participants WHERE uuid=@uuid";
            string conf = "";
            // Open connection
            if (this.OpenConnection() == true)
            {
                // create mysql command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@uuid", uuid);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                // Execute query
                while (dataReader.Read())
                {
                    conf = dataReader.GetString(0);
                }
                //close connection
                this.CloseConnection();
                return conf;
            }
            return "";
        }
        // check if registered for conference
        public bool CheckConf(string uuid)
        {
            string query = "SELECT confcode FROM participants WHERE uuid=@uuid";
            string conf = "";
            // Open connection
            if (this.OpenConnection() == true)
            {
                // create mysql command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@uuid", uuid);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                // Execute query
                while(dataReader.Read()){
                    conf = dataReader.GetString(0);
                }
                if (conf != "")
                {
                    return true;
                }
                //close connection
                this.CloseConnection();
                return false;
            }
            return false;
        }
        // Method that changes both the hashed password and respective salt
        public bool ChangePass(string uuid, string passwd)
        {
            string query = "UPDATE users SET passwd=@passwd, salt=@salt WHERE partid=@uuid";
            string salt = SeaSalt(25);
            // Open connection
            if (this.OpenConnection() == true)
            {
                string hashedPass = HashedBrowns(passwd, salt);
                // create mysql command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@uuid", uuid);
                cmd.Parameters.AddWithValue("@salt", salt);
                cmd.Parameters.AddWithValue("@passwd", hashedPass);
                // Execute query
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                //close connection
                this.CloseConnection();
                return true;
            }
            return false;
        }
        // Register conference method
        public bool RegConf(string uuid, string confcode)
        {
            string query = "UPDATE participants SET confcode=@conf WHERE uuid=@partid";

            // Open connection
            if (this.OpenConnection() == true)
            {
                // create mysql command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@partid", uuid);
                cmd.Parameters.AddWithValue("@conf", confcode);
                // Execute query
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
                //close connection
                this.CloseConnection();
                return true;
            }
            return false;
        }
        // deletes all data relating to particular user ID
        public void DeleteUser(string uuid)
        {
            string query = "DELETE FROM users WHERE partid=@uuid; DELETE FROM participants WHERE uuid=@uuid; DELETE FROM registrations WHERE partid=@uuid;";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@uuid", uuid);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }
        // this method logs in the user using a salted hashed password
        public string LoginUser(string user, string password)
        {
            // get salt and hashed password for user
            string query = "SELECT * FROM users WHERE username=@user";
            string saltDb = "";
            string uuid = "";
            string passwdDb = "";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@user", user);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while(dataReader.Read()){
                    passwdDb = dataReader.GetString("passwd");
                    uuid = dataReader.GetString("partid");
                    saltDb = dataReader.GetString("salt");
                }
                // use salt to compute hash of current password and then
                // compare it to hash in database to authenticate
                string hashString = HashedBrowns(password, saltDb);
                if(passwdDb.Equals(hashString)){
                    this.CloseConnection();
                    return uuid;
                }
                this.CloseConnection();
            }
            return "";
        }
        // gets the user type for setting permissions
        public int GetType(string uuid)
        {
            string query = "SELECT (type+0) FROM participants WHERE uuid=@uuid";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@uuid", uuid);
                int type = 0;
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    type = dataReader.GetInt32(0);
                }
                this.CloseConnection();
                return type;
            }
            return 0;
        }
        // method that creates the conference workshop
        public bool CreateWkshp(string wkname, string wkdesc, string wkdate, string conf)
        {
            // create the UUID for primary key
            string uuid = System.Guid.NewGuid().ToString();
            // create the parameters for query
            string query = "INSERT INTO workshops (uuid, confcode, wname, wdesc, wdate) VALUES(@uuid, @conf, @wname, @wdesc, @wdate)";

            // open connection
            if (this.OpenConnection() == true)
            {
                // create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);
                // pass in data as parameters to prevent SQL injection
                cmd.Parameters.AddWithValue("@uuid", uuid);
                cmd.Parameters.AddWithValue("@conf", conf);
                cmd.Parameters.AddWithValue("@wname", wkname);
                cmd.Parameters.AddWithValue("@wdate", wkdate);
                cmd.Parameters.AddWithValue("@wdesc", wkdesc);
                // Execute command
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // close connection
                this.CloseConnection();
                // show that it succeeded
                return true;
            }
            // show that it failed
            return false;
        }
        // method that creates the conference participant
        public string CreatePart(int type, string fname, string lname, int chap)
        {
            // create the UUID for primary key
            string uuid = System.Guid.NewGuid().ToString();
            // create the parameters for query
            string query = "INSERT INTO participants (uuid, type, fname, lname, chapnum) VALUES(@uuid, @type, @fname, @lname, @chapnum)";

            // open connection
            if (this.OpenConnection() == true)
            {
                // create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);
                // pass in data as parameters to prevent SQL injection
                cmd.Parameters.AddWithValue("@uuid", uuid);
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@fname", fname);
                cmd.Parameters.AddWithValue("@lname", lname);
                cmd.Parameters.AddWithValue("@chapnum", chap);
                // Execute command
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // close connection
                this.CloseConnection();
                // return the uuid so that it can be used to create the user
                return uuid;
            }
            // 
            return "";
        }
        // method which registers participants for workshops
        public bool RegWkshp(string partid, string wkshpid)
        {
            // create the parameters for query
            string query = "INSERT INTO registrations (partid, wkshpid) VALUES(@partid, @wkshpid)";
            //open connection
            if (this.OpenConnection() == true)
            {
                // create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);
                // pass in data as parameters to prevent SQL injection
                cmd.Parameters.AddWithValue("@partid", partid);
                cmd.Parameters.AddWithValue("@wkshpid", wkshpid);
                // Execute command
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // close connection
                this.CloseConnection();
                // return success
                return true;
            }
            // return failure
            return false;
        }
        // method which creates the actual user
        public bool CreateUser(string uuid, string user, string passwd)
        {
            // create the parameters for query
            string query = "INSERT INTO users (partid, username, passwd, salt) VALUES(@uuid, @username, @passwd, @salt)";
            // create a salt to hash the passwords with
            string salt = SeaSalt(25);
            //open connection
            if (this.OpenConnection() == true)
            {
                // create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);
                // hash the password with the created salt
                string hashedPass = HashedBrowns(passwd, salt);
                // pass in data as parameters to prevent SQL injection
                cmd.Parameters.AddWithValue("@uuid", uuid);
                cmd.Parameters.AddWithValue("@username", user);
                cmd.Parameters.AddWithValue("@passwd", hashedPass);
                cmd.Parameters.AddWithValue("@salt", salt);
                // Execute command
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // close connection
                this.CloseConnection();
                // return success
                return true;
            }
            // return failure
            return false;
        }
        // I had named this method before you told me and it's just too funny to try and change it
        // this method takes the password and salt and hashes it
        private string HashedBrowns(string passwd, string salt)
        {
            byte[] bytePass = Encoding.Unicode.GetBytes(passwd + salt);
            SHA512Managed hasher = new SHA512Managed();
            byte[] hash = hasher.ComputeHash(bytePass);
            string hashString = Convert.ToBase64String(hash);
            return hashString;
        }
        // same thing with this method, they complement each other nicely (that's a crypto joke)
        // this method generates a salt to accompany the password in the hash
        private string SeaSalt(int stringLength)
        {
            // it just pretty much makes a random string from the allowed characters
            const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            var rd = new Random();
            string salty = "";
            for (int i = 0; i < stringLength; i++)
            {
                salty = salty + allowedChars[rd.Next(0, allowedChars.Length)];
            }
            return salty;
        }
        // provides data for a report on the conference attendees
        public List<string>[] GetConferenceAttendees(string confcode)
        {
            List<string>[] result = new List<string>[4];
            result[0] = new List<string>();
            result[1] = new List<string>();
            result[2] = new List<string>();
            result[3] = new List<string>();
            if (this.OpenConnection() == true)
            {
                string query = "SELECT * FROM participants WHERE confcode=@conf ORDER BY type, lname ASC";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@conf", confcode);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                string[] types = { "Über Admin", "Chapter Head", "Adviser", "Member", "Guest" };
                int type = 0;
                while (dataReader.Read())
                {
                    result[0].Add(dataReader.GetString("fname"));
                    result[1].Add(dataReader.GetString("lname"));
                    type = dataReader.GetInt32("type");
                    result[2].Add(types[type-1]);
                    result[3].Add(dataReader.GetInt32("chapnum").ToString());
                }
                this.CloseConnection();
            }
            return result;
        }
        // this gets the workshops available from one conference to show
        // the selection the user has to sign up from
        public List<string>[] GetWorkshops(string confcode)
        {
            List<string>[] result = new List<string>[4];
            result[0] = new List<string>();
            result[1] = new List<string>();
            result[2] = new List<string>();
            result[3] = new List<string>();
            if (this.OpenConnection() == true)
            {
                string query = "SELECT * FROM workshops WHERE confcode=@conf";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@conf", confcode);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    result[0].Add(dataReader.GetString("wname"));
                    result[1].Add(dataReader.GetString("wdesc"));
                    result[2].Add(dataReader.GetDateTime("wdate").ToString());
                    result[3].Add(dataReader.GetString("uuid"));
                }
                this.CloseConnection();
            }
            return result;
        }
        // checks to make sure no duplicate username is in the database
        public bool CheckUser(string user)
        {
            string query = "SELECT COUNT(*) FROM users WHERE username=@user";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@user", user);
                int count = int.Parse(cmd.ExecuteScalar() + "");
                if (count == 0)
                {
                    this.CloseConnection();
                    return false;
                }
                this.CloseConnection();
            }
            return true;
        }
        // gets the associated records of the user to display for profile
        public string[] FetchUserData(string uuid)
        {
            string query = "SELECT * FROM participants WHERE uuid=@uuid";
            string[] userData = new string[4];
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@uuid", uuid);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    userData[0] = dataReader.GetString("fname");
                    userData[1] = dataReader.GetString("lname");
                    userData[2] = dataReader.GetString("confcode");
                    userData[3] = dataReader.GetString("chapnum");
                }
                this.CloseConnection();
            }
            return userData;
        }
        // finds the username for the referenced userID
        public string FetchUserName(string uuid)
        {
            string query = "SELECT username FROM users WHERE partid=@uuid";
            string userName = "";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@uuid", uuid);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    userName = dataReader.GetString(0);
                }
                this.CloseConnection();
            }
            return userName;
        }
        // gets the students attending a certain workshop
        public List<string>[] GetRegistrations(string wkshp)
        {
            string query = "SELECT * FROM participants p LEFT JOIN registrations r ON p.uuid = r.partid WHERE r.wkshpid=@wkshp";
            List<string>[] result = new List<string>[3];
            result[0] = new List<string>();
            result[1] = new List<string>();
            result[2] = new List<string>();
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@wkshp", wkshp);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    result[0].Add(dataReader.GetString("lname"));
                    result[1].Add(dataReader.GetString("fname"));
                    result[2].Add(dataReader.GetInt32("chapnum").ToString());
                }
                this.CloseConnection();
            }
            return result;
        }
        // get participants schedule
        public List<string>[] GetSchedule(string participant)
        {
            string query = "SELECT * FROM workshops w LEFT JOIN registrations r ON w.uuid = r.wkshpid WHERE r.partid=@participant ORDER BY wdate ASC";
            List<string>[] result = new List<string>[3];
            result[0] = new List<string>();
            result[1] = new List<string>();
            result[2] = new List<string>();
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@participant", participant);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    result[0].Add(dataReader.GetDateTime("wdate").ToString());
                    result[1].Add(dataReader.GetString("wname"));
                    result[2].Add(dataReader.GetString("wdesc"));
                }
                this.CloseConnection();
            }
            return result;
        }
    }
}