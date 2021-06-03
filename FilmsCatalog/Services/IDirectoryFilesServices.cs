using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsCatalog.Services
{
    public interface IDirectoryFilesServices
    {
        /// <summary>
        /// Checks the file extension. Is the file extension valid for uploading a file to the server.
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Returns true if the file has a valid extension. otherwise returns false</returns>
        Boolean IsAllowedFileFormat(IFormFile file);

        /// <summary>
        /// Returns the file extension
        /// </summary>
        /// <param name="file"></param>
        /// <returns>File extension in the form of a string in lowercase letters. Format ".extension"</returns>
        String GetFileExtension(IFormFile file);

        /// <summary>
        /// Generates a new name for the file
        /// </summary>
        /// <param name="file"></param>
        /// <returns>A string with a new file name</returns>
        String GetNewFileName(IFormFile file);

        /// <summary>
        /// Uploads the file to the specified directory on the server under the specified file name. If the specified directory does not exist, it is created on the server.
        /// </summary>
        /// <param name="file">Downloadable file.</param>
        /// <param name="localDirectory">Local path to the directory on the server.</param>
        /// <param name="fileName">The name of the file under which it will be saved.</param>
        /// <returns></returns>
        Task AddFileToServer(IFormFile file, String localDirectory ,String  fileName);

        /// <summary>
        /// Deletes the file from at the specified link. Returns true if the file was deleted and false if the file was not found at the specified path
        /// </summary>
        /// <param name="localPathToFile">Local file path</param>
        Boolean DeleteFile(String localPathToFile);

    }
}
