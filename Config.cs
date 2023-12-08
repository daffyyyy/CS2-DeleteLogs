using CounterStrikeSharp.API.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CS2_DeleteLogs
{
	public class CS2_DeleteLogsConfig : BasePluginConfig
	{
		public override int Version { get; set; } = 1;

		[JsonPropertyName("LogsOlderThan")]
		public int LogsOlderThan { get; set; } = 7;
		[JsonPropertyName("LogsBiggerThan")]
		public int LogsBiggerThan { get; set; } = 3;
		[JsonPropertyName("DeleteLogsOnlyBiggerThan")]
		public bool DeleteLogsOnlyBiggerThan { get; set; } = false;

	}
}
