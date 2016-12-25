namespace Contracts.Presentation
{
    public interface IFileServiceOptions
    {
        bool DereferenceLinks { get; }
        string DefaultExtensions { get; }
        string Filter { get; }
        string InitialDirectory { get; }
        string Title { get; }
    }
}
