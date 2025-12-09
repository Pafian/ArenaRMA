using System;

namespace ArenaRMA.Models
{
    public class Warranty
    {
        public int Id { get; set; }

        // ALAP ADATOK
        public string WarrantyCode { get; set; }      // GARANCIA ID
        public DateTime? ReceiveDate { get; set; }    // ÉRK. DÁTUM
        public string ReceiveStatus { get; set; }     // ERK.
        public string ReturnStatus { get; set; }      // VISZAK.
        public DateTime? ReturnDate { get; set; }     // VK. DÁTÚ
        public string Location { get; set; }          // HOL VAN?
        public string State { get; set; }             // ÁLLAPOT
        public string Technician { get; set; }        // TECHNIKUS

        // VEVŐ ADATOK
        public string InvoiceNumber { get; set; }     // SZÁMLA SZÁM
        public string BuyerCode { get; set; }         // VEVŐ AZONOSÍTÓ
        public string BuyerType { get; set; }         // VEVŐ TÍP.
        public string BuyerName { get; set; }         // VEVŐ NEVE
        public string BuyerPhone { get; set; }        // VEVŐ TEL.
        public string BuyerEmail { get; set; }        // VEVŐ EMAIL
        public string BuyerAddress { get; set; }      // VEVŐ CÍM

        // TERMÉK ADATOK
        public string ProductName { get; set; }       // TERMÉK MEGNEVEZÉS
        public string ProductIKEN { get; set; }       // IKÉN
        public string SerialNumber { get; set; }      // SERIAL NUM.
        public string Adapter { get; set; }           // ADAPTER

        // HIBA / MUNKA
        public string ErrorDescription { get; set; }  // HIBA LEÍRÁS
        public string WorkDone { get; set; }          // ELVÉGZETT MUNKA
        public string ReplacementPart { get; set; }   // CSERE IWS / cikkszám
        public string ReplacementProduct { get; set; }// CSERETERMÉK CÉLÉPC

        // EGYÉB
        public string ERPField { get; set; }          // ERP mező

        public DateTime CreatedAt { get; set; }
    }
}
