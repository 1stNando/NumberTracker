using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.IO;
using CsvHelper;

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

            //After we put the Write together, we then add a READ stream.
            var fileReader = new StreamReader("numbers.csv");

            // Creates a list of numbers we will be tracking
            var numbers = new List<int>();

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
