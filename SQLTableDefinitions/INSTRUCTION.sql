CREATE TABLE food_history.instruction
(
    recipe_id integer NOT NULL,
    text character varying(500) NOT NULL,
    position integer NOT NULL,
    CONSTRAINT "instruction_pkey" PRIMARY KEY (recipe_id, text),
    CONSTRAINT "recipe_FK" FOREIGN KEY (recipe_id)
        REFERENCES food_history.recipe(id) MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
)