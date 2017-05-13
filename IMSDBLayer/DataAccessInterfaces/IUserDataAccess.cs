using IMSDBLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSDBLayer.DataAccessInterfaces
{
    public interface IUserDataAccess
    {
        /// <summary>
        /// Create a new user in database
        /// </summary>
        /// <param name="user">The user object</param>
        /// <returns>A user object created</returns>
        User createUser(User user);
        /// <summary>
        /// Update the user in database
        /// </summary>
        /// <param name="user">The user instance</param>
        /// <returns>True if success, false if fail</returns>
        bool updateUser(User user);
        /// <summary>
        /// Get all user from database
        /// </summary>
        /// <returns>A list of user</returns>
        IEnumerable<User> getAll();
        /// <summary>
        /// Get an user from database using it's id
        /// </summary>
        /// <param name="userId">The guid of an users</param>
        /// <returns>An user object</returns>
        User fetchUserById(Guid userId);
        /// <summary>
        /// Get an user from database using it's identity id
        /// </summary>
        /// <param name="identityId">The identity id of a logged in user</param>
        /// <returns>An user object</returns>
        User fetchUserByIdentityId(Guid identityId);
        /// <summary>
        /// Get user by it's name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>An user object</returns>
        User fetchUserByName(string name);
        /// <summary>
        /// Get Users by it's role
        /// </summary>
        /// <param name="userType">roles of the user</param>
        /// <returns>A list of user</returns>
        IEnumerable<User> fetchUsersByUserType(int userType);
    } 
}
