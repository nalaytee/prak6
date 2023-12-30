using System;
using System.Collections.Generic;

namespace prak6
{
    // Интерфейс логгера
    public interface ILogger
    {
        void Log(string message);
    }

    // Реализация логгера для вывода в консоль
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }

    // Класс продукта
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    // Интерфейс фильтрации продуктов
    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, Func<T, bool> predicate);
    }

    // Реализация фильтрации продуктов по цене
    public class PriceFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> products, Func<Product, bool> predicate)
        {
            foreach (var product in products)
            {
                if (predicate(product))
                {
                    yield return product;
                }
            }
        }
    }

    // Интерфейс принтера
    public interface IPrinter
    {
        void Print();
    }

    // Реализация принтера
    public class Printer : IPrinter
    {
        public void Print()
        {
            Console.WriteLine("Печать...");
        }
    }

    // Интерфейс сканера
    public interface IScanner
    {
        void Scan();
    }

    // Реализация сканера
    public class Scanner : IScanner
    {
        public void Scan()
        {
            Console.WriteLine("Сканирование...");
        }
    }

    // Интерфейс устройства с несколькими задачами
    public interface IMultiTaskDevice
    {
        void Print();
        void Scan();
        void Fax();
    }

    // Реализация устройства с несколькими задачами
    public class MultiTaskDevice : IMultiTaskDevice
    {
        private readonly IPrinter _printer;
        private readonly IScanner _scanner;

        public MultiTaskDevice(IPrinter printer, IScanner scanner)
        {
            _printer = printer;
            _scanner = scanner;
        }

        public void Print()
        {
            _printer.Print();
        }

        public void Scan()
        {
            _scanner.Scan();
        }

        public void Fax()
        {
            Console.WriteLine("Отправка по факсу...");
        }
    }

    // Класс сервиса продуктов
    public class ProductService
    {
        private readonly ILogger _logger;

        public ProductService(ILogger logger)
        {
            _logger = logger;
        }

        public void AddProduct(Product product)
        {
            // Логирование добавления продукта
            _logger.Log($"Товар добавлен: {product.Name}, Цена: {product.Price}");
            // Дополнительная логика по добавлению продукта
        }
    }

    class Program
    {
        static void Main()
        {
            // Пример использования классов

            // Используем ConsoleLogger для логирования
            ILogger consoleLogger = new ConsoleLogger();

            // Создаем экземпляр ProductService, передавая ему consoleLogger
            ProductService productService = new ProductService(consoleLogger);

            // Добавляем продукт, что приведет к записи в консоль через consoleLogger
            productService.AddProduct(new Product { Name = "Ноутбук", Price = 1000 });

            // Пример использования фильтрации продуктов по цене
            var products = new List<Product>
            {
                new Product { Name = "Телефон", Price = 500 },
                new Product { Name = "Планшет", Price = 800 },
                new Product { Name = "ПК", Price = 1200 }
            };

            var priceFilter = new PriceFilter();
            var filteredProducts = priceFilter.Filter(products, p => p.Price > 700);

            Console.WriteLine("Продукты с ценой больше 700:");
            foreach (var product in filteredProducts)
            {
                Console.WriteLine($"{product.Name} - {product.Price}");
            }

            // Пример использования принтера и сканера
            IPrinter printer = new Printer();
            IScanner scanner = new Scanner();

            // Многозадачное устройство
            IMultiTaskDevice multiTaskDevice = new MultiTaskDevice(printer, scanner);

            // Используем принтер, сканер и факс
            multiTaskDevice.Print();
            multiTaskDevice.Scan();
            multiTaskDevice.Fax();

            Console.ReadLine();
        }
    }
}
