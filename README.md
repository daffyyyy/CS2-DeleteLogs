# CS2-DeleteLogs

### Description
Delete old logs and save some disk space, logs are deleted on map start

### Requirments
[CounterStrikeSharp](https://github.com/roflmuffin/CounterStrikeSharp/) **tested on v110**

### Configuration
After first launch, u need to configure plugin in  addons/counterstrikesharp/configs/plugins/CS2-DeleteLogs/CS2-DeleteLogs.json

```json
{
  "Version": 1, // Don't touch
  "LogsOlderThan": 7, // Delete only logs older than x days
  "LogsBiggerThan": 2, // Delete only logs bigger than x MB
  "DeleteLogsOnlyBiggerThan": false, // Activate or deactivate up
  "ConfigVersion": 1 // Don't touch
}
```
