using Enhetsregistret.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Enhetsregistret.Repositories
{
    public class JsonLoanRepository : ILoanRepository
    {
        private readonly string _filePath;

        public JsonLoanRepository(string filePath)
        {
            _filePath = filePath;
        }

        public void Add(Loan loan)
        {
            var currentItems = GetAll();
            currentItems.Add(loan);
            var itemsAsJson = JsonSerializer.Serialize(currentItems);
            File.WriteAllText(_filePath, itemsAsJson);
        }

        public List<Loan> GetAll()
        {
            try
            {
                var json = File.ReadAllText(_filePath);
                var list = JsonSerializer.Deserialize<List<Loan>>(json);
                if (list is null)
                {
                    throw new Exception("Den här listan innehåller fel värden. Vänligen kontrollera.");
                }
                else return list;
            }
            catch
            {
                return [];
            }
        }

        public void ReplaceAll(Loan[] enheter)
        {
            var currentItems = GetAll();
            currentItems.AddRange(enheter);
            var itemsAsJson = JsonSerializer.Serialize(currentItems);
            File.WriteAllText(_filePath, itemsAsJson);
        }

        public void Update(Loan newState)
        {
            var currentItems = GetAll();
            var itemToChangeID = newState.Id;
            var indexInList = currentItems.FindIndex(i => i.Id == itemToChangeID);
            currentItems[indexInList] = newState;
            var itemsAsJson = JsonSerializer.Serialize(currentItems);
            File.WriteAllText(_filePath, itemsAsJson);
        }
    }
}
