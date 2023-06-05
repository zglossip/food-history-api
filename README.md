# food-history-api (WIP)
A REST API that will be used to service an upcoming recipe catalog/food diary application

NOTE: In order to run this locally, you will need a file named `connectionsettings.json` in the root directory. This should contain several properties to connect to a database with a schema set up by the provided SQL queries in `/SQLTableDefinitions`.

| Property | Description |
| --- | --- |
| `datasource` | The URL of the data source |
| `database` | The name of the database containing the schema |
| `username` | The username to access the database |
| `password` | The password to access the database |

Eventually, a database with this setup may be publically avaliable, but I don't currently have the resources to host it.

## API

### Models

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
| `uom` | The name of the unit of measurement (UOM) for the ingredient (i.e. c, tbs, ml)
| `notes` | Any notes applied to the ingredient |

### Endpoints

#### `GET /recipe/{id}`

Response: Recipe
