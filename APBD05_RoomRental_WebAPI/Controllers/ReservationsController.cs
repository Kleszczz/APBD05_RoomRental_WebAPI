using APBD05_RoomRental_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD05_RoomRental_WebAPI.Controllers;

/*
 Metoda Endpoint Opis
 okGET /api/reservations Zwraca wszystkie rezerwacje.
 GET /api/reservations/{id} Zwraca jedną rezerwację.
 okGET /api/reservations?date=2026-05-10&status=confirmed&roomId=2 Zwraca rezerwacje przefiltrowane po query stringu.
 POST /api/reservations Tworzy nową rezerwację.
 PUT /api/reservations/{id} Aktualizuje istniejącą rezerwację.
 okDELETE /api/reservations/{id} Usuwa rezerwację.

Najważniejsze. W zadaniu muszą pojawić się różne sposoby przekazywania danych:
 id i buildingCode z trasy, filtry z query stringa oraz dane obiektów z body żądania w formacie JSON.
 */

[Route("api/[controller]")] //do zmiany
[ApiController]
public class ReservationsController : ControllerBase
{
    //GET /api/reservations Zwraca wszystkie rezerwacje. 
    //GET /api/reservations?date=2026-05-10&status=confirmed&roomId=2 Zwraca rezerwacje przefiltrowane po query stringu.
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

    //GET /api/reservations/{id} Zwraca jedną rezerwację.
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

    //POST /api/reservations Tworzy nową rezerwację.
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
        
        //TODO: Sprawdzic rezerwacje tego pokoju w godzinach
        var hasTimeConflict = Data.Reservations;
        
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

        //TODO: Zrobic weryfikacje tutaj.
        var hasTimeConflict = Data.Reservations;
        
        
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
    
    /*
    // GET api/rooms
    [HttpGet]
    public IActionResult Get([FromQuery] int minCapacity = 0)
    {
        return Ok(rooms.Where(r => r.Capacity >= minCapacity));
    }

    // GET api/rooms/{id}
    [Route("{id}")]
    [HttpGet]
    public IActionResult GetById([FromRoute] int id)
    {
        //var room = rooms.FirstOrDefault(r => r.Id == id);

        if (false) //do zmiany
        {
            return NotFound();
        }

        return Ok(); //zwrocic cos w ok
    }

    // POST api/rooms { "name": "Room 4", "capacity": 7 }
    [HttpPost]
    public IActionResult Post([FromBody] CreateRoomDto createRoomDto)
    {
        var room = new Room()
        {
            Id = rooms.Count + 1,
            Name = createRoomDto.Name,
            Capacity = createRoomDto.Capacity
        };

        rooms.Add(room);

        return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
    }
    */
}