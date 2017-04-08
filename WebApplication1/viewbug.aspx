<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="viewbug.aspx.cs" Inherits="BugTrack.viewbug" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class='col-md-12'>
        <asp:Literal
            ID='LiteralMsg' 
            runat="server"  
            Text="">
        </asp:Literal>
    </div>
    <div class="col-md-6">
        <div class="row">
            <div class="panel-group">
              <div class="panel panel-default">
                <div class="panel-heading">
                  <h4 class="panel-title">
                    <a data-toggle="collapse" href="#">Bug Action History</a>
                  </h4>
                </div>
                <div id="collapse3" class="panel-collapse collapse in">
                  <div class="panel-body">
                      <asp:Literal
                            ID='LiteralHist' 
                            runat="server"  
                            Text=" ">
                      </asp:Literal>
                  </div>
                </div>
              </div>
            </div>
        </div>
    </div>
    <div class="col-md-6">
        <div class="row">
            <div class="panel-group">
              <div class="panel panel-default">
                <div class="panel-heading">
                  <h4 class="panel-title">
                    <a data-toggle="collapse" href="#collapse1">Update Bug <%= Request.QueryString["item"] %> Status</a>
                  </h4>
                </div>
                <div id="collapse1" class="panel-collapse collapse in">
                  <div class="panel-body">
                      <asp:Literal
                            ID='LiteralGo' 
                            runat="server"  
                            Text=" ">
                      </asp:Literal>
                      <form id="form1" runat="server">
                        
                        <div class="form-group col-md-12">
                            <label for="email">Bug ID:</label>
                            <asp:TextBox ID="bugId" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-12">
                            <label for="email">Error Message(Short):</label>
                            <asp:TextBox ID="errm" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-12">
                            <label for="email">New Status:</label><br />
                            <asp:DropDownList CssClass="form-control" ID="newStat" runat="server"
                                  AppendDataBoundItems="true">
                            <asp:ListItem Value="-1">Select</asp:ListItem>
                            <asp:ListItem Value="-1">Fixed</asp:ListItem>
                            <asp:ListItem Value="-1">Removed</asp:ListItem>
                            
                            </asp:DropDownList>
                        </div>
                        <div class="form-group col-md-12">
                            <label for="email">Tracker:</label>
                            <asp:TextBox ID="trNo" CssClass="form-control" runat="server"></asp:TextBox>
                            <small>Enter Related Bug BugCode Or Generate New BugCode For this Bug</small>
                        </div>
                        <div class="form-group col-md-12">
                            <br />
                            <asp:Button ID="Button1" CssClass="btn btn-primary col-lg-12" runat="server" Text="Update Bug Status" OnClick="Button1_Click" />
                        </div>
                      </form>
                  </div>
                  
                </div>
              </div>
            </div>
        </div>
    </div>
    <div><br /></div>
    <div class="col-md-12">
        <div class="row">
            <div class="panel-group">
              <div class="panel panel-default">
                <div class="panel-heading">
                  <h4 class="panel-title">
                    <a data-toggle="collapse" href="#">Bug Track History</a>
                  </h4>
                </div>
                <div id="collapse2" class="panel-collapse collapse in">
                  <div class="panel-body">
                      <asp:Literal
                            ID='LiteralText' 
                            runat="server"  
                            Text=" ">
                      </asp:Literal>
                  </div>
                </div>
              </div>
            </div>
        </div>
    </div>
</asp:Content>
