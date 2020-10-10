//
//  main.cpp
//  Reference and Co.
//
//  Created by Made Indrayana on 10.10.20.
//
//  This program is created to display the difference between pass-by-value
//  and pass by reference in C++.
//

#include <iostream>
using namespace std;


int calc_copy(int i)
{
    cout << "Value before calc_copy is " << i << endl; // should print 10
    i = i*10;
    return i;
}

int calc_obj(int& i) //the "&" symbol is to say that we are passing an object ref
{
    cout << "Value before calc_obj is " << i << endl; // should print 10
    i = i*10;
    return i;
}


int main() {
        
        int pass_by_value = 10;
        int pass_by_ref = 10;

        calc_copy(pass_by_value);
        calc_obj(pass_by_ref);

        cout << "Value after calc_copy is " << pass_by_value << endl; // should print 10
        cout << "Value after calc_obj is " << pass_by_ref << endl; // should print 100
}
