// See https://aka.ms/new-console-template for more information

using System.Text;
using RobotApp.Models;
Console.OutputEncoding = Encoding.UTF8;



Grille grille = new Grille(4, 4);

grille.InitGame();