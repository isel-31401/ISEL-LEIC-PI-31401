using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Security.Principal;
using System.Diagnostics;
using CurricularUnitFormManager.Models.Individual;


namespace CurricularUnitFormManager.Models.Individual
{
    /*
     Need SingleTon
     */
    public sealed class UserLoad
    {
        private static volatile Dictionary<String, Individual> _users;// = new Dictionary<string, Individual>();
        private static volatile UserLoad _instance=null;
        private static object _lock = new Object();
        
        private readonly static String _filePath = "./Content/config/users.xml";

        private UserLoad()
        {
            //if (_users != null) return;
            _users = new Dictionary<string, Individual>();
            Environment.CurrentDirectory = HttpContext.Current.Server.MapPath("/");
            XmlTextReader  xmlFile = new XmlTextReader (_filePath);

             xmlFile.Read();
             while (xmlFile.Read())
             {
                 xmlFile.MoveToElement();
                 if (xmlFile.NodeType == XmlNodeType.Element && xmlFile.Name.Equals("user")){
                     Individual user = new Individual();
                     user.IndividualId = Int16.Parse(xmlFile.GetAttribute("IndividualId"));
                     user.FullName = xmlFile.GetAttribute("FullName").Trim();
                     user.UserName = xmlFile.GetAttribute("UserName").Trim();
                     user.Password = xmlFile.GetAttribute("Password").Trim();
                     user.Email = xmlFile.GetAttribute("Email").Trim();
                     user.PhotoName = xmlFile.GetAttribute("PhotoName").Trim();
                     user.SecurityAnswer = xmlFile.GetAttribute("SecurityAnswer").Trim();
                     user.SecurityQuestion = xmlFile.GetAttribute("SecurityQuestion").Trim();

                     foreach (string s in xmlFile.GetAttribute("Roles").Split(','))
                     {
                         user.addRole(s.Trim());
                     }
                     _users.Add(user.UserName, user);
                 }

             }
             xmlFile.Close();
        }
        
        public static void WriteUserDb()
        {


            XmlTextWriter repo = new XmlTextWriter(_filePath,null);
            repo.Formatting = Formatting.Indented;
            repo.Indentation = 0;
            repo.IndentChar = '\n';
            repo.WriteStartElement("users");
            foreach (String s in _users.Keys)
            {
                Individual i = _users[s];
                repo.WriteStartElement("user", "");
                repo.WriteAttributeString("IndividualId",i.IndividualId.ToString());
                repo.WriteAttributeString("FullName",i.FullName);
                repo.WriteAttributeString("UserName",i.UserName);
                repo.WriteAttributeString("Password",i.Password );
                repo.WriteAttributeString("Email", i.Email);
                repo.WriteAttributeString("PhotoName",i.PhotoName);
                repo.WriteAttributeString("SecurityAnswer",i.SecurityAnswer);
                repo.WriteAttributeString("SecurityQuestion",i.SecurityAnswer);

                String role="";
                String[] roles= i.getRoles();
                int x;
                for (x = 0; x < roles.Length - 1; x++)
                {
                    role = role + roles[x] + " , ";
                }
                role = role + roles[x];
                repo.WriteAttributeString("Roles", role);
                repo.WriteEndElement();
                

            }
            repo.WriteEndElement();
            repo.Close();

        }

        public static void Start()
        {
            if (_users == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new UserLoad();
                    }
                }
            }
        }

        public static Dictionary<String, Individual> Instance
        {
            get 
            {
                if (_users == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                    _instance = new UserLoad();
                        }
                    }
                }
                return _users;
            }
        }

        public static IEnumerable<IndividualRepoModel> GetAllIndividuals()
        {
            foreach (Individual u in _users.Values)
            {
                yield return new IndividualRepoModel(u);
            }
        }

        //TODO CurricularUnitFormManager: Debug only. need to be deleted after.
        public void printAllUsersInfo()
        {
            foreach (KeyValuePair<String, Individual> u in _users)
            {
                Console.WriteLine(u.Value.ToString());
            }
        }
    }

}
