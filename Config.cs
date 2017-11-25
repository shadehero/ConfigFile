using System;
using System.IO;
using System.Collections.Generic;

namespace ConfigFile
{
	public class Config
	{
		private string PathToFile;

		private void ClearFile() {
			File.WriteAllBytes(PathToFile, new byte[0]);
		}

		private string[] GetFileToArray() {
			return File.ReadAllLines(PathToFile);
		}

		private string GetFileToText() {
			return File.ReadAllText(PathToFile);)
		}

		private bool NameExits(string name) {
			string file = GetFileToText();
			return (file.Contains(name));
		}

		private int GetNameLineIndex(string name) {
			if (NameExits(name)) {
				string[] file = GetFileToArray();

				for (int i = 0; i < file.Length; i++) {
					
					string[] line = file[i].Split(':');

					if (line[0] == name) {
						return i;
					}
				}
				return 0;
			}
			return 0;
		}

		public Config(string file){
			PathToFile = file;
			if (!File.Exists(PathToFile)) {
				File.Create(PathToFile);
			}
		}

		public string GetValue(string name){
			if(NameExits(name)){
				string[] file = GetFileToArray();

				for (int i = 0; i<file.Length; i++){
					string[] splitLine = file[i].Split(':');

					if (splitLine[0] == name) {
					return splitLine[1];
					}
				}
				return string.Empty;
			}
			return string.Empty;
		}

		public void Add(string name, string value){
			File.AppendAllText(PathToFile,name + ":" + value + Environment.NewLine);
		}

		public void Update(string name, string newValue){
			if (NameExits(name)) {
				string[] file = GetFileToArray();
				int index = GetNameLineIndex(name);
				file[index] = name + ":" + newValue;
				ClearFile();
				File.WriteAllLines(PathToFile, file);
			}
		}

		public void Delete(string name) {
			if (NameExits(name)) {
				string[] file = GetFileToArray();
				List<string> newFileList = new List<string>();

				int index = GetNameLineIndex(name);
				file[index] = string.Empty;
				ClearFile();

				for (int i = 0; i < file.Length; i++) {
					if (file[i] != string.Empty) {
						newFileList.Add(file[i]);
					}
				}
				File.WriteAllLines(PathToFile,newFileList.ToArray());
			}
		}
	}
}
