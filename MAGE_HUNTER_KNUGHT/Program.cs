
using System;
using System.Collections.Generic;
namespace MAGE_HUNTER_KNUGHT
{
    

    public enum CharacterClass
    {
        Knight = 1,
        Hunter = 2,
        Mage = 3,
    }

    public enum ItemType
    {
        Key,
        Healing,
        Weapon,
        Armor,
        Potion
    }

    public class Item
    {
        public string Name { get; set; }
        public ItemType Type { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }

        public Item(string name, ItemType type, int quantity, string description)
        {
            Name = name;
            Type = type;
            Quantity = quantity;
            Description = description;
        }
    }

    public class Inventory
    {
        public List<Item> Items { get; set; } = new List<Item>();

        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            Items.Remove(item);
        }

        public void DisplayInventory()
        {
            if (Items.Count == 0)
            {
                Console.WriteLine("Инвентарь пуст.");
            }
            else
            {
                Console.WriteLine("Инвентарь:");
                foreach (var item in Items)
                {
                    Console.WriteLine($"- {item.Name} ({item.Type}), количество: {item.Quantity}, описание: {item.Description}");
                }
            }
        }
    }

    public class Chest
    {
        public List<Item> Items { get; set; } = new List<Item>();
        public int Capacity { get; set; }

        public Chest(int capacity)
        {
            Capacity = capacity;
        }

        public void AddItem(Item item)
        {
            if (Items.Count < Capacity)
            {
                Items.Add(item);
            }
            else
            {
                Console.WriteLine("Сундук полон.");
            }
        }

        public void RemoveItem(Item item)
        {
            Items.Remove(item);
        }

        public void DisplayChest()
        {
            if (Items.Count == 0)
            {
                Console.WriteLine("Сундук пуст.");
            }
            else
            {
                Console.WriteLine("Сундук:");
                foreach (var item in Items)
                {
                    Console.WriteLine($"- {item.Name} ({item.Type}), количество: {item.Quantity}, описание: {item.Description}");
                }
            }
        }
    }

    public class Character
    {
        public string Name { get; set; }
        public int Health { get; init; }
        public int Intelligence { get; init; }
        public int Armor { get; init; }
        public int Strength { get; init; }
        public Inventory Inventory { get; set; }
        public Chest Chest { get; set; }

        public Character(string name, CharacterClass characterClass, int chestCapacity)
        {
            Name = name;
            Inventory = new Inventory();
            Chest = new Chest(chestCapacity);

            switch (characterClass)
            {
                case CharacterClass.Knight:
                    Health = 100;
                    Armor = 60;
                    Intelligence = Random.Shared.Next(1, 20);
                    Strength = 42;
                    break;
                case CharacterClass.Hunter:
                    Health = 80;
                    Armor = 30;
                    Intelligence = Random.Shared.Next(10, 30);
                    Strength = 35;
                    break;
                case CharacterClass.Mage:
                    Health = 60;
                    Armor = 20;
                    Intelligence = 100;
                    Strength = Random.Shared.Next(5, 15);
                    break;
            }
        }

        public void MoveItemsToInventory()
        {
            foreach (var item in Chest.Items)
            {
                Inventory.AddItem(item);
            }
            Chest.Items.Clear();
            Console.WriteLine("Предметы из сундука перемещены в инвентарь.");
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Выберите класс персонажа:");
            Console.WriteLine("1 - Knight");
            Console.WriteLine("2 - Hunter");
            Console.WriteLine("3 - Mage");
            int numberOfClass = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите имя:");
            string name = Console.ReadLine();
            Console.WriteLine("Введите вместимость сундука:");
            int chestCapacity = Convert.ToInt32(Console.ReadLine());

            CharacterClass characterClass = (CharacterClass)numberOfClass;
            Character character = new Character(name, characterClass, chestCapacity);

            // Пример добавления предметов в сундук
            character.Chest.AddItem(new Item("Меч", ItemType.Weapon, 1, "Острый меч"));
            character.Chest.AddItem(new Item("Зелье лечения", ItemType.Healing, 3, "Восстанавливает здоровье"));

            Console.WriteLine($"Имя: {character.Name}, класс: {characterClass}, сила: {character.Strength}");
            character.Chest.DisplayChest();

            // Перемещение предметов из сундука в инвентарь
            character.MoveItemsToInventory();
            character.Inventory.DisplayInventory();
        }
    }
}
