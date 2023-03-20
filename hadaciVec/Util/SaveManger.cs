using System.ComponentModel.DataAnnotations;
using System.IO;
using Newtonsoft.Json;

namespace hadaciVec.Util;

public class SaveManger
{
	const string SavePath = "save.json";

	public SaveManger()
	{
		// future :)
	}

	// Load data form {save.json} file
	// return json Object
	public dynamic LoadData()
	{
		FileExist();
		string jsonString = File.ReadAllText(SavePath);
		return JsonConvert.DeserializeObject(jsonString)!;
	}

	// Save stats to {save.json} file
	// jsonString - [string]
	public void SaveStats(string jsonString)
	{
		FileExist();
		File.WriteAllText(SavePath, jsonString);
	}
	
	// Check {save.json} if exist
	// false -> create new {save.json} file
	private void FileExist()
	{
		try
		{
			JsonConvert.DeserializeObject(File.ReadAllText(SavePath));
		}
		catch
		{
			File.Create("save.json").Close();
			File.WriteAllText("save.json", "{}");
		}
	}
}