
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;


namespace Kosar
{

    public class Item
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
           //kosár feltöltése
            List<Item> cart = new List<Item>()          
            {
                new Item { Name = "alma", Price = 150, Quantity = 3 },
                new Item { Name = "tej", Price = 300, Quantity = 2 },
                new Item { Name = "kenyér", Price = 200, Quantity = 1 },
                new Item { Name = "birs alma", Price = 200, Quantity = 3 },          

            };
            Console.WriteLine("Végösszeg: " + Calculator(cart));
        }

        public static float Calculator(List<Item> cart)
        {
            //Kosárban lévő elemek árának kiszámítása
            float priceSum = cart.Sum(item => item.Price * item.Quantity);
            Console.WriteLine("Teljes kosár ár: " + priceSum);

            //hasDiscount változóba eltárolom hogy volt e 10% os kedvezmény
            bool hasDiscount = false;
            const float discountThreshold = 3000;
            const int appleDiscountThreshold = 5;
            if (priceSum > discountThreshold)
            {
                //10%-os kedvezmény azaz a teljes ár 90%-a
                priceSum *= 0.9f;
                hasDiscount = true;
                Console.WriteLine("3000Ft feletti 10% kedvezmény: " + priceSum);
            }
            //Ha 5-nél zöbb alma van akkor van még 10%-os kedvezmény az almára. 
            if (cart.Where(item => item.Name.Contains("alma")).Sum(item => item.Quantity) > appleDiscountThreshold)
            {
                //Almák össz ára
                float applePriceSum = cart.Where(item => item.Name.Contains("alma")).Sum(item => item.Price * item.Quantity);

                //Ha volt már 10% os kedvezmény akkor a 90% os alma árból vonjuk még le a 20%-ot ami igazából 28% az eredeti árhoz képest, mert a listában még az eredeti ár van.
                //Ha pedig nem volt 20% os kedvezmény akkor pedig csak az össz alma árból 20%-át vonjuk le.
                priceSum -= hasDiscount ? applePriceSum * 0.28f: applePriceSum * 0.2f;
                Console.WriteLine("5 nél több alma vásárlása esetén plusz 20% kedvezmény az almákra: " + priceSum);
            }
            Console.WriteLine();
            return priceSum;
        }
    }
}


