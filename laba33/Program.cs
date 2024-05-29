using System;
using System.Collections.Generic;
using System.Globalization;

public abstract class Work
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    public Work(string name, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Имя работы не может быть пустым или содержать только пробелы.", nameof(name));
        }

        if (price <= 0)
        {
            throw new ArgumentException("Цена работы должна быть положительной.", nameof(price));
        }

        Name = name;
        Price = price;
    }

    public abstract string GetWorkInfo();
}

public class Layout : Work
{
    public Layout(string name, decimal price) : base(name, price) { }

    public override string GetWorkInfo()
    {
        return $"Верстка: {Name} - {Price:C2}".Replace("₽", "₽");
    }
}

public class Design : Work
{
    public Design(string name, decimal price) : base(name, price) { }

    public override string GetWorkInfo()
    {
        return $"Дизайн: {Name} - {Price:C2}".Replace("₽", "₽");
    }
}

public class Site
{
    public List<Work> Works { get; set; }

    public Site(params Work[] works)
    {
        Works = works.ToList();
    }

    public void PrintWorks()
    {
        var workInfo = string.Join(", ", Works.Select(w => w.GetWorkInfo()));
        Console.WriteLine($"По сайту выполнены следующие работы: {workInfo}");
    }
}

class Program
{
    static void Main()
    {
        try
        {
            var layout = new Layout("Лендинг", 5000);
            var design = new Design("Логотип", 3000);

            var site = new Site(layout, design);
            site.PrintWorks();
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}