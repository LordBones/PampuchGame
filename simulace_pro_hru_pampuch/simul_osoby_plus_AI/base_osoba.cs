using System;
using System.Collections.Generic;
using Hledani_Nejkratsi_Cesty;
using System.Text;

namespace osoba_strazny_lump_jini
{
    interface IBase_Osoba
    {
       void AI_Thinking_Resolution();
    }

    public class Base_Osoba: IBase_Osoba
    {
        public enum typ_osoby { lump, strazny }
        public enum druh_osoby { neuveden , easy}

        public typ_osoby osoba;
        public druh_osoby kind_osoba;


        /// <summary>
        /// souradnice akrualni poyice
        /// </summary>
        public int X, Y;

        sMatrix bludiste;

        /// <summary>
        /// ulozena cesta v bodech kterou ma jit
        /// </summary>
        protected sPoint[] cesta;
        protected int index_cesty;

        /// <summary>
        /// seznam ostatnich osob ve hre
        /// </summary>
        protected Base_Osoba[] others_in_bludiste;

        public Base_Osoba()
        {
            X = 0;
            Y = 0;
            index_cesty = 0;
            bludiste = new sMatrix(0,0);
            others_in_bludiste = null;
        }

        public Base_Osoba(int x, int y, typ_osoby _osoba)
        {
            X = x;
            Y = y;
            osoba = _osoba;
            index_cesty = 0;
            bludiste = new sMatrix(0, 0);
            others_in_bludiste = null;
        }

        /// <summary>
        /// presune se po vygenerovane ceste na dalsi policko
        /// </summary>
        public void Move_To_Another_Point_Way()
        {
            if (cesta == null) return;

            if (index_cesty < cesta.Length && bludiste[cesta[index_cesty].x,cesta[index_cesty].y] == 0)
            {
                X = cesta[index_cesty].x;
                Y = cesta[index_cesty].y;
                index_cesty++;
            }
        }

        /// <summary>
        /// najde nejkratsi cestu mezi dvema body
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns> vraci pole bodu , tvorici nej cestu</returns>
        public sPoint [] Find_Best_Path(sPoint start, sPoint end)
        {
            Find_Best_Path_towidth Hledej = new Find_Best_Path_towidth();
            Hledej.Cil = end;
            Hledej.Start = start;
            Hledej.Set_Size_Bludiste(bludiste.max_x, bludiste.max_y);
            Hledej.Set_Copy_Bludiste(bludiste);
            Hledej.Hledej();

            return Hledej.pNej_cesta.ToArray();
        }

        public virtual void AI_Thinking_Resolution()
        {
        }

        // vlastosti
        public sPoint Get_Next_Move_Point
        {
            get { return (index_cesty < cesta.Length) ? cesta[index_cesty + 1] : cesta[index_cesty]; }
        }

        public sPoint[] Set_New_Cesta
        {
            set { cesta = value; index_cesty = 0; }
        }

        public sMatrix Bludiste
        {
            set { bludiste = value; }
            get { return bludiste; }
        }

        public Base_Osoba [] Others_in_Bludiste
        {
          set { others_in_bludiste = value; }
        }

        public sPoint Get_Pozition
        {
            get { return new sPoint(X, Y); }
        }

    }
}
