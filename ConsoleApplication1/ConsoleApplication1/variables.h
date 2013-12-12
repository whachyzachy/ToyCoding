// Variables.h

#include <string>

using namespace std;

string name, shipName;
string bigChoice="hangar";
int smallChoice;

int trainNumber;
int crewSize = 100, army = 1000, fighters = 100, totalArmy = army + (2 * fighters);
int armyCost = (int)((50 + (totalArmy * .01)) / 100), fightersCost = (int)((armyCost + (armyCost * .8)) / 100);
int wounded, healed;
int medbayUsage, medbaySize = 125;
int credits = 10000;
int hullHealth;
int shipStrength, troopStrength, fighterStrength;
int food;