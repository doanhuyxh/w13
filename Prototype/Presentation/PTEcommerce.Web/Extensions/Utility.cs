using Framework.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PTEcommerce.Web.Extensions
{
    public static class Utility
    {
        #region upload file
        public static UploadFileClass upload(string mediaPath, HttpPostedFileBase file, byte type = 1)
        {

            UploadFileClass objUpload = new UploadFileClass();
            objUpload = Utility.GetUploadFile(mediaPath, file.FileName, true);
            file.SaveAs(objUpload.Fullpath);
            if (type == 1)
            {
                objUpload.pathThumb = Utility.thumbImg(objUpload.StrPathTemp, objUpload.FolderPath, file.FileName);
                Utility.thumbImg(objUpload.StrPathTemp, objUpload.FolderPath, file.FileName, 185, 111, false);
            }
            return objUpload;
        }
        public static UploadFileClass GetUploadFile(string MediaPath, string strFileName, bool AddDatePath)
        {
            UploadFileClass tempUploadClass = new UploadFileClass();
            System.Text.RegularExpressions.Match matchResults;
            string strAdditionFolder = (AddDatePath ? String.Format(DateTime.Now.ToString("\\\\yyyy\\\\MM\\\\dd\\\\")) : "");
            string strSaveFile = strFileName;
            string strSaveFolder = MediaPath + strAdditionFolder;
            //Check folder exist
            try
            {
                if (System.IO.Directory.Exists(strSaveFolder) == false)
                {
                    //Create Directory
                    System.IO.Directory.CreateDirectory(strSaveFolder);
                }
                if (System.IO.File.Exists(strSaveFolder + strSaveFile))
                {
                    while (System.IO.File.Exists(strSaveFolder + strSaveFile))
                    {
                        matchResults = System.Text.RegularExpressions.Regex.Match(strSaveFile, "(?<FileName>.*?)(?:\\((?<AutoNumber>\\d*?)\\))?\\.(?<FileType>\\w*?)(?!.)");
                        if (matchResults.Success)
                        {
                            if (matchResults.Groups["AutoNumber"].Value == string.Empty)
                            {
                                strSaveFile = matchResults.Groups["FileName"].Value + "(1)." + matchResults.Groups["FileType"].Value;
                            }
                            else
                            {
                                strSaveFile = matchResults.Groups["FileName"].Value + "(" + (int.Parse(matchResults.Groups["AutoNumber"].Value) + 1).ToString() + ")." + matchResults.Groups["FileType"].Value;
                            }
                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            tempUploadClass.Virtualpath = strAdditionFolder.Replace("\\", "/") + strSaveFile;
            tempUploadClass.FileName = strSaveFile;
            tempUploadClass.Fullpath = strSaveFolder + strSaveFile;
            tempUploadClass.FolderPath = strAdditionFolder.Replace("\\", "/");
            tempUploadClass.StrPathTemp = strSaveFolder;
            return tempUploadClass;
        }
        public static UploadFileClass GetUploadFile(string MediaPath, string strFileName, bool AddDatePath, string UID)
        {
            UploadFileClass tempUploadClass = new UploadFileClass();
            System.Text.RegularExpressions.Match matchResults;
            string strAdditionFolder = (AddDatePath ? String.Format(DateTime.Now.ToString("\\\\yyyy\\\\MM\\\\dd\\\\")) : UID);
            string strSaveFile = strFileName;
            string strSaveFolder = MediaPath + strAdditionFolder;
            //Check folder exist
            try
            {
                if (System.IO.Directory.Exists(strSaveFolder) == false)
                {
                    //Create Directory
                    System.IO.Directory.CreateDirectory(strSaveFolder);
                }
                if (System.IO.File.Exists(strSaveFolder + strSaveFile))
                {
                    while (System.IO.File.Exists(strSaveFolder + strSaveFile))
                    {
                        matchResults = System.Text.RegularExpressions.Regex.Match(strSaveFile, "(?<FileName>.*?)(?:\\((?<AutoNumber>\\d*?)\\))?\\.(?<FileType>\\w*?)(?!.)");
                        if (matchResults.Success)
                        {
                            if (matchResults.Groups["AutoNumber"].Value == string.Empty)
                            {
                                strSaveFile = matchResults.Groups["FileName"].Value + "(1)." + matchResults.Groups["FileType"].Value;
                            }
                            else
                            {
                                strSaveFile = matchResults.Groups["FileName"].Value + "(" + (int.Parse(matchResults.Groups["AutoNumber"].Value) + 1).ToString() + ")." + matchResults.Groups["FileType"].Value;
                            }
                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            tempUploadClass.Virtualpath = strAdditionFolder.Replace("\\", "/") + strSaveFile;
            tempUploadClass.FileName = strSaveFile;
            tempUploadClass.Fullpath = strSaveFolder + strSaveFile;
            tempUploadClass.FolderPath = strAdditionFolder.Replace("\\", "/");
            tempUploadClass.StrPathTemp = strSaveFolder;
            return tempUploadClass;
        }
        public static bool CheckfileUpload(string fileName)
        {
            bool ret = false;
            string name = "";
            name = Path.GetExtension(fileName.ToLower());
            switch (name.ToLower())
            {
                case ".jpg":
                    ret = true;
                    break;
                case ".png":
                    ret = true;
                    break;
                case ".gif":
                    ret = true;
                    break;
                case ".bmp":
                    ret = true;
                    break;
                case ".doc":
                    ret = true;
                    break;
                case ".docx":
                    ret = true;
                    break;
                case ".xls":
                    ret = true;
                    break;
                case ".xlsx":
                    ret = true;
                    break;
                case ".rar":
                    ret = true;
                    break;
                case ".zip":
                    ret = true;
                    break;
                case ".pdf":
                    ret = true;
                    break;
            }
            return (ret);
        }
        public static string thumbImg(string fullPath, string virtualPath, string fileName, int tbwidth = 0, int tbheight = 0, bool autoSize = true)
        {
            int width = 0;
            int height = 0;
            // Thumb image
            System.Drawing.Image image = System.Drawing.Image.FromFile(fullPath + fileName);
            Size thumbSize = Utility.GetThumbSize(image, 100);
            if (autoSize)
            {
                width = thumbSize.Width;
                height = thumbSize.Height;
            }
            else
            {
                width = tbwidth;
                height = tbheight;
            }
            System.Drawing.Image thumb = image.GetThumbnailImage(width, height, null, IntPtr.Zero);
            var thumbPath = System.IO.Path.Combine(virtualPath, "thumb_" + width + "_" + fileName);
            var savePath = System.IO.Path.Combine(fullPath, "thumb_" + width + "_" + fileName);
            thumb.Save(savePath);
            thumb.Dispose();
            image.Dispose();
            // End thumb
            return thumbPath;
        }
        public static Size GetThumbSize(System.Drawing.Image original, int maxPixels)
        {
            int originalWidth = original.Width;
            int originalHeight = original.Height;
            double factor;
            if (originalWidth > originalHeight)
            {
                factor = (double)maxPixels / originalWidth;
            }
            else
            {
                factor = (double)maxPixels / originalHeight;
            }
            return new Size((int)(originalWidth * factor), (int)(originalHeight * factor));
        }
        #endregion
    }
    public class UploadFileClass
    {
        private string fileName;

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        private string fullpath;

        public string Fullpath
        {
            get { return fullpath; }
            set { fullpath = value; }
        }
        private string virtualpath;

        public string Virtualpath
        {
            get { return virtualpath; }
            set { virtualpath = value; }
        }

        private string folderPath;

        public string FolderPath
        {
            get { return folderPath; }
            set { folderPath = value; }
        }

        private string strPathTemp;

        public string StrPathTemp
        {
            get { return strPathTemp; }
            set { strPathTemp = value; }
        }
        public string pathThumb { get; set; }
    }
}