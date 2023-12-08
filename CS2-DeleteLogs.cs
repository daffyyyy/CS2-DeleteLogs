using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace CS2_DeleteLogs;

[MinimumApiVersion(110)]
public class CS2_DeleteLogs : BasePlugin, IPluginConfig<CS2_DeleteLogsConfig>
{
	public CS2_DeleteLogsConfig Config { get; set; } = new();

	public override string ModuleName => "CS2-DeleteLogs";
	public override string ModuleDescription => "Delete old logs";
	public override string ModuleAuthor => "daffyy";
	public override string ModuleVersion => "1.0.0";

	public override void Load(bool hotReload)
	{
		RegisterListener<Listeners.OnMapStart>(OnMapStart);

		if (hotReload)
		{
			OnMapStart(string.Empty);
		}
	}

	public void OnConfigParsed(CS2_DeleteLogsConfig config)
	{
		Config = config;
	}

	private void OnMapStart(string mapname)
	{
		DeleteOldLogs();
	}

	private void DeleteOldLogs()
	{
		string? parentDir = Path.GetDirectoryName(ModuleDirectory);
		string? grandParentDir = Path.GetDirectoryName(parentDir);

		if (parentDir == null || grandParentDir == null)
			return;

		string logsDirectory = Path.Combine(grandParentDir, "logs");

		try
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(logsDirectory);

			// Get all files in the directory
			FileInfo[] files = directoryInfo.GetFiles();

			// Calculate the date 14 days ago
			DateTime expiredDate = DateTime.Now.AddDays(-Config.LogsOlderThan);

			bool deleteBigger = Config.DeleteLogsOnlyBiggerThan;
			int minimumLogSize = Config.LogsBiggerThan;

			foreach (FileInfo file in files)
			{
				// Check if the file's last write time is older than 14 days
				if (file.LastWriteTime < expiredDate)
				{
					if (deleteBigger)
					{
						long fileSizeInBytes = file.Length;
						double fileSizeInMB = (double)fileSizeInBytes / (1024 * 1024);

						if (fileSizeInMB > minimumLogSize)
						{
							file.Delete();
							Logger.LogInformation($"Deleting log file {file.FullName} with size {fileSizeInMB:F2} MB");
						}
					}
					else
					{
						file.Delete();
						Logger.LogInformation($"Deleting log file {file.FullName}");
					}
				}
			}
			Logger.LogInformation("Old logs deleted!");
		}
		catch (Exception)
		{ }
	}
}