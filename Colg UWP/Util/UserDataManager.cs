using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colg_UWP.Util
{
    using Model;

    public class UserDataManager
    {
        public const string _fileName = "UserData";

        private static List<User> _users = new List<User>();

        public static IEnumerable<User> GetUsers()
        {
            return _users.AsReadOnly();
        }

        public static async Task InitializationAsync()
        {
            _users = await StorageHelper.ReadAsync<List<User>>(_fileName)
            ??
            _users;
        }

        public static async Task SaveUserData()
        {
            await StorageHelper.SaveAsync(_users, _fileName).ConfigureAwait(false);
        }

        public static void AddOrUpdateUser(User user)
        {
            var existingUser = _users.SingleOrDefault(u => u.UserName == user.UserName);
            _users.Remove(existingUser);
            _users.Add(user);
        }

        public static void RemoveUser(User user)
        {
            _users.Remove(user);
        }

        public static User GetActiveUser()
        {
            return _users.SingleOrDefault(u => u.IsActive);
        }
    }
}