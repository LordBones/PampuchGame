using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using test_app_hledani_cesty;
using System.IO;

namespace Hledani_Nejkratsi_Cesty
{
    /// <summary>
    /// trida repreyebtuje souradnice jednoho bodu
    /// </summary>
    public struct sPoint
    {
        public int x, y;
        public sPoint(int x, int y)
        { this.x = x; this.y = y; }
        public void SetXY(int x, int y)
        {
            this.x = x; this.y = y;
        }
    }

    /// <summary>
    /// dvouroymerne pole bajtu
    /// </summary>
    public struct sMatrix
    {
        public byte[,] pole;
        public int max_x;
        public int max_y;

        public byte this[int index, int index2]
        {
            get { return pole[index, index2]; }
            set { pole[index, index2] = value; }
        }
    }

    public class base_find_best_path
    {
        protected enum pSmer { nahoru = 1, doprava = 2, dolu = 3, doleva = 4 }
        protected enum pPolicko { volno = 0, zed = 1, obsazeno = 2, hledani_cile = 3 }

        /// <summary>
        /// slouzi k rychle detekci zdali tato cast nej cesty uz nebyla nalezena
        /// </summary>
        public sMatrix pBludiste;
        protected sPoint pStart, pCil;

        /// <summary>
        /// vysledna nejkratsi cesta
        /// </summary>
        public Stack<sPoint> pNej_cesta = null;

        /// <summary>
        /// jine
        /// </summary>
        public long Pocet_Vnoreni;
        public bool Animuj = false;

        /// <summary>
        /// nakopiruje nove bludiste
        /// 0 - volne pole, 1 - zed
        /// </summary>
        public void Set_Copy_Bludiste(sMatrix new_blud)
        {
            if (new_blud.max_x > pBludiste.max_x || new_blud.max_y > pBludiste.max_y)
                return;

            /// kopiruje pole

            for (int y = 0; y < new_blud.max_y; y++)
                for (int x = 0; x < new_blud.max_x; x++)
                {
                    pBludiste[x, y] = new_blud[x, y];
                }
        }

        // vlastnosti 
        public sPoint Start { get { return pStart; } set { pStart = value; } }
        public sPoint Cil { get { return pCil; } set { pCil = value; } }

    }
}
