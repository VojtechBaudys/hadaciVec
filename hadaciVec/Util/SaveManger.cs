using System;
using System.IO;

namespace hadaciVec.Util;
using Newtonsoft.Json;

public class SaveManger
{
	public SaveManger()
	{
		// future :)
	}

	public void SaveStats(string jsonString)
	{
		FileExist();
		File.WriteAllText("save.json", jsonString);
	}
	
	private void FileExist()
	{
		try
		{
			JsonConvert.DeserializeObject(File.ReadAllText("save.json"));
		}
		catch
		{
			File.Create("save.json").Close();
			File.WriteAllText("save.json", "{}");
		}
	}
}