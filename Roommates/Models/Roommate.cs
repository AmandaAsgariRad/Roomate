using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roommates.Models
{
    //C# representation of the Roomate table
    public class Roommate
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RentPortion { get; set; }
        public DateTime MovedInDate { get; set; }
        public Room room { get; set; }
    }
}
