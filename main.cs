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
  static string GetInput(string prompt = "> ")
  {
    Console.Write(prompt);
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
    string[] menuOptions = { "View Projects", "Add Project", "Exit" };
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
      if (userInput == ListNumber(menuOptions, "View Projects"))
      {
        string[] projects = storage.GetKeys();
        int projectNum = -1;
        bool cancel = false; //This is if the user enters back.
        
        //Loop for showing the user the saved projects and asking them to choose which one they want to open
        bool validInput = false;
        while (!validInput)
        {
          Console.WriteLine(WriteTitle("Projects"));
          int displayedItems = 0;
          for (int i = 0; i < projects.Length; i++)
          {
            Console.WriteLine(Convert.ToString(i+1) + ": " + projects[i]);
            displayedItems++;
          }
          Console.WriteLine(Convert.ToString(displayedItems+1) + ": Back");
          userInput = GetInput();

          //Tries to convert the user input to a number
          try
          {
            projectNum = Convert.ToInt32(userInput)-1;
          }
          catch
          {
            Console.Clear();
            GetInput("There was an error with your input. Please make sure your input is a number in the list and try again.\n> ");
            Console.Clear();
            continue;
          }

          if (projectNum < 0 || projectNum > projects.Length)
          {
            cancel = true;
          }

          validInput = true;
        }

        Console.Clear();
        
        //Loop for veiwing the project
        //Project num is the value you will want for the project in the storage.
        bool showProject = true; 
        while (showProject && !(cancel))
        {
          string[] options = { "Return to Main Menu" };
          
          Console.WriteLine(WriteTitle(projects[projectNum]));

          //Displays the options and gets user input
          for (int i = 0; i < options.Length; i++)
          {
            Console.WriteLine(Convert.ToString(i+1) + ": " + options[i]);
          }
          userInput = GetInput();

          //Checks the options
          if (userInput == ListNumber(options, "Return to Main Menu"))
          {
            showProject = false;
          }
        }
      }
      else if (userInput == ListNumber(menuOptions, "Add Project"))
      {
        //Gets basic project information
        string projTitle = GetInput("Please enter the project title:\n> ");
        string projDesc = GetInput("Please enter a description for your project:\n> ");
        string projLang = GetInput("Please enter the language for your project:\n> ");
        string projFolder = GetInput("Please enter the path to your project folder:\n> ");
        string mainScriptPath = GetInput("Please enter the path to your projects main source file:\n> ");
        //Gets language specific information
        string? csprojPath = "none";
        if (projLang == "C#")
        {
          csprojPath = GetInput("Please enter the path to your project file:\n> ");
        }

        //Saves the information
        //Has a null referance warning!
        storage.storageObject[projTitle] = new Dictionary<string,dynamic?>();
        storage.storageObject[projTitle]!["ProjectDescription"] = projDesc;
        storage.storageObject[projTitle]!["ProjectLanguage"] = projLang;
        storage.storageObject[projTitle]!["ProjectFolder"] = projFolder;
        storage.storageObject[projTitle]!["MainScriptPath"] = mainScriptPath;
        if (projLang == "C#")
        {
          storage.storageObject[projTitle]!["ProjectFilePath"] = csprojPath;
        }
        storage.Save();
      }
      else if (userInput == ListNumber(menuOptions, "Exit"))
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