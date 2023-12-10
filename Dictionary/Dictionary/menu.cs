using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dictionary;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        DictionaryManager dictionaryManager = new DictionaryManager();

        while (true)
        {
            Console.WriteLine("1. Создать словарь");
            Console.WriteLine("2. Добавить слово в словарь");
            Console.WriteLine("3. Заменить слово в словаре");
            Console.WriteLine("4. Удалить слово из словаря");
            Console.WriteLine("5. Удалить перевод слова");
            Console.WriteLine("6. Искать перевод слова");
            Console.WriteLine("7. Экспортировать словарь");
            Console.WriteLine("8. Выйти");

            Console.Write("Выберите действие: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    dictionaryManager.CreateDictionary();
                    break;
                case "2":
                    dictionaryManager.AddWord();
                    break;
                case "3":
                    dictionaryManager.ReplaceWord();
                    break;
                case "4":
                    dictionaryManager.DeleteWord();
                    break;
                case "5":
                    dictionaryManager.DeleteTranslation();
                    break;
                case "6":
                    dictionaryManager.SearchTranslation();
                    break;
                case "7":
                    dictionaryManager.ExportDictionary();
                    break;
                case "8":
                    dictionaryManager.SaveDictionariesToFile();
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Некорректный выбор. Пожалуйста, выберите снова.");
                    break;
            }
        }
    }
}
