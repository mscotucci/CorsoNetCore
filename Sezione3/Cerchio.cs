using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sezione3
{
    public class Cerchio : Figura
    {
        private readonly double _raggio;//Incapsulando

        public Cerchio(double raggio)
        {
            _raggio = raggio;
        }

        public override string FormulaPerimetro()
        {
            return "_raggio * 2* Math.PI";
        }

        public override double Perimetro()
        {
            return _raggio * 2* Math.PI;
        }
    }

    public class Cerchio2 : Cerchio
    {
        public Cerchio2(double raggio) : base(raggio)
        {
        }

    }
}
