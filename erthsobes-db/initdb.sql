CREATE TABLE public."__EFMigrationsHistory"
(
    "MigrationId" character varying(150) COLLATE pg_catalog."default" NOT NULL,
    "ProductVersion" character varying(32) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
)

TABLESPACE pg_default;

ALTER TABLE public."__EFMigrationsHistory"
    OWNER to orders;

INSERT INTO public."__EFMigrationsHistory"(
	"MigrationId", "ProductVersion")
	VALUES ('20201119223525_ordersMigration', '5.0.0');

CREATE TABLE "order_info" (
	"id" bigint NOT NULL,
	"product_id" uuid NOT NULL UNIQUE,
	"type" varchar NOT NULL,
	"cost" numeric NOT NULL,
	"phoneNumber" varchar NOT NULL,
	"email" varchar NOT NULL,
	"value" varchar NOT NULL,
	"attachment_id" bigint NOT NULL,
	CONSTRAINT "order_info_pk" PRIMARY KEY ("id")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "attachment" (
	"id" bigint NOT NULL UNIQUE,
	"hash" varchar NOT NULL UNIQUE
) WITH (
  OIDS=FALSE
);


ALTER TABLE "order_info" ADD CONSTRAINT "order_info_fk0" FOREIGN KEY ("attachment_id") REFERENCES "attachment"("id");





