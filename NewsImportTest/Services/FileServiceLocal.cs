using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace NewsImportTest.Services
{
    public class FileServiceLocal
    {
        private static readonly string _sFileFolder = "appdata";
        private readonly string _sWebRootPath;

        public FileServiceLocal(string sWebRootPath)
        {
            this._sWebRootPath = 
                sWebRootPath;
        }

        public  string Create(string sFileName, MemoryStream ms)
        {
            string sUploadFolder = Path.Combine(_sWebRootPath, _sFileFolder);
            string sImageName = Guid.NewGuid().ToString() + Path.GetExtension(sFileName);

            using (var fs = new FileStream(Path.Combine(sUploadFolder, sImageName), FileMode.Create))
            {
                ms.Seek(0, System.IO.SeekOrigin.Begin);
                ms.CopyTo(fs);
                fs.Flush();
            }

            return sImageName;
        }

        public MemoryStream Read(string sFileName)
        {
            string sUploadFolder = Path.Combine(_sWebRootPath, _sFileFolder);
            MemoryStream ms = new MemoryStream();

            try
            {
                using (FileStream file = new FileStream(Path.Combine(sUploadFolder, sFileName), FileMode.Open, FileAccess.Read))
                {
                    file.CopyTo(ms);
                    ms.Seek(0, System.IO.SeekOrigin.Begin);
                }
            }
            catch (Exception)
            {
                ms = null;
            }

            return ms;
        }

        public void Delete(string sFileName)
        {
            string sUploadFolder = Path.Combine(_sWebRootPath, _sFileFolder);
            string sPath = Path.Combine(sUploadFolder, sFileName);
            if (File.Exists(sPath))
            {
                 File.Delete(sPath);
            }
        }
    }
}