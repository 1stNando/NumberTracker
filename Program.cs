using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace NumberTracker
{
    class Program
    {

        // static void SaveStudents(List<Student> students)
        // {
        //   var writer = new StreamWriter("students.csv");
        //   var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
        //   csvWriter.WriteRecords(students);
        //   writer.Flush();
        // }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Number Tracker");

            //Part 2: After we put the Write together, we then add a READ stream.
            //so we create a file reader to read from numbers.csv.
            //var fileReader = new StreamReader("numbers.csv");

            //Part 3: use TextReader instead of a fileReader. This also solves cases where there is no file.csv found.
            // Algorithm: Detect if there is no file.
            // • If there is a file, use a StreamReader pointing to the file
            // • If there isn't a file, use a StringReader with an empty string
            TextReader reader;
            if (File.Exists("numbers.csv"))
            {
                //Assign StreamReader to read from the file
                reader = new StreamReader("numbers.csv");
            }
            else
            {
                //Assign StringReader to read from an empty string. And parse the csv from this empty string.
                reader = new StringReader("");
            }

            //Then we follow the reader by creating a configuration that indicates this CSV file has no header
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                //Tell the reader not to interpret the first row as a "header" since it is just the first number.
                HasHeaderRecord = false,
            };
            var csvReader = new CsvReader(reader, config);

            var numbers = csvReader.GetRecords<int>().ToList();

            ////////////////////////////////////////////Part 1 below:
            // Creates a list of numbers we will be tracking
            //var numbers = new List<int>();

            // Controls if we are still running our loop asking for more numbers
            var isRunning = true;

            // While we are running
            while (isRunning)
            {
                // Show the list of numbers
                Console.WriteLine("------------------");
                foreach (var number in numbers)
                {
                    Console.WriteLine(number);
                }
                Console.WriteLine("------------------");

                // Ask for a new number or the word quit to end
                Console.Write("Enter a number to store, or '(q)uit' to end: ");
                var input = Console.ReadLine().ToLower();

                if (input == "q")
                {
                    // If the input is quit, turn off the flag to keep looping
                    isRunning = false;
                }
                else
                {
                    // Parse the number and add it to the list of numbers
                    var number = int.Parse(input);
                    numbers.Add(number);
                }
            }

            // Create a stream for writing information into a file
            var fileWriter = new StreamWriter("numbers.csv");

            // Create an object that can write CSV to the fileWriter
            var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);

            // Hey CsvWriter! Here is the the data I want you to write about.  
            csvWriter.WriteRecords(numbers);

            // Tell the file we are done
            fileWriter.Close();
        }
    }
}
