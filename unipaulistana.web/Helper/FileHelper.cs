namespace unipaulistana.web.Helper
{
    using System;
    using System.IO;

    public class FileHelper
    {
        public static string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Guid.NewGuid().ToString()
                  + Path.GetExtension(fileName);
        }
    }
}
