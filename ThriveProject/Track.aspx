<%@ Page Title="Track" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Track.aspx.cs" Inherits="Track" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <title>Track</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <hgroup class="title">
        <asp:Label ID="lblCurrentAction" runat="server" Text="Track Food" Font-Bold="True" Font-Size="Large"></asp:Label>
    </hgroup>
    <asp:SqlDataSource ID="sqlDSAccess" runat="server" ConnectionString='<%$ ConnectionStrings:USDAFoods %>' ProviderName='<%$ ConnectionStrings:USDAFoods.ProviderName %>' SelectCommand="SELECT [NDB_No] AS FoodId, [Shrt_Desc] AS Name, [GmWt_Desc1] as ServingSize, [Energ_Kcal] AS Calories FROM [ABBREV] WHERE ([Shrt_Desc] LIKE '%' + ? + '%') ORDER BY [Shrt_Desc]">
        <SelectParameters>
            <asp:ControlParameter ControlID="tbFood" PropertyName="Text" Name="Shrt_Desc" Type="String"></asp:ControlParameter>
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlDSLocal" runat="server" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' DeleteCommand="DeleteFood" InsertCommand="CreateFood" SelectCommand="QueryFoods" UpdateCommand="UpdateFood" SelectCommandType="StoredProcedure" DeleteCommandType="StoredProcedure" InsertCommandType="StoredProcedure" UpdateCommandType="StoredProcedure" DataSourceMode="DataSet">
        <DeleteParameters>
            <asp:Parameter Name="FoodId" Type="Int32"></asp:Parameter>
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Name" Type="String"></asp:Parameter>
            <asp:Parameter Name="Calories" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="IsRestaurant" Type="Boolean"></asp:Parameter>
            <asp:Parameter Name="Categories" Type="String"></asp:Parameter>
            <asp:Parameter Name="ServingSize" Type="String"></asp:Parameter>
            <asp:Parameter Name="Return" Type="Int32" Direction="ReturnValue"></asp:Parameter>
        </InsertParameters>
        <SelectParameters>
            <asp:Parameter Name="Name" Type="String"></asp:Parameter>
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="FoodId" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="UserId" Type="Object"></asp:Parameter>
            <asp:Parameter Name="Name" Type="String"></asp:Parameter>
            <asp:Parameter Name="Calories" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="IsRestaurant" Type="Boolean"></asp:Parameter>
            <asp:Parameter Name="Categories" Type="String"></asp:Parameter>
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsMeals" runat="server" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' DeleteCommand="DELETE FROM [Meals] WHERE [MealId] = @MealId" InsertCommand="INSERT INTO [Meals] ([UserId], [Name], [TotalCalories], [Time], [Foods], [Servings]) VALUES (@UserId, @Name, @TotalCalories, @Time, @Foods, @Servings)" SelectCommand="QueryMeals" UpdateCommand="UPDATE [Meals] SET [UserId] = @UserId, [Name] = @Name, [TotalCalories] = @TotalCalories, [Time] = @Time, [Foods] = @Foods, [Servings] = @Servings WHERE [MealId] = @MealId" SelectCommandType="StoredProcedure">
        <DeleteParameters>
            <asp:Parameter Name="MealId" Type="Int32"></asp:Parameter>
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Name" Type="String"></asp:Parameter>
            <asp:Parameter Name="TotalCalories" Type="Double"></asp:Parameter>
            <asp:Parameter Name="Foods" Type="String"></asp:Parameter>
            <asp:Parameter Name="Servings" Type="String"></asp:Parameter>
        </InsertParameters>
        <SelectParameters>
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="Name" Type="String"></asp:Parameter>
            <asp:Parameter Name="TotalCalories" Type="Double"></asp:Parameter>
            <asp:Parameter Name="Foods" Type="String"></asp:Parameter>
            <asp:Parameter Name="Servings" Type="String"></asp:Parameter>
            <asp:Parameter Name="MealId" Type="Int32"></asp:Parameter>
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlDSExercises" runat="server" ConnectionString='<%$ ConnectionStrings:ExercisesConnectionString %>' ProviderName='<%$ ConnectionStrings:ExercisesConnectionString.ProviderName %>' SelectCommand="SELECT * FROM [Exercises] WHERE ([Description] LIKE '%' + ? + '%') ORDER BY [Description]">
        <SelectParameters>
            <asp:Parameter Name="Description" Type="String"></asp:Parameter>
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlDSLocalExercise" runat="server" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' DeleteCommand="DeleteExercise" DeleteCommandType="StoredProcedure" InsertCommand="CreateExercise" InsertCommandType="StoredProcedure" SelectCommand="QueryExercises" SelectCommandType="StoredProcedure" UpdateCommand="UpdateExercise" UpdateCommandType="StoredProcedure">
        <DeleteParameters>
            <asp:Parameter Name="ExerciseId" Type="Int32"></asp:Parameter>
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="UserId" Type="Object"></asp:Parameter>
            <asp:Parameter Name="Name" Type="String"></asp:Parameter>
            <asp:Parameter Name="CaloriesBurned" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="IsAerobic" Type="Boolean"></asp:Parameter>
            <asp:Parameter Name="Categories" Type="String"></asp:Parameter>
        </InsertParameters>
        <SelectParameters>
            <asp:Parameter Name="Name" Type="String"></asp:Parameter>
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="ExerciseId" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="UserId" Type="Object"></asp:Parameter>
            <asp:Parameter Name="Name" Type="String"></asp:Parameter>
            <asp:Parameter Name="CaloriesBurned" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="IsAerobic" Type="Boolean"></asp:Parameter>
            <asp:Parameter Name="Categories" Type="String"></asp:Parameter>
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlDSWorkouts" runat="server" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' DeleteCommand="DeleteWorkout" DeleteCommandType="StoredProcedure" InsertCommand="CreateWorkout" InsertCommandType="StoredProcedure" SelectCommand="GetWorkout" SelectCommandType="StoredProcedure" UpdateCommand="UpdateWorkout" UpdateCommandType="StoredProcedure">
        <DeleteParameters>
            <asp:Parameter Name="workoutID" Type="Int32"></asp:Parameter>
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="name" Type="String"></asp:Parameter>
            <asp:Parameter Name="totalCalories" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="exercises" Type="String"></asp:Parameter>
            <asp:Parameter Name="exerciseTimes" Type="String"></asp:Parameter>
        </InsertParameters>
        <SelectParameters>
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="workoutId" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="name" Type="String"></asp:Parameter>
            <asp:Parameter Name="totalCalories" Type="Double"></asp:Parameter>
            <asp:Parameter Name="exercises" Type="String"></asp:Parameter>
            <asp:Parameter Name="exerciseTimes" Type="String"></asp:Parameter>
        </UpdateParameters>
    </asp:SqlDataSource>
    <section id="track">
        <asp:Menu ID="trackMenu" Width="500px" runat="server" Orientation="Horizontal" StaticEnableDefaultPopOutImage="False" OnMenuItemClick="Menu1_MenuItemClick">
            <Items>
                <asp:MenuItem Text="Track Food" Value="0"></asp:MenuItem>
                <asp:MenuItem Text="Track Exercise" Value="1"></asp:MenuItem>
            </Items>
        </asp:Menu>
        <br />
        <br />
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="Tab1" runat="server">
                <asp:Button id="btnLessFoodDate" runat="server" Text="<" OnClick="btnLessFoodDate_Click"/>
                <asp:Label id="lblFoodDate" runat="server" Text=""></asp:Label>
                <asp:Button id="btnMoreFoodDate" runat="server" Text=">" OnClick="btnMoreFoodDate_Click"/>
                <br />
                <br />
                <asp:Panel ID="pnlSearchFood" runat="server" DefaultButton="btnSearchFood">
                    <asp:DropDownList ID="ddlSearchType" runat="server">
                        <asp:ListItem Value="name">By Name</asp:ListItem>
                        <asp:ListItem Value="type">By Type</asp:ListItem>
                        <asp:ListItem Value="category">By Category</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="tbFood" runat="server" ></asp:TextBox>
                    <asp:Button ID="btnSearchFood" runat="server" Text="Search" OnClick="btnSearchFood_Click" />
                </asp:Panel>
                <br />
                <asp:Panel ID="pnlFoodError" runat="server" Visible="false">
                    <asp:Label ID="lblError" runat="server" Visible="true" Text="The food you are looking for does not exist."></asp:Label><asp:LinkButton ID="lbtnCreate" runat="server" visble="false" Text="Create New Food?" OnClick="lbtnCreate_Click" Visible="true"></asp:LinkButton>
                </asp:Panel>
                <br />
                <section id="createFood">
                    <asp:Panel ID="pnlCreateFood" runat="server" Visible="false">
                        <h3>Create Food</h3>
                        <asp:Label ID="lblFoodName" runat="server" Text="Food Name: "></asp:Label>
                        <asp:TextBox ID="tbFoodName" runat="server"></asp:TextBox>
                        <asp:Label ID="lblCalories" runat="server" Text="Calories: "></asp:Label>
                        <asp:TextBox ID="tbCalories" runat="server"></asp:TextBox>
                        <asp:Label ID="lblCategories" runat="server" Text="Categories (seperated by a ,): "></asp:Label>
                        <asp:TextBox ID="tbCategories" runat="server"></asp:TextBox>
                        <asp:Label ID="lblIsRestaurant" runat="server" Text="Is Restaurant?: "></asp:Label>
                        <asp:CheckBox ID="cbIsRestaurant" runat="server" />
                        <asp:Label ID="lblServingSize" runat="server" Text="Serving Size: "></asp:Label>
                        <asp:TextBox ID="tbServingSize" runat="server"></asp:TextBox>
                        <asp:Label ID="lblServingsEaten" runat="server" Text="Servings Eaten: "></asp:Label>
                        <asp:TextBox ID="tbServingsEaten" runat="server"></asp:TextBox>
                        <asp:Label ID="lblMealToCreateIn" runat="server" Text="Meal Eaten In: "></asp:Label>
                        <asp:TextBox ID="tbMealEatenIn" runat="server"></asp:TextBox>
                        <asp:Button ID="btnCreateFood" runat="server" Text="Create" OnClick="btnCreateFood_Click"/>
                    </asp:Panel>
                </section>
                <asp:UpdatePanel ID="upnlFood" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvResults" runat="server" AutoGenerateSelectButton="true" AllowPaging="true" OnPageIndexChanging="gvResults_PageIndexChanging" OnSelectedIndexChanged="gvResults_SelectedIndexChanged" OnRowDataBound="gvResults_RowDataBound"></asp:GridView>
                        <br />
                        <section id="selectedFood">
                            <asp:Panel ID="pnlAddFood" runat="server" Visible="false" DefaultButton="btnAddFood">
                                <asp:Label ID="lblFoodDesc" runat="server" Text="" ></asp:Label><br />
                                <asp:Label ID="lblServings" runat="server" Text="How much did you eat?"></asp:Label>
                                <asp:TextBox ID="tbServings" runat="server" ></asp:TextBox> 
                                <asp:Label ID="lblMealName" runat="server" Text="Meal Name"></asp:Label>
                                <asp:TextBox ID="tbEnterMealName" runat="server"></asp:TextBox>
                                <asp:Button ID="btnAddFood" runat="server" Text="Add Food" OnClick="btnAddFood_Click"/>
                            </asp:Panel>
                            <asp:GridView ID="gvTodayMeals" runat="server"></asp:GridView>
                        </section>
                        </ContentTemplate>
                </asp:UpdatePanel>
            </asp:View>
            <asp:View ID="Tab2" runat="server">
            <asp:Button id="btnLessExerciseDate" runat="server" Text="<" OnClick="btnLessExerciseDate_Click"/>
                <asp:Label id="lblExerciseDate" runat="server" Text=""></asp:Label>
                <asp:Button id="btnMoreExerciseDate" runat="server" Text=">" OnClick="btnMoreExerciseDate_Click"/>
                <br />
                <br />
                <asp:Panel ID="searchExercise" runat="server" DefaultButton="btnSearchExercise">
                    <asp:DropDownList ID="ddlSearchTypeExercise" runat="server">
                        <asp:ListItem Value="name">By Name</asp:ListItem>
                        <asp:ListItem Value="type">By Type</asp:ListItem>
                        <asp:ListItem Value="category">By Category</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="tbExercise" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearchExercise" runat="server" Text="Search" OnClick="btnSearchExercise_Click"/>
                </asp:Panel>
                <asp:Panel ID="pnlExerciseError" runat="server" Visible="false">
                    <asp:Label ID="lblExerciseError" runat="server" Text="The exercise you are looking for does not exist."></asp:Label>
                    <asp:LinkButton ID="lbtnCreateExercise" runat="server" Text="Create Exercise?" OnClick="lbtnCreateExercise_Click"></asp:LinkButton>
                </asp:Panel>
                <asp:UpdatePanel ID="upnlExercise" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvExerciseResults" runat="server" AutoGenerateSelectButton="true" AllowPaging="true" OnPageIndexChanging="gvExerciseResults_PageIndexChanging" OnSelectedIndexChanged="gvExerciseResults_SelectedIndexChanged" OnRowDataBound="gvExerciseResults_RowDataBound"></asp:GridView>
                        <br />
                        <asp:Panel ID="pnlCreateExercise" runat="server">
                            <h3>Create Exercise</h3>
                            <asp:Label ID="lblSetExerciseName" runat="server" Text="Exercise Name: "></asp:Label>
                            <asp:TextBox ID="tbSetExerciseName" runat="server" ></asp:TextBox>
                            <asp:Label ID="lblSetCaloriesBurned" runat="server" Text="Calories Burned: "></asp:Label>
                            <asp:TextBox ID="tbSetCaloriesBurned" runat="server" Text="Text"></asp:TextBox>
                            <asp:Label ID="lblSetExerciseCategories" runat="server" Text="Categories (seperated by a ,): "></asp:Label>
                            <asp:TextBox ID="tbSetExerciseCategories" runat="server"></asp:TextBox>
                            <asp:Label ID="lblSetExerciseType" runat="server" Text="Exercise Type: "></asp:Label>
                            <asp:TextBox ID="tbSetExerciseType" runat="server"></asp:TextBox>
                            <asp:Label ID="lblSetExerciseHours" runat="server" Text="Hours Exercised: "></asp:Label>
                            <asp:TextBox ID="tbSetExerciseHours" runat="server"></asp:TextBox>
                            <asp:Label ID="lblSetExerciseMinutes" runat="server" Text="Minutes Exercised: "></asp:Label>
                            <asp:TextBox ID="tbSetExerciseMinutes" runat="server"></asp:TextBox>
                            <asp:Label ID="lblSetWorkoutName" runat="server" Text="Workout Name: "></asp:Label>
                            <asp:TextBox ID="tbSetWorkoutName" runat="server"></asp:TextBox>
                            <asp:Button ID="btnCreateExercise" runat="server" Text="Create Exercise" OnClick="btnCreateExercise_Click"/>
                        </asp:Panel>
                        <asp:Panel ID="pnlAddExercise" runat="server" Visible="false" DefaultButton="">
                            <asp:Label ID="lblExerciseDesc" runat="server" Text="" ></asp:Label><br />
                            <asp:Label ID="lblExerciseTime" runat="server" Text="How long did you exercise?"></asp:Label><br />
                            <asp:Label ID="lblExerciseHours" runat="server" Text="Hours: "></asp:Label>
                            <asp:TextBox ID="tbExerciseHours" runat="server"></asp:TextBox>
                            <asp:Label ID="lblExerciseMinutes" runat="server" Text="Minutes: "></asp:Label>
                            <asp:TextBox ID="tbExerciseMinutes" runat="server"></asp:TextBox>
                            <asp:Label ID="lblWorkoutName" runat="server" Text="Workout Name"></asp:Label>
                            <asp:TextBox ID="tbWorkoutoutName" runat="server"></asp:TextBox>
                            <asp:Button ID="btnAddExercise" runat="server" Text="Add Food" OnClick="btnAddExercise_Click"/>
                        </asp:Panel>
                        <asp:GridView ID="gvTodayWorkouts" runat="server"></asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:View>
        </asp:MultiView>
    </section>
</asp:Content>

