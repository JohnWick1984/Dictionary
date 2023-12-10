using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{

    class DictionaryManager
    {
        private Dictionary<string, Dictionary<string, List<string>>> dictionaries;

        public DictionaryManager()
        {
            dictionaries = LoadDictionariesFromFile();
        }

        public void CreateDictionary()
        {
            Console.Write("Введите название словаря: ");
            string dictionaryName = Console.ReadLine().Trim().ToLower();

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
            string dictionaryName = Console.ReadLine().Trim().ToLower();

            if (dictionaries.ContainsKey(dictionaryName))
            {
                Console.Write("Введите слово: ");
                string word = Console.ReadLine().Trim();

                Console.Write("Введите перевод(ы) через запятую: ");
                string[] translations = Console.ReadLine().Split(',');

                dictionaries[dictionaryName][word] = translations.Select(t => t.Trim()).ToList();
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
            string dictionaryName = Console.ReadLine().Trim().ToLower();

            if (dictionaries.ContainsKey(dictionaryName))
            {
                Console.Write("Введите слово, которое нужно заменить: ");
                string oldWord = Console.ReadLine().Trim();

                if (dictionaries[dictionaryName].ContainsKey(oldWord))
                {
                    Console.Write("Введите новое слово: ");
                    string newWord = Console.ReadLine().Trim();

                    Console.Write("Введите перевод(ы) через запятую: ");
                    string[] translations = Console.ReadLine().Split(',');

                    dictionaries[dictionaryName][newWord] = translations.Select(t => t.Trim()).ToList();
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
            string dictionaryName = Console.ReadLine().Trim().ToLower();

            if (dictionaries.ContainsKey(dictionaryName))
            {
                Console.Write("Введите слово, которое нужно удалить: ");
                string wordToDelete = Console.ReadLine().Trim();

                if (dictionaries[dictionaryName].ContainsKey(wordToDelete))
                {
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
            string dictionaryName = Console.ReadLine().Trim().ToLower();

            if (dictionaries.ContainsKey(dictionaryName))
            {
                Console.Write("Введите слово, у которого нужно удалить перевод: ");
                string wordToDeleteTranslation = Console.ReadLine().Trim();

                if (dictionaries[dictionaryName].ContainsKey(wordToDeleteTranslation))
                {
                    Console.Write("Введите перевод, который нужно удалить: ");
                    string translationToDelete = Console.ReadLine().Trim();

                    if (dictionaries[dictionaryName][wordToDeleteTranslation].Count > 1)
                    {
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
            string dictionaryName = Console.ReadLine().Trim().ToLower();

            if (dictionaries.ContainsKey(dictionaryName))
            {
                Console.Write("Введите слово для поиска перевода: ");
                string wordToSearch = Console.ReadLine().Trim();

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
            string dictionaryName = Console.ReadLine().Trim().ToLower();

            if (dictionaries.ContainsKey(dictionaryName))
            {
                Console.Write("Введите имя файла для сохранения: ");
                string fileName = Console.ReadLine();

                string directoryPath = AppDomain.CurrentDomain.BaseDirectory;
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

        private Dictionary<string, Dictionary<string, List<string>>> LoadDictionariesFromFile()
        {
            Dictionary<string, Dictionary<string, List<string>>> loadedDictionaries = new Dictionary<string, Dictionary<string, List<string>>>();

            try
            {
                string directoryPath = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = Path.Combine(directoryPath, "dictionaries.txt");

                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath, System.Text.Encoding.UTF8))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] parts = line.Split(':');

                            if (parts.Length == 3)
                            {
                                string dictionaryName = parts[0].Trim().ToLower();
                                string word = parts[1].Trim();
                                string[] translations = parts[2].Split(',').Select(t => t.Trim()).ToArray();

                                if (!loadedDictionaries.ContainsKey(dictionaryName))
                                {
                                    loadedDictionaries[dictionaryName] = new Dictionary<string, List<string>>();
                                }

                                loadedDictionaries[dictionaryName][word] = translations.ToList();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении словарей из файла: {ex.Message}");
            }

            return loadedDictionaries;
        }

        public void SaveDictionariesToFile()
        {
            try
            {
                string directoryPath = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = Path.Combine(directoryPath, "dictionaries.txt");

                using (StreamWriter writer = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
                {
                    foreach (var dictionary in dictionaries)
                    {
                        foreach (var entry in dictionary.Value)
                        {
                            writer.WriteLine($"{dictionary.Key}: {entry.Key}: {string.Join(", ", entry.Value)}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении словарей в файл: {ex.Message}");
            }
        }
    }

}
