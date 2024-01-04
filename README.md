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
    "version": "0.0.1",
    "_links": {
        "self": [
            {
                "href": "api",
                "templated": false,
                "method": "GET"
            }
        ],
        "auth:register": [
            {
                "href": "api/auth/register",
                "templated": false,
                "body": {
                    "firstName": "",
                    "lastName": "",
                    "email": "",
                    "password": ""
                },
                "method": "POST"
            }
        ],
        "auth:login": [
            {
                "href": "api/auth/login",
                "templated": false,
                "body": {
                    "email": "",
                    "password": ""
                },
                "method": "POST"
            }
        ],
        "programs:get-all": [
            {
                "href": "api/programs",
                "templated": false,
                "method": "GET"
            }
        ],
        "programs:get-by-id": [
            {
                "href": "api/programs/{programId}",
                "templated": true,
                "method": "GET"
            }
        ],
        "programs:create": [
            {
                "href": "api/programs",
                "templated": false,
                "body": {
                    "name": "",
                    "description": ""
                },
                "method": "POST"
            }
        ],
        "programs:update": [
            {
                "href": "api/programs",
                "templated": false,
                "body": {
                    "id": 123,
                    "name": "Updated Name",
                    "description": "Updated Description"
                },
                "method": "PUT"
            }
        ]
    }
}
```
