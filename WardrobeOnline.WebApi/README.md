# WardrobeOnline.WebApi

Проект разделён на 3 части:
### WebApi
Собственно, его величество API
### BLL
Бизнес логика
### DAL
Слой для доступа к базе

<b>Для разворачивания можно использовать докер </b> :cool:: 
```
docker-compose  -f "docker-compose.yml" -f "docker-compose.override.yml" --ansi never up -d
```