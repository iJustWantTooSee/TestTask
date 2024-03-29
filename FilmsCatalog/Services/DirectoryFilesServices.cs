﻿using FilmsCatalog.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FilmsCatalog.Services
{
    public class DirectoryFilesServices : IDirectoryFilesServices
    {
        private static readonly HashSet<String> AllowedExtensions = new HashSet<String> { ".jpg", ".jpeg", ".png", ".gif", ".tiff", ".raw", ".bmp", ".jfif" };
        private readonly ILogger logger;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public DirectoryFilesServices(ILogger<IDirectoryFilesServices> logger,
                IWebHostEnvironment hostingEnvironment,
                IHttpContextAccessor httpContextAccessor,
                UserManager<User> userManager)
        {
            this.logger = logger;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public bool IsAllowedFileFormat(IFormFile file)
        {
            return DirectoryFilesServices.AllowedExtensions.Contains(this.GetFileExtension(file));
        }

        public String GetFileExtension(IFormFile file)
        {
            var fileName = Path
               .GetFileName(ContentDispositionHeaderValue
               .Parse(file.ContentDisposition)
               .FileName.Value
               .Trim('"'));

            var fileExt = Path.GetExtension(fileName).ToLower();
            return fileExt;
        }

        public string GetNewFileName(IFormFile file)
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + Guid.NewGuid() + this.GetFileExtension(file);
        }

        public async Task AddFileToServer(IFormFile file, String localDirectory, String fileName)
        {
            var directoryToSave = Path
                .Combine(this.hostingEnvironment.WebRootPath, localDirectory);
            if (!Directory.Exists(directoryToSave))
            {
                Directory.CreateDirectory(directoryToSave);
            }
            
            using (var fileStream = new FileStream($"{directoryToSave}/{fileName}", FileMode.CreateNew, FileAccess.ReadWrite, FileShare.Read))
            {
                await file.CopyToAsync(fileStream);
            }

            var user = this.userManager.GetUserId(this.httpContextAccessor.HttpContext.User);
            this.logger.LogInformation($"A file with a local path was uploaded: {localDirectory}/{fileName}." +
                $"The user who uploaded the file: {user}" +
                $" Logging time: {DateTime.Now}");
        }

        public bool DeleteFile(string localPathToFile)
        {
            var pathToFileToDelete = Path
               .Combine(this.hostingEnvironment.WebRootPath, localPathToFile);

            var user = this.userManager.GetUserId(this.httpContextAccessor.HttpContext.User);

            this.logger.LogInformation($"Attempt to delete a file by path: {localPathToFile}." +
                 $"The user who attempt to delete the file: {user}" +
                $" Logging time: {DateTime.Now}");
           
            if (!File.Exists(pathToFileToDelete))
            {
                return false;
            }
            
            this.logger.LogInformation($"A file was deleted along the path: {localPathToFile}." +
                $"The user who was deleted the file: {user}" +
                $" Logging time: {DateTime.Now}");

            System.IO.File.Delete(pathToFileToDelete);
            return true;
        }

    }
}
