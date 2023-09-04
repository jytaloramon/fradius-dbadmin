CREATE TABLE IF NOT EXISTS tb_admin_group (
    admg_id SERIAL,
    admg_name VARCHAR (32) NOT NULL,
    CONSTRAINT admg_id_pkey PRIMARY KEY(admg_id),
    CONSTRAINT admg_name_key UNIQUE (admg_name)
);

CREATE TABLE IF NOT EXISTS tb_rule (
    rule_id INTEGER,
    rule_admg_id INTEGER NOT NULL,
    CONSTRAINT rule_id_pkey PRIMARY KEY(rule_id, rule_admg_id),
    CONSTRAINT rule_admg_id_fkey FOREIGN KEY (rule_admg_id) REFERENCES tb_admin_group (admg_id)
);

CREATE TABLE IF NOT EXISTS tb_admin (
    adm_id SERIAL,
    adm_admg_id SMALLINT NOT NULL,
    adm_username VARCHAR(32) NOT NULL,
    adm_email VARCHAR(64) NOT NULL,
    adm_password_hash VARCHAR(128) NOT NULL,
    adm_is_active BOOLEAN DEFAULT TRUE,
    CONSTRAINT adm_id_pkey PRIMARY KEY(adm_id),
    CONSTRAINT adm_admg_id_fkey FOREIGN KEY (adm_admg_id) REFERENCES tb_admin_group (admg_id),
    CONSTRAINT adm_username_key UNIQUE (adm_username),
    CONSTRAINT adm_email_key UNIQUE (adm_email)
);