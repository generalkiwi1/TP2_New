using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
namespace TP2PROF
{
  public class Pacman
  {
  //<AntoineRL>
    /// <summary>
    /// Position du pacman
    /// </summary>
    Vector2i position;
    /// <summary>
    /// Accesseur de la position en colonne
    /// Propriété C#
    /// </summary>
    public int Column
    {
      get { return position.X; }
      set { position.X = value; }
    }
    /// <summary>
    /// Accesseur de la position en ligne
    /// Propriété C#
    /// </summary>
    public int Row
    {
      get { return position.Y; }
      set { position.Y = value; }
    }


    // Propriétés SFML pour l'affichage
    Texture pacmanTexture = new Texture("Assets/Pacman.bmp");
    Sprite pacmanSprite = null;

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="row">Ligne de départ du pacman</param>
    /// <param name="column">Colonne de départ du pacman</param>
    public Pacman(int row, int column)
    {

      // Affectation de la position du pacman 
      // Ne pas oublier de lancer une exception si les paramètres sont invalides
      try
      {
        Row = row;
        Column = column;
      }
      catch (FormatException exception)
      {
        Console.WriteLine(exception.ToString());
        // On pourrait ou non faire un throw() ici
        //throw;
      }
      // Initialisation des propriétés SFML
      pacmanSprite = new Sprite(pacmanTexture);
      pacmanSprite.Origin = new Vector2f(pacmanTexture.Size.X/2, pacmanTexture.Size.Y / 2);
    }

    /// <summary>
    /// Déplace le pacman selon une direction donnée.
    /// </summary>
    /// <param name="direction">Direction dans laquelle on veut déplacer le pacman</param>
    /// <param name="grid">Grille de référence. Utilisée pour ne pas que le pacman passe au travers des murs</param>
    // A COMPLETER MÉTHODE MOVE
    public void Move(Direction direction, Grid grid)
    {
      if (Column + 1<grid.Width)
      {
        if (grid.PacmanOriginalPositionColumn + 1 != 1 && grid.PacmanOriginalPositionColumn + 1 != 6)
        {
          if (direction == Direction.East)
          {
            Column++;
          }
        }
      }
      if (Column - 1 > 0)
      {
        if (grid.PacmanOriginalPositionColumn - 1 != 1 && grid.PacmanOriginalPositionColumn - 1 != 6)
        {
          if (direction == Direction.West)
          {
            Column--;
          }
        }
      }
      if (Row + 1<grid.Height)
      {
        if (grid.PacmanOriginalPositionRow + 1 != 1 && grid.PacmanOriginalPositionRow + 1 != 6)
          if (direction == Direction.South)
          {
            Row++;
          }
      }
      if (Row - 1 > 0)
      {
        if (grid.PacmanOriginalPositionRow - 1 != 1 && grid.PacmanOriginalPositionRow - 1 != 6)
        {
          if (direction == Direction.North)
          {
            Row--;
          }
        }
      }
    }
    //</AntoineRL>




    /// <summary>
    /// Affiche le pacman dans la fenêtre de rendu.
    /// </summary>
    /// <param name="window">Fenêtre de rendu</param>
    public void Draw(RenderWindow window)
    {
      // ppoulin
      // A décommenter lorsqu'il sera possible d'accéder aux propriétés Column et Row
      // du pacman  
       pacmanSprite.Position = new Vector2f(PacmanGame.DEFAULT_GAME_ELEMENT_WIDTH* Column , 
                                            PacmanGame.DEFAULT_GAME_ELEMENT_HEIGHT*Row )+ pacmanSprite.Origin;
      window.Draw(pacmanSprite);
    }
  }
}
