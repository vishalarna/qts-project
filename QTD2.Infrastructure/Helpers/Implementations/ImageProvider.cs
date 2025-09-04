using Microsoft.Extensions.Options;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Helpers.Implementations
{
    public class ImageProvider : IImageProvider
    {
        private readonly QTDSettings.QTDSettings _qtdSettings;

        public ImageProvider(IOptions<QTDSettings.QTDSettings> qtdSettings)
        {
            _qtdSettings = qtdSettings.Value;
        }

        public string GetNERCLogo()
        {
            return EncodeCommonImage("NERCLogo.png");
        }

        protected string EncodeCommonImage(string fileName)
        {
            string filePath = _qtdSettings.SaveFilePath + "img\\_common\\" + fileName;
            return EncodeImage(filePath);
        }

        protected string EncodeImage(string filePath)
        {
            if (File.Exists(filePath))
            {
                byte[] fileBytes = File.ReadAllBytes(filePath);
                string base64String = Convert.ToBase64String(fileBytes);
                return base64String;
            }
            else
            {
                throw new QTDServerException($"Image not found at {filePath}.");
            }
        }
    }
}
