using AutoMapper;
using CarDealer.Dtos.Export;
using CarDealer.Dtos.Import;
using CarDealer.Models;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            CreateMap<SuppliersImportDto, Supplier>();

            CreateMap<PartsImportDto, Part>();

            CreateMap<CarsImportDto, Car>();

            CreateMap<CustomersImportDto, Customer>();

            CreateMap<SalesImportDto, Sale>();

            CreateMap<CarWithDistanceExport, Car>();

            CreateMap<CarBmwExportDto, Car>();

            CreateMap<PartCarDto, CarsImportDto>();

            CreateMap<CustomerTotalSalesDto, Customer>();


        }
    }
}
