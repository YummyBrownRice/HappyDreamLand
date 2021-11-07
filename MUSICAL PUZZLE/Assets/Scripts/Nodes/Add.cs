using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class Add : Node // Todo
{
    static int GCD(int a, int b)
    {
        int Remainder;
    
        while( b != 0 )
        {
            Remainder = a % b;
            a = b;
            b = Remainder;
        }
      
        return a;
    }
    private int len_input1;
    private int len_input2;
    private int gcd_len;
    private int lcm_len;
    private int coeff_1;
    private int coeff_2;
    private sequence res_1;
    private sequence res_2;
    public override void Process()
    {
        len_input1 = input[0].sequence.Count();
        len_input2 = input[1].sequence.Count();
        gcd_len = GCD(len_input1, len_input2);
        lcm_len = len_input1*len_input2/gcd_len;
        coeff_1 = lcm_len/len_input1;
        coeff_2 = lcm_len/len_input2;
        res_1 = new Sequence(input[0].sequence.ToArray());
        res_2 = new Sequence(input[1].sequence.ToArray());

        while(coeff_1 > 0){
            res_1 = Sequence(res_1.Concat(input[0]).ToArray());
            coeff_1 -= 1;
        }
        while(coeff_2 > 0){
            res_2 = Sequence(res_2.Concat(input[1]).ToArray());
            coeff_2 -= 1;
        }
        Output = new Sequence(res1.ToArray());
        for(int i=0;i<lcm_len;i++){
            Output[i] = Output[i] || res_2[i];
        }
    }
}
