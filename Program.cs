using System;
using System.Collections.Generic;
using System.Linq;

namespace Pr3_Toys
{
    class ToysComparer : IComparer<Toys>
    {
        public enum CompareField : byte
        {
            byToyName = 0,
            byAge = 1,
            byLength = 2,
            byWidth = 3,
            byHeight = 4,            
            byPrice = 5,
            byBrand = 6,
            byCountry = 7
        }
        public CompareField Field { get; set; }
        public int Compare(Toys t1, Toys t2)
        {
            switch (Field)
            {
                case CompareField.byToyName:
                    return t1.ToyName.CompareTo(t2.ToyName);
                case CompareField.byAge:
                    return t1.Age.CompareTo(t2.Age);
                case CompareField.byLength:
                    return t1.Length.CompareTo(t2.Length);
                case CompareField.byWidth:
                    return t1.Width.CompareTo(t2.Width);
                case CompareField.byHeight:
                    return t1.Height.CompareTo(t2.Height);                
                case CompareField.byPrice:
                    return t1.Price.CompareTo(t2.Price);                
                case CompareField.byBrand:
                    return t1.Brand.CompareTo(t2.Brand);
                case CompareField.byCountry:
                    return t1.Country.CompareTo(t2.Country);
                default:
                    return 0;
            }
        }        
    }
    abstract class Toys 
    {
        private string toyName; // уникальное название
        private int age; // возраст, от скольки лет
        private (double length, double width, double height) boxSize;        
        private string material; // из какого материала
        private double price; // цена
        private string brand; // бренд
        private string country; // страна, где изготовления
        public string ToyName => toyName;
        public int Age => age;
        public double Length => boxSize.length;
        public double Width => boxSize.width;
        public double Height => boxSize.height;
        public string Material => material;
        public double Price 
        {
            get => price;
            set
            {
                if (value <= 0)
                    throw new Exception("Ошибка! Цена игрушки должна быть положительным числом");
                else
                    price = value;
            }            
        }
        public string Brand => brand;
        public string Country => country;
        public Toys(string toyName, int age, (double length, double width, double height) boxSize, double price, string brand, string country) : this(toyName, age, "пластик", boxSize, price, brand, country) {}
        public Toys(string toyName, int age, string material, (double length, double width, double height) boxSize, double price, string brand, string country) 
        {
            this.toyName = toyName;
            if (age < 0)
                throw new Exception("Ошибка! Возраст должен быть неотрицательным целым числом");
            else
                this.age = age;
            this.material = material;
            if (boxSize.length <= 0)
                throw new Exception("Ошибка! Длина упаковки должна быть положительным числом");
            else            
                this.boxSize.length = boxSize.length;            
            if (boxSize.width <= 0)
                throw new Exception("Ошибка! Ширина упаковки должна быть положительным числом");
            else
                this.boxSize.width = boxSize.width;            
            if (boxSize.height <= 0)
                throw new Exception("Ошибка! Высота упаковки должна быть положительным числом");
            else
                this.boxSize.height = boxSize.height;            
            if (price <= 0)
                throw new Exception("Ошибка! Цена игрушки должна быть положительным числом");
            else
                this.price = price;
            this.brand = brand;
            this.country = country;
        }                
        public string GetSize()
        {
            return boxSize.length.ToString() + "x" + boxSize.width.ToString() + "x" + boxSize.height.ToString() + " см";
        }
        public virtual string GetAllInfo()
        {
            return "Игрушка: " + ToyName + "\n" + 
                   "Предназначена для детей от " + Age + " лет\n" +
                   "Материал: " + Material + "\n" +
                   "Размер упаковки: " + GetSize() + "\n" +
                   "Марка: " + Brand + "\n" +
                   "Страна-производитель: " + Country + "\n" +
                   "Цена: " + Price + " рублей\n";
        }                
    }
    class ConstructionSets : Toys
    {
        public static string name = "Конструктор";
        private int details; // количество деталей
        private int level; // уровень сложности
        public int Details => details;
        public int Level => level;
        public ConstructionSets(string toyName, int age, int details, int level, (double length, double width, double height) boxSize, double price, string brand, string country) 
            : base(toyName, age, boxSize, price, brand, country)
        {
            if (details <= 0)
                throw new Exception("Ошибка! Количество деталей должно быть положительным числом");
            else
                this.details = details;
            if (level <= 0)
                throw new Exception("Ошибка! Уровень должен быть положительным числом");
            else
                this.level = level;
        }
        public ConstructionSets(string toyName, int age, string material, int details, int level, (double length, double width, double height) boxSize, double price, string brand, string country)
            : base(toyName, age, material, boxSize, price, brand, country)
        {
            if (details <= 0)
                throw new Exception("Ошибка! Количество деталей должно быть положительным числом");
            else
                this.details = details;
            if (level <= 0)
                throw new Exception("Ошибка! Уровень должен быть положительным числом");
            else
                this.level = level;
        }        
        public override string GetAllInfo()
        {
            return "Игрушка: " + ToyName + "\n" +
                   "Предназначена для детей от " + Age + " лет\n" +
                   "Уровень сложности: " + Level + "\n" +
                   "Количество деталей: " + Details + "\n" +                              
                   "Материал: " + Material + "\n" +
                   "Размер упаковки: " + GetSize() + "\n" +
                   "Марка: " + Brand + "\n" +
                   "Страна-производитель: " + Country + "\n" +
                   "Цена: " + Price + " рублей\n";
        }
    }
    sealed class MetalSets : ConstructionSets
    {
        public static new string name = "Металлический конструктор";
        private bool tools; // есть инструменты для сборки или нет
        private bool electricalPart; // есть электронная часть или нет
        public bool Tools => tools;
        public bool ElectricalPart => electricalPart;           
        public MetalSets(string toyName, int age, bool tools, bool electricalPart, int details, int level, (double length, double width, double height) boxSize, double price, string brand, string country)
           : this(toyName, age, "металл", tools, electricalPart, details, level, boxSize, price, brand, country) {}
        public MetalSets(string toyName, int age, string material, bool tools, bool electricalPart, int details, int level, (double length, double width, double height) boxSize, double price, string brand, string country)
           : base(toyName, age, material, details, level, boxSize, price, brand, country)
        {
            this.tools = tools;
            this.electricalPart = electricalPart;
        }
        public override string GetAllInfo()
        {
            return "Игрушка: " + ToyName + "\n" +
                   "Предназначена для детей от " + Age + " лет\n" +
                   "Уровень сложности: " + Level + "\n" +
                   "Количество деталей: " + Details + "\n" +
                   "Специальные инструменты для сборки: " + Tools + "\n" +
                   "Электронная часть: " + ElectricalPart + "\n" +   
                   "Материал: " + Material + "\n" +
                   "Размер упаковки: " + GetSize() + "\n" +
                   "Марка: " + Brand + "\n" +
                   "Страна-производитель: " + Country + "\n" +
                   "Цена: " + Price + " рублей\n";
        }
    }
    abstract class SoftToys : Toys
    {
        private string filler; // наполнитель
        private bool music; // музыкальные или нет
        public string Filler => filler;
        public bool Music => music;
        public SoftToys(string toyName, int age, bool music, (double length, double width, double height) boxSize, double price, string brand, string country)           
            : this(toyName, age, "плюш", music, "синтепон", boxSize, price, brand, country) {}
        public SoftToys(string toyName, int age, string material, bool music, (double length, double width, double height) boxSize, double price, string brand, string country)
            : this(toyName, age, material, music, "синтепон", boxSize, price, brand, country) {}
        public SoftToys(string toyName, int age, bool music, string filler, (double length, double width, double height) boxSize, double price, string brand, string country)
            : this(toyName, age, "плюш", music, filler, boxSize, price, brand, country) {}
        public SoftToys(string toyName, int age, string material, bool music, string filler, (double length, double width, double height) boxSize, double price, string brand, string country)
            : base(toyName, age, material, boxSize, price, brand, country)
        {
            this.filler = filler;
            this.music = music;
        }
        public override string GetAllInfo()
        {
            return "Игрушка: " + ToyName + "\n" +
                   "Предназначена для детей от " + Age + " лет\n" +
                   "Музыкальный: " + Music + "\n" +
                   "Материал: " + Material + "\n" +
                   "Наполнитель: " + Filler + "\n" +                              
                   "Размер упаковки: " + GetSize() + "\n" +
                   "Марка: " + Brand + "\n" +
                   "Страна-производитель: " + Country + "\n" +
                   "Цена: " + Price + " рублей\n";
        }
    }
    sealed class VariousToys : SoftToys
    {
        public static string name = "Мягкая игрушка";
        private string type; // тип 
        public string Type => type;
        public VariousToys(string toyName, int age, bool music, string type, (double length, double width, double height) boxSize, double price, string brand, string country)
            : base(toyName, age, music, boxSize, price, brand, country)
        {
            this.type = type;
        }
        public VariousToys(string toyName, int age, string material, bool music, string type, (double length, double width, double height) boxSize, double price, string brand, string country)
            : base(toyName, age, material, music, boxSize, price, brand, country)
        {
            this.type = type;
        }
        public VariousToys(string toyName, int age, bool music, string filler, string type, (double length, double width, double height) boxSize, double price, string brand, string country)
            : base(toyName, age, music, filler, boxSize, price, brand, country)
        {
            this.type = type;
        }
        public VariousToys(string toyName, int age, string material, bool music, string filler, string type, (double length, double width, double height) boxSize, double price, string brand, string country)
            : base(toyName, age, material, music, filler, boxSize, price, brand, country)
        {
            this.type = type;
        }
        public override string GetAllInfo()
        {
            return "Игрушка: " + ToyName + "\n" +
                   "Предназначена для детей от " + Age + " лет\n" +
                   "Тип: " + Type + "\n" +
                   "Музыкальный: " + Music + "\n" +
                   "Материал: " + Material + "\n" +
                   "Наполнитель: " + Filler + "\n" +                              
                   "Размер упаковки: " + GetSize() + "\n" +
                   "Марка: " + Brand + "\n" +
                   "Страна-производитель: " + Country + "\n" +
                   "Цена: " + Price + " рублей\n";
        }
    }
    sealed class ToonToys : SoftToys
    {
        public static string name = "Персонаж мультфильма мягкий";
        private string cartoon; // герой мультфильма
        private string nameToon; // имя
        public string Cartoon => cartoon;
        public string NameToon => nameToon;
        public ToonToys(string toyName, int age, bool music, string cartoon, string nameToon, (double length, double width, double height) boxSize, double price, string brand, string country)
            : base(toyName, age, music, boxSize, price, brand, country)
        {
            this.cartoon = cartoon;
            this.nameToon = nameToon;
        }
        public ToonToys(string toyName, int age, string material, bool music, string cartoon, string nameToon, (double length, double width, double height) boxSize, double price, string brand, string country)
            : base(toyName, age, material, music, boxSize, price, brand, country)
        {
            this.cartoon = cartoon;
            this.nameToon = nameToon;
        }
        public ToonToys(string toyName, int age, bool music, string filler, string cartoon, string nameToon, (double length, double width, double height) boxSize, double price, string brand, string country)
            : base(toyName, age, music, filler, boxSize, price, brand, country)
        {
            this.cartoon = cartoon;
            this.nameToon = nameToon;
        }
        public ToonToys(string toyName, int age, string material, bool music, string filler, string cartoon, string nameToon, (double length, double width, double height) boxSize, double price, string brand, string country)
            : base(toyName, age, material, music, filler, boxSize, price, brand, country)
        {
            this.cartoon = cartoon;
            this.nameToon = nameToon;
        }
        public override string GetAllInfo()
        {
            return "Игрушка: " + ToyName + "\n" +
                   "Предназначена для детей от " + Age + " лет\n" +
                   "Герой мультфильма: " + Cartoon + "\n" +
                   "Имя: " + NameToon + "\n" +
                   "Музыкальный: " + Music + "\n" +
                   "Материал: " + Material + "\n" +
                   "Наполнитель: " + Filler + "\n" +                              
                   "Размер упаковки: " + GetSize() + "\n" +
                   "Марка: " + Brand + "\n" +
                   "Страна-производитель: " + Country + "\n" +
                   "Цена: " + Price + " рублей\n";
        }
    }
    sealed class Dolls : Toys
    {
        public static string name = "Кукла";
        private string series; // название
        private string nameDoll;
        public string Series => series;
        public string NameDoll => nameDoll;
        public Dolls(string toyName, int age, string series, string nameDoll, (double length, double width, double height) boxSize, double price, string brand, string country)
             : base(toyName, age, boxSize, price, brand, country)
        {
            this.series = series;
            this.nameDoll = nameDoll;
        }
        public Dolls(string toyName, int age, string material, string series, string nameDoll, (double length, double width, double height) boxSize, double price, string brand, string country)
             : base(toyName, age, material, boxSize, price, brand, country)
        {
            this.series = series;
            this.nameDoll = nameDoll;
        }
        public override string GetAllInfo()
        {
            return "Игрушка: " + ToyName + "\n" +
                   "Предназначена для детей от " + Age + " лет\n" +
                   "Серия: " + Series + "\n" +
                   "Имя: " + NameDoll + "\n" +
                   "Материал: " + Material + "\n" +                                                     
                   "Размер упаковки: " + GetSize() + "\n" +
                   "Марка: " + Brand + "\n" +
                   "Страна-производитель: " + Country + "\n" +
                   "Цена: " + Price + " рублей\n";
        }
    }
    class Transport : Toys
    {
        public static string name = "Транспорт";
        private string type; // тип
        private bool isModel; // является моделью реального транспортного средства или нет        
        private string model; // модель
        public string Type => type;
        public bool IsModel => isModel;
        public string Model => model;
        public Transport(string toyName, int age, string type, bool isModel, string model, (double length, double width, double height) boxSize, double price, string brand, string country)
             : base(toyName, age, boxSize, price, brand, country)
        {
            this.type = type;
            this.isModel = isModel;
            this.model = model;
        }
        public Transport(string toyName, int age, string material, string type, bool isModel, string model, (double length, double width, double height) boxSize, double price, string brand, string country)
             : base(toyName, age, material, boxSize, price, brand, country)
        {
            this.type = type;
            this.isModel = isModel;
            this.model = model;
        }
        public override string GetAllInfo()
        {
            return "Игрушка: " + ToyName + "\n" +
                   "Предназначена для детей от " + Age + " лет\n" +                              
                   "Тип: " + Type + "\n" +
                   "Модель реального транспортного средства: " + IsModel + "\n" +
                   "Модель: " + Model + "\n" +
                   "Материал: " + Material + "\n" +
                   "Размер упаковки: " + GetSize() + "\n" +
                   "Марка: " + Brand + "\n" +
                   "Страна-производитель: " + Country + "\n" +
                   "Цена: " + Price + " рублей\n";
        }
    }
    sealed class ControlledTr : Transport
    {
        public static new string name = "Радиоуправляемый транспорт";
        private bool battery; // есть батарейки в комплекте или нет
        public bool Battery => battery;
        public ControlledTr(string toyName, int age, string type, bool isModel, string model, bool battery, (double length, double width, double height) boxSize, double price, string brand, string country)
             : base(toyName, age, type, isModel, model, boxSize, price, brand, country)
        {
            this.battery = battery;
        }
        public ControlledTr(string toyName, int age, string material, string type, bool isModel, string model, bool battery, (double length, double width, double height) boxSize, double price, string brand, string country)
             : base(toyName, age, material, type, isModel, model, boxSize, price, brand, country)
        {
            this.battery = battery;
        }
        public override string GetAllInfo()
        {
            return "Игрушка: " + ToyName + "\n" +
                   "Предназначена для детей от " + Age + " лет\n" +                              
                   "Тип: " + Type + "\n" +
                   "Модель реального транспортного средства: " + IsModel + "\n" +
                   "Модель: " + Model + "\n" +
                   "Батарейки в комплекте: " + Battery + "\n" +
                   "Материал: " + Material + "\n" +                              
                   "Размер упаковки: " + GetSize() + "\n" +
                   "Марка: " + Brand + "\n" +
                   "Страна-производитель: " + Country + "\n" +
                   "Цена: " + Price + " рублей\n";
        }
    }
    class Program
    {
        public static Dictionary<string, Toys> allToys = new Dictionary<string, Toys>();
        static void Main()
        {
            Console.Clear();
            Console.Write("Выберите действие\n" +
                          "--------------------------------------------------\n" +
                          "1 - добавить игрушку\n" +
                          "2 - удалить игрушку\n" +
                          "3 - вывести все игрушки\n" +
                          "4 - подсчитать игрушки по категориям\n" +
                          "5 - найти игрушки с определенными характеристиками\n" +
                          "0 - выход\n");
            while (true)
            {
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '1': AddToy(); break;
                    case '2': DeleteToy(); break;
                    case '3': PrintToys(); break;
                    case '4': CountToy(); break;
                    case '5': FindToys(); break;
                    case '0': Environment.Exit(0); break;
                    default: break;
                }
            }           
        }
        public static void AddToy()
        {
            Console.Clear();
            Console.Write("Добавить в категорию\n" +
                          "--------------------------------\n" +
                          "1 - конструктор\n" +
                          "m - электронный конструктор\n" +
                          "2 - мягкая игрушка\n" +
                          "t - персонаж мультфильма\n" +   
                          "3 - кукла\n" +
                          "4 - транспорт\n" +
                          "c - транспорт с радиоуправлением\n" +
                          "5 - добавить игрушек для теста\n");            
            while (true)
            {
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '1': AddToy1(); break;
                    case 'm': AddToyM(); break;
                    case '2': AddToy2(); break;
                    case 't': AddToyT(); break;
                    case '3': AddToy3(); break;
                    case '4': AddToy4(); break;
                    case 'c': AddToyC(); break;
                    case '5': AddTest(); break;
                    default: break;
                }
            }                                
        }
        public static void AddToy1()
        {
            Console.Clear();
            Console.WriteLine("Категория " + ConstructionSets.name);
            (string nameKey, int age, (double length, double width, double height) boxSize, double price, string brand, string country) tuple = AddToyBase();
            (int level, int details) tupleDL = AddToyDL();
            ConstructionSets cs = new ConstructionSets(tuple.nameKey, tuple.age, tupleDL.details, tupleDL.level, tuple.boxSize, tuple.price, tuple.brand, tuple.country);
            allToys.Add(tuple.nameKey, cs);
            Console.WriteLine("Игрушка " + tuple.nameKey + " в категории " + ConstructionSets.name + " создана");
            End();
        }
        public static void AddToyM()
        {
            Console.Clear();
            Console.WriteLine("Категория " + MetalSets.name);
            (string nameKey, int age, (double length, double width, double height) boxSize, double price, string brand, string country) tuple = AddToyBase();
            (int level, int details) tupleDL = AddToyDL();
            Console.Write("Специальные инструменты для сборки (true/false): ");
            bool tools = bool.Parse(Console.ReadLine());
            Console.Write("Электронная часть (true/false): ");
            bool elPart = bool.Parse(Console.ReadLine());
            MetalSets ec = new MetalSets(tuple.nameKey, tuple.age, tools, elPart, tupleDL.details, tupleDL.level, tuple.boxSize, tuple.price, tuple.brand, tuple.country);
            allToys.Add(tuple.nameKey, ec);
            Console.WriteLine("Игрушка " + tuple.nameKey + " в категории " + MetalSets.name + " создана");
            End();
        }
        public static void AddToy2()
        {
            Console.Clear();
            Console.WriteLine("Категория " + VariousToys.name);
            (string nameKey, int age, (double length, double width, double height) boxSize, double price, string brand, string country) tuple = AddToyBase();
            Console.Write("Тип: ");
            string type = Console.ReadLine();
            Console.Write("Музыкальная (true/false): ");
            bool music = bool.Parse(Console.ReadLine());            
            VariousToys vt = new VariousToys(tuple.nameKey, tuple.age, music, type, tuple.boxSize, tuple.price, tuple.brand, tuple.country);
            allToys.Add(tuple.nameKey, vt);
            Console.WriteLine("Игрушка " + tuple.nameKey + " в категории " + VariousToys.name + " создана");
            End();
        }
        public static void AddToyT()
        {
            Console.Clear();
            Console.WriteLine("Категория " + ToonToys.name);
            (string nameKey, int age, (double length, double width, double height) boxSize, double price, string brand, string country) tuple = AddToyBase();            
            Console.Write("Герой мультфильма: ");
            string toon = Console.ReadLine();
            Console.Write("Имя: ");
            string name = Console.ReadLine();
            Console.Write("Музыкальная (true/false): ");
            bool music = bool.Parse(Console.ReadLine());
            ToonToys tt = new ToonToys(tuple.nameKey, tuple.age, music, toon, name, tuple.boxSize, tuple.price, tuple.brand, tuple.country);
            allToys.Add(tuple.nameKey, tt);
            Console.WriteLine("Игрушка " + tuple.nameKey + " в категории " + ToonToys.name + " создана");
            End();
        }        
        public static void AddToy3()
        {
            Console.Clear();
            Console.WriteLine("Категория " + Dolls.name);
            (string nameKey, int age, (double length, double width, double height) boxSize, double price, string brand, string country) tuple = AddToyBase();
            Console.Write("Серия: ");
            string series = Console.ReadLine();
            Console.Write("Имя: ");
            string name = Console.ReadLine();
            Dolls dl = new Dolls(tuple.nameKey, tuple.age, series, name, tuple.boxSize, tuple.price, tuple.brand, tuple.country);
            allToys.Add(tuple.nameKey, dl);
            Console.WriteLine("Игрушка " + tuple.nameKey + " в категории " + Dolls.name + " создана");
            End();
        }
        public static void AddToy4()
        {
            Console.Clear();
            Console.WriteLine("Категория " + Transport.name);
            (string nameKey, int age, (double length, double width, double height) boxSize, double price, string brand, string country) tuple = AddToyBase();
            (string type, bool isModel, string model) tupleTIM = AddToyTIM();
            Transport tr = new Transport(tuple.nameKey, tuple.age, tupleTIM.type, tupleTIM.isModel, tupleTIM.model, tuple.boxSize, tuple.price, tuple.brand, tuple.country);
            allToys.Add(tuple.nameKey, tr);
            Console.WriteLine("Игрушка " + tuple.nameKey + " в категории " + Transport.name + " создана");
            End();
        }
        public static void AddToyC()
        {
            Console.Clear();
            Console.WriteLine("Категория " + ControlledTr.name);
            (string nameKey, int age, (double length, double width, double height) boxSize, double price, string brand, string country) tuple = AddToyBase();
            (string type, bool isModel, string model) tupleTIM = AddToyTIM();
            Console.Write("Батарейки в комплекте (true/false): ");
            bool battery = bool.Parse(Console.ReadLine());
            ControlledTr ct = new ControlledTr(tuple.nameKey, tuple.age, tupleTIM.type, tupleTIM.isModel, tupleTIM.model, battery, tuple.boxSize, tuple.price, tuple.brand, tuple.country);
            allToys.Add(tuple.nameKey, ct);
            Console.WriteLine("Игрушка " + tuple.nameKey + " в категории " + ControlledTr.name + " создана");
            End();
        }
        public static (string, int, (double, double, double), double, string, string) AddToyBase()
        {
            Console.Write("Название: ");
            string nameKey = Console.ReadLine();
            while (allToys.ContainsKey(nameKey))
            {
                Console.WriteLine("Название уже занято, введите другое");
                Console.Write("Название: ");
                nameKey = Console.ReadLine();
            }
            Console.Write("Для детей от скольки лет: ");
            int age = int.Parse(Console.ReadLine());
            if (age < 0)
                throw new Exception("Ошибка! Возраст должен быть неотрицательным целым числом");
            Console.Write("Размер упаковки: ");
            string box = Console.ReadLine();
            string[] size = box.Split('x', 'х');
            if (size.Length != 3)
                throw new Exception("Ошибка! Указано неверное число измерений! Размер упаковки - это только длина, ширина и высота");
            if (double.Parse(size[0].Replace('.', ',')) <= 0 || double.Parse(size[1].Replace('.', ',')) <= 0 || double.Parse(size[2].Replace('.', ',')) <= 0)
                throw new Exception("Ошибка! Размеры упаковки должны быть положительными числами");
            var boxSize = (length: double.Parse(size[0].Replace('.', ',')), width: double.Parse(size[1].Replace('.', ',')), height: double.Parse(size[2].Replace('.', ',')));
            Console.Write("Цена: ");
            double price = double.Parse(Console.ReadLine().Replace('.', ','));
            if (price < 0)
                throw new Exception("Ошибка! Цена должна быть неотрицательным целым числом");
            Console.Write("Марка: ");
            string brand = Console.ReadLine();
            Console.Write("Страна-производитель: ");
            string country = Console.ReadLine();
            (string nameKeyT, int ageT, (double lengthT, double widthT, double heightT) boxSizeT, double priceT, string brandT, string countryT) tuple = (nameKey, age, boxSize, price, brand, country);
            return tuple;
        }
        public static (int, int) AddToyDL()
        {
            Console.Write("Уровень сложности: ");
            int level = int.Parse(Console.ReadLine());
            if (level <= 0)
                throw new Exception("Ошибка! Уровень должен быть положительным числом");
            Console.Write("Количество деталей: ");
            int details = int.Parse(Console.ReadLine());
            if (details <= 0)
                throw new Exception("Ошибка! Количество деталей должно быть положительным числом");
            (int levelT, int detailsT) tuple = (level, details);
            return tuple;
        }
        public static (string, bool, string) AddToyTIM()
        {
            Console.Write("Тип: ");
            string type = Console.ReadLine();
            Console.Write("Модель реального транспортного средства (true/false): ");
            bool isModel = bool.Parse(Console.ReadLine());
            string model = "-";
            if (isModel == true)
            {
                Console.Write("Модель: ");
                model = Console.ReadLine();
            }
            (string typeT, bool isModelT, string modelT) tuple = (type, isModel, model);
            return tuple;
        }
        public static void AddTest()
        {
            Console.Clear();
            allToys.Add("t10", new ConstructionSets("t10", 6, 300, 4, (10, 22.5, 40), 6000, "Lego", "Дания"));
            allToys.Add("t11", new ConstructionSets("t11", 3, 22, 1, (45, 28, 30), 2999, "Lego Duplo", "Дания"));
            allToys.Add("t12", new MetalSets("t12", 10, true, true, 60, 2, (15, 15, 7.5), 4999, "MetalGame", "Россия"));
            allToys.Add("t13", new VariousToys("t13", 4, "мех", true, "медведь", (15.5, 20, 20), 250.99, "Aurora", "Франция"));
            allToys.Add("t14", new ToonToys("t14", 5, false, "Смешарики", "Копатыч", (10, 10, 10), 580, "Smeshariky Original", "Россия"));
            allToys.Add("t15", new ToonToys("t15", 5, false, "Лило и Стич", "Стич", (25, 18, 18), 1500, "Disney", "Америка"));            
            allToys.Add("t16", new Dolls("t16", 1, "ткань", "Русская народная", "Алёна", (8, 10, 16), 100, "Народные игрушки", "Россия"));
            allToys.Add("t17", new Dolls("t17", 2, "Barbie Mermaid", "Lory", (15, 15, 30), 2000, "Barbie", "Америка"));
            allToys.Add("t18", new Transport("t18", 14, "вертолет", false, "-", (30, 15, 15), 1999, "Fly", "Польша"));
            allToys.Add("t19", new Transport("t19", 14, "катер", false, "-", (30, 45, 60), 2450, "Barbie", "Америка"));
            allToys.Add("t20", new ControlledTr("t20", 8, "металл", "машина", true, "Mclaren MP4/4", false, (40, 58, 10), 10000, "RacingCar", "Италия"));
            Console.WriteLine("Игрушки для теста добавлены");
            End();
        }
        public static void DeleteToy()
        {
            Console.Clear();
            if (allToys.Count == 0)
                Console.WriteLine("Нет элементов");
            else
            {
                Console.Write("Игрушка, которую необходимо удалить\n" +
                              "Название: ");
                string nameDelete = Console.ReadLine();
                while (!allToys.ContainsKey(nameDelete))
                {
                    Console.WriteLine("Игрушка с таким названием не найдена, введите новое");
                    Console.Write("Название: ");
                    nameDelete = Console.ReadLine();
                }
                allToys.Remove(nameDelete);
                Console.WriteLine("Ишрушка " + nameDelete + " удалена");                
            }
            End();
        }        
        public static void CountToy()
        {
            Console.Clear();
            if (allToys.Count == 0)
                Console.WriteLine("Нет элементов");
            else
            {
                Console.Write("Количество игрушек в каждой категории\n" +
                              "-------------------------------------\n");
                var result = from t in allToys
                             group t by t.Value.GetType().Name into g
                             orderby g.Key
                             select new { Name = g.Key, Count = g.Count() };                
                foreach (var group in result)
                    Console.WriteLine(group.Name + ": " + group.Count);
            }
            End();
        }       
        public static void FindToys()
        {
            Console.Clear();
            if (allToys.Count == 0)
                Console.WriteLine("Нет элементов");
            else
            {
                Console.Write("Найти\n" +
                              "-------------------------------------------------------------------\n" +
                              "1 - все куклы марки Barbie\n" +
                              "2 - весь транспорт, произведенный в Америке\n" +
                              "3 - все конструкторы, стоимость которых меньше 5000 рублей\n" +
                              "4 - игрушку с минимальным возрастным ограничением в одной категории\n" +
                              "5 - среднюю стоимость по одной категории игрушек\n" +
                              "6 - стоимость одной категории игрушек\n");
                while (true)
                {
                    switch (char.ToLower(Console.ReadKey(true).KeyChar))
                    {
                        case '1': FindBarbie(); break;
                        case '2': FindAmerican(); break;
                        case '3': FindSets(); break;
                        case '4': FindMin(); break;
                        case '5': FindAverage(); break;
                        case '6': FindSum(); break;                        
                        default: break;
                    }
                }
            }
            End();
        }        
        public static void FindBarbie()
        {
            Console.Clear();
            Console.WriteLine("Найти все куклы марки Barbie\n" +
                              "----------------------------");
            var result = from t in allToys
                         where t.Value.Brand == "Barbie" && t.Value.GetType().Name == "Dolls"
                         select t;
            if (result.Count() == 0)            
                Console.WriteLine("Элементы не найдены");            
            else
            {
                foreach (var t in result)
                    Console.WriteLine(t.Value.GetAllInfo());
            }            
            End();
        }
        public static void FindAmerican()
        {
            Console.Clear();
            Console.WriteLine("Найти весь транспорт, произведенный в Америке\n" +
                              "---------------------------------------------");
            var result = from t in allToys
                         where (t.Value.Country == "Америка" || t.Value.Country == "America") && (t.Value.GetType().Name == "Transport" || t.Value.GetType().Name == "ControlledTr")
                         select t;
            if (result.Count() == 0)
                Console.WriteLine("Элементы не найдены");
            else
            {
                foreach (var t in result)
                    Console.WriteLine(t.Value.GetAllInfo());
            }            
            End();
        }
        public static void FindSets()
        {
            Console.Clear();
            Console.WriteLine("Найти все конструкторы, стоимость которых меньше 5000 рублей\n" +
                              "------------------------------------------------------------");
            var result = from t in allToys
                         where t.Value.Price < 5000 && (t.Value.GetType().Name == "ConstructionSets" || t.Value.GetType().Name == "MetalSets")
                         select t;
            if (result.Count() == 0)
                Console.WriteLine("Элементы не найдены");
            else
            {
                foreach (var t in result)
                    Console.WriteLine(t.Value.GetAllInfo());
            }
            End();
        }
        public static void FindMin()
        {
            Console.Clear();
            Console.Write("Найти игрушку с минимальным возрастным ограничением в одной категории\n" +
                          "---------------------------------------------------------------------\n" +
                          "1 - конструктор\n" +
                          "m - электронный конструктор\n" +
                          "2 - мягкая игрушка\n" +
                          "t - персонаж мультфильма\n" +
                          "3 - кукла\n" +
                          "4 - транспорт\n" +
                          "c - транспорт с радиоуправлением\n" +
                          "5 - среди всех категорий\n");
            while (true)
            {
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '1': FindMinCategory("ConstructionSets"); break;
                    case 'm': FindMinCategory("MetalSets"); break;
                    case '2': FindMinCategory("VariousToys"); break;
                    case 't': FindMinCategory("ToonToys"); break;
                    case '3': FindMinCategory("Dolls"); break;
                    case '4': FindMinCategory("Transport"); break;
                    case 'c': FindMinCategory("ControlledTr"); break;
                    case '5': FindMinCategory(null); break;
                    default: break;
                }
            }
        }
        public static void FindMinCategory(string category)
        {
            Console.Clear();
            if (category == null)
            {
                Console.WriteLine("Найти игрушку с минимальным возрастным ограничением среди всех\n" +
                                  "--------------------------------------------------------------");
                var result = (from t in allToys
                              orderby t.Value.Age
                              select t).First();
                Console.WriteLine(result.Value.GetAllInfo());
            }
            else
            {
                try
                {
                    Console.WriteLine("Найти игрушку с минимальным возрастным ограничением в категории " + category + "\n" +
                                      "--------------------------------------------------------------------------------");
                    var result = (from t in allToys
                                  where t.Value.GetType().Name == category
                                  orderby t.Value.Age
                                  select t).First();
                    Console.WriteLine(result.Value.GetAllInfo());
                }
                catch (Exception)
                {
                    Console.WriteLine("Не удалось выполнить действие, нет элементов в данной категории");
                }
            }
            End();
        }
        public static void FindAverage()
        {
            Console.Clear();
            Console.Write("Найти среднюю стоимость по одной категории игрушек\n" +
                          "--------------------------------------------------\n" +
                          "1 - конструктор\n" +
                          "m - электронный конструктор\n" +
                          "2 - мягкая игрушка\n" +
                          "t - персонаж мультфильма\n" +
                          "3 - кукла\n" +
                          "4 - транспорт\n" +
                          "c - транспорт с радиоуправлением\n" +
                          "5 - среди всех категорий\n");
            while (true)
            {
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '1': FindAverageCategory("ConstructionSets"); break;
                    case 'm': FindAverageCategory("MetalSets"); break;
                    case '2': FindAverageCategory("VariousToys"); break;
                    case 't': FindAverageCategory("ToonToys"); break;
                    case '3': FindAverageCategory("Dolls"); break;
                    case '4': FindAverageCategory("Transport"); break;
                    case 'c': FindAverageCategory("ControlledTr"); break;
                    case '5': FindAverageCategory(null); break;
                    default: break;
                }
            }
        }
        public static void FindAverageCategory(string category)
        {
            Console.Clear();
            if (category == null)
            {
                Console.WriteLine("Найти среднюю стоимость всех игрушек\n" +
                                  "------------------------------------");
                var result = allToys.Average(t => t.Value.Price);
                Console.WriteLine(Math.Round(result, 2) + " рублей");
            }
            else
            {
                try
                {
                    Console.WriteLine("Найти среднюю стоимость всех игрушек в катерогии " + category + "\n" +
                                      "-----------------------------------------------------------------");
                    var result = allToys.Where(t => t.Value.GetType().Name == category).Average(t => t.Value.Price);
                    Console.WriteLine(Math.Round(result, 2) + " рублей");
                }
                catch (Exception)
                {
                    Console.WriteLine("Не удалось выполнить действие, нет элементов в данной категории");
                }
            }
            End();
        }
        public static void FindSum()
        {
            Console.Clear();
            Console.Write("Найти стоимость одной категории игрушек\n" +
                          "---------------------------------------\n" +
                          "1 - конструктор\n" +
                          "m - электронный конструктор\n" +
                          "2 - мягкая игрушка\n" +
                          "t - персонаж мультфильма\n" +
                          "3 - кукла\n" +
                          "4 - транспорт\n" +
                          "c - транспорт с радиоуправлением\n" +
                          "5 - среди всех категорий\n");
            while (true)
            {
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '1': FindSumCategory("ConstructionSets"); break;
                    case 'm': FindSumCategory("MetalSets"); break;
                    case '2': FindSumCategory("VariousToys"); break;
                    case 't': FindSumCategory("ToonToys"); break;
                    case '3': FindSumCategory("Dolls"); break;
                    case '4': FindSumCategory("Transport"); break;
                    case 'c': FindSumCategory("ControlledTr"); break;
                    case '5': FindSumCategory(null); break;
                    default: break;
                }
            }
        }
        public static void FindSumCategory(string category)
        {
            Console.Clear();
            if (category == null)
            {
                Console.WriteLine("Найти стоимость всех игрушек\n" +
                                  "----------------------------");
                var result = allToys.Sum(t => t.Value.Price);
                Console.WriteLine(Math.Round(result, 2) + " рублей");
            }
            else
            {
                Console.WriteLine("Найти стоимость игрушек в катерогии " + category + "\n" +
                                  "----------------------------------------------------");
                var result = allToys.Where(t => t.Value.GetType().Name == category).Sum(t => t.Value.Price);
                Console.WriteLine(Math.Round(result, 2) + " рублей");
            }
            End();
        }
        public static void PrintToys()
        {
            Console.Clear();
            if (allToys.Count == 0)
                Console.WriteLine("Нет элементов");
            else
            {
                Console.Write("Вывести все игрушки\n" +
                              "--------------------------------\n" +
                              "1 - без сортировки\n" +
                              "2 - с сортировкой\n");                              
                while (true)
                {
                    switch (char.ToLower(Console.ReadKey(true).KeyChar))
                    {
                        case '1': PrintAllToys(); break;
                        case '2': PrintSort(); break;                        
                        default: break;
                    }
                }                
            }
            End();
        }
        public static void PrintAllToys()
        {
            Console.Clear();
            Console.WriteLine("Все игрушки\n" +
                              "--------------------------------");
            foreach (var toy in allToys)
            {
                Console.WriteLine("Класс: " + toy.Value.GetType().Name);
                Console.WriteLine(toy.Value.GetAllInfo());
            }
            End();
        }        
        public static void PrintSort()
        {
            Console.Clear();
            Console.Write("Отсортировать\n" +
                          "--------------------------------\n" +       
                          "1 - по названию\n" +
                          "2 - по возрастному ограничению\n" +
                          "3 - по длине упаковки\n" +
                          "4 - по ширине упаковки\n" +
                          "5 - по высоте упаковки\n" +
                          "6 - по цене\n" +
                          "7 - по марке\n" +
                          "8 - по стране-производителе\n");            
            List<Toys> allToysList = new List<Toys>();            
            foreach (var toy in allToys)
                allToysList.Add(toy.Value);
            while (true)
            {
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '1': PrintParametrs(ToysComparer.CompareField.byToyName); break;
                    case '2': PrintParametrs(ToysComparer.CompareField.byAge); break;
                    case '3': PrintParametrs(ToysComparer.CompareField.byLength); break;
                    case '4': PrintParametrs(ToysComparer.CompareField.byWidth); break;
                    case '5': PrintParametrs(ToysComparer.CompareField.byHeight); break;
                    case '6': PrintParametrs(ToysComparer.CompareField.byPrice); break;
                    case '7': PrintParametrs(ToysComparer.CompareField.byBrand); break;
                    case '8': PrintParametrs(ToysComparer.CompareField.byCountry); break;
                    default: break;
                }
            }
        }
        public static void PrintParametrs(ToysComparer.CompareField field)
        {
            Console.Clear();
            Console.WriteLine("Все игрушки\n" +
                              "--------------------------------");
            List<Toys> allToysList = new List<Toys>();            
            foreach (var toy in allToys)
                allToysList.Add(toy.Value);
            ToysComparer tComparer = new ToysComparer() { Field = field };
            allToysList.Sort(tComparer);            
            foreach (var toy in allToysList)
            {
                switch (field)
                {
                    case ToysComparer.CompareField.byToyName:
                        Console.WriteLine(toy.ToyName); break;
                    case ToysComparer.CompareField.byAge:
                        Console.WriteLine(toy.Age); break;
                    case ToysComparer.CompareField.byLength:
                        Console.WriteLine(toy.Length); break;
                    case ToysComparer.CompareField.byWidth:
                        Console.WriteLine(toy.Width); break;
                    case ToysComparer.CompareField.byHeight:
                        Console.WriteLine(toy.Height); break;                    
                    case ToysComparer.CompareField.byPrice:
                        Console.WriteLine(toy.Price); break;
                    case ToysComparer.CompareField.byBrand:
                        Console.WriteLine(toy.Brand); break;
                    case ToysComparer.CompareField.byCountry:
                        Console.WriteLine(toy.Country); break;
                }
               Console.WriteLine(toy.GetAllInfo());
            }
            End();
        }
        public static void End()
        {
            Console.WriteLine("\n----------------------------\n" +
                              "Для выхода нажмите 0");
            while (true)
            {
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '0': Main(); break;
                    default: break;
                }
            }
        }
    }
}