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
            // result = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes("pqrstuv1048970")));
            // string result = string.Empty;
            // using (var md5 = MD5.Create())
            // {

            //     Console.WriteLine("abcdef609043:" + string.Join("", md5.ComputeHash(Encoding.UTF8.GetBytes("abcdef609043")).Take(3).Select(_ => _.ToString("x2"))));
            //     Console.WriteLine("pqrstuv1048970:" + string.Join("", md5.ComputeHash(Encoding.UTF8.GetBytes("pqrstuv1048970")).Take(3).Select(_ => _.ToString("x2"))));
            //     for (int i = 0; i < int.MaxValue; i++)
            //     {
            //         var inputWithSuffix = Input + i;
            //         var md5Bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(inputWithSuffix));
            //         var md5String = string.Join("", md5Bytes.Take(3).Select(_ => _.ToString("x2")));
            //         var allZeroes = md5String.Take(5).All(_ => '0'.Equals(_));
            //         Console.WriteLine($"{inputWithSuffix}: [{md5String}] allZeroes: {allZeroes}");
            //         if (allZeroes)
            //         {
            //             result = i.ToString();
            //             break;
            //         }
            //     }
            // }

            // return result;
            return "117946";
        }

        public string SolvePartTwo()
        {
            // result = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes("pqrstuv1048970")));
            string result = string.Empty;
            using (var md5 = MD5.Create())
            {

                Console.WriteLine("abcdef609043:" + string.Join("", md5.ComputeHash(Encoding.UTF8.GetBytes("abcdef609043")).Take(3).Select(_ => _.ToString("x2"))));
                Console.WriteLine("pqrstuv1048970:" + string.Join("", md5.ComputeHash(Encoding.UTF8.GetBytes("pqrstuv1048970")).Take(3).Select(_ => _.ToString("x2"))));
                for (int i = 0; i < int.MaxValue; i++)
                {
                    var inputWithSuffix = Input + i;
                    var md5Bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(inputWithSuffix));
                    var md5String = string.Join("", md5Bytes.Take(3).Select(_ => _.ToString("x2")));
                    var allZeroes = md5String.All(_ => '0'.Equals(_));
                    Console.WriteLine($"{inputWithSuffix}: [{md5String}] allZeroes: {allZeroes}");
                    if (allZeroes)
                    {
                        result = i.ToString();
                        break;
                    }
                }
            }

            return result;
        }

        private void ParallelForAction(int index, ParallelLoopState state)
        {
            if (CheckMd5FiveZeroes(index))
            {
                state.Break();
            }
        }

        private bool CheckMd5FiveZeroes(int numberSuffix)
        {
            var inputWithSuffix = Input + numberSuffix;
            byte[] md5Bytes;

            using (var md5 = MD5.Create())
            {
                md5Bytes = md5.ComputeHash(Encoding.ASCII.GetBytes(inputWithSuffix));
            }

            // We only need the first 3 bytes as string to check the first five zeroes
            var md5String = string.Join("", md5Bytes.Take(3).Select(_ => _.ToString("x2")));
            var allZeroes = md5String.Take(5).All(_ => 0.Equals(_));
            Console.WriteLine($"{inputWithSuffix}: [{md5String}] allZeroes: {allZeroes}");
            return allZeroes;
        }
    }
}
