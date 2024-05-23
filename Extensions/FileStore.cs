using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using estatedocflow.api.Models.Dtos;
using System.Net;

namespace estatedocflow.api.Extensions
{
    public class FileStore
    {
        private readonly IConfiguration _config;
        public FileStore(IConfiguration config)
        {
            _config = config;
        }

        public async Task<ServiceResponse<string>> UploadDocument(string filePath, string keyName)
        {
            var sr = new ServiceResponse<string>();
            string bucketName = _config.GetSection("AWSCredentials").GetValue<string>("BucketName");
            string AccessKey = _config.GetSection("AWSCredentials").GetValue<string>("AccessKey");
            string SecreyKey = _config.GetSection("AWSCredentials").GetValue<string>("SecreyKey");

            BasicAWSCredentials credentials = new BasicAWSCredentials(AccessKey, SecreyKey);

            // Create a new Amazon S3 client
            AmazonS3Client s3Client = new AmazonS3Client(credentials, Amazon.RegionEndpoint.USWest2);

            try
            {
                // Upload the file to Amazon S3
                TransferUtility fileTransferUtility = new TransferUtility(s3Client);
                fileTransferUtility.Upload(filePath, bucketName, keyName);
                sr.Success = true;
                sr.Code = (int)HttpStatusCode.OK;
                sr.Message = "Upload completed!";
                sr.Data = null;
            }
            catch (AmazonS3Exception ex)
            {
                sr.Success = false;
                sr.Code = (int)HttpStatusCode.NotImplemented;
                sr.Message = $"Error encountered on server. Message:'{0}' when writing an object\", e.Message";
                sr.Data = null;
            }
            catch (Exception ex)
            {
                sr.Success = false;
                sr.Code = (int)HttpStatusCode.NotImplemented;
                sr.Message = $"Unknown encountered on server. Message:'{{0}}' when writing an object\", e.Message";
                sr.Data = null;
            }
            return sr;
        }
    }
}
