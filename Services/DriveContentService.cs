using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebFileExplorer.Models;
using WebFileExplorer.Utility;

namespace WebFileExplorer.Services
{
    public class DriveContentService
    {
        private readonly ExcludedOptions _excludedOptions;

        public DriveContentService(ExcludedOptions filterOptions)
        {
            _excludedOptions = filterOptions;
        }

        public IEnumerable<DirDto> GetAllDrivesContent()
        {
            ICollection<DirDto> driveDirs = new List<DirDto>();
            List<Task<DirDto>> tasks = new List<Task<DirDto>>();

            // Each drive's content can be calculated in a separate thread
            foreach (var drive in DriveInfo.GetDrives().Where(drive => !_excludedOptions.GetExcludedDrives().Contains(drive.Name)))
            {
                Task<DirDto> task = Task.Run(() =>
                {
                    return GetDriveContent(drive);
                });
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"{DateTime.Now}: Content for all drives is received");

            tasks.ForEach(task => driveDirs.Add(task.Result));

            return driveDirs;
        }

        private DirDto GetDriveContent(DriveInfo drive)
        {
            DirDto rootDir = new DirDto() { Name = drive.Name };
            List<Task<DirDto>> tasks = new List<Task<DirDto>>();

            var directories = drive.RootDirectory
                .EnumerateDirectories()
                .Where(dir => !dir.Attributes.HasFlag(FileAttributes.Hidden) && !_excludedOptions.GetExcludedRootDirs().Contains(dir.FullName));

            foreach (var file in drive.RootDirectory.EnumerateFiles())
            {
                rootDir.Size += file.Length;
                rootDir.Files.Add(new()
                {
                    Name = file.Name,
                    Size = file.Length
                });
            }

            // Only directories of the highest level of drive should be calculated in a separate thread
            foreach (var directory in directories)
            {
                Task<DirDto> task = Task.Run(() =>
                {
                    return GetInnerDirContent(directory);
                });
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"{DateTime.Now}: All content for drive {drive.Name} is received");

            tasks.ForEach(task =>
            {
                rootDir.Dirs.Add(task.Result);
                rootDir.Size += task.Result.Size;
            });

            return rootDir;
        }

        private DirDto GetInnerDirContent(DirectoryInfo directoryInfo)
        {
            DirDto dir = new() { Name = directoryInfo.Name };

            foreach (var fileInfo in directoryInfo.EnumerateFiles())
            {
                dir.Size += fileInfo.Length;
                dir.Files.Add(new()
                { 
                    Name = fileInfo.Name,
                    Size = fileInfo.Length
                });
            }

            foreach (var dirInfo in directoryInfo.EnumerateDirectories())
            {
                try
                {
                    DirDto innerDir = GetInnerDirContent(dirInfo);
                    dir.Size += innerDir.Size;
                    dir.Dirs.Add(innerDir);
                }
                catch (UnauthorizedAccessException) { }
            }

            return dir;
        }
    }
}
