using UnityEngine;

public class Individual
{
    //Valor que simboliza qué tan apto es este individuo.
    public float fitness;
    //Esto también podría ser una lista de cromosomas. Pero por el momento sólo necesitamos uno.
    //También se podría usar uno sólo con muchos genes.
    public Chromosome chromosome;

    public Individual(Transform playerPosition)
    {
        chromosome = new Chromosome();
        fitness = CalculaFitness(playerPosition);
    }

    public float CalculaFitness(Transform playerPosition)
    {
        Vector3 v = new Vector3(chromosome.GetX(), playerPosition.position.y, chromosome.GetZ());
        return Vector3.Distance(playerPosition.position, v);
    }

    public Chromosome GetChromosome()
    {
        return chromosome;
    }

    public void SetChromosome(Chromosome c)
    {
        chromosome = c;
    }
}
