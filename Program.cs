/*
Backend test
Si implementi nel linguaggio di propria scelta tra NodeJs e C#, uno script da eseguire da linea di comando.
Si possono usare librerie di terze parti a vostro piacimento.
Lo script riceve in input il percorso di un file csv da importare, un numero di colonna nel quale cercare, una chiave di ricerca.
Lo script dovrà essere così invocato (esempio in PHP)

$ php src/search.php ./file.csv 2 Alberto

dove ./file.csv è il percorso di un file csv così formattato:
1,Rossi,Fabio,01/06/1990;
2,Gialli,Alessandro,02/07/1989;
3,Verdi,Alberto,03/08/1987;

Il numero 2 rappresenta l'indice di colonna in cui cercare (nel file precedente il nome)
Alberto rappresenta la chiave di ricerca.

L'output del comando deve essere la linea corrispondente, nel nostro caso:
3,Verdi,Alberto,03/08/1987;

Author: Marco Orla, Backend System Integrator, ITS ICT
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;

internal class Program
{
    private static void Main(string[] args)
    {
        const char separatorSearch = ','; //the separator we need to search to split a line
        const char separatorPrint = ',';  //the separator that will be printedbetween elements for printing
        const char lastChar = ';';        //the character that will be printed as the last character of the line

        string filePath = args[0];      //retrieving the file path from cmd
        int col = int.Parse(args[1]);   //retrieving the column number from cmd
        string name = args[2];          //retrieving the name from cmd

        int colN = col - 1;  //the given column number, in which we need to search, adjusted to work in an array with index 0

        //retrieving the csv content and putting it into a jagged array
        string[][] people = File.ReadLines(filePath)    //reading the file
                .Where(line => line != "")              //ignoring empty lines
                .Select(x => x.Split(separatorSearch))  //splitting the lines element divided by a comma
                .ToArray();                             //converting into an array

        //searching for the person in the given column number
        for (int row = 0; row < people.GetLength(0); row++)         //running through every row
        {
            for (int cols = 0; cols < people[row].Length; cols++)   //running through every column
            {
                //searching in every given column if the name corresponds as the given one
                //to make it more accessible, the name research is not case sensitive
                if (people[row][colN].ToLower() == name.ToLower())
                {
                    if(cols == 0)   //control to print the row number only once, at the beginning of the line
                    {
                        //printing the row number
                        Console.Write(row + 1 );
                        Console.Write(separatorPrint);
                    }

                    if (cols < people[row].Length - 1) //searching for the elements that should be separated with the right character
                    {
                        //printing the lines that corresponds to the given parameters
                        Console.Write(people[row][cols]);
                        Console.Write(separatorPrint);
                    }
                    else    //this is the final element, so it will be followed by a given character
                    {
                        //printing the final lines that corresponds to the given parameters
                        Console.Write(people[row][cols]);
                        Console.Write(lastChar);
                    }
                }
            }
        }
    }
}