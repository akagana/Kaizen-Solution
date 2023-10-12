using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ReceiptScanning.Models;

class Program
{
    static void Main()
    {
        List<Receipt> response = new List<Receipt>();
        //Metin Okuma işlemi için StreamReader kullanırız.
        using (StreamReader r = new StreamReader("response.json"))
        {
            //Text sonuna kadar tüm karakterleri okur ve string olarak döner.
            string json = r.ReadToEnd();
            response = JsonSerializer.Deserialize<List<Receipt>>(json);
        }

        var note = new List<BillLine>();

        var firstRow = new BillLine
        {
            Number = 1,
            Text = response[1].description,
            yValue = response[1].boundingPoly.vertices[0].y
        };
        note.Add(firstRow);

        for (int i = 2; i < response.Count(); i++)
        {
            var lastLine = note.Last();
            var y = response[i].boundingPoly.vertices[0].y;
            var searchLine = note.Where(c => Math.Abs(c.yValue - y) < 10).Select(c => c.Number).FirstOrDefault();
            if (searchLine == 0)
            {
                var justLine = new BillLine
                {
                    Number = lastLine.Number + 1,
                    Text = response[i].description,
                    yValue = response[i].boundingPoly.vertices[0].y
                };
                note.Add(justLine);
            }
            else
            {
                var selectedLine = note.Where(b => b.Number == searchLine).FirstOrDefault();
                selectedLine.Text = selectedLine.Text + " " + response[i].description;
            }
        }
        foreach (var line in note)
        {
            Console.WriteLine(line.Number + " " + line.Text);
        }
        Console.ReadLine();
    }
}