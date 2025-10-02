namespace CarRentalSystem.PL.Services
{
    public static class DocumentSettings 
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            //1.Get folder location
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName);
            //2.Get file name (unique)
            var fileName = $"{Guid.NewGuid()}{file.FileName}";

            var filePath = Path.Combine(folderPath, fileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream);

            return fileName;
        }
        //2.Delete  
        public static void DeleteFile(string fileName, string folderName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
