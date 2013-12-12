// Hangar.cpp

#include <Hangar.h>
#include <Variables.h>
//#include <Bridge.h>

#include <iostream>


void hangar(){

	if(bigChoice == "hangar"){

		cout << "You are in the hangar now. What do you want to do?" << endl;
		cout << "1: Train Troops (" << armyCost << " credits per 100 troops)" << endl;
		cout << "2: Train Space Fighters (" << fightersCost << " credits per 50 fighters)" << endl;
		cout << "3: Leave hangar back to the Bridge." << endl;
		cin >> smallChoice;

		if (smallChoice == 3){
			//bridge();
		}
		switch(smallChoice){
////////////////////////////// Army Training ///////////////////////////////////////////////
		case 1:
			if (credits < armyCost){
				cout << "You do not have enough credits to train any troops." << endl;
			}
			else{
				cout << "Train 100, 200, or 500 troops?" << endl;
				cout << "Enter 0 to train no troops." << endl;
				cin >> trainNumber;

				if (trainNumber == 100){
					cout << "You have trained 100 troops for " << armyCost << " credits.";
					army = army + 100;
					cout << "You now have " << army << " troops.";
					cout << "You have " << credits << " credits.";
				}	
				else if (trainNumber == 200){
					if(credits < (2 * armyCost)){
						cout << "You do not have enough credits to train 200 troops." << endl;
					}
					else{
						cout << "You have trained 200 troops for " << (2 * armyCost) << " credits.";
						army = army + 200;
						cout << "You now have " << army << " troops.";
						cout << "You have " << credits << " credits.";
					}
				}
				else if(trainNumber == 500){
					if(credits < (5 * armyCost)){
						cout << "You do not have enough credits to train 500 troops." << endl;
					}
					else{
						cout << "You have trained 500 troops for " << (5 * armyCost) << " credits.";
						army = army + 500;
						cout << "You now have " << army << " troops.";
						cout << "You have " << credits << " credits.";
					}
				}
				else{
					cout << "You chose not to train any troops." << endl;
				}
			}

			break;
///////////////////////////// Fighter Building /////////////////////////////////////////////////////////////
			
		case 2:
			if (credits < fightersCost){
				cout << "You do not have enough credits to build and fighters." << endl;
			}
			else{
				cout << "Build 50, 100, or 250 fighters?" << endl;
				cout << "Enter 0 to build no fighters." << endl;
				cin >> trainNumber;

				if (trainNumber == 50){
					cout << "You have trained 50 troops for " << armyCost << " credits.";
					fighters = fighters + 100;
					cout << "You now have " << fighters << " fighters.";
					cout << "You have " << credits << " credits.";
				}	
				else if (trainNumber == 100){
					if(credits < (2 * fightersCost)){
						cout << "You do not have enough credits to build 100 fighters." << endl;
					}
					else{
						cout << "You have trained 200 troops for " << (2 * armyCost) << " credits.";
						fighters = fighters + 200;
						cout << "You now have " << fighters << " fighters.";
						cout << "You have " << credits << " credits.";
					}
				}
				else if(trainNumber == 250){
					if(credits < (5 * fightersCost)){
						cout << "You do not have enough credits to build 250 fighters." << endl;
					}
					else{
						cout << "You have trained 500 troops for " << (5 * armyCost) << " credits.";
						army = army + 500;
						cout << "You now have " << fighters << " fighters.";
						cout << "You have " << credits << " credits.";
					}
				}
				else{
					cout << "You chose not to build any fighters." << endl;
				}
			}

			break;

		}
	}
}