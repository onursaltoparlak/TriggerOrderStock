# 📦 TriggerOrderStock

**TriggerOrderStock**, temel stok yönetimi ve sipariş takibini kolaylaştırmak için geliştirilen bir C# konsol uygulamasıdır. Sipariş girişi, ürün yönetimi, kasa durumu takibi, satış raporu gibi temel işlevleri kullanıcı dostu bir arayüzle sunar.

## 🛠 Özellikler

- 🧾 Sipariş Girişi ve Toplam Fiyat Hesaplama  
- 📦 Ürün Listeleme, Ekleme ve Güncelleme  
- 💵 Kasa Durumu Görüntüleme  
- 📊 Günlük Satış Raporu  
- 🎨 Renkli ve okunabilir konsol arayüzü  
- 📆 İşlem tarihini detaylı şekilde gösterme  
- 🧠 Trigger destekli işlem süreç yönetimi (DB tabanlı)

## 📷 Ekran Görüntüleri
![firstchoice](https://github.com/user-attachments/assets/81064b28-d576-45bc-b948-ca0377d9a796)

![secondchoice](https://github.com/user-attachments/assets/fd49720f-d18e-4f86-b93b-7ac4c02cfe47)

![thirdchoice](https://github.com/user-attachments/assets/474276e8-2764-484f-a050-ccc744747ce3)

![fourthchoice](https://github.com/user-attachments/assets/a9be6e32-0604-41b6-b85a-09ed3d3f0a8f)

![sixthchoice](https://github.com/user-attachments/assets/a0bcc957-cc2f-46f2-ba35-c130354489e1)

![seventhchoice](https://github.com/user-attachments/assets/0c73db7a-f2c7-4fe6-8150-d29c67f89832)

![eightchoice](https://github.com/user-attachments/assets/bb54d690-8b49-424b-b6ee-b14f4432fd04)

![ninthchoice](https://github.com/user-attachments/assets/f9b896d2-c8a4-4e42-9833-127a3b105ba3)

![lastchoice](https://github.com/user-attachments/assets/c635a21b-922c-401a-9e51-d0e67829f93a)




## 🗃️ Veritabanı Yapısı
Projede aşağıdaki temel tablolar kullanılmaktadır:

tblOrder → orderid, customer, productid, unitprice, totalprice, orderdate

tblProduct → productid, productname, unitprice, stock

tblProcess → processid, process

Not: Bu proje DB First yaklaşımıyla geliştirilmiştir.

## 🚀 Planlanan Geliştirmeler
 Ürün fiyat güncelleme modülü

 Günlük satış raporuna tarih bilgisi eklendi ✅

 Stok azaldığında otomatik uyarı sistemi

 Kullanıcı bazlı giriş/çıkış sistemi

## 🧠 Öğrenilenler / Kazanımlar
EF DB First yaklaşımıyla çalışma

Temel stok ve sipariş mantığı kurgulama

C# Console UI ile kullanıcı deneyimi geliştirme

Trigger destekli veri bütünlüğü sağlama
