using PlayGroundMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlayGroundMVC.Controllers
{
    public class FileController : Controller
    {
        // GET: File
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
                bool isView = false;
                // all files will be saved once not in chunks
                // Get files from http request
                HttpFileCollectionBase files = Request.Files;

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
                    return View();
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
                bool isView = false;
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
                    return View();
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

        // get file as take direct parm of file
        // HttpPostedFile not working
        [HttpPost, ActionName("FileUploadObj")]
        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            //working
            return null;
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
            return Path.Combine(Server.MapPath("~/Uploads"), fileName);
        }



    }
}