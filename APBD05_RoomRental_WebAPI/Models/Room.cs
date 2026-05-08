using System.ComponentModel.DataAnnotations;

namespace APBD05_RoomRental_WebAPI.Models;

public class Room
{
    private static int _nextId = 1;

    public int Id { get; } = _nextId++;

    [Required(ErrorMessage = "Room name is required.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Building code is required.")]
    public string BuildingCode { get; set; } = string.Empty;

    public int Floor { get; set; }

    [Range(1, 500, ErrorMessage = "Room capacity must be between 1 and 500.")]
    public int Capacity { get; set; }

    public bool HasProjector { get; set; }

    public bool IsActive { get; set; }
}