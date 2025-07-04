using Project11_TriggerOrderStock.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project11_TriggerOrderStock
{
  internal class Program
  {
    static void Main(string[] args)
    {
      Db11Project20Entities context = new Db11Project20Entities();

      Console.BackgroundColor = ConsoleColor.Gray;
      Console.ForegroundColor = ConsoleColor.DarkBlue;
      Console.Clear();

      string number;
      Console.WriteLine("### Sipariş Stok Sistemi ###");
      Console.WriteLine();
      Console.WriteLine("1-Ürün Listesi");
      Console.WriteLine("2-Sipariş Listesi");
      Console.WriteLine("3-Kasa Durumu");
      Console.WriteLine("4-Yeni Ürün Satışı");
      Console.WriteLine("5-İşlem Sayacı");
      Console.WriteLine("6-Ürün Stok Güncelleme");
      Console.WriteLine("7-Ürün Ekle");
      Console.WriteLine("8-Sipariş İptali");
      Console.WriteLine("9-Günlük Satış Raporu");
      Console.WriteLine("10-Ürün Fiyat Güncelleme");
      Console.WriteLine("11-Çıkış");
      Console.WriteLine();
      Console.WriteLine("----------------------------------------");
      Console.WriteLine();

      Console.Write("Lütfen yapmak istediğiniz işlemi seçiniz: ");
      number = Console.ReadLine();

      if (number == "1")
      {
        Console.WriteLine("---- Ürün Listesi ----");
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        var values = context.TblProduct.ToList();
        foreach (var item in values)
        {
          Console.WriteLine(item.ProductId + "-" + item.ProductName + ", Stok Sayısı: " + item.ProductStock + ", Fiyatı:" + item.ProductPrice + " ₺");
        }
      }

      if (number == "2")
      {
        Console.WriteLine("---- Sipariş Listesi ----");
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        var values = context.TblOrder.ToList();
        foreach (var item in values)
        {
          Console.WriteLine(item.OrderId + "-" + item.TblProduct.ProductName + " Birim Fiyat: " + item.UnitPrice + " Adet:" + item.Quantity + " Toplam Fiyat: " + item.TotalPrice + " ₺");
        }
      }

      if (number == "3")
      {
        Console.WriteLine("---- Kasa Durumu ----");
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        var values = context.TblCashRegister.Select(x => x.Balance).FirstOrDefault();
        Console.Write("Kasadaki Toplam Tutar: " + values + " ₺");

      }

      if (number == "4")
      {
        Console.WriteLine("---- Yeni Ürün Sipariş Girişi ----");

        Console.Write("Müşteri Adı: ");
        string customer = Console.ReadLine();

        Console.Write("Ürün ID: ");
        int productId = int.Parse(Console.ReadLine());

        Console.Write("Ürün Adedi: ");
        int quantity = int.Parse(Console.ReadLine());

        Console.WriteLine();

        Console.WriteLine("---- Ürün Bilgileri ----");
        Console.WriteLine();

        var productName = context.TblProduct.Where(x => x.ProductId == productId).Select(y => y.ProductName).FirstOrDefault();

        Console.WriteLine("Ürün Adı: " + productName);

        var productUnitPrice = context.TblProduct.Where(x => x.ProductId == productId).Select(y => y.ProductPrice).FirstOrDefault();

        Console.WriteLine("Birim Fiyatı: " + productUnitPrice);


        decimal totalPrice = quantity * decimal.Parse(productUnitPrice.ToString());
        Console.WriteLine("Toplam Fiyat: " + totalPrice);

        Console.WriteLine();
        Console.WriteLine("---- Ürün Bilgileri ----");

        TblOrder tblOrder = new TblOrder();
        tblOrder.UnitPrice = productUnitPrice;
        tblOrder.ProductId = productId;
        tblOrder.Quantity = quantity;
        tblOrder.TotalPrice = totalPrice;
        tblOrder.Customer = customer;
        tblOrder.OrderDate = DateTime.Now;

        context.TblOrder.Add(tblOrder);
        context.SaveChanges();

        var cashRegister = context.TblCashRegister.FirstOrDefault();
        if (cashRegister != null)
        {
          cashRegister.Balance += totalPrice;
        }
        else
        {
          TblCashRegister newCash = new TblCashRegister
          {
            Balance = totalPrice
          };
          context.TblCashRegister.Add(newCash);
        }

        context.SaveChanges();

        Console.WriteLine("Sipariş başarıyla kaydedildi.");
      }

      if (number == "5")
      {
        var value = context.TblProcess.Select(x => x.Process).FirstOrDefault();
        Console.WriteLine("Toplam İşlem Sayısı: " + value);
      }

      if (number == "6")
      {
        Console.WriteLine("---- Ürün Stok Güncelleme ----");
        Console.Write("Ürün ID: ");
        int productId = int.Parse(Console.ReadLine());
        Console.Write("Yeni Stok Sayısı: ");
        int newStock = int.Parse(Console.ReadLine());
        var product = context.TblProduct.Find(productId);
        if (product != null)
        {
          product.ProductStock = newStock;
          context.SaveChanges();
          Console.WriteLine("Stok güncellemesi başarılı.");
        }
        else
        {
          Console.WriteLine("Ürün bulunamadı.");
        }
      }

      if (number == "7")
      {
        Console.WriteLine("---- Yeni Ürün Ekleme ----");
        TblProduct tblProduct = new TblProduct();
        Console.Write("Ürün Adı: ");
        tblProduct.ProductName = Console.ReadLine();
        Console.Write("Ürün Stok Sayısı: ");
        tblProduct.ProductStock = int.Parse(Console.ReadLine());
        Console.Write("Ürün Fiyatı: ");
        tblProduct.ProductPrice = decimal.Parse(Console.ReadLine());
        context.TblProduct.Add(tblProduct);
        context.SaveChanges();
        Console.WriteLine("Ürün Başarıyla Eklendi");
      }

      if (number == "8")
      {
        Console.WriteLine("---- Sipariş İptali ----");

        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.Write("İptal etmek istediğiniz Sipariş ID’sini girin: ");
        int orderId = int.Parse(Console.ReadLine());

        var order = context.TblOrder.FirstOrDefault(x => x.OrderId == orderId);

        if (order != null)
        {
          Console.WriteLine($"Sipariş: {order.OrderId} - {order.TblProduct.ProductName} - {order.Quantity} adet - {order.TotalPrice} ₺");

          Console.Write("Bu siparişi iptal etmek istediğinize emin misiniz? (E/H): ");
          string confirmation = Console.ReadLine();

          if (confirmation.ToUpper() == "E")
          {
            var cash = context.TblCashRegister.FirstOrDefault();
            cash.Balance -= order.TotalPrice;

            var product = context.TblProduct.Find(order.ProductId);
            product.ProductStock += order.Quantity;

            context.TblOrder.Remove(order);

            var process = context.TblProcess.FirstOrDefault();
            process.Process -= 1;

            context.SaveChanges();
            Console.WriteLine("Sipariş başarıyla iptal edildi.");
          }
          else
          {
            Console.WriteLine("Sipariş iptali işlemi iptal edildi.");
          }
        }
        else
        {
          Console.WriteLine("Bu ID’ye ait bir sipariş bulunamadı.");
        }

      }

      if (number == "9")
      {
        Console.WriteLine($"---- {DateTime.Today.ToString("dd.MM.yyyy")} Günü Satış Raporu ----");

        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var todaysOrders = context.TblOrder
            .Where(x => x.OrderDate != null)
            .ToList()
            .Where(x => x.OrderDate.Value.Date == DateTime.Today)
            .ToList();

        if (todaysOrders.Count == 0)
        {
          Console.WriteLine("Bugün için herhangi bir sipariş bulunamadı.");
        }
        else
        {
          decimal totalRevenue = (decimal)todaysOrders.Sum(x => x.TotalPrice);
          int totalQuantity = (int)todaysOrders.Sum(x => x.Quantity);

          Console.WriteLine($"Toplam Sipariş Sayısı: {todaysOrders.Count}");
          Console.WriteLine($"Toplam Ürün Adedi: {totalQuantity}");
          Console.WriteLine($"Toplam Ciro: {totalRevenue} ₺");
          Console.WriteLine();

          foreach (var order in todaysOrders)
          {
            Console.WriteLine($"#{order.OrderId} - {order.TblProduct.ProductName} - {order.Quantity} adet - {order.TotalPrice} ₺");
          }
        }
      }

      if (number == "10")
      {
        Console.WriteLine("---- Ürün Fiyat Güncelleme ----");
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.Write("Fiyatını güncellemek istediğiniz ürünün ID'sini girin: ");
        int productId = int.Parse(Console.ReadLine());

        var product = context.TblProduct.FirstOrDefault(x => x.ProductId == productId);

        if (product != null)
        {
          Console.WriteLine($"Ürün: {product.ProductName}");
          Console.WriteLine($"Mevcut Fiyat: {product.ProductPrice} ₺");

          Console.Write("Yeni fiyatı girin: ");
          decimal newPrice = decimal.Parse(Console.ReadLine());

          product.ProductPrice = newPrice;
          context.SaveChanges();

          Console.WriteLine("Ürün fiyatı başarıyla güncellendi.");
        }
        else
        {
          Console.WriteLine("Ürün bulunamadı.");
        }
      }
      
      if (number == "11")
      {
        Console.WriteLine("Çıkış yapılıyor...");
      }


      Console.Read();
    }
  }
}
