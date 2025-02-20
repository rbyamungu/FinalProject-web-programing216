using System;
using System.Threading.Tasks;

namespace HalfAndHalf.Services
{
    /// <summary>
    /// Interface for storage services, typically used for file storage operations
    /// </summary>
    public interface IStorageService
    {
        /// <summary>
        /// Uploads a file to storage
        /// </summary>
        /// <param name="fileName">Name of the file</param>
        /// <param name="fileData">File content as byte array</param>
        /// <returns>URL or identifier of the uploaded file</returns>
        Task<string> UploadFileAsync(string fileName, byte[] fileData);

        /// <summary>
        /// Downloads a file from storage
        /// </summary>
        /// <param name="fileName">Name of the file to download</param>
        /// <returns>File content as byte array</returns>
        Task<byte[]> DownloadFileAsync(string fileName);

        /// <summary>
        /// Deletes a file from storage
        /// </summary>
        /// <param name="fileName">Name of the file to delete</param>
        /// <returns>True if deletion was successful, otherwise false</returns>
        Task<bool> DeleteFileAsync(string fileName);
    }
}