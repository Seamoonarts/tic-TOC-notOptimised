using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class Serial : MonoBehaviour
{
    SerialPort arduinoPort = new SerialPort("COM6");
    int cuantoFono = 0;
    int cuantoLuz = 5;
    public int cuantoBaldosa = 0;
    bool obraActiva = true;
    bool trabaElAnticaos = false;
    float timerFono;
    float timerLuz;
    float atendeElFono;  // para el caos
    float apagaLuz;      // para el caos
    bool telefonoActivo = true;
    bool luzActivo = false;
    public bool baldosaActivo = false;
  //  float tiempo = 0f;
  //  float tiempoDelay = 0.4f; //Leer cada 2 seg el SuenaFono() en el Update
    float tiempoElementos = 0f;
    float tiempoElementosDelay = 15f;
    public bool tutorial = true;
    bool tutoLuzTerminado = false;

    public void Awake() {
        arduinoPort.BaudRate = 9600;
        arduinoPort.Parity = Parity.None;
        arduinoPort.StopBits = StopBits.One;
        arduinoPort.DataBits = 8;
        arduinoPort.Handshake = Handshake.None;
    
    
    }

    // trabaElAnticaos = true;

    
    void Start()
    {
        foreach (string str in SerialPort.GetPortNames())
        {
            Debug.Log(string.Format("Existing COM port: {0}", str));
        }

        arduinoPort.ReadTimeout = 30;
        arduinoPort.Open();
        Debug.Log("ABIERTO COM");

        CaosNoActivo();

        timerFono = 0;
        atendeElFono = 10f;
        timerLuz = 0;
        apagaLuz = 10f;
        RandomFono();
        RandomLuz();
        RandomBaldosa();
        SuenaFono();  //ESTE TIENE QUE SER AL PISAR CUALQUIER BALDOSA DESPUES

        telefonoActivo = true;
        tutorial = true;
        tutoLuzTerminado = false;
    }

    void Update() {
        try
        {
            string dato_recibido = arduinoPort.ReadLine();
            // 1 = RF PRESIONADO.
            if (dato_recibido.Equals("Mensaje: 1")){
                obraActiva = false;
            }

        }catch(System.Exception ex1)
        {
            ex1 = new System.Exception();
        }


        if (obraActiva == true) {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                baldosaActivo = true;
                luzActivo = false;
                telefonoActivo = false;
                arduinoPort.WriteLine("fono0");
                arduinoPort.WriteLine("luz0");
                Debug.Log("Tecla espacio");
            }

            SilencioBruno();
            PressBoton();
            PressSwitch();
            //SuenaFono();
            PrendeLuz();
            //Debug.Log(luzActivo);   //////////////////   DEBUG ///////////////////////////////////

            if (telefonoActivo == true)
            {
                TimerFono();
            }    // timer para el caos luego del tutorial
            else {
                timerFono = 0;
            }

         /*   tiempo = tiempo + 1f * Time.deltaTime;  // delay para no reventar a mensajes de fono a arduino
            if (tiempo >= tiempoDelay)
            {   
                tiempo = 0f;
            }   */

            Falsos();

            if (Input.GetKeyDown(KeyCode.F1))
            {
                CaosNoActivo();
            }

            if (timerFono >= atendeElFono || timerLuz >= apagaLuz) {
                trabaElAnticaos = true;
            }        // traba anti - caos
            else if (timerFono <= atendeElFono && timerLuz <= apagaLuz)
            {
                trabaElAnticaos = false;
            }

            if (tutorial == false)
            {
                PrendeLuz();
                Invoke("SuenaFono", 5);   /// ESTO VA A TRAER PROBLEMAS CUANDO SE DESACTIVE EL TUTORIAL PONER DENTRO DE UN IF
                //AGREGAR EL VOLVERLOS VERDADEROS O SINO NO VA A FUNCIONAR

                tiempoElementos = tiempoElementos + 1f * Time.deltaTime;
                if (tiempoElementos >= tiempoElementosDelay)
                {
                    tiempoElementos = 0f;
                    tiempoElementosDelay = 10f;
                    if (luzActivo == false)
                    {
                        RandomLuz();
                        Invoke("PrendeLuz", cuantoLuz);
                        luzActivo = true;
                    }
                    if (telefonoActivo == false)
                    {
                        RandomFono();
                        Invoke("SuenaFono", cuantoFono);
                        telefonoActivo = true;
                    }
                }
            } 
            else if (tutorial == true && telefonoActivo == false && baldosaActivo == false)
            {
                luzActivo = true;
            }      // Tutorial
            if (tutoLuzTerminado == true) {
                baldosaActivo = true;
                luzActivo = false;
                arduinoPort.WriteLine("fono0");
                arduinoPort.WriteLine("luz0");
                Debug.Log("Baldosa activa");
            }                                                        // Fin tutorial
            



        } else // de obraactiva == true
        {
            Debug.Log("Frena obra");
            ReactivaObra();
        }

    }
    
    void SuenaFono()
    {
        if (cuantoFono == 0)
        {
            arduinoPort.WriteLine("fono0");
            Debug.Log("Suena fono0");
            timerFono = 0 - 2;
            telefonoActivo = false;
        }
        else if (cuantoFono <= 0)
        {
            arduinoPort.WriteLine("fono0");
            cuantoFono = 0;
            telefonoActivo = false;
        }
        else if (cuantoFono == 1)
        {
            arduinoPort.WriteLine("fono1");
            Debug.Log("Suena fono1");
        }
        else if (cuantoFono == 2)
        {
            arduinoPort.WriteLine("fono2");
            Debug.Log("Suena fono2");
        }
        else if (cuantoFono == 3)
        {
            arduinoPort.WriteLine("fono3");
            Debug.Log("Suena fono3");
        }
        else if (cuantoFono == 4)
        {
            arduinoPort.WriteLine("fono4");
            Debug.Log("Suena fono4");
        } else { }
        Debug.Log(cuantoFono);
    }

    void TimerFono()
    {
        timerFono += Time.deltaTime;
    }

    void TimerLuz()
    {
        timerLuz += Time.deltaTime;
    }

    void PrendeLuz()
    {
        if (cuantoLuz == 0)
        {
            arduinoPort.WriteLine("luz0");
            Debug.Log("Prende luz0");
            timerLuz = 0 - 2;
            luzActivo = false;
            tutoLuzTerminado = true;
        }
        if (cuantoLuz <= 0)
        {
            arduinoPort.WriteLine("luz0");
            cuantoLuz = 0;
            luzActivo = false;
        }
        else if (luzActivo == true)
        {
            if (cuantoLuz == 1)
            {
                arduinoPort.WriteLine("luz1");
                Debug.Log("Prende luz1");
            }
            else if (cuantoLuz == 2)
            {
                arduinoPort.WriteLine("luz2");
                Debug.Log("Prende luz2");
            }
            else if (cuantoLuz == 3)
            {
                arduinoPort.WriteLine("luz3");
                Debug.Log("Prende luz3");
            }
            else if (cuantoLuz == 4)
            {
                arduinoPort.WriteLine("luz4");
                Debug.Log("Prende luz4");
            }
        }
        else if (luzActivo == false){
            arduinoPort.WriteLine("luz0");
        }
    }


    void SilencioBruno() {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            cuantoFono = 0;
            cuantoLuz = 0;
            cuantoBaldosa = 0;
            baldosaActivo = false;
            luzActivo = false;
            telefonoActivo = false;
            arduinoPort.WriteLine("fono0");
            arduinoPort.WriteLine("luz0");
            Debug.Log("Z Silencio Bruno");
        }
    }

    void PressBoton()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && cuantoFono >= 1)
        {
            cuantoFono = cuantoFono - 1;
            SuenaFono();
            Debug.Log("Botón teléfono was pressed.");
            Debug.Log(cuantoFono);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)){
            SuenaFono();
            Debug.Log("Botón teléfono was pressed.");
        }

    }

    void PressSwitch()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) )
        {
            if (cuantoLuz >= 1)
            {
                cuantoLuz = cuantoLuz - 1;
                Debug.Log("Switch was pressed.");
                Debug.Log(cuantoLuz);
                PrendeLuz();
            }
            else if (cuantoLuz == 0) {
                luzActivo = false;
            }
        }

    }

    void RandomLuz() {
        cuantoLuz = Random.Range(2, 5);
    }

    void RandomFono()
    {
        cuantoFono = Random.Range(2, 5);
    }

    void RandomBaldosa()
    {
        cuantoBaldosa = Random.Range(2, 5);
    }


    void Falsos()
    {
        if (luzActivo == false)
        {
            RandomLuz();
            arduinoPort.WriteLine("luz0");
        }
        if (telefonoActivo == false)
        {
            RandomFono();
            arduinoPort.WriteLine("fono0");
        }
        if (baldosaActivo == false) {
            RandomBaldosa();
        }
    }

    void ReactivaObra() {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            obraActiva = true;
            trabaElAnticaos = false;
            Debug.Log("Reactivo");
        } else
        {
            CaosNoActivo();
        }
    }

    public void CaosActivo()
    {
        if (obraActiva == true)
        {
            arduinoPort.WriteLine("ca");
            Debug.Log("Arduino escucha caos activo");
        }
    }

    public void CaosNoActivo()
    {
        if (obraActiva == true && trabaElAnticaos == false)
        {
             arduinoPort.WriteLine("cd");
             Debug.Log("Arduino desactivame el motor carajo");
        }
    }

    public void ClosePort() {
        arduinoPort.Close();
    }   
}
