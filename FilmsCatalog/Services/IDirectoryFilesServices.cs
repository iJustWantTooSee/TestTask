using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsCatalog.Services
{
    public interface IDirectoryFilesServices
    {
        Boolean IsAllowedFileFormat(IFormFile file);

        String GetFileExtension(IFormFile file);

        String GetNewLocalFilePath(IFormFile file, String localPathToDownloadDirectory);

        Task AddFileToServer(IFormFile file, String localPathToDownloadDirectory);

        Boolean DeleteFile(String localPathToFile);

    }
}
