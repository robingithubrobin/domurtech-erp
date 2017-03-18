using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using DomurTech.Helpers.Options;

namespace DomurTech.Helpers
{
    public class ImageHelper
    {
        private static Font _font;
        private static HatchBrush _brush;
        public static Bitmap AddTextToImage(Bitmap bitmap, string text,int opacity, DirectionOptions directionOptions, Font textFont, bool automatic,Color textColor, Color textBackColor,HatchStyle brushStyle)
        {
            var pointTo = new Point();
            var textSize = new Size();
            _brush = new HatchBrush(brushStyle, textColor, textBackColor);
            var imageFrom = new Bitmap(10, 10);
            var downGraphics = Graphics.FromImage(imageFrom);
            textSize.Width = (int)downGraphics.MeasureString(text, textFont).Width;
            textSize.Height = (int)downGraphics.MeasureString(text, textFont).Height;
            _font = new Font(textFont.FontFamily, textFont.Size);
            if ((textSize.Width > bitmap.Width || textSize.Height > bitmap.Height) && automatic)
            {
                for (var i = 0; i < 101; i++)
                {
                    _font = new Font(textFont.FontFamily, textFont.Size * ((float)(100 - i) / 100));
                    textSize.Width = (int)downGraphics.MeasureString(text, _font).Width;
                    textSize.Height = (int)downGraphics.MeasureString(text, _font).Height;
                    if (textSize.Width <= bitmap.Width && textSize.Height <= bitmap.Height)
                    {
                        break;
                    }
                }
            }
            imageFrom = new Bitmap(textSize.Width, textSize.Height);
            downGraphics = Graphics.FromImage(imageFrom);
            downGraphics.DrawString(text, _font, _brush, new Point(0, 0));
            var opacityValue = opacity / 10.0F;
            float[][] matrix = {new float[] {1, 0, 0, 0, 0},new float[] {0, 1, 0, 0, 0},new float[] {0, 0, 1, 0, 0},new[] {0, 0, 0, opacityValue, 1},new float[] {0, 0, 0, 0, 1}};
            var colorMatrix = new ColorMatrix(matrix);
            var imageAttribute = new ImageAttributes();
            imageAttribute.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            if (text == "")
            {
                throw new NullReferenceException("Resmin üstüne konacak yazı girilmemiş.");
            }
            if (bitmap == null)
            {
                throw new NullReferenceException("Üzerine yazı yazılacak resim belirlenmedi.");
            }
            if (directionOptions == 0)
            {
                throw new NullReferenceException("Yazının resmin neresine yerleştirileceği belirlenmedi.");
            }
            var graphicsFromImage = Graphics.FromImage(bitmap);
            graphicsFromImage.InterpolationMode = InterpolationMode.High;
            switch (directionOptions)
            {
                case DirectionOptions.TopRight:
                    pointTo.X = bitmap.Width - imageFrom.Width;
                    pointTo.Y = 0;
                    break;

                case DirectionOptions.BottomLeft:
                    pointTo.X = 0;
                    pointTo.Y = bitmap.Height - imageFrom.Height;
                    break;

                case DirectionOptions.TopLeft:
                    pointTo.X = 0;
                    pointTo.Y = 0;

                    break;

                case DirectionOptions.BottomRight:
                    pointTo.X = bitmap.Width - imageFrom.Width;
                    pointTo.Y = bitmap.Height - imageFrom.Height;

                    break;

                case DirectionOptions.MiddleRight:
                    pointTo.X = bitmap.Width - imageFrom.Width;
                    pointTo.Y = bitmap.Height - (imageFrom.Height - (imageFrom.Height / 2));

                    break;

                case DirectionOptions.MiddleLeft:
                    pointTo.X = 0;
                    pointTo.Y = bitmap.Height - (imageFrom.Height - (imageFrom.Height / 2));

                    break;

                case DirectionOptions.TopCenter:
                    pointTo.X = bitmap.Width - (imageFrom.Width - (imageFrom.Width / 2));
                    pointTo.Y = 0;

                    break;

                case DirectionOptions.BottomCenter:
                    pointTo.X = bitmap.Width - (imageFrom.Width - (imageFrom.Width / 2));
                    pointTo.Y = bitmap.Height - imageFrom.Height;

                    break;

                case DirectionOptions.Center:
                    pointTo.X = (bitmap.Width / 2) - (imageFrom.Width / 2);
                    pointTo.Y = (bitmap.Height / 2) - (imageFrom.Height / 2);

                    break;

                default:
                    pointTo.X = 0;
                    pointTo.Y = 0;

                    break;

            }
            graphicsFromImage.DrawImage(imageFrom, new Rectangle(pointTo, imageFrom.Size),0, 0, imageFrom.Width, imageFrom.Height,GraphicsUnit.Pixel, imageAttribute);
            return bitmap;

        }
    }
}
