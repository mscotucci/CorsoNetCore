using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sezione3
{
    public abstract class Figura
    {
        protected int MyProperty { get; private set; }
        
        public abstract double Perimetro();
        public abstract string FormulaPerimetro();

        public virtual string GetName()
        {
            MyProperty = 0; 
            return GetType().Name;
        }

        public override string ToString()
        {
            return $"perimetro figura = {Perimetro()} nome figura = {GetName()} formula perimetro = {FormulaPerimetro()}";
        }

        public void SetMyProperty(int value)
        {
            if (value < 19)
            {
                throw new Exception();
            }
        }
    }
}
