using System;
using System.Collections;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Common.Functions
{
    public class Captcha : IDisposable
    {
        ArrayList SymbolList;
        Bitmap BitmapImage;
        string CaptchaText;
        //================================================
        public Captcha(string BackgroundImagePath)
        {
            Image img = System.Drawing.Image.FromFile(BackgroundImagePath);
            BitmapImage = new Bitmap(img, 200, 50);
            SymbolList = new ArrayList();
            MakeSymbol();
        }
        //================================================
        void MakeSymbol()
        {
            //for (int i = 65; i < 91; i++)
            //{
            //    char c = Convert.ToChar(i);
            //    SymbolList.Add(c);
            //    c = Convert.ToChar(i + 32);
            //    SymbolList.Add(c);
            //}
            for (int i = 48; i < 58; i++)
            {
                char c = Convert.ToChar(i);
                SymbolList.Add(c);
            }
        }
        //================================================
        public void Build()
        {
            Graphics graf = Graphics.FromImage(BitmapImage);
            Font font = new System.Drawing.Font("Arial", 25, FontStyle.Bold);
            Random rand1 = new Random();
            string Captcha = "";
            for (int i = 0; i < 5; i++)
            {
                graf.TranslateTransform(Convert.ToInt32(i * 0.14 * 220), 5);
                graf.RotateTransform(-1 * rand1.Next(0, 30));
                string GeneratedSymbol = SymbolList[rand1.Next(0, SymbolList.Count - 1)].ToString();
                graf.DrawString(GeneratedSymbol, font, Brushes.Black, 10, 10);
                Captcha = Captcha + GeneratedSymbol;
                graf.ResetTransform();
            }
            CaptchaText = Captcha;

        }
        //================================================
        public string GetGeneratedText()
        {
            return CaptchaText.Trim().ToLower();
        }
        //================================================
        public byte[] GetGeneratedImage()
        {
            byte[] buffer;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BitmapImage.Save(memoryStream, ImageFormat.Gif);
                memoryStream.Seek(0, SeekOrigin.Begin);
                var binaryReader = new BinaryReader(memoryStream);
                buffer = binaryReader.ReadBytes(Convert.ToInt32(memoryStream.Length));
                binaryReader.Dispose();
            }
            return buffer;
        }
        //=================================================================================================Disposing 
        #region DisposeObject
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }
            BitmapImage?.Dispose();
            SymbolList = null;
        }
        #endregion
        //=================================================================================================
    }
}
