# Food History API 
Version 0.1.0

A REST API that will be used to service an upcoming recipe catalog/food diary application

## Frontend

This API serves as the backend to the [Food History mobile application](https://github.com/zglossip/food-history-app)

To run the full application, you will need to clone and run both the backend and the frontend

In a future release, there may also be a webapp frontend

### Note about the Dockerfiles

There is a couple Dockerfiles included with this repository. This is primarily used by a Docker Compose config in the frontend repo.

## Instructions

### Live Development

* Ensure you have the [.NET 8.0 SDK](https://learn.microsoft.com/en-us/dotnet/core/install/linux-debian?tabs=dotnet8) installed
* In the terminal, run `dotnet run watch --urls=http://+:8080/`
* Navigate to `http://localhost:8080/swagger/index.html` to browse the API

### connectionsettings.json

In order to run this locally, you will need a file named `connectionsettings.json` in the root directory. This should contain several properties to connect to a database with a schema set up by the provided SQL queries in `/SQLTableDefinitions`.

| Property | Description |
| --- | --- |
| `host` | The host of the data source |
| `port` | The port of the data source |
| `database` | The name of the database containing the schema |
| `username` | The username to access the database |
| `password` | The password to access the database |

Eventually, a database with this setup may be publically avaliable, but I don't currently have the resources to host it.

## API

All endpoints are in application/json. The model objects are listed below the endpoints.

### Endpoints

#### GET /recipe

Fetches a list of recipes based on optional query params. Default returns all recipes.

- **Query Parameters:**
  - `course` (string): fetches only recipes that have this course. Can have multiple courses
  - `cuisine` (string): fetches only recipes that have this cuisine. Can have multiple cuisines
  - `tag` (string): fetches only recipes that have thist tag. Can have multiple tags
  - `sort` (string): property to sort the recipes on (options are `id` and `name`. Default `id`)
  - `reverse` (boolean): sets whether the sorting should be reversed. Default false
- **Response:**
  - \[Recipe\]

#### GET /recipe/{id}

Fetches a recipe based on its unique ID

- **Path Parameters:**
  - `id` (number): the recipe ID
- **Response:**
  - Recipe

#### GET /recipe/{id}/ingredients

Fetches a list of ingredients for a recipe

- **Path Parameters:**
  - `id` (number): the recipe ID
- **Response:**
  - IngredientList

#### GET /recipe/{id}/instructions

Fetches a list of instructions for a recipe

- **Path Parameters:**
  - `id` (number): the recipe ID
- **Response:**
  - InstructionList

#### POST /recipe

Creates a new recipe. Returns the same recipe with generated ID

- **Request:**
  - Recipe
- **Response:**
  - Recipe

#### PUT /recipe/{id}

Saves an existing recipe

- **Path Parameters:**
  - `id` (number): the recipe ID
- **Request:**
  - Recipe

#### PUT /recipe/{id}/ingredients

Saves a recipe's ingredients

- **Path Parameters:**
  - `id` (number): the recipe ID
- **Request:**
  - IngredientList

#### PUT /recipe/{id}/instructions

Saves a recipe's instructions

- **Path Parameters:**
  - `id` (number): the recipe ID
- **Request:**
  - InstructionList

#### DELETE /recipe/{id}

Deletes a recipe

- **Path Parameters:**
  - `id` (number): the recipe ID

## Models

**Recipe**

```JSON
{
  "id": 0,
  "link": "string",
  "name": "string",
  "courseTypes": ["string"],
  "cuisineType": ["string"],
  "tags": ["string"],
  "servingAmount": 0,
  "servingName": 0,
  "recipeSourceUrl": "string",
  "ingredients": "string",
  "instructions": "string"
}
```

| Property | Description |
| --- | --- |
| `id` | The unique ID for the recipe |
| `link` | The API link for the recipe |
| `name` | The name of the recipe |
| `courseTypes` | A list of strings representing the different courses the recipe can apply to (i.e. main, side, breakfast, snack) |
| `cuisineTypes` | A list of strings representing the different styles of cuisine the recipe can apply to (i.e. Italian, American, Indian) |
| `tags` | A list of string representing a list of miscellanious tags saved to the recipe |
| `servingAmount` | The number of servings the recipe makes |
| `servingName` | The unit of measurement for a serving of the recipe (i.e. serving, slice, sandwich) |
| `recipeSourceUrl` | A link to the external recipe site, if there is one |
| `ingredients` | The API link for the recipe's ingredients |
| `instructions` | The API link for the recipe's instructions |

**IngredientList**

```JSON
{
  "recipeId": 0,
  "recipe": "string",
  "ingredients": [
    {
      "name": "string",
      "quantity": 0.0,
      "uom": "string",
      "notes": "string"
    }
  ]
}
```

| Property | Description |
| --- | --- |
| `recipeId` | The unique ID of the recipe the ingredient list belongs to |
| `recipe` | The API link to the recipe the ingredient list belongs to |
| `ingredients` | The list of ingredients for the recipe (NOTE: See the properties below for the ingredient object) |
| --- | --- |
| `name` | The name of the ingredient |
| `quantity` | The number of units for the ingredient |
| `uom` | The name of the unit of measurement (UOM) for the ingredient (i.e. c, tbs, ml) |
| `notes` | Any notes applied to the ingredient |

**InstructionList**

```JSON
{
  "recipeId": 0,
  "recipe": "string",
  "instructions": ["string"]
}
```

| Property | Description |
| --- | --- |
| `recipeId` | The unique ID of the recipe the ingredient list belongs to |
| `recipe` | The API link to the recipe the ingredient list belongs to |
| `ingredients` | The ordered list of instructions for the recipe |

## Release History

### 0.1.0
This is the initial release of the application. This contains a basic CRUD api for recipes. Meant as a starting point for the application