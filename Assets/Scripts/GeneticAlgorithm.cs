using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Este script s� podr�a heredar de MonoBehaviour, puesto que as� podr�amos arrastrarlo
//a un objeto y probarlo.

public class GeneticAlgorithm : MonoBehaviour
{
    //Cantidad de individuos en este algoritmo.
    public int individualCount;
    //Cantidad de genes que va a contener cada individuo.
    public int genesCount;
    //Lista de todos los individuos en este algoritmo.
    public ArrayList allIndividuals;
    //Crea la poblaci�n inicial.
    public Transform playerPos;

    public void CreateInitialPopulation()
    {
        for (int i = 0; i < 100; i++)
        {
            allIndividuals.Add(new Individual(playerPos));
        }
    }

    //Devuelve una lista de los mejores padres. Para este ejemplo deber�a devolver 2 individuos,
    //pero puede devolver la cantidad que sea necesaria.
    public Individual[] SelectionProcess()
    {
        return null;
    }

    //Inicia el proceso de cruce. En este caso recibe una madre y un padre, pero puede
    //recibir tambi�n una lista de padres (no necesariamente debe respetar el proceso natural).
    public Individual[] CrossOver(Individual mother, Individual father)
    {
        return null;
    }

    //Inicia el proceso de cruce. En este caso recibe una madre y un padre, pero puede
    //recibir tambi�n una lista de padres (no necesariamente debe respetar el proceso natural).
    public void Mutation(Individual son1, Individual son2)
    {
        float prob = Random.RandomRange(0.0f, 1.0f);

        if (prob < 0.2)
        {
            int m = Random.RandomRange(-7, 7);
            son1.chromosome.SetX(son1.chromosome.GetX() + m);
            son1.chromosome.SetX(son1.chromosome.GetX() + m);
        }
    }

    //Como su nombre lo indica, se encarga de llamar a las dos funciones anteriores para crear
    //una nueva poblaci�n. No es necesario eliminar directamente a la poblaci�n actual, tambi�n se
    //puede sobreescribir los valores de sus genes.
    public void BasicGenetic()
    {
        /*for (int i = 0; i < length; i++)
        {

        }*/
        CreateInitialPopulation();
        Individual[] padres = SelectionProcess();
        Individual[] hijos = CrossOver(padres[0], padres[1]);
        Mutation(hijos[0], hijos[1]);

    }
}
