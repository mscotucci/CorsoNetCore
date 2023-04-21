using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sezione3
{
    public abstract class Figura
    {
        protected int MyProperty { get; set; }
        
        public abstract double Perimetro();
        public abstract string FormulaPerimetro();

        public virtual string GetName()
        {
            return GetType().Name;
        }

        public override string ToString()
        {
            return $"perimetro figura = {Perimetro()} nome figura = {GetName()} formula perimetro = {FormulaPerimetro()}";
        }
    }
}
