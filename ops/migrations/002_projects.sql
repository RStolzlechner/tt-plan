CREATE TABLE project(
    id          UUID        NOT NULL    PRIMARY KEY     DEFAULT gen_random_uuid(),
    name        TEXT        NOT NULL,
    start_date  TIMESTAMPTZ NOT NULL DEFAULT now(),
    end_date    TIMESTAMPTZ NULL,
    created_at  TIMESTAMPTZ NOT NULL DEFAULT now(),
    updated_at  TIMESTAMPTZ NOT NULL DEFAULT now()
);

CREATE OR REPLACE FUNCTION set_updated_at()
    RETURNS TRIGGER AS $$
BEGIN
    NEW.updated_at = NOW();
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_set_updated_at_project
    BEFORE UPDATE ON project
    FOR EACH ROW
EXECUTE FUNCTION set_updated_at();