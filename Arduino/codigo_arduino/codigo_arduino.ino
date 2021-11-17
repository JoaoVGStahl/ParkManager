// C++ code
//
#include <Servo.h>
Servo servo_ent;
Servo servo_sai;

bool bcancelaENT = false;
bool bcancelaSAI = false;
const int distancia_min = 5; //Configuração de Distancia Mínima em centimetros

//Sensor
const int TRIG_SAI = 5;
const int ECHO_SAI = 4;
const int TRIG_ENT = 3;
const int ECHO_ENT = 2;

void setup()
{
  Serial.begin(9600);
  servo_ent.attach(11, 500, 2500);
  servo_ent.write(90);

  servo_sai.attach(12, 500, 2500);
  servo_sai.write(90);

  // Configurações do Sensor
  pinMode(TRIG_ENT, OUTPUT);
  pinMode(ECHO_ENT, INPUT);
  pinMode(TRIG_SAI, OUTPUT);
  pinMode(ECHO_SAI, INPUT);
}

void loop()
{
  int distancia_ENT = sensor_morcego(TRIG_ENT, ECHO_ENT);
  int distancia_SAI = sensor_morcego(TRIG_SAI, ECHO_SAI);
  if (distancia_ENT >= distancia_min)
  {
    if(bcancelaENT){
      cancelaENT(90);
      bcancelaENT = false;
      Serial.write("B");
    }

  }

  if (distancia_SAI >= distancia_min)
  {
    if(bcancelaSAI){
      cancelaSAI(90);
      bcancelaSAI = false;
      Serial.write("B");
    } 

  }

  //---ABERTURA VIA SISTEMA-----
  if (Serial.available())
  {
    switch (Serial.read())
    {
    case 'E':
      cancelaENT(180);
      bcancelaENT = true;
      break;
    case 'S':
      cancelaSAI(180);
      bcancelaSAI = true;
      break;
    }
  }
}

void cancelaENT(int pos)
{
  servo_ent.write(pos);
}
void cancelaSAI(int pos)
{
  servo_sai.write(pos);
}

int sensor_morcego(int pinotrig, int pinoecho)
{
  digitalWrite(pinotrig, LOW);
  delayMicroseconds(2);
  digitalWrite(pinotrig, HIGH);
  delayMicroseconds(10);
  digitalWrite(pinotrig, LOW);

  return pulseIn(pinoecho, HIGH) / 58;
}
