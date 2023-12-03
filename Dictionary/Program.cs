using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Некорректный выбор. Пожалуйста, выберите снова.");
                    break;
            }
        }
    }
}

class DictionaryManager
{
    private Dictionary<string, Dictionary<string, List<string>>> dictionaries;

    public DictionaryManager()
    {
        dictionaries = new Dictionary<string, Dictionary<string, List<string>>>();
    }

    public void CreateDictionary()
    {
        Console.Write("Введите название словаря: ");
        string dictionaryName = Console.ReadLine();

        if (!dictionaries.ContainsKey(dictionaryName))
        {
            dictionaries[dictionaryName] = new Dictionary<string, List<string>>();
            Console.WriteLine($"Словарь '{dictionaryName}' создан.");
        }
        else
        {
            Console.WriteLine("Словарь с таким именем уже существует.");
        }
    }

    public void AddWord()
    {
        Console.Write("Введите название словаря: ");
        string dictionaryName = Console.ReadLine();

        if (dictionaries.ContainsKey(dictionaryName))
        {
            Console.Write("Введите слово: ");
            string word = Console.ReadLine();

            Console.Write("Введите перевод(ы) через запятую: ");
            string[] translations = Console.ReadLine().Split(',');

            dictionaries[dictionaryName][word] = translations.ToList();
            Console.WriteLine($"Слово '{word}' добавлено в словарь '{dictionaryName}'.");
        }
        else
        {
            Console.WriteLine("Словарь не найден.");
        }
    }

    public void ReplaceWord()
    {
        Console.Write("Введите название словаря: ");
        string dictionaryName = Console.ReadLine();

        if (dictionaries.ContainsKey(dictionaryName))
        {
            Console.Write("Введите слово, которое нужно заменить: ");
            string oldWord = Console.ReadLine();

            if (dictionaries[dictionaryName].ContainsKey(oldWord))
            {
                Console.Write("Введите новое слово: ");
                string newWord = Console.ReadLine();

                Console.Write("Введите перевод(ы) через запятую: ");
                string[] translations = Console.ReadLine().Split(',');

                dictionaries[dictionaryName][newWord] = translations.ToList();
                dictionaries[dictionaryName].Remove(oldWord);

                Console.WriteLine($"Слово '{oldWord}' заменено на '{newWord}' в словаре '{dictionaryName}'.");
            }
            else
            {
                Console.WriteLine("Слово не найдено в словаре.");
            }
        }
        else
        {
            Console.WriteLine("Словарь не найден.");
        }
    }

    public void DeleteWord()
    {
        Console.Write("Введите название словаря: ");
        string dictionaryName = Console.ReadLine();

        if (dictionaries.ContainsKey(dictionaryName))
        {
            Console.Write("Введите слово, которое нужно удалить: ");
            string wordToDelete = Console.ReadLine();

            if (dictionaries[dictionaryName].ContainsKey(wordToDelete))
            {
                // Удаляем слово и все его переводы
                dictionaries[dictionaryName].Remove(wordToDelete);
                Console.WriteLine($"Слово '{wordToDelete}' удалено из словаря '{dictionaryName}'.");
            }
            else
            {
                Console.WriteLine("Слово не найдено в словаре.");
            }
        }
        else
        {
            Console.WriteLine("Словарь не найден.");
        }
    }

    public void DeleteTranslation()
    {
        Console.Write("Введите название словаря: ");
        string dictionaryName = Console.ReadLine();

        if (dictionaries.ContainsKey(dictionaryName))
        {
            Console.Write("Введите слово, у которого нужно удалить перевод: ");
            string wordToDeleteTranslation = Console.ReadLine();

            if (dictionaries[dictionaryName].ContainsKey(wordToDeleteTranslation))
            {
                Console.Write("Введите перевод, который нужно удалить: ");
                string translationToDelete = Console.ReadLine();

                if (dictionaries[dictionaryName][wordToDeleteTranslation].Count > 1)
                {
                    // Удаляем конкретный вариант перевода
                    dictionaries[dictionaryName][wordToDeleteTranslation].Remove(translationToDelete);
                    Console.WriteLine($"Перевод '{translationToDelete}' удален у слова '{wordToDeleteTranslation}' в словаре '{dictionaryName}'.");
                }
                else
                {
                    Console.WriteLine("Нельзя удалить последний вариант перевода. У слова должен остаться хотя бы один перевод.");
                }
            }
            else
            {
                Console.WriteLine("Слово не найдено в словаре.");
            }
        }
        else
        {
            Console.WriteLine("Словарь не найден.");
        }
    }

    public void SearchTranslation()
    {
        Console.Write("Введите название словаря: ");
        string dictionaryName = Console.ReadLine();

        if (dictionaries.ContainsKey(dictionaryName))
        {
            Console.Write("Введите слово для поиска перевода: ");
            string wordToSearch = Console.ReadLine().ToLower(); // Приведение к нижнему регистру

            if (dictionaries[dictionaryName].ContainsKey(wordToSearch))
            {
                Console.WriteLine($"Перевод слова '{wordToSearch}' в словаре '{dictionaryName}':");
                foreach (string translation in dictionaries[dictionaryName][wordToSearch])
                {
                    Console.WriteLine(translation);
                }
            }
            else
            {
                Console.WriteLine("Слово не найдено в словаре.");
            }
        }
        else
        {
            Console.WriteLine("Словарь не найден.");
        }
    }

    public void ExportDictionary()
    {
        Console.Write("Введите название словаря для экспорта: ");
        string dictionaryName = Console.ReadLine();

        if (dictionaries.ContainsKey(dictionaryName))
        {
            Console.Write("Введите имя файла для сохранения: ");
            string fileName = Console.ReadLine();

            // Получение пути к папке программы
            string directoryPath = AppDomain.CurrentDomain.BaseDirectory;

            // Сочетание пути к папке программы и имени файла
            string filePath = Path.Combine(directoryPath, fileName);

            using (StreamWriter writer = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
            {
                foreach (var entry in dictionaries[dictionaryName])
                {
                    writer.WriteLine($"{entry.Key}: {string.Join(", ", entry.Value)}");
                }
            }

            Console.WriteLine($"Словарь '{dictionaryName}' экспортирован в файл '{filePath}'.");
        }
        else
        {
            Console.WriteLine("Словарь не найден.");
        }
    }
}
