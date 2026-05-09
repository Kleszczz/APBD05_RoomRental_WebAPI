using APBD05_RoomRental_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD05_RoomRental_WebAPI.Controllers;


[Route("api/[controller]")] //do zmiany
[ApiController]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllReservations(
        [FromQuery] DateOnly? date,
        [FromQuery] string? status,
        [FromQuery] int? roomId
    )
    {
        var reservations = Data.Reservations;

        if (date.HasValue)
        {
            reservations = reservations.Where(r => r.Date == date.Value).ToList();
        }

        if (status != null)
        {
            reservations = reservations.Where(r => r.Status.Equals(status)).ToList();
        }

        if (roomId.HasValue)
        {
            reservations = reservations.Where(r => r.RoomId == roomId.Value).ToList();
        }

        return Ok(reservations);
    }
    
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var reservation = Data.Reservations.FirstOrDefault(r => r.Id == id);

        if (reservation == null)
        {
            return NotFound();
        }

        return Ok(reservation);
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] Reservation reservation)
    {
        var room = Data.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);
        
        if (room is null)
            return NotFound();

        if (!room.IsActive)
            return Conflict();

        if (reservation.EndTime <= reservation.StartTime)
            return BadRequest();
        
        var hasTimeConflict = Data.Reservations
            .Where(r => r.RoomId == reservation.RoomId && r.Date == reservation.Date && r.Status == "planned")
            .Any(r => r.EndTime >= reservation.StartTime && r.StartTime <= reservation.EndTime);

        if (hasTimeConflict)
        {
            return Conflict();
        }
        
        Data.Reservations.Add(reservation);

        return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation);
    }
    
    //PUT /api/reservations/{id} Aktualizuje istniejącą rezerwację.
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] Reservation reservationUpdates)
    {
        var reservation = Data.Reservations.FirstOrDefault(r => r.Id == id);

        if (reservation == null) {
            return NotFound();
        }
        
        var room = Data.Rooms.FirstOrDefault(r => r.Id == reservationUpdates.RoomId);
        
        if (room == null) {
            return NotFound();
        }

        if (!room.IsActive) {
            return Conflict();
        }

        if (reservationUpdates.EndTime <= reservationUpdates.StartTime) {
            return BadRequest();
        }
        
        var hasTimeConflict = Data.Reservations
            .Where(r => r.RoomId == reservationUpdates.RoomId && r.Date == reservationUpdates.Date && r.Status == "planned" && r.Id != id)
            .Any(r => reservationUpdates.StartTime < r.EndTime && reservationUpdates.EndTime > r.StartTime);

        if (hasTimeConflict)
        {
            return Conflict();
        }
        
        reservation.RoomId = reservationUpdates.RoomId;
        reservation.OrganizerName = reservationUpdates.OrganizerName;
        reservation.Topic = reservationUpdates.Topic;
        reservation.Date = reservationUpdates.Date;
        reservation.StartTime = reservationUpdates.StartTime;
        reservation.EndTime = reservationUpdates.EndTime;
        reservation.Status = reservationUpdates.Status;

        return Ok(reservation);
    }
    
    
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var reservation = Data.Reservations.FirstOrDefault(r => r.Id == id);

        if (reservation == null)
            return NotFound();

        Data.Reservations.Remove(reservation);
        return NoContent();
    }
}