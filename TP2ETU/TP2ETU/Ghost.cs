using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
namespace TP2PROF
{
  // <MikaGauthier>
  public class Ghost
  {
    /// <summary>
    /// Position du fantôme
    /// </summary>
    private Vector2i position = new Vector2i(0, 0);

    /// <summary>
    /// Accesseur de la position en colonne
    /// Propriété C#
    /// </summary>
    public int Column
    {
      get
      {
        return position.Y; 
      }
      set
      {
        position.Y = value;
      }
    }

    /// <summary>
    /// Accesseur de la position en ligne
    /// Propriété C#
    /// </summary>
    public int Row
    {
      get
      { 
        return position.X;
      }
      set
      {
        position.X = value;
      }
    }

    /// <summary>
    /// Indique si le fantôme a été mangé par le pacman sans être
    /// retourné dans sa cage pour se régénérer
    /// </summary>
    private bool isWeak = false;

    /// <summary>
    /// Accesseur de la propriété isWeak
    /// Propriété C#
    /// </summary>
    public bool IsWeak
    {
      get
      {
        return isWeak;
      }
      set
      {
        isWeak = value;
      }
    }


    // Propriétés SFML pour l'affichage
    Texture ghostTextureNormal = new Texture("Assets/Ghost.bmp");
    Texture ghostTextureScared = new Texture("Assets/GhostScared.bmp");
    Texture ghostTextureWeak = new Texture("Assets/GhostWeak.bmp");
    Sprite ghostSprite = null;


    /// <summary>
    /// Identifiant du fantôme (entre 0 et 3 inclusivement) pour déterminer
    /// la couleur par défaut du fantôme dans la méthode Draw.
    /// </summary>
    private int ghostId = 0;

    /// <summary>
    /// Délai pour que le fantôme se mette en mouvement au début
    /// </summary>
    private const int DELAIS_GHOST = 1;

    /// <summary>
    /// Pour l'affichage SFML
    /// </summary>
    static Color[] ghostColors = new Color[] { Color.Red, new Color(255, 192, 203), new Color(137, 207, 240), new Color(255, 127, 80) };


    /// <summary>
    /// Compteur du nombre d'instances de fantômes
    /// </summary>
    static int nbGhostCreated = 0;
    static Random rnd = new Random();

    public Ghost(int row, int column)
    {
      // Affectation de la position du fantôme  
      // Ne pas oublier de lancer une exception si les paramètres sont invalides
      if (row <= 0 || row >= PacmanGame.DEFAULT_GAME_HEIGHT)
        throw new ArgumentOutOfRangeException("row");
      else if (column <= 0 || column >= PacmanGame.DEFAULT_GAME_WIDTH)
        throw new ArgumentOutOfRangeException("column");
      else 
      {
        Column = column;
        Row = row;
      }

      // Affectation de la propriété ghostId.
      // Quelle serait la meilleure "valeur" à affecter ici???
      ghostId = nbGhostCreated;

      // Incrémenter ici la propriété servant à compter le nombre de fantômes créés
      // jusqu'à date
      nbGhostCreated++;

      // Initialisation SFML
      ghostSprite = new Sprite(ghostTextureNormal);
      ghostSprite.Origin = new Vector2f(ghostTextureNormal.Size.X / 2, ghostTextureNormal.Size.Y / 2);
    }

    /// <summary>
    /// Déplace le fantôme selon une direction donnée.
    /// </summary>
    /// <param name="direction">Direction dans laquelle on veut déplacer le fantôme</param>
    /// <param name="grid">Grille de référence. Utilisée pour ne pas que le fantôme passe au travers des murs</param>
    // A COMPLETER MÉTHODE MOVE
    public void Move(Direction direction, Grid grid)
    {
      if (direction == Direction.East)
      {
        if (grid.GetGridElementAt(Row, Column + 1) == PacmanElement.Wall)
        {

        }
        else
          Column = position.Y + 1;
      }
      else if (direction == Direction.North)
      {
        if (grid.GetGridElementAt(Row - 1, Column ) == PacmanElement.Wall)
        {

        }
        else
          Row = position.X - 1;
      }
      else if (direction == Direction.West)
      {
        if (grid.GetGridElementAt(Row, Column - 1) == PacmanElement.Wall)
        {

        }
        else
          Column = position.Y - 1;
      }
      else if (direction == Direction.South)
      {
        if (grid.GetGridElementAt(Row + 1, Column ) == PacmanElement.Wall)
        {

        }
        else
          Row = position.X + 1;
      }
    }

    /// <summary>
    /// Affiche le fantôme dans la fenêtre de rendu.
    /// </summary>
    /// <param name="window">Fenêtre de rendu</param>
    /// <param name="isSuperPillActive">true si une super-pastille est active, false sinon</param>
    public void Draw(RenderWindow window, bool isSuperPillActive)
    {
      // Mise à jour de la texture du fantôme selon l'état du fantôme
      if (isSuperPillActive)
      {
        // État "effrayé", i.-e. le pacman a mangé une superpastille
        ghostSprite.Texture = ghostTextureWeak;
        ghostSprite.Color = Color.White;
      }
      else
      {
        // État "normal"
        ghostSprite.Texture = ghostTextureNormal;
        ghostSprite.Color = ghostColors[ghostId];
      }

      // ppoulin
      // A décommenter lorsqu'il sera possible d'accéder aux propriétés Column et Row
      // du fantôme
     ghostSprite.Position =    new Vector2f(PacmanGame.DEFAULT_GAME_ELEMENT_WIDTH * Column , 
                                         PacmanGame.DEFAULT_GAME_ELEMENT_HEIGHT * Row ) 
                            +  ghostSprite.Origin;
      window.Draw(ghostSprite);
    }

    /// <summary>
    /// Met à jour la position du fantôme
    /// </summary>
    /// <param name="grid">Grille de référence. Utilisée pour ne pas que le fantôme passe au travers des murs</param>
    /// <param name="pacmanPosition"></param>
    /// <param name="isSuperPillActive"></param>
    public void Update(Grid grid, Vector2i pacmanPosition, bool isSuperPillActive)
    {
      // ppoulin
      // A compléter 
      if (isSuperPillActive)
      {

      }
      else
      {
        Direction versPacman = PathFinder.FindShortestPath(grid, Row, Column, pacmanPosition.X, pacmanPosition.Y);
        Move(versPacman, grid);
      }
    }
  }
}
