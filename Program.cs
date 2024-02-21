using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnsiTools;
using Colors = AnsiTools.ANSICodes.Colors;
using HTTPUtils;
using System.Text.Json;




Console.Clear();
Console.WriteLine("Starting Assignment 2");

// SETUP 
const string myPersonalID = "816364a84adcdb874b77c85a6883abec71848e07d6abfc2fa4cedc64180bb37b"; // GET YOUR PERSONAL ID FROM THE ASSIGNMENT PAGE https://mm-203-module-2-server.onrender.com/
const string baseURL = "https://mm-203-module-2-server.onrender.com/";
const string startEndpoint = "start/"; // baseURl + startEndpoint + myPersonalID
const string taskEndpoint = "task/";   // baseURl + taskEndpoint + myPersonalID + "/" + taskID

// Creating a variable for the HttpUtils so that we dont have to type HttpUtils.instance every time we want to use it
HttpUtils httpUtils = HttpUtils.instance;

//#### REGISTRATION
// We start by registering and getting the first task
Response startRespons = await httpUtils.Get(baseURL + startEndpoint + myPersonalID);
Console.WriteLine($"Start:\n{Colors.Magenta}{startRespons}{ANSICodes.Reset}\n\n"); // Print the response from the server to the console
string taskID = "kuTw53L"; // We get the taskID from the previous response and use it to get the task (look at the console output to find the taskID)

//#### FIRST TASK 
// Fetch the details of the task from the server.
Response task1Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + taskID); // Get the task from the server
Task task1 = JsonSerializer.Deserialize<Task>(task1Response.content);
Console.WriteLine(task1Response);
Console.WriteLine($"TASK: {ANSICodes.Effects.Bold}{task1?.title}{ANSICodes.Reset}\n{task1?.description}\nParameters: {Colors.Yellow}{task1?.parameters}{ANSICodes.Reset}");

//calculate answer to task 1

 // Fetching parameters dynamically from the server
        Response taskResponse = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + taskID);
Task task = JsonSerializer.Deserialize<Task>(taskResponse.content);

        // Process the parameters and get the answer
        List<int> numbers = task.parameters.Split(',').Select(int.Parse).ToList();
List<int> primeNumbers = GetPrimeNumbers(numbers);
primeNumbers.Sort();

        // Display the result
        Console.WriteLine("Prime Numbers: " + string.Join(", ", primeNumbers));
    

    static List<int> GetPrimeNumbers(List<int> numbers)
    {
        // Function to check if a number is prime
        bool IsPrime(int num)
        {
            if (num < 2) return false;
            for (int i = 2; i <= Math.Sqrt(num); i++)
            {
                if (num % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        // Filter prime numbers from the list
        List<int> primeNumbers = numbers.Where(IsPrime).ToList();

        return primeNumbers;
    }
string primeNumbersString = string.Join(",", primeNumbers);
Response task1AnswerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + taskID, primeNumbersString);

Console.WriteLine($"Answer: {Colors.Green}{task1AnswerResponse}{primeNumbers}{ANSICodes.Reset}");


taskID = "aAaa23";
Console.WriteLine("\n-----------------------------------\n");


//#### SECOND TASK
// Fetch the details of the task from the server.
Response task2Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + taskID); // Get the task from the server
Task task2 = JsonSerializer.Deserialize<Task>(task2Response.content);
Console.WriteLine($"TASK: {ANSICodes.Effects.Bold}{task2?.title}{ANSICodes.Reset}\n{task2?.description}\nParameters: {Colors.Yellow}{task2?.parameters}{ANSICodes.Reset}");

// Calculate answer to task 2
double fahrenheitTemperature = double.Parse(task2.parameters);
double celsiusTemperature = FahrenheitToCelsius(fahrenheitTemperature);

// Display the result
Console.WriteLine($"Converted Temperature: {Colors.Green}{celsiusTemperature:F2}°C{ANSICodes.Reset}");

// Submit the answer to the server
Response task2AnswerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + taskID, celsiusTemperature.ToString("F2"));
Console.WriteLine($"Answer: {Colors.Green}{task2AnswerResponse}{ANSICodes.Reset}");

// Function to convert Fahrenheit to Celsius
static double FahrenheitToCelsius(double fahrenheit)
{
    return (fahrenheit - 32) * 5 / 9;
}



taskID = "psu31_4";
Console.WriteLine("\n-----------------------------------\n");

//#### THIRD TASK
// Fetch the details of the task from the server.
Response task3Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + taskID); // Get the task from the server
Task task3 = JsonSerializer.Deserialize<Task>(task3Response.content);
Console.WriteLine($"TASK: {ANSICodes.Effects.Bold}{task3?.title}{ANSICodes.Reset}\n{task3?.description}\nParameters: {Colors.Yellow}{task3?.parameters}{ANSICodes.Reset}");

// Process the parameters and get the answer
List<int> numbersForTask3 = task3.parameters.Split(',').Select(int.Parse).ToList();
int sumOfNumbers = numbersForTask3.Sum();

// Display the result
Console.WriteLine("Sum of Numbers: " + sumOfNumbers);

// Convert the sum to string and submit the answer to the server
string sumString = sumOfNumbers.ToString();
Response task3AnswerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + taskID, sumString);

Console.WriteLine($"Answer: {Colors.Green}{task3AnswerResponse}{sumString}{ANSICodes.Reset}");

taskID = "rEu25ZX";
Console.WriteLine("\n-----------------------------------\n");


//#### FOURTH TASK
// Fetch the details of the task from the server.
Response task4Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + taskID); // Get the task from the server
Task task4 = JsonSerializer.Deserialize<Task>(task4Response.content);
Console.WriteLine($"TASK: {ANSICodes.Effects.Bold}{task4?.title}{ANSICodes.Reset}\n{task4?.description}\nParameters: {Colors.Yellow}{task4?.parameters}{ANSICodes.Reset}");

// Process the parameters and get the answer
string romanNumeral = task4.parameters;
int integerResult = RomanToInteger(romanNumeral);

// Display the result
Console.WriteLine($"Integer Value of Roman Numeral '{romanNumeral}': {integerResult}");

// Convert the result to string and submit the answer to the server
string resultString = integerResult.ToString();
Response task4AnswerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + taskID, resultString);

Console.WriteLine($"Answer: {Colors.Green}{task4AnswerResponse}{resultString}{ANSICodes.Reset}");

// Function to convert Roman numeral to integer
static int RomanToInteger(string s)
{
    Dictionary<char, int> romanValues = new Dictionary<char, int>()
    {
        {'I', 1},
        {'V', 5},
        {'X', 10},
        {'L', 50},
        {'C', 100},
        {'D', 500},
        {'M', 1000}
    };

    int result = 0;
    int prevValue = 0;

    for (int i = s.Length - 1; i >= 0; i--)
    {
        int currentValue = romanValues[s[i]];

        if (currentValue < prevValue)
        {
            result -= currentValue;
        }
        else
        {
            result += currentValue;
        }

        prevValue = currentValue;
    }

    return result;
}

class Task
{
    public string? title { get; set; }
    public string? description { get; set; }
    public string? taskID { get; set; }
    public string? usierID { get; set; }
    public string? parameters { get; set; }
}