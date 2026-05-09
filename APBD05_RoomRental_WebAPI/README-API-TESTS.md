# Test Scenarios
## To są wygenerowane testy do ctrl+v dla swagger ktore myslalem ze przydadza sie podczas pisania kodu.

## RoomsController

### GET /api/rooms — all rooms
No parameters, just execute.

### GET /api/rooms/{id} — get by id (existing)
```
id: 1
```

### GET /api/rooms/{id} — get by id (test 404)
```
id: 999
```

### GET /api/rooms/building/{buildingCode} — get by building
```
buildingCode: B
```

### GET /api/rooms — filter by query string
```
minCapacity: 30
hasProjector: true
activeOnly: true
```

### POST /api/rooms — create new room
```json
{
  "name": "New Room E1",
  "buildingCode": "E",
  "floor": 1,
  "capacity": 20,
  "hasProjector": false,
  "isActive": true
}
```

### POST /api/rooms — invalid data (test 400)
```json
{
  "name": "",
  "buildingCode": "",
  "floor": 0,
  "capacity": -5,
  "hasProjector": false,
  "isActive": true
}
```

### PUT /api/rooms/{id} — update room
```
id: 6
```
```json
{
  "name": "Updated Room E1",
  "buildingCode": "E",
  "floor": 2,
  "capacity": 25,
  "hasProjector": true,
  "isActive": true
}
```

### DELETE /api/rooms/{id} — room with future reservations (test 409)
```
id: 1
```

### DELETE /api/rooms/{id} — delete room
```
id: 6
```

---

## ReservationsController

### GET /api/reservations — all reservations
No parameters, just execute.

### GET /api/reservations/{id} — get by id (existing)
```
id: 1
```

### GET /api/reservations/{id} — get by id (test 404)
```
id: 999
```

### GET /api/reservations — filter by query string
```
date: 2026-05-10
status: confirmed
```

### POST /api/reservations — create valid reservation
```json
{
  "roomId": 3,
  "organizerName": "Tomasz Kowalski",
  "topic": "New project meeting",
  "date": "2026-06-01",
  "startTime": "09:00:00",
  "endTime": "11:00:00",
  "status": "planned"
}
```

### POST /api/reservations — time conflict (test 409)
Run this AFTER the valid reservation above — same room, same day, overlapping hours.
```json
{
  "roomId": 3,
  "organizerName": "Anna Nowak",
  "topic": "Conflicting meeting",
  "date": "2026-06-01",
  "startTime": "10:00:00",
  "endTime": "12:00:00",
  "status": "planned"
}
```

### POST /api/reservations — inactive room (test 409)
Room id=4 has IsActive=false.
```json
{
  "roomId": 4,
  "organizerName": "Jan Kowalski",
  "topic": "Test inactive room",
  "date": "2026-07-01",
  "startTime": "10:00:00",
  "endTime": "12:00:00",
  "status": "planned"
}
```

### POST /api/reservations — non-existing room (test 404)
```json
{
  "roomId": 999,
  "organizerName": "Jan Kowalski",
  "topic": "Test non-existing room",
  "date": "2026-07-01",
  "startTime": "10:00:00",
  "endTime": "12:00:00",
  "status": "planned"
}
```

### POST /api/reservations — EndTime before StartTime (test 400)
```json
{
  "roomId": 1,
  "organizerName": "Jan Kowalski",
  "topic": "Test invalid time",
  "date": "2026-07-01",
  "startTime": "12:00:00",
  "endTime": "10:00:00",
  "status": "planned"
}
```

### PUT /api/reservations/{id} — update reservation
```
id: 3
```
```json
{
  "roomId": 3,
  "organizerName": "Marek Wisniewski",
  "topic": "Updated team meeting",
  "date": "2026-05-12",
  "startTime": "14:00:00",
  "endTime": "17:00:00",
  "status": "confirmed"
}
```

### DELETE /api/reservations/{id} — delete reservation
```
id: 5
```

### DELETE /api/reservations/{id} — non-existing reservation (test 404)
```
id: 999
```

---