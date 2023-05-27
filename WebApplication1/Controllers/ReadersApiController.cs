using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Dropbox.Api;
using Dropbox.Api.Files;
using Firebase.Storage;
using WebApplication1.Models;
using static Dropbox.Api.TeamLog.ClassificationType;

namespace ReadRix.Controllers
{
    public class customersApiController : ApiController
    {
        private Model1 db = new Model1();
        static string tokenstring = "sl.BNKhefvRB-rsuaeip8uDKjxV7wWtodMxriLwsijeD07Iljlw9pkTdMXGvf2n8vjvVF3mwUKP3LTIDP_soMcmI9nrBO-cLzqRH2LtG_2V7jeotPz0LsgY_Ty8HlPeZPux2mF2ZUCbbQ0o";

        // GET: api/customersApi
        public IQueryable<Customer> GetCustomers()
        {
            return db.Customers;
        }

        // GET: api/customersApi/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult GetCustomers(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/customersApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomers(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.Customer_ID)
            {
                return BadRequest();
            }

            db.Entry(customer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!customerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        [HttpPost]
        [Route("api/customers/postdropboximage")]
        public string PostImage()
        {

            try
            {
                var httpRequest = HttpContext.Current.Request;


                if (httpRequest.Files.Count > 0)
                {
                    foreach (string file1 in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file1];
                       
                        var fileName = postedFile.FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault();

                        var filePath = HttpContext.Current.Server.MapPath("~/Content/AdminPicture/" + fileName);
                        postedFile.SaveAs(filePath);
                        
                        using (var dbx = new DropboxClient(tokenstring))
                        {
                            string folder = "/EMS";
                            
                            string url = "";
                            string guid = System.Guid.NewGuid().ToString();
                            fileName = guid + fileName;
                            using (var mem = new MemoryStream(File.ReadAllBytes(filePath)))
                            {
                                var updated = dbx.Files.UploadAsync(folder + "/" + fileName, WriteMode.Overwrite.Instance, body: mem);
                                updated.Wait();

                                var shareFile = dbx.Sharing.CreateSharedLinkWithSettingsAsync(folder + "/" + fileName);
                                shareFile.Wait();
                                url = shareFile.Result.Url.Replace("www", "dl");

                            }
                            
                            return url;
                        }
                }
            }}
            catch (Exception exception)
            {
                return exception.Message + " - Server Error";
            }

            return "No Image Found or Somthing Went Wrong";
        }
        [HttpPost]
        [Route("api/customers/postimage")]
        public async Task<string> PostcloudImage()
        {

            try
            {
                var httpRequest = HttpContext.Current.Request;


                if (httpRequest.Files.Count > 0)
                {
                    foreach (string file1 in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file1];
                       
                        var fileName = postedFile.FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault();

                        var stroageImage = await new FirebaseStorage("emsfiles.appspot.com")
                        .Child("ItemImages")
                               .Child(Guid.NewGuid().ToString() + fileName)
                               .PutAsync(postedFile.InputStream);
                        string imgurl = stroageImage;
                        return imgurl;

                    }
                }
            }
            catch (Exception exception)
            {
                return exception.Message + " - Server Error";
            }

            return "No Image Found or Somthing Went Wrong";
        }

        // POST: api/customersApi
        [ResponseType(typeof(Customer))]
        public IHttpActionResult PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Customers.Add(customer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customer.Customer_ID }, customer);
        }

        // DELETE: api/customersApi/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult DeleteCustomer(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            db.Customers.Remove(customer);
            db.SaveChanges();

            return Ok(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool customerExists(int id)
        {
            return db.Customers.Count(e => e.Customer_ID == id) > 0;
        }
    }
}