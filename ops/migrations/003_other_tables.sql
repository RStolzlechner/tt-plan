-- MILESTONE
CREATE TABLE milestone(
    id          UUID        NOT NULL    PRIMARY KEY                                 DEFAULT gen_random_uuid(),
    project_id  UUID        NOT NULL    REFERENCES project(id) ON DELETE RESTRICT,
    name        TEXT        NOT NULL,
    start_date  TIMESTAMPTZ NOT NULL                                                DEFAULT now(),
    end_date    TIMESTAMPTZ NULL,
    created_at  TIMESTAMPTZ NOT NULL                                                DEFAULT now(),
    updated_at  TIMESTAMPTZ NOT NULL                                                DEFAULT now()
);

CREATE TRIGGER trg_set_updated_at_milestone
    BEFORE UPDATE ON milestone
    FOR EACH ROW
EXECUTE FUNCTION set_updated_at();

--TASK
CREATE TABLE task(
    id              UUID        NOT NULL    PRIMARY KEY                                 DEFAULT gen_random_uuid(),
    project_id      UUID        NOT NULL    REFERENCES project(id) ON DELETE RESTRICT,
    milestone_id    UUID        NULL        REFERENCES milestone(id) ON DELETE SET NULL,
    name            TEXT        NOT NULL,
    scheduled_at    TIMESTAMPTZ NOT NULL,
    estimate        INT4        NOT NULL                                                DEFAULT 1,
    status          TEXT        NOT NULL                                                DEFAULT 'not_started',
    created_at      TIMESTAMPTZ NOT NULL                                                DEFAULT now(),
    updated_at      TIMESTAMPTZ NOT NULL                                                DEFAULT now()
);

CREATE TRIGGER trg_set_updated_at_task
    BEFORE UPDATE ON task
    FOR EACH ROW
EXECUTE FUNCTION set_updated_at();

--JOURNAL
CREATE TABLE journal(
    id              UUID        NOT NULL    PRIMARY KEY                             DEFAULT gen_random_uuid(),
    task_id         UUID        NOT NULL    REFERENCES task(id) ON DELETE RESTRICT,
    description     TEXT        NOT NULL,
    hours           FLOAT8      NOT NULL,
    done_at         TIMESTAMPTZ NOT NULL,
    created_at      TIMESTAMPTZ NOT NULL                                            DEFAULT now(),
    updated_at      TIMESTAMPTZ NOT NULL                                            DEFAULT now()
);

CREATE TRIGGER trg_set_updated_at_journal
    BEFORE UPDATE ON journal
    FOR EACH ROW
EXECUTE FUNCTION set_updated_at();