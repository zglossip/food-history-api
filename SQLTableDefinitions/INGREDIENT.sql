CREATE TABLE zglossip."INGREDIENT"
(
    "NAME" character varying COLLATE NOT NULL,
    "UOM" character varying COLLATE,
    "QUANTITY" integer NOT NULL,
    "NOTES" character varying(200),
    "RECIPE_ID" integer NOT NULL,
    CONSTRAINT "INGREDIENT_pkey" PRIMARY KEY ("RECIPE_ID", "NAME"),
    CONSTRAINT "RECIPE_FK" FOREIGN KEY ("RECIPE_ID")
        REFERENCES zglossip."RECIPE" ("ID") MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
)