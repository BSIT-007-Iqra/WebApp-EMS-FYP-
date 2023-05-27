using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication1.Utils
{
    public class UploadPicToCloud
    {
        public static FirebaseStorage firebasestorage = new FirebaseStorage("emsfiles.appspot.com");
       public static async Task<string> firebaseUpload(HttpPostedFileBase pic)
        {

            var stroageImage = await new FirebaseStorage("emsfiles.appspot.com")
                   .Child("ItemImages")
                   .Child(Guid.NewGuid().ToString() + pic.FileName)
                   .PutAsync(pic.InputStream);
            string imgurl = stroageImage;
            return imgurl;
        } 
        //public static async Task<string> firebasetoUpload(string filePath,string picName)
        //{
        //    var stream = File.Open(filePath, FileMode.Open);
        //    var stroageImage = await new FirebaseStorage("readrixfiles.appspot.com")
        //     .Child("Files")
        //     .Child(Guid.NewGuid().ToString() + picName)
        //     .PutAsync(stream);
        //    var downloadUrl = stroageImage;
        //    return downloadUrl;
        //}
    }
}