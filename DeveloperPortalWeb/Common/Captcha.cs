using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InContact.DeveloperPortal.Web.Common
{
    public enum CaptchaVariable
    {
        imgWidth = 190,
        imgHeight = 120,
        captchaLength = 5,
        deductFromWidth = 150,
        deductFromHeight = 50,
        fontsize = 45
    }
    public class Captcha : ActionResult
    {
        private readonly string _imgSrc, _font, _responseContentType;
        private readonly int _imgWith, _imgHeight, _captchaLength, _deductFromWidth, _deductFromHeight, _fontsize;
        private readonly StringFormat _format;
        private readonly GraphicsPath _path;
        private HttpResponseBase _response;
        private Rectangle _rectangleObject;
        private Font _fontObject;
        private Bitmap _bmp, _bitmap;
        private Graphics _graphics;
        private HatchBrush _hatchBrush;

        private MemoryStream _memory;
        public Captcha()
        {
            _imgSrc = Convert.ToString(ConfigurationManager.AppSettings["captchaImagesrc"]);
            _font = "Comic Sans MS";
            _responseContentType = "image/jpeg";
            _imgWith = (int)CaptchaVariable.imgWidth;
            _imgHeight = (int)CaptchaVariable.imgHeight;
            _captchaLength = (int)CaptchaVariable.captchaLength;
            _deductFromWidth = (int)CaptchaVariable.deductFromWidth;
            _deductFromHeight = (int)CaptchaVariable.deductFromHeight;
            _fontsize = (int)CaptchaVariable.fontsize;
            _format = new StringFormat();
            _memory = new MemoryStream();
            _path = new GraphicsPath();
        }
        private string GetNewCaptchaString(int length)
        {
            return Guid.NewGuid().ToString().Substring(0, length);  // Returns reandom string of given length
        }
        public override void ExecuteResult(ControllerContext context)
        {

            string imagePath = context.HttpContext.Server.MapPath(_imgSrc);  // Get captcha background image
            string captchaText = GetNewCaptchaString(_captchaLength);  // Get new random Captcha string

            context.HttpContext.Session["captcha"] = captchaText; // Store Captcha string in session to validate later

            _bmp = new Bitmap(imagePath);  // Generate initial image with specified size and set image quality
            SetBitmapAndGraphicsForImage();

            int xCopyright = _imgWith - _deductFromWidth;
            int yCopyright = _imgHeight - _deductFromHeight;

            _fontObject = new Font(_font, _fontsize, FontStyle.Bold);  // Set captcha text font properties
            _rectangleObject = new Rectangle(xCopyright, yCopyright, 0, 0);

            _format.Alignment = StringAlignment.Near;   // Align captcha string inside image
            _format.LineAlignment = StringAlignment.Center;

            _path.AddString(captchaText, _fontObject.FontFamily, (int)_fontObject.Style, _fontObject.Size, _rectangleObject, _format);  // Bind captcha string to image

            SetCaptchaTextTexture();

            _response = context.HttpContext.Response; // Set response to be sent back to client
            _response.Clear();  // clear out any previous response
            _response.ContentType = _responseContentType;  // Set ContentType

            _bitmap.Save(_memory, ImageFormat.Jpeg);  //Saves this image to the specified stream in the specified format.

            DisposeObjects(); // Dispose all resources created

            _memory.WriteTo(HttpContext.Current.Response.OutputStream);  // Writes the entire contents of this memory stream to another stream.

        }

        private void SetBitmapAndGraphicsForImage()
        {
            _bitmap = new Bitmap(_bmp, new Size(_imgWith, _imgHeight));
            _graphics = Graphics.FromImage(_bitmap);
            _graphics.SmoothingMode = SmoothingMode.HighQuality;
            _graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
        }

        private void SetCaptchaTextTexture()
        {
            _hatchBrush = new HatchBrush(HatchStyle.Percent05, Color.FromName("White"), Color.FromName("Black"));
            _graphics.FillPath(_hatchBrush, _path);
        }

        private void DisposeObjects()
        {
            _bmp.Dispose();
            _fontObject.Dispose();
            _hatchBrush.Dispose();
            _graphics.Dispose();
            _path.Dispose();
            _bitmap.Dispose();
            _format.Dispose();
        }

    }
}