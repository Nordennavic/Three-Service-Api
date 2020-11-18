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