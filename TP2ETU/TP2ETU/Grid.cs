using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
namespace TP2PROF
{
  public class Grid
  {
    // <MGauthier>

    /// <summary>
    /// Grille logique du jeu.
    /// Tableau 2D de PacmanElement
    /// </summary>  
    // A compléter
    private PacmanElement[,] elements = null;


    /// <summary>
    /// Position de la cage des fantômes
    /// </summary>
    // A compléter
    private Vector2i ghostCagePosition;

    /// <summary>
    /// Accesseur du numéro de la ligne où se trouve la cage à fantômes
    /// Propriété C#
    /// </summary>
    // A compléter
    public int GhostCagePositionRow
    {
      get { return ghostCagePosition.Y; }
      set { ghostCagePosition.Y = value; }
    }

    /// <summary>
    /// Accesseur du numéro de la colonne où se trouve la cage à fantômes
    /// Propriété C#
    /// </summary>
    // A compléter
    public int GhostCagePositionColumn
    {
      get { return ghostCagePosition.X; }
      set { ghostCagePosition.X = value; }
    }


    /// <summary>
    /// Position originale du pacman
    /// </summary>
    // A compléter
    private Vector2i pacmanOriginalPosition;

    /// <summary>
    /// Accesseur du numéro de la ligne où se trouve le pacman au début
    /// Propriété c#
    /// </summary>
    // A compléter
    public int PacmanOriginalPositionRow
    {
      get { return pacmanOriginalPosition.Y; }
      set { pacmanOriginalPosition.Y = value; }
    }

    /// <summary>
    /// Accesseur du numéro de la colonne où se trouve le pacman au début
    /// Propriété C#
    /// </summary>
    // A compléter
    public int PacmanOriginalPositionColumn
    {
      get { return pacmanOriginalPosition.X; }
      set { pacmanOriginalPosition.X = value; }
    }

    /// <summary>
    /// Accesseur de la hauteur
    /// Propriété C#
    /// </summary>
    // A compléter
    public int Height
    {
      get 
      {
          return elements.GetLength(0);   
      }
    }

    /// <summary>
    /// Accesseur de la largeur
    /// Propriété C#
    /// </summary>
    // A compléter
    public int Width
    {
      get
      {
          return elements.GetLength(1);
      }
    }

    /// <summary>
    /// Constructeur sans paramètre
    /// </summary>
    // A compléter
    public Grid()
    {
      elements = new PacmanElement[PacmanGame.DEFAULT_GAME_HEIGHT, PacmanGame.DEFAULT_GAME_WIDTH];
      ghostCagePosition = new Vector2i(GhostCagePositionRow, GhostCagePositionColumn);
      pacmanOriginalPosition = new Vector2i(PacmanOriginalPositionRow, PacmanOriginalPositionColumn);
    }

    /// <summary>
    /// Charge un niveau à partir d'une chaine de caractères en mémoire.
    /// Voir l'énoncé du travail pour le format de la chaîne.
    /// </summary>
    /// <param name="content"> Le contenu du niveau en mémoire</param>
    /// <returns>true si le chargement est correct, false sinon</returns>
    public bool LoadFromMemory(string content)
    {
      bool retval = true;

      int onlyOnePacman = 0;
      int onlyOneCage = 0;

      // Séparation du document en ligne
      string[] fileLayer = content.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

      // Vérification document vide
      if (fileLayer.Length == PacmanGame.DEFAULT_GAME_HEIGHT)
      {
        // Incrémentation
        for (int i = 0; i < elements.GetLength(0); i++)
        {
          fileLayer[i] = fileLayer[i].Trim(new char[] { ' ', '\r', '\n','/' });
          // Incrémentation de la ligne courrante utilisée
          string[] currentLayer = fileLayer[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

          if (currentLayer.Length == PacmanGame.DEFAULT_GAME_WIDTH)
          {
            for (int j = 0; j < elements.GetLength(1); j++)
            {
              int type = int.Parse(currentLayer[j]);
              elements[i, j] = (PacmanElement)type;

              // Données des générations cage et pacman
              if (type == 3)
              {
                onlyOnePacman++;
              }
              if (type == 6)
              {
                onlyOneCage++;
              }
            }
          }
          else
          {
            retval = false;
            break;
          }
        }
      }
      else
        retval = false;

      if (onlyOnePacman == 0 || onlyOnePacman == 2 || onlyOneCage == 0 || onlyOneCage == 2)
        retval = false;

      return retval;
    }

    /// <summary>
    /// Retourne l'élément à la position spécifiée
    /// </summary>
    /// <param name="row">La ligne</param>
    /// <param name="column">La colonne</param>
    /// <returns>L'élément à la position spécifiée</returns>
    // A compléter
    public PacmanElement GetGridElementAt(int row, int column)
    {
      return elements[row, column];
    }

    /// <summary>
    /// Modifie le contenu du tableau à la position spécifiée
    /// </summary>
    /// <param name="row">La ligne</param>
    /// <param name="column">La colonne</param>
    /// <param name="element">Le nouvel élément à spécifier</param>
    public void SetGridElementAt (int row, int column, PacmanElement element)
    {
      elements[row, column] = element;
    }
  }
  // </MGauthier
}
