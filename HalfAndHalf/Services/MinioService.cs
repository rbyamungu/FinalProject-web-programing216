using Microsoft.AspNetCore.Http;
using Minio;
using Minio.DataModel;
using Minio.DataModel.Args;
using Microsoft.Extensions.Configuration;

namespace HalfAndHalf.Services
{
    public class MinioService : IStorageService
    {
        private readonly IMinioClient _minioClient;
        private readonly IConfiguration _configuration;
        private readonly string _defaultBucketName;

        public MinioService(IConfiguration configuration)
        {
            _configuration = configuration;
            _defaultBucketName = configuration["Minio:DefaultBucketName"] ?? "default-bucket";

            _minioClient = new MinioClient()
                .WithEndpoint(configuration["Minio:Endpoint"])
                .WithCredentials(
                    configuration["Minio:AccessKey"],
                    configuration["Minio:SecretKey"]
                )
                .WithSSL(configuration.GetValue<bool>("Minio:WithSSL"))
                .Build();
        }

        public async Task<string> UploadFileAsync(string fileName, byte[] fileData)
        {
            try
            {
                // Check if bucket exists, if not create it
                var bucketExistsArgs = new BucketExistsArgs().WithBucket(_defaultBucketName);
                bool exists = await _minioClient.BucketExistsAsync(bucketExistsArgs);
                if (!exists)
                {
                    var makeBucketArgs = new MakeBucketArgs().WithBucket(_defaultBucketName);
                    await _minioClient.MakeBucketAsync(makeBucketArgs);
                }

                // Generate a unique filename if not provided
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    fileName = $"{Guid.NewGuid()}.bin";
                }

                using var stream = new MemoryStream(fileData);
                var putObjectArgs = new PutObjectArgs()
                    .WithBucket(_defaultBucketName)
                    .WithObject(fileName)
                    .WithStreamData(stream)
                    .WithObjectSize(fileData.Length)
                    .WithContentType("application/octet-stream");

                await _minioClient.PutObjectAsync(putObjectArgs);
                return fileName;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error uploading file to Minio: {ex.Message}", ex);
            }
        }

        public async Task<byte[]> DownloadFileAsync(string fileName)
        {
            try
            {
                using var memoryStream = new MemoryStream();
                var getObjectArgs = new GetObjectArgs()
                    .WithBucket(_defaultBucketName)
                    .WithObject(fileName)
                    .WithCallbackStream(stream =>
                    {
                        stream.CopyTo(memoryStream);
                    });

                await _minioClient.GetObjectAsync(getObjectArgs);
                return memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error downloading file from Minio: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteFileAsync(string fileName)
        {
            try
            {
                var removeObjectArgs = new RemoveObjectArgs()
                    .WithBucket(_defaultBucketName)
                    .WithObject(fileName);

                await _minioClient.RemoveObjectAsync(removeObjectArgs);
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return false;
            }
        }

        // Optional: Method to upload IFormFile if you still want to keep that functionality
        public async Task<string> UploadFileAsync(IFormFile file, string bucketName = null)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            return await UploadFileAsync(
                $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}", 
                memoryStream.ToArray()
            );
        }
    }
}