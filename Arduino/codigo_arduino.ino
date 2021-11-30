/*
  Parametros Entrada:
    Iniciar => I
    Parar => P

    Abrir Cancela ENTRADA => E
    Abrir Cancela SAIDA => S

    Forçar Cancela ENTRADA => N
    Forçar Cancela SAIDA => M

  Parametros Saida:
    ENTRADA ABERTA => 1
    ENTRADA FECHADA => 2

    SAIDA ABERTA => 3
    SAIDA FECHADA => 4

    HÁ VEICULO ENTRADA=> 5
    NÃO HÁ VEICULO ENTRADA => 6

    HÁ VEICULO SAIDA => 7
    NÃO HÁ VEICULO SAIDA=> 8

    ARDUINO OPERANDO => 9
    ARDUINO PARADO => 0
*/

// Inicializa os Servos.
#include <Servo.h>
Servo servo_ent;
Servo servo_sai;

//Bool de controle das cancelas (aberto ou fechado).
bool cancelaENT = false;
bool cancelaSAI = false;

bool manterAbertoENT = false;
bool manterAbertoSAI = false;

//Bool de controle. Detecção de veiculo
bool haVeiculoENT = false;
bool haVeiculoSAI = false;

//Define se o sistema esta operando
bool inicio = false;

//Configuração de Distancia Mínima em centímetros.
const int distancia_min = 10; // 80 para Tinkercad e 10 para maquete
int distancia_ENT;
int distancia_SAI;

//Configuração de abertura e fechamento das cancelas. 
const int posicaoAbrir = 0; // Abrir -> 180 Para Tinkercad e 0 Para Maquete.
const int posicaoFechar = 90; // Fechar -> Sempre 90.

//Sensor ultrasonico (Definição de portas para cada pino do sensor)
const int ECHO_ENT = 2;
const int TRIG_ENT = 3;
const int ECHO_SAI = 4;
const int TRIG_SAI = 5;

//Codigos de comunicação serial
char comando[] = {'x','1','2','3','4','5','6','7','8'};

void setup()
{
  Serial.begin(9600);

  //Servo entrada. (Configuração de porta e posição Inicial)
  servo_ent.attach(11, 500, 2500);
  servo_ent.write(posicaoFechar);

  //Servo saida. (Configuração de porta e posição Inicial)
  servo_sai.attach(10, 500, 2500);
  servo_sai.write(posicaoFechar);

  //Configurações do Sensor
  pinMode(TRIG_ENT, OUTPUT);
  pinMode(ECHO_ENT, INPUT);
  pinMode(TRIG_SAI, OUTPUT);
  pinMode(ECHO_SAI, INPUT);
}

void loop()
{
  if (Serial.available())
  {
    switch (Serial.read())
    {
    case 'I':
      inicio = true;
      //Serial.write('9'); //Informa o sistema que o arduino esta Trabalhando.
      Inicia();
      break;
    }
  }

  while (inicio)
  {
    parkManager();
  }  
}

void parkManager()
{
    //Atualiza distancia recebida pelos sensores.
    distancia_ENT = calculaDistancia(TRIG_ENT, ECHO_ENT);
    distancia_SAI = calculaDistancia(TRIG_SAI, ECHO_SAI);

    fechaEntradaAuto();

    fechaSaidaAuto();

    aberturaViaSerial();  
}

void Inicia()
{
  distancia_ENT = calculaDistancia(TRIG_ENT, ECHO_ENT);
  distancia_SAI = calculaDistancia(TRIG_SAI, ECHO_SAI);

  if (distancia_ENT >= distancia_min)
  {
    Serial.write(comando[5]); //Informa o sistema que HÁ veiculo na ENTRADA
    Serial.flush();
    if(!haVeiculoENT)
    {
        haVeiculoENT = true;
    }    
  }else
  {
    Serial.write(comando[6]); //Informa o sistema que NÃO HÁ veiculo na ENTRADA
  }

  if (distancia_SAI >= distancia_min)
  {
    Serial.write(comando[7]); //Informa o sistema que HÁ veiculo na SAIDA
    if(!haVeiculoSAI)
    {
        haVeiculoSAI = true;
    }
  }else
  {
    Serial.write(comando[8]); //Informa o sistema que NÃO HÁ veiculo na SAIDA    
  }
}

void fechaEntradaAuto()
{
    if(!manterAbertoENT)
    {
        //Fecha cancela ENTRADA *AUTOMATICO*
        if (distancia_ENT >= distancia_min) //NAO TEM
        {
            if (haVeiculoENT)
            {
                haVeiculoENT = false;
                Serial.write(comando[6]); //Informa o sistema que NÃO HÁ veiculo na cancela ENTRADA
            }
            if (cancelaENT) // Se cancela estiver ABERTA
            {
                moveCancelaEnt('F');
            }
        }
        else
        {
            if (!haVeiculoENT)
            {
                haVeiculoENT = true;
                Serial.write(comando[5]); //Informa o sistema que HÁ veiculo na cancela ENTRADA
            }
        }
    }
    
}

void fechaSaidaAuto()
{
    if(!manterAbertoSAI)
    {
        //Fecha cancela SAIDA *AUTOMATICO*
        if (distancia_SAI >= distancia_min)
        {
            if (haVeiculoSAI)
            {
                haVeiculoSAI = false;
                Serial.write(comando[8]); //Informa o sistema que NÃO HÁ veiculo na cancela SAIDA
            }

            if (cancelaSAI)
            {
                moveCancelaSai('F');
            }
        }
        else
        {
            if (!haVeiculoSAI)
            {
                haVeiculoSAI = true;
                Serial.write(comando[7]); //Informa o sistema que HÁ veiculo na cancela SAIDA
            }
        }  
    }
      
}

void aberturaViaSerial()
{
    //---ABERTURA CANCELA VIA COMUNICAÇÃO SERIAL---//
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
    case 'N': // N - Força a cancela ENTRADA
      forcaEntrada();
      break;
    case 'M': // M - Força a cancela SAIDA
      forcaSaida();
      break;
    case 'P': // P - Para o arduino
      inicio = false;
      //Serial.write('0'); //Informa o sistema que o arduino esta Parado.
      break;
    }
  }
}

void moveCancelaEnt(char flag)
{
  switch (flag)
  {
  case 'A':
    if (distancia_ENT <= distancia_min) //Não há veiculo
    {
      servo_ent.write(posicaoAbrir); 
      Serial.write(comando[1]);    //Informa o sistema sobre a abertura da cancela ENTRADA
      cancelaENT = true;
    }
    break;

  case 'F':
    delay(3000);
    servo_ent.write(posicaoFechar);
    Serial.write(comando[2]); //Informa o sistema sobre o fechamento da cancela ENTRADA
    cancelaENT = false;
    break;
  }
}

void moveCancelaSai(char flag)
{
  switch (flag)
  {
  case 'A':
    if (distancia_SAI <= distancia_min)
    {
      servo_sai.write(posicaoAbrir);
      Serial.write(comando[3]); //Informa o sistema sobre a ABERTURA da cancela SAIDA
      cancelaSAI = true;
    }
    break;

  case 'F':
    delay(3000);
    servo_sai.write(posicaoFechar);
    Serial.write(comando[4]); //Informa o sistema sobre o FECHAMENTO da cancela SAIDA
    cancelaSAI = false;
    break;
  }
}

//Abre cancela ENTRADA mesmo que não houver veiculo.
void forcaEntrada()
{
    if(cancelaENT)
    {
        servo_ent.write(posicaoFechar);
        Serial.write(comando[2]); //Informa o sistema sobre o fechamento da cancela ENTRADA
        cancelaENT = false;
        manterAbertoENT = false;
        
    }else
    {
        servo_ent.write(posicaoAbrir);
        Serial.write(comando[1]); //Informa o sistema sobre a abertura da cancela ENTRADA
        cancelaENT = true;
        manterAbertoENT = true;
    }
    
}

//Abre cancela SAIDA mesmo que não houver veiculo.
void forcaSaida()
{
    if(cancelaSAI)
    {
        servo_sai.write(posicaoFechar);
        Serial.write(comando[4]); //Informa o sistema sobre o fechamento da cancela SAIDA
        cancelaSAI = false;
        manterAbertoSAI = false;

    }else
    {
        servo_sai.write(posicaoAbrir);
        Serial.write(comando[3]); //Informa o sistema sobre a abertura da cancela SAIDA
        cancelaSAI = true;
        manterAbertoSAI = true;
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
