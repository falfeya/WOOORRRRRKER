using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Worker
    {
        Random rnd = new Random();
        Repository r1;
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }       
        List<Worker> AddWorker()
        {
            List<Worker> a= new List<Worker>();
            for (int i = 0; i < rnd.Next(1, 5); i++)
            {
                a.Add(new Worker());
            }
            return a;
        }
    }
    public class Repository
    {
        string[] readText = new string[] { };
        private string fileName = "fileName.txt";
        string path = Directory.GetCurrentDirectory();
        public List<string> Zagrz(List<string> a)
        {
            readText = File.ReadAllLines($@"{path}{fileName}");
            foreach(string i in readText) 
            { 
                a.Add(i);
            }
            return a;
        }
        public void Show()
        {
            FileStream fs1 = new FileStream($@"{path}\{fileName}", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs1);
            Console.WriteLine(sr.ReadToEnd());
            sr.Close();
        }
        public string _fileName;
        public void ViewAll()
        {
            if (!File.Exists(_fileName))
            {
                Console.WriteLine("Файл не найден");
                return;
            }

            using (var reader = new StreamReader(_fileName))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var worker = new Worker
                    {
                        ID = int.Parse(values[0]),
                        Date = DateTime.Parse(values[1]),
                        Name = values[2],
                        Age = int.Parse(values[3])
                    };

                    Console.WriteLine($"{worker.ID}, {worker.Date}, {worker.Name}, {worker.Age}");
                }
            }
        }
        public void View(int id)
        {
            if (!File.Exists(_fileName))
            {
                Console.WriteLine("Файл не найден");
                return;
            }
            using (var reader = new StreamReader(_fileName))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (int.Parse(values[0]) == id)
                    {
                        var worker = new Worker
                        {
                            ID = int.Parse(values[0]),
                            Date = DateTime.Parse(values[1]),
                            Name = values[2],
                            Age = int.Parse(values[3])
                        };

                        Console.WriteLine($"{worker.ID}, {worker.Date}, {worker.Name}, {worker.Age}");
                        return;
                    }
                }
                Console.WriteLine("Запись не найдена");
            }
        }
        public void Create(Worker worker)
        {
            using (var writer = new StreamWriter(_fileName, true))
            {
                writer.WriteLine($"{worker.ID},{worker.Date},{worker.Name},{worker.Age}");
            }
        }
        public void Delete(int id)
        {
            if (!File.Exists(_fileName))
            {
                Console.WriteLine("Файл не найден");
                return;
            }
            var tempFileName = Path.GetTempFileName();
            using (var reader = new StreamReader(_fileName))
            using (var writer = new StreamWriter(tempFileName))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (int.Parse(values[0]) != id)
                    {
                        writer.WriteLine(line);
                    }
                }
            }
            File.Delete(_fileName);
            File.Move(tempFileName, _fileName);
        }
        public void LoadByDateRange(DateTime startDate, DateTime endDate)
        {
            if (!File.Exists(_fileName))
            {
                Console.WriteLine("Файл не найден");
                return;
            }
            using (var reader = new StreamReader(_fileName))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var date = DateTime.Parse(values[1]);

                    if (date >= startDate && date <= endDate)
                    {
                        var worker = new Worker
                        {
                            ID = int.Parse(values[0]),
                            Date = date,
                            Name = values[2],
                            Age = int.Parse(values[3])
                        };
                        Console.WriteLine($"{worker.ID}, {worker.Date}, {worker.Name}, {worker.Age}");
                    }
                }
            }
        }
    }
}
