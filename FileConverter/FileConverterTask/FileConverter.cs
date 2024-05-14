using FileConverterTask.Input;
using FileConverterTask.Model;
using FileConverterTask.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConverterTask
{
    internal class FileConverter
    {
        internal FileConverter(IFileReader fromFileParser, IFileCreator fileCreator)
        {
            FileParser = fromFileParser;
            FileCreator = fileCreator;
        }

        internal void Convert(FileInfo fromFile, FileInfo toFile)
        {
            if ((fromFile?.Exists ?? false) == false)
            {
                Console.WriteLine($"From file path doesn't exists! {fromFile?.FullName}");
                return;
            }

            IEnumerable<Person> persons;
            try
            {
                persons = FileParser.GetPersons(fromFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error when parsing from file {fromFile?.FullName}: {ex.Message}");
                throw;
            }
            try
            {
                FileCreator.CreateOutput(persons, toFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error when creating file {toFile.FullName}: {ex.Message}");
                throw;
            }
        }

        private IFileReader FileParser { get; }

        private IFileCreator FileCreator { get; }
    }
}
