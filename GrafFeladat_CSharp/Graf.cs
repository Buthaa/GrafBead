using System;
using System.Collections.Generic;

namespace GrafFeladat_CSharp
{
    /// <summary>
    /// Irányítatlan, egyszeres gráf.
    /// </summary>
    class Graf
    {
        int csucsokSzama;
        /// <summary>
        /// A gráf élei.
        /// Ha a lista tartalmaz egy(A, B) élt, akkor tartalmaznia kell
        /// a(B, A) vissza irányú élt is.
        /// </summary>
        readonly List<El> elek = new List<El>();
        /// <summary>
        /// A gráf csúcsai.
        /// A gráf létrehozása után új csúcsot nem lehet felvenni.
        /// </summary>
        readonly List<Csucs> csucsok = new List<Csucs>();

        /// <summary>
        /// Létehoz egy úgy, N pontú gráfot, élek nélkül.
        /// </summary>
        /// <param name="csucsok">A gráf csúcsainak száma</param>
        public Graf(int csucsok)
        {
            this.csucsokSzama = csucsok;

            // Minden csúcsnak hozzunk létre egy új objektumot
            for (int i = 0; i < csucsok; i++)
            {
                this.csucsok.Add(new Csucs(i));
            }
        }

        /// <summary>
        /// Hozzáad egy új élt a gráfhoz.
        /// Mindkét csúcsnak érvényesnek kell lennie:
        /// 0 &lt;= cs &lt; csúcsok száma.
        /// </summary>
        /// <param name="cs1">Az él egyik pontja</param>
        /// <param name="cs2">Az él másik pontja</param>
        public void Hozzaad(int cs1, int cs2)
        {
            if (cs1 < 0 || cs1 >= csucsokSzama ||
                cs2 < 0 || cs2 >= csucsokSzama)
            {
                throw new ArgumentOutOfRangeException("Hibas csucs index");
            }

            // Ha már szerepel az él, akkor nem kell felvenni
            foreach (var el in elek)
            {
                if (el.Csucs1 == cs1 && el.Csucs2 == cs2)
                {
                    return;
                }
            }

            elek.Add(new El(cs1, cs2));
            elek.Add(new El(cs2, cs1));
        }
        public void SzelessegiBejar(int kezdopont)
        {
            HashSet<int> bejart = new HashSet<int>();
            Queue<int> kovetkezok = new Queue<int>();
            kovetkezok.Enqueue(kezdopont);
            bejart.Add(kezdopont);
            while (kovetkezok.Count != 0)
            {
                int k = kovetkezok.Dequeue();
                Console.WriteLine(k);
                foreach (var el in this.elek)
                {
                    if ((el.Csucs1 == k) && (bejart.Contains(el.Csucs2)))
                    {
                        kovetkezok.Enqueue(el.Csucs2);
                        bejart.Add(el.Csucs2);
                    }
                }
            }

        }

        public void MelysegiBejar(int kezdopont)
        {
            HashSet<int> bejart = new HashSet<int>();
            Stack<int> kovetkezok = new Stack<int>();
            kovetkezok.Push(kezdopont);
            bejart.Add(kezdopont);
            while (kovetkezok.Count != 0)
            {
                int k = kovetkezok.Pop();
                Console.WriteLine(k);
                foreach (var el in this.elek)
                {
                    if ((el.Csucs1 == k) && (bejart.Contains(el.Csucs2)))
                    {
                        kovetkezok.Push(el.Csucs2);
                        bejart.Add(el.Csucs2);
                    }
                }
            }

        }

        public int MohoSzinezes()
        {

            Dictionary<int, int> szinezes = new Dictionary<int, int>();
            int maxSzin = this.csucsokSzama;
            HashSet<int> valaszthatoSzinek = new HashSet<int>();
            for (int i = 0; i < maxSzin - 1; i++)
            {
                valaszthatoSzinek.Add(i);
            }
            for (int i = 0; i < this.csucsokSzama - 1; i++)
            {
                foreach (var el in elek)
                {
                    if (el.Csucs1 == i)
                    {
                        if (szinezes.ContainsKey(el.Csucs2))
                        {
                            var szin = szinezes[el.Csucs2];
                            valaszthatoSzinek.Remove(szin);
                        }
                    }
                }
                var valasztottszin = valaszthatoSzinek.Min();
                szinezes.Add(i, valasztottszin);
            }
        }
        public override string ToString()
        {
            string str = "Csucsok:\n";
            foreach (var cs in csucsok)
            {
                str += cs + "\n";
            }
            str += "Elek:\n";
            foreach (var el in elek)
            {
                str += el + "\n";
            }
            return str;
        }
    }
}