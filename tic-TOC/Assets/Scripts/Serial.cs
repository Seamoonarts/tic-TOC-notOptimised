using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class Serial : MonoBehaviour
{
    SerialPort arduinoPort = new SerialPort("COM3");
    public delegate void EstadoInstalacion();
    private EstadoInstalacion estadoInstalacion;
    int cuantoFono = 2; 
    int cuantoLuz = 1;
    public int cuantoBaldosa = 0;
    float timerFono;
    float timerLuz;
    public float timerBaldosa;
    float atendeElFono;  // para el caos
    float apagaLuz;      // para el caos
    float baldosaFuePisada;
    bool telefonoActivo = true;
    bool luzActivo = false;
    public bool baldosaActivo = false;
    public bool tutorial = true;
    public bool intenso = false;
    int leemeDoUnaVez;
    int leemePerdiUnaVez;
    int leemeTutoUnaVez;
    int leemeInaUnaVez;
    int leemeCDUnaVez;
    int leemeCAUnaVez;
    //int p = 1;

    int timepoTotalObra = 90;
    int tiempoCaosIntenso = 60;
    float repeticionElementos = 8f;

    SpriteRenderer m_SpriteRenderer;
    public SpriteRenderer spriteRenderer;
    public Sprite FondoNegro;
    public Sprite FondoVacio;

    public bool baldosaU = false;
    public bool baldosaV = false;
    public bool baldosaX = false;
    public bool baldosaY = false;

    public bool baldosaA = false;
    public bool baldosaB = false;
    public bool baldosaD = false;
    public bool baldosaE = false;

    public void Awake() {
        arduinoPort.BaudRate = 9600;
        arduinoPort.Parity = Parity.None;
        arduinoPort.StopBits = StopBits.One;
        arduinoPort.DataBits = 8;
        arduinoPort.Handshake = Handshake.None;
    }

    
    void Start()
    {
        foreach (string str in SerialPort.GetPortNames())
        {
            Debug.Log(string.Format("Existing COM port: {0}", str));
        }

        arduinoPort.ReadTimeout = 30;
        arduinoPort.Open();
        Debug.Log("ABIERTO COM");

        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        estadoInstalacion = EstadoTutorial;

        CaosNoActivo();

        timerFono = 0f;
        atendeElFono = 10f;  
        timerLuz = 0f;
        apagaLuz = 10f;
        timerBaldosa = 0f;
        baldosaFuePisada = 10f;
        leemeDoUnaVez = 1;
        leemePerdiUnaVez = 1;
        leemeTutoUnaVez = 1;
        leemeInaUnaVez = 1;
        leemeCDUnaVez = 1;
        leemeCAUnaVez = 1;
      /*  RandomFono();
        RandomLuz();
        RandomBaldosa();*/
      

        telefonoActivo = true;
        luzActivo = false;
        baldosaActivo = false;
        tutorial = true;

        intenso = false;

      // InvokeRepeating("SuenaFono", 0, 1);
       // InvokeRepeating("PrendeLuz", 0, 1);
    }

    void Update() {
        try
        {
            string dato_recibido = arduinoPort.ReadLine();
            // 1 = RF PRESIONADO.
            if (dato_recibido.Equals("Mensaje: 1")) {
                estadoInstalacion = EstadoInactivo;
            }

        } catch (System.Exception ex1)
        {
            ex1 = new System.Exception();
        }

        if (Input.GetKeyDown(KeyCode.Z))  //Silencio Bruno
        {
            estadoInstalacion = EstadoInactivo;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            estadoInstalacion = EstadoTutorial;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ReactivaObra();
        }

        estadoInstalacion();
      //  PressBoton();
      //  PressSwitch();
    }
    
    void SuenaFonoTutorial()
    {
        baldosaV = true;
        if (Input.GetKey(KeyCode.V))  
        {
            telefonoActivo = false;
            luzActivo = true;
        }
    }
    

    void PrendeLuzTutorial()
    {
        baldosaA = true;
        arduinoPort.WriteLine("luz2");
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("apreto a");
            luzActivo = false;
            cuantoLuz = 0;
            baldosaActivo = true;
        }
    }
    
    void PrendeBaldosaTutorial()
    {
        Debug.Log("step tuto");
        luzActivo = false;
        cuantoBaldosa = 2;
        if (Input.GetKey(KeyCode.M))
        {
            baldosaActivo = false;
            cuantoBaldosa = 0;
            estadoInstalacion = EstadoNormal;
        }
    }


    void SuenaFono()
    {
        if (telefonoActivo)
        {
            if (cuantoFono == 1)
            {
                baldosaU = true;
            }
            if (cuantoFono == 2)
            {
                baldosaV = true;
            }
            if (cuantoFono == 3)
            {
                baldosaX = true;
            }
            if (cuantoFono == 4)
            {
                baldosaY = true;
            }


            /*       //   arduinoPort.WriteLine("fono" + cuantoFono);
                   if (cuantoFono == 0)
                   {
                       //  arduinoPort.WriteLine("fono0");
                       FindObjectOfType<AudioManager>().Pause("Llamada");
                       //  Falsos();
                       luzActivo = true;
                       Invoke("PrendeLuz", 3);
                       telefonoActivo = false;
                       timerFono = 0;
                   }
                   //Debug.Log("Suena fono dice: " + cuantoFono);*/
        }
        else {
            FindObjectOfType<AudioManager>().Pause("Llamada");
            Falsos();
            luzActivo = true;
            Invoke("PrendeLuz", 3);
            timerFono = 0;
        }
    }
    

    void PrendeLuz()
    {
        if (luzActivo)
        {
            // arduinoPort.WriteLine("luz" + cuantoLuz);
         /*   if (cuantoLuz == 0)
            {
                //   arduinoPort.WriteLine("luz0");
                //  Falsos();
                Invoke("BaldosaTrue", 2);  //baldosa
                luzActivo = false;
                BaldosaTrue();
                timerLuz = 0;
            }*/

            arduinoPort.WriteLine("luz4");
            if (cuantoFono == 1)
            {
                baldosaA = true;
            }
            if (cuantoFono == 2)
            {
                baldosaB = true;
            }
            if (cuantoFono == 3)
            {
                baldosaD = true;
            }
            if (cuantoFono == 4)
            {
                baldosaE = true;
            }
        }
        
        else if (!baldosaA && !baldosaB && !baldosaD && !baldosaE)
        {
            Debug.Log("Luz off");
            arduinoPort.WriteLine("luz0");
            Invoke("BaldosaTrue", 2);  //baldosa
            Falsos();
            luzActivo = false;
            timerLuz = 0;
        }
    }

    /*
    void PressBoton()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && cuantoFono >= 1)
        {
            cuantoFono = cuantoFono - 1;
            Debug.Log("Bot�n tel�fono was pressed.");
            arduinoPort.WriteLine("parlante");
            SuenaFono();
        }
    }

    void PressSwitch()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow) )
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

    */

    void RandomLuz() {
        cuantoLuz = Random.Range(1, 5);
    }

    void RandomFono()
    {
         cuantoFono = Random.Range(1, 5);
    }

   /* void RandomBaldosa()
    {
      cuantoBaldosa = Random.Range(1, 4);
    }
    */

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
     /*   if (baldosaActivo == false) {
            RandomBaldosa();
        }*/
    }  

    void RandomsYTruesFono()
    {
        if (telefonoActivo == false)
        {
            Invoke("RandomFono", 0);
            Debug.Log("Random Fono=" + cuantoFono);
            telefonoActivo = true;
            FindObjectOfType<AudioManager>().Play("Llamada");
        }
    }

    void RandomsYTruesLuz()
    {
        if (luzActivo == false)
        {
            RandomLuz();
            Debug.Log("Random Luz=" + cuantoLuz);
            luzActivo = true;
        }
    }

    void RandomsYTruesBaldosa()
    {
        if (baldosaActivo == false)
        {
            //  RandomBaldosa();
            cuantoBaldosa = 3;
            baldosaActivo = true;
        }
    }

    void ReactivaObra() {
            estadoInstalacion = EstadoTutorial;

            CaosNoActivo();
            RandomFono();
            RandomLuz();
          //  RandomBaldosa();

            estadoInstalacion = EstadoTutorial;
            timerFono = 0f;
            atendeElFono = 15f;
            timerLuz = 0f;
            apagaLuz = 15f;
            leemeDoUnaVez = 1;
            leemePerdiUnaVez = 1;
            leemeTutoUnaVez = 1;
            leemeCDUnaVez = 1;
            leemeCAUnaVez = 1;
            leemeInaUnaVez = 1;

            telefonoActivo = true;
            luzActivo = false;
            baldosaActivo = false;
            tutorial = true;
            FindObjectOfType<AudioManager>().Play("Base");
            FindObjectOfType<AudioManager>().Play("Llamada");
            Debug.Log("Reactivo");
    }

    public void CaosActivo()
    {
        FindObjectOfType<AudioManager>().Play("Caos");
        arduinoPort.WriteLine("ca");
        Debug.Log("CON motor");
    }

    public void CaosNoActivo()
    {
        
        arduinoPort.WriteLine("cd");
        Debug.Log("SIN motor");
    }

    public void CaosActivoParaBaldosa()
    {
        if (estadoInstalacion != EstadoInactivo) {
            arduinoPort.WriteLine("ca");
            Debug.Log("Prende motor");
        }
    }

    void LlamoPerdicion()
    {
        estadoInstalacion = EstadoPerdicion;
    }

    void LlamoInactivo()
    {
        estadoInstalacion = EstadoInactivo;
        cuantoBaldosa = 0;
        cuantoFono = 0;
        cuantoLuz = 0;
    }

    public void EstadoInactivo()
    {
        while (leemeInaUnaVez == 1)
        {
            cuantoBaldosa = 0;
            cuantoFono = 0;
            cuantoLuz = 0;
            baldosaActivo = false;
            luzActivo = false;
            telefonoActivo = false;
            intenso = false;
           // arduinoPort.WriteLine("fono0");
            arduinoPort.WriteLine("luz0");
            CaosNoActivo();
            FindObjectOfType<AudioManager>().Stop("Perdicion");
            FindObjectOfType<AudioManager>().Stop("CaosIntenso");
            FindObjectOfType<AudioManager>().Stop("Caos");
            FindObjectOfType<AudioManager>().Stop("Base");
            FindObjectOfType<AudioManager>().Stop("Llamada");
            CancelInvoke("RandomsYTruesFono");
            CancelInvoke("RandomsYTruesLuz");
            CancelInvoke("RandomsYTruesBaldosa");
            spriteRenderer.sprite = FondoNegro;
            leemeInaUnaVez = 2;
        }
        Debug.Log("Frena obra");
        Invoke("ReactivaObra", 30);   // TIEMPO DE DESCANSO
    } 

    public void EstadoTutorial()
    {
        if (telefonoActivo)
        {
            SuenaFonoTutorial();
        } else if (luzActivo)
        {
            Debug.Log("lee luzact");
            PrendeLuzTutorial();
        } else if (baldosaActivo)
        {
            PrendeBaldosaTutorial();
        }
        
        while (leemeTutoUnaVez == 1)
        {
            spriteRenderer.sprite = FondoVacio;
            FindObjectOfType<AudioManager>().Stop("Caos");
            FindObjectOfType<AudioManager>().Stop("CaosIntenso");
            FindObjectOfType<AudioManager>().Stop("Perdicion");
            leemeTutoUnaVez = 2;
            leemeDoUnaVez = 1;
        }
    }

    public void EstadoNormal()
    {
        while (leemeDoUnaVez == 1)
        {
            spriteRenderer.sprite = FondoVacio;
            InvokeRepeating("RandomFono", 2f, repeticionElementos);
            InvokeRepeating("RandomLuz", 2f, repeticionElementos);
            InvokeRepeating("RandomsYTruesFono", 2f, repeticionElementos);
            InvokeRepeating("RandomsYTruesLuz", 2f, repeticionElementos);
            InvokeRepeating("RandomsYTruesBaldosa", 14f, repeticionElementos);
            InvokeRepeating("SuenaFono", 0, 8);
            InvokeRepeating("PrendeLuz", 0, 10);
            InvokeRepeating("Falsos", 0, 2);
            Invoke("CaosIntenso", tiempoCaosIntenso);                            //// CAMBIAR LLAMADO FIN DE OBRA E INTENSIDAD CUANDO REGULE LA DURACION DE LA OBRA
            Invoke("LlamoPerdicion", timepoTotalObra);
            leemeDoUnaVez = 2;
            leemeTutoUnaVez = 1;
        }


        if (timerFono <= atendeElFono && timerLuz <= apagaLuz && timerBaldosa <= baldosaFuePisada)
        {
            while (leemeCDUnaVez == 1)
            {
                CaosNoActivo();
                leemeCDUnaVez = 2;
                leemeCAUnaVez = 1;
            }
            
            if (telefonoActivo == true) {
                timerFono += Time.deltaTime;
            }
            if (luzActivo == true)
            {
                timerLuz += Time.deltaTime;
            }
            if (baldosaActivo == true)
            {
                timerBaldosa += Time.deltaTime;
            }
        }
        else
        {
            while (leemeCAUnaVez == 1)
            {
                CaosActivo();
                leemeCAUnaVez = 2;
                leemeCDUnaVez = 1;
            }
        }

      
    }


    public void CaosIntenso()
    {
        intenso = true;
        FindObjectOfType<AudioManager>().Play("Base");
        FindObjectOfType<AudioManager>().Play("CaosIntenso");
    }

    public void EstadoPerdicion()
    {
        while (leemePerdiUnaVez == 1)
        {
            FindObjectOfType<AudioManager>().Play("Caos");
            FindObjectOfType<AudioManager>().Play("Perdicion");
            leemePerdiUnaVez = 2;
        }

        baldosaActivo = true;
        luzActivo = true;
        telefonoActivo = true;

        Invoke("LlamoInactivo", 12);
    } 

    public void ClosePort() {
        arduinoPort.Close();
    }   
}
