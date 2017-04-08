using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace BugTrack
{
    public partial class bugs1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connectionInfo = string.Format("server={0};user id={1};password={2};database={3};charset=utf8;",
                "localhost", "root", "", "bugrack");
            using (var connection = new MySqlConnection(connectionInfo))
            {
                connection.Open();
                var command = new MySqlCommand("Select * From projects;", connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            projec.Items.Add(reader.GetString(1));
                        }
                    }
                }
            }
            using (var connection = new MySqlConnection(connectionInfo))
            {
                connection.Open();
                var command = new MySqlCommand("Select * From bugs;", connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        LiteralText.Text += "<table class='table table-hover'>"
                            + "<tr><th>ID</th><th>Descr</th><th>Project</th><th>Error</th><th>Src</th><th>Class</th>"
                            + "<th>Method</th><th>Line</th><th>Track</th><th>Reported on</th><th>Action</th></tr>";
                        while (reader.Read())
                        {
                            LiteralText.Text += string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td>><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td><td>{9}</td>",
                                HttpUtility.HtmlEncode(reader.GetString(0)),
                                HttpUtility.HtmlEncode(reader.GetString(1)),
                                HttpUtility.HtmlEncode(reader.GetString(2)),
                                HttpUtility.HtmlEncode(reader.GetString(3)),
                                HttpUtility.HtmlEncode(reader.GetString(4)),
                                HttpUtility.HtmlEncode(reader.GetString(5)),
                                HttpUtility.HtmlEncode(reader.GetString(6)),
                                HttpUtility.HtmlEncode(reader.GetString(7)),
                                HttpUtility.HtmlEncode(reader.GetString(8)),
                                reader.GetString(9)
                                );
                            LiteralText.Text += "<td><a class='btn btn-warning btn-sm' href='viewbug.aspx?item=" + reader.GetString(0) + "&track=" + reader.GetString(8) + "&proj=" + reader.GetString(2) + "'>Go To</a></td></ tr >";
                        }
                        LiteralText.Text += "</table>";
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string descr = "", proj = "", err = "", src = "", cls = "", meth = "", lin = "", trck = "";
            descr = desc.Text;
            proj = projec.Text;
            err = errm.Text;
            src = srcfile.Text;
            cls = srccls.Text;
            meth = srcmet.Text;
            lin = line.Text;
            trck = link2.Text;

            string connectionInfo = string.Format("server={0};user id={1};password={2};database={3};charset=utf8;",
                "localhost", "root", "", "bugrack");
            using (var connection = new MySqlConnection(connectionInfo))
            {
                connection.Open();
                var command = new MySqlCommand("Insert Into bugs (descr, project, errormsg, srcfile, class, method, line, track) "+
                    "Values (?D, ?P, ?E, ?S, ?C, ?M, ?L, ?T);", connection);
                command.Parameters.AddWithValue("?D", descr);
                command.Parameters.AddWithValue("?P", proj);
                command.Parameters.AddWithValue("?E", err);
                command.Parameters.AddWithValue("?S", src);
                command.Parameters.AddWithValue("?C", cls);
                command.Parameters.AddWithValue("?M", meth);
                command.Parameters.AddWithValue("?L", lin);
                command.Parameters.AddWithValue("?T", trck);
                if (command.ExecuteNonQuery() > 0)
                {
                    LiteralMsg.Text += "<div class='alert alert-success'> Success! " +
                        "Bug Added Successfully.</ div > ";
                }
                else
                {
                    LiteralMsg.Text += "<div class='alert alert-danger'> Error! " +
                        "Failed To Add Bug. Please Try Again.</ div > ";
                }
            }

        }
    }
}