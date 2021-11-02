﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace FCM.DAO
{
    public class ImageProcessing
    {

        private static ImageProcessing instance;
        public static ImageProcessing Instance
        {
            get { if (instance == null) instance = new ImageProcessing(); return instance; }
            set => instance = value;
        }
        public byte[] convertImgToByte(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
        public Image ByteToImg(Byte[] byteString)
        {
            MemoryStream ms = new MemoryStream(byteString, 0, byteString.Length);
            ms.Write(byteString, 0, byteString.Length);
            Image image = Image.FromStream(ms);

            return image;
        }
        public BitmapImage Convert(Image img)
        {
            using (var memory = new MemoryStream())
            {
                img.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }
    }
}
