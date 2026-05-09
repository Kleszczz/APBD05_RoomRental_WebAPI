using APBD05_RoomRental_WebAPI.Models;

namespace APBD05_RoomRental_WebAPI;

public static class Data
{
    //Dane są wygenerwaone na podstawie modeli, modele sa autorskie zgodnie z poleceniem.
    public static List<Room> Rooms { get; } = new List<Room>
    {
        new Room {Name = "Aula A", BuildingCode = "A", Floor = 0, Capacity = 100, HasProjector = true, IsActive = true },
        new Room {Name = "Lab 204", BuildingCode = "B", Floor = 2, Capacity = 24, HasProjector = true, IsActive = true },
        new Room {Name = "Conference Room C1", BuildingCode = "C", Floor = 1, Capacity = 15, HasProjector = false, IsActive = true },
        new Room {Name = "Workshop D3", BuildingCode = "D", Floor = 3, Capacity = 30, HasProjector = true, IsActive = false },
        new Room {Name = "Lecture Hall B2", BuildingCode = "B", Floor = 2, Capacity = 60, HasProjector = true, IsActive = true }
    };

    public static List<Reservation> Reservations { get; } = new List<Reservation>
    {
        new Reservation {RoomId = 1, OrganizerName = "Jan Nowak", Topic = "Intro to programming", Date = new DateOnly(2026, 5, 10), StartTime = new TimeOnly(8, 0), EndTime = new TimeOnly(10, 0), Status = "confirmed" },
        new Reservation {RoomId = 2, OrganizerName = "Anna Kowalska", Topic = "HTTP workshops", Date = new DateOnly(2026, 5, 10), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(12, 30), Status = "confirmed" },
        new Reservation {RoomId = 3, OrganizerName = "Marek Wisniewski", Topic = "Team meeting", Date = new DateOnly(2026, 5, 12), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(16, 0), Status = "planned" },
        new Reservation {RoomId = 1, OrganizerName = "Katarzyna Zajac", Topic = "Database exam", Date = new DateOnly(2026, 5, 15), StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(11, 0), Status = "planned" },
        new Reservation {RoomId = 5, OrganizerName = "Piotr Lewandowski", Topic = "Algorithms lecture", Date = new DateOnly(2026, 5, 8), StartTime = new TimeOnly(12, 0), EndTime = new TimeOnly(14, 0), Status = "cancelled" }
    };
    
}