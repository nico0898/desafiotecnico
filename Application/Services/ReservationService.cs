using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _repository;
        private readonly int _roomCount = 3;

        public ReservationService(IReservationRepository repository)
        {
            _repository = repository;
        }

        public void AddReservation(Reservation reservation)
        {
            if (reservation.StartTime < new TimeOnly(9, 0) || reservation.EndTime > new TimeOnly(18, 0))
                throw new InvalidOperationException("La reserva debe estar entre las 9:00 y las 18:00.");

            if (reservation.EndTime <= reservation.StartTime)
                throw new InvalidOperationException("La hora de fin debe ser mayor que la hora de inicio.");

            var minGap = TimeSpan.FromMinutes(30);

            var sameDate = _repository.GetByDate(reservation.Date)
                .Where(r => r.RoomId == reservation.RoomId);

            foreach (var existing in sameDate)
            {
                if (!(reservation.EndTime <= existing.StartTime.Add(-minGap) ||
                    reservation.StartTime >= existing.EndTime.Add(minGap)))
                {
                    throw new InvalidOperationException("La reserva se superpone o no respeta el tiempo de acondicionamiento.");
                }
            }

            if (reservation.RoomId < 1 || reservation.RoomId > _roomCount)
                throw new InvalidOperationException("Sala inválida. Solo hay 3 salas disponibles.");

            _repository.Add(reservation);
        }

        public IEnumerable<Reservation> GetReservationsByDate(DateOnly date)
        {
            return _repository.GetByDate(date);
        }
    }
}