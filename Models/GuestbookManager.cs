using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace guestbook.Models {

    public class GuestbookManager {

        // database connectivity variables
        private MySqlConnection dbConnection; 
        private const string CONNECTION_STRING = "Server=localhost;Database=dotnetcoresamples;Uid=cfrederi;Pwd=Dexterismypetsname;SslMode=none;";
        private MySqlCommand dbCommand;
        private MySqlDataReader dbReader;

        // property variables
        private int _count;
        private List<GuestbookEntry> _guestbookEntry;
        // private List<SelectListItem> _categoryList;


        public GuestbookManager() {
            // initialization
            _count = 0;
            _guestbookEntry = new List<GuestbookEntry>();

            // construct key DB objects
            dbConnection = new MySqlConnection(CONNECTION_STRING);
            dbCommand = new MySqlCommand("", dbConnection);
        }     

        // ------------------------------------------------------- get/sets
        public int count {
            get {
                return _count;
            }
        }

        public List<GuestbookEntry> entries {
            get {
                return _guestbookEntry;
            }
        }

        public bool addEntryCheck {get; set;} = false;

        // ------------------------------------------------------- public methods
        public void setupMe() {
            // build guestbook data table
            getGuestbookData();
        }

        public void addEntry(string firstName, string lastName, string entry) {
            if (((firstName == null) || (lastName == null)) || ((firstName == null) && (lastName == null))){
                firstName = "Anonymous";
                lastName = "";
            }

            try {
                DateTime dateOfEntry = DateTime.Now;
                dbConnection.Open();   
                dbCommand.Parameters.Clear();
                dbCommand.CommandText ="INSERT INTO tblguestbook (firstName,lastName,dateOfEntry,entry) " +
                                        "VALUES (?firstName,?lastName,?dateOfEntry,?entry)";
                dbCommand.Parameters.AddWithValue("?firstName", firstName);                
                dbCommand.Parameters.AddWithValue("?lastName", lastName);                
                dbCommand.Parameters.AddWithValue("?dateOfEntry", dateOfEntry);
                dbCommand.Parameters.AddWithValue("?entry", entry);                
                dbCommand.ExecuteNonQuery(); 

            } catch (Exception e) {
            Console.WriteLine("\n>>> An error has occured with adding Entry");
                Console.WriteLine("\n>>> " + e.Message);
            } finally {
                dbConnection.Close();
            }       
        }

        private void getGuestbookData() {
            try {
                dbConnection.Open();
                dbCommand.CommandText = "SELECT * FROM tblguestbook";
                dbReader = dbCommand.ExecuteReader();
                while (dbReader.Read()) {

                    GuestbookEntry entry = new GuestbookEntry();
                    entry.firstName = dbReader["firstName"].ToString();
                    entry.lastName = dbReader["lastName"].ToString();
                    entry.dateOfEntry = dbReader["dateOfEntry"].ToString();
                    entry.entry = dbReader["entry"].ToString();
                    // add object to list
                    _guestbookEntry.Add(entry);
                }
                dbReader.Close();
                // sort entries so newest appear at the top
                _guestbookEntry = _guestbookEntry.OrderByDescending(o=>o.dateOfEntry).ToList();
                // accessing single value from db
                dbCommand.CommandText = "SELECT Count(*) FROM tblguestbook";                
                _count = Convert.ToInt32(dbCommand.ExecuteScalar());
            } catch (Exception e) {
                Console.WriteLine("\n>>> An error has occured with getGuestbookData");
                Console.WriteLine("\n>>> " + e.Message);
            } finally {
                dbConnection.Close();
            }
        }

        
    }

}