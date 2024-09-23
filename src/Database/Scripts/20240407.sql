﻿-- companies
CREATE TABLE public.companies (
	id int4 GENERATED BY DEFAULT AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 2147483647 START 1 CACHE 1 NO CYCLE) NOT NULL,
	name                VARCHAR(100)    NOT NULL,
	email               VARCHAR(100)    NOT NULL,
	phone_number        VARCHAR(20)     NOT NULL,
	financial_month     INT             NOT NULL,
	administrator_name  VARCHAR(100)    NOT NULL,
	created_by          INT             NOT NULL,
	created_on          TIMESTAMPTZ     NOT NULL,
	modified_by         INT                 NULL,
	modified_on         TIMESTAMPTZ         NULL,
	CONSTRAINT          pk_companies PRIMARY KEY (id)
);


CREATE TABLE domain_events (
	id                      UUID            NOT NULL,
	type                    TEXT            NOT NULL,
	json                    JSONB           NOT NULL,
	occurred_on             TIMESTAMPTZ     NOT NULL,
	process_started_on      TIMESTAMPTZ         NULL,
	process_completed_on    TIMESTAMPTZ         NULL,
	retry_on_error          BOOLEAN         NOT NULL,
	retry_count             INT             NOT NULL,
	error                   TEXT                NULL,
	constraint      pk_domain_events primary key (id)
);

-- users
CREATE TABLE public.users (
	id int4 GENERATED BY DEFAULT AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 2147483647 START 1 CACHE 1 NO CYCLE) NOT NULL,
	user_name text NULL,
	normalized_user_name text NULL,
	email text NULL,
	normalized_email text NULL,
	email_confirmed bool NOT NULL,
	password_hash text NULL,
	security_stamp text NULL,
	concurrency_stamp text NULL,
	phone_number text NULL,
	phone_number_confirmed bool NOT NULL,
	two_factor_enabled bool NOT NULL,
	lockout_end timestamptz NULL,
	lockout_enabled bool NOT NULL,
	access_failed_count int4 NOT NULL,
	created_by int NOT NULL,
	created_on timestamptz NOT NULL,
	modified_by int NULL,
	modified_on timestamptz NULL,
	CONSTRAINT pk_users PRIMARY KEY (id)
);
CREATE UNIQUE INDEX uk_users_email ON public.users USING btree (email);
CREATE UNIQUE INDEX uk_users_phone_number ON public.users USING btree (phone_number);
CREATE UNIQUE INDEX uk_users_user_name ON public.users USING btree (user_name);


-- roles
CREATE TABLE public.roles (
	id int4 GENERATED BY DEFAULT AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 2147483647 START 1 CACHE 1 NO CYCLE) NOT NULL,
	name text NULL,
	normalized_name text NULL,
	concurrency_stamp text NULL,
	created_by int NOT NULL,
	created_on timestamptz NOT NULL,
	modified_by int NULL,
	modified_on timestamptz NULL,
	CONSTRAINT pk_roles PRIMARY KEY (id)
);
CREATE UNIQUE INDEX uk_roles_name ON public.roles USING btree (name);


-- role_claims
CREATE TABLE public.role_claims (
	id int4 GENERATED BY DEFAULT AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 2147483647 START 1 CACHE 1 NO CYCLE) NOT NULL,
	role_id int4 NOT NULL,
	claim_type text NULL,
	claim_value text NULL,
	created_by int NOT NULL,
	created_on timestamptz NOT NULL,
	modified_by int NULL,
	modified_on timestamptz NULL,
	CONSTRAINT pk_role_claims PRIMARY KEY (id)
);
CREATE UNIQUE INDEX uk_role_claims_role_id_claim_type ON public.role_claims USING btree (role_id, claim_type);


-- public.user_claims definition
CREATE TABLE public.user_claims (
	id int4 GENERATED BY DEFAULT AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 2147483647 START 1 CACHE 1 NO CYCLE) NOT NULL,
	user_id int4 NOT NULL,
	claim_type text NULL,
	claim_value text NULL,
	created_by int NOT NULL,
	created_on timestamptz NOT NULL,
	modified_by int NULL,
	modified_on timestamptz NULL,
	CONSTRAINT pk_user_claims PRIMARY KEY (id),
    CONSTRAINT fk_user_claims_user_id FOREIGN KEY (user_id) REFERENCES users (id) MATCH SIMPLE
);
CREATE UNIQUE INDEX uk_user_claims_user_id_claim_type ON public.user_claims USING btree (user_id, claim_type);

-- public.user_logins definition
CREATE TABLE public.user_logins (
	login_provider text NOT NULL,
	provider_key text NOT NULL,
	provider_display_name text NULL,
	user_id int4 NOT NULL,
	created_by int NOT NULL,
	created_on timestamptz NOT NULL,
	modified_by int NULL,
	modified_on timestamptz NULL,
    CONSTRAINT fk_user_logins_user_id FOREIGN KEY (user_id) REFERENCES users (id) MATCH SIMPLE
);

-- public.user_tokens definition
CREATE TABLE public.user_tokens (
	user_id int4 NOT NULL,
	login_provider text NOT NULL,
	name text NOT NULL,
	value text NULL,
	created_by int NOT NULL,
	created_on timestamptz NOT NULL,
	modified_by int NULL,
	modified_on timestamptz NULL,
	CONSTRAINT pk_user_tokens PRIMARY KEY (user_id, login_provider, name),
    CONSTRAINT fk_user_tokens_user_id FOREIGN KEY (user_id) REFERENCES users (id) MATCH SIMPLE
);


-- public.user_roles definition
CREATE TABLE public.user_roles (
	user_id int4 NOT NULL,
	role_id int4 NOT NULL,
    created_by int NOT NULL,
    created_on timestamptz NOT NULL,
    modified_by int NULL,
    modified_on timestamptz NULL,
    CONSTRAINT fk_user_roles_user_id FOREIGN KEY (user_id) REFERENCES users (id) MATCH SIMPLE,
    CONSTRAINT fk_user_roles_role_id FOREIGN KEY (role_id) REFERENCES roles (id) MATCH SIMPLE
);
CREATE UNIQUE INDEX uk_user_roles_user_id_role_id ON public.user_roles USING btree (user_id, role_id);


-- public.refresh_tokens definition
CREATE TABLE public.refresh_tokens (
    id                  UUID        NOT NULL,
    user_id             int4        NOT NULL,
    value               text        NOT NULL,
    device_identifier   text        NOT NULL,
    created_on          timestamptz NOT NULL,
    modified_on         timestamptz     NULL,
    CONSTRAINT fk_refresh_tokens_user_id FOREIGN KEY (user_id) REFERENCES users (id) MATCH SIMPLE
);

CREATE UNIQUE INDEX uk_refresh_tokens_user_id_device_identifier ON public.refresh_tokens USING btree (user_id, device_identifier);
