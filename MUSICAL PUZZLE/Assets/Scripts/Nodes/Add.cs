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
    private Sequence.Beat[] res_1;
    private Sequence.Beat[] res_2;
    public override void Process()
    {
        len_input1 = input[0].sequence.Count();
        len_input2 = input[1].sequence.Count();
        gcd_len = GCD(len_input1, len_input2);
        lcm_len = len_input1*len_input2/gcd_len;
        coeff_1 = lcm_len/len_input1;
        coeff_2 = lcm_len/len_input2;
        res_1 = (Sequence.Beat[]) input[0].sequence.Clone();
        res_2 = (Sequence.Beat[]) input[1].sequence.Clone();

        for (int i=0; i < coeff_1-1; i++)
        {
            res_1 = res_1.Concat(input[0].sequence).ToArray();
        }
        for (int i=0; i < coeff_2-1; i++)
        {
            res_2 = res_2.Concat(input[1].sequence).ToArray();
        }

        Sequence.Beat[] OutputArray = (Sequence.Beat[]) res_1.Clone(); 
        for(int i=0;i<lcm_len;i++){
            OutputArray[i] |= res_2[i];
        }

        Output = new Sequence(OutputArray);
    }
}
