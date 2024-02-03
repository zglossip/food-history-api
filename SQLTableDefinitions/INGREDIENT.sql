CREATE TABLE food_history.ingredient
(
    recipe_id smallint NOT NULL,
    name character varying(20) NOT NULL,
    uom character varying(10),
    quantity numeric(10, 3) NOT NULL,
    notes character varying(150),
    PRIMARY KEY (recipe_id, name),
    CONSTRAINT "recipe_FK" FOREIGN KEY (recipe_id)
        REFERENCES food_history.recipe (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
);
