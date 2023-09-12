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
    float tiempoElementosDelay = 12f;
    public bool tutorial = true;
    int leemeDoUnaVez = 1;



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
        atendeElFono = 15f;
        timerLuz = 0;
        apagaLuz = 15f;
        leemeDoUnaVez = 1;
        RandomFono();
        RandomLuz();
        RandomBaldosa();
        SuenaFono();  //ESTE TIENE QUE SER AL PISAR CUALQUIER BALDOSA DESPUES

        telefonoActivo = true;
        luzActivo = false;
        baldosaActivo = false;
        tutorial = true;

        InvokeRepeating("SuenaFono", 0, 1);
        InvokeRepeating("PrendeLuz", 0, 1);
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
            //Debug.Log(cuantoBaldosa);   //////////////////   DEBUG ///////////////////////////////////

            

            if (Input.GetKeyDown(KeyCode.F1))
            {
                CaosNoActivo();
            }

            

            if (tutorial == false)
            {
                while (leemeDoUnaVez == 1) {
                    luzActivo = true;
                    telefonoActivo = true;
                    Invoke("PrendeLuz", 0);
                    Invoke("SuenaFono", 5);
                    Invoke("BaldosaTrue", 6);
                    leemeDoUnaVez = 2;
                }


                if (timerFono >= atendeElFono || timerLuz >= apagaLuz) {
                    CaosActivo();
                    Debug.Log("Caos activo");
                }        // traba anti - caos
                else if (timerFono <= atendeElFono && timerLuz <= apagaLuz)
                {
                    CaosNoActivo();
                    Debug.Log("Caos NO activo");
                }

                tiempoElementos = tiempoElementos + 1f * Time.deltaTime;
                if (tiempoElementos >= tiempoElementosDelay)
                {
                    if (luzActivo == false)
                    {
                        RandomLuz();
                        luzActivo = true;
                    }
                    if (telefonoActivo == false)
                    {
                        RandomFono();
                        telefonoActivo = true;
                    }
                    if (baldosaActivo == false)
                    {
                        RandomBaldosa();
                        baldosaActivo = true;
                    }
                    tiempoElementos = 0f;
                    tiempoElementosDelay = 8f;
                }
            } 


        } else // de obraactiva == true
        {
            Debug.Log("Frena obra");
            baldosaActivo = false;
            luzActivo = false;
            telefonoActivo = false;
            arduinoPort.WriteLine("fono0");
            arduinoPort.WriteLine("luz0");
            ReactivaObra();
        }

    }
    
    void SuenaFono()
    {
        if (telefonoActivo)
        {
            timerFono += Time.deltaTime;
            arduinoPort.WriteLine("fono" + cuantoFono);
            if (cuantoFono == 0)
                {
                arduinoPort.WriteLine("fono0");
                Falsos();
                luzActivo = true;
                Invoke("PrendeLuz", 3);
                telefonoActivo = false;
                timerFono = 0;
            }
            Debug.Log("Suena fono dice: " + cuantoFono);
        }
    }

    void PrendeLuz()
    {
        if (luzActivo)
        {
            timerLuz += Time.deltaTime;
            arduinoPort.WriteLine("luz" + cuantoLuz);
            if (cuantoLuz == 0)
            {
                arduinoPort.WriteLine("luz0");
                Falsos();
                Invoke("BaldosaTrue", 2);  //baldosa
                luzActivo = false;
                baldosaActivo = true;
                timerLuz = 0;
            }
            Debug.Log("Suena luz dice: " + cuantoLuz);
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
           
            Debug.Log("Botón teléfono was pressed.");
            //Debug.Log(cuantoFono);
            SuenaFono();
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

    void BaldosaTrue()
    {
        baldosaActivo = true;
    }

    void Falsos()
    {
        if (luzActivo == false)
        {
            RandomLuz();
        }
        if (telefonoActivo == false)
        {
            RandomFono();
        }
        if (baldosaActivo == false) {
            RandomBaldosa();
        }
    }

    void ReactivaObra() {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            obraActiva = true;
            Debug.Log("Reactivo");
        }
    }

    public void CaosActivo()
    {
            arduinoPort.WriteLine("ca");
            Debug.Log("Arduino escucha caos activo");
    }

    public void CaosNoActivo()
    {
             arduinoPort.WriteLine("cd");
             Debug.Log("Arduino desactivame el motor");
    }

    public void ClosePort() {
        arduinoPort.Close();
    }   
}
