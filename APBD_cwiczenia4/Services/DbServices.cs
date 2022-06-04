using APBD_cwiczenia4.Models;
using System.Data.SqlClient;

namespace APBD_cwiczenia4.Services
{
    public class DbServices
    {
        public interface IDatabaseService
        {
            IEnumerable<Animals> GetAnimals(string ordered);
            Animals AddAnimals(Animals animal);
            Animals UpdateAnimals(Animals animal, int idAnimal);
            void DeleteAnimals(int idAnimal);
            bool animalExists(int idAnimal);
        }
        public class SqlServerDatabaseService : IDatabaseService
        {
            private IConfiguration _configuration;

            public SqlServerDatabaseService(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public IEnumerable<Animals> GetAnimals(string ordered)
            {
                List<Animals> animals = new List<Animals>();
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
                {
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    con.Open();
                    if (string.IsNullOrEmpty(ordered))
                    {
                        com.CommandText = "Select * from Animal ORDER BY NAME ASC";
                    }
                    else if (ordered == "name" || ordered == "description" || ordered == "category" || ordered == "area")
                    {
                        com.CommandText = "Select * from Animal ORDER BY " + ordered;
                    }
                    SqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        Animals animal = new Animals
                        {
                            Name = dr["Name"].ToString(),
                            Description = dr["Description"].ToString(),
                            Category = dr["Category"].ToString(),
                            Area = dr["Area"].ToString()
                        };
                        animals.Add(animal);
                    }
                }
                return animals;
            }
            public Animals AddAnimals(Animals animal)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
                {
                    SqlCommand com = new SqlCommand();

                    com.Connection = con;
                    con.Open();
                    com.CommandText = "INSERT INTO ANIMAL([name], [description], [category], [area]) VALUES (@name, @description, @category, @area)";
                    com.Parameters.AddWithValue("@name", animal.Name.ToString());
                    com.Parameters.AddWithValue("@description", animal.Description.ToString());
                    com.Parameters.AddWithValue("@category", animal.Category.ToString());
                    com.Parameters.AddWithValue("@area", animal.Area.ToString());
                    SqlDataReader dr = com.ExecuteReader();
                }
                return animal;
            }
            public Animals UpdateAnimals(Animals animal, int idAnimal)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
                {
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    con.Open();
                    com.CommandText = "UPDATE Animal SET Name = @name, Description = @description, Category = @category, Area = @area WHERE idAnimal =@idAnimal";
                    com.Parameters.AddWithValue("@name", animal.Name.ToString());
                    com.Parameters.AddWithValue("@description", animal.Description.ToString());
                    com.Parameters.AddWithValue("@category", animal.Category.ToString());
                    com.Parameters.AddWithValue("@area", animal.Area.ToString());
                    com.Parameters.AddWithValue("@idAnimal", idAnimal);
                    SqlDataReader dr = com.ExecuteReader();
                }
                return animal;
            }
            public bool animalExists(int idAnimal)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
                {
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    con.Open();
                    com.CommandText = "SELECT COUNT(IdAnimal) FROM ANIMAL WHERE idAnimal =@idAnimal";
                    com.Parameters.AddWithValue("@idAnimal", idAnimal);
                    int exists = (int)com.ExecuteScalar();
                    if (exists == 0) return false; else return true;
                }
            }
            public void DeleteAnimals(int idAnimal)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
                {
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    con.Open();
                    com.CommandText = "DELETE FROM ANIMAL WHERE idAnimal =@idAnimal";
                    com.Parameters.AddWithValue("@idAnimal", idAnimal);
                    SqlDataReader dr = com.ExecuteReader();
                }
                return;
            }

            IEnumerable<Animals> IDatabaseService.GetAnimals(string ordered)
            {
                throw new NotImplementedException();
            }
       
        }
    
}
}
