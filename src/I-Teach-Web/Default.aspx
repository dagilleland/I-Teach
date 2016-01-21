<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="I_Teach_Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>I-Teach &mdash; Instructor Tools</h1>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Manage Courses Domain</h2>
            <p>This domain is responsible for</p>
            <ul>
                <li>Adding/Retiring Courses</li>
                <li>Setting Course Evaluation Components</li>
                <li>Scheduling Course Offerings/Sections</li>
            </ul>

            <asp:TextBox runat="server" ID="CourseNumber" placeholder="Course #" />
            <asp:TextBox runat="server" ID="CourseName" placeholder="Course Name" />
            <asp:LinkButton ID="AddCourse" runat="server" OnClick="AddCourse_Click">Add Course</asp:LinkButton>
            <hr />
            <h4>Current Courses</h4>
            <asp:GridView ID="CurrentCourses" runat="server" DataKeyNames="Number"
                AutoGenerateColumns="false"
                OnSelectedIndexChanging="CurrentCourses_SelectedIndexChanging"
                ItemType="I_Teach.ViewModels.CourseAdministrationDomain.Course">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="CourseNumber" runat="server" Text="<%# Item.Number %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="CourseName" runat="server"><%# Item.Name %></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowSelectButton="True"></asp:CommandField>
                </Columns>
                <EmptyDataTemplate>There are no courses in the database.</EmptyDataTemplate>
            </asp:GridView>
            <hr />
            <h4>Course Evaluation Components</h4>
            <asp:LinkButton ID="SaveCourseEvaluation" runat="server" Enabled="false" OnClick="SaveCourseEvaluation_Click">Save Course Evaluation</asp:LinkButton>
            <h5>Component Groups</h5>
            <asp:ListView ID="CourseEvaluationComponents" runat="server"
                ItemType="I_Teach.ViewModels.CourseAdministrationDomain.EvaluationGroup"
                InsertItemPosition="FirstItem" OnItemCommand="CourseEvaluationComponents_ItemCommand">
                <LayoutTemplate>
                    <div id="itemPlaceholder" runat="server"></div>
                </LayoutTemplate>
                <ItemTemplate>
                    <div>
                        <asp:LinkButton ID="SelectItem" runat="server" CommandName="Select">
                            <asp:Label ID="GroupName" runat="server" placeholder="Group Name" Text="<%# Item.Name %>" />
                            <asp:Label ID="PassMark" runat="server" TextMode="Number" placeholder="Pass Mark" Text="<%# Item.PassMark %>" />
                        </asp:LinkButton><asp:LinkButton ID="EditItem" runat="server" CommandName="Edit">Edit</asp:LinkButton><asp:LinkButton ID="Delete" runat="server" CommandName="Delete">Delete</asp:LinkButton>
                    </div>
                </ItemTemplate>
                <EditItemTemplate>
                    <div>
                        <asp:TextBox ID="GroupName" runat="server" placeholder="Group Name" Text="<%# BindItem.Name %>" />
                        <asp:TextBox ID="PassMark" runat="server" TextMode="Number" placeholder="Pass Mark" Text="<%# BindItem.PassMark %>" />
                        <asp:LinkButton ID="UpdateItem" runat="server" CommandName="Update">Update</asp:LinkButton><asp:LinkButton ID="Cancel" runat="server" CommandName="Cancel">Cancel</asp:LinkButton>
                    </div>
                </EditItemTemplate>
                <SelectedItemTemplate>
                    <div>
                        <asp:Label ID="GroupName" runat="server" placeholder="Group Name" Text="<%# Item.Name %>" />
                        <asp:Label ID="PassMark" runat="server" TextMode="Number" placeholder="Pass Mark" Text="<%# Item.PassMark %>" />
                        <asp:LinkButton ID="EditItem" runat="server" CommandName="Edit">Edit</asp:LinkButton>
                        <blockquote>
                            <h6>Component Items</h6>
                            <asp:ListView ID="GroupEvaluationItems" runat="server"
                                DataSource="<%# Item.Items %>"
                                ItemType="I_Teach.ViewModels.CourseAdministrationDomain.EvaluationItem"
                                OnItemCommand="GroupEvaluationItems_ItemCommand"
                                InsertItemPosition="FirstItem">
                                <LayoutTemplate>
                                    <div id="itemPlaceholder" runat="server"></div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <div>
                                        <asp:TextBox ID="ItemName" runat="server" placeholder="Item Name" Enabled="false" Text="<%# Item.Name %>" />
                                        <asp:TextBox ID="Weight" runat="server" TextMode="Number" placeholder="Weight" Enabled="false" Text="<%# Item.Weight %>" />
                                        <asp:LinkButton ID="EditItem" runat="server" CommandName="Edit">Edit</asp:LinkButton><asp:LinkButton ID="Delete" runat="server" CommandName="Delete">Delete</asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div>
                                        <asp:TextBox ID="ItemName" runat="server" placeholder="Item Name" Text="<%# BindItem.Name %>" />
                                        <asp:TextBox ID="Weight" runat="server" TextMode="Number" placeholder="Weight" Text="<%# BindItem.Weight %>" />
                                        <asp:LinkButton ID="UpdateItem" runat="server" CommandName="Update">Update</asp:LinkButton><asp:LinkButton ID="Cancel" runat="server" CommandName="Cancel">Cancel</asp:LinkButton>
                                    </div>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <div>
                                        <asp:TextBox ID="ItemName" runat="server" placeholder="Item Name" Text="<%# BindItem.Name %>" />
                                        <asp:TextBox ID="Weight" runat="server" TextMode="Number" placeholder="Weight" Text="<%# BindItem.Weight %>" />
                                        <asp:LinkButton ID="AddItem" runat="server" CommandName="Insert">Add</asp:LinkButton><asp:LinkButton ID="Clear" runat="server" CommandName="Clear">Clear</asp:LinkButton>
                                    </div>
                                </InsertItemTemplate>
                            </asp:ListView>
                        </blockquote>
                    </div>
                </SelectedItemTemplate>
                <InsertItemTemplate>
                    <div>
                        <asp:TextBox ID="GroupName" runat="server" placeholder="Group Name" Text="<%# BindItem.Name %>" />
                        <asp:TextBox ID="PassMark" runat="server" TextMode="Number" placeholder="Pass Mark" Text="<%# BindItem.PassMark %>" />
                        <asp:LinkButton ID="AddItem" runat="server" CommandName="Insert">Add</asp:LinkButton><asp:LinkButton ID="Clear" runat="server" CommandName="Clear">Clear</asp:LinkButton>
                    </div>
                </InsertItemTemplate>
            </asp:ListView>
            <h5>Component Items</h5>
            <asp:ListView ID="CourseEvaluationItems" runat="server"
                ItemType="I_Teach.ViewModels.CourseAdministrationDomain.EvaluationItem"
                OnItemCommand="CourseEvaluationItems_ItemCommand"
                InsertItemPosition="FirstItem">
                <LayoutTemplate>
                    <div id="itemPlaceholder" runat="server"></div>
                </LayoutTemplate>
                <ItemTemplate>
                    <div>
                        <asp:TextBox ID="ItemName" runat="server" placeholder="Item Name" Enabled="false" Text="<%# Item.Name %>" />
                        <asp:TextBox ID="Weight" runat="server" TextMode="Number" placeholder="Weight" Enabled="false" Text="<%# Item.Weight %>" />
                        <asp:LinkButton ID="EditItem" runat="server" CommandName="Edit">Edit</asp:LinkButton>
                        <asp:LinkButton ID="Delete" runat="server" CommandName="Delete">Delete</asp:LinkButton>
                    </div>
                </ItemTemplate>
                <EditItemTemplate>
                    <div>
                        <asp:TextBox ID="ItemName" runat="server" placeholder="Item Name" Text="<%# BindItem.Name %>" />
                        <asp:TextBox ID="Weight" runat="server" TextMode="Number" placeholder="Weight" Text="<%# BindItem.Weight %>" />
                        <asp:LinkButton ID="UpdateItem" runat="server" CommandName="Update">Update</asp:LinkButton>
                        <asp:LinkButton ID="Cancel" runat="server" CommandName="Cancel">Cancel</asp:LinkButton>
                    </div>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <div>
                        <asp:TextBox ID="ItemName" runat="server" placeholder="Item Name" Text="<%# BindItem.Name %>" />
                        <asp:TextBox ID="Weight" runat="server" TextMode="Number" placeholder="Weight" Text="<%# BindItem.Weight %>" />
                        <asp:LinkButton ID="AddItem" runat="server" CommandName="Insert">Add</asp:LinkButton>
                        <asp:LinkButton ID="Clear" runat="server" CommandName="Clear">Clear</asp:LinkButton>
                    </div>
                </InsertItemTemplate>
            </asp:ListView>
        </div>
    <div class="col-md-4">
        <h2>Manage Sections</h2>
        <p>This domain is responsible for</p>
        <ul>
            <li>Adding/Removing/Transferring Students</li>
            <li>Adjusting Evaluation Components</li>
        </ul>
    </div>
    <div class="col-md-4">
        <h2>Student Marks</h2>
        <p>This domain is responsible for</p>
        <ul>
            <li>Entering/Correcting Student Marks</li>
        </ul>
    </div>
    </div>
    <hr />
    <div class="row">
        <div class="col-md-4">
        </div>
        <div class="col-md-4">
        </div>
        <div class="col-md-4">
        </div>
    </div>

</asp:Content>
