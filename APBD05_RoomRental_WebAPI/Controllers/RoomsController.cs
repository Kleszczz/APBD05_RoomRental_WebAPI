using APBD05_RoomRental_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD05_RoomRental_WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllRooms(
        [FromQuery] int? minCapacity,
        [FromQuery] bool? hasProjector,
        [FromQuery] bool? activeOnly
    )
    {
        var rooms = Data.Rooms;

        if (minCapacity.HasValue)
        {
            rooms = rooms.Where(r => r.Capacity > minCapacity.Value).ToList();
        }

        if (hasProjector.HasValue)
        {
            rooms = rooms.Where(r => r.HasProjector == hasProjector.Value).ToList();
        }

        if (activeOnly.HasValue)
        {
            rooms = rooms.Where(r => r.IsActive == activeOnly.Value).ToList();
        }

        return Ok(rooms);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var room = Data.Rooms.FirstOrDefault(r => r.Id == id);

        if (room == null)
        {
            return NotFound();
        }

        return Ok(room);
    }


    [HttpGet("/building/{buildingCode}")]
    public IActionResult GetRoomsFromBuilding(string buildingCode)
    {
        var rooms = Data.Rooms.Where(r => r.BuildingCode.Equals(buildingCode)).ToList();

        return Ok(rooms);
    }


    [HttpPost]
    public IActionResult Create([FromBody] Room room)
    {
        Data.Rooms.Add(room);
        return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
    }


    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] Room roomUpdates)
    {
        var room = Data.Rooms.FirstOrDefault(r => r.Id == id);

        if (room == null)
        {
            return NotFound();
        }

        room.Name = roomUpdates.Name;
        room.BuildingCode = roomUpdates.BuildingCode;
        room.Floor = roomUpdates.Floor;
        room.Capacity = roomUpdates.Capacity;
        room.HasProjector = roomUpdates.HasProjector;
        room.IsActive = roomUpdates.IsActive;

        return Ok(room);
    }
    
    //DELETE /api/rooms/{id} Usuwa salę.
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var room = Data.Rooms.FirstOrDefault(r => r.Id == id);

        if (room == null)
        {
            return NotFound();
        }

        var hasFutureReservations = Data.Reservations
            .Any(r => r.RoomId == id
                      && r.Date >= DateOnly.FromDateTime(DateTime.Today)
                      && r.Status != "cancelled");

        if (hasFutureReservations)
        {
            Conflict();
        }
        
        Data.Rooms.Remove(room);
        return NoContent();
    }
}