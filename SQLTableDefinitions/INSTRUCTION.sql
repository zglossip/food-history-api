CREATE TABLE food_history.instruction
(
    recipe_id smallint NOT NULL,
    "position" smallint NOT NULL,
    text character varying(500) NOT NULL,
    PRIMARY KEY (recipe_id, "position"),
    CONSTRAINT "recipe_FK" FOREIGN KEY (recipe_id)
        REFERENCES food_history.recipe (id) MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
        NOT VALID
);