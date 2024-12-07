using System.Windows.Input;
using ToyPlCorrectness;
using ToyPlCorrectness.Application;
using ToyPlCorrectness.Language;

if (args.Length == 0)
{
    Console.WriteLine("First argument should be path to program file");
    Environment.Exit(1);
}

try
{
    if (args.Length > 1 && int.TryParse(args[1], out var n) && n > 3)
    {
        Constants.N = n;
    }

    var (programWithCondition, programVariables) = GetCommand(args[0]);

    foreach (var vcCond in programWithCondition.GetVc())
    {
        Console.WriteLine(vcCond);
    }
}
catch (Exception ex)
{
    Console.Error.WriteLine(ex.Message);
    Environment.Exit(1);
}


(ProgramWithCondition, HashSet<string> variables) GetCommand(string fileName)
{
    var file = new FileInfo(fileName);
    var extension = file.Extension;

    return extension switch
    {
        ".tpl" => ToyPlTranslator.GetProgram(fileName),
        _ => throw new ArgumentOutOfRangeException()
    };
}
