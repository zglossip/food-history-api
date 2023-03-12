CREATE TABLE food_history.recipe
(
    id bigint NOT NULL DEFAULT nextval('food_history.recipe_id_seq'::regclass),
    name character varying(100) COLLATE pg_catalog."default" NOT NULL,
    serving_amount integer NOT NULL,
    serving_name character varying(20) COLLATE pg_catalog."default" NOT NULL,
    source character varying(100) COLLATE pg_catalog."default",
    CONSTRAINT recipe_pkey PRIMARY KEY (id)
)