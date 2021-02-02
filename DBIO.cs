using DBProvider;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseIO
{
    public class DBIO
    {
        MyDB database = new MyDB();
        
        public Users GetUsers(string username, string password)
        {
            //without param
            //string query = "SELECT * FROM Users WHERE id ='" + id + "' AND password ='" + password + "'";
            //Users row;
            //row = database.Database.SqlQuery<Users>(query).FirstOrDefault();
            //return row;

            //with params
            //Users row;
            //row = database.Database.SqlQuery<Users>("SELECT * FROM Users WHERE username = @username AND password = @password", 
            //    new SqlParameter("@username", username),
            //    new SqlParameter("@password",password)).FirstOrDefault();
            //return row;

            Users row;
            row = database.Users.Where(q => q.username == username && q.password == password).FirstOrDefault();
            return row;
        }
        
        public int GetLatestID()
        {
            int number;
            number = database.Users.Count();

            return number;
        }
        public List<Users> GetUsersList()
        {
            return database.Users.OrderByDescending(q => q.ID).ToList();
        }

        public Users GetObject_Users(string id)
        {
            int deleteId = Int32.Parse(id);
            return database.Users.Where(q => q.ID == deleteId).FirstOrDefault();
        }
        

        //Add
        public void AddObject<T>(T obj)
        {
            database.Set(obj.GetType()).Add(obj);
        }
        //Delete
        public void DeleteObject<T>(T obj)
        {
            database.Set(obj.GetType()).Remove(obj);
        }
        public void Save()
        {
            database.SaveChanges();
        }
    }
}
