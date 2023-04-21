using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sezione3
{
    public class Quadrato : Figura
    {
        private readonly double _lato;

        public Quadrato(double lato)
        {
            _lato = lato;
        }

        public override string FormulaPerimetro()
        {
            return "_lato * 4";
        }

        public override double Perimetro()
        {
            return _lato * 4;
        }
    }
}
