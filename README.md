# bbg-education
This is a practice app to track the course make-up and student progress in programs offerred by the Berkshires Botanical Garden.
- Example:  Horticulture Certificate Program I  (which i am currently participating in and have to track myself on paper)

This is very much an in progress project - in current development.

## What's in it
Some sample code in C#/.NET Core to show the following:
- Clean Architecture concepts
- Vertical Slice
- Controller APIs
- Basic jwt authentication
- Dapper (no EF)
- Unit Testing
- Commands/Queries
- Repository Pattern
- Basic SQL tables, views, stored procs


## Coming soon
In progress, coming soon:
- HATEOS
- Minimal APIs
- Logging
- Integration Tests
- Acceptance Tests
- Another Domain:  BbgSession
- More authentication (OAuth, refresh token)
- Add more functionality, error handling, etc. to the stored procs
- Front End ??

## Discoverable API/REST calls

https://localhost:7270/api

```json
{
    "api": {
        "_links": [
            {
                "rel": "self",
                "href": "api",
                "method": "GET"
            },
            {
                "rel": "auth:register",
                "href": "api/auth/register",
                "method": "POST",
                "body": {
                    "firstName": "",
                    "lastName": "",
                    "email": "",
                    "password": ""
                }
            },
            {
                "rel": "auth:login",
                "href": "api/auth/login",
                "method": "POST",
                "body": {
                    "email": "",
                    "password": ""
                }
            },
            {
                "rel": "programs:get_all",
                "href": "api/programs",
                "method": "GET"
            },
            {
                "rel": "programs:get_by_id",
                "href": "api/programs/{programId}",
                "method": "GET"
            },
            {
                "rel": "programs:create",
                "href": "api/programs",
                "method": "POST",
                "body": {
                    "name": "",
                    "description": ""
                }
            },
            {
                "rel": "programs:update",
                "href": "api/programs",
                "method": "PUT",
                "body": {
                    "id": -123,
                    "name": "Updated Name",
                    "description": "Updated Description"
                }
            }
        ]
    },
    "version": "0.0.1"
}
```
