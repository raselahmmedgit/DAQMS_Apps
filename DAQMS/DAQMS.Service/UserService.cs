using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DAQMS.Domain.Models;
using System.Configuration;

namespace DAQMS.Service
{
    #region Interface Implement : User

    public class UserService : IUserService
    {
        #region Global Variable Declaration
        //private string strCon = String.Format("Server=localhost;Port=5432;User Id=postgres;Password=sa123456;Database=test_db;");
        private IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["AppDbContext"].ConnectionString);
        //private IDbConnection _db = new SqlConnection(String.Format("Server=localhost;Port=5432;User Id=postgres;Password=sa123456;Database=test_db;"));
        #endregion

        #region Actions

        public List<User> GetAll()
        {
            return this._db.Query<User>("SELECT * FROM user").ToList();
        }

        public User Find(int id)
        {
            return this._db.Query<User>("SELECT * FROM user WHERE user_id = @user_id", new { id }).SingleOrDefault();
        }

        public User Add(User user)
        {
            var sqlQuery = "INSERT INTO user (user_name, user_emailaddress) VALUES(@user_name, @user_emailaddress); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var userId = this._db.Query<int>(sqlQuery, user).Single();
            return user;
        }

        public User Update(User user)
        {
            var sqlQuery =
                "UPDATE user " +
                "SET user_name = @user_name, " +
                "    user_emailaddress  = @user_emailaddress, " +
                "WHERE user_id = @user_id";
            this._db.Execute(sqlQuery, user);
            return user;
        }

        public void Remove(int id)
        {
            var sqlQuery =
                "DELETE FROM user " +
                "WHERE user_id = @user_id";
            this._db.Execute(sqlQuery);
        }

        public User GetById(int id)
        {
            using (var multipleResults = this._db.QueryMultiple("GetUserByID", new { Id = id }, commandType: CommandType.StoredProcedure))
            {
                var user = multipleResults.Read<User>().SingleOrDefault();

                return user;
            }
        }

        #endregion
    }

    #endregion
    
    #region Interface : User

    public interface IUserService
    {
        List<User> GetAll();
        User Find(int id);
        User Add(User user);
        User Update(User user);
        void Remove(int id);
        User GetById(int id);
    }

    #endregion

}
