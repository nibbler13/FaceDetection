namespace FaceDetection
{
	partial class FormMain
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.buttonAdd = new System.Windows.Forms.Button();
			this.listViewImages = new System.Windows.Forms.ListView();
			this.columnHeaderFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderOriginalSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderResult = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.buttonDelete = new System.Windows.Forms.Button();
			this.checkBoxMisAd = new System.Windows.Forms.CheckBox();
			this.groupBoxSave = new System.Windows.Forms.GroupBox();
			this.checkBoxLoyalty = new System.Windows.Forms.CheckBox();
			this.checkBoxCircle = new System.Windows.Forms.CheckBox();
			this.buttonSavePath = new System.Windows.Forms.Button();
			this.textBoxSavePath = new System.Windows.Forms.TextBox();
			this.buttonSaveAll = new System.Windows.Forms.Button();
			this.textBoxSelectedWidth = new System.Windows.Forms.TextBox();
			this.labelSavePath = new System.Windows.Forms.Label();
			this.buttonSaveSelected = new System.Windows.Forms.Button();
			this.labelSelectedWidth = new System.Windows.Forms.Label();
			this.checkBoxSelectedSize = new System.Windows.Forms.CheckBox();
			this.groupBoxFileList = new System.Windows.Forms.GroupBox();
			this.groupBoxPreview = new System.Windows.Forms.GroupBox();
			this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
			this.textBoxStatus = new System.Windows.Forms.TextBox();
			this.pictureBoxBottomLineRight = new System.Windows.Forms.PictureBox();
			this.pictureBoxBottomLineLeft = new System.Windows.Forms.PictureBox();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.groupBoxSave.SuspendLayout();
			this.groupBoxFileList.SuspendLayout();
			this.groupBoxPreview.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxBottomLineRight)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxBottomLineLeft)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonAdd
			// 
			this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonAdd.Location = new System.Drawing.Point(6, 355);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(85, 23);
			this.buttonAdd.TabIndex = 1;
			this.buttonAdd.Text = "Добавить";
			this.buttonAdd.UseVisualStyleBackColor = true;
			this.buttonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
			// 
			// listViewImages
			// 
			this.listViewImages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listViewImages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderFileName,
            this.columnHeaderOriginalSize,
            this.columnHeaderResult});
			this.listViewImages.FullRowSelect = true;
			this.listViewImages.Location = new System.Drawing.Point(6, 19);
			this.listViewImages.Name = "listViewImages";
			this.listViewImages.ShowItemToolTips = true;
			this.listViewImages.Size = new System.Drawing.Size(475, 330);
			this.listViewImages.TabIndex = 5;
			this.listViewImages.UseCompatibleStateImageBehavior = false;
			this.listViewImages.View = System.Windows.Forms.View.Details;
			this.listViewImages.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListViewImages_ColumnClick);
			this.listViewImages.SelectedIndexChanged += new System.EventHandler(this.ListViewImages_SelectedIndexChanged);
			// 
			// columnHeaderFileName
			// 
			this.columnHeaderFileName.Text = "Имя файла";
			this.columnHeaderFileName.Width = 253;
			// 
			// columnHeaderOriginalSize
			// 
			this.columnHeaderOriginalSize.Text = "Разрешение";
			this.columnHeaderOriginalSize.Width = 81;
			// 
			// columnHeaderResult
			// 
			this.columnHeaderResult.Text = "Результат";
			this.columnHeaderResult.Width = 115;
			// 
			// buttonDelete
			// 
			this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonDelete.Enabled = false;
			this.buttonDelete.Location = new System.Drawing.Point(396, 355);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(85, 23);
			this.buttonDelete.TabIndex = 6;
			this.buttonDelete.Text = "Удалить";
			this.buttonDelete.UseVisualStyleBackColor = true;
			this.buttonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
			// 
			// checkBoxMisAd
			// 
			this.checkBoxMisAd.AutoSize = true;
			this.checkBoxMisAd.Enabled = false;
			this.checkBoxMisAd.Location = new System.Drawing.Point(80, 41);
			this.checkBoxMisAd.Name = "checkBoxMisAd";
			this.checkBoxMisAd.Size = new System.Drawing.Size(177, 30);
			this.checkBoxMisAd.TabIndex = 7;
			this.checkBoxMisAd.Text = "Инфоклиника, Active Directory\r\n500 х 500, квадрат";
			this.checkBoxMisAd.UseVisualStyleBackColor = true;
			this.checkBoxMisAd.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
			// 
			// groupBoxSave
			// 
			this.groupBoxSave.Controls.Add(this.checkBoxLoyalty);
			this.groupBoxSave.Controls.Add(this.checkBoxCircle);
			this.groupBoxSave.Controls.Add(this.buttonSavePath);
			this.groupBoxSave.Controls.Add(this.textBoxSavePath);
			this.groupBoxSave.Controls.Add(this.buttonSaveAll);
			this.groupBoxSave.Controls.Add(this.textBoxSelectedWidth);
			this.groupBoxSave.Controls.Add(this.labelSavePath);
			this.groupBoxSave.Controls.Add(this.buttonSaveSelected);
			this.groupBoxSave.Controls.Add(this.labelSelectedWidth);
			this.groupBoxSave.Controls.Add(this.checkBoxSelectedSize);
			this.groupBoxSave.Controls.Add(this.checkBoxMisAd);
			this.groupBoxSave.Location = new System.Drawing.Point(12, 402);
			this.groupBoxSave.Name = "groupBoxSave";
			this.groupBoxSave.Size = new System.Drawing.Size(487, 144);
			this.groupBoxSave.TabIndex = 8;
			this.groupBoxSave.TabStop = false;
			this.groupBoxSave.Text = "Параметры сохранения";
			// 
			// checkBoxLoyalty
			// 
			this.checkBoxLoyalty.AutoSize = true;
			this.checkBoxLoyalty.Enabled = false;
			this.checkBoxLoyalty.Location = new System.Drawing.Point(263, 41);
			this.checkBoxLoyalty.Name = "checkBoxLoyalty";
			this.checkBoxLoyalty.Size = new System.Drawing.Size(132, 30);
			this.checkBoxLoyalty.TabIndex = 19;
			this.checkBoxLoyalty.Text = "Монитор лояльности\r\n500 x 500, круг";
			this.checkBoxLoyalty.UseVisualStyleBackColor = true;
			this.checkBoxLoyalty.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
			// 
			// checkBoxCircle
			// 
			this.checkBoxCircle.AutoSize = true;
			this.checkBoxCircle.Enabled = false;
			this.checkBoxCircle.Location = new System.Drawing.Point(202, 77);
			this.checkBoxCircle.Name = "checkBoxCircle";
			this.checkBoxCircle.Size = new System.Drawing.Size(49, 17);
			this.checkBoxCircle.TabIndex = 18;
			this.checkBoxCircle.Text = "Круг";
			this.checkBoxCircle.UseVisualStyleBackColor = true;
			// 
			// buttonSavePath
			// 
			this.buttonSavePath.Enabled = false;
			this.buttonSavePath.Location = new System.Drawing.Point(454, 13);
			this.buttonSavePath.Name = "buttonSavePath";
			this.buttonSavePath.Size = new System.Drawing.Size(25, 25);
			this.buttonSavePath.TabIndex = 17;
			this.buttonSavePath.Text = "...";
			this.buttonSavePath.UseVisualStyleBackColor = true;
			this.buttonSavePath.Click += new System.EventHandler(this.ButtonSavePath_Click);
			// 
			// textBoxSavePath
			// 
			this.textBoxSavePath.Enabled = false;
			this.textBoxSavePath.Location = new System.Drawing.Point(109, 16);
			this.textBoxSavePath.Name = "textBoxSavePath";
			this.textBoxSavePath.ReadOnly = true;
			this.textBoxSavePath.Size = new System.Drawing.Size(339, 20);
			this.textBoxSavePath.TabIndex = 16;
			this.textBoxSavePath.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
			// 
			// buttonSaveAll
			// 
			this.buttonSaveAll.Enabled = false;
			this.buttonSaveAll.Location = new System.Drawing.Point(321, 103);
			this.buttonSaveAll.Name = "buttonSaveAll";
			this.buttonSaveAll.Size = new System.Drawing.Size(160, 35);
			this.buttonSaveAll.TabIndex = 11;
			this.buttonSaveAll.Text = "Обработать и сохранить\r\nвесь список";
			this.buttonSaveAll.UseVisualStyleBackColor = true;
			this.buttonSaveAll.Click += new System.EventHandler(this.ButtonSaveAll_Click);
			// 
			// textBoxSelectedWidth
			// 
			this.textBoxSelectedWidth.Enabled = false;
			this.textBoxSelectedWidth.Location = new System.Drawing.Point(377, 75);
			this.textBoxSelectedWidth.MaxLength = 4;
			this.textBoxSelectedWidth.Name = "textBoxSelectedWidth";
			this.textBoxSelectedWidth.Size = new System.Drawing.Size(60, 20);
			this.textBoxSelectedWidth.TabIndex = 12;
			this.textBoxSelectedWidth.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
			this.textBoxSelectedWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxSelected_KeyPress);
			// 
			// labelSavePath
			// 
			this.labelSavePath.AutoSize = true;
			this.labelSavePath.Enabled = false;
			this.labelSavePath.Location = new System.Drawing.Point(6, 19);
			this.labelSavePath.Name = "labelSavePath";
			this.labelSavePath.Size = new System.Drawing.Size(96, 13);
			this.labelSavePath.TabIndex = 15;
			this.labelSavePath.Text = "Путь сохранения:";
			// 
			// buttonSaveSelected
			// 
			this.buttonSaveSelected.Enabled = false;
			this.buttonSaveSelected.Location = new System.Drawing.Point(6, 103);
			this.buttonSaveSelected.Name = "buttonSaveSelected";
			this.buttonSaveSelected.Size = new System.Drawing.Size(160, 35);
			this.buttonSaveSelected.TabIndex = 9;
			this.buttonSaveSelected.Text = "Обработать и сохранить\r\nвыбранное изображение";
			this.buttonSaveSelected.UseVisualStyleBackColor = true;
			this.buttonSaveSelected.Click += new System.EventHandler(this.ButtonSaveSelected_Click);
			// 
			// labelSelectedWidth
			// 
			this.labelSelectedWidth.AutoSize = true;
			this.labelSelectedWidth.Enabled = false;
			this.labelSelectedWidth.Location = new System.Drawing.Point(257, 78);
			this.labelSelectedWidth.Name = "labelSelectedWidth";
			this.labelSelectedWidth.Size = new System.Drawing.Size(114, 13);
			this.labelSelectedWidth.TabIndex = 11;
			this.labelSelectedWidth.Text = "размер стороны (px):";
			// 
			// checkBoxSelectedSize
			// 
			this.checkBoxSelectedSize.AutoSize = true;
			this.checkBoxSelectedSize.Enabled = false;
			this.checkBoxSelectedSize.Location = new System.Drawing.Point(50, 77);
			this.checkBoxSelectedSize.Name = "checkBoxSelectedSize";
			this.checkBoxSelectedSize.Size = new System.Drawing.Size(146, 17);
			this.checkBoxSelectedSize.TabIndex = 10;
			this.checkBoxSelectedSize.Text = "Произвольный размер:";
			this.checkBoxSelectedSize.UseVisualStyleBackColor = true;
			this.checkBoxSelectedSize.CheckedChanged += new System.EventHandler(this.CheckBoxSelectedSize_CheckedChanged);
			// 
			// groupBoxFileList
			// 
			this.groupBoxFileList.Controls.Add(this.listViewImages);
			this.groupBoxFileList.Controls.Add(this.buttonAdd);
			this.groupBoxFileList.Controls.Add(this.buttonDelete);
			this.groupBoxFileList.Location = new System.Drawing.Point(12, 12);
			this.groupBoxFileList.Name = "groupBoxFileList";
			this.groupBoxFileList.Size = new System.Drawing.Size(487, 384);
			this.groupBoxFileList.TabIndex = 12;
			this.groupBoxFileList.TabStop = false;
			this.groupBoxFileList.Text = "Список файлов для обработки";
			// 
			// groupBoxPreview
			// 
			this.groupBoxPreview.Controls.Add(this.pictureBoxPreview);
			this.groupBoxPreview.Controls.Add(this.textBoxStatus);
			this.groupBoxPreview.Location = new System.Drawing.Point(505, 12);
			this.groupBoxPreview.Name = "groupBoxPreview";
			this.groupBoxPreview.Size = new System.Drawing.Size(374, 534);
			this.groupBoxPreview.TabIndex = 13;
			this.groupBoxPreview.TabStop = false;
			this.groupBoxPreview.Text = "Предпросмотр";
			// 
			// pictureBoxPreview
			// 
			this.pictureBoxPreview.Location = new System.Drawing.Point(6, 19);
			this.pictureBoxPreview.Name = "pictureBoxPreview";
			this.pictureBoxPreview.Size = new System.Drawing.Size(362, 483);
			this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBoxPreview.TabIndex = 0;
			this.pictureBoxPreview.TabStop = false;
			// 
			// textBoxStatus
			// 
			this.textBoxStatus.BackColor = System.Drawing.SystemColors.Info;
			this.textBoxStatus.Location = new System.Drawing.Point(6, 508);
			this.textBoxStatus.Name = "textBoxStatus";
			this.textBoxStatus.ReadOnly = true;
			this.textBoxStatus.Size = new System.Drawing.Size(362, 20);
			this.textBoxStatus.TabIndex = 14;
			this.textBoxStatus.Text = "Изображение не выбрано";
			// 
			// pictureBoxBottomLineRight
			// 
			this.pictureBoxBottomLineRight.Image = global::FaceDetection.Properties.Resources.bottomLineContinuesClear;
			this.pictureBoxBottomLineRight.Location = new System.Drawing.Point(661, 552);
			this.pictureBoxBottomLineRight.Name = "pictureBoxBottomLineRight";
			this.pictureBoxBottomLineRight.Size = new System.Drawing.Size(230, 10);
			this.pictureBoxBottomLineRight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBoxBottomLineRight.TabIndex = 14;
			this.pictureBoxBottomLineRight.TabStop = false;
			// 
			// pictureBoxBottomLineLeft
			// 
			this.pictureBoxBottomLineLeft.Image = global::FaceDetection.Properties.Resources.bottomLineTemplate;
			this.pictureBoxBottomLineLeft.Location = new System.Drawing.Point(0, 552);
			this.pictureBoxBottomLineLeft.Name = "pictureBoxBottomLineLeft";
			this.pictureBoxBottomLineLeft.Size = new System.Drawing.Size(662, 10);
			this.pictureBoxBottomLineLeft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBoxBottomLineLeft.TabIndex = 15;
			this.pictureBoxBottomLineLeft.TabStop = false;
			// 
			// progressBar
			// 
			this.progressBar.Location = new System.Drawing.Point(0, 552);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(892, 10);
			this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.progressBar.TabIndex = 7;
			this.progressBar.Visible = false;
			// 
			// FormMain
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(891, 562);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.pictureBoxBottomLineLeft);
			this.Controls.Add(this.pictureBoxBottomLineRight);
			this.Controls.Add(this.groupBoxPreview);
			this.Controls.Add(this.groupBoxFileList);
			this.Controls.Add(this.groupBoxSave);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FaceDetection";
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Control_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Control_DragEnter);
			this.groupBoxSave.ResumeLayout(false);
			this.groupBoxSave.PerformLayout();
			this.groupBoxFileList.ResumeLayout(false);
			this.groupBoxPreview.ResumeLayout(false);
			this.groupBoxPreview.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxBottomLineRight)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxBottomLineLeft)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBoxPreview;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.ListView listViewImages;
		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.CheckBox checkBoxMisAd;
		private System.Windows.Forms.GroupBox groupBoxSave;
		private System.Windows.Forms.Label labelSelectedWidth;
		private System.Windows.Forms.CheckBox checkBoxSelectedSize;
		private System.Windows.Forms.TextBox textBoxSelectedWidth;
		private System.Windows.Forms.Button buttonSaveSelected;
		private System.Windows.Forms.Button buttonSaveAll;
		private System.Windows.Forms.ColumnHeader columnHeaderFileName;
		private System.Windows.Forms.ColumnHeader columnHeaderResult;
		private System.Windows.Forms.GroupBox groupBoxFileList;
		private System.Windows.Forms.GroupBox groupBoxPreview;
		private System.Windows.Forms.Button buttonSavePath;
		private System.Windows.Forms.TextBox textBoxSavePath;
		private System.Windows.Forms.Label labelSavePath;
		private System.Windows.Forms.ColumnHeader columnHeaderOriginalSize;
		private System.Windows.Forms.TextBox textBoxStatus;
		private System.Windows.Forms.PictureBox pictureBoxBottomLineRight;
		private System.Windows.Forms.PictureBox pictureBoxBottomLineLeft;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.CheckBox checkBoxCircle;
		private System.Windows.Forms.CheckBox checkBoxLoyalty;
	}
}

