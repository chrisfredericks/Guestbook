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

        // public List<SelectListItem> categoryList {
        //     get {
        //         return _categoryList;
        //     }
        // }

        // ------------------------------------------------------- public methods
        public void setupMe() {
            // // get category list data
            // getCategoryList();
            // build guestbook data table
            getGuestbookData();
        }

        // public void getChosenGrade(string gradeID) {
            
        //     try {
        //         dbConnection.Open();
        //         dbCommand.Parameters.Clear();
        //         dbCommand.CommandText = "SELECT * FROM tblGrades WHERE gradeID = ?gradeID";
        //         dbCommand.Parameters.AddWithValue("?gradeID", gradeID);
        //         dbReader = dbCommand.ExecuteReader();
        //         while (dbReader.Read()) {

        //             Grade grade = new Grade();
        //             grade.gradeID = Convert.ToInt32(dbReader["gradeID"]);
        //             grade.categoryID = Convert.ToInt32(dbReader["categoryID"]);
        //             grade.courseName = dbReader["courseName"].ToString();
        //             grade.grade = dbReader["grade"].ToString();
        //             grade.comments = dbReader["comments"].ToString();
        //             grade.description = dbReader["description"].ToString();
        //             // add object to list
        //             _guestbookEntry.Add(grade);
        //         }
        //         dbReader.Close();
                
        //     } catch (Exception e) {
        //         Console.WriteLine("\n>>> An error has occured with get Grades");
        //         Console.WriteLine("\n>>> " + e.Message);
        //     } finally {
        //         dbConnection.Close();
        //     }

        // }

        // public void addGrade(string courseName, string courseDescription, string grade, string comments) {
        //     try {
        //         dbConnection.Open();
        //         // dbCommand.CommandText ="INSERT INTO tblGrades (categoryID,courseName,description,grade,comments) " +
        //         //                         "VALUES (" + categoryID + ",'" + courseName + "','" + courseDescription + "'," + 
        //         //                         grade +",'" + comments + "')";   
        //         dbCommand.Parameters.Clear();
        //         dbCommand.CommandText ="INSERT INTO tblGrades (categoryID,courseName,description,grade,comments) " +
        //                                 "VALUES (?categoryID,?courseName,?courseDescription,?grade,?comments)";
        //         dbCommand.Parameters.AddWithValue("?categoryID", categoryID);                
        //         dbCommand.Parameters.AddWithValue("?courseName", courseName);                
        //         dbCommand.Parameters.AddWithValue("?courseDescription", courseDescription);
        //         dbCommand.Parameters.AddWithValue("?grade", grade);                
        //         dbCommand.Parameters.AddWithValue("?comments", comments);                
        //         dbCommand.ExecuteNonQuery(); 

        //      } catch (Exception e) {
        //         Console.WriteLine("\n>>> An error has occured with get Grades");
        //         Console.WriteLine("\n>>> " + e.Message);
        //     } finally {
        //         dbConnection.Close();
        //     }       
        // }

        // public Category getCategoryName() {
        //     try {
        //         dbConnection.Open();
        //         dbCommand.Parameters.Clear();
        //         Console.WriteLine("\nCategory ID >>> " + categoryID);

        //         dbCommand.CommandText = "SELECT * FROM tblCategory WHERE categoryID = ?categoryID";
        //         dbCommand.Parameters.AddWithValue("?categoryID", categoryID);                
        //         dbReader = dbCommand.ExecuteReader();
        //         dbReader.Read();
        //         _catName.categoryID = Convert.ToInt32(dbReader["categoryID"]);
        //         _catName.categoryName = dbReader["categoryName"].ToString();
        //         dbConnection.Close();
        //         Console.WriteLine("\nCategory ID >>> " + categoryName);

        //     } catch (Exception e) {
        //         Console.WriteLine("\n>>> An error has occured with get Grades");
        //         Console.WriteLine("\n>>> " + e.Message);
        //     } finally {
        //         dbConnection.Close();
        //     }
        //     return _catName;
        // }
        // ------------------------------------------------------- private methods
        
        // private void getCategoryList() {
        //     try {
        //         dbConnection.Open();
        //         dbCommand.CommandText = "SELECT * FROM tblCategory";
        //         dbReader = dbCommand.ExecuteReader();

        //         // populate the list that will populate the dropdown
        //         while (dbReader.Read()) {
        //             SelectListItem item = new SelectListItem();
        //             item.Text = Convert.ToString(dbReader["categoryName"]);
        //             item.Value = Convert.ToString(dbReader["categoryID"]);
        //             _categoryList.Add(item);
        //         }
        //         dbReader.Close();

        //         // ***************** Challenge *****************
        //         if (categoryID == 0) {categoryID = Convert.ToInt32(_categoryList[0].Value);}
        //         // ***************** Challenge *****************

        //     } catch (Exception e) {
        //         Console.WriteLine("\n>>> An error has occured with get Grades");
        //         Console.WriteLine("\n>>> " + e.Message);
        //     } finally {
        //         dbConnection.Close();
        //     }
        // }

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