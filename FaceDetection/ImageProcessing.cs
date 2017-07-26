using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FaceDetection {
	class ImageProcessing {
		private FormMain owner;
		private ImageCodecInfo imageCodecInfo;
		private EncoderParameters encoderParameters;

		public ImageProcessing(FormMain owner) {
			this.owner = owner;


			imageCodecInfo = GetEncoder(ImageFormat.Jpeg);

			Encoder encoder = Encoder.Quality;
			encoderParameters = new EncoderParameters(1);
			EncoderParameter encoderParameter = new EncoderParameter(encoder, 90L);
			encoderParameters.Param[0] = encoderParameter;
		}

		public bool CropImageAndSave(string fileName, List<SaveFormat> saveFormats, string saveFolderPath) {
			Console.WriteLine("CropImageAndSave: " + fileName);
			Rectangle[] faces = DetectFace(fileName);

			string result = "";
			string imageSize = "Не вычислялось";
			if (faces.Length == 0) {
				result = "Лицо не обнаружено";
			} else if (faces.Length > 1) {
				result = "Обнаружено несколько лиц";
			}

			if (!string.IsNullOrEmpty(result)) {
				owner.UpdateResultTextInListView(fileName, imageSize, result);
				return false;
			}

			bool saveSuccess = false;

			try {
				using (Bitmap original = LoadImageFromFileAndRotate(fileName)) {
					imageSize = original.Width + " x " + original.Height;
					Rectangle croppedField = new Rectangle(
							faces[0].X - (int)(faces[0].Width / 2),
							faces[0].Y - (int)(faces[0].Height / 2),
							faces[0].Width * 2,
							faces[0].Height * 2);

					foreach (SaveFormat saveFormat in saveFormats) {
						if (croppedField.Width < saveFormat.width * 0.7 ||
							croppedField.Height < saveFormat.height * 0.7) {
							result += saveFormat.nameAppendix + " - недостаточный размер исходного изображения; ";
							continue;
						}

						using (Bitmap croppedBitmap = CropImage(original, croppedField)) {
							Bitmap resizedBitmap = ResizeImage(croppedBitmap, saveFormat.width, saveFormat.height);
							if (saveFormat.needToCircle)
								resizedBitmap = RoundCorners(resizedBitmap, resizedBitmap.Width, Color.White);

							string newFileName = saveFolderPath + "\\" + Path.GetFileNameWithoutExtension(fileName) +
								" - " + saveFormat.nameAppendix + ".jpg";
							resizedBitmap.Save(newFileName, imageCodecInfo, encoderParameters);
							resizedBitmap.Dispose();
							resizedBitmap = null;
						}
					}
				}
			} catch (Exception e) {
				result += e.Message + "; ";
			}

			if (string.IsNullOrEmpty(result)) {
				result = "Успешно";
				saveSuccess = true;
			}

			owner.UpdateResultTextInListView(fileName, imageSize, result);
			return saveSuccess;
		}

		public Rectangle[] DetectFace(string fileName) {
			try {
				using (CascadeClassifier faceClassifier = new CascadeClassifier("haarcascade_frontalface_default.xml"))
				using (Bitmap bitmap = LoadImageFromFileAndRotate(fileName))
				using (Image<Bgr, Byte> imageCV = new Image<Bgr, byte>(bitmap))
				using (Mat mat = imageCV.Mat) {
					int width = bitmap.Width;
					int height = bitmap.Height;
					int min = width < height ? width : height;
					return faceClassifier.DetectMultiScale(mat, 1.3, 5, new Size((int)(min * 0.2), (int)(min * 0.2)));
				}
			} catch (Exception) {
				return new Rectangle[] { };
			}
		}

		public Bitmap ResizeImage(Image image, int width, int height) {
			Rectangle destRect = new Rectangle(0, 0, width, height);
			Bitmap destImage = new Bitmap(width, height);

			destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

			using (var graphics = Graphics.FromImage(destImage)) {
				graphics.CompositingMode = CompositingMode.SourceCopy;
				graphics.CompositingQuality = CompositingQuality.HighQuality;
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.SmoothingMode = SmoothingMode.HighQuality;
				graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

				using (var wrapMode = new ImageAttributes()) {
					wrapMode.SetWrapMode(WrapMode.TileFlipXY);
					graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
				}
				graphics.Dispose();
			}

			return destImage;
		}

		public Bitmap CropImage(Bitmap source, Rectangle section) {
			Bitmap bitmap = new Bitmap(section.Width, section.Height);

			using (Graphics graphics = Graphics.FromImage(bitmap)) {
				graphics.FillRectangle(Brushes.White, 0, 0, section.Width, section.Height);
				graphics.DrawImage(source, new Rectangle(0, 0, section.Width, section.Height), section, GraphicsUnit.Pixel);
				graphics.Dispose();
			}

			return bitmap;
		}

		public Bitmap CropToCircle(Bitmap srcImage, Color backgroundColor) {
			Bitmap bitmap = new Bitmap(srcImage.Width, srcImage.Height, srcImage.PixelFormat);

			using (Graphics graphics = Graphics.FromImage(bitmap))
			using (Brush brush = new SolidBrush(backgroundColor))
			using (GraphicsPath path = new GraphicsPath()) {
				graphics.SmoothingMode = SmoothingMode.AntiAlias;
				graphics.FillRectangle(brush, 0, 0, bitmap.Width, bitmap.Height);
				path.AddEllipse(0, 0, bitmap.Width, bitmap.Height);
				graphics.SetClip(path);
				graphics.DrawImage(srcImage, 0, 0);
			}

			return bitmap;
		}

		public Bitmap RoundCorners(Bitmap StartImage, int CornerRadius, Color BackgroundColor) {
			Bitmap RoundedImage = new Bitmap(StartImage.Width, StartImage.Height);
			using (Graphics graphics = Graphics.FromImage(RoundedImage))
			using (GraphicsPath graphicsPath = new GraphicsPath())
			using (Brush brush = new TextureBrush(StartImage)) {
				graphics.Clear(BackgroundColor);
				graphics.SmoothingMode = SmoothingMode.AntiAlias;
				graphicsPath.AddArc(0, 0, CornerRadius, CornerRadius, 180, 90);
				graphicsPath.AddArc(0 + RoundedImage.Width - CornerRadius, 0, CornerRadius, CornerRadius, 270, 90);
				graphicsPath.AddArc(0 + RoundedImage.Width - CornerRadius, 0 + RoundedImage.Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
				graphicsPath.AddArc(0, 0 + RoundedImage.Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);
				graphics.FillPath(brush, graphicsPath);
			}

			return RoundedImage;
		}

		public Bitmap LoadImageFromFileAndRotate(string fileName) {
			try {
				Bitmap bitmap = new Bitmap(fileName);

				PropertyItem pi = bitmap.PropertyItems.Select(x => x)
												   .FirstOrDefault(x => x.Id == 0x0112);
				if (pi == null)
					return bitmap;

				byte o = pi.Value[0];

				if (o == 2) bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
				if (o == 3) bitmap.RotateFlip(RotateFlipType.RotateNoneFlipXY);
				if (o == 4) bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
				if (o == 5) bitmap.RotateFlip(RotateFlipType.Rotate90FlipX);
				if (o == 6) bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
				if (o == 7) bitmap.RotateFlip(RotateFlipType.Rotate90FlipY);
				if (o == 8) bitmap.RotateFlip(RotateFlipType.Rotate90FlipXY);

				return bitmap;
			} catch (Exception) {
				return null;
			}
		}

		private ImageCodecInfo GetEncoder (ImageFormat format) {
			ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
			foreach (ImageCodecInfo codec in codecs)
				if (codec.FormatID == format.Guid)
					return codec;
			return null;
		}

		public static byte[] ImageToByte(Image img) {
			ImageConverter converter = new ImageConverter();
			return (byte[])converter.ConvertTo(img, typeof(byte[]));
		}
	}
}
