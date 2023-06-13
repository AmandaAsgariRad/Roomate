using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Roommates.Models;

namespace Roommates.Repositories
{
    public class RoommateRepository: BaseRepository
    {
        public RoommateRepository(string connectionString) : base(connectionString) { }

        public Roommate GetById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT FirstName, RentPortion, Name
                                      FROM Roommate
                                      JOIN Room ON Roommate.RoomId = Room.Id
                                      WHERE Roommate.Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Roommate roomMate = null;

                        // If we only expect a single row back from the database, we don't need a while loop.
                        if (reader.Read())
                        {
                            roomMate = new Roommate
                            {
                                Id = id,
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                RentPortion = reader.GetInt32(reader.GetOrdinal("RentPortion")),
                                
                                room = new Room()
                                {
                                    Name = reader.GetString(reader.GetOrdinal("Name"))

                                }

                                
                            };
                        }
                        return roomMate;
                    }
                }
            }
        }

        

    }
}
