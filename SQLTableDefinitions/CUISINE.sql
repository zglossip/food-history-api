CREATE TABLE recipe_catalog.cuisine
(
    recipe_id smallint NOT NULL,
    text character varying(10) NOT NULL,
    PRIMARY KEY (recipe_id, text),
    CONSTRAINT "recipe_FK" FOREIGN KEY (recipe_id)
        REFERENCES recipe_catalog.recipe (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
);