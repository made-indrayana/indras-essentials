//
//  main.cpp
//  Reference and Co.
//
//  Created by Made Indrayana on 10.10.20.
//
//  This program is created to display the usage of pass-by-value,
//  pass by reference and pointer in C++.
//

#include <iostream>
using namespace std;


int calc_copy(int i)
{
    cout << "Value of pass_by_value before calc_copy is " << i << endl;
    i = i*10;
    return i;
}

int calc_obj(int& i) //the "&" symbol is to say that we are passing an object ref
{
    cout << "Value of pass_by_ref before calc_obj is " << i << endl;
    i = i*10;
    return i;
}

int calc_with_pointer(int* i) //the "*" symbol is to say that we take a dereferenced pointer
{
    cout << "Value of p before calc_with_pointer is " << *i << endl;
    *i = *i * 10;
    return *i;
}


int main() {
        
    int pass_by_value = 10;
    int pass_by_ref = 20;
    int pass_by_pointer = 50;

    calc_copy(pass_by_value);
    calc_obj(pass_by_ref);

    cout << "Value of pass_by_value after calc_copy is " << pass_by_value << endl; // should print 10
    cout << "Value of pass_by_ref after calc_obj is " << pass_by_ref << endl << endl; // should print 100
    
    cout << "Now we talk about pointer..." << endl;
    
    int *p = &pass_by_pointer;
    
    cout << "Value of p after assigned to pass_by_pointer is " << *p << endl; // should print 50
    cout << "If we print p directly, it will print " << p << endl; // should print memory address
    cout << "That is a memory address! We have to dereference always with *." << endl << endl;
    
    calc_with_pointer(p);
    
    cout << "Value of p after calculated with pointer is " << *p << endl; //should print 500
    cout << "Value of pass_by_pointer after calculated with pointer " << pass_by_pointer << endl << endl; //should have changed the original stuff

    int *n = new int;
    
    cout << "Value of n after allocating from heap is " << *n << endl; // should print garbage
    cout << "If we print n directly, it will print " << n << endl; // should print memory address
    *n = 30;
    cout << "Value of n after initializing is " << *n << endl; // should print 30

    calc_with_pointer(n);
    
    cout << "Value of n after calculated with pointer is " << *n << endl; //should print 300
    
}
