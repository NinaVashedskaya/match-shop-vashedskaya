using Microsoft.EntityFrameworkCore;

Console.WriteLine("Магазин спичек «Чирк»: проект запущен!");

// Дальше — код по шагам задания.

using var db = new ShopContext();
db.Database.EnsureDeleted();   // удалить базу, если она осталась с прошлого запуска
db.Database.EnsureCreated();   // создать базу и таблицы по классам

Console.WriteLine("=== Шаг 3 ===");

var ivan = new Customer { Name = "Иван Петров", Phone = "+7 900 111-22-33", City = "Омск" };
var olga = new Customer { Name = "Ольга Смирнова", Phone = "+7 900 444-55-66", City = "Томск" };
var anna = new Customer { Name = "Анна Алексеева", Phone = "+7 900 777-88-99", City = "Владивосток" };
var ksenia = new Customer { Name = "Ксения Цаголова", Phone = "+7 900 000-11-22", City = "Санкт-Петербург" };
var denis = new Customer { Name = "Денис Талалуев", Phone = "+7 900 333-44-55", City = "Караганда" };

var crap = new Product { Name = "ширпотреб", Price = 9.99m, CountInPack = 10, CountInStock = 20, Description = "не горят" };
var red = new Product { Name = "красные спички", Price = 25m, CountInPack = 20, CountInStock = 40 };
var black = new Product { Name = "черные спички", Price = 25m, CountInPack = 20, CountInStock = 40 };
var redblack = new Product { Name = "красно-черные спички", Price = 20m, CountInPack = 25, CountInStock = 100, Description = "хит сезона" };
var lux = new Product { Name = "люкс", Price = 499.99m, CountInPack = 10, CountInStock = 10, Description = "горят в воде" };
 
var number1 = new Order { Quantity = 1, Customer = ivan, Product = crap };
var number2 = new Order { Quantity = 10, Customer = olga, Product = redblack };
var number3 = new Order { Quantity = 1, Customer = olga, Product = lux };
var number4 = new Order { Quantity = 20, Customer = anna, Product = red };
var number5 = new Order { Quantity = 20, Customer = denis, Product = red };

db.Customers.Add(ivan);
db.Customers.Add(olga);
db.Customers.Add(anna);
db.Customers.Add(ksenia);
db.Customers.Add(denis);

db.Products.Add(crap);
db.Products.Add(red);
db.Products.Add(black);
db.Products.Add(redblack);
db.Products.Add(lux);

db.Orders.Add(number1);
db.Orders.Add(number2);
db.Orders.Add(number3);
db.Orders.Add(number4);
db.Orders.Add(number5);

db.SaveChanges();   // только здесь данные уходят в базу

Console.WriteLine("=== Шаг 4 ===");

var omsk = db.Customers
    .Where(c => c.City == "Омск")   // только из Омска
    .OrderBy(c => c.Name)            // по алфавиту
    .ToList();                       // здесь запрос уходит в базу

foreach (var c in omsk)
    Console.WriteLine($"{c.Name}, {c.Phone}");

var num1 = db.Products
    .Where(p => p.Price < 100)   
    .OrderBy(p => p.Price)            
    .ToList();                       
    
foreach (var p in num1)
    Console.WriteLine($"{p.Name}, {p.Price}");

var num2 = db.Products
    .Count(p => p.CountInStock > 0);   

Console.WriteLine($"видов спичек на складе : {num2}");

var num3 = db.Products
    .OrderByDescending (p => p.Price) 
    .Take(3)           
    .ToList();                       

foreach (var p in num3)
    Console.WriteLine($"{p.Name}, {p.Price}");

var num4 = db.Orders
    .Include(o => o.Customer)
    .Include(o => o.Product)
    .ToList();

decimal total = 0;

foreach (var o in num4)
{
    decimal sum = o.Product.Price * o.Quantity;
    Console.WriteLine(
        $"{o.Customer.Name} - {o.Product.Name} - {o.Quantity} шт. - {sum}");
    total += sum;
}

Console.WriteLine($"Общая выручка: {total}");

Console.WriteLine("=== Шаг 5 ===");

var customer = db.Customers.First(c => c.Name == "Иван Петров");   // найти
customer.Phone = "+7 900 999-00-00";                                  // изменить
db.SaveChanges();   // EF сам видит, что изменилось, и отправляет UPDATE

var num5 = db.Products.ToList();   
foreach (var p in num5) { p.Price *= 1.1m; }                     
db.SaveChanges();

var num6 = db.Products.First(p => p.Name == "ширпотреб");
num6.CountInStock = 0;
db.SaveChanges();

void CreateOrder (ShopContext db, string costumer_name, string product_name, int quantity)
{
    var costumer = db.Customers.First(c => c.Name == costumer_name);
    var product = db.Products.First(c => c.Name == product_name);
    if (product.CountInStock >= quantity)
    {
        var order = new Order
        {
            Customer = costumer,
            Product = product,
            Quantity = quantity
        };
        db.Orders.Add(order);
        product.CountInStock -= quantity;
        db.SaveChanges();
        Console.WriteLine("заказ успешно оформлен");
    }
    else
    {
        Console.WriteLine("недостаточно на складе");
    }
}

CreateOrder(db, "Иван Петров", "красные спички", 10);
CreateOrder(db, "Ольга Смирнова", "ширпотреб", 5);
