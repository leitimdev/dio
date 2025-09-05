using Celular.Models;
using System;

class Program
{
    static void Main(string[] args)
    {
        Smartphone nokia = new Nokia("123456789", "Nokia 3310", "IMEI123456", 16);
        Smartphone iphone = new Iphone("987654321", "iPhone 13", "IMEI987654", 128);

        nokia.Ligar();
        nokia.ReceberLigacao();
        nokia.InstalarAplicativo("WhatsApp");

        iphone.Ligar();
        iphone.ReceberLigacao();
        iphone.InstalarAplicativo("Instagram");
    }
}