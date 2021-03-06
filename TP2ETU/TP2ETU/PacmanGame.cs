﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;
namespace TP2PROF
{
  public class PacmanGame
  {
    /// <summary>
    /// Nombre de cases en largeur dans le jeu du Pacman
    /// </summary>
    public const int DEFAULT_GAME_WIDTH = 21;

    /// <summary>
    /// Nombre de cases en hauteur dans le jeu du Pacman
    /// </summary>
    public const int DEFAULT_GAME_HEIGHT = 22;

    /// <summary>
    /// Largeur de rendu d'un élément de jeu
    /// </summary>
    public const int DEFAULT_GAME_ELEMENT_HEIGHT = 20;

    /// <summary>
    /// Hauteur de rendu d'un élément de jeu
    /// </summary>
    public const int DEFAULT_GAME_ELEMENT_WIDTH = 20;

    /// <summary>
    /// La grille principale de jeu. Elle est créée dans la méthode LoadGrid
    /// </summary>
    
    // <MikaGauthier>
    private Grid grid = null;


    /// <summary>
    /// Nombre de fantômes présents dans le jeu
    /// </summary>
    // A COMPLETER

    private const int NB_GHOSTS = 4;

    /// <summary>
    /// Les 4 fantômes du jeu
    /// </summary>
    // A COMPLETER
    private Ghost[] ghosts = new Ghost[NB_GHOSTS];

    /// <summary>
    /// Compteur des fantomes pour l'uptade
    /// </summary>
    private int updateCpt = 0;

    /// <summary>
    /// Le pacman du jeu
    /// </summary>
    // A COMPLETER
    private Pacman pacman = null; 

    /// <summary>
    /// Durée d'activation d'une superpastille (en secondes)
    /// </summary>
    private const int SUPERPILL_ACTIVATION_TIME = 13;

    /// <summary>
    /// Compteur d'activation superpill
    /// </summary>
    private int updateSuperPillActive;

    /// <summary>
    /// Si la SuperPill est active ou non
    /// </summary>
    private bool isSuperPillActive = false;

    /// <summary>
    /// Accesseur permettant de savoir si une super pastille est active
    /// Propriété C#
    /// </summary>
    // A COMPLETER
    public bool SuperPillActive
    {
      get 
      {
        return isSuperPillActive; 
      }
      
      set
      { 
        isSuperPillActive = value; 
      }
      
    } 

    // </MikaGauthier>

    // Propriétés SFML pour l'affichage des pastilles et super-pastilles
    const float SMALL_PILL_RADIUS = DEFAULT_GAME_ELEMENT_HEIGHT/8;
    const float SUPER_PILL_RADIUS = 2 * SMALL_PILL_RADIUS;
    CircleShape smallPillShape = new CircleShape(SMALL_PILL_RADIUS);
    CircleShape superPillShape = new CircleShape(SUPER_PILL_RADIUS);

    // Propriétés SFML pour l'affichage du labyrinthe
    Texture wallTexture = new Texture("Assets/Wall.bmp");
    Sprite wallSprite = null;

    /// <summary>
    /// Constructeur du jeu de Pacman
    /// </summary>
    public PacmanGame()
    {
      // A COMPLETER   
      grid = new Grid();

      // Initialisation SFML
      smallPillShape.Origin = new Vector2f((float)-(DEFAULT_GAME_ELEMENT_WIDTH- SMALL_PILL_RADIUS )/ 2, -(float)(DEFAULT_GAME_ELEMENT_HEIGHT- SMALL_PILL_RADIUS )/ 2);
      superPillShape.Origin = new Vector2f((float)-(DEFAULT_GAME_ELEMENT_WIDTH- SUPER_PILL_RADIUS) / 2, -(float)(DEFAULT_GAME_ELEMENT_HEIGHT- SUPER_PILL_RADIUS) / 2);
      wallSprite = new Sprite(wallTexture);      
    }
    
    // <MikaGauthier>

    /// <summary>
    /// Charge un fichier de labyrinthe.
    /// </summary>
    /// <param name="path">Le fichier contenant la description du labyrinthe de jeu</param>
    /// <returns>true si le chargement s'est bien effectué, false sinon</returns>
    public bool LoadGrid(string path)
    {
      bool retval = System.IO.File.Exists(path);

      if (retval)
      {
        string fileContent = System.IO.File.ReadAllText(path);
        // ppoulin
        // Appelez la méthode LoadFromMemory ici
        // A COMPLETER
        retval = grid.LoadFromMemory(fileContent);

        // Si le chargement s'est correctement effectué
        if (true == retval)
        {
          // On parcourt la grille et, avec la méthode GetGridElementAt // Besoin de ton avis ici
          // On trouve les positions où il y a des fantômes
          int ghostCpt = 0;

          for (int row = 0; row < grid.Height; row++)
          {
            for (int col = 0; col < grid.Width; col++)
            {
              PacmanElement currentPosition = grid.GetGridElementAt(row, col);

              if (currentPosition == PacmanElement.Ghost)
              {
                ghosts[ghostCpt]  = new Ghost(row,col);
                ghostCpt++;
                grid.SetGridElementAt(row, col, PacmanElement.None);

              }

              else if (currentPosition == PacmanElement.Pacman)
              {
              //<AntoineRL>
                pacman = new Pacman(row, col);
                grid.SetGridElementAt(row, col, PacmanElement.None);
              //</AntoineRL>
              }
            }
          }
        }
      }
      return retval;
    }

    // </MikaGauthier>

    /// <summary>
    /// Met à jour la logique de jeu
    /// </summary>
    /// <param name="key">La touche entrée par le joueur pour contrôle le pacman</param>
    /// <returns>EndGameResult.NotFinished si la partie est toujours en cours, EndGameResult.Win
    /// si le joueur a mangé toutes les pastilles ou EndGameResult.Losse si le joueur s'est fait
    /// mangé par un fantôme</returns>
    public EndGameResult Update(Keyboard.Key key)
    {
      EndGameResult partieFinie = EndGameResult.NotFinished;

      //<AntoineRL>
      // Déplacement du joueur
      if (key == Keyboard.Key.Left)
      {
        // A COMPLETER 
        pacman.Move(Direction.West, grid);
      }
      else if (key == Keyboard.Key.Right)
      {
        // A COMPLETER
        pacman.Move(Direction.East, grid);
      }
      else if (key == Keyboard.Key.Up)
      {
        // A COMPLETER
        pacman.Move(Direction.North, grid);
      }
      else if (key == Keyboard.Key.Down)
      {
        // A COMPLETER
        pacman.Move(Direction.South, grid);
      }
      //</AntoineRL>

      // <MikaGauthier>

      // Mise à jour des fantômes
      // A COMPLETER  
      // Sleep a second 
      if (updateCpt % 10 == 0)
      {
        if (updateSuperPillActive > SUPERPILL_ACTIVATION_TIME)
        {
          SuperPillActive = false;
          updateSuperPillActive = 0;
        }
        else if (SuperPillActive)
        {
          updateSuperPillActive++;
        }
        ghosts[0].Update(grid, new Vector2i(pacman.Row, pacman.Column), SuperPillActive);
        ghosts[1].Update(grid, new Vector2i(pacman.Row, pacman.Column), SuperPillActive);
        ghosts[2].Update(grid, new Vector2i(pacman.Row, pacman.Column), SuperPillActive);
        ghosts[3].Update(grid, new Vector2i(pacman.Row, pacman.Column), SuperPillActive);
      }
      updateCpt++;

      // Limitation de int
      if (updateCpt == 10000)
      {
        updateCpt = 0;
      }

      

      // Gestion des collisions avec le pacman
      // A COMPLETER    
      for (int i = 0; i < NB_GHOSTS; i++)
      {
        if (pacman.Row  == ghosts[i].Row && pacman.Column == ghosts[i].Column && SuperPillActive == false)
        {
          partieFinie = EndGameResult.Losse;
        }
        else if (pacman.Row == ghosts[i].Row && pacman.Column == ghosts[i].Column && SuperPillActive == true)
        {
          if (i == 0)
          {
            ghosts[i].Column = 10;
            ghosts[i].Row = 8;
          }
          else if (i == 1)
          {
            ghosts[i].Column = 10;
            ghosts[i].Row = 9;
          }
          else if (i == 2)
          {
            ghosts[i].Column = 9;
            ghosts[i].Row = 9;
          }
          else if (i == 3)
          {
            ghosts[i].Column = 11;
            ghosts[i].Row = 9;
          }
          partieFinie = EndGameResult.NotFinished;
        }
      }

      // </MikaGauthier>
      //<AntoineRL>

      // Vérification du ramassage d'une pastille
      // A COMPLETER    
      if (grid.GetGridElementAt(pacman.Row,pacman.Column)==PacmanElement.Pill)
      {
        grid.SetGridElementAt(pacman.Row, pacman.Column, PacmanElement.None);
      }

      // </AntoineRL>
      // <MikaGauthier>

      // Vérification de l'activation d'un superpill
      // A COMPLETER    
      if (grid.GetGridElementAt(pacman.Row,pacman.Column) == PacmanElement.SuperPill)
      {
        SuperPillActive = true;
        updateSuperPillActive = 0;
        grid.SetGridElementAt(pacman.Row, pacman.Column, PacmanElement.None);
      }

      // Validations de fin de partie
      // A COMPLETER car il faut que la partie finisse s'il ne reste plus de pastille
      if (CountNbPillsRemaining() == 0)
      {
        partieFinie = EndGameResult.Win;
      }

      // ou si le pacman a été mangé par un fantôme
      return partieFinie;

      // </MikaGauthier>
    }

    /// <summary>
    /// Calcule le nombre de pastille non encore ramassées par le pacman
    /// </summary>
    /// <returns>Le nombre de pastille non encore ramassées</returns>
    // A COMPLETER    
    //<AntoineRL>
    private int CountNbPillsRemaining()
    {
      // Grid grid = new Grid();
      int compteurPills = 0;
      for (int i = 0; i < PacmanGame.DEFAULT_GAME_HEIGHT; i++)
      {
        for (int j = 0; j < PacmanGame.DEFAULT_GAME_WIDTH; j++)
        {
          if (grid.GetGridElementAt(i,j)==PacmanElement.Pill)
          {
            compteurPills++;
          }
        }
      }
      return compteurPills;
    }
    //</AntoineRL>



    /// <summary>
    /// Dessine les éléments du jeu à l'écran
    /// </summary>
    /// <param name="window">Le contexte de rendu</param>
    public void Draw(RenderWindow window)
    {
      // PPOULIN
      // A DECOMMENTER LORSQUE LES CLASSES AURONT ÉTÉ CODÉES
      for (int row = 0; row < grid.Height; row++)
      {
        for (int col = 0; col < grid.Width; col++)
        {
          // Pastille régulière
          if (grid.GetGridElementAt(row, col)==PacmanElement.Pill)                  
          {
            smallPillShape.Position = new Vector2f(col * DEFAULT_GAME_ELEMENT_WIDTH, row * DEFAULT_GAME_ELEMENT_HEIGHT);
            window.Draw(smallPillShape);     
          }
          // Super pastille
          else if (grid.GetGridElementAt(row, col) == PacmanElement.SuperPill)
          {

            superPillShape.Radius =  SUPER_PILL_RADIUS;
            superPillShape.Position = new Vector2f(col * DEFAULT_GAME_ELEMENT_WIDTH, row * DEFAULT_GAME_ELEMENT_HEIGHT);
            window.Draw(superPillShape);
          }
          // Mur
          else if (grid.GetGridElementAt(row, col) == PacmanElement.Wall)
          {
            wallSprite.Position = new Vector2f(col * DEFAULT_GAME_ELEMENT_WIDTH, row * DEFAULT_GAME_ELEMENT_HEIGHT);
            window.Draw(wallSprite);
          }
        }
      }


      // Les 4 fantômes
      for (int i = 0; i < NB_GHOSTS; i++)
      {
        if (ghosts[i] != null)
          ghosts[i].Draw(window, SuperPillActive);
      }

      // Le pacman
      if (null != pacman)
        pacman.Draw(window);
        
    }
  }
}
