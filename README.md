# Announce

## Contents
* [Getting Started](#getting-started)
* [API Endpoints](#api-endpoints)

## Getting Started

### Prerequisites

- .NET 8
- Docker
- Docker Compose

### Running the Application

1. Clone the repository to your local machine.
2. Navigate to the root directory of the project where the `docker-compose.yml` file is located.
3. Run the following command to start the application:

   ```
   docker-compose up -d
   ```

   This command will start two containers:
   - SQL Server database (`announce.db`)
   - Announce API (`announce.api`)

4. The API will be available at:
   - HTTPS: `https://localhost:7275`

### Accessing the Database

The SQL Server database is exposed on port 1400. You can connect to it using the following credentials:
- Server: `localhost,1400`
- User: `sa`
- Password: `Password1!`

## API Endpoints

### Get All Announcements

- **GET:** `/api/announcements`
- **Response Body:** List of `AnnouncementDto` objects
```C# 
[
    {
    "id": "00000000-0000-0000-0000-000000000000",
    "title": "string",
    "description": "string",
    "createdAt": "0001-01-01T00:00:00Z"
    }
]
```

### Get Announcement by ID

- **GET:** `/api/announcements/{id}`
- **Parameters:** 
  - `id` (Guid) - The ID of the announcement
- **Response Body:** `AnnouncementDto` object
```C# 
{
  "id": "00000000-0000-0000-0000-000000000000",
  "title": "string",
  "description": "string",
  "createdAt": "0001-01-01T00:00:00Z"
}
```

### Create Announcement

- **POST:** `/api/announcements`
- **Request Body:** `AddAnnouncementCommand` object
```C# 
{
  "title": "string",
  "description": "string"
}
```
- **Response Body:** Created `AnnouncementDto` object
```C# 
{
  "id": "00000000-0000-0000-0000-000000000000",
  "title": "string",
  "description": "string",
  "createdAt": "0001-01-01T00:00:00Z"
}
```

### Update Announcement

- **PUT:** `/api/announcements/{id}`
- **Parameters:**
  - `id` (Guid) - The ID of the announcement to update
- **Request Body:** `UpdateAnnouncementCommand` object
```C# 
{
 "id": "00000000-0000-0000-0000-000000000000",
  "title": "string",
  "description": "string"
}
```
- **Response Body:** No content (204) if successful

### Delete Announcement

- **DELETE:** `/api/announcements/{id}`
- **Parameters:**
  - `id` (Guid) - The ID of the announcement to delete
- **Response:** No content (204) if successful

### Get Similar Announcements

- **GET:** `/api/announcements/{id}/similar?count=3`
- **Parameters:**
  - `id` (Guid) - The ID of the reference announcement
  - `count` (int, optional) - The number of similar announcements to return (default: 3)
- **Response Body:** List of similar `AnnouncementDto` objects
```C# 
[
    {
    "id": "00000000-0000-0000-0000-000000000000",
    "title": "string",
    "description": "string",
    "createdAt": "0001-01-01T00:00:00Z"
    }
]
```
