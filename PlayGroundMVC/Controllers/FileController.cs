using PlayGroundMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace PlayGroundMVC.Controllers
{
    public class FileController : Controller
    {
        // GET: File
        // uploading a file using html razor syntax , here is two approach
        // 1 submit on the same overload method index and display the message-- not need
        //2 submit to the other method like fileuplod and send tempdata to index to display the message -need
        // i set up the fileuplad view to index see will it redirect to index method or just show the view page of index

        public ActionResult Index()
        {
            return View();
        }
        // give a custome name for action
        // can this remove ambiguity from same method name => yes fix the issue
        [HttpPost, ActionName("FileUpload")]
        public ActionResult FileUpload()
        {
            try
            {
                bool isView = true;
                // all files will be saved once not in chunks
                // Get files from http request
                HttpFileCollectionBase files = Request.Files;
                var getNameIfExist = Request.Form["FileName"];
                if (files.Count > 0)
                {
                    // upload file by individually
                    // loop through all files
                    // comment of uncomment this to test => for each not working cant cast to file
                    //foreach (var file in files)
                    //{
                    //    HttpPostedFileBase fileBase = (HttpPostedFileBase)file;
                    //    UploadFile(fileBase);
                    //}
                    //or
                    // no need to cast
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase fileBase = files[i];
                        UploadFile(fileBase);
                    }
                }

                if (isView)
                {
                    ViewBag.Message = "file uploaded.";
                    return View("Index");
                }
                else
                {
                    return Json(new { msg = "file uploaded." });
                }

            }
            catch (Exception ex)
            {

                throw;
            }


            return null;
        }

        // Q=> this method not working 
        // change the model to single file accept
        // but how to get list of file here
        // not working for signle file
        //Case 2:  take a parm with it like name
        // getting filename but not file

        [HttpPost, ActionName("FileUploadModel")]
        public ActionResult FileUpload(FileModel fileModel)
        {
            try
            {
                bool isView = true;
                // you can send list of file or single file this is demo of using both of them
                if (fileModel.File != null)
                {

                    var fileName = GetPath(fileModel.File.FileName);
                    fileModel.File.SaveAs(fileName);

                }

                //code removed test for signle file

                if (fileModel.Files != null && fileModel.Files.Count > 0)
                {

                    for (int i = 0; i < fileModel.Files.Count; i++)
                    {
                        var fileName = GetPath(fileModel.Files[i].FileName);
                        fileModel.Files[i].SaveAs(fileName);
                    }
                }
                if (isView)
                {
                    ViewBag.Message = "file uploaded.";
                    return View("Index");
                }
                else
                {
                    return Json(new { msg = "files uploaded." });
                }


            }
            catch (Exception ex)
            {

                throw;
            }

            return null;
        }


        [HttpGet, ActionName("FileUploadChunks")]
        public ActionResult FileUploadChunks()
        {
            //working
            return View();
        }

        // get file as take direct parm of file
        // HttpPostedFile not working
        [HttpPost, ActionName("FileUploadChunks")]
        public ActionResult FileUpload(HttpPostedFileBase file)
        {

            var filePath = GetPath(file.FileName);
            var bytes = GetByte(file.InputStream);
            using (var fs = new FileStream(filePath, FileMode.Append)) {

                fs.Write(bytes,0, bytes.Length);
            }


            //working
            return Json(new { msg = "file chunck uploaded"});
        }
        byte[] GetByte(Stream stream)
        {

            byte[] buffer = new byte[stream.Length];
            using (var ms = new MemoryStream()) {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0) {
                    ms.Write(buffer,0,read);
                }
                return ms.ToArray();

            }

        }





        void UploadFile(HttpPostedFileBase file)
        {
            string fileName = GetPath(file.FileName);
            file.SaveAs(fileName);
        }

        string GetPath(string fileName)
        {
            // path is used to combine a path and name of file
            // server is used to map a path form local directory of folder
            return Path.Combine(Server.MapPath(ReadWebConfigKey("UploadFolder")), fileName);
        }
        /// <summary>
        /// Read Value form web.config file
        /// </summary>
        /// <param name="KeyName"></param>
        /// <returns></returns>
        string ReadWebConfigKey(string KeyName) {
            // two class object to read a key value form webconfig "UploadFolder"
            var val = ConfigurationManager.AppSettings[KeyName];
            var val2 = WebConfigurationManager.AppSettings[KeyName];
            return val2;
        }


    }
}