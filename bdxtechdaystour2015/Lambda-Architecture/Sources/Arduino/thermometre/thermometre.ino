#include <ArduinoJson.h>

char* device = "temdevice1";
char* type = "temp";
int sensorPin = 0;
float SUPPLY_VOLTAGE = 5.0;
// Coef = (SUPLLY_VOLATE * mV / Analog Value) / (10mv equal to 1 Celcius)
float coef = ((SUPPLY_VOLTAGE * 1000 / 1024) / 10);
const int BUFFER_SIZE = JSON_OBJECT_SIZE(3);
void setup()
{
  Serial.begin(9600);
  int start = 0;
  while(start == 0)
  {
    start = Serial.parseInt();
    delay(500);
  }
}

void loop()
{
  StaticJsonBuffer<BUFFER_SIZE> jsonBuffer;
  float temp = analogRead(sensorPin) * coef;
  JsonObject& root = jsonBuffer.createObject();
  root["device"] = device;
  root["type"] = type;
  root["data"] = temp;
  root.printTo(Serial);
  Serial.println();
  delay(1000);
}



