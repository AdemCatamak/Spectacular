string BranchName = Argument("branchName", string.Empty);
string NugetServer = Argument("nugetServer", "https://api.nuget.org/v3/index.json");
string NugetApiKey = Argument("nugetApiKey", string.Empty);
string CIPlatform = Argument("ciPlatform", string.Empty);

string MasterCIPlatform = "travis";
string SelectedEnvironment = string.Empty;

string SolutionName = "Spectacular";
var ProjectsToBePacked  = new Project[]
{
  new Project("Spectacular"),
};

var FunctionalTestProjectPatterns = new string[]{
  "./**/*UnitTest.csproj",
};

var BuildConfig = "Release";
var DotNetPackedPath = "./dotnet-packed/";

string MasterEnvironment = "prod";
var BranchEnvironmentPairs = new Dictionary<string, string>()
{
  {"master", MasterEnvironment },
};

string[] DirectoriesToBeRemoved  = new string[]{
  $"./**/{SolutionName}*/**/bin/**",
  $"./**/{SolutionName}*/**/obj/**",
  $"./**/{SolutionName}*/**/build/**",
  DotNetPackedPath,
};

string CheckEnvVariableStage = "Check Env Variable";
string RemoveDirectoriesStage = "Remove Directories";
string DotNetCleanStage = "DotNet Clean";
string FunctionalTestStage = "Functional Tests";
string DotNetPackStage = "DotNet Pack";
string PushNugetStage = "Push Nuget";
string FinalStage = "Final";

Task(CheckEnvVariableStage)
.Does(()=>
{
  if(string.IsNullOrEmpty(BranchName))
    throw new Exception("branchName should not be empty");

  string originalBranchName = BranchName;
  BranchName = BranchName.Replace("refs/heads/","");

  Console.WriteLine($"BranchName = {BranchName} | OriginalBranchName = {originalBranchName}");
  
  if(BranchEnvironmentPairs.ContainsKey(BranchName))
  {
    SelectedEnvironment = BranchEnvironmentPairs[BranchName];
    Console.WriteLine("Selected Env = " + SelectedEnvironment);
  }
  else
  {
    Console.WriteLine("There is no predefined env for this branch");
  }

  Console.WriteLine($"CI Platform = {CIPlatform}");

  if(!string.IsNullOrEmpty(SelectedEnvironment) && CIPlatform == MasterCIPlatform)
    if(string.IsNullOrEmpty(NugetServer) || string.IsNullOrEmpty(NugetApiKey))
      throw new Exception("When selected environment is not empty, you should supply nuget server and nuget api key");
});

Task(RemoveDirectoriesStage)
.DoesForEach(DirectoriesToBeRemoved  , (directoryPath)=>
{
  var directories = GetDirectories(directoryPath);
    
  foreach (var directory in directories)
  {
    if(!DirectoryExists(directory)) continue;
    
    Console.WriteLine("Directory is cleaning : " + directory.ToString());     

    var settings = new DeleteDirectorySettings
    {
      Force = true,
      Recursive  = true
    };
    DeleteDirectory(directory, settings);
  }
});

Task(DotNetCleanStage)
.IsDependentOn(CheckEnvVariableStage)
.IsDependentOn(RemoveDirectoriesStage)
.Does(()=>
{
  DotNetCoreClean($"{SolutionName}.sln");
});

Task(FunctionalTestStage)
.IsDependentOn(DotNetCleanStage)
.DoesForEach(FunctionalTestProjectPatterns, (testProjectPattern)=>
{
  FilePathCollection testProjects = GetFiles(testProjectPattern);
  foreach (var testProject in testProjects)
  {
    Console.WriteLine($"Tests are running : {testProject.ToString()}" );
    var testSettings = new DotNetCoreTestSettings{Configuration = BuildConfig};
    DotNetCoreTest(testProject.FullPath, testSettings);
  }
});

Task(DotNetPackStage)
.IsDependentOn(FunctionalTestStage)
.DoesForEach(ProjectsToBePacked , (project)=>
{
  FilePath projFile = GetCsProjFile(project.Name);
  
  DateTime now = DateTime.UtcNow;
  var ticks= now.Ticks;
  string versionSuffix = $"{SelectedEnvironment}-{ticks}";
  if(SelectedEnvironment == MasterEnvironment)
    versionSuffix = string.Empty;

  var settings = new DotNetCorePackSettings
  {
    Configuration = BuildConfig,
    OutputDirectory = DotNetPackedPath,
    VersionSuffix = versionSuffix
  };

  DotNetCorePack(projFile.ToString(), settings);
});


Task(PushNugetStage)
.WithCriteria(() => !string.IsNullOrEmpty(SelectedEnvironment) && CIPlatform == MasterCIPlatform)
.IsDependentOn(DotNetPackStage)
.Does(()=>
{
  string filePathPattern = $"{DotNetPackedPath}*.nupkg";

  var nugetPushSettings = new DotNetCoreNuGetPushSettings
  {
    ApiKey = NugetApiKey,
    Source = NugetServer,
    ArgumentCustomization = args=>args.Append("--skip-duplicate")
  };
  
  Console.WriteLine($"{filePathPattern} is publishing");

  DotNetCoreNuGetPush(filePathPattern, nugetPushSettings);
    
  Console.WriteLine($"{filePathPattern} is published");
  Console.WriteLine();
});


Task(FinalStage)
.IsDependentOn(PushNugetStage)
.Does(() =>
{
  Console.WriteLine("Operation is completed succesfully");
});

var target = Argument("target", FinalStage);
RunTarget(target);

// Utility

FilePath GetCsProjFile(string projectName)
{
  FilePathCollection projFiles = GetFiles($"./**/{projectName}.csproj");
  if(projFiles.Count != 1)
  {
    foreach(var pName in projFiles)
    {
      Console.WriteLine(pName);
    }
    
    throw new Exception($"Only one {projectName}.csproj should be found");
  }
  
  return projFiles.First();
}

class Project
{
  public Project(string name, string runtime = "")
  {
    Name = name;
    Runtime = runtime;
  }
  
  public string Name {get;}
  public string Runtime {get;}
}
