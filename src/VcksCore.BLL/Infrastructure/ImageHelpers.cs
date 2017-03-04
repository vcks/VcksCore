using System;
using System.Linq;

using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

namespace VcksCore.BLL.Infrastructure
{
    public static class ImageHelpers
    {
        public static byte[] GetCroppedImage(byte[] bytes, int l = 0)
        {
            ImageCodecInfo jpgInfo = ImageCodecInfo.GetImageEncoders().Where(codecInfo => codecInfo.MimeType == "image/jpeg").First();
            MemoryStream m = new MemoryStream(bytes);
            Image image = Image.FromStream(m);

            if (l == 0)
                l = Math.Min(image.Width, image.Height);

            int targetWidth = l;
            int targetHeight = l;

            Image finalImage = image;
            System.Drawing.Bitmap bitmap = null;
            try
            {
                int left = 0;
                int top = 0;
                int srcWidth = targetWidth;
                int srcHeight = targetHeight;
                bitmap = new System.Drawing.Bitmap(targetWidth, targetHeight);
                double croppedHeightToWidth = (double)targetHeight / targetWidth;
                double croppedWidthToHeight = (double)targetWidth / targetHeight;

                if (image.Width > image.Height)
                {
                    srcWidth = (int)(Math.Round(image.Height * croppedWidthToHeight));
                    if (srcWidth < image.Width)
                    {
                        srcHeight = image.Height;
                        left = (image.Width - srcWidth) / 2;
                    }
                    else
                    {
                        srcHeight = (int)Math.Round(image.Height * ((double)image.Width / srcWidth));
                        srcWidth = image.Width;
                        top = (image.Height - srcHeight) / 2;
                    }
                }
                else
                {
                    srcHeight = (int)(Math.Round(image.Width * croppedHeightToWidth));
                    if (srcHeight < image.Height)
                    {
                        srcWidth = image.Width;
                        top = (image.Height - srcHeight) / 2;
                    }
                    else
                    {
                        srcWidth = (int)Math.Round(image.Width * ((double)image.Height / srcHeight));
                        srcHeight = image.Height;
                        left = (image.Width - srcWidth) / 2;
                    }
                }
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(image, new Rectangle(0, 0, bitmap.Width, bitmap.Height), new Rectangle(left, top, srcWidth, srcHeight), GraphicsUnit.Pixel);
                }
                finalImage = bitmap;
            }
            catch { }
            try
            {
                using (EncoderParameters encParams = new EncoderParameters(1))
                {
                    encParams.Param[0] = new EncoderParameter(Encoder.Quality, (long)100);

                    using (var ms = new MemoryStream())
                    {
                        finalImage.Save(ms, jpgInfo, encParams);
                        return ms.ToArray();
                    }
                }
            }
            catch { }
            if (bitmap != null)
            {
                bitmap.Dispose();
            }
            return null;
        }
    }

        //    //Overload for crop that default starts top left of the image.
        //    public static System.Drawing.Image CropImage(System.Drawing.Image Image, int Height, int Width)
        //    {
        //        return CropImage(Image, Height, Width, 0, 0);
        //    }

        //    //The crop image sub
        //    public static System.Drawing.Image CropImage(System.Drawing.Image Image, int Height, int Width, int StartAtX, int StartAtY)
        //    {
        //        Image outimage;
        //        MemoryStream mm = null;
        //        try
        //        {
        //            //check the image height against our desired image height
        //            if (Image.Height < Height)
        //            {
        //                Height = Image.Height;
        //            }

        //            if (Image.Width < Width)
        //            {
        //                Width = Image.Width;
        //            }

        //            //create a bitmap window for cropping
        //            Bitmap bmPhoto = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
        //            bmPhoto.SetResolution(72, 72);

        //            //create a new graphics object from our image and set properties
        //            Graphics grPhoto = Graphics.FromImage(bmPhoto);
        //            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
        //            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //            grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;

        //            //now do the crop
        //            grPhoto.DrawImage(Image, new Rectangle(0, 0, Width, Height), StartAtX, StartAtY, Width, Height, GraphicsUnit.Pixel);

        //            // Save out to memory and get an image from it to send back out the method.
        //            mm = new MemoryStream();
        //            bmPhoto.Save(mm, System.Drawing.Imaging.ImageFormat.Jpeg);
        //            Image.Dispose();
        //            bmPhoto.Dispose();
        //            grPhoto.Dispose();
        //            outimage = Image.FromStream(mm);

        //            return outimage;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error cropping image, the error was: " + ex.Message);
        //        }
        //    }

        //    //Hard resize attempts to resize as close as it can to the desired size and then crops the excess
        //    public static System.Drawing.Image HardResizeImage(int Width, int Height, System.Drawing.Image Image)
        //    {
        //        int width = Image.Width;
        //        int height = Image.Height;
        //        Image resized = null;
        //        if (Width > Height)
        //        {
        //            resized = ResizeImage(Width, Width, Image);
        //        }
        //        else
        //        {
        //            resized = ResizeImage(Height, Height, Image);
        //        }
        //        Image output = CropImage(resized, Height, Width);
        //        //return the original resized image
        //        return output;
        //    }

        //    //Image resizing
        //    public static System.Drawing.Image ResizeImage(int maxWidth, int maxHeight, System.Drawing.Image Image)
        //    {
        //        int width = Image.Width;
        //        int height = Image.Height;
        //        if (width > maxWidth || height > maxHeight)
        //        {
        //            //The flips are in here to prevent any embedded image thumbnails -- usually from cameras
        //            //from displaying as the thumbnail image later, in other words, we want a clean
        //            //resize, not a grainy one.
        //            Image.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipX);
        //            Image.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipX);

        //            float ratio = 0;
        //            if (width > height)
        //            {
        //                ratio = (float)width / (float)height;
        //                width = maxWidth;
        //                height = Convert.ToInt32(Math.Round((float)width / ratio));
        //            }
        //            else
        //            {
        //                ratio = (float)height / (float)width;
        //                height = maxHeight;
        //                width = Convert.ToInt32(Math.Round((float)height / ratio));
        //            }

        //            //return the resized image
        //            return Image.GetThumbnailImage(width, height, null, IntPtr.Zero);
        //        }
        //        //return the original resized image
        //        return Image;
        //    }
        //}
    }