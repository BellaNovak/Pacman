using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; } //metodo para referenciar o spiterenderer
     
    public Sprite[] sprites; //vetor com os sprites que serao animados

    public float animationTime = 0.25f; //tempo de um sprite para outro

    public int animationFrame { get; private set; } //indice de qual sprite esta

    public bool loop = true; //determinar se deve ou nao entrar em loop o sprite

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() //funcao para fazer a animacao 
    {
        InvokeRepeating(nameof(Advance), this.animationTime, this.animationTime);
    }

    private void Advance()
    {
        if(!this.spriteRenderer.enabled){ //verifica se o sprite renderer esta desativado
            return;
        }

        this.animationFrame++;

        if (this.animationFrame >= this.sprites.Length && this.loop){ //verifica se o frame atual eh o ultimo
            this.animationFrame = 0;
        } 

        if (this.animationFrame >= 0 && this.animationFrame < this.sprites.Length){ //metodo que garante que sempre estara dentro do vetor
            this.spriteRenderer.sprite = this.sprites[this.animationFrame];
        } 
    }

    public void Restart() //reinicia a animacao
    {
        this.animationFrame = -1;

        Advance();
    }
}
