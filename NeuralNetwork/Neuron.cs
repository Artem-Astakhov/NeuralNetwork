using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class Neuron
    {
        public List<double> Weight { get; }
        public List<double> Inputs { get; }

        public NeuronType NeuronType { get; }
        public double Output { get; private set; }
        public double Delta { get; private set; }


        public Neuron(int inputCount, NeuronType type = NeuronType.Normal)
        {
            NeuronType = type;
            Weight = new List<double>();
            Inputs = new List<double>();
            InitWeightRandom(inputCount);
        }

        private void InitWeightRandom(int inputCount)
        {
            var rnd = new Random();

            for (int i = 0; i < inputCount; i++)
            {
                if(NeuronType == NeuronType.Input)
                {
                    Weight.Add(1);
                }
                else
                {
                    Weight.Add(rnd.NextDouble());
                }               
                Inputs.Add(0);
            }
        }

        public double FeedForward(List<double> inputs)
        {
            for(int i =0; i< inputs.Count; i++)
            {
                Inputs[i] = inputs[i];
            }

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

        private double SigmoidDx(double x)
        {
            var sigmoid = Sigmoid(x);
            var result = sigmoid*(1 - sigmoid);
            return result;
        }

        public override string ToString()
        {
            return Output.ToString();
        }


        public void Learn(double error, double learningRate)
        {
            if(NeuronType == NeuronType.Input)
            {
                return;
            }

            Delta = error * SigmoidDx(Output);

            for(int i =0; i<Weight.Count; i++)
            {
                var weight = Weight[i];
                var input = Inputs[i];

                var newWeight = weight - input * Delta * learningRate;
                Weight[i] = newWeight;
            }
        }
    }
}
