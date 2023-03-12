CREATE TABLE food_history.tag
(
    recipe_id integer NOT NULL,
    text character varying(20) NOT NULL,
    CONSTRAINT "tag_pkey" PRIMARY KEY (recipe_id, text),
    CONSTRAINT "tag_UK" UNIQUE (recipe_id, text),
    CONSTRAINT "recipe_FK" FOREIGN KEY (recipe_id)
        REFERENCES food_history.recipe (id) MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
)