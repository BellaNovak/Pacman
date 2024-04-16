using UnityEngine;

public class GhostScatter : GhostBehavior
{
    private void OnDisable()
    {
        this.ghost.chase.Enable();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if(node != null && this.enabled && !this.ghost.frightened.enabled)
        {
            int index = Random.Range(0, node.availableDirections.Count); //verifica quantas direcoes estao livres e escolhe uma aleatoria

            if(node.availableDirections[index] == -this.ghost.movement.direction && node.availableDirections.Count > 1) //essa verificacao evita backtracking a nao ser que seja a unuca opcao
            {
                index++;

                if(index >= node.availableDirections.Count){ //evita de ter overflow
                    index = 0;
                }
            }

            this.ghost.movement.SetDirection(node.availableDirections[index]);
        }

    }
}
