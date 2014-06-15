using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BussinessLogic.DTOs.Factures;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace BussinessLogic.Helpers
{
    public static class PDFMaker
    {
        public static MemoryStream createPDF(Document pdf, MemoryStream output, FactureDto facture, Exchange exchange)
        {
            try
            {
                PdfWriter.GetInstance(pdf, output);

                pdf.Open();

                var polish = BaseFont.CP1250;
                BaseFont bf = BaseFont.CreateFont("c:/windows/fonts/arial.ttf", polish, BaseFont.EMBEDDED);

                var boldTableFont = new Font(bf, 10, Font.BOLD);
                var normalTableFont = new Font(bf, 9, Font.NORMAL);
                var itemBoldTableFontTable = new Font(bf, 8, Font.BOLDITALIC);
                var itemNormalTableFontTable = new Font(bf, 7, Font.NORMAL);
                var sumBoldTableFont = new Font(bf, 8, Font.BOLD);
                var sumNormalTableFont = new Font(bf, 7, Font.BOLD);
                var sumTableFont = new Font(bf, 11, Font.BOLD);

                var p = new Paragraph("Data wystawienia: " + facture.CreationTime, new Font(bf, 9, Font.NORMAL));
                p.Alignment = Element.ALIGN_RIGHT;
                pdf.Add(p);

                p = new Paragraph("Miejsce wystawienia: " + facture.CreationPlace, new Font(bf, 9, Font.NORMAL));
                p.Alignment = Element.ALIGN_RIGHT;
                pdf.Add(p);

                p = new Paragraph("Faktura VAT", new Font(bf, 20, Font.NORMAL));
                p.Alignment = Element.ALIGN_CENTER;
                pdf.Add(p);

                p = new Paragraph("FV " + facture.FactureNr, new Font(bf, 16, Font.NORMAL));
                p.Alignment = Element.ALIGN_CENTER;
                pdf.Add(p);

                p = new Paragraph("Oryginał/Kopia", new Font(bf, 10, Font.NORMAL));
                p.Alignment = Element.ALIGN_CENTER;
                pdf.Add(p);

                pdf.Add(Chunk.NEWLINE);

                var infoTable = new PdfPTable(4);
                infoTable.HorizontalAlignment = 1;
                infoTable.DefaultCell.Border = 0;
                infoTable.SpacingBefore = 10;
                infoTable.SpacingAfter = 10;
                infoTable.SetWidths(new int[] { 1, 1, 1, 1 });

                infoTable.AddCell(new Phrase("Sprzedawca nazwa:", boldTableFont));
                infoTable.AddCell(new Phrase(facture.ProviderName, normalTableFont));

                infoTable.AddCell(new Phrase("Nabywca nazwa:", boldTableFont));
                infoTable.AddCell(new Phrase(facture.ClientName, normalTableFont));

                infoTable.AddCell(new Phrase("Sprzedawca adres:", boldTableFont));
                infoTable.AddCell(new Phrase(facture.ProviderAddress, normalTableFont));

                infoTable.AddCell(new Phrase(new Phrase("Nabywca adres:", boldTableFont)));
                infoTable.AddCell(new Phrase(facture.ClientAddress, normalTableFont));

                infoTable.AddCell(new Phrase("Sprzedawca NIP:", boldTableFont));
                infoTable.AddCell(new Phrase(facture.ProviderNIP, normalTableFont));

                infoTable.AddCell(new Phrase("Nabywca NIP:", boldTableFont));
                infoTable.AddCell(new Phrase(facture.ClientNIP, normalTableFont));

                infoTable.AddCell(new Phrase("Sprzedawca informacja:", boldTableFont));
                infoTable.AddCell(new Phrase(facture.ProviderInfo, normalTableFont));

                infoTable.AddCell(new Phrase("Nabywca informacja:", boldTableFont));
                infoTable.AddCell(new Phrase(facture.ClientInfo, normalTableFont));

                pdf.Add(infoTable);

                infoTable = new PdfPTable(4);
                infoTable.HorizontalAlignment = 1;
                infoTable.DefaultCell.Border = 0;
                infoTable.SpacingBefore = 10;
                infoTable.SpacingAfter = 10;
                infoTable.SetWidths(new int[] { 1, 1, 1, 1 });

                infoTable.AddCell(new Phrase("Numer konta bankowego:", boldTableFont));
                infoTable.AddCell(new Phrase(facture.ProviderBankAccountNumber, normalTableFont));

                infoTable.AddCell(new Phrase("Data realizacji:", boldTableFont));
                infoTable.AddCell(new Phrase(facture.RealizationTime.ToString(), normalTableFont));

                infoTable.AddCell(new Phrase("Dane do przelewu:", boldTableFont));
                infoTable.AddCell(new Phrase(facture.ProviderBankInfo, normalTableFont));

                infoTable.AddCell(new Phrase("Termin płatności:", boldTableFont));
                infoTable.AddCell(new Phrase(facture.ExpirationTime.ToString(), normalTableFont));

                pdf.Add(infoTable);

                pdf.Add(Chunk.NEWLINE);

                PdfPTable table = new PdfPTable(9);
                table.HorizontalAlignment = 1;
                infoTable.SpacingBefore = 10;
                infoTable.SpacingAfter = 10;
                table.SetWidths(new int[] { 2, 1, 1, 1, 1, 1, 1, 1, 1 });
                var cell = new PdfPCell(new Phrase("nazwa", itemBoldTableFontTable));
                cell.Border = 1;
                cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("PKWiU", itemBoldTableFontTable));
                cell.Border = 1;
                cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("ilość", itemBoldTableFontTable));
                cell.Border = 1;
                cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("j.m.", itemBoldTableFontTable));
                cell.Border = 1;
                cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("netto za szt", itemBoldTableFontTable));
                cell.Border = 1;
                cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("stawka VAT", itemBoldTableFontTable));
                cell.Border = 1;
                cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("wartość netto", itemBoldTableFontTable));
                cell.Border = 1;
                cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("wartość VAT", itemBoldTableFontTable));
                cell.Border = 1;
                cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("wartość brutto", itemBoldTableFontTable));
                cell.Border = 1;
                cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                table.AddCell(cell);

                var taxes = new Dictionary<double, TaxSum>();

                foreach (var item in facture.Items)
                {
                    cell = new PdfPCell(new Phrase(item.Name, itemNormalTableFontTable));
                    cell.Border = 1;
                    cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase(item.PKWiU, itemNormalTableFontTable));
                    cell.Border = 1;
                    cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase(item.Quantity.ToString(), itemNormalTableFontTable));
                    cell.Border = 1;
                    cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase(item.SaleTypeName, itemNormalTableFontTable));
                    cell.Border = 1;
                    cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase(exchange.calc(item.PricePerUnit), itemNormalTableFontTable));
                    cell.Border = 1;
                    cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase(item.TaxValue.ToString("P"), itemNormalTableFontTable));
                    cell.Border = 1;
                    cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase(exchange.calc(item.PriceWithoutTax), itemNormalTableFontTable));
                    cell.Border = 1;
                    cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase(exchange.calc(item.PriceTax), itemNormalTableFontTable));
                    cell.Border = 1;
                    cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase(exchange.calc(item.PriceWithTax), itemNormalTableFontTable));
                    cell.Border = 1;
                    cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                    table.AddCell(cell);

                    if (taxes.ContainsKey(item.TaxValue))
                    {
                        TaxSum t;
                        taxes.TryGetValue(item.TaxValue, out t);
                        t.add(item.PriceWithoutTax, item.PriceTax, item.PriceWithTax);
                        taxes.Remove(item.TaxValue);
                        taxes.Add(item.TaxValue, t);
                    }
                    else
                    {
                        var t = new TaxSum();
                        t.add(item.PriceWithoutTax, item.PriceTax, item.PriceWithTax);
                        taxes.Add(item.TaxValue, t);
                    }
                }

                pdf.Add(table);

                infoTable = new PdfPTable(2);
                infoTable.HorizontalAlignment = 1;
                infoTable.DefaultCell.Border = 0;
                infoTable.SpacingBefore = 10;
                infoTable.SpacingAfter = 10;
                infoTable.SetWidths(new int[] { 1, 1 });


                var table1 = new PdfPTable(2);
                table1.DefaultCell.Border = 0;
                table1.HorizontalAlignment = 0;
                table1.SpacingBefore = 10;
                table1.SetWidths(new int[] { 1, 1 });

                table1.AddCell(new Phrase("Zapłacono:", boldTableFont));
                table1.AddCell(new Phrase(exchange.calc(facture.Paid) + " " + exchange.code, normalTableFont));

                table1.AddCell(new Phrase("Do zapłaty:", boldTableFont));
                table1.AddCell(new Phrase(exchange.calc(facture.Remain) + " " + exchange.code, normalTableFont));

                table1.AddCell(new Phrase("Forma płatnosci:", boldTableFont));
                table1.AddCell(new Phrase(facture.Payment, normalTableFont));



                var table2 = new PdfPTable(4);
                table2.HorizontalAlignment = 2;
                table2.SpacingBefore = 10;
                table2.SetWidths(new int[] { 1, 1, 1, 2 });

                cell = new PdfPCell(new Phrase("Stawka VAT", itemBoldTableFontTable));
                cell.Border = 1;
                cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                table2.AddCell(cell);
                cell = new PdfPCell(new Phrase("Podsuma netto", itemBoldTableFontTable));
                cell.Border = 1;
                cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                table2.AddCell(cell);
                cell = new PdfPCell(new Phrase("Podsuma VAT", itemBoldTableFontTable));
                cell.Border = 1;
                cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                table2.AddCell(cell);
                cell = new PdfPCell(new Phrase("Podsuma brutto", itemBoldTableFontTable));
                cell.Border = 1;
                cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                table2.AddCell(cell);

                foreach (var key in taxes.Keys)
                {
                    TaxSum t;
                    taxes.TryGetValue(key, out t);

                    cell = new PdfPCell(new Phrase(key.ToString("P"), sumNormalTableFont));
                    cell.Border = 1;
                    cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                    table2.AddCell(cell);
                    cell = new PdfPCell(new Phrase(exchange.calc(t.netto), sumNormalTableFont));
                    cell.Border = 1;
                    cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                    table2.AddCell(cell);
                    cell = new PdfPCell(new Phrase(exchange.calc(t.vat), sumNormalTableFont));
                    cell.Border = 1;
                    cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                    table2.AddCell(cell);
                    cell = new PdfPCell(new Phrase(exchange.calc(t.brutto), sumNormalTableFont));
                    cell.Border = 1;
                    cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                    table2.AddCell(cell);
                }

                cell = new PdfPCell(new Phrase("RAZEM", sumBoldTableFont));
                cell.Border = 2;
                cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                table2.AddCell(cell);
                cell = new PdfPCell(new Phrase(exchange.calc(facture.SumWithoutTax), sumBoldTableFont));
                cell.Border = 2;
                cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                table2.AddCell(cell);
                cell = new PdfPCell(new Phrase(exchange.calc(facture.SumOfTax), sumBoldTableFont));
                cell.Border = 2;
                cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                table2.AddCell(cell);
                cell = new PdfPCell(new Phrase(exchange.calc(facture.SumWithTax) + " " + exchange.code, sumTableFont));
                cell.Border = 3;
                cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                table2.AddCell(cell);

                infoTable.AddCell(table1);
                infoTable.AddCell(table2);

                pdf.Add(infoTable);


                var table3 = new PdfPTable(2);
                table3.HorizontalAlignment = 1;
                table3.DefaultCell.Border = 0;
                table3.SetWidths(new int[] { 1, 9 });

                table3.AddCell(new Phrase("Słownie:", boldTableFont));
                table3.AddCell(new Phrase(facture.SumString, normalTableFont));

                pdf.Add(table3);

                pdf.Add(Chunk.NEWLINE);
                pdf.Add(Chunk.NEWLINE);

                infoTable = new PdfPTable(3);
                infoTable.HorizontalAlignment = Element.ALIGN_CENTER;
                infoTable.SpacingBefore = 10;
                infoTable.SpacingAfter = 10;
                infoTable.DefaultCell.Border = 0;
                infoTable.SetWidths(new int[] { 3, 1, 3 });

                cell = new PdfPCell(new Phrase("podpis wystawiającego", normalTableFont));
                cell.Border = 1;
                cell.VerticalAlignment = Element.ALIGN_BOTTOM;
                cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                infoTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(""));
                cell.Border = 0;
                infoTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("podpis odbierającego", normalTableFont));
                cell.Border = 1;
                cell.VerticalAlignment = Element.ALIGN_BOTTOM;
                cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                infoTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Wystawił: " + facture.Issuer, normalTableFont));
                cell.Border = 0;
                cell.MinimumHeight = 30;
                cell.VerticalAlignment = Element.ALIGN_LEFT;
                cell.HorizontalAlignment = 0; //0=do lewej, 1=wycentrowane, 2=do prawej
                infoTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(""));
                cell.Border = 0;
                cell.MinimumHeight = 30;
                infoTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Odebrał: " + facture.Reciver, normalTableFont));
                cell.Border = 0;
                cell.MinimumHeight = 30;
                cell.VerticalAlignment = Element.ALIGN_LEFT;
                cell.HorizontalAlignment = 1; //0=do lewej, 1=wycentrowane, 2=do prawej
                infoTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Adnotacje:", normalTableFont));
                cell.Border = 1;
                cell.Colspan = 3;
                cell.MinimumHeight = 100;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.HorizontalAlignment = 0; //0=do lewej, 1=wycentrowane, 2=do prawej
                infoTable.AddCell(cell);

                pdf.Add(infoTable);
            }
            catch (DocumentException de)
            {
                var error = "Nie udało się nawiązać wygenerować dokumentu PDF: " + de.Message;
                return null;
            }finally
            {
                pdf.Close();
            }
            return output;
        }
    }
}
