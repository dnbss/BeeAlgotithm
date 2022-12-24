using BeeAlgorithm;
using MathParser;

string expression = "f(x1,x2,x3) = (x1-2)^2 + (x2-3)^2 + (x3 + 6)^2";

Parser parser = new Parser();

parser.ConvertFunc(expression);

BeeAlgorithm.BeeAlgorithm algorithm = new BeeAlgorithm.BeeAlgorithm();

int countScoutBees = 300;
int countEliteBees = 50;
int countBestPoints = 80;
int countElitePoints = 20;
int countDefaultBees = 20;
double delta = 1;
int countIterations = 100;
double lowerBound = 20;
double upperBound = 30;

var arguments = algorithm.GetResultArguments(
    expression,
    countScoutBees,
    countBestPoints,
    countElitePoints,
    countEliteBees,
    countDefaultBees,
    delta, 
    countIterations,
    lowerBound,
    upperBound);

for (int i = 0; i < arguments.Length; i++)
{
    Console.WriteLine($"{parser.ArgumentName(i)} = {arguments[i]}");
}