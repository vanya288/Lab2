using System;
using System.Collections;

namespace Lab2
{
    public class Eratosthenes
    {
        public void PrintPrimeNumbers(int _max)
        {
            ArrayList list;
            bool[]    markedNumbers;

            markedNumbers = this.MarkPrimeNumbers(_max); 
            list          = this.GetMarkedNumberList(markedNumbers);

            foreach (int num in list)
            {
                Console.Write(num + " ");
            }
        }

        public bool[] MarkPrimeNumbers(int _max)
        {
            if (_max <= 1)
            {
                throw new ArgumentOutOfRangeException($"{nameof(_max)} must be more than 1.");
            }

            // Make an array indicating whether numbers are prime.
            bool[] is_prime = new bool[_max + 1];
            for (int i = 2; i <= _max; i++) is_prime[i] = true;

            // Cross out multiples.
            for (int i = 2; i <= _max; i++)
            {
                // See if i is prime.
                if (is_prime[i])
                {
                    // Knock out multiples of i.
                    for (int j = i * 2; j <= _max; j += i)
                        is_prime[j] = false;
                }
            }
            return is_prime;
        }
        
        public ArrayList GetMarkedNumberList(bool[] _markedNumbers)
        {
            ArrayList list;

            if (_markedNumbers.Length <= 2)
            {
                throw new ArgumentException($"{nameof(_markedNumbers)} length should be more than 2.");
            }

            list = new ArrayList();

            for (int i = 2; i <= _markedNumbers.Length - 1; i++)
            {
                if (_markedNumbers[i])
                {
                    list.Add(i);
                }
            }

            return list;
        }
    }
}
