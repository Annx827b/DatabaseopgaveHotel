using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DatabaseopgaveHotel
{
    class DBClient
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Hotel;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
       
        #region Hotel
        private int GetMaxHotelNo(SqlConnection connection)
        {
            Console.WriteLine("Calling -> GetMaxHotelNo")
            string queryStringMaxHotelNo = "SELECT  MAX(Hotel_No)  FROM DemoHotel";
            Console.WriteLine($"SQL applied: {queryStringMaxHotelNo}");

       
            SqlCommand command = new SqlCommand(queryStringMaxHotelNo, connection);
            SqlDataReader reader = command.ExecuteReader();

       
            int MaxHotel_No = 0;
            if (reader.Read())
            {
               
                MaxHotel_No = reader.GetInt32(0); 
            }
            reader.Close();

            Console.WriteLine($"Max hotel#: {MaxHotel_No}");
            Console.WriteLine();

            return MaxHotel_No;
        }

        private int DeleteHotel(SqlConnection connection, int hotel_no)
        {
            Console.WriteLine("Calling -> DeleteHotel");

            string deleteCommandString = $"DELETE FROM DemoHotel  WHERE Hotel_No = {hotel_no}";
            Console.WriteLine($"SQL applied: {deleteCommandString}");

            SqlCommand command = new SqlCommand(deleteCommandString, connection);
            Console.WriteLine($"Deleting hotel #{hotel_no}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

            return numberOfRowsAffected;
        }

        private int UpdateHotel(SqlConnection connection, Hotel hotel)
        {
            Console.WriteLine("Calling -> UpdateHotel");

            string updateCommandString = $"UPDATE DemoHotel SET Name='{hotel.Name}', Address='{hotel.Address}' WHERE Hotel_No = {hotel.Hotel_No}";
            Console.WriteLine($"SQL applied: {updateCommandString}");

            SqlCommand command = new SqlCommand(updateCommandString, connection);
            Console.WriteLine($"Updating hotel #{hotel.Hotel_No}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

            return numberOfRowsAffected;
        }

        private int InsertHotel(SqlConnection connection, Hotel hotel)
        {
            Console.WriteLine("Calling -> InsertHotel");

            string insertCommandString = $"INSERT INTO DemoHotel VALUES({hotel.Hotel_No}, '{hotel.Name}', '{hotel.Address}')";
            Console.WriteLine($"SQL applied: {insertCommandString}");

            SqlCommand command = new SqlCommand(insertCommandString, connection);

            Console.WriteLine($"Creating hotel #{hotel.Hotel_No}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

            return numberOfRowsAffected;
        }

        private List<Hotel> ListAllHotels(SqlConnection connection)
        {
            Console.WriteLine("Calling -> ListAllHotels");

            string queryStringAllHotels = "SELECT * FROM DemoHotel";
            Console.WriteLine($"SQL applied: {queryStringAllHotels}");

            SqlCommand command = new SqlCommand(queryStringAllHotels, connection);
            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine("Listing all hotels:");

            if (!reader.HasRows)
            {
                Console.WriteLine("No hotels in database");
                reader.Close();

                return null;
            }

            List<Hotel> hotels = new List<Hotel>();
            while (reader.Read())
            {
                Hotel nextHotel = new Hotel()
                {
                    Hotel_No = reader.GetInt32(0),
                    Name = reader.GetString(1),    
                    Address = reader.GetString(2)  
                };

                hotels.Add(nextHotel);
                Console.WriteLine(nextHotel);
            }

            reader.Close();
            Console.WriteLine();

            return hotels;
        }

        private Hotel GetHotel(SqlConnection connection, int hotel_no)
        {
            Console.WriteLine("Calling -> GetHotel");

            string queryStringOneHotel = $"SELECT * FROM DemoHotel WHERE hotel_no = {hotel_no}";
            Console.WriteLine($"SQL applied: {queryStringOneHotel}");

            SqlCommand command = new SqlCommand(queryStringOneHotel, connection);
            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine($"Finding hotel#: {hotel_no}");

            if (!reader.HasRows)
            {
                Console.WriteLine("No hotels in database");
                reader.Close();

                return null;
            }

            Hotel hotel = null;
            if (reader.Read())
            {
                hotel = new Hotel()
                {
                    Hotel_No = reader.GetInt32(0), 
                    Name = reader.GetString(1),   
                    Address = reader.GetString(2)  
                };

                Console.WriteLine(hotel);
            }

            reader.Close();
            Console.WriteLine();

            return hotel;
        }
        public void Start()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
              
                connection.Open();

                ListAllHotels(connection);

                Hotel newHotel = new Hotel()
                {
                    Hotel_No = GetMaxHotelNo(connection) + 1,
                    Name = "New Hotel",
                    Address = "Maglegaardsvej 2, 4000 Roskilde"
                };

                InsertHotel(connection, newHotel);

                ListAllHotels(connection);

                Hotel hotelToBeUpdated = GetHotel(connection, newHotel.Hotel_No);

                hotelToBeUpdated.Name += "(updated)";
                hotelToBeUpdated.Address += "(updated)";

                UpdateHotel(connection, hotelToBeUpdated);

                ListAllHotels(connection);

                Hotel hotelToBeDeleted = GetHotel(connection, hotelToBeUpdated.Hotel_No);

                DeleteHotel(connection, hotelToBeDeleted.Hotel_No);

                ListAllHotels(connection);
            }
        }

        #endregion

        #region Facility
        private int GetMaxFacilityNo(SqlConnection connection)
        {
            Console.WriteLine("Calling -> GetMaxFacilityNo");

            
            string queryStringMaxFacilityNo = "SELECT  MAX(Facility_No)  FROM DemoFacility";
            Console.WriteLine($"SQL applied: {queryStringMaxFacilityNo}");

           
            SqlCommand command = new SqlCommand(queryStringMaxFacilityNo, connection);
            SqlDataReader reader = command.ExecuteReader();

           
            int MaxFacility_No = 0;

            
            if (reader.Read())
            {
               
                MaxFacility_No = reader.GetInt32(0); 
            }

           
            reader.Close();

            Console.WriteLine($"Max Facility#: {MaxFacility_No}");
            Console.WriteLine();

           
            return MaxFacility_No;
        }

        private int DeleteFacility(SqlConnection connection, int Facility_no)
        {
            Console.WriteLine("Calling -> DeleteFacility");

            
            string deleteCommandString = $"DELETE FROM DemoFacility  WHERE Facility_No = {Facility_no}";
            Console.WriteLine($"SQL applied: {deleteCommandString}");

            
            SqlCommand command = new SqlCommand(deleteCommandString, connection);
            Console.WriteLine($"Deleting Facility #{Facility_no}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

            
            return numberOfRowsAffected;
        }

        private int UpdateFacility(SqlConnection connection, Facility Facility)
        {
            Console.WriteLine("Calling -> UpdateFacility");

            
            string updateCommandString = $"UPDATE DemoFacility SET Name='{Facility.Name}' WHERE Facility_No = {Facility.Facility_No}";
            Console.WriteLine($"SQL applied: {updateCommandString}");

            
            SqlCommand command = new SqlCommand(updateCommandString, connection);
            Console.WriteLine($"Updating Facility #{Facility.Facility_No}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

           
            return numberOfRowsAffected;
        }

        private int InsertFacility(SqlConnection connection, Facility Facility)
        {
            Console.WriteLine("Calling -> InsertFacility");

            
            string insertCommandString = $"INSERT INTO DemoFacility VALUES({Facility.Facility_No}, '{Facility.Name}')";
            Console.WriteLine($"SQL applied: {insertCommandString}");

            SqlCommand command = new SqlCommand(insertCommandString, connection);

            Console.WriteLine($"Creating Facility #{Facility.Facility_No}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

            return numberOfRowsAffected;
        }

        private List<Facility> ListAllFacilitys(SqlConnection connection)
        {
            Console.WriteLine("Calling -> ListAllFacilitys");

            
            string queryStringAllFacilitys = "SELECT * FROM DemoFacility";
            Console.WriteLine($"SQL applied: {queryStringAllFacilitys}");

           
            SqlCommand command = new SqlCommand(queryStringAllFacilitys, connection);
            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine("Listing all Facilitys in Database:");

            if (!reader.HasRows)
            {
                
                Console.WriteLine("No Facilitys were found in the database");
                reader.Close();

                
                return null;
            }

          
            List<Facility> Facilitys = new List<Facility>();
            while (reader.Read())
            {
                
                Facility nextFacility = new Facility()
                {
                    Facility_No = reader.GetInt32(0), 
                    Name = reader.GetString(1),    
                };

               
                Facilitys.Add(nextFacility);

                Console.WriteLine(nextFacility);
            }

       
            reader.Close();
            Console.WriteLine();

            
            return Facilitys;
        }

        private Facility GetFacility(SqlConnection connection, int Facility_no)
        {
            Console.WriteLine("Calling -> GetFacility");

          
            string queryStringOneFacility = $"SELECT * FROM DemoFacility WHERE Facility_no = {Facility_no}";
            Console.WriteLine($"SQL applied: {queryStringOneFacility}");

           
            SqlCommand command = new SqlCommand(queryStringOneFacility, connection);
            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine($"Finding Facility#: {Facility_no}");

           
            if (!reader.HasRows)
            { 
                Console.WriteLine("No Facilities were found in the database");
                reader.Close();

                return null;
            }

            
            Facility Facility = null;
            if (reader.Read())
            {
                Facility = new Facility()
                {
                    Facility_No = reader.GetInt32(0), 
                    Name = reader.GetString(1),  
                };

                Console.WriteLine(Facility);
            }

            reader.Close();
            Console.WriteLine();

           
            return Facility;
        }
        public void StartFacility()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
             
                connection.Open();

                ListAllFacilitys(connection);
                Facility newFacility = new Facility()
                {
                    Facility_No = GetMaxFacilityNo(connection) + 1,
                    Name = "Diskotek"
                };

                InsertFacility(connection, newFacility);
                ListAllFacilitys(connection);

                Facility facilityToBeUpdated = GetFacility(connection, newFacility.Facility_No);
                facilityToBeUpdated.Name += "(updated)";
                UpdateFacility(connection, facilityToBeUpdated);
        
                ListAllFacilitys(connection);
                Facility facilityToBeDeleted = GetFacility(connection, facilityToBeUpdated.Facility_No);
                DeleteFacility(connection, facilityToBeDeleted.Facility_No);

                ListAllFacilitys(connection);
            }
        }
        #endregion

    }
}
