using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMSDBLayer.Models;
using IMSDBLayer.DataAccessInterfaces;

namespace IMSDBLayer.DataAccessObjects
{
    public class UserDataAccess : IUserDataAccess
    {
        public UserDataAccess() { }
        public User createUser(User user)
        {
            using (IMSEntities context = new IMSEntities())
            {
                context.Users.Add(user);
                context.SaveChanges();
                return context.Users.Find(user);
            }
        }

        public User fetchUserById(Guid userId)
        {
            using (IMSEntities context = new IMSEntities())
            {

                return context.Users.Where(u => u.Id == userId).FirstOrDefault();
            }
        }

        public User fetchUserByIdentityId(Guid identityId)
        {
            using (IMSEntities context = new IMSEntities())
            {
                var x = context.Users.Where(u => u.IdentityId == identityId.ToString()).FirstOrDefault();
                return x;
            }
        }

        public User fetchUserByName(string name)
        {
            using (IMSEntities context = new IMSEntities())
            {

                return context.Users.Where(u => u.Name == name).FirstOrDefault();
            }
        }

        public IEnumerable<User> fetchUsersByUserType(int userType)
        {
            using (IMSEntities context = new IMSEntities())
            {

                return context.Users.Where(u => u.Type == userType).ToList();
            }
        }

        public IEnumerable<User> getAll()
        {
            using (IMSEntities context = new IMSEntities())
            {

                return context.Users.ToList();
            }
        }

        public bool updateUser(User user)
        {
            using (IMSEntities context = new IMSEntities())
            {
                var old = context.Users.Where(u => u.Id == user.Id).FirstOrDefault();

                old = user;

                if (context.SaveChanges() > 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
