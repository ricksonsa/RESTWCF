using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using System.Data.OleDb;

namespace RestfulWCF.Service
{
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da classe "Service1" no arquivo de código, svc e configuração ao mesmo tempo.
    // OBSERVAÇÃO: Para iniciar o cliente de teste do WCF para testar esse serviço, selecione Service1.svc ou Service1.svc.cs no Gerenciador de Soluções e inicie a depuração.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service1 : IService1
    {
        public string DoInclude(string userName, string password, string name, string mail)
        {
            return UserDao.IncludeUser(userName, password, name, mail);
        }

        public string DoWork()
        {
            return "TEst";
        }

    }

    [DataContract]
    public class UserDao
    {
        [DataMember]
        public string Name { get; }
        [DataMember]
        public int Id { get; }
        [DataMember]
        public string Mail { get; }
        [DataMember]
        public string UserName { get; }

        private static readonly OleDbConnection conn = new OleDbConnection(@"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = C:\ProgramData\Adobe\VolupiaMessenger\Nova pasta\VolupiaServer\bin\Debug\VolupiaDB.mdb");

        public UserDao(int id, string userName, string mail, string name)
        {
            Id = id;
            UserName = userName;
            Mail = mail;
            Name = name;
        }

        public static bool CheckAccount(string loginEntry, string password)
        {
            object userObj = null, passObj = null, mailObj = null, name = null;
            int id = 0;
            string sql = "Select * from tbl_VolupiaUser Where Username = @Name or Email = @Name and Password = @Pass";
            OleDbParameter pararm = new OleDbParameter("@Name", loginEntry);
            OleDbParameter pararp = new OleDbParameter("@Pass", password);
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            cmd.Parameters.Add(pararm);
            cmd.Parameters.Add(pararp);
            conn.Open();


            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                id = (int)reader["Código"];
                name = reader["nm_Name"].ToString();
                userObj = reader["Username"].ToString();
                passObj = reader["Password"];
                mailObj = reader["Email"].ToString();
            }
            conn.Close();


            if (id != 0)
            {
                if (loginEntry == userObj.ToString() || loginEntry == mailObj.ToString())
                    if (password == passObj.ToString())
                        return true;
            }
            return false;
        }

        public static UserDao SQLlogin(string loginEntry, string password)
        {
            object userObj = null, passObj = null, mailObj = null, name = null;
            int id = 0;
            string sql = "Select * from tbl_VolupiaUser Where Username = @Name or Email = @Name and Password = @Pass";
            OleDbParameter pararm = new OleDbParameter("@Name", loginEntry);
            OleDbParameter pararp = new OleDbParameter("@Pass", password);
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            cmd.Parameters.Add(pararm);
            cmd.Parameters.Add(pararp);
            conn.Open();


            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                id = (int)reader["Código"];
                name = reader["nm_Name"].ToString();
                userObj = reader["Username"].ToString();
                passObj = reader["Password"];
                mailObj = reader["Email"].ToString();
            }

            UserDao VolupiaUser = new UserDao(id, userObj.ToString(), mailObj.ToString(), name.ToString());

            conn.Close();


            return VolupiaUser;
        }

        public static string IncludeUser(string userName, string password, string name, string mail)
        {
            string result = "Error";
            string sql = "INSERT INTO tbl_VolupiaUser (Username,Email,Password,nm_Name) VALUES (@User,@Mail,@Pass,@Name)";
            OleDbParameter p1 = new OleDbParameter("@User", userName);
            OleDbParameter p2 = new OleDbParameter("@Pass", password);
            OleDbParameter p3 = new OleDbParameter("@Name", name);
            OleDbParameter p4 = new OleDbParameter("@Mail", mail);
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            conn.Open();
            var re = cmd.ExecuteNonQuery();
            if (re > 0)
                result = "Success";
            conn.Close();
            return result;
        }
    }
}
