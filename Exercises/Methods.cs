using System;

namespace Exercises
{
    internal class Methods
    {
        static void Main3(string[] args)
        {
            int initialStock = 50;
            int quantityToAdd = 20;
            int addedQuantity;

            UpdateStock(initialStock, quantityToAdd, out int updatedStock, out addedQuantity);
            Console.WriteLine($"Inventario inicial: {initialStock}");
            Console.WriteLine($"Cantidad agregada {quantityToAdd}");
            Console.WriteLine($"Inventario actualizado: {updatedStock}");

            adjustStock(ref updatedStock, 10);
            Console.WriteLine($"Ajuste de entrada: {updatedStock}");

            adjustStock(ref updatedStock, -20);
            Console.WriteLine($"Ajuste de salida: {updatedStock}");

            (string productName, int stock) = GetProductInfo("Laptop", 20);
            Console.Write($"Nombre del producto: {productName}");

            Console.Write($"Inventario del producto: {stock}");



            Console.ReadKey();
        }
        public static void UpdateStock(int initialStock, int quantityToAdd, out int updatedStock, out int addedQuantity)
        {
            addedQuantity = quantityToAdd;
            updatedStock = initialStock + addedQuantity;
        }

        public static void adjustStock(ref int stock, int adjustment)
        {
            stock += adjustment;
        }

        public static (string productName, int stock) GetProductInfo(string productName, int stock)
        {
            return (productName, stock);
        }
    }
}
