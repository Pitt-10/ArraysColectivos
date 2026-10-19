using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColectivosManager : MonoBehaviour
{

    [SerializeField] ColectivoScript[] colectivos;
    [SerializeField] float maxCapacidad = 60;
    [SerializeField] float minCapacidad = 30;

    private bool isActivated;

    
    // Start is called before the first frame update
    void Start()
    {
        colectivos = FindObjectsOfType<ColectivoScript>();
        DesactivarTodosLosColectivos();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ActivarTodosLosColectivos();
            AsignarCapacidadRandomALosColectivos();
            CargarCombustibleRandomATodosLosColectivos();
            DesactivarTodosLosColectivos();
            ActivarColectivosConMenosDeMedioTanque();
            ActivarSignoDeAdvertenciaEnColectivosConMenosDe10DeCombustible();
            MostrarPromedioDeCombustible();
            MostrarCombustibleFaltanteParaLlenarTodosLosTanques();
        }
    }

    private void ActivarTodosLosColectivos()
    {
        for (int i = 0; i < colectivos.Length; i++) { 
            colectivos[i].gameObject.SetActive(true);
        }
    }

    private void DesactivarTodosLosColectivos()
    {
        for (int i = 0; i < colectivos.Length; i++) { 
            colectivos[i].gameObject.SetActive(false);
        }
    }

    private void AsignarCapacidadRandomALosColectivos()
    {
         //respetar los valores max y min establecidos en este script
        for (int i = 0; i < colectivos.Length; i++) {
            colectivos[i].capacidadTanque = Random.Range(minCapacidad, maxCapacidad);
        }
    }

    private void CargarCombustibleRandomATodosLosColectivos()
    {
        //cargar una cantidad aleatoria de combustible a cada colectivo
        //respetando la capacidad máxima del tanque
        for (int i = 0; i < colectivos.Length; i++) {
            colectivos[i].cargaActual = Random.Range(0, colectivos[i].capacidadTanque);
        }
    }
    private void ActivarColectivosConMenosDeMedioTanque()
    {
        for (int i = 0; i < colectivos.Length; i++) {
            if (colectivos[i].cargaActual < colectivos[i].capacidadTanque / 2) {
                colectivos[i].gameObject.SetActive(true);
            }
        }
    }


    private void ActivarSignoDeAdvertenciaEnColectivosConMenosDe10DeCombustible()
    {
        foreach (ColectivoScript colectivo in colectivos) {
            if (colectivo.cargaActual < 10f) {
                colectivo.WarningSign.SetActive(true);
            }
        }
    }

    private void MostrarPromedioDeCombustible()
    {
        //mostrar en consola el promedio de combustible de todos los colectivos
        float cargaTotalColectivos = 0;
        foreach (ColectivoScript colectivo in colectivos) {
            cargaTotalColectivos += colectivo.cargaActual;
        }
        Debug.Log("El promedio de combustible de todos los colectivos es: " + cargaTotalColectivos / colectivos.Length);
    }

    private void MostrarCombustibleFaltanteParaLlenarTodosLosTanques()
    {
        //mostrar en consola la cantidad de combustible necesaria para llenar todos los tanques
        //de todos los colectivos
        foreach (ColectivoScript colectivo in colectivos) {
            Debug.Log("Cantidad necesaria para llenar el tanque del  " + colectivo + ": " + (colectivo.capacidadTanque - colectivo.cargaActual));
        }
    }
}
