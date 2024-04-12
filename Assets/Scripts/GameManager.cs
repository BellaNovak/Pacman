using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;

    public Transform pallets;

    public int ghostMultiplier { get; private set; } = 1;

    public int score { get; private set; }

    public int lives { get; private set; } 

    private void Start() //funcao quando se da start
    {
        NewGame();
    }

    private void Update() //funcao para atualizar o jogo caso o jogador perca todas as vidas
    {
        if (this.lives <= 0 && Input.anyKeyDown){
            NewGame();
        }
    }

    private void NewGame() //funcao para iniciar o jogo
    {
        SetScore(0); //comeca com 0 pontos
        SetLives(3); //comeca com 3 vidas
        NewRound();
    }

    private void NewRound() //funcao quando o jogador perde uma vida
    {
        foreach(Transform pellet in this.pallets){
            pellet.gameObject.SetActive(true); //pellet se mantem inalterados
        }

        ResetState();
    }

    private void ResetState() //funcao para resetar a posicao do pacman e dos fantasmas
    {
        ResetGhostMultiplier();

        for(int i = 0; i < this.ghosts.Length; i++){
            this.ghosts[i].ResetState();
        }

        this.pacman.ResetState();

    }

    private void GameOver() //funcao caso o jogador perca todas as vidas
    {
        for(int i = 0; i < this.ghosts.Length; i++){
            this.ghosts[i].gameObject.SetActive(false);
        }

        this.pacman.gameObject.SetActive(false);
    }

    private void SetScore(int score) //funcao para marcar a pontuacao
    {
        this.score = score;
    }

    private void SetLives(int lives) //funcao para marcar as vidas
    {
        this.lives = lives;
    }

    public void GhostEaten(Ghost ghost) //funcao caso um fantasma seja comido
    {
        int points = ghost.points * ghostMultiplier;
        SetScore(this.score + points); //incremento de pontos
        this.ghostMultiplier++; //multiplicador de pontos dos fantasmas
    }

    public void PacmanEaten() //funcao caso o pacman seja comido
    {
        this.pacman.gameObject.SetActive(false);

        SetLives(this.lives - 1); //perde uma vida

        if(this.lives > 0){
            Invoke(nameof(ResetState), 3.0f); //se nao acabar todas as vidas, reseta a posicao do pacman e dos fantasmas depois de 3 segundos
        } else{
            GameOver(); //se acabar as vidas, acaba o jogo
        }

    }

    public void PelletEaten(Pellet pellet) //funcao para comer os pelleta
    {
        pellet.gameObject.SetActive(false); //faz o pellet sumir pois foi comido
        SetScore(this.score + pellet.points); //incrementa a pontuacao

        if(!HasRemainingPellets())
        {
            this.pacman.gameObject.SetActive(false); 
            Invoke(nameof(NewRound), 3.0f); //novo round comeca
        }

    }

    public void PowerPelleteEaten(PowerPellet pellet)
    {
        PelletEaten(pellet);
        CancelInvoke(); //cancela o invoke caso o tempo do powerpellet nao tenha acabado e outro seja comido
        Invoke(nameof(ResetGhostMultiplier), pellet.duration); //reseta o multiplicador quando o tempo do powerpellet acaba
    }

    private bool HasRemainingPellets()
    {
        foreach(Transform pellet in this.pallets)
        {
            if(pellet.gameObject.activeSelf){ //verifica se todos os pellets foram comidos
                return true;
            }
        }

        return false;
    }

    private void ResetGhostMultiplier()
    {
        this.ghostMultiplier = 1;
    }
}
