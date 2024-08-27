using System;
using System.Windows.Forms;

namespace SimpleAddIn
{
    public static class Utilities
    {
        public class ConvertImage : System.Windows.Forms.AxHost
        {
            public ConvertImage()
                : base("d98dde2b-db2c-4a1d-b94f-d7fba6d3d086")
            {
            }

            public static stdole.IPictureDisp ConvertImageToIPictureDisp(System.Drawing.Image Image)
            {
                try
                {
                    return (stdole.IPictureDisp)GetIPictureFromPicture(Image);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Couldn't ConvertImageToIPictureDisp: " + ex.Message);
                    return null;
                }
            }

            public static System.Drawing.Image ConvertIPictureDispToImage(stdole.IPictureDisp IPict)
            {
                try
                {
                    return GetPictureFromIPictureDisp(IPict);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Couldn't set up ConvertIPictureDispToImage: " + ex.Message);
                    return null;
                }
            }
        }
    }
}
