using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayGroundMVC.Models
{
    //creae file model to uplaod file using model not from httpRequest object
    // Bug=> this method not working so comment files for 1st case test


    public class FileModel
    {
        // not working
        //for single file
        // public HttpPostedFile File { get; set; }
        // for list of file
        // public HttpFileCollection Files { get; set; }


        //working
       // public FileModel()
       // {
       //     Files = new HttpFileCollectionBase();
       // }
        //files with not work you have to intance fo HttpFileCollectionBase lookup to constructor
        public List<HttpPostedFileBase> Files { get; set; }
        public HttpPostedFileBase File { get; set; }
        public string FileName { get; set; }

    }
}