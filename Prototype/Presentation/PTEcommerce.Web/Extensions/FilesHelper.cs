using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace PTEcommerce.Web.Extensions
{
    public class FilesHelper
    {
        String DeleteURL = null;
        String DeleteType = null;
        String StorageRoot = null;
        String UrlBase = null;
        String tempPath = null;
        //ex:"~/Files/something/";
        String serverMapPath = null;
        public FilesHelper(String DeleteURL, String DeleteType, String StorageRoot, String UrlBase, String tempPath, String serverMapPath)
        {
            this.DeleteURL = DeleteURL;
            this.DeleteType = DeleteType;
            this.StorageRoot = StorageRoot;
            this.UrlBase = UrlBase;
            this.tempPath = tempPath;
            this.serverMapPath = serverMapPath;
        }

        public void DeleteFiles(String pathToDelete)
        {

            string path = HostingEnvironment.MapPath(pathToDelete);

            System.Diagnostics.Debug.WriteLine(path);
            if (Directory.Exists(path))
            {
                DirectoryInfo di = new DirectoryInfo(path);
                foreach (FileInfo fi in di.GetFiles())
                {
                    System.IO.File.Delete(fi.FullName);
                    System.Diagnostics.Debug.WriteLine(fi.Name);
                }

                di.Delete(true);
            }
        }

        public String DeleteFile(String file)
        {
            System.Diagnostics.Debug.WriteLine("DeleteFile");
            //    var req = HttpContext.Current;
            System.Diagnostics.Debug.WriteLine(file);

            String fullPath = Path.Combine(StorageRoot, file);
            System.Diagnostics.Debug.WriteLine(fullPath);
            System.Diagnostics.Debug.WriteLine(System.IO.File.Exists(fullPath));
            String thumbPath = "/" + file + ".80x80.jpg";
            String partThumb1 = Path.Combine(StorageRoot, "thumbs");
            String partThumb2 = Path.Combine(partThumb1, file + ".80x80.jpg");

            System.Diagnostics.Debug.WriteLine(partThumb2);
            System.Diagnostics.Debug.WriteLine(System.IO.File.Exists(partThumb2));
            if (System.IO.File.Exists(fullPath))
            {
                //delete thumb 
                if (System.IO.File.Exists(partThumb2))
                {
                    System.IO.File.Delete(partThumb2);
                }
                System.IO.File.Delete(fullPath);
                String succesMessage = "Ok";
                return succesMessage;
            }
            String failMessage = "Error Delete";
            return failMessage;
        }
        public JsonFiles GetFileList()
        {

            var r = new List<ViewDataUploadFilesResult>();

            String fullPath = Path.Combine(StorageRoot);
            if (Directory.Exists(fullPath))
            {
                DirectoryInfo dir = new DirectoryInfo(fullPath);
                foreach (FileInfo file in dir.GetFiles())
                {
                    int SizeInt = unchecked((int)file.Length);
                    r.Add(UploadResult(file.Name, SizeInt, file.FullName));
                }

            }
            JsonFiles files = new JsonFiles(r);

            return files;
        }

        public void UploadAndShowResults(HttpContextBase ContentBase, List<ViewDataUploadFilesResult> resultList, int thumbWidth = 80, int thumbHeight = 80)
        {
            var httpRequest = ContentBase.Request;


            System.Diagnostics.Debug.WriteLine(Directory.Exists(tempPath));

            //String fullPath = Path.Combine(StorageRoot);
            //Directory.CreateDirectory(fullPath);
            //// Create new folder for thumbs
            //Directory.CreateDirectory(fullPath + "/thumbs/");

            foreach (String inputTagName in httpRequest.Files)
            {

                var headers = httpRequest.Headers;

                var file = httpRequest.Files[inputTagName];
                System.Diagnostics.Debug.WriteLine(file.FileName);

                if (string.IsNullOrEmpty(headers["X-File-Name"]))
                {

                    UploadWholeFile(ContentBase, resultList, thumbWidth, thumbHeight);
                }
                else
                {

                    UploadPartialFile(headers["X-File-Name"], ContentBase, resultList);
                }
            }
        }


        private void UploadWholeFile(HttpContextBase requestContext, List<ViewDataUploadFilesResult> statuses, int thumbWidth, int thumbHeight)
        {

            var request = requestContext.Request;
            //var catePath = request.Form["slCategory"];

            for (int i = 0; i < request.Files.Count; i++)
            {
                var file = request.Files[i];
                String pathOnServer = Path.Combine(StorageRoot);
                //String pathOnServer = Path.Combine("C:\\Users\\MinhNN\\Desktop\\Momart\\Prototype\\Presentation\\PTEcommerce.Admin\\Uploads\\Images");
                string strAdditionFolder = String.Format(DateTime.Now.ToString("\\\\yyyy\\\\MM\\\\dd\\\\"));
                string strSaveFolder = pathOnServer + strAdditionFolder;
                String fileThumb = string.Empty;
                if (System.IO.Directory.Exists(strSaveFolder) == false)
                {
                    //Create Directory
                    System.IO.Directory.CreateDirectory(strSaveFolder);
                }
                var fullPath = Path.Combine(strSaveFolder, Path.GetFileName(file.FileName));

                file.SaveAs(fullPath);

                //Create thumb
                string[] imageArray = file.FileName.Split('.');
                if (imageArray.Length != 0)
                {
                    String extansion = imageArray[imageArray.Length - 1];
                    if (extansion.ToLower() != "jpg" && extansion.ToLower() != "jpeg" && extansion.ToLower() != "png" && extansion.ToLower() != "gif") //Do not create thumb if file is not an image
                    {

                    }
                    else
                    {
                        var ThumbfullPath = Path.Combine(pathOnServer, "thumbs");
                        string folderThumb = ThumbfullPath + strAdditionFolder;
                        if (System.IO.Directory.Exists(folderThumb) == false)
                        {
                            //Create Directory
                            System.IO.Directory.CreateDirectory(folderThumb);
                        }

                        Image imgPhoto = Image.FromStream(file.InputStream);

                        int sourceWidth = imgPhoto.Width;
                        int sourceHeight = imgPhoto.Height;
                        if (sourceWidth < 468)
                            thumbWidth = sourceWidth;

                        if (sourceHeight < 350)
                            thumbHeight = sourceHeight;

                        fileThumb = "thumb_" + thumbWidth + "_" + file.FileName;
                        var ThumbfullPath2 = Path.Combine(folderThumb, fileThumb);
                        using (MemoryStream stream = new MemoryStream(System.IO.File.ReadAllBytes(fullPath)))
                        {
                            var thumbnail = ResizeImg(imgPhoto, thumbWidth, thumbHeight);
                            thumbnail.Save(ThumbfullPath2);

                            //var thumbnail = new WebImage(stream).Resize(thumbWidth, thumbHeight);
                            //thumbnail.Save(ThumbfullPath2, "jpg");
                        }

                    }
                }
                statuses.Add(UploadResult(file, strAdditionFolder, "thumb_" + thumbWidth + "_"));
            }
        }
        private void UploadPartialFile(string fileName, HttpContextBase requestContext, List<ViewDataUploadFilesResult> statuses)
        {
            var request = requestContext.Request;
            if (request.Files.Count != 1) throw new HttpRequestValidationException("Attempt to upload chunked file containing more than one fragment per request");
            var file = request.Files[0];
            var inputStream = file.InputStream;
            String patchOnServer = Path.Combine(StorageRoot);
            var fullName = Path.Combine(patchOnServer, Path.GetFileName(file.FileName));
            var ThumbfullPath = Path.Combine(fullName, Path.GetFileName("thumb_" + file.FileName));
            ImageHandler handler = new ImageHandler();

            var ImageBit = ImageHandler.LoadImage(fullName);
            handler.Save(ImageBit, 80, 80, 10, ThumbfullPath);
            using (var fs = new FileStream(fullName, FileMode.Append, FileAccess.Write))
            {
                var buffer = new byte[1024];

                var l = inputStream.Read(buffer, 0, 1024);
                while (l > 0)
                {
                    fs.Write(buffer, 0, l);
                    l = inputStream.Read(buffer, 0, 1024);
                }
                fs.Flush();
                fs.Close();
            }
            statuses.Add(UploadResult(file, "", "thumb_"));
        }
        private Bitmap ResizeImg(System.Drawing.Image image, int width = 468, int height = 350)
        {
            if (image.Width < 468)
                width = image.Width;

            if (image.Height < 350)
                height = image.Height;

            Bitmap newImage = new Bitmap(width, height);
            //set the resolutions the same to avoid cropping due to resolution differences
            newImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            using (Graphics gr = Graphics.FromImage(newImage))
            {
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.DrawImage(image, new Rectangle(0, 0, width, height));
            }
            return newImage;
        }
        public ViewDataUploadFilesResult UploadResult(HttpPostedFileBase file, String PathLevel2 = "", string thumbName = "")
        {
            String getType = System.Web.MimeMapping.GetMimeMapping(file.FileName);
            var result = new ViewDataUploadFilesResult()
            {
                name = file.FileName,
                size = file.ContentLength,
                type = getType,
                url = UrlBase + PathLevel2 + file.FileName,
                deleteUrl = DeleteURL + file.FileName,
                thumbnailUrl = CheckThumb(getType, file.FileName, PathLevel2, thumbName),
                deleteType = DeleteType,
            };
            return result;
        }
        public ViewDataUploadFilesResult UploadResult(string fileName, int Size, string fullpath)
        {
            String getType = System.Web.MimeMapping.GetMimeMapping(fullpath);
            var result = new ViewDataUploadFilesResult()
            {
                name = fileName,
                size = Size,
                type = getType,
                url = UrlBase + fileName,
                deleteUrl = DeleteURL + fileName,
                thumbnailUrl = CheckThumb(getType, fileName),
                deleteType = DeleteType,
            };
            return result;
        }

        public String CheckThumb(String type, String FileName, String PathLevel2 = "", string thumbName = "")
        {
            var splited = type.Split('/');
            if (splited.Length == 2)
            {
                string extansion = splited[1];
                if (extansion.Equals("jpeg") || extansion.Equals("jpg") || extansion.Equals("png") || extansion.Equals("gif"))
                {
                    String thumbnailUrl = UrlBase + "/thumbs/" + PathLevel2 + thumbName + FileName;
                    return thumbnailUrl;
                }
                else
                {
                    if (extansion.Equals("octet-stream")) //Fix for exe files
                    {
                        return "/Content/Free-file-icons/48px/exe.png";

                    }
                    if (extansion.Contains("zip")) //Fix for exe files
                    {
                        return "/Content/Free-file-icons/48px/zip.png";
                    }
                    String thumbnailUrl = "/Content/Free-file-icons/48px/" + extansion + ".png";
                    return thumbnailUrl;
                }
            }
            else
            {
                return UrlBase + "/thumbs/" + FileName + ".80x80.jpg";
            }

        }
        public List<String> FilesList()
        {

            List<String> Filess = new List<String>();
            string path = HostingEnvironment.MapPath(serverMapPath);
            System.Diagnostics.Debug.WriteLine(path);
            if (Directory.Exists(path))
            {
                DirectoryInfo di = new DirectoryInfo(path);
                foreach (FileInfo fi in di.GetFiles())
                {
                    Filess.Add(fi.Name);
                    System.Diagnostics.Debug.WriteLine(fi.Name);
                }

            }
            return Filess;
        }
    }
    public class ViewDataUploadFilesResult
    {
        public string name { get; set; }
        public int size { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public string deleteUrl { get; set; }
        public string thumbnailUrl { get; set; }
        public string deleteType { get; set; }
    }
    public class JsonFiles
    {
        public ViewDataUploadFilesResult[] files;
        public string TempFolder { get; set; }
        public JsonFiles(List<ViewDataUploadFilesResult> filesList)
        {
            files = new ViewDataUploadFilesResult[filesList.Count];
            for (int i = 0; i < filesList.Count; i++)
            {
                files[i] = filesList.ElementAt(i);
            }

        }

    }
}