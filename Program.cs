using System.Collections.Generic;
using System.Reflection;
using System;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var executingAsm = Assembly.GetExecutingAssembly();
            var challengeInterface = typeof(IChallenge);
            var allTypes = executingAsm.GetTypes();
            var allChallenges = allTypes.Where(_ => _.IsPublic && !_.IsAbstract && _.IsClass && challengeInterface.IsAssignableFrom(_))
                                        .ToArray();
            var yearChallengesDict = new Dictionary<string, List<Type>>();

            foreach (var type in allChallenges)
            {
                var year = type.Namespace?.Split('.').Last();

                if (string.IsNullOrWhiteSpace(year))
                    continue;

                if (!yearChallengesDict.ContainsKey(year))
                {
                    yearChallengesDict.Add(year, new List<Type>());
                }

                yearChallengesDict[year].Add(type);
            }

            Console.WriteLine("Choose one of the following challenges [Year].[Day]:");
            foreach (var (year, types) in yearChallengesDict)
            {
                Console.WriteLine($" + {year}");
                foreach (var type in types)
                {
                    Console.WriteLine($" |- {type.Name}");
                }
            }

            string userInput;
            if (args.Length > 0)
            {
                userInput = args[0];
            }
            else
            {
                userInput = Console.ReadLine();
            }

            if (string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("Error: input empty");
                return;
            }

            var yearDayPair = userInput.Split('.');
            if (yearDayPair.Length == 2)
            {
                if (!yearChallengesDict.ContainsKey(yearDayPair[0]))
                {
                    Console.WriteLine("Error: year not found");
                    return;
                }

                var dayType = yearChallengesDict[yearDayPair[0]].FirstOrDefault(_ => _.Name.Equals(yearDayPair[1]));
                if (dayType == null)
                {
                    Console.WriteLine("Error: day not found");
                    return;
                }

                var challenge = (IChallenge)Activator.CreateInstance(dayType);
                Console.WriteLine($"Result ({yearDayPair[0]}=>{yearDayPair[1]}): PartOne = '{challenge.SolvePartOne()}' PartTwo = '{challenge.SolvePartTwo()}'");
            }
        }
    }
}
