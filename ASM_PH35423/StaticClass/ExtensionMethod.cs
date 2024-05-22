using Microsoft.AspNetCore.Components.Forms;

namespace ASM_PH35423.StaticClass
{
    public static class ExtensionMethod
    {
        public static async Task<IFormFile> ToFormFile(this IBrowserFile browserFile)
        {
            var memoryStream = new MemoryStream();
            await browserFile.OpenReadStream().CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            return new FormFile(memoryStream, 0, browserFile.Size, browserFile.Name, browserFile.Name)
            {
                Headers = new HeaderDictionary(),
                ContentType = browserFile.ContentType
            };
        }
    }
}
