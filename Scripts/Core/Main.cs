using Serilog;
using Godot;

namespace ArcadeFootball.Scripts.Core;

public partial class Main : Node
{
	public override void _Ready()
	{
		// 初始化日志
		Log.Logger = new LoggerConfiguration()
			.MinimumLevel.Debug()  // 最低记录级别
			.WriteTo.Console()     // 输出到控制台
			.WriteTo.File(         // 输出到文件
				"logs/game.log", 
				rollingInterval: RollingInterval.Day,  // 按天分文件
				outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
			)
			.CreateLogger();
		
		Log.Information("游戏启动");
	}
	
	public override void _ExitTree()
	{
		Log.Information("游戏关闭");
		Log.CloseAndFlush();  // 确保日志写入完成
	}
}
