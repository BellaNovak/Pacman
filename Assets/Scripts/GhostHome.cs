using System.Collections;
using UnityEngine;

public class GhostHome : GhostBehavior
{
    public Transform inside;
    public Transform outsite;
    private void OnEnable()
    {
        StopAllCoroutines();
    }
    private void OnDisable()
    {
        if (this.gameObject.activeSelf){
            StartCoroutine(ExitTransition());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) //funcao que faz o fantasma ir para cima e para baixo quando esta na caixa
    {
        if (this.enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle")){ //verifica se esta em colisao com algum obstaculo
            this.ghost.movement.SetDirection(-this.ghost.movement.direction); //faz o fantasma ir na direcao contraria a que ele estava
        }

    }

    private IEnumerator ExitTransition()
    {
        this.ghost.movement.SetDirection(Vector2.up, true); //forca o fantasma a ir para cima mesmo tendo uma parede
        this.ghost.movement.rigidbody.isKinematic = true; //desabilita a funcao rigidbody
        this.ghost.movement.enabled = false;

        Vector3 position = this.transform.position;
        float duration = 0.5f;
        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(position, this.inside.position, elapsed / duration);
            newPosition.z = position.z;
            this.ghost.transform.position= newPosition;
            elapsed += Time.deltaTime;
            yield return null;

        }

        elapsed = 0.0f;

        while(elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(this.inside.position, this.outsite.position, elapsed / duration);
            newPosition.z = position.z;
            this.ghost.transform.position= newPosition;
            elapsed += Time.deltaTime;
            yield return null;

        }

        this.ghost.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f : 1.0f, 0.0f), true); 
        this.ghost.movement.rigidbody.isKinematic = false; 
        this.ghost.movement.enabled = true;
    }
   
}
