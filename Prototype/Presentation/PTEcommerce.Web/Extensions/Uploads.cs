using Framework.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTEcommerce.Web.Extensions
{
    public class Upload
    {
        /// <summary>
        /// Uploads the specified media path.
        /// </summary>
        /// <param name="mediaPath">The media path.</param>
        /// <param name="file">The file.</param>
        /// <param name="width">The thumb width.</param>
        /// <param name="height">The thumb height.</param>
        /// <param name="type">The type. If type != 1 is not image file, so do not thumb</param>
        /// <returns>UploadFileClass.</returns>
        public static UploadFileClass upload(string mediaPath, HttpPostedFileBase file, int width, int height, byte type = 1)
        {

            UploadFileClass objUpload = new UploadFileClass();
            objUpload = Utility.GetUploadFile(mediaPath, file.FileName, true);
            file.SaveAs(objUpload.Fullpath);
            if (type == 1)
            {
                // objUpload.pathThumb = Utility.thumbImg(objUpload.StrPathTemp, objUpload.FolderPath, file.FileName);
                objUpload.pathThumb = Utility.thumbImg(objUpload.StrPathTemp, objUpload.FolderPath, file.FileName, width, height, false);
            }
            return objUpload;
        }
        /// <summary>
        /// Uploads the specified media path. The file can change name before upload
        /// </summary>
        /// <param name="mediaPath"></param>
        /// <param name="file"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static UploadFileClass upload(string mediaPath, HttpPostedFileBase file, string filename, int width, int height, byte type = 1)
        {

            UploadFileClass objUpload = new UploadFileClass();
            objUpload = Utility.GetUploadFile(mediaPath, filename, true);
            file.SaveAs(objUpload.Fullpath);
            if (type == 1)
            {
                // objUpload.pathThumb = Utility.thumbImg(objUpload.StrPathTemp, objUpload.FolderPath, file.FileName);
                objUpload.pathThumb = Utility.thumbImg(objUpload.StrPathTemp, objUpload.FolderPath, filename, width, height, false);
            }
            return objUpload;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediaPath"></param>
        /// <param name="file"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static UploadFileClass upload(string mediaPath, HttpPostedFileBase file, int[] width, int[] height)
        {

            UploadFileClass objUpload = new UploadFileClass();
            objUpload = Utility.GetUploadFile(HttpContext.Current.Server.MapPath(mediaPath), file.FileName, true);
            file.SaveAs(objUpload.Fullpath);
            objUpload.pathThumb = Utility.thumbImg(objUpload.StrPathTemp, objUpload.FolderPath, file.FileName);
            for (int i = 0; i < width.Length; i++)
            {
                if (height[i] != null && height[i] > 0)
                {
                    Utility.thumbImg(objUpload.StrPathTemp, objUpload.FolderPath, file.FileName, width[i], height[i], false);
                }
                else
                {
                    Utility.thumbImg(objUpload.StrPathTemp, objUpload.FolderPath, file.FileName, width[i], width[i], false);
                }

            }
            //Utility.thumbImg(objUpload.StrPathTemp, objUpload.FolderPath, file.FileName, width, height, false);
            return objUpload;
        }

    }
}