// Классы таблиц: Customer, Product, Order (шаги 1–2).

public class Customer
{
    public int Id { get; set; }              // первичный ключ: EF узнаёт его по имени Id
    public string Name { get; set; } = "";   // имя
    public string Phone { get; set; } = "";  // телефон
    public string City { get; set; } = "";   // город
    public List<Order> Orders { get; set; } = new();
}

public class Product
{
    public int Id { get; set; }              
    public string Name { get; set; } = "";   
    public decimal Price { get; set; } = 0;  
    public int CountInPack { get; set; } = 0;   
    public int CountInStock { get; set; } = 0;
    public string? Description { get; set; }
    public List<Order> Orders { get; set; } = new();
}

// отличаются наличием not null у столбца с названием, тк "string" не дает оставить свойство без значения (в отличии от "string?" в столбце с описанием)

public class Order
{
    public int Id { get; set; }
    public int Quantity { get; set; }                       // сколько коробок
    public DateTime Date { get; set; } = DateTime.UtcNow;   // честно загуглила, думала, что надо timestamp

    public int CustomerId { get; set; }                     // внешний ключ: Id покупателя
    public Customer Customer { get; set; } = null!;         // сам покупатель

    public int ProductId { get; set; }               
    public Product Product { get; set; } = null!;   
}
