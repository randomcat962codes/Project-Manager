using JsonStorage;

class Program
{
  public static void Main(string[] args)
  {
    Storage storage = new Storage("projectManager.json");
    storage.Load();

    Console.WriteLine("Hello World!");
    Console.Write("Press enter to close the program > ");
    Console.ReadLine();
  }
}