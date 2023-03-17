using System.Text.Json;
using CatanTests;

namespace PioniereVonNeuropaClient;

static class Program{
	/// <summary>
	///  The main entry point for the application.
	/// </summary>
	private static string BoardFile =
		"C:\\Users\\Luca\\RiderProjects\\CatanTests\\CatanTests\\bin\\Debug\\net7.0\\board.json";

	[STAThread]
	static void Main() {
		Game board = JsonSerializer.Deserialize<Game>(
			File.ReadAllBytes(BoardFile)) ?? throw new InvalidOperationException("Bumm");

		// To customize application configuration such as set high DPI settings or default font,
		// see https://aka.ms/applicationconfiguration.
		ApplicationConfiguration.Initialize();
		Application.Run(new Board(board));
	}
}