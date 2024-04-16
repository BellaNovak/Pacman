using UnityEngine;

public class Ghost : MonoBehaviour
{
   public Movement movement { get; private set; }
   public GhostHome home { get; private set; }
   public GhostScatter scatter { get; private set; }
   public GhostChase chase { get; private set; }
   public GhostFrightened frightened { get; private set; }
   public GhostBehavior initialBehavior;
   public Transform target;
   public int points = 200; //pontuacao por comer um fantasma

   private void Awake()
   {
      this.movement = GetComponent<Movement>();
      this.home = GetComponent<GhostHome>();
      this.scatter = GetComponent<GhostScatter>();
      this.chase = GetComponent<GhostChase>();
      this.frightened = GetComponent<GhostFrightened>();
   }

   private void Start()
   {
      ResetState();
   }

   public void ResetState()
   {
      this.movement.ResetState();
      this.gameObject.SetActive(true);

      this.frightened.Disable();
      this.chase.Disable();
      this.scatter.Enable();

      if (this.home != this.initialBehavior){
         this.home.Disable();
      }

      if (this.initialBehavior != null){
         this.initialBehavior.Enable();
      }

   }

   private void OnCollisionEnter2D(Collision2D collision) //funcao que checa a colisao entre o pacman e os fantasmas
   {
      if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
      {
         if (this.frightened.enabled){
            FindObjectOfType<GameManager>().GhostEaten(this); //se o modo como medo estiver ativado, o fantasma eh comido
         } else {
            FindObjectOfType<GameManager>().PacmanEaten(); //se estiver desativado, o pacman eh comido
         }

      }
   }
   
}
