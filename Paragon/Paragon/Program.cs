using System;
using System.IO;

namespace Paragon
{
    class Program
    {
        private static byte nr_paragonu = 0;

        static void Main(string[] args)
        {
            Worker prac1 = new Worker(1, "Damian");

            Order ham = new Order("Hamburger", 11.99, 2);       // (nazwa, cena, ilosc)
            Order cola = new Order("Coca-Cola", 5.99, 1);
            Order chips = new Order("Frytki", 3.99, 2);
            Order nuggets = new Order("Nuggetsy", 14.59, 1);

            string[] names = { ham.getName, cola.getName, chips.getName, nuggets.getName };
            double[] prices = { ham.getPrice, cola.getPrice, chips.getPrice, nuggets.getPrice };
            int[] amounts = { ham.getAmount, cola.getAmount, chips.getAmount, nuggets.getAmount };

            double total_price = 0;
            for (int i = 0; i < prices.Length; i++)
            {
                total_price += prices[i] * amounts[i];
            }
            total_price = Math.Round(total_price, 2);

            CustomerCash customer = new CustomerCash(100);

            string nameFile = string.Format("paragon-{0:yyyy-MM-dd_hh-mm-ss-tt}.txt", DateTime.Now);    // nazwa pliku
            StreamWriter sw = new StreamWriter(nameFile);                                               // tworzy plik .txt

            sw.WriteLine("     FIRMA sp. z o.o.");
            sw.WriteLine("   65-246  Zielona Góra");
            sw.WriteLine("      ul. Podgórna 50");
            sw.WriteLine("    NIP: 012-345-67-89\n");

            sw.WriteLine(String.Format("{0,-20:yyyy-MM-dd} {0,5:t}", DateTime.Now));          // data i godzina na paragonie
            sw.WriteLine("#{0,-4:D3} {1,20}", prac1.getId, prac1.getName);                    // pracownik [id      nazwa]

            sw.WriteLine(" - - - - - - - - - - - - -");
            sw.WriteLine("     PARAGON FISKALNY");
            sw.WriteLine(" - - - - - - - - - - - - -");



            for (int ctr = 0; ctr < names.Length; ctr++)
            {
                    sw.WriteLine("{0,-11}\n{1,26:0.00}", names[ctr], amounts[ctr] + "x" + prices[ctr] + " " + prices[ctr] * amounts[ctr]);
            }


            sw.WriteLine(" - - - - - - - - - - - - -");
            sw.WriteLine("{0,-5} {1,20:0.00}", "SUMA:", "PLN " + total_price);                  // suma cen zamowienia do zaplaty
            sw.WriteLine(" - - - - - - - - - - - - -");

            bool cash_or_card = true;
            if (cash_or_card)                                                                           // kartą czy gotówką
            {   
                sw.WriteLine("{0,-10} {1,15:0.00}", "Gotówka:", customer.getCash);                     // gotowka od klienta
                sw.WriteLine("{0,-10} {1,15:0.00}", "Reszta:", customer.getCash - total_price);        // reszta
            }
            else
            {
                sw.WriteLine("{0,-14} {1,11:0.00}", "Płatność kartą", total_price);
            }

            nr_paragonu += 1 ;                                                                       // numer paragonu
            sw.WriteLine("\n{0,-7} {1,13} {2:D4}", "P.fisk.","Nr", nr_paragonu);
            sw.WriteLine("       AFN 12345678");
            sw.WriteLine("   ZAPRASZAMY PONOWNIE");
            sw.Close();
        }
        class Order
        {
            string name;
            double price;
            int amount;

            public string getName => name;
            public double getPrice => price;
            public int getAmount => amount;

            public Order(string name, double price, int amount)
            {
                this.name = name;
                this.price = price;
                this.amount = amount;
            }
        }

        class Worker
        {
            int id;
            string name;

            public int getId => id;
            public string getName => name;

            public Worker(int id, string name)
            {
                this.id = id;
                this.name = name;
            }
        }

        class CustomerCash
        {
            double cash;

            public double getCash => cash;
            public double setCash { set => cash = value; }

            public CustomerCash(double cash)
            {
                this.cash = cash;
            }
        }
    }
}