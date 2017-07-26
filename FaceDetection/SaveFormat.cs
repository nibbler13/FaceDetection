namespace FaceDetection {
	class SaveFormat {
		public string nameAppendix { get; set; }
		public int width { get; set; }
		public int height { get; set; }
		public bool needToCircle { get; set; }

		public SaveFormat(string nameAppendix, int width, int height, bool needToCircle) {
			this.nameAppendix = nameAppendix;
			this.width = width;
			this.height = height;
			this.needToCircle = needToCircle;
		}
	}
}
