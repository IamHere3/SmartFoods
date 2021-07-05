using System;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using SmartFoods.DataObjects;
using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;

namespace SmartFoods
{
    class DBManager
    {
        // static MobileServiceClient client = new MobileServiceClient("https://smartfooduos.azurewebsites.net");
        /// <summary>
        /// this will return a SQLDataReader which will hold the data needed by the project. A using System.Data.SqlClient statement will be required
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>Data from database</returns>
        public static List<Recipe> SelectAllRecipes(int difficulty)
        {
            List<Recipe> recipes = new List<Recipe>();
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "smartfooduos.database.windows.net";
                builder.UserID = "UOS@smartfooduos";
                builder.Password = "Smartfood1";
                builder.InitialCatalog = "Smartfood";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT id, eng_name, itl_name, description, diff_rating, img_loc, prep_time, itl_description ");
                    sb.Append("FROM recipe ");
                    sb.Append("Where diff_rating <= " + difficulty + ";");
                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Recipe recipe = new Recipe();
                                recipe.Id = reader.GetInt32(0);
                                recipe.EngName = reader.GetString(1);
                                recipe.ItlName = reader.GetString(2);
                                recipe.Description = reader.GetString(3);
                                recipe.Difficulty = reader.GetInt32(4);
                                recipe.ImgLoc = reader.GetString(5);
                                recipe.PrepTime = reader.GetInt32(6);
                                recipe.ItlDescription = reader.GetString(7);
                                recipes.Add(recipe);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                // it broke
            }

            return recipes;
        }
        /// <summary>
        /// Selects the recipe ingredients
        /// </summary>
        /// <param name="recipeId"> the recipes Id</param>
        /// <returns></returns>
        public static List<RecipeIngredient> SelectRecipeIngredients(int recipeId)
        {
            List<RecipeIngredient> ingredients = new List<RecipeIngredient>();
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "smartfooduos.database.windows.net";
                builder.UserID = "UOS@smartfooduos";
                builder.Password = "Smartfood1";
                builder.InitialCatalog = "Smartfood";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("select recipe_ingredient.id, ingredient.eng_name as 'eng_name', ingredient.itl_name as 'itl_name', quantity, unit ");
                    sb.Append("from recipe_ingredient ");
                    sb.Append("join ingredient on recipe_ingredient.ingredient_id = ingredient.id ");
                    sb.Append("where recipe_ingredient.recipe_id = " + recipeId + ";"); // limit search to 1 recipe
                    string sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RecipeIngredient ingredient = new RecipeIngredient();
                                ingredient.Id = reader.GetInt32(0);
                                ingredient.EngName = reader.GetString(1);
                                ingredient.ItlName = reader.GetString(2);
                                ingredient.Quantity = reader.GetDecimal(3);
                                ingredient.Unit = reader.GetString(4);
                                ingredients.Add(ingredient);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                // it broke
            }

            return ingredients;
        }

        /// <summary>
        /// Selects the Recipes Utensils
        /// </summary>
        /// <param name="recipeId"> the recipes Id </param>
        /// <returns></returns>
        public static List<RecipeUtensil> SelectRecipeUtensils(int recipeId)
        {
            List<RecipeUtensil> utensils = new List<RecipeUtensil>();
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "smartfooduos.database.windows.net";
                builder.UserID = "UOS@smartfooduos";
                builder.Password = "Smartfood1";
                builder.InitialCatalog = "Smartfood";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("select recipe_utensil.id, utensil.eng_name as 'eng_name', utensil.itl_name as 'itl_name' ");
                    sb.Append("from recipe_utensil ");
                    sb.Append("join utensil on recipe_utensil.utensil_id = utensil.id ");
                    sb.Append("where recipe_utensil.recipe_id = " + recipeId + ";");
                    string sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RecipeUtensil utensil = new RecipeUtensil();
                                utensil.Id = reader.GetInt32(0);
                                utensil.EngName = reader.GetString(1);
                                utensil.ItlName = reader.GetString(2);
                                utensils.Add(utensil);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                // it broke
            }

            return utensils;
        }

        /// <summary>
        /// Get ingreients for dropdown box
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public static List<Ingredient> SelectIngredients(int accountId)
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "smartfooduos.database.windows.net";
                builder.UserID = "UOS@smartfooduos";
                builder.Password = "Smartfood1";
                builder.InitialCatalog = "Smartfood";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("select id, eng_name, itl_name from ingredient ");
                    sb.Append("where id not in (select ingredient_id from account_ingredient ");
                    sb.Append("where account_id = " + accountId + ");");
                    string sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Ingredient ingredient = new Ingredient();
                                ingredient.Id = reader.GetInt32(0);
                                ingredient.EngName = reader.GetString(1);
                                ingredient.ItlName = reader.GetString(2);
                                ingredients.Add(ingredient);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                // it broke
            }

            return ingredients;
        }

        /// <summary>
        /// use to add ingredients to account ingredients
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="ingredientId"></param>
        /// <param name="quantity"></param>
        /// <param name="unit"></param>
        public static void AddIngredientToAccount(int accountId, int ingredientId, decimal quantity, string unit)
        {
            List<RecipeUtensil> utensils = new List<RecipeUtensil>();
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "smartfooduos.database.windows.net";
                builder.UserID = "UOS@smartfooduos";
                builder.Password = "Smartfood1";
                builder.InitialCatalog = "Smartfood";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("insert into account_ingredient (id, account_id, ingredient_id, quantity, unit) ");
                    sb.Append("values(((select isnull(max(id), 0) from account_ingredient) + 1), " + accountId + ", " + ingredientId + ", " + quantity + ", '" + unit + "');");
                    string sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // probably faster way to do this
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException e)
            {
                // it broke
            }
        }

        /// <summary>
        /// get list of ingredients which user has
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public static List<AccountIngredient> SelectAccountIngredients(int accountId)
        {
            List<AccountIngredient> ingredients = new List<AccountIngredient>();
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "smartfooduos.database.windows.net";
                builder.UserID = "UOS@smartfooduos";
                builder.Password = "Smartfood1";
                builder.InitialCatalog = "Smartfood";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("select ingredient.eng_name as 'eng_name', ingredient.itl_name as 'itl_name', quantity, unit, ingredient_id ");
                    sb.Append("from account_ingredient join ingredient ");
                    sb.Append("on ingredient.id = account_ingredient.ingredient_id ");
                    sb.Append("where account_ingredient.account_id = " + accountId + ";");
                    string sql = sb.ToString();


                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AccountIngredient ingredient = new AccountIngredient();
                                ingredient.EngName = reader.GetString(0);
                                ingredient.ItlName = reader.GetString(1);
                                ingredient.Quantity = reader.GetDecimal(2);
                                ingredient.Unit = reader.GetString(3);
                                ingredient.IngredientId = reader.GetInt32(4);
                                ingredients.Add(ingredient);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                // it broke
            }

            return ingredients;
        }

        /// <summary>
        /// Removes the ingredients from the daabase
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="ingredientId"></param>
        public static void RemoveIngredientFromAccount(int accountId, int ingredientId)
        {
            List<RecipeUtensil> utensils = new List<RecipeUtensil>();
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "smartfooduos.database.windows.net"; 
                builder.UserID = "UOS@smartfooduos"; 
                builder.Password = "Smartfood1"; 
                builder.InitialCatalog = "Smartfood";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("delete account_ingredient ");
                    sb.Append("where id in (select id from account_ingredient ");
                    sb.Append("where account_id = " + accountId + " and ingredient_id = " + ingredientId + ");");
                    string sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // probably better way to do this
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException e)
            {
                // it broke
            }
        }

        /// <summary>
        /// Adds favorited recipt to account 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="recipeId"></param>
        public static void AddFavouriteToAccount(int accountId, int recipeId)
        {
            List<RecipeUtensil> utensils = new List<RecipeUtensil>();
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "smartfooduos.database.windows.net";
                builder.UserID = "UOS@smartfooduos";
                builder.Password = "Smartfood1";
                builder.InitialCatalog = "Smartfood";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("insert into favourite (id, account_id, recipe_id) ");
                    sb.Append("values(((SELECT ISNULL(MAX(id), 0) FROM favourite) + 1), " + accountId + ", " + recipeId + "); ");
                    string sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // probably faster way to do this
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException e)
            {
                // it broke
            }
        }

        /// <summary>
        /// Removes recepie from favourite account
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="recipeId"></param>
        public static void RemoveFavouriteFromAccount(int accountId, int recipeId)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "smartfooduos.database.windows.net";
                builder.UserID = "UOS@smartfooduos";
                builder.Password = "Smartfood1";
                builder.InitialCatalog = "Smartfood";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("delete favourite ");
                    sb.Append("where id in (select id from favourite ");
                    sb.Append("where account_id = " + accountId + " and recipe_id = " + recipeId + ");");
                    string sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException e)
            {
                // it broke
            }
        }

        /// <summary>
        /// Returns if the recipies state (if it is favriouted or not)
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        public static bool IsFavourite(int accountId, int recipeId)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "smartfooduos.database.windows.net";
                builder.UserID = "UOS@smartfooduos";
                builder.Password = "Smartfood1";
                builder.InitialCatalog = "Smartfood";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("select recipe_id ");
                    sb.Append("from favourite ");
                    sb.Append("where account_id = " + accountId + ";");
                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader.GetInt32(0) == recipeId)
                                {
                                    // is a favourite
                                    return true;
                                }
                            }
                        }
                    }
                }
                return false;
            }
            catch (SqlException e)
            {
                // it broke
                return false;
            }
        }

        /// <summary>
        /// returns favourite recipes 
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public static List<Recipe> SelectFavouriteRecipes(int accountId)
        {
            List<Recipe> recipes = new List<Recipe>();
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "smartfooduos.database.windows.net";
                builder.UserID = "UOS@smartfooduos";
                builder.Password = "Smartfood1";
                builder.InitialCatalog = "Smartfood";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("select id, eng_name, itl_name, description, diff_rating, img_loc, prep_time, itl_description ");
                    sb.Append("from recipe ");
                    sb.Append("where id in (select recipe_id ");
                    sb.Append("from favourite ");
                    sb.Append("where account_id = " + accountId + ");");
                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Recipe recipe = new Recipe();
                                recipe.Id = reader.GetInt32(0);
                                recipe.EngName = reader.GetString(1);
                                recipe.ItlName = reader.GetString(2);
                                recipe.Description = reader.GetString(3);
                                recipe.Difficulty = reader.GetInt32(4);
                                recipe.ImgLoc = reader.GetString(5);
                                recipe.PrepTime = reader.GetInt32(6);
                                recipe.ItlDescription = reader.GetString(7);
                                recipes.Add(recipe);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                // it broke
            }

            return recipes;
        }

        /// <summary>
        /// returns if utenisils are selected or not
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="untensilId"></param>
        /// <returns></returns>
        public static bool IsUtensilSelected(int accountId, int untensilId)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "smartfooduos.database.windows.net";
                builder.UserID = "UOS@smartfooduos";
                builder.Password = "Smartfood1";
                builder.InitialCatalog = "Smartfood";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("select utensil_id ");
                    sb.Append("from account_utensil ");
                    sb.Append("where account_id = " + accountId + ";");
                    string sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader.GetInt32(0) == untensilId)
                                {
                                    // is a favourite
                                    return true;
                                }
                            }
                        }
                    }
                }
                return false;
            }
            catch (SqlException e)
            {
                // it broke
                return false;
            }


        }

        /// <summary>
        /// Removes the item from the users account
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="utensilId"></param>
        public static void RemoveUtensilFromAccount(int accountId, int utensilId)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "smartfooduos.database.windows.net";
                builder.UserID = "UOS@smartfooduos";
                builder.Password = "Smartfood1";
                builder.InitialCatalog = "Smartfood";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("delete account_utensil ");
                    sb.Append("where id in (select id from account_utensil ");
                    sb.Append("where account_id = " + accountId + " and utensil_id = " + utensilId + ");");
                    string sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException e)
            {
                // it broke
            }
        }


        /// <summary>
        /// Adds utensiles to the users account
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="utensilId"></param>
        public static void AddUtensilToAccount(int accountId, int utensilId)
        {
            List<RecipeUtensil> utensils = new List<RecipeUtensil>();
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "smartfooduos.database.windows.net";
                builder.UserID = "UOS@smartfooduos";
                builder.Password = "Smartfood1";
                builder.InitialCatalog = "Smartfood";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("insert into account_utensil (id, account_id, utensil_id) ");
                    sb.Append("values(((SELECT ISNULL(MAX(id), 0) FROM account_utensil) + 1), " + accountId + ", " + utensilId + "); ");
                    string sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // probably faster way to do this
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException e)
            {
                // it broke
            }
        }
    }
}