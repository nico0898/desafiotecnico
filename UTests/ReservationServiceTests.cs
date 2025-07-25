using Application.Services;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Repositories;

namespace UTests;

public class ReservationServiceTests
{
    private readonly IReservationRepository _repository;
    private readonly ReservationService _service;

    public ReservationServiceTests()
    {
        _repository = new ReservationRepository(); // InMemory
        _service = new ReservationService(_repository);
    }

    [Fact]
    public void Should_Add_Valid_Reservation()
    {
        var reservation = CreateReservation("09:00", "10:00");
        _service.AddReservation(reservation);
        var result = _repository.GetByDate(reservation.Date);
        Assert.Contains(result, r => r.CustomerName == reservation.CustomerName);
    }

    [Fact]
    public void Should_Throw_When_Overlapping()
    {
        var r1 = CreateReservation("10:00", "11:00");
        var r2 = CreateReservation("10:30", "11:30");

        _service.AddReservation(r1);
        var ex = Assert.Throws<InvalidOperationException>(() => _service.AddReservation(r2));
        Assert.Contains("superpone", ex.Message);
    }

    [Fact]
    public void Should_Throw_When_Less_Than_30_Min_Between_Reservations()
    {
        var r1 = CreateReservation("10:00", "11:00");
        var r2 = CreateReservation("11:15", "12:00"); // solo 15 minutos

        _service.AddReservation(r1);
        var ex = Assert.Throws<InvalidOperationException>(() => _service.AddReservation(r2));
        Assert.Contains("acondicionamiento", ex.Message);
    }

    [Fact]
    public void Should_Throw_When_EndTime_Before_StartTime()
    {
        var reservation = CreateReservation("11:00", "10:00");

        var ex = Assert.Throws<InvalidOperationException>(() => _service.AddReservation(reservation));
        Assert.Contains("mayor que la hora de inicio", ex.Message);
    }

    [Fact]
    public void Should_Throw_When_Time_Out_Of_Range()
    {
        var reservation = CreateReservation("08:00", "09:00");

        var ex = Assert.Throws<InvalidOperationException>(() => _service.AddReservation(reservation));
        Assert.Contains("entre las 9:00 y las 18:00", ex.Message);
    }

    [Fact]
    public void Should_Throw_When_Invalid_RoomId()
    {
        var reservation = CreateReservation("09:00", "10:00", roomId: 99);

        var ex = Assert.Throws<InvalidOperationException>(() => _service.AddReservation(reservation));
        Assert.Contains("Sala inválida", ex.Message);
    }

    [Fact]
    public void Should_Allow_Same_Time_In_Different_Rooms()
    {
        var r1 = CreateReservation("10:00", "11:00", roomId: 1);
        var r2 = CreateReservation("10:00", "11:00", roomId: 2);

        _service.AddReservation(r1);
        _service.AddReservation(r2);

        var result = _repository.GetByDate(r1.Date);
        Assert.Equal(2, result.Count());
    }

    private Reservation CreateReservation(string start, string end, int roomId = 1)
    {
        return new Reservation
        {
            RoomId = roomId,
            Date = DateOnly.FromDateTime(DateTime.Today),
            StartTime = TimeOnly.Parse(start),
            EndTime = TimeOnly.Parse(end),
            CustomerName = $"Test-{Guid.NewGuid()}"
        };
    }
}