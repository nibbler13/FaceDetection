using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceDetection {
	class ActiveDirectory {
		public static void InsertPicture(string sAMAccountName, string fileName) {//byte[] data) {
			using (DirectorySearcher dsSearcher = new DirectorySearcher()) {
				dsSearcher.Filter = "(&(objectClass=user) (sAMAccountName=" + sAMAccountName + "))";
				SearchResult result = dsSearcher.FindOne();
				if (result == null) {
					Console.WriteLine("sAMAccountName: " + sAMAccountName + " - user not founded");
					return;
				}

				using (FileStream inFile = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read)) {
					byte[] binaryData = new byte[inFile.Length];
					int bytesRead = inFile.Read(binaryData, 0, (int)inFile.Length);

					byte[] oldData = GetUserPictureBytes(sAMAccountName);

					using (DirectoryEntry user = new DirectoryEntry(result.Path)) {
						user.UsePropertyCache = false;
						user.Properties["jpegPhoto"].Clear();
						user.Properties["jpegPhoto"].Add(binaryData);
						user.CommitChanges();
					}

					byte[] newData = GetUserPictureBytes(sAMAccountName);

					bool updated = true;
					if (oldData == null && newData == null) {
						updated = false;
					} else if (oldData == null || newData == null) {

					} else {
						if (oldData.SequenceEqual(newData))
							updated = false;
					}

					Console.WriteLine(sAMAccountName + "|updateStatus|" + updated);
				}
			}
		}


		public static byte[] GetUserPictureBytes(string sAMAccountName) {
			using (DirectorySearcher dsSearcher = new DirectorySearcher()) {
				dsSearcher.Filter = "(&(objectClass=user) (sAMAccountName=" + sAMAccountName + "))";
				SearchResult result = dsSearcher.FindOne();

				using (DirectoryEntry user = new DirectoryEntry(result.Path)) {
					return user.Properties["jpegPhoto"].Value as byte[];
				}
			}
		}

		public static Image GetUserPicture(string sAMAccountName) {
			using (DirectorySearcher dsSearcher = new DirectorySearcher()) {
				dsSearcher.Filter = "(&(objectClass=user) (sAMAccountName=" + sAMAccountName + "))";
				SearchResult result = dsSearcher.FindOne();

				using (DirectoryEntry user = new DirectoryEntry(result.Path)) {
					byte[] data = user.Properties["jpegPhoto"].Value as byte[];

					if (data != null) {
						using (MemoryStream s = new MemoryStream(data)) {
							return Bitmap.FromStream(s);
						}
					}

					return null;
				}
			}
		}
	}
}
