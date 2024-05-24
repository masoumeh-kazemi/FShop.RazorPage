#nullable enable
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace FShop.RazorPage.Infrastructure.Utils.CustomValidation.IFormFile
{
    public class MaxFileSizeAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly int file_size;

        /// <summary>
        ///  
        /// </summary>
        /// <param name="fileSize">Megabyte</param>
        public MaxFileSizeAttribute(int fileSize)
        {
            file_size = fileSize * 1024;
        }
        public override bool IsValid(object? value)
        {
            var fileInput = value as Microsoft.AspNetCore.Http.IFormFile;
            if (fileInput == null) return true;


            var size = fileInput.Length / 1024;
            return size <= file_size;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (!context.Attributes.ContainsKey("data-val"))
                context.Attributes.Add("data-val", "true");
            context.Attributes.Add("fileSize", file_size.ToString());
#pragma warning disable CS8604 // Possible null reference argument for parameter 'value' in 'void IDictionary<string, string>.Add(string key, string value)'.
            context.Attributes.Add("data-val-fileSize", ErrorMessage);
#pragma warning restore CS8604 // Possible null reference argument for parameter 'value' in 'void IDictionary<string, string>.Add(string key, string value)'.
        }
    }
}