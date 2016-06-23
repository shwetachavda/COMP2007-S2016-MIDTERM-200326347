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
    public partial class TodoDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
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
            int TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

            // connect to EF
            using (TodoConnection db = new TodoConnection())
            {


                // populate a todo object instance with the todoID from the URL Parameter
                Todo updatedTodo = (from todo in db.Todos
                                          where todo.TodoID == TodoID
                                          select todo).FirstOrDefault();

                //map the todo properties to the form control
                if (updatedTodo != null)
                {
                    var compeleted = updatedTodo.Completed;
                    TodoNameTextBox.Text = updatedTodo.TodoName;
                    TodoNotesTextBox.Text = updatedTodo.TodoNotes;
                    CompletedCheckBox.Checked = Convert.ToBoolean( compeleted);

                }
            }

           
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/TodoList.aspx");
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            // connect to EF
            using (TodoConnection db = new TodoConnection())
            {
                //create a neew todo
                Todo newTodo = new Todo();
                int TodoID = 0;

                if (Request.QueryString.Count > 0)
                {
                    //get the todoID from url
                    TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

                    //get the current todo from DB
                    newTodo = (from todo in db.Todos
                               where todo.TodoID==TodoID
                               select todo).FirstOrDefault();
                }
                //add form data to the new todo record
                newTodo.TodoName = TodoNameTextBox.Text;
                newTodo.TodoNotes = TodoNotesTextBox.Text;

                newTodo.Completed = CompletedCheckBox.Checked;


                // use LINQ to ADO.NET to add / insert new todo into the database

                if (TodoID == 0)
                {
                    db.Todos.Add(newTodo);
                }


                //save changes
                db.SaveChanges();

                //redirect back to updated todo page
                Response.Redirect("~/TodoList.aspx");

            }
        }
    }
}