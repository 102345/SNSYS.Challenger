

CREATE SEQUENCE customersupplier_sequence START 1;

CREATE SEQUENCE customersupplieraddress_sequence START 1;

CREATE SEQUENCE customersuppliercontact_sequence START 1;


CREATE TABLE IF NOT EXISTS public."Users"
(
    "Id" integer NOT NULL,
    "Name" character varying(100) COLLATE pg_catalog."default",
    "Password" character varying(50) COLLATE pg_catalog."default",
    CONSTRAINT "Users_pkey" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Users"
    OWNER to postgres;

CREATE TABLE public."CustomerSupplier"
(
    "Id" integer DEFAULT nextval('customersupplier_sequence') NOT NULL,
    "Name" character varying(50) NOT NULL,
    "Type" "char" NOT NULL,
    "DocumentNumber" character varying(25) NOT NULL,
    PRIMARY KEY ("Id")
);

ALTER TABLE IF EXISTS public."CustomerSupplier"
    OWNER to postgres;

CREATE TABLE IF NOT EXISTS public."CustomerSupplierAddress"
(
    "Id" integer NOT NULL DEFAULT nextval('customersupplieraddress_sequence'::regclass),
    "Address" character varying(100) COLLATE pg_catalog."default" NOT NULL,
    "City" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "ZIP" character varying(20) COLLATE pg_catalog."default" NOT NULL,
    "Country" character varying(25) COLLATE pg_catalog."default" NOT NULL,
    "CustomerSupplierId" integer,
    CONSTRAINT "CustomerSupplierAddress_pkey" PRIMARY KEY ("Id"),
    CONSTRAINT fk_customersupplieraddress_customersupplier FOREIGN KEY ("CustomerSupplierId")
        REFERENCES public."CustomerSupplier" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."CustomerSupplierAddress"
    OWNER to postgres;


CREATE TABLE IF NOT EXISTS public."CustomerSupplierContact"
(
    "Id" integer NOT NULL DEFAULT nextval('customersuppliercontact_sequence'::regclass),
    "Name" character varying(100) COLLATE pg_catalog."default" NOT NULL,
    "Email" character varying(50) COLLATE pg_catalog."default" NULL,
    "PhoneNumber" integer,
    "Position" character varying(25) COLLATE pg_catalog."default" NOT NULL,
	"Department" character varying(25) COLLATE pg_catalog."default" NOT NULL,
    "CustomerSupplierId" integer,
    CONSTRAINT "CustomerSupplierContact_pkey" PRIMARY KEY ("Id"),
    CONSTRAINT fk_customersuppliercontact_customersupplier FOREIGN KEY ("CustomerSupplierId")
        REFERENCES public."CustomerSupplier" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."CustomerSupplierContact"
    OWNER to postgres;

INSERT INTO public."Users"(
	"Id", "Name", "Password")
	VALUES (1, 'teste', '123');
	
INSERT INTO public."CustomerSupplier"(
	 "Name", "Type", "DocumentNumber")
	VALUES ('Coca Cola Company SA', 'S', '1234567');

INSERT INTO public."CustomerSupplierAddress"(
	"Address", "City", "ZIP", "Country", "CustomerSupplierId")
	VALUES ('Avenue Pemberton', 'Atlanta', 12345678, 'USA', 1);
	
	
INSERT INTO public."CustomerSupplierContact"(
	"Name", "Email", "PhoneNumber", "Position", "Department", "CustomerSupplierId")
	VALUES ('Carl Peterson','carl.peterson@cocacolacompany.com',01400423078, 'Sales Manager', 'Sales',1);
	
INSERT INTO public."CustomerSupplierContact"(
	"Name", "Email", "PhoneNumber", "Position", "Department", "CustomerSupplierId")
	VALUES ('Victoria Smith Alvarez','victoria.alvarez@cocacolacompany.com',01400423056, 'IT Contact Suport', 'IT',1);