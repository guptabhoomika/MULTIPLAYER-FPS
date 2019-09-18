using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataTranslator : MonoBehaviour
{
   private static string Kill_Sym ="[KILLS]";
   private static string Death_Sym = "[DEATHS]";
    
    public static string ValuesToData(int kills)
    {
        return Kill_Sym + kills;
    }
    public  static int DataToKills(string data)
    {
        return int.Parse(DataToValue(data, Kill_Sym));
    }
    

    public static int DataToDeaths(string data)
    {
         
        return int.Parse(DataToValue(data, Death_Sym));
    }



    public static string DataToValue(string data , string symbol)
    {
        string[] pieces = data.Split('/');
        foreach (string piece in pieces)
        {
            if (piece.StartsWith(symbol)) ;
            {
               return piece.Substring(symbol.Length);

            }
        }

        Debug.LogError(symbol + "not found");
        return "";

    }
}
