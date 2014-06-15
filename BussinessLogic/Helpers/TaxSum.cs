using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BussinessLogic.Helpers
{
    public class TaxSum
    {
        public decimal netto { get; private set; }
        public decimal vat { get; private set; }
        public decimal brutto { get; private set; }

        public TaxSum()
        {
            netto = new decimal(0);
            vat = new decimal(0);
            brutto = new decimal(0);
        }

        public TaxSum add(decimal netto, decimal vat, decimal brutto)
        {
            this.netto += netto;
            this.vat += vat;
            this.brutto += brutto;
            return this;
        }
    }
}
