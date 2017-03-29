using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2PROF
{
  public static class PathFinder
  {
    /// <summary>
    /// Initialise le tableau des coûts de déplacements, Le tableau est 
    /// initialisé à int.MaxValue partout sauf à l'endroit où se trouve le
    /// fantôme où le tableau est initialisé à 0.
    /// </summary>
    /// <param name="aGrid">La grille du jeu: pour connaitre les dimensions attendues</param>
    /// <param name="fromX">La position du pacman en colonne</param>
    /// <param name="fromY">La position du pacman en ligne</param>
    /// <returns>Le tableau initialisé correctement</returns>
    // A COMPLÉTER  Méthode InitCosts
    //<AntoineRL>
    public static int[,] InitCosts(Grid aGrid, int fromX, int fromY)
    {
      int[,] tableauJeu = new int[PacmanGame.DEFAULT_GAME_HEIGHT, PacmanGame.DEFAULT_GAME_WIDTH];
      for (int i = 0; i < PacmanGame.DEFAULT_GAME_HEIGHT; i++)
      {
        for (int j = 0; j < PacmanGame.DEFAULT_GAME_WIDTH; j++)
        {
          tableauJeu[i, j] = int.MaxValue;
        }
      }
      tableauJeu[fromX, fromY] = 0;
      return tableauJeu;
    }

    /// <summary>
    /// Détermine le premier déplacement nécessaire pour déplacer un objet de la position (fromX, fromY)
    /// vers la position (toX, toY). 
    /// <param name="aGrid">La grille du jeu: pour connaitre les positions des murs</param>
    /// <param name="fromX">La position de départ en colonne</param>
    /// <param name="fromY">La position de départ en ligne</param>
    /// <param name="toX">La position cible en colonne</param>
    /// <param name="toY">La position cible en ligne</param>
    /// <remark>Typiquement, la position (fromX, fromY) est celle du fantôme tandis 
    /// que la position (toX, toY) est celle du pacman.</remark>
    /// <returns>La direction dans laquelle on doit aller. Direction.None si l'on
    /// est déjà rendu ou Direction.Undefined s'il est impossible d'atteindre la cible</returns>
    /// </summary>
    // A COMPLÉTER  Méthode FindShortestPath
    public static Direction FindShortestPath(Grid aGrid, int fromX, int fromY, int toX, int toY)
    {
      int[,] distances = InitCosts(aGrid, fromX, fromY);
      distances = ComputeCosts(aGrid, fromX, fromY, toX, toY, distances);
      Direction directionChoisit = RecurseFindDirection(distances, toX, toY, fromX, fromY, aGrid);
      return directionChoisit;

    }





    /// <summary>
    /// Calcule le nombre de déplacements requis pour aller de la position (fromX, fromY)
    /// vers la position (toX, toY). 
    /// <param name="aGrid">La grille du jeu: pour connaitre les positions des murs</param>
    /// <param name="fromX">La position de départ en colonne</param>
    /// <param name="fromY">La position de départ en ligne</param>
    /// <param name="toX">La position cible en colonne</param>
    /// <param name="toY">La position cible en ligne</param>
    /// <param name="costs">Le tableau des coûts à remplir</param>
    /// <remark>Typiquement, la position (fromX, fromY) est celle du fantôme tandis 
    /// que la position (toX, toY) est celle du pacman.</remark>
    /// <remark>Cette méthode est récursive</remark>
    /// </summary>
    // A COMPLÉTER  Méthode ComputeCosts
    public static int[,] ComputeCosts(Grid aGrid, int fromX, int fromY, int toX, int toY, int[,] costs)
    {
      //costs = InitCosts(aGrid, fromX, fromY);
      if (fromX == toX && fromY == toY)
      {
        return costs;
      }
      else
      {
        if ((fromX > 0 && fromX < PacmanGame.DEFAULT_GAME_WIDTH) && (fromY - 1 > 0 && fromY - 1 < PacmanGame.DEFAULT_GAME_HEIGHT))
        {
          if (costs[fromX, fromY] + 1 < costs[fromX, fromY - 1])
          {
            costs[fromX, fromY - 1] = (costs[fromX, fromY - 1]) + 1;
          }
          return ComputeCosts(aGrid, fromX - 1, fromY, toX, toY, costs);
        }
        if ((fromX - 1 > 0 && fromX - 1 < PacmanGame.DEFAULT_GAME_WIDTH) && (fromY > 0 && fromY < PacmanGame.DEFAULT_GAME_HEIGHT))
        {
          if (costs[fromX, fromY] + 1 < costs[fromX - 1, fromY])
          {
            costs[fromX - 1, fromY] = (costs[fromX - 1, fromY]) + 1;
          }
          return ComputeCosts(aGrid, fromX, fromY + 1, toX, toY, costs);
        }
        if ((fromX > 0 && fromX < PacmanGame.DEFAULT_GAME_WIDTH) && (fromY + 1 > 0 && fromY + 1 < PacmanGame.DEFAULT_GAME_HEIGHT))
        {
          if (costs[fromX, fromY] + 1 < costs[fromX, fromY + 1])
          {
            costs[fromX, fromY + 1] = (costs[fromX, fromY + 1]) + 1;
          }
          return ComputeCosts(aGrid, fromX + 1, fromY, toX, toY, costs);
        }
        if ((fromX + 1 > 0 && fromX + 1 < PacmanGame.DEFAULT_GAME_WIDTH) && (fromY > 0 && fromY < PacmanGame.DEFAULT_GAME_HEIGHT))
        {
          if (costs[fromX, fromY] + 1 < costs[fromX + 1, fromY])
          {
            costs[fromX + 1, fromY] = (costs[fromX + 1, fromY]) - 1;
          }
          return ComputeCosts(aGrid, fromX + 1, fromY, toX, toY, costs);
        }
        return costs;
      }
    }




    /// <summary>
    /// Parcourt le tableau de coûts pour trouver le premier déplacement requis pour aller de la position (fromX, fromY)
    /// vers la position (toX, toY). 
    /// <param name="costs">Le tableau des coûts prédédemment calculés</param>
    /// <param name="targetX">La position cible en colonne</param>
    /// <param name="targetY">La position cible en ligne</param>
    /// <remark>Typiquement, la position (targetX, targetY) est celle du pacman.</remark>
    /// <remark>Cette méthode est récursive</remark>
    /// </summary>
    /// <returns>La direction dans laquelle on doit aller. Direction.None si l'on
    /// est déjà rendu ou Direction.Undefined s'il est impossible d'atteindre la cible</returns>
    // A COMPLÉTER  Méthode RecurseFindDirection
    //<Mika>
    public static Direction RecurseFindDirection(int[,] costs, int targetX, int targetY, int fromX, int fromY, Grid aGrid)
    {
      costs = ComputeCosts(aGrid, fromX, fromY, targetX, targetY, costs);
      Direction directionCourante = Direction.Undefined;
      if (costs[targetX, targetY - 1] == (costs[targetX, targetY] - 1) && (targetY - 1 > 1))
      {
        if (costs[targetX, targetY - 1] == costs[fromX, fromY])
        {
          directionCourante = Direction.South;
          return directionCourante;
        }
        else
        {
          return FindShortestPath(aGrid, targetX, targetY - 1, fromX, fromY);
        }
      }
      else if (costs[targetX - 1, targetY] == (costs[targetX, targetY] - 1) && (targetX - 1 > 1))
      {
        if (costs[targetX - 1, targetY] == costs[fromX, fromY])
        {
          directionCourante = Direction.East;
          return directionCourante;
        }
        else
        {
          return FindShortestPath(aGrid, targetX - 1, targetY, fromX, fromY);
        }
      }
      else if (costs[targetX, targetY + 1] == (costs[targetX, targetY] - 1) && (targetY + 1 < PacmanGame.DEFAULT_GAME_HEIGHT-1))
      {
        if (costs[targetX, targetY + 1] == costs[fromX, fromY])
        {
          directionCourante = Direction.North;
          return directionCourante;
        }
        else
        {
          return FindShortestPath(aGrid, targetX, targetY + 1, fromX, fromY);
        }
      }
      else if (costs[targetX + 1, targetY] == (costs[targetX, targetY] - 1) && (targetX + 1 < PacmanGame.DEFAULT_GAME_WIDTH - 1))
      {
        if (costs[targetX + 1, targetY] == costs[fromX, fromY])
        {
          directionCourante = Direction.West;
          return directionCourante;
        }
        else
        {
          return FindShortestPath(aGrid, targetX + 1, targetY, fromX, fromY);
        }
      }
      return directionCourante;
    }
    //</Mika>
    //</AntoineRL>

  }

}
