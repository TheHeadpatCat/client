namespace GagSpeak.CkCommons.FileSystem;

public enum SortMode
{
    FoldersFirst,
    Lexicographical,
}

public interface ISortMode<T> where T : class
{
    string Name        { get; }
    string Description { get; }

    IEnumerable<FileSystem<T>.IPath> GetChildren(FileSystem<T>.Folder folder);

    public static readonly ISortMode<T> FoldersFirst           = new FoldersFirstT();
    public static readonly ISortMode<T> Lexicographical        = new LexicographicalT();

    // Folder first modifier.
    private struct FoldersFirstT : ISortMode<T>
    {
        public string Name
            => "Folders First";

        public string Description
            => "In each folder, sort all subfolders lexicographically, then sort all leaves lexicographically.";

        public IEnumerable<FileSystem<T>.IPath> GetChildren(FileSystem<T>.Folder folder)
            => folder.GetSubFolders().Cast<FileSystem<T>.IPath>().Concat(folder.GetLeaves());
    }

    // Default
    private struct LexicographicalT : ISortMode<T>
    {
        public string Name
            => "Lexicographical";

        public string Description
            => "In each folder, sort all children lexicographically.";

        public IEnumerable<FileSystem<T>.IPath> GetChildren(FileSystem<T>.Folder folder)
            => folder.Children;
    }
}
