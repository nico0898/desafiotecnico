using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly List<Reservation> _reservations = new();

        public void Add(Reservation reservation)
        {
            _reservations.Add(reservation);
        }

        public IEnumerable<Reservation> GetByDate(DateOnly date)
        {
            return _reservations.Where(r => r.Date == date);
        }

        public IEnumerable<Reservation> GetAll() => _reservations;
    }
}