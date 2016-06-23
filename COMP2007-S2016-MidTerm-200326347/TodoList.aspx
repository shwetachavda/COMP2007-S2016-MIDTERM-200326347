<%@ Page Title="Todo List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TodoList.aspx.cs" Inherits="COMP2007_S2016_MidTerm_200326347.TodoList" %>
<%--
File Name: TodoDetails.aspx
Author Name:  Shweta Chavda(200326347)
Website Name: http://comp2007-s2016-midterm-200326347.azurewebsites.net
Description:  This page allows user to see all the todos
 @date: June 23, 2016
   --%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="container">
        <div class="row">
            <div class="col-md-offset-2 col-md-8">
                <h1>ToDo List</h1>
                <a href="TodoDetails.aspx" class="btn btn-success btn-sm"><i class="fa fa-plus"></i>Add Todo</a>

                <div>
                    <label for="PageSizeDropDownList">Records per Page: </label>
                    <asp:DropDownList ID="PageSizeDropDownList" runat="server"
                        AutoPostBack="true" CssClass="btn btn-default bt-sm dropdown-toggle"
                        OnSelectedIndexChanged="PageSizeDropDownList_SelectedIndexChanged">
                        <asp:ListItem Text="3" Value="3" />
                        <asp:ListItem Text="5" Value="5" />
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="All" Value="10000" />
                    </asp:DropDownList>
                </div>
                <div>
                    <asp:GridView runat="server" CssClass="table table-bordered table-striped table-hover" 
                        ID="TodoGridview" AutoGenerateColumns="False" OnRowDeleting="TodoGridview_RowDeleting" AllowPaging="True" AllowSorting="true"
                        OnSorting="TodoGridview_Sorting" OnRowDataBound="TodoGridview_RowDataBound"
                        PageSize="3" OnPageIndexChanging="TodoGridview_PageIndexChanging" PagerStyle-CssClass="pagination-ys">
                        <Columns>
                            <asp:BoundField DataField="TodoID" HeaderText="TodoID" Visible="true"/>
                            <asp:BoundField DataField="TodoName" HeaderText="Todo" Visible="true"/>
                             <asp:BoundField DataField="TodoNotes" HeaderText="Notes" Visible="true"/>
                            <asp:CheckBoxField Text="Completed" DataField="Completed" Visible="true" />
                                
                                 
                          <asp:HyperLinkField HeaderText="Edit" Text="<i class='fa fa-pencil-square-o fa-lg'></i> Edit" 
                            NavigateUrl="~/TodoDetails.aspx.cs" ControlStyle-CssClass="btn btn-primary btn-sm" runat="server" 
                              DataNavigateUrlFields="TodoID" DataNavigateUrlFormatString="TodoDetails.aspx?TodoID={0}"> 
<ControlStyle CssClass="btn btn-primary btn-sm"></ControlStyle>
                            </asp:HyperLinkField>
                              <asp:CommandField  HeaderText="Delete" DeleteText="<i class='fa fa-trash-o fa-lg'></i> Delete"
                            ShowDeleteButton="true" ButtonType="Link" ControlStyle-CssClass="btn btn-danger btn-sm" >  
<ControlStyle CssClass="btn btn-danger btn-sm"></ControlStyle>
                            </asp:CommandField>
                        </Columns>

<PagerStyle CssClass="pagination-ys"></PagerStyle>
                    </asp:GridView>
            
            
        </div>
    </div>

</asp:Content>
