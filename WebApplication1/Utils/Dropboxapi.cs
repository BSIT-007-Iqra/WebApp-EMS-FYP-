using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Dropbox.Api;
using Dropbox.Api.Files;
using System.Text;
using Dropbox.Api.Sharing;

namespace WebApplication1.Utils
{
    public class Dropboxapi
    {
        static string tokenstring = "sl.BNKhefvRB-rsuaeip8uDKjxV7wWtodMxriLwsijeD07Iljlw9pkTdMXGvf2n8vjvVF3mwUKP3LTIDP_soMcmI9nrBO-cLzqRH2LtG_2V7jeotPz0LsgY_Ty8HlPeZPux2mF2ZUCbbQ0o";

        public static Task<string> UploadPicToDropbox(string file, string fileName)
        {
            using (var dbx = new DropboxClient(tokenstring))
            {
                //string file = @"C:\users\dell\Desktop\Dummy.txt";
                string folder = "/EMS";
                // string filename = "dummytextfile.txt";
                string url = "";
                string guid = System.Guid.NewGuid().ToString();
                fileName = guid + fileName;
                using (var mem = new MemoryStream(File.ReadAllBytes(file)))
                {
                    var updated = dbx.Files.UploadAsync(folder + "/" + fileName, WriteMode.Overwrite.Instance, body: mem);
                    updated.Wait();

                    var shareFile = dbx.Sharing.CreateSharedLinkWithSettingsAsync(folder + "/" + fileName);
                    shareFile.Wait();
                    url = shareFile.Result.Url.Replace("www", "dl");

                }
                //Console.Write(url);
                return Task.FromResult(url);
            }
        }
    }
}