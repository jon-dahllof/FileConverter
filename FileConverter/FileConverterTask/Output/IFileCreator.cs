using FileConverterTask.Model;

namespace FileConverterTask.Output
{
    public interface IFileCreator
    {
        void CreateOutput(IEnumerable<Person> persons, FileInfo outputfilename);
    }
}