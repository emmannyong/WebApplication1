using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace BugTrack
{
    public partial class viewbug : System.Web.UI.Page
    {
        string connectionInfo = string.Format("server={0};user id={1};password={2};database={3};charset=utf8;",
                "localhost", "root", "", "bugrack");
        protected void Page_Load(object sender, EventArgs e)
        {
            string proj = Request.QueryString["proj"];
            string bug = Request.QueryString["item"];
            string track = Request.QueryString["track"];

            bugId.Text = bug;
            string bugID = "", bugN = "", bl = "", bsrc = "", bcl = "", bm = "", em = "";
            using (var connection = new MySqlConnection(connectionInfo))
            {
                connection.Open();
                var command = new MySqlCommand("Select * From bugs WHERE `id`=" + bug + ";", connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            bugID = reader.GetString(0);
                            bugN = reader.GetString(1);
                            em = reader.GetString(3);
                            bl = reader.GetString(7);
                            bsrc = reader.GetString(4);
                            bcl = reader.GetString(5);
                            bm = reader.GetString(6);
                        }
                    }
                }
            }

            String prbase = getPRBase(proj);
            bugId.Text = bugID;
            trNo.Text = track;
            errm.Text = em;
            
            LiteralGo.Text += "<a class='btn btn-info btn-md' target='_blank' href='"+ prbase +"/"+ bsrc + "'> Start Editing</a><br />&nbsp;&nbsp;&nbsp;"
                + "<p>At: " + bcl+" >> "+bm+" >> Line "+bl+"</p>";

            using (var connection = new MySqlConnection(connectionInfo))
            {
                connection.Open();
                var command = new MySqlCommand("Select * From bugs WHERE `track`='"+track+"';", connection);
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
                            LiteralText.Text += "<td><a href='viewbug.aspx?item=" + reader.GetString(0) + "&track=" + reader.GetString(8) + "&proj=" + reader.GetString(2) + "'>Go To</a></td></ tr >";
                        }
                        LiteralText.Text += "</table>";
                    }
                }
            }
            getHistory(bugID);
        }
        public void getHistory(string x)
        {
            using (var connection = new MySqlConnection(connectionInfo))
            {
                connection.Open();
                var command = new MySqlCommand("Select * From history WHERE `bug`='" + x + "';", connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        LiteralHist.Text += "<table class='table table-hover'>"
                            + "<tr><th>ID</th><th>User</th><th>Bug</th><th>Track</th><th>Error</th><th>Status</th><th>Date</th></tr>";
                        while (reader.Read())
                        {
                            LiteralHist.Text += string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td>><td>{5}</td><td>{6}</td>",
                                HttpUtility.HtmlEncode(reader.GetString(0)),
                                HttpUtility.HtmlEncode(reader.GetString(1)),
                                HttpUtility.HtmlEncode(reader.GetString(2)),
                                HttpUtility.HtmlEncode(reader.GetString(3)),
                                HttpUtility.HtmlEncode(reader.GetString(4)),
                                HttpUtility.HtmlEncode(reader.GetString(5)),
                                reader.GetString(6)
                                );
                        }
                        LiteralHist.Text += "</table>";
                    }
                }
            }
        }
        public String getPRBase(string x)
        {
            string pbase = "";
            using (var connection = new MySqlConnection(connectionInfo))
            {
                connection.Open();
                var command = new MySqlCommand("Select psrc From projects WHERE pname='"+x+"';", connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            pbase = reader.GetString(0);
                        }
                    }
                    return pbase;
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string bid = "", err = "", newst = "", trno = "", user = "";
            bid = bugId.Text;
            err = errm.Text;
            newst = newStat.Text;
            trno = trNo.Text;
            user = Session["username"].ToString();

            using (var connection = new MySqlConnection(connectionInfo))
            {
                connection.Open();
                var command = new MySqlCommand("Insert Into history (user, bug, track, error, newstat) " +
                    "Values (?U, ?D, ?P, ?E, ?S);", connection);
                command.Parameters.AddWithValue("?U", user);
                command.Parameters.AddWithValue("?D", bid);
                command.Parameters.AddWithValue("?P", trno);
                command.Parameters.AddWithValue("?E", err);
                command.Parameters.AddWithValue("?S", newst);
                //Update
                var command1 = new MySqlCommand("UPDATE bugs SET status = ?N WHERE `id` = ?I;", connection);
                command1.Parameters.AddWithValue("?N", newst);
                command1.Parameters.AddWithValue("?I", bid);
                if (command.ExecuteNonQuery() > 0 && command1.ExecuteNonQuery() > 0)
                {
                    LiteralMsg.Text += "<div class='alert alert-success'> Success! " +
                        "Bug Updated Successfully.</ div > ";
                }
                else
                {
                    LiteralMsg.Text += "<div class='alert alert-danger'> Error! " +
                        "Failed To Update Bug. Please Try Again.</ div > ";
                }
            }
        }
    }
}