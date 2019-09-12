using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Globalization;
using System;



public static class Futilities
{
    //public Dictionary<Char, int> charDict = null;
    
    public static int getAverageTokenLength(string input)
    {
        string[] tokens = input.Split();
        return input.Length / tokens.Length;
    }



    public static string reverse(string input)
    {
        string output = "";
        for (int i = input.Length - 1; i >= 0; i--)
        {
            output += input[i];
        }
        return output;
    }
    public static string CheckIfNumOutputString(string input)
    {
        IFormatProvider provider = CultureInfo.CreateSpecificCulture("en-US");
        float waste = 0;
        if (float.TryParse(input, NumberStyles.Any, provider, out waste))
        {
            return "Number";
        }
        return "Text";
    }
    public static string CountfNumOutputString(string input)
    {
        IFormatProvider provider = CultureInfo.CreateSpecificCulture("en-US");
        string[] strings = input.Split();
        int numStr = 0;
        int numNum = 0;
        float waste = 0;
        for (int i = 0; i < strings.Length; i++)
        {
            if (float.TryParse(strings[i], NumberStyles.Any, provider, out waste))
            {
                numNum++;
            }
            else
            {
                numStr++;
            }
        }
        return "" + numNum + " Numbers and " + numStr + " Strings.";
    }

    

    }