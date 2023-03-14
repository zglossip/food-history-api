CREATE TABLE food_history.course
(
    recipe_id integer NOT NULL,
    text character varying(10) NOT NULL,
    CONSTRAINT "course_pkey" PRIMARY KEY (recipe_id, text),
    CONSTRAINT "recipe_FK" FOREIGN KEY (recipe_id)
        REFERENCES food_history.recipe (id) MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
)