# Adda Social Network

## Описание
Adda — это социальная сеть, реализованная на ASP.NET Core 9.0 с использованием архитектуры многомодульного решения. Проект поддерживает регистрацию, авторизацию, создание и редактирование профиля, добавление друзей, публикацию постов, комментарии, лайки, обмен сообщениями (чаты), загрузку фотографий, создание альбомов и хобби. Для хранения файлов используется S3-совместимое хранилище (Minio).

## Структура решения
- **Web** — основной веб-проект (MVC, Razor Views, контроллеры, статические файлы, SignalR для чатов)
- **Core** — бизнес-логика, основные сущности, сервисы, обработчики команд/запросов
- **Persistence** — работа с базой данных (Entity Framework Core, миграции, контекст)
- **Contracts** — контракты, DTO, запросы/ответы между слоями
- **S3** — сервис для работы с S3-хранилищем (Minio)
- **UnitTest** — модульные тесты

## Основные сущности
- **User** — пользователь (имя, фамилия, email, возраст, страна, аватар, хобби, категории друзей)
- **ProfileData** — профиль пользователя (о себе, рабочая зона, образование, контакты, соцсети)
- **Friend, FriendCategory** — друзья и их категории
- **Post, Comment, Like** — посты, комментарии, лайки
- **Chat, Message** — чаты и сообщения между пользователями
- **Photo, Album, Hobby** — фотографии, альбомы, хобби пользователя

## Технологии
- ASP.NET Core 9.0 (MVC, Identity, SignalR)
- Entity Framework Core (PostgreSQL)
- Minio (S3-совместимое хранилище)
- Docker (Dockerfile для сборки)
- Bootstrap, jQuery (UI)

## Быстрый старт
### 1. Клонирование репозитория
```bash
git clone <repo_url>
cd <папка_проекта>
```

### 2. Настройка переменных окружения и конфигов
- **Web/appsettings.json** — настройте строки подключения к БД, email и S3:
```json
"ConnectionStrings": {
  "DefaultConnection": "Host=...;Port=5432;Database=...;Username=...;Password=...;"
},
"EmailSettings": {
  "Email": "...",
  "Password": "...",
  "Host": "smtp.yandex.ru",
  "Port": "465"
},
"Application": {
  "S3": {
    "AccessKey": "...",
    "SecretKey": "...",
    "ServiceUrl": "...",
    "BucketName": "..."
  }
}
```

### 3. Миграции и база данных
- Убедитесь, что PostgreSQL доступен и база создана.
- Примените миграции:
```bash
dotnet ef database update --project Persistence --startup-project Web
```

### 4. Запуск через Docker
```bash
docker build -t adda-social-network .
docker run -p 8080:80 adda-social-network
```

### 5. Локальный запуск
```bash
dotnet run --project Web
```

## Основные возможности
- Регистрация и авторизация пользователей
- Редактирование профиля, добавление информации о себе, соцсетей, хобби
- Добавление/удаление друзей, категории друзей
- Публикация постов, комментарии, лайки
- Загрузка фотографий, создание альбомов
- Встроенные чаты (SignalR)
- Админ-панель для управления пользователями, фото, альбомами, хобби

## Контакты и поддержка
- Email для уведомлений: указывается в `appsettings.json`
- Для вопросов и багов — создавайте issue в репозитории

## Деплой и инфраструктура

Перед запуском приложения убедитесь, что у вас работают контейнеры с базой данных PostgreSQL и S3-хранилищем Minio. Пример конфигурации с помощью `docker-compose`:

```yaml
db:
  image: postgres:15
  restart: always
  environment:
    POSTGRES_DB: adda_social_network
    POSTGRES_USER: postgres
    POSTGRES_PASSWORD: postgresmaster
  ports:
    - "5432:5432"
  volumes:
    - pgdata:/var/lib/postgresql/data

minio:
  image: minio/minio:latest
  restart: always
  environment:
    MINIO_ROOT_USER: ROOTNAME
    MINIO_ROOT_PASSWORD: CHANGEME123
  command: server /data --console-address ":9001"
  ports:
    - "9000:9000"
    - "9001:9001"
  volumes:
    - minio_data:/data

volumes:
  pgdata:
  minio_data:
```

- После запуска контейнеров (`docker-compose up -d`) убедитесь, что параметры подключения в `Web/appsettings.json` соответствуют этим сервисам.
- Для доступа к Minio-консоли используйте http://localhost:9001 (логин и пароль как в переменных окружения).

---

**Внимание:**
- Не храните реальные пароли и ключи в публичных репозиториях!
- Для production используйте переменные окружения и секреты. 