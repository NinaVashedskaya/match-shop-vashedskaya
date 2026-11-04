using Microsoft.EntityFrameworkCore;

Console.WriteLine("Магазин спичек «Чирк»: проект запущен!");

// Дальше — код по шагам задания.

using var db = new ShopContext();
db.Database.EnsureDeleted();   // удалить базу, если она осталась с прошлого запуска
db.Database.EnsureCreated();   // создать базу и таблицы по классам
