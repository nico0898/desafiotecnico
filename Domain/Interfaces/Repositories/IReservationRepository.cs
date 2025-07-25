using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IReservationRepository
    {
        void Add(Reservation reservation);
        IEnumerable<Reservation> GetByDate(DateOnly date);
        IEnumerable<Reservation> GetAll();
    }
}