CREATE TABLE food_history.ingredient
(
    name character varying NOT NULL,
    uom character varying,
    quantity integer NOT NULL,
    notes character varying(200),
    recipe_id integer NOT NULL,
    CONSTRAINT "ingredient_pkey" PRIMARY KEY (recipe_id, name),
    CONSTRAINT "recipe_FK" FOREIGN KEY (recipe_id)
        REFERENCES food_history.recipe (id) MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
)