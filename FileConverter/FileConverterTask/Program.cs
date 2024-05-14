// See https://aka.ms/new-console-template for more information

using FileConverterTask;
using FileConverterTask.Input;
using FileConverterTask.Output;

new FileConverter(new OldFileSystemParser(), new XmlOutPutCreator()).Convert(GetInputFile(), GetOutputFile());

FileInfo GetInputFile()
{
    return new FileInfo(args[0]);
}

FileInfo GetOutputFile()
{
    return new FileInfo(args[1]);
}
