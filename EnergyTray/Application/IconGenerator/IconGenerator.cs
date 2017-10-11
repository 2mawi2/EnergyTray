using System;
using System.Drawing;
using System.Reflection;
using EnergyTray.Application.Extensions;
using EnergyTray.Properties;

namespace EnergyTray.Application.IconGenerator
{
    public class IconGenerator
    {
        public static Icon GetIcon(string text)
        {
            var bitmap = new Bitmap(16, 16);

            var path = Assembly.GetExecutingAssembly().GetManifestResourceStream("facicon.ico");
            
            var icon = new Icon(path);
            var drawFont = new Font("Calibri", 16, FontStyle.Bold);
            var drawBrush = new SolidBrush(Color.White);

            var graphics = Graphics.FromImage(bitmap);

            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;
            graphics.DrawIcon(icon, 0, 0);
            graphics.DrawString(text, drawFont, drawBrush, 1, 2);

            bitmap.Save("icon.ico", System.Drawing.Imaging.ImageFormat.Icon);

            var createdIcon = Icon.FromHandle(bitmap.GetHicon());

            DisposeElements(icon, drawFont, drawBrush, graphics, bitmap);

            return createdIcon;
        }

        private static void DisposeElements(params IDisposable[] disposables)
        {
            disposables.ForEach(i => i.Dispose());
        }
    }
}