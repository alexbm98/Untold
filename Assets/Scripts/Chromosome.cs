using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chromosome
{
    //Lista de valores. Cada uno representa una solución a un problema.
    //Generalmente se rellena con valores entre -1 y 1.
    public int[] genes;
    //No es "completamente" necesario, pero sirve para crear la lista
    //de valores inicial. Le pasamos por parámetro la cantidad de genes.
    //Deberíamos usar un gen para cada problema a resolver.
    //Por ejemplo, si lo querémos usar como dirección, necesitamos 3:
    //x, y, z.

    public Chromosome()
    {
        genes = new int [2];

        genes[0] = Random.Range(5, 70);
        genes[1] = Random.Range(5, 70);
    }

    public int GetX()
    {
        return genes[0];
    }

    public int GetZ()
    {
        return genes[1];
    }

    public void SetX(int x)
    {
        genes[0] = x;
    }

    public void SetZ(int z)
    {
        genes[1] = z;
    }
}
