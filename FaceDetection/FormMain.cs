using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using Emgu.CV;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Threading;
using Emgu.CV.Structure;
using System.Diagnostics;
using System.Text;
using Microsoft.VisualBasic.FileIO;

namespace FaceDetection {
	public partial class FormMain : Form {
		private ImageProcessing imageProcessing;
		private ListViewColumnSorter listViewColumnSorter;
		private List<string> imageAcceptableFormats = new List<string>() {
			"*.bmp", "*.dib", "*.jpeg", "*.jpg", "*.jpe", "*.png", "*.pbm",
			"*.pgm", "*.ppm", "*.sr", "*.ras", "*.tiff", "*.tif", "*.exr", "*.jp2"
		};

		public FormMain() {
			InitializeComponent();
			imageProcessing = new ImageProcessing(this);
			listViewColumnSorter = new ListViewColumnSorter();
			listViewImages.ListViewItemSorter = listViewColumnSorter;


			//pictureBoxPreview.Image = ActiveDirectory.GetUserPicture("nn-admin");
			//List<string[]> values = GetCsvFileContent();
			//string rootPath = @"\\mssu-fs-01\MSSU FILES\IT\FaceDetection\АвтоматическиОбработанныеИзображения\Инфоклиника, Active Directory 500 х 500, квадрат\";
			//for(int i = 0; i < values.Count; i++) {
			//	string fileName = values[i][0];
			//	string userName = values[i][1].Split('@')[0];
			//	Console.WriteLine(userName + " - " + fileName);

			//	ActiveDirectory.InsertPicture(userName, rootPath + fileName);
			//}


			//Console.WriteLine(values.Count);
		}


		//public static List<string[]> GetCsvFileContent() {
		//	List<string[]> returnValue = new List<string[]>();

		//	try {
		//		using (TextFieldParser parser = new TextFieldParser(@"C:\Users\nn-admin\Desktop\Соответствие фотографий учеткам AD.txt", 
		//			Encoding.GetEncoding("windows-1251"))) {
		//			parser.TextFieldType = FieldType.Delimited;
		//			parser.SetDelimiters("\t");
		//			while (!parser.EndOfData) {
		//				string[] fields = parser.ReadFields();
		//				returnValue.Add(fields);
		//			}
		//		}
		//	} catch (Exception) {
		//	}

		//	return returnValue;
		//}






		private void ButtonAdd_Click(object sender, EventArgs e) {
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Изображение|" + string.Join(";", imageAcceptableFormats);
			openFileDialog.CheckFileExists = true;
			openFileDialog.CheckPathExists = true;
			openFileDialog.Multiselect = true;
			openFileDialog.RestoreDirectory = true;

			if (openFileDialog.ShowDialog() != DialogResult.OK)
				return;

			Thread thread = new Thread(() => CheckFilesFormatAndAddToList(openFileDialog.FileNames.ToList()));
			thread.Start();
		}

		private void ButtonDelete_Click(object sender, EventArgs e) {
			foreach (ListViewItem item in listViewImages.SelectedItems)
				listViewImages.Items.Remove(item);

			UpdateControlsState();
		}

		private void ButtonSavePath_Click(object sender, EventArgs e) {
			Console.WriteLine("buttonSavePath_Click");
			FolderBrowserDialog dialog = new FolderBrowserDialog();
			dialog.Description = "Выберите папку для сохранения обработанных изображений";
			dialog.ShowNewFolderButton = true;

			if (dialog.ShowDialog() != DialogResult.OK)
				return;

			if (string.IsNullOrWhiteSpace(dialog.SelectedPath))
				return;

			textBoxSavePath.Text = dialog.SelectedPath;
			buttonSaveAll.Select();
		}

		private void ButtonSaveSelected_Click(object sender, EventArgs e) {
			Console.WriteLine("buttonSaveSelected_Click");

			if (listViewImages.SelectedItems.Count != 1) {
				MessageBox.Show("Количество выбранных изображений не равно одному", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			string fileName = listViewImages.SelectedItems[0].Name;
			Thread thread = new Thread(() => ProcessAllFiles(new List<string> { fileName }));
			thread.Start();
		}

		private void ButtonSaveAll_Click(object sender, EventArgs e) {
			Console.WriteLine("buttonSaveAll_Click");

			List<string> fileNames = new List<string>();
			foreach (ListViewItem item in listViewImages.Items)
				fileNames.Add(item.Name);

			Thread thread = new Thread(() => ProcessAllFiles(fileNames));
			thread.Start();
		}


		private void ListViewImages_SelectedIndexChanged(object sender, EventArgs e) {
			Console.WriteLine("listViewImages_SelectedIndexChanged");
			int selectedCount = listViewImages.SelectedItems.Count;
			buttonDelete.Enabled = selectedCount > 0;
			UpdateSaveButtonsState();

			string status = "";

			try {
				if (selectedCount == 1) {
					string fileName = listViewImages.SelectedItems[0].Name;
					Bitmap bitmap = imageProcessing.LoadImageFromFileAndRotate(fileName);
					pictureBoxPreview.Image = bitmap;
					Rectangle[] faces = imageProcessing.DetectFace(fileName);
					CreateFaceRectangle(faces);

					if (faces.Length == 1) {
						//buttonSaveSelected.Enabled = false;
						status = "Область с лицом выделена квадратом";
					} else {
						status = "Обработка выбранного изображения невозможна, ";
						status += faces.Length == 0 ?
							"лицо не удалось обнаружить" :
							"на изображении обнаружено несколько лиц";

						buttonSaveSelected.Enabled = false;
					}
				} else {
					status = selectedCount == 0 ?
						"Изображение не выбрано" :
						"Выбрано более одного изображения, предпросмотр недоступен";
					pictureBoxPreview.Image = null;
					pictureBoxPreview.Controls.Clear();
				}
			} catch (Exception) {
				status = "Не удалось загрузить изображение";
			}

			textBoxStatus.Text = status;
		}

		private void ListViewImages_ColumnClick(object sender, ColumnClickEventArgs e) {
			if (e.Column == listViewColumnSorter.ColumnToSort) {
				if (listViewColumnSorter.OrderOfSort == SortOrder.Ascending) {
					listViewColumnSorter.OrderOfSort = SortOrder.Descending;
				} else {
					listViewColumnSorter.OrderOfSort = SortOrder.Ascending;
				}
			} else {
				listViewColumnSorter.ColumnToSort = e.Column;
				listViewColumnSorter.OrderOfSort = SortOrder.Ascending;
			}

			listViewImages.SetSortIcon(e.Column, listViewColumnSorter.OrderOfSort);
			listViewImages.Sort();
		}

		private void ListViewImagesAddItem(ListViewItem item) {
			if (InvokeRequired) {
				Invoke(new Action<ListViewItem>(ListViewImagesAddItem), new object[] { item });
				return;
			}

			if (listViewImages.Items.ContainsKey(item.Name))
				return;

			listViewImages.Items.Add(item);
		}


		private void Control_DragEnter(object sender, DragEventArgs e) {
			Console.WriteLine("listViewImages_DragEnter");
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Link;
		}

		private void Control_DragDrop(object sender, DragEventArgs e) {
			Console.WriteLine("listViewImages_DragDrop");
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

			List<string> fileNames = new List<string>();

			foreach (string fileName in files) {
				FileAttributes fileAttribute = File.GetAttributes(fileName);
				if ((fileAttribute & FileAttributes.Directory) == FileAttributes.Directory) {
					try {
						string[] filesInDirectory = Directory.GetFiles(fileName, "*", System.IO.SearchOption.AllDirectories);
						fileNames.AddRange(filesInDirectory);
					} catch (Exception exception) {
						Console.WriteLine(exception.Message + " " + exception.StackTrace);
					}
				} else {
					fileNames.Add(fileName);
				}
			}

			Thread thread = new Thread(() => CheckFilesFormatAndAddToList(fileNames));
			thread.Start();
		}



		private void CheckBoxSelectedSize_CheckedChanged(object sender, EventArgs e) {
			Console.WriteLine("checkBoxSelectedSize_CheckedChanged");
			UpdateSelectedSizeControlsState();
			UpdateSaveButtonsState();
		}

		private void CheckBox_CheckedChanged(object sender, EventArgs e) {
			Console.WriteLine("checkBox1_CheckedChanged");
			UpdateSaveButtonsState();
		}


		private void TextBox_TextChanged(object sender, EventArgs e) {
			Console.WriteLine("textBoxSavePath_TextChanged");
			UpdateSaveButtonsState();
		}

		private void TextBoxSelected_KeyPress(object sender, KeyPressEventArgs e) {
			Console.WriteLine("textBoxSelected_KeyPress");
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
				e.Handled = true;
		}


		private void UpdateControlsState() {
			if (InvokeRequired) {
				Invoke(new Action(UpdateControlsState));
				return;
			}

			Control[] controls = new Control[] {
				checkBoxMisAd,
				checkBoxLoyalty,
				checkBoxSelectedSize,
				labelSavePath,
				textBoxSavePath,
				buttonSavePath
			};

			foreach (Control control in controls)
				control.Enabled = listViewImages.Items.Count > 0;

			UpdateSelectedSizeControlsState();
			UpdateSaveButtonsState();

			if (listViewImages.Items.Count == 0) {
				Control[] controlsToDisable = new Control[] {
					buttonSaveSelected,
					buttonSaveAll,
					textBoxSelectedWidth,
					labelSelectedWidth
				};

				foreach (Control control in controlsToDisable)
					control.Enabled = false;

				buttonAdd.Select();
			} else {
				listViewImages.Items[0].Selected = true;
				buttonSavePath.Select();
			}
		}

		private void UpdateSelectedSizeControlsState() {
			Console.WriteLine("UpdateSelectedSizeControlsState");
			bool enabled = checkBoxSelectedSize.Checked;
			Control[] controls = new Control[] {
				checkBoxCircle,
				labelSelectedWidth,
				textBoxSelectedWidth
			};

			foreach (Control control in controls)
				control.Enabled = enabled;
		}

		private void UpdateSaveButtonsState() {
			bool enabled = true;

			if (!checkBoxSelectedSize.Checked && !checkBoxMisAd.Checked && !checkBoxLoyalty.Checked)
				enabled = false;

			if (checkBoxSelectedSize.Checked)
				if (string.IsNullOrEmpty(textBoxSelectedWidth.Text))
					enabled = false;

			if (string.IsNullOrEmpty(textBoxSavePath.Text))
				enabled = false;

			buttonSaveAll.Enabled = enabled;

			if (listViewImages.SelectedItems.Count != 1)
				enabled = false;

			buttonSaveSelected.Enabled = enabled;

		}

		private void UpdateProgressState(int progress) {
			if (InvokeRequired) {
				Invoke(new Action<int>(UpdateProgressState), new object[] { progress });
				return;
			}

			progressBar.Value = progress;

			if (progress == 0) {
				progressBar.Visible = true;
			} else if (progress == 100) {
				progressBar.Visible = false;
			}
		}

		public void UpdateResultTextInListView(string fileName, string imageSize, string result) {
			if (InvokeRequired) {
				Invoke(new Action<string, string, string>(UpdateResultTextInListView), new object[] { fileName, imageSize, result });
				return;
			}

			int index = listViewImages.Items.IndexOfKey(fileName);
			if (listViewImages.Items[index].SubItems.Count < 3) {
				listViewImages.Items[index].SubItems.Add(imageSize);
				listViewImages.Items[index].SubItems.Add(result);
			} else {
				listViewImages.Items[index].SubItems[1].Text = imageSize;
				listViewImages.Items[index].SubItems[2].Text = result;
			}
		}

		public void UpdateStatus(string status) {
			if (InvokeRequired) {
				Invoke(new Action<string>(UpdateStatus), new object[] { status });
				return;
			}

			textBoxStatus.Text = status;
		}


		private void CheckFilesFormatAndAddToList(List<string> fileNames) {
			Console.WriteLine("CheckFilesFormatAndAddToList");

			UpdateProgressState(0);

			float step = 100.0f / fileNames.Count;
			float value = 0.0f;

			int fileNumber = 1;
			foreach (string fileName in fileNames) {
				Console.WriteLine("current file: " + fileName);
				UpdateStatus("Добавление файлов: " + fileNumber + " из " + fileNames.Count);
				fileNumber++;

				try {
					string extension = "*" + Path.GetExtension(fileName).ToLower();
					if (!imageAcceptableFormats.Contains(extension))
						continue;
					
					ListViewItem item = new ListViewItem(new string[] { Path.GetFileName(fileName), "", "" });
					item.Name = fileName;

					ListViewImagesAddItem(item);
				} catch (Exception) {
				}

				value += step;
				UpdateProgressState((int)value);
			}

			UpdateProgressState(100);
			UpdateControlsState();
			UpdateStatus("Добавление файлов завершено");
		}

		private void CreateFaceRectangle(Rectangle[] faces) {
			if (faces.Length == 0)
				return;

			PropertyInfo pInfo = pictureBoxPreview.GetType().
				GetProperty("ImageRectangle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			Rectangle rectangle = (Rectangle)pInfo.GetValue(pictureBoxPreview, null);

			double widthRatio = (double)rectangle.Width / (double)pictureBoxPreview.Image.Width;
			double heightRatio = (double)rectangle.Height / (double)pictureBoxPreview.Image.Height;

			foreach (Rectangle face in faces) {
				Rectangle faceSize = new Rectangle(
					(int)(face.X * widthRatio) + rectangle.X,
					(int)(face.Y * heightRatio) + rectangle.Y,
					(int)(face.Width * widthRatio),
					(int)(face.Height * heightRatio));

				Label labelCutScope = new Label();
				labelCutScope.SetBounds(
					faceSize.X - faceSize.Width / 2,
					faceSize.Y - faceSize.Height / 2,
					faceSize.Width * 2,
					faceSize.Height * 2);
				labelCutScope.BackColor = Color.FromArgb(20, Color.OrangeRed);
				labelCutScope.BorderStyle = BorderStyle.FixedSingle;
				pictureBoxPreview.Controls.Add(labelCutScope);
				labelCutScope.BringToFront();

				Label labelFace = new Label();
				labelFace.Bounds = faceSize;
				labelFace.BackColor = Color.FromArgb(30, Color.OrangeRed);
				labelFace.BorderStyle = BorderStyle.FixedSingle;
				pictureBoxPreview.Controls.Add(labelFace);
				labelFace.BringToFront();
			}
		}

		private void ProcessAllFiles(List<string> fileNames) {
			UpdateProgressState(0);

			float step = 100.0f / fileNames.Count;
			float value = 0.0f;

			bool allIsSuccessed = true;
			int fileNumber = 1;
			foreach (string fileName in fileNames) {
				Console.WriteLine("current file: " + fileName);
				UpdateStatus("Обработка файлов: " + fileNumber + " из " + fileNames.Count);
				fileNumber++;

				if (!imageProcessing.CropImageAndSave(fileName, GetSaveFormats(), textBoxSavePath.Text))
					allIsSuccessed = false;

				value += step;
				UpdateProgressState((int)value);
			}

			UpdateProgressState(100);

			if (allIsSuccessed) {
				MessageBox.Show("Обработка завершена", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
			} else {
				MessageBox.Show("Обработка завершена" + Environment.NewLine + "Имеются файлы, которые не удалось обработать, подробности в колонке с результатом",
					"Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}

			try {
				Process.Start(textBoxSavePath.Text);
			} catch (Exception) {
			}

			UpdateStatus("Обработка завершена");
		}

		private List<SaveFormat> GetSaveFormats() {
			List<SaveFormat> saveFormats = new List<SaveFormat>();

			if (checkBoxMisAd.Checked)
				saveFormats.Add(new SaveFormat(checkBoxMisAd.Text.Replace(Environment.NewLine, " "),
					500, 500, false));

			if (checkBoxLoyalty.Checked)
				saveFormats.Add(new SaveFormat(checkBoxLoyalty.Text.Replace(Environment.NewLine, " "),
					500, 500, true));

			if (checkBoxSelectedSize.Checked)
				saveFormats.Add(new SaveFormat(textBoxSelectedWidth.Text + " x " + textBoxSelectedWidth.Text,
					int.Parse(textBoxSelectedWidth.Text), int.Parse(textBoxSelectedWidth.Text), checkBoxCircle.Checked));

			return saveFormats;
		}
	}
}
