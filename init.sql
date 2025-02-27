CREATE EXTENSION IF NOT EXISTS pgcrypto;

-- Таблица ролей
CREATE TABLE IF NOT EXISTS public.roles
(
    id int PRIMARY KEY NOT NULL,
    name text NOT NULL
);

ALTER TABLE IF EXISTS public.roles
    OWNER to kir1l9x;

-- Добавление ролей admin и client
INSERT INTO public.roles (id, name) VALUES
                                        (52, 'admin'),
                                        (100, 'client')
ON CONFLICT (id) DO NOTHING;

-- Таблица пользователей
CREATE TABLE IF NOT EXISTS public.users
(
    id uuid PRIMARY KEY NOT NULL,
    username text NOT NULL,
    hash_password text NOT NULL,
    role int NOT NULL,
    created_on timestamp DEFAULT NOW(),
    FOREIGN KEY (role) REFERENCES roles(id)
);

ALTER TABLE IF EXISTS public.users
    OWNER to kir1l9x;

CREATE UNIQUE INDEX IF NOT EXISTS idx_username ON public.users(username);

-- Добавление пользователя admin
INSERT INTO public.users (id, username, hash_password, role, created_on)
VALUES (
           gen_random_uuid(), -- Генерация случайного UUID для пользователя
           'admin',
           encode(digest('admin', 'sha256'), 'base64'), -- Хэширование пароля с использованием SHA256
           52, -- Роль admin
           NOW()
       )
ON CONFLICT (username) DO NOTHING;

-- Таблица аккаунтов
CREATE TABLE IF NOT EXISTS public.accounts
(
    id uuid PRIMARY KEY NOT NULL,
    account_number text NOT NULL UNIQUE,
    user_id uuid NOT NULL,
    balance money DEFAULT 0.00,
    created_on timestamp DEFAULT NOW(),
    FOREIGN KEY (user_id) REFERENCES users(id)
);

ALTER TABLE IF EXISTS public.accounts
    OWNER to kir1l9x;

-- Таблица транзакций
CREATE TABLE IF NOT EXISTS public.transactions
(
    id uuid PRIMARY KEY NOT NULL,
    account_id uuid NOT NULL,
    type text NOT NULL CHECK (type IN ('Replenishment', 'WriteOff')),
    amount money NOT NULL,
    date timestamp DEFAULT NOW(),
    FOREIGN KEY (account_id) REFERENCES accounts(id)
);

ALTER TABLE IF EXISTS public.transactions
    OWNER to kir1l9x;

-- Таблица логов
CREATE TABLE IF NOT EXISTS public."audit_logs"
(
    id uuid PRIMARY KEY NOT NULL,
    user_id uuid,
    action_description text NOT NULL,
    action_time timestamp DEFAULT NOW()
);

ALTER TABLE IF EXISTS public."audit_logs"
    OWNER to kir1l9x;
