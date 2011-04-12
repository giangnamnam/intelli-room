/*
IntelliRoom Arudino 2.0
*/
#include <Messenger.h> //importamos una libreria para hacer mas facil el soporte de mensajes

Messenger message = Messenger();

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
byte rInit = 0;  //Valor rojo inicial
byte gInit = 0;  //Valor verde inicial
byte bInit = 0;  //Valor azul inicial

byte rNow = 0;  //Valor rojo actual
byte gNow = 0;  //Valor verde actual
byte bNow = 0;  //Valor azul actual

byte rEnd = 0;   //Valor rojo final
byte gEnd = 0;   //Valor verde final
byte bEnd = 0;   //Valor azul final

unsigned int timeNow = 0;  //tiempo actual
unsigned int timeEnd = 0;  //tiempo final

void setup()
{
  //pongo el puerto serie a 9600 baudios
  Serial.begin(9600);
  message.attach(messageReady);
  
  //por defecto iluminamos a este color (en un futuro lo pondremos a negro)
  SetColor(128,128,128);
}

void loop()
{
   while ( Serial.available() )  message.process(Serial.read ());
   
   if(timeEnd > timeNow)
   {
     rNow = CalculeValue(rInit,rEnd); //Calculamos la componente roja
     gNow = CalculeValue(gInit,gEnd); //Calculamos la componente verde
     bNow = CalculeValue(bInit,bEnd); //Calculamos la componente azul
     SetColor(rNow,gNow,bNow); //la imprimimos en los LEDs
     timeNow++; //aumentamos el tiempo
   }
   
}

void SetColor(byte r, byte g, byte b)
{
  analogWrite(pinLedR, r); //metemos valor en el PWM asignado al valor r
  analogWrite(pinLedG, g); //metemos valor en el PWM asignado al valor g
  analogWrite(pinLedB, b); //metemos valor en el PWM asignado al valor b
  rNow = r;
  gNow = g;
  bNow = b;
}

byte CalculeValue(byte initColor, byte endColor)
{
  return (timeNow*(endColor-initColor)/timeEnd)+initColor; //hacemos el calculo del valor actual por medio de una formula (documentado en la documentacion)
}

void messageReady()
{
  if ( message.available() )
  {   
    //modo directo
    if( message.checkString("DIRECT"))
    {
      rInit = message.readInt();
      gInit = message.readInt();
      bInit = message.readInt();
      timeNow = 0;
      timeEnd = 0;
      SetColor(rInit,gInit,bInit);
    }
    
    //modo degradado
    if (message.checkString("DEGRADED"))
    {
      rEnd = message.readInt();
      gEnd = message.readInt();
      bEnd = message.readInt();
      timeNow = 0;
      timeEnd = message.readInt();
      SetColor(rEnd,gEnd,bEnd);
    }
    
    //modo encender dispositivo
    if( message.checkString("SWITCHON"))
    {
     int device = message.readInt(); 
     if(device == 0){ digitalWrite(dev0, HIGH); }
     if(device == 1){ digitalWrite(dev1, HIGH); }
     if(device == 2){ digitalWrite(dev2, HIGH); }
     if(device == 3){ digitalWrite(dev3, HIGH); }
     if(device == 4){ digitalWrite(dev4, HIGH); }
     if(device == 5){ digitalWrite(dev5, HIGH); }
     if(device == 6){ digitalWrite(dev6, HIGH); }
     if(device == 7){ digitalWrite(dev7, HIGH); }
     if(device == 8){ digitalWrite(dev8, HIGH); }
     if(device == 9){ digitalWrite(dev9, HIGH); }
    }
    
    if( message.checkString("SWITCHOFF"))
    {
     int device = message.readInt(); 
     if(device == 0){ digitalWrite(dev0, LOW); }
     if(device == 1){ digitalWrite(dev1, LOW); }
     if(device == 2){ digitalWrite(dev2, LOW); }
     if(device == 3){ digitalWrite(dev3, LOW); }
     if(device == 4){ digitalWrite(dev4, LOW); }
     if(device == 5){ digitalWrite(dev5, LOW); }
     if(device == 6){ digitalWrite(dev6, LOW); }
     if(device == 7){ digitalWrite(dev7, LOW); }
     if(device == 8){ digitalWrite(dev8, LOW); }
     if(device == 9){ digitalWrite(dev9, LOW); }
    }
  }
}


