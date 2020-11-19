using System.Collections.Concurrent;
using System.Diagnostics;
using System.Data;
using System.Threading;
using System.Text;
using System.Security.Cryptography;
using System;
using System.Linq;
using AdventOfCode.Common;
using System.Threading.Tasks;

namespace AdventOfCode.Year2015
{
    // https://adventofcode.com/2015/day/4
    /*****************************************************************************************************

    --- Day 4: The Ideal Stocking Stuffer ---

    Santa needs help mining some AdventCoins (very similar to bitcoins) to use as gifts for all the economically forward-thinking little girls and boys.

    To do this, he needs to find MD5 hashes which, in hexadecimal, start with at least five zeroes. The input to the MD5 hash is some secret key (your puzzle input, given below) followed by a number in decimal. To mine AdventCoins, you must find Santa the lowest positive number (no leading zeroes: 1, 2, 3, ...) that produces such a hash.

    For example:

        If your secret key is abcdef, the answer is 609043, because the MD5 hash of abcdef609043 starts with five zeroes (000001dbbfa...), and it is the lowest such number to do so.
        If your secret key is pqrstuv, the lowest number it combines with to make an MD5 hash starting with five zeroes is 1048970; that is, the MD5 hash of pqrstuv1048970 looks like 000006136ef....

    Your puzzle answer was 117946.

    The first half of this puzzle is complete! It provides one gold star: *
    --- Part Two ---

    Now find one that starts with six zeroes.

    Your puzzle answer was 3938038.

    Both parts of this puzzle are complete! They provide two gold stars: **

    Your puzzle input is still ckczppom.

    *****************************************************************************************************/

    public class Day04 : IChallenge
    {
        public string Input => "ckczppom";

        public string SolvePartOne()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            string result = string.Empty;

            using (var md5 = new MD5CryptoServiceProvider())
            {
                for (int i = 0; i < int.MaxValue; i += 1)
                {
                    var inputWithSuffix = Input + i;
                    var md5Bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(inputWithSuffix));
                    var md5String = string.Join("", md5Bytes.Take(3).Select(_ => _.ToString("x2")));
                    var allZeroes = md5String.Take(5).All(_ => '0'.Equals(_));
                    if (allZeroes)
                    {
                        result = i.ToString();
                        break;
                    }
                }
            }
            //Fastest by just a fraction 0,1335241 seconds


            // var md5Array = new MD5CryptoServiceProvider[Environment.ProcessorCount];
            // for (int i = 0; i < Environment.ProcessorCount; i++)
            // {
            //     md5Array[i] = new MD5CryptoServiceProvider();
            // }
            //
            // for (int i = 0; i < int.MaxValue; i += Environment.ProcessorCount)
            // {
            //     var forResult = Parallel.For(i, i + Environment.ProcessorCount, (index, loopState) =>
            //     {
            //         var inputWithSuffix = Input + index;
            //         var md5Bytes = md5Array[index % Environment.ProcessorCount].ComputeHash(Encoding.UTF8.GetBytes(inputWithSuffix));
            //         var md5String = string.Join("", md5Bytes.Take(3).Select(_ => _.ToString("x2")));
            //         var allZeroes = md5String.Take(5).All(_ => '0'.Equals(_));
            //         if (allZeroes)
            //         {
            //             loopState.Break();
            //             return;
            //         }
            //     });
            //
            //     if (!forResult.IsCompleted)
            //     {
            //         result = forResult.LowestBreakIteration.ToString();
            //         break;
            //     }
            // }
            //
            // for (int i = 0; i < Environment.ProcessorCount; i++)
            // {
            //     md5Array[i].Dispose();
            // }
            // Almost there 0,1473921 seconds



            // var md5Array = new MD5CryptoServiceProvider[Environment.ProcessorCount];
            // for (int i = 0; i < Environment.ProcessorCount; i++)
            // {
            //     md5Array[i] = new MD5CryptoServiceProvider();
            // }
            //
            // var md5Concurrent = new ConcurrentQueue<MD5CryptoServiceProvider>(md5Array);
            // var forResult2 = Parallel.For(0, int.MaxValue, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, (index, loopState) =>
            // {
            //     var inputWithSuffix = Input + index;
            //     if (md5Concurrent.TryDequeue(out MD5CryptoServiceProvider md5))
            //     {
            //         var md5Bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(inputWithSuffix));
            //         md5Concurrent.Enqueue(md5);
            //         var md5String = string.Join("", md5Bytes.Take(3).Select(_ => _.ToString("x2")));
            //         var allZeroes = md5String.Take(5).All(_ => '0'.Equals(_));
            //         if (allZeroes)
            //         {
            //             loopState.Break();
            //             return;
            //         }
            //     }
            // });
            //
            // if (!forResult2.IsCompleted)
            //     result = forResult2.LowestBreakIteration.ToString();
            //
            // for (int i = 0; i < Environment.ProcessorCount; i++)
            // {
            //     md5Array[i].Dispose();
            // }
            // Too slow 0,3927178 seconds

            stopwatch.Stop();
            Console.WriteLine($"[StopWatch Part One] Time elapsed to calculate: '{stopwatch.Elapsed.TotalSeconds} seconds'.");
            return result;
        }

        public string SolvePartTwo()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            string result = string.Empty;

            // using (var md5 = new MD5CryptoServiceProvider())
            // {
            //     for (int i = 0; i < int.MaxValue; i += 1)
            //     {
            //         var inputWithSuffix = Input + i;
            //         var md5Bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(inputWithSuffix));
            //         var md5String = string.Join("", md5Bytes.Take(3).Select(_ => _.ToString("x2")));
            //         var allZeroes = md5String.All(_ => '0'.Equals(_));
            //         if (allZeroes)
            //         {
            //             result = i.ToString();
            //             break;
            //         }
            //     }
            // }
            // //Fastest 2,8970991 seconds

            // var md5Array = new MD5CryptoServiceProvider[Environment.ProcessorCount];
            // for (int i = 0; i < Environment.ProcessorCount; i++)
            // {
            //     md5Array[i] = new MD5CryptoServiceProvider();
            // }

            // for (int i = 0; i < int.MaxValue; i += Environment.ProcessorCount)
            // {
            //     var forResult = Parallel.For(i, i + Environment.ProcessorCount, (index, loopState) =>
            //     {
            //         var inputWithSuffix = Input + index;
            //         var md5Bytes = md5Array[index % Environment.ProcessorCount].ComputeHash(Encoding.UTF8.GetBytes(inputWithSuffix));
            //         var md5String = string.Join("", md5Bytes.Take(3).Select(_ => _.ToString("x2")));
            //         var allZeroes = md5String.All(_ => '0'.Equals(_));
            //         if (allZeroes)
            //         {
            //             loopState.Break();
            //             return;
            //         }
            //     });

            //     if (!forResult.IsCompleted)
            //     {
            //         result = forResult.LowestBreakIteration.ToString();
            //         break;
            //     }
            // }

            // for (int i = 0; i < Environment.ProcessorCount; i++)
            // {
            //     md5Array[i].Dispose();
            // }
            // // Not so fast 3,4635795 seconds

            // var md5Array = new MD5CryptoServiceProvider[Environment.ProcessorCount];
            // for (int i = 0; i < Environment.ProcessorCount; i++)
            // {
            //     md5Array[i] = new MD5CryptoServiceProvider();
            // }

            // var md5Concurrent = new ConcurrentQueue<MD5CryptoServiceProvider>(md5Array);
            // var forResult2 = Parallel.For(0, int.MaxValue, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, (index, loopState) =>
            // {
            //     var inputWithSuffix = Input + index;
            //     if (md5Concurrent.TryDequeue(out MD5CryptoServiceProvider md5))
            //     {
            //         var md5Bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(inputWithSuffix));
            //         md5Concurrent.Enqueue(md5);
            //         var md5String = string.Join("", md5Bytes.Take(3).Select(_ => _.ToString("x2")));
            //         var allZeroes = md5String.All(_ => '0'.Equals(_));
            //         if (allZeroes)
            //         {
            //             loopState.Break();
            //             return;
            //         }
            //     }
            // });

            // if (!forResult2.IsCompleted)
            //     result = forResult2.LowestBreakIteration.ToString();

            // for (int i = 0; i < Environment.ProcessorCount; i++)
            // {
            //     md5Array[i].Dispose();
            // }
            // // Too slow 8,6130016 seconds

            stopwatch.Stop();
            Console.WriteLine($"[StopWatch Part Two] Time elapsed to calculate: '{stopwatch.Elapsed.TotalSeconds} seconds'.");
            return result;
        }
    }
}
