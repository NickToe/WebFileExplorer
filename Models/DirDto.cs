using System.Collections.Generic;

namespace WebFileExplorer.Models
{
    public record DirDto
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public ICollection<DirDto> Dirs { get; set; } = new List<DirDto>();
        public ICollection<FileDto> Files { get; set; } = new List<FileDto>();
    }
}
