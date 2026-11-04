// Классы таблиц: Customer, Product, Order (шаги 1–2).

public class Customer
{
    public int Id { get; set; }              // первичный ключ: EF узнаёт его по имени Id
    public string Name { get; set; } = "";   // имя
    public string Phone { get; set; } = "";  // телефон
    public string City { get; set; } = "";   // город
}

public class Product
{
    public int Id { get; set; }              
    public string Name { get; set; } = "";   
    public decimal Price { get; set; } = 0;  
    public int CountInPack { get; set; } = 0;   
    public int CountInStock { get; set; } = 0;
    public string? Description { get; set; }
}

// отличаются наличием not null у столбца с названием, тк "string" не дает оставить свойство без значения (в отличии от "string?" в столбце с описанием)
