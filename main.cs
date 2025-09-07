using JsonStorage;

class Program
{
  //Function for generating underlined titles
  static string WriteTitle(string title)
  {
    string underline = "";
    foreach (char x in title)
    {
      underline += "-";
    }
    return title + "\n" + underline;
  }
  //Function for getting the displayed number of an item in a list
  static string ListNumber(string[] arr, string val)
  {
    return Convert.ToString(Array.IndexOf(arr, val)+1);
  }
  //Function for handling potential null values in input
  static string GetInput()
  {
    Console.Write("> ");
    string result = "";
    string? inp = Console.ReadLine();
    if (!(inp == null))
    {
      result = inp;
    }
    return result;
  }

  public static void Main(string[] args)
  {
    //Storage information
    Storage storage = new Storage("projectManager.json");
    storage.Load();

    //Menu information
    string[] menuOptions = { "Exit" };
    string userInput;

    //Program loop
    bool runProgram = true;
    while (runProgram)
    {
      Console.WriteLine(WriteTitle("Project Manager"));

      //Display options & get input from user
      for (int i = 0; i < menuOptions.Length; i++)
      {
        Console.WriteLine(Convert.ToString(i + 1) + ": " + menuOptions[i]);
      }
      userInput = GetInput();

      Console.Clear();

      //Checks for valid input and runs the selected options, otherwise asks the user to repeat their input and check for errors
      if (userInput == ListNumber(menuOptions, "Exit"))
      {
        runProgram = false;
      }
      else //Invalid input
      {
        Console.WriteLine("There was an error in your input. Please make sure the option you choice is in the list, and is a number.\nPress enter to continue.");
        GetInput();
      }

      Console.Clear();
    }
  }
}