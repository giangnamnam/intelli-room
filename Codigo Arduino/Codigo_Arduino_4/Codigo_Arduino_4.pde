/*
IntelliRoom Arduino 3.2
*/
#include <Messenger.h> //importamos una libreria para hacer más fácil el soporte de mensajes

Messenger message = Messenger(); //iniciamos los mensajes

//Configuracion de pines
int pinLedR = 9;  //Red LED
int pinLedG = 10; //Green LED
int pinLedB = 11; //Blue LED

int dev0 = 2;  //Dispositivo electrico 0
int dev1 = 4;  //Dispositivo electrico 1
int dev2 = 7;  //Dispositivo electrico 2
int dev3 = 0;  //Dispositivo electrico 3
int dev4 = 0;  //Dispositivo electrico 4
int dev5 = 0;  //Dispositivo electrico 5
int dev6 = 0;  //Dispositivo electrico 6
int dev7 = 0;  //Dispositivo electrico 7
int dev8 = 0;  //Dispositivo electrico 8
int dev9 = 0;  //Dispositivo electrico 9

//Configuracion de variables: para el degradado de colores, funcion DEGRADRED
int rInit = 0;  //Valor rojo inicial
int gInit = 0;  //Valor verde inicial
int bInit = 0;  //Valor azul inicial

int rNow = 0;  //Valor rojo actual
int gNow = 0;  //Valor verde actual
int bNow = 0;  //Valor azul actual

int rEnd = 0;   //Valor rojo final
int gEnd = 0;   //Valor verde final
int bEnd = 0;   //Valor azul final

unsigned long timeInit = 0; //tiempo inicial
unsigned long timeNow = 0;  //tiempo actual
unsigned long timeEnd = 0;  //tiempo final

//Variales para el uso de la funcion RANDOM
boolean randomMode = false;
unsigned long timeRandom = 0;

void setup()
{
  //pongo el puerto serie a 9600 baudios
  Serial.begin(9600);
  message.attach(messageReady);
  
  //iniciamos los pines digitales
  pinMode(dev0, OUTPUT);
  pinMode(dev1, OUTPUT);
  pinMode(dev2, OUTPUT);
  pinMode(dev3, OUTPUT);
  pinMode(dev4, OUTPUT);
  pinMode(dev5, OUTPUT);
  pinMode(dev6, OUTPUT);
  pinMode(dev7, OUTPUT);
  pinMode(dev8, OUTPUT);
  pinMode(dev9, OUTPUT);
  
  //por defecto iluminamos a este color (en un futuro lo pondremos a negro)
  SetColor(1,1,1);
}

void loop()
{
   if (Serial.available()){
     message.process(Serial.read());
   }
     
   timeNow = millis();
   if(timeEnd > timeNow) //si tiempo actual es menor que tiempo final entonces encontramos en una situacion de degradado
   {
     rNow = CalculeValue(rInit,rEnd); //Calculamos la componente roja
     gNow = CalculeValue(gInit,gEnd); //Calculamos la componente verde
     bNow = CalculeValue(bInit,bEnd); //Calculamos la componente azul
     SetColor(rNow,gNow,bNow); //la imprimimos en los LEDs
   }
   
   if(randomMode && (timeEnd < timeNow))
   {
     timeInit = millis();
     timeEnd = millis() + timeRandom;
     ConfigRandomColor();
   }
   //espero 30 milisegundo (esto no desabilita el Rx)
   //delay(50);
}

void SetColor(int r, int g, int b)
{
  analogWrite(pinLedR, r); //metemos valor en el PWM asignado al valor r
  analogWrite(pinLedG, g); //metemos valor en el PWM asignado al valor g
  analogWrite(pinLedB, b); //metemos valor en el PWM asignado al valor b
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
     delay(10);
     gEnd = random(256);
     delay(10);
     bEnd = random(256);
}

int CalculeValue(int colorInit, int colorEnd)
{  
  return abs((timeNow-timeInit)*(colorEnd-colorInit)/(timeEnd-timeInit))+colorInit;
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
      rInit = message.readInt();
      gInit = message.readInt();
      bInit = message.readInt();
      timeEnd = millis();
      randomMode = false;
      SetColor(rInit,gInit,bInit);
    }
    //Modo degradado
    if (message.checkString("DEGRADED"))
    {
      rInit = rNow;
      gInit = gNow;
      bInit = bNow;
      rEnd = message.readInt();
      gEnd = message.readInt();
      bEnd = message.readInt();
      timeInit = millis();
      randomMode = false;
      timeEnd = timeInit + message.readLong();
    }
    //Modo Aleatorio (RANDOM 0/1 timeRandom)
    if (message.checkString("RANDOM"))
    {
      int randValue = message.readInt(); 
      //Activar funcion RANDOM
      if (randValue == 1)
        { 
          randomMode = true;
          timeRandom = message.readLong();
          ConfigRandomColor();
        }
      //Desactivar modo RANDOM (no importa el valor del tiempo)
      if (randValue == 0)
        {
          randomMode = false;
        }
    }
    
    //METODOS DISPOSITIVO
    //Encender dispositivo
    if (message.checkString("SWITCHON"))
    {
     int device = message.readInt(); 
     if (device == 0){ digitalWrite(dev0, HIGH); }
     if (device == 1){ digitalWrite(dev1, HIGH); }
     if (device == 2){ digitalWrite(dev2, HIGH); }
     if (device == 3){ digitalWrite(dev3, HIGH); }
     if (device == 4){ digitalWrite(dev4, HIGH); }
     if (device == 5){ digitalWrite(dev5, HIGH); }
     if (device == 6){ digitalWrite(dev6, HIGH); }
     if (device == 7){ digitalWrite(dev7, HIGH); }
     if (device == 8){ digitalWrite(dev8, HIGH); }
     if (device == 9){ digitalWrite(dev9, HIGH); }
    }
    //Apagar dispositivo
    if (message.checkString("SWITCHOFF"))
    {
     int device = message.readInt(); 
     if (device == 0){ digitalWrite(dev0, LOW); }
     if (device == 1){ digitalWrite(dev1, LOW); }
     if (device == 2){ digitalWrite(dev2, LOW); }
     if (device == 3){ digitalWrite(dev3, LOW); }
     if (device == 4){ digitalWrite(dev4, LOW); }
     if (device == 5){ digitalWrite(dev5, LOW); }
     if (device == 6){ digitalWrite(dev6, LOW); }
     if (device == 7){ digitalWrite(dev7, LOW); }
     if (device == 8){ digitalWrite(dev8, LOW); }
     if (device == 9){ digitalWrite(dev9, LOW); }
    }
    
    if(message.checkString("CHECK"))
    {
        Serial.write("ACK");
    }
    
    //vaciamos lo que tengamos en el puerto serie
    Serial.flush();
  }
}

