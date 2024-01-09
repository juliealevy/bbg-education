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
- BbgProgram and BbgSession domains with CRU (no D yet)
- HAL/HATEOS responses
- First error handling in stored proc, rest in progress


## Coming soon
In progress, coming soon:
- Minimal APIs
- Logging
- Integration Tests
- Acceptance Tests
- More authentication (OAuth, refresh token)
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
                "method": "POST",
                "body": {
                    "firstName": "",
                    "lastName": "",
                    "email": "",
                    "password": ""
                }
            }
        ],
        "auth:login": [
            {
                "href": "api/auth/login",
                "templated": false,
                "method": "POST",
                "body": {
                    "email": "",
                    "password": ""
                }
            }
        ],
        "auth:logout": [
            {
                "href": "api/auth/logout/{username}",
                "templated": true,
                "method": "POST"
            }
        ],
        "program:get-all": [
            {
                "href": "api/programs",
                "templated": false,
                "method": "GET"
            }
        ],
        "program:get-by-id": [
            {
                "href": "api/programs/{programId}",
                "templated": true,
                "method": "GET"
            }
        ],
        "program:create": [
            {
                "href": "api/programs",
                "templated": false,
                "method": "POST",
                "body": {
                    "name": "",
                    "description": ""
                }
            }
        ],
        "program:update": [
            {
                "href": "api/programs/{programId}",
                "templated": true,
                "method": "PUT",
                "body": {
                    "name": "",
                    "description": ""
                }
            }
        ],
        "session:create": [
            {
                "href": "api/programs/{programId}/sessions",
                "templated": true,
                "method": "POST",
                "body": {
                    "name": "",
                    "description": "",
                    "startDate": "2023-09-13",
                    "endDate": "2023-12-20"
                }
            }
        ],
        "session:update": [
            {
                "href": "api/programs/{programId}/sessions/{sessionId}",
                "templated": true,
                "method": "PUT",
                "body": {
                    "name": "",
                    "description": "",
                    "startDate": "2023-09-13",
                    "endDate": "2023-12-20"
                }
            }
        ],
        "session:get-by-id": [
            {
                "href": "api/programs/{programId}/sessions/{sessionId}",
                "templated": true,
                "method": "GET"
            }
        ],
        "session:get-by-program-id": [
            {
                "href": "api/programs/{programId}/sessions",
                "templated": true,
                "method": "GET"
            }
        ],
        "session:get-all": [
            {
                "href": "api/sessions",
                "templated": false,
                "method": "GET"
            }
        ]
    }
}
```
