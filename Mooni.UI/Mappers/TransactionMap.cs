using AutoMapper;
using Mooni.Domain.Entities;
using Mooni.Domain.InputModels.Transaction;
using Mooni.Domain.ViewModels.Transaction;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace Mooni.UI.Mappers
{
    public class TransactionMap : Profile
    {
        public TransactionMap()
        {
            CreateMap<CreateTransactionInputModel, Transaction>()
                .ForPath(e => e.Amount.Value, opt => opt.MapFrom(e => ConvertCurrencyToDouble(e.Amount)))
                .ForPath(e => e.Amount.Currency, opt => opt.MapFrom(e => e.AmountCurrency))
                .ForMember(e => e.OverdueDate, opt => opt.MapFrom(s => s.Date.ToUniversalTime()))
                .ForMember(e => e.Date, opt => opt.MapFrom(s => s.AsOverdueDate ? DateTime.Now.ToUniversalTime() : s.Date))
                ;
            CreateMap<Transaction, TransactionViewModel>()
                .ForMember(e=> e.CategoryIconId,opt=> opt.MapFrom(s=> s.Category.IconId))
                .ForMember(e => e.CategoryColor, opt => opt.MapFrom(s => s.Category.Color))
                .ForMember(e => e.CategoryName, opt => opt.MapFrom(s => s.Category.Name))
                .ForMember(e => e.Date, opt => opt.MapFrom(s => s.OverdueDate))
                .ForMember(e => e.Amount, opt => opt.MapFrom(s => s.Amount.Currency+" "+s.Amount.Value.ToString("N2", CultureInfo.CurrentCulture)))
                ;
        }
        private double ConvertCurrencyToDouble(string currencyString)
        => double.Parse(currencyString, NumberStyles.AllowCurrencySymbol | NumberStyles.Currency);
    }
}
