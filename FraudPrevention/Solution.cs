using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraudPrevention
{
    public class Record
    {
        public int DealID { get; set; }
        public int OrderID { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string CreditCarderNumber { get; set; }

    }

    public class Solution
    {
        static void Main(string[] args)
        {
            List<Record> records = ReadInput();
            List<string> frauds = CheckForFraud(records);
            foreach (var fraud in frauds)
            {
                Console.WriteLine(fraud);
            }

            Console.ReadLine();
        }

        private static List<string> CheckForFraud(List<Record> records)
        {
            List<string> frauds = new List<string>();

            for (int i = 0; i < records.Count; i++)
            {
                bool fraudDetected = false;
                StringBuilder sb = new StringBuilder();
                var current = records[i];
                sb.Append(current.OrderID);

                for (int j = i+1; j < records.Count-1; j++)
                {                    
                    if (current.DealID == records[j].DealID &&
                        current.State == records[j].State &&
                        current.Zip == records[j].Zip &&
                        current.Street == records[j].Street &&
                        current.City == records[j].City &&
                        current.CreditCarderNumber != records[j].CreditCarderNumber)
                    {
                        fraudDetected = true;
                        sb.Append(",");
                        sb.Append(records[j].OrderID);
                    }

                    if (current.DealID == records[j].DealID &&
                        current.Email == records[j].Email &&                        
                        current.CreditCarderNumber != records[j].CreditCarderNumber)
                    {
                        fraudDetected = true;
                        sb.Append(",");
                        sb.Append(records[j].OrderID);
                    }
                }

                sb.AppendLine();

                if (fraudDetected)
                    frauds.Add(sb.ToString());
            }

            return frauds;
        }

        private static List<Record> ReadInput()
        {
            List<Record> records = new List<Record>();

            string linesCount = Console.ReadLine();

            for (int i = 0; i < int.Parse(linesCount); i++)
            {
                string record = Console.ReadLine();
                string[] fields = record.ToUpper().Split(',');

                Record order = new Record()
                {
                    OrderID = int.Parse(fields[0]),
                    DealID = int.Parse(fields[1]),
                    Email = CleanEmail(fields[2]),
                    Street = Abbreviate(fields[3]),
                    City = Abbreviate(fields[4]),
                    State = fields[5],
                    Zip = fields[6],
                    CreditCarderNumber = fields[7]
                };

                records.Add(order);
            }

            return records;
        }

        private static string Abbreviate(string word)
        {
            if (word.Contains("ROAD"))
            {
                return word.Replace("ROAD", "Rd.");
            }

            if (word.Contains("STREET"))
            {
                return word.Replace("STREET", "St.");
            }

            if (word.Contains("ILLINOIS"))
            {
                return word.Replace("ILLINOIS", "IL");
            }

            if (word.Contains("CALIFORNIA"))
            {
                return word.Replace("CALIFORNIA", "CA");
            }

            if (word.Contains("NEW YORK"))
            {
                return word.Replace("NEW YORK", "NY");
            }

            return word;
        }

        private static string CleanEmail(string email)
        {
            string result = email.Replace(".", "");

            int plusIndex = result.IndexOf("+");
            if (plusIndex >=0)
            {
                int atIndex = result.IndexOf("@");
                result = result.Remove(plusIndex, atIndex - plusIndex);
            }

            return result;
        }
    }

    
}
