using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class Neuron
    {
        public List<double> Weight { get; set; }
        public NeuronType NeuronType { get; }
        public double Output { get; private set; }


        public Neuron(int inputCount, NeuronType type = NeuronType.Normal)
        {
            NeuronType = type;
            Weight = new List<double>();

            for(int i=0; i < inputCount; i++)
            {
                Weight.Add(1);
            }
        }

        public double FeedForward(List<double> inputs)
        {
            var sum = 0.0;
            if (inputs.Count == Weight.Count)
            {
                for (int i = 0; i < inputs.Count; i++)
                {
                    sum += inputs[i] * Weight[i];
                }
            }
            if (NeuronType != NeuronType.Input)
            {
                Output = Sigmoid(sum);
            }
            else
            {
                Output = sum;
            }
            
            return Output;
        }

        private double Sigmoid(double x)
        {
            var result = 1.0 / (1.0 + Math.Exp(-x));
            return result;
        }

        public override string ToString()
        {
            return Output.ToString();
        }

        public void SetWeights(params double[] weights)
        {
            for (int i = 0; i<weights.Length; i++)
            {
                Weight[i] = weights[i];
            }
        }
    }
}
