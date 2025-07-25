using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Reservation
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int RoomId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string CustomerName { get; set; } = string.Empty;
    }
}