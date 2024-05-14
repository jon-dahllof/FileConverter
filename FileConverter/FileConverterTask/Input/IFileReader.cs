using FileConverterTask.Model;

namespace FileConverterTask.Input
{
    internal interface IFileReader
    {
        internal IEnumerable<Person> GetPersons(FileInfo fileInfo);
    }
}