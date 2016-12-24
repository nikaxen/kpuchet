using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using uchet1.Models;


namespace uchet1.DAO
{
    public class RecordsDAO : DAO
    {

        public List<Statement> dekGETALLSTATEMENTS()
        {
            Connect();

            List<Statement> recordList = new List<Statement>();
            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Statement AS a INNER JOIN AspNetUsers AS b ON (a.id_user = b.Id) INNER JOIN Subject AS c ON (a.id_subject = c.id_subject)", Connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Statement record = new Statement();
                    record.id_statement = Convert.ToInt32(reader["id_statement"]);
                    record.id_subject = Convert.ToInt32(reader["id_subject"]);
                    record.prepname = Convert.ToString(reader["fio"]); 
                    record.subjtitle = Convert.ToString(reader["subject_title"]);
                    record.id_user = Convert.ToString(reader["id_user"]);
                    record.status = Convert.ToString(reader["status"]);
                    record.title = Convert.ToString(reader["title"]);
                    
                    recordList.Add(record);
                }
                reader.Close();
            }
            catch (Exception) { }
            finally { Disconnect(); }
            return recordList;
        }
        public List<Statement> SearchByStatements(string sq)
        {
            Connect();
            List<Statement> recordList = new List<Statement>();
            try
            {
                string qstring = "SELECT * FROM Statement AS a INNER JOIN AspNetUsers AS b ON (a.id_user = b.Id) INNER JOIN Subject AS c ON (a.id_subject = c.id_subject) WHERE a.title LIKE '%"+ sq +"%'";
                SqlCommand command = new SqlCommand(qstring, Connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Statement record = new Statement();
                    record.id_statement = Convert.ToInt32(reader["id_statement"]);
                    record.id_subject = Convert.ToInt32(reader["id_subject"]);
                    record.prepname = Convert.ToString(reader["fio"]);
                    record.subjtitle = Convert.ToString(reader["subject_title"]);
                    record.id_user = Convert.ToString(reader["id_user"]);
                    record.status = Convert.ToString(reader["status"]);
                    record.title = Convert.ToString(reader["title"]);

                    recordList.Add(record);
                }
                reader.Close();
            }
            catch (Exception) { }
            finally { Disconnect(); }
            return recordList;
        }
        public List<Statement> getStatementById(int st_id)
        {
            Connect();
            List<Statement> recordList = new List<Statement>();
            try
            {
                string qstring = "SELECT * FROM Statement AS a INNER JOIN Subject AS c ON (a.id_subject = c.id_subject) WHERE a.id_statement='" + st_id +"'";
                SqlCommand command = new SqlCommand(qstring, Connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Statement record = new Statement();
                    record.id_statement = Convert.ToInt32(reader["id_statement"]);
                    record.id_subject = Convert.ToInt32(reader["id_subject"]);
                    //record.prepname = Convert.ToString(reader["fio"]);
                    record.subjtitle = Convert.ToString(reader["subject_title"]);
                    //record.id_user = Convert.ToString(reader["id_user"]);
                    record.status = Convert.ToString(reader["status"]);
                    record.title = Convert.ToString(reader["title"]);

                    recordList.Add(record);
                }
                reader.Close();
            }
            catch (Exception) { }
            finally { Disconnect(); }
            return recordList;
        }
        public List<Statement> ShowReadyStatements()
        {
            Connect();
            List<Statement> recordList = new List<Statement>();
            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Statement AS a INNER JOIN AspNetUsers AS b ON (a.id_user = b.Id) INNER JOIN Subject AS c ON (a.id_subject = c.id_subject) WHERE a.status='ready'", Connection);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Statement record = new Statement();
                    record.id_statement = Convert.ToInt32(reader["id_statement"]);
                    record.id_subject = Convert.ToInt32(reader["id_subject"]);
                    record.prepname = Convert.ToString(reader["fio"]);
                    record.subjtitle = Convert.ToString(reader["subject_title"]);
                    record.id_user = Convert.ToString(reader["id_user"]);
                    record.status = Convert.ToString(reader["status"]);
                    record.title = Convert.ToString(reader["title"]);
                    recordList.Add(record);
                }
                reader.Close();
            }
            catch (Exception) { }
            finally { Disconnect(); }
            return recordList;
        }
        public List<Statement> getStatementsForPrep(string prepid)
        {
            Connect();
            List<Statement> recordList = new List<Statement>();
            try
            {
                string qstring = "SELECT * FROM Statement AS a INNER JOIN AspNetUsers AS b ON (a.id_user = b.Id) INNER JOIN Subject AS c ON (a.id_subject = c.id_subject) WHERE b.Id='"+ prepid +"' AND a.status='opened'";
                SqlCommand command = new SqlCommand(qstring, Connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Statement record = new Statement();
                    record.id_statement = Convert.ToInt32(reader["id_statement"]);
                    record.id_subject = Convert.ToInt32(reader["id_subject"]);
                    record.prepname = Convert.ToString(reader["fio"]);
                    record.subjtitle = Convert.ToString(reader["subject_title"]);
                    record.id_user = Convert.ToString(reader["id_user"]);
                    record.status = Convert.ToString(reader["status"]);
                    record.title = Convert.ToString(reader["title"]);

                    recordList.Add(record);
                }
                reader.Close();
            }
            catch (Exception) { }
            finally { Disconnect(); }
            return recordList;
        }

        

        public List<Mark> studGetMarks(string studid, string viewmode)
        {
            Connect();
            List<Mark> recordList = new List<Mark>();
            try
            {
                string qstring;
                switch (viewmode) {
                    case "all":
                        qstring = "SELECT * FROM Mark AS a INNER JOIN AspNetUsers AS b ON (a.id_user = b.Id) INNER JOIN Statement AS c ON (a.id_statement = c.id_statement) INNER JOIN Subject AS d ON (c.id_subject=d.id_subject) WHERE a.id_user ='" + studid + "'";
                        break;
                    case "badmarksonly":
                        qstring = "SELECT * FROM Mark AS a INNER JOIN AspNetUsers AS b ON (a.id_user = b.Id) INNER JOIN Statement AS c ON (a.id_statement = c.id_statement) INNER JOIN Subject AS d ON (c.id_subject=d.id_subject) WHERE a.id_user ='" + studid + "' AND mark<3";
                        break;
                    default:
                        qstring ="";
                        break;
                }
                SqlCommand command = new SqlCommand(qstring, Connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Mark record = new Mark();
                    record.id_mark = Convert.ToInt32(reader["id_mark"]);
                    record.id_statement = Convert.ToInt32(reader["id_statement"]);
                    record.statement_title = Convert.ToString(reader["title"]);
                    record.subject_id = Convert.ToInt32(reader["id_subject"]);
                    record.subject_title = Convert.ToString(reader["subject_title"]);
                    record.id_user = Convert.ToString(reader["id_user"]);
                    record.themark = Convert.ToString(reader["mark"]);
                    record.user_fio = Convert.ToString(reader["fio"]);

                    recordList.Add(record);
                }
                reader.Close();
            }
            catch (Exception) { }
            finally { Disconnect(); }
            return recordList;
        }

        public List<Mark> getMarkDetails(int markid)
        {
            Connect();
            List<Mark> recordList = new List<Mark>();
            try
            {
                string qstring = "SELECT * FROM Mark AS a INNER JOIN AspNetUsers AS b ON (a.id_user = b.Id) INNER JOIN Statement AS c ON (a.id_statement = c.id_statement) INNER JOIN Subject AS d ON (c.id_subject=d.id_subject) WHERE  id_mark='"+ markid +"'";  
                SqlCommand command = new SqlCommand(qstring, Connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Mark record = new Mark();
                    record.id_mark = Convert.ToInt32(reader["id_mark"]);
                    record.id_statement = Convert.ToInt32(reader["id_statement"]);
                    record.statement_title = Convert.ToString(reader["title"]);
                    record.subject_id = Convert.ToInt32(reader["id_subject"]);
                    record.subject_title = Convert.ToString(reader["subject_title"]);
                    record.id_user = Convert.ToString(reader["id_user"]);
                    record.themark = Convert.ToString(reader["mark"]);
                    record.user_fio = Convert.ToString(reader["fio"]);

                    recordList.Add(record);
                }
                reader.Close();
            }
            catch (Exception) { }
            finally { Disconnect(); }
            return recordList;
        }

        public void RemoveMarkFromStatement(int removemarkid)
        {
            try
            {
                Connect();
                string str = "DELETE FROM Mark WHERE id_mark=" + removemarkid;
                SqlCommand com = new SqlCommand(str, Connection);
                com.ExecuteNonQuery();
            }
            finally
            {
                Disconnect();
            }
        }

        public List<Mark> getMarksForStatement(int id)
        {
            Connect();
            List<Mark> recordList = new List<Mark>();
            try
            {
                string qstring = "SELECT * FROM Mark AS a INNER JOIN AspNetUsers AS b ON (a.id_user = b.Id) WHERE a.id_statement='"+ id + "'";
                SqlCommand command = new SqlCommand(qstring, Connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Mark record = new Mark();
                    record.id_statement = Convert.ToInt32(reader["id_statement"]);
                    record.id_user = Convert.ToString(reader["id_user"]);
                    record.id_mark = Convert.ToInt32(reader["id_mark"]);
                    record.themark = Convert.ToString(reader["mark"]);
                    record.user_fio = Convert.ToString(reader["fio"]);
                    recordList.Add(record);
                }
                reader.Close();
            }
            catch (Exception) { }
            finally { Disconnect(); }
            return recordList;
        }

        
        public List<UserRoles> ShowUserRoles()
        {
            Connect();
            List<UserRoles> recordList = new List<UserRoles>();
            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM AspNetUsers AS a INNER JOIN AspNetUserRoles AS b ON (a.Id = b.UserId)", Connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    UserRoles record = new UserRoles();
                    record.id_role = Convert.ToString(reader["RoleId"]);
                    record.id_user = Convert.ToString(reader["Id"]);
                    record.user_fio = Convert.ToString(reader["fio"]);

                    recordList.Add(record);
                }
                reader.Close();
            }
            catch (Exception) { }
            finally { Disconnect(); }
            return recordList;
        }

        public void StatementSetStatus(int st_id, string set_status) {
            try
            {
                Connect();
                string str = "UPDATE Statement SET status='" + set_status
                    + "' WHERE id_statement=" + st_id;
                SqlCommand com = new SqlCommand(str, Connection);
                com.ExecuteNonQuery();
            }
            finally
            {
                Disconnect();
            }
        }
        public void SetUserRole(string userid, string roleid)
        {
            try
            {
                Connect();
                string str = "UPDATE AspNetUserRoles SET RoleId='" + roleid
                    + "' WHERE UserId='" + userid +"'";
                SqlCommand com = new SqlCommand(str, Connection);
                com.ExecuteNonQuery();
            }
            finally
            {
                Disconnect();
            }
        }

        public string dekGetStudGroup(string stid)
        {
            Connect();
            String qstring = "SELECT * FROM StudInGroup AS a INNER JOIN StudGroup AS b ON(a.groupid = b.studgroupid) WHERE a.studid='"+ stid +"'";
            //String qstring = "SELECT * FROM StudGroup AS a INNER JOIN StudInGroup AS b ON(a.studgroupid = b.groupid) WHERE studid=" + stid;
            String result=null;
            try
            {
                SqlCommand command = new SqlCommand(qstring, Connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = Convert.ToString(reader["studgroupname"]);
                }
                reader.Close();
            }
            catch (Exception) { }
            finally { Disconnect(); }
            return result;
        }
        public List<Users> dekGetAllStudentGroups()
        {
            Connect();
            List<Users> recordList = new List<Users>();
            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM StudGroup", Connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Users record = new Users();
                    record.studgroupid = Convert.ToInt32(reader["studgroupid"]);
                    record.studgroupname = Convert.ToString(reader["studgroupname"]);
                    recordList.Add(record);
                }
                reader.Close();
            }
            catch (Exception) { }
            finally { Disconnect(); }
            return recordList;
        }
        public List<Users> dekGetAllStuds() {
            Connect();
            List<Users> recordList = new List<Users>();
            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM AspNetUsers AS a INNER JOIN AspNetUserRoles AS b ON (a.Id = b.UserId) WHERE RoleId=4", Connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Users record = new Users();
                    record.Id = Convert.ToString(reader["Id"]);
                    record.fio = Convert.ToString(reader["fio"]);
                    record.birthdate = Convert.ToDateTime(reader["birthdate"]);
                    recordList.Add(record);
                }
                reader.Close();
            }
            catch (Exception) { }
            finally { Disconnect(); }
            return recordList;
        }
        public List<Users> dekGETALLUSERS()
        {
            Connect();
            List<Users> recordList = new List<Users>();
            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM AspNetUsers", Connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Users record = new Users();
                    record.Id = Convert.ToString(reader["Id"]);
                    record.Email = Convert.ToString(reader["Email"]);
                    record.fio = Convert.ToString(reader["fio"]);
                    record.birthdate = Convert.ToDateTime(reader["birthdate"]);
                    recordList.Add(record);
                }
                reader.Close();
            }
            catch (Exception) { }
            finally { Disconnect(); }
            return recordList;
        }
        public List<Users> getUserById(string user_id)
        {
            Connect();
            List<Users> recordList = new List<Users>();
            try
            {
                string qstring = "SELECT * FROM AspNetUsers WHERE Id='"+ user_id +"'";
                SqlCommand command = new SqlCommand(qstring, Connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Users record = new Users();
                    record.Id = Convert.ToString(reader["Id"]);
                    record.Email = Convert.ToString(reader["Email"]);
                    record.fio = Convert.ToString(reader["fio"]);
                    record.birthdate = Convert.ToDateTime(reader["birthdate"]);
                    recordList.Add(record);
                }
                reader.Close();
            }
            catch (Exception) { }
            finally { Disconnect(); }
            return recordList;
        }
        public List<Subject> getAllSubjects()
        {
            Connect();
            List<Subject> recordList = new List<Subject>();
            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Subject", Connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Subject record = new Subject();
                    record.id_subject = Convert.ToInt32(reader["id_subject"]);
                    record.subject_title = Convert.ToString(reader["subject_title"]);
                    recordList.Add(record);
                }
                reader.Close();
            }
            catch (Exception) { }
            finally { Disconnect(); }
            return recordList;
        }
        public List<Statement> GetStudGroupByStatementID(int stid) {
            Connect();
            List<Statement> recordList = new List<Statement>();
            try
            {
                string qstring = "SELECT * FROM StudGroup AS a INNER JOIN Statement AS b ON(a.studgroupid = b.idstudgroup) WHERE id_statement=" + stid;
                SqlCommand command = new SqlCommand(qstring, Connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Statement st = new Statement();
                    st.studgroupname = Convert.ToString(reader["studgroupname"]);
                    st.idstudgroup = Convert.ToInt32(reader["studgroupid"]);
                    recordList.Add(st);
                }
                reader.Close();
            }
            catch (Exception) { }
            finally { Disconnect(); }
            return recordList;
        }
        public List<Users> ShowUsersByRole(int role_id)
        {
            Connect();
            List<Users> recordList = new List<Users>();
            try
            {
                string qstring = "SELECT * FROM AspNetUsers WHERE Id IN (SELECT UserId FROM AspNetUserRoles WHERE RoleId=" + role_id + ")";
                SqlCommand command = new SqlCommand(qstring, Connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Users record = new Users();
                    record.Id = Convert.ToString(reader["Id"]);
                    record.Email = Convert.ToString(reader["Email"]);
                    record.fio = Convert.ToString(reader["fio"]);
                    record.birthdate = Convert.ToDateTime(reader["birthdate"]);
                    recordList.Add(record);
                }
                reader.Close();
            }
            catch (Exception) { }
            finally { Disconnect(); }
            return recordList;
        }
        public List<Users> ShowStudOfGroup(int gr_id)
        {
            Connect();
            List<Users> recordList = new List<Users>();
            try
            {
                string qstring = "SELECT * FROM AspNetUsers AS a INNER JOIN StudInGroup AS b ON(a.Id=b.studid) WHERE groupid='"+ gr_id +"'";
                SqlCommand command = new SqlCommand(qstring, Connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Users record = new Users();
                    record.Id = Convert.ToString(reader["Id"]);
                    record.Email = Convert.ToString(reader["Email"]);
                    record.fio = Convert.ToString(reader["fio"]);
                    record.birthdate = Convert.ToDateTime(reader["birthdate"]);
                    recordList.Add(record);
                }
                reader.Close();
            }
            catch (Exception) { }
            finally { Disconnect(); }
            return recordList;
        }
        public bool AddStatement(Statement statement)
        {
            bool result = true;
            Connect();
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Statement(id_subject, id_user, status, title, idstudgroup)" +
                    "VALUES (@id_subject, @id_user, @status, @title, @idstudgroup)", Connection
                    );
                cmd.Parameters.Add(new SqlParameter("@id_subject", statement.id_subject));
                cmd.Parameters.Add(new SqlParameter("@id_user", statement.id_user));
                cmd.Parameters.Add(new SqlParameter("@status", "opened"));
                cmd.Parameters.Add(new SqlParameter("@title", statement.title));
                cmd.Parameters.Add(new SqlParameter("@idstudgroup", statement.idstudgroup));
                cmd.ExecuteNonQuery();
            }
            catch (Exception) { result = false; }
            finally { Disconnect(); }
            return result;
        }

        public bool AddGroupForStudent(string uid, int sgid) {
            bool result = true;
            Connect();
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO StudInGroup(studid, groupid)" +
                    "VALUES (@studid, @groupid)", Connection
                    );
                cmd.Parameters.Add(new SqlParameter("@studid", uid));
                cmd.Parameters.Add(new SqlParameter("@groupid", sgid));
                cmd.ExecuteNonQuery();
            }
            catch (Exception) { result = false; }
            finally { Disconnect(); }
            return result;
        }

        public bool AddMark(Mark mark)
        {
            bool result = true;
            Connect();
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Mark(id_statement, id_user, mark)" +
                    "VALUES (@id_statement, @id_user, @themark)", Connection
                    );
                cmd.Parameters.Add(new SqlParameter("@id_statement", mark.id_statement));
                cmd.Parameters.Add(new SqlParameter("@id_user", mark.id_user));
                cmd.Parameters.Add(new SqlParameter("@themark", mark.themark));
                cmd.ExecuteNonQuery();
            }
            catch (Exception) { result = false; }
            finally { Disconnect(); }
            return result;
        }






        //end
    }
}