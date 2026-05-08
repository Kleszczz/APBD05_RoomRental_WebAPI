using Microsoft.AspNetCore.Mvc;

namespace APBD05_RoomRental_WebAPI.Controllers;

[Route("api/[controller]")] //do zmiany
[ApiController]
public class ReservationsController : ControllerBase
{
    
    
    
    
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