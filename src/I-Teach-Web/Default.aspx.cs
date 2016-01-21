using I_Teach.ViewModels.CourseAdministrationDomain;
using I_Teach_Web.BackEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace I_Teach_Web
{
    public partial class _Default : Page
    {
        public Tuple<List<EvaluationGroup>, List<EvaluationItem>> CourseEvaluation
        {
            get
            {
                Tuple<List<EvaluationGroup>, List<EvaluationItem>> _CourseEvaluation =
                    ViewState["CourseEvaluation"] as Tuple<List<EvaluationGroup>, List<EvaluationItem>>;
                if (_CourseEvaluation == null)
                {
                    _CourseEvaluation = new Tuple<List<EvaluationGroup>, List<EvaluationItem>>(new List<EvaluationGroup>(), new List<EvaluationItem>());
                    ViewState.Add("CourseEvaluation", _CourseEvaluation);
                }
                return _CourseEvaluation;
            }
            set
            {
                ViewState["CourseEvaluation"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateCurrentCourses();
            }
        }

        private void PopulateCurrentCourses()
        {
            CurrentCourses.DataSource = PublicApi.ListCurrentCourses();
            CurrentCourses.DataBind();
        }

        protected void CurrentCourses_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            if (e.NewSelectedIndex >= 0)
            {
                var row = CurrentCourses.Rows[e.NewSelectedIndex];
                var courseNumber = row.FindControl("CourseNumber") as Label;
                CourseEvaluation = PublicApi.GetEvaluationComponents(courseNumber.Text);
                CourseEvaluationComponents.DataSource = CourseEvaluation.Item1;
                CourseEvaluationComponents.DataBind();
                CourseEvaluationItems.DataSource = CourseEvaluation.Item2;
                CourseEvaluationItems.DataBind();
                SaveCourseEvaluation.Enabled = true;

                CurrentCourses.SelectedIndex = e.NewSelectedIndex;
                e.Cancel = true;
            }
        }

        protected void AddCourse_Click(object sender, EventArgs e)
        {
            PublicApi.AddCourse(CourseNumber.Text, CourseName.Text);
            PopulateCurrentCourses();
            CourseNumber.Text = "";
            CourseName.Text = "";
        }

        protected void CourseEvaluationItems_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            EvaluationItem data = null;
            switch (e.CommandName)
            {
                case "Insert":
                    data = new EvaluationItem
                    {
                        Name = (e.Item.FindControl("ItemName") as TextBox).Text,
                        Weight = int.Parse((e.Item.FindControl("Weight") as TextBox).Text)
                    };
                    CourseEvaluation.Item2.Add(data);
                    break;
                case "Clear":
                    CourseEvaluationItems.EditIndex = -1;
                    break;
                case "Delete":
                    CourseEvaluation.Item2.RemoveAt(e.Item.DataItemIndex);
                    break;
                case "Edit":
                    CourseEvaluationItems.EditIndex = e.Item.DataItemIndex;
                    break;
                case "Update":
                    data = new EvaluationItem
                    {
                        Name = (e.Item.FindControl("ItemName") as TextBox).Text,
                        Weight = int.Parse((e.Item.FindControl("Weight") as TextBox).Text)
                    };
                    CourseEvaluation.Item2.ElementAt(e.Item.DataItemIndex).Name = data.Name;
                    CourseEvaluation.Item2.ElementAt(e.Item.DataItemIndex).Weight = data.Weight;
                    CourseEvaluationItems.EditIndex = -1;
                    break;
                case "Cancel":
                    (e.Item.FindControl("ItemName") as TextBox).Text = "";
                    (e.Item.FindControl("Weight") as TextBox).Text = "";
                    break;
            }
            CourseEvaluationItems.DataSource = CourseEvaluation.Item2;
            CourseEvaluationItems.DataBind();
            e.Handled = true;
        }

        protected void CourseEvaluationComponents_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            EvaluationGroup data = null;
            int mark;
            switch (e.CommandName)
            {
                case "Insert":
                    data = new EvaluationGroup();
                    data.Name = (e.Item.FindControl("GroupName") as TextBox).Text;
                    if (int.TryParse((e.Item.FindControl("PassMark") as TextBox).Text, out mark))
                        data.PassMark = mark;
                    else
                        data.PassMark = null;
                    CourseEvaluationComponents.EditIndex = -1;
                    CourseEvaluation.Item1.Add(data);
                    break;
                case "Clear":
                    CourseEvaluationComponents.EditIndex = -1;
                    break;
                case "Delete":
                    CourseEvaluation.Item1.RemoveAt(e.Item.DataItemIndex);
                    break;
                case "Edit":
                    CourseEvaluationComponents.EditIndex = e.Item.DataItemIndex;
                    break;
                case "Update":
                    CourseEvaluation.Item1.ElementAt(e.Item.DataItemIndex).Name = (e.Item.FindControl("GroupName") as TextBox).Text;
                    if (int.TryParse((e.Item.FindControl("PassMark") as TextBox).Text, out mark))
                        CourseEvaluation.Item1.ElementAt(e.Item.DataItemIndex).PassMark = mark;
                    else
                        CourseEvaluation.Item1.ElementAt(e.Item.DataItemIndex).PassMark = null;
                    CourseEvaluationComponents.EditIndex = -1;
                    break;
                case "Cancel":
                    (e.Item.FindControl("GroupName") as TextBox).Text = "";
                    (e.Item.FindControl("PassMark") as TextBox).Text = "";
                    break;
                case "Select":
                    CourseEvaluationComponents.SelectedIndex = e.Item.DataItemIndex;
                    break;
            }
            CourseEvaluationComponents.DataSource = CourseEvaluation.Item1;
            CourseEvaluationComponents.DataBind();
            e.Handled = true;
        }

        protected void GroupEvaluationItems_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            EvaluationItem data = null;
            switch (e.CommandName)
            {
                case "Insert":
                    data = new EvaluationItem
                    {
                        Name = (e.Item.FindControl("ItemName") as TextBox).Text,
                        Weight = int.Parse((e.Item.FindControl("Weight") as TextBox).Text)
                    };
                    CourseEvaluation.Item1[CourseEvaluationComponents.SelectedIndex].Items.Add(data);
                    break;
                case "Clear":
                    CourseEvaluationItems.EditIndex = -1;
                    break;
                case "Delete":
                    CourseEvaluation.Item1[CourseEvaluationComponents.SelectedIndex].Items.RemoveAt(e.Item.DataItemIndex);
                    break;
                case "Edit":
                    CourseEvaluationItems.EditIndex = e.Item.DataItemIndex;
                    break;
                case "Update":
                    data = new EvaluationItem
                    {
                        Name = (e.Item.FindControl("ItemName") as TextBox).Text,
                        Weight = int.Parse((e.Item.FindControl("Weight") as TextBox).Text)
                    };
                    CourseEvaluation.Item1[CourseEvaluationComponents.SelectedIndex].Items.ElementAt(e.Item.DataItemIndex).Name = data.Name;
                    CourseEvaluation.Item1[CourseEvaluationComponents.SelectedIndex].Items.ElementAt(e.Item.DataItemIndex).Weight = data.Weight;
                    CourseEvaluationItems.EditIndex = -1;
                    break;
                case "Cancel":
                    (e.Item.FindControl("ItemName") as TextBox).Text = "";
                    (e.Item.FindControl("Weight") as TextBox).Text = "";
                    break;
            }
            CourseEvaluationComponents.DataSource = CourseEvaluation.Item1;
            CourseEvaluationComponents.DataBind();
            e.Handled = true;
        }

        protected void SaveCourseEvaluation_Click(object sender, EventArgs e)
        {
            // TODO: Save the course evaluation.
            var courseNumber = (CurrentCourses.Rows[CurrentCourses.SelectedIndex].FindControl("CourseNumber") as Label).Text;
            PublicApi.SetEvaluationComponents(courseNumber, this.CourseEvaluation);

            // Clean up display
            CourseEvaluation = null;
            CourseEvaluationComponents.DataSource = null;
            CourseEvaluationComponents.DataBind();
            CourseEvaluationItems.DataSource = null;
            CourseEvaluationItems.DataBind();
            SaveCourseEvaluation.Enabled = false;

            CurrentCourses.SelectedIndex = -1;
        }
    }
}