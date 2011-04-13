/*
IntelliRoom
Lampara con PWM
*/
#include <Messenger.h> //importamos una libreria para hacer mas facil el soporte de mensajes

Messenger message = Messenger();

int pinLedR = 9;  //Red LED
int pinLedG = 10; //Green LED
int pinLedB = 11; //Blue LED

int disp1 = 2;  //Dispositivo electrico 1
int disp2 = 4;  //Dispositivo electrico 2
int disp3 = 7;  //Dispositivo electrico 3

void setup()
{
  //pongo el puerto serie a 9600 baudios
  Serial.begin(9600);
  message.attach(messageReady);
  
  //por defecto iluminamos a este color (en un futuro lo pondremos a negro)
  SetColor(128,128,128);
}

void SetColor(byte r, byte g, byte b)
{
  analogWrite(pinLedR, r);
  analogWrite(pinLedG, g);
  analogWrite(pinLedB, b);
}

void messageReady()
{
  if ( message.available() )
  {   
    //modo directo
    if( message.checkString("DIRECT") )
    {
      int r = message.readInt();
      int g = message.readInt();
      int b = message.readInt();
      SetColor(r,g,b);
    }
    
    //modo encender dispositivo
    if( message.checkString("SWITCHON"))
    {
      
    }
  
  }
}

void loop()
{
   while ( Serial.available() )  message.process(Serial.read () );
}

