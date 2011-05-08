/*
IntelliRoom Arduino 0.70
*/

#include <Messenger.h> //importamos una librería para hacer más fácil el soporte de mensajes

//Configuración de pines
#define PINLEDR 9   //Red LED
#define PINLEDG 10  //Green LED
#define PINLEDB 11  //Blue LED

//Configuramos los dispositivos
#define arrayLength 10  //Numero de dispositivos
uint8_t devices[] = {2, 4, 7, 12, 13, 0, 0, 0, 0, 0}; //Salidas de cada uno de los dispositivos

//Configuración de variables: para el degradado de colores, función GRANDIENT
uint8_t rInit = 0;  //Valor rojo inicial
uint8_t gInit = 0;  //Valor verde inicial
uint8_t bInit = 0;  //Valor azul inicial

uint8_t rNow = 0;  //Valor rojo actual
uint8_t gNow = 0;  //Valor verde actual
uint8_t bNow = 0;  //Valor azul actual

uint8_t rEnd = 0;   //Valor rojo final
uint8_t gEnd = 0;   //Valor verde final
uint8_t bEnd = 0;   //Valor azul final

unsigned long timeInit = 0; //tiempo inicial
unsigned long timeNow = 0;  //tiempo actual
unsigned long timeEnd = 0;  //tiempo final

//Variables para el uso de la función RANDOM
unsigned long timeRandom = 0;

//Iniciamos mensajes
Messenger message = Messenger();

void setup()
{
  Serial.begin(9600);//configuro el puerto serie a 9600 baudios

  message.attach(messageReady);//pongo la funcion callback de message

  //iniciamos los pines digitales
  for(int i = 0; i < arrayLength; i++)
    pinMode(devices[i], OUTPUT);
}

void loop()
{
  while(Serial.available()){
    message.process(Serial.read());
  }

  timeNow = millis();

  if(timeEnd>timeNow) //si tiempo actual es menor que tiempo final entonces encontramos en una situación de degradado
  {
    UpdateValues(); //Calculamos las componentes
    SetColor(rNow,gNow,bNow); //la imprimimos en los LEDs
  }
  else
  {
    if(timeRandom!=0)
    {
      timeInit = millis();
      timeEnd = timeInit + timeRandom;
      ConfigRandomColor();
    }
    else // llegados aquí es que todo proceso ha terminado, así que asignamos al valor final.
    {
      SetColor(rEnd,gEnd,bEnd);
    }  
  }
}

void SetColor(int r, int g, int b)
{
  analogWrite(PINLEDR, r); //metemos valor en el PWM asignado al valor r
  analogWrite(PINLEDG, g); //metemos valor en el PWM asignado al valor g
  analogWrite(PINLEDB, b); //metemos valor en el PWM asignado al valor b
  rNow = r;
  gNow = g;
  bNow = b;
}

void ConfigRandomColor()
{
  rInit = rEnd;
  gInit = gEnd;
  bInit = bEnd;

  rEnd = random(256);
  delay(5);
  gEnd = random(256);
  delay(5);
  bEnd = random(256);
}

void UpdateValues()
{  
  int time1 = timeNow-timeInit;
  int time2 = timeEnd-timeInit;

  float time3 = ((float) time1)/time2;

  rNow = time3*(rEnd-rInit)+rInit;
  gNow = time3*(gEnd-gInit)+gInit;
  bNow = time3*(bEnd-bInit)+bInit;
}

//Metodo que contiene las funciones a ejecutar
void messageReady()
{
  if (message.available() )
  { 
    //METODOS DE COLOR
    //Modo directo
    if (message.checkString("DIRECT"))
    {
      rEnd = message.readInt();
      gEnd = message.readInt();
      bEnd = message.readInt();
      timeEnd = millis();
      timeRandom = 0;
      SetColor(rEnd,gEnd,bEnd);
    }

    //Modo degradado
    else if (message.checkString("GRADIENT"))
    {
      rInit = rNow;
      gInit = gNow;
      bInit = bNow;
      rEnd = message.readInt();
      gEnd = message.readInt();
      bEnd = message.readInt();
      timeInit = millis();
      timeRandom = 0;
      timeEnd = timeInit + message.readLong();
    }

    //Modo Aleatorio (RANDOM 0/1 timeRandom)
    else if (message.checkString("RANDOM"))
    {
      timeRandom = message.readLong();
      //Activar funcion RANDOM
      if (timeRandom != 0)
      { 
        rEnd=rNow;
        gEnd=gNow;
        bEnd=bNow;
        ConfigRandomColor();
      }
    }

    //METODOS DISPOSITIVO
    //Encender dispositivo
    else if (message.checkString("SWITCHON"))
    {
      int device = message.readInt(); 
      if(device >= 0 && device < arrayLength)
        digitalWrite(devices[device], HIGH); //activa la señal del device
    }

    //Apagar dispositivo
    else if (message.checkString("SWITCHOFF"))
    {
      int device = message.readInt(); 
      if(device >= 0 && device < arrayLength)
        digitalWrite(devices[device], LOW); //desactiva la señal del device
    }

    // METODO DETECCION
    else if (message.checkString("CHECK"))
    {
      Serial.write("ACK\r\n"); //devuelve el mensaje de comprobación
    }
  }
}


