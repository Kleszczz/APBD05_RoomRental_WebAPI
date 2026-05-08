using System.ComponentModel.DataAnnotations;

namespace APBD05_RoomRental_WebAPI.Models;

public class Reservation
{
    private static int _nextId = 1;

    public int Id { get; } = _nextId++;

    public int RoomId { get; set; }

    [Required(ErrorMessage = "Organizer name is required.")]
    public string OrganizerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Topic is required.")]
    public string Topic { get; set; } = string.Empty;

    public DateOnly Date { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    [Required]
    public string Status { get; set; } = "planned";
}