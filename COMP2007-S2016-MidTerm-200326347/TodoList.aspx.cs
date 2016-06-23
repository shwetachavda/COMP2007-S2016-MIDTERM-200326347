/*
 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//using statements required to connect to EF
using COMP2007_S2016_MidTerm_200326347.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;

namespace COMP2007_S2016_MidTerm_200326347
{
    public partial class TodoList : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Session["SortColumn"] = "TodoID";
                Session["SortDirection"] = "ASC";
                //get the TODO data
                this.GetTodo();
            }
        }

        /**
        * <summary>
        * This method gets the todo data from the DB
        * </summary>
        * 
        * @method GetTodo
        * @returns {void}
        */
        protected void GetTodo()
        {
            // connect to EF
            using (TodoConnection db = new TodoConnection())
            {
                string SortString = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();

                // query the TODO Table using EF and LINQ
                var todos = (from allTodos in db.Todos
                                select allTodos);

                // bind the result to the GridView
                TodoGridview.DataSource = todos.AsQueryable().OrderBy(SortString).ToList();
                TodoGridview.DataBind();
            }
        }

        protected void TodoGridview_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //store the row clicked
            int selectedRow = e.RowIndex;

            //get the selected todoID 
            int todoID = Convert.ToInt32(TodoGridview.DataKeys[selectedRow].Values["TodoID"]);

            //use EF to find selected todo and delete it
            using (TodoConnection db = new TodoConnection())
            {
                // create object of the Student class and store the query string inside of it
                Todo deletedTodo = (from todoRecords in db.Todos
                                          where todoRecords.TodoID == todoID
                                          select todoRecords).FirstOrDefault();

                // remove the selected student from the db
                db.Todos.Remove(deletedTodo);

                // save my changes back to the database
                db.SaveChanges();

                // refresh the grid
                this.GetTodo();
            }

        }

        protected void TodoGridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
             /**
         * <summary>
         * This event handler allows pagination to occur for the todo page
         * </summary>
         * 
         * @method TodoGridView_PageIndexChanging
         * @param {object} sender
         * @param {GridViewPageEventArgs} e
         * @returns {void}
         */
        
            // Set the new page number
            TodoGridview.PageIndex = e.NewPageIndex;

            // refresh the grid
            this.GetTodo();
        

    }

        protected void PageSizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //set the new page size
            TodoGridview.PageSize = Convert.ToInt32(PageSizeDropDownList.SelectedValue);

            this.GetTodo();
        }

        protected void TodoGridview_Sorting(object sender, GridViewSortEventArgs e)
        {
            Session["SortColumn"] = e.SortExpression;

            // Refresh the Grid
            this.GetTodo();

            // toggle the direction
            Session["SortDirection"] = Session["SortDirection"].ToString() == "ASC" ? "DESC" : "ASC";
        }

        protected void TodoGridview_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if(e.Row.RowType== DataControlRowType.Header)
                {
                    LinkButton linkbutton = new LinkButton();
                    for(int index=0; index < TodoGridview.Columns.Count - 1; index++)
                    {
                        if (TodoGridview.Columns[index].SortExpression == Session["SortCoulmn"].ToString())
                        {
                            if(Session["SortDirection"].ToString()=="ASC")
                            {
                                linkbutton.Text = "<i class='fa fa-caret-up fa-lg'></i>";
                            }
                            else
                            {
                                linkbutton.Text = " <i class='fa fa-caret-down fa-lg'></i>";
                            }
                            e.Row.Cells[index].Controls.Add(linkbutton);
                        }
                    }
                }
            }
        }
    }
}