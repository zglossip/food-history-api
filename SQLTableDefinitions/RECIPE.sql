CREATE TABLE zglossip.recipe
(
    "ID" bigint NOT NULL DEFAULT nextval('zglossip."recipe_ID_seq"'::regclass),
    "NAME" character varying(100) NOT NULL,
    "SERVING_AMOUNT" integer NOT NULL,
    "SERVING_NAME" character varying(20) NOT NULL,
    "SOURCE" character varying(100),
    CONSTRAINT recipe_pkey PRIMARY KEY ("ID")
)