using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace lb2.Models
{
    [Serializable]
    public class User
    {
        public string login { get; set; }
        public string password { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        private bool isAdmin { get; set; }

        public User(string l, string p, string n, string m)
        {
            login = l;
            password = p;
            fullName = n;
            email = m;
            isAdmin = false;
        }

        public User(string l, string p, string n, string m, bool a)
        {
            login = l;
            password = p;
            fullName = n;
            email = m;
            isAdmin = a;
        }

        public User()
        {
        }

        public void SetAdmin()
        {
            this.isAdmin = true;
        }

        public bool CheckAdmin()
        {
            return isAdmin;
        }       
        
    }

    [Serializable]
    public class Users
    {
        public static string fileName = System.AppDomain.CurrentDomain.BaseDirectory+"/App_Data/Users.txt";
        public static List<User> users = new List<User>();

        public Users(List<User> us)
        {
            users = us;
            if(!users.Exists(c=>c.login=="admin"))
                users.Add(new User("admin", "1111", "Administrator", "admin@gmail.com", true));
            if (!users.Exists(c => c.login == "admin1"))
                users.Add(new User("admin1", "2222", "Administrator1", "admin1@gmail.com", true));
            if (!users.Exists(c => c.login == "admin2"))
                users.Add(new User("admin2", "3333", "Administrator2", "admin2@gmail.com", true));
        }

        public static void Add(string l, string p, string n, string m)
        {
            if(users.Exists(c=>c.login== l))
            {                
                throw new Exception("Login is exist.");
            }
            else
                users.Add(new User(l, p, n, m));
        }

        public static bool Add(User user)
        {
            if (users.Exists(c => c.login == user.login))
            {
                return false;
            }
            else
            {
                users.Add(user);
                return true;
            }
        }
        public static void Update(string l, string p, string n, string m)
        {
            if (!users.Exists(c => c.login == l))
            {
                throw new Exception("Login does not exist.");
            }
            else
            {
                int i = users.IndexOf(users.Single(c => c.login == l));
                users.ElementAt(i).password = p;
                users.ElementAt(i).fullName = n;
                users.ElementAt(i).email = m;
            }
        }
        public static void Delete(string l)
        {
            if (!users.Exists(c => c.login == l))
            {
                throw new Exception("Login does not exist.");
            }
            else
            {
                users.Remove(users.Single(c => c.login == l));
            }
        }
        public static bool IsAdmin(string l)
        {
            return users.Single(c => c.login == l).CheckAdmin();
        }

        public static bool IsUser(string l)
        {
            if (!users.Exists(c => c.login == l))
            {
                return false;
            }
            else
            {
                return true;
            }
        }  
        
        public static bool CheckPass(string l, string pass)
        {
            if (!users.Exists(c => c.login == l && c.password == pass))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void Save()
        {            
            Stream stream = File.Open(fileName, FileMode.OpenOrCreate);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(stream, users);
            stream.Close();
        }

        public static void Open()
        {
            
            if (File.Exists(fileName))
            {
                Stream stream = File.OpenRead(fileName);
                BinaryFormatter deserializer = new BinaryFormatter();
                users = (List<User>)deserializer.Deserialize(stream);
                stream.Close();
            }
            if (!users.Exists(c => c.login == "admin"))
                users.Add(new User("admin", "1111", "Administrator", "admin@gmail.com", true));
            if (!users.Exists(c => c.login == "admin1"))
                users.Add(new User("admin1", "2222", "Administrator1", "admin1@gmail.com", true));
            if (!users.Exists(c => c.login == "admin2"))
                users.Add(new User("admin2", "3333", "Administrator2", "admin2@gmail.com", true));
        }

        public static string[] ValidData(User user)
        {
            if (user.login.Length > 4)
            {
                if (user.password.Length > 2)
                {
                    if (user.fullName.Length > 0)
                    {
                        if (user.email.Contains("@"))
                        {
                            return new string[2] { "0", "0" };
                        }
                        else
                            return new string[2] { "email", "Email must contains '@'" };
                    }
                    else
                        return new string[2] { "fullName", "Name not valid" };
                }
                else
                    return new string[2] { "password", "The password must be more than 2 characters" };
            }
            else
                return new string[2] { "login", "The login must be more than 4 characters" };

        }
    }

    public class LoginView
    {
        [Required(ErrorMessage = "Enter login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Enter password")]
        public string Password { get; set; }

    }

    //public class DelView
    //{
    //    [Required(ErrorMessage = "Login do not exist")]
    //    public string Login { get; set; }

    //    public DelView() { Login = ""}

    //    public DelView(string l)
    //    {
    //        Login = l;
    //    }
    //}

    //public class UsersView :List<User>
    //{
    //    public UsersView()
    //    {
    //        for (int i = 0; i < Users.users.Count; i++)
    //            Add(Users.users[i]);
    //    }
    //}

}
