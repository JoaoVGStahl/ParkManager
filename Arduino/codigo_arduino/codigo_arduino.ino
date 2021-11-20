// Inicializa os Servos.
#include <Servo.h>
Servo servo_ent;
Servo servo_sai;

//Bool de controle das cancelas (aberto ou fechado).
bool cancelaENT = false;
bool cancelaSAI = false;

//Bool de controle. Detecção de veiculo
bool haVeiculoENT = false;
bool haVeiculoSAI = false;
//dsad

//Define se o sistema esta operando (I= Inicia - P= Para)
char inicio = "P";

//Configuração de Distancia Mínima em centimetros.
const int distancia_min = 10;
int distancia_ENT = 81;
int distancia_SAI = 81;

//Sensor ultrasonico (Definição de portas para cada pino do sensor)
const int ECHO_ENT = 2;
const int TRIG_ENT = 3;
const int ECHO_SAI = 4;
const int TRIG_SAI = 5;

void setup()
{
  Serial.begin(9600);

  //Servo entrada
  servo_ent.attach(11, 500, 2500);
  servo_ent.write(90);

  //Servo saida
  servo_sai.attach(10, 500, 2500);
  servo_sai.write(90);

  //Configurações do Sensor
  pinMode(TRIG_ENT, OUTPUT);
  pinMode(ECHO_ENT, INPUT);
  pinMode(TRIG_SAI, OUTPUT);
  pinMode(ECHO_SAI, INPUT);
}

void loop()
{
  if (inicio == "I")
  {
    distancia_ENT = calculaDistancia(TRIG_ENT, ECHO_ENT);
    distancia_SAI = calculaDistancia(TRIG_SAI, ECHO_SAI);

    //Fecha cancela Entrada *AUTOMATICO*
    if (distancia_ENT >= distancia_min)
    {
      if (cancelaENT)
      {
        moveCancelaEnt('F');
      }
    }
    else
    {
      Serial.write(5); //Informa o sistema que HÁ veiculo na cancela ENTRADA
    }

    //Fecha cancela Saida *AUTOMATICO*
    if (distancia_SAI >= distancia_min)
    {
      if (cancelaSAI)
      {
        moveCancelaSai('F');
      }
    }
    else
    {
      Serial.write(7); //Informa o sistema que HÁ veiculo na cancela SAIDA
    }

    //---ABERTURA VIA COMUNICAÇÃO SERIAL---//
    if (Serial.available())
    {
      switch (Serial.read())
      {
      case 'E': // E - Abre a cancela ENTRADA
        moveCancelaEnt('A');
        break;
      case 'S': // S - Abre a cancela SAIDA
        moveCancelaSai('A');        
        break;
      }
    }
  }
  else
  {
    //Sistema Parado
  }
}

void moveCancelaEnt(char flag)
{
  switch (flag)
  {
  case 'A':
    if (!(distancia_ENT >= distancia_min))
    {
      servo_ent.write(0);
      Serial.write(1); //Informa o sistema sobre a abertura da cancela ENTRADA
    }
    else
    {
      Serial.write(6); //Informa o sistema que NÃO há veiculo na cancela ENTRADA
    }
    break;

  case 'F':
    delay(3000);
    servo_ent.write(90);
    Serial.write(2); //Informa o sistema sobre o fechamento da cancela ENTRADA
    cancelaENT = false;
    break;
  }
}

void moveCancelaSai(char flag)
{
  switch (flag)
  {
  case 'A':
    if (!(distancia_SAI >= distancia_min))
    {
      servo_sai.write(0);
      Serial.write(3); //Informa o sistema sobre a abertura da cancela SAIDA
      cancelaSAI = true;
    }
    else
    {
      Serial.write(8); //Informa o sistema que não há veiculo na cancela SAIDA
    }
    break;

  case 'F':
    delay(3000);
    servo_sai.write(90);
    Serial.write(4); //Informa o sistema sobre o fechamento da cancela SAIDA
    cancelaSAI = false;    
    break;
  }
}

int calculaDistancia(int pinotrig, int pinoecho)
{
  digitalWrite(pinotrig, LOW);
  delayMicroseconds(2);
  digitalWrite(pinotrig, HIGH);
  delayMicroseconds(10);
  digitalWrite(pinotrig, LOW);

  return pulseIn(pinoecho, HIGH) / 58;
}
