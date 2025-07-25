using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IReservationService
    {
        void AddReservation(Reservation reservation);
        IEnumerable<Reservation> GetReservationsByDate(DateOnly date);
    }
}