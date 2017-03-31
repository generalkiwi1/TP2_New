using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP2PROF;
namespace TP2Tests
{
  /// <summary>
  /// Description résumée pour TestsPathFinder
  /// </summary>
  [TestClass]
  public class TestsPathFinder
  {
    const string VALID_LEVEL_01 = @"
1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1;
1,1,0,4,4,4,4,4,4,4,1,4,4,4,4,4,4,4,4,1,1;
1,1,5,1,1,4,1,1,1,4,1,4,1,1,1,4,1,1,5,1,1;
1,1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1;
1,1,4,1,1,4,1,4,1,1,1,1,1,4,1,4,1,1,4,1,1;
1,1,4,4,4,4,1,4,4,4,1,4,4,4,1,4,4,4,4,1,1;
1,1,1,1,1,4,1,1,1,4,1,4,1,1,1,4,1,1,1,1,1;
1,1,1,1,1,4,1,4,4,4,4,4,4,4,1,4,1,1,1,1,1;
1,1,1,1,1,4,1,4,1,1,2,1,1,4,1,4,1,1,1,1,1;
1,1,1,4,4,4,4,4,1,2,2,2,1,4,4,4,4,4,1,1,1;
1,1,1,1,1,4,1,4,1,1,6,1,1,4,1,4,1,1,1,1,1;
1,1,1,1,1,4,1,4,1,1,1,1,1,4,1,4,1,1,1,1,1;
1,1,1,1,1,4,1,4,4,4,4,4,4,4,1,4,1,1,1,1,1;
1,1,1,1,1,4,1,4,1,1,1,1,1,4,1,4,1,1,1,1,1;
1,1,4,4,4,4,4,4,4,4,1,4,4,4,4,4,4,4,4,1,1;
1,1,4,1,1,4,1,1,1,4,1,4,1,1,1,4,1,1,4,1,1;
1,1,4,4,1,4,4,4,4,4,3,4,4,4,4,4,1,4,4,1,1;
1,1,1,4,1,4,1,4,1,1,1,1,1,4,1,4,1,4,1,1,1;
1,1,4,4,4,4,1,4,4,4,1,4,4,4,1,4,4,4,4,1,1;
1,1,5,1,1,1,1,1,1,4,1,4,1,1,1,1,1,1,5,1,1;
1,1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1;
1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1";
    #region MANDAT1


    /// <summary>
    /// Test de l'initialisation des coûts.
    /// Vous devez vous assurer que la méthode InitCost initialise
    /// les valeurs du tableau à +infini partout sauf à l'endroit de départ
    /// (initialisation à 0)
    /// </summary>
    //<AntoineRL>
    [TestMethod]
    public void TestInitCost_01()
    {
      // Mise en place des données      
      Grid aGrid = new Grid();
      int fromX = 4;
      int fromY = 4;
      int[,] tabJeu = new int[aGrid.Height, aGrid.Width];
      // Appel de la méthode à tester
      tabJeu = PathFinder.InitCosts(aGrid, fromX, fromY);
      // Validations
      for (int i = 0; i < tabJeu.GetLength(0); i++)
      {
        for (int j = 0; j < tabJeu.GetLength(1); j++)
        {
          if (j == fromX && i == fromY)
          {
            Assert.AreEqual(0, tabJeu[i, j]);
          }
          else
          {
            Assert.AreEqual(int.MaxValue, tabJeu[i, j]);
          }
        }
      }

    }

    /// <summary>
    /// Test de calcul des coûts dans la grille de base.
    /// Vous devez vous assurer que le calcul des coûts se
    /// fait correctement. Pour cela, faites l'appel à la méthode
    /// InitCosts puis ComputeCosts et faites quelques validations
    /// pour différents scénarios: chemins existants, chemins 
    /// inexistants (ex. à partie ou dans un mur!)
    /// </summary>
    [TestMethod]
    public void TestComputeCost_01()
    {
      // Mise en place des données
      Grid aGrid = new Grid();
      int fromX = 2;
      int fromY = 4;
      int toX = 3;
      int toY = 6;
      int[,] costs = new int[aGrid.Height, aGrid.Width];
      // Appel de la méthode à tester
      aGrid.LoadFromMemory(VALID_LEVEL_01);
      costs = PathFinder.InitCosts(aGrid, fromX, fromY);
      PathFinder.ComputeCosts(aGrid, fromX, fromY, toX, toY, costs);

      // Validations
      // Chemins existants
      if (fromX - 1 > 0)
      {
        if (aGrid.GetGridElementAt(fromX - 1 , fromY) != PacmanElement.Wall)
        {
          if (costs[fromX , fromY] + 1 < costs[fromX - 1 ,  fromY])
          {
            Assert.AreEqual((costs[fromX , fromY]) + 1, costs[fromX - 1 , fromY]);
          }
        }
      }
      if (fromY - 1 > 0)
      {
        if (aGrid.GetGridElementAt(fromX,  fromY - 1) != PacmanElement.Wall)
        {
          if (costs[fromX , fromY] + 1 < costs[fromX , fromY - 1])
          {
            Assert.AreEqual((costs[fromX , fromY]) + 1, costs[fromX , fromY - 1]);
          }
        }
      }
      if (fromX + 1 < aGrid.Width)
      {
        if (aGrid.GetGridElementAt(fromX + 1 , fromY) != PacmanElement.Wall)
        {
          if (costs[fromX , fromY] + 1 < costs[fromX + 1, fromY ])
          {
            Assert.AreEqual((costs[fromX , fromY]) + 1, costs[fromX + 1 , fromY]);
          }
        }
      }
      if (fromY + 1 < aGrid.Height)
      {
        if (aGrid.GetGridElementAt(fromX, fromY + 1) != PacmanElement.Wall)
        {
          if (costs[fromX , fromY] + 1 < costs[fromX, fromY + 1])
          {
            Assert.AreEqual((costs[fromX , fromY]) + 1, costs[fromX, fromY + 1 ]);
          }
        }
      }
      // Chemins inexistants
      if (costs[fromX , fromY] + 1 > costs[fromX - 1 , fromY])
      {
        Assert.AreEqual(int.MaxValue, costs[fromX - 1 , fromY]);
      }
      if (costs[fromX , fromY] + 1 > costs[fromX , fromY - 1])
      {
        Assert.AreEqual(int.MaxValue, costs[fromX , fromY - 1]);
      }
      if (costs[fromX , fromY] + 1 > costs[fromX + 1 , fromY ])
      {
        Assert.AreEqual(int.MaxValue, costs[fromX + 1 , fromY]);
      }
      if (costs[fromX , fromY] + 1 > costs[fromX , fromY + 1])
      {
        Assert.AreEqual(int.MaxValue, costs[fromX , fromY + 1]);
      }

        ;
    }
    

    /// <summary>
    /// Test de calcul d'une direction lorsque le point de départ
    /// est le même que le point d'arrivée.
    /// </summary>
    [TestMethod]
    public void TestFindPath_NoDisplacement()
    {
      // Mise en place des données
      Grid aGrid = new Grid();
      int fromX = 2;
      int fromY = 5;
      int toX = 2;
      int toY = 5;
      int[,] costs = new int[aGrid.Height, aGrid.Width];
      // Appel de la méthode à tester
      aGrid.LoadFromMemory(VALID_LEVEL_01);
      costs = PathFinder.InitCosts(aGrid, fromX, fromY);
      PathFinder.ComputeCosts(aGrid, fromX, fromY, toX, toY, costs);

      // Validations
      for(int i=0; i<costs.GetLength(0);i++)
      {
        for(int j=0; j<costs.GetLength(1);j++)
        {
         if(i==fromX && j==fromY)
         {
            Assert.AreEqual(0, costs[fromX, fromY]);
         }
         else
         {
            Assert.AreEqual(int.MaxValue, costs[i, j]);
         }
        } 
      }
      // Cleanup
    }

    /// <summary>
    /// Test de calcul d'une direction lorsque le point de départ
    /// juste à gauche du point d'arrivée.
    /// </summary>
    [TestMethod]
    public void TestFindPath_ToEast()
    {
      // Mise en place des données
      Grid aGrid = new Grid();
      int fromX = 1;
      int fromY = 3;
      int toX = 1;
      int toY = 4;
      int[,] costs = new int[aGrid.Height, aGrid.Width];
      // Appel de la méthode à tester
      aGrid.LoadFromMemory(VALID_LEVEL_01);
      costs = PathFinder.InitCosts(aGrid, fromX, fromY);
      PathFinder.ComputeCosts(aGrid, fromX, fromY, toX, toY, costs);

      // Validations
      if (costs[fromX, fromY] + 1 < costs[toX, toY])
      {
        Assert.AreEqual(costs[fromX, fromY] + 1, costs[toX, toY]);
      }
      // Cleanup      

    }

    /// <summary>
    /// Test de calcul d'une direction lorsque le point de départ
    /// juste à droite du point d'arrivée.
    /// </summary>
    [TestMethod]
    public void TestFindPath_ToWest()
    {
      // Mise en place des données
      Grid aGrid = new Grid();
      int fromX = 1;
      int fromY = 4;
      int toX = 1;
      int toY = 3;
      int[,] costs = new int[aGrid.Height, aGrid.Width];
      // Appel de la méthode à tester
      aGrid.LoadFromMemory(VALID_LEVEL_01);
      costs = PathFinder.InitCosts(aGrid, fromX, fromY);
      PathFinder.ComputeCosts(aGrid, fromX, fromY, toX, toY, costs);
      // Validations
      if (costs[fromX, fromY] + 1 < costs[toX, toY])
      {
        Assert.AreEqual(costs[fromX, fromY] + 1, costs[toX, toY]);
      }
      // Cleanup
    }

    /// <summary>
    /// Test de calcul d'une direction lorsque le point de départ
    /// est juste en dessous du point d'arrivée.
    /// </summary>
    [TestMethod]
    public void TestFindPath_ToNorth()
    {
      // Mise en place des données
      Grid aGrid = new Grid();
      int fromX = 2;
      int fromY = 5;
      int toX = 1;
      int toY = 5;
      int[,] costs = new int[aGrid.Height, aGrid.Width];

      // Appel de la méthode à tester
      aGrid.LoadFromMemory(VALID_LEVEL_01);
      costs = PathFinder.InitCosts(aGrid, fromX, fromY);
      PathFinder.ComputeCosts(aGrid, fromX, fromY, toX, toY, costs);

      // Validations
      if (costs[fromX, fromY] + 1 < costs[toX, toY])
      {
        Assert.AreEqual(costs[fromX, fromY] + 1, costs[toX, toY]);
      }
      // Cleanup
    }
    /// <summary>
    /// Test de calcul d'une direction lorsque le point de départ
    /// est juste au dessus du point d'arrivée.
    /// </summary>
    [TestMethod]
    public void TestFindPath_ToSouth()
    {
      // Mise en place des données
      Grid aGrid = new Grid();
      int fromX = 1;
      int fromY = 5;
      int toX = 2;
      int toY = 5;
      int[,] costs = new int[aGrid.Height, aGrid.Width];

      // Appel de la méthode à tester
      aGrid.LoadFromMemory(VALID_LEVEL_01);
      costs = PathFinder.InitCosts(aGrid, fromX, fromY);
      PathFinder.ComputeCosts(aGrid, fromX, fromY, toX, toY, costs);

      // Validations
      if (costs[fromX, fromY] + 1 < costs[toX, toY])
      {
        Assert.AreEqual(costs[fromX, fromY] + 1, costs[toX, toY]);
      }
      // Cleanup
    }
    

    
    /// <summary>
    /// Test de calcul d'une direction impossible (vers un mur).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ImpossibleToWall()
    {
      // Mise en place des données
      Grid aGrid = new Grid();
      int fromX = 1;
      int fromY = 4;
      int toX = 2;
      int toY = 4;
      int[,] costs = new int[aGrid.Height, aGrid.Width];

      // Appel de la méthode à tester
      aGrid.LoadFromMemory(VALID_LEVEL_01);
      costs = PathFinder.InitCosts(aGrid, fromX, fromY);
      PathFinder.ComputeCosts(aGrid, fromX, fromY, toX, toY, costs);

      // Validations
      Assert.AreEqual(int.MaxValue, costs[toX, toY]);
      // Cleanup
    }

    /// <summary>
    /// Test de calcul d'une direction impossible (à partie d'un mur).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ImpossibleFromWall()
    {      // Mise en place des données
      Grid aGrid = new Grid();
      int fromX = 2;
      int fromY = 4;
      int toX = 1;
      int toY = 4;
      int[,] costs = new int[aGrid.Height, aGrid.Width];

      // Appel de la méthode à tester
      aGrid.LoadFromMemory(VALID_LEVEL_01);
      costs = PathFinder.InitCosts(aGrid, fromX, fromY);
      PathFinder.ComputeCosts(aGrid, fromX, fromY, toX, toY, costs);

      // Validations
      Assert.AreEqual(0, costs[fromX, fromY]);
      // Cleanup
    }
    //</AntoineRL>
    #endregion
    #region MANDAT2
    int[,] simpleCostArray1 = new int[,]{
      {int.MaxValue,  int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue },
      {int.MaxValue,  int.MaxValue, 7,            6,            7,            8,            int.MaxValue },
      {int.MaxValue,  3,            int.MaxValue, 5,            int.MaxValue, 9,            int.MaxValue },
      {int.MaxValue,  2,            int.MaxValue, 4,            int.MaxValue, int.MaxValue, int.MaxValue },
      {int.MaxValue,  1,            2,            3,            int.MaxValue, 7,            int.MaxValue },
      {int.MaxValue,  0,            int.MaxValue, 4,            int.MaxValue, 6,            int.MaxValue },
      {int.MaxValue,  1,            2,            3,            4,            5,            int.MaxValue },
      {int.MaxValue,  int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue }
    };
    /// <summary>
    /// Test de calcul du premier déplacement vers le nord.
    /// Vous devez aller vers la haut (ex. (x=1, y=4)).  La direction
    /// retournée par PathFinder.RecurseFindDirection devrait
    /// être le "nord".
    /// Utilisez le tableau simpleCostArray1 comme tableau des coûts.
    /// </summary>
    [TestMethod]
    public void TestRecurseFindDirection_North01()
    {
      // Mise en place des données      
      Grid grid = new Grid();
      grid.LoadFromMemory(VALID_LEVEL_01);
      // Appel de la méthode à tester
      Direction testNorth = PathFinder.RecurseFindDirection(simpleCostArray1, 1, 3, 2, 3, grid);
      // Validation des résultats
      Assert.AreEqual(Direction.North, testNorth);
      // Clean-up
    }
    /// <summary>
    /// Test de calcul du second déplacement vers le nord.
    /// Vous devez choisir une cible "vers le nord" plus complexe que celle juste 
    /// au-dessus.  La direction retournée par PathFinder.RecurseFindDirection 
    /// devrait être le "nord".
    /// Utilisez le tableau simpleCostArray1 comme tableau des coûts.
    /// </summary>
    [TestMethod]
    public void TestRecurseFindDirection_North02()
    {
      // Mise en place des données      
      Grid grid = new Grid();
      grid.LoadFromMemory(VALID_LEVEL_01);
      // Appel de la méthode à tester
      Direction testNorth = PathFinder.RecurseFindDirection(simpleCostArray1, 2, 5, 2, 3, grid);
      // Validation des résultats
      Assert.AreEqual(Direction.North, testNorth);
      // Clean-up
    }

    /// <summary>
    /// Test de calcul du troisième déplacement vers le nord
    /// Vous devez choisir une cible "vers le nord" plus complexe que celle juste 
    /// au-dessus et autre que pour le test précédent.  La direction 
    /// retournée par PathFinder.RecurseFindDirection devrait être le "nord".
    /// Utilisez le tableau simpleCostArray1 comme tableau des coûts.
    /// </summary>
    [TestMethod]
    public void TestRecurseFindDirection_North03()
    {
      // Mise en place des données      
      Grid grid = new Grid();
      grid.LoadFromMemory(VALID_LEVEL_01);
      // Appel de la méthode à tester
      Direction testNorth = PathFinder.RecurseFindDirection(simpleCostArray1, 2, 5, 5, 1, grid);
      // Validation des résultats
      Assert.AreEqual(Direction.North, testNorth);
      // Clean-up
    }
    /// <summary>
    /// Test de calcul du premier déplacement vers le sud
    /// Vous devez choisir une cible vers la bas (ex. (x=1, y=6)).  La direction
    /// retournée par PathFinder.RecurseFindDirection devrait
    /// être le "sud".
    /// Utilisez le tableau simpleCostArray1 comme tableau des coûts.
    /// </summary>
    [TestMethod]
    public void TestRecurseFindDirection_South01()
    {
      // Mise en place des données      
      Grid grid = new Grid();
      grid.LoadFromMemory(VALID_LEVEL_01);
      // Appel de la méthode à tester
      Direction testSouth = PathFinder.RecurseFindDirection(simpleCostArray1, 6, 1, 5, 1, grid);
      // Validation des résultats
      Assert.AreEqual(Direction.South, testSouth);
      // Clean-up
    }

    /// <summary>
    /// Test de calcul du second déplacement vers le sud
    /// Vous devez choisir une cible "vers le bas" plus complexe que celle juste 
    /// en-dessous.  La direction retournée par PathFinder.RecurseFindDirection 
    /// devrait être le "sud".
    /// Utilisez le tableau simpleCostArray1 comme tableau des coûts.    
    /// </summary>
    [TestMethod]
    public void TestRecurseFindDirection_South02()
    {
      // Mise en place des données      
      Grid grid = new Grid();
      grid.LoadFromMemory(VALID_LEVEL_01);
      // Appel de la méthode à tester
      Direction testSouth = PathFinder.RecurseFindDirection(simpleCostArray1, 4, 5, 5, 1, grid);
      // Validation des résultats
      Assert.AreEqual(Direction.South, testSouth);
      // Clean-up
    }

    /// <summary>
    /// Test de calcul du troisième déplacement vers le sud
    /// Vous devez choisir une cible "vers le bas" plus complexe que celle juste 
    /// en-dessous et autre que pour le test précédent.  La direction 
    /// retournée par PathFinder.RecurseFindDirection devrait être le "sud".
    /// Utilisez le tableau simpleCostArray1 comme tableau des coûts.
    /// </summary>
    [TestMethod]
    public void TestRecurseFindDirection_South03()
    {
      // Mise en place des données      
      Grid grid = new Grid();
      grid.LoadFromMemory(VALID_LEVEL_01);
      // Appel de la méthode à tester
      Direction testSouth = PathFinder.RecurseFindDirection(simpleCostArray1, 6, 1, 5, 1, grid);
      // Validation des résultats
      Assert.AreEqual(Direction.South, testSouth);
      // Clean-up
    }

    int[,] simpleCostArray2 = new int[,]{
      {int.MaxValue,  int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue,  int.MaxValue, int.MaxValue },
      {int.MaxValue,  int.MaxValue, int.MaxValue, 7,            int.MaxValue, 13,           12,            11,           int.MaxValue },
      {int.MaxValue,  8,            int.MaxValue, 6,            int.MaxValue, 14,           int.MaxValue,  10,           int.MaxValue },
      {int.MaxValue,  7,            int.MaxValue, 5,            int.MaxValue, 15,           int.MaxValue,  9,            int.MaxValue },
      {int.MaxValue,  6,            5,            4,            int.MaxValue, 16,           int.MaxValue,  8,            int.MaxValue },
      {int.MaxValue,  5,            4,            3,            int.MaxValue, 17,           int.MaxValue,  7,            int.MaxValue },
      {int.MaxValue,  4,            int.MaxValue, 2,            int.MaxValue, 18,           int.MaxValue,  6,            int.MaxValue },
      {int.MaxValue,  3,            int.MaxValue, 1,            int.MaxValue, int.MaxValue, int.MaxValue,  5,            int.MaxValue },
      {int.MaxValue,  2,            1,            0,            1,            2,            3,             4,            int.MaxValue },
      {int.MaxValue,  int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue,  int.MaxValue, int.MaxValue }
    };
    /// <summary>
    /// Test de calcul du premier déplacement vers l'ouest
    /// Vous devez aller vers la gauche (ex. (x=1, y=6)).  La direction
    /// retournée par PathFinder.RecurseFindDirection devrait
    /// être l'"ouest".
    /// Utilisez le tableau simpleCostArray2 comme tableau des coûts.
    /// </summary>
    [TestMethod]
    public void TestRecurseFindDirection_West01()
    {
      // Mise en place des données      
      Grid grid = new Grid();
      grid.LoadFromMemory(VALID_LEVEL_01);
      // Appel de la méthode à tester
      Direction testWest = PathFinder.RecurseFindDirection(simpleCostArray2, 8, 2, 8, 3, grid);
      // Validation des résultats
      Assert.AreEqual(Direction.West, testWest);
      // Clean-up
    }
    /// <summary>
    /// Test de calcul du premier déplacement vers l'est
    /// Vous devez aller vers la droite (ex. (x=5, y=6)).  La direction
    /// retournée par PathFinder.RecurseFindDirection devrait
    /// être l'"est".
    /// Utilisez le tableau simpleCostArray2 comme tableau des coûts.
    /// </summary>
    [TestMethod]
    public void TestRecurseFindDirection_East01()
    {
      // Mise en place des données      
      Grid grid = new Grid();
      grid.LoadFromMemory(VALID_LEVEL_01);
      // Appel de la méthode à tester
      Direction testEst = PathFinder.RecurseFindDirection(simpleCostArray2, 8, 4, 8, 3, grid);
      // Validation des résultats
      Assert.AreEqual(Direction.East, testEst);
      // Clean-up
    }

    // INVERSEMENT DES X ET Y

    /// <summary>
    /// Test de calcul d'une direction vers le nord à partir 
    /// du bas de la grille à gauche (x=2,y=20) vers le haut à gauche(x=2,y=2).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ComplexToNorth01()
    {
      // Mise en place des données      
      Grid grid = new Grid();
      grid.LoadFromMemory(VALID_LEVEL_01);
      int ghostX = 20;
      int ghostY = 2;
      int pacmanX = 2;
      int pacmanY = 2;
      // Appel de la méthode à tester
      Direction testNorth = PathFinder.FindShortestPath(grid, ghostX, ghostY, pacmanX, pacmanY);
      // Validation des résultats
      Assert.AreEqual(Direction.North, testNorth);
      // Clean-up
    }
    /// <summary>
    /// Test de calcul d'une direction vers le nord à partir 
    /// du bas de la grille à gauche(x=2,y=20) vers le haut au centre(x=11,y=2).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ComplexToNorth02()
    {
      // Mise en place des données      
      Grid grid = new Grid();
      grid.LoadFromMemory(VALID_LEVEL_01);
      int ghostX = 20;
      int ghostY = 2;
      int pacmanX = 2;
      int pacmanY = 11;
      // Appel de la méthode à tester
      Direction testNorth = PathFinder.FindShortestPath(grid, ghostX, ghostY, pacmanX, pacmanY);
      // Validation des résultats
      Assert.AreEqual(Direction.North, testNorth);
      // Clean-up
    }
    /// <summary>
    /// Test de calcul d'une direction vers le nord à partir 
    /// du bas de la grille à gauche (x=2,y=20) vers le haut à droite (x=18,y=2).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ComplexToNorth03()
    {
      // Mise en place des données      
      Grid grid = new Grid();
      grid.LoadFromMemory(VALID_LEVEL_01);
      int ghostX = 20;
      int ghostY = 2;
      int pacmanX = 2;
      int pacmanY = 18;
      // Appel de la méthode à tester
      Direction testNorth = PathFinder.FindShortestPath(grid, ghostX, ghostY, pacmanX, pacmanY);
      // Validation des résultats
      Assert.AreEqual(Direction.North, testNorth);
      // Clean-up
    }

    /// <summary>
    /// Test de calcul d'une direction vers le sud à partir 
    /// du haut de la grille à gauche (x=2,y=2) vers le bas à gauche (x=2,y=20).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ComplexToSouth01()
    {
      // Mise en place des données      
      Grid grid = new Grid();
      grid.LoadFromMemory(VALID_LEVEL_01);
      int ghostX = 2;
      int ghostY = 2;
      int pacmanX = 20;
      int pacmanY = 2;
      // Appel de la méthode à tester
      Direction testSouth = PathFinder.FindShortestPath(grid, ghostX, ghostY, pacmanX, pacmanY);
      // Validation des résultats
      Assert.AreEqual(Direction.South, testSouth);
      // Clean-up
    }
    /// <summary>
    /// Test de calcul d'une direction vers le sud à partir 
    /// du haut de la grille à gauche (x=2,y=2) vers le bas au centre (x=11,y=20).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ComplexToSouth02()
    {
      // Mise en place des données      
      Grid grid = new Grid();
      grid.LoadFromMemory(VALID_LEVEL_01);
      int ghostX = 2;
      int ghostY = 2;
      int pacmanX = 20;
      int pacmanY = 11;
      // Appel de la méthode à tester
      Direction testSouth = PathFinder.FindShortestPath(grid, ghostX, ghostY, pacmanX, pacmanY);
      // Validation des résultats
      Assert.AreEqual(Direction.South, testSouth);
      // Clean-up
    }
    /// <summary>
    /// Test de calcul d'une direction vers le sud à partir 
    /// du haut de la grille à gauche (x=2,y=2) vers le bas à droite(x=18,y=19).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ComplexToSouth03()
    {
      // Mise en place des données      
      Grid grid = new Grid();
      grid.LoadFromMemory(VALID_LEVEL_01);
      int ghostX = 2;
      int ghostY = 2;
      int pacmanX = 19;
      int pacmanY = 18;
      // Appel de la méthode à tester
      Direction testSouth = PathFinder.FindShortestPath(grid, ghostX, ghostY, pacmanX, pacmanY);
      // Validation des résultats
      Assert.AreEqual(Direction.South, testSouth);
      // Clean-up
    }


    /// <summary>
    /// Test de calcul d'une direction vers l'est à partir 
    /// du haut de la grille à gauche (x=3,y=3) vers la droite en haut (x=18,y=3).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ComplexToEast01()
    {
      // Mise en place des données      
      Grid grid = new Grid();
      grid.LoadFromMemory(VALID_LEVEL_01);
      int ghostX = 3;
      int ghostY = 3;
      int pacmanX = 3;
      int pacmanY = 18;
      // Appel de la méthode à tester
      Direction testEst = PathFinder.FindShortestPath(grid, ghostX, ghostY, pacmanX, pacmanY);
      // Validation des résultats
      Assert.AreEqual(Direction.East, testEst);
      // Clean-up
    }
    /// <summary>
    /// Test de calcul d'une direction vers l'est à partir 
    /// du haut de la grille à gauche vers la gauche (x=3,y=3) au centre vertical (x=15,y=11).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ComplexToEast02()
    {
      // Mise en place des données      
      Grid grid = new Grid();
      grid.LoadFromMemory(VALID_LEVEL_01);
      int ghostX = 3;
      int ghostY = 3;
      int pacmanX = 11;
      int pacmanY = 15;
      // Appel de la méthode à tester
      Direction testEst = PathFinder.FindShortestPath(grid, ghostX, ghostY, pacmanX, pacmanY);
      // Validation des résultats
      Assert.AreEqual(Direction.East, testEst);
      // Clean-up
    }
    /// <summary>
    /// Test de calcul d'une direction vers l'est à partir 
    /// du haut de la grille à gauche vers la gauche (x=2,y=3), vers le haut vers la droite (x=18,y=3)
    /// </summary>
    [TestMethod]
    public void TestFindPath_ComplexToEast03()
    {
      // Mise en place des données      
      Grid grid = new Grid();
      grid.LoadFromMemory(VALID_LEVEL_01);
      int ghostX = 3;
      int ghostY = 2;
      int pacmanX = 3;
      int pacmanY = 18;
      // Appel de la méthode à tester
      Direction testEst = PathFinder.FindShortestPath(grid, ghostX, ghostY, pacmanX, pacmanY);
      // Validation des résultats
      Assert.AreEqual(Direction.East, testEst);
      // Clean-up
    }

    /// <summary>
    /// Test de calcul d'une direction vers l'ouest à partir 
    /// du haut de la grille à droite (x=18,y=3) vers la gauche en haut (x=2,y=3).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ComplexToWest01()
    {
      // Mise en place des données      
      Grid grid = new Grid();
      grid.LoadFromMemory(VALID_LEVEL_01);
      int ghostX = 3;
      int ghostY = 18;
      int pacmanX = 3;
      int pacmanY = 2;
      // Appel de la méthode à tester
      Direction testWest = PathFinder.FindShortestPath(grid, ghostX, ghostY, pacmanX, pacmanY);
      // Validation des résultats
      Assert.AreEqual(Direction.West, testWest);
      // Clean-up
    }

    #endregion
  }
}
