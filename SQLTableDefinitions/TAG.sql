CREATE TABLE zglossip."TAG"
(
    "RECIPE_ID" integer NOT NULL,
    "TEXT" character varying(20) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "TAG_pkey" PRIMARY KEY ("RECIPE_ID", "TEXT"),
    CONSTRAINT "TAG_UK" UNIQUE ("RECIPE_ID", "TEXT"),
    CONSTRAINT "RECIPE_FK" FOREIGN KEY ("RECIPE_ID")
        REFERENCES zglossip."RECIPE" ("ID") MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
)