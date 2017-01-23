# Directory Cleaner

Loops through a Directory recursively and deletes sub directories that match a particular pattern.

## Example Usage

`DirectoryCleaner.exe -d C:\FolderToLoopThrough -p ".svn"`

This will remove all the .svn folders from a the `C:\FolderToLoopThrough` directory. It will also mark files as not readonly so they can be deleted.
